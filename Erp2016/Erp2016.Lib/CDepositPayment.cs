using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CDepositPayment
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();
        private readonly linqViewDataContext _dbView = new linqViewDataContext();

        public CDepositPayment()
        {
        }

        public DepositPayment Get(int id)
        {
            return _db.DepositPayments.FirstOrDefault(q => q.DepositPaymentId == id);
        }

        public DepositPayment GetByPaymentId(int paymentId)
        {
            return _db.DepositPayments.FirstOrDefault(q => q.PaymentId == Convert.ToInt32(paymentId));
        }

        public int Add(DepositPayment obj)
        {
            try
            {
                obj.CreatedDate = DateTime.Now;

                _db.DepositPayments.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.DepositPayments.Max(x => x.DepositPaymentId);
        }

        public bool Update(DepositPayment obj)
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

        public bool Delete(DepositPayment obj)
        {
            try
            {
                _db.DepositPayments.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public List<vwDepositPayment> GetDepositListByStudentId(int studentId)
        {
            return _dbView.vwDepositPayments.Where(x => x.StudentId == studentId).ToList();
        }

    }
}