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
    
    public partial class HomestayPlacement
    {
        public int HostPlacementId { get; set; }
        public Nullable<int> HostId { get; set; }
        public Nullable<int> StudentBasicId { get; set; }
        public Nullable<int> RoomId { get; set; }
        public Nullable<int> BedId { get; set; }
        public Nullable<int> StudentId { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> PlacementStatus { get; set; }
        public Nullable<int> PlacementType { get; set; }
        public Nullable<int> CreatedId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedId { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string PlacementFilebyAgency { get; set; }
        public Nullable<int> HomestayAgencyId { get; set; }
    }
}
