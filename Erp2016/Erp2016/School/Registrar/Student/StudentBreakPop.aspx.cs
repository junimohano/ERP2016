using System;
using System.Data;
using Erp2016.Lib;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;

namespace School.Registrar.Student
{
    public partial class StudentBreakPop : PageBase
    {
        public int InvoiceId { get; set; }

        public StudentBreakPop() : base((int)CConstValue.Menu.Student)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InvoiceId = Convert.ToInt32(Request["id"]);

            if (!IsPostBack)
            {
                FileDownloadList1.InitFileDownloadList((int)CConstValue.Upload.Break);

                var vwInvoice = new CInvoice().GetVwInvoice(InvoiceId);
                if (vwInvoice != null)
                {
                    RadDatePickerProgramStartDate.SelectedDate = vwInvoice.StartDate;
                    RadDatePickerProgramEndDate.SelectedDate = vwInvoice.EndDate;
                }
                RadDatePickerStartDate.SelectedDate = DateTime.Today;
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
                            var cBreak = new CBreak();
                            var b = new Break();

                            b.InvoiceId = original.InvoiceId;
                            b.BreakStartDate = RadDatePickerStartDate.SelectedDate.Value;
                            b.BreakEndDate = RadDatePickerEndDate.SelectedDate.Value;

                            b.StartDate = RadDatePickerProgramStartDate.SelectedDate.Value;
                            b.EndDate = RadDatePickerProgramEndDate.SelectedDate.Value;
                            b.Reason = RadTextBoxReason.Text;
                            b.IsActive = true;
                            b.CreatedId = CurrentUserId;

                            int breakId = cBreak.Add(b);
                            if (breakId > 0)
                            {
                                // save uploading file
                                FileDownloadList1.SaveFile(breakId);

                                // Program
                                if (original.ProgramRegistrationId != null)
                                {
                                    var cProgramRegiInfo = new CProgramRegistration();
                                    var programRegiInfo = cProgramRegiInfo.Get(Convert.ToInt32(original.ProgramRegistrationId));
                                    programRegiInfo.EndDate = RadDatePickerProgramEndDate.SelectedDate.Value;
                                    programRegiInfo.UpdatedId = CurrentUserId;
                                    programRegiInfo.UpdatedDate = DateTime.Now;

                                    cProgramRegiInfo.Update(programRegiInfo);
                                }
                                // Homestay
                                else if (original.HomestayRegistrationId != null)
                                {
                                    var cHomestayPlacement = new CHomestayPlacement();
                                    var homestayStudentRequest = cHomestayPlacement.GetByStudentBasicId(Convert.ToInt32(original.HomestayRegistrationId));
                                    homestayStudentRequest.EndDate = RadDatePickerProgramEndDate.SelectedDate.Value;
                                    homestayStudentRequest.UpdatedId = CurrentUserId;
                                    homestayStudentRequest.UpdatedDate = DateTime.Now;

                                    cHomestayPlacement.Update(homestayStudentRequest);
                                }
                                // Dormitory
                                else if (original.DormitoryRegistrationId != null)
                                {
                                    var cDormitoryPlacement = new CDormitoryPlacement();
                                    var dormitoryPlacement = cDormitoryPlacement.GetByStudentBasicId(Convert.ToInt32(original.DormitoryRegistrationId));
                                    dormitoryPlacement.EndDate = RadDatePickerProgramEndDate.SelectedDate.Value;
                                    dormitoryPlacement.UpdatedId = CurrentUserId;
                                    dormitoryPlacement.UpdatedDate = DateTime.Now;

                                    cDormitoryPlacement.Update(dormitoryPlacement);
                                }

                                RunClientScript("Close();");
                            }
                            else
                                ShowMessage("failed to update inqury (Add Break)");
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
            var cInvoice = new CInvoice();
            var invoiceInfo = cInvoice.Get(InvoiceId);

            var cProgramReg = new CProgramRegistration();
            var ProReg = cProgramReg.Get(Convert.ToInt32(invoiceInfo.ProgramRegistrationId));

            if (RadDatePickerStartDate.SelectedDate < ProReg.StartDate)
            {
                RadDatePickerStartDate.SelectedDate = ProReg.StartDate;
            }
            if (RadDatePickerEndDate.SelectedDate != null)
            {
                if (RadDatePickerStartDate.SelectedDate > RadDatePickerEndDate.SelectedDate)
                {
                    RadDatePickerStartDate.SelectedDate = RadDatePickerEndDate.SelectedDate;
                    RadNumericTextBoxBreakDays.Value = 1;
                }
                else
                {
                    var days = new TimeSpan();
                    days = Convert.ToDateTime(RadDatePickerEndDate.SelectedDate) -
                           Convert.ToDateTime(RadDatePickerStartDate.SelectedDate);
                    RadNumericTextBoxBreakDays.Value = Convert.ToInt32(days.Days) + 1;
                }
            }
            else
            {
                RadDatePickerEndDate.SelectedDate = RadDatePickerStartDate.SelectedDate;
                RadNumericTextBoxBreakDays.Value = 1;
            }

            UpdatedEndDate();
        }

        protected void RadDatePickerEndDate_OnSelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            var cInvoice = new CInvoice();
            var invoiceInfo = cInvoice.Get(InvoiceId);

            var cProgramReg = new CProgramRegistration();
            var ProReg = cProgramReg.Get(Convert.ToInt32(invoiceInfo.ProgramRegistrationId));

            if (RadDatePickerEndDate.SelectedDate > ProReg.EndDate)
            {
                RadDatePickerEndDate.SelectedDate = ProReg.EndDate;
            }
            if (RadDatePickerStartDate != null)
            {
                if (RadDatePickerEndDate.SelectedDate < RadDatePickerStartDate.SelectedDate)
                {
                    RadDatePickerEndDate.SelectedDate = RadDatePickerStartDate.SelectedDate;
                    RadNumericTextBoxBreakDays.Value = 1;
                }
                else
                {
                    var days = new TimeSpan();
                    days = Convert.ToDateTime(RadDatePickerEndDate.SelectedDate) - Convert.ToDateTime(RadDatePickerStartDate.SelectedDate);
                    RadNumericTextBoxBreakDays.Value = Convert.ToInt32(days.Days) + 1;
                }
            }
            else
            {
                RadDatePickerStartDate.SelectedDate = RadDatePickerEndDate.SelectedDate;
                RadNumericTextBoxBreakDays.Value = 1;
            }

            UpdatedEndDate();
        }

        private void UpdatedEndDate()
        {
            if (RadDatePickerProgramEndDate.SelectedDate != null)
                RadDatePickerProgramEndDate.SelectedDate = RadDatePickerProgramEndDate.SelectedDate.Value.AddDays((double)RadNumericTextBoxBreakDays.Value);
        }

    }
}