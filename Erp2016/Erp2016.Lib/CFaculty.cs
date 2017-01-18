using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CFaculty
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CFaculty()
        {
        }

        public Faculty Get(int id)
        {
            return _db.Faculties.FirstOrDefault(q => q.FacultyId == id);
        }

        public int Add(Faculty obj)
        {
            try
            {
                _db.Faculties.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.Faculties.Max(x => x.FacultyId);
        }

        public bool Update(Faculty obj)
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

        public bool Delete(Faculty obj)
        {
            try
            {
                _db.Faculties.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }
        
        public List<CListModel> GetFacultyList(int siteId)
        {
            var result = new List<CListModel>();
            var qry = _db.Faculties.Where(q => q.SiteId == siteId).OrderBy(q => q.Name);

            foreach (var q in qry)
            {
                result.Add(new CListModel { Name = q.Name, Value = q.FacultyId.ToString() });
            }

            return result;
        }
        
        public List<CListModel> GetFacultyName(int facultyId)
        {
            var result = new List<CListModel>();
            var qry = _db.Faculties.Where(q => q.FacultyId == facultyId).OrderBy(q => q.Name);

            foreach (var q in qry)
            {
                result.Add(new CListModel { Name = q.Name, Value = q.FacultyId.ToString() });
            }

            return result;
        }

        public List<CFilterListModel> GetFacultyNameList()
        {
            return _db.Faculties.OrderBy(q => q.Name).Select(p => new CFilterListModel { FacultyName = p.Name }).Distinct().ToList();
        }

    }
}