using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Erp2016.Lib
{
    public class CGrade
    {
        private readonly linqDBDataContext _db = new linqDBDataContext();

        /// <summary>
        ///     insert data into Grade table base on gradeSchemaItems if grade table doesn't have them.
        /// </summary>
        /// <param name="programClassStudentId"></param>
        /// <param name="currentUserId"></param>
        public void InsertGradeDataBaseOnGradeSchemaItem(int programClassStudentId, int gradeSchemaId, int currentUserId)
        {
            try
            {
                var resultList = _db.GradeSchemas.Where(c => c.GradeSchemaId == gradeSchemaId).Join(_db.GradeSchemaItems, x => x.GradeSchemaId, y => y.GradeSchemaId, (x, y) => new {x, y})
                    .GroupJoin(_db.Grades.Where(z => z.ProgramClassStudentId == programClassStudentId), a => a.y.GradeSchemaItemId, b => b.GradeSchemaItemId, (a, b) => new
                    {
                        //TempGradeSchemaId = a.x.GradeSchemaId,
                        TempGradeSchemaItemId = a.y.GradeSchemaItemId,
                        TempGradesObj = b.FirstOrDefault()
                    });

                var insertList = new List<Grade>();
                foreach (var result in resultList)
                {
                    if (result.TempGradesObj == null)
                    {
                        insertList.Add(new Grade
                        {
                            //GradeSchemaId = result.TempGradeSchemaId,
                            GradeSchemaId = gradeSchemaId,
                            GradeSchemaItemId = result.TempGradeSchemaItemId,
                            ProgramClassStudentId = programClassStudentId,
                            Score = null,
                            CreatedId = currentUserId,
                            CreatedDate = DateTime.Now
                        });
                    }
                }

                _db.Grades.InsertAllOnSubmit(insertList);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }

        public int Add(Grade obj)
        {
            try
            {
                _db.Grades.InsertOnSubmit(obj);
                _db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return -1;
            }
            return _db.Grades.Max(x => x.GradeId);
        }

        public bool Update(Grade obj)
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

        public bool Delete(Grade obj)
        {
            try
            {
                _db.Grades.DeleteOnSubmit(obj);
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