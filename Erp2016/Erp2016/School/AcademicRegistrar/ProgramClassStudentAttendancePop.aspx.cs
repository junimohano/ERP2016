using System;
using System.Data;
using System.Linq;
using Erp2016.Lib;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;
using Telerik.Web.UI.Calendar.View;

namespace School.AcademicRegistrar
{
    public partial class ProgramClassStudentAttendancePop : PageBase
    {
        private int ProgramClassId { get; set; }

        public ProgramClassStudentAttendancePop() : base((int)CConstValue.Menu.ProgramClass)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ProgramClassId = Convert.ToInt32(Request["programClassId"]);

            if (!IsPostBack)
            {
                ResetForm();
                LoadAttendanceType();
            }

            LinqDataSourceClassStudentList.WhereParameters.Clear();
            LinqDataSourceClassStudentList.WhereParameters.Add("ProgramClassId", DbType.Int32, ProgramClassId.ToString());
            LinqDataSourceClassStudentList.Where = "ProgramClassId == @ProgramClassId";

            RadDatePickerToday.SelectedDate = DateTime.Today;
        }

        protected void LoadAttendanceType()
        {
            RadComboBoxAttendanceType.Items.Clear();
            RadComboBoxAttendanceType.Text = string.Empty;
            RadComboBoxAttendanceType.DataSource = new CAttendance().GetAttendanceTypeList();
            RadComboBoxAttendanceType.DataTextField = "Name";
            RadComboBoxAttendanceType.DataValueField = "Value";
            RadComboBoxAttendanceType.DataBind();
            RadComboBoxAttendanceType.Items.Insert(0, new RadComboBoxItem("N/A", null));
            // present
            RadComboBoxAttendanceType.SelectedIndex = 6;
        }

        private void ResetForm()
        {
            // reset
            RadCalendarAttendance.SelectedDates.Clear();
            RadCalendarAttendance.SpecialDays.Clear();
            RadCalendarAttendance.RangeMinDate = DateTime.Now;
            RadCalendarAttendance.RangeMaxDate = DateTime.Now;
            RadTextBoxAttendanceRate.Text = string.Empty;
        }

        protected void RadGridClassStudent_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GetCalendarAttendance();
        }

        protected void RadCalendarAttendance_OnSelectionChanged(object sender, SelectedDatesEventArgs e)
        {
            if (RadGridClassStudent.SelectedValue != null)
            {
                var attendance = new CAttendance();
                var studentId = Convert.ToInt32(RadGridClassStudent.SelectedValues["StudentId"]);

                foreach (RadDate date in RadCalendarAttendance.SelectedDates)
                {
                    var previousDate = attendance.Get(ProgramClassId, studentId, date.Date);
                    if (previousDate != null)
                        attendance.Delete(previousDate);

                    if (string.IsNullOrEmpty(RadComboBoxAttendanceType.SelectedValue) == false)
                    {
                        // insert
                        attendance.Add(new Erp2016.Lib.Attendance
                        {
                            StudentId = studentId,
                            ProgramClassId = ProgramClassId,
                            AttendanceType = Convert.ToInt32(RadComboBoxAttendanceType.SelectedValue),
                            AttendanceDate = date.Date,
                            CreatedDate = DateTime.Now,
                            CreatedId = CurrentUserId
                        });
                    }
                }

                GetCalendarAttendance();
            }
        }

        /// <summary>
        ///     get days without Working Holiday
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public int GetWorkingDays(DateTime from, DateTime to)
        {
            var totalDays = 0;
            for (var date = from; date <= to; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    if (RadCalendarAttendance.SpecialDays.Cast<RadCalendarDay>().Any(x => x.Date == date) == false)
                        totalDays++;
                }
            }

            return totalDays;
        }

        private void GetCalendarAttendance()
        {
            ResetForm();
            if (RadGridClassStudent.SelectedValue != null)
            {
                RadCalendarAttendance.RangeMinDate = (DateTime)RadGridClassStudent.SelectedValues["StartDate"];
                RadCalendarAttendance.RangeMaxDate = (DateTime)RadGridClassStudent.SelectedValues["EndDate"];

                //// disable the selection for the specific day
                //var calendarDay = new RadCalendarDay();
                //calendarDay.Date = DateTime.Now;
                //calendarDay.IsToday = true;
                //calendarDay.ItemStyle.Font.Strikeout = true;
                //RadCalendarAttendance.SpecialDays.Add(calendarDay);

                var attendanceList = new CAttendance().Get(ProgramClassId, Convert.ToInt32(RadGridClassStudent.SelectedValues["StudentId"]));
                foreach (var a in attendanceList)
                {
                    var cal = new RadCalendarDay();
                    cal.Date = a.AttendanceDate;

                    var cAttendanceDict = new CDict();
                    var attendanceDict = cAttendanceDict.GetDictByTypeAndValue(1515, a.AttendanceType);

                    cal.ToolTip = a.AttendanceDate.ToShortDateString() + " " + attendanceDict.Name + " [" + attendanceDict.Abbreviation + "]";

                    switch (a.AttendanceType)
                    {
                        //Absent
                        case 0:
                            cal.TemplateID = "DayTemplateAbsentAttendance";
                            break;

                        // Break(Vacation)
                        case 1:
                            cal.TemplateID = "DayTemplateBreakAttendance";
                            break;

                        // Dismissal
                        case 2:
                            cal.TemplateID = "DayTemplateDismissalAttendance";
                            break;

                        // Excuse
                        case 3:
                            cal.TemplateID = "DayTemplateExcuseAttendance";
                            break;

                        // Late
                        case 4:
                            cal.TemplateID = "DayTemplateLateAttendance";
                            break;

                        // Present
                        case 5:
                            cal.TemplateID = "DayTemplatePresentAttendance";
                            break;

                        // Probation
                        case 6:
                            cal.TemplateID = "DayTemplateProbationAttendance";
                            break;

                        // Co - op / JSHINE Practicum / Practicum
                        case 7:
                            cal.TemplateID = "DayTemplateCoopAttendance";
                            break;

                        // Withdrawal
                        case 8:
                            cal.TemplateID = "DayTemplateWithdrawalAttendance";
                            break;
                    }

                    RadCalendarAttendance.SpecialDays.Add(cal);
                }

                var startDate = (DateTime)RadGridClassStudent.SelectedValues["StartDate"];
                var endDate = (DateTime)RadGridClassStudent.SelectedValues["EndDate"];
                var totalDay = GetWorkingDays(startDate, endDate);
                var attendanceDays = RadCalendarAttendance.SpecialDays.Cast<RadCalendarDay>().Count(s => s.IsSelectable);

                RadTextBoxAttendanceCount.Text = attendanceDays + " / " + totalDay;
                // percent of total rate
                RadTextBoxAttendanceRate.Text = Math.Round(((attendanceDays / (double)totalDay) * 100), 2) + " %";
            }
        }

        protected void RadCalendarAttendance_OnLoad(object sender, EventArgs e)
        {
            var siteLocation = (new CSiteLocation()).Get(CurrentSiteLocationId);
            var holiday = (new CHoliday()).Get(siteLocation.Province);
            foreach (var h in holiday)
            {
                var cal = new RadCalendarDay();
                cal.Date = h.HolidayDate;
                cal.IsSelectable = false;
                cal.ToolTip = h.Name + (h.Abbreviation == string.Empty ? string.Empty : " (" + h.Abbreviation + ")") + " " + h.HolidayDate.Date.ToShortDateString();
                cal.IsDisabled = true;
                RadCalendarAttendance.SpecialDays.Add(cal);
            }

            //RadCalendarAttendance.FocusedDate = DateTime.Today;
        }

        protected void RadGridClassStudent_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        protected void RadCalendarAttendance_OnPreRender(object sender, EventArgs e)
        {
            foreach (CalendarView view in RadCalendarAttendance.CalendarView.ChildViews)
            {
                ((MonthView)view).TitleFormat = @"MMM yyyy";
            }
        }

        protected void RadCalendarAttendance_OnDayRender(object sender, DayRenderEventArgs e)
        {
            // Do a token replace..
            e.Cell.Text = e.Cell.Text.Replace("{DayOfMonth}", e.Day.Date.Day.ToString());
        }
        
    }
}