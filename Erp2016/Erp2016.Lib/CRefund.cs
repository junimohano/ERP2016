using System;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CRefund
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();
        private readonly linqViewDataContext _dbView = new linqViewDataContext();

        public CRefund()
        {
        }

        public Refund Get(int id)
        {
            return _db.Refunds.FirstOrDefault(q => q.RefundId == id);
        }
        
        public int Add(Refund obj)
        {
            try
            {
                _db.Refunds.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.Refunds.Max(x => x.RefundId);
        }

        public bool Update(Refund obj)
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

        public bool Delete(Refund obj)
        {
            try
            {
                _db.Refunds.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        //public bool OverPaidCheck(int invoiceId)
        //{
        //    var viewDb = new linqViewDataContext();

        //    var qry = viewDb.vwPayments.Where(q => q.InvoiceId == invoiceId).FirstOrDefault();

        //    if (qry.PayAmount > qry.AgencyPriceSum && qry.PayAmount == qry.Gross)
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        public double[] RefundPolicy(DateTime startDate, DateTime endDate, DateTime refundDate, int siteLocationId)
        {
            double rate = 0;
            var totalDays = endDate - startDate;
            var studyDays = refundDate - startDate;

            var rates = new double[2];

            var studyRate = Math.Round((studyDays.TotalDays / totalDays.TotalDays), 2) * 100;

            switch (siteLocationId)
            {
                case 15: //MTCU-KGIBC        :   15(KGICBC-TOR);Toronto
                case 27: //MTCU-UCCBT        :   27(Toronto)
                    //// A program shorter than 12 months                                >   Retain the lesser of 20% of total fee or $500
                    //// A program 12 months or longer                                   >   Retain the lesser of 20% of total fee or $500, Less fees earned for delivered portion in current 12-month, Less fees paid for any subsequent period
                    //// A program progressed more than 50% or 6 months (annual basis)   >   No Refund
                    rate = 20;
                    break;
                case 1: //PCTIA-MTI         :   1(Vancouver)
                case 2: //PCTIA-MTI         :   2(Burnaby)
                case 3: //PCTIA-MTI         :   3(Surrey)
                case 4: //PCTIA-MTI         :   4(North Road)
                case 5: //PCTIA-MTI         :   5(Chilliwack)
                case 6: //PCTIA-MTI         :   6(Abbotsford)
                case 14: //PCTIA-KGIBC       :   14(KGICBC-CTC);Vancouver 
                case 16: //PCTIA-KGIBC       :   16(KGICBC-VIC);Victoria
                case 22: //PCTIA-VIA         :   22(PGIC-VIA)
                case 28: //PCTIA-UCCBT       :   28(Vancouver)
                    //// Dismissed before 10% of the period                              >   Retain 30% of the tuition
                    //// Dismissed after 10% and before 30% of the period                >   Retain 50% of the tuition
                    //// Dismissed after 30% of the period                               >   No refund
                    if (studyRate < 10)
                    {
                        rate = 70;
                    }
                    else if (studyRate < 30)
                    {
                        rate = 50;
                    }
                    else
                    {
                        rate = 0;
                    }
                    break;
                case 18: //PGIC  :   18(Toronto)
                case 19: //PGIC  :   19(Vancouver)
                case 20: //PGIC  :   20(PGCC)
                case 21: //PGIC  :   21(Victoria)
                    //// 0 - 10% of the program completed                                >   50% tuition refund less reg. fee
                    //// 11 - 29% of the program completed                               >   30% tuition refund less reg. fee
                    //// 30 - 100% of the program completed                              >   NO REFUND
                    if (studyRate < 10)
                    {
                        rate = 50;
                    }
                    else if (studyRate < 30)
                    {
                        rate = 30;
                    }
                    else
                    {
                        rate = 0;
                    }
                    break;
                case 7: //CAC   :   7(Toronto)
                case 8: //CAC   :   8(Vancouver)
                    //// 0 - 10% of the program completed                                >   50% tuition refund less reg. fee
                    //// 11 - 25% of the program completed                               >   30% tuition refund less reg. fee
                    //// 25 - 100% of the program completed                              >   NO REFUND
                    if (studyRate < 10)
                    {
                        rate = 50;
                    }
                    else if (studyRate < 25)
                    {
                        rate = 30;
                    }
                    else
                    {
                        rate = 0;
                    }
                    break;
                case 9: //KGIC  :   9(Toronto)
                case 10: //KGIC  :   10(Vancouver)
                case 11: //KGIC  :   11(Victoria)
                case 12: //KGIC  :   12(Surrey)
                case 13: //KGIC  :   13(Halifax)
                    //// 0 - 10% of program's duration                                   >   70% tuition refund less reg. fee
                    //// 11 - 30% of program's duration                                  >   50% tuition refund less reg. fee
                    //// 30 - 100% of program's duration                                 >   NO REFUND
                    if (studyRate < 10)
                    {
                        rate = 70;
                    }
                    else if (studyRate < 30)
                    {
                        rate = 50;
                    }
                    else
                    {
                        rate = 0;
                    }
                    break;
                case 25: //SEC   :   25(Toronto)
                case 26: //SEC   :   26(Vancouver)
                    //// Within 10 calendar days OR school days of the start date         >   50% tuition refund less reg. fee
                    //// More than 10 days of the courses completed                      >   NO REFUND
                    if (GetBusinessDays(startDate, studyDays) < 10)
                    {
                        rate = 50;
                    }
                    else
                    {
                        rate = 0;
                    }
                    break;
                case 29: //UIS   :   29(Toronto)
                    //// After class begins                                              >   NO Refund
                    rate = 0;
                    break;
                default:
                    rate = 0;
                    break;
            }

            rates[0] = rate;
            rates[1] = studyRate;
            return rates;
        }

        public int GetBusinessDays(DateTime startDate, TimeSpan studyDay)
        {
            var businessDays = 0;
            var study = Convert.ToInt32(studyDay);
            for (var i = 0; i < study; i++)
            {
                if (startDate.AddDays(i).DayOfWeek != DayOfWeek.Saturday ||
                    startDate.AddDays(i).DayOfWeek != DayOfWeek.Sunday)
                {
                    businessDays += 1;
                }
            }
            return businessDays;
        }

        public DateTime GetBusinessDate(DateTime startDate)
        {
            var BusinessDate = startDate;
            var businessDays = 0;
            var i = 0;
            while (businessDays < 5)
            {
                if (startDate.AddDays(i).DayOfWeek != DayOfWeek.Saturday ||
                    startDate.AddDays(i).DayOfWeek != DayOfWeek.Sunday)
                {
                    businessDays += 1;
                }
                i++;
            }

            return BusinessDate.AddDays(i);
        }

        public string GetTableNameForVwRefund()
        {
            return CGlobal.GetTableName(_dbView.vwRefunds.ToString());
        }

        public string GetTableNameForVwRefundApprovalList()
        {
            return CGlobal.GetTableName(_dbView.vwRefundApprovalLists.ToString());
        }

        public vwRefund GetVwRefund(int refundId)
        {
            return _dbView.vwRefunds.FirstOrDefault(x => x.RefundId == refundId);
        }
    }
}