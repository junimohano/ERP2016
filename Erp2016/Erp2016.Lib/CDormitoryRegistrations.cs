using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Data.Linq;

namespace Erp2016.Lib
{
    public class CDormitoryRegistrations
    {

        private readonly linqDBDataContext _db = new linqDBDataContext();
        private readonly linqViewDataContext _dbView = new linqViewDataContext();

        public CDormitoryRegistrations()
        {

        }

        public DormitoryRegistration GetDormitoryStudentRequest(int DormitoryStudentId)
        {
            return _db.DormitoryRegistrations.FirstOrDefault(q => q.DormitoryRegistrationId == DormitoryStudentId);
        }

        public int GetInvoiceIdbyDormitoryStudentId(int DormitoryStudentId)
        {
            int InvoiceId = 0;
            var Invoice = _db.Invoices.Where(q => q.DormitoryRegistrationId == DormitoryStudentId).FirstOrDefault();
            if (Invoice != null)
            {
                InvoiceId = Invoice.InvoiceId;
            }
            return InvoiceId;
        }

        
        public int GetCountDormitoryStudentRequestId(int StudentID)
        {
            int result = 0;
            int count = 0;
            count = Convert.ToInt32(_db.DormitoryRegistrations.Where(q => q.StudentId == StudentID).Count().ToString());
            if (count > 0)
            {
                result = Convert.ToInt32(_db.DormitoryRegistrations.Where(q => q.StudentId == StudentID).Max(q => q.DormitoryRegistrationId).ToString());

            }
            return result;
        }
  
        public ISingleResult<spGetDormitoryStudentListResult> GetDormitoryStudentList(int SiteLocationId)
        {
            return _dbView.spGetDormitoryStudentList(SiteLocationId);
        }

        public ISingleResult<spGetDormitoryStudentHistoryListResult> GetDormitoryStudentHistoryList(int SiteLocationId, int StudentId, int DormitoryStudentId)
        {
            return _dbView.spGetDormitoryStudentHistoryList(SiteLocationId, StudentId, DormitoryStudentId);
        }
        public ISingleResult<spGetDormitoryInvoiceByRequestIdResult> GetDormitoryInvoiceByRequestId(int DormitoryStudentBasicId)
        {
            return _dbView.spGetDormitoryInvoiceByRequestId(DormitoryStudentBasicId);
        }

        public int SiteLocationbyStudentId(int StudentId)

        {
            int SiteLocationId = 0;
            var select = _db.Students.Where(q => q.StudentId == StudentId).Single();
            SiteLocationId = select.SiteLocationId;
            return SiteLocationId;

            //Join 3 tables: SiteLocation, Site, Student
            //var select = (
            //                        from S in db.Students
            //                        join L in db.SiteLocations on S.SiteLocationId equals L.SiteLocationId
            //                        join T in db.Sites on L.SiteId equals T.SiteId
            //                        where S.StudentId == StudentId
            //                        select new { LocationId = L.SiteLocationId, CampusName = T.Abbreviation, Location=T.City }
            //                        )
            //                        .Single() ;


            //var TempSelectList = db.Students.Where(q => q.StudentNo=="A").ToList();
            //var SelectList = db.Students.Where(q => q.SiteLocationId == 9).ToList();
            //foreach (var Select in SelectList)
            //{
            //    //Select.StudentNo DormitoryHostBed
            //    TempSelectList.Add(Select);
            //}




        }

        public ISingleResult<spGetSiteLocationIdByStudentIdResult> GetSiteLocationByStudentId(int StudentId)
        {
            return _dbView.spGetSiteLocationIdByStudentId(StudentId);
        }

        private List<DormitoryHostBed> HostRoomBedList(int HostId, int RoomId)
        {
            List<DormitoryHostBed> ResultList = new List<DormitoryHostBed>();

            if (HostId > 0)
            {
                if (RoomId == 0)
                {
                    //All BedList PER HOST
                    var HostBedList = _db.DormitoryHostBeds.Where(q => q.HostId == HostId).ToList();
                    ResultList = HostBedList;
                }
                else
                {
                    //All BedList PER ROOM
                    var RoomBedList = _db.DormitoryHostBeds.Where(q => q.HostId == HostId && q.HostRoomId == RoomId).ToList();
                    ResultList = RoomBedList;
                }

            }
            return ResultList;
        }
        //Avalible: Host List 
        public List<DormitoryHost> DormitoryHostListWithVacantBed(int SiteLocationId, DateTime StartDate, DateTime EndDate)
        {
            List<DormitoryHost> Host_VacancyBed = new List<DormitoryHost>();
            //var HostList = db.DormitoryHosts.Where(q=>q.HouseActiveDate<=StartDate && q.HouseActiveStutas==1).ToList();

            var HostList = (from H in _db.DormitoryHosts
                            join L in _db.DormitoryHostPrefferedSchools on H.DormitoryHostId equals L.HostId
                            where H.ActiveDate <= StartDate && H.HostStatus == 1
                                    && L.SiteLocationId == SiteLocationId
                            select H

                          ).ToList();

            foreach (var Host in HostList) //All Host
            {
                ////Empty BedList
                //var BedList = db.DormitoryHostBeds.Where(q => q.HostId == 0).ToList();

                int RoomId = 0;
                var AllBedList = HostRoomBedList(Host.DormitoryHostId, RoomId);

                foreach (var Bed in AllBedList)
                {
                    bool Vacancy = false;
                    Vacancy = DormitoryVacantBed(Bed.HostBedId, StartDate, EndDate);
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
        public List<DormitoryHostRoom> DormitoryRoomListWithVacantBed(int HostId, DateTime StartDate, DateTime EndDate)
        {
            List<DormitoryHostRoom> Room_VacancyBed = new List<DormitoryHostRoom>();
            var RoomList = _db.DormitoryHostRooms.Where(q => q.HostId == HostId).ToList();

            foreach (var Room in RoomList) //All Host Room
            {
                var AllBedList = HostRoomBedList(HostId, Room.HostRoomId);
                foreach (var Bed in AllBedList)
                {
                    bool Vacancy = false;
                    Vacancy = DormitoryVacantBed(Bed.HostBedId, StartDate, EndDate);
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
        public List<DormitoryHostBed> DormitoryBedListWithVacantBed(int HostId, int RoomId, DateTime StartDate, DateTime EndDate)
        {
            List<DormitoryHostBed> VacancyBed = new List<DormitoryHostBed>();

            var AllBedList = HostRoomBedList(HostId, RoomId);
            foreach (var Bed in AllBedList)
            {
                bool Vacancy = false;
                Vacancy = DormitoryVacantBed(Bed.HostBedId, StartDate, EndDate);
                if (Vacancy)
                {
                    VacancyBed.Add(Bed);
                }
            }


            return VacancyBed;

        }
        public bool DormitoryVacantBed(int BedId, DateTime StartDate, DateTime EndDate) // Avalible:Bed
        {
           
            bool Vacancy = false;


          
                var PlacementList = _db.DormitoryPlacements.Where(q => q.BedId == BedId).ToList();
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
        public int Add(DormitoryRegistration obj)
        {
            try
            {
                
                _db.DormitoryRegistrations.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }

            return _db.DormitoryRegistrations.Max(x => x.DormitoryRegistrationId);

        }

        public bool Update(DormitoryRegistration obj)
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

        public bool Delete(DormitoryRegistration obj)
        {
            try
            {
                _db.DormitoryRegistrations.DeleteOnSubmit(obj);
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
