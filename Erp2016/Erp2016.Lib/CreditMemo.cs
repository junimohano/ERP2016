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
    
    public partial class CreditMemo
    {
        public int CreditMemoId { get; set; }
        public int CreditMemoType { get; set; }
        public int CreditMemoIndex { get; set; }
        public string CreditMemoNumber { get; set; }
        public int CreditMemoPartialIndex { get; set; }
        public int InvoiceId { get; set; }
        public Nullable<int> PaymentId { get; set; }
        public decimal OriginalCreditMemoAmount { get; set; }
        public Nullable<System.DateTime> CreditMemoStartDate { get; set; }
        public Nullable<System.DateTime> CreditMemoEndDate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> UpdatedId { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
