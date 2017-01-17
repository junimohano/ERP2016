using System.Diagnostics;

namespace Erp2016.Lib.Report.Academics
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for RStartingStudents.
    /// </summary>
    public partial class RAttendanceSheet : Telerik.Reporting.Report
    {
        private int _rowCount = 0;

        public RAttendanceSheet(int programClassId)
        {
            InitializeComponent();

            var vwProgramClass = new CProgramClass().GetVwProgramClass(programClassId);

            htmlTextBoxClassInfo.Value = $@"Class : <b>{vwProgramClass.ClassName}</b><br>
Instructor : <b>{vwProgramClass.InstructorName}</b><br>
Room : <b>{vwProgramClass.ClassRoomName}</b><br>
Time : <b>{"N/A"}</b>";

            ReportParameters["ProgramClassId"].Value = programClassId;
            ReportParameters["StartDate"].Value = DateTime.Today.AddMonths(-1);
            ReportParameters["EndDate"].Value = DateTime.Today;
        }

        private void RAttendanceSheet_ItemDataBinding(object sender, EventArgs e)
        {
            try
            {
                _rowCount = 0;
                textBoxWeek1Mon.Value = string.Empty;
                textBoxWeek2Mon.Value = string.Empty;
                textBoxWeek3Mon.Value = string.Empty;
                textBoxWeek4Mon.Value = string.Empty;
                textBoxWeek1Tue.Value = string.Empty;
                textBoxWeek2Tue.Value = string.Empty;
                textBoxWeek3Tue.Value = string.Empty;
                textBoxWeek4Tue.Value = string.Empty;
                textBoxWeek1Wed.Value = string.Empty;
                textBoxWeek2Wed.Value = string.Empty;
                textBoxWeek3Wed.Value = string.Empty;
                textBoxWeek4Wed.Value = string.Empty;
                textBoxWeek1Thu.Value = string.Empty;
                textBoxWeek2Thu.Value = string.Empty;
                textBoxWeek3Thu.Value = string.Empty;
                textBoxWeek4Thu.Value = string.Empty;
                textBoxWeek1Fri.Value = string.Empty;
                textBoxWeek2Fri.Value = string.Empty;
                textBoxWeek3Fri.Value = string.Empty;
                textBoxWeek4Fri.Value = string.Empty;

                Telerik.Reporting.Processing.Report rpt = (Telerik.Reporting.Processing.Report)sender;

                var startDate = ((DateTime)rpt.Parameters["StartDate"].Value).Date;
                var endDate = ((DateTime)rpt.Parameters["EndDate"].Value).Date;

                bool isFirstInsert = true;
                DateTime startDateReal = DateTime.Today;
                DateTime endDateReal = DateTime.Today;

                int weekIndex = 1;

                while (startDate <= endDate)
                {
                    Telerik.Reporting.TextBox tb = null;
                    switch (startDate.DayOfWeek)
                    {
                        case DayOfWeek.Monday:
                            switch (weekIndex)
                            {
                                case 1:
                                    tb = textBoxWeek1Mon;
                                    break;
                                case 2:
                                    tb = textBoxWeek2Mon;
                                    break;
                                case 3:
                                    tb = textBoxWeek3Mon;
                                    break;
                                case 4:
                                    tb = textBoxWeek4Mon;
                                    break;
                            }
                            break;
                        case DayOfWeek.Tuesday:
                            switch (weekIndex)
                            {
                                case 1:
                                    tb = textBoxWeek1Tue;
                                    break;
                                case 2:
                                    tb = textBoxWeek2Tue;
                                    break;
                                case 3:
                                    tb = textBoxWeek3Tue;
                                    break;
                                case 4:
                                    tb = textBoxWeek4Tue;
                                    break;
                            }
                            break;
                        case DayOfWeek.Wednesday:
                            switch (weekIndex)
                            {
                                case 1:
                                    tb = textBoxWeek1Wed;
                                    break;
                                case 2:
                                    tb = textBoxWeek2Wed;
                                    break;
                                case 3:
                                    tb = textBoxWeek3Wed;
                                    break;
                                case 4:
                                    tb = textBoxWeek4Wed;
                                    break;
                            }
                            break;
                        case DayOfWeek.Thursday:
                            switch (weekIndex)
                            {
                                case 1:
                                    tb = textBoxWeek1Thu;
                                    break;
                                case 2:
                                    tb = textBoxWeek2Thu;
                                    break;
                                case 3:
                                    tb = textBoxWeek3Thu;
                                    break;
                                case 4:
                                    tb = textBoxWeek4Thu;
                                    break;
                            }
                            break;
                        case DayOfWeek.Friday:
                            switch (weekIndex)
                            {
                                case 1:
                                    tb = textBoxWeek1Fri;
                                    break;
                                case 2:
                                    tb = textBoxWeek2Fri;
                                    break;
                                case 3:
                                    tb = textBoxWeek3Fri;
                                    break;
                                case 4:
                                    tb = textBoxWeek4Fri;
                                    break;
                            }

                            weekIndex++;
                            break;
                    }

                    if (tb != null)
                    {
                        tb.Value = startDate.Day.ToString();

                        if (isFirstInsert)
                        {
                            startDateReal = startDate;
                            isFirstInsert = false;
                        }
                        endDateReal = startDate;
                    }

                    startDate = startDate.AddDays(1);
                }

                //rpt.Parameters["StartDate"].Value = startDateReal;
                //rpt.Parameters["EndDate"].Value = endDateReal;

                htmlTextBoxDate.Value = startDateReal.ToString("MM-dd-yyyy") + " ~ " + endDateReal.ToString("MM-dd-yyyy");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }

        private void htmlTextBoxStudentInfo_ItemDataBinding(object sender, EventArgs e)
        {
            Telerik.Reporting.Processing.HtmlTextBox txt = (Telerik.Reporting.Processing.HtmlTextBox)sender;
            Telerik.Reporting.Processing.IDataObject dataObject = (Telerik.Reporting.Processing.IDataObject)txt.DataObject;

            txt.Value = $@"{dataObject["StudentNo"]} <b>{dataObject["StudentName"]}</b><br>{dataObject["Gender"]} {((DateTime)dataObject["StartDate"]).ToString("MM-dd-yy")}~{((DateTime)dataObject["EndDate"]).ToString("MM-dd-yy")} <b>{dataObject["CountryName"]}</b>";

            _rowCount++;
        }

        private void htmlTextBoxTotal_ItemDataBinding(object sender, EventArgs e)
        {
            htmlTextBoxTotal.Value = $@"<b>Total : {_rowCount}</b>";
        }
    }
}