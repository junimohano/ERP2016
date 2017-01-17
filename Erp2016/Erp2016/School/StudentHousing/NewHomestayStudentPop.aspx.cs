using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;
using System.Data;


public partial class School_Registrar_Student_NewHomesayStudentPop : PageBase
{
    private RadGrid _radGridInvoiceItems;

    public int Id { get; set; }
    public int StudentIdFromStudentRegistrar; // redirect From StudentInformation Page
    public int ScheduleChange;
    protected void Page_Load(object sender, EventArgs e)
    {

        // find Invoice user control
        _radGridInvoiceItems = InvoiceItemGrid1.GetRadGridInvoiceItems();
        // connect event of invoice Items.
        _radGridInvoiceItems.PreRender += _radGridInvoiceItems_PreRender;
        _radGridInvoiceItems.MasterTableView.DataSourceID = null;
        _radGridInvoiceItems.DataSourceID = null;
        // just view
        InvoiceItemGrid1.SetEditMode(false);

        Id = Convert.ToInt32(Request["id"].ToString()); //HomestayStudentId
        StudentIdFromStudentRegistrar = Convert.ToInt32(Request["StudentId"].ToString());



        if (Request["ScheduleChange"] != null)
        {
            string sc = Request["ScheduleChange"].ToString();
            ScheduleChange = Convert.ToInt32(sc);

        }

        if (!IsPostBack)
        {


            file_upload.InitFileDownloadList((int)CConstValue.Upload.Homestay);

            var global = new CGlobal();

            SetDateTime();


            if (Id > 0)
            {
                FillFormData(Id); //Fill Form if it's not a new request

                UpdateToolBar.Items[0].Text = "Requested";
            }

            Dropdown_StudentlList(Id, StudentIdFromStudentRegistrar); // Load Student List

            if (ScheduleChange == 1) // Schedule Change
                ShowScheduleChange(true);
            else
                ShowScheduleChange(false);
        }
    }
    protected void ShowScheduleChange(bool show)
    {
        lblScheduleChange.Visible = show;
        txtScheduleComment.Visible = show;
        Grid_ChangeList.Visible = show;
    }
    protected int GetHomestayStudentId(int StudentId)
    {
        int result = 0;
        var cHomestayStudentRequest = new CHomestayStudentRequest();

        int HomestayStudentId = cHomestayStudentRequest.GetCountHomestayStudentRequestId(StudentId);
        result = HomestayStudentId;
        return result;
    }
    protected void SetDateTime()
    {
        DateTime SetDate = DateTime.Today;
        DateTime SetTime = DateTime.Now;

        DatePickerStartDate.SelectedDate = SetDate;
        DatePickerEndDate.SelectedDate = SetDate;
        DatePicker_DepartureDate.SelectedDate = SetDate;
        DatePickArrivalDate.SelectedDate = SetDate;
        TimePickerArrivalTime.SelectedTime = DateTime.Now.TimeOfDay;
        TimePickerDepartureTime.SelectedTime = DateTime.Now.TimeOfDay;
    }

    protected void UpdateToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
    {
        if (e.Item.Text!= "Close Window")
        {
            UpdateStudentRegistar();
        }
        
    }
    protected void UpdateStudentRegistar()
    {
        if (Request["id"] != null)
        {

            int HomestayStudentId = Id;
            var cHomestayStudentRequest = new CHomestayStudentRequest();

            if (ScheduleChange == 0)
            {
                if (HomestayStudentId == 0) //New Registrar
                {
                    int AddResult = 0;
                    HomestayStudentBasic Student = new HomestayStudentBasic();

                    //Retrive Data from Form
                    Student = GetFormData(Student);
                    //Homestay Status
                    Student.HomestayStudentStatus = 0; //Pending=0, Requested=1, Placed by School=2, Placed by Agency =3, Canceled =4
                    Student.CreatedDate = DateTime.Now;
                    Student.CreatedUserId = CurrentUserId;
                    AddResult = cHomestayStudentRequest.Add(Student);
                    if (AddResult > 0)
                    {
                        file_upload.SaveFile(AddResult);
                      //  GenerateInvoice(AddResult, (int)CConstValue.InvoiceType.Homestay);
                        RunClientScript("Close();");
                        ShowMessage("Added a new homestay registar and Generated an invoice successfully.");
                    }
                    else //Failed
                    {
                        ShowMessage("Failed to add a new homestay registar, Please try it again.");

                    }

                }
                else //Update Registrar
                {
                    bool UpdateResult = false;
                    HomestayStudentBasic UpdateStudent = cHomestayStudentRequest.GetHomestayStudentRequest(HomestayStudentId);
                    // Retrive Data from From
                    UpdateStudent = GetFormData(UpdateStudent);

                    UpdateStudent.HomestayStudentStatus = 1; //Requested

                    UpdateStudent.UpdateUserId = CurrentUserId;
                    UpdateStudent.UpdatedDate = DateTime.Now;
                    UpdateResult = cHomestayStudentRequest.Update(UpdateStudent);
                    int InvoiceId = 0;
                    var cInvoice = new CInvoice();
                    InvoiceId = cInvoice.GetInvoiceIdbyHomestayStudentId(HomestayStudentId);

                    if (InvoiceId==0)
                    {
                       
                        GenerateInvoice(HomestayStudentId, (int)CConstValue.InvoiceType.Homestay);  //Generate Invoice

                    }


                    file_upload.SaveFile(UpdateStudent.HomestayStudentId);

                    if (UpdateResult)
                    {
                        RunClientScript("Close();");
                        ShowMessage("Requested Homestay Registar successfully.");
                    }
                    else //Failed
                    {
                        ShowMessage("Failed to update Homestay Registrar, please try it again.");
                    }
                }
            }
            else  //Schedule Change: New Homestay Replacement Request
            {
                //int SCResult = 0;
                //HomestayStudentBasic StudentBasic = new HomestayStudentBasic();

                ////Retrive Data from Form
                //StudentBasic = GetFormData(StudentBasic);
                ////Homestay Status
                //StudentBasic.HomestayStudentStatus = 1; //Requested=1, Placed by School=2, Placed by Agency =3, Canceled =4, Schedule Change=5
                //StudentBasic.CreatedDate = DateTime.Now;
                //StudentBasic.CreatedUserId = CurrentUserId;
                //SCResult = cHomestayStudentRequest.Add(StudentBasic);
                //if (SCResult > 0)
                //{
                //    file_upload.SaveFile(SCResult);
                //    GenerateInvoice(SCResult, 16);
                //    var cHomestayCancleSheduleChange = new CHomestayCancelSheduleChange();
                //    HomestayCancelScheduleChange Cancel = new HomestayCancelScheduleChange();
                //    Cancel.CancellationDate = DateTime.MaxValue;
                //    Cancel.Comment = txtScheduleComment.Text;
                //    Cancel.ChangeType = 2; // 1=Cancel, 2=Schedule Change
                //    Cancel.OriginalStudentBasicId = Convert.ToInt32(Request["Id"]);
                //    Cancel.NewStudentBasicId = SCResult;
                //    Cancel.RequestStatus = 0; //0=Pending, 1=Rejected, 2=Approved
                //    Cancel.CreatedDate = DateTime.Now;
                //    Cancel.CreatedId = CurrentUserId;
                //    Cancel.UpdatedDate = DateTime.Now;
                //    Cancel.UpdatedId = CurrentUserId;

                //    int result = 0;
                //    result = cHomestayCancleSheduleChange.Add(Cancel);
                //    Grid_ChangeList.Rebind();
                //    ShowMessage("Homestay Schedule Change are added and Generated an invoice successfully.");
                //}
                //else //Failed
                //{
                //    ShowMessage("Failed to Add Schedule Change, Please try it again.");

                //}
                //Wait for Jun's Schedule Change Invoice Function
                //Update Student Request Status: Schedule Change
                //Release Homestay Placement (Host Bed)

            }

        }
    }

    protected HomestayStudentBasic GetFormData(HomestayStudentBasic Student)
    {

        //Airpot Information
        Student.PickUp = Convert.ToInt32(ddl_pickup.SelectedValue.ToString());
        Student.ArrivalDate = DatePickArrivalDate.SelectedDate;
        Student.ArriveTime = TimePickerArrivalTime.SelectedTime;
        Student.ArrivalAirLine = txtArrivalAirLine.Text.ToString().Trim();
        Student.ArrivalFlightNo = txtArrivalFlilghtNo.Text.ToString().Trim();
        Student.DepartingFrom = txtDepartingFrom.Text.ToString().Trim();
        Student.DropOff = Convert.ToInt32(ddlDropoff.SelectedValue.ToString());
        Student.DepartureDate = DatePicker_DepartureDate.SelectedDate;
        Student.DepartureTime = TimePickerDepartureTime.SelectedTime;
        Student.DepartureAirLine = txtDepartureAirline.Text.ToString().Trim();
        Student.DepartureFlightNo = txtDepartureFlightNo.Text.ToString().Trim();
        Student.LeavingFrom = txtLeavingFrom.Text.ToString().Trim();

        //Preference
        Student.Smoking = Convert.ToInt32(ddlSmoking.SelectedValue.ToString());
        Student.Internet = Convert.ToInt32(ddlInternet.SelectedValue.ToString());
        Student.RoomType = Convert.ToInt32(ddlRoomType.SelectedValue.ToString());
        Student.MealType = Convert.ToInt32(ddlMealType.SelectedValue.ToString());
        Student.GuardianRequired = Convert.ToInt32(ddlGuardian.SelectedValue.ToString());
        Student.Pet = Convert.ToInt32(ddlPet.SelectedValue.ToString());
        Student.Children = Convert.ToInt32(ddlChildren.SelectedValue.ToString());
        Student.Medication = txtMedication.Text.ToString().Trim();
        Student.Diet = txtDiet.Text.ToString().Trim();

        //General Information
        Student.StartDate = DatePickerStartDate.SelectedDate;
        Student.EndDate = DatePickerEndDate.SelectedDate;
        Student.ExtensionFlag = Convert.ToInt32(ddlExtention.SelectedValue.ToString());
        int Weeks = 0;
        int ExtraDay = 0;
        if (TxtHomestayWeeks.Text.ToString().Trim().Length > 0)
        {
            Weeks = Convert.ToInt32(TxtHomestayWeeks.Text.ToString());
        }
        if (TxtExtraDays.Text.ToString().Length > 0)
        {
            ExtraDay = Convert.ToInt32(TxtExtraDays.Text.ToString());
        }
        Student.HomestayWeeks = Weeks;
        Student.ExtraDays = ExtraDay;


        Student.UrgentRequest = Convert.ToInt32(ddlUrgent.SelectedValue.ToString());
        Student.AllergyType = Convert.ToInt32(ddlAllergy.SelectedValue.ToString());
        Student.AllergyNote = txtAllergyType.Text.ToString();
        Student.Comments = txtComments.Text;
        Student.PlacedUserId = 0;
        Student.StudentId = Convert.ToInt32(ddlStudent.SelectedValue);


        return Student;

    }
    protected void Grid_ChangeList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        //var cHomestayChange = new CHomestayCancelSheduleChange();
        //Grid_ChangeList.DataSource = cHomestayChange.HomestayChangeList(Convert.ToInt32(Request["Id"]), 2);
    }

    protected void Grid_ChangeList_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem Item = (GridDataItem)e.Item;

            if (Item["RequestStatus"].Text == "0")
            {
                Item["RequestStatus"].Text = "Pending";
            }
            else
            {
                Item["RequestStatus"].Text = "Cancelled";
            }

            //int CancelScheduleChange =Convert.ToInt32(Item.GetDataKeyValue("HomestayCancelSchdeduleId"));
            //var cHomestaySC = new CHomestayCancelSheduleChange();
            //HomestayCancelScheduleChange SC = cHomestaySC.GetSC(CancelScheduleChange);
            //int HomestayStudentBasicId =(int) SC.NewStudentBasicId;
            //var cHomestayStudentBasic = new CHomestayStudentRequest();
            //HomestayStudentBasic Student = cHomestayStudentBasic.GetHomestayStudentRequest(HomestayStudentBasicId);
            //Item["StartDate"].Text =string.Format("{0:MM-dd-yyyy}", Student.StartDate);
            //Item["EndDate"].Text =string.Format("{0:MM-dd-yyyy}", Student.EndDate);
        }
    }
    protected void FillFormData(int HomestayStudentId)
    {
        int type = Convert.ToInt32(Request["StudentId"].ToString()); // 0: New, 1000000:Schedule Change
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
        ddlUrgent.SelectedValue = Student.UrgentRequest.ToString();
        ddlAllergy.SelectedValue = Student.AllergyType.ToString();
        txtAllergyType.Text = Student.AllergyNote;
        txtComments.Text = Student.Comments;
        //File Upload
        file_upload.GetFileDownload(Convert.ToInt32(HomestayStudentId));
        ddlStudent.Enabled = false;

    }
    private void _radGridInvoiceItems_PreRender(object sender, EventArgs e)
    {
        ResetGridInvoice();
    }

    public DataTable GetInvoiceData()
    {
        // student invoice


        DataTable table = new DataTable();
        table.Columns.Add("InvoiceCoaItemId", typeof(int));
        table.Columns.Add("StandardPrice", typeof(decimal));
        table.Columns.Add("StudentPrice", typeof(decimal));
        table.Columns.Add("AgencyPrice", typeof(decimal));
        table.Columns.Add("Remark", typeof(string));
        table.Columns.Add("InvoiceItemId", typeof(int));


        Decimal HomestayPrice = 0;

        int StudentId = 0;
        StudentId = GetStudentId(Id);
        if (StudentId == 0)
        {
            StudentId = Convert.ToInt32(ddlStudent.SelectedValue);
        }
        if (StudentId > 0)
        {
            int type = 0;
            if (ddlMealType.SelectedValue == "1")
            {
                type = 1; // Homestay Fee: Half Meal
            }
            else
            {
                type = 2; //Homestay Fee: Full Meal
            }

            if (ddlGuardian.SelectedValue == "2")
            {
                type = 3; //Junior Fee
            }

            HomestayPrice = GetHomestayPrice(CurrentSiteLocationId, type) * Convert.ToInt32(TxtHomestayWeeks.Text.Trim().ToString()); //Homestay Fee

            Decimal Extra = GetHomestayPrice(CurrentSiteLocationId, 7) * Convert.ToInt32(TxtExtraDays.Text.Trim().ToString()); //Exta Days

            HomestayPrice = HomestayPrice + Extra;

            table.Rows.Add(15, HomestayPrice, HomestayPrice, HomestayPrice, string.Empty, 0); // HomestayFee

            HomestayPrice = GetHomestayPrice(CurrentSiteLocationId, 4); // Placement Fee
            table.Rows.Add(17, HomestayPrice, HomestayPrice, HomestayPrice, string.Empty, 0);  //Placement Fee
        }



        if (ddl_pickup.SelectedValue == "2")
        {

            HomestayPrice = GetHomestayPrice(CurrentSiteLocationId, 5);
            table.Rows.Add(35, HomestayPrice, HomestayPrice, HomestayPrice, string.Empty, 0);   //Airport Pick up Fee

        }
        if (ddlDropoff.SelectedValue == "2")
        {

            HomestayPrice = GetHomestayPrice(CurrentSiteLocationId, 6);
            table.Rows.Add(37, HomestayPrice, HomestayPrice, HomestayPrice, string.Empty, 0);      //Airport Drop off Fee
        }




        return table;



    }
    protected void Dropdown_StudentlList(int HomestayStudentId, int StudentId)
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
        ddlStudent.Filter = (RadComboBoxFilter)Convert.ToInt32(1);
        ddlStudent.Items.Insert(0, new RadComboBoxItem("", "0"));
        if (StudentId > 0)
        {
            ddlStudent.SelectedValue = StudentId.ToString();
        }
        else if (StudentId == 0 && HomestayStudentId > 0)
        {
            ddlStudent.SelectedValue = GetStudentId(HomestayStudentId).ToString();
        }
        else if (HomestayStudentId == 0 && StudentId == 0)
        {
            ddlStudent.SelectedIndex = 0;
        }

        ddlStudent.DataBind();
    }

    protected void ddlStudent_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        int StudentId = 0;
        StudentId = Convert.ToInt32(ddlStudent.SelectedValue);

    }
    protected int GetStudentId(int HomestayStudentId)
    {
        int StudentId = 0;
        if (HomestayStudentId > 0)
        {
            var cHomestayStudent = new CHomestayStudentRequest();
            HomestayStudentBasic StudentBasic = cHomestayStudent.GetHomestayStudentRequest(HomestayStudentId);
            StudentId = Convert.ToInt32(StudentBasic.StudentId);
        }
        return StudentId;
    }

    protected void GenerateInvoice(int HomestayStudentId, int InvoiceType)
    {
        int InvoiceId = 0;
        // Generate Invoice
        var cInvoice = new CInvoice();
        var invoice = new Invoice();

        invoice.HomestayRegistrationId = HomestayStudentId;
        invoice.StudentId = Convert.ToInt32(ddlStudent.SelectedValue);
        invoice.SiteLocationId = CurrentSiteLocationId;
        invoice.InvoiceType = InvoiceType;//8 Homestay
        
        invoice.Status = (int)CConstValue.InvoiceStatus.Pending; // Pending
        invoice.CreatedId = CurrentUserId;
        invoice.CreatedDate = DateTime.Now;
        invoice.UpdatedId = CurrentUserId;
        invoice.UpdatedDate = DateTime.Now;
        InvoiceId = cInvoice.Add(invoice); //DB:Invoice 
        if (InvoiceId > 0)
        {
            Decimal HomestayPrice = 0;
            int type = 0;
            if (ddlMealType.SelectedValue == "1")
            {
                type = 1; // Homestay Fee: Half Meal
            }
            else
            {
                type = 2; //Homestay Fee: Full Meal
            }

            if (ddlGuardian.SelectedValue == "2")
            {
                type = 3; //Junior Fee
            }
            // HomestayPrice = GetHomestayPrice(CurrentSiteLocationId, type);
            HomestayPrice = GetHomestayPrice(CurrentSiteLocationId, type) * Convert.ToInt32(TxtHomestayWeeks.Text.Trim().ToString()); //Homestay Fee

            Decimal Extra = GetHomestayPrice(CurrentSiteLocationId, 7) * Convert.ToInt32(TxtExtraDays.Text.Trim().ToString()); //Exta Days

            HomestayPrice = HomestayPrice + Extra;
            GenerateInvoiceItem(InvoiceId, 15, HomestayPrice); //Homestay Fee
            type = 4;
            HomestayPrice = GetHomestayPrice(CurrentSiteLocationId, type);
            GenerateInvoiceItem(InvoiceId, 17, HomestayPrice); //Homestay Placement Fee
            if (ddl_pickup.SelectedValue == "2")
            {
                type = 5;
                HomestayPrice = GetHomestayPrice(CurrentSiteLocationId, type);
                GenerateInvoiceItem(InvoiceId, 35, HomestayPrice); //Airport Pick Up

            }
            if (ddlDropoff.SelectedValue == "2")
            {
                type = 6;
                HomestayPrice = GetHomestayPrice(CurrentSiteLocationId, type);
                GenerateInvoiceItem(InvoiceId, 37, HomestayPrice); //Airport Drop Off 

            }

            //to present invoice
            ResetGridInvoice();

        }

    }
    private void ResetGridInvoice()
    {
        _radGridInvoiceItems.DataSource = GetInvoiceData();
        _radGridInvoiceItems.Rebind();
    }

    protected void GenerateInvoiceItem(int InvoiceId, int CoaItemId, Decimal HomestayPrice)

    { // Save Invoice Item

        var invoiceItemHomestay = new InvoiceItem();
        invoiceItemHomestay.InvoiceId = InvoiceId;
        invoiceItemHomestay.InvoiceCoaItemId = CoaItemId;
        invoiceItemHomestay.StandardPrice = HomestayPrice;
        invoiceItemHomestay.StudentPrice = HomestayPrice;
        invoiceItemHomestay.AgencyPrice = HomestayPrice;
        invoiceItemHomestay.CreatedId = CurrentUserId;
        invoiceItemHomestay.CreatedDate = DateTime.Now;
        invoiceItemHomestay.UpdatedId = CurrentUserId;
        invoiceItemHomestay.UpdatedDate = DateTime.Now;
        var cInvoiceHomestay = new CInvoiceItem();
        cInvoiceHomestay.Add(invoiceItemHomestay);


    }
    public Decimal GetHomestayPrice(int SiteLocationId, int HomestayPriceType)
    {
        Decimal price = 0;
        var cHomestayCost = new CHomestayCost();
        HomestayCost cost = new HomestayCost();
        cost = cHomestayCost.Get(SiteLocationId);
        if (cost.HomestayCostId.ToString() != null)
        {

            switch (HomestayPriceType)
            {
                case 1: //Homestay Fee: Half Meal
                    price = Convert.ToDecimal(cost.Private_HalfMealFee);
                    break;
                case 2: //Homestay Fee: Full Meal
                    price = Convert.ToDecimal(cost.Private_FullMealFee);
                    break;
                case 3: //Homestay Fee: Junior Fee
                    price = Convert.ToDecimal(cost.JuniorFee);
                    break;
                case 4: //Placement Fee
                    price = Convert.ToDecimal(cost.PlacementFee);
                    break;
                case 5: // Airport Pickup
                    price = Convert.ToDecimal(cost.AirportPickUpFee);
                    break;
                case 6: //Airport Drop Off
                    price = Convert.ToDecimal(cost.AirportDropOffFee);
                    break;
                case 7: // Extra Day
                    price = Convert.ToDecimal(cost.ExtralDayFee);
                    break;

            }
        }
        return price;

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
            weeks = weeks+1;
            ExtraDays = 0;
        } 

        TxtHomestayWeeks.Text = weeks.ToString();
        TxtExtraDays.Text = ExtraDays.ToString();

    }

    protected void DatePickArrivalDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        DatePickerStartDate.SelectedDate = DatePickArrivalDate.SelectedDate;
    }
}
