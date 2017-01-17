using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;
using System.Data;


public partial class School_StudentHousing_NewDormitoryStudentPop : PageBase
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

        Id = Convert.ToInt32(Request["id"].ToString()); //DormitoryRegistrationId
        StudentIdFromStudentRegistrar = Convert.ToInt32(Request["StudentId"].ToString());
        if (Request["ScheduleChange"] != null)
        {
            string sc = Request["ScheduleChange"].ToString();
            ScheduleChange = Convert.ToInt32(sc);

        }

        if (!IsPostBack)
        {


            file_upload.InitFileDownloadList((int)CConstValue.Upload.Dormitory);

            var global = new CGlobal();

            SetDateTime();


            if (Id > 0)
            {
                FillFormData(Id); //Fill Form if it's not a new request
            }
            else if (Id == 0)
            {
                if (StudentIdFromStudentRegistrar > 0)
                {
                    ddlStudent.Enabled = false;
                }
            }

            Dropdown_StudentlList(Id, StudentIdFromStudentRegistrar); // Load Student List

            //if (StudentIdFromStudentRegistrar == 0)
            //{


            //}
            //else
            //{

            //}



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
    protected int GeDormitoryStudentId(int StudentId)
    {
        int result = 0;
        var cDormitoryStudentRequest = new CDormitoryRegistrations();

        int DormitoryRegistrationId = cDormitoryStudentRequest.GetCountDormitoryStudentRequestId(StudentId);
        result = DormitoryRegistrationId;
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
        if (UpdateToolBar.TabIndex == 0)
        {
            UpdateStudentRegistar();
        }

    }
    protected void UpdateStudentRegistar()
    {
        if (Request["id"] != null)
        {

            int DormitoryRegistrationId = Id;
            var cDormitoryStudentRequest = new CDormitoryRegistrations();

            if (ScheduleChange == 0)
            {
                if (DormitoryRegistrationId == 0) //New Registrar
                {
                    int AddResult = 0;
                    DormitoryRegistration Student = new DormitoryRegistration();

                    //Retrive Data from Form
                    Student = GetFormData(Student);
                    //Dormitory Status
                    Student.DormitoryStudentStatus = 1; //Requested=1, Placed by School=2, Placed by Agency =3, Canceled =4
                    Student.CreatedDate = DateTime.Now;
                    Student.CreatedId = CurrentUserId;
                    AddResult = cDormitoryStudentRequest.Add(Student);
                    if (AddResult > 0)
                    {
                        file_upload.SaveFile(AddResult);
                        GenerateInvoice(AddResult, (int)CConstValue.InvoiceType.Dormitory);
                        RunClientScript("Close();");


                    }
                    else //Failed
                    {
                        ShowMessage("Failed to add a new dormitory registar, Please try it again.");

                    }

                }
                else //Update Registrar
                {
                    bool UpdateResult = false;
                    DormitoryRegistration UpdateStudent = cDormitoryStudentRequest.GetDormitoryStudentRequest(DormitoryRegistrationId);
                    // Retrive Data from From
                    UpdateStudent = GetFormData(UpdateStudent);

                    UpdateStudent.UpdatedId = CurrentUserId;
                    UpdateStudent.UpdatedDate = DateTime.Now;
                    UpdateResult = cDormitoryStudentRequest.Update(UpdateStudent);
                    file_upload.SaveFile(UpdateStudent.DormitoryRegistrationId);

                    if (UpdateResult)
                    {
                        RunClientScript("Close();");
                        ShowMessage("Updated Dormitory Registration successfully.");
                    }
                    else //Failed
                    {
                        ShowMessage("Failed to update Dormitory Registration, please try it again.");
                    }
                }
            }
            else  //Schedule Change: New Dormitory Replacement Request
            {
                int SCResult = 0;
                DormitoryRegistration StudentBasic = new DormitoryRegistration();

                //Retrive Data from Form
                StudentBasic = GetFormData(StudentBasic);
                //Homestay Status
                StudentBasic.DormitoryStudentStatus = 1; //Requested=1, Placed by School=2, Placed by Agency =3, Canceled =4, Schedule Change=5
                StudentBasic.CreatedDate = DateTime.Now;
                StudentBasic.CreatedId = CurrentUserId;
                SCResult = cDormitoryStudentRequest.Add(StudentBasic);
                if (SCResult > 0)
                {
                    file_upload.SaveFile(SCResult);
                    GenerateInvoice(SCResult, 14);// ScheduleChange
                    Grid_ChangeList.Rebind();
                    RunClientScript("Close();");
                    ShowMessage("Dormitory Schedule Change are added and Generated an invoice successfully.");
                }
                else //Failed
                {
                    ShowMessage("Failed to Add Schedule Change, Please try it again.");

                }
                //Wait for Jun's Schedule Change Invoice Function
                //Update Student Request Status: Schedule Change
                //Release Dormitory Placement (Host Bed)

            }

        }
    }

    protected DormitoryRegistration GetFormData(DormitoryRegistration Student)
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


        //General Information
        Student.StartDate = DatePickerStartDate.SelectedDate;
        Student.EndDate = DatePickerEndDate.SelectedDate;
        Student.ExtensionFlag = Convert.ToInt32(ddlExtention.SelectedValue.ToString());
        int Weeks = 0;
        int ExtraDay = 0;
        if (TxtDormitoryWeeks.Text.ToString().Trim().Length > 0)
        {
            Weeks = Convert.ToInt32(TxtDormitoryWeeks.Text.ToString());
        }
        if (TxtExtraDays.Text.ToString().Length > 0)
        {
            ExtraDay = Convert.ToInt32(TxtExtraDays.Text.ToString());
        }
        Student.DormitoryWeeks = Weeks;
        Student.ExtraDays = ExtraDay;


        Student.UrgentRequest = Convert.ToInt32(ddlUrgent.SelectedValue.ToString());
        Student.Comments = txtComments.Text;
        Student.PlacedUserId = 0;
        Student.StudentId = Convert.ToInt32(ddlStudent.SelectedValue);


        return Student;

    }
    protected void Grid_ChangeList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
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

            //int CancelScheduleChange = Convert.ToInt32(Item.GetDataKeyValue("DormitoryCancelSchdeduleId"));
            //var cDormitorySC = new CDormitoryCancelSheduleChange();
            //DormitoryCancelScheduleChange SC = cDormitorySC.GetSC(CancelScheduleChange);
            //int DormitoryRegistrationId = (int)SC.NewStudentBasicId;
            //var cDormitoryStudentBasic = new CDormitoryRegistrations();

        }
    }
    protected void FillFormData(int DormitoryRegistrationId)
    {
        int type = Convert.ToInt32(Request["StudentId"].ToString()); // 0: New, 1000000:Schedule Change
        var cDormitoryStudentBasic = new CDormitoryRegistrations();
        DormitoryRegistration Student = cDormitoryStudentBasic.GetDormitoryStudentRequest(DormitoryRegistrationId);

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
            TxtDormitoryWeeks.Text = "";
        }
        else
        {
            TxtDormitoryWeeks.Text = Weeks.ToString();
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

        txtComments.Text = Student.Comments;
        //File Upload
        file_upload.GetFileDownload(Convert.ToInt32(DormitoryRegistrationId));
        ddlStudent.Enabled = false;


    }
    private void _radGridInvoiceItems_PreRender(object sender, EventArgs e)
    {
        ResetGridInvoice();
    }

    public DataTable GetInvoiceData()
    {

        DataTable table = new DataTable();
        table.Columns.Add("InvoiceCoaItemId", typeof(int));
        table.Columns.Add("StandardPrice", typeof(decimal));
        table.Columns.Add("StudentPrice", typeof(decimal));
        table.Columns.Add("AgencyPrice", typeof(decimal));
        table.Columns.Add("Remark", typeof(string));
        table.Columns.Add("InvoiceItemId", typeof(int));
        //student invoice
        DateTime StartTime = Convert.ToDateTime(DatePickerStartDate.SelectedDate);
        DateTime EndTime = Convert.ToDateTime(DatePickerEndDate.SelectedDate);

        if (EndTime > StartTime)
        {


            Decimal DormitoryPrice = 0;

            int StudentId = 0;
            StudentId = GetStudentId(Id);
            if (StudentId == 0)
            {
                StudentId = Convert.ToInt32(ddlStudent.SelectedValue);
            }
            if (Id > 0)
            {

            }
            if (StudentId > 0)
            {
                int type = 0;

                type = 1;

                DormitoryPrice = GetDormitoryPrice(CurrentSiteLocationId, type) * Convert.ToInt32(TxtDormitoryWeeks.Text.Trim().ToString());
                //Exta Days
                type = 6;
                Decimal Extra = GetDormitoryPrice(CurrentSiteLocationId, type) * Convert.ToInt32(TxtExtraDays.Text.Trim().ToString());


                DormitoryPrice = DormitoryPrice + Extra;

                table.Rows.Add(21, DormitoryPrice, DormitoryPrice, DormitoryPrice, string.Empty, 0); // DormitoryFee

                DormitoryPrice = GetDormitoryPrice(CurrentSiteLocationId, 2); // Placement Fee
                table.Rows.Add(23, DormitoryPrice, DormitoryPrice, DormitoryPrice, string.Empty, 0);  //Placement Fee

                type = 5; //Dormitory Deposit Fee
                DormitoryPrice = GetDormitoryPrice(CurrentSiteLocationId, type);
                table.Rows.Add(25, DormitoryPrice, DormitoryPrice, DormitoryPrice, string.Empty, 0);
            }



            if (ddl_pickup.SelectedValue == "2")
            {

                DormitoryPrice = GetDormitoryPrice(CurrentSiteLocationId, 3);
                table.Rows.Add(35, DormitoryPrice, DormitoryPrice, DormitoryPrice, string.Empty, 0);   //Airport Pick up Fee

            }
            if (ddlDropoff.SelectedValue == "2")
            {

                DormitoryPrice = GetDormitoryPrice(CurrentSiteLocationId, 4);
                table.Rows.Add(37, DormitoryPrice, DormitoryPrice, DormitoryPrice, string.Empty, 0);      //Airport Drop off Fee
            }

        }


        return table;



    }
    protected void Dropdown_StudentlList(int DormitoryRegistrationId, int StudentId)
    {
        ddlStudent.Items.Clear();

        var cglobal = new CGlobal();
        int SiteLocationId = CurrentSiteLocationId;
        if (StudentId > 0)
        {
            var cStudentRequest = new CDormitoryRegistrations();
            SiteLocationId = cStudentRequest.SiteLocationbyStudentId(StudentId);

        }
        ddlStudent.DataSource = cglobal.LoadStudentList(SiteLocationId); //CurrentSiteLocationId
        ddlStudent.DataTextField = "Name";
        ddlStudent.DataValueField = "Value";
        ddlStudent.Filter = (RadComboBoxFilter)Convert.ToInt32(1);
        ddlStudent.Items.Insert(0, new RadComboBoxItem("", "0"));
        if (StudentId > 0)
        {
            ddlStudent.SelectedValue = StudentId.ToString();
        }
        else if (StudentId == 0 && DormitoryRegistrationId > 0)
        {
            ddlStudent.SelectedValue = GetStudentId(DormitoryRegistrationId).ToString();
        }
        else if (DormitoryRegistrationId == 0 && StudentId == 0)
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
    protected int GetStudentId(int DormitoryRegistrationId)
    {
        int StudentId = 0;
        if (DormitoryRegistrationId > 0)
        {
            var cDormitoryStudent = new CDormitoryRegistrations();
            DormitoryRegistration StudentBasic = cDormitoryStudent.GetDormitoryStudentRequest(DormitoryRegistrationId);
            StudentId = Convert.ToInt32(StudentBasic.StudentId);
        }
        return StudentId;
    }

    protected void GenerateInvoice(int DormitoryRegistrationId, int InvoiceType)
    {
        int InvoiceId = 0;
        // Generate Invoice
        var cInvoice = new CInvoice();
        var invoice = new Invoice();

        invoice.DormitoryRegistrationId = DormitoryRegistrationId;
        invoice.StudentId = Convert.ToInt32(ddlStudent.SelectedValue);
        invoice.SiteLocationId = CurrentSiteLocationId;
        invoice.InvoiceType = InvoiceType;//11 Dormitory,14 Dormitory Schedule Change
        
        invoice.Status = (int)CConstValue.InvoiceStatus.Pending; // Pending
        invoice.CreatedId = CurrentUserId;
        invoice.CreatedDate = DateTime.Now;

        InvoiceId = cInvoice.Add(invoice); //DB:Invoice 
        if (InvoiceId > 0)
        {
            Decimal DormitoryPrice = 0;
            int type = 0;
            //New ERP DormitoryT_V06_20150108

            //DormitoryFee, Type=1
            type = 1;
            DormitoryPrice = GetDormitoryPrice(CurrentSiteLocationId, type) * Convert.ToInt32(TxtDormitoryWeeks.Text.Trim().ToString());
            //Exta Days
            type = 6;
            Decimal Extra = GetDormitoryPrice(CurrentSiteLocationId, type) * Convert.ToInt32(TxtExtraDays.Text.Trim().ToString());

            DormitoryPrice = DormitoryPrice + Extra;
            GenerateInvoiceItem(InvoiceId, 21, DormitoryPrice); //Dormitory Fee
            type = 2;
            DormitoryPrice = GetDormitoryPrice(CurrentSiteLocationId, type);
            GenerateInvoiceItem(InvoiceId, 23, DormitoryPrice); //Dormitory Placement Fee

            type = 5; //Dormitory Deposit Fee
            DormitoryPrice = GetDormitoryPrice(CurrentSiteLocationId, type);
            GenerateInvoiceItem(InvoiceId, 25, DormitoryPrice);


            if (ddl_pickup.SelectedValue == "2")
            {
                type = 3;
                DormitoryPrice = GetDormitoryPrice(CurrentSiteLocationId, type);
                GenerateInvoiceItem(InvoiceId, 35, DormitoryPrice); //Airport Pick Up

            }
            if (ddlDropoff.SelectedValue == "2")
            {
                type = 4;
                DormitoryPrice = GetDormitoryPrice(CurrentSiteLocationId, type);
                GenerateInvoiceItem(InvoiceId, 37, DormitoryPrice); //Airport Drop Off 
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

    protected void GenerateInvoiceItem(int InvoiceId, int CoaItemId, Decimal DormitoryPrice)

    { // Save Invoice Item

        var invoiceItemDormitory = new InvoiceItem();
        invoiceItemDormitory.InvoiceId = InvoiceId;
        invoiceItemDormitory.InvoiceCoaItemId = CoaItemId;
        invoiceItemDormitory.StandardPrice = DormitoryPrice;
        invoiceItemDormitory.StudentPrice = DormitoryPrice;
        invoiceItemDormitory.AgencyPrice = DormitoryPrice;
        invoiceItemDormitory.CreatedId = CurrentUserId;
        invoiceItemDormitory.CreatedDate = DateTime.Now;
        invoiceItemDormitory.UpdatedId = CurrentUserId;
        invoiceItemDormitory.UpdatedDate = DateTime.Now;
        var cInvoiceDormitory = new CInvoiceItem();
        cInvoiceDormitory.Add(invoiceItemDormitory);


    }
    public Decimal GetDormitoryPrice(int SiteLocationId, int DormitoryPriceType)
    {
        Decimal price = 0;
        var cDormitoryCost = new CDormitoryCost();
        DormitoryCost cost = new DormitoryCost();
        cost = cDormitoryCost.Get(SiteLocationId);
        if (cost.DormitoryCostId.ToString() != null)
        {

            switch (DormitoryPriceType)
            {
                case 1: //Dormitory Fee
                    price = Convert.ToDecimal(cost.DormitoryFee);
                    break;
                case 2: //Placement Fee
                    price = Convert.ToDecimal(cost.PlacementFee);
                    break;
                case 3: // Airport Pickup
                    price = Convert.ToDecimal(cost.AirportPickUpFee);
                    break;
                case 4: //Airport Drop Off
                    price = Convert.ToDecimal(cost.AirportDropOffFee);
                    break;

                case 5: //Deposit Fee
                    price = Convert.ToDecimal(cost.DepositFee);
                    break;

                case 6: // Extra Day
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
            weeks = weeks + 1;
            ExtraDays = 0;
        }
        TxtDormitoryWeeks.Text = weeks.ToString();
        TxtExtraDays.Text = ExtraDays.ToString();

    }
    
}