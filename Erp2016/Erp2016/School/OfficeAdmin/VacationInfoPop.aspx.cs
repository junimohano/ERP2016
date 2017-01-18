using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Erp2016.Lib;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;
using Telerik.Web.UI.Calendar.View;

namespace School.OfficeAdmin
{
    public partial class VacationInfoPop : PageBase
    {
        private int Id { get; set; }

        public VacationInfoPop() : base((int)CConstValue.Menu.Vacation)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Id = Convert.ToInt32(Request["id"]);

            if (!IsPostBack)
            {
                var scriptManager = (RadScriptManager)Page.Master.FindControl("RadScriptManager1");
                //scriptManager.Scripts.Add(new ScriptReference() { Path = ResolveUrl("~/assets/js/jquery.printArea.js") });
                //scriptManager.RegisterPostBackControl(RadButtonFileDownload);

                var obj = new CVacation();
                var vacationDetail = obj.Get(Id);
                var cUser = new CUser();
                var user = cUser.Get((int)vacationDetail.CreatedId);
                ViewState["UserId"] = user.UserId;

                SetVisibleItems(false);

                var dt = new DataTable();
                dt.Columns.Add("FullName");
                dt.Columns.Add("Site");
                dt.Columns.Add("SiteLocation");
                var newDr = dt.NewRow();
                newDr["FullName"] = cUser.GetUserName(user);
                var campus = (new CSiteLocation()).Get(user.SiteLocationId);
                newDr["SiteLocation"] = campus.Name;
                var site = (new CSite()).Get(campus.SiteId);
                newDr["Site"] = site.Name;
                dt.Rows.Add(newDr);
                RadGridInfo.DataSource = dt;
            }
        }

        private void SetVisibleItems(bool isActive)
        {
            if (isActive == false)
            {
                //RadComboBoxVacationType.Enabled = false;
            }
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            // close
            if (e.Item.Text == "Close")
            {
                RunClientScript("Close();");
            }
            else if (e.Item.Text == "Print")
            {
                RunClientScript("ShowPrint();");
            }
        }

        protected void RadGridDays_Load(object sender, EventArgs e)
        {
            var cVacationSchema = new CVacationSchema();
            CVacationTypeInfoModel vacationTypeInfoModel = cVacationSchema.GetVacationTypeInfoModel(Convert.ToInt32(ViewState["UserId"]));
            ((RadGrid)sender).DataSource = cVacationSchema.Get(vacationTypeInfoModel);
        }

        protected void RadCalendar1_OnPreRender(object sender, EventArgs e)
        {
            foreach (CalendarView view in RadCalendar1.CalendarView.ChildViews)
            {
                ((MonthView)view).TitleFormat = @"MMM yyyy";
            }
        }

        protected void RadCalendar1_OnDayRender(object sender, DayRenderEventArgs e)
        {
            // Do a token replace..
            e.Cell.Text = e.Cell.Text.Replace("{DayOfMonth}", e.Day.Date.Day.ToString());
        }

        protected void RadCalendar1_OnLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var cSiteLocation = (new CSiteLocation()).Get(CurrentSiteLocationId);
                var holiday = (new CHoliday()).Get(cSiteLocation.Province);
                foreach (var h in holiday)
                {
                    var cal = new RadCalendarDay();
                    cal.Date = h.HolidayDate;
                    cal.IsSelectable = false;
                    cal.ToolTip = h.Name + (h.Abbreviation == string.Empty ? string.Empty : " (" + h.Abbreviation + ")") + " " + h.HolidayDate.Date.ToShortDateString();
                    cal.IsDisabled = true;
                    RadCalendar1.SpecialDays.Add(cal);
                }

                var vacationList = (new CVacation()).GetDetail(Convert.ToInt32(ViewState["UserId"]));
                foreach (var v in vacationList)
                {
                    // without rejected, canceled
                    if (v.ApprovalStatus != (int)CConstValue.ApprovalStatus.Rejected && v.ApprovalStatus != (int)CConstValue.ApprovalStatus.Canceled)
                    {
                        CVacationDetail vacationDetail = new CVacationDetail();
                        var vacationDetailList = vacationDetail.Get(v.VacationId);
                        foreach (VacationDetail vd in vacationDetailList)
                        {
                            var cal = new RadCalendarDay();
                            cal.Date = vd.Date;

                            var cVacationDict = new CDict();
                            var vacationDict = cVacationDict.GetDictByTypeAndValue(1376, v.VacationType);
                            var totalDays = vacationDetailList.Count();

                            var cRequestDict = new CDict();
                            var requestDict = cRequestDict.GetDictByTypeAndValue(217, (int)v.ApprovalStatus);

                            cal.ToolTip = vd.Date.ToShortDateString() + " " + vacationDict.Name + " (" + (vd.IsFullDay ? totalDays : totalDays - 0.5) + ") [" + requestDict.Name + "] No : " + v.VacationId;

                            switch (v.VacationType)
                            {
                                // Paid
                                case 0:
                                    if (vd.IsFullDay)
                                        cal.TemplateID = "DayTemplatePaidFullDay";
                                    else
                                        cal.TemplateID = "DayTemplatePaidHalfDay";
                                    break;

                                // Unpaid
                                case 1:
                                    if (vd.IsFullDay)
                                        cal.TemplateID = "DayTemplateUnPaidFullDay";
                                    else
                                        cal.TemplateID = "DayTemplateUnPaidHalfDay";
                                    break;

                                // SickDay
                                case 2:
                                    if (vd.IsFullDay)
                                        cal.TemplateID = "DayTemplateSickFullDay";
                                    else
                                        cal.TemplateID = "DayTemplateSickHalfDay";
                                    break;

                                // Entitlement
                                case 3:
                                    if (vd.IsFullDay)
                                        cal.TemplateID = "DayTemplateEntitlementFullDay";
                                    else
                                        cal.TemplateID = "DayTemplateEntitlementHalfDay";
                                    break;
                            }

                            RadCalendar1.SpecialDays.Add(cal);
                        }
                    }
                }

                // set this year jan 1
                RadCalendar1.FocusedDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
        }

        protected void RadCalendar1_OnSelectionChanged(object sender, SelectedDatesEventArgs e)
        {
            try
            {
                var gridType = 2;
                var approvalType = ((int)CConstValue.ApprovalStatus.Approved).ToString();
                var no = string.Empty;
                foreach (RadCalendarDay s in RadCalendar1.SpecialDays)
                {
                    if (s.Date == e.SelectedDates[0].Date)
                    {
                        var split = s.ToolTip.Split(new[] { " No : " }, StringSplitOptions.None);
                        no = split[1];
                        break;
                    }
                }
                if (no != string.Empty)
                {
                    RunClientScript("ShowNewPop('" + no + "', '1', '" + gridType + "', '" + approvalType + "');");
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }

            e.SelectedDates.Clear();
        }
    }
}