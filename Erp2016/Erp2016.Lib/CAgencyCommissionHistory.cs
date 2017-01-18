using System;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CAgencyCommissionHistory
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CAgencyCommissionHistory()
        {
        }

        public AgencyCommissionHistory Get(int id)
        {
            return _db.AgencyCommissionHistories.FirstOrDefault(q => q.AgencyCommissionHistoryId == id);
        }
        
        public int Add(AgencyCommissionHistory obj)
        {
            try
            {
                _db.AgencyCommissionHistories.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.AgencyCommissionHistories.Max(x => x.AgencyCommissionHistoryId);
        }

        public bool Update(AgencyCommissionHistory obj)
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

        public bool Delete(AgencyCommissionHistory obj)
        {
            try
            {
                _db.AgencyCommissionHistories.DeleteOnSubmit(obj);
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