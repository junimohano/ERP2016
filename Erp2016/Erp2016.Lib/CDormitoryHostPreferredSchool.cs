using System;
using System.Linq;
using System.Diagnostics;

namespace Erp2016.Lib
{
    public class CDormitoryHostPreferredSchool
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CDormitoryHostPreferredSchool ()
        {

        }

        public DormitoryHostPrefferedSchool  Get(int id)
        {
            return _db.DormitoryHostPrefferedSchools.FirstOrDefault(q => q.HostSchoolId == id);
        }

        //public ISingleResult<spGetDormitoryHostPreferredSchoolListResult> DormitoryHostPreferredSchoolList(int hostid)
        //{
        //    return vwdb.spGetDormitoryHostPreferredSchoolList(hostid);

        //}

        public DormitoryHostPrefferedSchool GetHostTopSchool(int hostid)
        {
            return _db.DormitoryHostPrefferedSchools.FirstOrDefault(q => q.HostId == hostid && q.DefaultHostSchool == true );
        }

        public SiteLocation GetHostTopShoolNameLocation(int SiteLocationId)
        {
            return _db.SiteLocations.FirstOrDefault(q => q.SiteLocationId == SiteLocationId);

        }
        public int Add(DormitoryHostPrefferedSchool obj)
        {
            try
            {

                _db.DormitoryHostPrefferedSchools.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }

            return _db.DormitoryHostPrefferedSchools.Max(x => x.HostSchoolId );

        }

        public bool Update(DormitoryHostPrefferedSchool  obj)
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
        public int DormitorySchoolSiteLocationId(int HostId)
        {
            int SiteLocationId = 0;

            return SiteLocationId;
        }

        public bool Delete(DormitoryHostPrefferedSchool obj)
        {
            try
            {
                _db.DormitoryHostPrefferedSchools.DeleteOnSubmit(obj);
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
