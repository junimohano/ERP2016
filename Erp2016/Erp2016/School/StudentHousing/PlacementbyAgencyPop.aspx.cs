using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;


public partial class School_StudentHousing_PlacementbyAgencyPop : PageBase
{
    public int HomestayStudentId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        
            file_upload.InitFileDownloadList((int)CConstValue.Upload.HomestayAgency);
            if (Request["Id"].ToString() != null)
            {
                HomestayStudentId = Convert.ToInt32(Request["Id"].ToString());
                
            }
       
    }

    protected void UpdateToolBar_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
    {
        if (HomestayStudentId > 0)
        {

            int Status = 0;
            switch (e.Item.Text)
            {
                case "Accepted":
                    Status = 1; //Accepted
                    break;
                case "Cancelled:":
                    Status = 0; //Cancelled
                    break;
                case "Rejected":
                    Status = 3; //Rejected
                    break;

                default:
                    Status = 0;
                    break;


            }
            if (e.Item.Text!="Close Window" )
            {
                HomestayStudentBasic Request = StudentRequest(HomestayStudentId);
                int StudentId = Convert.ToInt32(Request.StudentId);


                int AddResult = 0;
                var cHomestayPlacement = new CHomestayPlacement();
                HomestayPlacement Placement = new HomestayPlacement();
                Placement.StudentBasicId = HomestayStudentId;
                Placement.StudentId = StudentId;
                Placement.HostId = 0;
                Placement.RoomId = 0;
                Placement.BedId = 0;
                Placement.HomestayAgencyId = 0;
                Placement.StartDate = Request.StartDate;
                Placement.EndDate = Request.EndDate;

                Placement.PlacementStatus =Status; //  Accepted=1, Canceled =0, Rejected:3, Schedule Change=2

                Placement.PlacementComment = txt_Comment.Text;
                Placement.PlacementType = 2; // Placed by School=1, Place by Agency =2
                Placement.CreatedDate = DateTime.Now;
                Placement.CreatedId = CurrentUserId;
                Placement.UpdatedDate = DateTime.Now;
                Placement.UpdatedId = CurrentUserId;
                Placement.HomestayAgencyId = CurrentUserId;
                file_upload.GetFileDownload(Convert.ToInt32(HomestayStudentId));

                if (cHomestayPlacement.CountPlacementbyBasicId(HomestayStudentId) == 0)
                {
                    AddResult = cHomestayPlacement.Add(Placement);
                }
                else
                {
                    HomestayPlacement UpdatePlacement = cHomestayPlacement.GetByStudentBasicId(HomestayStudentId);
                    UpdatePlacement.PlacementStatus = Status;
                    UpdatePlacement.PlacementComment = txt_Comment.Text;
                    UpdatePlacement.UpdatedDate = DateTime.Now;
                    UpdatePlacement.UpdatedId = CurrentUserId;
                    cHomestayPlacement.Update(UpdatePlacement);
                }


                    file_upload.SaveFile(HomestayStudentId);
                    // in HomestayBasic table
                    int BasicStatus = 0;
                    switch (Status)
                    {
                        case 0:
                            BasicStatus = 6;// Cancelled by Agency
                            break;
                        case 1:
                            BasicStatus = 3; //Placed By Agency 
                            break;
                        case 3:
                            BasicStatus = 7; //Rejected by Agency
                            break;
                        default:
                            break;
                    }
                    UpdateHomestayStudentStatus(HomestayStudentId, BasicStatus); 
                    
                    RunClientScript("Close();");
                    ShowMessage("Homestay Placement Request has been updated successfully.");
              
            }
        }
    }
    protected HomestayStudentBasic StudentRequest(int HomestayStudentId)
    {
        var cHomestayStudentBasic = new CHomestayStudentRequest();
        HomestayStudentBasic Student = cHomestayStudentBasic.GetHomestayStudentRequest(HomestayStudentId);
        return Student;
    }
}