using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.OfficeAdmin
{
    public partial class PurchaseOrderPop : PageBase
    {
        private int Id { get; set; }

        public PurchaseOrderPop() : base((int)CConstValue.Menu.PurchaseOrder)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Id = Convert.ToInt32(Request["id"]);

            if (!IsPostBack)
            {
                foreach (GridColumn v in RadGridPurchaseOrderDetail.Columns)
                {
                    if (v.GetType() == typeof(GridTemplateColumn))
                    {
                        var column = (GridTemplateColumn)v;
                        switch (column.UniqueName)
                        {
                            case "Quantity":
                                column.DefaultInsertValue = "1";
                                break;
                        }
                    }
                }

                var scriptManager = (RadScriptManager)Page.Master.FindControl("RadScriptManager1");
                //scriptManager.Scripts.Add(new ScriptReference() { Path = ResolveUrl("~/assets/js/jquery.printArea.js") });
                //scriptManager.RegisterPostBackControl(RadButtonFileDownload);
                FileDownloadList1.InitFileDownloadList((int)CConstValue.Upload.PurchaseOrder);

                var global = new CGlobal();
                RadComboBoxType.DataSource = global.GetDictionary(1466);
                RadComboBoxType.DataTextField = "Name";
                RadComboBoxType.DataValueField = "Value";
                RadComboBoxType.DataBind();

                RadComboBoxPriority.DataSource = global.GetDictionary(1470);
                RadComboBoxPriority.DataTextField = "Name";
                RadComboBoxPriority.DataValueField = "Value";
                RadComboBoxPriority.DataBind();

                RadComboBoxShippingMethod.DataSource = global.GetDictionary(1471);
                RadComboBoxShippingMethod.DataTextField = "Name";
                RadComboBoxShippingMethod.DataValueField = "Value";
                RadComboBoxShippingMethod.DataBind();

                RadComboBoxReviewType.DataSource = global.GetDictionary(1489);
                RadComboBoxReviewType.DataTextField = "Name";
                RadComboBoxReviewType.DataValueField = "Value";
                RadComboBoxReviewType.DataBind();
                RadComboBoxReviewType.Items.Add(new RadComboBoxItem("N/A", null));
                RadComboBoxReviewType.SelectedIndex = RadComboBoxReviewType.Items.Count - 1;

                var obj = new CPurchaseOrder();
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
                    obj = new CPurchaseOrder(Id);

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
                            var supervisor = refundApproveInfo.CheckApprovalEnable((int)CConstValue.Approval.PurchaseOrder, Convert.ToInt32(Id));

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
                    // Purchase Order from HQ
                    else if (requestOrApprovalType == "2")
                    {
                        // Wating for review from HQ
                        if (approvalType == ((int)CConstValue.ApprovalStatus.WaitingForPreviewFromHq).ToString())
                        {
                            buttonList.Add("Accept");
                            buttonList.Add("Reject");
                            buttonList.Add("Close");
                        }
                        // Approved
                        else if (approvalType == ((int) CConstValue.ApprovalStatus.Approved).ToString())
                        {
                            buttonList.Add("Print");
                            buttonList.Add("Accept");
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

                if (approvalType != ((int)CConstValue.ApprovalStatus.WaitingForPreviewFromHq).ToString() &&
                    approvalType != ((int)CConstValue.ApprovalStatus.Approved).ToString())
                    RunClientScript("HideReview();");

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

                // New
                if (Request["createOrListType"] == "0")
                {
                    var cUser = new CUser();
                    var user = cUser.Get(CurrentUserId);
                    if (user != null)
                    {
                        RadTextBoxShipToName.Text = cUser.GetUserName(user);
                        RadTextBoxShipToAddress.Text = user.Address1 + " " + user.Address2;
                        RadTextBoxShipToCity.Text = user.City;
                        RadTextBoxShipToProvince.Text = user.Province;
                        RadTextBoxShipToPostalCode.Text = user.PostalCode;
                        RadTextBoxShipToPhone.Text = user.CellPhone;
                        RadTextBoxShipToEmail.Text = user.Email;
                    }

                    // Init
                    RadNumericTextBoxShippingTerms.Value = 1;
                    RadDatePickerDeliveryDate.SelectedDate = DateTime.Now;
                }
                else {
                    var result = obj.Get(Id);
                    if (result != null)
                    {
                        RadComboBoxType.SelectedIndex = result.PurchaseOrderType;
                        RadComboBoxPriority.SelectedIndex = (int)result.PriorityType;
                        RadComboBoxShippingMethod.SelectedIndex = (int)result.ShippingMethodType;
                        RadNumericTextBoxShippingTerms.Value = result.ShippingTerms;
                        RadDatePickerDeliveryDate.SelectedDate = result.ShippingDeliveryDate;
                        RadTextBoxDescription.Text = result.Description;

                        RadTextBoxVendorName.Text = result.VendorName;
                        RadTextBoxVendorAddress.Text = result.VendorAddress;
                        RadTextBoxVendorCity.Text = result.VendorCity;
                        RadTextBoxVendorProvince.Text = result.VendorProvince;
                        RadTextBoxVendorPostalCode.Text = result.VendorPostalCode;
                        RadTextBoxVendorPhone.Text = result.VendorPhone;
                        RadTextBoxVendorEmail.Text = result.VendorEmail;

                        RadTextBoxShipToName.Text = result.ShipToName;
                        RadTextBoxShipToAddress.Text = result.ShipToAddress;
                        RadTextBoxShipToCity.Text = result.ShipToCity;
                        RadTextBoxShipToProvince.Text = result.ShipToProvince;
                        RadTextBoxShipToPostalCode.Text = result.ShipToPostalCode;
                        RadTextBoxShipToPhone.Text = result.ShipToPhone;
                        RadTextBoxShipToEmail.Text = result.ShipToEmail;

                        if (result.ReviewType != null)
                            RadComboBoxReviewType.SelectedIndex = (int)result.ReviewType;
                        if (result.ReviewDate != null)
                            RadDatePickerReviewDate.SelectedDate = result.ReviewDate;
                        if (result.ReviewUserId != null)
                        {
                            var cUser = new CUser();
                            var user = cUser.Get((int)result.ReviewUserId);
                            RadTextBoxReviewUser.Text = cUser.GetUserName(user);
                        }
                        RadDatePickerReviewDate.SelectedDate = result.ReviewDate;
                        RadTextBoxReviewMemo.Text = result.ReviewMemo;
                        RadNumericTextBoxTax.Value = (double)result.Tax;
                    }
                }

            }
        }

        private void SetVisibleItems(bool isActive)
        {
            if (isActive == false)
            {
                RadComboBoxType.Enabled = false;
                RadComboBoxPriority.Enabled = false;
                RadComboBoxShippingMethod.Enabled = false;
                RadNumericTextBoxShippingTerms.Enabled = false;
                RadDatePickerDeliveryDate.Enabled = false;
                RadTextBoxDescription.Enabled = false;

                RadTextBoxVendorName.Enabled = false;
                RadTextBoxVendorAddress.Enabled = false;
                RadTextBoxVendorCity.Enabled = false;
                RadTextBoxVendorProvince.Enabled = false;
                RadTextBoxVendorPostalCode.Enabled = false;
                RadTextBoxVendorPhone.Enabled = false;
                RadTextBoxVendorEmail.Enabled = false;

                RadTextBoxShipToName.Enabled = false;
                RadTextBoxShipToAddress.Enabled = false;
                RadTextBoxShipToCity.Enabled = false;
                RadTextBoxShipToProvince.Enabled = false;
                RadTextBoxShipToPostalCode.Enabled = false;
                RadTextBoxShipToPhone.Enabled = false;
                RadTextBoxShipToEmail.Enabled = false;

                RadNumericTextBoxTax.ReadOnly = true;

                FileDownloadList1.SetVisibieUploadControls(false);

                RadGridPurchaseOrderDetail.AllowAutomaticInserts = false;
                RadGridPurchaseOrderDetail.AllowAutomaticUpdates = false;
                RadGridPurchaseOrderDetail.AllowAutomaticDeletes = false;
                RadGridPurchaseOrderDetail.MasterTableView.EditMode = GridEditMode.InPlace;
                RadGridPurchaseOrderDetail.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                // hidden delete button
                RadGridPurchaseOrderDetail.MasterTableView.Columns[RadGridPurchaseOrderDetail.MasterTableView.Columns.Count - 1].Visible = false;
            }
        }

        protected void RadGridPurchaseOrderDetail_Load(object sender, EventArgs e)
        {
            LinqDataSourcePurchaseOrderDetail.WhereParameters.Clear();
            LinqDataSourcePurchaseOrderDetail.WhereParameters.Add("PurchaseOrderId", DbType.Int32, Id.ToString());
            LinqDataSourcePurchaseOrderDetail.Where = "PurchaseOrderId == @PurchaseOrderId";
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            // Save
            if (e.Item.Text == "TempSave" || e.Item.Text == "Request")
            {
                if (IsValid)
                {
                    var cObj = new CPurchaseOrder();
                    var obj = cObj.Get(Id);

                    // new one
                    if (obj == null)
                    {
                        obj = new Erp2016.Lib.PurchaseOrder();
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
                        ViewState["NewIndex"] = obj.PurchaseOrderId.ToString();
                    }

                    obj.ApprovalId = CurrentUserId;
                    obj.ApprovalDate = DateTime.Now;

                    if (e.Item.Text == "TempSave")
                        obj.ApprovalStatus = null;
                    else
                    {
                        var cApprovalHistory = new CApprovalHistory();
                        cApprovalHistory.DelApprovalHistory((int)CConstValue.Approval.PurchaseOrder, Convert.ToInt32(ViewState["NewIndex"]));

                        // approve request 
                        var approval = new CApproval();
                        var approvalResult = approval.ApproveRequstCreate((int)CConstValue.Approval.PurchaseOrder, CurrentUserId, Convert.ToInt32(ViewState["NewIndex"]));
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
                        new CMail().SendMail(CConstValue.Approval.PurchaseOrder, CConstValue.MailStatus.ToApproveUser, Convert.ToInt32(ViewState["NewIndex"]), string.Empty, CurrentUserId);
                    }

                    obj.PurchaseOrderType = RadComboBoxType.SelectedIndex;
                    obj.PriorityType = RadComboBoxPriority.SelectedIndex;
                    obj.ShippingMethodType = RadComboBoxShippingMethod.SelectedIndex;
                    obj.ShippingTerms = (int)RadNumericTextBoxShippingTerms.Value;
                    obj.ShippingDeliveryDate = RadDatePickerDeliveryDate.SelectedDate.Value;
                    obj.Description = RadTextBoxDescription.Text;

                    obj.VendorName = RadTextBoxVendorName.Text;
                    obj.VendorAddress = RadTextBoxVendorAddress.Text;
                    obj.VendorCity = RadTextBoxVendorCity.Text;
                    obj.VendorProvince = RadTextBoxVendorProvince.Text;
                    obj.VendorPostalCode = RadTextBoxVendorPostalCode.Text;
                    obj.VendorPhone = RadTextBoxVendorPhone.Text;
                    obj.VendorEmail = RadTextBoxVendorEmail.Text;

                    obj.ShipToName = RadTextBoxShipToName.Text;
                    obj.ShipToAddress = RadTextBoxShipToAddress.Text;
                    obj.ShipToCity = RadTextBoxShipToCity.Text;
                    obj.ShipToProvince = RadTextBoxShipToProvince.Text;
                    obj.ShipToPostalCode = RadTextBoxShipToPostalCode.Text;
                    obj.ShipToPhone = RadTextBoxShipToPhone.Text;
                    obj.ShipToEmail = RadTextBoxShipToEmail.Text;

                    obj.Tax = (decimal)RadNumericTextBoxTax.Value;

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
                var cObj = new CPurchaseOrder();
                var obj = cObj.Get(Id);
                if (obj != null)
                {
                    if (!string.IsNullOrEmpty(RadComboBoxReviewType.SelectedValue))
                        obj.ReviewType = Convert.ToInt32(RadComboBoxReviewType.SelectedValue);
                    obj.ReviewMemo = RadTextBoxReviewMemo.Text;
                    obj.ReviewUserId = CurrentUserId;
                    obj.ReviewDate = DateTime.Now;
                    obj.UpdatedId = CurrentUserId;
                    obj.UpdatedDate = DateTime.Now;
                    cObj.Update(obj);
                    //RunClientScript("Close();");

                    RunClientScript("ShowApprovalAcceptWindow('" + Id + "');");
                }
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

        protected void RadGridPurchaseOrderDetail_OnBatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {
            foreach (var command in e.Commands)
            {
                if (command.Type.ToString() != "Cancel")
                {
                    command.NewValues["PurchaseOrderId"] = Convert.ToInt32(ViewState["NewIndex"]);

                    command.NewValues["Quantity"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["Quantity"]))) ? 1 : Convert.ToInt32(command.NewValues["Quantity"]);
                    command.NewValues["Price"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["Price"]))) ? 0 : Convert.ToDecimal(command.NewValues["Price"]);

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
                LinqDataSourceApprovalList.WhereParameters.Add("ApproveType", DbType.Int32, ((int)CConstValue.Approval.PurchaseOrder).ToString());
                LinqDataSourceApprovalList.Where = "MenuSeqId == @MenuSeqId AND ApproveType == @ApproveType";
            }
        }
    }
}