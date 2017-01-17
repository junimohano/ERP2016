using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CCancel
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CCancel()
        {
        }

        public Cancel Get(int id)
        {
            return _db.Cancels.FirstOrDefault(q => q.CancelId == id);
        }

        public int Add(Cancel obj)
        {
            try
            {
                obj.CreatedDate = DateTime.Now;

                _db.Cancels.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.Cancels.Max(x => x.CancelId);
        }

        public bool Update(Cancel obj)
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

        public bool Delete(Cancel obj)
        {
            try
            {
                _db.Cancels.DeleteOnSubmit(obj);
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