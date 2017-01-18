using System;
using System.Linq;
using System.Diagnostics;
using System.Data.Linq;

namespace Erp2016.Lib
{
    public class CDormitoryHost
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();
        private readonly linqViewDataContext _dbView = new linqViewDataContext();

        public CDormitoryHost()
        {

        }

        public DormitoryHost Get(int id)
        {
            return _db.DormitoryHosts.FirstOrDefault(q => q.DormitoryHostId == id);
        }
        public ISingleResult<spGetDormitoryHostListResult> GetDormitoryHostList(int SiteLocationId)
        {
            return _dbView.spGetDormitoryHostList(SiteLocationId);
        }
        public ISingleResult<spGetDormitoryHostActiveResult> GetDormitoryHostActiveList(int SiteLocationId, DateTime StartDate, DateTime EndDate)
        {
            return _dbView.spGetDormitoryHostActive(SiteLocationId, StartDate, EndDate);

        }

        public int MaxHostId()
        {
            int result = 0;

            result = Convert.ToInt32(_db.DormitoryHosts.Max(q => q.DormitoryHostId).ToString());
            return result;
        }


        public int Add(DormitoryHost obj)
        {
            try
            {

                _db.DormitoryHosts.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }

            return _db.DormitoryHosts.Max(x => x.DormitoryHostId);

        }

        public bool Update(DormitoryHost obj)
        {
            try
            {
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public bool Delete(DormitoryHost obj)
        {
            try
            {
                _db.DormitoryHosts.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

    }
}
