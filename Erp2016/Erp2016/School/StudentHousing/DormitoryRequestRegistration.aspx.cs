using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;

public partial class School_StudentHousing_DormitoryRequestRegistration : PageBase
{
    public School_StudentHousing_DormitoryRequestRegistration() : base((int)CConstValue.Menu.DormitoryRequestRegistration)
    { }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            file_upload.InitFileDownloadList((int)CConstValue.Upload.Homestay);
            file_upload.SetVisibieUploadControls(false);

            var UserId = (new CUser()).Get(CurrentUserId);
            var SiteId = (new CSite()).Get(CurrentSiteId);

            var fname = UserId.FirstName;
            var lname = UserId.LastName;

        }
    }

    protected void Grid_StudentList_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (Grid_StudentList.SelectedValue != null)
        {
            int OriginalId = Convert.ToInt32(Grid_StudentList.SelectedValue);
            RadToolBar3.Items[2].Enabled = true; //Placement
            RadToolBar3.Items[4].Enabled = true; //Shcedule Change

            RadToolBar3.Items[6].Enabled = true; //Cancel 
            RadToolBar3.Items[8].Enabled = true; // Invoice and Payment

            //Show Placement Request Detail
            ShowPlacementRequestDetail(Convert.ToInt32(Grid_StudentList.SelectedValue));
            PageViewVisible(true, true, true, true);
            GridHistory.Rebind();
            Grid_DormitoryPlacement.Rebind();

        }

    }
    protected void Tab_Host_TabClick(object sender, RadTabStripEventArgs e)
    {

        if (Grid_StudentList.SelectedValue != null)
        {
            switch (Convert.ToInt32(Tab_Host.SelectedIndex))
            {
                case 0: // Request Detail
                    PageViewVisible(true, false, false, false);
                    break;
                case 1: //Request History
                    PageViewVisible(false, true, false, false);
                    GridHistory.Rebind();
                    break;
                case 2: // Placement by School 
                    PageViewVisible(false, false, true, false);
                    Grid_DormitoryPlacement.Rebind();
                    break;
                case 3:// Placement by Agency
                    PageViewVisible(false, false, false, true);
                    //Grid_PlacementAgency.Rebind();
                    //Download.Visible = false;

                    break;

            }
        }
    }
    protected void PageViewVisible(bool basic, bool request, bool placementSchool, bool placementAgency)
    {
        PageView_Basic.Visible = basic;
        PageView_History.Visible = request;
        PageView_Placement.Visible = placementSchool;
        //PageView_PlacementAgency.Visible = placementAgency;


    }
    protected void ButtonGridRefresh_OnClick(object sender, EventArgs e)
    {
        Grid_StudentList.Rebind();
    }
    protected void ShowPlacementRequestDetail(int DormitoryRegistrationId)
    {

        var cHomestayStudentBasic = new CDormitoryRegistrations();
        DormitoryRegistration Student = cHomestayStudentBasic.GetDormitoryStudentRequest(DormitoryRegistrationId);
        //Airpot Information

        ddl_pickup.SelectedValue = Student.PickUp.ToString();
        DatePickArrivalDate.SelectedDate = Student.ArrivalDate;
        TimePickerArrivalTime.SelectedTime = Student.ArriveTime;
        txtArrivalAirLine.Text = Student.ArrivalAirLine;
        txtArrivalFlilghtNo.Text = Student.ArrivalFlightNo;
        txtDepartingFrom.Text = Student.DepartingFrom;
        ddlDropoff.SelectedValue = Student.DropOff.ToString();
        DatePicker_DepartureDate.SelectedDate = Student.DepartureDate;
        TimePickerDepartureTime.SelectedTime = Student.DepartureTime;
        txtDepartureAirline.Text = Student.DepartureAirLine;
        txtDepartureFlightNo.Text = Student.DepartureFlightNo;
        txtLeavingFrom.Text = Student.LeavingFrom;



        //General Information
        DatePickerStartDate.SelectedDate = Student.StartDate;
        DatePickerEndDate.SelectedDate = Student.EndDate;

        ddlExtention.SelectedValue = Student.ExtensionFlag.ToString();
        int Weeks = Convert.ToInt32(Student.DormitoryWeeks);
        int ExtraDay = Convert.ToInt32(Student.ExtraDays);

        if (Weeks == 0)
        {
            TxtHomestayWeeks.Text = "";
        }
        else
        {
            TxtHomestayWeeks.Text = Weeks.ToString();
        }

        if (ExtraDay == 0)
        {
            TxtExtraDays.Text = "";
        }
        else
        {
            TxtExtraDays.Text = ExtraDay.ToString();
        }
        if (ExtraDay == 7)
        {
            Weeks = Weeks + 1;
            TxtHomestayWeeks.Text = Weeks.ToString();
            TxtExtraDays.Text = "0";
        }
        ddlUrgent.SelectedValue = Student.UrgentRequest.ToString();

        txtComments.Text = Student.Comments;
        //File Upload
        file_upload.GetFileDownload(Convert.ToInt32(DormitoryRegistrationId));
        Dropdown_StudentlList(Convert.ToInt32(Student.StudentId)); // Load Student List

    }
    protected void Dropdown_StudentlList(int StudentId)
    {
        ddlStudent.Items.Clear();

        var cglobal = new CGlobal();
        int SiteLocationId = CurrentSiteLocationId;
        if (StudentId > 0)
        {
            var cStudentRequest = new CDormitoryRegistrations();
            SiteLocationId = cStudentRequest.SiteLocationbyStudentId(StudentId);

        }
        ddlStudent.DataSource = cglobal.LoadStudentList(SiteLocationId);//CurrentSiteLocationId
        ddlStudent.DataTextField = "Name";
        ddlStudent.DataValueField = "Value";
        if (StudentId == 0)
        {
            ddlStudent.SelectedIndex = 0;
        }
        else

        {
            ddlStudent.SelectedValue = StudentId.ToString();
        }

        ddlStudent.DataBind();
    }
    protected void Grid_StudentList_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem DataItem = e.Item as GridDataItem;
                DataBound(DataItem);
            }
        }
    }

    protected void RadToolBar3_OnButtonClick(object sender, RadToolBarEventArgs e)
    {


        if (e.Item.Index == 0) // New Dormitory Placement Request
        {
            RunClientScript("NewDormitoryNewWindow(0,0,0)");

        }
        if (Convert.ToInt32(Grid_StudentList.SelectedValue) > 0)
        {
            int DormitoryRegistrationId = 0;
            DormitoryRegistrationId = Convert.ToInt32(Grid_StudentList.SelectedValue);

            if (DormitoryRegistrationId > 0)
            {


                switch (e.Item.Index)
                {
                    case 2://Placement

                        //RunClientScript("NewDormitoryNewWindow(" + DormitoryRegistrationId + ",0,0);");  // Modify Homestay Request
                        RunClientScript("PlacementbySchoolNewWindow(" + DormitoryRegistrationId + ");");
                        break;

                    case 4: //Shcedule Change
                        RunClientScript("NewDormitoryNewWindow(" + DormitoryRegistrationId + ",0,1);");
                        break;

                    case 6: //Cancel 
                        RunClientScript("CancelDormitoryNewWindow(" + DormitoryRegistrationId + ");");
                        break;

                    case 8: //Invoice and Payment
                        RunClientScript("PaymentNewWindow(" + DormitoryRegistrationId + ");");
                        break;


                }
            }
        }
    }

    protected void Grid_StudentList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        var cDormitoryStudent = new CDormitoryRegistrations();

        Grid_StudentList.DataSource = cDormitoryStudent.GetDormitoryStudentList(CurrentSiteLocationId);

    }
    protected void GridHistory_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem DataItem = e.Item as GridDataItem;
            DataBound(DataItem);


        }

    }

    protected void Grid_DormitoryPlacement_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        if (Grid_StudentList.SelectedValue != null)
        {

            var cDormitoryPlacement = new CDormitoryPlacement();
            Grid_DormitoryPlacement.DataSource = cDormitoryPlacement.GetDormitoryPlacementByRequestId(Convert.ToInt32(Grid_StudentList.SelectedValue));
            Grid_DormitoryPlacement.Visible = true;
        }

    }

    protected void DataBound(GridDataItem DataItem)
    {
        //  GridDataItem DataItem = e.Item as GridDataItem;

        // int HomestayStudentId = Convert.ToInt32(DataItem["HomestayStudentId"].Text.ToString().Trim());

        //HB.UrgentRequest AS UrgentFlag, 
        string UgentFlag = "";
        switch (DataItem["UrgentFlag"].Text.Trim().ToString())
        {
            case "0":
                UgentFlag = "-";
                break;
            case "1":
                UgentFlag = "No";
                break;
            case "2":
                UgentFlag = "Yes";
                break;
        }
        DataItem["UrgentFlag"].Text = UgentFlag;


        string ExtensionFlag = "";
        switch (DataItem["ExtensionFlag"].Text.Trim().ToString())
        {
            case "0":
                ExtensionFlag = "-";
                break;
            case "1":
                ExtensionFlag = "No";
                break;
            case "2":
                ExtensionFlag = "Yes";
                break;
        }
        DataItem["ExtensionFlag"].Text = ExtensionFlag;
        //HB.PickUp, 
        string PickUp = "";
        switch (DataItem["PickUp"].Text.Trim().ToString())
        {
            case "0":
                PickUp = "-";
                break;
            case "1":
                PickUp = "No";
                break;
            case "2":
                PickUp = "Yes";
                break;

        }
        DataItem["PickUp"].Text = PickUp;
        //HB.DropOff,
        string DropOff = "";
        switch (DataItem["DropOff"].Text.Trim().ToString())
        {
            case "0":
                DropOff = "-";
                break;
            case "1":
                DropOff = "No";
                break;
            case "2":
                DropOff = "Yes";
                break;

        }
        DataItem["DropOff"].Text = DropOff;


        //HB.StudentStatus,
        string HomestayStudentStatus = "";
        switch (DataItem["DormitoryStudentStatus"].Text.Trim().ToString())
        {
            case "0":
                HomestayStudentStatus = "Pending";
                break;

            case "1":
                HomestayStudentStatus = "Requested";
                break;
            case "2":
                HomestayStudentStatus = "Placed";
                break;

            case "4":
                HomestayStudentStatus = "Canceled";
                break;
            case "5":
                HomestayStudentStatus = "Schedule Change";
                break;
        }
        DataItem["DormitoryStudentStatus"].Text = HomestayStudentStatus;

        //HB.PlacedUserId
        string PlacedByName = "-";
        string userid = DataItem["PlacedUserId"].Text.Trim().ToString();
        if (Convert.ToInt32(userid) > 0)
        {
            var cUser = new CUser();

            User user = cUser.Get(Convert.ToInt32(userid));
            PlacedByName = cUser.GetUserName(user);
        }

        DataItem["PlacedUserId"].Text = PlacedByName;


    }
    protected string FloorName(int RoomFloor)
    {
        string FloorName = string.Empty;
        switch (RoomFloor)
        {
            case 1:
                FloorName = "First Floor";
                break;
            case 2:
                FloorName = "Second Floor";
                break;
            case 3:
                FloorName = "Third Floor";
                break;
            case 4:
                FloorName = "Other Floor";
                break;
            case 5:
                FloorName = "Basement";
                break;

        }
        return FloorName;
    }

    protected void GridHistory_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        if (Grid_StudentList.SelectedValue != null)
        {
            int userid = CurrentUserId;
            int StudentId = 0;
            int DormitoryRegistrationId = Convert.ToInt32(Grid_StudentList.SelectedValue);

            var cDormitoryStudent = new CDormitoryRegistrations();
            DormitoryRegistration Student = cDormitoryStudent.GetDormitoryStudentRequest(DormitoryRegistrationId);
            StudentId = Convert.ToInt32(Student.StudentId);
            if (StudentId > 0)
            {
                GridHistory.DataSource = cDormitoryStudent.GetDormitoryStudentHistoryList(CurrentSiteLocationId, StudentId, DormitoryRegistrationId);

            }

        }

    }



    protected void Grid_DormitoryPlacement_ItemDataBound(object sender, GridItemEventArgs e)

    {
        if (e.Item is GridDataItem)
        {

            GridDataItem DataItem = e.Item as GridDataItem;
            DataItem["PlacementStatus"].Text = PlaceStatus(DataItem["PlacementStatus"].Text);
            // DataItem["HostRoomFloor"].Text = FloorName(Convert.ToInt32(DataItem["HostRoomFloor"].Text));
        }




    }
    protected string PlaceStatus(string Status)
    {
        string DormitoryStatus = "";
        if (Status == "1") ////  Placed=1, Canceled =0, Schedule Changed=2
        {
            DormitoryStatus = "Placed";
        }
        else if (Status == "0")
        {
            DormitoryStatus = "Canceled";
        }
        else if (Status == "2")
        {
            DormitoryStatus = "Schedule Changed";
        }
        return DormitoryStatus;
    }

    protected void Grid_PlacementAgency_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        if (Grid_StudentList.SelectedValue != null)
        {

        }
    }




    protected void DatePickerEndDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        //Auto Calculate Weeks Number and Extra Day
        DateTime StartDate = Convert.ToDateTime(DatePickerStartDate.SelectedDate);
        DateTime EndDate = Convert.ToDateTime(DatePickerEndDate.SelectedDate);
        int days = Convert.ToInt32((EndDate - StartDate).TotalDays);
        int weeks = Convert.ToInt32(days / 7);
        int ExtraDays = days - weeks * 7 + 1;

        if (ExtraDays == 7)
        {
            weeks = weeks + 1;
            ExtraDays = 0;
        }

        TxtHomestayWeeks.Text = weeks.ToString();
        TxtExtraDays.Text = ExtraDays.ToString();
    }
}