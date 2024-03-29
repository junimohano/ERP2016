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
    
    public partial class CreditMemoPayout
    {
        public int CreditMemoPayoutId { get; set; }
        public int CreditMemoId { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> PayoutMethod { get; set; }
        public string ChkHolderName { get; set; }
        public string ChkAddress { get; set; }
        public string ChkPhoneNum { get; set; }
        public string BankName { get; set; }
        public string BankAccountName { get; set; }
        public string BankBranchAddress { get; set; }
        public string InstitutionNo { get; set; }
        public string BranchNo { get; set; }
        public string TransitNo { get; set; }
        public string SwiftCode { get; set; }
        public string BankRouting { get; set; }
        public string AccountNo { get; set; }
        public bool Disbursement { get; set; }
        public Nullable<System.DateTime> DisbursementDate { get; set; }
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
