using System;
using System.Data;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.OfficeAdmin
{
    public partial class Expense : PageBase
    {
        public Expense() : base((int)CConstValue.Menu.Expense)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            // get completed or rejected list
            LinqDataSourceRequest.WhereParameters.Clear();
            LinqDataSourceRequest.WhereParameters.Add("Id", DbType.Int32, CurrentUserId.ToString());
            LinqDataSourceRequest.Where = "CreatedId == @Id";

            LinqDataSourceApproval.WhereParameters.Clear();
            LinqDataSourceApproval.WhereParameters.Add("ApprovalUser", DbType.Int32, CurrentUserId.ToString());
            LinqDataSourceApproval.WhereParameters.Add("IsApprovalRequest", DbType.Boolean, bool.TrueString);
            LinqDataSourceApproval.Where =
                "ApprovalUser == @ApprovalUser && IsApprovalRequest == @IsApprovalRequest && CreatedId != @ApprovalUser";
            //}
        }

        protected void ExportExcel(object sender, EventArgs e)
        {
        }

        protected void RadGrid_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var grid = ((RadGrid) sender);

            var gridType = 0;
            var approvalType = string.Empty;

            if (sender == RadGridRequest)
            {
                gridType = 0;
            }
            else
            {
                gridType = 1;
            }

            var approvalStatus = grid.SelectedValues["ApprovalStatus"];
            if (approvalStatus == null)
                approvalType = string.Empty;
            else
                approvalType = approvalStatus.ToString();

            RunClientScript("ShowNewPop('" + grid.SelectedValues["ExpenseId"] + "', '1', '" + gridType + "', '" + approvalType + "');");
        }

        protected void ToolbarButtonClicked(object sender, RadToolBarEventArgs e)
        {
            if (e.Item.Text == "New Expense")
            {
                RunClientScript("ShowNewPop('0', '0', '0', '0');");
            }
        }

        protected void ButtonGridRefresh_OnClick(object sender, EventArgs e)
        {
            // refresh
            RadGridRequest.Rebind();
            RadGridApproval.Rebind();
        }
        
        protected void RadGridRequest_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        protected void RadGridApproval_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
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