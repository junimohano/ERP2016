using System;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.AcademicRegistrar
{
    public partial class GradeSchemaPop : PageBase
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public GradeSchemaPop() : base((int)CConstValue.Menu.GradeSchema)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Id = Convert.ToInt32(Request["id"]);
            Type = Request["type"];

            if (!IsPostBack)
            {
                ResetForm();

                // new
                if (Type == "0")
                {
                    //
                }
                // modify
                else
                {
                    var gradeSchema = new CGradeSchema();
                    var grade = gradeSchema.Get(Id);
                    if (grade != null)
                    {
                        RadTextBoxName.Text = grade.Name;
                        if (grade.IsGlobal)
                            RadComboBoxIsGlobal.SelectedIndex = 0;
                        else
                            RadComboBoxIsGlobal.SelectedIndex = 1;

                        if (grade.ProgramId != null)
                        {
                            var program = new CProgram().Get(Convert.ToInt32(grade.ProgramId));
                            if (program != null)
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

                            if (grade.ProgramCourseId != null)
                            {
                                var programCourse = new CProgramCourse().Get(Convert.ToInt32(grade.ProgramCourseId));
                                if (programCourse != null)
                                {
                                    RadComboBoxProgramCourse.SelectedValue = programCourse.ProgramCourseId.ToString();
                                    LoadProgramCourseLevel(RadComboBoxProgramCourse.SelectedValue);

                                    if (grade.ProgramCourseLevelId != null)
                                    {
                                        var programCourseLevel = new CProgramCourseLevel().Get(Convert.ToInt32(grade.ProgramCourseLevelId));
                                        if (programCourseLevel != null)
                                        {
                                            RadComboBoxProgramCourseLevel.SelectedValue = programCourseLevel.ProgramCourseLevelId.ToString();
                                            LoadProgramClass(RadComboBoxProgram.SelectedValue, RadComboBoxProgramCourse.SelectedValue, RadComboBoxProgramCourseLevel.SelectedValue);

                                            if (grade.ProgramClassId != null)
                                            {
                                                var programclass = new CProgramClass().Get(Convert.ToInt32(grade.ProgramClassId));
                                                if (programclass != null)
                                                {
                                                    RadComboBoxProgramClass.SelectedValue = programclass.ProgramClassId.ToString();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            RadComboBoxFaculty.OpenDropDownOnLoad = false;
            RadComboBoxProgramGroup.OpenDropDownOnLoad = false;
            RadComboBoxProgram.OpenDropDownOnLoad = false;
            RadComboBoxProgramCourse.OpenDropDownOnLoad = false;
            RadComboBoxProgramCourseLevel.OpenDropDownOnLoad = false;
            RadComboBoxProgramClass.OpenDropDownOnLoad = false;
        }

        protected void RadToolBar1_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            if (e.Item.Text == "Save")
            {
                var cGradeSchema = new CGradeSchema();
                var gradeSchema = new Erp2016.Lib.GradeSchema();

                // new
                if (Type == "0")
                {
                    gradeSchema = new Erp2016.Lib.GradeSchema();
                    gradeSchema.CreatedId = CurrentUserId;
                    gradeSchema.CreatedDate = DateTime.Now;
                    gradeSchema.SiteLocationId = CurrentSiteLocationId;

                    if (RadComboBoxIsGlobal.SelectedValue == "1")
                    {
                        var result = cGradeSchema.GetGlobal(CurrentSiteLocationId);
                        if (result != null)
                        {
                            Type = "1";
                            gradeSchema = result;
                        }
                    }
                }
                // modify
                else
                    gradeSchema = cGradeSchema.Get(Id);

                gradeSchema.Name = RadTextBoxName.Text;
                gradeSchema.IsGlobal = RadComboBoxIsGlobal.SelectedValue == "1" ? true : false;

                if (!string.IsNullOrEmpty(RadComboBoxProgram.SelectedValue))
                    gradeSchema.ProgramId = Convert.ToInt32(RadComboBoxProgram.SelectedValue);

                if (!string.IsNullOrEmpty(RadComboBoxProgramCourse.SelectedValue))
                    gradeSchema.ProgramCourseId = Convert.ToInt32(RadComboBoxProgramCourse.SelectedValue);

                if (!string.IsNullOrEmpty(RadComboBoxProgramCourseLevel.SelectedValue))
                    gradeSchema.ProgramCourseLevelId = Convert.ToInt32(RadComboBoxProgramCourseLevel.SelectedValue);

                if (!string.IsNullOrEmpty(RadComboBoxProgramClass.SelectedValue))
                    gradeSchema.ProgramClassId = Convert.ToInt32(RadComboBoxProgramClass.SelectedValue);

                if (gradeSchema.IsGlobal)
                {
                    gradeSchema.ProgramId = null;
                    gradeSchema.ProgramCourseId = null;
                    gradeSchema.ProgramCourseLevelId = null;
                    gradeSchema.ProgramClassId = null;
                }

                // new
                if (Type == "0")
                {
                    cGradeSchema.Add(gradeSchema);
                }
                // modify
                else
                {
                    gradeSchema.UpdatedId = CurrentUserId;
                    gradeSchema.UpdatedDate = DateTime.Now;
                    cGradeSchema.Update(gradeSchema);
                }

                RunClientScript("Close();");
            }
        }

        protected void ResetForm()
        {
            LoadFaculty();
            LoadProgramGroup(null);
            LoadProgram(null);
            LoadProgramCourse(null);
            LoadProgramCourseLevel(null);
            LoadProgramClass(null, null, null);
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

        protected void LoadProgramClass(string programId, string programCourseId, string programCourseLevelId)
        {
            RadComboBoxProgramClass.Items.Clear();
            RadComboBoxProgramClass.Text = string.Empty;
            if (!string.IsNullOrEmpty(programId))
            {
                RadComboBoxProgramClass.DataSource = new CProgramClass().GetProgramClassList(Convert.ToInt32(programId), string.IsNullOrEmpty(programCourseId) ? 0 : Convert.ToInt32(programCourseId), string.IsNullOrEmpty(programCourseLevelId) ? 0 : Convert.ToInt32(programCourseLevelId));
                RadComboBoxProgramClass.DataTextField = "Name";
                RadComboBoxProgramClass.DataValueField = "Value";
                RadComboBoxProgramClass.DataBind();
            }
            RadComboBoxProgramClass.Items.Insert(0, new RadComboBoxItem("N/A", null));
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
            LoadProgramClass(RadComboBoxProgram.SelectedValue, RadComboBoxProgramCourse.SelectedValue, RadComboBoxProgramCourseLevel.SelectedValue);
            RadComboBoxProgramClass.OpenDropDownOnLoad = true;
        }
    }
}