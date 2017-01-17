using System;
using System.Data;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.OfficeAdmin
{
    public partial class VacationForHq : PageBase
    {
        public VacationForHq() : base((int)CConstValue.Menu.VacationForHq)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //}

            LinqDataSourceVacationHq.WhereParameters.Clear();
            LinqDataSourceVacationHq.WhereParameters.Add("ApprovalStatus", DbType.Int32, ((int)CConstValue.ApprovalStatus.Rejected).ToString());
            LinqDataSourceVacationHq.Where = "ApprovalStatus >= @ApprovalStatus";
        }

        protected void ButtonGridRefresh_OnClick(object sender, EventArgs e)
        {
            // refresh
            RadGrid1.Rebind();
        }

        protected void RadToolBar1_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            if (RadGrid1.SelectedValue == null) return;

            if (e.Item.Text == "View")
            {
                var gridType = 2;
                var approvalType = string.Empty;

                var approvalStatus = RadGrid1.SelectedValues["ApprovalStatus"];
                if (approvalStatus == null)
                    approvalType = string.Empty;
                else
                    approvalType = approvalStatus.ToString();

                RunClientScript("ShowNewPop('" + RadGrid1.SelectedValues["No"] + "', '1', '" + gridType + "', '" + approvalType + "');");
            }
            else if (e.Item.Text == "Detail View")
            {
                RunClientScript("ShowInfoPop('" + RadGrid1.SelectedValues["No"] + "');");
            }
        }

        protected void RadGrid1_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }
    }
}