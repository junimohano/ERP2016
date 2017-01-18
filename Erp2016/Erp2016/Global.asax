<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Routing" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        RegisterRoutes(RouteTable.Routes);
    }

    private void RegisterRoutes(RouteCollection routes)
    {
        // Login
        routes.MapPageRoute(string.Empty, "Login", "~/Login.aspx");

        // Home
        routes.MapPageRoute(string.Empty, "Dashboard", "~/School/Home/Dashboard.aspx");
        routes.MapPageRoute(string.Empty, "MyMessage", "~/School/Home/MyMessage.aspx");

        routes.MapPageRoute(string.Empty, "BulletinBoardPop", "~/School/Home/BulletinBoardPop.aspx");
        routes.MapPageRoute(string.Empty, "MyMessagePop", "~/School/Home/MyMessagePop.aspx");

        // Office Admin
        routes.MapPageRoute(string.Empty, "User", "~/School/OfficeAdmin/User.aspx");
        routes.MapPageRoute(string.Empty, "Hire", "~/School/OfficeAdmin/Hire.aspx");
        routes.MapPageRoute(string.Empty, "HireForHq", "~/School/OfficeAdmin/HireForHq.aspx");
        routes.MapPageRoute(string.Empty, "Vacation", "~/School/OfficeAdmin/Vacation.aspx");
        routes.MapPageRoute(string.Empty, "VacationForHq", "~/School/OfficeAdmin/VacationForHq.aspx");
        routes.MapPageRoute(string.Empty, "BusinessTrip", "~/School/OfficeAdmin/BusinessTrip.aspx");
        routes.MapPageRoute(string.Empty, "BusinessTripForHq", "~/School/OfficeAdmin/BusinessTripForHq.aspx");
        routes.MapPageRoute(string.Empty, "Expense", "~/School/OfficeAdmin/Expense.aspx");
        routes.MapPageRoute(string.Empty, "ExpenseForHq", "~/School/OfficeAdmin/ExpenseForHq.aspx");
        routes.MapPageRoute(string.Empty, "PurchaseOrder", "~/School/OfficeAdmin/PurchaseOrder.aspx");
        routes.MapPageRoute(string.Empty, "PurchaseOrderForHq", "~/School/OfficeAdmin/PurchaseOrderForHq.aspx");
        routes.MapPageRoute(string.Empty, "CorporateCreditCard", "~/School/OfficeAdmin/CorporateCreditCard.aspx");
        routes.MapPageRoute(string.Empty, "CorporateCreditCardForHq", "~/School/OfficeAdmin/CorporateCreditCardForHq.aspx");
        routes.MapPageRoute(string.Empty, "CorporateCreditCardSchema", "~/School/OfficeAdmin/CorporateCreditCardSchema.aspx");
        routes.MapPageRoute(string.Empty, "Inventory", "~/School/OfficeAdmin/Inventory.aspx");

        routes.MapPageRoute(string.Empty, "BusinessTripPop", "~/School/OfficeAdmin/BusinessTripPop.aspx");
        routes.MapPageRoute(string.Empty, "ExpensePop", "~/School/OfficeAdmin/ExpensePop.aspx");
        routes.MapPageRoute(string.Empty, "PurchaseOrderPop", "~/School/OfficeAdmin/PurchaseOrderPop.aspx");
        routes.MapPageRoute(string.Empty, "UserPermissionPop", "~/School/OfficeAdmin/UserPermissionPop.aspx");
        routes.MapPageRoute(string.Empty, "UserInformationPop", "~/School/OfficeAdmin/UserInformationPop.aspx");
        routes.MapPageRoute(string.Empty, "HirePop", "~/School/OfficeAdmin/HirePop.aspx");
        routes.MapPageRoute(string.Empty, "VacationInfoPop", "~/School/OfficeAdmin/VacationInfoPop.aspx");
        routes.MapPageRoute(string.Empty, "VacationPop", "~/School/OfficeAdmin/VacationPop.aspx");
        routes.MapPageRoute(string.Empty, "CorporateCreditCardPop", "~/School/OfficeAdmin/CorporateCreditCardPop.aspx");
        routes.MapPageRoute(string.Empty, "CorporateCreditCardSchemaPop", "~/School/OfficeAdmin/CorporateCreditCardSchemaPop.aspx");
        routes.MapPageRoute(string.Empty, "InventoryPop", "~/School/OfficeAdmin/InventoryPop.aspx");

        // Registrar
        routes.MapPageRoute(string.Empty, "Student", "~/School/Registrar/Student/Student.aspx");
        routes.MapPageRoute(string.Empty, "Agency", "~/School/Registrar/Agency.aspx");
        routes.MapPageRoute(string.Empty, "PackageProgram", "~/School/Registrar/PackageProgram.aspx");
        routes.MapPageRoute(string.Empty, "Scholarship", "~/School/Registrar/Scholarship.aspx");
        routes.MapPageRoute(string.Empty, "Promotion", "~/School/Registrar/Promotion.aspx");
        routes.MapPageRoute(string.Empty, "Refund", "~/School/Registrar/Refund.aspx");
        routes.MapPageRoute(string.Empty, "Break", "~/School/Registrar/Break.aspx");
        routes.MapPageRoute(string.Empty, "Cancel", "~/School/Registrar/Cancel.aspx");
        routes.MapPageRoute(string.Empty, "Transfer", "~/School/Registrar/Transfer.aspx");
        routes.MapPageRoute(string.Empty, "ProgramChange", "~/School/Registrar/ProgramChange.aspx");
        routes.MapPageRoute(string.Empty, "ScheduleChange", "~/School/Registrar/ScheduleChange.aspx");

        routes.MapPageRoute(string.Empty, "PackageProgramNewPop", "~/School/Registrar/Student/PackageProgramNewPop.aspx");
        routes.MapPageRoute(string.Empty, "ProgramNewPop", "~/School/Registrar/Student/ProgramNewPop.aspx");

        routes.MapPageRoute(string.Empty, "StudentBreakPop", "~/School/Registrar/Student/StudentBreakPop.aspx");
        routes.MapPageRoute(string.Empty, "StudentCancelPop", "~/School/Registrar/Student/StudentCancelPop.aspx");
        routes.MapPageRoute(string.Empty, "StudentProgramChangePop", "~/School/Registrar/Student/StudentProgramChangePop.aspx");
        routes.MapPageRoute(string.Empty, "StudentRefundPop", "~/School/Registrar/Student/StudentRefundPop.aspx");
        routes.MapPageRoute(string.Empty, "StudentPop", "~/School/Registrar/Student/StudentPop.aspx");
        routes.MapPageRoute(string.Empty, "StudentScheduleChangePop", "~/School/Registrar/Student/StudentScheduleChangePop.aspx");
        routes.MapPageRoute(string.Empty, "StudentTransferPop", "~/School/Registrar/Student/StudentTransferPop.aspx");
        routes.MapPageRoute(string.Empty, "PackageProgramPop", "~/School/Registrar/PackageProgramPop.aspx");
        routes.MapPageRoute(string.Empty, "PromotionPop", "~/School/Registrar/PromotionPop.aspx");
        routes.MapPageRoute(string.Empty, "ScholarshipPop", "~/School/Registrar/ScholarshipPop.aspx");
        routes.MapPageRoute(string.Empty, "RefundPop", "~/School/Registrar/RefundPop.aspx");
        routes.MapPageRoute(string.Empty, "AgencyOldInfoPop", "~/School/Registrar/AgencyOldInfoPop.aspx");
        
        // StudentHousing
        routes.MapPageRoute(string.Empty, "HomestayRequestRegistration", "~/School/StudentHousing/HomestayRequestRegistration.aspx");
        routes.MapPageRoute(string.Empty, "HomestayHostRegistration", "~/School/StudentHousing/HomestayHostRegistration.aspx");
        routes.MapPageRoute(string.Empty, "HomestaySummary", "~/School/StudentHousing/HomestaySummary.aspx");
        routes.MapPageRoute(string.Empty, "NewHomestayStudentPop", "~/School/StudentHousing/NewHomestayStudentPop.aspx"); // New Homesaty Placement Reuqest
        routes.MapPageRoute(string.Empty, "PlacementbySchoolPop", "~/School/StudentHousing/PlacementbySchoolPop.aspx"); // Placement by School Homesaty Placement Reuqest
        routes.MapPageRoute(string.Empty, "PlacementbyAgencyPop", "~/School/StudentHousing/PlacementbyAgencyPop.aspx"); // Placement by Agency Homesaty Placement Reuqest
        
        routes.MapPageRoute(string.Empty, "CancelHomestayPop", "~/School/StudentHousing/CancelHomestayPop.aspx"); // Cancel Homesaty Placement Reuqest 
        routes.MapPageRoute(string.Empty, "PaymentHomestayPop", "~/School/StudentHousing/PaymentHomestayPop.aspx");// Homestay Payment
        routes.MapPageRoute(string.Empty, "DormitoryRequestRegistration", "~/School/StudentHousing/DormitoryRequestRegistration.aspx");
        routes.MapPageRoute(string.Empty, "DormitoryHostRegistration", "~/School/StudentHousing/DormitoryHostRegistration.aspx");
        routes.MapPageRoute(string.Empty, "NewDormitoryStudentPop", "~/School/StudentHousing/NewDormitoryStudentPop.aspx"); // New Homesaty Placement Reuqest
        routes.MapPageRoute(string.Empty, "CancelDormitoryPop", "~/School/StudentHousing/CancelDormitoryPop.aspx"); // Cancel Dormitory Placement Reuqest 
        routes.MapPageRoute(string.Empty, "PaymentDormitoryPop", "~/School/StudentHousing/PaymentDormitoryPop.aspx");// Dormitory Payment
        routes.MapPageRoute(string.Empty, "PlacementDormitoryPop", "~/School/StudentHousing/PlacementDormitorybySchoolPop.aspx"); //  Placement Dormitory       
        // Sales
        routes.MapPageRoute(string.Empty, "Invoice", "~/School/Sales/Invoice.aspx");
        routes.MapPageRoute(string.Empty, "Payment", "~/School/Sales/Payment.aspx");
        routes.MapPageRoute(string.Empty, "CreditMemo", "~/School/Sales/CreditMemo.aspx");
        routes.MapPageRoute(string.Empty, "Deposit", "~/School/Sales/Deposit.aspx");

        routes.MapPageRoute(string.Empty, "DepositPop", "~/School/Sales/DepositPop.aspx");
        routes.MapPageRoute(string.Empty, "CreditMemoPayoutPop", "~/School/Sales/CreditMemoPayoutPop.aspx");
        routes.MapPageRoute(string.Empty, "CreditMemoPayoutHistoryPop", "~/School/Sales/CreditMemoPayoutHistoryPop.aspx");
        routes.MapPageRoute(string.Empty, "DepositAddExtraPaymentPop", "~/School/Sales/DepositAddExtraPaymentPop.aspx");
        routes.MapPageRoute(string.Empty, "PaymentNewPop", "~/School/Sales/PaymentNewPop.aspx");
        routes.MapPageRoute(string.Empty, "SimpleInvoiceNewPop", "~/School/Sales/SimpleInvoiceNewPop.aspx");

        // Academic Registrar
        routes.MapPageRoute(string.Empty, "GradeSchema", "~/School/AcademicRegistrar/GradeSchema.aspx");
        routes.MapPageRoute(string.Empty, "Faculty", "~/School/AcademicRegistrar/Faculty.aspx");
        routes.MapPageRoute(string.Empty, "ProgramGroup", "~/School/AcademicRegistrar/ProgramGroup.aspx");
        routes.MapPageRoute(string.Empty, "Program", "~/School/AcademicRegistrar/Program.aspx");
        routes.MapPageRoute(string.Empty, "ProgramCourse", "~/School/AcademicRegistrar/ProgramCourse.aspx");
        routes.MapPageRoute(string.Empty, "ProgramCourseLevel", "~/School/AcademicRegistrar/ProgramCourseLevel.aspx");
        routes.MapPageRoute(string.Empty, "ProgramClassRoom", "~/School/AcademicRegistrar/ProgramClassRoom.aspx");
        routes.MapPageRoute(string.Empty, "ProgramClass", "~/School/AcademicRegistrar/ProgramClass.aspx");

        routes.MapPageRoute(string.Empty, "GradeSchemaPop", "~/School/AcademicRegistrar/GradeSchemaPop.aspx");
        routes.MapPageRoute(string.Empty, "ProgramClassStudentInformationPop", "~/School/AcademicRegistrar/ProgramClassStudentInformationPop.aspx");
        routes.MapPageRoute(string.Empty, "ProgramClassStudentInformationMovementPop", "~/School/AcademicRegistrar/ProgramClassStudentInformationMovementPop.aspx");
        routes.MapPageRoute(string.Empty, "ProgramClassStudentAttendancePop", "~/School/AcademicRegistrar/ProgramClassStudentAttendancePop.aspx");
        routes.MapPageRoute(string.Empty, "ProgramClassStudentGradePop", "~/School/AcademicRegistrar/ProgramClassStudentGradePop.aspx");

        // System
        routes.MapPageRoute(string.Empty, "ApprovalChart", "~/School/Systems/ApprovalChart.aspx");
        routes.MapPageRoute(string.Empty, "NoPermission", "~/School/Systems/NoPermission.aspx");
        routes.MapPageRoute(string.Empty, "Dict", "~/School/Systems/Dict.aspx");
        routes.MapPageRoute(string.Empty, "Holiday", "~/School/Systems/Holiday.aspx");

        // Report
        routes.MapPageRoute(string.Empty, "ReportPop", "~/School/Report/ReportPop.aspx");
        routes.MapPageRoute(string.Empty, "ReportPayout", "~/School/Report/ReportPayout.aspx");
        routes.MapPageRoute(string.Empty, "ReportDisbursement", "~/School/Report/ReportDisbursement.aspx");
        routes.MapPageRoute(string.Empty,"ReportHomestay","~/School/Report/ReportHomesaty.aspx");
        // Shared
        routes.MapPageRoute(string.Empty, "ApprovalAcceptPop", "~/School/Shared/ApprovalAcceptPop.aspx");
        routes.MapPageRoute(string.Empty, "ApprovalApprovePop", "~/School/Shared/ApprovalApprovePop.aspx");
        routes.MapPageRoute(string.Empty, "ApprovalCancelPop", "~/School/Shared/ApprovalCancelPop.aspx");
        routes.MapPageRoute(string.Empty, "ApprovalCompletePop", "~/School/Shared/ApprovalCompletePop.aspx");
        routes.MapPageRoute(string.Empty, "ApprovalRejectPop", "~/School/Shared/ApprovalRejectPop.aspx");
        routes.MapPageRoute(string.Empty, "ApprovalRevisePop", "~/School/Shared/ApprovalRevisePop.aspx");
        routes.MapPageRoute(string.Empty, "InvoiceItemGridPop", "~/School/Shared/InvoiceItemGridPop.aspx");
        routes.MapPageRoute(string.Empty, "PaymentHistoryGridPop", "~/School/Shared/PaymentHistoryGridPop.aspx");
        routes.MapPageRoute(string.Empty, "DepositPaymentGridPop", "~/School/Shared/DepositPaymentGridPop.aspx");
        
    }
</script>
