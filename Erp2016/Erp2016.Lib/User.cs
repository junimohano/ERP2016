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
    
    public partial class User
    {
        public int UserId { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }
        public Nullable<int> SINNo { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string MaritalStatus { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int UserPositionId { get; set; }
        public string EmployeeNumber { get; set; }
        public Nullable<int> Supervisor { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string PersonalEmail { get; set; }
        public string EContactName { get; set; }
        public string ERelation { get; set; }
        public string EPhone { get; set; }
        public string EAddress { get; set; }
        public int SiteLocationId { get; set; }
        public byte[] Picture { get; set; }
        public string EmailPassword { get; set; }
        public bool IsMarketer { get; set; }
        public bool IsActive { get; set; }
        public int CreatedId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> UpdatedId { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
