using System;
using System.Collections.Generic;
using System.Text;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.Registrar
{
    public partial class PromotionPop : PageBase
    {
        public int Id { get; set; }

        public PromotionPop() : base((int)CConstValue.Menu.Promotion)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Id = Convert.ToInt32(Request["Id"]);

            if (!IsPostBack)
            {
                var scriptManager = (RadScriptManager)Page.Master.FindControl("RadScriptManager1");
                //scriptManager.RegisterPostBackControl(RadButtonFileDownload);
                FileDownloadList1.InitFileDownloadList((int)CConstValue.Upload.HomestayAgency);

                var global = new CGlobal();
                //Nation drop down list
                RadComboBoxCountry.DataSource = global.GetCountry();
                RadComboBoxCountry.DataTextField = "Name";
                RadComboBoxCountry.DataValueField = "Value";
                RadComboBoxCountry.Filter = (RadComboBoxFilter)Convert.ToInt32(1);
                RadComboBoxCountry.Items.Insert(0, new RadComboBoxItem("Choose a Country"));
                RadComboBoxCountry.DataBind();

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

                    var cS = new CPromotion().Get(Id);

                    foreach (RadComboBoxItem item in RadComboBoxCountry.Items)
                    {
                        if (item.Value == cS.CountryId.ToString())
                        {
                            item.Selected = true;
                            break;
                        }
                    }

                    RadNumericTextBoxAmount.Value = (double)cS.Amount;
                    RadDatePickerStartDate.SelectedDate = cS.StartDate;
                    RadDatePickerEndDate.SelectedDate = cS.EndDate;

                    tbComment.Text = cS.Memo;

                    // UP LOAD
                    FileDownloadList1.GetFileDownload(Convert.ToInt32(Id));

                }

            }
        }

        private void GetSiteLocation(bool isModify)
        {
            RadComboBoxSiteLocation.Items.Clear();
            List<SiteLocation> siteLocationList = new List<SiteLocation>();

            if (isModify)
            {

                var cPromotionSiteLocation = new CPromotionSiteLocation();
                var promotionSiteLocation = cPromotionSiteLocation.GetPromotionSiteLocationList(Id);
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
                    var cPromo = new CPromotion();
                    var promo = new Erp2016.Lib.Promotion();

                    // new
                    if (Request["type"] == "0")
                    {
                        promo = new Erp2016.Lib.Promotion();
                        promo.CreatedId = CurrentUserId;
                        promo.CreatedDate = DateTime.Now;
                    }
                    // modify
                    else
                        promo = cPromo.Get(Id);

                    promo.CountryId = Convert.ToInt32(RadComboBoxCountry.SelectedValue);

                    promo.Amount = (decimal)RadNumericTextBoxAmount.Value;
                    promo.Memo = tbComment.Text;
                    promo.CreatedDate = DateTime.Now;
                    promo.CreatedId = CurrentUserId;
                    promo.StartDate = (DateTime)RadDatePickerStartDate.SelectedDate;
                    promo.EndDate = (DateTime)RadDatePickerEndDate.SelectedDate;
                    promo.IsActive = false;

                    int promotionId;

                    // new
                    if (Request["type"] == "0")
                    {
                        promo.IsActive = false;
                        promotionId = cPromo.Add(promo);
                    }
                    // modify
                    else
                    {
                        promo.UpdatedId = CurrentUserId;
                        promo.UpdatedDate = DateTime.Now;
                        cPromo.Update(promo);

                        promotionId = promo.PromotionId;
                    }

                    var cPromotionSiteLocation = new CPromotionSiteLocation();
                    cPromotionSiteLocation.DelPromotionSiteLocationList(promotionId);

                    foreach (var siteLocation in RadComboBoxSiteLocation.CheckedItems)
                    {
                        var promotionSiteLocation = new PromotionSiteLocation()
                        {
                            CreatedId = CurrentUserId,
                            PromotionId = promotionId,
                            SiteLocationId = Convert.ToInt32(siteLocation.Value),
                            CreatedDate = DateTime.Now
                        };
                        cPromotionSiteLocation.Add(promotionSiteLocation);
                    }

                    FileDownloadList1.SaveFile(promotionId);

                    if (e.Item.Text == "TempSave")
                    {
                        RunClientScript("Close();");
                    }
                    else
                    {
                        var cApprovalHistory = new CApprovalHistory();
                        cApprovalHistory.DelApprovalHistory((int)CConstValue.Approval.Promotion, promotionId);

                        var approval = new CApproval().ApproveRequstCreate((int)CConstValue.Approval.Promotion, CurrentUserId, promotionId);
                        if (approval > 0)
                        {
                            var cPromotion = new CPromotion();
                            var promotion = cPromotion.Get(promotionId);
                            promotion.ApprovalStatus = approval;
                            promotion.ApprovalId = CurrentUserId;
                            promotion.ApprovalDate = DateTime.Now;
                            cPromotion.Update(promotion);

                            new CMail().SendMail(CConstValue.Approval.Promotion, CConstValue.MailStatus.ToApproveUser, promotion.PromotionId, promotion.PromotionMasterNo, CurrentUserId);

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