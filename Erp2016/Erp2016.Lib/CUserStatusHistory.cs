using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CUserStatusHistory
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CUserStatusHistory()
        {

        }
        public UserStatus Get(int id)
        {
            return _db.UserStatus.FirstOrDefault(q => q.UserStatusId== id);
        }

        public int Add(UserStatus obj)
        {
            try
            {
                _db.UserStatus.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.UserStatus.Max(x => x.UserStatusId);
        }

        public bool Update(UserStatus obj)
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

        public bool Delete(UserStatus obj)
        {
            try
            {
                _db.UserStatus.DeleteOnSubmit(obj);
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
