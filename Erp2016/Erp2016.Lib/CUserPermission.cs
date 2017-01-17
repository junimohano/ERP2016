using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Erp2016.Lib
{
    public class CUserPermission
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();
        private readonly linqViewDataContext _dbView = new linqViewDataContext();

        public CUserPermission()
        {
        }

        public UserPermission Get(int id)
        {
            return _db.UserPermissions.FirstOrDefault(q => q.UserPermissionId == id);
        }

        public List<UserPermission> Get(int userId, int menuId)
        {
            return _db.UserPermissions.Where(q => q.UserId == userId && q.MenuId == menuId).ToList();
        }

        public UserPermission Get(int userId, int menuId, int permissionType)
        {
            return _db.UserPermissions.FirstOrDefault(q => q.UserId == userId && q.MenuId == menuId && q.PermissionType == permissionType);
        }

        public UserPermission Get(int userId, int menuId, int permissionType, int siteLocationId)
        {
            return _db.UserPermissions.FirstOrDefault(q => q.UserId == userId && q.MenuId == menuId && q.PermissionType == permissionType && q.SiteLocationId == siteLocationId);
        }

        public int Add(UserPermission obj)
        {
            try
            {
                _db.UserPermissions.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.UserPermissions.Max(x => x.UserPermissionId);
        }

        public bool Update(UserPermission obj)
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

        public bool Delete(UserPermission obj)
        {
            try
            {
                _db.UserPermissions.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public bool Delete(IEnumerable<UserPermission> objs)
        {
            try
            {
                _db.UserPermissions.DeleteAllOnSubmit(objs);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public List<CUserPermissionModel> GetUserPermissionModelList(int userId)
        {
            List<CUserPermissionModel> tempList = new List<CUserPermissionModel>();

            foreach (int index in Enum.GetValues(typeof(CConstValue.Menu)))
            {
                var userPermissionModel = new CUserPermissionModel()
                {
                    MenuId = index,
                    SearchSiteLocationList = new List<CUserPermissionSearchSiteLocationModel>(),
                    SearchWhereSiteLocationSb = new StringBuilder(),
                    SearchSiteList = new List<CUserPermissionSearchSiteModel>(),
                    SearchWhereSiteSb = new StringBuilder(),
                    // default
                    IsAccess = false,
                    IsModify = false
                };

                var result = _dbView.vwUserPermissions.Where(x => x.UserId == userId && x.MenuId == index);
                foreach (vwUserPermission vwUserPermission in result)
                {
                    switch (vwUserPermission.PermissionType)
                    {
                        // Access
                        case 0:
                            userPermissionModel.IsAccess = true;
                            break;

                        // Modify
                        case 1:
                            userPermissionModel.IsModify = true;
                            break;

                        // Search
                        case 2:
                            // siteLocation
                            userPermissionModel.SearchSiteLocationList.Add(new CUserPermissionSearchSiteLocationModel()
                            {
                                SiteLocationId = (int)vwUserPermission.SiteLocationId,
                                SiteLocationIdName = "SiteLocationId" + vwUserPermission.SiteLocationId
                            });
                            userPermissionModel.SearchWhereSiteLocationSb.Append("SiteLocationId == @SiteLocationId" + vwUserPermission.SiteLocationId + " || ");

                            // Site
                            if (userPermissionModel.SearchSiteList.Any(x => x.SiteId == vwUserPermission.SiteId) == false)
                            {
                                userPermissionModel.SearchSiteList.Add(new CUserPermissionSearchSiteModel()
                                {
                                    SiteId = (int)vwUserPermission.SiteId,
                                    SiteIdName = "SiteId" + vwUserPermission.SiteId
                                });
                                userPermissionModel.SearchWhereSiteSb.Append("SiteId == @SiteId" + vwUserPermission.SiteId + " || ");
                            }
                            break;
                    }
                }

                // SiteLocation
                // data exist
                if (userPermissionModel.SearchWhereSiteLocationSb.ToString().EndsWith("|| "))
                {
                    userPermissionModel.SearchWhereSiteLocationSb = userPermissionModel.SearchWhereSiteLocationSb.Remove(userPermissionModel.SearchWhereSiteLocationSb.Length - 3, 3);
                    userPermissionModel.SearchWhereSiteLocationSb = userPermissionModel.SearchWhereSiteLocationSb.Insert(0, "(");
                    userPermissionModel.SearchWhereSiteLocationSb = userPermissionModel.SearchWhereSiteLocationSb.Insert(userPermissionModel.SearchWhereSiteLocationSb.Length - 1, ")");
                }
                // no data
                else
                {
                    // default data
                    userPermissionModel.SearchSiteLocationList.Add(new CUserPermissionSearchSiteLocationModel()
                    {
                        SiteLocationId = 0,
                        SiteLocationIdName = "SiteLocationId0"
                    });
                    userPermissionModel.SearchWhereSiteLocationSb.Append("(SiteLocationId == @SiteLocationId0)");
                }

                // Site
                // data exist
                if (userPermissionModel.SearchWhereSiteSb.ToString().EndsWith("|| "))
                {
                    userPermissionModel.SearchWhereSiteSb = userPermissionModel.SearchWhereSiteSb.Remove(userPermissionModel.SearchWhereSiteSb.Length - 3, 3);
                    userPermissionModel.SearchWhereSiteSb = userPermissionModel.SearchWhereSiteSb.Insert(0, "(");
                    userPermissionModel.SearchWhereSiteSb = userPermissionModel.SearchWhereSiteSb.Insert(userPermissionModel.SearchWhereSiteSb.Length - 1, ")");
                }
                // no data
                else
                {
                    // default data
                    userPermissionModel.SearchSiteList.Add(new CUserPermissionSearchSiteModel()
                    {
                        SiteId = 0,
                        SiteIdName = "SiteId0"
                    });
                    userPermissionModel.SearchWhereSiteSb.Append("(SiteId == @SiteId0)");
                }

                tempList.Add(userPermissionModel);
            }
            return tempList;
        }

        public ISingleResult<GetUserPermissionSiteLocationListProcResult> GetUserPermissionSiteLocationListProc(int userId, int menuId)
        {
            return _dbView.GetUserPermissionSiteLocationListProc(userId, menuId);
        }

        private void AddPosition(CConstValue.Menu menu, CConstValue.PermissionType permissionType, List<int> siteLocationList, int userId, int currentUserId)
        {
            foreach (var siteLocation in siteLocationList)
            {
                var siteLocationTemp = permissionType == CConstValue.PermissionType.Search ? siteLocation : (int?)null;
                Add(new UserPermission()
                {
                    MenuId = (int)menu,
                    PermissionType = (int)permissionType,
                    UserId = userId,
                    SiteLocationId = siteLocationTemp,
                    CreatedId = currentUserId,
                    CreatedDate = DateTime.Now
                });
            }
        }

        public void SetBasicPermission(User user, int currentUserId)
        {
            var userPermissions = _db.UserPermissions.Where(x => x.UserId == user.UserId);
            if (userPermissions.Any())
                Delete(userPermissions);

            var userId = user.UserId;
            var userGroupId = new CUserPosition().Get(user.UserPositionId).UserGroupId;

            var siteLocationList = new List<int>();
            switch (userGroupId)
            {
                case (int)CConstValue.UserGroupForLoy.Accounting:
                case (int)CConstValue.UserGroupForLoy.GandA:
                case (int)CConstValue.UserGroupForLoy.GraphicDesign:
                case (int)CConstValue.UserGroupForLoy.HR:
                case (int)CConstValue.UserGroupForLoy.IT:
                case (int)CConstValue.UserGroupForLoy.Management:
                case (int)CConstValue.UserGroupForLoy.OverseasOffice:
                    siteLocationList.AddRange(new CSiteLocation().GetAll().Select(x => x.SiteLocationId));
                    break;

                case (int)CConstValue.UserGroupForBrandDirector.CAC:
                case (int)CConstValue.UserGroupForBrandDirector.KGIBC:
                case (int)CConstValue.UserGroupForBrandDirector.KGIC:
                case (int)CConstValue.UserGroupForBrandDirector.MTI:
                case (int)CConstValue.UserGroupForBrandDirector.PGIC:
                case (int)CConstValue.UserGroupForBrandDirector.SEC:
                case (int)CConstValue.UserGroupForBrandDirector.UCCBT:
                case (int)CConstValue.UserGroupForBrandDirector.UIS:
                    siteLocationList.AddRange(new CSiteLocation().GetAll(new CSiteLocation().Get(user.SiteLocationId).SiteId).Select(x => x.SiteLocationId));
                    break;

                default:
                    siteLocationList.Add(user.SiteLocationId);
                    break;
            }


            // permission
            switch (userGroupId)
            {
                case (int)CConstValue.UserGroupForInstructor.CAC:
                case (int)CConstValue.UserGroupForInstructor.KGIC:
                case (int)CConstValue.UserGroupForInstructor.PGIC:
                case (int)CConstValue.UserGroupForInstructor.SEC:
                case (int)CConstValue.UserGroupForInstructor.UCCBT:
                case (int)CConstValue.UserGroupForInstructor.UIS:
                case (int)CConstValue.UserGroupForInstructor.MTI:
                case (int)CConstValue.UserGroupForInstructor.KGIBC:
                case (int)CConstValue.UserGroupForInstructor.VIA:
                    AddPosition(CConstValue.Menu.BulletinBoard, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.User, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.User, CConstValue.PermissionType.Search, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.ProgramClass, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.ProgramClass, CConstValue.PermissionType.Modify, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.Vacation, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.Vacation, CConstValue.PermissionType.Modify, siteLocationList, userId, currentUserId);
                    break;

                default:
                    AddPosition(CConstValue.Menu.BulletinBoard, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.User, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.User, CConstValue.PermissionType.Search, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.Expense, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.Expense, CConstValue.PermissionType.Modify, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.CorporateCreditCard, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.CorporateCreditCard, CConstValue.PermissionType.Modify, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.BusinessTrip, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.BusinessTrip, CConstValue.PermissionType.Modify, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.PurchaseOrder, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.PurchaseOrder, CConstValue.PermissionType.Modify, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.Vacation, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.Vacation, CConstValue.PermissionType.Modify, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.Student, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.Student, CConstValue.PermissionType.Modify, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.Student, CConstValue.PermissionType.Search, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.Agency, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.Agency, CConstValue.PermissionType.Modify, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.Agency, CConstValue.PermissionType.Search, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.PackageProgram, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.PackageProgram, CConstValue.PermissionType.Modify, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.PackageProgram, CConstValue.PermissionType.Search, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.Promotion, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.Promotion, CConstValue.PermissionType.Modify, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.Promotion, CConstValue.PermissionType.Search, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.Scholarship, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.Scholarship, CConstValue.PermissionType.Modify, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.Scholarship, CConstValue.PermissionType.Search, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.Refund, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.Refund, CConstValue.PermissionType.Modify, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.Refund, CConstValue.PermissionType.Search, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.Cancel, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.Cancel, CConstValue.PermissionType.Search, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.Break, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.Break, CConstValue.PermissionType.Search, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.ScheduleChange, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.ScheduleChange, CConstValue.PermissionType.Search, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.Invoice, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.Invoice, CConstValue.PermissionType.Modify, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.Invoice, CConstValue.PermissionType.Search, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.Payment, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.Payment, CConstValue.PermissionType.Modify, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.Payment, CConstValue.PermissionType.Search, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.Deposit, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.Deposit, CConstValue.PermissionType.Modify, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.Deposit, CConstValue.PermissionType.Search, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.CreditMemo, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.CreditMemo, CConstValue.PermissionType.Modify, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.CreditMemo, CConstValue.PermissionType.Search, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.Faculty, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.Faculty, CConstValue.PermissionType.Modify, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.Faculty, CConstValue.PermissionType.Search, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.ProgramGroup, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.ProgramGroup, CConstValue.PermissionType.Modify, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.ProgramGroup, CConstValue.PermissionType.Search, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.Program, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.Program, CConstValue.PermissionType.Modify, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.Program, CConstValue.PermissionType.Search, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.ProgramCourse, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.ProgramCourse, CConstValue.PermissionType.Modify, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.ProgramCourse, CConstValue.PermissionType.Search, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.ProgramCourseLevel, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.ProgramCourseLevel, CConstValue.PermissionType.Modify, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.ProgramCourseLevel, CConstValue.PermissionType.Search, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.ProgramClassRoom, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.ProgramClassRoom, CConstValue.PermissionType.Modify, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.ProgramClassRoom, CConstValue.PermissionType.Search, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.ProgramClass, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.ProgramClass, CConstValue.PermissionType.Modify, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.ProgramClass, CConstValue.PermissionType.Search, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.HomestayHostRegistration, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.HomestayHostRegistration, CConstValue.PermissionType.Modify, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.HomestayHostRegistration, CConstValue.PermissionType.Search, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.HomestayPlacementRequest, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.HomestayPlacementRequest, CConstValue.PermissionType.Modify, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.HomestayPlacementRequest, CConstValue.PermissionType.Search, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.DormitoryHostRegistration, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.DormitoryHostRegistration, CConstValue.PermissionType.Modify, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.DormitoryHostRegistration, CConstValue.PermissionType.Search, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.DormitoryRequestRegistration, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.DormitoryRequestRegistration, CConstValue.PermissionType.Modify, siteLocationList, userId, currentUserId);
                    AddPosition(CConstValue.Menu.DormitoryRequestRegistration, CConstValue.PermissionType.Search, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.GradeSchema, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.ApprovalChart, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);

                    AddPosition(CConstValue.Menu.Holiday, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);

                    if (userGroupId == (int)CConstValue.UserGroupForLoy.Accounting ||
                        userGroupId == (int)CConstValue.UserGroupForLoy.GandA ||
                        userGroupId == (int)CConstValue.UserGroupForLoy.IT)
                    {
                        AddPosition(CConstValue.Menu.ExpenseForHq, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);

                        AddPosition(CConstValue.Menu.CorporateCreditCardForHq, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);

                        AddPosition(CConstValue.Menu.BusinessTripForHq, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);

                        AddPosition(CConstValue.Menu.PurchaseOrderForHq, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    }

                    if (userGroupId == (int)CConstValue.UserGroupForLoy.HR ||
                        userGroupId == (int)CConstValue.UserGroupForLoy.IT)
                    {
                        AddPosition(CConstValue.Menu.HireForHq, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);

                        AddPosition(CConstValue.Menu.VacationForHq, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);

                        AddPosition(CConstValue.Menu.ApprovalChart, CConstValue.PermissionType.Modify, siteLocationList, userId, currentUserId);

                        AddPosition(CConstValue.Menu.Holiday, CConstValue.PermissionType.Modify, siteLocationList, userId, currentUserId);

                        AddPosition(CConstValue.Menu.VacationForHq, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                        AddPosition(CConstValue.Menu.VacationForHq, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                        AddPosition(CConstValue.Menu.VacationForHq, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                    }

                    if (userGroupId == (int)CConstValue.UserGroupForLoy.IT)
                    {
                        AddPosition(CConstValue.Menu.Dictionary, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                        AddPosition(CConstValue.Menu.Inventory, CConstValue.PermissionType.Access, siteLocationList, userId, currentUserId);
                        AddPosition(CConstValue.Menu.Inventory, CConstValue.PermissionType.Modify, siteLocationList, userId, currentUserId);
                    }

                    break;
            }

        }
    }
}