using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.Registrar
{
    public partial class Agency : PageBase
    {
        public Agency() : base((int)CConstValue.Menu.Agency)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ResetForm();
            }

            SearchAgency();
        }

        private void GetSiteLocation()
        {
            RadComboBoxSiteLocation.Items.Clear();
            List<SiteLocation> siteLocationList = new List<SiteLocation>();
            if (RadGridAgency.SelectedValue != null)
            {
                var cAgencySiteLocation = new CAgencySiteLocation();
                var agencySiteLocation = cAgencySiteLocation.GetAgencySiteLocationList(Convert.ToInt32(RadGridAgency.SelectedValue));
                if (agencySiteLocation.Count > 0)
                {
                    var siteLocation = new CSiteLocation().Get(agencySiteLocation[0].SiteLocationId);
                    siteLocationList = new CSiteLocation().GetSiteLocationBySiteId(siteLocation.SiteId);

                    RadTextBoxSite.Text = (new CSite()).Get(siteLocation.SiteId).Abbreviation;
                }

                foreach (var siteLocation in siteLocationList)
                {
                    RadComboBoxSiteLocation.Items.Add(new RadComboBoxItem(siteLocation.Name, siteLocation.SiteLocationId.ToString()));
                }

                foreach (var agencySiteLo in agencySiteLocation)
                {
                    foreach (RadComboBoxItem siteLocation in RadComboBoxSiteLocation.Items)
                    {
                        if (agencySiteLo.SiteLocationId == Convert.ToInt32(siteLocation.Value))
                        {
                            siteLocation.Checked = true;
                        }
                    }
                }
            }
            else
            {
                var cSiteLocation = new CSiteLocation();
                siteLocationList = cSiteLocation.GetSiteLocationBySiteId(CurrentSiteId);
                foreach (var siteLocation in siteLocationList)
                {
                    RadComboBoxSiteLocation.Items.Add(new RadComboBoxItem(siteLocation.Name, siteLocation.SiteLocationId.ToString()));
                }

                RadTextBoxSite.Text = (new CSite()).Get(CurrentSiteId).Abbreviation;
            }

            RadComboBoxSiteLocation_OnSelectedIndexChanged(null, null);
        }

        protected void AgencyButtonClicked(object sender, RadToolBarEventArgs e)
        {
            if (e.Item.Text == @"New")
            {
                RadGridAgency.SelectedIndexes.Clear();
                ResetForm();
            }
            else if (e.Item.Text == @"Update" && RadGridAgency.SelectedValue != null)
            {
                if (IsValid)
                {
                    var cAgency = new CAgency();
                    var agc = cAgency.Get(Convert.ToInt32(RadGridAgency.SelectedValue));

                    var oriBasicCommission = Convert.ToDouble(agc.CommissionRateBasic);
                    var oriSeasonalCommission = Convert.ToDouble(agc.CommissionRateSeasonal);
                    var oriCreditLimit = Convert.ToDouble(agc.CreditLimit);

                    agc.Name = RadComboBoxAgencyName.Text;
                    agc.Abbreviation = tbAgencyShortName.Text;
                    agc.GroupName = tbAgencyGroupName.Text;
                    agc.PrintingName = tbAgencyPrintName.Text;
                    agc.AgencyType = tbAgencyType.Text;
                    agc.CommissionRateBasic = tbCommissionRateBasic.Value;
                    agc.CommissionRateSeasonal = tbCommissionRateSeason.Value;
                    agc.CreditLimit = tbCreditLimit.Value;
                    agc.Location = Convert.ToInt32(ddlBusinessLocation.SelectedValue);
                    agc.MainTargetCountry = Convert.ToInt32(ddlMainTarget.SelectedValue);
                    if (tbContractStart.SelectedDate != null)
                        agc.ContractStartDate = Convert.ToDateTime(tbContractStart.SelectedDate);
                    if (tbContractEnd.SelectedDate != null)
                        agc.ContractEndDate = Convert.ToDateTime(tbContractEnd.SelectedDate);
                    agc.APPaymentTerm = tbAPPayTerm.Text;
                    agc.APPaymentMethod = tbAPPayMethod.Text;
                    agc.APBillingType = tbAPBillingType.Text;
                    agc.APPaymentPriority = tbAPPAymentPriority.Text;
                    agc.APPaymentSchedule = tbAPPaySchedule.Text;
                    agc.ARCollectionTerm = tbARCollectionTerm.Text;
                    agc.ARType = tbARType.Text;
                    agc.ARCollectionPriority = tbARCollection.Text;
                    agc.ARCollectionSchedule = tbARCollectionSchedule.Text;
                    agc.ARCollectionMethod = tbARCollectionMethod.Text;
                    agc.AgencyRegNo = tbAgencyNo.Text;
                    agc.Currency = tbCurrency.Text;
                    agc.Comment = tbComment.Text;

                    agc.AgencyId = agc.AgencyId;
                    agc.FirstName = agFname.Text;
                    agc.LastName = agLname.Text;
                    agc.Salutation = agTitle.Text;
                    agc.Phone = agPhone.Text;
                    agc.Mobile = agMobile.Text;
                    agc.Fax = agFax.Text;
                    agc.PEmail = agPEmail.Text;
                    agc.SEmail = agSEmail.Text;
                    agc.Website = agWebsite.Text;
                    agc.Address = agAddress.Text;
                    agc.City = agCity.Text;
                    agc.Province = agProvince.Text;
                    agc.Postal = agPostal.Text;
                    if (ddlAgencyCountry.SelectedValue != string.Empty)
                        agc.CountryId = Convert.ToInt32(ddlAgencyCountry.SelectedValue);

                    agc.FirstName1 = agFname1.Text;
                    agc.LastName1 = agLname1.Text;
                    agc.Salutation1 = agTitle1.Text;
                    agc.Phone1 = agPhone1.Text;
                    agc.Mobile1 = agMobile1.Text;
                    agc.Fax1 = agFax1.Text;
                    agc.PEmail1 = agPEmail1.Text;
                    agc.SEmail1 = agSEmail1.Text;
                    agc.Website1 = agWebsite1.Text;
                    agc.Address1 = agAddress1.Text;
                    agc.City1 = agCity1.Text;
                    agc.Province1 = agProvince1.Text;
                    agc.Postal1 = agPostal1.Text;
                    if (ddlAgencyCountry1.SelectedValue != string.Empty)
                        agc.CountryId1 = Convert.ToInt32(ddlAgencyCountry1.SelectedValue);

                    agc.IsActive = RadButtonActive.Checked;
                    agc.UpdatedId = CurrentUserId;
                    agc.UpdatedDate = DateTime.Now;

                    if (oriBasicCommission == tbCommissionRateBasic.Value && oriSeasonalCommission == tbCommissionRateSeason.Value && oriCreditLimit == tbCreditLimit.Value)
                    {
                        cAgency.Update(agc);
                    }
                    else
                    {
                        cAgency.Update(agc);

                        var cHistory = new CAgencyCommissionHistory();
                        var history = new AgencyCommissionHistory();

                        history.AgencyId = agc.AgencyId;

                        history.OriBasicCommission = oriBasicCommission;
                        history.OriSeasonCommission = oriSeasonalCommission;
                        history.OriCreditLimit = oriCreditLimit;

                        history.ChBasicCommission = (double)agc.CommissionRateBasic;
                        history.ChSeasonCommission = (double)agc.CommissionRateSeasonal;
                        history.ChCreditLimit = (double)agc.CreditLimit;

                        history.SiteLocationId = CurrentSiteLocationId;
                        history.UpdatedDate = DateTime.Now;
                        history.UpdatedId = CurrentUserId;

                        cHistory.Add(history);
                    }

                    var cAgencySiteLocation = new CAgencySiteLocation();
                    cAgencySiteLocation.DelAgencySiteLocationList(Convert.ToInt32(RadGridAgency.SelectedValue));

                    foreach (var siteLocation in RadComboBoxSiteLocation.CheckedItems)
                    {
                        var agencySiteLocation = new AgencySiteLocation()
                        {
                            CreatedId = CurrentUserId,
                            AgencyId = Convert.ToInt32(RadGridAgency.SelectedValue),
                            SiteLocationId = Convert.ToInt32(siteLocation.Value),
                            CreatedDate = DateTime.Now
                        };
                        cAgencySiteLocation.Add(agencySiteLocation);
                    }

                    RadGridAgency.Rebind();
                    ShowMessage("Update Agency Info Successfully");
                }
            }
            else if (e.Item.Text == @"Save")
            {
                if (IsValid)
                {
                    var cAgency = new CAgency();
                    var agc = new Erp2016.Lib.Agency();

                    agc.Name = RadComboBoxAgencyName.Text;
                    agc.Abbreviation = tbAgencyShortName.Text;
                    agc.GroupName = tbAgencyGroupName.Text;
                    agc.PrintingName = tbAgencyPrintName.Text;
                    agc.AgencyType = tbAgencyType.Text;
                    agc.CommissionRateBasic = tbCommissionRateBasic.Value;
                    agc.CommissionRateSeasonal = tbCommissionRateSeason.Value;
                    agc.CreditLimit = tbCreditLimit.Value;
                    agc.Location = Convert.ToInt32(ddlBusinessLocation.SelectedValue);
                    agc.MainTargetCountry = Convert.ToInt32(ddlMainTarget.SelectedValue);
                    agc.ContractStartDate = (tbContractStart.SelectedDate == null) ? Convert.ToDateTime("1900-01-01") : Convert.ToDateTime(tbContractStart.SelectedDate);
                    agc.ContractEndDate = (tbContractEnd.SelectedDate == null) ? Convert.ToDateTime("1900-01-01") : Convert.ToDateTime(tbContractEnd.SelectedDate);

                    agc.APPaymentTerm = tbAPPayTerm.Text;
                    agc.APPaymentMethod = tbAPPayMethod.Text;
                    agc.APBillingType = tbAPBillingType.Text;
                    agc.APPaymentPriority = tbAPPAymentPriority.Text;
                    agc.APPaymentSchedule = tbAPPaySchedule.Text;
                    agc.ARCollectionTerm = tbARCollectionTerm.Text;
                    agc.ARType = tbARType.Text;
                    agc.ARCollectionPriority = tbARCollection.Text;
                    agc.ARCollectionSchedule = tbARCollectionSchedule.Text;
                    agc.ARCollectionMethod = tbARCollectionMethod.Text;
                    agc.AgencyRegNo = tbAgencyNo.Text;
                    agc.Currency = tbCurrency.Text;
                    agc.Comment = tbComment.Text;

                    agc.ApprovalDate = DateTime.Now;

                    agc.IsActive = RadButtonActive.Checked;
                    agc.CreatedId = CurrentUserId;
                    agc.CreatedDate = DateTime.Now;

                    agc.AgencyId = agc.AgencyId;
                    agc.FirstName = agFname.Text;
                    agc.LastName = agLname.Text;
                    agc.Salutation = agTitle.Text;
                    agc.Phone = agPhone.Text;
                    agc.Mobile = agMobile.Text;
                    agc.Fax = agFax.Text;
                    agc.PEmail = agPEmail.Text;
                    agc.SEmail = agSEmail.Text;
                    agc.Website = agWebsite.Text;
                    agc.Address = agAddress.Text;
                    agc.City = agAddress.Text;
                    agc.Province = agProvince.Text;
                    agc.Postal = agPostal.Text;
                    if (ddlAgencyCountry.SelectedValue != string.Empty)
                        agc.CountryId = Convert.ToInt32(ddlAgencyCountry.SelectedValue);

                    agc.FirstName1 = agFname1.Text;
                    agc.LastName1 = agLname1.Text;
                    agc.Salutation1 = agTitle1.Text;
                    agc.Phone1 = agPhone1.Text;
                    agc.Mobile1 = agMobile1.Text;
                    agc.Fax1 = agFax1.Text;
                    agc.PEmail1 = agPEmail1.Text;
                    agc.SEmail1 = agSEmail1.Text;
                    agc.Website1 = agWebsite1.Text;
                    agc.Address1 = agAddress1.Text;
                    agc.City1 = agCity1.Text;
                    agc.Province1 = agProvince1.Text;
                    agc.Postal1 = agPostal1.Text;
                    if (ddlAgencyCountry1.SelectedValue != string.Empty)
                        agc.CountryId1 = Convert.ToInt32(ddlAgencyCountry1.SelectedValue);

                    int agencyId = cAgency.Add(agc);
                    if (agencyId > 0)
                    {
                        var cAgencySiteLocation = new CAgencySiteLocation();
                        cAgencySiteLocation.DelAgencySiteLocationList(agencyId);

                        foreach (var siteLocation in RadComboBoxSiteLocation.CheckedItems)
                        {
                            var agencySiteLocation = new AgencySiteLocation()
                            {
                                CreatedId = CurrentUserId,
                                AgencyId = agencyId,
                                SiteLocationId = Convert.ToInt32(siteLocation.Value),
                                CreatedDate = DateTime.Now
                            };
                            cAgencySiteLocation.Add(agencySiteLocation);
                        }

                        RadGridAgency.Rebind();
                        ShowMessage("Add Agency Info Successfully");
                    }
                }
                else
                    ShowMessage("Failed To Add.");
            }
            else if (e.Item.Text == "Old Agency Lookup")
            {
                RunClientScript("ShowAgencyOldInfoWindow();");
            }
        }

        protected void GetAgencyInfo()
        {
            ResetForm();

            if (RadGridAgency.SelectedValue != null)
            {
                var cAgc = new CAgency();
                var agc = cAgc.Get(Convert.ToInt32(RadGridAgency.SelectedValue));

                if (agc.AgencyId > 0)
                {
                    ddlPAgency.SelectedValue = Convert.ToString(agc.ParentAgencyId);
                    RadComboBoxAgencyName.Text = agc.Name;

                    tbAgencyShortName.Text = agc.Abbreviation;
                    tbAgencyGroupName.Text = agc.GroupName;
                    tbAgencyPrintName.Text = agc.PrintingName;
                    tbAgencyType.Text = agc.AgencyType;
                    tbCommissionRateBasic.Text = agc.CommissionRateBasic.ToString();
                    tbCommissionRateSeason.Text = agc.CommissionRateSeasonal.ToString();
                    tbCreditLimit.Text = agc.CreditLimit.ToString();
                    ddlBusinessLocation.SelectedValue = agc.Location.ToString();
                    ddlMainTarget.SelectedValue = agc.MainTargetCountry.ToString();
                    tbContractStart.SelectedDate = agc.ContractStartDate;
                    tbContractEnd.SelectedDate = agc.ContractEndDate;

                    tbAPPayTerm.Text = agc.APPaymentTerm;
                    tbAPPayMethod.Text = agc.APPaymentMethod;
                    tbAPBillingType.Text = agc.APBillingType;
                    tbAPPAymentPriority.Text = agc.APPaymentPriority;
                    tbAPPaySchedule.Text = agc.APPaymentSchedule;
                    tbARCollectionTerm.Text = agc.ARCollectionTerm;
                    tbARType.Text = agc.ARType;
                    tbARCollection.Text = agc.ARCollectionPriority;
                    tbARCollectionSchedule.Text = agc.ARCollectionSchedule;
                    tbARCollectionMethod.Text = agc.ARCollectionMethod;
                    tbAgencyNo.Text = agc.AgencyRegNo;
                    tbCurrency.Text = agc.Currency;
                    tbComment.Text = agc.Comment;
                    RadButtonActive.Checked = agc.IsActive;

                    agFname.Text = agc.FirstName;
                    agLname.Text = agc.LastName;
                    agTitle.Text = agc.Salutation;
                    agPhone.Text = agc.Phone;
                    agMobile.Text = agc.Mobile;
                    agFax.Text = agc.Fax;
                    agPEmail.Text = agc.PEmail;
                    agSEmail.Text = agc.SEmail;
                    agWebsite.Text = agc.Website;
                    agAddress.Text = agc.Address;
                    agCity.Text = agc.City;
                    agProvince.Text = agc.Province;
                    agPostal.Text = agc.Postal;
                    ddlAgencyCountry.SelectedValue = agc.CountryId.ToString();

                    agFname1.Text = agc.FirstName1;
                    agLname1.Text = agc.LastName1;
                    agTitle1.Text = agc.Salutation1;
                    agPhone1.Text = agc.Phone1;
                    agMobile1.Text = agc.Mobile1;
                    agFax1.Text = agc.Fax1;
                    agPEmail1.Text = agc.PEmail1;
                    agSEmail1.Text = agc.SEmail1;
                    agWebsite1.Text = agc.Website1;
                    agAddress1.Text = agc.Address1;
                    agCity1.Text = agc.City1;
                    agProvince1.Text = agc.Province1;
                    agPostal1.Text = agc.Postal1;
                    ddlAgencyCountry1.SelectedValue = agc.CountryId1.ToString();

                    if (RadToolBar1.FindItemByText("New") != null) RadToolBar1.FindItemByText("New").Enabled = true;
                    if (RadToolBar1.FindItemByText("Save") != null) RadToolBar1.FindItemByText("Save").Text = @"Update";
                }
            }
        }

        protected void ResetForm()
        {
            var global = new CGlobal();
            var agcname = new CAgency();

            ddlBusinessLocation.Items.Clear();
            ddlBusinessLocation.Text = string.Empty;
            ddlBusinessLocation.DataSource = global.GetCountry();
            ddlBusinessLocation.DataTextField = "Name";
            ddlBusinessLocation.DataValueField = "Value";
            ddlBusinessLocation.Filter = (RadComboBoxFilter)Convert.ToInt32(1);
            ddlBusinessLocation.DataBind();

            ddlMainTarget.Items.Clear();
            ddlMainTarget.Text = string.Empty;
            ddlMainTarget.DataSource = global.GetCountry();
            ddlMainTarget.DataTextField = "Name";
            ddlMainTarget.DataValueField = "Value";
            ddlMainTarget.Filter = (RadComboBoxFilter)Convert.ToInt32(1);
            ddlMainTarget.DataBind();

            ddlAgencyCountry.Items.Clear();
            ddlAgencyCountry.Text = string.Empty;
            ddlAgencyCountry.DataSource = global.GetCountry();
            ddlAgencyCountry.DataTextField = "Name";
            ddlAgencyCountry.DataValueField = "Value";
            ddlAgencyCountry.Filter = (RadComboBoxFilter)Convert.ToInt32(1);
            ddlAgencyCountry.DataBind();

            ddlAgencyCountry1.Items.Clear();
            ddlAgencyCountry1.Text = string.Empty;
            ddlAgencyCountry1.DataSource = global.GetCountry();
            ddlAgencyCountry1.DataTextField = "Name";
            ddlAgencyCountry1.DataValueField = "Value";
            ddlAgencyCountry1.Filter = (RadComboBoxFilter)Convert.ToInt32(1);
            ddlAgencyCountry1.DataBind();

            ddlPAgency.Items.Clear();
            ddlPAgency.Text = string.Empty;
            ddlPAgency.DataSource = agcname.GetAgencyPName();
            ddlPAgency.DataTextField = "Name";
            ddlPAgency.DataValueField = "Value";
            ddlPAgency.Filter = (RadComboBoxFilter)Convert.ToInt32(1);
            ddlPAgency.DataBind();
            ddlPAgency.Items.Insert(0, new RadComboBoxItem("N/A", null));

            RadComboBoxAgencyName.Items.Clear();
            RadComboBoxAgencyName.Text = string.Empty;
            RadComboBoxAgencyName.DataSource = agcname.GetAgencyName();
            RadComboBoxAgencyName.DataTextField = "Name";
            RadComboBoxAgencyName.DataValueField = "Value";
            RadComboBoxAgencyName.Filter = (RadComboBoxFilter)Convert.ToInt32(1);
            RadComboBoxAgencyName.DataBind();

            RadComboBoxAgencyName.Text = "";
            tbAgencyShortName.Text = "";
            tbAgencyGroupName.Text = "";
            tbAgencyPrintName.Text = "";
            tbAgencyType.Text = "";
            tbCommissionRateBasic.Text = "";
            tbCommissionRateSeason.Text = "";
            tbCreditLimit.Text = "";

            tbContractStart.SelectedDate = null;
            tbContractEnd.SelectedDate = null;

            tbAPPayTerm.Text = "";
            tbAPPayMethod.Text = "";
            tbAPBillingType.Text = "";
            tbAPPAymentPriority.Text = "";
            tbAPPaySchedule.Text = "";
            tbARCollectionTerm.Text = "";
            tbARType.Text = "";
            tbARCollection.Text = "";
            tbARCollectionSchedule.Text = "";
            tbARCollectionMethod.Text = "";
            tbAgencyNo.Text = "";
            tbCurrency.Text = "";
            tbComment.Text = "";
            RadButtonActive.Checked = true;

            agFname.Text = "";
            agLname.Text = "";
            agTitle.Text = "";
            agPhone.Text = "";
            agMobile.Text = "";
            agFax.Text = "";
            agPEmail.Text = "";
            agSEmail.Text = "";
            agWebsite.Text = "";
            agAddress.Text = "";
            agCity.Text = "";
            agProvince.Text = "";
            agPostal.Text = "";

            agFname1.Text = "";
            agLname1.Text = "";
            agTitle1.Text = "";
            agPhone1.Text = "";
            agMobile1.Text = "";
            agFax1.Text = "";
            agPEmail1.Text = "";
            agSEmail1.Text = "";
            agWebsite1.Text = "";
            agAddress1.Text = "";
            agCity1.Text = "";
            agProvince1.Text = "";
            agPostal1.Text = "";

            if (RadToolBar1.FindItemByText("New") != null) RadToolBar1.FindItemByText("New").Enabled = false;
            if (RadToolBar1.FindItemByText("Update") != null) RadToolBar1.FindItemByText("Update").Text = @"Save";

            GetSiteLocation();
        }

        public override void SetVisibleModifyControllers()
        {
            if (UserPermissionModel.IsModify == false)
            {
                // toolbar
                foreach (RadToolBarItem toolbarItem in RadToolBar1.Items)
                {
                    if (toolbarItem.Text == "Student Invoice" || toolbarItem.Text == "Agency Invoice") continue;

                    toolbarItem.Enabled = false;
                }
            }
        }

        protected void RadGridAgency_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        protected void RadGridAgency_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["AgencyId"] = RadGridAgency.SelectedValue;

            GetSiteLocation();
            GetAgencyInfo();
        }

        protected void ButtonGridRefresh_OnClick(object sender, EventArgs e)
        {
            RadGridAgency.Rebind();
        }

        protected void RadToolBarAgency_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Text)
            {
                case "Request":
                    if (RadGridAgency.SelectedValue != null)
                    {
                        var cApprovalHistory = new CApprovalHistory();
                        cApprovalHistory.DelApprovalHistory((int)CConstValue.Approval.Agency, Convert.ToInt32(RadGridAgency.SelectedValue));

                        // approve request 
                        var approval = new CApproval().ApproveRequstCreate((int)CConstValue.Approval.Agency, CurrentUserId, Convert.ToInt32(RadGridAgency.SelectedValue));
                        if (approval > 0)
                        {
                            var cAgency = new CAgency();
                            var agency = cAgency.Get(Convert.ToInt32(RadGridAgency.SelectedValue));
                            agency.ApprovalStatus = approval;
                            agency.ApprovalId = CurrentUserId;
                            agency.ApprovalDate = DateTime.Now;
                            //agency.ApprovalMemo = "";
                            cAgency.Update(agency);

                            new CMail().SendMail(CConstValue.Approval.Agency, CConstValue.MailStatus.ToApproveUser, agency.AgencyId, agency.AgencyNumber, CurrentUserId);
                        }

                        RadGridAgency.Rebind();
                    }
                    break;
                case "Approve":
                    if (RadGridAgency.SelectedValue != null)
                        RunClientScript("ShowApprovalWindow('" + RadGridAgency.SelectedValue + "');");
                    break;
                case "Reject":
                    if (RadGridAgency.SelectedValue != null)
                        RunClientScript("ShowApprovalRejectWindow('" + RadGridAgency.SelectedValue + "');");
                    break;
                case "Revise":
                    if (RadGridAgency.SelectedValue != null)
                        RunClientScript("ShowApprovalReviseWindow('" + RadGridAgency.SelectedValue + "');");
                    break;
                case "Cancel":
                    if (RadGridAgency.SelectedValue != null)
                        RunClientScript("ShowApprovalCancelWindow('" + RadGridAgency.SelectedValue + "');");
                    break;
            }
        }

        protected void RadGridAgency_OnPreRender(object sender, EventArgs e)
        {
            if (ViewState["AgencyId"] != null)
            {
                foreach (GridDataItem item in RadGridAgency.Items)
                {
                    if (item.GetDataKeyValue("AgencyId").ToString() == ViewState["AgencyId"].ToString())
                    {
                        if (item.Selected == false)
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
            }

            SetApprovalList();

            // toolbar
            foreach (RadToolBarItem toolbarItem in RadToolBarAgency.Items)
            {
                if (RadGridAgency.SelectedValue != null)
                {
                    var cAgency = new CAgency();
                    var agency = cAgency.Get(Convert.ToInt32(RadGridAgency.SelectedValue));
                    // request active check
                    if (toolbarItem.Text == "Request")
                    {
                        if ((agency.ApprovalStatus == null ||
                            agency.ApprovalStatus == (int)CConstValue.ApprovalStatus.Revise) && agency.CreatedId == CurrentUserId)
                            toolbarItem.Enabled = true;
                        else
                            toolbarItem.Enabled = false;
                    }
                    else if (toolbarItem.Text == "Cancel")
                    {
                        if ((agency.ApprovalStatus == null ||
                            agency.ApprovalStatus == (int)CConstValue.ApprovalStatus.Revise ||
                            agency.ApprovalStatus == (int)CConstValue.ApprovalStatus.Requested) && agency.CreatedId == CurrentUserId)
                            toolbarItem.Enabled = true;
                        else
                            toolbarItem.Enabled = false;
                    }
                    else if (toolbarItem.Text == "Approve" || toolbarItem.Text == "Reject")
                    {
                        // approve active check
                        var refundApproveInfo = new CGlobal();
                        var supervisor = refundApproveInfo.CheckApprovalEnable((int)CConstValue.Approval.Agency,
                            Convert.ToInt32(agency.AgencyId));

                        // supervisor or loy employees
                        if ((CurrentUserId == supervisor)
                            && agency.ApprovalStatus != (int)CConstValue.ApprovalStatus.Approved
                            && agency.ApprovalStatus != (int)CConstValue.ApprovalStatus.Rejected
                            && agency.ApprovalStatus != (int)CConstValue.ApprovalStatus.Canceled
                            && agency.ApprovalStatus != null
                            && CurrentUserId != agency.ApprovalId)
                            toolbarItem.Enabled = true;
                        else
                            toolbarItem.Enabled = false;
                    }
                }
                else
                {
                    if (toolbarItem.Text == "View") continue;
                    toolbarItem.Enabled = false;
                }
            }
        }

        private void SetApprovalList()
        {
            LinqDataSource LinqDataSourceApprovalList = ApprovalLine1.GetSqlDataSourceApprovalList();
            if (LinqDataSourceApprovalList != null)
            {
                LinqDataSourceApprovalList.WhereParameters.Clear();
                if (RadGridAgency.SelectedValue != null)
                    LinqDataSourceApprovalList.WhereParameters.Add("MenuSeqId", DbType.Int32, RadGridAgency.SelectedValue.ToString());
                else
                    LinqDataSourceApprovalList.WhereParameters.Add("MenuSeqId", DbType.Int32, "0");
                LinqDataSourceApprovalList.WhereParameters.Add("ApproveType", DbType.Int32, ((int)CConstValue.Approval.Agency).ToString());
                LinqDataSourceApprovalList.Where = "ApproveType == @ApproveType and MenuSeqId == @MenuSeqId";
            }
        }

        protected void RadDropDownListView_OnSelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            ViewState["SelectedTextView"] = e.Text;
            SearchAgency();
        }

        private void SearchAgency()
        {
            LinqDataSource1.WhereParameters.Clear();
            foreach (var model in UserPermissionModel.SearchSiteList)
                LinqDataSource1.WhereParameters.Add(model.SiteIdName, DbType.Int32, model.SiteId.ToString());
            LinqDataSource1.Where = UserPermissionModel.SearchWhereSiteSb.ToString();
            SetViewType(LinqDataSource1, RadGridAgency);
        }

        protected void RadComboBoxSiteLocation_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var sb = new StringBuilder();
            var collection = RadComboBoxSiteLocation.CheckedItems;

            if (collection.Count != 0)
            {
                sb.Append("<h4>Checked SiteLocation List</h4>");
                foreach (var item in collection)
                    sb.Append("<label>" + item.Text + "</label>");

                itemsClientSide.Text = sb.ToString();
            }
            else
            {
                itemsClientSide.Text = string.Empty;
            }
        }

        protected void RadComboBoxAgencyName_OnTextChanged(object sender, EventArgs e)
        {
            var cAgency = new CAgency();
            var agency = cAgency.Get(RadComboBoxAgencyName.Text);
            if (agency != null)
            {
                ShowMessage(RadComboBoxAgencyName.Text + " already has been registered. Please use another one.");
                RadComboBoxAgencyName.Text = string.Empty;
            }
            else
            {
                ShowMessage("It is possible to use.");
            }
        }
    }
}