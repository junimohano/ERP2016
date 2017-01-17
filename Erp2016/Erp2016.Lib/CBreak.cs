using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CBreak
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CBreak()
        {
        }

        public Break Get(int id)
        {
            return _db.Breaks.FirstOrDefault(q => q.BreakId == id);
        }

        public int Add(Break obj)
        {
            try
            {
                obj.CreatedDate = DateTime.Now;

                _db.Breaks.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.Breaks.Max(x => x.BreakId);
        }

        public bool Update(Break obj)
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

        public bool Delete(Break obj)
        {
            try
            {
                _db.Breaks.DeleteOnSubmit(obj);
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