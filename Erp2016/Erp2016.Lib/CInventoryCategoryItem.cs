using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CInventoryCategoryItem
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();
        private readonly linqViewDataContext _dbView = new linqViewDataContext();

        public CInventoryCategoryItem()
        {
        }

        public InventoryCategoryItem Get(int id)
        {
            return _db.InventoryCategoryItems.FirstOrDefault(q => q.InventoryCategoryItemId == id);
        }
        
        public int Add(InventoryCategoryItem obj)
        {
            try
            {
                _db.InventoryCategoryItems.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.InventoryCategoryItems.Max(x => x.InventoryCategoryItemId);
        }

        public bool Update(InventoryCategoryItem obj)
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

        public bool Delete(InventoryCategoryItem obj)
        {
            try
            {
                _db.InventoryCategoryItems.DeleteOnSubmit(obj);
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