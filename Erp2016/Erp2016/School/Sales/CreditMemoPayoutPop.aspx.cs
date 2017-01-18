using System;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.Sales
{
    public partial class CreditMemoPayoutPop : PageBase
    {
        private int Id { get; set; }

        public CreditMemoPayoutPop() : base((int)CConstValue.Menu.CreditMemo)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // CreditMemoId in new or CreditMemoPayoutId in modify
            Id = Convert.ToInt32(Request["id"]);

            if (!IsPostBack)
            {
                FileDownloadList1.InitFileDownloadList((int)CConstValue.Upload.CreditMemo);

                CreditMemoPayout1.SetCreditVisible(false);
                CreditMemoPayout1.SetReadonly(false);

                // new
                if (Request["type"] == "0")
                {
                    //
                }
                // modify
                else
                {
                    var cCreditMemoPayout = new CCreditMemoPayout().Get(Id);
                    RadNumericTextBoxAmount.Value = (double)cCreditMemoPayout.Amount;
                    RadNumericTextBoxAmount.ReadOnly = true;

                    CreditMemoPayout1.SetData(cCreditMemoPayout);

                    // get Filedownload List
                    FileDownloadList1.GetFileDownload(Id);
                }
            }
        }

        protected void RadToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Text)
            {
                case "TempSave":
                case "Request":
                    if (IsValid)
                    {
                        var cC = new CCreditMemoPayout();
                        var c = new Erp2016.Lib.CreditMemoPayout();

                        // new
                        if (Request["type"] == "0")
                        {
                            c = new Erp2016.Lib.CreditMemoPayout();
                            c.CreatedId = CurrentUserId;
                            c.CreatedDate = DateTime.Now;
                            c.CreditMemoId = Id;
                        }
                        // modify
                        else
                        {
                            c = cC.Get(Id);
                        }

                        c = CreditMemoPayout1.GetData(c);
                        c.Amount = (decimal)RadNumericTextBoxAmount.Value;
                        c.IsActive = true;
                        c.SiteLocationId = CurrentSiteLocationId;

                        int creditMemoPayoutId;

                        // new
                        if (Request["type"] == "0")
                        {
                            decimal availableAmount = new CCreditMemo().GetAvailableCreditAmount(Id);
                            if (availableAmount >= c.Amount)
                            {
                                decimal originalAmount = new CCreditMemo().GetOriginalCreditAmount(Id);
                                if ((originalAmount - availableAmount) + c.Amount < 0)
                                    ShowMessage("result of available amount can't be negative amount");
                                else
                                {
                                    creditMemoPayoutId = cC.Add(c);

                                    // save file
                                    FileDownloadList1.SaveFile(Id);

                                    if (e.Item.Text == "TempSave")
                                    {
                                        RunClientScript("Close();");
                                    }
                                    else
                                    {
                                        if (SetReqeust(creditMemoPayoutId))
                                            RunClientScript("Close();");
                                        else
                                            ShowMessage("error requesting");
                                    }
                                }

                            }
                            else
                                ShowMessage("paid amount is bigger than available credit amount");
                        }
                        // modify
                        else
                        {
                            c.UpdatedId = CurrentUserId;
                            c.UpdatedDate = DateTime.Now;
                            cC.Update(c);

                            creditMemoPayoutId = c.CreditMemoPayoutId;

                            // save file
                            FileDownloadList1.SaveFile(Id);

                            if (SetReqeust(creditMemoPayoutId))
                                RunClientScript("Close();");
                            else
                                ShowMessage("error requesting");
                        }
                    }
                    break;

                case "Close":
                    RunClientScript("Close();");
                    break;
            }
        }

        private bool SetReqeust(int creditMemoPayoutId)
        {
            var cApprovalHistory = new CApprovalHistory();
            cApprovalHistory.DelApprovalHistory((int)CConstValue.Approval.CreditMemoPayout, creditMemoPayoutId);

            // approve request 
            var approval = new CApproval().ApproveRequstCreate((int)CConstValue.Approval.CreditMemoPayout, CurrentUserId, creditMemoPayoutId);
            if (approval > 0)
            {
                var cP = new CCreditMemoPayout();
                var c = cP.Get(creditMemoPayoutId);
                c.ApprovalStatus = approval;
                c.ApprovalId = CurrentUserId;
                c.ApprovalDate = DateTime.Now;
                //c.ApprovalMemo = "";
                cP.Update(c);

                new CMail().SendMail(CConstValue.Approval.CreditMemoPayout, CConstValue.MailStatus.ToApproveUser, c.CreditMemoPayoutId, string.Empty, CurrentUserId);

                return true;

            }
            return false;
        }
    }
}