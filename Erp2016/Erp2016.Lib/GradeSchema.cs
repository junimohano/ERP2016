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
    
    public partial class GradeSchema
    {
        public int GradeSchemaId { get; set; }
        public string Name { get; set; }
        public bool IsGlobal { get; set; }
        public int SiteLocationId { get; set; }
        public Nullable<int> ProgramId { get; set; }
        public Nullable<int> ProgramCourseId { get; set; }
        public Nullable<int> ProgramCourseLevelId { get; set; }
        public Nullable<int> ProgramClassId { get; set; }
        public int CreatedId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> UpdatedId { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
