using System;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CScheduleChange
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CScheduleChange()
        {
        }

        public ScheduleChange Get(int id)
        {
            return _db.ScheduleChanges.FirstOrDefault(q => q.ScheduleChangeId == id);
        }

        public int Add(ScheduleChange obj)
        {
            try
            {
                obj.CreatedDate = DateTime.Now;

                _db.ScheduleChanges.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.ScheduleChanges.Max(x => x.ScheduleChangeId);
        }

        public bool Update(ScheduleChange obj)
        {
            try
            {
                obj.UpdatedDate = DateTime.Now;

                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public bool Delete(ScheduleChange obj)
        {
            try
            {
                _db.ScheduleChanges.DeleteOnSubmit(obj);
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