using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;
using Convert = System.Convert;

namespace School.OfficeAdmin
{
    public partial class ExpensePop : PageBase
    {
        private int Id { get; set; }

        public ExpensePop() : base((int)CConstValue.Menu.Expense)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Id = Convert.ToInt32(Request["id"]);

            if (!IsPostBack)
            {
                foreach (GridColumn v in RadGridExpenseDetail.Columns)
                {
                    if (v.GetType() == typeof(GridTemplateColumn))
                    {
                        var column = (GridTemplateColumn)v;
                        switch (column.UniqueName)
                        {
                            case "Date":
                                column.DefaultInsertValue = DateTime.Today.ToString("MM-dd-yyyy");
                                break;
                        }
                    }
                }

                var scriptManager = (RadScriptManager)Page.Master.FindControl("RadScriptManager1");
                //scriptManager.Scripts.Add(new ScriptReference() { Path = ResolveUrl("~/assets/js/jquery.printArea.js") });
                //scriptManager.RegisterPostBackControl(RadButtonFileDownload);
                FileDownloadList1.InitFileDownloadList((int)CConstValue.Upload.Expense);

                var obj = new CExpense();
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
                    obj = new CExpense(Id);

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
                        else if (approvalType == ((int)CConstValue.ApprovalStatus.Requested).ToString())
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
                            var supervisor = refundApproveInfo.CheckApprovalEnable((int)CConstValue.Approval.Expense, Convert.ToInt32(Id));

                            if (CurrentUserId == supervisor)
                            {
                                buttonList.Add("Approve");
                                buttonList.Add("Reject");
                                buttonList.Add("Revise");
                                buttonList.Add("Close");
                            }
                            else
                                buttonList.Add("Close");
                        }

                        SetVisibleItems(false);
                    }
                    // Hire from HQ
                    else if (requestOrApprovalType == "2")
                    {
                        // Wating for review from HQ
                        if (approvalType == ((int)CConstValue.ApprovalStatus.WaitingForPreviewFromHq).ToString())
                        {
                            buttonList.Add("Accept");
                            buttonList.Add("Reject");
                            buttonList.Add("Close");
                        }
                        // In progress
                        else if (approvalType == ((int)CConstValue.ApprovalStatus.InProgress).ToString())
                        {
                            buttonList.Add("Print");
                            buttonList.Add("Complete");
                            buttonList.Add("Reject");
                            buttonList.Add("Close");
                        }
                        // Approved
                        else if (approvalType == ((int) CConstValue.ApprovalStatus.Approved).ToString())
                        {
                            buttonList.Add("Print");
                            buttonList.Add("Close");
                        }
                        else
                        {
                            buttonList.Add("Close");
                        }

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

                // new or temp
                if (approvalType == ((int)CConstValue.ApprovalStatus.Canceled).ToString() || approvalType == string.Empty)
                {
                    FileDownloadList1.SetVisibieUploadControls(true);
                }
                else
                {
                    FileDownloadList1.SetVisibieUploadControls(false);
                }

                var dt = new DataTable();
                dt.Columns.Add("DocNo");
                dt.Columns.Add("Site");
                dt.Columns.Add("Location");
                dt.Columns.Add("Name");
                dt.Columns.Add("Date");
                var newDr = dt.NewRow();
                newDr["DocNo"] = obj.DocNo;
                newDr["Site"] = obj.Site;
                newDr["Location"] = obj.Location;
                newDr["Name"] = obj.Name;
                newDr["Date"] = obj.Date;
                dt.Rows.Add(newDr);

                RadGridInfo.DataSource = dt;

                // date
                if (obj.StartDate != null)
                    RadDatePickerStart.SelectedDate = obj.StartDate;
                if (obj.EndDate != null)
                    RadDatePickerEnd.SelectedDate = obj.EndDate;

                RadNumericTextBoxCashAdvance.Value = (double)obj.CashAdvance;
            }
        }

        private void SetVisibleItems(bool isActive)
        {
            if (isActive == false)
            {
                RadDatePickerStart.Enabled = false;
                RadDatePickerEnd.Enabled = false;
                FileDownloadList1.SetVisibieUploadControls(false);
                RadNumericTextBoxCashAdvance.Enabled = false;

                RadGridExpenseDetail.AllowAutomaticInserts = false;
                RadGridExpenseDetail.AllowAutomaticUpdates = false;
                RadGridExpenseDetail.AllowAutomaticDeletes = false;
                RadGridExpenseDetail.MasterTableView.EditMode = GridEditMode.InPlace;
                RadGridExpenseDetail.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                // hidden delete button
                RadGridExpenseDetail.MasterTableView.Columns[RadGridExpenseDetail.MasterTableView.Columns.Count - 1].Visible = false;
            }
        }

        protected void RadGridExpenseDetail_Load(object sender, EventArgs e)
        {
            LinqDataSourceExpenseDetail.WhereParameters.Clear();
            LinqDataSourceExpenseDetail.WhereParameters.Add("ExpenseId", DbType.Int32, Id.ToString());
            LinqDataSourceExpenseDetail.Where = "ExpenseId == @ExpenseId";
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            // Save
            if (e.Item.Text == "TempSave" || e.Item.Text == "Request")
            {
                if (IsValid)
                {
                    var cObj = new CExpense();
                    var obj = cObj.Get(Id);

                    // new one
                    if (obj == null)
                    {
                        obj = new Erp2016.Lib.Expense();
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
                        ViewState["NewIndex"] = obj.ExpenseId.ToString();
                    }

                    obj.ApprovalId = CurrentUserId;
                    obj.ApprovalDate = DateTime.Now;

                    if (e.Item.Text == "TempSave")
                        obj.ApprovalStatus = null;
                    else
                    {
                        var cApprovalHistory = new CApprovalHistory();
                        cApprovalHistory.DelApprovalHistory((int)CConstValue.Approval.Expense, Convert.ToInt32(ViewState["NewIndex"]));

                        // approve request 
                        var approval = new CApproval();
                        var approvalResult = approval.ApproveRequstCreate((int)CConstValue.Approval.Expense, CurrentUserId, Convert.ToInt32(ViewState["NewIndex"]));
                        if (approvalResult > 0)
                        {
                            obj.ApprovalStatus = approvalResult;
                        }
                        else
                        {
                            ShowMessage("Failed");
                            return;
                        }

                        // $5000 
                        if (RadNumericTextBoxGrandTotal.Value > 5000)
                        {
                            
                        }

                        new CMail().SendMail(CConstValue.Approval.Expense, CConstValue.MailStatus.ToApproveUser, Convert.ToInt32(ViewState["NewIndex"]), string.Empty, CurrentUserId);
                    }

                    obj.PeriodStart = RadDatePickerStart.SelectedDate;
                    obj.PeriodEnd = RadDatePickerEnd.SelectedDate;
                    obj.CashAdvance = (decimal)RadNumericTextBoxCashAdvance.Value;

                    cObj.Update(obj);

                    // save uploading file
                    FileDownloadList1.SaveFile(Convert.ToInt32(ViewState["NewIndex"]));

                    // save other tables
                    RunClientScript("SaveChanges();");
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
            // Accept
            else if (e.Item.Text == "Accept")
            {
                RunClientScript("ShowApprovalAcceptWindow('" + Id + "');");
            }
            // Complete
            else if (e.Item.Text == "Complete")
            {
                RunClientScript("ShowApprovalCompleteWindow('" + Id + "');");
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

        protected void RadGridExpenseDetail_OnBatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {
            foreach (var command in e.Commands)
            {
                if (command.Type.ToString() != "Cancel")
                {
                    command.NewValues["ExpenseId"] = Convert.ToInt32(ViewState["NewIndex"]);
                    command.NewValues["Date"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["Date"]))) ? new DateTime(9999, 12, 31) : command.NewValues["Date"];
                    command.NewValues["Office"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["Office"]))) ? 0 : Convert.ToDecimal(command.NewValues["Office"]);
                    command.NewValues["Lodging"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["Lodging"]))) ? 0 : Convert.ToDecimal(command.NewValues["Lodging"]);
                    command.NewValues["Ground"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["Ground"]))) ? 0 : Convert.ToDecimal(command.NewValues["Ground"]);
                    command.NewValues["Meals"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["Meals"]))) ? 0 : Convert.ToDecimal(command.NewValues["Meals"]);
                    command.NewValues["Advertising"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["Advertising"]))) ? 0 : Convert.ToDecimal(command.NewValues["Advertising"]);
                    command.NewValues["Mail"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["Mail"]))) ? 0 : Convert.ToDecimal(command.NewValues["Mail"]);
                    command.NewValues["Telephone"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["Telephone"]))) ? 0 : Convert.ToDecimal(command.NewValues["Telephone"]);
                    command.NewValues["Km"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["Km"]))) ? 0 : Convert.ToDecimal(command.NewValues["Km"]);
                    command.NewValues["Kilometrage"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["Kilometrage"]))) ? 0 : Convert.ToDecimal(command.NewValues["Kilometrage"]);
                    command.NewValues["Miscellaneous"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["Miscellaneous"]))) ? 0 : Convert.ToDecimal(command.NewValues["Miscellaneous"]);

                    command.NewValues["CreatedId"] = CurrentUserId;
                    command.NewValues["CreatedDate"] = DateTime.Now;
                }
            }
        }

        protected void ApprovalLine1_OnLoad(object sender, EventArgs e)
        {
            LinqDataSource LinqDataSourceApprovalList = ApprovalLine1.GetSqlDataSourceApprovalList();
            if (LinqDataSourceApprovalList != null)
            {
                LinqDataSourceApprovalList.WhereParameters.Clear();
                LinqDataSourceApprovalList.WhereParameters.Add("MenuSeqId", DbType.Int32, Id.ToString());
                LinqDataSourceApprovalList.WhereParameters.Add("ApproveType", DbType.Int32, ((int)CConstValue.Approval.Expense).ToString());
                LinqDataSourceApprovalList.Where = "MenuSeqId == @MenuSeqId AND ApproveType == @ApproveType";
            }
        }
    }
}