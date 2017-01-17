using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;
using Convert = System.Convert;

namespace School.Sales
{
    public partial class Invoice : PageBase //System.Web.UI.Page
    {
        private LinqDataSource _sqlDataSourceInvoiceItems;
        private RadGrid _radGridInvoiceItems;

        public Invoice() : base((int)CConstValue.Menu.Invoice)
        {
        }

        public void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //var scriptManager = (RadScriptManager)Page.Master.FindControl("RadScriptManager1");
                //scriptManager.RegisterPostBackControl(RadToolBar1.FindItemByText("Excel"));
                if (CurrentGroupId == (int)CConstValue.UserGroupForAccountExcelExport.Accounting ||
                    CurrentGroupId == (int)CConstValue.UserGroupForAccountExcelExport.IT)
                {
                    RadToolBar1.FindItemByText("Excel").Visible = true;
                    RadToolBar1.FindItemByText("ExcelDetail").Visible = true;
                }
            }

            // find user control
            _sqlDataSourceInvoiceItems = InvoiceItemGrid1.GetSqlDataSourceInvoiceItems();
            _radGridInvoiceItems = InvoiceItemGrid1.GetRadGridInvoiceItems();
            // connect event of invoice Items.
            _radGridInvoiceItems.PreRender += _radGridInvoiceItems_PreRender;

            InvoiceListSearch();
        }

        public void GetInvoiceItems()
        {
            var btnConfirm = RadToolBar1.FindItemByText("Confirm");
            var btnModify = RadToolBar1.FindItemByText("Modify");
            var btnCancel = RadToolBar1.FindItemByText("Cancel");
            var btnStudentInvoice = RadToolBar1.FindItemByText("Student Invoice");
            var btnAgencyInvoice = RadToolBar1.FindItemByText("Agency Invoice");
            var btnNewSimpleInvoice = RadToolBar1.FindItemByText("New Simple Invoice");

            if (RadGridInvoice.SelectedValue == null)
            {
                _sqlDataSourceInvoiceItems.WhereParameters.Clear();
                _sqlDataSourceInvoiceItems.WhereParameters.Add("InvoiceId", DbType.Int32, "0");
                _sqlDataSourceInvoiceItems.Where = "InvoiceId == @InvoiceId";

                LinqDataSourceInvoiceHistory.WhereParameters.Clear();
                LinqDataSourceInvoiceHistory.WhereParameters.Add("InvoiceId", DbType.Int32, "0");
                LinqDataSourceInvoiceHistory.Where = "InvoiceId == @InvoiceId";
            }
            else
            {
                var cInvoice = new CInvoice();
                var invoice = cInvoice.Get(Convert.ToInt32(RadGridInvoice.SelectedValue.ToString()));

                _sqlDataSourceInvoiceItems.WhereParameters.Clear();
                _sqlDataSourceInvoiceItems.WhereParameters.Add("InvoiceId", DbType.Int32, RadGridInvoice.SelectedValue.ToString());
                _sqlDataSourceInvoiceItems.Where = "InvoiceId == @InvoiceId";

                if (invoice.OriginalInvoiceId != null)
                {
                    LinqDataSourceInvoiceHistory.WhereParameters.Clear();
                    LinqDataSourceInvoiceHistory.WhereParameters.Add("InvoiceId", DbType.Int32, invoice.InvoiceId.ToString());
                    LinqDataSourceInvoiceHistory.WhereParameters.Add("InvoiceNumber", DbType.String, invoice.InvoiceNumber.Substring(2, 8));
                    LinqDataSourceInvoiceHistory.WhereParameters.Add("InvoicePartialIndex", DbType.Int32, invoice.InvoicePartialIndex.ToString());
                    LinqDataSourceInvoiceHistory.Where = "InvoiceId != @InvoiceId && InvoiceNumber.Contains(@InvoiceNumber) && InvoicePartialIndex < @InvoicePartialIndex";
                }
                else
                {
                    LinqDataSourceInvoiceHistory.WhereParameters.Clear();
                    LinqDataSourceInvoiceHistory.WhereParameters.Add("OriginalInvoiceId", DbType.Int32, "0");
                    LinqDataSourceInvoiceHistory.Where = "InvoiceId == @OriginalInvoiceId";
                }

                ddlFG.SelectedValue = invoice.IsFinancialGurantee.ToString();

                var status = Convert.ToInt32(invoice.Status);
                var invoiceType = Convert.ToInt32(invoice.InvoiceType);
                var delete = _radGridInvoiceItems.MasterTableView.GetColumn("DeleteColumn");
                if (status == (int)CConstValue.InvoiceStatus.Pending)
                {
                    switch (invoiceType)
                    {
                        case (int)CConstValue.InvoiceType.Simple:
                        case (int)CConstValue.InvoiceType.General:
                        case (int)CConstValue.InvoiceType.Manual:
                        case (int)CConstValue.InvoiceType.Dormitory:
                        case (int)CConstValue.InvoiceType.Homestay:
                            btnConfirm.Enabled = true;
                            btnModify.Enabled = false;
                            btnCancel.Enabled = true;
                            btnStudentInvoice.Enabled = true;
                            btnAgencyInvoice.Enabled = true;
                            _radGridInvoiceItems.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
                            _radGridInvoiceItems.MasterTableView.EditMode = GridEditMode.Batch;
                            delete.Visible = true;
                            ddlFG.Enabled = true;
                            break;
                        default:
                            btnConfirm.Enabled = false; //Confirm
                            btnModify.Enabled = false; //Modify
                            btnCancel.Enabled = false; //Cancel
                            btnStudentInvoice.Enabled = true; //Student Invoice Print
                            btnAgencyInvoice.Enabled = true; //Agency Invoice Print
                            _radGridInvoiceItems.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                            _radGridInvoiceItems.MasterTableView.EditMode = GridEditMode.InPlace;
                            delete.Visible = false;
                            ddlFG.Enabled = false;
                            break;

                    }
                }
                else if (status == (int)CConstValue.InvoiceStatus.Invoiced) //Invoiced
                {
                    switch (invoiceType)
                    {
                        case (int)CConstValue.InvoiceType.Simple:
                        case (int)CConstValue.InvoiceType.General:
                        case (int)CConstValue.InvoiceType.Manual:
                        case (int)CConstValue.InvoiceType.Dormitory:
                        case (int)CConstValue.InvoiceType.Homestay:
                            btnConfirm.Enabled = false;
                            btnModify.Enabled = true;
                            btnCancel.Enabled = true;
                            btnStudentInvoice.Enabled = true;
                            btnAgencyInvoice.Enabled = true;
                            _radGridInvoiceItems.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                            _radGridInvoiceItems.MasterTableView.EditMode = GridEditMode.InPlace;
                            delete.Visible = false;
                            ddlFG.Enabled = false;
                            break;
                        default:
                            btnConfirm.Enabled = false; //Confirm
                            btnModify.Enabled = false; //Modify
                            btnCancel.Enabled = false; //Cancel
                            btnStudentInvoice.Enabled = true; //Student Invoice Print
                            btnAgencyInvoice.Enabled = true; //Agency Invoice Print
                            _radGridInvoiceItems.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                            _radGridInvoiceItems.MasterTableView.EditMode = GridEditMode.InPlace;
                            delete.Visible = false;
                            ddlFG.Enabled = false;
                            break;
                    }
                }
                else
                {
                    btnConfirm.Enabled = false;
                    btnModify.Enabled = false;
                    btnCancel.Enabled = false;
                    btnStudentInvoice.Enabled = true;
                    btnAgencyInvoice.Enabled = true;
                    _radGridInvoiceItems.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                    _radGridInvoiceItems.MasterTableView.EditMode = GridEditMode.InPlace;
                    delete.Visible = false;
                    ddlFG.Enabled = false;
                }

                if (invoice.AgencyId != null)
                {
                    var cAgency = new CAgency();
                    var agency = cAgency.Get(Convert.ToInt32(invoice.AgencyId));
                    if (agency != null)
                    {
                        tbAgencyName.Text = agency.Name;
                        if (agency.Location != null)
                        {
                            var country = new CCountry().Get((int)agency.Location);
                            tbCountryCity.Text = country.Name;
                        }
                        else
                            tbCountryCity.Text = string.Empty;
                        if (agency.ContractStartDate != null && agency.ContractEndDate != null)
                            tbContractDate.Text = agency.ContractStartDate.Value.Date.ToString("MM-dd-yyyy") + " - " + agency.ContractEndDate.Value.Date.ToString("MM-dd-yyyy");
                        tbCommissionRate.Text = invoice.AgencyRate + "%";
                        tbDescription.Text = agency.Comment;
                    }
                }
                else
                {
                    tbAgencyName.Text = "Direct Student";
                    tbCountryCity.Text = string.Empty;
                    tbContractDate.Text = string.Empty;
                    tbCommissionRate.Text = string.Empty;
                    tbDescription.Text = string.Empty;
                }

                if (_radGridInvoiceItems.MasterTableView.EditMode == GridEditMode.Batch)
                    InvoiceItemGrid1.SetTypeOfInvoiceCoaItem(invoice.InvoiceType);
            }

            _radGridInvoiceItems.Rebind();
            RadGridInvoiceHistory.Rebind();
        }

        protected void ddlFG_OnSelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var cInvoice = new CInvoice();
            var invoice = cInvoice.Get(Convert.ToInt32(RadGridInvoice.SelectedValue.ToString()));

            invoice.IsFinancialGurantee = Convert.ToBoolean(ddlFG.SelectedValue);

            if (cInvoice.Update(invoice))
            {
                var cInvoiceItem = new CInvoiceItem();
                var invoiceItems = cInvoiceItem.GetInvoiceItems(invoice.InvoiceId);

                // fg mode
                if (invoice.IsFinancialGurantee)
                {
                    foreach (var v in invoiceItems)
                    {
                        // save fg
                        var cInvoiceItemFg = new CInvoiceItemFg();
                        var InvoiceItemFg = cInvoiceItemFg.Get(v.InvoiceItemId);
                        if (InvoiceItemFg == null)
                        {
                            cInvoiceItemFg.Add(new InvoiceItemFg()
                            {
                                InvoiceItemId = v.InvoiceItemId,
                                StandardPrice = v.StandardPrice,
                                StudentPrice = v.StudentPrice,
                                AgencyPrice = v.AgencyPrice,
                                CreatedId = CurrentUserId,
                                CreatedDate = DateTime.Now
                            });
                        }
                        else
                        {
                            InvoiceItemFg.StandardPrice = v.StandardPrice;
                            InvoiceItemFg.StudentPrice = v.StudentPrice;
                            InvoiceItemFg.AgencyPrice = v.AgencyPrice;
                            InvoiceItemFg.UpdatedId = CurrentUserId;
                            InvoiceItemFg.UpdatedDate = DateTime.Now;

                            cInvoiceItemFg.Update(InvoiceItemFg);
                        }

                        // original invoice set negative value to zero
                        if (v.StandardPrice < 0)
                            v.StandardPrice = 0;

                        if (v.StudentPrice < 0)
                            v.StudentPrice = 0;

                        cInvoiceItem.Update(v);
                    }
                }
                else
                {
                    foreach (var v in invoiceItems)
                    {
                        // save fg
                        var cInvoiceItemFg = new CInvoiceItemFg();
                        var invoiceItemFg = cInvoiceItemFg.Get(v.InvoiceItemId);

                        if (invoiceItemFg != null)
                        {
                            v.StandardPrice = invoiceItemFg.StandardPrice;
                            v.StudentPrice = invoiceItemFg.StudentPrice;
                            v.AgencyPrice = invoiceItemFg.AgencyPrice;
                            v.UpdatedId = CurrentUserId;
                            v.UpdatedDate = DateTime.Now;
                        }
                        cInvoiceItem.Update(v);
                    }
                }

                ShowMessage("Updated to Financial Gurantee status");
            }

            RadGridInvoice.Rebind();
        }

        protected void InvoiceToolbarButtonClicked(object sender, RadToolBarEventArgs e)
        {

            switch (e.Item.Text)
            {
                case "Confirm":
                    if (RadGridInvoice.SelectedValue != null)
                    {
                        var cInvoice = new CInvoice();
                        var invoice = cInvoice.Get(Convert.ToInt32(RadGridInvoice.SelectedValue));
                        invoice.Status = (int)CConstValue.InvoiceStatus.Invoiced; // Invoice Status(34) : Invoiced
                        invoice.UpdatedId = CurrentUserId;
                        invoice.UpdatedDate = DateTime.Now;

                        if (cInvoice.Update(invoice))
                            ShowMessage("Update inquiry successfully");
                        else
                            ShowMessage("Failed to update inquiry");
                        e.Item.Enabled = false;
                        RadGridInvoice.Rebind();
                    }
                    break;
                case "Cancel":
                    if (RadGridInvoice.SelectedValue != null)
                    {
                        var cInvoice = new CInvoice();
                        var invoice = cInvoice.Get(Convert.ToInt32(RadGridInvoice.SelectedValue));

                        if (invoice.Status == (int)CConstValue.InvoiceStatus.Pending)
                        {
                            RunClientScript("ShowCancelWindow(" + RadGridInvoice.SelectedValue + ");");
                        }
                        else if (invoice.Status == (int)CConstValue.InvoiceStatus.Invoiced)
                        {
                            if (new CPayment().InvoiceCheck(invoice.InvoiceId) == 0)
                                RunClientScript("ShowCancelWindow(" + RadGridInvoice.SelectedValue + ");");
                            else
                                ShowMessage("It can't because of already paid in invoice");
                        }
                        RadGridInvoice.Rebind();
                    }
                    break;
                case "Agency Invoice":
                    if (RadGridInvoice.SelectedValue != null)
                    {
                        var selectedInvoiceList = new List<int>();
                        foreach (GridDataItem item in RadGridInvoice.SelectedItems)
                            selectedInvoiceList.Add((int)item.GetDataKeyValue("InvoiceId"));

                        RunClientScript("ShowReportPop('" + String.Join(", ", selectedInvoiceList.ToArray()) + "', '" + (int)CConstValue.Report.InvoiceAgency + "' );");
                    }
                    break;
                case "Student Invoice":
                    if (RadGridInvoice.SelectedValue != null)
                    {
                        var selectedInvoiceList = new List<int>();
                        foreach (GridDataItem item in RadGridInvoice.SelectedItems)
                            selectedInvoiceList.Add((int)item.GetDataKeyValue("InvoiceId"));

                        RunClientScript("ShowReportPop('" + String.Join(", ", selectedInvoiceList.ToArray()) + "', '" + (int)CConstValue.Report.InvoiceStudent + "' );");
                    }
                    break;
                case "Modify":
                    if (RadGridInvoice.SelectedValue != null)
                    {
                        var cInvoice = new CInvoice();
                        var invoice = cInvoice.Get(Convert.ToInt32(RadGridInvoice.SelectedValue));

                        if (invoice.Status == (int)CConstValue.InvoiceStatus.Invoiced ||
                            invoice.InvoiceType == (int)CConstValue.InvoiceType.General ||
                            invoice.InvoiceType == (int)CConstValue.InvoiceType.Simple ||
                            invoice.InvoiceType == (int)CConstValue.InvoiceType.Manual ||
                            invoice.InvoiceType == (int)CConstValue.InvoiceType.Homestay ||
                            invoice.InvoiceType == (int)CConstValue.InvoiceType.Dormitory) //invoiced
                        {
                            var payments = new CPayment();

                            if (payments.InvoiceCheck(invoice.InvoiceId) == 0)
                            {
                                invoice.Status = (int)CConstValue.InvoiceStatus.Cancelled_MD; // Invoice Status(34) : Cancelled_M
                                invoice.UpdatedId = CurrentUserId;
                                invoice.UpdatedDate = DateTime.Now;

                                if (cInvoice.Update(invoice))
                                {
                                    var cNewInvoice = new CInvoice();
                                    var newInvoice = new Erp2016.Lib.Invoice();
                                    CGlobal.Copy(invoice, newInvoice);
                                    newInvoice.OriginalInvoiceId = invoice.InvoiceId;
                                    newInvoice.Status = (int)CConstValue.InvoiceStatus.Pending; // pending
                                    newInvoice.CreatedId = CurrentUserId;
                                    newInvoice.CreatedDate = DateTime.Now;

                                    if (cNewInvoice.Add(newInvoice) > 0)
                                    {
                                        var cInvoiceItem = new CInvoiceItem();
                                        List<InvoiceItem> originalInvoiceItems = cInvoiceItem.GetInvoiceItems(invoice.InvoiceId);
                                        List<InvoiceItem> newInvoiceItems = new List<InvoiceItem>();
                                        foreach (InvoiceItem ori in originalInvoiceItems)
                                        {
                                            var newInvoiceItem = new InvoiceItem();
                                            CGlobal.Copy(ori, newInvoiceItem);
                                            newInvoiceItem.InvoiceId = newInvoice.InvoiceId;
                                            newInvoiceItem.CreatedId = CurrentUserId;
                                            newInvoiceItem.CreatedDate = DateTime.Now;
                                            newInvoiceItems.Add(newInvoiceItem);
                                        }
                                        // copy invoiceItems
                                        if (cInvoiceItem.Add(newInvoiceItems) == false)
                                            ShowMessage("Error inserting invoice Items");

                                        RadGridInvoice.Rebind();
                                    }
                                }
                            }
                            else
                            {
                                ShowMessage("It can't because of already paid in invoice");
                            }
                        }
                    }
                    break;
                case "New Simple Invoice":
                    RunClientScript("ShowNewSimpleInvoice();");
                    break;
                case "Student Page":
                    if (RadGridInvoice.SelectedValue != null)
                        Response.Redirect("~/Student?id=" + RadGridInvoice.SelectedValues["StudentId"]);
                    break;
                case "Payment Page":
                    if (RadGridInvoice.SelectedValue != null)
                        Response.Redirect("~/Payment?id=" + RadGridInvoice.SelectedValues["StudentId"]);
                    break;
                case "Deposit Page":
                    if (RadGridInvoice.SelectedValue != null)
                        Response.Redirect("~/Deposit?id=" + RadGridInvoice.SelectedValues["StudentId"]);
                    break;
                case "CreditMemo Page":
                    if (RadGridInvoice.SelectedValue != null)
                        Response.Redirect("~/CreditMemo?id=" + RadGridInvoice.SelectedValues["StudentId"]);
                    break;
                case "Refund Page":
                    if (RadGridInvoice.SelectedValue != null)
                        Response.Redirect("~/Refund?id=" + RadGridInvoice.SelectedValues["StudentId"]);
                    break;
            }
        }

        protected void InvoiceListToolbar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            if (e.Item.Text == @"Search")
            {
                InvoiceListSearch();
            }
        }

        protected void InvoiceListSearch()
        {
            LinqDataSourceInvoice.WhereParameters.Clear();
            foreach (var model in UserPermissionModel.SearchSiteLocationList)
                LinqDataSourceInvoice.WhereParameters.Add(model.SiteLocationIdName, DbType.Int32, model.SiteLocationId.ToString());
            LinqDataSourceInvoice.Where = UserPermissionModel.SearchWhereSiteLocationSb.ToString();

            var studentId = Request["id"];
            if (!string.IsNullOrEmpty(studentId))
            {
                LinqDataSourceInvoice.WhereParameters.Add("StudentId", DbType.Int32, studentId);
                if (LinqDataSourceInvoice.Where.Length > 0)
                    LinqDataSourceInvoice.Where += " && StudentId == @StudentId";
                else
                    LinqDataSourceInvoice.Where = "StudentId == @StudentId";

                if (!IsPostBack)
                {
                    RadGridInvoice.MasterTableView.Rebind();
                    foreach (GridDataItem item in RadGridInvoice.Items)
                    {
                        if (item.GetDataKeyValue("StudentId").ToString() == studentId)
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
            }
        }

        private void _radGridInvoiceItems_PreRender(object sender, EventArgs e)
        {
            if (RadGridInvoice.SelectedValue != null)
                // save invoiceId
                InvoiceItemGrid1.InvoiceId = (int)RadGridInvoice.SelectedValue;
            // validation
            InvoiceItemGrid1.ValidateInvoiceItems();
            // Get Select
            // todo: here is problem....
            GetInvoiceItems();
            SetVisibleModifyControllers();
        }

        protected void RadGridInvoiceHistory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadGridInvoiceHistory.SelectedValue != null)
                RunClientScript("ShowInvoiceWindow(" + RadGridInvoiceHistory.SelectedValue + ");");
        }

        protected void RadGridInvoice_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        public override void SetVisibleModifyControllers()
        {
            if (UserPermissionModel.IsModify == false)
            {
                // toolbar
                foreach (RadToolBarItem toolbarItem in RadToolBar1.Items)
                {
                    toolbarItem.Enabled = false;
                }

                ddlFG.Enabled = false;
                InvoiceItemGrid1.SetEditMode(false);
            }
        }

        protected void RadGridInvoice_OnPreRender(object sender, EventArgs e)
        {
            if (ViewState["InvoiceId"] != null)
            {
                foreach (GridDataItem item in RadGridInvoice.Items)
                {
                    if (item.GetDataKeyValue("InvoiceId").ToString() == ViewState["InvoiceId"].ToString())
                    {
                        if (item.Selected == false)
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
            }
        }

        protected void RadGridInvoice_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["InvoiceId"] = RadGridInvoice.SelectedValue;
        }

        protected void ButtonGridRefresh_OnClick(object sender, EventArgs e)
        {
            RadGridInvoice.Rebind();
        }

        protected void RadGridInvoiceHistory_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        protected void RadButtonExcel_OnClick(object sender, EventArgs e)
        {
            var filterExpression = ConvertFilterExpressionToSqlExpression(RadGridInvoice.MasterTableView.Columns);
            var tempDt = new CInvoice().GetVwInvoiceExcel(filterExpression);
            ExportExcel(tempDt, "Invoice Detail");
        }

        protected void RadButtonExcelDetail_OnClick(object sender, EventArgs e)
        {
            var filterExpression = ConvertFilterExpressionToSqlExpression(RadGridInvoice.MasterTableView.Columns, true);
            var tempDt = new CInvoice().GetInvoiceDetailExcel(filterExpression);
            ExportExcel(tempDt, "Invoice Detail for pivot");
        }
    }
}