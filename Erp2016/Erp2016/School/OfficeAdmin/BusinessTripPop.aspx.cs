using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.OfficeAdmin
{
    public partial class BusinessTripPop : PageBase
    {
        private int Id { get; set; }

        public BusinessTripPop() : base((int)CConstValue.Menu.BusinessTrip)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Id = Convert.ToInt32(Request["id"]);

            if (!IsPostBack)
            {
                foreach (GridColumn v in RadGridFlight.Columns)
                {
                    if (v.GetType() == typeof(GridTemplateColumn))
                    {
                        var column = (GridTemplateColumn)v;
                        switch (column.UniqueName)
                        {
                            case "AirDate":
                                column.DefaultInsertValue = DateTime.Today.ToString("MM-dd-yyyy");
                                break;
                            case "Departure":
                                column.DefaultInsertValue = "12:00 AM";
                                break;
                        }
                    }
                }

                foreach (GridColumn v in RadGridAccommodation.Columns)
                {
                    if (v.GetType() == typeof(GridTemplateColumn))
                    {
                        var column = (GridTemplateColumn)v;
                        switch (column.UniqueName)
                        {
                            case "AccomIn":
                                column.DefaultInsertValue = DateTime.Today.ToString("MM-dd-yyyy");
                                break;
                            case "AccomOut":
                                column.DefaultInsertValue = DateTime.Today.ToString("MM-dd-yyyy");
                                break;
                        }
                    }
                }

                foreach (GridColumn v in RadGridCash.Columns)
                {
                    if (v.GetType() == typeof(GridTemplateColumn))
                    {
                        var column = (GridTemplateColumn)v;
                        switch (column.UniqueName)
                        {
                            case "CashDate":
                                column.DefaultInsertValue = DateTime.Today.ToString("MM-dd-yyyy");
                                break;
                            case "CashTime":
                                column.DefaultInsertValue = "12:00 AM";
                                break;
                        }
                    }
                }

                var scriptManager = (RadScriptManager)Page.Master.FindControl("RadScriptManager1");
                //scriptManager.Scripts.Add(new ScriptReference() { Path = ResolveUrl("~/assets/js/jquery.printArea.js") });
                //scriptManager.RegisterPostBackControl(RadButtonFileDownload);
                FileDownloadList1.InitFileDownloadList((int)CConstValue.Upload.BusinessTrip);

                var obj = new CBusinessTrip();
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
                    obj = new CBusinessTrip(Id);

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
                            var supervisor = refundApproveInfo.CheckApprovalEnable((int)CConstValue.Approval.BusinessTrip, Convert.ToInt32(Id));

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
                if (approvalType == "0" || approvalType == string.Empty)
                {
                    FileDownloadList1.SetVisibieUploadControls(true);
                }
                else
                {
                    FileDownloadList1.SetVisibieUploadControls(false);
                }

                // radio
                if (obj.TypeOfTrip != null)
                {
                    if (obj.TypeOfTrip.ToLower() == "local")
                    {
                        RadButtonOverseas.Checked = false;
                        RadButtonLocal.Checked = true;
                    }
                    else
                    {
                        RadButtonLocal.Checked = false;
                        RadButtonOverseas.Checked = true;
                    }
                }

                var dt = new DataTable();
                dt.Columns.Add("DocNo");
                dt.Columns.Add("Site");
                dt.Columns.Add("Location");
                dt.Columns.Add("Name");
                dt.Columns.Add("Date");
                var newDr = dt.NewRow();
                newDr["DocNo"] = obj.DocNo.ToString();
                newDr["Site"] = obj.Site;
                newDr["Location"] = obj.Location;
                newDr["Name"] = obj.Name1;
                newDr["Date"] = obj.Name2;
                dt.Rows.Add(newDr);

                RadGridInfo.DataSource = dt;
            }
        }

        private void SetVisibleItems(bool isActive)
        {
            if (isActive == false)
            {
                RadButtonLocal.ReadOnly = true;
                RadButtonOverseas.ReadOnly = true;
                FileDownloadList1.SetVisibieUploadControls(false);


                RadGridFlight.AllowAutomaticInserts = false;
                RadGridFlight.AllowAutomaticUpdates = false;
                RadGridFlight.AllowAutomaticDeletes = false;
                RadGridFlight.MasterTableView.EditMode = GridEditMode.InPlace;
                RadGridFlight.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                // hidden delete button
                RadGridFlight.MasterTableView.Columns[RadGridFlight.MasterTableView.Columns.Count - 1].Visible = false;

                RadGridAccommodation.AllowAutomaticInserts = false;
                RadGridAccommodation.AllowAutomaticUpdates = false;
                RadGridAccommodation.AllowAutomaticDeletes = false;
                RadGridAccommodation.MasterTableView.EditMode = GridEditMode.InPlace;
                RadGridAccommodation.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                // hidden delete button
                RadGridAccommodation.MasterTableView.Columns[RadGridAccommodation.MasterTableView.Columns.Count - 1].Visible = false;

                RadGridCash.AllowAutomaticInserts = false;
                RadGridCash.AllowAutomaticUpdates = false;
                RadGridCash.AllowAutomaticDeletes = false;
                RadGridCash.MasterTableView.EditMode = GridEditMode.InPlace;
                RadGridCash.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                // hidden delete button
                RadGridCash.MasterTableView.Columns[RadGridCash.MasterTableView.Columns.Count - 1].Visible = false;
            }
        }

        protected void RadGridFlight_Load(object sender, EventArgs e)
        {
            LinqDataSourceFlight.WhereParameters.Clear();
            LinqDataSourceFlight.WhereParameters.Add("BusinessTripId", DbType.Int32, Id.ToString());
            LinqDataSourceFlight.Where = "BusinessTripId == @BusinessTripId";
        }

        protected void RadGridAccommodation_Load(object sender, EventArgs e)
        {
            LinqDataSourceAccommodation.WhereParameters.Clear();
            LinqDataSourceAccommodation.WhereParameters.Add("BusinessTripId", DbType.Int32, Id.ToString());
            LinqDataSourceAccommodation.Where = "BusinessTripId == @BusinessTripId";
        }

        protected void RadGridCash_Load(object sender, EventArgs e)
        {
            LinqDataSourceCash.WhereParameters.Clear();
            LinqDataSourceCash.WhereParameters.Add("BusinessTripId", DbType.Int32, Id.ToString());
            LinqDataSourceCash.Where = "BusinessTripId == @BusinessTripId";
        }

        private RadNumericTextBox GetAirRate()
        {
            var footer = (GridFooterItem)RadGridFlight.MasterTableView.GetItems(GridItemType.Footer)[0];
            return (RadNumericTextBox)footer["AirRate"].FindControl("LabelFlightRate2");
        }

        private RadNumericTextBox GetAccomRate()
        {
            var footer = (GridFooterItem)RadGridAccommodation.MasterTableView.GetItems(GridItemType.Footer)[0];
            return (RadNumericTextBox)footer["AccomRate"].FindControl("LabelAccomRate2");
        }

        private RadNumericTextBox GetMealsRate()
        {
            var footer = (GridFooterItem)RadGridCash.MasterTableView.GetItems(GridItemType.Footer)[0];
            return (RadNumericTextBox)footer["MealsRate"].FindControl("LabelCashMealsRate2");
        }

        private RadNumericTextBox GetGroundRate()
        {
            var footer = (GridFooterItem)RadGridCash.MasterTableView.GetItems(GridItemType.Footer)[0];
            return (RadNumericTextBox)footer["GroundRate"].FindControl("LabelCashGroundRate2");
        }

        private RadNumericTextBox GetCashAdvanceTotal()
        {
            var footer = (GridFooterItem)RadGridCash.MasterTableView.GetItems(GridItemType.Footer)[0];
            return (RadNumericTextBox)footer["AccomAgency"].FindControl("LabelCashAdvanceTotal");
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            // Save
            if (e.Item.Text == "TempSave" || e.Item.Text == "Request")
            {
                if (IsValid)
                {
                    var cObj = new CBusinessTrip();
                    var obj = cObj.Get(Id);

                    // new one
                    if (obj == null)
                    {
                        obj = new Erp2016.Lib.BusinessTrip();
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
                        ViewState["NewIndex"] = obj.BusinessTripId.ToString();
                    }

                    obj.ApprovalId = CurrentUserId;
                    obj.ApprovalDate = DateTime.Now;

                    if (e.Item.Text == "TempSave")
                        obj.ApprovalStatus = null;
                    else
                    {
                        var cApprovalHistory = new CApprovalHistory();
                        cApprovalHistory.DelApprovalHistory((int)CConstValue.Approval.BusinessTrip, Convert.ToInt32(ViewState["NewIndex"]));

                        // approve request 
                        var approval = new CApproval();
                        var approvalResult = approval.ApproveRequstCreate((int)CConstValue.Approval.BusinessTrip, CurrentUserId, Convert.ToInt32(ViewState["NewIndex"]));
                        if (approvalResult > 0)
                        {
                            obj.ApprovalStatus = approvalResult;
                        }
                        else
                        {
                            ShowMessage("Failed");
                            return;
                        }

                        // mail
                        new CMail().SendMail(CConstValue.Approval.BusinessTrip, CConstValue.MailStatus.ToApproveUser, Convert.ToInt32(ViewState["NewIndex"]), string.Empty, CurrentUserId);
                    }

                    obj.Type = RadButtonLocal.Checked ? RadButtonLocal.Text : RadButtonOverseas.Text;
                    obj.AirSub = Convert.ToDecimal(GetAirRate().Value);
                    obj.AccomSub = Convert.ToDecimal(GetAccomRate().Value);
                    obj.GroundSub = Convert.ToDecimal(GetGroundRate().Value);
                    obj.MealsSub = Convert.ToDecimal(GetMealsRate().Value);
                    obj.CashSub = Convert.ToDecimal(GetCashAdvanceTotal().Value);
                    obj.GrandSub = Convert.ToDecimal(RadNumericTextBoxGrandTotal.Value);

                    cObj.Update(obj);

                    // save uploading file
                    FileDownloadList1.SaveFile(Convert.ToInt32(ViewState["NewIndex"]));

                    // save other tables
                    RunClientScript("SaveChanges();");
                }
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
            // Cancel
            else if (e.Item.Text == "Cancel")
            {
                RunClientScript("ShowApprovalCancelWindow('" + Id + "');");
            }
            // Print
            else if (e.Item.Text == "Print")
            {
                RunClientScript("ShowPrint();");
            }
            // close
            else if (e.Item.Text == "Close")
            {
                RunClientScript("Close();");
            }
        }

        protected void RadGridFlight_OnBatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {
            foreach (var command in e.Commands)
            {
                if (command.Type.ToString() != "Cancel")
                {
                    command.NewValues["BusinessTripId"] = Convert.ToInt32(ViewState["NewIndex"]);
                    command.NewValues["AirDate"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["AirDate"]))) ? new DateTime(9999, 12, 31) : command.NewValues["AirDate"];
                    command.NewValues["AirRate"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["AirRate"]))) ? 0 : Convert.ToDecimal(command.NewValues["AirRate"]);

                    command.NewValues["CreatedId"] = CurrentUserId;
                    command.NewValues["CreatedDate"] = DateTime.Now;
                }
            }
        }

        protected void RadGridAccommodation_OnBatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {
            foreach (var command in e.Commands)
            {
                if (command.Type.ToString() != "Cancel")
                {
                    command.NewValues["BusinessTripId"] = Convert.ToInt32(ViewState["NewIndex"]);
                    command.NewValues["AccomIn"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["AccomIn"]))) ? new DateTime(9999, 12, 31) : command.NewValues["AccomIn"];
                    command.NewValues["AccomOut"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["AccomOut"]))) ? new DateTime(9999, 12, 31) : command.NewValues["AccomOut"];
                    command.NewValues["AccomRate"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["AccomRate"]))) ? 0 : Convert.ToDecimal(command.NewValues["AccomRate"]);

                    command.NewValues["CreatedId"] = CurrentUserId;
                    command.NewValues["CreatedDate"] = DateTime.Now;
                }
            }
        }

        protected void RadGridCash_OnBatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {
            foreach (var command in e.Commands)
            {
                if (command.Type.ToString() != "Cancel")
                {
                    command.NewValues["BusinessTripId"] = Convert.ToInt32(ViewState["NewIndex"]);
                    command.NewValues["CashDate"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["CashDate"]))) ? new DateTime(9999, 12, 31) : command.NewValues["CashDate"];
                    command.NewValues["GroundRate"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["GroundRate"]))) ? 0 : Convert.ToDecimal(command.NewValues["GroundRate"]);
                    command.NewValues["MealsRate"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["MealsRate"]))) ? 0 : Convert.ToDecimal(command.NewValues["MealsRate"]);

                    command.NewValues["CreatedId"] = CurrentUserId;
                    command.NewValues["CreatedDate"] = DateTime.Now;
                }
            }
        }

        //protected void ApprovalListView_OnLoad(object sender, EventArgs e)
        //{
        //    //ApprovalLine1.FindControl()
        //    //LinqDataSourceApprovalList.WhereParameters.Clear();
        //    //LinqDataSourceApprovalList.WhereParameters.Add("MenuSeqId", DbType.Int32, Id.ToString());
        //    //LinqDataSourceApprovalList.WhereParameters.Add("ApproveType", DbType.Int32, ((int)CConstValue.Approval.BUSINESS_TRIP).ToString());
        //    //LinqDataSourceApprovalList.Where = "MenuSeqId == @MenuSeqId AND ApproveType == @ApproveType";
        //}


        protected void ApprovalLine1_OnLoad(object sender, EventArgs e)
        {
            LinqDataSource LinqDataSourceApprovalList = ApprovalLine1.GetSqlDataSourceApprovalList();
            if (LinqDataSourceApprovalList != null)
            {
                LinqDataSourceApprovalList.WhereParameters.Clear();
                LinqDataSourceApprovalList.WhereParameters.Add("MenuSeqId", DbType.Int32, Id.ToString());
                LinqDataSourceApprovalList.WhereParameters.Add("ApproveType", DbType.Int32, ((int)CConstValue.Approval.BusinessTrip).ToString());
                LinqDataSourceApprovalList.Where = "MenuSeqId == @MenuSeqId AND ApproveType == @ApproveType";
            }
        }

    }
}