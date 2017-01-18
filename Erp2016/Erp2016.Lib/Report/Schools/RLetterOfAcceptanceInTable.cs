using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace Erp2016.Lib.Report.Schools
{
    /// <summary>
    /// Summary description for RTFTblLetterAcceptance.
    /// </summary>
    public partial class RLetterOfAcceptanceInTable : Telerik.Reporting.Report
    {
        public RLetterOfAcceptanceInTable(int invoiceId)
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

            textBoxDLI.Value = $@"Designated learning institution number (DLI #) : {"O19375754382"}";
            textBoxDateOfIssue.Value = $@"Date of Issue : {DateTime.Today.ToString("MM-dd-yy")}";

            htmlTextBoxFaimlyName.Value = $@"1. Family Name : <br><b>{student.LastName1}</b>";
            htmlTextBoxFirstName.Value = $@"2. First Name and Initials : <br><b>{student.FirstName}</b>";
            htmlTextBoxDateOfBirth.Value = $@"3. Date of Birth : <br><b>{student.DOB?.ToString("MM-dd-yy")}</b>";
            htmlTextBoxStudentId.Value = $@"4. Student ID Number : <br><b>{student.StudentNo}</b>";
            htmlTextBoxStudentFull.Value = $@"5. Student Full Mailing Address : <br><b>{student.Address1InCanada}</b>";
            htmlTextBoxDates.Value = $@"6. Dates : <br>Start Date : <b>{programRegistration.StartDate?.ToString("MM-dd-yy")}</b><br>Completion Date : <b>{programRegistration.EndDate?.ToString("MM-dd-yy")}</b>";
            htmlTextBoxNameOfSchool.Value = $@"7. Name of School/Institution(include public or private) : <br><b>{site.Name}</b>";
            htmlTextBoxLevelOfStudy.Value = $@"8. Level of Study : <br><b>{"N/A"}</b>";
            htmlTextBoxProgram.Value = $@"9. Program/Major/Course : <br><b>{program.ProgramFullName + " " + (programRegistration.HrsStatus == null ? string.Empty : "(" + programRegistration.HrsStatus + "/week)")}</b>";
            htmlTextBoxHoursOfInstruction.Value = $@"10. Hours of Instruction per Week : <br><b>{programRegistration.Weeks}</b>";
            htmlTextBoxAcademicYear.Value = $@"11. Academic Year of Study which the student will enter (e.g., Year 2 of 3 Year Program)<br><b>{"N/A"}</b>";
            htmlTextBoxLateRegistrationDate.Value = $@"12. Late Registration Date : <br><b>{"N/A"}</b>";

            var vwStudentContract = new CStudent().GetVwStudentContract(invoiceId);
            var isFullPayment = false;
            if (vwStudentContract?.DepositConfirmCnt == vwStudentContract?.PaymentCnt && vwStudentContract.Balance == 0)
                isFullPayment = true;

            htmlTextBoxConditionOfAcceptance.Value = $@"13. Condition of Acceptance : (must be paid in full at least 2 weeks before start date)<br><b>{(isFullPayment ? "Full Fee Payment" : "Not fully Fee Payment")}</b>";

            var invoiceItemList = new CInvoiceItem().GetInvoiceItems(invoiceId);
            var tuitionFee = invoiceItemList.FirstOrDefault(x => x.InvoiceCoaItemId == (int)CConstValue.InvoiceCoaItem.TuitionBasic);

            htmlTextBoxEstimatedTuitionFees.Value = $@"14. Estimated Tuition Fees : (not including homestay accommodation fee)<br>Tuition Fee : <b>${tuitionFee}</b>";

            string scholarshipMasterNo = string.Empty;
            if (invoice.ScholarshipId != null)
                scholarshipMasterNo = new CScholarship().Get((int)invoice.ScholarshipId)?.ScholarshipMasterNo;

            htmlTextBoxScholarship.Value = $@"15. Scholarship/Teaching Assistantship: <br><b>{scholarshipMasterNo}</b>";
            htmlTextBoxExchangeStudent.Value = $@"16. Exchange Student (yes/no) : <br><b>{"No"}</b>";
            htmlTextBoxLicensingInformation.Value = $@"17. Licensing Information where applicable for Private Institution (yes/no/not applicable): <br><b>{"N/A"}</b>";
            htmlTextBoxIfDestinedForQuebec.Value = $@"18. If destined for Quebec, has CAQ information been sent to student (yes/no/not applicable) : <br><b>{"N/A"}</b>";
            htmlTextBoxGuardianship.Value = $@"19. Guardianship/Custodianship details if applicable : <br><b>{"N/A"}</b>";
            htmlTextBoxCredentials.Value = $@"20. Credentials : <br><b>{site.Name} Certificates and/or Diploma</b>";
            htmlTextBoxRequirementsForSuccessful.Value = $@"21. Requirements for successful program completion : <br><b>{"70% grade and 85% attendance"}</b>";
            htmlTextBoxSignatureOfInstitution.Value = $@"22. Signature of Institution Representative : <br>";

            string name = string.Empty;
            string position = string.Empty;
            switch (siteLocation.SiteId)
            {
                // CAC
                case 2:
                    name = "Christine Jang";
                    position = "Site Administrator";
                    break;
                default:
                    name = string.Empty;
                    position = string.Empty;
                    break;
            }

            htmlTextBoxNameOfInstitution.Value = $@"23. Name of Institution Representative (please print) : <br><b>{name} - {position}</b>";

            htmlTextBoxStudentSignature.Value = $@"24. Student's signature : <br><br><br><br><br>I have read and received a copy this contract and a copy of statement of the student's rights and responsibilities.";

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