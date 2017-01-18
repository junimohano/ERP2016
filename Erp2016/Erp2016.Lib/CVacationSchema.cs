using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CVacationSchema
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public CVacationSchema()
        {
        }

        public VacationSchema Get(int id)
        {
            return _db.VacationSchemas.FirstOrDefault(q => q.UserId == id);
        }

        public DataTable Get(CVacationTypeInfoModel vacationTypeInfoModel)
        {
            var type = "Type";
            var paidVacation = "Used Paid Vacation Days";
            var sick = "Used Sick days";
            var entitlement = "Used Entitlement Days";

            var dt = new DataTable();
            dt.Columns.Add(type);
            dt.Columns.Add(paidVacation);
            dt.Columns.Add(sick);
            if (vacationTypeInfoModel.ThisYear.EntitlementTotalDays > 0)
                dt.Columns.Add(entitlement);

            var newThisYearDr = dt.NewRow();
            newThisYearDr[type] = vacationTypeInfoModel.ThisYear.PaidDate.Year;
            newThisYearDr[paidVacation] = vacationTypeInfoModel.ThisYear.PaidUsedDays + " / " + vacationTypeInfoModel.ThisYear.PaidTotalDays;
            newThisYearDr[sick] = vacationTypeInfoModel.ThisYear.SickUsedDays + " / " + vacationTypeInfoModel.ThisYear.SickTotalDays;
            if (vacationTypeInfoModel.ThisYear.EntitlementTotalDays > 0)
                newThisYearDr[entitlement] = vacationTypeInfoModel.ThisYear.EntitlementUsedDays + " / " + vacationTypeInfoModel.ThisYear.EntitlementTotalDays;
            dt.Rows.Add(newThisYearDr);

            var newNextYearDr = dt.NewRow();
            newNextYearDr[type] = vacationTypeInfoModel.NextYear.PaidDate.Year;
            newNextYearDr[paidVacation] = vacationTypeInfoModel.NextYear.PaidUsedDays + " / " + vacationTypeInfoModel.NextYear.PaidTotalDays;
            newNextYearDr[sick] = vacationTypeInfoModel.NextYear.SickUsedDays + " / " + vacationTypeInfoModel.NextYear.SickTotalDays;
            if (vacationTypeInfoModel.ThisYear.EntitlementTotalDays > 0)
                newNextYearDr[entitlement] = vacationTypeInfoModel.NextYear.EntitlementUsedDays + " / " + vacationTypeInfoModel.NextYear.EntitlementTotalDays;
            dt.Rows.Add(newNextYearDr);

            return dt;
        }

        public CVacationTypeInfoModel GetVacationTypeInfoModel(int userId)
        {
            CVacationTypeInfoModel vacationTypeInfoModel = new CVacationTypeInfoModel()
            {
                NextYear = new CVacationTypeModel(),
                ThisYear = new CVacationTypeModel()
            };

            SetVacationTypeInfoModel(userId, (int)CConstValue.VacationType.PaidVacationDay, vacationTypeInfoModel);
            SetVacationTypeInfoModel(userId, (int)CConstValue.VacationType.SickDay, vacationTypeInfoModel);
            SetVacationTypeInfoModel(userId, (int)CConstValue.VacationType.EntitlementDay, vacationTypeInfoModel);

            return vacationTypeInfoModel;
        }

        public void SetVacationTypeInfoModel(int userId, int vacationType, CVacationTypeInfoModel vacationTypeInfoModel)
        {
            List<VacationSchema> vacationSchemaList = _db.VacationSchemas.OrderByDescending(x => x.Date).Where(x => x.UserId == userId && x.VacationType == vacationType).ToList();
            if (vacationSchemaList.Any())
            {
                VacationSchema nextYearSchema = vacationSchemaList[0];
                VacationSchema thisYearSchema = vacationSchemaList[1];
                VacationSchema lastYearSchema = vacationSchemaList.Count() > 2 ? vacationSchemaList[2]
                    : new VacationSchema()
                    {
                        // last year Dec 31
                        Date = new DateTime(thisYearSchema.Date.Year - 1, 12, 31),
                        VacationType = vacationType,
                        TotalDays = 0,
                        UserId = userId
                    };

                double nextYearUsedDays = 0;
                double thisYearUsedDays = 0;

                // used days in nextYear
                // without rejected, canceled
                var nextYearList = _db.Vacations.Where(x => x.CreatedId == userId && x.VacationType == vacationType).Join(_db.VacationDetails, x => x.VacationId, y => y.VacationId,
                    (a, b) => new
                    {
                        a,
                        b
                    }).Where(x => x.b.Date > thisYearSchema.Date && x.b.Date <= nextYearSchema.Date && x.a.ApprovalStatus != (int)CConstValue.ApprovalStatus.Rejected && x.a.ApprovalStatus != (int)CConstValue.ApprovalStatus.Canceled);

                foreach (var v in nextYearList)
                {
                    if (v.b.IsFullDay)
                        // + full day
                        nextYearUsedDays++;
                    else
                        // + half day
                        nextYearUsedDays += 0.5;
                }

                // used days in thisYear
                // without rejected, canceled
                var thisYearList = _db.Vacations.Where(x => x.CreatedId == userId && x.VacationType == vacationType).Join(_db.VacationDetails, x => x.VacationId, y => y.VacationId,
                    (a, b) => new
                    {
                        a,
                        b
                    }).Where(x => x.b.Date > lastYearSchema.Date && x.b.Date <= thisYearSchema.Date && x.a.ApprovalStatus != (int)CConstValue.ApprovalStatus.Rejected && x.a.ApprovalStatus != (int)CConstValue.ApprovalStatus.Canceled);

                foreach (var v in thisYearList)
                {
                    if (v.b.IsFullDay)
                        // + full day
                        thisYearUsedDays++;
                    else
                        // + half day
                        thisYearUsedDays += 0.5;
                }

                switch (vacationType)
                {
                    // paid
                    case (int)CConstValue.VacationType.PaidVacationDay:
                        vacationTypeInfoModel.ThisYear.PaidTotalDays = thisYearSchema.TotalDays;
                        vacationTypeInfoModel.ThisYear.PaidUsedDays = thisYearUsedDays;
                        vacationTypeInfoModel.ThisYear.PaidDate = thisYearSchema.Date;

                        vacationTypeInfoModel.NextYear.PaidTotalDays = nextYearSchema.TotalDays;
                        vacationTypeInfoModel.NextYear.PaidUsedDays = nextYearUsedDays;
                        vacationTypeInfoModel.NextYear.PaidDate = nextYearSchema.Date;
                        break;

                    // sick
                    case (int)CConstValue.VacationType.SickDay:
                        vacationTypeInfoModel.ThisYear.SickTotalDays = thisYearSchema.TotalDays;
                        vacationTypeInfoModel.ThisYear.SickUsedDays = thisYearUsedDays;
                        vacationTypeInfoModel.ThisYear.SickDate = thisYearSchema.Date;

                        vacationTypeInfoModel.NextYear.SickTotalDays = nextYearSchema.TotalDays;
                        vacationTypeInfoModel.NextYear.SickUsedDays = nextYearUsedDays;
                        vacationTypeInfoModel.NextYear.SickDate = nextYearSchema.Date;
                        break;

                    // Entitlement
                    case (int)CConstValue.VacationType.EntitlementDay:
                        vacationTypeInfoModel.ThisYear.EntitlementTotalDays = thisYearSchema.TotalDays;
                        vacationTypeInfoModel.ThisYear.EntitlementUsedDays = thisYearUsedDays;
                        vacationTypeInfoModel.ThisYear.EntitlementDate = thisYearSchema.Date;

                        vacationTypeInfoModel.NextYear.EntitlementTotalDays = nextYearSchema.TotalDays;
                        vacationTypeInfoModel.NextYear.EntitlementUsedDays = nextYearUsedDays;
                        vacationTypeInfoModel.NextYear.EntitlementDate = nextYearSchema.Date;
                        break;
                }
            }
        }

        public int Add(VacationSchema obj)
        {
            try
            {
                _db.VacationSchemas.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.VacationSchemas.Max(x => x.VacationSchemaId);
        }

        public bool Update(VacationSchema obj)
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

        public bool Delete(VacationSchema obj)
        {
            try
            {
                _db.VacationSchemas.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public double GetTotalDays(int userId, int vacationType, DateTime date)
        {
            double totalDays = 0;

            VacationSchema vacation = null;
            // sickday
            if (vacationType == 2)
            {
                vacation = _db.VacationSchemas.OrderBy(x => x.Date).FirstOrDefault(x => x.UserId == userId && x.Date > date && x.VacationType == vacationType);
            }
            // others
            else
            {
                vacation = _db.VacationSchemas.FirstOrDefault(x => x.UserId == userId && x.Date.Year == date.Year && x.VacationType == vacationType);
            }

            if (vacation != null)
                totalDays = vacation.TotalDays;

            return totalDays;
        }
    }
}