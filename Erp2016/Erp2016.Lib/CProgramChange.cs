using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CProgramChange
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CProgramChange()
        {
        }

        public ProgramChange Get(int id)
        {
            return _db.ProgramChanges.FirstOrDefault(q => q.ProgramChangeId == id);
        }

        public int Add(ProgramChange obj)
        {
            try
            {
                obj.CreatedDate = DateTime.Now;

                _db.ProgramChanges.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.ProgramChanges.Max(x => x.ProgramChangeId);
        }

        public bool Update(ProgramChange obj)
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

        public bool Delete(ProgramChange obj)
        {
            try
            {
                _db.ProgramChanges.DeleteOnSubmit(obj);
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