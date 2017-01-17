using System;
using System.Diagnostics;
using System.Drawing;

namespace Erp2016.Lib.Report.Schools
{
    /// <summary>
    /// Summary description for RTFLetterAcceptance.
    /// </summary>
    public partial class RLetterOfAcceptance : Telerik.Reporting.Report
    {
        public RLetterOfAcceptance(int invoiceId)
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

            var studentGender = (student.Gender == false ? "Mr. " : "Ms. ");
            textBoxDate.Value = DateTime.Today.ToString("MM-dd-yy");
            // id
            textBoxId.Value = "ID : " + student.StudentNo;
            // letter of acceptance
            textBoxLetterOfAcceptance.Value = $@"Letter Of Acceptance: {studentGender + cStudent.GetStudentFullName(student)}";
            // date of birth
            textBoxDateOfBirth.Value = $@"(Date of Birth: {student.DOB?.ToString("MM-dd-yy")})";
            // DLI
            textBoxDLI.Value = $@"Designated learning institution number (DLI #) : {"O19375754382"}";

            var programType = "Part-time";
            var hours = string.Empty;
            var weeks = string.Empty;

            if (programRegistration.HrsStatus != null)
            {
                if (programRegistration.HrsStatus >= 20)
                    programType = "Full-time";

                hours = $"({programRegistration.HrsStatus} hours per week)";
            }

            if (programRegistration.Weeks != null)
                weeks = programRegistration.Weeks + " weeks";

            var program = new CProgram().Get(programRegistration.ProgramId);
            var siteLocation = new CSiteLocation().Get(student.SiteLocationId);
            var site = new CSite().Get(siteLocation.SiteId);
            // this letter
            textBoxThisLetter.Value = $@"This letter certifies that {studentGender + cStudent.GetStudentFullName(student)} has been accepted for {programType} studies {hours} of {program?.ProgramFullName} at the {siteLocation.Name} campus of {site.Abbreviation}. The period of enrollment is {weeks} beginning {programRegistration.StartDate?.ToString("MM-dd-yy")} and ending {programRegistration.EndDate?.ToString("MM-dd-yy")}.";
            
            // if
            textBoxIfYouShould.Value = $@"If you should have any questions regarding the enrollment of {studentGender + student.LastName1} at our college, please do not hesitate to contact our campus director.";

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