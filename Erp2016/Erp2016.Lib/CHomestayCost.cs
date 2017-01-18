using System.Linq;

namespace Erp2016.Lib
{
    public class CHomestayCost
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CHomestayCost()
        {

        }

        public HomestayCost Get(int SiteLocationId)
        {
            return _db.HomestayCosts.First(q => q.SiteLocationId == SiteLocationId);
        }

    }
}