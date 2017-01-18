using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;


namespace Erp2016.Lib
{
    public class CHomestayHostRoom
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CHomestayHostRoom()
        {

        }

        //spGetHomestayHostBedActive

        public HomestayHostRoom Get(int id)
        {
            return _db.HomestayHostRooms.FirstOrDefault(q => q.HostRoomId == id);
        }
        public List<HomestayHostRoom> GetHostRoomList(int host_id)
        {
            List <HomestayHostRoom> HostRoomList = new List<HomestayHostRoom>();
            HostRoomList = _db.HomestayHostRooms.Where(q => q.HostId == host_id).OrderBy(q => q.HostRoomName).ToList();
            return HostRoomList;

        }
        public int GetHomestayHostRoomNumber(int hostid)
        {
            int room_number = 0;
            room_number = Convert.ToInt32(_db.HomestayHostRooms.Where(q => q.HostId == hostid).Count().ToString());
            return room_number;
        }
        public int Add(HomestayHostRoom obj)
        {
            try
            {

                _db.HomestayHostRooms.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }

            return _db.HomestayHostRooms.Max(x => x.HostRoomId);

        }

        public bool Update(HomestayHostRoom obj)
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

        public bool Delete(HomestayHostRoom  obj)
        {
            try
            {
                _db.HomestayHostRooms.DeleteOnSubmit(obj);
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
