using System.Data;
using System.IO;
using System.Web;

namespace Erp2016.Lib.Report
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for InvoiceReport.
    /// </summary>
    public partial class RPayment : Telerik.Reporting.Report
    {
        public RPayment(int reportType, int currentUserId, int invoiceId, string paymentArray = null)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            ReportParameters["InvoiceId"].Value = invoiceId;

            var cUser = new CUser();
            var user = cUser.Get(currentUserId);
            textBoxReceiptIssuer.Value = cUser.GetUserName(user);

            var sqlDataSourcePaymentHistoryParameter = sqlDataSourcePaymentHistory.Parameters[0];
            var detailsTableFilter = detailsTable.Filters[0];
            var tableTotalFilter = tableTotal.Filters[0];

            switch (reportType)
            {
                case (int)CConstValue.Report.PaymentStudent:
                    // default
                    break;

                case (int)CConstValue.Report.PaymentAgency:
                    // Payment
                    textBoxInvoiceTitle.Value = "Receipt (Net)";
                    textBoxTotalInvoice.Value = "= Fields.AgencyPriceSum";
                    textBoxBalance.Value = "= Fields.AgencyPriceSum - Fields.PayAmount";

                    // Invoice
                    itemDescriptionTextBoxStudentPrice.Value = "= Fields.AgencyPrice";
                    textBoxStudentPriceSum2.Value = "=Sum(Fields.AgencyPrice)";
                    break;

                case (int)CConstValue.Report.DetailPaymentStudent:
                    sqlDataSourcePaymentHistoryParameter.Name = "PaymentId";
                    sqlDataSourcePaymentHistoryParameter.Value = "In (" + paymentArray + ")";

                    detailsTableFilter.Expression = "= Fields.PaymentId";
                    detailsTableFilter.Operator = FilterOperator.In;
                    detailsTableFilter.Value = "= Split(\",\", \"" + paymentArray + "\")";

                    tableTotalFilter.Expression = "= Fields.PaymentId";
                    tableTotalFilter.Operator = FilterOperator.In;
                    tableTotalFilter.Value = "= Split(\",\", \"" + paymentArray + "\")";
                    break;

                case (int)CConstValue.Report.DetailPaymentAgency:
                    sqlDataSourcePaymentHistoryParameter.Name = "PaymentId";
                    sqlDataSourcePaymentHistoryParameter.Value = "In (" + paymentArray + ")";

                    detailsTableFilter.Expression = "= Fields.PaymentId";
                    detailsTableFilter.Operator = FilterOperator.In;
                    detailsTableFilter.Value = "= Split(\",\", \"" + paymentArray + "\")";

                    tableTotalFilter.Expression = "= Fields.PaymentId";
                    tableTotalFilter.Operator = FilterOperator.In;
                    tableTotalFilter.Value = "= Split(\",\", \"" + paymentArray + "\")";

                    // additional
                    textBoxInvoiceTitle.Value = "Receipt (Net)";
                    textBoxTotalInvoice.Value = "= Fields.AgencyPriceSum";
                    textBoxBalance.Value = "= Fields.AgencyPriceSum - Fields.PayAmount";

                    // Invoice
                    itemDescriptionTextBoxStudentPrice.Value = "= Fields.AgencyPrice";
                    textBoxStudentPriceSum2.Value = "=Sum(Fields.AgencyPrice)";
                    break;
            }

            var invoice = new CInvoice().Get(invoiceId);
            if (invoice != null)
            {
                var logoPath = new CGlobal().GetLogoImagePath((int)invoice.SiteLocationId, CConstValue.ImageType.Basic);
                if (logoPath != string.Empty)
                    companyLogoPictureBox.Value = Image.FromFile(logoPath);
            }
        }

    }
}