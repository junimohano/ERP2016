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
    
    public partial class Deposit
    {
        public int DepositId { get; set; }
        public int DepositIndex { get; set; }
        public string DepositNumber { get; set; }
        public int Status { get; set; }
        public int Bank { get; set; }
        public string Comment { get; set; }
        public int SiteLocationId { get; set; }
        public System.DateTime DepositDate { get; set; }
        public int CreatedId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> UpdatedId { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<System.DateTime> HQConfirmDate { get; set; }
        public Nullable<int> HQConfirmUserId { get; set; }
    }
}
