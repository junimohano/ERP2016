using System;
using System.Data;
using Erp2016.Lib;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;

namespace School.AcademicRegistrar
{
    public partial class ProgramClass : PageBase
    {
        public ProgramClass() : base((int)CConstValue.Menu.ProgramClass)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ResetForm();
            }

            LinqDataSourceProgramClass.WhereParameters.Clear();
            foreach (var model in UserPermissionModel.SearchSiteLocationList)
                LinqDataSourceProgramClass.WhereParameters.Add(model.SiteLocationIdName, DbType.Int32, model.SiteLocationId.ToString());
            LinqDataSourceProgramClass.Where = UserPermissionModel.SearchWhereSiteLocationSb.ToString();
            // get completed or rejected list

            RadComboBoxFaculty.OpenDropDownOnLoad = false;
            RadComboBoxProgramGroup.OpenDropDownOnLoad = false;
            RadComboBoxProgram.OpenDropDownOnLoad = false;
            RadComboBoxProgramCourse.OpenDropDownOnLoad = false;
            RadComboBoxProgramCourseLevel.OpenDropDownOnLoad = false;
            RadComboBoxWeeks.OpenDropDownOnLoad = false;
        }

        public override void SetVisibleModifyControllers()
        {
            if (UserPermissionModel.IsModify == false)
            {
                // toolbar
                foreach (RadToolBarItem toolbarItem in RadToolBarProgramClass.Items)
                {
                    toolbarItem.Enabled = false;
                }
            }
        }

        protected void RadGridProgramClass_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        protected void RadGridProgramClass_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GetProgramClass();
        }

        protected void RadToolBarProgramClass_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Text)
            {
                case "New":
                    RadGridProgramClass.SelectedIndexes.Clear();
                    ResetForm();
                    break;

                case "Save":
                    var cC = new CProgramClass();
                    var c = new Erp2016.Lib.ProgramClass();
                    c.ProgramId = Convert.ToInt32(RadComboBoxProgram.SelectedValue);

                    if (string.IsNullOrEmpty(RadComboBoxProgramCourse.SelectedValue) == false)
                        c.ProgramCourseId = Convert.ToInt32(RadComboBoxProgramCourse.SelectedValue);

                    if (string.IsNullOrEmpty(RadComboBoxProgramCourseLevel.SelectedValue) == false)
                        c.ProgramCourseLevelId = Convert.ToInt32(RadComboBoxProgramCourseLevel.SelectedValue);

                    c.InstructorId = Convert.ToInt32(RadComboBoxInstructor.SelectedValue);
                    c.ProgramClassRoomId = Convert.ToInt32(RadComboBoxClassRoom.SelectedValue);

                    c.StartDate = RadDatePickerStartDate.SelectedDate.Value;
                    c.EndDate = RadDatePickerEndDate.SelectedDate.Value;
                    c.ClassWeek = Convert.ToInt32(RadComboBoxWeeks.SelectedValue);

                    if (RadTimePickerStartMon.SelectedTime != null && RadTimePickerEndMon.SelectedTime != null)
                    {
                        c.MondayStartTime = RadTimePickerStartMon.SelectedTime;
                        c.MondayEndTime = RadTimePickerEndMon.SelectedTime;
                    }
                    if (RadTimePickerStartTue.SelectedTime != null && RadTimePickerEndTue.SelectedTime != null)
                    {
                        c.TuesdayStartTime = RadTimePickerStartTue.SelectedTime;
                        c.TuesdayEndTime = RadTimePickerEndTue.SelectedTime;
                    }
                    if (RadTimePickerStartWed.SelectedTime != null && RadTimePickerEndWed.SelectedTime != null)
                    {
                        c.WednesdayStartTime = RadTimePickerStartWed.SelectedTime;
                        c.WednesdayEndTime = RadTimePickerEndWed.SelectedTime;
                    }
                    if (RadTimePickerStartThu.SelectedTime != null && RadTimePickerEndThu.SelectedTime != null)
                    {
                        c.ThursdayStartTime = RadTimePickerStartThu.SelectedTime;
                        c.ThursdayEndTime = RadTimePickerEndThu.SelectedTime;
                    }
                    if (RadTimePickerStartFri.SelectedTime != null && RadTimePickerEndFri.SelectedTime != null)
                    {
                        c.FridayStartTime = RadTimePickerStartFri.SelectedTime;
                        c.FridayEndTime = RadTimePickerEndFri.SelectedTime;
                    }

                    c.WeekDays = RadNumericTextBoxClassDays.Value;
                    c.WeekHours = RadNumericTextBoxClassHours.Value;
                    c.TotalHours = RadNumericTextBoxTotalHours.Value;

                    c.SiteLocationId = CurrentSiteLocationId;
                    c.Name = RadTextBoxProgramClass.Text;
                    c.Description = RadTextBoxDescription.Text;
                    c.IsActive = RadButtonActive.Checked;
                    c.CreatedId = CurrentUserId;
                    c.CreatedDate = DateTime.Now.Date;

                    if (cC.Add(c) > 0)
                    {
                        ShowMessage("'" + c.Name + "' is added.");
                        RadGridProgramClass.Rebind();
                    }
                    else
                        ShowMessage("Failed to add");
                    break;

                case "Update":
                    if (RadGridProgramClass.SelectedValue != null)
                    {
                        var cP = new CProgramClass();
                        var p = cP.Get(Convert.ToInt32(RadGridProgramClass.SelectedValue));
                        p.ProgramId = Convert.ToInt32(RadComboBoxProgram.SelectedValue);

                        if (string.IsNullOrEmpty(RadComboBoxProgramCourse.SelectedValue) == false)
                            p.ProgramCourseId = Convert.ToInt32(RadComboBoxProgramCourse.SelectedValue);
                        else
                            p.ProgramCourseId = null;

                        if (string.IsNullOrEmpty(RadComboBoxProgramCourseLevel.SelectedValue) == false)
                            p.ProgramCourseLevelId = Convert.ToInt32(RadComboBoxProgramCourseLevel.SelectedValue);
                        else
                            p.ProgramCourseLevelId = null;

                        p.InstructorId = Convert.ToInt32(RadComboBoxInstructor.SelectedValue);
                        p.ProgramClassRoomId = Convert.ToInt32(RadComboBoxClassRoom.SelectedValue);

                        p.StartDate = RadDatePickerStartDate.SelectedDate.Value;
                        p.EndDate = RadDatePickerEndDate.SelectedDate.Value;
                        p.ClassWeek = Convert.ToInt32(RadComboBoxWeeks.SelectedValue);

                        if (RadTimePickerStartMon.SelectedTime != null && RadTimePickerEndMon.SelectedTime != null)
                        {
                            p.MondayStartTime = RadTimePickerStartMon.SelectedTime;
                            p.MondayEndTime = RadTimePickerEndMon.SelectedTime;
                        }
                        if (RadTimePickerStartTue.SelectedTime != null && RadTimePickerEndTue.SelectedTime != null)
                        {
                            p.TuesdayStartTime = RadTimePickerStartTue.SelectedTime;
                            p.TuesdayEndTime = RadTimePickerEndTue.SelectedTime;
                        }
                        if (RadTimePickerStartWed.SelectedTime != null && RadTimePickerEndWed.SelectedTime != null)
                        {
                            p.WednesdayStartTime = RadTimePickerStartWed.SelectedTime;
                            p.WednesdayEndTime = RadTimePickerEndWed.SelectedTime;
                        }
                        if (RadTimePickerStartThu.SelectedTime != null && RadTimePickerEndThu.SelectedTime != null)
                        {
                            p.ThursdayStartTime = RadTimePickerStartThu.SelectedTime;
                            p.ThursdayEndTime = RadTimePickerEndThu.SelectedTime;
                        }
                        if (RadTimePickerStartFri.SelectedTime != null && RadTimePickerEndFri.SelectedTime != null)
                        {
                            p.FridayStartTime = RadTimePickerStartFri.SelectedTime;
                            p.FridayEndTime = RadTimePickerEndFri.SelectedTime;
                        }

                        p.WeekDays = RadNumericTextBoxClassDays.Value;
                        p.WeekHours = RadNumericTextBoxClassHours.Value;
                        p.TotalHours = RadNumericTextBoxTotalHours.Value;

                        p.SiteLocationId = CurrentSiteLocationId;
                        p.Name = RadTextBoxProgramClass.Text;
                        p.Description = RadTextBoxDescription.Text;
                        p.IsActive = RadButtonActive.Checked;

                        p.UpdatedId = CurrentUserId;
                        p.UpdatedDate = DateTime.Now.Date;

                        if (cP.Update(p))
                        {
                            ShowMessage("'" + p.Name + "' is updated.");
                            RadGridProgramClass.Rebind();
                        }
                        else
                            ShowMessage("Failed to update");
                    }
                    break;

                case "Student Information":
                    if (RadGridProgramClass.SelectedValue != null)
                        RunClientScript("ShowStudentClassInfo('" + RadGridProgramClass.SelectedValue + "', '" + RadGridProgramClass.SelectedValues["ProgramCourseId"] + "');");
                    break;
                case "Student Attendance":
                    if (RadGridProgramClass.SelectedValue != null)
                        RunClientScript("ShowStudentClassAttendance('" + RadGridProgramClass.SelectedValue + "');");
                    break;
                case "Student Grade":
                    if (RadGridProgramClass.SelectedValue != null)
                        RunClientScript("ShowStudentClassGrade('" + RadGridProgramClass.SelectedValue + "');");
                    break;

                // Schools
                case "Class Summary":
                    if (RadGridProgramClass.SelectedValue != null)
                        RunClientScript("ShowReportPop('" + RadGridProgramClass.SelectedValue + "', '" + (int)CConstValue.Report.ClassSummary + "' );");
                    break;
                case "Starting Students":
                    if (RadGridProgramClass.SelectedValue != null)
                        RunClientScript("ShowReportPop('" + RadGridProgramClass.SelectedValue + "', '" + (int)CConstValue.Report.StartingStudents + "' );");
                    break;
                case "Completed Graduates Students":
                    if (RadGridProgramClass.SelectedValue != null)
                        RunClientScript("ShowReportPop('" + RadGridProgramClass.SelectedValue + "', '" + (int)CConstValue.Report.CompletedGraduatesStudents + "' );");
                    break;
                case "Attendance Sheet":
                    if (RadGridProgramClass.SelectedValue != null)
                        RunClientScript("ShowReportPop('" + RadGridProgramClass.SelectedValue + "', '" + (int)CConstValue.Report.AttendanceSheet + "');");
                    break;
            }
        }

        protected void GetProgramClass()
        {
            ResetForm();
            if (RadGridProgramClass.SelectedValue != null)
            {
                var c = new CProgramClass().Get(Convert.ToInt32(RadGridProgramClass.SelectedValue));
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
                            }
                        }
                    }
                    RadComboBoxInstructor.SelectedValue = c.InstructorId.ToString();
                    RadTextBoxProgramClass.Text = c.Name;
                    RadTextBoxDescription.Text = c.Description;
                    RadButtonActive.Checked = c.IsActive;

                    RadDatePickerStartDate.SelectedDate = c.StartDate;
                    RadDatePickerEndDate.SelectedDate = c.EndDate;
                    RadComboBoxWeeks.SelectedValue = c.ClassWeek.ToString();

                    RadTimePickerStartMon.SelectedTime = c.MondayStartTime;
                    RadTimePickerStartTue.SelectedTime = c.TuesdayStartTime;
                    RadTimePickerStartWed.SelectedTime = c.WednesdayStartTime;
                    RadTimePickerStartThu.SelectedTime = c.ThursdayStartTime;
                    RadTimePickerStartFri.SelectedTime = c.FridayStartTime;

                    RadTimePickerEndMon.SelectedTime = c.MondayEndTime;
                    RadTimePickerEndTue.SelectedTime = c.TuesdayEndTime;
                    RadTimePickerEndWed.SelectedTime = c.WednesdayEndTime;
                    RadTimePickerEndThu.SelectedTime = c.ThursdayEndTime;
                    RadTimePickerEndFri.SelectedTime = c.FridayEndTime;
                }

                SetClassTime();

                RadToolBarProgramClass.FindItemByText("New").Enabled = true;
                if (RadToolBarProgramClass.FindItemByText("Save") != null) RadToolBarProgramClass.FindItemByText("Save").Text = "Update";
            }
        }

        protected void ResetForm()
        {
            LoadFaculty();
            LoadProgramGroup(null);
            LoadProgram(null);
            LoadProgramCourse(null);
            LoadProgramCourseLevel(null);

            LoadClassWeeks();
            LoadInstructor();
            LoadClassRoom();

            RadTimePickerStartMon.SelectedTime = null;
            RadTimePickerStartTue.SelectedTime = null;
            RadTimePickerStartWed.SelectedTime = null;
            RadTimePickerStartThu.SelectedTime = null;
            RadTimePickerStartFri.SelectedTime = null;

            RadTimePickerEndMon.SelectedTime = null;
            RadTimePickerEndTue.SelectedTime = null;
            RadTimePickerEndWed.SelectedTime = null;
            RadTimePickerEndThu.SelectedTime = null;
            RadTimePickerEndFri.SelectedTime = null;

            RadDatePickerStartDate.SelectedDate = null;
            RadDatePickerEndDate.SelectedDate = null;

            RadTextBoxProgramClass.Text = string.Empty;
            RadTextBoxDescription.Text = string.Empty;
            RadButtonActive.Checked = true;

            RadToolBarProgramClass.FindItemByText("New").Enabled = false;
            if (RadToolBarProgramClass.FindItemByText("Update") != null) RadToolBarProgramClass.FindItemByText("Update").Text = "Save";
        }

        private void LoadInstructor()
        {
            RadComboBoxInstructor.Items.Clear();
            RadComboBoxInstructor.Text = string.Empty;
            RadComboBoxInstructor.DataSource = new CUser().GetInstructorList(CurrentSiteLocationId);
            RadComboBoxInstructor.DataTextField = "Name";
            RadComboBoxInstructor.DataValueField = "Value";
            RadComboBoxInstructor.DataBind();
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

        protected void LoadClassWeeks()
        {
            RadComboBoxWeeks.Items.Clear();
            RadComboBoxWeeks.Text = string.Empty;
            RadComboBoxWeeks.DataSource = new CProgram().GetProgramWeeksList();
            RadComboBoxWeeks.DataTextField = "Name";
            RadComboBoxWeeks.DataValueField = "Value";
            RadComboBoxWeeks.DataBind();
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

        protected void LoadClassRoom()
        {
            RadComboBoxClassRoom.Items.Clear();
            RadComboBoxClassRoom.Text = string.Empty;
            RadComboBoxClassRoom.DataSource = new CProgramClassRoom().GetClassRoomListBySiteLocationId(CurrentSiteLocationId);
            RadComboBoxClassRoom.DataTextField = "Name";
            RadComboBoxClassRoom.DataValueField = "Value";
            RadComboBoxClassRoom.DataBind();
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
            RadTextBoxProgramClass.Focus();
        }

        protected void RadDatePickerStartDate_OnSelectedDateChanged(object o, SelectedDateChangedEventArgs e)
        {
            RadComboBoxWeeks.OpenDropDownOnLoad = true;
        }

        protected void RadComboBoxWeeks_OnSelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (RadDatePickerStartDate.SelectedDate != null)
            {
                var weeks = Convert.ToInt32(e.Value);
                var start = Convert.ToDateTime(RadDatePickerStartDate.SelectedDate);
                RadDatePickerEndDate.SelectedDate = CProgramRegistration.GetEndDate(start, weeks);
            }
        }

        private void SetClassTime()
        {
            int classDays = 0;
            double classHours = 0;
            if (RadTimePickerStartMon.SelectedTime != null && RadTimePickerEndMon.SelectedTime != null)
            {
                classDays++;
                classHours = (RadTimePickerEndMon.SelectedTime - RadTimePickerStartMon.SelectedTime).Value.TotalHours;
            }

            if (RadTimePickerStartTue.SelectedTime != null && RadTimePickerEndTue.SelectedTime != null)
            {
                classDays++;
                classHours += (RadTimePickerEndTue.SelectedTime - RadTimePickerStartTue.SelectedTime).Value.TotalHours;
            }

            if (RadTimePickerStartWed.SelectedTime != null && RadTimePickerEndWed.SelectedTime != null)
            {
                classDays++;
                classHours += (RadTimePickerEndWed.SelectedTime - RadTimePickerStartWed.SelectedTime).Value.TotalHours;
            }

            if (RadTimePickerStartThu.SelectedTime != null && RadTimePickerEndThu.SelectedTime != null)
            {
                classDays++;
                classHours += (RadTimePickerEndThu.SelectedTime - RadTimePickerStartThu.SelectedTime).Value.TotalHours;
            }

            if (RadTimePickerStartFri.SelectedTime != null && RadTimePickerEndFri.SelectedTime != null)
            {
                classDays++;
                classHours += (RadTimePickerEndFri.SelectedTime - RadTimePickerStartFri.SelectedTime).Value.TotalHours;
            }

            RadNumericTextBoxClassDays.Text = classDays.ToString();
            RadNumericTextBoxClassHours.Text = classHours.ToString();
            RadNumericTextBoxTotalHours.Text = (classDays * classHours).ToString();
        }

        protected void RadTimePicker_OnSelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            SetClassTime();
        }

        protected void ButtonGridRefresh_OnClick(object sender, EventArgs e)
        {
            RadGridProgramClass.Rebind();
        }
    }
}