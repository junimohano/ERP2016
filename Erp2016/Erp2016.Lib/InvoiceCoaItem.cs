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
    
    public partial class InvoiceCoaItem
    {
        public int InvoiceCoaItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemDetail { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public int ItemType { get; set; }
        public string CreditFlag { get; set; }
        public int RevenueRecognition { get; set; }
        public bool PackageProgramFlag { get; set; }
        public bool RefundFlag { get; set; }
        public bool GeneralFlag { get; set; }
        public bool SimpleFlag { get; set; }
        public bool ManualFlag { get; set; }
        public int Rank { get; set; }
        public Nullable<bool> IsOnlySystem { get; set; }
        public int CreatedId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> UpdatedId { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
