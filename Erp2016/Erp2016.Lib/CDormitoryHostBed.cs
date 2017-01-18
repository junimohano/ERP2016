using System.Data.Linq;
using System.Diagnostics;
using System.Linq;
using System;

namespace Erp2016.Lib
{
    public class CDormitoryHostBed
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();
        private readonly linqViewDataContext _dbView = new linqViewDataContext();

        public CDormitoryHostBed()
        {

        }

        public DormitoryHostBed Get(int id)
        {
            return _db.DormitoryHostBeds.FirstOrDefault(q => q.HostBedId == id);
        }

        public int GetDormitoryHostBedNumber(int hostid)
        {
            int bed_number = 0;
            bed_number = Convert.ToInt32(_db.DormitoryHostBeds.Where(q => q.HostId == hostid).Count().ToString());
            return bed_number;
        }
        public ISingleResult<spGetDormitoryHostBedListResult> DormitoryHostBedList(int hostid)
        {
            return _dbView.spGetDormitoryHostBedList(@hostid);
        }

        public ISingleResult<spGetDormitoryHostBedbyRoomResult> DormitoryHostBedByRoom(int hostid, int roomid)
        {

            return _dbView.spGetDormitoryHostBedbyRoom(hostid, roomid);
        }
        public ISingleResult<spHostBedPlacementResult> GetDormitoryBedPlaced(int BedId)
        {
            return _dbView.spHostBedPlacement(BedId);
        }

        public int Add(DormitoryHostBed obj)
        {
            try
            {

                _db.DormitoryHostBeds.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }

            return _db.DormitoryHostBeds.Max(x => x.HostBedId);

        }

        public bool Update(DormitoryHostBed  obj)
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

        public bool Delete(DormitoryHostBed  obj)
        {
            try
            {
                _db.DormitoryHostBeds.DeleteOnSubmit(obj);
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
