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
    
    public partial class PackageProgram
    {
        public int PackageProgramId { get; set; }
        public Nullable<int> ProgramId { get; set; }
        public string PackageProgramName { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
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
