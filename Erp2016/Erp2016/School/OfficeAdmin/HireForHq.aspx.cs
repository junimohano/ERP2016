using System;
using System.Data;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.OfficeAdmin
{
    public partial class HireForHq : PageBase
    {
        public HireForHq() : base((int)CConstValue.Menu.HireForHq)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //}

            LinqDataSourceHireHq.WhereParameters.Clear();
            LinqDataSourceHireHq.WhereParameters.Add("ApprovalStatus", DbType.Int32, ((int)CConstValue.ApprovalStatus.Rejected).ToString());
            LinqDataSourceHireHq.Where = "ApprovalStatus = @ApprovalStatus";
        }

        protected void RadGrid_OnSelectedIndexChanged(object sender, EventArgs e)
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
            RadGridApproval.Rebind();
        }

        protected void RadGridApproval_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }
    }
}