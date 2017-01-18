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
    
    public partial class PurchaseOrder
    {
        public int PurchaseOrderId { get; set; }
        public int PurchaseOrderType { get; set; }
        public Nullable<int> PriorityType { get; set; }
        public Nullable<int> ShippingMethodType { get; set; }
        public Nullable<int> ShippingTerms { get; set; }
        public Nullable<System.DateTime> ShippingDeliveryDate { get; set; }
        public string Description { get; set; }
        public string VendorName { get; set; }
        public string VendorAddress { get; set; }
        public string VendorCity { get; set; }
        public string VendorProvince { get; set; }
        public string VendorPostalCode { get; set; }
        public string VendorPhone { get; set; }
        public string VendorEmail { get; set; }
        public string ShipToName { get; set; }
        public string ShipToAddress { get; set; }
        public string ShipToCity { get; set; }
        public string ShipToProvince { get; set; }
        public string ShipToPostalCode { get; set; }
        public string ShipToPhone { get; set; }
        public string ShipToEmail { get; set; }
        public Nullable<int> ReviewType { get; set; }
        public string ReviewMemo { get; set; }
        public Nullable<int> ReviewUserId { get; set; }
        public Nullable<System.DateTime> ReviewDate { get; set; }
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
