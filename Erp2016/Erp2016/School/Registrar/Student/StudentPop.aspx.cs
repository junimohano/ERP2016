using System;
using Erp2016.Lib;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;

namespace School.Registrar.Student
{
    public partial class StudentPop : PageBase //System.Web.UI.Page
    {
        public StudentPop() : base((int)CConstValue.Menu.Student)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FileDownloadList1.InitFileDownloadList((int)CConstValue.Upload.Student);

                LoadSite(CurrentSiteId);
                LoadSiteLocation(CurrentSiteId);

                var global = new CGlobal();

                ddlCountry.DataSource = global.GetCountry();
                ddlCountry.DataTextField = "Name";
                ddlCountry.DataValueField = "Value";
                ddlCountry.Filter = (RadComboBoxFilter)Convert.ToInt32(1);
                ddlCountry.DataBind();
                ddlCountry.SelectedIndex = 35; //canada

                ddlPerCountry.DataSource = global.GetCountry();
                ddlPerCountry.DataTextField = "Name";
                ddlPerCountry.DataValueField = "Value";
                ddlPerCountry.Filter = (RadComboBoxFilter)Convert.ToInt32(1);
                ddlPerCountry.DataBind();
                ddlPerCountry.SelectedIndex = 35; // canada

                ddlStatus.DataSource = global.GetDictionary(15);
                ddlStatus.DataTextField = "Name";
                ddlStatus.DataValueField = "Value";
                ddlStatus.DataBind();
                ddlStatus.SelectedIndex = 0;

                ddlPermit.DataSource = global.GetDictionary(16);
                ddlPermit.DataTextField = "Name";
                ddlPermit.DataValueField = "Value";
                ddlPermit.DataBind();
                ddlPermit.SelectedIndex = 0;

                var marketer = new CUser();
                ddlmarketer.DataSource = marketer.GetMarketerList(CurrentSiteId);
                ddlmarketer.DataTextField = "Name";
                ddlmarketer.DataValueField = "Value";
                ddlmarketer.DataBind();
                ddlmarketer.Items.Insert(0, new RadComboBoxItem("N/A", null));
                bool isSelected = false;
                foreach (RadComboBoxItem item in ddlmarketer.Items)
                {
                    if (item.Value == CurrentUserId.ToString())
                    {
                        item.Selected = true;
                        isSelected = true;
                        break;
                    }
                }
                if (isSelected == false)
                    ddlmarketer.SelectedIndex = 0;

                tbInsuranceStartDate.Enabled = false;
                tbInsuranceEndtDate.Enabled = false;
                tbInsuranceDayFee.Enabled = false;
                tbInsuranceTotalAmt.Enabled = false;
            }
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
            foreach (RadComboBoxItem item in RadComboBoxSite.Items)
            {
                if (item.Value == CurrentSiteId.ToString())
                {
                    item.Selected = true;
                    break;
                }
            }
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

            foreach (RadComboBoxItem item in RadComboBoxSiteLocation.Items)
            {
                if (item.Value == CurrentSiteLocationId.ToString())
                {
                    item.Selected = true;
                    break;
                }
            }
        }

        protected void tbDateOfBirth_OnSelectedDateChanged(object o, SelectedDateChangedEventArgs e)
        {
            var age = DateTime.Now.Year - Convert.ToDateTime(tbDateOfBirth.SelectedDate).Year;

            if (age < 12)
                ddlAgeSegregation.SelectedValue = "3";
            else if (age < 18)
                ddlAgeSegregation.SelectedValue = "2";
            else
                ddlAgeSegregation.SelectedValue = "1";
        }

        protected void StudentButtonClicked(object sender, RadToolBarEventArgs e)
        {
            if (e.Item.Text == @"Save")
            {
                if (IsValid)
                {
                    var cStudentRegi = new CStudent();
                    var studentRegi = new Erp2016.Lib.Student();

                    studentRegi.AcademicStatus = 1; // 1:NEW
                                                    //studentRegi.IsActive = ;
                                                    //studentRegi.RegisterDate = ;

                    studentRegi.SiteLocationId = Convert.ToInt32(RadComboBoxSiteLocation.SelectedValue);
                    studentRegi.FirstName = tbFirstName.Text;
                    studentRegi.LastName1 = tbLastName1.Text;
                    studentRegi.LastName2 = tbLastName2.Text;
                    studentRegi.MiddleName1 = tbMiddleName1.Text;
                    studentRegi.MiddleName2 = tbMiddleName2.Text;

                    studentRegi.Address1InCanada = tbCadAddress.Text;
                    studentRegi.CityInCanada = tbCadCity.Text;
                    studentRegi.ProvinceInCanada = tbCadProvince.Text;
                    studentRegi.PostalCodeInCanada = tbCadZipcode.Text;

                    studentRegi.PermanentAddress1 = tbPerAddress.Text;
                    studentRegi.PermanentCity = tbPerCity.Text;
                    studentRegi.PermanentProvince = tbPerState.Text;
                    studentRegi.PermanentPostalCode = tbPerZiocode.Text;
                    studentRegi.PermanentCountry = Convert.ToInt32(ddlPerCountry.SelectedValue);

                    studentRegi.Phone1 = tbPhone1.Text;
                    studentRegi.Phone2 = tbPhone2.Text;
                    studentRegi.Email1 = tbEmail1.Text;
                    studentRegi.Email2 = tbEmail2.Text;
                    studentRegi.Fax = tbFax.Text;

                    studentRegi.DOB = Convert.ToDateTime(tbDateOfBirth.SelectedDate);
                    studentRegi.StudentType = Convert.ToInt32(ddlStudentType.SelectedValue);
                    studentRegi.Passport = tbPassport.Text;
                    studentRegi.LoanNo = tbLoanNo.Text;

                    studentRegi.ContactName = tbContactName.Text;
                    studentRegi.ContactPhone = tbContactPhone.Text;
                    studentRegi.ContactRelationship = tbContactRelationship.Text;
                    studentRegi.Comment = tbComment.Text;

                    studentRegi.Gender = Convert.ToBoolean(ddlGender.SelectedValue);
                    if (!string.IsNullOrEmpty(ddlmarketer.SelectedValue))
                        studentRegi.MarketerId = Convert.ToInt32(ddlmarketer.SelectedValue);

                    studentRegi.VisaStatus = Convert.ToInt32(ddlStatus.SelectedValue);
                    studentRegi.VisaStart = (tbStatusStartDate.SelectedDate == null) ? Convert.ToDateTime("1900-01-01") : Convert.ToDateTime(tbStatusStartDate.SelectedDate);
                    studentRegi.VisaEnd = (tbStatusEndDate.SelectedDate == null) ? Convert.ToDateTime("1900-01-01") : Convert.ToDateTime(tbStatusEndDate.SelectedDate);
                    studentRegi.WorkPermitStatus = Convert.ToInt32(ddlPermit.SelectedValue);
                    studentRegi.WorkPermitStart = (tbPermitStartDate.SelectedDate == null) ? Convert.ToDateTime("1900-01-01") : Convert.ToDateTime(tbPermitStartDate.SelectedDate);
                    studentRegi.WorkPermitEnd = (tbPermitEndDate.SelectedDate == null) ? Convert.ToDateTime("1900-01-01") : Convert.ToDateTime(tbPermitEndDate.SelectedDate);

                    studentRegi.Insurance = Convert.ToBoolean(ddlInsurance.SelectedValue);
                    studentRegi.InsuranceStart = (tbInsuranceStartDate.SelectedDate == null) ? Convert.ToDateTime("1900-01-01") : Convert.ToDateTime(tbInsuranceStartDate.SelectedDate);
                    studentRegi.InsuranceEnd = (tbInsuranceEndtDate.SelectedDate == null) ? Convert.ToDateTime("1900-01-01") : Convert.ToDateTime(tbInsuranceEndtDate.SelectedDate);
                    studentRegi.InsuranceDayFee = (tbInsuranceDayFee.Text == null || tbInsuranceDayFee.Text == "") ? 0 : Convert.ToDecimal(tbInsuranceDayFee.Text);
                    studentRegi.InsuranceTotal = (tbInsuranceTotalAmt.Text == null || tbInsuranceTotalAmt.Text == "") ? 0 : Convert.ToDecimal(tbInsuranceTotalAmt.Text);

                    studentRegi.CountryId = Convert.ToInt32(ddlCountry.SelectedValue);

                    studentRegi.CreatedId = CurrentUserId;

                    int newStudentId = cStudentRegi.Add(studentRegi);
                    if (newStudentId > 0)
                    {
                        // UP LOAD
                        FileDownloadList1.SaveFile(newStudentId);

                        RunClientScript("Save();");
                    }
                    else
                    {
                        ShowMessage("failed to update the Student');");
                    }
                    return;
                }
                else
                {
                    ShowMessage("Please check wrong values");
                }
            }
            else if (e.Item.Text == @"Cancel")
            {
            }
        }

        protected void ddlInsurance_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (e.Value == "True")
            {
                tbInsuranceStartDate.Enabled = true;
                tbInsuranceEndtDate.Enabled = true;
                tbInsuranceDayFee.Enabled = true;
                tbInsuranceTotalAmt.Enabled = true;
            }
            else
            {
                tbInsuranceStartDate.Clear();
                tbInsuranceEndtDate.Clear();
                tbInsuranceDayFee.Text = "";
                tbInsuranceTotalAmt.Text = "";

                tbInsuranceStartDate.Enabled = false;
                tbInsuranceEndtDate.Enabled = false;
                tbInsuranceDayFee.Enabled = false;
                tbInsuranceTotalAmt.Enabled = false;
            }
        }

        protected void tbInsuranceDayFee_TextChanged(object sender, EventArgs e)
        {
            var dayFee = sender as RadNumericTextBox;
            var start = (tbInsuranceStartDate.SelectedDate != null) ? Convert.ToDateTime(tbInsuranceStartDate.SelectedDate) : Convert.ToDateTime("1900-01-01");
            var end = (tbInsuranceEndtDate.SelectedDate != null) ? Convert.ToDateTime(tbInsuranceEndtDate.SelectedDate) : Convert.ToDateTime("1900-01-01");
            if (dayFee.Text != "" && Convert.ToInt32(dayFee.Text) > 0 && start > Convert.ToDateTime("1900-01-01") && end > Convert.ToDateTime("1900-01-01") && end > start)
            {
                var period = end - start;
                tbInsuranceTotalAmt.Value = Convert.ToInt32(dayFee.Text) * period.Days;
            }
            else
            {
                tbInsuranceTotalAmt.Value = 0;
            }
        }

        protected void RadComboBoxSite_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadSiteLocation(Convert.ToInt32(RadComboBoxSite.SelectedValue));
        }
    }
}