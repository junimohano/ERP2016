using System;
using System.Data;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.Registrar.Student
{
    public partial class StudentCancelPop : PageBase
    {
        public int InvoiceId { get; set; }

        public StudentCancelPop() : base((int)CConstValue.Menu.Student)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InvoiceId = Convert.ToInt32(Request["id"]);

            if (!IsPostBack)
            {
                RadDatePickerApply.SelectedDate = DateTime.Today;
                FileDownloadList1.InitFileDownloadList((int)CConstValue.Upload.Cancel);
            }
        }

        protected void ToolbarButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Text)
            {
                case "Request":
                    if (IsValid)
                    {
                        var cInvoice = new CInvoice();
                        var original = cInvoice.Get(InvoiceId);
                        if (original != null)
                        {
                            original.Status = (int)CConstValue.InvoiceStatus.Cancelled_CC;
                            original.UpdatedId = CurrentUserId;

                            if (cInvoice.Update(original))
                            {
                                var cCancel = new CCancel();
                                var cancel = new Cancel();

                                cancel.InvoiceId = original.InvoiceId;
                                cancel.ApplyDate = RadDatePickerApply.SelectedDate.Value;
                                cancel.Reason = RadTextBoxReason.Text;
                                cancel.IsActive = true;
                                cancel.CreatedId = CurrentUserId;

                                int cancelId = cCancel.Add(cancel);
                                if (cancelId > 0)
                                {
                                    // save uploading file
                                    FileDownloadList1.SaveFile(cancelId);

                                    // Program
                                    if (original.ProgramRegistrationId != null)
                                    {
                                        var cProgramRegiInfo = new CProgramRegistration();
                                        var programRegiInfo = cProgramRegiInfo.Get(Convert.ToInt32(original.ProgramRegistrationId));
                                        programRegiInfo.UpdatedId = CurrentUserId;
                                        programRegiInfo.UpdatedDate = DateTime.Now;
                                        programRegiInfo.ProgramRegistrationType = 12; // cancel

                                        cProgramRegiInfo.Update(programRegiInfo);
                                    }
                                    // Homestay
                                    else if (original.HomestayRegistrationId != null)
                                    {
                                        var cHomestayStudentRequest = new CHomestayStudentRequest();
                                        var homestayStudentRequest = cHomestayStudentRequest.GetHomestayStudentRequest(Convert.ToInt32(original.HomestayRegistrationId));
                                        homestayStudentRequest.UpdateUserId = CurrentUserId;
                                        homestayStudentRequest.UpdatedDate = DateTime.Now;
                                        homestayStudentRequest.HomestayStudentStatus = 1; // cancel
                                       

                                        cHomestayStudentRequest.Update(homestayStudentRequest);
                                    }
                                    // Dormitory
                                    else if (original.DormitoryRegistrationId != null)
                                    {
                                        var cDormitoryStudentRequest = new CDormitoryRegistrations();
                                        var dormitoryStudentRequest = cDormitoryStudentRequest.GetDormitoryStudentRequest(Convert.ToInt32(original.DormitoryRegistrationId));
                                        dormitoryStudentRequest.DormitoryStudentStatus = 1; // cancel
                                        dormitoryStudentRequest.UpdatedId = CurrentUserId;
                                        dormitoryStudentRequest.UpdatedDate = DateTime.Now;

                                        cDormitoryStudentRequest.Update(dormitoryStudentRequest);
                                    }

                                    if (original.ScholarshipId != null)
                                    {
                                        var cScholarship = new CScholarship();
                                        var scholarship = cScholarship.Get((int)original.ScholarshipId);
                                        if (scholarship != null)
                                        {
                                            scholarship.IsActive = true;
                                            cScholarship.Update(scholarship);
                                        }
                                    }

                                    RunClientScript("Close();");
                                }
                                else
                                    ShowMessage("failed to update inqury (Add Cancel)");
                            }
                            else
                                ShowMessage("failed to update inqury (Update Original Invoice)");
                        }
                        else
                            ShowMessage("failed to update inqury (Original Invoice is null)");
                    }
                    break;
                case "Close":
                    RunClientScript("Close();");
                    break;
            }
        }

    }
}