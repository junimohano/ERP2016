using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace App_Data
{
    public partial class DepositPaymentGrid : UserControl
    {
        public RadGrid GetRadGridDepositPayment()
        {
            return RadGridDepositPayment;
        }

        public LinqDataSource GetLinqDataSourceDepositPayment()
        {
            return LinqDataSourceDepositPayment;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void RadGridDepositPayment_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataItem = (GridDataItem)e.Item;
                if ((dataItem["PaidAmount"].FindControl("lblPaidAmount") as Label).Text.Contains("-"))
                    (dataItem["PaidAmount"].FindControl("lblPaidAmount") as Label).Style["color"] = CConstValue.NagativeColorName;
            }
            else if (e.Item is GridFooterItem)
            {
                var footer = (GridFooterItem)e.Item;
            }
        }

        protected void RadGridDepositPayment_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            PageBase.SetFilterCheckListItems(e);
        }
    }
}