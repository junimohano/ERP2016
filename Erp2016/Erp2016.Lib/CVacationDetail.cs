using System;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CVacationDetail
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CVacationDetail()
        {
        }

        public IQueryable<VacationDetail> Get(int vacationId)
        {
            return _db.VacationDetails.Where(x => x.VacationId == vacationId);
        }

        public int Add(VacationDetail obj)
        {
            try
            {
                _db.VacationDetails.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.VacationDetails.Max(x => x.VacationDetailId);
        }

        public bool Update(VacationDetail obj)
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

        public bool Delete(VacationDetail obj)
        {
            try
            {
                _db.VacationDetails.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }
        
        public void DeleteItemsByVacationId(int vacationId)
        {
            _db.VacationDetails.DeleteAllOnSubmit(Get(vacationId));
            _db.SubmitChanges();
        }
    }
}