using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace App_Data
{
    public partial class PaymentHistoryGrid : UserControl
    {
        public RadGrid GetRadGridPaymentHistory()
        {
            return RadGridPaymentHistory;
        }

        public LinqDataSource GetLinqDataSourcePaymentHistory()
        {
            return LinqDataSourcePaymentHistory;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void RadGridPaymentHistory_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataItem = (GridDataItem)e.Item;
                if ((dataItem["PayAmount"].FindControl("lblPayAmount") as Label).Text.Contains("-"))
                    (dataItem["PayAmount"].FindControl("lblPayAmount") as Label).Style["color"] = CConstValue.NagativeColorName;
            }
            else if (e.Item is GridFooterItem)
            {
                var footer = (GridFooterItem)e.Item;
            }
        }
        
        protected void RadGridPaymentHistory_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            PageBase.SetFilterCheckListItems(e);
        }
    }
}