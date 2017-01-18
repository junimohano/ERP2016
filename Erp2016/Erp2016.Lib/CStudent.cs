using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CStudent
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();
        private readonly linqViewDataContext _dbView = new linqViewDataContext();

        public CStudent()
        {
        }

        public Student Get(int id)
        {
            return _db.Students.FirstOrDefault(q => q.StudentId == id);
        }

        public int Add(Student obj)
        {
            try
            {
                // Get Master Id
                var masterSearch = _db.Students.FirstOrDefault(x => x.FirstName == obj.FirstName && x.LastName1 == obj.LastName1 && x.CountryId == obj.CountryId && x.DOB == obj.DOB);
                if (masterSearch != null)
                {
                    obj.StudentMasterIndex = masterSearch.StudentMasterIndex;
                    obj.StudentMasterNo = masterSearch.StudentMasterNo;
                }
                else
                {
                    var last = (from q in _db.Students
                                where q.CreatedDate.Year == DateTime.Now.Year
                                orderby q.StudentId descending
                                select q).FirstOrDefault();

                    var nowYear = Convert.ToInt32(DateTime.Now.ToString("yy"));

                    obj.StudentMasterIndex = 1;

                    if (last != null)
                        obj.StudentMasterIndex = last.StudentMasterIndex + 1;

                    obj.StudentMasterNo = "MA" + nowYear + obj.StudentMasterIndex.ToString("D6");
                }

                var cSiteLocation = new CSiteLocation();
                var siteLocation = cSiteLocation.Get(obj.SiteLocationId);

                int lastStudentIndex = 0;
                var lastStudent = _db.Students.Join(_db.SiteLocations, a => a.SiteLocationId, b => b.SiteLocationId, (a, b) => new { a, b }).Where(x => x.b.SiteId == siteLocation.SiteId);
                if (lastStudent.Any())
                    lastStudentIndex = lastStudent.Max(x => x.a.StudentIndex);

                obj.StudentIndex = lastStudentIndex + 1;

                obj.StudentNo = obj.StudentIndex.ToString("D6");

                obj.IsActive = true;
                obj.CreatedDate = DateTime.Now;

                _db.Students.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.Students.Max(x => x.StudentId);
        }

        public bool Update(Student obj)
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

        public bool Delete(Student obj)
        {
            try
            {
                _db.Students.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public vwStudentContract GetVwStudentContract(int invoiceId)
        {
            return _dbView.vwStudentContracts.FirstOrDefault(x => x.InvoiceId == invoiceId);
        }

        public List<vwStudentCount> GetStudentCount()
        {
            return _dbView.vwStudentCounts.ToList();
        }

        public List<CFilterListModel> GetStudentNameList()
        {
            return _db.Students.OrderBy(q => q.FirstName).Select(p => new CFilterListModel { StudentName = new CStudent().GetStudentName(p) }).Distinct().ToList();
        }

        public List<Student> GetStudentList(int siteLocationId)
        {
            return _db.Students.Where(x => x.SiteLocationId == siteLocationId).OrderBy(q => q.FirstName).ToList();
        }

        public string GetStudentName(Student student)
        {
            if (student != null)
                return student.FirstName + (student.LastName1 == null ? string.Empty : " " + student.LastName1);

            return string.Empty;
        }

        public string GetStudentFullName(Student student)
        {
            if (student != null)
            {
                return student.FirstName +
                       (student.MiddleName1 == null ? string.Empty : " " + student.MiddleName1) +
                       (student.MiddleName2 == null ? string.Empty : " " + student.MiddleName2) +
                       (student.LastName1 == null ? string.Empty : " " + student.LastName1) +
                       (student.LastName2 == null ? string.Empty : " " + student.LastName2);
            }
            return string.Empty;
        }

    }
}