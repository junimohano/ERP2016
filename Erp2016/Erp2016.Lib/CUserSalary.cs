using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CUserSalary
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CUserSalary()
        {

        }
        public UserSalary Get(int id)
        {
            return _db.UserSalaries.FirstOrDefault(q => q.UserSalaryId == id);
        }
        
        public int Add(UserSalary obj)
        {
            try
            {
                _db.UserSalaries.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.UserSalaries.Max(x => x.UserSalaryId);
        }

        public bool Update(UserSalary obj)
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

        public bool Delete(UserSalary obj)
        {
            try
            {
                _db.UserSalaries.DeleteOnSubmit(obj);
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
