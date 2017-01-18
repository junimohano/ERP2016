using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Data.Linq;


namespace Erp2016.Lib
{
    public class CHomestayHostBasic
    {

        private readonly linqDBDataContext _db = new linqDBDataContext();
        private readonly linqViewDataContext _dbView = new linqViewDataContext();

        public CHomestayHostBasic()
        {

        }

        public HomestayHostBasic Get(int id)
        {
            return _db.HomestayHostBasics.FirstOrDefault(q => q.HostId == id);
        }

        public ISingleResult<spGetHomestayHostListResult> GetHomestayHostList(int SiteLocationId)
        {
            return _dbView.spGetHomestayHostList(SiteLocationId);
        }

        public ISingleResult<spGetHomestayHostActiveResult> GetHomestayHostActiveList(int SiteLocationId, DateTime StartDate, DateTime EndDate)
        {
            return _dbView.spGetHomestayHostActive(SiteLocationId, StartDate, EndDate);
        }

        public int MaxHostId()
        {
            int result = 0;

            result = Convert.ToInt32(_db.HomestayHostBasics.Max(q => q.HostId).ToString());
            return result;
        }
        
        private List<HomestayHostBed> HostRoomBedList(int HostId, int RoomId)
        {
            List<HomestayHostBed> ResultList = new List<HomestayHostBed>();

            if (HostId > 0)
            {
                if (RoomId == 0)
                {
                    //All BedList PER HOST
                    var HostBedList = _db.HomestayHostBeds.Where(q => q.HostId == HostId).ToList();
                    ResultList = HostBedList;
                }
                else
                {
                    //All BedList PER ROOM
                    var RoomBedList = _db.HomestayHostBeds.Where(q => q.HostId == HostId && q.HostRoomId == RoomId).ToList();
                    ResultList = RoomBedList;
                }

            }
            return ResultList;
        }
        //Avalible: Host List 
        public List<HomestayHostBasic> HomestayHostListWithVacantBed(int SiteLocationId, DateTime StartDate, DateTime EndDate)
        {
            List<HomestayHostBasic> Host_VacancyBed = new List<HomestayHostBasic>();
            //var HostList = db.HomestayHostBasics.Where(q=>q.HouseActiveDate<=StartDate && q.HouseActiveStutas==1).ToList();

            var HostList = (from H in _db.HomestayHostBasics
                            join L in _db.HomestayHostPrefferedSchools on H.HostId equals L.HostId
                            where H.HouseActiveDate <= StartDate && H.HouseActiveStutas == 1
                                    && L.SiteLocationId == SiteLocationId
                            select H

                          ).ToList();

            foreach (var Host in HostList) //All Host
            {
                ////Empty BedList
                //var BedList = db.HomestayHostBeds.Where(q => q.HostId == 0).ToList();

                int RoomId = 0;
                var AllBedList = HostRoomBedList(Host.HostId, RoomId);

                foreach (var Bed in AllBedList)
                {
                    bool Vacancy = false;
                    Vacancy = HomestayVacantBed(Bed.HostBedId, StartDate, EndDate);
                    if (Vacancy)
                    {
                        Host_VacancyBed.Add(Host);
                        break;
                    }
                }

            }

            return Host_VacancyBed;

        }
        //Avalible: Room List
        public List<HomestayHostRoom> HomestayRoomListWithVacantBed(int HostId, DateTime StartDate, DateTime EndDate)
        {
            List<HomestayHostRoom> Room_VacancyBed = new List<HomestayHostRoom>();
            var RoomList = _db.HomestayHostRooms.Where(q => q.HostId == HostId).ToList();

            foreach (var Room in RoomList) //All Host Room
            {
                var AllBedList = HostRoomBedList(HostId, Room.HostRoomId);
                foreach (var Bed in AllBedList)
                {
                    bool Vacancy = false;
                    Vacancy = HomestayVacantBed(Bed.HostBedId, StartDate, EndDate);
                    if (Vacancy)
                    {
                        Room_VacancyBed.Add(Room);
                        break;
                    }
                }

            }
            return Room_VacancyBed;

        }

        //Avalible: Bed List
        public List<HomestayHostBed> HomestayBedListWithVacantBed(int HostId, int RoomId, DateTime StartDate, DateTime EndDate)
        {
            List<HomestayHostBed> VacancyBed = new List<HomestayHostBed>();

            var AllBedList = HostRoomBedList(HostId, RoomId);
            foreach (var Bed in AllBedList)
            {
                bool Vacancy = false;
                Vacancy = HomestayVacantBed(Bed.HostBedId, StartDate, EndDate);
                if (Vacancy)
                {
                    VacancyBed.Add(Bed);
                }
            }


            return VacancyBed;

        }
        public bool HomestayVacantBed(int BedId, DateTime StartDate, DateTime EndDate) // Avalible:Bed
        {
            bool Vacancy = false;

            var PlacementList = _db.HomestayPlacements.Where(q => q.BedId == BedId).ToList();
            if (PlacementList.Count == 0)
            {
                Vacancy = true; //Not being used
            }
            else if (PlacementList.Count > 0)
            {
                foreach (var Placement in PlacementList)
                {
                    if (Placement.StartDate > EndDate)
                    {
                        Vacancy = true;
                        break;
                    }

                    if (Placement.EndDate < StartDate)
                    {
                        Vacancy = true;
                        break;
                    }

                }

            }

            return Vacancy;

        }
        public int Add(HomestayHostBasic obj)
        {
            try
            {

                _db.HomestayHostBasics.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }

            return _db.HomestayHostBasics.Max(x => x.HostId);

        }

        public bool Update(HomestayHostBasic obj)
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

        public bool Delete(HomestayHostBasic obj)
        {
            try
            {
                _db.HomestayHostBasics.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }


        public List<CFilterListModel> GetFatherNameList()
        {
            return _db.HomestayHostBasics.OrderBy(q => q.FatherFirstName).Select(p => new CFilterListModel { FatherName = p.FatherFirstName }).Distinct().ToList();
        }

        public List<CFilterListModel> GetHostRegistrationDateList()
        {
            return _db.HomestayHostBasics.OrderBy(q => q.CreatedDate).Select(p => new CFilterListModel { RegistrationDate = p.CreatedDate.ToString() }).Distinct().ToList();
        }



    }

}
