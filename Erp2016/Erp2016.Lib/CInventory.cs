using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CInventory
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();
        private readonly linqViewDataContext _dbView = new linqViewDataContext();

        public CInventory()
        {
        }

        public Inventory Get(int id)
        {
            return _db.Inventories.FirstOrDefault(q => q.InventoryId == id);
        }

        public int Add(Inventory obj)
        {
            try
            {
                var nowYear = DateTime.Now.ToString("yyyy");

                var last = (from q in _db.Inventories
                            where q.InventoryCode.Substring(0, 4) == nowYear
                            orderby q.InventoryIndex descending
                            select q).FirstOrDefault();

                if (last == null)
                    obj.InventoryIndex = 1;
                else
                    obj.InventoryIndex = last.InventoryIndex + 1;

                var siteLocation = new CSiteLocation().Get(obj.SiteLocationId);
                var site = new CSite().Get(siteLocation.SiteId);
                string siteName;
                if (site?.SiteId == 1)
                    siteName = site.Abbreviation;
                else
                    siteName = site?.Abbreviation?.Substring(0, 2) + siteLocation?.Name?.Substring(0, 1);

                var inventoryCategoryItem = new CInventoryCategoryItem().Get(obj.InventoryCategoryItemId);
                var inventoryCategory = new CInventoryCategory().Get(inventoryCategoryItem.InventoryCategoryId);

                obj.InventoryCode = nowYear + siteName + inventoryCategory?.Abbreviation + obj.InventoryIndex.ToString("D6");

                _db.Inventories.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.Inventories.Max(x => x.InventoryId);
        }

        public bool Update(Inventory obj)
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

        public bool Delete(Inventory obj)
        {
            try
            {
                _db.Inventories.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }


        public List<CListModel> GetInventoryCategoryList()
        {
            var result = new List<CListModel>();
            foreach (var d in _db.InventoryCategories.OrderBy(q => q.Name))
                result.Add(new CListModel { Name = d.Name, Value = d.InventoryCategoryId.ToString() });

            return result;
        }

        public List<CListModel> GetInventoryCategoryItemList(int inventoryCategoryId)
        {
            var result = new List<CListModel>();
            foreach (var d in _db.InventoryCategoryItems.Where(x=>x.InventoryCategoryId == inventoryCategoryId).OrderBy(q => q.Name))
                result.Add(new CListModel { Name = d.Name, Value = d.InventoryCategoryItemId.ToString() });

            return result;
        }
        
        public List<CFilterListModel> GetInventoryCategoryNameList()
        {
            return _db.InventoryCategories.OrderBy(q => q.Name).Select(p => new CFilterListModel { InventoryCategoryName = p.Name }).Distinct().ToList();
        }


        public List<CFilterListModel> GetInventoryCategoryItemNameList()
        {
            return _db.InventoryCategoryItems.OrderBy(q => q.Name).Select(p => new CFilterListModel { InventoryCategoryItemName = p.Name }).Distinct().ToList();
        }


        public List<CFilterListModel> GetConditionNameList()
        {
            return _db.Dicts.Where(x => x.DictType == 1622).OrderBy(q => q.Value).Select(p => new CFilterListModel { ConditionName = p.Name }).Distinct().ToList();
        }


        public List<CFilterListModel> GetInUseNameList()
        {
            return _db.Dicts.Where(x => x.DictType == 1624).OrderBy(q => q.Value).Select(p => new CFilterListModel { InUseName = p.Name }).Distinct().ToList();
        }
    }
}