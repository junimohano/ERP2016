using System;
using System.Data;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.OfficeAdmin
{
    public partial class CorporateCreditCardSchema : PageBase
    {
        public CorporateCreditCardSchema() : base((int)CConstValue.Menu.CorporateCreditCardSchema)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }

            //LinqDataSourceCorporateCreditCardSchema.WhereParameters.Clear();
            //LinqDataSourceCorporateCreditCardSchema.WhereParameters.Add("SiteLocationId", DbType.Int32, CurrentSiteLocationId.ToString());
            //LinqDataSourceCorporateCreditCardSchema.Where = "SiteLocationId == @SiteLocationId";
        }

        protected void RadToolBar1_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            if (e.Item.Text == "Add Corporate Credit Card")
            {
                RunClientScript("ShowPop('-1', '0');");
            }
            else if (e.Item.Text == "Modify Corporate Credit Card")
            {
                if (RadGridCorporateCreditCardSchema.SelectedValue != null)
                    RunClientScript("ShowPop('" + RadGridCorporateCreditCardSchema.SelectedValue + "', '1');");
            }
        }
        
        protected void ButtonGridRefresh_OnClick(object sender, EventArgs e)
        {
            RadGridCorporateCreditCardSchema.Rebind();
        }
        
        protected void RadGridGradeName_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
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
    }
}