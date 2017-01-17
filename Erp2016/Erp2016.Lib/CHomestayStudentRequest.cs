using System;
using System.Linq;
using System.Diagnostics;
using System.Data.Linq;

namespace Erp2016.Lib
{
    public class CHomestayStudentRequest
    {

        private readonly linqDBDataContext _db = new linqDBDataContext();
        private readonly linqViewDataContext _dbView = new linqViewDataContext();

        public CHomestayStudentRequest()
        {

        }

        public HomestayStudentBasic GetHomestayStudentRequest(int HomestayStudentId)
        {
            return _db.HomestayStudentBasics.FirstOrDefault(q => q.HomestayStudentId == HomestayStudentId);
        }

        public int GetInvoiceIdbyHomestayStudentId(int HomestayStudentId)
        {
            int InvoiceId = 0;
            var Invoice = _db.Invoices.Where(q => q.HomestayRegistrationId == HomestayStudentId).FirstOrDefault();
            if (Invoice != null)
            {
                InvoiceId = Invoice.InvoiceId;
            }
            return InvoiceId;
        }

        public int GetCountHomestayStudentRequestId(int StudentID)
        {
            int result = 0;
            int count = 0;
            count = Convert.ToInt32(_db.HomestayStudentBasics.Where(q => q.StudentId == StudentID).Count().ToString());
            if (count>0)
            {
                result = Convert.ToInt32(_db.HomestayStudentBasics.Where(q => q.StudentId == StudentID).Max(q => q.HomestayStudentId).ToString());

            }
            return result;
        }
    
        public ISingleResult<spGetHomestayStudentListByPermissionResult> GetHomestayStudentList(int UserId)
        {
            return _dbView.spGetHomestayStudentListByPermission(UserId); //spGetHomestayStudentList(SiteLocationId);
        }

        public ISingleResult<spGetHomestayStudentHistoryListResult> GetHomestayStudentHistoryList(int SiteLocationId, int StudentId, int HomestayStudentId)
        {
            return _dbView.spGetHomestayStudentHistoryList(SiteLocationId,StudentId, HomestayStudentId);
        }
        public ISingleResult<spGetHomestayInvoiceByRequestIdResult> GetHomestayInvoiceByRequestId(int HomestayStudentBasicId)
        {
            return _dbView.spGetHomestayInvoiceByRequestId(HomestayStudentBasicId);
        }
        public int Add(HomestayStudentBasic obj)
        {
            try
            {

                _db.HomestayStudentBasics.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }

            return _db.HomestayStudentBasics.Max(x => x.HomestayStudentId);

        }

        public bool Update(HomestayStudentBasic  obj)
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

        public bool Delete(HomestayStudentBasic obj)
        {
            try
            {
                _db.HomestayStudentBasics.DeleteOnSubmit(obj);
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
