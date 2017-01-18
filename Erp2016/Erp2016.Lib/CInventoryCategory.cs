using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CInventoryCategory
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();
        private readonly linqViewDataContext _dbView = new linqViewDataContext();

        public CInventoryCategory()
        {
        }

        public InventoryCategory Get(int id)
        {
            return _db.InventoryCategories.FirstOrDefault(q => q.InventoryCategoryId == id);
        }
        
        public int Add(InventoryCategory obj)
        {
            try
            {
                _db.InventoryCategories.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.InventoryCategories.Max(x => x.InventoryCategoryId);
        }

        public bool Update(InventoryCategory obj)
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

        public bool Delete(InventoryCategory obj)
        {
            try
            {
                _db.InventoryCategories.DeleteOnSubmit(obj);
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