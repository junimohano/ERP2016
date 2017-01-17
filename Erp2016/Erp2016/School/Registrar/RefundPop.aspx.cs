using System;
using System.Collections.Specialized;
using System.Data;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;
using System.Web.UI;

namespace School.Registrar
{
    public partial class RefundPop : PageBase
    {
        private int RefundId { get; set; }

        private LinqDataSource _sqlDataSourceInvoiceItems;
        private RadGrid _radGridInvoiceItems;

        public RefundPop() : base((int)CConstValue.Menu.Refund)
        {
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request["__EVENTTARGET"] == "btnNew" &&
            //Request["__EVENTARGUMENT"] == "MyArgument")
            //{
            //    //do something
            //}

            // Refund_R
            InvoiceItemGrid1.SetTypeOfInvoiceCoaItem(2);

            RefundId = Convert.ToInt32(Request["id"]);

            // find user control
            _sqlDataSourceInvoiceItems = InvoiceItemGrid1.GetSqlDataSourceInvoiceItems();
            _radGridInvoiceItems = InvoiceItemGrid1.GetRadGridInvoiceItems();
            // connect event of invoice Items.
            _radGridInvoiceItems.PreRender += _radGridInvoiceItems_PreRender;
            //_radGridInvoiceItems.MasterTableView.CommandItemSettings.ShowSaveChangesButton = false;

            FileDownloadList1.InitFileDownloadList((int)CConstValue.Upload.Refund);

            CreditMemoPayout1.SetReadonly(false);

            if (!IsPostBack)
            {
                //// new
                //if (Request["type"] == "0")
                //{
                //    // nothing
                //}
                //else
                //{
                var cRefund = new CRefund();
                var refund = cRefund.Get(RefundId);
                FileDownloadList1.GetFileDownload(refund.RefundId);

                InvoiceItemGrid1.InvoiceId = refund.InvoiceId;
                InvoiceItemGrid1.SetEditMode(true);

                CreditMemoPayout1.SetCreditVisible(true);
                var cCreditMemoPayout = new CCreditMemoPayout();
                var creditMemoPayout = cCreditMemoPayout.Get(refund.CreditMemoPayoutId);
                CreditMemoPayout1.SetData(creditMemoPayout);
                //}
            }
        }

        private void _radGridInvoiceItems_PreRender(object sender, EventArgs e)
        {
            _sqlDataSourceInvoiceItems.WhereParameters.Clear();
            _sqlDataSourceInvoiceItems.WhereParameters.Add("InvoiceId", DbType.Int32, InvoiceItemGrid1.InvoiceId.ToString());
            _sqlDataSourceInvoiceItems.Where = "InvoiceId == @InvoiceId";
        }

        protected void RadToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Text)
            {
                case "TempSave":
                case "Request":
                    if (IsValid)
                    {
                        var cRefund1 = new CRefund();
                        var refund1 = cRefund1.Get(RefundId);

                        FileDownloadList1.SaveFile(refund1.RefundId);

                        var cCreditMemoPayout = new CCreditMemoPayout();
                        var creditMemoPayout = cCreditMemoPayout.Get(refund1.CreditMemoPayoutId);
                        creditMemoPayout = CreditMemoPayout1.GetData(creditMemoPayout);
                        creditMemoPayout.UpdatedId = CurrentUserId;

                        if (cCreditMemoPayout.Update(creditMemoPayout))
                        {
                            if (cRefund1.Update(refund1))
                            {
                                if (e.Item.Text == "TempSave")
                                {
                                    RunClientScript("Close();");
                                }
                                else
                                {
                                    var cApprovalHistory = new CApprovalHistory();
                                    cApprovalHistory.DelApprovalHistory((int)CConstValue.Approval.Refund, RefundId);

                                    // approve request 
                                    var approval = new CApproval().ApproveRequstCreate((int)CConstValue.Approval.Refund, CurrentUserId, RefundId);
                                    if (approval > 0)
                                    {
                                        var cRefund = new CRefund();
                                        var refund = cRefund.Get(RefundId);
                                        refund.ApprovalStatus = approval;
                                        refund.ApprovalId = CurrentUserId;
                                        refund.ApprovalDate = DateTime.Now;
                                        //packageProgram.ApprovalMemo = "";
                                        cRefund.Update(refund);

                                        new CMail().SendMail(CConstValue.Approval.Refund, CConstValue.MailStatus.ToApproveUser, refund.RefundId, string.Empty, CurrentUserId);

                                        RunClientScript("Close();");
                                    }
                                    else
                                        ShowMessage("error requesting");
                                }
                            }
                            else
                                ShowMessage("Error updating refund");
                        }
                        else
                            ShowMessage("Error updating creditMemoPayout");
                    }
                    else
                        ShowMessage("Error can't find refund");
                    break;

                case "Close":
                    RunClientScript("Close();");
                    break;
            }
        }


    }
}