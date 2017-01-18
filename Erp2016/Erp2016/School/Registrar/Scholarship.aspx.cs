using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.Registrar
{
    public partial class Scholarship : PageBase
    {
        public Scholarship() : base((int)CConstValue.Menu.Scholarship)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //}
            SearchScholarship();
        }

        private void GetSiteLocation()
        {
            RadComboBoxSiteLocation.Items.Clear();
            List<SiteLocation> siteLocationList = new List<SiteLocation>();
            if (RadGrid1.SelectedValue != null)
            {
                var cScholarshipSiteLocation = new CScholarshipSiteLocation();
                var scholarshipSiteLocation = cScholarshipSiteLocation.GetScholarshipSiteLocationList(Convert.ToInt32(RadGrid1.SelectedValue));
                if (scholarshipSiteLocation.Count > 0)
                {
                    var siteLocation = new CSiteLocation().Get(scholarshipSiteLocation[0].SiteLocationId);
                    siteLocationList = new CSiteLocation().GetSiteLocationBySiteId(siteLocation.SiteId);

                    tbScholarSite.Text = (new CSite()).Get(siteLocation.SiteId).Abbreviation;
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
            }

            RadComboBoxSiteLocation_OnSelectedIndexChanged(null, null);
        }

        protected void StudentButtonClicked(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Text)
            {
                case "Request":
                    if (RadGrid1.SelectedValue != null)
                        RunClientScript("ShowPop('" + RadGrid1.SelectedValue + "', '1');");
                    break;
                case "Approve":
                    if (RadGrid1.SelectedValue != null)
                        RunClientScript("ShowApprovalWindow('" + RadGrid1.SelectedValue + "');");
                    break;
                case "Reject":
                    if (RadGrid1.SelectedValue != null)
                        RunClientScript("ShowApprovalRejectWindow('" + RadGrid1.SelectedValue + "');");
                    break;
                case "Revise":
                    if (RadGrid1.SelectedValue != null)
                        RunClientScript("ShowApprovalReviseWindow('" + RadGrid1.SelectedValue + "');");
                    break;
                case "Cancel":
                    if (RadGrid1.SelectedValue != null)
                        RunClientScript("ShowApprovalCancelWindow('" + RadGrid1.SelectedValue + "');");
                    break;
                case "New Scholarship":
                    RunClientScript("ShowPop('-1', '0');");
                    break;
            }
        }

        protected void GetStudent()
        {
            if (RadGrid1.SelectedValue != null)
            {
                var cStud = new CScholarship();
                var stud = cStud.Get(Convert.ToInt32(RadGrid1.SelectedValue));

                tbMaster.Text = stud.ScholarshipMasterNo;

                var cAgency = new CAgency();
                var agency = cAgency.Get(Convert.ToInt32(stud.AgencyId));
                tbAgency.Text = agency.Name;

                tbAmount.Text = stud.Amount.ToString();
                rbAmount.Checked = tbAmount.Text == string.Empty ? false : true;

                tbWeek.Text = stud.Weeks.ToString();
                rbWeeks.Checked = tbWeek.Text == string.Empty ? false : true;

                tbComment.Text = stud.Memo;
            }

            GetSiteLocation();
        }

        private void SetApprovalList()
        {
            LinqDataSource LinqDataSourceApprovalList = ApprovalLine1.GetSqlDataSourceApprovalList();
            if (LinqDataSourceApprovalList != null)
            {
                LinqDataSourceApprovalList.WhereParameters.Clear();
                if (RadGrid1.SelectedValue != null)
                    LinqDataSourceApprovalList.WhereParameters.Add("MenuSeqId", DbType.Int32, RadGrid1.SelectedValue.ToString());
                else
                    LinqDataSourceApprovalList.WhereParameters.Add("MenuSeqId", DbType.Int32, "0");
                LinqDataSourceApprovalList.WhereParameters.Add("ApproveType", DbType.Int32, ((int)CConstValue.Approval.Scholarship).ToString());
                LinqDataSourceApprovalList.Where = "ApproveType == @ApproveType and MenuSeqId == @MenuSeqId";
            }
        }

        protected void ButtonGridRefresh_OnClick(object sender, EventArgs e)
        {
            RadGrid1.Rebind();
        }

        protected void RadGrid1_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
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
            }
        }

        protected void RadDropDownListView_OnSelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            ViewState["SelectedTextView"] = e.Text;
            SearchScholarship();
        }

        private void SearchScholarship()
        {
            LinqDataSource1.WhereParameters.Clear();
            foreach (var model in UserPermissionModel.SearchSiteList)
                LinqDataSource1.WhereParameters.Add(model.SiteIdName, DbType.Int32, model.SiteId.ToString());
            LinqDataSource1.Where = UserPermissionModel.SearchWhereSiteSb.ToString();
            SetViewType(LinqDataSource1, RadGrid1);
        }

        protected void RadGrid1_OnPreRender(object sender, EventArgs e)
        {
            if (ViewState["ScholarshipId"] != null)
            {
                foreach (GridDataItem item in RadGrid1.Items)
                {
                    if (item.GetDataKeyValue("ScholarshipId").ToString() == ViewState["ScholarshipId"].ToString())
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
            GetScholarshipHistory();
            GetStudent();

            foreach (RadToolBarItem toolbarItem in RadToolBar1.Items)
            {
                if (RadGrid1.SelectedValue != null)
                {
                    var cScholar = new CScholarship();
                    var scholar = cScholar.Get(Convert.ToInt32(RadGrid1.SelectedValue));

                    // request active check
                    if (toolbarItem.Text == "Request")
                    {
                        if ((scholar.ApprovalStatus == null || scholar.ApprovalStatus == (int)CConstValue.ApprovalStatus.Revise) && scholar.CreatedId == CurrentUserId)
                            toolbarItem.Enabled = true;
                        else
                            toolbarItem.Enabled = false;
                    }
                    else if (toolbarItem.Text == "Cancel")
                    {
                        if ((scholar.ApprovalStatus == null ||
                            scholar.ApprovalStatus == (int)CConstValue.ApprovalStatus.Revise ||
                            scholar.ApprovalStatus == (int)CConstValue.ApprovalStatus.Requested) && scholar.CreatedId == CurrentUserId)
                            toolbarItem.Enabled = true;
                        else
                            toolbarItem.Enabled = false;
                    }
                    else if (toolbarItem.Text == "Approve" || toolbarItem.Text == "Reject" || toolbarItem.Text == "Revise")
                    {
                        // approve active check
                        var refundApproveInfo = new CGlobal();
                        var supervisor = refundApproveInfo.CheckApprovalEnable((int)CConstValue.Approval.Scholarship, Convert.ToInt32(scholar.ScholarshipId));

                        // supervisor
                        if ((CurrentUserId == supervisor)
                            && scholar.ApprovalStatus != (int)CConstValue.ApprovalStatus.Approved
                            && scholar.ApprovalStatus != (int)CConstValue.ApprovalStatus.Rejected
                            && scholar.ApprovalStatus != (int)CConstValue.ApprovalStatus.Canceled
                            && scholar.ApprovalStatus != null
                            && CurrentUserId != scholar.ApprovalId)
                            toolbarItem.Enabled = true;
                        else
                            toolbarItem.Enabled = false;
                    }
                }
                else
                {
                    if (toolbarItem.Text == "View" || toolbarItem.Text == "New Scholarship") continue;
                    toolbarItem.Enabled = false;
                }
            }
        }

        private void GetScholarshipHistory()
        {
            LinqDataSourceScholarshipHistory.WhereParameters.Clear();
            if (RadGrid1.SelectedValue != null)
                LinqDataSourceScholarshipHistory.WhereParameters.Add("ScholarshipId", DbType.Int32, RadGrid1.SelectedValue.ToString());
            else
                LinqDataSourceScholarshipHistory.WhereParameters.Add("ScholarshipId", DbType.Int32, "0");
            LinqDataSourceScholarshipHistory.Where = "ScholarshipId == @ScholarshipId";
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

        protected void RadGrid1_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["ScholarshipId"] = RadGrid1.SelectedValue;
        }

        protected void RadGridScholarshipHistory_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }
    }
}