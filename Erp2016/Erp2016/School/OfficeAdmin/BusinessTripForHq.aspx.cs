using System;
using System.Collections;
using System.Data;
using System.Text;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.OfficeAdmin
{
    public partial class BusinessTripForHq : PageBase
    {
        public BusinessTripForHq() : base((int)CConstValue.Menu.BusinessTripForHq)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // register postback
            var scriptManager = (RadScriptManager)Page.Master.FindControl("RadScriptManager1");
            //scriptManager.RegisterPostBackControl(RadButtonExcel);

            SetLinqDataWhere();

            if (!IsPostBack)
            {
            }
        }

        private void SetLinqDataWhere()
        {
            LinqDataSourceHq.WhereParameters.Clear();
            LinqDataSourceHq.WhereParameters.Add("ApprovalStatus", DbType.Int32, ((int)CConstValue.ApprovalStatus.Rejected).ToString());
            LinqDataSourceHq.Where = "ApprovalStatus >= @ApprovalStatus";
        }

        protected void RadGridList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var gridType = 2;
            var approvalType = string.Empty;

            foreach (GridDataItem item in ((RadGrid)sender).SelectedItems)
            {
                approvalType = item.GetDataKeyValue("ApprovalStatus").ToString();
                break;
            }

            RunClientScript("ShowNewPop('" + ((RadGrid) sender).SelectedValue + "', '1', '" + gridType + "', '" + approvalType +"'); ");
        }
        
        protected void ButtonGridRefresh_OnClick(object sender, EventArgs e)
        {
            // refresh
            RadGridList.Rebind();
        }

        protected void RadButtonExcel_OnClick(object sender, EventArgs e)
        {
            RadGridList.ExportSettings.Excel.Format =
                (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), "Xlsx");
            RadGridList.ExportSettings.OpenInNewWindow = true;
            RadGridList.ExportSettings.ExportOnlyData = true;
            RadGridList.ExportSettings.IgnorePaging = true;

            RadGridList.MasterTableView.ExportToExcel();
        }

        protected void RadGridList_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }
    }
}