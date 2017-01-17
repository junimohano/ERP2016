using System;
using System.Collections.Generic;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.StudentHousing
{
    public partial class HomestayHostRegistration : PageBase
    {
        public HomestayHostRegistration() : base((int)CConstValue.Menu.HomestayHostRegistration)
        { }

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                var UserId = (new CUser()).Get(CurrentUserId);
                var SiteId = (new CSite()).Get(CurrentSiteId);

                var fname = UserId.FirstName;
                var lname = UserId.LastName;

                DatePickFatherDOB.SelectedDate = DateTime.Today;
                DatePickMemberDOB.SelectedDate = DateTime.Today;
                DatePickMemberDOB.SelectedDate = DateTime.Today;
                DatePickStatusDate.SelectedDate = DateTime.Today;

                // Load Dropdown List
                Dropdown_Province();
                Dropdown_ShcoolList();

                Dropdown_Campus();
                EnableTab(false, false);
                //VisiableBtn(false);

                PageView_Basic.Selected = true;
                //SiteLocation()
                IniSiteLocation();

            }
        }

        protected void IniSiteLocation()
        {
            LoadSiteLocation(0);
            LoadSite(CurrentSiteId);

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
            ddlSchoolName.SelectedIndex = 0;
            ddlSchoolName.DataBind();
        }


        protected void Dropdown_RoomLocation()
        {
            ddlRoom.Items.Clear();
            var cglobale = new CGlobal();
            int hostid = 0;


            if (Session["hostid"] != null)
            {
                hostid = Convert.ToInt32(Session["hostid"].ToString());
                ddlRoom.DataSource = cglobale.LoadRoomList(hostid);
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
        protected void Grid_HomestayPlacement_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Grid_HostList.SelectedValue != null)
            {

                var cHomestayPlament = new CHomestayPlacement();
                Grid_HomestayPlacement.DataSource = cHomestayPlament.GetHomestayPlacementByHostId(Convert.ToInt32(Grid_HostList.SelectedValue));
                Grid_HomestayPlacement.Visible = true;
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
        protected void Grid_HostList_FilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        //public static void SetFilterCheckListItems(GridFilterCheckListItemsRequestedEventArgs e)
        //{
        //    object dataSource = null;
        //    string dataField = (e.Column as IGridDataColumn).GetActiveDataField();


        //    var cHomestay_HostBasic = new CHomestay_HostBasic();

        //    switch (dataField)
        //    {
        //        // Common

        //        case "FatherName":
        //            //var cHomestay_HostBasic = new CHomestay_HostBasic();
        //            dataSource = cHomestay_HostBasic.GetFatherNameList();
        //            break;

        //        case "RegistrationDate":

        //            dataSource = cHomestay_HostBasic.GetHostRegistrationDateList();
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

                Session["hostid"] = HostID;
                SetSessionNull();
                FillFormBySelectedHostid(HostID);
                Dropdown_RoomLocation();
                Dropdown_ShcoolList();
                ddlSchoolName.SelectedIndex = 0;
                EnableTab(true, true);
                //VisiableBtn(false);
                ClearForms();



            }
        }
        protected void LoadSiteLocation(int siteId)
        {
            var global = new CGlobal();
            RadComboBoxSiteLocation.Items.Clear();
            RadComboBoxSiteLocation.Text = string.Empty;
            RadComboBoxSiteLocation.DataSource = global.GetSiteLocationBySiteId(siteId);
            RadComboBoxSiteLocation.DataTextField = "Name";
            RadComboBoxSiteLocation.DataValueField = "Value";
            RadComboBoxSiteLocation.DataBind();
        }
        protected void LoadSite(int siteId)
        {
            var global = new CGlobal();
            RadComboBoxSite.Items.Clear();
            RadComboBoxSite.Text = string.Empty;
            RadComboBoxSite.DataSource = global.GetSiteId(siteId);
            RadComboBoxSite.DataTextField = "Name";
            RadComboBoxSite.DataValueField = "Value";
            RadComboBoxSite.DataBind();
        }

        protected void RadComboBoxSite_OnSelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadSiteLocation(Convert.ToInt32(RadComboBoxSite.SelectedValue));
            RadComboBoxSiteLocation.OpenDropDownOnLoad = true;
        }

        protected void EnableTab(bool bl, bool fl)
        {
            Tab_Host.Tabs[1].Enabled = bl;
            Tab_Host.Tabs[2].Enabled = bl;
            Tab_Host.Tabs[3].Enabled = bl;
            Tab_Host.Tabs[4].Enabled = bl;
            Tab_Host.Tabs[5].Enabled = fl;
            Tab_Host.Tabs[6].Enabled = bl;

        }
        //public override void SetVisibleModifyControllers()
        //{
        //    //if (UserPermissionModel.IsModify == false)
        //    //{
        //    //    // toolbar
        //    //    foreach (RadToolBarItem toolbarItem in RadToolBarProgram.Items)
        //    //   {
        //    //        toolbarItem.Enabled = false;
        //    //   }


        //    //}
        //}


        protected bool VerifyBasicForm()
        {
            bool result = false;
            int father_firstnameLen = txtFatherFirstName.Text.ToString().Trim().Length;
            int father_lastnameLen = txtFatherLastName.Text.ToString().Trim().Length;
            int mother_firstnameLen = txtMotherFirstName.Text.ToString().Trim().Length;
            int mother_lastnameLen = txtMotherLastName.Text.ToString().Trim().Length;
            int home_addressLen = txtHomeAddress.Text.ToString().Trim().Length;
            int home_cityLen = txtCity.Text.ToString().Trim().Length;
            int home_ProvinceSelectedIndex = ddlProvice.SelectedIndex;
            int home_PostalCodeLen = txtPostalCode.Text.ToString().Trim().Length;
            int home_phoneLen = txtHomePhone.Text.ToString().Trim().Length;


            if ((((father_firstnameLen > 0 && father_lastnameLen > 0) && (mother_firstnameLen == 0 && mother_lastnameLen == 0))) || (father_lastnameLen > 0 && father_lastnameLen > 0 && mother_firstnameLen > 0 && mother_lastnameLen > 0) || ((father_firstnameLen == 0 && father_lastnameLen == 0) && (mother_firstnameLen > 0 && mother_lastnameLen > 0)))
            {

                if (home_addressLen > 0 && home_cityLen > 0 && home_ProvinceSelectedIndex > 0 && home_PostalCodeLen > 0)
                {

                    if (home_phoneLen > 0)
                    {
                        result = true;
                    }
                    else
                    {

                        ShowMessage("Please input Home Phone.");
                    }

                }
                else
                {
                    ShowMessage("Please input Home Address or city or province or postal caode.");
                }

            }
            else
            {

                ShowMessage("Please input Host Name");
            }


            return result;
        }

        protected bool VerifyMemberForm()
        {
            bool result = false;

            int Member_firstNameLen = txtMemberFirstName.Text.ToString().Trim().Length;
            int Member_lastNameLen = txtMemberLastName.Text.ToString().Trim().Length;
            if (Member_firstNameLen == 0 && Member_lastNameLen == 0)
            {
                result = true;
            }
            else
            {
                if (txtMemberRelationship.Text.ToString().Trim().Length > 0)
                {
                    if (Member_firstNameLen > 0 && Member_lastNameLen > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        ShowMessage("Please input First Name and Last Name");
                    }
                }
                else
                {
                    ShowMessage("Please input Relationship.");
                }
            }

            return result;

        }

        protected void btn_basic_save_Click(object sender, EventArgs e)
        {

            var cHomestay_basic = new CHomestayHostBasic();
            int hostid = 0;  //Homestay HostId

            if (Session["hostid"] != null)
            {
                hostid = Convert.ToInt32(Session["hostid"].ToString());
            }

            //if (VerifyBasicForm())
            //{
            if (hostid == 0) //new host registration
            {
                var HB = new HomestayHostBasic();
                //Father's Information
                HB.FatherFirstName = txtFatherFirstName.Text;
                HB.FatherLastName = txtFatherLastName.Text;
                HB.FatherDOB = DatePickFatherDOB.SelectedDate;
                HB.FatherOccupation = txtFatherOccupation.Text;

                HB.FatherCRC = Convert.ToBoolean(Convert.ToInt32((ddlFatherCRC.SelectedValue)));
                HB.FatherGuardian = Convert.ToBoolean(Convert.ToInt32(ddlFatherGuardian.SelectedValue.ToString()));
                HB.FathereMail = txtFatherEmail.Text;
                //Mother's Information
                HB.MotherFirstName = txtMotherFirstName.Text;
                HB.MotherLastName = txtMotherLastName.Text;
                HB.MotherDOB = DatePickMotherDOB.SelectedDate;
                HB.MotherOccupation = txtMotherOccupation.Text;
                HB.MotherCRC = Convert.ToBoolean(Convert.ToInt32((ddlMotherCRC.SelectedValue)));
                HB.MotherGuardian = Convert.ToBoolean(Convert.ToInt32((ddlMotherGuardian.SelectedValue)));
                HB.MothereMail = txtMotherEmail.Text;
                //Host Home Information
                HB.HouseAddress = txtHomeAddress.Text;
                HB.HouseCity = txtCity.Text;
                HB.HouseProvince = ddlProvice.SelectedValue;
                HB.HousePostalCode = txtPostalCode.Text;
                HB.HousePhone = txtHomePhone.Text;
                HB.FatherWorkPhone = txtWorkPhone.Text;
                HB.FatherCellPhone = txtCellPhone.Text;
                HB.MotherCellPhone = txtAdditionalPhone.Text; //Additional Phone
                HB.HouseEglishSpoken = Convert.ToBoolean(Convert.ToInt32((ddlEnligsh.SelectedValue)));
                HB.HouseLanguageSpoken = txtLanguage.Text;
                //Host Options Information
                HB.OptionsSmokingPermitted = Convert.ToBoolean(Convert.ToInt32((ddlSmoking.SelectedValue)));
                HB.OptionsPetFlag = Convert.ToBoolean(Convert.ToInt32((ddlPet.SelectedValue)));
                HB.HouseAlarmSystem = Convert.ToBoolean(Convert.ToInt32(ddlHomeAlarm.SelectedValue));
                HB.OptionsInternetOffered = Convert.ToBoolean(Convert.ToInt32(ddlInternetOffered.SelectedValue));
                HB.OptionsInternet_Type = ddlInternetType.SelectedValue;
                HB.OptionsInternetExtraCharge = Convert.ToInt32(txtExtraCharge.Text);
                HB.OptionsInternet_Usage = ddlUsage.SelectedValue;
                //Host Emergency Contact
                HB.AdminHostEmergencyPerson = txtContactName.Text;
                HB.AdminCompanyPersonPhone = txtContactPhone.Text;
                HB.AdminHostEmergencyReleationship = txtRelationship.Text;
                //HB.FamilyMemberNumber = 0;
                // Host status
                HB.HouseActiveStutas = 0;// Pending
                HB.HouseActiveDate = DateTime.Now; //

                //Host Meal Plan and Fridge
                HB.MealPlanBreakfastType = Convert.ToBoolean(Convert.ToInt32(ddlBreakfast.SelectedValue));
                HB.MealPlanLunchType = Convert.ToBoolean(Convert.ToInt32(ddlLunch.SelectedValue));
                HB.MealPlanDinnerType = Convert.ToBoolean(Convert.ToInt32(ddlDinner.SelectedValue));
                HB.FridgeSharedSerperatedAccess = Convert.ToInt32(ddlFridge.SelectedValue);

                //Create date, Created user
                HB.CreatedId = CurrentUserId;
                HB.CreatedDate = DateTime.Now;

                //SiteLocationId
                HB.SiteLocationId = Convert.ToInt32(RadComboBoxSiteLocation.SelectedValue);
                HB.PreferredGender = Convert.ToInt32(RDLHostGender.SelectedValue);
                HB.HostRanking = Convert.ToInt32(txtRanking.Text);
                int HB_Add = cHomestay_basic.Add(HB);
                if (HB_Add == -1)
                {
                    ShowMessage("Failed to add Host Basic Information, please try it again");
                }
                else
                {
                    int maxhostid = cHomestay_basic.MaxHostId();
                    if (maxhostid > 0)
                    {
                        Session["hostid"] = maxhostid;
                        // test room location
                        //Default School Information
                        file_upload.SaveFile(maxhostid);
                        var cHomesatyPreferredSchool = new CHomestayHostPreferredSchool();
                        HomestayHostPrefferedSchool School = new HomestayHostPrefferedSchool();
                        School.HostId = maxhostid;
                        School.SiteLocationId = CurrentSiteLocationId;
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

                            ShowMessage("Host Basic Information is saved successfully.");

                        }

                    }

                    // Grid_HostList.Rebind();
                }
            }
            else    //update host registration
            {


                var host_basic = cHomestay_basic.Get(hostid);
                host_basic.HostId = hostid;

                host_basic.FatherFirstName = txtFatherFirstName.Text;
                host_basic.FatherLastName = txtFatherLastName.Text;
                host_basic.FatherDOB = DatePickFatherDOB.SelectedDate;
                host_basic.FatherOccupation = txtFatherOccupation.Text;

                host_basic.FatherCRC = Convert.ToBoolean(Convert.ToInt32((ddlFatherCRC.SelectedValue)));
                host_basic.FatherGuardian = Convert.ToBoolean(Convert.ToInt32(ddlFatherGuardian.SelectedValue.ToString()));
                host_basic.FathereMail = txtFatherEmail.Text;
                //Mother's Information
                host_basic.MotherFirstName = txtMotherFirstName.Text;
                host_basic.MotherLastName = txtMotherLastName.Text;
                host_basic.MotherDOB = DatePickMotherDOB.SelectedDate;
                host_basic.MotherOccupation = txtMotherOccupation.Text;
                host_basic.MotherCRC = Convert.ToBoolean(Convert.ToInt32((ddlMotherCRC.SelectedValue)));
                host_basic.MotherGuardian = Convert.ToBoolean(Convert.ToInt32((ddlMotherGuardian.SelectedValue)));
                host_basic.MothereMail = txtMotherEmail.Text;
                //Host Home Information
                host_basic.HouseAddress = txtHomeAddress.Text;
                host_basic.HouseCity = txtCity.Text;
                host_basic.HouseProvince = ddlProvice.SelectedValue;
                host_basic.HousePostalCode = txtPostalCode.Text;
                host_basic.HousePhone = txtHomePhone.Text;
                host_basic.FatherWorkPhone = txtWorkPhone.Text;
                host_basic.FatherCellPhone = txtCellPhone.Text;
                host_basic.MotherCellPhone = txtAdditionalPhone.Text; //Additional Phone
                host_basic.HouseEglishSpoken = Convert.ToBoolean(Convert.ToInt32((ddlEnligsh.SelectedValue)));
                host_basic.HouseLanguageSpoken = txtLanguage.Text;
                //Host Options Information
                host_basic.OptionsSmokingPermitted = Convert.ToBoolean(Convert.ToInt32((ddlSmoking.SelectedValue)));
                host_basic.OptionsPetFlag = Convert.ToBoolean(Convert.ToInt32((ddlPet.SelectedValue)));
                host_basic.HouseAlarmSystem = Convert.ToBoolean(Convert.ToInt32(ddlHomeAlarm.SelectedValue));
                host_basic.OptionsInternetOffered = Convert.ToBoolean(Convert.ToInt32(ddlInternetOffered.SelectedValue));
                host_basic.OptionsInternet_Type = ddlInternetType.SelectedValue;
                host_basic.OptionsInternetExtraCharge = Convert.ToInt32(txtExtraCharge.Text);
                host_basic.OptionsInternet_Usage = ddlUsage.SelectedValue;
                //Host Emergency Contact
                host_basic.AdminHostEmergencyPerson = txtContactName.Text;
                host_basic.AdminCompanyPersonPhone = txtContactPhone.Text;
                host_basic.AdminHostEmergencyReleationship = txtRelationship.Text;
                //host_basic.FamilyMemberNumber = 0;


                //Host Meal Plan and Fridge
                host_basic.MealPlanBreakfastType = Convert.ToBoolean(Convert.ToInt32(ddlBreakfast.SelectedValue));
                host_basic.MealPlanLunchType = Convert.ToBoolean(Convert.ToInt32(ddlLunch.SelectedValue));
                host_basic.MealPlanDinnerType = Convert.ToBoolean(Convert.ToInt32(ddlDinner.SelectedValue));
                host_basic.FridgeSharedSerperatedAccess = Convert.ToInt32(ddlFridge.SelectedValue);

                host_basic.UpdatedDate = DateTime.Now;
                host_basic.UpdatedId = CurrentUserId;

                //SiteLocationId
                host_basic.SiteLocationId = Convert.ToInt32(RadComboBoxSiteLocation.SelectedValue);
                host_basic.PreferredGender = Convert.ToInt32(RDLHostGender.SelectedValue);
                host_basic.HostRanking = Convert.ToInt32(txtRanking.Text);

                bool HB_Update = cHomestay_basic.Update(host_basic);
                if (HB_Update)
                {
                    file_upload.SaveFile(hostid);
                    //VisiableBtn(false);
                    EnableTab(true, true);
                    ShowMessage("Homestay host basic information is updated successfully.");
                }
                else
                {
                    ShowMessage("Failed to update Host Basic Information, please try it again.");
                }

            }

            ClearHostBasicForm();// Clear up the Basic Form
            //}

        }

        protected void btn_registration_Click(object sender, EventArgs e)

        {

            Session["hostid"] = null;

            Grid_FamilyMember.Rebind();
            Grid_HostRoom.Rebind();
            Grid_HostBed.Rebind();
            Grid_School.Rebind();


            //Clear Host Basic Information
            ClearHostBasicForm();

            //load dropdown lists
            Dropdown_Province();
            Dropdown_ShcoolList();
            Dropdown_RoomLocation();
            ddlSchoolName.SelectedValue = "0";

            SetSessionNull();
            //Clear Forms
            ClearForms();
            Tab_Host.SelectedIndex = 0;
            EnableTab(false, false);

            PageView_Basic.Selected = true;
            IniSiteLocation();
            RadComboBoxSite.Enabled = true;
            RadComboBoxSiteLocation.Enabled = true;
            //VisiableBtn(false);
        }


        protected void SetSessionNull()
        {
            Session["FamilyMemberId"] = null;
            Session["HostRoomId"] = null;
            Session["HostBedId"] = null;
            Session["HostSchoolId"] = null;
        }
        protected void btn_Member_Click(object sender, EventArgs e)
        {
            if (VerifyMemberForm())
            {
                int hostid = 0;
                if (Session["hostid"] != null)
                {
                    hostid = Convert.ToInt32(Session["hostid"].ToString());

                    var HM = new HomestayHostFamilyMember();
                    HM.HostId = hostid;
                    HM.MemberFirstName = txtMemberFirstName.Text;
                    HM.MemberLastName = txtMemberLastName.Text;
                    HM.MemberDOB = DatePickMemberDOB.SelectedDate;
                    HM.MemberGender = Convert.ToBoolean(Convert.ToInt32(ddlMemberGender.SelectedValue));
                    HM.MemberCRC = Convert.ToBoolean(Convert.ToInt32(ddlMemberCRC.SelectedValue));
                    HM.MemberLivingHome = Convert.ToBoolean(Convert.ToInt32(ddlLivingHome.SelectedValue));
                    HM.Relationship = txtMemberRelationship.Text;

                    int hostMemberid = 0;
                    if (Session["FamilyMemberId"] != null)
                    {
                        hostMemberid = Convert.ToInt32(Session["FamilyMemberId"].ToString());
                    }
                    var cHomestay_HostFamilyMember = new CHomestayHostFamilyMember();

                    //Save
                    if (hostMemberid == 0) //new
                    {
                        HM.CreatedId = CurrentUserId;
                        HM.CreatedDate = DateTime.Now;

                        int HM_Save = cHomestay_HostFamilyMember.Add(HM);

                        if (HM_Save == -1)
                        {
                            ShowMessage("Failed to add Host Family Member, please try it again.");
                        }
                        else
                        {
                            ShowMessage("Host Family Member is added successfully.");
                        }

                    }
                    else //update
                    {

                        var host_member = cHomestay_HostFamilyMember.Get(hostMemberid);

                        host_member.FamilyMemberId = hostMemberid;
                        host_member.MemberFirstName = txtMemberFirstName.Text;
                        host_member.MemberLastName = txtMemberLastName.Text;
                        host_member.MemberCRC = Convert.ToBoolean(Convert.ToInt32(ddlMemberCRC.SelectedValue));
                        host_member.MemberDOB = DatePickMemberDOB.SelectedDate;
                        host_member.UpdatedId = CurrentUserId;
                        host_member.UpdatedDate = DateTime.Now;
                        host_member.Relationship = txtMemberRelationship.Text;


                        Boolean HM_Update = cHomestay_HostFamilyMember.Update(host_member);
                        if (HM_Update)
                        {
                            ShowMessage("Host Family Member is updated successfully.");

                        }
                        else
                        {
                            ShowMessage("Failed to update Host Family Member, please try it again.");
                        }
                    }


                    ClearHostFamilyMemberForm(); // Clear up Family Member Form
                }
            }

        }

        protected void btn_AddRoom_Click(object sender, EventArgs e)
        {
            int hostid = 0;
            if (Session["hostid"] != null)
            {
                hostid = Convert.ToInt32(Session["hostid"].ToString());

                int hostRoomid = 0;
                if (Session["HostRoomId"] != null)
                {
                    hostRoomid = Convert.ToInt32(Session["HostRoomId"].ToString());
                }

                var cHomestay_HostRoom = new CHomestayHostRoom();
                if (hostRoomid == 0) //new room
                {
                    var HRoom = new HomestayHostRoom();
                    hostid = Convert.ToInt32(Session["hostid"].ToString());
                    HRoom.HostId = hostid;
                    HRoom.HostRoomName = txtRoomName.Text;
                    HRoom.HostRoomFloor = Convert.ToInt32(ddlRoomLocation.SelectedValue);
                    HRoom.HostRoomType = Convert.ToBoolean(Convert.ToInt32(ddlRoomType.SelectedValue));
                    HRoom.CreatedId = CurrentUserId;
                    HRoom.CreatedDate = DateTime.Now;
                    int HRoom_Save = cHomestay_HostRoom.Add(HRoom);
                    if (HRoom_Save == -1)
                    {
                        ShowMessage("Failed to add Host Room, please try it again.");
                    }
                    else
                    {
                        ShowMessage("Host Room is added successfully.");
                    }
                }
                else //update room
                {
                    var host_room = cHomestay_HostRoom.Get(hostRoomid);
                    host_room.HostRoomId = hostRoomid;
                    host_room.HostRoomName = txtRoomName.Text;
                    host_room.HostRoomFloor = Convert.ToInt32(ddlRoomLocation.SelectedValue);
                    host_room.HostRoomType = Convert.ToBoolean(Convert.ToInt32(ddlRoomType.SelectedValue));
                    host_room.UpdatedId = CurrentUserId;
                    host_room.UpdatedDate = DateTime.Now;
                    Boolean HRoom_Update = cHomestay_HostRoom.Update(host_room);
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
            if (Session["hostid"] != null)
            {

                int hostid = 0;
                hostid = Convert.ToInt32(Session["hostid"].ToString());

                int hostBedid = 0;
                if (Session["HostBedId"] != null)
                {
                    hostBedid = Convert.ToInt32(Session["HostBedId"]);
                }
                var cHomestay_HostBed = new CHomestayHostBed();
                if (hostBedid == 0) //new bed
                {
                    var HBed = new HomestayHostBed();
                    HBed.HostId = hostid;
                    HBed.BedName = txtBedName.Text;
                    HBed.HostRoomId = Convert.ToInt32(ddlRoom.SelectedValue);
                    HBed.CreatedId = CurrentUserId;
                    HBed.CreatedDate = DateTime.Now;
                    int HBed_Save = cHomestay_HostBed.Add(HBed);
                    if (HBed_Save == -1)
                    {
                        ShowMessage("Failed to add Host Bed, please try it again.");
                    }
                    else
                    {
                        ShowMessage("Host Bed is added successfully.");
                    }
                }
                else //update bed
                {
                    var host_bed = cHomestay_HostBed.Get(hostBedid);
                    host_bed.HostId = hostid;
                    host_bed.HostBedId = hostBedid;
                    host_bed.HostRoomId = Convert.ToInt32(ddlRoom.SelectedValue);
                    host_bed.BedName = txtBedName.Text;
                    host_bed.UpdatedId = CurrentUserId;
                    host_bed.UpdatedDate = DateTime.Now;
                    Boolean HBed_Update = cHomestay_HostBed.Update(host_bed);
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
        protected void ClearForms()
        {
            ClearHostPreferredSchoolForm();
            ClearHostFamilyMemberForm();
            ClearHostRoomForm();
            ClearHostBedForm();

        }


        protected void btn_shcool_Click(object sender, EventArgs e)
        {
            if (Session["hostid"] != null)
            {
                int hostid = 0;
                hostid = Convert.ToInt32(Session["hostid"].ToString());
                int hostPrefferedSchoolId = 0;

                if (Session["HostSchoolId"] != null)
                {
                    hostPrefferedSchoolId = Convert.ToInt32(Session["HostSchoolId"].ToString());
                }

                var cHomestay_HostPreferredSchool = new CHomestayHostPreferredSchool();

                if (hostPrefferedSchoolId == 0) //new shcool
                {
                    var HS = new HomestayHostPrefferedSchool();
                    HS.HostId = hostid;
                    HS.HostSchoolId = Convert.ToInt32(ddlSiteLocation.SelectedValue);
                    HS.SiteLocationId = Convert.ToInt32(ddlSchoolName.SelectedValue);
                    HS.ContactUserId = Convert.ToInt32(ddlSchoolContactName.SelectedValue);
                    HS.MajorIntersection = txtMajorIntersection.Text;
                    HS.DistanceSchool = txtDistanceToSchool.Text;
                    HS.DistanceStation = txtDistanceBusSubway.Text;
                    HS.CreatedId = CurrentUserId;
                    HS.CreatedDate = DateTime.Now;
                    int HS_Save = cHomestay_HostPreferredSchool.Add(HS);
                    if (HS_Save == -1)
                    {
                        ShowMessage("Failed to add Host Preferred School, please try it again.");
                    }
                    else
                    {
                        ShowMessage("Host Preferred School is added successfully.");
                    }
                }
                else //update school
                {

                    var host_school = cHomestay_HostPreferredSchool.Get(hostPrefferedSchoolId);
                    host_school.HostSchoolId = hostPrefferedSchoolId;
                    host_school.HostId = Convert.ToInt32(Session["hostid"].ToString());
                    host_school.SiteLocationId = Convert.ToInt32(ddlSchoolName.SelectedValue);
                    host_school.ContactUserId = Convert.ToInt32(ddlSchoolContactName.SelectedValue);
                    host_school.MajorIntersection = txtMajorIntersection.Text;
                    host_school.DistanceSchool = txtDistanceToSchool.Text;
                    host_school.DistanceStation = txtDistanceBusSubway.Text;

                    host_school.UpdatedId = CurrentUserId;
                    host_school.UpdatedDate = DateTime.Now;
                    Boolean HS_Update = cHomestay_HostPreferredSchool.Update(host_school);
                    if (HS_Update)
                    {

                        ShowMessage("Host Preferred School is updated successfully.");
                    }
                    else
                    {
                        ShowMessage("Failed to update Host Preferred School, please try it again.");
                    }
                }

                ClearHostPreferredSchoolForm(); // Clear up Host School Form
            }
        }

        protected void btn_basic_cancel_Click(object sender, EventArgs e)
        {
            ClearHostBasicForm();

        }
        protected void ClearHostBasicForm()
        {
            //Father's Information
            txtFatherFirstName.Text = "";
            txtFatherLastName.Text = "";
            DatePickFatherDOB.SelectedDate = DateTime.Today;
            txtFatherOccupation.Text = "";
            ddlFatherCRC.SelectedValue = "0";
            ddlFatherGuardian.SelectedValue = "0";
            txtFatherEmail.Text = "";
            //Mother's Information
            txtMotherFirstName.Text = "";
            txtMotherLastName.Text = "";
            DatePickMotherDOB.SelectedDate = DateTime.Today;
            txtMotherOccupation.Text = "";
            ddlMotherCRC.SelectedValue = "0";
            ddlMotherGuardian.SelectedValue = "1";
            txtMotherEmail.Text = "";
            //Host Home Information
            txtHomeAddress.Text = "";
            txtCity.Text = "";

            ddlProvice.SelectedIndex = 0;
            txtPostalCode.Text = "";
            txtHomePhone.Text = "";
            txtWorkPhone.Text = "";
            txtCellPhone.Text = "";
            txtAdditionalPhone.Text = ""; //Additional Phone
            ddlEnligsh.SelectedValue = "0";
            txtLanguage.Text = "";
            //Host Options Information

            ddlSmoking.SelectedValue = "0";
            ddlPet.SelectedValue = "0";
            ddlHomeAlarm.SelectedValue = "0";
            ddlInternetOffered.SelectedValue = "0";
            ddlInternetType.SelectedValue = "Wireless";
            txtExtraCharge.Text = "0";
            ddlUsage.SelectedValue = "Unlimited";

            //Host Emergency Contact
            txtContactName.Text = "";
            txtContactPhone.Text = "";
            txtRelationship.Text = "";

            // Host status
            ddlHostStatus.SelectedValue = "0";
            DatePickStatusDate.SelectedDate = DateTime.Today;

            //Host Meal Plan and Fridge
            ddlBreakfast.SelectedValue = "1";
            ddlLunch.SelectedValue = "0";
            ddlDinner.SelectedValue = "0";
            ddlFridge.SelectedValue = "0";

            Grid_HostList.Rebind();

            txtRanking.Text = "0";
            RDLHostGender.SelectedValue = "0";

            file_upload.InitFileDownloadList((int)CConstValue.Upload.HomestayCRC);
            file_upload.GetFileDownload(0);


        }

        protected void btn_cancel_familyMember_Click(object sender, EventArgs e)
        {
            //Clear Host Family Member Form
            ClearHostFamilyMemberForm();
        }
        protected void ClearHostFamilyMemberForm()
        {

            txtMemberFirstName.Text = "";
            txtMemberLastName.Text = "";
            DatePickMemberDOB.SelectedDate = DateTime.Today;
            ddlMemberGender.SelectedValue = "0";
            ddlMotherCRC.SelectedValue = "0";
            ddlLivingHome.SelectedValue = "1";
            txtMemberRelationship.Text = "";
            Session["FamilyMemberId"] = null;
            Grid_FamilyMember.Rebind();
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

        protected void ClearHostPreferredSchoolForm()
        {
            Session["HostSchoolId"] = 0;
            txtDistanceToSchool.Text = "";
            txtDistanceBusSubway.Text = "";
            txtMajorIntersection.Text = "";

            Dropdown_ShcoolList();
            ddlSchoolName.SelectedValue = "0";
            Dropdown_SchoolContactUserList(0);
            Grid_School.Rebind();
            Grid_HostList.Rebind();
        }


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
            ClearHostPreferredSchoolForm();
        }
        protected void FillFormBySelectedHostid(int hostid)
        {
            if (Session["hostid"] != null)
            {

                var Host = (new CHomestayHostBasic()).Get(hostid);

                file_upload.InitFileDownloadList((int)CConstValue.Upload.HomestayCRC);


                // Download File
                file_upload.InitFileDownloadList((int)CConstValue.Upload.HomestayCRC);
                file_upload.SetVisibieUploadControls(true);
                file_upload.GetFileDownload(Convert.ToInt32(hostid));
                file_upload.Visible = true;


                var siteLocation = new CSiteLocation().Get(Convert.ToInt32(Host.SiteLocationId));
                LoadSite(siteLocation.SiteId);
                LoadSiteLocation(siteLocation.SiteId);
                RadComboBoxSite.SelectedValue = siteLocation.SiteId.ToString();
                RadComboBoxSiteLocation.SelectedValue = siteLocation.SiteLocationId.ToString();
                RadComboBoxSite.Enabled = false;
                RadComboBoxSiteLocation.Enabled = false;
                txtRanking.Text = Host.HostRanking.ToString();
                //Father's Information
                txtFatherFirstName.Text = Host.FatherFirstName;
                txtFatherLastName.Text = Host.FatherLastName;
                DatePickFatherDOB.SelectedDate = Host.FatherDOB;
                txtFatherOccupation.Text = Host.FatherOccupation;
                //Preferred Gender
                RDLHostGender.SelectedValue = Host.PreferredGender.ToString();

                //Ranking
                if (Convert.ToBoolean(Host.FatherCRC.ToString()))
                {
                    ddlFatherCRC.SelectedValue = "1";
                }
                else
                {
                    ddlFatherCRC.SelectedValue = "0";
                }

                if (Convert.ToBoolean(Host.FatherGuardian.ToString()))
                {
                    ddlFatherGuardian.SelectedValue = "1";
                }
                else
                {
                    ddlFatherGuardian.SelectedValue = "0";
                }

                txtFatherEmail.Text = Host.FathereMail;
                //Mother's Information
                txtMotherFirstName.Text = Host.MotherFirstName;
                txtMotherLastName.Text = Host.MotherLastName;
                DatePickMotherDOB.SelectedDate = Host.MotherDOB;
                txtMotherOccupation.Text = Host.MotherOccupation;
                if (Convert.ToBoolean(Host.MotherCRC.ToString()))
                {
                    ddlMotherCRC.SelectedValue = "1";
                }
                else
                {
                    ddlMotherCRC.SelectedValue = "0";
                }

                if (Convert.ToBoolean(Host.MotherGuardian.ToString()))
                {
                    ddlMotherGuardian.SelectedValue = "1";
                }
                else
                {
                    ddlMotherGuardian.SelectedValue = "0";
                }
                txtMotherEmail.Text = Host.MothereMail;

                //Host Home Information
                txtHomeAddress.Text = Host.HouseAddress;
                txtCity.Text = Host.HouseCity;

                ddlProvice.SelectedText = Host.HouseProvince;
                ddlProvice.SelectedValue = Host.HouseProvince;

                txtPostalCode.Text = Host.HousePostalCode;
                txtHomePhone.Text = Host.HousePhone;
                txtWorkPhone.Text = Host.FatherWorkPhone;
                txtCellPhone.Text = Host.FatherCellPhone;
                txtAdditionalPhone.Text = Host.MotherCellPhone; //Additional Phone

                ddlEnligsh.SelectedValue = Convert.ToInt32(Convert.ToBoolean(Host.HouseEglishSpoken.ToString())).ToString();

                txtLanguage.Text = Host.HouseLanguageSpoken;
                //Host Options Information

                if (Convert.ToBoolean(Host.OptionsSmokingPermitted.ToString()))
                {
                    ddlSmoking.SelectedValue = "1";
                }
                else
                { ddlSmoking.SelectedValue = "0"; }

                if (Convert.ToBoolean(Host.OptionsPetFlag.ToString()))
                {
                    ddlPet.SelectedValue = "1";
                }
                else
                {
                    ddlPet.SelectedValue = "0";
                }

                if (Convert.ToBoolean(Host.HouseAlarmSystem.ToString()))
                {
                    ddlHomeAlarm.SelectedValue = "1";
                }
                else
                {
                    ddlHomeAlarm.SelectedValue = "0";
                }

                if (Convert.ToBoolean(Host.OptionsInternetOffered.ToString()))
                {
                    ddlInternetOffered.SelectedValue = "1";
                }
                else
                {
                    ddlInternetOffered.SelectedValue = "0";
                }
                ddlInternetType.SelectedValue = Host.OptionsInternet_Type;
                txtExtraCharge.Text = Host.OptionsInternetExtraCharge.ToString();
                ddlUsage.SelectedValue = Host.OptionsInternet_Usage.ToString();

                //Host Emergency Contact
                txtContactName.Text = Host.AdminHostEmergencyPerson;
                txtContactPhone.Text = Host.AdminHostEmergencyPhone;
                txtRelationship.Text = Host.AdminHostEmergencyReleationship;

                // Host status
                LoadDropListHostStatus();
                ddlHostStatus.SelectedValue = HomestayHostStatus(Convert.ToInt32(Host.HouseActiveStutas.ToString()));

                DatePickStatusDate.SelectedDate = Host.HouseActiveDate;

                //Host Meal Plan and Fridge
                if (Convert.ToBoolean(Host.MealPlanBreakfastType))
                {
                    ddlBreakfast.SelectedValue = "1";
                }
                else
                {
                    ddlBreakfast.SelectedValue = "0";
                }

                if (Convert.ToBoolean(Host.MealPlanLunchType))
                {
                    ddlLunch.SelectedValue = "1";
                }
                else
                {
                    ddlLunch.SelectedValue = "0";
                }
                if (Convert.ToBoolean(Host.MealPlanDinnerType))
                {
                    ddlDinner.SelectedValue = "1";
                }
                else
                {
                    ddlDinner.SelectedValue = "0";
                }

                ddlFridge.SelectedValue = Host.FridgeSharedSerperatedAccess.ToString();

                //Family Member List
                Grid_FamilyMember.Rebind();

                //Host Room List
                Grid_HostRoom.Rebind();

                //Host Bed List
                Grid_HostBed.Rebind();

                //Host Preferred School List
                Grid_School.Rebind();

                // Placement History
                Grid_HomestayPlacement.Rebind();

                //Host Status
                LoadDropListHostStatus();
                ddlHostStatus.SelectedValue = Host.HouseActiveStutas.ToString();
                DatePickStatusDate.SelectedDate = Host.HouseActiveDate;

            }
            else
            {
                ShowMessage("Session time out.");

            }

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
        protected void Grid_FamilyMember_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

            if (Session["hostid"] != null)
            {
                List<HomestayHostFamilyMember> FamilyMember = new List<HomestayHostFamilyMember>();
                var cHomestayFamilyMember = new CHomestayHostFamilyMember();
                FamilyMember = cHomestayFamilyMember.GetFamilyMemberList(Convert.ToInt32(Session["hostid"].ToString()));
                Grid_FamilyMember.DataSource = FamilyMember;
            }

        }

        protected void Grid_FamilyMember_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Grid_FamilyMember.SelectedValue != null)
            {
                int FamilyMemberId = Convert.ToInt32(Grid_FamilyMember.SelectedValue.ToString());
                Session["FamilyMemberId"] = FamilyMemberId;

                var cHomestayFamilyMember = new CHomestayHostFamilyMember();
                HomestayHostFamilyMember FM = new HomestayHostFamilyMember();
                FM = cHomestayFamilyMember.Get(FamilyMemberId);

                txtMemberFirstName.Text = FM.MemberFirstName;
                txtMemberLastName.Text = FM.MemberLastName;
                DatePickMemberDOB.SelectedDate = FM.MemberDOB;
                ddlMemberGender.SelectedValue = Convert.ToInt32(Convert.ToBoolean(FM.MemberGender.ToString())).ToString();
                ddlMotherCRC.SelectedValue = Convert.ToInt32(Convert.ToBoolean(FM.MemberCRC.ToString())).ToString();
                ddlLivingHome.SelectedValue = Convert.ToUInt32(Convert.ToBoolean(FM.MemberLivingHome.ToString())).ToString();
                txtMemberRelationship.Text = FM.Relationship;

                Grid_FamilyMember.Rebind();
                Grid_HostRoom.Rebind();

            }
        }

        protected void Grid_FamilyMember_ItemDataBound(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridDataItem)
            {

                GridDataItem DataItem = e.Item as GridDataItem;
                Boolean Gender = Convert.ToBoolean(DataItem["FamilyMember_Gender"].Text.ToString().Trim());
                if (Gender)
                {
                    DataItem["FamilyMember_Gender"].Text = "Male";
                }
                else
                {
                    DataItem["FamilyMember_Gender"].Text = "Female";
                }


            }
        }


        protected void Grid_HostRoom_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Session["hostid"] != null)
            {
                List<HomestayHostRoom> HostRoom = new List<HomestayHostRoom>();
                var cHomestayHostRoom = new CHomestayHostRoom();
                HostRoom = cHomestayHostRoom.GetHostRoomList(Convert.ToInt32(Session["hostid"].ToString()));
                Grid_HostRoom.DataSource = HostRoom;
            }

        }

        protected void Grid_HostRoom_ItemDataBound(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridDataItem)
            {
                GridDataItem DataItem = e.Item as GridDataItem;
                Boolean RoomType = Convert.ToBoolean(DataItem["HostRoomType"].Text.ToString().Trim());
                if (RoomType)
                {
                    DataItem["HostRoomType"].Text = "Private Room";
                }
                else
                {
                    DataItem["HostRoomType"].Text = "Shared Room";
                }

                int RoomFloor = Convert.ToInt32(DataItem["HostRoomFloor"].Text.ToString());

                DataItem["HostRoomFloor"].Text = FloorName(RoomFloor);


            }
        }

        protected void Grid_HostRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Grid_HostRoom.SelectedValue != null)
            {
                int HostRoomId = Convert.ToInt32(Grid_HostRoom.SelectedValue.ToString());
                Session["HostRoomId"] = HostRoomId;

                var cHomestay_HostRoom = new CHomestayHostRoom();
                HomestayHostRoom Host_Room = cHomestay_HostRoom.Get(HostRoomId);

                txtRoomName.Text = Host_Room.HostRoomName;

                ddlRoomLocation.SelectedValue = Convert.ToInt32(Host_Room.HostRoomFloor).ToString();

                ddlRoomType.SelectedValue = Convert.ToUInt32(Convert.ToBoolean(Host_Room.HostRoomType)).ToString();

            }
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



        protected void Grid_HostBed_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Session["hostid"] != null)
            {
                var cHomestayHostBed = new CHomestayHostBed();
                Grid_HostBed.DataSource = cHomestayHostBed.HomestayHostBedList(Convert.ToInt32(Session["hostid"].ToString()));
            }

        }

        protected void Grid_HostBed_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem DataItem = e.Item as GridDataItem;

                int RoomFloor = Convert.ToInt32(DataItem["HostRoomFloor"].Text.ToString());

                DataItem["HostRoomFloor"].Text = FloorName(RoomFloor);

                int BedId = Convert.ToInt32(DataItem["HostBedId"].Text);


                var cBed = new CHomestayHostBed();
                RadGrid GridPlacement = (RadGrid)DataItem.FindControl("GridPlaced");
                if (GridPlacement != null)
                {
                    GridPlacement.DataSource = cBed.GetHomestayBedPlaced(BedId);
                    GridPlacement.Rebind();

                }


            }

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
                var cHomestayHostBed = new CHomestayHostBed();

                HomestayHostBed host_bed = cHomestayHostBed.Get(BedId);
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
            if (Session["hostid"] != null)
            {
                var schoolList = new CHomestayHostPreferredSchool();
                Grid_School.DataSource = schoolList.HomestayHostPreferredSchoolList(Convert.ToInt32(Session["hostid"].ToString()));


            }

        }

        protected void Grid_School_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Grid_School.SelectedValue != null)
            {
                int SchoolId = 0;
                SchoolId = Convert.ToInt32(Grid_School.SelectedValue);
                if (SchoolId > 0)
                {
                    Session["HostSchoolId"] = SchoolId;
                    var cHostSchool = new CHomestayHostPreferredSchool();
                    var HostSchool = cHostSchool.Get(SchoolId);
                    Dropdown_ShcoolList();

                    ddlSchoolName.SelectedValue = HostSchool.SiteLocationId.ToString();

                    Dropdown_SchoolContactUserList(Convert.ToInt32(HostSchool.SiteLocationId));
                    ddlSchoolContactName.SelectedValue = HostSchool.ContactUserId.ToString();
                    txtDistanceToSchool.Text = HostSchool.DistanceSchool.ToString();
                    txtDistanceBusSubway.Text = HostSchool.DistanceStation.ToString();
                    txtMajorIntersection.Text = HostSchool.MajorIntersection.ToString();
                }



            }
        }

        protected void ddlSchoolName_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            int SchoolLocationId = 0;
            SchoolLocationId = Convert.ToInt32(ddlSchoolName.SelectedValue);
            if (SchoolLocationId > 0)
            {

                Dropdown_SchoolContactUserList(SchoolLocationId);

            }
        }

        protected void Dropdown_SchoolContactUserList(int SiteLocationId)
        {
            ddlSchoolContactName.Items.Clear();


            var cglobal = new CGlobal();
            ddlSchoolContactName.DataSource = cglobal.LoadSchooContactlList(SiteLocationId);
            ddlSchoolContactName.DataTextField = "Name";
            ddlSchoolContactName.DataValueField = "Value";
            ddlSchoolContactName.SelectedIndex = 0;
            ddlSchoolContactName.DataBind();

        }

        protected void Tab_Host_TabClick(object sender, RadTabStripEventArgs e)
        {
            if (Session["hostid"] != null)
            {
                switch (Convert.ToInt32(Tab_Host.SelectedIndex))
                {
                    //case 0:
                    //    break;
                    case 1:
                        Grid_FamilyMember.Rebind();

                        break;
                    case 2:
                        Grid_HostRoom.Rebind();
                        break;
                    case 3:
                        Grid_HostBed.Rebind();
                        break;
                    case 4:
                        Grid_School.Rebind();
                        break;
                    case 5:
                        break;

                }
                SetSessionNull();
                ClearForms();
            }

        }

        protected void btn_register_Click(object sender, EventArgs e)
        {
            if (Session["hostid"] != null)
            {
                var cHomestayHostBasic = new CHomestayHostBasic();
                HomestayHostBasic host_basic = new HomestayHostBasic();
                host_basic = cHomestayHostBasic.Get(Convert.ToInt32(Session["hostid"].ToString()));
                //Host status
                host_basic.HouseActiveStutas = Convert.ToInt32(ddlHostStatus.SelectedValue); //pending=0, active=1,inactive=2
                host_basic.HouseActiveDate = DatePickStatusDate.SelectedDate;
                host_basic.UpdatedId = CurrentUserId;
                host_basic.UpdatedDate = DateTime.Now;
                bool HB_Update = cHomestayHostBasic.Update(host_basic);
                if (HB_Update)
                {
                    EnableTab(true, true);
                    ShowMessage("Homestay host basic information is updated successfully.");
                }
                else
                {
                    ShowMessage("Failed to update Host Basic Information, please try it again.");
                }
                LoadDropListHostStatus();
                DatePickStatusDate.SelectedDate = DateTime.Today;
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
                int hostid = Convert.ToInt32(DataItem["HostId"].Text.ToString());
                int status = Convert.ToInt32(DataItem["HouseActiveStutas"].Text.ToString().Trim());
                DataItem["HouseActiveStutas"].Text = HomestayHostStatus(status);
                //School and Campus
                //var cHostSchool = new CHomestayHostPreferredSchool();
                //HomestayHostPrefferedSchool hostTopSchool = cHostSchool.GetHostTopSchool(hostid);
                
                //Family Member
                RadLabel lblFamilyMember = (RadLabel)DataItem.FindControl("lbl_FamilyMember");
                var cHostFamily = new CHomestayHostFamilyMember();
                int MemberNumber = 0;
                MemberNumber = cHostFamily.GetFamilyMemberNumber(hostid);
                string FatherFirstName = DataItem["FatherFirstName"].Text.Trim();
                if (FatherFirstName != "&nbsp;")
                {
                    if (FatherFirstName.Length > 1)
                    {
                        MemberNumber = MemberNumber + 1;

                    }
                }
                String MotherFirstName = DataItem["MotherFirstName"].Text.Trim();
                if (MotherFirstName != "&nbsp;")
                {
                    if (MotherFirstName.Length > 1)
                    {
                        MemberNumber = MemberNumber + 1;
                    }
                }

                lblFamilyMember.Text = MemberNumber.ToString();

                //Room Number
                RadLabel lblRoomNumber = (RadLabel)DataItem.FindControl("lbl_RoomNumber");
                var cHostRoom = new CHomestayHostRoom();
                int RoomNumber = 0;
                RoomNumber = cHostRoom.GetHomestayHostRoomNumber(hostid);
                lblRoomNumber.Text = RoomNumber.ToString();
                //Bed Number
                RadLabel lblBedNumber = (RadLabel)DataItem.FindControl("lbl_BedNumber");
                var cHostBed = new CHomestayHostBed();
                int BedNumber = 0;
                BedNumber = cHostBed.GetHomestayHostBedNumber(hostid);
                lblBedNumber.Text = BedNumber.ToString();
            }
        }

        protected void Grid_HostList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var cHomestayBasic = new CHomestayHostBasic();

            Grid_HostList.DataSource = cHomestayBasic.GetHomestayHostList(CurrentSiteLocationId);

        }


    }
}