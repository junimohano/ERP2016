using System.Data.Linq;
using System.Diagnostics;
using System.Linq;
using System;

namespace Erp2016.Lib
{
    public class CHomestayPlacement
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();
        private readonly linqViewDataContext _dbView = new linqViewDataContext();

        public CHomestayPlacement()
        {

        }

        public HomestayPlacement Get(int PlacementId)
        {
            return _db.HomestayPlacements.First(q => q.HostPlacementId ==PlacementId);
        }
        public int CountPlacementbyBasicId(int StudentBasicID)
        {
            int result = 0;

            result = Convert.ToInt32(_db.HomestayPlacements.Count (q => q.StudentBasicId==StudentBasicID).ToString());
            return result;
        }

        public HomestayPlacement GetByStudentBasicId(int studentBasicId)
        {
            return _db.HomestayPlacements.First(q => q.StudentBasicId == studentBasicId);
        }

        public ISingleResult<spGetHomestayPlacementByRequestIdResult> GetHomestayPlacementByRequestId(int HomestayStudentRequestId)
        {
            return _dbView.spGetHomestayPlacementByRequestId(HomestayStudentRequestId);
        }
        public ISingleResult<spGetHomestayPlacementByHostIdResult> GetHomestayPlacementByHostId(int HostId)
        {
            return _dbView.spGetHomestayPlacementByHostId(HostId);
        }
        public ISingleResult<spGetHomestayPlacementByAgencyResult> GetHomestayPlacementByAgency(int StudentRequestId)
        {
            return _dbView.spGetHomestayPlacementByAgency(StudentRequestId);
        }

        public int Add(HomestayPlacement obj)
        {
            try
            {

                _db.HomestayPlacements.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }

            return _db.HomestayPlacements.Max(x => x.HostPlacementId);

        }
        public bool Update(HomestayPlacement obj)
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

        public bool Delete(HomestayPlacement obj)
        {
            try
            {
                _db.HomestayPlacements.DeleteOnSubmit(obj);
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
