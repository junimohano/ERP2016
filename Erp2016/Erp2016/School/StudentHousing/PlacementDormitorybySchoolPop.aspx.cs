using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;


public partial class School_StudentHousing_PlacementDormitorybySchoolPop :PageBase 
{
    public int DormitoryRegistrationId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["Id"].ToString()!=null)
        {
            DormitoryRegistrationId = Convert.ToInt32(Request["Id"].ToString());
           

        }    
    }

    protected void UpdateToolBar_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
    {
        if (DormitoryRegistrationId >0)
        {
            if (UpdateToolBar.TabIndex ==0)
            {
                
            DormitoryRegistration Request = StudentRequest(DormitoryRegistrationId);
            int StudentId = Convert.ToInt32(Request.StudentId);
                if (Grid_HostList.SelectedValue != null)
                {
                    int HostId = Convert.ToInt32(Grid_HostList.SelectedValue);

                    if (Grid_HostRoom.SelectedValue != null)
                    {
                        int RoomId = Convert.ToInt32(Grid_HostRoom.SelectedValue);
                        if (Grid_HostBed.SelectedValue != null)
                        {
                            int BedId = Convert.ToInt32(Grid_HostBed.SelectedValue);
                            //Dormitory Placement

                            int AddResult = 0;
                            var cDormitoryPlacement = new CDormitoryPlacement();
                            DormitoryPlacement Placement = new DormitoryPlacement();
                            Placement.StudentBasicId = DormitoryRegistrationId;
                            Placement.StudentId = StudentId;
                            Placement.HostId = HostId;
                            Placement.RoomId = RoomId;
                            Placement.BedId = BedId;
                            Placement.DormitoryAgencyId= 0;
                            Placement.StartDate = Request.StartDate;
                            Placement.EndDate = Request.EndDate;
                            Placement.PlacementStatus = 1; //  Placed=1, Canceled =0, Schedule Change=2
                            Placement.PlacementType = 1; // Placed by School=1, Place by Agency =2
                            Placement.CreatedDate = DateTime.Now;
                            Placement.CreatedId = CurrentUserId;
                            Placement.UpdatedDate = DateTime.Now;
                            Placement.UpdatedId = CurrentUserId;
                            AddResult = cDormitoryPlacement.Add(Placement);

                            if (AddResult > 0)
                            {

                                var cRequest = new CDormitoryRegistrations();

                                UpdateDormitoryStudentStatus(DormitoryRegistrationId, 2); //Placed By School 
                                RunClientScript("Close();");
                                ShowMessage("Dormitory Placement Request has been placed successfully.");
                            }
                            else //Failed
                            {
                                ShowMessage("Failed to add Dormitory Placement Request, Please try it again.");

                            }


                        }
                    }

                }
            }
            
        }

    }

    protected void Grid_HostList_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (DormitoryRegistrationId>0)
        {
            
            DormitoryRegistration Student = StudentRequest(DormitoryRegistrationId);
            DateTime StartDate = Convert.ToDateTime(Student.StartDate);
            DateTime EndDate = Convert.ToDateTime(Student.EndDate);
            var cDormitoryBasic = new CDormitoryRegistrations();
            Grid_HostList.DataSource = cDormitoryBasic.DormitoryHostListWithVacantBed(CurrentSiteLocationId, StartDate, EndDate); 
                //cDormitoryBasic.GetDormitoryHostActiveList(CurrentSiteLocationId, StartDate,EndDate);


        }

    }
    protected DormitoryRegistration StudentRequest(int DormitoryRegistrationId)
    {
        var cDormitoryStudentRequest = new CDormitoryRegistrations();
        DormitoryRegistration Student =cDormitoryStudentRequest.GetDormitoryStudentRequest(DormitoryRegistrationId);
        return Student;
    }

    protected void Grid_HostList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Grid_HostList.SelectedValue.ToString()!=null)
        {
            Grid_HostRoom.Rebind();
            Grid_HostBed.Visible = false;
        }

    }

    protected void Grid_HostList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {

            GridDataItem DataItem = e.Item as GridDataItem;
            int hostid = Convert.ToInt32(DataItem["DormitoryHostId"].Text.ToString());
            int status = Convert.ToInt32(DataItem["HostStatus"].Text.ToString().Trim());
            DataItem["HostStatus"].Text = DormitoryHostStatus(status);
            //School and Campus
            var cHostSchool = new CDormitoryHostPreferredSchool();
            DormitoryHostPrefferedSchool hostTopSchool = cHostSchool.GetHostTopSchool(hostid);

            SiteLocation HostSchoolLocation = cHostSchool.GetHostTopShoolNameLocation(Convert.ToInt32(hostTopSchool.SiteLocationId));
            var cSite = new CSite();
            Site SchoolName = cSite.Get(Convert.ToInt32(HostSchoolLocation.SiteId));
            RadLabel lblTopShool = (RadLabel)DataItem.FindControl("lbl_HostTopSchool");
            lblTopShool.Text = SchoolName.Abbreviation.ToString();
            RadLabel lblTopCampus = (RadLabel)DataItem.FindControl("lbl_HostTopCampus");
            lblTopCampus.Text = HostSchoolLocation.City.ToString();


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


    protected bool AvalibleHost(int HostId)
    {
        bool avalible = false;

       // HomestayAvalibleHost


        return avalible;
    }


    protected void Grid_HostRoom_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        if (Grid_HostList.SelectedValue !=null)
        {
            int hostId = 0;
            hostId = Convert.ToInt32(Grid_HostList.SelectedValue.ToString());
            List<DormitoryHostRoom> HostRoom = new List<DormitoryHostRoom>();
            var cDormitoryHostRoom = new CDormitoryRegistrations();
            DormitoryRegistration  Student = StudentRequest(DormitoryRegistrationId);
            HostRoom = cDormitoryHostRoom.DormitoryRoomListWithVacantBed(hostId, Convert.ToDateTime(Student.StartDate), Convert.ToDateTime(Student.EndDate));
            Grid_HostRoom.DataSource = HostRoom;
            
        }
        
    }

    protected void Grid_HostRoom_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Grid_HostRoom.SelectedValue.ToString()!=null)
        {
            Grid_HostBed.Visible = true;
            Grid_HostBed.Rebind();
           
       
        }
    }
   
    protected void Grid_HostRoom_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem DataItem = e.Item as GridDataItem;
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
        if (Grid_HostList.SelectedValue!=null && Grid_HostRoom.SelectedValue!=null)
        {
            var cDormitoryHostBed = new CDormitoryRegistrations();
            int hostid = Convert.ToInt32(Grid_HostList.SelectedValue.ToString());
            int roomid = Convert.ToInt32(Grid_HostRoom.SelectedValue.ToString());

            DormitoryRegistration  Student = StudentRequest(DormitoryRegistrationId);
            Grid_HostBed.DataSource = cDormitoryHostBed.DormitoryBedListWithVacantBed(hostid, roomid, Convert.ToDateTime(Student.StartDate), Convert.ToDateTime(Student.EndDate));
           Grid_HostBed.Visible = true;

        }
    }

    protected void Grid_HostBed_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Grid_HostBed.SelectedValue!=null)
        {

            
            Grid_HostBed.Visible = true;

            GridDataItem Item = (GridDataItem)Grid_HostBed.SelectedItems[0];
            RadLabel lblAbliable = (RadLabel)Item.FindControl("lbl_avalible");
            if (lblAbliable.Text=="Yes")
            {
                foreach (GridDataItem item in Grid_HostBed.MasterTableView.Items)
                {
                    int HostBedId = Convert.ToInt32(item.GetDataKeyValue("HostBedId").ToString());
                    RadCheckBox chbPlacement = (RadCheckBox)item.FindControl("chb_placement");
                    if (HostBedId == Convert.ToInt32(Grid_HostBed.SelectedValue.ToString()))
                    {
                        chbPlacement.Checked = true;
                    }
                    else
                    {
                        chbPlacement.Checked = false;
                    }
                }
            }
            
        }

    }
   
    protected void Grid_HostBed_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem Item = (GridDataItem)e.Item;
            RadLabel lblAbliable = (RadLabel)Item.FindControl("lbl_avalible");
            RadCheckBox chbPlacement = (RadCheckBox)Item.FindControl("chb_placement");
            // Check Avalible

            if (lblAbliable.Text == "No")
            {
                chbPlacement.Visible = false;
            }
            else
            {
                chbPlacement.Visible = true;
            }

        }

    }
}