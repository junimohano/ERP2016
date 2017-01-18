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
    public partial class RInvoice : Telerik.Reporting.Report
    {
        public RInvoice(int reportType, int currentUserId, int invoiceId)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            
            ReportParameters["InvoiceId"].Value = invoiceId;

            var cUser = new CUser();
            var user = cUser.Get(currentUserId);
            textBoxInvoiceIssuer.Value = cUser.GetUserName(user);

            switch (reportType)
            {
                case (int)CConstValue.Report.InvoiceStudent:
                    // default
                    break;
                case (int)CConstValue.Report.InvoiceAgency:
                    textBoxInvoiceTitle.Value = "Invoice (Net)";
                    itemDescriptionTextBoxStudentPrice.Value = "= Fields.AgencyPrice";
                    textBoxStudentPriceSum1.Value = "=Sum(Fields.AgencyPrice)";
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

        private void RInvoice_ItemDataBound(object sender, EventArgs e)
        {
            Telerik.Reporting.Processing.Report rpt = (Telerik.Reporting.Processing.Report)sender;
            detailsTable.Visible = rpt.Parameters["IsDetail"].Value.ToString() == "True";
        }
    }
}