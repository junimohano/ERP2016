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
    
    public partial class BusinessTripCash
    {
        public int BusinessTripCashId { get; set; }
        public Nullable<int> BusinessTripId { get; set; }
        public Nullable<System.DateTime> CashDate { get; set; }
        public string CashCity { get; set; }
        public string CashTime { get; set; }
        public string CashMin { get; set; }
        public string AccomAgent { get; set; }
        public string AccomAgency { get; set; }
        public string GroundType { get; set; }
        public Nullable<decimal> GroundRate { get; set; }
        public string MealsType { get; set; }
        public Nullable<decimal> MealsRate { get; set; }
        public string CashNote { get; set; }
        public int CreatedId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> UpdatedId { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
