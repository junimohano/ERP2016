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
    /// Summary description for RStudentSchedule.
    /// </summary>
    public partial class RStudentSchedule : Telerik.Reporting.Report
    {
       
        private int CurrentUserId { get; set; }
        public RStudentSchedule (): this (0)
        {
        }

        public RStudentSchedule(int currentUserId)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            CurrentUserId = currentUserId;

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        public void SetData()
        {
            var cUser = new CUser();
            var user = cUser.Get(CurrentUserId);
          


            var invoice = new CInvoice().Get(Convert.ToInt32(ReportParameters["InvoiceId"].Value));
            if (invoice != null)
            {
                var logoPath = new CGlobal().GetLogoImagePath((int)invoice.SiteLocationId);
                if (logoPath != string.Empty)
                    companyLogoPictureBox.Value = Image.FromFile(logoPath);
            }
        }
    }
}