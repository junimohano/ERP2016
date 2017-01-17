//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Agency
    {
        public int AgencyId { get; set; }
        public Nullable<int> ParentAgencyId { get; set; }
        public string AgencyNumber { get; set; }
        public int AgencyIndex { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string GroupName { get; set; }
        public string PrintingName { get; set; }
        public string AgencyType { get; set; }
        public Nullable<double> CommissionRateBasic { get; set; }
        public Nullable<double> CommissionRateSeasonal { get; set; }
        public Nullable<double> CreditLimit { get; set; }
        public Nullable<int> Location { get; set; }
        public Nullable<int> MainTargetCountry { get; set; }
        public Nullable<System.DateTime> ContractStartDate { get; set; }
        public Nullable<System.DateTime> ContractEndDate { get; set; }
        public string APPaymentTerm { get; set; }
        public string APPaymentMethod { get; set; }
        public string APBillingType { get; set; }
        public string APPaymentPriority { get; set; }
        public string APPaymentSchedule { get; set; }
        public string ARCollectionTerm { get; set; }
        public string ARType { get; set; }
        public string ARCollectionPriority { get; set; }
        public string ARCollectionSchedule { get; set; }
        public string ARCollectionMethod { get; set; }
        public string AgencyRegNo { get; set; }
        public string Currency { get; set; }
        public string Comment { get; set; }
        public string BizNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Salutation { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string PEmail { get; set; }
        public string SEmail { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Postal { get; set; }
        public Nullable<int> CountryId { get; set; }
        public string FirstName1 { get; set; }
        public string LastName1 { get; set; }
        public string Salutation1 { get; set; }
        public string Phone1 { get; set; }
        public string Mobile1 { get; set; }
        public string Fax1 { get; set; }
        public string PEmail1 { get; set; }
        public string SEmail1 { get; set; }
        public string Website1 { get; set; }
        public string Address1 { get; set; }
        public string City1 { get; set; }
        public string Province1 { get; set; }
        public string Postal1 { get; set; }
        public Nullable<int> CountryId1 { get; set; }
        public bool IsActive { get; set; }
        public int CreatedId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> UpdatedId { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> ApprovalId { get; set; }
        public Nullable<System.DateTime> ApprovalDate { get; set; }
        public Nullable<int> ApprovalStatus { get; set; }
        public string ApprovalMemo { get; set; }
    }
}
