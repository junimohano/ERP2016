using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.Registrar
{
    public partial class Promotion : PageBase
    {
        public Promotion() : base((int)CConstValue.Menu.Promotion)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var scriptManager = (RadScriptManager)Page.Master.FindControl("RadScriptManager1");
            //scriptManager.RegisterPostBackControl(RadButtonFileDownload);

            if (!IsPostBack)
            {
                FileDownloadList1.InitFileDownloadList((int)CConstValue.Upload.Promotion);
                FileDownloadList1.SetVisibieUploadControls(false);
            }

            SearchPromotion();
        }

        private void GetSiteLocation()
        {
            RadComboBoxSiteLocation.Items.Clear();
            List<SiteLocation> siteLocationList = new List<SiteLocation>();
            if (RadGrid1.SelectedValue != null)
            {
                var cPromotionSiteLocation = new CPromotionSiteLocation();
                var promotionSiteLocation = cPromotionSiteLocation.GetPromotionSiteLocationList(Convert.ToInt32(RadGrid1.SelectedValue));
                if (promotionSiteLocation.Count > 0)
                {
                    var siteLocation = new CSiteLocation().Get(promotionSiteLocation[0].SiteLocationId);
                    siteLocationList = new CSiteLocation().GetSiteLocationBySiteId(siteLocation.SiteId);

                    RadTextBoxSite.Text = (new CSite()).Get(siteLocation.SiteId).Abbreviation;
                }

                foreach (var siteLocation in siteLocationList)
                {
                    RadComboBoxSiteLocation.Items.Add(new RadComboBoxItem(siteLocation.Name, siteLocation.SiteLocationId.ToString()));
                }

                foreach (var promotionSiteLo in promotionSiteLocation)
                {
                    foreach (RadComboBoxItem siteLocation in RadComboBoxSiteLocation.Items)
                    {
                        if (promotionSiteLo.SiteLocationId == Convert.ToInt32(siteLocation.Value))
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

        protected void RadToolBar1_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Text)
            {
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
                case "New Promotion":
                    RunClientScript("ShowPop('-1', '0');");
                    break;
                case "Request":
                    if (RadGrid1.SelectedValue != null)
                        RunClientScript("ShowPop('" + RadGrid1.SelectedValue + "', '1');");
                    break;
            }
        }

        protected void GetStudent()
        {
            if (RadGrid1.SelectedValue != null && RadGrid1.SelectedValue.ToString() != "")
            {
                var cStud = new CPromotion();
                var stud = cStud.Get(Convert.ToInt32(RadGrid1.SelectedValue));

                if (stud.PromotionId > 0)
                {
                    tbMaster.Text = stud.PromotionMasterNo;
                    RadTextBoxCountry.Text = new CCountry().Get(stud.CountryId).Name;

                    RadNumericTextBoxAmount.Text = stud.Amount.ToString();
                    tbComment.Text = stud.Memo;
                }
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
                LinqDataSourceApprovalList.WhereParameters.Add("ApproveType", DbType.Int32, ((int)CConstValue.Approval.Promotion).ToString());
                LinqDataSourceApprovalList.Where = "ApproveType == @ApproveType and MenuSeqId == @MenuSeqId";
            }
        }

        protected void ButtonGridRefresh_OnClick(object sender, EventArgs e)
        {
            RadGrid1.Rebind();
        }

        protected void RadGrid1_OnFilterCheckListItemsRequested(object sender,
            GridFilterCheckListItemsRequestedEventArgs e)
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
            SearchPromotion();
        }

        private void SearchPromotion()
        {
            LinqDataSource1.WhereParameters.Clear();
            foreach (var model in UserPermissionModel.SearchSiteList)
                LinqDataSource1.WhereParameters.Add(model.SiteIdName, DbType.Int32, model.SiteId.ToString());
            LinqDataSource1.Where = UserPermissionModel.SearchWhereSiteSb.ToString();
            SetViewType(LinqDataSource1, RadGrid1);
        }

        protected void RadGrid1_PreRender(object sender, EventArgs e)
        {
            if (ViewState["PromotionId"] != null)
            {
                foreach (GridDataItem item in RadGrid1.Items)
                {
                    if (item.GetDataKeyValue("PromotionId").ToString() == ViewState["PromotionId"].ToString())
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
            GetStudent();
            GetPromotionHistory();

            FileDownloadList1.GetFileDownload(Convert.ToInt32(RadGrid1.SelectedValue));

            foreach (RadToolBarItem toolbarItem in RadToolBar1.Items)
            {
                if (RadGrid1.SelectedValue != null)
                {
                    var cPromotion = new CPromotion();
                    var promotion = cPromotion.Get(Convert.ToInt32(RadGrid1.SelectedValue));

                    // request active check
                    if (toolbarItem.Text == "Request")
                    {
                        if ((promotion.ApprovalStatus == null || promotion.ApprovalStatus == (int)CConstValue.ApprovalStatus.Revise) && promotion.CreatedId == CurrentUserId)
                            toolbarItem.Enabled = true;
                        else
                            toolbarItem.Enabled = false;
                    }
                    else if (toolbarItem.Text == "Cancel")
                    {
                        if ((promotion.ApprovalStatus == null ||
                            promotion.ApprovalStatus == (int)CConstValue.ApprovalStatus.Revise ||
                            promotion.ApprovalStatus == (int)CConstValue.ApprovalStatus.Requested) && promotion.CreatedId == CurrentUserId)
                            toolbarItem.Enabled = true;
                        else
                            toolbarItem.Enabled = false;
                    }
                    else if (toolbarItem.Text == "Approve" || toolbarItem.Text == "Reject" || toolbarItem.Text == "Revise")
                    {
                        // approve active check
                        var refundApproveInfo = new CGlobal();
                        var supervisor = refundApproveInfo.CheckApprovalEnable((int)CConstValue.Approval.Promotion, Convert.ToInt32(promotion.PromotionId));

                        // supervisor
                        if ((CurrentUserId == supervisor)
                            && promotion.ApprovalStatus != (int)CConstValue.ApprovalStatus.Approved
                            && promotion.ApprovalStatus != (int)CConstValue.ApprovalStatus.Rejected
                            && promotion.ApprovalStatus != (int)CConstValue.ApprovalStatus.Canceled
                            && promotion.ApprovalStatus != null
                            && CurrentUserId != promotion.ApprovalId)
                            toolbarItem.Enabled = true;
                        else
                            toolbarItem.Enabled = false;
                    }
                }
                else
                {
                    if (toolbarItem.Text == "View" || toolbarItem.Text == "New Promotion") continue;
                    toolbarItem.Enabled = false;
                }
            }

        }

        private void GetPromotionHistory()
        {
            LinqDataSourcePromotionHistory.WhereParameters.Clear();
            if (RadGrid1.SelectedValue != null)
                LinqDataSourcePromotionHistory.WhereParameters.Add("PromotionId", DbType.Int32, RadGrid1.SelectedValue.ToString());
            else
                LinqDataSourcePromotionHistory.WhereParameters.Add("PromotionId", DbType.Int32, "0");
            LinqDataSourcePromotionHistory.Where = "PromotionId == @PromotionId";
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
            ViewState["PromotionId"] = RadGrid1.SelectedValue;
        }

        protected void RadGridPromotionHistory_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }
    }
}