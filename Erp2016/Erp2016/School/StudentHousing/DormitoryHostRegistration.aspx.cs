
using System;
using System.Collections.Generic;
using Erp2016.Lib;
using Telerik.Web.UI;


namespace School.StudentHousing
{
    public partial class DormitoryHostRegistration : PageBase
    {
        public DormitoryHostRegistration() : base((int)CConstValue.Menu.DormitoryHostRegistration)
        { }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var UserId = (new CUser()).Get(CurrentUserId);
                var SiteId = (new CSite()).Get(CurrentSiteId);

                var fname = UserId.FirstName;
                var lname = UserId.LastName;

                DatePickerLeaseStartDate.SelectedDate = DateTime.Today;
                DatePickerLeaseEndDate.SelectedDate = DateTime.Today;

                DatePickStatusDate.SelectedDate = DateTime.Today;

                // Load Dropdown List
                Dropdown_Province();
                Dropdown_ShcoolList();

                Dropdown_Campus();
                EnableTab(false, false);
                //VisiableBtn(false);

                PageView_Basic.Selected = true;
                Session["DormitoryHostid"] = null;

            }
        }

        protected void Dropdown_Campus()
        {
            ddlSiteLocation.Items.Clear();
            var global = new CGlobal();
            ddlSiteLocation.DataSource = global.GetSiteLocationBySiteId(CurrentSiteId);
            ddlSiteLocation.DataTextField = "Name";
            ddlSiteLocation.DataValueField = "Value";
            ddlSiteLocation.SelectedIndex = 0;
            ddlSiteLocation.DataBind();

        }
        protected void Dropdown_ShcoolList()
        {
            ddlSchoolName.Items.Clear();

            var cglobal = new CGlobal();
            ddlSchoolName.DataSource = cglobal.LoadSchoolList(CurrentUserId);
            ddlSchoolName.DataTextField = "Name";
            ddlSchoolName.DataValueField = "Value";
            ddlSchoolName.SelectedValue = CurrentSiteLocationId.ToString();
            ddlSchoolName.DataBind();
        }


        protected void Dropdown_RoomLocation()
        {
            ddlRoom.Items.Clear();
            var cglobale = new CGlobal();
            int hostid = 0;


            if (Session["DormitoryHostid"] != null)
            {
                hostid = Convert.ToInt32(Session["DormitoryHostid"].ToString());
                ddlRoom.DataSource = cglobale.LoadDormatoryRoomList(hostid);
                ddlRoom.DataTextField = "Name";
                ddlRoom.DataValueField = "Value";
                ddlRoom.SelectedIndex = 0;
                ddlRoom.DataBind();

            }


        }
        protected void Dropdown_Province()
        {
            ddlProvice.Items.Clear();
            ddlProvice.Items.Add(new DropDownListItem("Ontario", "Ontario"));
            ddlProvice.Items.Add(new DropDownListItem("British Columbia", "British Columbia"));
            ddlProvice.Items.Add(new DropDownListItem("Nova Scotia", "Nova Scotia"));
            ddlProvice.SelectedIndex = 0;

        }


        protected void Grid_HostList_FilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        //public static void SetFilterCheckListItems(GridFilterCheckListItemsRequestedEventArgs e)
        //{
        //    object dataSource = null;
        //    string dataField = (e.Column as IGridDataColumn).GetActiveDataField();


        //    var cDormitory_HostBasic = new CDormitory_HostBasic();

        //    switch (dataField)
        //    {
        //        // Common

        //        case "FatherName":
        //            //var cDormitory_HostBasic = new CDormitory_HostBasic();
        //            dataSource = cDormitory_HostBasic.GetFatherNameList();
        //            break;

        //        case "RegistrationDate":

        //            dataSource = cDormitory_HostBasic.GetHostRegistrationDateList();
        //            break;

        //    }

        //    if (dataSource != null)
        //        SetFilter(e, dataField, dataSource);
        //}
        private static void SetFilter(GridFilterCheckListItemsRequestedEventArgs e, string dataField, object dataSource)
        {
            e.ListBox.DataSource = dataSource;
            e.ListBox.DataKeyField = dataField;
            e.ListBox.DataTextField = dataField;
            e.ListBox.DataValueField = dataField;
            e.ListBox.DataBind();
        }

        protected void ShowHostDetail()
        {
            if (Grid_HostList.SelectedValue != null)
            {
                int HostID = Convert.ToInt32(Grid_HostList.SelectedValue.ToString());

                Session["DormitoryHostid"] = HostID;
                Dropdown_ShcoolList();
                ddlSchoolName.SelectedIndex = 0;
                FillFormBySelectedHostid(HostID);
                Dropdown_RoomLocation();


                EnableTab(true, true);
                //VisiableBtn(false);
                SetSessionNull();
                ClearForm();


            }
        }

        protected void Grid_DormitoryPlacement_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Grid_HostList.SelectedValue != null)
            {

                var cDormitoryPlament = new CDormitoryPlacement();
                Grid_DormitoryPlacement.DataSource = cDormitoryPlament.GetDormitoryPlacementHistoryByHostId(Convert.ToInt32(Grid_HostList.SelectedValue));
                Grid_DormitoryPlacement.Visible = true;
            }

        }
        protected void Grid_DormitoryPlacement_ItemDataBound(object sender, GridItemEventArgs e)

        {
            if (e.Item is GridDataItem)
            {

                GridDataItem DataItem = e.Item as GridDataItem;



                //if (DataItem["PlacementType"].Text == "1")
                //{
                //    DataItem["PlacementType"].Text = "Placed By School";
                //}

                DataItem["PlacementStatus"].Text = PlaceStatus(DataItem["PlacementStatus"].Text);

                // DataItem["HostRoomFloor"].Text = FloorName(Convert.ToInt32(DataItem["HostRoomFloor"].Text));

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
            if (Status == "1") ////  Placed=1, Canceled =0, Schedule Changed=2
            {
                HomestayStatus = "Placed";
            }
            else if (Status == "0")
            {
                HomestayStatus = "Canceled";
            }
            else if (Status == "2")
            {
                HomestayStatus = "Schedule Changed";
            }
            return HomestayStatus;
        }
        protected void Tab_Host_TabClick(object sender, RadTabStripEventArgs e)
        {
            if (Session["DormitoryHostid"] != null)
            {
                switch (Convert.ToInt32(Tab_Host.SelectedIndex))
                {

                    case 1:
                        Grid_HostRoom.Rebind();
                        break;
                    case 2:
                        Grid_HostBed.Rebind();
                        break;
                    case 3:
                        Grid_School.Rebind();
                        break;
                    case 4:
                        Grid_DormitoryPlacement.Rebind();
                        break;
                }
                SetSessionNull();
                ClearForm();
            }
        }
        protected void EnableTab(bool bl, bool fl)
        {
            Tab_Host.Tabs[1].Enabled = bl;
            Tab_Host.Tabs[2].Enabled = bl;
            Tab_Host.Tabs[3].Enabled = bl;
            Tab_Host.Tabs[4].Enabled = bl;


        }
        public override void SetVisibleModifyControllers()
        {
            //if (UserPermissionModel.IsModify == false)
            //{
            //    // toolbar
            //    foreach (RadToolBarItem toolbarItem in RadToolBarProgram.Items)
            //    {
            //        toolbarItem.Enabled = false;
            //    }


            //}
        }






        protected DormitoryHost GetFormData()
        {

            DormitoryHost Dormitory = new DormitoryHost();
            //Dormitory Information
            Dormitory.DormitoryHostName = txtDormitoryName.Text;
            Dormitory.HomeType = ddlHomeType.SelectedValue;
            Dormitory.Address = txtHomeAddress.Text;
            Dormitory.City = txtCity.Text;
            Dormitory.Province = ddlProvice.SelectedValue;
            Dormitory.PostalCode = txtPostalCode.Text;
            Dormitory.PhoneNumber = txtPhone.Text;
            Dormitory.AdditionalPhone = txtAdditionalPhone.Text;
            Dormitory.HostStatus = 0;
            Dormitory.LeaseStartDate = DatePickerLeaseStartDate.SelectedDate;
            Dormitory.LeaseEndDate = DatePickerLeaseEndDate.SelectedDate;
            Dormitory.CoordinatorName = txtCoordianatorName.Text;
            Dormitory.CoordinatorPhone = txtCoordianatorPhone.Text;
            Dormitory.SchoolName = ddlSchoolName.SelectedValue;
            Dormitory.ActiveDate = DateTime.Today;
            Dormitory.InactiveDate = DateTime.MaxValue;
            Dormitory.CreatedDate = DateTime.Today;
            Dormitory.CreatedId = CurrentUserId;
            Dormitory.UpdatedDate = DateTime.Today;
            Dormitory.UpdatedId = CurrentUserId;
            return Dormitory;

        }


        protected void btn_basic_save_Click(object sender, EventArgs e)
        {

            var cDormitoryHost = new CDormitoryHost();
            int hostid = 0;  //Dormitory HostId

            if (Session["DormitoryHostid"] != null)
            {
                hostid = Convert.ToInt32(Session["DormitoryHostid"].ToString());
            }


            if (hostid == 0) //new host registration
            {
                int Dormitory_Add = cDormitoryHost.Add(GetFormData());
                if (Dormitory_Add == -1)
                {

                    ShowMessage("Failed to add Dormitory Host Information, please try it again");
                }
                else
                {
                    int maxhostid = cDormitoryHost.MaxHostId();
                    if (maxhostid > 0)
                    {
                        Session["DormitoryHostid"] = maxhostid;
                        // test room location
                        //Default School Information

                        var cHomesatyPreferredSchool = new CDormitoryHostPreferredSchool();
                        DormitoryHostPrefferedSchool School = new DormitoryHostPrefferedSchool();
                        School.HostId = maxhostid;
                        School.SiteLocationId = Convert.ToInt32(ddlSchoolName.SelectedValue); //CurrentSiteLocationId;
                        School.DefaultHostSchool = true;
                        School.ContactUserId = CurrentUserId;
                        School.DistanceSchool = "";
                        School.DistanceStation = "";
                        School.MajorIntersection = "";
                        School.CreatedDate = DateTime.Now;
                        School.CreatedId = CurrentUserId;

                        int shcoolid = cHomesatyPreferredSchool.Add(School);
                        if (shcoolid > 0)
                        {
                            EnableTab(true, true);
                            //VisiableBtn(true);

                            ShowMessage("Dormitory Host Information is saved successfully.");

                        }

                    }

                    Grid_HostList.Rebind();
                }
            }
            else    //update host registration
            {

                DormitoryHost Dormitory = new DormitoryHost();

                Dormitory = cDormitoryHost.Get(hostid);
                //Dormitory Information
                Dormitory.DormitoryHostName = txtDormitoryName.Text;
                Dormitory.HomeType = ddlHomeType.SelectedValue;
                Dormitory.Address = txtHomeAddress.Text;
                Dormitory.City = txtCity.Text;
                Dormitory.Province = ddlProvice.SelectedValue;
                Dormitory.PostalCode = txtPostalCode.Text;
                Dormitory.PhoneNumber = txtPhone.Text;
                Dormitory.AdditionalPhone = txtAdditionalPhone.Text;
                Dormitory.HostStatus = 0;
                Dormitory.LeaseStartDate = DatePickerLeaseStartDate.SelectedDate;
                Dormitory.LeaseEndDate = DatePickerLeaseEndDate.SelectedDate;
                Dormitory.CoordinatorName = txtCoordianatorName.Text;
                Dormitory.SchoolName = ddlSchoolName.SelectedValue;



                Dormitory.UpdatedDate = DateTime.Today;
                Dormitory.UpdatedId = CurrentUserId;
                bool HB_Update = cDormitoryHost.Update(Dormitory);
                if (HB_Update)
                {

                    //VisiableBtn(false);
                    EnableTab(true, true);
                    ShowMessage("Dormitory Host Information is updated successfully.");
                }
                else
                {
                    ShowMessage("Failed to update Dormitory Host Information, please try it again.");
                }

            }

            ClearHostBasicForm();// Clear up the Basic Form


        }

        protected void btn_registration_Click(object sender, EventArgs e)

        {

            Session["DormitoryHostid"] = null;


            Grid_HostRoom.Rebind();
            Grid_HostBed.Rebind();
            Grid_School.Rebind();


            //Clear Host Basic Information
            ClearHostBasicForm();
            //Clear Host Family Member Information

            //Clear Host Room 
            ClearHostRoomForm();
            //Clear Host Bed
            ClearHostBedForm();
            //Clear Host Preferred School

            //load dropdown lists
            Dropdown_Province();
            Dropdown_ShcoolList();
            Dropdown_RoomLocation();


            SetSessionNull();

            Tab_Host.SelectedIndex = 0;
            EnableTab(false, false);

            PageView_Basic.Selected = true;
            //VisiableBtn(false);
        }

        protected void SetSessionNull()
        {
            Session["FamilyMemberId"] = null;
            Session["HostRoomId"] = null;
            Session["HostBedId"] = null;
            Session["HostSchoolId"] = null;
        }
        protected void ClearForm()
        {
            ClearHostRoomForm();
            ClearHostBedForm();

        }

        protected void btn_AddRoom_Click(object sender, EventArgs e)
        {
            int hostid = 0;
            if (Session["DormitoryHostid"] != null)
            {
                hostid = Convert.ToInt32(Session["DormitoryHostid"].ToString());

                int hostRoomid = 0;
                if (Session["HostRoomId"] != null)
                {
                    hostRoomid = Convert.ToInt32(Session["HostRoomId"].ToString());
                }

                var cDormitory_HostRoom = new CDormitoryHostRoom();
                if (hostRoomid == 0) //new room
                {
                    var HRoom = new DormitoryHostRoom();
                    hostid = Convert.ToInt32(Session["DormitoryHostid"].ToString());
                    HRoom.HostId = hostid;
                    HRoom.HostRoomName = txtRoomName.Text;
                    HRoom.HostRoomFloor = 1; //Convert.ToInt32(ddlRoomLocation.SelectedValue);
                    HRoom.HostRoomType = false; //Convert.ToBoolean(Convert.ToInt32(ddlRoomType.SelectedValue));
                    HRoom.CreatedId = CurrentUserId;
                    HRoom.CreatedDate = DateTime.Now;
                    int HRoom_Save = cDormitory_HostRoom.Add(HRoom);
                    if (HRoom_Save == -1)
                    {
                        ShowMessage("Failed to add Dormitory Host Room, please try it again.");
                    }
                    else
                    {
                        ShowMessage("Dormitory Host Room is added successfully.");
                    }
                }
                else //update room
                {
                    var host_room = cDormitory_HostRoom.Get(hostRoomid);
                    host_room.HostRoomId = hostRoomid;
                    host_room.HostRoomName = txtRoomName.Text;
                    host_room.HostRoomFloor = 1; // Convert.ToInt32(ddlRoomLocation.SelectedValue);
                    host_room.HostRoomType = false; Convert.ToBoolean(Convert.ToInt32(ddlRoomType.SelectedValue));
                    host_room.UpdatedId = CurrentUserId;
                    host_room.UpdatedDate = DateTime.Now;
                    Boolean HRoom_Update = cDormitory_HostRoom.Update(host_room);
                    if (HRoom_Update)
                    {
                        ShowMessage("Host Room is updated successfully.");
                    }
                    else
                    {
                        ShowMessage("Failed to update Host Room, please try it again.");
                    }
                }


                ClearHostRoomForm(); //Clear up Host Room Form
            }


        }

        protected void btn_AddBed_Click(object sender, EventArgs e)
        {
            if (Session["DormitoryHostid"] != null)
            {

                int hostid = 0;
                hostid = Convert.ToInt32(Session["DormitoryHostid"].ToString());

                int hostBedid = 0;
                if (Session["HostBedId"] != null)
                {
                    hostBedid = Convert.ToInt32(Session["HostBedId"]);
                }
                var cDormitory_HostBed = new CDormitoryHostBed();
                if (hostBedid == 0) //new bed
                {
                    var HBed = new DormitoryHostBed();
                    HBed.HostId = hostid;
                    HBed.BedName = txtBedName.Text;
                    HBed.HostRoomId = Convert.ToInt32(ddlRoom.SelectedValue);
                    HBed.CreatedId = CurrentUserId;
                    HBed.CreatedDate = DateTime.Now;
                    int HBed_Save = cDormitory_HostBed.Add(HBed);
                    if (HBed_Save == -1)
                    {
                        ShowMessage("Failed to add Host Bed, please try it again.");
                    }
                    else
                    {
                        ShowMessage("Dormatory Host Bed is added successfully.");
                    }
                }
                else //update bed
                {
                    var host_bed = cDormitory_HostBed.Get(hostBedid);
                    host_bed.HostId = hostid;
                    host_bed.HostBedId = hostBedid;
                    host_bed.HostRoomId = Convert.ToInt32(ddlRoom.SelectedValue);
                    host_bed.BedName = txtBedName.Text;
                    host_bed.UpdatedId = CurrentUserId;
                    host_bed.UpdatedDate = DateTime.Now;
                    Boolean HBed_Update = cDormitory_HostBed.Update(host_bed);
                    if (HBed_Update)
                    {
                        ShowMessage("Host Bed is updated successfully.");
                    }
                    else
                    {
                        ShowMessage("Failed to update Host Bed, please try it again.");
                    }

                }


                ClearHostBedForm(); // Clear up Host Bed Form

            }

        }



        protected void btn_shcool_Click(object sender, EventArgs e)
        {
            //        if (Session["DormitoryHostid"]!= null)
            //        {
            //            int hostid = 0;
            //            hostid = Convert.ToInt32(Session["DormitoryHostid"].ToString());
            //            int hostPrefferedSchoolId = 0;

            //            if (Session["HostSchoolId"] != null)
            //            {
            //                hostPrefferedSchoolId = Convert.ToInt32(Session["HostSchoolId"].ToString());
            //            }

            //            var cDormitory_HostPreferredSchool = new CDormitoryHostPreferredSchool();

            //            if (hostPrefferedSchoolId == 0) //new shcool
            //            {
            //                var HS = new DormitoryHostPrefferedSchool();
            //                HS.HostId = hostid;
            //                HS.HostSchoolId = Convert.ToInt32(ddlSiteLocation.SelectedValue);
            //                HS.SiteLocationId = Convert.ToInt32(ddlSchoolName.SelectedValue);
            //                HS.ContactUserId = Convert.ToInt32(ddlSchoolContactName.SelectedValue);
            //                HS.MajorIntersection = txtMajorIntersection.Text;
            //                HS.DistanceSchool = txtDistanceToSchool.Text;
            //                HS.DistanceStation = txtDistanceBusSubway.Text;
            //                HS.CreatedId = CurrentUserId;
            //                HS.CreatedDate = DateTime.Now;
            //                int HS_Save = cDormitory_HostPreferredSchool.Add(HS);
            //                if (HS_Save == -1)
            //                {
            //                    ShowMessage("Failed to add Host Preferred School, please try it again.");
            //                }
            //                else
            //                {
            //                    ShowMessage("Host Preferred School is added successfully.");
            //                }
            //            }
            //            else //update school
            //            {

            //                var host_school = cDormitory_HostPreferredSchool.Get(hostPrefferedSchoolId);
            //                host_school.HostSchoolId = hostPrefferedSchoolId;
            //                host_school.HostId = Convert.ToInt32(Session["DormitoryHostid"].ToString());
            //                host_school.SiteLocationId = Convert.ToInt32(ddlSchoolName.SelectedValue);
            //                host_school.ContactUserId = Convert.ToInt32(ddlSchoolContactName.SelectedValue);
            //                host_school.MajorIntersection = txtMajorIntersection.Text;
            //                host_school.DistanceSchool = txtDistanceToSchool.Text;
            //                host_school.DistanceStation = txtDistanceBusSubway.Text;

            //                host_school.UpdatedId = CurrentUserId;
            //                host_school.UpdatedDate = DateTime.Now;
            //                Boolean HS_Update = cDormitory_HostPreferredSchool.Update(host_school);
            //                if (HS_Update)
            //                {

            //                    ShowMessage("Host Preferred School is updated successfully.");
            //                }
            //                else
            //                {
            //                    ShowMessage("Failed to update Host Preferred School, please try it again.");
            //                }
            //            }

            //            ClearHostPreferredSchoolForm(); // Clear up Host School Form
            //        }
        }

        protected void btn_basic_cancel_Click(object sender, EventArgs e)
        {
            ClearHostBasicForm();

        }
        protected void ClearHostBasicForm()
        {

            txtDormitoryName.Text = "";
            txtHomeAddress.Text = "";
            txtCity.Text = "";
            txtPostalCode.Text = "";
            txtPhone.Text = "";
            txtAdditionalPhone.Text = "";
            txtCoordianatorName.Text = "";
            txtCoordianatorPhone.Text = "";
            ddlSchoolName.SelectedValue = "0";
            DatePickerLeaseStartDate.SelectedDate = DateTime.Today;
            DatePickerLeaseEndDate.SelectedDate = DateTime.Today;
            ddlHomeType.SelectedIndex = 0;
            ddlProvice.SelectedIndex = 0;
            Grid_HostList.Rebind();

        }



        protected void ClearHostRoomForm()
        {
            Session["HostRoomId"] = null;
            txtRoomName.Text = "";
            ddlRoomLocation.SelectedValue = "1";
            ddlRoomType.SelectedValue = "1";
            Dropdown_RoomLocation();
            ddlRoom.SelectedIndex = 0;
            Grid_HostRoom.Rebind();
            Grid_HostList.Rebind();
        }
        protected void ClearHostBedForm()
        {
            txtBedName.Text = "";
            Session["HostBedId"] = null;
            Dropdown_RoomLocation();
            ddlRoom.SelectedValue = "0";
            Grid_HostBed.Rebind();
            Grid_HostList.Rebind();
        }


        //{
        //    //        Session["HostSchoolId"] = 0;
        //    //        txtDistanceToSchool.Text = "";
        //    //        txtDistanceBusSubway.Text = "";
        //    //        txtMajorIntersection.Text = "";

        //    //        Dropdown_ShcoolList();
        //    //        ddlSchoolName.SelectedValue = "0";
        //    //        Dropdown_SchoolContactUserList(0);
        //    //        Grid_School.Rebind();
        //    //        Grid_HostList.Rebind();
        //}


        protected void btn_CancelRoom_Click(object sender, EventArgs e)
        {
            //Clear Host Room Form
            ClearHostRoomForm();
        }

        protected void btn_CancelBed_Click(object sender, EventArgs e)
        {
            //Clear Host Bed Form
            ClearHostBedForm();
        }

        protected void btn_cancel_shcool_Click(object sender, EventArgs e)
        {
            //Clear Host Preferred School Form
            //  ClearHostPreferredSchoolForm();
        }
        protected void FillFormBySelectedHostid(int hostid)
        {
            if (Session["DormitoryHostid"] != null)
            {

                var Dormitory = (new CDormitoryHost()).Get(hostid);
                //DormitoryHost Dormitory = new DormitoryHost();
                //Dormitory Information

                txtDormitoryName.Text = Dormitory.DormitoryHostName;
                txtHomeAddress.Text = Dormitory.Address;
                txtCity.Text = Dormitory.City;
                txtPostalCode.Text = Dormitory.PostalCode;
                txtPhone.Text = Dormitory.PhoneNumber;
                txtAdditionalPhone.Text = Dormitory.AdditionalPhone;
                txtCoordianatorName.Text = Dormitory.CoordinatorName;
                txtCoordianatorPhone.Text = Dormitory.CoordinatorPhone;
                ddlSchoolName.SelectedValue = Dormitory.SchoolName;
                DatePickerLeaseStartDate.SelectedDate = Dormitory.LeaseStartDate;
                DatePickerLeaseEndDate.SelectedDate = Dormitory.LeaseEndDate;
                ddlHomeType.SelectedIndex = Convert.ToInt32(Dormitory.HomeType);
                ddlProvice.SelectedValue = Dormitory.Province;
                //School
                var cHostSchool = new CDormitoryHostPreferredSchool();
                DormitoryHostPrefferedSchool HostSchool = cHostSchool.GetHostTopSchool(hostid);
                ddlSchoolName.SelectedValue = HostSchool.SiteLocationId.ToString();

                // Host status
                LoadDropListHostStatus();
                ddlHostStatus.SelectedValue = Convert.ToInt32(Dormitory.HostStatus).ToString();// HomestayHostStatus(Convert.ToInt32(Dormitory.HostStatus.ToString()));

                if (Dormitory.HostStatus == 1 || Dormitory.HostStatus == 0)
                {
                    DatePickStatusDate.SelectedDate = Dormitory.ActiveDate;
                }
                else
                {
                    DatePickStatusDate.SelectedDate = Dormitory.InactiveDate;
                }
                if (Dormitory.HostStatus == 1)
                {
                    DatePickActiveDate.SelectedDate = Dormitory.ActiveDate;
                }
                else if (Dormitory.HostStatus == 2)
                {
                    DatePickInactiveDate.SelectedDate = Dormitory.InactiveDate;
                }
                else if (Dormitory.HostStatus == 0)
                {
                    DatePickActiveDate.SelectedDate = null;
                    DatePickInactiveDate.SelectedDate = null;
                }

                Grid_HostRoom.Rebind();

                Grid_HostBed.Rebind();

                Grid_DormitoryPlacement.Rebind();
            }
            else
            {
                ShowMessage("Session time out.");

            }

        }
        protected string DormitoryHostStatus(int Status)
        {
            string HostStatus = "0";
            switch (Status)
            {
                case 0:
                    HostStatus = "Pending";
                    break;
                case 1:
                    HostStatus = "Active";
                    break;
                case 2:
                    HostStatus = "Inactive";
                    break;
            }
            return HostStatus;
        }


        protected string HomestayHostStatus(int Status)
        {
            string HostStatus = "0";
            switch (Status)
            {
                case 0:
                    HostStatus = "Pending";
                    break;
                case 1:
                    HostStatus = "Active";
                    break;
                case 2:
                    HostStatus = "Inactive";
                    break;
            }
            return HostStatus;
        }

        protected void Grid_FamilyMember_ItemDataBound(object sender, GridItemEventArgs e)
        {

            //        if (e.Item is GridDataItem)
            //        {

            //            GridDataItem DataItem = e.Item as GridDataItem;
            //            Boolean Gender = Convert.ToBoolean(DataItem["FamilyMember_Gender"].Text.ToString().Trim());
            //            if (Gender)
            //            {
            //                DataItem["FamilyMember_Gender"].Text = "Male";
            //            }
            //            else
            //            {
            //                DataItem["FamilyMember_Gender"].Text = "Female";
            //            }

            //        }
        }


        protected void Grid_HostRoom_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Session["DormitoryHostid"] != null)
            {
                List<DormitoryHostRoom> HostRoom = new List<DormitoryHostRoom>();
                var cDormitoryHostRoom = new CDormitoryHostRoom();
                HostRoom = cDormitoryHostRoom.GetHostRoomList(Convert.ToInt32(Session["DormitoryHostid"].ToString()));
                Grid_HostRoom.DataSource = HostRoom;
            }

        }

        protected void Grid_HostRoom_ItemDataBound(object sender, GridItemEventArgs e)
        {


        }


        protected void Grid_HostRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Grid_HostRoom.SelectedValue != null)
            {
                int HostRoomId = Convert.ToInt32(Grid_HostRoom.SelectedValue.ToString());
                Session["HostRoomId"] = HostRoomId;

                var cDormitory_HostRoom = new CDormitoryHostRoom();
                DormitoryHostRoom Host_Room = cDormitory_HostRoom.Get(HostRoomId);

                txtRoomName.Text = Host_Room.HostRoomName;

                ddlRoomLocation.SelectedValue = Convert.ToInt32(Host_Room.HostRoomFloor).ToString();

                ddlRoomType.SelectedValue = Convert.ToUInt32(Convert.ToBoolean(Host_Room.HostRoomType)).ToString();

            }
        }
        protected string FloorName(int RoomFloor)
        {
            string FloorName = string.Empty;
            //        switch (RoomFloor)
            //        {
            //            case 1:
            //                FloorName = "First Floor";
            //                break;
            //            case 2:
            //                FloorName = "Second Floor";
            //                break;
            //            case 3:
            //                FloorName = "Third Floor";
            //                break;
            //            case 4:
            //                FloorName = "Other Floor";
            //                break;
            //            case 5:
            //                FloorName = "Basement";
            //                break;

            //        }
            return FloorName;
        }



        protected void Grid_HostBed_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Session["DormitoryHostid"] != null)
            {
                var cDormitoryHostBed = new CDormitoryHostBed();
                Grid_HostBed.DataSource = cDormitoryHostBed.DormitoryHostBedList(Convert.ToInt32(Session["DormitoryHostid"].ToString()));
            }

        }

        protected void Grid_HostBed_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //        if (e.Item is GridDataItem)
            //        {
            //            GridDataItem DataItem = e.Item as GridDataItem;

            //            int RoomFloor = Convert.ToInt32(DataItem["HostRoomFloor"].Text.ToString());

            //            DataItem["HostRoomFloor"].Text = FloorName(RoomFloor);

            //            int BedId = Convert.ToInt32(DataItem["HostBedId"].Text);


            //            var cBed = new CDormitoryHostBed();
            //            RadGrid GridPlacement = (RadGrid)DataItem.FindControl("GridPlaced");
            //            if (GridPlacement != null)
            //            {
            //                GridPlacement.DataSource = cBed.GetDormitoryBedPlaced(BedId);
            //                GridPlacement.Rebind();

            //            }


            //        }

        }

        protected void Grid_HostBed_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Grid_HostBed.SelectedValue != null)
            {
                int BedId = Convert.ToInt32(Grid_HostBed.SelectedValue.ToString());
                if (BedId > 0)
                {
                    Session["HostBedId"] = BedId;
                }
                var cDormitoryHostBed = new CDormitoryHostBed();

                DormitoryHostBed host_bed = cDormitoryHostBed.Get(BedId);
                int roomid = Convert.ToInt32(host_bed.HostRoomId);


                txtBedName.Text = host_bed.BedName;
                Dropdown_RoomLocation();
                ddlRoom.SelectedValue = roomid.ToString();



            }
        }

        protected void Grid_School_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        protected void Grid_School_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //if (Session["DormitoryHostid"] != null)
            //{
            //    var schoolList = new CDormitoryHostPreferredSchool();
            //    Grid_School.DataSource = schoolList.DormitoryHostPreferredSchoolList(Convert.ToInt32(Session["DormitoryHostid"].ToString()));


            //}

        }

        protected void Grid_School_SelectedIndexChanged(object sender, EventArgs e)
        {
            //        if (Grid_School.SelectedValue != null)
            //        {
            //            int SchoolId = 0;
            //            SchoolId = Convert.ToInt32(Grid_School.SelectedValue);
            //            if (SchoolId > 0)
            //            {
            //                Session["HostSchoolId"] = SchoolId;
            //                var cHostSchool = new CDormitoryHostPreferredSchool();
            //                var HostSchool = cHostSchool.Get(SchoolId);
            //                Dropdown_ShcoolList();

            //                ddlSchoolName.SelectedValue = HostSchool.SiteLocationId.ToString();

            //                Dropdown_SchoolContactUserList(Convert.ToInt32(HostSchool.SiteLocationId));
            //                ddlSchoolContactName.SelectedValue = HostSchool.ContactUserId.ToString();
            //                txtDistanceToSchool.Text = HostSchool.DistanceSchool.ToString();
            //                txtDistanceBusSubway.Text = HostSchool.DistanceStation.ToString();
            //                txtMajorIntersection.Text = HostSchool.MajorIntersection.ToString();
            //            }



            //        }
        }

        protected void ddlSchoolName_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            //        int SchoolLocationId = 0;
            //        SchoolLocationId = Convert.ToInt32(ddlSchoolName.SelectedValue);
            //        if (SchoolLocationId > 0)
            //        {

            //            Dropdown_SchoolContactUserList(SchoolLocationId);

            //        }
            //    }

            //    protected void Dropdown_SchoolContactUserList(int SiteLocationId)
            //    {
            //        ddlSchoolContactName.Items.Clear();


            //        var cglobal = new CGlobal();
            //        ddlSchoolContactName.DataSource = cglobal.LoadSchooContactlList(SiteLocationId);
            //        ddlSchoolContactName.DataTextField = "Name";
            //        ddlSchoolContactName.DataValueField = "Value";
            //        ddlSchoolContactName.SelectedIndex = 0;
            //        ddlSchoolContactName.DataBind();

            //    }

            //    protected void Tab_Host_TabClick(object sender, RadTabStripEventArgs e)
            //    {
            //        if (Session["DormitoryHostid"]!= null)
            //        {
            //            switch (Convert.ToInt32(Tab_Host.SelectedIndex))
            //            {
            //                //case 0:
            //                //    break;

            //                case 1:
            //                    Grid_HostRoom.Rebind();
            //                    break;
            //                case 2:
            //                    Grid_HostBed.Rebind();
            //                    break;
            //                case 3:
            //                    Grid_School.Rebind();
            //                    break;
            //                case 4:
            //                    break;

            //            }
            //        }

        }

        protected void btn_register_Click(object sender, EventArgs e)
        {
            if (Session["DormitoryHostid"] != null)
            {
                var cDormitoryHostBasic = new CDormitoryHost();
                DormitoryHost host_basic = new DormitoryHost();
                host_basic = cDormitoryHostBasic.Get(Convert.ToInt32(Session["DormitoryHostid"].ToString()));
                //Host status
                host_basic.HostStatus = Convert.ToInt32(ddlHostStatus.SelectedValue); //pending=0, active=1,inactive=2
                if (host_basic.HostStatus == 1 || host_basic.HostStatus == 0)
                {
                    host_basic.ActiveDate = DatePickStatusDate.SelectedDate;
                    if (host_basic.HostStatus == 1)
                    {
                        DatePickActiveDate.SelectedDate = DatePickStatusDate.SelectedDate;
                    }

                }
                else
                {
                    host_basic.InactiveDate = DatePickStatusDate.SelectedDate;
                    DatePickInactiveDate.SelectedDate = DatePickStatusDate.SelectedDate;
                }

                host_basic.UpdatedId = CurrentUserId;
                host_basic.UpdatedDate = DateTime.Now;
                bool HB_Update = cDormitoryHostBasic.Update(host_basic);
                if (HB_Update)
                {
                    EnableTab(true, true);
                    ShowMessage("Dormitory host basic information is updated successfully.");
                }
                else
                {
                    ShowMessage("Failed to update Host Basic Information, please try it again.");
                }
                //LoadDropListHostStatus();
                //DatePickStatusDate.SelectedDate = DateTime.Today;
                Grid_HostList.Rebind();

            }
        }
        protected void LoadDropListHostStatus()
        {
            ddlHostStatus.Items.Clear();
            ddlHostStatus.Items.Add(new DropDownListItem("Pending", "0"));
            ddlHostStatus.Items.Add(new DropDownListItem("Active", "1"));
            ddlHostStatus.Items.Add(new DropDownListItem("Inactive", "2"));


        }
        protected void Grid_HostList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowHostDetail();
        }

        protected void Grid_HostList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem DataItem = e.Item as GridDataItem;
                int hostid = Convert.ToInt32(DataItem["DormitoryHostId"].Text.ToString());
                int status = Convert.ToInt32(DataItem["HostStatus"].Text.ToString().Trim());
                DataItem["HostStatus"].Text = DormitoryHostStatus(status);
                
                //Room Number
                RadLabel lblRoomNumber = (RadLabel)DataItem.FindControl("lbl_RoomNumber");
                var cHostRoom = new CDormitoryHostRoom();
                int RoomNumber = 0;
                RoomNumber = cHostRoom.GetDormitoryHostRoomNumber(hostid);
                lblRoomNumber.Text = RoomNumber.ToString();
                //Bed Number
                RadLabel lblBedNumber = (RadLabel)DataItem.FindControl("lbl_BedNumber");
                var cHostBed = new CDormitoryHostBed();
                int BedNumber = 0;
                BedNumber = cHostBed.GetDormitoryHostBedNumber(hostid);
                lblBedNumber.Text = BedNumber.ToString();
            }
        }

        protected void Grid_HostList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var cDormitory = new CDormitoryHost();

            Grid_HostList.DataSource = cDormitory.GetDormitoryHostList(CurrentSiteLocationId);

        }


    }
}