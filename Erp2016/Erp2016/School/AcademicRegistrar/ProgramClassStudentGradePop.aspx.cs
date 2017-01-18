using System;
using System.Data;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.AcademicRegistrar
{
    public partial class ProgramClassStudentGradePop : PageBase
    {
        private int ProgramClassId { get; set; }
        private int? GradeSchemaId { get; set; }

        public ProgramClassStudentGradePop() : base((int)CConstValue.Menu.ProgramClass)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ProgramClassId = Convert.ToInt32(Request["programClassId"]);

            if (!IsPostBack)
            {
            }

            int? programCourseLevelId = null;
            int? programCourseId = null;
            int programId = 0;

            var cProgramClass = new CProgramClass();
            var programClass = cProgramClass.Get(ProgramClassId);
            if (programClass != null)
            {
                programCourseLevelId = programClass.ProgramCourseLevelId;
                programCourseId = programClass.ProgramCourseId;
                programId = programClass.ProgramId;
            }

            var cGradeSchema = new CGradeSchema();
            var gradeSchema = cGradeSchema.GetGradeSchema(CurrentSiteLocationId, programId, programCourseId, programCourseLevelId, ProgramClassId);

            if (gradeSchema != null)
            {
                RadTextBoxGrade.Text = gradeSchema.Name;
                GradeSchemaId = gradeSchema.GradeSchemaId;
            }

            // ProgramClass Grid
            LinqDataSourceClassStudentList.WhereParameters.Clear();
            LinqDataSourceClassStudentList.WhereParameters.Add("ProgramClassId", DbType.Int32, ProgramClassId.ToString());
            LinqDataSourceClassStudentList.Where = "ProgramClassId == @ProgramClassId";
        }

        private void ResetForm()
        {
            RadTextBoxLetter.Text = string.Empty;
        }

        protected void RadGridClassStudent_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ResetForm();

            if (GradeSchemaId != null)
            {
                // Create Grade data based on GradeSchemaItem
                var grade = new CGrade();
                grade.InsertGradeDataBaseOnGradeSchemaItem(Convert.ToInt32(RadGridClassStudent.SelectedValues["ProgramClassStudentId"]), (int)GradeSchemaId, CurrentUserId);
            }
            //SetLetter();
        }

        private void SetLetter()
        {
            if (GradeSchemaId != null && RadGridClassStudent.SelectedValue != null)
            {
                var gradeSchema = new CGradeSchema();
                RadTextBoxLetter.Text = gradeSchema.GetGradeLetter((int)GradeSchemaId, Convert.ToInt32(RadGridClassStudent.SelectedValues["ProgramClassStudentId"]));
            }
        }

        protected void RadGridGrade_OnBatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {
            foreach (var command in e.Commands)
            {
                if (command.Type.ToString() != "Delete")
                {
                    command.NewValues["UpdatedId"] = CurrentUserId.ToString();
                    command.NewValues["UpdatedDate"] = DateTime.Now;
                }
            }
        }

        protected void RadGridGrade_OnPreRender(object sender, EventArgs e)
        {
            RefreshGrade();


            SetLetter();
        }

        public void RefreshGrade()
        {
            LinqDataSourceGrade.WhereParameters.Clear();
            if (RadGridClassStudent.SelectedValue != null && GradeSchemaId != null)
            {
                LinqDataSourceGrade.WhereParameters.Add("ProgramClassStudentId", DbType.Int32, RadGridClassStudent.SelectedValues["ProgramClassStudentId"].ToString());
                LinqDataSourceGrade.WhereParameters.Add("GradeSchemaId", DbType.Int32, GradeSchemaId.ToString());
            }
            else
            {
                LinqDataSourceGrade.WhereParameters.Add("ProgramClassStudentId", DbType.Int32, 0.ToString());
                LinqDataSourceGrade.WhereParameters.Add("GradeSchemaId", DbType.Int32, 0.ToString());
            }
            LinqDataSourceGrade.Where = "ProgramClassStudentId == @ProgramClassStudentId && GradeSchemaId == @GradeSchemaId";
        }

        protected void RadGridClassStudent_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }
    }
}