using System;

namespace Erp2016.Lib
{
    [Serializable]
    public class CVacationTypeModel
    {
        public double PaidTotalDays { get; set; }
        public double PaidUsedDays { get; set; }
        public DateTime PaidDate { get; set; }

        public double SickTotalDays { get; set; }
        public double SickUsedDays { get; set; }
        public DateTime SickDate { get; set; }

        public double EntitlementTotalDays { get; set; }
        public double EntitlementUsedDays { get; set; }
        public DateTime EntitlementDate { get; set; }
    }
}
