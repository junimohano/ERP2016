
namespace Erp2016.Lib
{
    public class CFilterListModel
    {
        // share
        public string SiteName { get; set; }
        public string SiteLocationName { get; set; }
        public string CountryName { get; set; }
        public string AgencyName { get; set; }
        public string ProgramName { get; set; }
        public string StudentName { get; set; }
        public string UserName { get; set; }
        public string ApprovalUserName { get; set; }
        public string LoginId { get; set; }
        public string InstructorName { get; set; }
        public string ProgramStatusName { get; set; }

        // Approval status
        public string Status { get; set; }

        // Dashboard
        public string Type { get; set; }

        // invoice
        public string InvoiceCoaItemId { get; set; }
        public string InvoiceName { get; set; }
        public string InvoiceType { get; set; }
        public string InvoiceStatus { get; set; }

        // invoice, deposit
        public string PaidMethod { get; set; }

        // deposit
        public string DepositBank { get; set; }
        public string DepositStatus { get; set; }
        public string ExtraTypeName { get; set; }

        // Creditmemo
        public string StatusName { get; set; }
        public string CreditMemoType { get; set; }
        public string PayoutMethodName { get; set; }

        // Academic
        public string FacultyName { get; set; }
        public string ProgramGroupName { get; set; }

        // vacation
        public string VacationType { get; set; }

        // User
        public string CreatedUserName { get; set; }
        public string UpdatedUserName { get; set; }
        public string PositionName { get; set; }
        public string Email { get; set; }

        //Homestay
        public string FatherName { get; set; }
        public string RegistrationDate { get; set; }

        // PurchaseOrder
        public string PurchaseOrderTypeName { get; set; }
        public string PriorityTypeName { get; set; }
        public string ReviewTypeName { get; set; }

        // Inventory
        public string AssignedUserName { get; set; }
        public string InventoryCategoryName { get; set; }
        public string InventoryCategoryItemName { get; set; }
        public string ConditionName { get; set; }
        public string InUseName { get; set; }
    }
    
}
