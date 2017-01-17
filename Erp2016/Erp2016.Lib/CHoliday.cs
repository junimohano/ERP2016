using System;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CHoliday
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public IQueryable<Holiday> Get(string pro)
        {
            return _db.Holidays.Where(x => x.Year == DateTime.Now.Year && x.IsActive && x.Province == pro);
        }

        public int Add(Holiday obj)
        {
            try
            {
                _db.Holidays.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.Holidays.Max(x => x.HolidayId);
        }

        public bool Update(Holiday obj)
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

        public bool Delete(Holiday obj)
        {
            try
            {
                _db.Holidays.DeleteOnSubmit(obj);
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