using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CProgramGroup
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CProgramGroup()
        {
        }

        public ProgramGroup Get(int id)
        {
            return _db.ProgramGroups.FirstOrDefault(q => q.ProgramGroupId == id);
        }

        public int Add(ProgramGroup obj)
        {
            try
            {
                _db.ProgramGroups.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.ProgramGroups.Max(x => x.ProgramGroupId);
        }

        public bool Update(ProgramGroup obj)
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

        public bool Delete(ProgramGroup obj)
        {
            try
            {
                _db.ProgramGroups.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }
        
        public List<CListModel> GetProgramGroupList()
        {
            var result = new List<CListModel>();
            var qry = _db.ProgramGroups.OrderBy(q => q.Name);

            foreach (var q in qry)
            {
                result.Add(new CListModel { Name = q.Name, Value = q.ProgramGroupId.ToString() });
            }

            return result;
        }

        public List<CListModel> GetProgramGroupList(int siteId)
        {
            var result = new List<CListModel>();
            var qry = _db.ProgramGroups.Where(q => q.SiteId == siteId).OrderBy(q => q.Name);

            foreach (var q in qry)
            {
                result.Add(new CListModel { Name = q.Name, Value = q.ProgramGroupId.ToString() });
            }

            return result;
        }

        public List<CListModel> GetProgramGroupList(int siteId, int facultyId)
        {
            var result = new List<CListModel>();
            var qry = _db.ProgramGroups.Where(q => q.SiteId == siteId && q.FacultyId == facultyId)
                .OrderBy(q => q.Name);
            if (facultyId == 0)
            {
                qry = _db.ProgramGroups.Where(q => q.SiteId == siteId).OrderBy(q => q.Name);
            }
            foreach (var q in qry)
            {
                result.Add(new CListModel { Name = q.Name, Value = q.ProgramGroupId.ToString() });
            }

            return result;
        }

        public List<CListModel> GetProgramGroupName(int id)
        {
            var result = new List<CListModel>();
            var qry = _db.ProgramGroups.Where(q => q.ProgramGroupId == id).OrderBy(q => q.Name);

            foreach (var q in qry)
            {
                result.Add(new CListModel { Name = q.Name, Value = q.ProgramGroupId.ToString() });
            }

            return result;
        }
        
        /* For Class Start */

        public List<CListModel> GetProgramNameList(int siteLocationId, int id)
        {
            var result = new List<CListModel>();
            var qry = _db.Programs.Join(_db.ProgramSiteLocations, x => x.ProgramId, y => y.ProgramId, (a, b) => new { a, b }).Where(q => q.b.SiteLocationId == siteLocationId && q.a.ProgramGroupId == id).OrderBy(q => q.a.ProgramFullName);

            foreach (var q in qry)
            {
                result.Add(new CListModel { Name = q.a.ProgramFullName, Value = q.a.ProgramId.ToString() });
            }

            return result;
        }

        public List<CListModel> GetProgramName(int id)
        {
            var result = new List<CListModel>();
            var qry = _db.Programs.Where(q => q.ProgramId == id).OrderBy(q => q.ProgramFullName);

            foreach (var q in qry)
            {
                result.Add(new CListModel { Name = q.ProgramFullName, Value = q.ProgramId.ToString() });
            }

            return result;
        }


        public List<CListModel> GetCourseNameList(int siteLocationId, int id)
        {
            //var result = new List<CListModel>();
            //var qry = db.ProgramCourses.Join(db.Programs, x => x.ProgramId, y => y.ProgramId, (a, b) => new { a, b }).Join(db.ProgramSiteLocations, x => x.a.ProgramId, y => y.ProgramId, (a, b) => new { a, b }).Where(q => q.SiteLocationId == siteLocationId && q.ProgramId == id).OrderBy(q => q.CourseName);

            //foreach (var q in qry)
            //{
            //    result.Add(new CListModel { Name = q.CourseName, Value = q.ProgramCourseId.ToString() });
            //}

            //return result;
            return null;
        }

        public List<CListModel> GetCourseName(int id)
        {
            var result = new List<CListModel>();
            var qry = _db.ProgramCourses.Where(q => q.ProgramCourseId == id).OrderBy(q => q.CourseName);

            foreach (var q in qry)
            {
                result.Add(new CListModel { Name = q.CourseName, Value = q.ProgramCourseId.ToString() });
            }

            return result;
        }
        /* For Class End */
        
        public List<CFilterListModel> GetProgramGroupNameList()
        {
            return _db.ProgramGroups.OrderBy(q => q.Name).Select(p => new CFilterListModel { ProgramGroupName = p.Name }).Distinct().ToList();
        }

    }
}