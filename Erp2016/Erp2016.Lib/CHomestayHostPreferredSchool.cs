using System;
using System.Linq;
using System.Diagnostics;
using System.Data.Linq;

namespace Erp2016.Lib
{
    public class CHomestayHostPreferredSchool
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();
        private readonly linqViewDataContext _dbView = new linqViewDataContext();

        public CHomestayHostPreferredSchool ()
        {

        }

        public HomestayHostPrefferedSchool  Get(int id)
        {
            return _db.HomestayHostPrefferedSchools.FirstOrDefault(q => q.HostSchoolId == id);
        }

        public ISingleResult<spGetHomestayHostPreferredSchoolListResult> HomestayHostPreferredSchoolList(int hostid)
        {
            return _dbView.spGetHomestayHostPreferredSchoolList(hostid);

        }
        public HomestayHostPrefferedSchool GetHostTopSchool(int hostid)
        {
            return _db.HomestayHostPrefferedSchools.FirstOrDefault(q => q.HostId == hostid && q.DefaultHostSchool == true );
        }
        public SiteLocation GetHostTopShoolNameLocation(int SiteLocationId)
        {
            return _db.SiteLocations.FirstOrDefault(q => q.SiteLocationId == SiteLocationId);

        }
        public int Add(HomestayHostPrefferedSchool obj)
        {
            try
            {

                _db.HomestayHostPrefferedSchools.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }

            return _db.HomestayHostPrefferedSchools.Max(x => x.HostSchoolId );

        }

        public bool Update(HomestayHostPrefferedSchool  obj)
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

        public bool Delete(HomestayHostPrefferedSchool obj)
        {
            try
            {
                _db.HomestayHostPrefferedSchools.DeleteOnSubmit(obj);
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
