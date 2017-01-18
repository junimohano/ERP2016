using System;
using System.Data;
using System.Linq;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.Systems
{
    public partial class ApprovalChart : PageBase
    {
        public ApprovalChart() : base((int)CConstValue.Menu.ApprovalChart)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                foreach (int index in Enum.GetValues(typeof(CConstValue.Approval)))
                {
                    RadComboBoxChartList.Items.Add(new RadComboBoxItem(Enum.GetName(typeof(CConstValue.Approval), index), index.ToString()));
                }

                SetTreeviewChart(hfApproveType.Value);

                LinqDataSourceUser.WhereParameters.Clear();
                LinqDataSourceUser.WhereParameters.Add("Id", DbType.Int32, "0");
                LinqDataSourceUser.Where = "UserId == @Id";
            }
        }

        private void SetTreeviewChart(string approvalType, CApproval staff = null)
        {
            if (staff == null) staff = new CApproval();

            trSupervisorChart.DataTextField = "UserName";
            trSupervisorChart.DataValueField = "UserId";
            trSupervisorChart.DataFieldID = "UserId";
            trSupervisorChart.DataFieldParentID = "Supervisor";
            trSupervisorChart.ValidationGroup = "UserPositionId";
            trSupervisorChart.DataSource = staff.GetSupervisorChart(Convert.ToInt32(approvalType), CurrentSiteId);
            trSupervisorChart.DataBind();

            trSupervisorChart.ExpandAllNodes();
        }

        protected void HandleDrop(object sender, RadTreeNodeDragDropEventArgs e)
        {
            var sourceNode = e.SourceDragNode;
            var destinationNode = e.DestDragNode;
            //RadTreeViewDropPosition dropPosition = e.DropPosition;
            var dragNodes = e.DraggedNodes.ToList();

            // move treeview
            if (destinationNode != null)
            {
                var cDests = new CApproval();
                var dests = cDests.Get(Convert.ToInt32(destinationNode.Value), Convert.ToInt32(hfApproveType.Value));

                if (dragNodes.Any())
                {
                    foreach (var n in dragNodes)
                    {
                        var cDrags = new CApproval();
                        var drags = cDests.Get(Convert.ToInt32(n.Value), Convert.ToInt32(hfApproveType.Value));

                        var emp = drags.UserId;

                        switch (e.DropPosition)
                        {
                            case RadTreeViewDropPosition.Over:
                                drags.Supervisor = dests.UserId;
                                break;
                        }
                        cDrags.UpdateStaff(Convert.ToInt32(hfApproveType.Value), emp, drags.Supervisor);
                        SetTreeviewChart(hfApproveType.Value);
                        //}
                    }
                }
            }
            // move to grid
            else if (e.HtmlElementID == RadGridSupervisor.ClientID)
            {
                foreach (var n in dragNodes)
                {
                    var cDrags = new CApproval();
                    var drags = cDrags.Get(Convert.ToInt32(n.Value), Convert.ToInt32(hfApproveType.Value));
                    var emp = drags.UserId;

                    drags.Supervisor = null;

                    cDrags.UpdateStaff(Convert.ToInt32(hfApproveType.Value), emp, drags.Supervisor);
                    SetTreeviewChart(hfApproveType.Value);
                }
            }
        }

        protected void RadComboBoxChartList_SelectedIndexChanged(object sender,
            RadComboBoxSelectedIndexChangedEventArgs e)
        {
            hfApproveType.Value = e.Value;
            SetTreeviewChart(hfApproveType.Value);
        }

        protected void trSupervisorChart_OnNodeClick(object sender, RadTreeNodeEventArgs e)
        {
            LinqDataSourceUser.WhereParameters.Clear();
            LinqDataSourceUser.WhereParameters.Add("Id", DbType.Int32, e.Node.Value);
            LinqDataSourceUser.Where = "UserId == @Id";

            RadGridInfo.DataBind();
        }

        protected void RadGridSupervisor_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var dt = new DataTable();
            dt.Columns.Add("Supervisor");
            RadGridSupervisor.DataSource = dt;
        }

        public override void SetVisibleModifyControllers()
        {
            if (UserPermissionModel.IsModify == false)
            {
                trSupervisorChart.EnableDragAndDrop = false;
            }
        }
    }
}