﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Erp2016.Lib
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class linqDBDataContext : DbContext
    {
        public linqDBDataContext()
            : base("name=linqDBDataContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Agency> Agencies { get; set; }
        public virtual DbSet<AgencyCommissionHistory> AgencyCommissionHistories { get; set; }
        public virtual DbSet<AgencySiteLocation> AgencySiteLocations { get; set; }
        public virtual DbSet<Approval> Approvals { get; set; }
        public virtual DbSet<ApprovalHistory> ApprovalHistories { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<Break> Breaks { get; set; }
        public virtual DbSet<BulletinBoard> BulletinBoards { get; set; }
        public virtual DbSet<BusinessTrip> BusinessTrips { get; set; }
        public virtual DbSet<BusinessTripAccom> BusinessTripAccoms { get; set; }
        public virtual DbSet<BusinessTripCash> BusinessTripCashes { get; set; }
        public virtual DbSet<BusinessTripFlight> BusinessTripFlights { get; set; }
        public virtual DbSet<Cancel> Cancels { get; set; }
        public virtual DbSet<CountryMarket> CountryMarkets { get; set; }
        public virtual DbSet<CreditMemo> CreditMemoes { get; set; }
        public virtual DbSet<CreditMemoCreditHistory> CreditMemoCreditHistories { get; set; }
        public virtual DbSet<CreditMemoPayout> CreditMemoPayouts { get; set; }
        public virtual DbSet<CreditMemoPayoutHistory> CreditMemoPayoutHistories { get; set; }
        public virtual DbSet<Deposit> Deposits { get; set; }
        public virtual DbSet<DepositPayment> DepositPayments { get; set; }
        public virtual DbSet<Dict> Dicts { get; set; }
        public virtual DbSet<DormitoryCancelScheduleChange> DormitoryCancelScheduleChanges { get; set; }
        public virtual DbSet<DormitoryCost> DormitoryCosts { get; set; }
        public virtual DbSet<DormitoryHost> DormitoryHosts { get; set; }
        public virtual DbSet<DormitoryHost00> DormitoryHost00 { get; set; }
        public virtual DbSet<DormitoryHostBed> DormitoryHostBeds { get; set; }
        public virtual DbSet<DormitoryHostBed00> DormitoryHostBed00 { get; set; }
        public virtual DbSet<DormitoryHostPrefferedSchool> DormitoryHostPrefferedSchools { get; set; }
        public virtual DbSet<DormitoryHostPrefferedSchool00> DormitoryHostPrefferedSchool00 { get; set; }
        public virtual DbSet<DormitoryHostRoom> DormitoryHostRooms { get; set; }
        public virtual DbSet<DormitoryHostRoom00> DormitoryHostRoom00 { get; set; }
        public virtual DbSet<DormitoryPlacement> DormitoryPlacements { get; set; }
        public virtual DbSet<DormitoryRegistration> DormitoryRegistrations { get; set; }
        public virtual DbSet<Expense> Expenses { get; set; }
        public virtual DbSet<ExpenseDetail> ExpenseDetails { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<GradeSchema> GradeSchemas { get; set; }
        public virtual DbSet<GradeSchemaItem> GradeSchemaItems { get; set; }
        public virtual DbSet<GradeSchemaLetterItem> GradeSchemaLetterItems { get; set; }
        public virtual DbSet<Hire> Hires { get; set; }
        public virtual DbSet<Holiday> Holidays { get; set; }
        public virtual DbSet<HomestayCancelScheduleChange> HomestayCancelScheduleChanges { get; set; }
        public virtual DbSet<HomestayCost> HomestayCosts { get; set; }
        public virtual DbSet<HomestayHostBasic> HomestayHostBasics { get; set; }
        public virtual DbSet<HomestayHostBasic00> HomestayHostBasic00 { get; set; }
        public virtual DbSet<HomestayHostBed> HomestayHostBeds { get; set; }
        public virtual DbSet<HomestayHostBed00> HomestayHostBed00 { get; set; }
        public virtual DbSet<HomestayHostFamilyMember00> HomestayHostFamilyMember00 { get; set; }
        public virtual DbSet<HomestayHostPrefferedSchool00> HomestayHostPrefferedSchool00 { get; set; }
        public virtual DbSet<HomestayHostRoom> HomestayHostRooms { get; set; }
        public virtual DbSet<HomestayHostRoom00> HomestayHostRoom00 { get; set; }
        public virtual DbSet<HomestayPlacement> HomestayPlacements { get; set; }
        public virtual DbSet<HomestayStudentBasic> HomestayStudentBasics { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceCoaItem> InvoiceCoaItems { get; set; }
        public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }
        public virtual DbSet<InvoiceItemFg> InvoiceItemFgs { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<PackageProgram> PackagePrograms { get; set; }
        public virtual DbSet<PackageProgramDetail> PackageProgramDetails { get; set; }
        public virtual DbSet<PackageProgramSiteLocation> PackageProgramSiteLocations { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Program> Programs { get; set; }
        public virtual DbSet<ProgramChange> ProgramChanges { get; set; }
        public virtual DbSet<ProgramClass> ProgramClasses { get; set; }
        public virtual DbSet<ProgramClassRoom> ProgramClassRooms { get; set; }
        public virtual DbSet<ProgramClassStudent> ProgramClassStudents { get; set; }
        public virtual DbSet<ProgramCourse> ProgramCourses { get; set; }
        public virtual DbSet<ProgramCourseLevel> ProgramCourseLevels { get; set; }
        public virtual DbSet<ProgramFee> ProgramFees { get; set; }
        public virtual DbSet<ProgramGroup> ProgramGroups { get; set; }
        public virtual DbSet<ProgramOtherFeeInfo> ProgramOtherFeeInfoes { get; set; }
        public virtual DbSet<ProgramOtherInfo> ProgramOtherInfoes { get; set; }
        public virtual DbSet<ProgramRegistration> ProgramRegistrations { get; set; }
        public virtual DbSet<ProgramSiteLocation> ProgramSiteLocations { get; set; }
        public virtual DbSet<ProgramTuition> ProgramTuitions { get; set; }
        public virtual DbSet<Promotion> Promotions { get; set; }
        public virtual DbSet<PromotionSiteLocation> PromotionSiteLocations { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public virtual DbSet<Refund> Refunds { get; set; }
        public virtual DbSet<ScheduleChange> ScheduleChanges { get; set; }
        public virtual DbSet<Scholarship> Scholarships { get; set; }
        public virtual DbSet<ScholarshipSiteLocation> ScholarshipSiteLocations { get; set; }
        public virtual DbSet<Site> Sites { get; set; }
        public virtual DbSet<SiteLocation> SiteLocations { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<SystemLog> SystemLogs { get; set; }
        public virtual DbSet<Transfer> Transfers { get; set; }
        public virtual DbSet<UploadFile> UploadFiles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<UserInformation> UserInformations { get; set; }
        public virtual DbSet<UserPermission> UserPermissions { get; set; }
        public virtual DbSet<UserPosition> UserPositions { get; set; }
        public virtual DbSet<UserSalary> UserSalaries { get; set; }
        public virtual DbSet<UserStatusHistory> UserStatusHistories { get; set; }
        public virtual DbSet<Vacation> Vacations { get; set; }
        public virtual DbSet<VacationDetail> VacationDetails { get; set; }
        public virtual DbSet<VacationSchema> VacationSchemas { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<HomestayHostFamilyMember> HomestayHostFamilyMembers { get; set; }
        public virtual DbSet<HomestayHostPrefferedSchool> HomestayHostPrefferedSchools { get; set; }
        public virtual DbSet<Student_KGIC> Student_KGIC { get; set; }
    }
}
