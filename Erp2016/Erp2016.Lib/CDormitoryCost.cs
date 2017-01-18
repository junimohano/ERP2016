using System.Linq;

namespace Erp2016.Lib
{
    public class CDormitoryCost
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CDormitoryCost()
        {
        }

        public DormitoryCost Get(int SiteLocationId)
        {
            return _db.DormitoryCosts.First(q => q.SiteLocationId == SiteLocationId);
        }

    }
}