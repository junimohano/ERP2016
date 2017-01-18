using System;

namespace Erp2016.Lib
{
    public class CProgramModel
    {
        public int ProgramRegistrationId { get; set; }
        public int StudentId { get; set; }
        public int ProgramId { get; set; }
        public int? PackageProgramId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Shift { get; set; }
        public int? AgencyId { get; set; }
        public int? AgencyContactId { get; set; }
        public string AgencyContactName { get; set; }
        public string ProgramComment { get; set; }
        public string PaymentComment { get; set; }
        public int? HrsStatus { get; set; }
        public int? Weeks { get; set; }
        public int? Months { get; set; }
        public decimal? Tuition { get; set; }
        public int SiteId { get; set; }
        public int? SiteLocationId { get; set; }
        public int? ScholarshipId { get; set; }
        public int? PromotionId { get; set; }
        public bool IsCancel { get; set; }
        public DateTime? CancelDate { get; set; }
        public bool IsWithdraw { get; set; }
        public DateTime? WithdrawDate { get; set; }
        public bool IsTransfer { get; set; }
        public DateTime? TransferDate { get; set; }
        public int? TransferFromId { get; set; }
        public string ProgramFullName { get; set; }
        public string AgencyName { get; set; }
        public string StudentMasterNo { get; set; }
        public string StudentNo { get; set; }
        public string FirstName { get; set; }
        public string LastName1 { get; set; }
        public string PackageProgramName { get; set; }
    }
}