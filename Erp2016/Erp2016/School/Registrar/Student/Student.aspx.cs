using System;
using System.Data;
using System.Web.Routing;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.Registrar.Student
{
    public partial class Student : PageBase
    {
        public Student() : base((int)CConstValue.Menu.Student)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // TODO: TEST
            var scriptManager = (RadScriptManager)Page.Master.FindControl("RadScriptManager1");
            //scriptManager.RegisterPostBackControl(RadToolBar3.FindItemByText("Excel"));

            if (!IsPostBack)
            {
                FileDownloadList1.InitFileDownloadList((int)CConstValue.Upload.Student);
                //FileDownloadList1.SetVisibieUploadControls(false);

                ResetForm();
            }

            var studentId = Request["id"];
            if (!IsPostBack && !string.IsNullOrEmpty(studentId))
            {
                // select me
                LinqDataSourceStudents.WhereParameters.Clear();
                LinqDataSourceStudents.WhereParameters.Add("StudentId", DbType.Int32, studentId);
                LinqDataSourceStudents.Where = "StudentId == @StudentId";

                RadGridStudentList.MasterTableView.Rebind();

                foreach (GridDataItem item in RadGridStudentList.Items)
                {
                    if (item.GetDataKeyValue("StudentId").ToString() == studentId)
                    {
                        item.Selected = true;
                        GetStudent();
                        break;
                    }
                }
            }
            else
            {
                LinqDataSourceStudents.WhereParameters.Clear();
                foreach (var model in UserPermissionModel.SearchSiteLocationList)
                    LinqDataSourceStudents.WhereParameters.Add(model.SiteLocationIdName, DbType.Int32, model.SiteLocationId.ToString());
                LinqDataSourceStudents.Where = UserPermissionModel.SearchWhereSiteLocationSb.ToString();
            }

            GetStudentContract();
        }

        protected void StudentButtonClicked(object sender, RadToolBarEventArgs e)
        {
            if (e.Item.Text == "Update" && RadGridStudentList.SelectedValue != null)
            {
                if (IsValid)
                {
                    var cStud = new CStudent();
                    var stud = cStud.Get(Convert.ToInt32(RadGridStudentList.SelectedValue));

                    stud.FirstName = tbFirstName.Text;
                    stud.LastName1 = tbLastName1.Text;
                    stud.LastName2 = tbLastName2.Text;
                    stud.MiddleName1 = tbMiddleName1.Text;
                    stud.MiddleName2 = tbMiddleName2.Text;

                    stud.Address1InCanada = tbCadAddress.Text;
                    stud.CityInCanada = tbCadCity.Text;
                    stud.ProvinceInCanada = tbCadProvince.Text;
                    stud.PostalCodeInCanada = tbCadZipcode.Text;

                    stud.PermanentAddress1 = tbPerAddress.Text;
                    stud.PermanentCity = tbPerCity.Text;
                    stud.PermanentProvince = tbPerState.Text;
                    stud.PermanentPostalCode = tbPerZiocode.Text;
                    stud.PermanentCountry = (ddlPerCountry.SelectedValue == "") ? 239 : Convert.ToInt32(ddlPerCountry.SelectedValue); //239:N/A

                    stud.Phone1 = tbPhone1.Text;
                    stud.Phone2 = tbPhone2.Text;
                    stud.Email1 = tbEmail1.Text;
                    stud.Email2 = tbEmail2.Text;
                    stud.Fax = tbFax.Text;

                    stud.DOB = Convert.ToDateTime(tbDateOfBirth.SelectedDate);
                    stud.StudentType = Convert.ToInt32(ddlStudentType.SelectedValue);
                    stud.Passport = tbPassport.Text;
                    stud.LoanNo = tbLoanNo.Text;

                    stud.ContactName = tbContactName.Text;
                    stud.ContactPhone = tbContactPhone.Text;
                    stud.ContactRelationship = tbContactRelationship.Text;
                    stud.Comment = tbComment.Text;

                    stud.Gender = Convert.ToBoolean(ddlGender.SelectedValue);
                    //stud.MarketerId = Convert.ToInt32(ddlmarketer.SelectedValue);

                    stud.VisaStatus = Convert.ToInt32(ddlStatus.SelectedValue);
                    stud.VisaStart = (tbStatusStartDate.SelectedDate == null) ? Convert.ToDateTime("1900-01-01") : Convert.ToDateTime(tbStatusStartDate.SelectedDate);
                    stud.VisaEnd = (tbStatusEndDate.SelectedDate == null) ? Convert.ToDateTime("1900-01-01") : Convert.ToDateTime(tbStatusEndDate.SelectedDate);
                    stud.WorkPermitStatus = Convert.ToInt32(ddlPermit.SelectedValue);
                    stud.WorkPermitStart = (tbPermitStartDate.SelectedDate == null) ? Convert.ToDateTime("1900-01-01") : Convert.ToDateTime(tbPermitStartDate.SelectedDate);
                    stud.WorkPermitEnd = (tbPermitEndDate.SelectedDate == null) ? Convert.ToDateTime("1900-01-01") : Convert.ToDateTime(tbPermitEndDate.SelectedDate);

                    stud.Insurance = Convert.ToBoolean(ddlInsurance.SelectedValue);
                    stud.InsuranceStart = (tbInsuranceStartDate.SelectedDate == null) ? Convert.ToDateTime("1900-01-01") : Convert.ToDateTime(tbInsuranceStartDate.SelectedDate);
                    stud.InsuranceEnd = (tbInsuranceEndtDate.SelectedDate == null) ? Convert.ToDateTime("1900-01-01") : Convert.ToDateTime(tbInsuranceEndtDate.SelectedDate);
                    stud.InsuranceDayFee = (tbInsuranceDayFee.Text == null || tbInsuranceDayFee.Text == "") ? 0 : Convert.ToDecimal(tbInsuranceDayFee.Text);
                    stud.InsuranceTotal = (tbInsuranceTotalAmt.Text == null || tbInsuranceTotalAmt.Text == "") ? 0 : Convert.ToDecimal(tbInsuranceTotalAmt.Text);

                    stud.CountryId = Convert.ToInt32(ddlCountry.SelectedValue);

                    stud.UpdatedId = CurrentUserId;

                    if (cStud.Update(stud))
                    {
                        // UP LOAD
                        FileDownloadList1.SaveFile(Convert.ToInt32(RadGridStudentList.SelectedValue));

                        ShowMessage("Update inqury successfully");
                    }
                    else
                    {
                        ShowMessage("Failed to update inqury");
                    }
                    RadGridStudentList.Rebind();
                }
            }
        }

        protected void GetStudent()
        {
            ResetForm();

            if (RadGridStudentList.SelectedValue != null && RadGridStudentList.SelectedValue.ToString() != "")
            {
                var cStud = new CStudent();
                var stud = cStud.Get(Convert.ToInt32(RadGridStudentList.SelectedValue));

                if (stud.StudentId > 0)
                {
                    var siteLocation = new CSiteLocation().Get(stud.SiteLocationId);
                    LoadSite(siteLocation.SiteId);
                    LoadSiteLocation(siteLocation.SiteId);
                    RadComboBoxSite.SelectedValue = siteLocation.SiteId.ToString();
                    RadComboBoxSiteLocation.SelectedValue = siteLocation.SiteLocationId.ToString();

                    ddlmarketer.SelectedValue = stud.MarketerId.ToString();

                    tbFirstName.Text = stud.FirstName;
                    tbMiddleName1.Text = stud.MiddleName1;
                    tbMiddleName2.Text = stud.MiddleName2;
                    tbLastName1.Text = stud.LastName1;
                    tbLastName2.Text = stud.LastName2;

                    ddlGender.SelectedValue = stud.Gender.ToString();
                    tbDateOfBirth.SelectedDate = stud.DOB;

                    var age = DateTime.Now.Year - stud.DOB.Value.Year;

                    if (age < 12)
                        ddlAgeSegregation.SelectedValue = "3";
                    else if (age < 18)
                        ddlAgeSegregation.SelectedValue = "2";
                    else
                        ddlAgeSegregation.SelectedValue = "1";

                    ddlStudentType.SelectedValue = stud.StudentType.ToString();
                    ddlCountry.SelectedValue = stud.CountryId.ToString();

                    tbPhone1.Text = stud.Phone1;
                    tbPhone2.Text = stud.Phone2;

                    tbEmail1.Text = stud.Email1;
                    tbEmail2.Text = stud.Email2;

                    tbPassport.Text = stud.Passport;
                    tbLoanNo.Text = stud.LoanNo;

                    tbComment.Text = stud.Comment;

                    //tbStudentMasterNo.Text = stud.StudentMasterNo;

                    tbContactName.Text = stud.ContactName;
                    tbContactRelationship.Text = stud.ContactRelationship;
                    tbContactPhone.Text = stud.ContactPhone;

                    tbPerAddress.Text = stud.PermanentAddress1;
                    tbPerCity.Text = stud.PermanentCity;
                    tbPerState.Text = stud.PermanentProvince;
                    tbPerZiocode.Text = stud.PermanentPostalCode;
                    ddlPerCountry.SelectedValue = (stud.PermanentCountry.ToString() == null || stud.PermanentCountry.ToString() == "") ? "0" : stud.PermanentCountry.ToString();

                    tbCadAddress.Text = stud.Address1InCanada;
                    tbCadCity.Text = stud.CityInCanada;
                    tbCadProvince.Text = stud.ProvinceInCanada;
                    tbCadZipcode.Text = stud.PostalCodeInCanada;

                    ddlInsurance.SelectedValue = stud.Insurance.ToString();
                    //if (ddlInsurance.SelectedValue == "False")
                    //    tbInsuranceStartDate.Visible = false;
                    //else
                    //    tbInsuranceStartDate.Visible = true;

                    if (stud.InsuranceStart > Convert.ToDateTime("1900-01-01"))
                        tbInsuranceStartDate.SelectedDate = stud.InsuranceStart;
                    else
                        tbInsuranceStartDate.SelectedDate = null;
                    if (stud.InsuranceEnd > Convert.ToDateTime("1900-01-01"))
                        tbInsuranceEndtDate.SelectedDate = stud.InsuranceEnd;
                    else
                        tbInsuranceEndtDate.SelectedDate = null;
                    tbInsuranceDayFee.Text = stud.InsuranceDayFee.ToString();
                    tbInsuranceTotalAmt.Text = stud.InsuranceTotal.ToString();

                    ddlStatus.SelectedValue = (stud.VisaStatus.ToString() == null || stud.VisaStatus.ToString() == "") ? "120" : stud.VisaStatus.ToString();
                    tbStatusStartDate.SelectedDate = stud.VisaStart;
                    tbStatusEndDate.SelectedDate = stud.VisaEnd;
                    if (stud.VisaStart > Convert.ToDateTime("1900-01-01"))
                        tbStatusStartDate.SelectedDate = stud.VisaStart;
                    else
                        tbStatusStartDate.SelectedDate = null;
                    if (stud.VisaEnd > Convert.ToDateTime("1900-01-01"))
                        tbStatusEndDate.SelectedDate = stud.VisaEnd;
                    else
                        tbStatusEndDate.SelectedDate = null;

                    ddlPermit.SelectedValue = (stud.WorkPermitStatus.ToString() == null || stud.WorkPermitStatus.ToString() == "") ? "126" : stud.WorkPermitStatus.ToString();
                    tbPermitStartDate.SelectedDate = stud.WorkPermitStart;
                    tbPermitEndDate.SelectedDate = stud.WorkPermitEnd;
                    if (stud.WorkPermitStart > Convert.ToDateTime("1900-01-01"))
                        tbPermitStartDate.SelectedDate = stud.WorkPermitStart;
                    else
                        tbPermitStartDate.SelectedDate = null;
                    if (stud.WorkPermitEnd > Convert.ToDateTime("1900-01-01"))
                        tbPermitEndDate.SelectedDate = stud.WorkPermitEnd;
                    else
                        tbPermitEndDate.SelectedDate = null;

                    FileDownloadList1.GetFileDownload(Convert.ToInt32(RadGridStudentList.SelectedValue));
                }
            }
        }

        protected void ResetForm()
        {
            var global = new CGlobal();

            ddlCountry.Text = string.Empty;
            ddlCountry.Items.Clear();
            ddlCountry.DataSource = global.GetCountry();
            ddlCountry.DataTextField = "Name";
            ddlCountry.DataValueField = "Value";
            ddlCountry.Filter = (RadComboBoxFilter)Convert.ToInt32(1);
            ddlCountry.DataBind();

            ddlPerCountry.Text = string.Empty;
            ddlPerCountry.Items.Clear();
            ddlPerCountry.DataSource = global.GetCountry();
            ddlPerCountry.DataTextField = "Name";
            ddlPerCountry.DataValueField = "Value";
            ddlPerCountry.Filter = (RadComboBoxFilter)Convert.ToInt32(1);
            ddlPerCountry.DataBind();

            ddlStatus.Text = string.Empty;
            ddlStatus.Items.Clear();
            ddlStatus.DataSource = global.GetDictionary(15);
            ddlStatus.DataTextField = "Name";
            ddlStatus.DataValueField = "Value";
            ddlStatus.DataBind();

            ddlPermit.Text = string.Empty;
            ddlPermit.Items.Clear();
            ddlPermit.DataSource = global.GetDictionary(16);
            ddlPermit.DataTextField = "Name";
            ddlPermit.DataValueField = "Value";
            ddlPermit.DataBind();

            var marketer = new CUser();
            ddlmarketer.Text = string.Empty;
            ddlmarketer.Items.Clear();
            ddlmarketer.DataSource = marketer.GetMarketerList(CurrentSiteId);
            ddlmarketer.DataTextField = "Name";
            ddlmarketer.DataValueField = "Value";
            ddlmarketer.DataBind();
            ddlmarketer.Items.Insert(0, new RadComboBoxItem("N/A", null));

            LoadSite(CurrentSiteId);
            LoadSiteLocation(0);

            tbFirstName.Text = "";
            tbMiddleName1.Text = "";
            tbMiddleName2.Text = "";
            tbLastName1.Text = "";
            tbLastName2.Text = "";

            ddlGender.SelectedValue = "";
            tbDateOfBirth.SelectedDate = null;

            ddlStudentType.SelectedValue = "";
            //ddlCountry.SelectedValue = "239";

            tbPhone1.Text = "";
            tbPhone2.Text = "";

            tbEmail1.Text = "";
            tbEmail2.Text = "";

            tbPassport.Text = "";
            tbLoanNo.Text = "";

            tbComment.Text = "";

            //tbStudentMasterNo.Text = "";

            tbContactName.Text = "";
            tbContactRelationship.Text = "";
            tbContactPhone.Text = "";

            tbPerAddress.Text = "";
            tbPerCity.Text = "";
            tbPerState.Text = "";
            tbPerZiocode.Text = "";
            ddlPerCountry.SelectedValue = "";

            tbCadAddress.Text = "";
            tbCadCity.Text = "";
            tbCadProvince.Text = "";
            tbCadZipcode.Text = "";

            ddlInsurance.SelectedValue = "";
            tbInsuranceStartDate.SelectedDate = null;
            tbInsuranceEndtDate.SelectedDate = null;
            tbInsuranceDayFee.Text = "";
            tbInsuranceTotalAmt.Text = "";

            ddlStatus.SelectedValue = "";
            tbStatusStartDate.SelectedDate = null;
            tbStatusEndDate.SelectedDate = null;

            ddlPermit.SelectedValue = "";
            tbPermitStartDate.SelectedDate = null;
            tbPermitEndDate.SelectedDate = null;
        }

        protected void LoadSiteLocation(int siteId)
        {
            var global = new CGlobal();
            RadComboBoxSiteLocation.Items.Clear();
            RadComboBoxSiteLocation.Text = string.Empty;
            RadComboBoxSiteLocation.DataSource = global.GetSiteLocationBySiteId(siteId);
            RadComboBoxSiteLocation.DataTextField = "Name";
            RadComboBoxSiteLocation.DataValueField = "Value";
            RadComboBoxSiteLocation.DataBind();
        }

        protected void LoadSite(int siteId)
        {
            var global = new CGlobal();
            RadComboBoxSite.Items.Clear();
            RadComboBoxSite.Text = string.Empty;
            RadComboBoxSite.DataSource = global.GetSiteId(siteId);
            RadComboBoxSite.DataTextField = "Name";
            RadComboBoxSite.DataValueField = "Value";
            RadComboBoxSite.DataBind();
        }

        protected void RadComboBoxSite_OnSelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadSiteLocation(Convert.ToInt32(RadComboBoxSite.SelectedValue));
            RadComboBoxSiteLocation.OpenDropDownOnLoad = true;
        }

        private void GetStudentContract()
        {
            if (RadGridStudentList.SelectedValue != null)
            {
                LinqDataSourceStudentContract.WhereParameters.Clear();
                LinqDataSourceStudentContract.WhereParameters.Add("StudentId", DbType.Int32, RadGridStudentList.SelectedValues["StudentId"].ToString());
                LinqDataSourceStudentContract.Where = "StudentId == @StudentId";
            }
        }

        protected void Refresh(object sender, EventArgs e)
        {
            GetStudent();
            GetStudentContract();
        }

        protected void RadToolBar3_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Text)
            {
                case "New Student":
                    RunClientScript("ShowNewPop();");
                    break;

                case "Invoice Page":
                    if (RadGridStudentList.SelectedValue != null)
                        Response.Redirect("~/Invoice?id=" + RadGridStudentList.SelectedValues["StudentId"]);
                    break;
                case "Payment Page":
                    if (RadGridStudentList.SelectedValue != null)
                        Response.Redirect("~/Payment?id=" + RadGridStudentList.SelectedValues["StudentId"]);
                    break;
                case "Deposit Page":
                    if (RadGridStudentList.SelectedValue != null)
                        Response.Redirect("~/Deposit?id=" + RadGridStudentList.SelectedValues["StudentId"]);
                    break;
                case "CreditMemo Page":
                    if (RadGridStudentList.SelectedValue != null)
                        Response.Redirect("~/CreditMemo?id=" + RadGridStudentList.SelectedValues["StudentId"]);
                    break;
                case "Refund Page":
                    if (RadGridStudentList.SelectedValue != null)
                        Response.Redirect("~/Refund?id=" + RadGridStudentList.SelectedValues["StudentId"]);
                    break;
            }
        }

        protected void ButtonGridRefresh_OnClick(object sender, EventArgs e)
        {
            RadGridStudentList.Rebind();

            GetStudent();
            GetStudentContract();
        }

        protected void RadGridStudentList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var btnNewProgram = RadToolBarStudentContract.FindItemByText("New Program");
            var btnNewPackageProgram = RadToolBarStudentContract.FindItemByText("New Package");
            var btnNewManualInvoice = RadToolBarStudentContract.FindItemByText("New Manual Invoice");
            var btnNewHomesaty = RadToolBarStudentContract.FindItemByText("New Homestay");
            var btnNewDormitory = RadToolBarStudentContract.FindItemByText("New Dormitory");
            if (RadGridStudentList.SelectedValue == null)
            {
                btnNewProgram.Enabled = false;
                btnNewPackageProgram.Enabled = false;
                btnNewManualInvoice.Enabled = false;
                btnNewHomesaty.Enabled = false;
                btnNewDormitory.Enabled = false;
            }
            else
            {
                btnNewProgram.Enabled = true;
                btnNewPackageProgram.Enabled = true;
                btnNewManualInvoice.Enabled = true;
                btnNewHomesaty.Enabled = true;
                btnNewDormitory.Enabled = true;
            }

            GetStudent();
            GetStudentContract();
        }

        protected void RadToolBarStudentContract_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Text)
            {
                case "New Program":
                    if (RadGridStudentList.SelectedValue != null)
                        RunClientScript("ShowRegProgramNewWindow(" + RadGridStudentList.SelectedValue + ");");
                    break;
                case "New Package":
                    if (RadGridStudentList.SelectedValue != null)
                        RunClientScript("ShowRegPackageProgramNewWindow(" + RadGridStudentList.SelectedValue + ");");
                    break;
                case "New Manual Invoice":
                    if (RadGridStudentList.SelectedValue != null)
                    {
                        var cInvoice = new CInvoice();
                        var invoice = new Erp2016.Lib.Invoice
                        {
                            StudentId = Convert.ToInt32(RadGridStudentList.SelectedValue),
                            SiteLocationId = CurrentSiteLocationId,
                            InvoiceType = (int)CConstValue.InvoiceType.Manual, // manual invoice,
                            Status = (int)CConstValue.InvoiceStatus.Pending, // pending
                            CreatedId = CurrentUserId,
                            CreatedDate = DateTime.Now
                        };

                        if (cInvoice.Add(invoice) > 0)
                            ShowMessage("Manual Invoice has been created.");
                        else
                            ShowMessage("Failed to insert inqury");

                        GetStudentContract();
                    }
                    break;

                case "New Homestay":
                    if (RadGridStudentList.SelectedValue != null)
                        RunClientScript("ShowNewHomestayNewWindow(0," + RadGridStudentList.SelectedValue + ",0);");
                    break;

                case "New Dormitory":
                    if (RadGridStudentList.SelectedValue != null)
                        RunClientScript("ShowNewDormitoryNewWindow(0," + RadGridStudentList.SelectedValue + ",0);");  // Modify Dormitory Request
                    break;

                case "View Invoice":
                    if (RadGridStudentContract.SelectedValue != null)
                        RunClientScript("ShowInvoiceWindow(" + RadGridStudentContract.SelectedValue + ");");
                    break;
                case "Refund":
                    if (RadGridStudentContract.SelectedValue != null)
                        RunClientScript("ShowRefundWindow(" + RadGridStudentContract.SelectedValue + ");");
                    break;
                case "Transfer":
                    if (RadGridStudentContract.SelectedValue != null)
                        RunClientScript("ShowTransferWindow(" + RadGridStudentContract.SelectedValue + ");");
                    break;
                case "Break":
                    if (RadGridStudentContract.SelectedValue != null)
                        RunClientScript("ShowBreakWindow(" + RadGridStudentContract.SelectedValue + ");");
                    break;
                case "Cancel":
                    if (RadGridStudentContract.SelectedValue != null)
                        RunClientScript("ShowCancelWindow(" + RadGridStudentContract.SelectedValue + ");");
                    break;
                case "Program Change":
                    if (RadGridStudentContract.SelectedValue != null)
                        RunClientScript("ShowProgramChangeWindow(" + RadGridStudentContract.SelectedValue + ");");
                    break;
                case "Schedule Change":
                    if (RadGridStudentContract.SelectedValue != null)
                        RunClientScript("ShowScheduleChangeWindow(" + RadGridStudentContract.SelectedValue + ");");
                    break;

                // Schools
                case "Letter Of Acceptance":
                    if (RadGridStudentContract.SelectedValue != null)
                        RunClientScript("ShowReportPop('" + RadGridStudentContract.SelectedValue + "', '" + (int)CConstValue.Report.LetterOfAcceptance + "' );");
                    break;
                case "Letter Of Acceptance in table":
                    if (RadGridStudentContract.SelectedValue != null)
                        RunClientScript("ShowReportPop('" + RadGridStudentContract.SelectedValue + "', '" + (int)CConstValue.Report.LetterOfAcceptanceInTable + "' );");
                    break;
                case "Student Contract":
                    if (RadGridStudentContract.SelectedValue != null)
                        RunClientScript("ShowReportPop('" + RadGridStudentContract.SelectedValue + "', '" + (int)CConstValue.Report.StudentContract + "' );");
                    break;
                case "Orientation Form":
                    if (RadGridStudentContract.SelectedValue != null)
                        RunClientScript("ShowReportPop('" + RadGridStudentContract.SelectedValue + "', '" + (int)CConstValue.Report.OrientationForm + "' );");
                    break;
                case "Confirmation Of Completion Letter":
                    if (RadGridStudentContract.SelectedValue != null)
                        RunClientScript("ShowReportPop('" + RadGridStudentContract.SelectedValue + "', '" + (int)CConstValue.Report.ConfirmationOfCompletionLetter + "' );");
                    break;
                case "Confirmation Of Enrollment":
                    if (RadGridStudentContract.SelectedValue != null)
                        RunClientScript("ShowReportPop('" + RadGridStudentContract.SelectedValue + "', '" + (int)CConstValue.Report.ConfirmationOfEnrollment + "' );");
                    break;

                // Academy
                case "Certification":
                    if (RadGridStudentContract.SelectedValue != null)
                        RunClientScript("ShowReportPop('" + RadGridStudentContract.SelectedValue + "', '" + (int)CConstValue.Report.Certification + "' );");
                    break;
            }
        }

        protected void RadGridStudentContract_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadGridStudentContract.SelectedValue != null)
            {
                CStudent student = new CStudent();
                var studentContract = student.GetVwStudentContract(Convert.ToInt32(RadGridStudentContract.SelectedValue));
                if (studentContract != null)
                {
                    var btnRefund = RadToolBarStudentContract.FindItemByText("Refund");
                    var btnTransfer = RadToolBarStudentContract.FindItemByText("Transfer");
                    var btnCancel = RadToolBarStudentContract.FindItemByText("Cancel");
                    var btnBreak = RadToolBarStudentContract.FindItemByText("Break");
                    var btnScheduleChange = RadToolBarStudentContract.FindItemByText("Schedule Change");
                    var btnProgramChange = RadToolBarStudentContract.FindItemByText("Program Change");

                    // init
                    btnRefund.Enabled = false;
                    btnTransfer.Enabled = false;
                    btnCancel.Enabled = false;
                    btnBreak.Enabled = false;
                    btnScheduleChange.Enabled = false;
                    btnProgramChange.Enabled = false;

                    var balance = studentContract.Balance;

                    var paymentCnt = studentContract.PaymentCnt;
                    var depositConfirmCnt = studentContract.DepositConfirmCnt;

                    var invoiceStatus = studentContract.Status;
                    var invoiceType = studentContract.InvoiceTypeInt;
                    var invoiceId = studentContract.InvoiceId;

                    var startDate = studentContract.StartDate;
                    var today = DateTime.Now;

                    // =======================
                    // Refund
                    // =======================
                    // Full Paid
                    if (balance >= 0 && paymentCnt > 0 && paymentCnt == depositConfirmCnt && invoiceStatus == (int)CConstValue.InvoiceStatus.Invoiced)
                    {
                        switch (invoiceType)
                        {
                            case (int)CConstValue.InvoiceType.Simple:
                            case (int)CConstValue.InvoiceType.General:
                            case (int)CConstValue.InvoiceType.Manual:
                            case (int)CConstValue.InvoiceType.Dormitory:
                            case (int)CConstValue.InvoiceType.Homestay:
                                btnRefund.Enabled = true;
                                break;
                        }
                    }

                    // =======================
                    // Cancel
                    // =======================
                    // Before Program Start
                    if (startDate > today)
                    {
                        switch (invoiceType)
                        {
                            case (int)CConstValue.InvoiceType.Simple:
                            case (int)CConstValue.InvoiceType.General:
                            case (int)CConstValue.InvoiceType.Manual:
                            case (int)CConstValue.InvoiceType.Dormitory:
                            case (int)CConstValue.InvoiceType.Homestay:

                                if (invoiceStatus == (int)CConstValue.InvoiceStatus.Pending)
                                {
                                    btnCancel.Enabled = true;
                                }
                                else if (invoiceStatus == (int)CConstValue.InvoiceStatus.Invoiced)
                                {
                                    if (new CPayment().InvoiceCheck(invoiceId) == 0)
                                        btnCancel.Enabled = true;
                                }
                                break;
                        }
                    }

                    // =======================
                    // Break
                    // =======================
                    // After Program Start
                    if (startDate <= today)
                    {
                        switch (invoiceType)
                        {
                            case (int)CConstValue.InvoiceType.Simple:
                            case (int)CConstValue.InvoiceType.General:
                            case (int)CConstValue.InvoiceType.Manual:
                                btnBreak.Enabled = true;
                                break;
                        }
                    }

                    // =======================
                    // Schedule change
                    // =======================
                    // Before Program Start
                    if (startDate > today)
                    {
                        switch (invoiceType)
                        {
                            case (int)CConstValue.InvoiceType.Simple:
                            case (int)CConstValue.InvoiceType.General:
                            case (int)CConstValue.InvoiceType.Manual:
                            case (int)CConstValue.InvoiceType.Dormitory:
                            case (int)CConstValue.InvoiceType.Homestay:
                                if (invoiceStatus == (int)CConstValue.InvoiceStatus.Pending || invoiceStatus == (int)CConstValue.InvoiceStatus.Invoiced)
                                    btnScheduleChange.Enabled = true;
                                break;
                        }
                    }

                    // =======================
                    // Program Change
                    // =======================
                    // nothing 

                    // =======================
                    // Transfer
                    // =======================
                    // nothing 
                }

            }
        }

        protected void RadGridStudentList_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        private void Excel()
        {
            RadGridStudentList.ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), "Xlsx");
            RadGridStudentList.ExportSettings.OpenInNewWindow = true;
            RadGridStudentList.ExportSettings.ExportOnlyData = true;
            RadGridStudentList.ExportSettings.IgnorePaging = true;

            RadGridStudentList.MasterTableView.ExportToExcel();
        }

        public override void SetVisibleModifyControllers()
        {
            if (UserPermissionModel.IsModify == false)
            {
                // toolbar
                foreach (RadToolBarItem toolbarItem in RadToolBar1.Items)
                {
                    toolbarItem.Enabled = false;
                }
                // toolbar
                foreach (RadToolBarItem toolbarItem in RadToolBar3.Items)
                {
                    toolbarItem.Enabled = false;
                }
                // toolbar
                foreach (RadToolBarItem toolbarItem in RadToolBarStudentContract.Items)
                {
                    if (toolbarItem.Text == "View Invoice") continue;

                    toolbarItem.Enabled = false;
                }
            }
        }

        protected void RadTabStript1_OnTabClick(object sender, RadTabStripEventArgs e)
        {
            if (e.Tab.Text == "Contract")
                RadGridStudentList_OnSelectedIndexChanged(null, null);
        }

    }
}