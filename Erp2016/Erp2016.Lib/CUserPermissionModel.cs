using System.Collections.Generic;
using System.Text;

namespace Erp2016.Lib
{
    public class CUserPermissionModel
    {
        public int MenuId { get; set; }

        public bool IsAccess { get; set; }
        public bool IsModify { get; set; }

        public List<CUserPermissionSearchSiteLocationModel> SearchSiteLocationList { get; set; }
        public StringBuilder SearchWhereSiteLocationSb { get; set; }

        public List<CUserPermissionSearchSiteModel> SearchSiteList { get; set; }
        public StringBuilder SearchWhereSiteSb { get; set; }
    }
}
