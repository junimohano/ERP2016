using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;


namespace Erp2016.Lib
{
    public class CDormitoryHostRoom
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CDormitoryHostRoom()
        {

        }

        public DormitoryHostRoom Get(int id)
        {
            return _db.DormitoryHostRooms.FirstOrDefault(q => q.HostRoomId == id);
        }
        public List<DormitoryHostRoom> GetHostRoomList(int host_id)
        {
            List <DormitoryHostRoom> HostRoomList = new List<DormitoryHostRoom>();
            HostRoomList = _db.DormitoryHostRooms.Where(q => q.HostId == host_id).OrderBy(q => q.HostRoomName).ToList();
            return HostRoomList;

        }
        public int GetDormitoryHostRoomNumber(int hostid)
        {
            int room_number = 0;
            room_number = Convert.ToInt32(_db.DormitoryHostRooms.Where(q => q.HostId == hostid).Count().ToString());
            return room_number;
        }
        public int Add(DormitoryHostRoom obj)
        {
            try
            {

                _db.DormitoryHostRooms.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }

            return _db.DormitoryHostRooms.Max(x => x.HostRoomId);

        }

        public bool Update(DormitoryHostRoom obj)
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

        public bool Delete(DormitoryHostRoom  obj)
        {
            try
            {
                _db.DormitoryHostRooms.DeleteOnSubmit(obj);
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
