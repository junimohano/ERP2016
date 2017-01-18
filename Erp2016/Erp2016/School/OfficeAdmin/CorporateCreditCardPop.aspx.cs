using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.OfficeAdmin
{
    public partial class CorporateCreditCardPop : PageBase
    {
        private int Id { get; set; }

        public CorporateCreditCardPop() : base((int)CConstValue.Menu.CorporateCreditCard)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Id = Convert.ToInt32(Request["id"]);

            if (!IsPostBack)
            {
                foreach (GridColumn v in RadGridCorporateCreditCardDetail.Columns)
                {
                    if (v.GetType() == typeof(GridTemplateColumn))
                    {
                        var column = (GridTemplateColumn)v;
                        switch (column.UniqueName)
                        {
                            case "Date":
                                column.DefaultInsertValue = DateTime.Today.ToString("MM-dd-yyyy");
                                break;
                            case "SiteLocationId":
                                var vwSiteLocationList = new CSiteLocation().GetSiteLocationList(CurrentSiteLocationId);
                                if (vwSiteLocationList != null)
                                {
                                    column.DefaultInsertValue = vwSiteLocationList.SiteAndSiteLocationName;
                                }
                                break;
                        }
                    }
                }

                var scriptManager = (RadScriptManager)Page.Master.FindControl("RadScriptManager1");
                //scriptManager.Scripts.Add(new ScriptReference() { Path = ResolveUrl("~/assets/js/jquery.printArea.js") });
                //scriptManager.RegisterPostBackControl(RadButtonFileDownload);
                FileDownloadList1.InitFileDownloadList((int)CConstValue.Upload.CorporateCreditCard);

                var obj = new CCorporateCreditCard();
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
                    obj = new CCorporateCreditCard(Id);

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
                            var supervisor = refundApproveInfo.CheckApprovalEnable((int)CConstValue.Approval.CorporateCreditCard, Convert.ToInt32(Id));

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
                        else if (approvalType == ((int)CConstValue.ApprovalStatus.Approved).ToString())
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
            }
        }

        private void SetVisibleItems(bool isActive)
        {
            if (isActive == false)
            {
                RadDatePickerStart.Enabled = false;
                RadDatePickerEnd.Enabled = false;
                FileDownloadList1.SetVisibieUploadControls(false);

                RadGridCorporateCreditCardDetail.AllowAutomaticInserts = false;
                RadGridCorporateCreditCardDetail.AllowAutomaticUpdates = false;
                RadGridCorporateCreditCardDetail.AllowAutomaticDeletes = false;
                RadGridCorporateCreditCardDetail.MasterTableView.EditMode = GridEditMode.InPlace;
                RadGridCorporateCreditCardDetail.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                // hidden delete button
                RadGridCorporateCreditCardDetail.MasterTableView.Columns[RadGridCorporateCreditCardDetail.MasterTableView.Columns.Count - 1].Visible = false;
            }
        }

        protected void RadGridCorporateCreditCardDetail_Load(object sender, EventArgs e)
        {
            LinqDataSourceCorporateCreditCardDetail.WhereParameters.Clear();
            LinqDataSourceCorporateCreditCardDetail.WhereParameters.Add("CorporateCreditCardId", DbType.Int32, Id.ToString());
            LinqDataSourceCorporateCreditCardDetail.Where = "CorporateCreditCardId == @CorporateCreditCardId";
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            // Save
            if (e.Item.Text == "TempSave" || e.Item.Text == "Request")
            {
                if (IsValid)
                {
                    var corporateCreditCardSchema = new CCorporateCreditCardSchema().GetByUserId(CurrentUserId);
                    if (corporateCreditCardSchema != null)
                    {
                        var corporateCreditCard = new CCorporateCreditCard().GetByUserId(CurrentUserId);
                        if (corporateCreditCard != null)
                        {
                            var startDay = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 4);
                            var endDay = startDay.AddMonths(1);
                            if (corporateCreditCard.PeriodStart >= startDay && corporateCreditCard.PeriodEnd < endDay)
                            {
                                ShowMessage("Credit on this month is already requested. Try it again when 4th beginning of month.");
                                return;
                            }
                        }

                        if (RadNumericTextBoxGrandTotal.Value > (double)corporateCreditCardSchema.CreditLimit)
                        {
                            ShowMessage("Your limited Credit Amount of  is " + corporateCreditCardSchema.CreditLimit);
                            return;
                        }

                        var cObj = new CCorporateCreditCard();
                        var obj = cObj.Get(Id);

                        // new one
                        if (obj == null)
                        {
                            obj = new Erp2016.Lib.CorporateCreditCard();
                            obj.CreatedId = Convert.ToInt32(CurrentUserId);
                            obj.CreatedDate = DateTime.Now;
                            obj.PeriodStart = (DateTime)RadDatePickerStart.SelectedDate;
                            obj.PeriodEnd = (DateTime)RadDatePickerEnd.SelectedDate;
                            int newIndex = Convert.ToInt32(cObj.Add(obj).ToString());
                            obj = cObj.Get(newIndex);
                            ViewState["NewIndex"] = newIndex;
                        }
                        else
                        {
                            obj.PeriodStart = (DateTime)RadDatePickerStart.SelectedDate;
                            obj.PeriodEnd = (DateTime)RadDatePickerEnd.SelectedDate;
                            obj.UpdatedId = Convert.ToInt32(CurrentUserId);
                            obj.UpdatedDate = DateTime.Now;
                            ViewState["NewIndex"] = obj.CorporateCreditCardId.ToString();
                        }

                        obj.ApprovalId = CurrentUserId;
                        obj.ApprovalDate = DateTime.Now;

                        if (e.Item.Text == "TempSave")
                            obj.ApprovalStatus = null;
                        else
                        {
                            var cApprovalHistory = new CApprovalHistory();
                            cApprovalHistory.DelApprovalHistory((int)CConstValue.Approval.CorporateCreditCard, Convert.ToInt32(ViewState["NewIndex"]));

                            // approve request 
                            var approval = new CApproval();
                            var approvalResult = approval.ApproveRequstCreate((int)CConstValue.Approval.CorporateCreditCard, CurrentUserId, Convert.ToInt32(ViewState["NewIndex"]));
                            if (approvalResult > 0)
                            {
                                obj.ApprovalStatus = approvalResult;
                            }
                            else
                            {
                                ShowMessage("Failed");
                                return;
                            }

                            new CMail().SendMail(CConstValue.Approval.CorporateCreditCard, CConstValue.MailStatus.ToApproveUser, Convert.ToInt32(ViewState["NewIndex"]), string.Empty, CurrentUserId);
                        }



                        cObj.Update(obj);

                        // save uploading file
                        FileDownloadList1.SaveFile(Convert.ToInt32(ViewState["NewIndex"]));

                        // save other tables
                        RunClientScript("SaveChanges();");

                    }
                    else
                        ShowMessage("not found Limit credit amount. It needs to be registered.");

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

        protected void RadGridCorporateCreditCardDetail_OnBatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {
            foreach (var command in e.Commands)
            {
                if (command.Type.ToString() != "Cancel")
                {
                    command.NewValues["CorporateCreditCardId"] = Convert.ToInt32(ViewState["NewIndex"]);
                    command.NewValues["Date"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["Date"]))) ? new DateTime(9999, 12, 31) : command.NewValues["Date"];
                    command.NewValues["SiteLocationId"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["SiteLocationId"]))) ? CurrentSiteLocationId : Convert.ToInt32(command.NewValues["SiteLocationId"]);
                    command.NewValues["Web"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["Web"]))) ? 0 : Convert.ToDecimal(command.NewValues["Web"]);
                    command.NewValues["Ground"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["Ground"]))) ? 0 : Convert.ToDecimal(command.NewValues["Ground"]);
                    command.NewValues["Office"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["Office"]))) ? 0 : Convert.ToDecimal(command.NewValues["Office"]);
                    command.NewValues["Meals"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["Meals"]))) ? 0 : Convert.ToDecimal(command.NewValues["Meals"]);
                    command.NewValues["Promotion"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["Promotion"]))) ? 0 : Convert.ToDecimal(command.NewValues["Promotion"]);
                    command.NewValues["Membership"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["Membership"]))) ? 0 : Convert.ToDecimal(command.NewValues["Mail"]);
                    command.NewValues["Mail"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["Mail"]))) ? 0 : Convert.ToDecimal(command.NewValues["Mail"]);
                    command.NewValues["Legal"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["Legal"]))) ? 0 : Convert.ToDecimal(command.NewValues["Legal"]);
                    command.NewValues["Student"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["Student"]))) ? 0 : Convert.ToDecimal(command.NewValues["Student"]);
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
                LinqDataSourceApprovalList.WhereParameters.Add("ApproveType", DbType.Int32, ((int)CConstValue.Approval.CorporateCreditCard).ToString());
                LinqDataSourceApprovalList.Where = "MenuSeqId == @MenuSeqId AND ApproveType == @ApproveType";
            }
        }
    }
}