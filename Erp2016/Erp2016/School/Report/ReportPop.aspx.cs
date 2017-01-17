using System;
using Erp2016.Lib;
using Erp2016.Lib.Report;
using Erp2016.Lib.Report.Academics;
using Erp2016.Lib.Report.Schools;
using Telerik.Reporting;

namespace School.Report
{
    public partial class ReportPop : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var reportBook = new ReportBook();

                var reportType = Convert.ToInt32(Request["reportType"]);
                var idParameters = Request["id"].Split(',');

                InstanceReportSource reportSource = new InstanceReportSource();
                switch (reportType)
                {
                    case (int)CConstValue.Report.InvoiceStudent:
                    case (int)CConstValue.Report.InvoiceAgency:
                        foreach (var id in idParameters)
                            reportBook.Reports.Add(new RInvoice(reportType, CurrentUserId, Convert.ToInt32(id)));

                        // lump sum invoice
                        if (reportBook.Reports.Count > 1)
                            reportBook.Reports.Add(new RInvoiceLumpSum(reportType, CurrentUserId, Request["id"]));
                        break;

                    case (int)CConstValue.Report.PaymentStudent:
                    case (int)CConstValue.Report.PaymentAgency:
                        var paymentReport = new RPayment(reportType, CurrentUserId, Convert.ToInt32(Request["id"]));
                        reportBook.Reports.Add(paymentReport);
                        break;

                    case (int)CConstValue.Report.DetailPaymentStudent:
                    case (int)CConstValue.Report.DetailPaymentAgency:
                        var payment = new CPayment().Get(Convert.ToInt32(idParameters[0]));
                        var detailPaymentReport = new RPayment(reportType, CurrentUserId, payment.InvoiceId, Request["id"]);
                        reportBook.Reports.Add(detailPaymentReport);
                        break;

                    // Schools
                    case (int)CConstValue.Report.LetterOfAcceptance:
                        var letterOfAcceptance = new RLetterOfAcceptance(Convert.ToInt32(Request["id"]));
                        reportBook.Reports.Add(letterOfAcceptance);
                        break;
                    case (int)CConstValue.Report.LetterOfAcceptanceInTable:
                        var letterOfAcceptanceInTable = new RLetterOfAcceptanceInTable(Convert.ToInt32(Request["id"]));
                        reportBook.Reports.Add(letterOfAcceptanceInTable);
                        break;
                    case (int)CConstValue.Report.StudentContract:
                        var letterOfAcceptanceInTable2 = new RLetterOfAcceptanceInTable(Convert.ToInt32(Request["id"]));
                        var refundPolicy = new RRefundPolicy(Convert.ToInt32(Request["id"]));
                        reportBook.Reports.Add(letterOfAcceptanceInTable2);
                        reportBook.Reports.Add(refundPolicy);
                        break;
                    case (int)CConstValue.Report.OrientationForm:
                        var orientationForm = new ROrientationForm(Convert.ToInt32(Request["id"]));
                        reportBook.Reports.Add(orientationForm);
                        break;
                    case (int)CConstValue.Report.ConfirmationOfCompletionLetter:
                        var confirmationOfCompletionLetter = new RConfirmationOfCompletionLetter(Convert.ToInt32(Request["id"]));
                        reportBook.Reports.Add(confirmationOfCompletionLetter);
                        break;
                    case (int)CConstValue.Report.ConfirmationOfEnrollment:
                        var confirmationOfEnrollment = new RConfirmationOfEnrollment(Convert.ToInt32(Request["id"]));
                        reportBook.Reports.Add(confirmationOfEnrollment);
                        break;

                    // Academy
                    case (int)CConstValue.Report.Certification:
                        var certification = new RCertification(Convert.ToInt32(Request["id"]));
                        reportBook.Reports.Add(certification);
                        break;

                    case (int)CConstValue.Report.ClassSummary:
                        var classSummary = new RClassSummary(Convert.ToInt32(Request["id"]));
                        reportBook.Reports.Add(classSummary);
                        break;

                    case (int)CConstValue.Report.StartingStudents:
                        var startingStudents = new RStartingStudents(Convert.ToInt32(Request["id"]));
                        reportBook.Reports.Add(startingStudents);
                        break;

                    case (int)CConstValue.Report.CompletedGraduatesStudents:
                        var completedGraduatesStudents = new RCompletedGraduatesStudents(Convert.ToInt32(Request["id"]));
                        reportBook.Reports.Add(completedGraduatesStudents);
                        break;

                    case (int)CConstValue.Report.AttendanceSheet:
                        var attendanceSheet = new RAttendanceSheet(Convert.ToInt32(Request["id"]));
                        reportBook.Reports.Add(attendanceSheet);
                        break;
                }

                reportSource.ReportDocument = reportBook;
                ReportViewer1.ReportSource = reportSource;
            }

        }
    }
}