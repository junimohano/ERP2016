using System;

namespace Erp2016.Lib
{
    public class CUserHistoryModel
    {        
        public string DisplayName { get; set; }        
        public int HistoryId { get; set; }
        public int UserId { get; set; }
        public int Type { get; set; }
        public DateTime? IssuedDate { get; set; }
        public string Reason { get; set; }
        public int? ROEIssue { get; set; }

        public DateTime CreatedDate { get; set; }
        public int CreatedId { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedId { get; set; }
    }
}
