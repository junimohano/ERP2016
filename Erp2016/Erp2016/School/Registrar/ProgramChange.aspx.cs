using System;
using System.Data;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.Registrar
{
    public partial class ProgramChange : PageBase
    {
        public ProgramChange() : base((int)CConstValue.Menu.ProgramChange)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LinqDataSource1.WhereParameters.Clear();
            foreach (var model in UserPermissionModel.SearchSiteLocationList)
                LinqDataSource1.WhereParameters.Add(model.SiteLocationIdName, DbType.Int32, model.SiteLocationId.ToString());
            LinqDataSource1.Where = UserPermissionModel.SearchWhereSiteLocationSb.ToString();
        }

        public override void SetVisibleModifyControllers()
        {
            if (UserPermissionModel.IsModify == false)
            {

            }
        }

        protected void RadGrid1_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void RadGrid1_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }
    }
}