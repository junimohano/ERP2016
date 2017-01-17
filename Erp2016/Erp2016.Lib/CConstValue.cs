using System;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Erp2016.Lib
{
    public static class CConstValue
    {
        public static readonly string WebSiteUrl = "http://www.prod.loyalistgroup.com";
        public static readonly string WebUploadPath = @"C:\inetpub\wwwroot\Erp2016Upload\";
        // admin id
        public static readonly int UserSystemId = 1762;

        // Color of Negtive price in RadGrid
        public static readonly string NagativeColorName = Color.OrangeRed.Name;

        public enum ImageType
        {
            Basic,
            Small,
            Logo,
            LogoSide,
            Sign
        }

        public enum VacationType
        {
            PaidVacationDay = 0,
            UnPaidVacationDay = 1,
            SickDay = 2,
            EntitlementDay = 3
        }

        public enum InvoiceCoaItem
        {
            // basic register for program
            TuitionBasic = 1,
            TuitionScholarship = 2,
            CommissionTuition = 6,
            Registration = 7,
            MaterialOthers = 11,
            Administration = 13,
            InternshipBasic = 27,
            TestExamFee = 31,
            Insurance = 33,
            ServiceCertificate = 45,
            Other = 73,

            // homestay or dormitory
            AirportPickup = 35,
            AirportPickupDiscount = 36,
            AirportDropoff = 37,
            AirportDropoffDiscount = 38,
            AirportPickupAndDropoff = 39,
            AirportPickupAndDropoffDiscount = 40
        }

        public enum InvoiceCoaItemForHomestay
        {
            HomestayBasic = 15,
            HomestayBasicDiscount = 16,
            HomestayPlacement = 17,
            HomestayPlacementDiscount = 18,
            HomestayInternetGuarantee = 19,
            HomestayOtherDiscount = 20
        }

        public enum InvoiceCoaItemForDormitory
        {
            DormitoryBasic = 21,
            DormitoryBasicDiscount = 22,
            DormitoryPlacement = 23,
            DormitoryPlacementDiscount = 24,
            DormitoryKeyDeposit = 25,
            DormitoryKeyDepositDiscount = 26
        }

        /// <summary>
        /// for sending mail
        /// </summary>
        public enum MailStatus
        {
            ToApproveUser,
            ToApproveUserAndRequestUser,
            ToRequestUser
        }

        public enum InvoiceStatus
        {
            Pending = 0,
            Invoiced = 1,
            Invoiced_Hold = 2,
            Cancelled_RF = 3,
            Cancelled_TF = 4,
            Cancelled_SC = 5,
            Cancelled_PC = 6,
            Cancelled_CC = 7,
            Cancelled_MD = 8,
            Cancelled_HR = 9,
            Cancelled_DR = 10,
            Cancelled_HS = 11,
            Cancelled_DS = 12,
            Cancelled_HC = 13,
            Cancelled_DC = 14,
            Cancelled_BR = 15,
        }

        public enum InvoiceType
        {
            General = 0,
            Simple = 1,
            Manual = 2,
            Refund_RF = 3,
            Refund_TF = 4,
            Refund_SC = 5,
            Refund_PC = 6,
            Refund_RV = 7,
            Homestay = 8,
            Dormitory = 9,
            Refund_HR = 10,
            Refund_DR = 11,
            Refund_HS = 12,
            Refund_DS = 13
        }

        public enum CreditMemoType
        {
            CP = 1,
            MDF = 2,
            OverPaid = 3,
            Refund = 4,
            HomestayBasic = 5,
            HomestayPickup = 6,
            HomestayDropOff = 7,
            HomestayPickupAndDropOff = 8
        }

        /// <summary>
        /// Approval Status
        /// </summary>
        public enum ApprovalStatus
        {
            Canceled = 0,
            Requested = 1,
            Approval1St = 2,
            Approval2Nd = 3,
            Approval3Rd = 4,
            Approval4Th = 5,
            Approval5Th = 6,
            Approval6Th = 7,
            Approval7Th = 8,
            Approval8Th = 9,
            Approval9Th = 10,
            Approval11Th = 11,
            Revise = 97,
            Rejected = 98,
            Approved = 99,
            WaitingForPreviewFromHq = 100,
            InProgress = 101
        }

        /// <summary>
        /// Approval status
        /// </summary>
        public enum Approval
        {
            Basic = 1,
            Agency = 2,
            Scholarship = 3,
            Refund = 4,
            BusinessTrip = 6,
            Package = 7,
            PurchaseOrder = 8,
            Expense = 9,
            Homestay = 10,
            Hire = 11,
            Promotion = 12,
            Vacation = 13,
            CreditMemoPayout = 14,
            CorporateCreditCard = 15
        }

        /// <summary>
        /// File Type
        /// </summary>
        public enum Upload
        {
            PackageProgram = 1,
            Refund = 2,
            Deposit = 3,
            BusinessTrip = 4,
            Expense = 5,
            Hire = 6,
            Homestay = 7,
            Promotion = 8,
            PurchaseOrder = 9,
            BulletinBoard = 10,
            CreditMemo = 11,
            HomestayAgency = 12,
            Dormitory = 13,
            Break = 14,
            Transfer = 15,
            Cancel = 16,
            ProgramChange = 17,
            ScheduleChange = 18,
            Student = 19,
            User = 20,
            HomestayCRC = 21,
            CorporateCreditCard = 22,
            Inventory = 23
        }

        /// <summary>
        /// Marketer UserGroup No
        /// </summary>
        public enum UserGroupForAdvisor
        {
            CAC = 217,
            KGIC = 225,
            PGIC = 244,
            SEC = 251,
            UCCBT = 257,
            UIS = 261,
            MTI = 238,
            KGIBC = 221
        }

        /// <summary>
        /// Instructor UserGroup No
        /// </summary>
        public enum UserGroupForInstructor
        {
            CAC = 216,
            KGIC = 227,
            PGIC = 246,
            SEC = 253,
            UCCBT = 255,
            UIS = 259,
            MTI = 239,
            KGIBC = 218,
            VIA = 264
        }

        /// <summary>
        /// usergroup for each permisson 
        /// </summary>
        public enum UserGroupForUserPermission
        {
            HR = 232,
            IT = 233
        }
        public enum UserGroupForUserInformation
        {
            HR = 232,
            IT = 233
        }
        public enum UserGroupForVacation
        {
            HR = 232,
            IT = 233
        }
        public enum UserGroupForDepositConfirm
        {
            Accounting = 229,
            IT = 233
        }
        public enum UserGroupForCreditPayoutHistory
        {
            Accounting = 229,
            IT = 233
        }

        public enum UserGroupForAccountExcelExport
        {
            Accounting = 229,
            IT = 233
        }

        public enum UserGroupForLoy
        {
            Accounting = 229,
            GandA = 230,
            GraphicDesign = 231,
            HR = 232,
            IT = 233,
            Management = 234,
            OverseasOffice = 235
        }

        public enum UserGroupForBrandDirector
        {
            CAC = 215,
            KGIBC = 219,
            KGIC = 226,
            MTI = 240,
            PGIC = 242,
            SEC = 252,
            UCCBT = 256,
            UIS = 260
        }
        public enum PermissionType
        {
            Access = 0,
            Modify = 1,
            Search = 2
        }

        public enum Report
        {
            InvoiceAgency,
            InvoiceStudent,
            PaymentAgency,
            PaymentStudent,
            DetailPaymentAgency,
            DetailPaymentStudent,

            // Schools
            LetterOfAcceptance,
            LetterOfAcceptanceInTable,
            StudentContract,
            OrientationForm,
            ConfirmationOfCompletionLetter,
            ConfirmationOfEnrollment,

            // Academy
            Certification,
            ClassSummary,
            StartingStudents,
            CompletedGraduatesStudents,
            ClassList,
            AttendanceSheet
        }

        public enum Menu
        {
            BulletinBoard = 0,
            Student = 1,
            GradeSchema = 2,
            Faculty = 3,
            ProgramGroup = 4,
            Program = 5,
            PackageProgram = 6,
            ProgramCourse = 7,
            ProgramCourseLevel = 8,
            ProgramClassRoom = 9,
            ProgramClass = 10,
            Scholarship = 11,
            Promotion = 12,
            Invoice = 13,
            Payment = 14,
            Deposit = 15,
            CreditMemo = 16,
            Refund = 17,
            Break = 18,
            Cancel = 19,
            Transfer = 20,
            ProgramChange = 21,
            ScheduleChange = 22,
            Agency = 23,
            User = 24,
            BusinessTrip = 25,
            BusinessTripForHq = 26,
            Expense = 27,
            ExpenseForHq = 28,
            PurchaseOrder = 29,
            PurchaseOrderForHq = 30,
            ApprovalChart = 31,
            Hire = 32,
            HireForHq = 33,
            Vacation = 34,
            VacationForHq = 35,
            Dictionary = 36,
            Holiday = 37,
            HomestayPlacementRequest = 38,
            HomestayHostRegistration = 39,
            DormitoryHostRegistration = 40,
            DormitoryRequestRegistration = 41,
            CorporateCreditCard = 42,
            CorporateCreditCardForHq = 43,
            CorporateCreditCardSchema = 44,
            Inventory = 45
        }

    }
}