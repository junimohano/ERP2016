using System;
using System.Diagnostics;
using System.Drawing;

namespace Erp2016.Lib.Report.Schools
{
    /// <summary>
    /// Summary description for RTFTblLetterAcceptance.
    /// </summary>
    public partial class ROrientationForm : Telerik.Reporting.Report
    {
        public ROrientationForm(int invoiceId)
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

            htmlTextBoxDate.Value = "Date : " + DateTime.Today.ToString("MM-dd-yy");

            textBoxRe.Value = $"RE: STUDENT ORIENTATION FOR {program.ProgramFullName}";

            htmlTextBoxBody.Value = $@"
<b>TO: {new CStudent().GetStudentFullName(student)} #{student.StudentNo}</b><br>
C/O: GLOBAL INTERCITY STUDENT CENTER<br><br>

We sincerely welcome you to {site.Name}. Your session starts {programRegistration.StartDate?.ToString("MM-dd-yy")}
and it is very important that you be here for your level placement and orientation.<br><br>

<b>ORIENTATION</b><br>
<b><u>{site.Abbreviation}'s orientation starts 9:00am {programRegistration.StartDate?.ToString("MM-dd-yy")} and students are asked to come to school
by no later than 8:50am.</u></b>Counselors will inform you on school policies, class schedules along with
a brief tour of the outlying area.<br>
<b><u>YOUR FIRST DAY AT SCHOOL INCLUDES</u></b><br>
1. A written placement test<br>
2. Orientation with counselors<br>
3. Individual oral interview with a school instructor<br><br>

<b>PLEASE MAKE SURE TO BRING FOLLOWNG ITEMS WITH YOU:</b><br>
1. A pencil and an eraser for the Placement Test<br>
2. A photocopy of your passport(the page with your passport photo)<br>
3. A photocopy of your Valid Immigration Document(Study Permit / Work Permit / Visitor's
Record)<br>
4. A photocopy of your Medical Insurance Document<br>
5. A photocopy of your Letter of Acceptance and the Refund Policy with your signatures on<br><br>

<b>CHANGE OF SCHEDULE</b><br>
<b>If you are not able to attend the placement/orientation, you must notify the school
immediately.</b><br><br>

<b>CLEARING CUSTOMS</b><br>
You may not study for over 6 months when entering Canada with a tourist visa. Please have with
you your {site.Abbreviation} Letter of Acceptance and Homestay detail. Also, it is a good idea to be prepared to
answer simple question that the customs officer may have for you.";


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