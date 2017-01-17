using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CPackageProgram
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();
        private readonly linqViewDataContext _dbView = new linqViewDataContext();

        public PackageProgram GetPackageProgram(int packageProgramId)
        {
            return _db.PackagePrograms.FirstOrDefault(x => x.PackageProgramId == packageProgramId);
            //var query = from p100 in db.PackagePrograms
            //            where p100.PackageProgramId == packageProgramId
            //            select new
            //            {
            //                p100
            //            };
        }

        public List<CListModel> GetPackageProgramBySiteIdAndCountryId(int siteLocationId)
        {
            var result = new List<CListModel>();

            var tables = _db.PackagePrograms.Join(_db.PackageProgramSiteLocations, x => x.PackageProgramId, y => y.PackageProgramId, (a, b) => new { a, b }).Where(x => x.b.SiteLocationId == siteLocationId && x.a.IsActive == true && x.a.ApprovalStatus == 99 && x.a.EndDate >= DateTime.Today); 
            foreach (var t in tables)
            {
                result.Add(new CListModel { Name = t.a.PackageProgramName, Value = t.a.PackageProgramId + "," + t.a.ProgramId });
            }

            return result;
        }

        public decimal? GetStandardTuition(int packageProgramId)
        {
            var seraching = _db.PackageProgramDetails.FirstOrDefault(x => x.PackageProgramId == packageProgramId && x.InvoiceCoaItemId == 1);
            if (seraching != null)
                return seraching.AgencyPrice;
            return 0;
        }

        public IEnumerable<PackageProgramDetail> GetPackageProgramDetail(int packageProgramId)
        {
            return _db.PackageProgramDetails.Where(x => x.PackageProgramId == packageProgramId);
        }

        public vwPackageProgram GetViewPackageProgram(int packageProgramId)
        {
            return _dbView.vwPackagePrograms.FirstOrDefault(x => x.PackageProgramId == packageProgramId);
        }

        public int Add(PackageProgram obj)
        {
            try
            {
                _db.PackagePrograms.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.PackagePrograms.Max(x => x.PackageProgramId);
        }

        public bool Update(PackageProgram obj)
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

        public bool Delete(PackageProgram obj)
        {
            try
            {
                _db.PackagePrograms.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public void SetCancel(int PackageProgramId)
        {
            var query = _db.PackagePrograms.FirstOrDefault(x => x.PackageProgramId == PackageProgramId);
            if (query != null)
            {
                _db.PackagePrograms.DeleteOnSubmit(query);

                var query1 =
                    _db.PackageProgramDetails.FirstOrDefault(x => x.PackageProgramId == PackageProgramId);

                if (query1 != null)
                    _db.PackageProgramDetails.DeleteOnSubmit(query1);

                _db.SubmitChanges();
            }
        }

        public string GetTableNameForVwPackageProgram()
        {
            return CGlobal.GetTableName(_dbView.vwPackagePrograms.ToString());
        }

        public string GetTableNameForVwPackageProgramApprovalList()
        {
            return CGlobal.GetTableName(_dbView.vwPackageProgramApprovalLists.ToString());
        }
    }
}