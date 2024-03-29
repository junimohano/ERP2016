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
    
    public partial class HomestayHostBasic
    {
        public int HostId { get; set; }
        public string FatherLastName { get; set; }
        public string FatherFirstName { get; set; }
        public string FatherOccupation { get; set; }
        public Nullable<System.DateTime> FatherDOB { get; set; }
        public Nullable<bool> FatherCRC { get; set; }
        public string FathereMail { get; set; }
        public string FatherWorkPhone { get; set; }
        public string FatherCellPhone { get; set; }
        public Nullable<bool> FatherGuardian { get; set; }
        public string MotherLastName { get; set; }
        public string MotherFirstName { get; set; }
        public string MotherOccupation { get; set; }
        public Nullable<System.DateTime> MotherDOB { get; set; }
        public Nullable<bool> MotherCRC { get; set; }
        public string MotherWorkPhone { get; set; }
        public string MotherCellPhone { get; set; }
        public string MothereMail { get; set; }
        public Nullable<bool> MotherGuardian { get; set; }
        public Nullable<int> HouseType { get; set; }
        public string HouseAddress { get; set; }
        public string HouseCity { get; set; }
        public string HouseProvince { get; set; }
        public string HousePostalCode { get; set; }
        public string HousePhone { get; set; }
        public Nullable<bool> HouseEglishSpoken { get; set; }
        public string HouseLanguageSpoken { get; set; }
        public Nullable<bool> OptionsSmokingPermitted { get; set; }
        public Nullable<bool> OptionsPetFlag { get; set; }
        public string OptionsPetTyeRemark { get; set; }
        public Nullable<bool> OptionsInternetOffered { get; set; }
        public Nullable<bool> OptionsInternetIncHomestayFee { get; set; }
        public Nullable<int> OptionsInternetExtraCharge { get; set; }
        public string OptionsInternet_Type { get; set; }
        public string OptionsInternet_Usage { get; set; }
        public Nullable<bool> MealPlanBreakfastType { get; set; }
        public Nullable<bool> MealPlanLunchType { get; set; }
        public Nullable<bool> MealPlanDinnerType { get; set; }
        public Nullable<int> FridgeSharedSerperatedAccess { get; set; }
        public Nullable<bool> HouseAlarmSystem { get; set; }
        public Nullable<bool> HouseAlarmPasswordGiven { get; set; }
        public Nullable<int> HouseActiveStutas { get; set; }
        public Nullable<System.DateTime> HouseActiveDate { get; set; }
        public Nullable<System.DateTime> HouseInactiveDate { get; set; }
        public string HouseInactiveReason { get; set; }
        public string AdminHostCompanyName { get; set; }
        public string AdminHostCompanyPerson { get; set; }
        public string AdminCompanyPersonEmail { get; set; }
        public string AdminCompanyPersonPhone { get; set; }
        public string AdminHostEmergencyPerson { get; set; }
        public string AdminHostEmergencyPhone { get; set; }
        public string AdminHostEmergencyReleationship { get; set; }
        public Nullable<int> CreatedId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedId { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
