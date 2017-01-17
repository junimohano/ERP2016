using System;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CInvoiceItemFg
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CInvoiceItemFg()
        {
        }

        public InvoiceItemFg Get(int id)
        {
            return _db.InvoiceItemFgs.FirstOrDefault(q => q.InvoiceItemId == id);
        }
        
        public int Add(InvoiceItemFg obj)
        {
            try
            {
                _db.InvoiceItemFgs.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.InvoiceItemFgs.Max(x => x.InvoiceItemFgId);
        }
        
        public bool Update(InvoiceItemFg obj)
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

        public bool Delete(InvoiceItemFg obj)
        {
            try
            {
                _db.InvoiceItemFgs.DeleteOnSubmit(obj);
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