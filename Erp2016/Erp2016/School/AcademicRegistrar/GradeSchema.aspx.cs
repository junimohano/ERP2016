using System;
using System.Data;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.AcademicRegistrar
{
    public partial class GradeSchema : PageBase
    {
        public GradeSchema() : base((int)CConstValue.Menu.GradeSchema)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }

            LinqDataSourceGradeName.WhereParameters.Clear();
            LinqDataSourceGradeName.WhereParameters.Add("SiteLocationId", DbType.Int32, CurrentSiteLocationId.ToString());
            LinqDataSourceGradeName.Where = "SiteLocationId == @SiteLocationId";

            RefreshGrade();
            RefreshGradeLetter();
        }

        protected void RadToolBar1_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            if (e.Item.Text == "Add Grade Name")
            {
                RunClientScript("ShowPop('-1', '0');");
            }
            else if (e.Item.Text == "Modify Grade Name")
            {
                if (RadGridGradeName.SelectedValue != null)
                    RunClientScript("ShowPop('" + RadGridGradeName.SelectedValue + "', '1');");
            }
        }

        protected void RadGridGrade_OnBatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {
            foreach (var command in e.Commands)
            {
                if (command.Type.ToString() != "Delete")
                {
                    command.NewValues["GradeSchemaId"] = RadGridGradeName.SelectedValue;
                    if (command.NewValues["GradeSchemaItemId"] == null)
                    {
                        command.NewValues["CreatedId"] = CurrentUserId.ToString();
                        command.NewValues["CreatedDate"] = DateTime.Now;
                    }
                    else
                    {
                        command.NewValues["UpdatedId"] = CurrentUserId.ToString();
                        command.NewValues["UpdatedDate"] = DateTime.Now;
                    }
                }
            }
        }

        protected void ButtonGridRefresh_OnClick(object sender, EventArgs e)
        {
            RadGridGradeName.Rebind();
        }

        protected void RadGridGrade_OnPreRender(object sender, EventArgs e)
        {
            RefreshGrade();
        }

        private void RefreshGrade()
        {
            LinqDataSourceGrade.WhereParameters.Clear();
            if (RadGridGradeName.SelectedValue != null)
                LinqDataSourceGrade.WhereParameters.Add("GradeSchemaId", DbType.Int32, RadGridGradeName.SelectedValue.ToString());
            else
                LinqDataSourceGrade.WhereParameters.Add("GradeSchemaId", DbType.Int32, 0.ToString());
            LinqDataSourceGrade.Where = "GradeSchemaId == @GradeSchemaId";
        }

        private void RefreshGradeLetter()
        {
            LinqDataSourceGradeLetter.WhereParameters.Clear();
            if (RadGridGradeName.SelectedValue != null)
                LinqDataSourceGradeLetter.WhereParameters.Add("GradeSchemaId", DbType.Int32, RadGridGradeName.SelectedValue.ToString());
            else
                LinqDataSourceGradeLetter.WhereParameters.Add("GradeSchemaId", DbType.Int32, 0.ToString());
            LinqDataSourceGradeLetter.Where = "GradeSchemaId == @GradeSchemaId";
        }

        protected void RadGridGradeName_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        protected void RadGridGradeLetter_OnBatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {
            foreach (var command in e.Commands)
            {
                if (command.Type.ToString() != "Delete")
                {
                    command.NewValues["GradeSchemaId"] = RadGridGradeName.SelectedValue;
                    if (command.NewValues["GradeSchemaLetterItemId"] == null)
                    {
                        command.NewValues["CreatedId"] = CurrentUserId.ToString();
                        command.NewValues["CreatedDate"] = DateTime.Now;
                    }
                    else
                    {
                        command.NewValues["UpdatedId"] = CurrentUserId.ToString();
                        command.NewValues["UpdatedDate"] = DateTime.Now;
                    }
                }
            }
        }

        protected void RadGridGradeLetter_OnPreRender(object sender, EventArgs e)
        {
            RefreshGradeLetter();
        }

        protected void RadGridGrade_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        protected void RadGridGradeLetter_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
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

                RadGridGrade.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                RadGridGrade.MasterTableView.EditMode = GridEditMode.InPlace;
                RadGridGrade.MasterTableView.GetColumn("DeleteColumn").Visible = false; ;

                RadGridGradeLetter.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                RadGridGradeLetter.MasterTableView.EditMode = GridEditMode.InPlace;
                RadGridGradeLetter.MasterTableView.GetColumn("DeleteColumn").Visible = false;
            }
        }
    }
}