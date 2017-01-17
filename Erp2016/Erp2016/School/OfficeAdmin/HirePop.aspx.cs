using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.OfficeAdmin
{
    public partial class HirePop : PageBase
    {
        private int Id { get; set; }

        public HirePop() : base((int)CConstValue.Menu.Hire)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Id = Convert.ToInt32(Request["id"]);

            RadAsyncUpload asyncUpload = FileDownloadList1.GetAsyncUpload();
            asyncUpload.FileUploaded += HirePop_FileUploaded;

            if (!IsPostBack)
            {
                var scriptManager = (RadScriptManager)Page.Master.FindControl("RadScriptManager1");
                //scriptManager.Scripts.Add(new ScriptReference() { Path = ResolveUrl("~/assets/js/jquery.printArea.js") });
                //scriptManager.RegisterPostBackControl(RadButtonFileDownload);
                FileDownloadList1.InitFileDownloadList((int)CConstValue.Upload.Hire);

                var obj = new CHire();
                var requestOrApprovalType = Request["requestOrApprovalType"];
                var approvalType = Request["approvalType"];

                var buttonList = new List<string>();

                // new
                if (Request["createOrListType"] == "0")
                {
                    obj = obj.GetNewDocument(CurrentUserId);

                    buttonList.Add("TempSave");
                    buttonList.Add("Request");
                    buttonList.Add("Close");

                    SetVisibleItems(true);
                }
                // select
                else
                {
                    FileDownloadList1.GetFileDownload(Convert.ToInt32(Id));

                    // date
                    obj = new CHire(Id);

                    // request list
                    if (requestOrApprovalType == "0")
                    {
                        // Revise
                        if (approvalType == ((int)CConstValue.ApprovalStatus.Revise).ToString())
                        {
                            buttonList.Add("Request");
                            buttonList.Add("Cancel");
                            buttonList.Add("Close");

                            SetVisibleItems(true);
                        }
                        // TempSave
                        else if (approvalType == string.Empty)
                        {
                            buttonList.Add("TempSave");
                            buttonList.Add("Request");
                            buttonList.Add("Cancel");
                            buttonList.Add("Close");

                            SetVisibleItems(true);
                        }
                        // Request
                        else if (approvalType == "1")
                        {
                            buttonList.Add("Cancel");
                            buttonList.Add("Close");

                            SetVisibleItems(false);
                        }
                        else
                        {
                            buttonList.Add("Close");

                            SetVisibleItems(false);
                        }
                    }
                    // approval
                    else if (requestOrApprovalType == "1")
                    {
                        // approved or rejected
                        if (approvalType == ((int)CConstValue.ApprovalStatus.Approved).ToString() ||
                            approvalType == ((int)CConstValue.ApprovalStatus.Rejected).ToString() ||
                            approvalType == ((int)CConstValue.ApprovalStatus.Canceled).ToString())
                        {
                            buttonList.Add("Close");
                        }
                        else
                        {
                            var refundApproveInfo = new CGlobal();
                            var supervisor = refundApproveInfo.CheckApprovalEnable((int)CConstValue.Approval.Hire, Convert.ToInt32(Id));

                            if (CurrentUserId == supervisor)
                            {
                                buttonList.Add("Approve");
                                buttonList.Add("Reject");
                                buttonList.Add("Revise");
                                buttonList.Add("Close");
                            }
                            else
                            {
                                buttonList.Add("Close");
                            }
                        }

                        SetVisibleItems(false);
                    }
                    // Hire from HQ
                    else if (requestOrApprovalType == "2")
                    {
                        buttonList.Add("Print");
                        buttonList.Add("Cancel");
                        buttonList.Add("Close");

                        SetVisibleItems(false);
                    }
                }

                foreach (RadToolBarItem item in RadToolBar1.Items)
                {
                    if (buttonList.Contains(item.Text))
                        item.Visible = true;
                    else
                        item.Visible = false;
                }

                if (approvalType == ((int)CConstValue.ApprovalStatus.Approved).ToString())
                {
                    FileDownloadList1.SetVisibieUploadControls(true);
                    //if (FileDownloadList1.GetFileInfoCountForHire() == 0)
                    //    FileDownloadList1.SetVisibieUploadControls(true);
                    //else
                    //    FileDownloadList1.SetVisibieUploadControls(false);
                }
                else
                    FileDownloadList1.SetVisibieUploadControls(false);

                var dt = new DataTable();
                dt.Columns.Add("DocNo");
                dt.Columns.Add("DateOfIssue");
                dt.Columns.Add("DraftingDepartment");
                dt.Columns.Add("ShelfLife");
                var newDr = dt.NewRow();
                newDr["DocNo"] = obj.DocNo;
                newDr["DateOfIssue"] = obj.DateOfIssue;
                newDr["DraftingDepartment"] = obj.DraftingDepartment;
                newDr["ShelfLife"] = obj.ShelfLife;
                dt.Rows.Add(newDr);

                RadGridInfo.DataSource = dt;

                // Get Data
                var hireObj = obj.Get(Id);
                if (hireObj != null)
                {
                    RadComboBoxDepartment.SelectedValue = hireObj.Department.ToString();
                    RadComboBoxGenre.SelectedValue = hireObj.Genre.ToString();
                    RadComboBoxCondition.SelectedValue = hireObj.Condition.ToString();

                    RadTextBoxJobTitle.Text = hireObj.JobTitle;
                    RadTextBoxReasonForHiring.Text = hireObj.ReasonForHiring;
                    RadEditorDuties.Content = hireObj.DutiesAndResponsibilities;
                    RadEditorSkills.Content = hireObj.SkillsAndExperienceAndQualification;
                    RadTextBoxSalary.Text = hireObj.SalaryOrWage;
                    RadTextBoxEmployment.Text = hireObj.EmploymentCategory;
                    RadTextBoxHours.Text = hireObj.HoursOrDaysOfWork;
                    RadEditorAdditional.Content = hireObj.AdditionalComments;
                }
            }
        }

        private void HirePop_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            // save uploading file
            FileDownloadList1.SaveFile(Id);
        }

        private void SetVisibleItems(bool isActive)
        {
            if (isActive == false)
            {
                RadComboBoxDepartment.Enabled = false;
                RadComboBoxGenre.Enabled = false;
                RadComboBoxCondition.Enabled = false;

                RadTextBoxJobTitle.Enabled = false;
                RadTextBoxReasonForHiring.Enabled = false;
                RadTextBoxSalary.Enabled = false;
                RadTextBoxEmployment.Enabled = false;
                RadTextBoxHours.Enabled = false;

                RadEditorDuties.EditModes = EditModes.Preview;
                RadEditorSkills.EditModes = EditModes.Preview;
                RadEditorAdditional.EditModes = EditModes.Preview;
            }
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            // Save or Modify
            if (e.Item.Text == "TempSave" || e.Item.Text == "Request")
            {
                if (IsValid)
                {
                    var cObj = new CHire();
                    var obj = cObj.Get(Id);

                    // new one
                    if (obj == null)
                    {
                        obj = new Erp2016.Lib.Hire();
                        obj.CreatedId = Convert.ToInt32(CurrentUserId);
                        obj.CreatedDate = DateTime.Now;
                        int newIndex = Convert.ToInt32(cObj.Add(obj).ToString());
                        obj = cObj.Get(newIndex);
                        ViewState["NewIndex"] = newIndex;
                    }
                    else
                    {
                        obj.UpdatedId = Convert.ToInt32(CurrentUserId);
                        obj.UpdatedDate = DateTime.Now;
                        ViewState["NewIndex"] = obj.HireId.ToString();
                    }

                    obj.ApprovalId = CurrentUserId;
                    obj.ApprovalDate = DateTime.Now;


                    if (e.Item.Text == "TempSave")
                        obj.ApprovalStatus = null;
                    else
                    {
                        var cApprovalHistory = new CApprovalHistory();
                        cApprovalHistory.DelApprovalHistory((int)CConstValue.Approval.Hire, Convert.ToInt32(ViewState["NewIndex"]));

                        // approve request 
                        var approval = new CApproval();
                        var approvalResult = approval.ApproveRequstCreate((int)CConstValue.Approval.Hire, CurrentUserId, Convert.ToInt32(ViewState["NewIndex"]));
                        if (approvalResult > 0)
                        {
                            obj.ApprovalStatus = approvalResult;
                        }
                        else
                        {
                            ShowMessage("Failed");
                            return;
                        }

                        new CMail().SendMail(CConstValue.Approval.Hire, CConstValue.MailStatus.ToApproveUser, Convert.ToInt32(ViewState["NewIndex"]), string.Empty, CurrentUserId);
                    }

                    obj.Department = Convert.ToInt32(RadComboBoxDepartment.SelectedValue);
                    obj.Genre = Convert.ToInt32(RadComboBoxGenre.SelectedValue);
                    obj.Condition = Convert.ToInt32(RadComboBoxCondition.SelectedValue);

                    obj.JobTitle = RadTextBoxJobTitle.Text;
                    obj.ReasonForHiring = RadTextBoxReasonForHiring.Text;
                    obj.DutiesAndResponsibilities = RadEditorDuties.Text;
                    obj.SkillsAndExperienceAndQualification = RadEditorSkills.Text;
                    obj.SalaryOrWage = RadTextBoxSalary.Text;
                    obj.EmploymentCategory = RadTextBoxEmployment.Text;
                    obj.HoursOrDaysOfWork = RadTextBoxHours.Text;
                    obj.AdditionalComments = RadEditorAdditional.Text;

                    cObj.Update(obj);

                    // save other tables
                    RunClientScript("Close();");
                }
                else
                    ShowMessage("Failed");
            }
            // Revise
            else if (e.Item.Text == "Revise")
            {
                RunClientScript("ShowApprovalReviseWindow('" + Id + "');");
            }
            // Approval
            else if (e.Item.Text == "Approve")
            {
                RunClientScript("ShowApprovalWindow('" + Id + "');");
            }
            // Reject
            else if (e.Item.Text == "Reject")
            {
                RunClientScript("ShowApprovalRejectWindow('" + Id + "');");
            }
            // Print
            else if (e.Item.Text == "Print")
            {
                RunClientScript("ShowPrint();");
            }
            // Cancel
            else if (e.Item.Text == "Cancel")
            {
                RunClientScript("ShowApprovalCancelWindow('" + Id + "');");
            }
            // close
            else if (e.Item.Text == "Close")
            {
                RunClientScript("Close();");
            }
        }

        protected void ApprovalLine1_OnLoad(object sender, EventArgs e)
        {
            LinqDataSource LinqDataSourceApprovalList = ApprovalLine1.GetSqlDataSourceApprovalList();
            if (LinqDataSourceApprovalList != null)
            {
                LinqDataSourceApprovalList.WhereParameters.Clear();
                LinqDataSourceApprovalList.WhereParameters.Add("MenuSeqId", DbType.Int32, Id.ToString());
                LinqDataSourceApprovalList.WhereParameters.Add("ApproveType", DbType.Int32, ((int)CConstValue.Approval.Hire).ToString());
                LinqDataSourceApprovalList.Where = "MenuSeqId == @MenuSeqId AND ApproveType == @ApproveType";
            }
        }
    }
}