using System;

namespace Erp2016.Lib
{
    [Serializable]
    public class CVacationTypeInfoModel
    {
        public CVacationTypeModel ThisYear { get; set; }
        public CVacationTypeModel NextYear { get; set; }
    }
}
