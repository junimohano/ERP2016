using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;

namespace App_Data
{
    public partial class RefundInfo : UserControl
    {
        private int InvoiceId
        {
            get
            {
                return (int)ViewState["InvoiceId"];
            }
            set { ViewState["InvoiceId"] = value; }
        }

        private int CurrentSiteLocationId
        {
            get
            {
                return (int)ViewState["CurrentSiteLocationId"];
            }
            set { ViewState["CurrentSiteLocationId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        public void InitReundInfo(int invoiceId, int currentSiteLocationId, bool isUsingRefund)
        {
            InvoiceId = invoiceId;
            CurrentSiteLocationId = currentSiteLocationId;
            RadDatePickerActualDate.SelectedDate = DateTime.Today;

            if (isUsingRefund)
                CalRefundDate();
            else {
                RadNumericTextBoxRefundRate.ReadOnly = false;
                RadNumericTextBoxRefundRate.Enabled = false;
            }
        }

        private void CalRefundDate()
        {
            var refundDate = Convert.ToDateTime(RadDatePickerActualDate.SelectedDate);

            //if (RefundDate < RequestDate)
            //    tbRefundDate.SelectedDate = RequestDate;

            var cInvoice = new CInvoice();
            var invoice = cInvoice.Get(InvoiceId);

            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;

            // Program
            if (invoice.ProgramRegistrationId != null)
            {
                var cProgramRegiInfo = new CProgramRegistration();
                var programRegiInfo = cProgramRegiInfo.Get(Convert.ToInt32(invoice.ProgramRegistrationId));
                startDate = Convert.ToDateTime(programRegiInfo.StartDate);
                endDate = Convert.ToDateTime(programRegiInfo.EndDate);
            }
            // Homestay
            else if (invoice.HomestayRegistrationId != null)
            {
                var cHomestayPlacement = new CHomestayPlacement();
                var homestayStudentRequest = cHomestayPlacement.GetByStudentBasicId(Convert.ToInt32(invoice.HomestayRegistrationId));
                startDate = Convert.ToDateTime(homestayStudentRequest.StartDate);
                endDate = Convert.ToDateTime(homestayStudentRequest.EndDate);
            }
            // Dormitory
            else if (invoice.DormitoryRegistrationId != null)
            {
                var cDormitoryPlacement = new CDormitoryPlacement();
                var programRegiInfo = cDormitoryPlacement.GetByStudentBasicId(Convert.ToInt32(invoice.DormitoryRegistrationId));
                startDate = Convert.ToDateTime(programRegiInfo.StartDate);
                endDate = Convert.ToDateTime(programRegiInfo.EndDate);
            }

            if (refundDate < startDate)
            {
                RadNumericTextBoxRefundRate.Value = 100;
            }
            else
            {
                //////////////////////////////////////////
                //Refund Policy Method Call ( CRefund.cs )
                //////////////////////////////////////////
                var refundData = new CRefund();
                var rates = refundData.RefundPolicy(startDate, endDate, refundDate, CurrentSiteLocationId);
                RadNumericTextBoxRefundRate.Value = rates[0];

                var studyRate = rates[1] > 100 ? 100 : rates[1];
                RadNumericTextBoxStudyRate.Value = studyRate;
            }
        }

        protected void tbRefundDate_OnSelectedDateChanged(object o, SelectedDateChangedEventArgs e)
        {
            CalRefundDate();
        }

        public RadNumericTextBox GetStudyRate()
        {
            return RadNumericTextBoxStudyRate;
        }
        public RadDatePicker RadActualDate()
        {
            return RadDatePickerActualDate;
        }
        public RadNumericTextBox GetRefundRate()
        {
            return RadNumericTextBoxRefundRate;
        }
        public RadTextBox GetReason()
        {
            return RadTextBoxReason;
        }
    }
}