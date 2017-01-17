using System;
using System.Data;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.OfficeAdmin
{
    public partial class Inventory : PageBase
    {
        public Inventory() : base((int)CConstValue.Menu.Inventory)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            // get completed or rejected list
            //LinqDataSourceInventory.WhereParameters.Clear();
            //LinqDataSourceInventory.WhereParameters.Add("Id", DbType.Int32, CurrentUserId.ToString());
            //LinqDataSourceInventory.Where = "CreatedId == @Id";
            
            //}
        }
        
        protected void RadGrid_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            RunClientScript("ShowNewPop('" + RadGridInventory.SelectedValues["InventoryId"] + "', '1');");
        }

        protected void ToolbarButtonClicked(object sender, RadToolBarEventArgs e)
        {
            if (e.Item.Text == "New Inventory")
            {
                RunClientScript("ShowNewPop('0', '0');");
            }
        }

        protected void ButtonGridRefresh_OnClick(object sender, EventArgs e)
        {
            // refresh
            RadGridInventory.Rebind();
        }
        
        protected void RadGridInventory_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
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