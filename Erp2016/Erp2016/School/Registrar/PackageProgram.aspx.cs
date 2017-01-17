using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;

namespace School.Registrar
{
    public partial class PackageProgram : PageBase
    {
        public PackageProgram() : base((int)CConstValue.Menu.PackageProgram)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ResetForm();
            }

            LinqDataSourceInvoice.WhereParameters.Clear();
            LinqDataSourceInvoice.WhereParameters.Add("PackageProgramFlag", DbType.Boolean, bool.TrueString);
            LinqDataSourceInvoice.Where = "PackageProgramFlag== @PackageProgramFlag";

            FileDownloadList1.InitFileDownloadList((int)CConstValue.Upload.PackageProgram);
            FileDownloadList1.SetVisibieUploadControls(false);

            SearchPackageProgram();
            GetPackageDetail();
        }
        private void ResetForm()
        {
            RadTextBoxFaculty.Text = string.Empty;
            RadTextBoxProgramGroup.Text = string.Empty;
            RadTextBoxProgram.Text = string.Empty;
            LabelDescription.Text = string.Empty;
            LabelPackageStartDate.Text = string.Empty;
            LabelPackageEndDate.Text = string.Empty;
        }

        private void GetSiteLocation()
        {
            RadComboBoxSiteLocation.Items.Clear();
            List<SiteLocation> siteLocationList = new List<SiteLocation>();
            if (RadGridProgramPackage.SelectedValue != null)
            {
                var cPackageProgramSiteLocation = new CPackageProgramSiteLocation();
                var packageProgramSiteLocation = cPackageProgramSiteLocation.GetPackageProgramSiteLocationList(Convert.ToInt32(RadGridProgramPackage.SelectedValue));
                if (packageProgramSiteLocation.Count > 0)
                {
                    var siteLocation = new CSiteLocation().Get(packageProgramSiteLocation[0].SiteLocationId);
                    siteLocationList = new CSiteLocation().GetSiteLocationBySiteId(siteLocation.SiteId);

                    RadTextBoxSite.Text = (new CSite()).Get(siteLocation.SiteId).Abbreviation;
                }

                foreach (var siteLocation in siteLocationList)
                {
                    RadComboBoxSiteLocation.Items.Add(new RadComboBoxItem(siteLocation.Name, siteLocation.SiteLocationId.ToString()));
                }

                foreach (var packageProgramSiteLo in packageProgramSiteLocation)
                {
                    foreach (RadComboBoxItem siteLocation in RadComboBoxSiteLocation.Items)
                    {
                        if (packageProgramSiteLo.SiteLocationId == Convert.ToInt32(siteLocation.Value))
                        {
                            siteLocation.Checked = true;
                        }
                    }
                }
            }
            else
            {
                var cSiteLocation = new CSiteLocation();
                siteLocationList = cSiteLocation.GetSiteLocationBySiteId(CurrentSiteId);
                foreach (var siteLocation in siteLocationList)
                {
                    RadComboBoxSiteLocation.Items.Add(new RadComboBoxItem(siteLocation.Name, siteLocation.SiteLocationId.ToString()));
                }
            }

            RadComboBoxSiteLocation_OnSelectedIndexChanged(null, null);
        }

        private void GetPackageDetail()
        {
            // detail grid
            LinqDataSourceProgramDetail.WhereParameters.Clear();
            if (RadGridProgramPackage.SelectedValue == null)
                LinqDataSourceProgramDetail.WhereParameters.Add("PackageProgramId", DbType.Int32, 0.ToString());
            else
                LinqDataSourceProgramDetail.WhereParameters.Add("PackageProgramId", DbType.Int32, RadGridProgramPackage.SelectedValue.ToString());

            LinqDataSourceProgramDetail.Where = "PackageProgramId == @PackageProgramId";
        }

        private void SetApprovalList()
        {
            LinqDataSource LinqDataSourceApprovalList = ApprovalLine1.GetSqlDataSourceApprovalList();
            if (LinqDataSourceApprovalList != null)
            {
                LinqDataSourceApprovalList.WhereParameters.Clear();
                if (RadGridProgramPackage.SelectedValue != null)
                    LinqDataSourceApprovalList.WhereParameters.Add("MenuSeqId", DbType.Int32, RadGridProgramPackage.SelectedValue.ToString());
                else
                    LinqDataSourceApprovalList.WhereParameters.Add("MenuSeqId", DbType.Int32, "0");
                LinqDataSourceApprovalList.WhereParameters.Add("ApproveType", DbType.Int32, ((int)CConstValue.Approval.Package).ToString());
                LinqDataSourceApprovalList.Where = "ApproveType == @ApproveType and MenuSeqId == @MenuSeqId";

                //ApprovalListView.Rebind();
            }
        }

        protected void ExportExcel(object sender, EventArgs e)
        {

        }

        protected void RadGrid_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ResetForm();

            // detail information
            var packageProgram = new CPackageProgram().GetPackageProgram(Convert.ToInt32(RadGridProgramPackage.SelectedValue));

            LabelPackageProgramName.Text = packageProgram.PackageProgramName;
            var program = (new CProgram()).Get((int)packageProgram.ProgramId);
            if (program != null)
            {
                RadTextBoxProgram.Text = program.ProgramFullName;
                if (program.ProgramGroupId != null)
                {
                    var programGroup = new CProgramGroup().Get((int)program.ProgramGroupId);
                    if (programGroup != null)
                    {
                        if (programGroup.FacultyId != null)
                            RadTextBoxFaculty.Text = new CFaculty().Get((int)programGroup.FacultyId)?.Name;
                        RadTextBoxProgramGroup.Text = programGroup.Name;
                    }
                }
            }

            LabelDescription.Text = packageProgram.Description;
            LabelPackageStartDate.Text = string.Format("{0:MM-dd-yyyy}", packageProgram.StartDate);
            LabelPackageEndDate.Text = string.Format("{0:MM-dd-yyyy}", packageProgram.EndDate);

            GetSiteLocation();

            // toolbar
            foreach (RadToolBarItem toolbarItem in RadToolBarPackageProgram.Items)
            {
                // request active check
                if (toolbarItem.Text == "Request")
                {
                    if ((packageProgram.ApprovalStatus == null ||
                        packageProgram.ApprovalStatus == (int)CConstValue.ApprovalStatus.Revise) && packageProgram.CreatedId == CurrentUserId)
                        toolbarItem.Enabled = true;
                    else
                        toolbarItem.Enabled = false;
                }
                else if (toolbarItem.Text == "Cancel")
                {
                    if ((packageProgram.ApprovalStatus == null ||
                        packageProgram.ApprovalStatus == (int)CConstValue.ApprovalStatus.Revise ||
                        packageProgram.ApprovalStatus == (int)CConstValue.ApprovalStatus.Requested) && packageProgram.CreatedId == CurrentUserId)
                        toolbarItem.Enabled = true;
                    else
                        toolbarItem.Enabled = false;
                }
                else if (toolbarItem.Text == "Approve" || toolbarItem.Text == "Reject")
                {
                    // approve active check
                    var refundApproveInfo = new CGlobal();
                    var supervisor = refundApproveInfo.CheckApprovalEnable((int)CConstValue.Approval.Package, Convert.ToInt32(packageProgram.PackageProgramId));

                    // supervisor or loy employees
                    if ((CurrentUserId == supervisor)
                        && packageProgram.ApprovalStatus != (int)CConstValue.ApprovalStatus.Approved
                        && packageProgram.ApprovalStatus != (int)CConstValue.ApprovalStatus.Rejected
                        && packageProgram.ApprovalStatus != (int)CConstValue.ApprovalStatus.Canceled
                        && packageProgram.ApprovalStatus != null
                        && CurrentUserId != packageProgram.ApprovalId)
                        toolbarItem.Enabled = true;
                    else
                        toolbarItem.Enabled = false;
                }
            }

            // Detail View
            if (CurrentUserId == packageProgram.CreatedId && packageProgram.ApprovalStatus == null)
            {
                RadGridPackageProgramDetail.AllowAutomaticInserts = true;
                RadGridPackageProgramDetail.AllowAutomaticUpdates = true;
                RadGridPackageProgramDetail.AllowAutomaticDeletes = true;
                //RadGridPackageProgramDetail.MasterTableView.EditMode = GridEditMode.Batch;
                RadGridPackageProgramDetail.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
                // hidden delete button
                RadGridPackageProgramDetail.MasterTableView.Columns[RadGridPackageProgramDetail.MasterTableView.Columns.Count - 1].Visible = true;
            }
            else
            {
                RadGridPackageProgramDetail.AllowAutomaticInserts = false;
                RadGridPackageProgramDetail.AllowAutomaticUpdates = false;
                RadGridPackageProgramDetail.AllowAutomaticDeletes = false;
                //RadGridPackageProgramDetail.MasterTableView.EditMode = GridEditMode.InPlace;
                RadGridPackageProgramDetail.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                // hidden delete button
                RadGridPackageProgramDetail.MasterTableView.Columns[RadGridPackageProgramDetail.MasterTableView.Columns.Count - 1].Visible = false;
            }
            //SetApprovalList();
        }

        protected void RadGridPackageProgramDetail_OnBatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {
            foreach (var command in e.Commands)
            {
                if (command.Type.ToString() != "Delete")
                {
                    command.NewValues["PackageProgramId"] = Convert.ToInt32(RadGridProgramPackage.SelectedValue);

                    var standardPrice = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["StandardPrice"]))) ? 0 : Convert.ToDecimal(command.NewValues["StandardPrice"]);
                    var studentPrice = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["StudentPrice"]))) ? 0 : Convert.ToDecimal(command.NewValues["StudentPrice"]);
                    var agencyPrice = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["AgencyPrice"]))) ? 0 : Convert.ToDecimal(command.NewValues["AgencyPrice"]);

                    var invoiceCoaItem = (new CInvoiceCoaItem()).Get(Convert.ToInt32(command.NewValues["InvoiceCoaItemId"]));
                    if (invoiceCoaItem != null)
                    {
                        standardPrice = Math.Abs(standardPrice) * invoiceCoaItem.ItemType;
                        studentPrice = Math.Abs(studentPrice) * invoiceCoaItem.ItemType;
                        agencyPrice = Math.Abs(agencyPrice) * invoiceCoaItem.ItemType;
                    }

                    command.NewValues["StandardPrice"] = standardPrice;
                    command.NewValues["StudentPrice"] = studentPrice;
                    command.NewValues["AgencyPrice"] = agencyPrice;

                    command.NewValues["CreatedId"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["CreatedId"]))) ? 0 : Convert.ToInt32(command.NewValues["CreatedId"]);
                    command.NewValues["CreatedDate"] = (string.IsNullOrEmpty(Convert.ToString(command.NewValues["CreatedDate"]))) ? DateTime.Now : command.NewValues["CreatedDate"];
                }
            }
        }

        protected void RadToolBarPackageProgram_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Text)
            {
                case "Request":
                    if (RadGridProgramPackage.SelectedValue != null)
                        RunClientScript("ShowPop('" + RadGridProgramPackage.SelectedValue + "', '1');");
                    break;
                case "Approve":
                    if (RadGridProgramPackage.SelectedValue != null)
                        RunClientScript("ShowApprovalWindow('" + RadGridProgramPackage.SelectedValue + "');");
                    break;
                case "Reject":
                    if (RadGridProgramPackage.SelectedValue != null)
                        RunClientScript("ShowApprovalRejectWindow('" + RadGridProgramPackage.SelectedValue + "');");
                    break;
                case "Revise":
                    if (RadGridProgramPackage.SelectedValue != null)
                        RunClientScript("ShowApprovalReviseWindow('" + RadGridProgramPackage.SelectedValue + "');");
                    break;
                case "Cancel":
                    if (RadGridProgramPackage.SelectedValue != null)
                        RunClientScript("ShowApprovalCancelWindow('" + RadGridProgramPackage.SelectedValue + "');");
                    break;
                case "New Package":
                    RunClientScript("ShowPop('-1', '0');");
                    break;
            }
        }

        protected void RadGridPackageProgramDetail_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataItem = (GridDataItem)e.Item;
                if ((dataItem["StandardPrice"].FindControl("lblStandardPrice") as Label).Text.Contains("-"))
                    (dataItem["StandardPrice"].FindControl("lblStandardPrice") as Label).Style["color"] = CConstValue.NagativeColorName;
                if ((dataItem["StudentPrice"].FindControl("lblStudentPrice") as Label).Text.Contains("-"))
                    (dataItem["StudentPrice"].FindControl("lblStudentPrice") as Label).Style["color"] = CConstValue.NagativeColorName;
                if ((dataItem["StudentPrice"].FindControl("lblAgencyPrice") as Label).Text.Contains("-"))
                    (dataItem["AgencyPrice"].FindControl("lblAgencyPrice") as Label).Style["color"] = CConstValue.NagativeColorName;
            }
            else if (e.Item is GridFooterItem)
            {
                var footer = (GridFooterItem)e.Item;
                //if ((footer["StandardPrice"].FindControl("TotalStandard") as RadNumericTextBox).Text.Contains("-"))
                //    (footer["StandardPrice"].FindControl("TotalStandard") as RadNumericTextBox).Style["color"] = Color.OrangeRed.Name;
            }
        }

        protected void RadGridPackageProgramDetail_OnPreRender(object sender, EventArgs e)
        {
            SetApprovalList();
            GetPackageDetail();

            if (RadGridProgramPackage.SelectedValue != null)
                FileDownloadList1.GetFileDownload(Convert.ToInt32(RadGridProgramPackage.SelectedValue));

        }

        protected void RadGridProgramPackage_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

        public override void SetVisibleModifyControllers()
        {
            if (UserPermissionModel.IsModify == false)
            {
                // toolbar
                foreach (RadToolBarItem toolbarItem in RadToolBarPackageProgram.Items)
                {
                    toolbarItem.Enabled = false;
                }

                RadGridPackageProgramDetail.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                RadGridPackageProgramDetail.MasterTableView.EditMode = GridEditMode.InPlace;
                var delete = RadGridPackageProgramDetail.MasterTableView.GetColumn("DeleteColumn");
                delete.Visible = false;
            }
        }

        protected void RadDropDownListView_OnSelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            ViewState["SelectedTextView"] = e.Text;
            SearchPackageProgram();
        }

        private void SearchPackageProgram()
        {
            LinqDataSourceProgramPackage.WhereParameters.Clear();
            foreach (var model in UserPermissionModel.SearchSiteList)
                LinqDataSourceProgramPackage.WhereParameters.Add(model.SiteIdName, DbType.Int32, model.SiteId.ToString());
            LinqDataSourceProgramPackage.Where = UserPermissionModel.SearchWhereSiteSb.ToString();
            SetViewType(LinqDataSourceProgramPackage, RadGridProgramPackage);
        }

        protected void RadComboBoxSiteLocation_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var sb = new StringBuilder();
            var collection = RadComboBoxSiteLocation.CheckedItems;

            if (collection.Count != 0)
            {
                sb.Append("<h4>Checked SiteLocation List</h4>");
                foreach (var item in collection)
                    sb.Append("<label>" + item.Text + "</label>");

                itemsClientSide.Text = sb.ToString();
            }
            else
            {
                itemsClientSide.Text = string.Empty;
            }
        }

        protected void RadGridPackageProgramDetail_OnFilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            SetFilterCheckListItems(e);
        }

    }
}