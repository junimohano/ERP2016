using System;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CGradeSchema
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        public GradeSchema Get(int gradeSchemaId)
        {
            return _db.GradeSchemas.FirstOrDefault(x => x.GradeSchemaId == gradeSchemaId);
        }


        public int Add(GradeSchema obj)
        {
            try
            {
                _db.GradeSchemas.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.GradeSchemas.Max(x => x.GradeSchemaId);
        }

        public bool Update(GradeSchema obj)
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

        public bool Delete(GradeSchema obj)
        {
            try
            {
                _db.GradeSchemas.DeleteOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            return true;
        }

        public GradeSchema GetGlobal(int siteLocationId)
        {
            return _db.GradeSchemas.FirstOrDefault(x => x.SiteLocationId == siteLocationId && x.IsGlobal);
        }

        public GradeSchema GetGradeSchema(int siteLocationId, int programId, int? programCourseId, int? programCourseLevelId, int programClassId)
        {
            var result = _db.GradeSchemas.FirstOrDefault(x => x.SiteLocationId == siteLocationId && x.IsGlobal);
            if (result != null)
            {
                var result4 = _db.GradeSchemas.FirstOrDefault(x => x.SiteLocationId == siteLocationId && x.ProgramId == programId && x.ProgramCourseId == null && x.ProgramCourseLevelId == null && x.ProgramClassId == null);
                if (result4 != null)
                    result = result4;

                if (programCourseId != null)
                {
                    var result3 = _db.GradeSchemas.FirstOrDefault(x => x.SiteLocationId == siteLocationId && x.ProgramId == programId && x.ProgramCourseId == programCourseId && x.ProgramCourseLevelId == null && x.ProgramClassId == null);
                    if (result3 != null)
                        result = result3;
                }

                if (programCourseLevelId != null)
                {
                    var result2 = _db.GradeSchemas.FirstOrDefault(x => x.SiteLocationId == siteLocationId && x.ProgramId == programId && x.ProgramCourseId == programCourseId && x.ProgramCourseLevelId == programCourseLevelId && x.ProgramClassId == null);
                    if (result2 != null)
                        result = result2;
                }

                var result1 = _db.GradeSchemas.FirstOrDefault(x => x.SiteLocationId == siteLocationId && x.ProgramId == programId && x.ProgramCourseId == programCourseId && x.ProgramCourseLevelId == programCourseLevelId && x.ProgramClassId == programClassId);
                if (result1 != null)
                    result = result1;
            }

            return result;
        }

        public string GetGradeLetter(int gradeSchemaId, int programClassStudentId)
        {
            double? sum = _db.Grades.Where(x => x.GradeSchemaId == gradeSchemaId && x.ProgramClassStudentId == programClassStudentId).Sum(x => x.Score);
            if (sum != null)
            {
                GradeSchemaLetterItem result;
                if (sum >= 100)
                    result = _db.GradeSchemaLetterItems.FirstOrDefault(x => x.GradeSchemaId == gradeSchemaId && x.RangeFrom >= 100 && x.RangeTo <= sum);
                else
                    result = _db.GradeSchemaLetterItems.FirstOrDefault(x => x.GradeSchemaId == gradeSchemaId && x.RangeFrom > sum && x.RangeTo <= sum);

                if (result != null)
                    return result.LetterGrade;
            }

            return string.Empty;
        }

    }
}