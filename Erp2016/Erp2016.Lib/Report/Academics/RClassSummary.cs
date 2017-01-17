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
    public partial class RClassSummary : Telerik.Reporting.Report
    {
        public RClassSummary(int programClassId)
        {
            InitializeComponent();

            var programClass = new CProgramClass().Get(programClassId);
            ReportParameters["SiteLocationId"].Value = programClass?.SiteLocationId;
        }

        private void RClassSummary_ItemDataBinding(object sender, EventArgs e)
        {
            try
            {
                Telerik.Reporting.Processing.Report rpt = (Telerik.Reporting.Processing.Report)sender;
                var country = new CCountry().Get(Convert.ToInt32(rpt.Parameters["CountryId"].Value));
                htmlTextBoxCountry.Value = "<b>Student numbers in classes : </b>" + country?.Name;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
    }
}