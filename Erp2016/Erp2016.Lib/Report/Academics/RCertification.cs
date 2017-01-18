using System;
using System.Diagnostics;
using System.Drawing;

namespace Erp2016.Lib.Report.Academics
{
    /// <summary>
    /// Summary description for RTFTblLetterAcceptance.
    /// </summary>
    public partial class RCertification : Telerik.Reporting.Report
    {
        public RCertification(int invoiceId)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            var invoice = new CInvoice().Get(invoiceId);
            if (invoice?.ProgramRegistrationId == null) return;

            var programRegistration = new CProgramRegistration().Get((int)invoice.ProgramRegistrationId);
            if (programRegistration == null) return;

            var cStudent = new CStudent();
            var student = cStudent.Get((int)invoice.StudentId);
            if (student == null) return;
            
            var program = new CProgram().Get(programRegistration.ProgramId);
            var siteLocation = new CSiteLocation().Get(student.SiteLocationId);
            var site = new CSite().Get(siteLocation.SiteId);

            var weeks = programRegistration.Weeks == null ? string.Empty : programRegistration.Weeks + " weeks";
            var programDescription = program.ProgramFullName + (programRegistration.HrsStatus != null ? $"({programRegistration.HrsStatus}/week)" : string.Empty) + " Program";

            htmlTextBoxBody.Value = $@"This Certification awarded to<br>
<b>{cStudent.GetStudentFullName(student)}</b><br>
for successfully completing {weeks} in the<br>
<b>{programDescription}</b><br>
at {site.Name}, {siteLocation.Name}, ON, Canada";

            htmlTextBoxDate.Value = $"Dated : <b>{DateTime.Today.ToString("MM-dd-yy")}</b>";

            try
            {
                var signPath = new CGlobal().GetLogoImagePath((int)invoice.SiteLocationId, CConstValue.ImageType.Sign);
                if (signPath != string.Empty)
                    pictureBoxSign.Value = Image.FromFile(signPath);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
    }
}