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
    
    public partial class InvoiceItem
    {
        public int InvoiceItemId { get; set; }
        public int InvoiceId { get; set; }
        public int InvoiceCoaItemId { get; set; }
        public Nullable<decimal> StandardPrice { get; set; }
        public Nullable<decimal> StudentPrice { get; set; }
        public Nullable<decimal> AgencyPrice { get; set; }
        public string Remark { get; set; }
        public int Rank { get; set; }
        public int CreatedId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> UpdatedId { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
