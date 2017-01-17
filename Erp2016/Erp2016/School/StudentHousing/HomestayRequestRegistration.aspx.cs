using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;

public partial class School_StudentHousing_HomestayRequestRegistration : PageBase
{
    public School_StudentHousing_HomestayRequestRegistration() : base((int)CConstValue.Menu.HomestayPlacementRequest)
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
            RadToolBar3.Items[2].Enabled = true;
            RadToolBar3.Items[4].Enabled = true;
            RadToolBar3.Items[6].Enabled = true;
            RadToolBar3.Items[8].Enabled = true;
            RadToolBar3.Items[10].Enabled = true;
            RadToolBar3.Items[12].Enabled = true;

            //Show Placement Request Detail
            ShowPlacementRequestDetail(Convert.ToInt32(Grid_StudentList.SelectedValue));
            PageViewVisible(true, true, true, true);
            GridHistory.Rebind();
            Grid_HomestayPlacement.Rebind();
            Grid_PlacementAgency.Rebind();

            Download.Visible = false;

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
                    Grid_HomestayPlacement.Rebind();
                    break;
                case 3:// Placement by Agency
                    PageViewVisible(false, false, false, true);
                    Grid_PlacementAgency.Rebind();
                    Download.Visible = false;

                    break;

            }
        }
    }
    protected void PageViewVisible(bool basic, bool request, bool placementSchool, bool placementAgency)
    {
        PageView_Basic.Visible = basic;
        PageView_History.Visible = request;
        PageView_Placement.Visible = placementSchool;
        PageView_PlacementAgency.Visible = placementAgency;


    }
    protected void ShowPlacementRequestDetail(int HomestayStudentId)
    {

        var cHomestayStudentBasic = new CHomestayStudentRequest();
        HomestayStudentBasic Student = cHomestayStudentBasic.GetHomestayStudentRequest(HomestayStudentId);
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

        //Preference
        ddlSmoking.SelectedValue = Student.Smoking.ToString();
        ddlInternet.SelectedValue = Student.Internet.ToString();
        ddlRoomType.SelectedValue = Student.RoomType.ToString();
        ddlMealType.SelectedValue = Student.MealType.ToString();
        ddlGuardian.SelectedValue = Student.GuardianRequired.ToString();
        ddlPet.SelectedValue = Student.Pet.ToString();
        ddlChildren.SelectedValue = Student.Children.ToString();
        txtMedication.Text = Student.Medication;
        txtDiet.Text = Student.Diet;

        //General Information
        DatePickerStartDate.SelectedDate = Student.StartDate;
        DatePickerEndDate.SelectedDate = Student.EndDate;

        ddlExtention.SelectedValue = Student.ExtensionFlag.ToString();

        int Weeks = Convert.ToInt32(Student.HomestayWeeks);
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
        ddlAllergy.SelectedValue = Student.AllergyType.ToString();
        txtAllergyType.Text = Student.AllergyNote;
        txtComments.Text = Student.Comments;
        //File Upload
        file_upload.GetFileDownload(Convert.ToInt32(HomestayStudentId));
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


        if (e.Item.Index == 0) // New Homestay Placement Request
        {
            RunClientScript("NewHomestayNewWindow(0,0,0)");

        }
        if (Convert.ToInt32(Grid_StudentList.SelectedValue) > 0)
        {
            int HomestayStudentId = 0;
            HomestayStudentId = Convert.ToInt32(Grid_StudentList.SelectedValue);

            if (HomestayStudentId > 0)
            {

                switch (e.Item.Index)
                {
                    
                    case 2: //Request
                        //ConfirmRequest(int HomestayStudentId)
                        RunClientScript("NewHomestayNewWindow(" + HomestayStudentId + ",0,0);");
                        break;
                    case 4:
                        //RunClientScript("NewHomestayNewWindow(" + HomestayStudentId + ",0,0);");// Modify Homestay Request
                        // Placement by School
                        RunClientScript("PlacementbySchoolNewWindow(" + HomestayStudentId + ");");
                        break;
                    case 6:
                        // Placement by Agency
                        RunClientScript("PlacementbyAgencyNewWindow(" + HomestayStudentId + ");");
                        break;

                    case 8:
                        // Shcedule Change

                        RunClientScript("NewHomestayNewWindow(" + HomestayStudentId + ",0,1);");

                        break;

                    case 10:
                        //Cancel Request 

                        RunClientScript("CancelHomestayNewWindow(" + HomestayStudentId + ");");
                        break;

                    case 12:
                        // Invoice/Payment
                        RunClientScript("PaymentNewWindow(" + HomestayStudentId + ");");
                        break;


                }
            }
        }
    }


    protected void ConfirmRequest(int HomestayStudentId)
    {

      
        var cHomestayStudentRequest = new CHomestayStudentRequest();
        //Generate Invoice if there is no invoice number in invoice table

        //Status=1 as Requested

    }
    protected void Grid_StudentList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        var cHomestayStudent = new CHomestayStudentRequest();

        Grid_StudentList.DataSource = cHomestayStudent.GetHomestayStudentList(CurrentUserId);//CurrentSiteLocationId

    }
    protected void GridHistory_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem DataItem = e.Item as GridDataItem;
            DataBound(DataItem);


        }

    }

    protected void Grid_HomestayPlacement_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        if (Grid_StudentList.SelectedValue != null)
        {

            var cHomestayPlament = new CHomestayPlacement();
            Grid_HomestayPlacement.DataSource = cHomestayPlament.GetHomestayPlacementByRequestId(Convert.ToInt32(Grid_StudentList.SelectedValue));
            Grid_HomestayPlacement.Visible = true;
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

        //HB.ExtensionFlag, HB.MealType, 
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
        //HB.GuardianRequired, 
        string GuardianRequired = "";
        switch (DataItem["GuardianRequired"].Text.Trim().ToString())
        {
            case "0":
                GuardianRequired = "-";
                break;
            case "1":
                GuardianRequired = "No";
                break;
            case "2":
                GuardianRequired = "Yes";
                break;

        }
        DataItem["GuardianRequired"].Text = GuardianRequired;
        //HB.Internet, 
        string Internet = "";
        switch (DataItem["Internet"].Text.Trim().ToString())
        {
            case "0":
                Internet = "-";
                break;
            case "1":
                Internet = "No";
                break;
            case "2":
                Internet = "Yes";
                break;

        }
        DataItem["Internet"].Text = Internet;
        //HB.HomestayStudentStatus,
        string HomestayStudentStatus = "";
        switch (DataItem["HomestayStudentStatus"].Text.Trim().ToString())
        {
            case "0":
                HomestayStudentStatus = "Pending";
                break;
            case "1":
                HomestayStudentStatus = "Requested";
                break;
            case "2":
                HomestayStudentStatus = "Placed by School";
                break;
            case "3":
                HomestayStudentStatus = "Placed by Agency";
                break;
            case "6":
                HomestayStudentStatus = "Cacelled by Agency";
                break;
            case "7":
                HomestayStudentStatus = "Rejected by Agency";
                break;
            case "4":
                HomestayStudentStatus = "Canceled";
                break;
            case "5":
                HomestayStudentStatus = "Schedule Change";
                break;
        }
        DataItem["HomestayStudentStatus"].Text = HomestayStudentStatus;
        //HB.MealType
        string MealType = "";
        switch (DataItem["MealType"].Text.Trim().ToString())
        {
            case "0":
                MealType = "-";
                break;
            case "1":
                MealType = "Half Meal";
                break;
            case "2":
                MealType = "Full Meal";
                break;

        }
        DataItem["MealType"].Text = MealType;
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
            int HomestayStudentId = Convert.ToInt32(Grid_StudentList.SelectedValue);

            var cHomestayStudent = new CHomestayStudentRequest();
            HomestayStudentBasic Student = cHomestayStudent.GetHomestayStudentRequest(HomestayStudentId);
            StudentId = Convert.ToInt32(Student.StudentId);
            if (StudentId > 0)
            {
                GridHistory.DataSource = cHomestayStudent.GetHomestayStudentHistoryList(CurrentSiteLocationId, StudentId, HomestayStudentId);

            }

        }

    }



    protected void Grid_HomestayPlacement_ItemDataBound(object sender, GridItemEventArgs e)

    {
        if (e.Item is GridDataItem)
        {

            GridDataItem DataItem = e.Item as GridDataItem;



            if (DataItem["PlacementType"].Text == "1")
            {
                DataItem["PlacementType"].Text = "Placed By School";
            }

            DataItem["PlacementStatus"].Text = PlaceStatus(DataItem["PlacementStatus"].Text);

            DataItem["HostRoomFloor"].Text = FloorName(Convert.ToInt32(DataItem["HostRoomFloor"].Text));

            if (DataItem["HostRoomType"].Text == "True")
            {
                DataItem["HostRoomType"].Text = "Private Room";
            }
            else
            {
                DataItem["HostRoomType"].Text = "Shared Room";
            }
        }




    }
    protected string PlaceStatus(string Status)
    {
        string HomestayStatus = "";

        switch (Status)
        {
            case "0":
                HomestayStatus = "Canceled";
                break;
            case "1":
                HomestayStatus = "Placed";
                break;
            case "2":
                HomestayStatus = "Schedule Changed";
                break;
            case "3":
                HomestayStatus = "Rejected";
                break;

        }
        return HomestayStatus;
    }

    protected void Grid_PlacementAgency_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        if (Grid_StudentList.SelectedValue != null)
        {
            var cPlacement = new CHomestayPlacement();
            Grid_PlacementAgency.DataSource = cPlacement.GetHomestayPlacementByAgency(Convert.ToInt32(Grid_StudentList.SelectedValue));
            Grid_PlacementAgency.Visible = true;


        }
    }


    protected void Grid_PlacementAgency_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {

            GridDataItem DataItem = (GridDataItem)e.Item;
            if (DataItem["PlacementType"].Text == "2")
            {
                DataItem["PlacementType"].Text = "Placed By Agency";
            }
            DataItem["PlacementStatus"].Text = PlaceStatus(DataItem["PlacementStatus"].Text);


        }
    }

    protected void Grid_PlacementAgency_ItemCommand(object sender, GridCommandEventArgs e)
    {

        GridDataItem item = e.Item as GridDataItem;
        // Download File
        Download.InitFileDownloadList((int)CConstValue.Upload.HomestayAgency);
        Download.SetVisibieUploadControls(false);
        Download.GetFileDownload(Convert.ToInt32(Grid_StudentList.SelectedValue));
        Download.Visible = true;

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

  

    protected void ButtonGridRefresh_OnClick(object sender, EventArgs e)
    {
        Grid_StudentList.Rebind();
    }

    protected void TimePickerArrivalTime_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {

    }

    protected void DatePickerArrivalDate_SelectedDateChange(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        
    }

    protected void Grid_StudentList_OnPreRender(object sender, EventArgs e)
    {

    }
    protected void Grid_StudentList_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
    {
        SetFilterCheckListItems(e);
    }
}