using System.Data.Linq;
using System.Diagnostics;
using System.Linq;
using System;

namespace Erp2016.Lib
{
    public class CHomestayHostBed
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();
        private readonly linqViewDataContext _dbView = new linqViewDataContext();

        public CHomestayHostBed()
        {

        }

        public HomestayHostBed Get(int id)
        {
            return _db.HomestayHostBeds.FirstOrDefault(q => q.HostBedId == id);
        }

        public int GetHomestayHostBedNumber(int hostid)
        {
            int bed_number = 0;
            bed_number = Convert.ToInt32(_db.HomestayHostBeds.Where(q => q.HostId == hostid).Count().ToString());
            return bed_number;
        }
        public ISingleResult<spGetHomestayHostBedListResult> HomestayHostBedList(int hostid)
        {
            return _dbView.spGetHomestayHostBedList(@hostid);
        }

        public ISingleResult<spGetHomestayHostBedbyRoomResult> HomestayHostBedByRoom(int hostid, int roomid)
        {
            return _dbView.spGetHomestayHostBedbyRoom(hostid, roomid);
        }
        public ISingleResult<spHostBedPlacementResult> GetHomestayBedPlaced(int BedId)
        {
            return _dbView.spHostBedPlacement(BedId);
        }

        public int Add(HomestayHostBed obj)
        {
            try
            {

                _db.HomestayHostBeds.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }

            return _db.HomestayHostBeds.Max(x => x.HostBedId);

        }

        public bool Update(HomestayHostBed obj)
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

        public bool Delete(HomestayHostBed obj)
        {
            try
            {
                _db.HomestayHostBeds.DeleteOnSubmit(obj);
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
