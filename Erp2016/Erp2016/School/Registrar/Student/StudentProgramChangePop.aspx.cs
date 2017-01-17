using System;
using System.Collections.Generic;
using System.Data;
using Erp2016.Lib;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;

namespace School.Registrar.Student
{
    public partial class StudentProgramChangePop : PageBase
    {
        public int InvoiceId { get; set; }

        public StudentProgramChangePop() : base((int)CConstValue.Menu.Student)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InvoiceId = Convert.ToInt32(Request["id"]);

            if (!IsPostBack)
            {
                FileDownloadList1.InitFileDownloadList((int)CConstValue.Upload.ProgramChange);
                RefundInfo1.InitReundInfo(InvoiceId, CurrentSiteLocationId, true);


                /////////////////
                var global = new CGlobal();

                var student = new CStudent().Get(InvoiceId);

                LoadAgency(student.SiteLocationId);
                ddlAgencyContact.Items.Insert(0, new RadComboBoxItem("-None-", "0"));
                LoadFaculty();
                LoadProgramGroup("0");
                LoadProgram("0");

                var cCountry = new CCountry().Get((int)student.CountryId);
                ViewState["CountryMarketId"] = cCountry.CountryMarketId;

                tbRequestDate.SelectedDate = DateTime.Now;

                ddlProgramWeeks.DataSource = GetProgramWeeksList();
                ddlProgramWeeks.DataTextField = "Name";
                ddlProgramWeeks.DataValueField = "Value";
                ddlProgramWeeks.DataBind();
                ddlProgramWeeks.Items.Insert(0, new RadComboBoxItem("-Select Weeks-", "0"));

                ddlPrgHours.DataSource = global.GetDictionary(150);
                ddlPrgHours.DataTextField = "Name";
                ddlPrgHours.DataValueField = "Value";
                ddlPrgHours.DataBind();
                ddlPrgHours.Items.Insert(0, new RadComboBoxItem("-Select HRS-", "0"));
            }
        }

        protected void ToolbarButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Text)
            {
                case "Request":
                    if (IsValid)
                    {
                        var invoiceInfo = new CInvoice(InvoiceId.ToString());
                        if (invoiceInfo != null)
                        {
                            if (tbProgramChangeDate.SelectedDate < invoiceInfo.StartDate &&
                                invoiceInfo.AgencyNet <= invoiceInfo.Balance)
                            {
                                //Before Program Start and UnPaid
                                var newInvoice = NewProgramInvoiceCreate();

                                if (newInvoice > 0)
                                {
                                    //var cBCTinfo = new CBCT();
                                    //var BCTinfo = new BCT();

                                    //BCTinfo.InvoiceId = Convert.ToInt32(hfInvoiceId.Value);
                                    //BCTinfo.RequestDate = Convert.ToDateTime(tbRequestDate.SelectedDate);
                                    //BCTinfo.ActualBCTdate = Convert.ToDateTime(tbProgramChangeDate.SelectedDate);

                                    //BCTinfo.TransferTotalDaysOfProgram = Convert.ToInt32(tbTotalDaysOfProgram.Value);
                                    //BCTinfo.TransferTotalDayTaken = Convert.ToInt32(tbTotalTakenDays.Value);
                                    //BCTinfo.TransferDays = Convert.ToInt32(tbCancelDays.Value);

                                    //BCTinfo.ProgramChangeInvoiceId = newInvoice;

                                    //BCTinfo.CreatedId = CurrentUserId;

                                    //BCTinfo.Reason = tbProgramChangeReason.Text;

                                    //BCTinfo.ApprovalStatus = (int)CConstValue.ApprovalStatus.Approved;
                                    ////payout status >Interface:0 Requested:2 Progress:3 Approved:99 Reject:98 Revise:97
                                    //BCTinfo.ApprovalDate = DateTime.Now;
                                    //BCTinfo.ApprovalId = CurrentUserId;

                                    //var BCTid = cBCTinfo.Add(BCTinfo, CConstValue.BctTypeProgramChange);

                                    //if (BCTid > 0)
                                    //{
                                    //    var cInvoice = new CInvoice();
                                    //    var cancelInvoice = cInvoice.Get(Convert.ToInt32(hfInvoiceId.Value));
                                    //    cancelInvoice.Status = 5; //Cancelled_P (Program Change)
                                    //    cancelInvoice.UpdatedId = CurrentUserId;

                                    //    if (cInvoice.Update(cancelInvoice))
                                    //    {
                                    //        var cProgRegInfo = new CProgramRegistration();
                                    //        var progRegInfo = cProgRegInfo.Get(Convert.ToInt32(cancelInvoice.ProgramRegistrationId));

                                    //        progRegInfo.IsCancel = true;
                                    //        progRegInfo.CancelDate = DateTime.Now;

                                    //        if (cProgRegInfo.Update(progRegInfo)) // Program registration info modify to Cancel status
                                    //        {
                                    //            RunClientScript("Close();");
                                    //        }
                                    //        else
                                    //        {
                                    //            ShowMessage("failed to update inqury (BCT(Program Change) Program Status)");
                                    //        }
                                    //    }
                                    //    else
                                    //    {
                                    //        ShowMessage("failed to update inqury (BCT(Program Change) Invoice Cancelled Update)");
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    ShowMessage("failed to update inqury (BCT(Program Change))");
                                    //}
                                }
                            }
                            else if (tbProgramChangeDate.SelectedDate < invoiceInfo.StartDate && invoiceInfo.Balance == 0)
                            {
                                //Before Program start and Paid
                                if (RefundLogic(100, 100))
                                {
                                    RunClientScript("Close();");
                                }
                                else
                                {
                                    ShowMessage("failed to update inqury (BCT(Program Change) Refund Request)");
                                }
                            }
                            else if (tbProgramChangeDate.SelectedDate >= invoiceInfo.StartDate && invoiceInfo.Balance == 0)
                            {
                                //After Program start and Paid
                                if (RefundLogic(Convert.ToInt32(tbTotalDaysOfProgram.Value), Convert.ToInt32(tbCancelDays.Value)))
                                {
                                    RunClientScript("Close();");
                                }
                                else
                                {
                                    ShowMessage("failed to update inqury (BCT(Program Change) Refund Request)");
                                }
                            }
                        }
                    }
                    break;
                case "Close":
                    RunClientScript("Close();");
                    break;
            }
        }

        protected bool RefundLogic(int totalDay, int cancelDays)
        {
            var cInvoice = new CInvoice();
            var original = cInvoice.Get(InvoiceId);

            if (original != null)
            {
                original.Status = (int)CConstValue.InvoiceStatus.Invoiced_Hold; //Invoiced(Hold)
                if (cInvoice.Update(original))
                {
                    var originalInvoiceId = original.InvoiceId;

                    var cprogramChangeInvoice = new CInvoice();
                    var programChangeInvoice = new Invoice();

                    programChangeInvoice.OriginalInvoiceId = originalInvoiceId;
                    programChangeInvoice.InvoiceType = (int)CConstValue.InvoiceType.Refund_PC; //Refund Invoice

                    programChangeInvoice.StudentId = original.StudentId;
                    programChangeInvoice.AgencyId = original.AgencyId;
                    programChangeInvoice.ProgramRegistrationId = original.ProgramRegistrationId;
                    programChangeInvoice.HomestayRegistrationId = original.HomestayRegistrationId;
                    // programChangeInvoice.ScholarshipId = original.ScholarshipId;
                    programChangeInvoice.SiteLocationId = original.SiteLocationId;

                    programChangeInvoice.IsFinancialGurantee = original.IsFinancialGurantee;

                    programChangeInvoice.Status = (int)CConstValue.InvoiceStatus.Pending; // Pending
                    programChangeInvoice.CreatedId = CurrentUserId;
                    programChangeInvoice.CreatedDate = DateTime.Now;

                    var invoiceId = cprogramChangeInvoice.Add(programChangeInvoice);

                    //if (invoiceId > 0)
                    //{
                    //    var programChangeInvoiceItems = new CInvoiceItem();

                    //    if (programChangeInvoiceItems.TransferCancelItemsUpdate(originalInvoiceId, invoiceId, Convert.ToInt32(tbTotalDaysOfProgram.Value), Convert.ToInt32(tbCancelDays.Value), CurrentUserId))
                    //    {
                    //        decimal refundAmt = programChangeInvoiceItems.TotalAmount(invoiceId);

                    //        var cCreditMemo = new CCreditMemo();
                    //        var creditMemo = new CreditMemo();

                    //        creditMemo.CreditMemoType = (int)CConstValue.CreditType.Refund; //Program Change
                    //        creditMemo.InvoiceId = invoiceId;
                    //        creditMemo.OriginalCreditMemoAmount = Math.Abs(refundAmt);
                    //        creditMemo.CreatedId = CurrentSiteLocationId;
                    //        creditMemo.CreatedDate = DateTime.Now;
                    //        creditMemo.IsActive = false;

                    //        var creditMemoId = cCreditMemo.Add(creditMemo);

                    //        if (creditMemoId > 0)
                    //        {
                    //            var newinvoice = NewProgramInvoiceCreate();
                    //            if (newinvoice > 0)
                    //            {
                    //                //var cBCTinfo = new CBCT();
                    //                //var BCTinfo = new BCT();

                    //                //BCTinfo.CreditMemoId = creditMemoId;
                    //                //BCTinfo.InvoiceId = invoiceId;
                    //                //BCTinfo.RequestDate = Convert.ToDateTime(tbRequestDate.SelectedDate);
                    //                //BCTinfo.ActualBCTdate = Convert.ToDateTime(tbProgramChangeDate.SelectedDate);
                    //                //BCTinfo.BCTamount = Math.Abs(refundAmt);
                    //                //BCTinfo.CreatedId = CurrentUserId;

                    //                //BCTinfo.ApprovalStatus = 0; //
                    //                //BCTinfo.Reason = tbProgramChangeReason.Text;

                    //                //BCTinfo.TransferDays = Convert.ToInt32(tbCancelDays.Value);
                    //                //BCTinfo.TransferTotalDayTaken = Convert.ToInt32(tbTotalTakenDays.Value);
                    //                //BCTinfo.TransferTotalDaysOfProgram = Convert.ToInt32(tbTotalDaysOfProgram.Value);

                    //                //BCTinfo.ProgramChangeInvoiceId = newinvoice;

                    //                //var BCTid = cBCTinfo.Add(BCTinfo, CConstValue.BctTypeProgramChange);


                    //                //if (BCTid > 0)
                    //                //{
                    //                //    return true;
                    //                //}
                    //            }
                    //        }
                    //    }
                    //}
                }
            }
            return false;
        }

        protected int NewProgramInvoiceCreate()
        {
            var cProgRegInfo = new CProgramRegistration();
            var programReg = new ProgramRegistration();

            programReg.StudentId = 0;
            programReg.ProgramId = Convert.ToInt32(ddlProgramName.SelectedValue);

            programReg.StartDate = tbPrgStartDate.SelectedDate;
            programReg.EndDate = tbPrgEndDate.SelectedDate;

            programReg.Weeks = Convert.ToInt32(ddlProgramWeeks.SelectedValue);
            programReg.HrsStatus = Convert.ToInt32(ddlPrgHours.SelectedValue);

            var proRegId = cProgRegInfo.Add(programReg); //DB:ProgramRegistration

            if (proRegId > 0)
            {
                var cInvoice = new CInvoice();
                var invoice = new Invoice();

                invoice.ProgramRegistrationId = proRegId;
                invoice.StudentId = 0;
                invoice.AgencyId = Convert.ToInt32(ddlAgency.SelectedValue);
                //invoice.ScholarshipId = Scholarship;
                invoice.SiteLocationId = CurrentSiteLocationId;

                invoice.InvoiceType = (int)CConstValue.InvoiceType.General; //General Invoice(IN)

                invoice.Status = (int)CConstValue.InvoiceStatus.Pending; // Pending
                invoice.CreatedId = CurrentUserId;
                invoice.CreatedDate = DateTime.Now;

                var invoiceId = cInvoice.Add(invoice); //DB:Invoice

                //if (invoiceId > 0)
                //{
                //    var cInvoiceItem = new CInvoiceItem();
                //    var invoiceItem = new InvoiceItem();

                //    invoiceItem.InvoiceId = invoiceId;
                //    invoiceItem.InvoiceCoaItemId = 1; //1:Tuition
                //    invoiceItem.StandardPrice = Convert.ToDecimal(tbPrgStandardTuition.Value);
                //    invoiceItem.StudentPrice = Convert.ToDecimal(tbPrgTuition.Value);
                //    invoiceItem.AgencyPrice = Convert.ToDecimal(tbPrgTuition.Value);

                //    invoiceItem.CreatedId = CurrentUserId;
                //    invoiceItem.CreatedDate = DateTime.Now;

                //    if (cInvoiceItem.Add(invoiceItem) > 0) //DB:InvoiceItem(Tuition Item)
                //    {
                //        var standardTuitionId = Convert.ToInt32(ViewState["ProgramTuitionId"]);
                //        var commission = Convert.ToDecimal(tbPrgTuition.Value) * Convert.ToDecimal(tbCommissionRate.Value) / 100;

                //        if (commission > 0)
                //        {
                //            cInvoiceItem.CommissionAdd(invoiceId, commission, CurrentUserId); //DB:InvoiceItem(Commission-Tuition)
                //        }

                //        if (cInvoiceItem.ItemsAdd(invoiceId, standardTuitionId, CurrentUserId))
                //        //DB:InvoiceItem(Other Items)
                //        {
                //            return invoiceId;
                //        }
                //        ShowMessage("failed to update inqury (Add Invoice Items)");
                //    }
                //    else
                //    {
                //        ShowMessage("failed to update inqury (Add Invoice Items)");
                //    }
                //}
                //else
                //{
                //    ShowMessage("failed to update inqury (Invoice)");
                //}
            }
            else
            {
                ShowMessage("failed to update inqury (Program Registragion)");
            }
            return 0;
        }

        public List<CListModel> GetProgramWeeksList()
        {
            var result = new List<CListModel>();

            var weeks = 1;
            while (weeks <= 52)
            {
                result.Add(new CListModel { Name = weeks.ToString(), Value = weeks.ToString() });
                weeks++;
            }

            return result;
        }

        protected void tbProgramChangeDate_SelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            var cInvoice = new CInvoice();
            var qryInvoice = cInvoice.Get(InvoiceId);

            var cQryProRegi = new CProgramRegistration();
            var qryProRegi = cQryProRegi.Get(Convert.ToInt32(qryInvoice.ProgramRegistrationId));

            var TransferDate = Convert.ToDateTime(tbProgramChangeDate.SelectedDate);

            var StartDate = Convert.ToDateTime(qryProRegi.StartDate);
            var EndDate = Convert.ToDateTime(qryProRegi.EndDate);
            var totalProgramDays = EndDate - StartDate;
            var totalTakenDays = new TimeSpan();
            var totalCancelDays = new TimeSpan();

            if (TransferDate <= StartDate)
            {
                totalCancelDays = totalProgramDays;
            }
            else
            {
                totalTakenDays = TransferDate - StartDate;
                totalCancelDays = totalProgramDays - totalTakenDays;
            }

            tbTotalDaysOfProgram.Value = Convert.ToInt32(totalProgramDays.Days);
            tbTotalTakenDays.Value = Convert.ToInt32(totalTakenDays.Days);
            tbCancelDays.Value = Convert.ToInt32(totalCancelDays.Days);
        }

        protected void ddlAgency_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (Convert.ToInt32(ddlAgency.SelectedValue) > 0)
            {
                var cAgency = new CAgency();
                var agency = cAgency.Get(Convert.ToInt32(ddlAgency.SelectedValue));
                tbCommissionRate.Value = agency.CommissionRateBasic;
            }
            else
            {
                tbCommissionRate.Value = 0;
            }
        }

        protected void ddlFaculty_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadProgramGroup(ddlFaculty.SelectedValue);
            //LoadProgram(ddlProgramGrp.SelectedValue);
            LoadProgram(ddlFaculty.SelectedValue);
        }

        protected void ddlProgramGrp_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            LoadProgramGroup(ddlFaculty.SelectedValue);
        }

        protected void ddlProgramGrp_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadProgram(ddlProgramGrp.SelectedValue);
        }

        protected void ddlProgramName_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            LoadProgram(ddlProgramGrp.SelectedValue);
        }

        protected void tbPrgStartDate_SelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            ddlProgramWeeks.Enabled = true;
        }

        protected void ddlProgramWeeks_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var weeks = Convert.ToInt32(e.Value);
            var start = Convert.ToDateTime(tbPrgStartDate.SelectedDate);
            tbPrgEndDate.SelectedDate = CProgramRegistration.GetEndDate(start, weeks);
            tbPrgEndDate.Enabled = true;
            ddlPrgHours.Enabled = true;
        }

        protected void ddlPrgHours_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //var programTuition = new CProgramTuition();
            var programTuition = new CProgramTuition();
            //var programStudentTuition = new CProgramStudentTuition();

            var programId = Convert.ToInt32(ddlProgramName.SelectedValue);
            var weeks = Convert.ToInt32(ddlProgramWeeks.SelectedValue);
            var hrs = Convert.ToInt32(ddlPrgHours.SelectedValue);

            var standardTuition = programTuition.GetStandardTuition(programId, weeks, hrs, Convert.ToInt32(ViewState["CountryMarketId"]));
            ViewState["ProgramTuitionId"] = standardTuition.ProgramTuitionId;

            tbPrgStandardTuition.Value = Convert.ToDouble(standardTuition.Tuition);
            tbPrgTuition.Value = Convert.ToDouble(standardTuition.Tuition);
        }

        protected void LoadAgency(int siteLocation)
        {
            var cAgency = new CAgency();

            ddlAgency.DataSource = cAgency.GetAgency(siteLocation);
            ddlAgency.DataTextField = "Name";
            ddlAgency.DataValueField = "Value";
            ddlAgency.DataBind();
            ddlAgency.Items.Insert(0, new RadComboBoxItem("-None(Direct Student)-", "0"));
        }

        protected void LoadFaculty()
        {
            var myDepId = CurrentSiteLocationId;
            var faculty = new CFaculty();

            ddlFaculty.DataSource = faculty.GetFacultyList(myDepId);
            ddlFaculty.DataTextField = "Name";
            ddlFaculty.DataValueField = "Value";
            ddlFaculty.DataBind();
            ddlFaculty.Items.Insert(0, new RadComboBoxItem("-ALL-", "0"));
        }

        protected void LoadProgramGroup(string facultyId)
        {
            var myDepId = CurrentSiteLocationId;
            var programgroups = new CProgramGroup();

            ddlProgramGrp.DataSource = programgroups.GetProgramGroupList(myDepId, Convert.ToInt32(facultyId));
            ddlProgramGrp.DataTextField = "Name";
            ddlProgramGrp.DataValueField = "Value";
            ddlProgramGrp.DataBind();
            ddlProgramGrp.Items.Insert(0, new RadComboBoxItem("-ALL-", "0"));
        }

        protected void LoadProgram(string programgrpId)
        {
            var myDepId = CurrentSiteLocationId;
            var program = new CProgram();

            ddlProgramName.DataSource = program.GetProgramList(myDepId, Convert.ToInt32(programgrpId));
            ddlProgramName.DataTextField = "Name";
            ddlProgramName.DataValueField = "Value";
            ddlProgramName.DataBind();
            ddlProgramName.Items.Insert(0, new RadComboBoxItem("-Select Program-", "0"));
        }
    }
}