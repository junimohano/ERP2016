using System;
using System.Collections.Generic;
using System.Data;
using Erp2016.Lib;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;

namespace School.Registrar.Student
{
    public partial class StudentScheduleChangePop : PageBase
    {
        public int InvoiceId { get; set; }

        public StudentScheduleChangePop() : base((int)CConstValue.Menu.Student)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InvoiceId = Convert.ToInt32(Request["id"]);

            if (!IsPostBack)
            {
                FileDownloadList1.InitFileDownloadList((int)CConstValue.Upload.ScheduleChange);

                var vwInvoice = new CInvoice().GetVwInvoice(InvoiceId);
                if (vwInvoice != null)
                {
                    RadDatePickerApplyDate.SelectedDate = DateTime.Today;

                    RadDatePickerStartDate.SelectedDate = vwInvoice.StartDate;
                    RadDatePickerEndDate.SelectedDate = vwInvoice.EndDate;
                }
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
                            var startDate = DateTime.Today;
                            var endDate = DateTime.Today;

                            // Program
                            if (original.ProgramRegistrationId != null)
                            {
                                var cProgramRegiInfo = new CProgramRegistration();
                                var programRegiInfo = cProgramRegiInfo.Get(Convert.ToInt32(original.ProgramRegistrationId));

                                startDate = programRegiInfo.StartDate.Value;
                                endDate = programRegiInfo.EndDate.Value;

                                programRegiInfo.StartDate = RadDatePickerStartDate.SelectedDate.Value;
                                programRegiInfo.EndDate = RadDatePickerEndDate.SelectedDate.Value;
                                programRegiInfo.UpdatedId = CurrentUserId;
                                programRegiInfo.UpdatedDate = DateTime.Now;

                                cProgramRegiInfo.Update(programRegiInfo);
                            }
                            // Homestay
                            else if (original.HomestayRegistrationId != null)
                            {
                                var cHomestayStudentRequest = new CHomestayStudentRequest();
                                var homestayStudentRequest = cHomestayStudentRequest.GetHomestayStudentRequest(Convert.ToInt32(original.HomestayRegistrationId));

                                startDate = homestayStudentRequest.StartDate.Value;
                                endDate = homestayStudentRequest.EndDate.Value;

                                homestayStudentRequest.StartDate = RadDatePickerStartDate.SelectedDate.Value;
                                homestayStudentRequest.EndDate = RadDatePickerEndDate.SelectedDate.Value;
                                homestayStudentRequest.UpdateUserId = CurrentUserId;
                                homestayStudentRequest.UpdatedDate = DateTime.Now;
                                homestayStudentRequest.HomestayStudentStatus = 2; // scheduel Change

                                cHomestayStudentRequest.Update(homestayStudentRequest);
                            }
                            // Dormitory
                            else if (original.DormitoryRegistrationId != null)
                            {
                                var cDormitoryStudentRequest = new CDormitoryRegistrations();
                                var dormitoryStudentRequest = cDormitoryStudentRequest.GetDormitoryStudentRequest(Convert.ToInt32(original.DormitoryRegistrationId));

                                startDate = dormitoryStudentRequest.StartDate.Value;
                                endDate = dormitoryStudentRequest.EndDate.Value;

                                dormitoryStudentRequest.StartDate = RadDatePickerStartDate.SelectedDate.Value;
                                dormitoryStudentRequest.EndDate = RadDatePickerEndDate.SelectedDate.Value;
                                dormitoryStudentRequest.DormitoryStudentStatus = 2; // scheduel Change
                                dormitoryStudentRequest.UpdatedId = CurrentUserId;
                                dormitoryStudentRequest.UpdatedDate = DateTime.Now;

                                cDormitoryStudentRequest.Update(dormitoryStudentRequest);
                            }

                            var cScheduleChange = new CScheduleChange();
                            var s = new ScheduleChange();

                            s.InvoiceId = original.InvoiceId;
                            s.ApplyDate = RadDatePickerApplyDate.SelectedDate.Value;
                            s.StartDate = startDate;
                            s.EndDate = endDate;
                            s.Reason = RadTextBoxReason.Text;
                            s.IsActive = true;
                            s.CreatedId = CurrentUserId;

                            int scheduleChangeId = cScheduleChange.Add(s);
                            if (scheduleChangeId > 0)
                            {
                                // save uploading file
                                FileDownloadList1.SaveFile(scheduleChangeId);

                                RunClientScript("Close();");
                            }
                            else
                                ShowMessage("failed to update inqury (Add Schedule Change)");
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

        protected void RadDatePickerStartDate_OnSelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            if (RadDatePickerStartDate.SelectedDate > RadDatePickerEndDate.SelectedDate)
                RadDatePickerStartDate.SelectedDate = RadDatePickerEndDate.SelectedDate;
        }

        protected void RadDatePickerEndDate_OnSelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            if (RadDatePickerStartDate.SelectedDate > RadDatePickerEndDate.SelectedDate)
                RadDatePickerEndDate.SelectedDate = RadDatePickerStartDate.SelectedDate;
        }
    }
}