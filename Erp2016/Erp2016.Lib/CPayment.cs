using System;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Erp2016.Lib
{
    public class CPayment
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();
        private readonly linqViewDataContext _dbView = new linqViewDataContext();

        public CPayment()
        {
        }

        public Payment Get(int id)
        {
            return _db.Payments.FirstOrDefault(q => q.PaymentId == id);
        }

        public Payment Get(string receipt)
        {
            return _db.Payments.FirstOrDefault(q => q.PaymentNumber == receipt);
        }

        public int Add(Payment obj)
        {
            try
            {
                var nowYear = DateTime.Now.ToString("yy");

                var invoicePartial = _db.Payments.OrderByDescending(x => x.PaymentIndex).FirstOrDefault(x => x.InvoiceId == obj.InvoiceId);
                if (invoicePartial == null)
                {
                    var last = (from q in _db.Payments
                                where q.PaymentNumber.Substring(2, 2) == nowYear
                                orderby q.PaymentIndex descending
                                select q).FirstOrDefault();

                    if (last == null)
                        obj.PaymentIndex = 1;
                    else
                        obj.PaymentIndex = last.PaymentIndex + 1;

                    obj.PaymentPartialIndex = 1;
                }
                else
                {
                    nowYear = invoicePartial.PaymentNumber.Substring(2, 2);
                    obj.PaymentIndex = invoicePartial.PaymentIndex;
                    obj.PaymentPartialIndex = invoicePartial.PaymentPartialIndex + 1;
                }

                obj.PaymentNumber = "PR" + nowYear + obj.PaymentIndex.ToString("D6") + "_" + obj.PaymentPartialIndex.ToString("D2");
                obj.CreatedDate = DateTime.Now;

                _db.Payments.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.Payments.Max(x => x.PaymentId);
        }

        public bool Update(Payment obj)
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

        public bool Delete(Payment obj)
        {
            try
            {
                _db.Payments.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public int InvoiceCheck(int invoiceId)
        {
            var invoiceCnt = 0;

            var qry = _db.Payments.Where(q => q.InvoiceId == invoiceId);
            if (qry != null)
            {
                invoiceCnt = qry.Count();
            }
            return invoiceCnt;
        }

        public vwPaymentHistory GetvwPaymentHistory(int paymentId)
        {
            return _dbView.vwPaymentHistories.FirstOrDefault(x => x.PaymentId == paymentId);
        }

        public Payment GetReversePayment(int originalPaymentId)
        {
            return _db.Payments.FirstOrDefault(x => x.OriginalPaymentId == originalPaymentId);
        }

        public vwPayment GetvwPayment(int invoiceId)
        {
            return _dbView.vwPayments.FirstOrDefault(x => x.InvoiceId == invoiceId);
        }

        public DataTable GetVwPaymentExcel(StringBuilder filterExpressionSb)
        {
            var tempDt = _dbView.ExecuteQuery<vwPaymentExcel>("SELECT * FROM " + nameof(vwPaymentExcel) + (filterExpressionSb.Length == 0 ? string.Empty : " WHERE " + filterExpressionSb));
            return CGlobal.ConvertToDataTable(tempDt);
        }
    }
}