using System;
using System.Data;
using Erp2016.Lib;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;

namespace School.Registrar.Student
{
    public partial class StudentTransferPop : PageBase
    {
        public int InvoiceId { get; set; }

        public StudentTransferPop() : base((int)CConstValue.Menu.Student)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InvoiceId = Convert.ToInt32(Request["id"]);

            if (!IsPostBack)
            {
                FileDownloadList1.InitFileDownloadList((int)CConstValue.Upload.Transfer);
                RefundInfo1.InitReundInfo(InvoiceId, CurrentSiteLocationId, true);


                //////////////
                tbRequestDate.SelectedDate = DateTime.Now;

                var global = new CGlobal();

                ddlSite.DataSource = global.GetSiteId();
                ddlSite.DataTextField = "Name";
                ddlSite.DataValueField = "Value";
                ddlSite.DataBind();
                ddlSite.Items.Insert(0, new RadComboBoxItem("- Select School -", "0"));

                ddlSiteLocation.Items.Insert(0, new RadComboBoxItem("-Select Campus-", "0"));
            }
        }

        protected void ToolbarButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Text)
            {
                case "Request":
                    if (IsValid)
                    {
                        var cOriginalInvoice = new CInvoice();
                        var original = cOriginalInvoice.Get(InvoiceId);

                        if (original != null)
                        {
                            original.Status = (int)CConstValue.InvoiceStatus.Invoiced_Hold; //Invoiced(Hold)
                            if (cOriginalInvoice.Update(original))
                            {
                                var originalInvoiceId = original.InvoiceId;

                                var cTransferInvoice = new CInvoice();
                                var transferInvoice = new Invoice();

                                transferInvoice.OriginalInvoiceId = originalInvoiceId;
                                transferInvoice.InvoiceType = (int)CConstValue.InvoiceType.Refund_TF; //Transfer Invoice

                                transferInvoice.StudentId = original.StudentId;
                                transferInvoice.AgencyId = original.AgencyId;
                                transferInvoice.ProgramRegistrationId = original.ProgramRegistrationId;
                                transferInvoice.HomestayRegistrationId = original.HomestayRegistrationId;
                                transferInvoice.ScholarshipId = original.ScholarshipId;
                                transferInvoice.PromotionId = original.PromotionId;
                                transferInvoice.SiteLocationId = original.SiteLocationId;

                                transferInvoice.IsFinancialGurantee = original.IsFinancialGurantee;

                                transferInvoice.Status = (int)CConstValue.InvoiceStatus.Pending; // Pending
                                transferInvoice.CreatedId = CurrentUserId;
                                transferInvoice.CreatedDate = DateTime.Now;

                                var invoiceId = cTransferInvoice.Add(transferInvoice);

                                if (invoiceId > 0)
                                {
                                    var transferInvoiceItems = new CInvoiceItem();

                                    //if (transferInvoiceItems.TransferCancelItemsUpdate(originalInvoiceId, invoiceId, Convert.ToInt32(tbTotalDaysOfProgram.Value), Convert.ToInt32(tbTransferDays.Value), CurrentUserId))
                                    //{
                                    //    var refundAmt = transferInvoiceItems.TotalAmount(invoiceId);

                                    //    var cCreditMemo = new CCreditMemo();
                                    //    var creditMemo = new CreditMemo();

                                    //    creditMemo.CreditMemoType = (int)CConstValue.CreditType.Refund; //Transfer
                                    //    creditMemo.InvoiceId = invoiceId; // Transfer Invoice Id
                                    //    creditMemo.OriginalCreditMemoAmount = Math.Abs(refundAmt);
                                    //    creditMemo.CreatedId = CurrentUserId;
                                    //    creditMemo.CreatedDate = DateTime.Now;
                                    //    creditMemo.IsActive = false;

                                    //    var creditMemoId = cCreditMemo.Add(creditMemo);

                                    //    if (creditMemoId > 0)
                                    //    {
                                    //        //var cBCTinfo = new CBCT();
                                    //        //var BCTinfo = new BCT();

                                    //        //BCTinfo.CreditMemoId = creditMemoId;
                                    //        //BCTinfo.InvoiceId = invoiceId;
                                    //        //BCTinfo.RequestDate = Convert.ToDateTime(tbRequestDate.SelectedDate);
                                    //        //BCTinfo.ActualBCTdate = Convert.ToDateTime(tbTransferDate.SelectedDate);
                                    //        //BCTinfo.BCTamount = Math.Abs(refundAmt);
                                    //        //BCTinfo.CreatedId = CurrentUserId;

                                    //        //BCTinfo.ApprovalStatus = (int)CConstValue.ApprovalStatus.Requested;
                                    //        ////payout status > Requested:2 Progress:3 Approved:99 Reject:98 Revise:97
                                    //        //BCTinfo.Reason = tbTransferReason.Text;

                                    //        //BCTinfo.TransferToDepartmentId = Convert.ToInt32(ddlDepartment.SelectedValue);

                                    //        //BCTinfo.TransferDays = Convert.ToInt32(tbTransferDays.Value);
                                    //        //BCTinfo.TransferTotalDayTaken = Convert.ToInt32(tbTotalTakenDays.Value);
                                    //        //BCTinfo.TransferTotalDaysOfProgram = Convert.ToInt32(tbTotalDaysOfProgram.Value);
                                    //        //var BCTid = cBCTinfo.Add(BCTinfo, CConstValue.BctTypeTransfer);
                                    //        //if (BCTid > 0)
                                    //        //{
                                    //        //    var approval = new CApproval();
                                    //        //    if (approval.ApproveRequstCreate(CConstValue.ApprovalBct, CurrentUserId, BCTid) > 0)
                                    //        //    {
                                    //        //        RunClientScript("Close();");
                                    //        //    }
                                    //        //    else
                                    //        //    {
                                    //        //        ShowMessage("failed to update inqury (BCT(Transfer) ApprovalHistory)");
                                    //        //    }
                                    //        //}
                                    //        //else
                                    //        //{
                                    //        //    ShowMessage("failed to update inqury (BCT(Transfer))");
                                    //        //}
                                    //    }
                                    //    else
                                    //    {
                                    //        ShowMessage("failed to update inqury (Credit Memo)");
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    ShowMessage("failed to update inqury (Invoice Items)");
                                    //}
                                }
                                else
                                {
                                    ShowMessage("failed to update inqury (transfer Invoice)");
                                }
                            }
                            else
                            {
                                ShowMessage("failed to update inqury (Update Original Invoice)");
                            }
                        }
                        else
                        {
                            ShowMessage("failed to update inqury (Original Invoice is null)");
                        }
                    }
                    break;
                case "Close":
                    RunClientScript("Close();");
                    break;
            }
        }

        protected void tbTransferDate_SelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            var cInvoice = new CInvoice();
            var qryInvoice = cInvoice.Get(InvoiceId);
            var cQryProRegi = new CProgramRegistration();
            var qryProRegi = cQryProRegi.Get(Convert.ToInt32(qryInvoice.ProgramRegistrationId));

            var TransferDate = Convert.ToDateTime(tbTransferDate.SelectedDate);

            var StartDate = Convert.ToDateTime(qryProRegi.StartDate);
            var EndDate = Convert.ToDateTime(qryProRegi.EndDate);
            var totalProgramDays = EndDate - StartDate;
            var totalTakenDays = new TimeSpan();
            var totalTransferDays = new TimeSpan();

            if (TransferDate <= StartDate)
            {
                totalTransferDays = totalProgramDays;
            }
            else
            {
                totalTakenDays = TransferDate - StartDate;
                totalTransferDays = totalProgramDays - totalTakenDays;
            }

            tbTotalDaysOfProgram.Value = Convert.ToInt32(totalProgramDays.Days);
            tbTotalTakenDays.Value = Convert.ToInt32(totalTakenDays.Days);
            tbTransferDays.Value = Convert.ToInt32(totalTransferDays.Days);
        }

        protected void ddlSite_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadSiteLocation(ddlSite.SelectedValue);
        }

        protected void LoadSiteLocation(string SiteId)
        {
            var global = new CGlobal();

            ddlSiteLocation.DataSource = global.GetSiteLocationBySiteId(Convert.ToInt32(SiteId));
            ddlSiteLocation.DataTextField = "Name";
            ddlSiteLocation.DataValueField = "Value";
            ddlSiteLocation.DataBind();
            ddlSiteLocation.Items.Insert(0, new RadComboBoxItem("-Select Location-", "0"));
        }
    }
}