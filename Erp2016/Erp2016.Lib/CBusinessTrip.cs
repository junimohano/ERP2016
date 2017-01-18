using System;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CBusinessTrip
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CBusinessTrip()
        {
        }

        public CBusinessTrip(int docNo)
        {
            var query = from b100 in _db.BusinessTrips
                        join u100 in _db.Users on b100.CreatedId equals u100.UserId
                        join d100 in _db.SiteLocations on u100.SiteLocationId equals d100.SiteLocationId
                        join s100 in _db.Sites on d100.SiteId equals s100.SiteId
                        where b100.BusinessTripId == docNo
                        select new
                        {
                            b100,
                            u100,
                            s100
                        };

            foreach (var q in query)
            {
                DocNo = q.b100.BusinessTripId;
                TypeOfTrip = q.b100.Type;
                ShelfLife = 5;
                Site = q.s100.Name;
                Location = q.s100.City;
                Name1 = new CUser().GetUserName(q.u100);
                Name2 = q.b100.CreatedDate.ToString();
                Po1 = string.Empty;
                Po2 = string.Empty;
                break;
            }
        }

        public int DocNo { get; set; }
        public int ShelfLife { get; set; }
        public string Site { get; set; }
        public string Location { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Po1 { get; set; }
        public string Po2 { get; set; }

        public string TypeOfTrip { get; set; }
        public string UploadCopyOfPassport { get; set; }
        public decimal FlightTotal { get; set; }
        public decimal AccommodationTotal { get; set; }
        public decimal CashGroundTotal { get; set; }
        public decimal CashMealsTotal { get; set; }
        public decimal CashAdvanceTotal { get; set; }
        public decimal GrandTotal { get; set; }

        public BusinessTrip Get(int businessTripId)
        {
            return _db.BusinessTrips.FirstOrDefault(x => x.BusinessTripId == businessTripId);
        }

        public CBusinessTrip GetNewDocument(int currentUserId)
        {
            //int maxValue = db.BusinessTrips.Max(x => x.BusinessTripId);
            //DocNo = maxValue + 1;
            DocNo = 0;
            ShelfLife = 5;
            var result = _db.Users.Join(_db.SiteLocations, a => a.SiteLocationId, b => b.SiteLocationId, (a, b) => new { a, b }).Join(_db.Sites, a => a.b.SiteId, b => b.SiteId, (a, b) => new { a, b }).FirstOrDefault(x => x.a.a.UserId == currentUserId);
            Site = result.b.Name;
            Location = result.b.City;
            Name1 = new CUser().GetUserName(result.a.a);
            Name2 = DateTime.Now.ToString();
            return this;
        }

        //public void SetDelete(int businessTripId)
        //{
        //    var query = _db.BusinessTrips.FirstOrDefault(x => x.BusinessTripId == businessTripId);
        //    if (query != null)
        //    {
        //        _db.BusinessTrips.DeleteOnSubmit(query);

        //        var query1 = _db.BusinessTripFlights.FirstOrDefault(x => x.BusinessTripId == businessTripId);
        //        var query2 = _db.BusinessTripAccoms.FirstOrDefault(x => x.BusinessTripId == businessTripId);
        //        var query3 = _db.BusinessTripCashes.FirstOrDefault(x => x.BusinessTripId == businessTripId);

        //        if (query1 != null)
        //            _db.BusinessTripFlights.DeleteOnSubmit(query1);
        //        if (query2 != null)
        //            _db.BusinessTripAccoms.DeleteOnSubmit(query2);
        //        if (query3 != null)
        //            _db.BusinessTripCashes.DeleteOnSubmit(query3);

        //        _db.SubmitChanges();
        //    }
        //}

        public int Add(BusinessTrip obj)
        {
            try
            {
                _db.BusinessTrips.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.BusinessTrips.Max(x => x.BusinessTripId);
        }

        public bool Update(BusinessTrip obj)
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

        public bool Delete(BusinessTrip obj)
        {
            try
            {
                _db.BusinessTrips.DeleteOnSubmit(obj);
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