using System;
using Erp2016.Lib;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;

namespace School.Systems
{
    public partial class Holiday : PageBase
    {
        public Holiday() : base((int)CConstValue.Menu.Holiday)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void RadGridHoliday_PreRender(object sender, EventArgs e)
        {

        }

        protected void RadGridHoliday_BatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {
            foreach (var command in e.Commands)
            {
                if (command.Type.ToString() != "Delete")
                {
                    command.NewValues["IsActive"] = command.NewValues["IsActive"].ToString() == "1" ? false : true;

                    DateTime Dob = Convert.ToDateTime(command.NewValues["HolidayDate"]);

                    var Years = Dob.Year;

                    command.NewValues["Year"] = Years;

                    if (command.Type.ToString() == "Insert")
                    {
                        command.NewValues["CreatedDate"] = DateTime.Now;
                        command.NewValues["CreatedId"] = CurrentUserId;
                    }
                    else if (command.Type.ToString() == "Update")
                    {
                        command.NewValues["UpdatedDate"] = DateTime.Now;
                        command.NewValues["UpdatedId"] = CurrentUserId;
                    }
                }
            }
        }

        protected void RadGridHoliday_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        protected void txtHolidayDate_SelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {

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
            }
        }
        
        public override void SetVisibleModifyControllers()
        {
            if (UserPermissionModel.IsModify == false)
            {
                var delete = RadGridHoliday.MasterTableView.GetColumn("DeleteColumn");
                delete.Visible = false;
                RadGridHoliday.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                RadGridHoliday.MasterTableView.EditMode = GridEditMode.InPlace;
            }
        }

        protected void RadGridHoliday_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }
    }
}