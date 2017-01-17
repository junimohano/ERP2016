using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

namespace Erp2016.Lib
{
    public class CCreditMemo
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();
        private readonly linqViewDataContext _dbView = new linqViewDataContext();

        public CCreditMemo()
        {
        }

        public CreditMemo Get(int id)
        {
            return _db.CreditMemos.FirstOrDefault(q => q.CreditMemoId == id);
        }

        public int Add(CreditMemo obj)
        {
            try
            {
                var nowYear = DateTime.Now.ToString("yy");

                var last = (from q in _db.CreditMemos
                            where q.CreditMemoNumber.Substring(2, 2) == nowYear
                            orderby q.CreditMemoIndex descending
                            select q).FirstOrDefault();

                var creditType = _db.Dicts.FirstOrDefault(q => q.DictType == 69 && q.Value == obj.CreditMemoType);

                if (last == null)
                    obj.CreditMemoIndex = 1;
                else
                    obj.CreditMemoIndex = last.CreditMemoIndex + 1;

                //var lastPartial = (from q in db.CreditMemos
                //                   where q.CreditMemoNumber.Substring(4, 2) == nowYear && q.InvoiceId == obj.InvoiceId
                //                   orderby q.CreditMemoPartialIndex descending
                //                   select q).FirstOrDefault();

                //if (lastPartial == null)
                obj.CreditMemoPartialIndex = 1;
                //else
                //    obj.CreditMemoPartialIndex = lastPartial.CreditMemoIndex + 1;

                obj.CreditMemoNumber = "CM" + creditType.Abbreviation + nowYear + obj.CreditMemoIndex.ToString("D6") + "_" + obj.CreditMemoPartialIndex.ToString("D2");

                _db.CreditMemos.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.CreditMemos.Max(x => x.CreditMemoId);
        }

        public bool Update(CreditMemo obj)
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

        public bool Delete(CreditMemo obj)
        {
            try
            {
                _db.CreditMemos.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public decimal GetAvailableCreditAmount(int id)
        {
            decimal amt = 0;
            var qry = _dbView.vwCreditMemos.FirstOrDefault(q => q.CreditMemoId == id);
            if (qry != null)
            {
                amt = (decimal)qry.AvailableCreditAmount;
            }
            return amt;
        }

        public decimal GetOriginalCreditAmount(int id)
        {
            decimal amt = 0;
            var qry = _dbView.vwCreditMemos.FirstOrDefault(q => q.CreditMemoId == id);
            if (qry != null)
            {
                amt = qry.OriginalCreditMemoAmount;
            }
            return amt;
        }

        public decimal GetSumOfMdfAndCp(int refundInvoiceId)
        {
            var refundInvoice = _db.Invoices.FirstOrDefault(x => x.InvoiceId == refundInvoiceId);
            if (refundInvoice != null)
            {
                return _db.Invoices.Where(x => x.InvoiceNumber.Contains(refundInvoice.InvoiceNumber.Substring(2, 8)))
                                    .Join(_db.CreditMemos, x => x.InvoiceId, y => y.InvoiceId, (x, y) => new { x, y })
                                    .Where(x => x.y.CreditMemoType == (int)CConstValue.CreditMemoType.CP || x.y.CreditMemoType == (int)CConstValue.CreditMemoType.MDF)
                                    .Sum(x => x.y.OriginalCreditMemoAmount);

            }
            return 0;
        }

        public void ValidateOverPaid(int depositId, int userId)
        {
            // Get Payment list
            var resultPayment = _db.Payments.Join(_db.DepositPayments, x => x.PaymentId, y => y.PaymentId, (a, b) => new { a, b }).Where(x => x.b.DepositId == depositId).OrderByDescending(x => x.a.CreatedDate);
            foreach (var payment in resultPayment)
            {
                // Get invoice deposited pay list
                var resultPaymentDepositedAmount = _dbView.vwPaymentDepositedAmounts.FirstOrDefault(x => x.InvoiceId == payment.a.InvoiceId);
                if (resultPaymentDepositedAmount != null)
                {
                    decimal creditCp = 0;
                    decimal creditMdf = 0;
                    decimal creditOverPay = 0;

                    decimal needToCreditPrice = 0;

                    var creditResult = _db.CreditMemos.Where(x => x.InvoiceId == payment.a.InvoiceId);
                    if (creditResult.Any())
                    {
                        // Current CP
                        var cpResult = creditResult.Where(x => x.CreditMemoType == (int)CConstValue.CreditMemoType.CP);
                        if (cpResult.Any())
                            creditCp = cpResult.Sum(x => x.OriginalCreditMemoAmount);

                        // Current MDF
                        var mdfResult = creditResult.Where(x => x.CreditMemoType == (int)CConstValue.CreditMemoType.MDF);
                        if (mdfResult.Any())
                            creditMdf = mdfResult.Sum(x => x.OriginalCreditMemoAmount);

                        // Current OverPay
                        var overPayResult = creditResult.Where(x => x.CreditMemoType == (int)CConstValue.CreditMemoType.OverPaid);
                        if (overPayResult.Any())
                            creditOverPay = overPayResult.Sum(x => x.OriginalCreditMemoAmount);
                    }

                    // =======================
                    // cal Homestay CreditMemo
                    // =======================
                    // items about homestay will be created just once when fully paid.
                    if (resultPaymentDepositedAmount.Balance == 0)
                    {
                        // if creditType of homestay don't have, create them.
                        if (_db.CreditMemos.Any(x => x.InvoiceId == payment.a.InvoiceId &&
                                                 x.CreditMemoType == (int)CConstValue.CreditMemoType.HomestayBasic ||
                                                 x.CreditMemoType == (int)CConstValue.CreditMemoType.HomestayPickup ||
                                                 x.CreditMemoType == (int)CConstValue.CreditMemoType.HomestayDropOff ||
                                                 x.CreditMemoType == (int)CConstValue.CreditMemoType.HomestayPickupAndDropOff) == false)
                        {
                            var cInvoiceItems = new CInvoiceItem().GetInvoiceItems(payment.a.InvoiceId);
                            foreach (var invoiceItem in cInvoiceItems)
                            {
                                var creditMemoType = 0;
                                switch (invoiceItem.InvoiceCoaItemId)
                                {
                                    case (int)CConstValue.InvoiceCoaItemForHomestay.HomestayBasic:
                                        creditMemoType = (int)CConstValue.CreditMemoType.HomestayBasic;
                                        break;
                                    case (int)CConstValue.InvoiceCoaItem.AirportPickup:
                                        creditMemoType = (int)CConstValue.CreditMemoType.HomestayPickup;
                                        break;
                                    case (int)CConstValue.InvoiceCoaItem.AirportDropoff:
                                        creditMemoType = (int)CConstValue.CreditMemoType.HomestayDropOff;
                                        break;
                                    case (int)CConstValue.InvoiceCoaItem.AirportPickupAndDropoff:
                                        creditMemoType = (int)CConstValue.CreditMemoType.HomestayPickupAndDropOff;
                                        break;
                                    default:
                                        continue;
                                }

                                new CCreditMemo().Add(new CreditMemo
                                {
                                    CreditMemoType = creditMemoType,
                                    InvoiceId = payment.a.InvoiceId,
                                    CreditMemoStartDate = DateTime.Now,
                                    CreditMemoEndDate = DateTime.Now.AddYears(1),
                                    CreatedId = userId,
                                    CreatedDate = DateTime.Now,
                                    PaymentId = payment.a.PaymentId,
                                    OriginalCreditMemoAmount = resultPaymentDepositedAmount.AgencyId == null ? (decimal)invoiceItem.StudentPrice : (decimal)invoiceItem.AgencyPrice,
                                    IsActive = true
                                });
                            }
                        }
                    }

                    // ==============
                    // cal OverPaid
                    // ==============
                    needToCreditPrice = (decimal)resultPaymentDepositedAmount.OverPaid - creditOverPay;

                    if (needToCreditPrice > payment.a.Amount)
                        needToCreditPrice = payment.a.Amount;

                    if (needToCreditPrice > 0)
                    {
                        var cCreditMemoOther = new CCreditMemo();
                        cCreditMemoOther.Add(new CreditMemo
                        {
                            CreditMemoType = (int)CConstValue.CreditMemoType.OverPaid,
                            InvoiceId = payment.a.InvoiceId,
                            CreditMemoStartDate = DateTime.Now,
                            CreditMemoEndDate = DateTime.Now.AddYears(1),
                            CreatedId = userId,
                            CreatedDate = DateTime.Now,
                            PaymentId = payment.a.PaymentId,
                            OriginalCreditMemoAmount = needToCreditPrice,
                            IsActive = true 
                        });
                    }

                    // with Agency
                    if (resultPaymentDepositedAmount.AgencyId != null && resultPaymentDepositedAmount.IsGross)
                    {
                        decimal remainedPrice = (decimal)resultPaymentDepositedAmount.PayAmount - (decimal)resultPaymentDepositedAmount.AgencyPriceSum;

                        if (remainedPrice > (creditCp + creditMdf))
                        {

                            if (remainedPrice > payment.a.Amount)
                                remainedPrice = payment.a.Amount;

                            // ==============
                            // Cal CP
                            // ==============
                            if (resultPaymentDepositedAmount.CP > creditCp)
                            {
                                needToCreditPrice = (decimal)resultPaymentDepositedAmount.CP - creditCp;

                                if (needToCreditPrice < remainedPrice)
                                    remainedPrice = remainedPrice - needToCreditPrice;
                                else {
                                    needToCreditPrice = remainedPrice;
                                    remainedPrice = 0;
                                }

                                var cCreditMemoCp = new CCreditMemo();
                                cCreditMemoCp.Add(new CreditMemo
                                {
                                    CreditMemoType = (int)CConstValue.CreditMemoType.CP,
                                    InvoiceId = payment.a.InvoiceId,
                                    CreditMemoStartDate = DateTime.Now,
                                    CreditMemoEndDate = DateTime.Now.AddYears(1),
                                    CreatedId = userId,
                                    CreatedDate = DateTime.Now,
                                    PaymentId = payment.a.PaymentId,
                                    OriginalCreditMemoAmount = needToCreditPrice,
                                    IsActive = true
                                });
                            }

                            // ==============
                            // Cal MDF
                            // ==============
                            if (remainedPrice > 0)
                            {
                                if (resultPaymentDepositedAmount.MDF > creditMdf)
                                {
                                    needToCreditPrice = (decimal)resultPaymentDepositedAmount.MDF - creditMdf;

                                    if (needToCreditPrice < remainedPrice)
                                        remainedPrice = remainedPrice - needToCreditPrice;
                                    else {
                                        needToCreditPrice = remainedPrice;
                                        remainedPrice = 0;
                                    }

                                    var cCreditMemoMdf = new CCreditMemo();
                                    cCreditMemoMdf.Add(new CreditMemo
                                    {
                                        CreditMemoType = (int)CConstValue.CreditMemoType.MDF,
                                        InvoiceId = payment.a.InvoiceId,
                                        CreditMemoStartDate = DateTime.Now,
                                        CreditMemoEndDate = DateTime.Now.AddYears(1),
                                        CreatedId = userId,
                                        CreatedDate = DateTime.Now,
                                        PaymentId = payment.a.PaymentId,
                                        OriginalCreditMemoAmount = needToCreditPrice,
                                        IsActive = true
                                    });
                                }
                            }
                        }
                    }

                }
            }
        }

        public vwCreditMemo GetVwCreditMemo(int creditMemoId)
        {
            return _dbView.vwCreditMemos.FirstOrDefault(x => x.CreditMemoId == creditMemoId);
        }

        public List<CFilterListModel> GetCreditMemoTypeNameList()
        {
            return _db.Dicts.Where(x => x.DictType == 69).OrderBy(q => q.Value).Select(p => new CFilterListModel { CreditMemoType = p.Name }).Distinct().ToList();
        }

        public DataTable GetVwCreditMemoExcel(StringBuilder filterExpressionSb)
        {
            var tempDt = _dbView.ExecuteQuery<vwCreditMemoExcel>("SELECT * FROM " + nameof(vwCreditMemoExcel) + (filterExpressionSb.Length == 0 ? string.Empty : " WHERE " + filterExpressionSb));
            return CGlobal.ConvertToDataTable(tempDt);
        }
    }
}