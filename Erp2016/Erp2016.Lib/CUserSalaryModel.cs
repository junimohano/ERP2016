using System;

namespace Erp2016.Lib
{
    public class CUserSalaryModel
    {
        public string DisplayName { get; set; }
        public int SalaryId { get; set; }
        public int UserId { get; set; }
        public int SalaryType { get; set; }
        public decimal Salary { get; set; }
        public DateTime EffectDate { get; set; }
    }
}
