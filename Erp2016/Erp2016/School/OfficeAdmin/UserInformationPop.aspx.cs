using System;
using System.Data;
using System.Text;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.OfficeAdmin
{
    public partial class UserInformationPop : PageBase
    {
        private int Id { get; set; }

        public UserInformationPop() : base((int)CConstValue.Menu.User)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (new CUser().IsUserInformation(CurrentGroupId) == false)
                Response.Redirect("~/NoPermission");

            Id = Convert.ToInt32(Request["id"]);

            if (!IsPostBack)
            {
                FileDownloadList1.InitFileDownloadList((int)CConstValue.Upload.User);

                var cUsrinfo = new CUserInfomation();
                var usrinfo = cUsrinfo.Get(Id);
                if (usrinfo != null)
                {
                    RadDatePickerActualHireDate.SelectedDate = usrinfo.ActualHireDate;
                    RadComboBoxJobType.SelectedValue = usrinfo.JobType.ToString();
                    CheckBoxBank.Checked = Convert.ToBoolean(usrinfo.BankInfo);
                    CheckBoxDiploma.Checked = Convert.ToBoolean(usrinfo.Diploma);
                    CheckBoxResume.Checked = Convert.ToBoolean(usrinfo.Resume);
                    CheckBoxOfferLetter.Checked = Convert.ToBoolean(usrinfo.OfferLetter);


                }

                FileDownloadList1.GetFileDownload(Id);
            }

            sqlDataSourceVacationSchema.WhereParameters.Clear();
            sqlDataSourceVacationSchema.WhereParameters.Add("UserId", DbType.Int32, Id.ToString());
            sqlDataSourceVacationSchema.Where = "UserId== @UserId";

            LinqDataSourceUserSalary.WhereParameters.Clear();
            LinqDataSourceUserSalary.WhereParameters.Add("UserId", DbType.Int32, Id.ToString());
            LinqDataSourceUserSalary.Where = "UserId== @UserId";


            LinqDataSourceUserStatus.WhereParameters.Clear();
            LinqDataSourceUserStatus.WhereParameters.Add("UserId", DbType.Int32, Id.ToString());
            LinqDataSourceUserStatus.Where = "UserId== @UserId";
        }

        protected void RadToolBarUserInformation_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Text)
            {
                case "Save":
                    var cUsrinfo = new CUserInfomation();
                    var usrinfo = cUsrinfo.Get(Id);

                    bool isSave = false;
                    if (usrinfo == null)
                    {
                        usrinfo = new Erp2016.Lib.UserInformation();
                        isSave = true;
                        usrinfo.UserId = Id;
                        usrinfo.CreatedId = CurrentUserId;
                        usrinfo.CreatedDate = DateTime.Now;
                    }
                    else
                    {
                        usrinfo.UpdatedId = CurrentUserId;
                        usrinfo.UpdatedDate = DateTime.Now;
                    }

                    usrinfo.ActualHireDate = RadDatePickerActualHireDate.SelectedDate;
                    usrinfo.JobType = Convert.ToInt32(RadComboBoxJobType.SelectedValue);
                    usrinfo.BankInfo = CheckBoxBank.Checked;
                    usrinfo.Diploma = CheckBoxDiploma.Checked;
                    usrinfo.Resume = CheckBoxResume.Checked;
                    usrinfo.OfferLetter = CheckBoxOfferLetter.Checked;

                    if (isSave)
                        cUsrinfo.Add(usrinfo);
                    else
                        cUsrinfo.Update(usrinfo);

                    // UP LOAD
                    FileDownloadList1.SaveFile(Id);

                    RunClientScript("Close();");
                    break;

                case "Close":
                    RunClientScript("Close();");
                    break;
            }
        }

        protected void RadGridVacationSchema_OnBatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {
            foreach (var command in e.Commands)
            {
                if (command.Type.ToString() != "Delete")
                {
                    var totalDays = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["TotalDays"]))) ? 0 : Convert.ToDouble(command.NewValues["TotalDays"]);
                    var date = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["Date"]))) ? DateTime.Now : Convert.ToDateTime(command.NewValues["Date"]);

                    command.NewValues["UserId"] = Id;

                    command.NewValues["TotalDays"] = totalDays;
                    command.NewValues["Date"] = date;

                    if (command.NewValues["VacationSchemaId"] == null)
                    {
                        command.NewValues["CreatedId"] = CurrentUserId.ToString();
                        command.NewValues["CreatedDate"] = DateTime.Now;
                    }
                    else
                    {
                        command.NewValues["UpdatedId"] = CurrentUserId.ToString();
                        command.NewValues["UpdatedDate"] = DateTime.Now;
                    }
                }
            }
        }

        protected void RadGridVacationSchema_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        protected void RadGridUserSalary_OnBatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {
            foreach (var command in e.Commands)
            {
                if (command.Type.ToString() != "Delete")
                {
                    var salary = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["Salary"]))) ? 0 : Convert.ToDecimal(command.NewValues["Salary"]);
                    var date = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["EffectDate"]))) ? DateTime.Now : Convert.ToDateTime(command.NewValues["EffectDate"]);

                    command.NewValues["UserId"] = Id;

                    command.NewValues["Salary"] = salary;
                    command.NewValues["EffectDate"] = date;

                    if (command.NewValues["UserSalaryId"] == null)
                    {
                        command.NewValues["CreatedId"] = CurrentUserId.ToString();
                        command.NewValues["CreatedDate"] = DateTime.Now;
                    }
                    else
                    {
                        command.NewValues["UpdatedId"] = CurrentUserId.ToString();
                        command.NewValues["UpdatedDate"] = DateTime.Now;
                    }
                }
            }
        }

        protected void RadGridUserSalary_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        protected void RadGridUserStatus_OnBatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {
            foreach (var command in e.Commands)
            {
                if (command.Type.ToString() != "Delete")
                {
                    var date = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["IssuedDate"]))) ? DateTime.Now : Convert.ToDateTime(command.NewValues["IssuedDate"]);

                    command.NewValues["UserId"] = Id;

                    command.NewValues["IssuedDate"] = date;

                    if (command.NewValues["UserStatusId"] == null)
                    {
                        command.NewValues["CreatedId"] = CurrentUserId.ToString();
                        command.NewValues["CreatedDate"] = DateTime.Now;
                    }
                    else
                    {
                        command.NewValues["UpdatedId"] = CurrentUserId.ToString();
                        command.NewValues["UpdatedDate"] = DateTime.Now;
                    }
                }
            }
        }

        protected void RadGridUserStatus_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e); ;
        }
    }
}