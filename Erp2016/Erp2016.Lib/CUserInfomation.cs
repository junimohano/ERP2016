using System;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CUserInfomation
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CUserInfomation()
        {
        }

        public UserInformation Get(int id)
        {
            return _db.UserInformations.FirstOrDefault(q => q.UserId == id);
        }

        public int Add(UserInformation obj)
        {
            try
            {
                _db.UserInformations.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.UserInformations.Max(x => x.UserInformationId);
        }

        public bool Update(UserInformation obj)
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

        public bool Delete(UserInformation obj)
        {
            try
            {
                _db.UserInformations.DeleteOnSubmit(obj);
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
