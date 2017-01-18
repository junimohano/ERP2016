using System;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CCountryMarket
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();
      
        public CCountryMarket()
        {
        }

        public CountryMarket Get(int id)
        {
            return _db.CountryMarkets.FirstOrDefault(q => q.CountryMarketId == id);
        }

        public int Add(CountryMarket obj)
        {
            try
            {
                _db.CountryMarkets.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.CountryMarkets.Max(x => x.CountryMarketId);
        }

        public bool Update(CountryMarket obj)
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

        public bool Delete(CountryMarket obj)
        {
            try
            {
                _db.CountryMarkets.DeleteOnSubmit(obj);
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