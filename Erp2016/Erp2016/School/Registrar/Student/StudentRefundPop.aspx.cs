using System;
using System.Data;
using Erp2016.Lib;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;

namespace School.Registrar.Student
{
    public partial class StudentRefundPop : PageBase
    {
        public int InvoiceId { get; set; }

        public StudentRefundPop() : base((int)CConstValue.Menu.Student)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InvoiceId = Convert.ToInt32(Request["id"]);

            if (!IsPostBack)
            {
                FileDownloadList1.InitFileDownloadList((int)CConstValue.Upload.Refund);
                RefundInfo1.InitReundInfo(InvoiceId, CurrentSiteLocationId, true);
            }
        }

        protected void ToolbarButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Text)
            {
                case "Request":
                    if (IsValid)
                    {
                        var cInvoice = new CInvoice();
                        var original = cInvoice.Get(InvoiceId);
                        if (original != null)
                        {
                            original.Status = (int)CConstValue.InvoiceStatus.Invoiced_Hold;
                            original.UpdatedId = CurrentUserId;

                            if (cInvoice.Update(original))
                            {
                                var cRefundInvoice = new CInvoice();
                                var refundInvoice = new Erp2016.Lib.Invoice();
                                CGlobal.Copy(original, refundInvoice);
                                refundInvoice.OriginalInvoiceId = original.InvoiceId;
                                switch (original.InvoiceType)
                                {
                                    case (int)CConstValue.InvoiceType.General:
                                    case (int)CConstValue.InvoiceType.Simple:
                                    case (int)CConstValue.InvoiceType.Manual:
                                        refundInvoice.InvoiceType = (int)CConstValue.InvoiceType.Refund_RF;
                                        break;
                                    case (int)CConstValue.InvoiceType.Homestay:
                                        refundInvoice.InvoiceType = (int)CConstValue.InvoiceType.Refund_HR;
                                        break;
                                    case (int)CConstValue.InvoiceType.Dormitory:
                                        refundInvoice.InvoiceType = (int)CConstValue.InvoiceType.Refund_DR;
                                        break;
                                }
                                refundInvoice.Status = (int)CConstValue.InvoiceStatus.Pending;
                                refundInvoice.CreatedId = CurrentUserId;
                                refundInvoice.CreatedDate = DateTime.Now;
                                
                                var invoiceId = cRefundInvoice.Add(refundInvoice);
                                if (invoiceId > 0)
                                {
                                    var refundInvoiceItems = new CInvoiceItem();
                                    refundInvoiceItems.RefundItemsUpdate(original.InvoiceId, invoiceId, Convert.ToDecimal(RefundInfo1.GetRefundRate().Value), CurrentUserId);

                                    var cCreditMemo = new CCreditMemo();
                                    var creditMemo = new CreditMemo();

                                    creditMemo.CreditMemoType = (int)CConstValue.CreditMemoType.Refund;
                                    creditMemo.InvoiceId = invoiceId; //Refund Invoice Id
                                    creditMemo.OriginalCreditMemoAmount = 0;
                                    creditMemo.CreatedId = CurrentUserId;
                                    creditMemo.CreatedDate = DateTime.Now;
                                    creditMemo.IsActive = false;

                                    var creditMemoId = cCreditMemo.Add(creditMemo);
                                    if (creditMemoId > 0)
                                    {
                                        var cCreditMemoPayout = new CCreditMemoPayout();
                                        var creditMemoPayout = new CreditMemoPayout();

                                        creditMemoPayout.CreditMemoId = creditMemoId;
                                        creditMemoPayout.Amount = 0;
                                        creditMemoPayout.IsActive = false;
                                        creditMemoPayout.CreatedId = CurrentUserId;
                                        creditMemoPayout.CreatedDate = DateTime.Now;

                                        var creditMemoPayoutId = cCreditMemoPayout.Add(creditMemoPayout);
                                        if (creditMemoPayoutId > 0)
                                        {
                                            var cRefund = new CRefund();
                                            var refund = new Refund();

                                            refund.CreditMemoPayoutId = creditMemoPayoutId;
                                            refund.InvoiceId = invoiceId;
                                            refund.RefundDate = Convert.ToDateTime(RefundInfo1.RadActualDate().SelectedDate);
                                            refund.RefundRate = Convert.ToDouble(RefundInfo1.GetRefundRate().Value);
                                            refund.RefundReason = RefundInfo1.GetReason().Text;
                                            refund.IsActive = false;
                                            refund.CreatedId = CurrentUserId;
                                            refund.CreatedDate = DateTime.Now;

                                            if (cRefund.Add(refund) > 0)
                                            {
                                                // save uploading file
                                                FileDownloadList1.SaveFile(refund.RefundId);

                                                RunClientScript("Close();");
                                            }
                                            ShowMessage("failed to update inqury (Add Refund Info)");
                                        }
                                        else
                                            ShowMessage("failed to update inqury (Add CreditMemoPayout)");
                                    }
                                    else
                                        ShowMessage("failed to update inqury (Add CreditMemo)");
                                }
                                else
                                    ShowMessage("failed to update inqury (Add Refund Invoice)");
                            }
                            else
                                ShowMessage("failed to update inqury (Update Original Invoice)");
                        }
                        else
                            ShowMessage("failed to update inqury (Original Invoice is null)");
                    }
                    break;
                case "Close":
                    RunClientScript("Close();");
                    break;
            }
        }

    }
}