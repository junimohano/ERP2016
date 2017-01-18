using System;
using System.Collections;
using System.Data;
using System.Text;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.OfficeAdmin
{
    public partial class PurchaseOrderForHq : PageBase
    {
        public PurchaseOrderForHq() : base((int)CConstValue.Menu.PurchaseOrderForHq)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // register postback
            var scriptManager = (RadScriptManager) Page.Master.FindControl("RadScriptManager1");
            //scriptManager.RegisterPostBackControl(RadButtonExcel);
            //scriptManager.RegisterPostBackControl(RadButtonDetailExcel);

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

        protected void RadGridList_Load(object sender, EventArgs e)
        {
        }

        protected void RadGridList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var grid = ((RadGrid) sender);

            var gridType = 2;
            var approvalType = string.Empty;

            var approvalStatus = grid.SelectedValues["ApprovalStatus"];
            if (approvalStatus == null)
                approvalType = string.Empty;
            else
                approvalType = approvalStatus.ToString();

            RunClientScript("ShowNewPop('" + grid.SelectedValues["No"] + "', '1', '" + gridType + "', '" + approvalType + "');");
        }
        
        protected void ButtonGridRefresh_OnClick(object sender, EventArgs e)
        {
            // refresh
            RadGridList.Rebind();
        }

        protected void RadButtonExcel_OnClick(object sender, EventArgs e)
        {
            RadGridList.ExportSettings.Excel.Format = (GridExcelExportFormat) Enum.Parse(typeof (GridExcelExportFormat), "Xlsx");
            RadGridList.ExportSettings.OpenInNewWindow = true;
            RadGridList.ExportSettings.ExportOnlyData = true;
            RadGridList.ExportSettings.IgnorePaging = true;

            RadGridList.MasterTableView.ExportToExcel();
        }

        protected void RadButtonDetailExcel_OnClick(object sender, EventArgs e)
        {
            //var expense = new CExpense();
            //var tempDt = expense.GetExpenseDetailList(REQUEST_STATUS,
            //    ((RadTextBox) radToolbar.Items[0].FindControl("RadTextBoxEmpNum")).Text,
            //    ((RadTextBox) radToolbar.Items[0].FindControl("RadTextBoxEmpName")).Text,
            //    ((RadComboBox) radToolbar.Items[0].FindControl("RadComboxCampus")).SelectedItem.Text,
            //    ((RadComboBox) radToolbar.Items[0].FindControl("RadComboBoxLocation")).SelectedItem.Text,
            //    ((RadComboBox) radToolbar.Items[0].FindControl("RadComboBoxResult")).SelectedItem.Text);

            //ExportExcel(tempDt, "ExpenseDetail");
        }

        protected void RadGridList_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }
    }
}