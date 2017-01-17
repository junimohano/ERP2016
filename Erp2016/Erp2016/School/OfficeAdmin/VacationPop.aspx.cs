using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;
using Telerik.Web.UI.Calendar.View;
using DayRenderEventArgs = Telerik.Web.UI.Calendar.DayRenderEventArgs;

namespace School.OfficeAdmin
{
    public partial class VacationPop : PageBase
    {
        private int Id { get; set; }

        public VacationPop() : base((int)CConstValue.Menu.Vacation)
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
                var requestOrApprovalType = Request["requestOrApprovalType"];
                var approvalType = Request["approvalType"];

                var buttonList = new List<string>();

                // new
                if (Request["createOrListType"] == "0")
                {
                    obj = obj.GetNewDocument(CurrentUserId);

                    //buttonList.Add("TempSave");
                    buttonList.Add("Request");
                    buttonList.Add("Close");

                    SetVisibleItems(true);
                }
                // select
                else
                {
                    // date
                    obj = new CVacation(Id);

                    // request list
                    if (requestOrApprovalType == "0")
                    {
                        // Revise
                        if (approvalType == ((int)CConstValue.ApprovalStatus.Revise).ToString())
                        {
                            buttonList.Add("Request");
                            buttonList.Add("Cancel");
                            buttonList.Add("Close");

                            SetVisibleItems(true);
                        }
                        // TempSave
                        else if (approvalType == string.Empty)
                        {
                            buttonList.Add("Request");
                            buttonList.Add("Cancel");
                            buttonList.Add("Close");

                            SetVisibleItems(true);
                        }
                        // Request
                        else if (approvalType == "1")
                        {
                            buttonList.Add("Cancel");
                            buttonList.Add("Close");

                            SetVisibleItems(false);
                        }
                        else
                        {
                            buttonList.Add("Close");

                            SetVisibleItems(false);
                        }
                    }
                    // approval
                    else if (requestOrApprovalType == "1")
                    {
                        // approved or rejected
                        if (approvalType == ((int)CConstValue.ApprovalStatus.Approved).ToString())
                        {
                            // permission
                            if (CurrentGroupId == (int)CConstValue.UserGroupForVacation.IT ||
                                CurrentGroupId == (int)CConstValue.UserGroupForVacation.HR)
                            {
                                buttonList.Add("Cancel");
                            }

                            buttonList.Add("Close");
                        }
                        else if (approvalType == ((int)CConstValue.ApprovalStatus.Rejected).ToString() ||
                                approvalType == ((int)CConstValue.ApprovalStatus.Canceled).ToString())
                        {
                            buttonList.Add("Close");
                        }
                        else
                        {
                            var refundApproveInfo = new CGlobal();
                            var supervisor = refundApproveInfo.CheckApprovalEnable((int)CConstValue.Approval.Vacation, Convert.ToInt32(Id));

                            if (CurrentUserId == supervisor)
                            {
                                buttonList.Add("Approve");
                                buttonList.Add("Reject");
                                buttonList.Add("Revise");
                                buttonList.Add("Close");
                            }
                            else
                            {
                                buttonList.Add("Close");
                            }
                        }

                        SetVisibleItems(false);
                    }
                    // Hire from HQ
                    else if (requestOrApprovalType == "2")
                    {
                        // permission
                        if (CurrentGroupId == (int)CConstValue.UserGroupForVacation.IT ||
                            CurrentGroupId == (int)CConstValue.UserGroupForVacation.HR)
                        {
                            buttonList.Add("Cancel");
                        }

                        buttonList.Add("Print");
                        buttonList.Add("Close");

                        SetVisibleItems(false);
                    }
                }

                foreach (RadToolBarItem item in RadToolBar1.Items)
                {
                    if (buttonList.Contains(item.Text))
                        item.Visible = true;
                    else
                        item.Visible = false;
                }

                var dt = new DataTable();
                dt.Columns.Add("DocNo");
                dt.Columns.Add("DateOfIssue");
                var newDr = dt.NewRow();
                newDr["DocNo"] = obj.DocNo;
                newDr["DateOfIssue"] = obj.DateOfIssue;
                dt.Rows.Add(newDr);
                RadGridInfo.DataSource = dt;

                // Get Data
                var vacationObj = obj.Get(Id);
                if (vacationObj != null)
                {
                    RadComboBoxVacationType.SelectedIndex = vacationObj.VacationType;

                    CVacationDetail vacationDetailClass = new CVacationDetail();

                    foreach (VacationDetail vd in vacationDetailClass.Get(vacationObj.VacationId))
                    {
                        RadComboBoxDayType.SelectedIndex = vd.IsFullDay ? 0 : 1;
                        RadCalendar1.SelectedDates.Add(new RadDate(vd.Date));
                    }

                    ViewState["SelectedDates"] = RadCalendar1.SelectedDates.ToArray();
                    RadTextBoxRemark.Text = vacationObj.Remark;

                    SetDataInformation();

                    // save for remain Days in load
                    ViewState["UserId"] = vacationObj.CreatedId;
                }
                else
                {
                    // save for remain Days in load
                    ViewState["UserId"] = CurrentUserId;
                }
            }
        }

        private void SetDataInformation()
        {
            DateTime startDate = GetStartDate();
            if (startDate == DateTime.MaxValue)
                RadDatePickerStartDate.SelectedDate = null;
            else
                RadDatePickerStartDate.SelectedDate = startDate;

            DateTime endDate = GetEndDate();
            if (endDate == DateTime.MinValue)
                RadDatePickerEndDate.SelectedDate = null;
            else
                RadDatePickerEndDate.SelectedDate = endDate;

            if (RadCalendar1.SelectedDates.Count > 0)
            {
                // full day
                if (Convert.ToInt32(RadComboBoxDayType.SelectedValue) == 1)
                    RadNumTextBoxDays.Text = RadCalendar1.SelectedDates.Count.ToString();
                // half day
                else
                {
                    // half day can't select more than one day so only can be 0.5
                    RadNumTextBoxDays.Text = 0.5.ToString();
                }
            }
            else
                RadNumTextBoxDays.Text = string.Empty;
        }

        private DateTime GetStartDate()
        {
            DateTime minDateTime = DateTime.MaxValue;
            foreach (RadDate radDate in RadCalendar1.SelectedDates)
            {
                if (minDateTime > radDate.Date)
                    minDateTime = radDate.Date;
            }
            return minDateTime;
        }

        private DateTime GetEndDate()
        {
            DateTime maxDateTime = DateTime.MinValue;
            foreach (RadDate radDate in RadCalendar1.SelectedDates)
            {
                if (maxDateTime < radDate.Date)
                    maxDateTime = radDate.Date;
            }
            return maxDateTime;
        }

        protected void RadGridInfo_Load(object sender, EventArgs e)
        {
        }

        private void SetVisibleItems(bool isActive)
        {
            if (isActive == false)
            {
                RadComboBoxVacationType.Enabled = false;
                RadComboBoxDayType.Enabled = false;

                ViewState["IsRadCalder"] = false;

                RadDatePickerStartDate.Enabled = false;
                RadDatePickerEndDate.Enabled = false;
                RadNumTextBoxDays.Enabled = false;
                RadTextBoxRemark.ReadOnly = true;
            }
            else
            {
                ViewState["IsRadCalder"] = true;
            }
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            // Save
            if (e.Item.Text == "TempSave" || e.Item.Text == "Request")
            {
                if (IsValid)
                {
                    var cObj = new CVacation();
                    var obj = cObj.Get(Id);

                    // new one
                    if (obj == null)
                    {
                        obj = new Erp2016.Lib.Vacation();
                        obj.CreatedId = Convert.ToInt32(CurrentUserId);
                        obj.CreatedDate = DateTime.Now;

                        int newIndex = Convert.ToInt32(cObj.Add(obj).ToString());
                        obj = cObj.Get(newIndex);
                        ViewState["NewIndex"] = newIndex;
                    }
                    else
                    {
                        obj.UpdatedId = Convert.ToInt32(CurrentUserId);
                        obj.UpdatedDate = DateTime.Now;
                        ViewState["NewIndex"] = obj.VacationId.ToString();
                    }

                    obj.ApprovalId = CurrentUserId;
                    obj.ApprovalDate = DateTime.Now;

                    if (e.Item.Text == "TempSave")
                        obj.ApprovalStatus = null;
                    else
                    {
                        var cApprovalHistory = new CApprovalHistory();
                        cApprovalHistory.DelApprovalHistory((int)CConstValue.Approval.Vacation, Convert.ToInt32(ViewState["NewIndex"]));

                        // approve request 
                        var approval = new CApproval();
                        var approvalResult = approval.ApproveRequstCreate((int)CConstValue.Approval.Vacation, CurrentUserId, Convert.ToInt32(ViewState["NewIndex"]));
                        if (approvalResult > 0)
                        {
                            obj.ApprovalStatus = approvalResult;
                        }
                        else
                        {
                            ShowMessage("Failed");
                            return;
                        }

                        new CMail().SendMail(CConstValue.Approval.Vacation, CConstValue.MailStatus.ToApproveUser, Convert.ToInt32(ViewState["NewIndex"]), string.Empty, CurrentUserId);
                    }

                    // data
                    obj.VacationType = Convert.ToInt32(RadComboBoxVacationType.SelectedValue);
                    obj.Remark = RadTextBoxRemark.Text;

                    cObj.Update(obj);

                    // delete
                    CVacationDetail vacationDetailClass = new CVacationDetail();
                    vacationDetailClass.DeleteItemsByVacationId(obj.VacationId);

                    // insert
                    foreach (RadDate radDate in RadCalendar1.SelectedDates)
                    {
                        vacationDetailClass.Add(new VacationDetail()
                        {
                            CreatedId = CurrentUserId,
                            CreatedDate = DateTime.Now,
                            VacationId = obj.VacationId,
                            IsFullDay = RadComboBoxDayType.SelectedValue == "1" ? true : false,
                            Date = radDate.Date
                        });
                    }

                    // save other tables
                    RunClientScript("Close();");
                }
                else
                    ShowMessage("Failed");
            }
            // Revise
            else if (e.Item.Text == "Revise")
            {
                RunClientScript("ShowApprovalReviseWindow('" + Id + "');");
            }
            // Approval
            else if (e.Item.Text == "Approve")
            {
                RunClientScript("ShowApprovalWindow('" + Id + "');");
            }
            // Reject
            else if (e.Item.Text == "Reject")
            {
                RunClientScript("ShowApprovalRejectWindow('" + Id + "');");
            }
            // Print
            else if (e.Item.Text == "Print")
            {
                RunClientScript("ShowPrint();");
            }
            // Cancel
            else if (e.Item.Text == "Cancel")
            {
                RunClientScript("ShowApprovalCancelWindow('" + Id + "');");
            }
            // close
            else if (e.Item.Text == "Close")
            {
                RunClientScript("Close();");
            }
        }

        protected void RadCalendar1_OnSelectionChanged(object sender, SelectedDatesEventArgs e)
        {
            if ((bool)ViewState["IsRadCalder"] == false)
            {
                RadCalendar1.SelectedDates.Clear();
                var selectedDateList = (DateTime[])ViewState["SelectedDates"];
                foreach (var d in selectedDateList)
                {
                    RadCalendar1.SelectedDates.Add(new RadDate(d));
                }
                return;
            }

            // if half day
            if (Convert.ToInt32(RadComboBoxDayType.SelectedValue) == 0)
            {
                if (RadCalendar1.SelectedDates.Count > 0)
                {
                    // only choose one day
                    DateTime halfDay = RadCalendar1.SelectedDates[RadCalendar1.SelectedDates.Count - 1].Date;
                    RadCalendar1.SelectedDates.Clear();
                    RadCalendar1.SelectedDates.Add(new RadDate(halfDay));
                }
            }

            CVacationTypeInfoModel vacationTypeInfoModel = (CVacationTypeInfoModel)ViewState["VacationTypeInfoModel"];

            double selectedThisYearUsedDays = 0;
            double selectedNextYearUsedDays = 0;

            double thisYearTotalDays = 0;
            double thisYearUsedDays = 0;
            double nextYearTotalDays = 0;
            double nextYearUsedDays = 0;

            DateTime compareDate = DateTime.Now;

            switch (Convert.ToInt32(RadComboBoxVacationType.SelectedValue))
            {
                // paid
                case (int)CConstValue.VacationType.PaidVacationDay:
                    compareDate = vacationTypeInfoModel.ThisYear.PaidDate;

                    thisYearTotalDays = vacationTypeInfoModel.ThisYear.PaidTotalDays;
                    thisYearUsedDays = vacationTypeInfoModel.ThisYear.PaidUsedDays;

                    nextYearTotalDays = vacationTypeInfoModel.NextYear.PaidTotalDays;
                    nextYearUsedDays = vacationTypeInfoModel.NextYear.PaidUsedDays;
                    break;

                // unpaid
                case (int)CConstValue.VacationType.UnPaidVacationDay:
                    // nothing
                    break;

                // sick
                case (int)CConstValue.VacationType.SickDay:
                    compareDate = vacationTypeInfoModel.ThisYear.SickDate;

                    thisYearTotalDays = vacationTypeInfoModel.ThisYear.SickTotalDays;
                    thisYearUsedDays = vacationTypeInfoModel.ThisYear.SickUsedDays;

                    nextYearTotalDays = vacationTypeInfoModel.NextYear.SickTotalDays;
                    nextYearUsedDays = vacationTypeInfoModel.NextYear.SickUsedDays;
                    break;

                // entitlement
                case (int)CConstValue.VacationType.EntitlementDay:
                    compareDate = vacationTypeInfoModel.ThisYear.EntitlementDate;

                    thisYearTotalDays = vacationTypeInfoModel.ThisYear.EntitlementTotalDays;
                    thisYearUsedDays = vacationTypeInfoModel.ThisYear.EntitlementUsedDays;

                    nextYearTotalDays = vacationTypeInfoModel.NextYear.EntitlementTotalDays;
                    nextYearUsedDays = vacationTypeInfoModel.NextYear.EntitlementUsedDays;
                    break;
            }

            // without unpaid
            if (Convert.ToInt32(RadComboBoxVacationType.SelectedValue) != 1)
            {
                foreach (RadDate radDate in RadCalendar1.SelectedDates)
                {
                    // next year
                    if (radDate.Date > compareDate)
                    {
                        // full day
                        if (Convert.ToInt32(RadComboBoxDayType.SelectedValue) == 1)
                            selectedNextYearUsedDays++;
                        else
                            selectedNextYearUsedDays += 0.5;
                    }
                    // this year
                    else
                    {
                        // full day
                        if (Convert.ToInt32(RadComboBoxDayType.SelectedValue) == 1)
                            selectedThisYearUsedDays++;
                        else
                            selectedThisYearUsedDays += 0.5;
                    }
                }

                // compare used date
                if ((selectedThisYearUsedDays + thisYearUsedDays) <= thisYearTotalDays && (selectedNextYearUsedDays + nextYearUsedDays) <= nextYearTotalDays)
                {
                    // success
                }
                else
                {
                    ShowMessage("Not available days");

                    RadCalendar1.SelectedDates.Clear();
                    var selectedDateList = (DateTime[])ViewState["SelectedDates"];
                    if (selectedDateList != null)
                    {
                        foreach (var d in selectedDateList)
                        {
                            RadCalendar1.SelectedDates.Add(new RadDate(d));
                        }
                    }
                    return;
                }
            }

            SetDataInformation();
            // save selectedDates
            ViewState["SelectedDates"] = RadCalendar1.SelectedDates.ToArray();
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
                            // except selectedDays to speaciaDays for selecting
                            if (RadCalendar1.SelectedDates.ToArray().Any(x => x.Date == vd.Date))
                                continue;

                            var cal = new RadCalendarDay();
                            cal.Date = vd.Date;
                            cal.IsSelectable = false;

                            var vacationDict = (new CDict()).GetDictByTypeAndValue(1376, v.VacationType);
                            var totalDays = vacationDetailList.Count();

                            var requestDict = (new CDict()).GetDictByTypeAndValue(217, (int)v.ApprovalStatus);

                            cal.ToolTip = vd.Date.ToShortDateString() + " " + vacationDict.Name + " (" + (vd.IsFullDay ? totalDays : totalDays - 0.5) + ") [" + requestDict.Name + "] No : " + v.VacationId;

                            switch (v.VacationType)
                            {
                                case (int)CConstValue.VacationType.PaidVacationDay:
                                    if (vd.IsFullDay)
                                        cal.TemplateID = "DayTemplatePaidFullDay";
                                    else
                                        cal.TemplateID = "DayTemplatePaidHalfDay";
                                    break;

                                case (int)CConstValue.VacationType.UnPaidVacationDay:
                                    if (vd.IsFullDay)
                                        cal.TemplateID = "DayTemplateUnPaidFullDay";
                                    else
                                        cal.TemplateID = "DayTemplateUnPaidHalfDay";
                                    break;

                                case (int)CConstValue.VacationType.SickDay:
                                    if (vd.IsFullDay)
                                        cal.TemplateID = "DayTemplateSickFullDay";
                                    else
                                        cal.TemplateID = "DayTemplateSickHalfDay";
                                    break;

                                case (int)CConstValue.VacationType.EntitlementDay:
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

                // select current date
                if (RadCalendar1.SelectedDates.Count > 0)
                    RadCalendar1.FocusedDate = RadCalendar1.SelectedDates[0].Date;

            }
        }

        protected void RadComboBoxDayType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadCalendar1.SelectedDates.Clear();
            RadDatePickerStartDate.SelectedDate = null;
            RadDatePickerEndDate.SelectedDate = null;
            RadNumTextBoxDays.Text = null;
            ViewState["SelectedDates"] = null;
        }

        protected void RadComboBoxVacationType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadCalendar1.SelectedDates.Clear();
            RadDatePickerStartDate.SelectedDate = null;
            RadDatePickerEndDate.SelectedDate = null;
            RadNumTextBoxDays.Text = null;
            ViewState["SelectedDates"] = null;
        }

        protected void RadGridDays_Load(object sender, EventArgs e)
        {
            var cVacationSchema = new CVacationSchema();
            // get Data
            CVacationTypeInfoModel vacationTypeInfoModel = cVacationSchema.GetVacationTypeInfoModel(Convert.ToInt32(ViewState["UserId"]));
            ViewState["VacationTypeInfoModel"] = vacationTypeInfoModel;
            ((RadGrid)sender).DataSource = cVacationSchema.Get(vacationTypeInfoModel);
        }

        protected void ApprovalLine1_OnLoad(object sender, EventArgs e)
        {
            LinqDataSource LinqDataSourceApprovalList = ApprovalLine1.GetSqlDataSourceApprovalList();
            if (LinqDataSourceApprovalList != null)
            {
                LinqDataSourceApprovalList.WhereParameters.Clear();
                LinqDataSourceApprovalList.WhereParameters.Add("MenuSeqId", DbType.Int32, Id.ToString());
                LinqDataSourceApprovalList.WhereParameters.Add("ApproveType", DbType.Int32, ((int)CConstValue.Approval.Vacation).ToString());
                LinqDataSourceApprovalList.Where = "MenuSeqId == @MenuSeqId AND ApproveType == @ApproveType";
            }
        }
    }
}