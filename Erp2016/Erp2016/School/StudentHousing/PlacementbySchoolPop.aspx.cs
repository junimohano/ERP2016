using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;


public partial class School_StudentHousing_PlacementbySchoolHomestayPop :PageBase 
{
    public int HomestayStudentId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["Id"].ToString()!=null)
        {
            HomestayStudentId = Convert.ToInt32(Request["Id"].ToString());
           

        }    
    }

    protected void UpdateToolBar_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
    {
        if (HomestayStudentId >0)
        {
            if (UpdateToolBar.TabIndex ==0)
            {
                
            HomestayStudentBasic Request = StudentRequest(HomestayStudentId);
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
                            //Homestay Placement

                            int AddResult = 0;
                            var cHomestayPlacement = new CHomestayPlacement();
                            HomestayPlacement Placement = new HomestayPlacement();
                            Placement.StudentBasicId = HomestayStudentId;
                            Placement.StudentId = StudentId;
                            Placement.HostId = HostId;
                            Placement.RoomId = RoomId;
                            Placement.BedId = BedId;
                            Placement.HomestayAgencyId = 0;
                            Placement.StartDate = Request.StartDate;
                            Placement.EndDate = Request.EndDate;
                            Placement.PlacementStatus = 1; //  Placed=1, Canceled =0, Schedule Change=2
                            Placement.PlacementType = 1; // Placed by School=1, Place by Agency =2
                            Placement.CreatedDate = DateTime.Now;
                            Placement.CreatedId = CurrentUserId;
                            Placement.UpdatedDate = DateTime.Now;
                            Placement.UpdatedId = CurrentUserId;
                            AddResult = cHomestayPlacement.Add(Placement);

                            if (AddResult > 0)
                            {

                                var cRequest = new CHomestayStudentRequest();

                                UpdateHomestayStudentStatus(HomestayStudentId, 2); //Placed By School 
                                RunClientScript("Close();");
                                ShowMessage("Homestay Placement Request has been placed successfully.");
                            }
                            else //Failed
                            {
                                ShowMessage("Failed to add Homestay Placement Request, Please try it again.");

                            }


                        }
                    }

                }
            }
            
        }

    }

    protected void Grid_HostList_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (HomestayStudentId>0)
        {
            
            HomestayStudentBasic Student = StudentRequest(HomestayStudentId);
            DateTime StartDate = Convert.ToDateTime(Student.StartDate);
            DateTime EndDate = Convert.ToDateTime(Student.EndDate);
            var cHomestayBasic = new CHomestayHostBasic();
            Grid_HostList.DataSource = cHomestayBasic.HomestayHostListWithVacantBed(CurrentSiteLocationId, StartDate, EndDate);
            //cHomestayBasic.GetHomestayHostActiveList(CurrentSiteLocationId, StartDate,EndDate);


        }

    }
    protected HomestayStudentBasic StudentRequest(int HomestayStudentId)
    {
        var cHomestayStudentBasic = new CHomestayStudentRequest();
        HomestayStudentBasic Student = cHomestayStudentBasic.GetHomestayStudentRequest(HomestayStudentId);
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
            int hostid = Convert.ToInt32(DataItem["HostId"].Text.ToString());
            int status = Convert.ToInt32(DataItem["HouseActiveStutas"].Text.ToString().Trim());
            DataItem["HouseActiveStutas"].Text = "Active";
            //School and Campus
            var cHostSchool = new CHomestayHostPreferredSchool();
            HomestayHostPrefferedSchool hostTopSchool = cHostSchool.GetHostTopSchool(hostid);

            SiteLocation HostSchoolLocation = cHostSchool.GetHostTopShoolNameLocation(Convert.ToInt32(hostTopSchool.SiteLocationId));
            var cSite = new CSite();
            Site SchoolName = cSite.Get(Convert.ToInt32(HostSchoolLocation.SiteId));
            RadLabel lblTopShool = (RadLabel)DataItem.FindControl("lbl_HostTopSchool");
            lblTopShool.Text = SchoolName.Abbreviation.ToString();
            RadLabel lblTopCampus = (RadLabel)DataItem.FindControl("lbl_HostTopCampus");
            lblTopCampus.Text = HostSchoolLocation.City.ToString();

            //Family Member
            RadLabel lblFamilyMember = (RadLabel)DataItem.FindControl("lbl_FamilyMember");
            var cHostFamily = new CHomestayHostFamilyMember();
            int MemberNumber = 0;
            MemberNumber = cHostFamily.GetFamilyMemberNumber(hostid);
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


            //
            if (hostid == 1) //AvalibleHost(int HostId)
            {
                DataItem.Display = false;

            }
        }

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
                List<HomestayHostRoom> HostRoom = new List<HomestayHostRoom>();
                var cHomestayHostRoom = new CHomestayHostBasic();
                HomestayStudentBasic Student = StudentRequest(HomestayStudentId);
                HostRoom = cHomestayHostRoom.HomestayRoomListWithVacantBed(hostId, Convert.ToDateTime(Student.StartDate), Convert.ToDateTime(Student.EndDate));
                //cHomestayHostRoom.GetHostRoomList(hostId);
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
            var cHomestayHosBed = new CHomestayHostBasic();
            int hostid = Convert.ToInt32(Grid_HostList.SelectedValue.ToString());
            int roomid = Convert.ToInt32(Grid_HostRoom.SelectedValue.ToString());
            HomestayStudentBasic Student = StudentRequest(HomestayStudentId);
            Grid_HostBed.DataSource = cHomestayHosBed.HomestayBedListWithVacantBed(hostid,roomid,Convert.ToDateTime(Student.StartDate),Convert.ToDateTime(Student.EndDate));

           // cHomestayHostBed.HomestayHostBedByRoom(hostid, roomid);
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