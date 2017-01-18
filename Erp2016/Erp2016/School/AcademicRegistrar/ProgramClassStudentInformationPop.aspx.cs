using System;
using System.Data;
using System.Text;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.AcademicRegistrar
{
    public partial class ProgramClassStudentInformationPop : PageBase
    {
        private int ProgramClassId { get; set; }
        private int? ProgramCourseId { get; set; }

        public ProgramClassStudentInformationPop() : base((int)CConstValue.Menu.ProgramClass)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ProgramClassId = Convert.ToInt32(Request["programClassId"]);
            ProgramCourseId = Request["programCourseId"] == string.Empty ? (int?)null : Convert.ToInt32(Request["programCourseId"]);

            if (!IsPostBack)
            {
            }

            LinqDataSourceClassStudentList.WhereParameters.Clear();
            LinqDataSourceClassStudentList.WhereParameters.Add("ProgramClassId", DbType.Int32, ProgramClassId.ToString());
            LinqDataSourceClassStudentList.Where = "ProgramClassId == @ProgramClassId";

            LinqDataSourceProgramStudentList.WhereParameters.Clear();
            LinqDataSourceProgramStudentList.WhereParameters.Add("ProgramClassId", DbType.Int32, ProgramClassId.ToString());
            LinqDataSourceProgramStudentList.Where = "ProgramClassId == @ProgramClassId";
        }

        protected void RadGridProgramStudent_OnRowDrop(object sender, GridDragDropEventArgs e)
        {
            if (e.DraggedItems.Count != 0)
            {
                foreach (var dataItem in e.DraggedItems)
                {
                    //string pid = dataItem.GetDataKeyValue("ProgramRegistrationId").ToString();
                    //var preg = new CProgramRegistration(Convert.ToInt32(pid));

                    //preg.IsTransfer = true;
                    //preg.TransferDate = DateTime.Now;
                    //preg.TransferFromId = CurrentUserId;

                    //if (preg.Update())
                    //{
                    var cProgramclassStudent = new CProgramClassStudent();
                    var programClassStudent = new ProgramClassStudent();
                    programClassStudent.ProgramClassId = Convert.ToInt32(ProgramClassId);
                    if (ProgramCourseId != null)
                        programClassStudent.ProgramCourseId = Convert.ToInt32(ProgramCourseId);
                    programClassStudent.StudentId = Convert.ToInt32(dataItem.GetDataKeyValue("StudentId").ToString());
                    programClassStudent.ProgramRegistrationId = Convert.ToInt32(dataItem.GetDataKeyValue("ProgramRegistrationId").ToString());
                    programClassStudent.CreatedDate = DateTime.Now;
                    programClassStudent.CreatedId = CurrentUserId;


                    if (cProgramclassStudent.Add(programClassStudent) > 0)
                    {
                        ShowMessage("Transfer Success");
                    }
                }

                refreshGrid();
            }
            else
            {
                ShowMessage("Transfer Failed");
            }
        }

        protected void RadGridClassStudent_OnRowDrop(object sender, GridDragDropEventArgs e)
        {
            if (e.DraggedItems.Count != 0)
            {
                foreach (var dataItem in e.DraggedItems)
                {
                    var sid = dataItem.GetDataKeyValue("ProgramClassStudentId").ToString();
                    var cProgramClassStudent = new CProgramClassStudent();
                    var programClassStudent = cProgramClassStudent.Get(Convert.ToInt32(sid));

                    var cStudent = new CStudent();
                    var student = cStudent.Get(programClassStudent.StudentId);

                    var cProgramRegistration = new CProgramRegistration();
                    var programRegistration = cProgramRegistration.Get(programClassStudent.ProgramRegistrationId);
                    if (programRegistration.EndDate < DateTime.Today)
                        ShowMessage("Move Failed : " + cStudent.GetStudentName(student) + "'s the End Date should not be earlier than today.");
                    else if (cProgramClassStudent.Delete(programClassStudent))
                        ShowMessage("Moved successfuly : " + cStudent.GetStudentName(student));
                }

                refreshGrid();

            }
            else
            {
                ShowMessage("Transfer Failed");
            }
        }

        private void refreshGrid()
        {
            RadGridClassStudent.Rebind();
            RadGridProgramStudent.Rebind();
        }

        protected void RadGridProgramStudent_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        protected void RadGridClassStudent_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        protected void RadToolBar1_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Text)
            {
                case "Move students to another class":
                    var sb = new StringBuilder();
                    foreach (GridDataItem item in RadGridClassStudent.SelectedItems)
                    {
                        sb.Append(item.GetDataKeyValue("ProgramClassStudentId") + ",");
                    }

                    if (sb.Length > 0)
                    {
                        sb = sb.Remove(sb.Length - 1, 1);
                        RunClientScript("ShowStudentMovement('" + sb + "');");
                    }
                    break;
            }
        }

        protected void ButtonGridRefresh_OnClick(object sender, EventArgs e)
        {
            refreshGrid();
        }
    }
}