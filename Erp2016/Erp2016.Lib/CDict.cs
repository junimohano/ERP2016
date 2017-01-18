using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CDict
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CDict()
        {
        }

        public Dict Get(int id)
        {
            return _db.Dicts.FirstOrDefault(q => q.DictId == id);
        }

        public int Add(Dict obj)
        {
            try
            {
                _db.Dicts.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.Dicts.Max(x => x.DictId);
        }

        public bool Update(Dict obj)
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

        public bool Delete(Dict obj)
        {
            try
            {
                _db.Dicts.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public List<CListModel> GetDictType()
        {
            var qry = _db.Dicts.Where(q => q.DictType == 0);

            return qry.OrderBy(q => q.Name).Select(q => new CListModel { Name = q.Name, Value = q.DictId.ToString() }).ToList();
        }

        public List<CListModel> GetDictList(int typeid, bool sortbyname, bool showvalue)
        {
            var result = new List<CListModel>();

            var qry = _db.Dicts.Where(q => q.DictType == typeid);
            var qrys = (sortbyname) ? qry.OrderBy(q => q.Name) : qry.OrderBy(q => q.Value);

            foreach (var q in qrys)
            {
                var n = new CListModel();

                n.Name = (showvalue) ? (q.Value + " " + q.Name) : q.Name;
                n.Value = q.Value.ToString();
                n.Comment = q.DictId.ToString();

                result.Add(n);
            }

            return result;
        }

        public Dict GetDictByTypeAndValue(int typeid, int value)
        {
            return _db.Dicts.FirstOrDefault(q => q.DictType == typeid && q.Value == value);
        }

        public static string GetDictName(int typeid, int value)
        {
            var qry = new CDict();
            return qry.GetDictByTypeAndValue(typeid, value).Name;
        }
    }
}