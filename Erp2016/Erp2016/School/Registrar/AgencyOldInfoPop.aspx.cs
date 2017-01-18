using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.Registrar
{
    public partial class AgencyOldInfoPop : PageBase
    {
        public AgencyOldInfoPop() : base((int)CConstValue.Menu.Agency)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }

            //LinqDataSourceAgencyOldInfo.WhereParameters.Clear();
            //LinqDataSourceAgencyOldInfo.WhereParameters.Add("PurchaseOrderId", DbType.Int32, Id.ToString());
            //LinqDataSourceAgencyOldInfo.Where = "PurchaseOrderId == @PurchaseOrderId";
        }
        

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            if (e.Item.Text == "Close")
                RunClientScript("Close();");
        }

        protected void RadGridAgencyOldInfo_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }
        
    }
}