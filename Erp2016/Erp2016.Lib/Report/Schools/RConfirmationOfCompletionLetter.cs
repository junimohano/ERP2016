using System;
using System.Diagnostics;
using System.Drawing;

namespace Erp2016.Lib.Report.Schools
{
    /// <summary>
    /// Summary description for RTFTblLetterAcceptance.
    /// </summary>
    public partial class RConfirmationOfCompletionLetter : Telerik.Reporting.Report
    {
        public RConfirmationOfCompletionLetter(int invoiceId)
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

            htmlTextBoxSubTitle.Value = $@"{site.Name} - {siteLocation.Name} - Canada";
            htmlTextBoxStudentId.Value = $@"Student ID : {student.StudentNo}";
            htmlTextBoxDateOfIssue.Value = $@"Date of Issue : {DateTime.Today}";

            htmlTextBoxThisIs.Value = $@"This is to confirm that the following student has successfully completed their studies at {site.Name}.";

            htmlTextBoxFamilyName.Value = $@"FAMILY NAME : {student.LastName1}";
            htmlTextBoxFirstName.Value = $@"FIRST NAME : {student.FirstName}";
            htmlTextBoxDateOfBirth.Value = $@"DATE OF BIRTH : {student.DOB?.ToString("MM-dd-yy")}";
            htmlTextBoxProgram.Value = $@"PROGRAM : {program.ProgramFullName}";
            htmlTextBoxPeriod.Value = $@"PERIOD : {programRegistration.StartDate?.ToString("MM-dd-yy")} ~ {programRegistration.EndDate?.ToString("MM-dd-yy")}";

            switch (siteLocation.SiteId)
            {
                // CAC
                case 2:
                    textBoxName.Value = "Christine Jang";
                    textBoxJobTitle.Value = "Site Administrator";
                    break;
                default:
                    textBoxName.Value = string.Empty;
                    textBoxJobTitle.Value = string.Empty;
                    break;
            }

            try
            {
                var logoPath = new CGlobal().GetLogoImagePath((int)invoice.SiteLocationId, CConstValue.ImageType.Logo);
                if (logoPath != string.Empty)
                    pictureBoxCompanyLogo.Value = Image.FromFile(logoPath);

            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }

            try
            {
                var sideLogoPath = new CGlobal().GetLogoImagePath((int)invoice.SiteLocationId, CConstValue.ImageType.LogoSide);
                if (sideLogoPath != string.Empty)
                    pictureBoxSideLogo.Value = Image.FromFile(sideLogoPath);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
    }
}