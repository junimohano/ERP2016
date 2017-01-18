
using System.Collections.Generic;
using System.Data.Linq;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System;


namespace Erp2016.Lib
{
    public class CDormitoryPlacement
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();
        private readonly linqViewDataContext _dbView = new linqViewDataContext();

        public CDormitoryPlacement()
        {

        }

        public DormitoryPlacement Get(int PlacementId)
        {
            return _db.DormitoryPlacements.First(q => q.HostPlacementId ==PlacementId);
        }


        public DormitoryPlacement GetByStudentBasicId(int studentBasicId)
        {
            return _db.DormitoryPlacements.First(q => q.StudentBasicId == studentBasicId);
        }

        public ISingleResult<spGetDormitoryPlacementByRequestIdResult> GetDormitoryPlacementByRequestId(int DormitoryStudentRequestId)
        {
            return _dbView.spGetDormitoryPlacementByRequestId(DormitoryStudentRequestId);
        }
        public ISingleResult<spGetDormitoryPlacementByHostIdResult> GetDormitoryPlacementByHostId(int DormitoryHostId)
        {
            return _dbView.spGetDormitoryPlacementByHostId(DormitoryHostId);
        }
        public ISingleResult<spGetDormitoryPlacementHistoryByHostIdResult> GetDormitoryPlacementHistoryByHostId(int DormitoryHostId)
        {
            return _dbView.spGetDormitoryPlacementHistoryByHostId(DormitoryHostId);
        }

        public int Add(DormitoryPlacement obj)
        {
            try
            {

                _db.DormitoryPlacements.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }

            return _db.DormitoryPlacements.Max(x => x.HostPlacementId);

        }
        public bool Update(DormitoryPlacement obj)
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

        public bool Delete(DormitoryPlacement obj)
        {
            try
            {
                _db.DormitoryPlacements.DeleteOnSubmit(obj);
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
