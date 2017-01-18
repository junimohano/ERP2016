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
    public partial class RInvoiceLumpSum : Telerik.Reporting.Report
    {
        public RInvoiceLumpSum(int reportType, int currentUserId, string invoiceArray)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            // = Split(",", "26427, 26429") => filter
            // In (Parameters.InvoiceId.Value, Parameters.InvoiceId2.Value) => query

            sqlDataSourceInvoiceDetail.Parameters[0].Value = "In (" + invoiceArray + ")";
            tableTotal.Filters[0].Value = "= Split(\",\", \"" + invoiceArray + "\")";
            
            switch (reportType)
            {
                case (int)CConstValue.Report.InvoiceStudent:
                    // default
                    break;
                case (int)CConstValue.Report.InvoiceAgency:
                    textBoxInvoiceTitle.Value = "Invoice Lump Sum (Net)";
                    textBoxStudentPriceSum1.Value = "=Sum(Fields.AgencyPrice)";
                    break;
            }

            var cUser = new CUser();
            var user = cUser.Get(currentUserId);
            textBoxInvoiceIssuer.Value = cUser.GetUserName(user);
            var logoPath = new CGlobal().GetLogoImagePath(user.SiteLocationId, CConstValue.ImageType.Basic);
            if (logoPath != string.Empty)
                companyLogoPictureBox.Value = Image.FromFile(logoPath);
        }
        
    }
}