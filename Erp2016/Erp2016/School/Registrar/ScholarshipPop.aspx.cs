using System;
using System.Collections.Generic;
using System.Text;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.Registrar
{
    public partial class ScholarshipPop : PageBase
    {
        public int Id { get; set; }

        public ScholarshipPop() : base((int)CConstValue.Menu.Scholarship)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Id = Convert.ToInt32(Request["Id"]);

            if (!IsPostBack)
            {
                var cAgency = new CAgency();
                ddlAgencyName.DataSource = cAgency.GetAgency(CurrentSiteLocationId);
                ddlAgencyName.DataTextField = "Name";
                ddlAgencyName.DataValueField = "Value";
                ddlAgencyName.Filter = (RadComboBoxFilter)Convert.ToInt32(1);
                ddlAgencyName.Items.Insert(0, new RadComboBoxItem("Type a Agency Name"));
                ddlAgencyName.DataBind();

                //LoadSite();

                // new
                if (Request["type"] == "0")
                {
                    GetSiteLocation(false);
                }
                // modify
                else
                {
                    GetSiteLocation(true);

                    var cP = new CScholarship().Get(Id);

                    foreach (RadComboBoxItem item in ddlAgencyName.Items)
                    {
                        if (item.Value == cP.AgencyId.ToString())
                        {
                            item.Selected = true;
                            break;
                        }
                    }

                    if (cP.Amount == 0)
                    {
                        rbWeeks.Checked = true;
                        tbWeeks.Text = cP.Weeks.ToString();
                    }
                    else
                    {
                        rbAmount.Checked = true;
                        RadNumericTextBoxAmount.Value = (double)cP.Amount;
                    }
                    tbExpireDate.SelectedDate = cP.ExpireDate;

                    foreach (RadComboBoxItem item in RadComboBoxMinimum.Items)
                    {
                        if (item.Value == cP.MininumRegistrationWeeks.ToString())
                        {
                            item.Selected = true;
                            break;
                        }
                    }

                    tbComment.Text = cP.Memo;
                }
            }
        }

        private void GetSiteLocation(bool isModify)
        {
            RadComboBoxSiteLocation.Items.Clear();
            List<SiteLocation> siteLocationList = new List<SiteLocation>();

            if (isModify)
            {

                var cScholarshipSiteLocation = new CScholarshipSiteLocation();
                var scholarshipSiteLocation = cScholarshipSiteLocation.GetScholarshipSiteLocationList(Id);
                if (scholarshipSiteLocation.Count > 0)
                {
                    var siteLocation = new CSiteLocation().Get(scholarshipSiteLocation[0].SiteLocationId);
                    siteLocationList = new CSiteLocation().GetSiteLocationBySiteId(siteLocation.SiteId);

                    RadTextBoxSite.Text = (new CSite()).Get(siteLocation.SiteId).Abbreviation;
                }

                foreach (var siteLocation in siteLocationList)
                {
                    RadComboBoxSiteLocation.Items.Add(new RadComboBoxItem(siteLocation.Name, siteLocation.SiteLocationId.ToString()));
                }

                foreach (var scholarshipSiteLo in scholarshipSiteLocation)
                {
                    foreach (RadComboBoxItem siteLocation in RadComboBoxSiteLocation.Items)
                    {
                        if (scholarshipSiteLo.SiteLocationId == Convert.ToInt32(siteLocation.Value))
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

        protected void LoadSiteLocation(string siteId)
        {
            var global = new CGlobal();

            RadComboBoxSiteLocation.DataSource = global.GetSiteLocationBySiteId(Convert.ToInt32(siteId));
            RadComboBoxSiteLocation.DataTextField = "Name";
            RadComboBoxSiteLocation.DataValueField = "Value";
            RadComboBoxSiteLocation.DataBind();
        }

        //protected void LoadSite()
        //{
        //    var global = new CGlobal();

        //    RadComboBoxSite.DataSource = global.GetSiteId();
        //    RadComboBoxSite.DataTextField = "Name";
        //    RadComboBoxSite.DataValueField = "Value";
        //    RadComboBoxSite.DataBind();
        //    RadComboBoxSite.Items.Insert(0, new RadComboBoxItem("- Select Site - "));
        //}

        protected void StudentButtonClicked(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Text)
            {
                case "TempSave":
                case "Request":
                    var cScholarshipReg = new CScholarship();
                    var scholarshipReg = new Erp2016.Lib.Scholarship();
                    // new
                    if (Request["type"] == "0")
                    {
                        scholarshipReg = new Erp2016.Lib.Scholarship();
                        scholarshipReg.CreatedId = CurrentUserId;
                        scholarshipReg.CreatedDate = DateTime.Now;
                    }
                    // modify
                    else
                        scholarshipReg = cScholarshipReg.Get(Id);

                    scholarshipReg.AgencyId = Convert.ToInt32(ddlAgencyName.SelectedValue);
                    if (rbAmount.Checked)
                        scholarshipReg.Amount = Convert.ToDecimal(RadNumericTextBoxAmount.Text == string.Empty ? "0" : RadNumericTextBoxAmount.Text);
                    if (rbWeeks.Checked)
                        scholarshipReg.Weeks = Convert.ToInt32(tbWeeks.Text);

                    scholarshipReg.ExpireDate = Convert.ToDateTime(tbExpireDate.SelectedDate);
                    scholarshipReg.MininumRegistrationWeeks = Convert.ToInt32(RadComboBoxMinimum.SelectedValue);
                    scholarshipReg.Memo = tbComment.Text;
                    scholarshipReg.CreatedId = CurrentUserId;
                    scholarshipReg.IsActive = false;

                    int scholarshipId;

                    // new
                    if (Request["type"] == "0")
                    {
                        scholarshipReg.IsActive = false;
                        scholarshipId = cScholarshipReg.Add(scholarshipReg);
                    }
                    // modify
                    else
                    {
                        scholarshipReg.UpdatedId = CurrentUserId;
                        scholarshipReg.UpdatedDate = DateTime.Now;
                        cScholarshipReg.Update(scholarshipReg);

                        scholarshipId = scholarshipReg.ScholarshipId;
                    }

                    var cScholarshipSiteLocation = new CScholarshipSiteLocation();
                    cScholarshipSiteLocation.DelScholarshipSiteLocation(scholarshipId);

                    foreach (var siteLocation in RadComboBoxSiteLocation.CheckedItems)
                    {
                        var scholarshipSiteLocation = new ScholarshipSiteLocation()
                        {
                            CreatedId = CurrentUserId,
                            ScholarshipId = scholarshipId,
                            SiteLocationId = Convert.ToInt32(siteLocation.Value),
                            CreatedDate = DateTime.Now
                        };
                        cScholarshipSiteLocation.Add(scholarshipSiteLocation);
                    }

                    if (e.Item.Text == "TempSave")
                    {
                        RunClientScript("Close();");
                    }
                    else
                    {
                        var cApprovalHistory = new CApprovalHistory();
                        cApprovalHistory.DelApprovalHistory((int)CConstValue.Approval.Scholarship, scholarshipId);

                        var approval = new CApproval().ApproveRequstCreate((int)CConstValue.Approval.Scholarship, CurrentUserId, scholarshipId);
                        if (approval > 0)
                        {
                            var cScholar = new CScholarship();
                            var scholar = cScholar.Get(scholarshipId);
                            scholar.ApprovalStatus = approval;
                            scholar.ApprovalId = CurrentUserId;
                            scholar.ApprovalDate = DateTime.Now;
                            cScholar.Update(scholar);

                            new CMail().SendMail(CConstValue.Approval.Scholarship, CConstValue.MailStatus.ToApproveUser, scholar.ScholarshipId, scholar.ScholarshipMasterNo, CurrentUserId);

                            RunClientScript("Close();");
                        }
                        else
                            ShowMessage("error requesting");
                    }
                    break;

                case "Close":
                    RunClientScript("Close();");
                    break;

            }
        }

        protected void RadComboBoxSite_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadSiteLocation(e.Value);
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
    }
}