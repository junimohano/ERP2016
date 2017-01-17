using System;
using System.Collections.Generic;
using System.Data;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.AcademicRegistrar
{
    public partial class ProgramClassStudentInformationMovementPop : PageBase
    {
        private readonly List<int> _classStudentIdList = new List<int>();
        private int CurrentProgramClassId { get; set; }

        public ProgramClassStudentInformationMovementPop() : base((int)CConstValue.Menu.ProgramClass)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var tempSplit = Request["programClassStudentIdList"].Split(',');
            foreach (var v in tempSplit)
            {
                _classStudentIdList.Add(Convert.ToInt32(v));
            }
            var programClassStudent = new CProgramClassStudent().Get(_classStudentIdList[0]);
            CurrentProgramClassId = programClassStudent.ProgramClassId;

            if (!IsPostBack)
            {
                ResetForm();

                var c = new CProgramClass().Get(CurrentProgramClassId);
                if (c != null)
                {
                    var program = new CProgram().Get(c.ProgramId);
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

                        if (c.ProgramCourseId != null)
                        {
                            var programCourse = new CProgramCourse().Get((int)c.ProgramCourseId);
                            if (programCourse != null)
                            {
                                RadComboBoxProgramCourse.SelectedValue = programCourse.ProgramCourseId.ToString();
                                LoadProgramCourseLevel(RadComboBoxProgramCourse.SelectedValue);
                            }
                        }

                        if (c.ProgramCourseLevelId != null)
                        {
                            var programCourseLevel = new CProgramCourseLevel().Get((int)c.ProgramCourseLevelId);
                            if (programCourseLevel != null)
                            {
                                RadComboBoxProgramCourseLevel.SelectedValue = programCourseLevel.ProgramCourseLevelId.ToString();
                                LoadProgramClass(RadComboBoxProgramCourse.SelectedValue, CurrentProgramClassId);

                            }
                        }
                    }
                }
            }

            SearchProgramClassStudent();

            RadComboBoxFaculty.OpenDropDownOnLoad = false;
            RadComboBoxProgramGroup.OpenDropDownOnLoad = false;
            RadComboBoxProgram.OpenDropDownOnLoad = false;
            RadComboBoxProgramCourse.OpenDropDownOnLoad = false;
            RadComboBoxProgramCourseLevel.OpenDropDownOnLoad = false;
            RadComboBoxProgramClass.OpenDropDownOnLoad = false;
        }

        private void SearchProgramClassStudent()
        {
            LinqDataSourceClassStudentList.WhereParameters.Clear();
            if (string.IsNullOrEmpty(RadComboBoxProgramClass.SelectedValue))
                LinqDataSourceClassStudentList.WhereParameters.Add("ProgramClassId", DbType.Int32, 0.ToString());
            else
                LinqDataSourceClassStudentList.WhereParameters.Add("ProgramClassId", DbType.Int32, RadComboBoxProgramClass.SelectedValue);
            LinqDataSourceClassStudentList.Where = "ProgramClassId == @ProgramClassId";
        }

        protected void ResetForm()
        {
            LoadFaculty();
            LoadProgramGroup(null);
            LoadProgram(null);
            LoadProgramCourse(null);
            LoadProgramCourseLevel(null);
            LoadProgramClass(null, 0);
        }

        public override void SetVisibleModifyControllers()
        {
            if (UserPermissionModel.IsModify == false)
            {

            }
        }

        protected void RadGridClassStudent_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
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
            RadComboBoxProgramCourse.Items.Insert(0, new RadComboBoxItem("N/A", null));
        }

        protected void LoadProgramCourseLevel(string programCourseId)
        {
            RadComboBoxProgramCourseLevel.Items.Clear();
            RadComboBoxProgramCourseLevel.Text = string.Empty;
            if (!string.IsNullOrEmpty(programCourseId))
            {
                RadComboBoxProgramCourseLevel.DataSource = new CProgramCourseLevel().GetProgramCourseLevelList(Convert.ToInt32(programCourseId));
                RadComboBoxProgramCourseLevel.DataTextField = "Name";
                RadComboBoxProgramCourseLevel.DataValueField = "Value";
                RadComboBoxProgramCourseLevel.DataBind();
            }
            RadComboBoxProgramCourseLevel.Items.Insert(0, new RadComboBoxItem("N/A", null));
        }

        protected void LoadProgramClass(string programCourseLevelId, int currentProgramClassId)
        {
            RadComboBoxProgramClass.Items.Clear();
            RadComboBoxProgramClass.Text = string.Empty;
            if (!string.IsNullOrEmpty(programCourseLevelId))
            {
                RadComboBoxProgramClass.DataSource = new CProgramClass().GetProgramClassList(Convert.ToInt32(programCourseLevelId), currentProgramClassId);
                RadComboBoxProgramClass.DataTextField = "Name";
                RadComboBoxProgramClass.DataValueField = "Value";
                RadComboBoxProgramClass.DataBind();
            }
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
            LoadProgramCourseLevel(RadComboBoxProgramCourse.SelectedValue);
            RadComboBoxProgramCourseLevel.OpenDropDownOnLoad = true;
        }

        protected void RadComboBoxProgramCourseLevel_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadProgramClass(RadComboBoxProgramCourseLevel.SelectedValue, CurrentProgramClassId);
            RadComboBoxProgramClass.OpenDropDownOnLoad = true;

            SearchProgramClassStudent();
        }

        protected void RadComboBoxProgramClass_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SearchProgramClassStudent();
        }

        protected void RadToolBar1_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Text)
            {
                case "Save":
                    foreach (var v in _classStudentIdList)
                    {
                        var cProgramClassStudent = new CProgramClassStudent();
                        var programClassStudent = cProgramClassStudent.Get(v);
                        if (programClassStudent != null)
                        {
                            programClassStudent.ProgramClassId = Convert.ToInt32(RadComboBoxProgramClass.SelectedValue);
                            programClassStudent.ProgramCourseId = Convert.ToInt32(RadComboBoxProgramCourse.SelectedValue);

                            programClassStudent.UpdatedId = CurrentUserId;
                            programClassStudent.UpdatedDate = DateTime.Now;

                            cProgramClassStudent.Update(programClassStudent);
                        }
                    }

                    RunClientScript("Close();");
                    break;
            }

        }


    }
}