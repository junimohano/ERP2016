using System;
using System.Data;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.AcademicRegistrar
{
    public partial class ProgramGroup : PageBase
    {
        public ProgramGroup() : base((int)CConstValue.Menu.ProgramGroup)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ResetForm();
            }

            LinqDataSourceProgramGroup.WhereParameters.Clear();
            foreach (var model in UserPermissionModel.SearchSiteList)
                LinqDataSourceProgramGroup.WhereParameters.Add(model.SiteIdName, DbType.Int32, model.SiteId.ToString());
            LinqDataSourceProgramGroup.Where = UserPermissionModel.SearchWhereSiteSb.ToString();
        }

        protected void LoadFaculty()
        {
            RadComboBoxFaculty.Items.Clear();
            RadComboBoxFaculty.Text = string.Empty;
            RadComboBoxFaculty.DataSource = new CFaculty().GetFacultyList(CurrentSiteId);
            RadComboBoxFaculty.DataTextField = "Name";
            RadComboBoxFaculty.DataValueField = "Value";
            RadComboBoxFaculty.DataBind();
            RadComboBoxFaculty.Items.Insert(0, new RadComboBoxItem("N/A", null));
        }

        protected void ToolbarClick(object sender, RadToolBarEventArgs e)
        {
            if (e.Item.Text == "New")
            {
                Grid.SelectedIndexes.Clear();
                ResetForm();
            }
            else if (e.Item.Text == "Save")
            {
                var cProg = new CProgramGroup();
                var prog = new Erp2016.Lib.ProgramGroup();
                prog.SiteId = CurrentSiteId;
                if (!string.IsNullOrEmpty(RadComboBoxFaculty.SelectedValue))
                    prog.FacultyId = Convert.ToInt32(RadComboBoxFaculty.SelectedValue);
                prog.IsActive = RadButtonActive.Checked;
                prog.Name = tbProgram.Text;
                prog.Description = tbDescript.Text;
                prog.CreatedId = CurrentUserId;
                prog.CreatedDate = DateTime.Now.Date;

                if (cProg.Add(prog) > 0)
                {
                    ShowMessage("'" + prog.Name + "' is added.");
                    Grid.Rebind();
                }
                else
                    ShowMessage("Failed to add");
            }
            else if (e.Item.Text == "Update")
            {
                if (Grid.SelectedValue != null)
                {
                    var cProg = new CProgramGroup();
                    var prog = cProg.Get(Convert.ToInt32(Grid.SelectedValue));
                    if (RadComboBoxFaculty.SelectedValue != null)
                        prog.FacultyId = Convert.ToInt32(RadComboBoxFaculty.SelectedValue);
                    prog.Name = tbProgram.Text;
                    prog.IsActive = RadButtonActive.Checked;
                    prog.Description = tbDescript.Text;
                    prog.UpdatedId = CurrentUserId;
                    prog.UpdatedDate = DateTime.Now;

                    if (cProg.Update(prog))
                    {
                        ShowMessage("'" + prog.Name + "' is updated.");
                        Grid.Rebind();
                    }
                    else
                        ShowMessage("Failed to update");
                }
            }
        }

        protected void SelectProgram(object sender, EventArgs e)
        {
            GetProgram();
        }

        protected void GetProgram()
        {
            ResetForm();
            if (Grid.SelectedValue != null)
            {
                var cProg = new CProgramGroup();
                var prog = cProg.Get(Convert.ToInt32(Grid.SelectedValue));

                RadComboBoxFaculty.SelectedValue = prog.FacultyId.ToString();
                tbProgram.Text = prog.Name;
                tbDescript.Text = prog.Description;
                RadButtonActive.Checked = prog.IsActive;

                RadToolBarProgramGroup.FindItemByText("New").Enabled = true;
                if (RadToolBarProgramGroup.FindItemByText("Save") != null) RadToolBarProgramGroup.FindItemByText("Save").Text = "Update";
            }
        }

        protected void ResetForm()
        {
            LoadFaculty();

            tbProgram.Text = "";
            tbDescript.Text = "";
            RadButtonActive.Checked = true;

            RadToolBarProgramGroup.FindItemByText("New").Enabled = false;
            if (RadToolBarProgramGroup.FindItemByText("Update") != null) RadToolBarProgramGroup.FindItemByText("Update").Text = "Save";
        }

        protected void ddlFaculty_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            tbProgram.Focus();
        }

        protected void Grid_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        public override void SetVisibleModifyControllers()
        {
            if (UserPermissionModel.IsModify == false)
            {
                // toolbar
                foreach (RadToolBarItem toolbarItem in RadToolBarProgramGroup.Items)
                {
                    toolbarItem.Enabled = false;
                }
            }
        }
    }
}