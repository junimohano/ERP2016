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
    
    public partial class ProgramFee
    {
        public int ProgramFeeId { get; set; }
        public Nullable<int> ProgramTuitionId { get; set; }
        public Nullable<int> ProgramId { get; set; }
        public int InvoiceCoaItemId { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
        public int CreatedId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int UpdatedId { get; set; }
        public System.DateTime UpdatedDate { get; set; }
    }
}
