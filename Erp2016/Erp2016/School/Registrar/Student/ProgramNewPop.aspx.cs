using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;

namespace School.Registrar.Student
{
    public partial class ProgramNewPop : PageBase
    {
        public int Id { get; set; }

        public ProgramNewPop() : base((int)CConstValue.Menu.Student)
        {
        }

        public int StudentSiteId
        {
            get { return (int?)ViewState["StudentSiteId"] ?? 0; }
            set { ViewState["StudentSiteId"] = value; }
        }

        public int StudentSiteLocationId
        {
            get { return (int?)ViewState["StudentSiteLocationId"] ?? 0; }
            set { ViewState["StudentSiteLocationId"] = value; }
        }

        public int? ScholarshipId
        {
            get { return (int?)ViewState["ScholarshipId"]; }
            set { ViewState["ScholarshipId"] = value; }
        }

        public int? PromotionId
        {
            get { return (int?)ViewState["PromotionId"]; }
            set { ViewState["PromotionId"] = value; }
        }

        private RadGrid _radGridInvoiceItems;

        protected void Page_Load(object sender, EventArgs e)
        {
            // find user control
            _radGridInvoiceItems = InvoiceItemGrid1.GetRadGridInvoiceItems();
            // connect event of invoice Items.
            _radGridInvoiceItems.PreRender += _radGridInvoiceItems_PreRender;
            _radGridInvoiceItems.MasterTableView.DataSourceID = null;
            _radGridInvoiceItems.DataSourceID = null;
            // just view
            InvoiceItemGrid1.SetEditMode(false);

            Id = Convert.ToInt32(Request["id"]);

            if (!IsPostBack)
            {
                var global = new CGlobal();
                var cStudent = new CStudent();
                var student = cStudent.Get(Id);

                var studentSite = new CSiteLocation().Get(student.SiteLocationId);

                StudentSiteId = studentSite.SiteId;
                StudentSiteLocationId = student.SiteLocationId;

                LoadAgency();
                LoadFaculty();
                LoadProgramGroup("0");
                LoadProgram("0");

                ddlProgramWeeks.DataSource = new CProgram().GetProgramWeeksList();
                ddlProgramWeeks.DataTextField = "Name";
                ddlProgramWeeks.DataValueField = "Value";
                ddlProgramWeeks.DataBind();

                ddlPrgHours.DataSource = global.GetDictionary(150);
                ddlPrgHours.DataTextField = "Name";
                ddlPrgHours.DataValueField = "Value";
                ddlPrgHours.DataBind();


                var cCountry = new CCountry().Get((int)student.CountryId);
                var cCountryMarket = new CCountryMarket().Get((int)cCountry.CountryMarketId);
                ViewState["CountryMarketId"] = cCountry.CountryMarketId;

                ttName1.Text = cStudent.GetStudentName(student) + " [" + student.StudentNo + "]";
                ttName2.Text = cCountryMarket.Name;
            }

            ddlAgency.OpenDropDownOnLoad = false;
            ddlFaculty.OpenDropDownOnLoad = false;
            ddlProgramGrp.OpenDropDownOnLoad = false;
            ddlProgramName.OpenDropDownOnLoad = false;
            ddlProgramWeeks.OpenDropDownOnLoad = false;
            ddlPrgHours.OpenDropDownOnLoad = false;
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            ResetGridInvoice();
        }

        private void _radGridInvoiceItems_PreRender(object sender, EventArgs e)
        {
            //ResetGridInvoice();
        }

        protected void LoadAgency()
        {
            ddlAgency.Items.Clear();
            ddlAgency.Text = string.Empty;
            ddlAgency.DataSource = new CAgency().GetAgency(StudentSiteLocationId);
            ddlAgency.DataTextField = "Name";
            ddlAgency.DataValueField = "Value";
            ddlAgency.DataBind();
            ddlAgency.Items.Insert(0, new RadComboBoxItem("N/A (Direct Student)", null));
        }

        protected void LoadFaculty()
        {
            ddlFaculty.Items.Clear();
            ddlFaculty.Text = string.Empty;
            ddlFaculty.DataSource = new CFaculty().GetFacultyList(StudentSiteId);
            ddlFaculty.DataTextField = "Name";
            ddlFaculty.DataValueField = "Value";
            ddlFaculty.DataBind();
            ddlFaculty.Items.Insert(0, new RadComboBoxItem("N/A", null));
        }

        protected void LoadProgramGroup(string facultyId)
        {
            ddlProgramGrp.Items.Clear();
            ddlProgramGrp.Text = string.Empty;
            if (!string.IsNullOrEmpty(facultyId))
                ddlProgramGrp.DataSource = new CProgramGroup().GetProgramGroupList(StudentSiteId, Convert.ToInt32(facultyId));
            else
                ddlProgramGrp.DataSource = new CProgramGroup().GetProgramGroupList(StudentSiteId);
            ddlProgramGrp.DataTextField = "Name";
            ddlProgramGrp.DataValueField = "Value";
            ddlProgramGrp.DataBind();
            ddlProgramGrp.Items.Insert(0, new RadComboBoxItem("N/A", null));
        }

        protected void LoadProgram(string programGroupId)
        {
            ddlProgramName.Items.Clear();
            ddlProgramName.Text = string.Empty;
            if (!string.IsNullOrEmpty(programGroupId))
                ddlProgramName.DataSource = new CProgram().GetProgramList(StudentSiteLocationId, Convert.ToInt32(programGroupId));
            else
                ddlProgramName.DataSource = new CProgram().GetProgramList(StudentSiteLocationId);
            ddlProgramName.DataTextField = "Name";
            ddlProgramName.DataValueField = "Value";
            ddlProgramName.DataBind();
        }

        private bool GetAgencyRate()
        {
            if (!string.IsNullOrEmpty(ddlAgency.SelectedValue))
            {
                var cAgency = new CAgency();
                var agency = cAgency.Get(Convert.ToInt32(ddlAgency.SelectedValue));
                if (agency != null)
                    tbCommissionRate.Value = RadButtonAgencyRateBasic.Checked ? agency.CommissionRateBasic : agency.CommissionRateSeasonal;
                else
                    tbCommissionRate.Value = 0;
                return true;
            }

            tbCommissionRate.Value = 0;
            return false;
        }

        protected void ddlAgency_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (GetAgencyRate())
                ddlFaculty.OpenDropDownOnLoad = true;
        }

        protected void ddlFaculty_OnSelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadProgramGroup(ddlFaculty.SelectedValue);

            ddlProgramGrp.OpenDropDownOnLoad = true;
        }

        protected void ddlProgramGrp_OnSelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadProgram(ddlProgramGrp.SelectedValue);

            ddlProgramName.OpenDropDownOnLoad = true;
        }

        protected void ddlProgramName_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void tbPrgStartDate_OnSelectedDateChanged(object o, SelectedDateChangedEventArgs e)
        {
            ddlProgramWeeks.Enabled = true;
            ddlProgramWeeks.OpenDropDownOnLoad = true;
        }

        protected void ddlProgramWeeks_OnSelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var weeks = Convert.ToInt32(e.Value);
            var start = Convert.ToDateTime(tbPrgStartDate.SelectedDate);
            tbPrgEndDate.SelectedDate = CProgramRegistration.GetEndDate(start, weeks);
            tbPrgEndDate.Enabled = true;
            ddlPrgHours.Enabled = true;

            ddlPrgHours.OpenDropDownOnLoad = true;
        }

        protected void ddlPrgHours_OnSelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var programTuition = new CProgramTuition();

            var programId = Convert.ToInt32(ddlProgramName.SelectedValue);
            var weeks = Convert.ToInt32(ddlProgramWeeks.SelectedValue);
            var hrs = Convert.ToInt32(ddlPrgHours.SelectedValue);

            // G1 Market
            var standardTuition = programTuition.GetStandardTuition(programId, weeks, hrs, 1);
            if (standardTuition != null)
                tbPrgStandardTuition.Value = Convert.ToDouble(standardTuition.Tuition);

            // Student Market
            var countryMarketTuition = programTuition.GetStandardTuition(programId, weeks, hrs, Convert.ToInt32(ViewState["CountryMarketId"]));
            if (countryMarketTuition != null)
            {
                ViewState["ProgramTuitionId"] = countryMarketTuition.ProgramTuitionId;
                RadNumericTextBoxCountryMarketTuition.Value = Convert.ToDouble(countryMarketTuition.Tuition);
                tbPrgTuition.Value = Convert.ToDouble(countryMarketTuition.Tuition);
            }
            else
            {
                RadNumericTextBoxCountryMarketTuition.Value = 0;
                tbPrgTuition.Value = 0;
            }

            tbPrgTuition.Focus();
        }

        protected void ToolbarButtonClick(object sender, RadToolBarEventArgs e)
        {
            if (e.Item.Text == "Save")
            {
                if (IsValid)
                {
                    if (!string.IsNullOrEmpty(ddlAgency.SelectedValue) && (tbCommissionRate.Value == 0 || tbCommissionRate.Value == null))
                    {
                        ShowMessage("Commision Rate should be written.");
                        return;
                    }

                    var cScholarship = new CScholarship();
                    if (ScholarshipId != null)
                    {
                        var scholarship = cScholarship.GetVwScholarship((int)ScholarshipId);
                        if (RadButtonAvailableScholarshipAmount.Checked)
                        {
                            if (scholarship.AvailableAmount == 0 || (double)scholarship.AvailableAmount < RadNumericTextBoxScholarshipAmount.Value)
                            {
                                ShowMessage("Scholarship Amount is bigger than availalble Amount.");
                                return;
                            }
                        }
                        else
                        {
                            if (scholarship.AvailableWeeks == 0 || (double)scholarship.AvailableWeeks < RadNumericTextBoxScholarshipWeeks.Value)
                            {
                                ShowMessage("Scholarship Weeks are bigger than availalble Weeks.");
                                return;
                            }
                        }
                    }

                    var cProgramReg = new CProgramRegistration();
                    var programReg = new ProgramRegistration();

                    programReg.StudentId = Id;
                    programReg.ProgramId = Convert.ToInt32(ddlProgramName.SelectedValue);

                    programReg.StartDate = tbPrgStartDate.SelectedDate;
                    programReg.EndDate = tbPrgEndDate.SelectedDate;
                    programReg.ProgramRegistrationType = 9;

                    if (!string.IsNullOrEmpty(ddlProgramWeeks.SelectedValue))
                        programReg.Weeks = Convert.ToInt32(ddlProgramWeeks.SelectedValue);

                    if (!string.IsNullOrEmpty(ddlPrgHours.SelectedValue))
                        programReg.HrsStatus = Convert.ToInt32(ddlPrgHours.SelectedValue);

                    programReg.CreatedId = CurrentUserId;
                    programReg.CreatedDate = DateTime.Now;

                    var proRegId = cProgramReg.Add(programReg); //DB:ProgramRegistration

                    if (proRegId > 0)
                    {
                        var cInvoice = new CInvoice();
                        var invoice = new Invoice();

                        invoice.ProgramRegistrationId = proRegId;
                        invoice.StudentId = Id;
                        if (!string.IsNullOrEmpty(ddlAgency.SelectedValue))
                        {
                            invoice.AgencyId = Convert.ToInt32(ddlAgency.SelectedValue);
                            invoice.IsAgencySeasonalRate = RadButtonAgencyRateSeasonal.Checked;
                            invoice.AgencyRate = tbCommissionRate.Value;
                        }

                        if (ScholarshipId != null)
                        {
                            invoice.ScholarshipId = ScholarshipId;
                            if (RadButtonAvailableScholarshipAmount.Checked)
                            {
                                invoice.ScholarshipAmount = (decimal)RadNumericTextBoxScholarshipAmount.Value;
                            }
                            else
                            {
                                invoice.ScholarshipAmount = (decimal)RadNumericTextBoxScholarshipAmount.Value;
                                invoice.ScholarshipWeeks = (int)RadNumericTextBoxScholarshipWeeks.Value;
                            }
                        }
                        invoice.PromotionId = PromotionId;

                        invoice.SiteLocationId = CurrentSiteLocationId;
                        invoice.InvoiceType = (int)CConstValue.InvoiceType.General; //General Invoice(IN)


                        invoice.Status = (int)CConstValue.InvoiceStatus.Pending; // Pending
                        invoice.CreatedId = CurrentUserId;
                        invoice.CreatedDate = DateTime.Now;

                        var invoiceId = cInvoice.Add(invoice); //DB:Invoice

                        if (invoiceId > 0)
                        {
                            var cInvoiceItem = new CInvoiceItem();

                            foreach (GridDataItem item in _radGridInvoiceItems.Items)
                            {
                                var invoiceCoaItemId = (RadDropDownList)item.FindControl("ddlInvoiceItems");
                                var standardPrice = (Label)item.FindControl("lblStandardPrice");
                                var studentPrice = (Label)item.FindControl("lblStudentPrice");
                                var agencyPrice = (Label)item.FindControl("lblAgencyPrice");

                                var invoiceItem = new InvoiceItem();
                                invoiceItem.InvoiceId = invoiceId;

                                invoiceItem.InvoiceCoaItemId = Convert.ToInt32(invoiceCoaItemId.SelectedValue);
                                invoiceItem.StandardPrice = Convert.ToDecimal(standardPrice.Text.Replace("$", string.Empty));
                                invoiceItem.StudentPrice = Convert.ToDecimal(studentPrice.Text.Replace("$", string.Empty));
                                invoiceItem.AgencyPrice = Convert.ToDecimal(agencyPrice.Text.Replace("$", string.Empty));

                                invoiceItem.CreatedId = CurrentUserId;
                                invoiceItem.CreatedDate = DateTime.Now;

                                cInvoiceItem.Add(invoiceItem);
                            }

                            // disable used scholarship
                            if (ScholarshipId != null)
                            {
                                var sScholarship = new CScholarship();
                                var scholarship = sScholarship.Get((int)ScholarshipId);
                                scholarship.IsActive = true;
                                sScholarship.Update(scholarship);
                            }

                            RunClientScript("Close();");
                        }
                        else
                            ShowMessage("failed to update inqury (Add Invoice Items)");
                    }
                    else
                        ShowMessage("failed to update inqury (Invoice)");
                }
            }
            else
            {
                ShowMessage("Fill in data");
            }
        }

        private void ResetGridInvoice()
        {
            _radGridInvoiceItems.DataSource = GetInvoiceData();
            _radGridInvoiceItems.Rebind();
        }

        public DataTable GetInvoiceData()
        {
            // student insurance
            var cStudent = new CStudent();
            var student = cStudent.Get(Id);

            DataTable table = new DataTable();
            table.Columns.Add("InvoiceCoaItemId", typeof(int));
            table.Columns.Add("StandardPrice", typeof(decimal));
            table.Columns.Add("StudentPrice", typeof(decimal));
            table.Columns.Add("AgencyPrice", typeof(decimal));
            table.Columns.Add("Remark", typeof(string));
            table.Columns.Add("InvoiceItemId", typeof(int));

            // Tuition
            if (tbPrgTuition.Value.ToString() != "0" && tbPrgTuition.Value.ToString() != string.Empty)
            {
                table.Rows.Add((int)CConstValue.InvoiceCoaItem.TuitionBasic, tbPrgStandardTuition.Value, tbPrgTuition.Value, tbPrgTuition.Value, string.Empty, 0);

                // Scholarship
                double scholarshipPrice = 0;
                ScholarshipId = null;
                ImageScholarshipSuccess.Visible = false;
                ImageScholarshipFail.Visible = true;
                RadNumericTextBoxAvailableScholarshipAmount.Text = string.Empty;
                RadNumericTextBoxAvailableScholarshipWeeks.Text = string.Empty;

                if (RadTextBoxScholarship.Text != string.Empty)
                {
                    if (student != null && !string.IsNullOrEmpty(ddlAgency.SelectedValue))
                    {
                        var cScholarship = new CScholarship();
                        var scholarship = cScholarship.GetScholarship(RadTextBoxScholarship.Text.Replace("-", string.Empty), student.SiteLocationId, Convert.ToInt32(ddlProgramWeeks.Text), Convert.ToInt32(ddlAgency.SelectedValue));
                        if (scholarship != null)
                        {
                            // search scholarship with availalble value over than 1
                            var vwScholarship = cScholarship.GetVwScholarship(scholarship.ScholarshipId);
                            if (vwScholarship != null)
                            {
                                // if invoice doesn't have, it can be null
                                decimal availableAmount = vwScholarship.AvailableAmount ?? 0;
                                int availableWeeks = vwScholarship.AvailableWeeks ?? 0;

                                if (scholarship.Amount != null)
                                {
                                    RadNumericTextBoxAvailableScholarshipAmount.Value = (double)availableAmount;

                                    if (!string.IsNullOrEmpty(RadNumericTextBoxScholarshipAmount.Text))
                                    {
                                        if ((double)availableAmount < RadNumericTextBoxScholarshipAmount.Value)
                                            RadNumericTextBoxScholarshipAmount.Value = (double)availableAmount;
                                    }
                                    else
                                    {
                                        RadNumericTextBoxScholarshipAmount.Value = (double)availableAmount;
                                    }
                                    scholarshipPrice = (double)RadNumericTextBoxScholarshipAmount.Value * -1;

                                    RadNumericTextBoxScholarshipWeeks.Text = string.Empty;
                                    RadButtonAvailableScholarshipAmount.Checked = true;
                                    RadButtonAvailableScholarshipWeeks.Checked = false;
                                }
                                else
                                {
                                    RadNumericTextBoxAvailableScholarshipWeeks.Value = availableWeeks;

                                    if (!string.IsNullOrEmpty(RadNumericTextBoxScholarshipWeeks.Text))
                                    {
                                        if ((double)availableWeeks < RadNumericTextBoxScholarshipWeeks.Value)
                                            RadNumericTextBoxScholarshipWeeks.Value = (double)availableWeeks;
                                    }
                                    else
                                    {
                                        RadNumericTextBoxScholarshipWeeks.Value = (double)availableWeeks;
                                    }
                                    // todo: cal week for scholarship.
                                    // cal weeks !!!!!!!!!!!!!!!!!!
                                    scholarshipPrice = (double)RadNumericTextBoxScholarshipWeeks.Value;
                                    RadNumericTextBoxScholarshipAmount.Value = scholarshipPrice;

                                    RadButtonAvailableScholarshipAmount.Checked = false;
                                    RadButtonAvailableScholarshipWeeks.Checked = true;
                                }

                                table.Rows.Add((int)CConstValue.InvoiceCoaItem.TuitionScholarship, 0, 0, scholarshipPrice, string.Empty, 0);

                                ScholarshipId = scholarship.ScholarshipId;
                                ImageScholarshipSuccess.Visible = true;
                                ImageScholarshipFail.Visible = false;
                            }
                        }
                    }
                }

                // Commission
                if (tbCommissionRate.Value.ToString() != "0" && tbCommissionRate.Value.ToString() != string.Empty)
                {
                    table.Rows.Add((int)CConstValue.InvoiceCoaItem.CommissionTuition, 0, 0, (tbPrgTuition.Value + scholarshipPrice) * (tbCommissionRate.Value / -100), string.Empty, 0);
                }

                // Promotion
                PromotionId = null;
                ImagePromotionSuccess.Visible = false;
                ImagePromotionFail.Visible = true;
                if (RadTextBoxPromotion.Text != string.Empty)
                {
                    if (student != null)
                    {
                        var cPromotion = new CPromotion();
                        var promotion = cPromotion.GetPromotion(RadTextBoxPromotion.Text, student.SiteLocationId);
                        if (promotion != null)
                        {
                            PromotionId = promotion.PromotionId;
                            ImagePromotionSuccess.Visible = true;
                            ImagePromotionFail.Visible = false;
                        }
                    }
                }

                // from Other fee info
                if (!string.IsNullOrEmpty(ddlProgramName.SelectedValue))
                {
                    var cProgramOtherFeeInfo = new CProgramOtherFeeInfo();
                    var programOtherFeeInfo = cProgramOtherFeeInfo.Get(Convert.ToInt32(ddlProgramName.SelectedValue));
                    if (programOtherFeeInfo != null)
                    {
                        // other fees
                        var regFee = programOtherFeeInfo.RegFee + programOtherFeeInfo.JRegFee;
                        if (regFee > 0)
                            table.Rows.Add((int)CConstValue.InvoiceCoaItem.Registration, regFee, regFee, regFee, string.Empty, 0);

                        var materialFee = programOtherFeeInfo.AcademicFee + programOtherFeeInfo.MaterialFee + programOtherFeeInfo.UniformFee + programOtherFeeInfo.SupplyFee;
                        if (materialFee > 0)
                            table.Rows.Add((int)CConstValue.InvoiceCoaItem.MaterialOthers, materialFee, materialFee, materialFee, string.Empty, 0);

                        var testFee = programOtherFeeInfo.TestFee + programOtherFeeInfo.ExamFee;
                        if (testFee > 0)
                            table.Rows.Add((int)CConstValue.InvoiceCoaItem.TestExamFee, testFee, testFee, testFee, string.Empty, 0);

                        var internshipFee = programOtherFeeInfo.InternFee + programOtherFeeInfo.PracticeFee + programOtherFeeInfo.LCFee + programOtherFeeInfo.SDFee + programOtherFeeInfo.UPFee;
                        if (internshipFee > 0)
                            table.Rows.Add((int)CConstValue.InvoiceCoaItem.InternshipBasic, internshipFee, internshipFee, internshipFee, string.Empty, 0);

                        var administration = programOtherFeeInfo.ACFee + programOtherFeeInfo.AdminFee + programOtherFeeInfo.UAGFee;
                        if (administration > 0)
                            table.Rows.Add((int)CConstValue.InvoiceCoaItem.Administration, administration, administration, administration, string.Empty, 0);

                        var certificateFee = programOtherFeeInfo.CFee;
                        if (certificateFee > 0)
                            table.Rows.Add((int)CConstValue.InvoiceCoaItem.ServiceCertificate, certificateFee, certificateFee, certificateFee, string.Empty, 0);

                        var otherFee = programOtherFeeInfo.OtherFee;
                        if (otherFee > 0)
                            table.Rows.Add((int)CConstValue.InvoiceCoaItem.Other, otherFee, otherFee, otherFee, string.Empty, 0);
                    }
                }

                if (student != null)
                {
                    if (student.Insurance)
                        // 33
                        table.Rows.Add((int)CConstValue.InvoiceCoaItem.Insurance, student.InsuranceTotal, student.InsuranceTotal, student.InsuranceTotal);
                }

                // agency check
                if (string.IsNullOrEmpty(ddlAgency.SelectedValue))
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        dr["AgencyPrice"] = 0;
                    }
                }
            }
            return table;
        }

        protected void RadButtonAgencyRateBasic_OnCheckedChanged(object sender, EventArgs e)
        {
            GetAgencyRate();
        }

        protected void RadButtonAgencyRateSeasonal_OnCheckedChanged(object sender, EventArgs e)
        {
            GetAgencyRate();
        }

    }
}