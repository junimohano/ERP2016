using System.Diagnostics;

namespace Erp2016.Lib.Report.Academics
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for RStartingStudents.
    /// </summary>
    public partial class RStartingStudents : Telerik.Reporting.Report
    {
        public RStartingStudents(int programClassId)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            var programClass = new CProgramClass().Get(programClassId);

            ReportParameters["SiteLocationId"].Value = programClass?.SiteLocationId;
            ReportParameters["StatusId"].Value = 1;
            ReportParameters["StartDate"].Value = DateTime.Today.AddMonths(-1);
            ReportParameters["EndDate"].Value = DateTime.Today;

        }
        
        private void RStartingStudents_ItemDataBinding(object sender, EventArgs e)
        {
            try
            {
                Telerik.Reporting.Processing.Report rpt = (Telerik.Reporting.Processing.Report)sender;
                htmlTextBoxDate.Value = ((DateTime)rpt.Parameters["StartDate"].Value).ToString("MM-dd-yyyy") + " ~ " + ((DateTime)rpt.Parameters["EndDate"].Value).ToString("MM-dd-yyyy");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
    }
}