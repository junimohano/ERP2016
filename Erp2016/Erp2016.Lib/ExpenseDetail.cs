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
    
    public partial class ExpenseDetail
    {
        public int ExpenseDetailId { get; set; }
        public Nullable<int> ExpenseId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Office { get; set; }
        public Nullable<decimal> Lodging { get; set; }
        public Nullable<decimal> Ground { get; set; }
        public Nullable<decimal> Meals { get; set; }
        public Nullable<decimal> Advertising { get; set; }
        public Nullable<decimal> Mail { get; set; }
        public Nullable<decimal> Telephone { get; set; }
        public Nullable<decimal> Km { get; set; }
        public Nullable<decimal> Miscellaneous { get; set; }
        public int CreatedId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> UpdatedId { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}