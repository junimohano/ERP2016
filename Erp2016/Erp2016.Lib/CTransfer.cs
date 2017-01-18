using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CTransfer
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CTransfer()
        {
        }

        public Transfer Get(int id)
        {
            return _db.Transfers.FirstOrDefault(q => q.TransferId == id);
        }

        public int Add(Transfer obj)
        {
            try
            {
                obj.CreatedDate = DateTime.Now;

                _db.Transfers.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.Transfers.Max(x => x.TransferId);
        }

        public bool Update(Transfer obj)
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

        public bool Delete(Transfer obj)
        {
            try
            {
                _db.Transfers.DeleteOnSubmit(obj);
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