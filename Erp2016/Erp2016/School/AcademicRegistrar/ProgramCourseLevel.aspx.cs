using System;
using System.Data;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.AcademicRegistrar
{
    public partial class ProgramCourseLevel : PageBase
    {
        public ProgramCourseLevel() : base((int)CConstValue.Menu.ProgramCourseLevel)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ResetForm();
            }

            LinqDataSourceProgramCourseLevel.WhereParameters.Clear();
            foreach (var model in UserPermissionModel.SearchSiteList)
                LinqDataSourceProgramCourseLevel.WhereParameters.Add(model.SiteIdName, DbType.Int32, model.SiteId.ToString());
            LinqDataSourceProgramCourseLevel.Where = UserPermissionModel.SearchWhereSiteSb.ToString();

            RadComboBoxFaculty.OpenDropDownOnLoad = false;
            RadComboBoxProgramGroup.OpenDropDownOnLoad = false;
            RadComboBoxProgram.OpenDropDownOnLoad = false;
            RadComboBoxProgramCourse.OpenDropDownOnLoad = false;
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
                var cC = new CProgramCourseLevel();
                var c = new Erp2016.Lib.ProgramCourseLevel();
                c.ProgramCourseId = Convert.ToInt32(RadComboBoxProgramCourse.SelectedValue);
                c.IsActive = RadButtonActive.Checked;
                c.Level = RadTextBoxProgramCourseLevel.Text;
                c.Description = RadTextBoxDescription.Text;
                c.CreatedId = CurrentUserId;
                c.CreatedDate = DateTime.Now.Date;

                if (cC.Add(c) > 0)
                {
                    ShowMessage("'" + c.Level + "' is added.");
                    Grid.Rebind();
                }
                else
                    ShowMessage("Failed to add");
            }
            else if (e.Item.Text == "Update")
            {
                if (Grid.SelectedValue != null)
                {
                    var cC = new CProgramCourseLevel();
                    var c = cC.Get(Convert.ToInt32(Grid.SelectedValue));
                    c.ProgramCourseId = Convert.ToInt32(RadComboBoxProgramCourse.SelectedValue);
                    c.IsActive = true;
                    c.Level = RadTextBoxProgramCourseLevel.Text;
                    c.Description = RadTextBoxDescription.Text;
                    c.IsActive = RadButtonActive.Checked;
                    c.UpdatedId = CurrentUserId;
                    c.UpdatedDate = DateTime.Now;

                    if (cC.Update(c))
                    {
                        ShowMessage("'" + c.Level + "' is updated.");
                        Grid.Rebind();
                    }
                    else
                        ShowMessage("Failed to update");
                }
            }
        }

        protected void GetProgramCourseLevel()
        {
            ResetForm();
            if (Grid.SelectedValue != null)
            {
                var c = new CProgramCourseLevel().Get(Convert.ToInt32(Grid.SelectedValue));
                if (c != null)
                {
                    var programCourse = new CProgramCourse().Get(c.ProgramCourseId);
                    if (programCourse != null)
                    {
                        var program = new CProgram().Get(programCourse.ProgramId);

                        if (program != null)
                        {
                            if (program.ProgramGroupId != null)
                            {
                                var programGroup = new CProgramGroup().Get(Convert.ToInt32(program.ProgramGroupId));
                                if (programGroup != null)
                                {
                                    RadComboBoxFaculty.SelectedValue = programGroup.FacultyId.ToString();
                                    LoadProgramGroup(RadComboBoxFaculty.SelectedValue);
                                }
                                RadComboBoxProgramGroup.SelectedValue = program.ProgramGroupId.ToString();
                                LoadProgram(RadComboBoxProgramGroup.SelectedValue);
                            }
                            RadComboBoxProgram.SelectedValue = program.ProgramId.ToString();
                            LoadProgramCourse(RadComboBoxProgram.SelectedValue);
                        }
                    }
                    RadTextBoxProgramCourseLevel.Text = c.Level;
                    RadTextBoxDescription.Text = c.Description;
                    RadButtonActive.Checked = c.IsActive;
                }

                RadToolBarProgramGroup.FindItemByText("New").Enabled = true;
                if (RadToolBarProgramGroup.FindItemByText("Save") != null) RadToolBarProgramGroup.FindItemByText("Save").Text = "Update";
            }
        }

        protected void ResetForm()
        {
            LoadFaculty();
            LoadProgramGroup(null);
            LoadProgram(null);
            LoadProgramCourse(null);

            RadTextBoxProgramCourseLevel.Text = string.Empty;
            RadTextBoxDescription.Text = string.Empty;
            RadButtonActive.Checked = true;

            RadToolBarProgramGroup.FindItemByText("New").Enabled = false;
            if (RadToolBarProgramGroup.FindItemByText("Update") != null) RadToolBarProgramGroup.FindItemByText("Update").Text = "Save";
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

        protected void LoadProgramGroup(string facultyId)
        {
            RadComboBoxProgramGroup.Items.Clear();
            RadComboBoxProgramGroup.Text = string.Empty;
            if (!string.IsNullOrEmpty(facultyId))
                RadComboBoxProgramGroup.DataSource = new CProgramGroup().GetProgramGroupList(CurrentSiteId, Convert.ToInt32(facultyId));
            else
                RadComboBoxProgramGroup.DataSource = new CProgramGroup().GetProgramGroupList(CurrentSiteId);

            RadComboBoxProgramGroup.DataTextField = "Name";
            RadComboBoxProgramGroup.DataValueField = "Value";
            RadComboBoxProgramGroup.DataBind();
            RadComboBoxProgramGroup.Items.Insert(0, new RadComboBoxItem("N/A", null));
        }

        protected void LoadProgram(string programGroupId)
        {
            RadComboBoxProgram.Items.Clear();
            RadComboBoxProgram.Text = string.Empty;
            if (!string.IsNullOrEmpty(programGroupId))
                RadComboBoxProgram.DataSource = new CProgram().GetProgramList(CurrentSiteLocationId, Convert.ToInt32(programGroupId));
            else
                RadComboBoxProgram.DataSource = new CProgram().GetProgramList(CurrentSiteLocationId);

            RadComboBoxProgram.DataTextField = "Name";
            RadComboBoxProgram.DataValueField = "Value";
            RadComboBoxProgram.DataBind();
        }

        protected void LoadProgramCourse(string programId)
        {
            RadComboBoxProgramCourse.Items.Clear();
            RadComboBoxProgramCourse.Text = string.Empty;
            if (!string.IsNullOrEmpty(programId))
            {
                RadComboBoxProgramCourse.DataSource = new CProgramCourse().GetProgramCourseList(Convert.ToInt32(programId));
                RadComboBoxProgramCourse.DataTextField = "Name";
                RadComboBoxProgramCourse.DataValueField = "Value";
                RadComboBoxProgramCourse.DataBind();
            }
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

        protected void Grid_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GetProgramCourseLevel();
        }

        protected void RadComboBoxFaculty_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadProgramGroup(RadComboBoxFaculty.SelectedValue);

            RadComboBoxProgramGroup.OpenDropDownOnLoad = true;
        }

        protected void RadComboBoxProgramGroup_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadProgram(RadComboBoxProgramGroup.SelectedValue);
            RadComboBoxProgram.OpenDropDownOnLoad = true;
            LoadProgramCourse(RadComboBoxProgram.SelectedValue);
        }

        protected void RadComboBoxProgram_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadProgramCourse(RadComboBoxProgram.SelectedValue);
            RadComboBoxProgramCourse.OpenDropDownOnLoad = true;
        }

        protected void RadComboBoxProgramCourse_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadTextBoxProgramCourseLevel.Focus();
        }
    }
}