using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Erp2016.Lib;
using Telerik.Web.UI;

public class PageBase : Page, IPageModifyAccess
{
    public int CurrentUserId { get; set; }
    public int CurrentSiteId { get; set; }
    public int CurrentSiteLocationId { get; set; }
    public int CurrentPositionId { get; set; }
    public int CurrentGroupId { get; set; }
    public string CurrentUserName { get; set; }
    public string CurrentSiteName { get; set; }
    public string CurrentSiteLocationName { get; set; }
    public List<CUserPermissionModel> CurrentPermissionList { get; set; }
    public CUserPermissionModel UserPermissionModel { get; set; }

    private readonly int _menuId;
    private Unit _heightForTopMenu;
    private Unit _heightForH4;
    private Unit _heightForToolbar;
    private Unit _heightForDocInfo;

    public PageBase()
    {
    }

    public PageBase(int menuId)
    {
        this._menuId = menuId;
    }

    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
            HttpContext.Current.Session.Timeout = 60 * 60;

        if (Session["UserID"] != null)
        {
            CurrentUserId = Convert.ToInt32(Session["UserID"].ToString());
            CurrentSiteId = Convert.ToInt32(Session["SiteId"]);
            CurrentSiteLocationId = Convert.ToInt32(Session["SiteLocationId"]);
            CurrentPositionId = Convert.ToInt32(Session["UserPositionId"]);
            CurrentGroupId = Convert.ToInt32(Session["UserGroupId"]);
            CurrentPermissionList = (List<CUserPermissionModel>)Session["UserPermissionModelList"];
            CurrentSiteName = Session["SiteName"]?.ToString();
            CurrentSiteLocationName = Session["SiteLocationName"]?.ToString();
            CurrentUserName = Session["UserName"]?.ToString();

            if (CurrentPermissionList != null)
            {
                var userPermissionModel = CurrentPermissionList.FirstOrDefault(x => x.MenuId == _menuId);
                if (userPermissionModel != null)
                {
                    if (userPermissionModel.IsAccess == false)
                    {
                        if (HttpContext.Current.Request.Url.AbsolutePath != "/NoPermission")
                            Response.Redirect("~/NoPermission");
                    }
                    else
                        UserPermissionModel = userPermissionModel;
                }
            }
        }
        else
            Response.Redirect("~/Login", true);

        base.OnInit(e);
    }

    protected override void OnLoadComplete(EventArgs e)
    {
        base.OnLoadComplete(e);
        SetVisibleModifyControllers();
    }

    public virtual void SetVisibleModifyControllers()
    {
        // over write
    }

    public void RunClientScript(string script)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "Script", script, true);
    }

    public void ShowMessage(string msg)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "AlertMessage", string.Format("radalert(\"" + "{0}" + "\", 300, 150, \"Message\");", msg), true);
    }


    /// <summary>
    /// </summary>
    /// <param name="asyncUpload">asyncUpload control</param>
    /// <param name="approveType">menu type</param>
    /// <param name="seqId">db seq</param>
    public void UploadFiles(RadAsyncUpload asyncUpload, int approveType, int seqId)
    {
        foreach (UploadedFile uploadedFile in asyncUpload.UploadedFiles)
        {
            var upType = approveType.ToString();
            var upSeq = seqId.ToString();

            // Server.MapPath("~/Upload/");
            var pathType = Path.Combine(CConstValue.WebUploadPath, upType);
            var pathSeq = Path.Combine(pathType, upSeq);

            if (!Directory.Exists(pathType))
            {
                Directory.CreateDirectory(pathType);
                Directory.CreateDirectory(pathSeq);
            }
            else if (!Directory.Exists(pathSeq))
            {
                Directory.CreateDirectory(pathSeq);
            }

            asyncUpload.TargetFolder = upType + @"\" + upSeq + @"\";

            var fileName = DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + "_" + DateTime.Now.ToString("hhmmss") + "_" + uploadedFile.FileName;

            uploadedFile.SaveAs(Path.Combine(pathSeq, fileName));


            var cFile = new CUploadFile();
            var file = new UploadFile();
            file.UploadType = Convert.ToInt32(upType);
            file.SeqId = Convert.ToInt32(upSeq);
            file.Path = asyncUpload.TargetFolder;
            file.Name = fileName;

            file.CreatedId = CurrentUserId;
            file.CreatedDate = DateTime.Now;
            file.UpdatedId = CurrentUserId;
            file.UpdatedDate = DateTime.Now;

            if (cFile.Add(file) > 0)
            {
            }
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="uploadFileId"></param>
    public void DownloadFile(int uploadFileId)
    {
        var cUploadFile = new CUploadFile();
        var uploadFile = cUploadFile.Get(uploadFileId);
        var fullPath = Path.Combine(CConstValue.WebUploadPath, uploadFile.Path, uploadFile.Name);

        try
        {
            Response.Clear();
            // Clear the content of the response
            Response.ClearContent();
            Response.ClearHeaders();
            // Buffer response so that page is sent
            // after processing is complete.
            Response.BufferOutput = true;
            // Add the file name and attachment,
            // which will force the open/cance/save dialog to show, to the header

            var oFile = new FileInfo(fullPath);
            var fileName = oFile.Name;

            if (File.Exists(fullPath))
            {

                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);

                Response.ContentType = "application/octet-stream";
                Response.WriteFile(fullPath);

                Response.Flush();

                HttpContext.Current.ApplicationInstance.CompleteRequest();
                //Response.End();
            }
            else
            {
                if (Request.UrlReferrer != null)
                {
                    var csType = GetType();
                    var jsScript = "alert('File Not Found');";
                    ScriptManager.RegisterClientScriptBlock(Page, csType, "popup", jsScript, true);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Print(ex.Message);
        }
    }

    public void ExportExcel(DataTable dt, string title)
    {
        try
        {
            // open license
            var gemboxService = new GemboxService();
            var excelFile = gemboxService.SetExcelFile(dt, title);

            if (excelFile != null)
            {
                var fileName = title + "_" + DateTime.Now.Ticks + ".xlsx";
                HttpContext.Current.Response.Clear();
                //// Clear the content of the response
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);

                var excelStream = new MemoryStream();
                excelFile.SaveXlsx(excelStream);
                excelStream.WriteTo(HttpContext.Current.Response.OutputStream);
                HttpContext.Current.Response.Flush();
                //HttpContext.Current.Response.End();
            }
            else
                new Exception("Error");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (!IsPostBack)
            SetControlAutoHeight();
    }

    public void SetControlAutoHeight()
    {
        var skinCookie = Request.Cookies["Skin"];

        // LightWeight Mode
        switch (skinCookie?.Value)
        {
            case "Bootstrap":
                _heightForTopMenu = new Unit(100, UnitType.Pixel);
                _heightForH4 = new Unit(40, UnitType.Pixel);
                _heightForToolbar = new Unit(50, UnitType.Pixel);
                _heightForDocInfo = new Unit(80, UnitType.Pixel);
                break;

            case "MetroTouch":
            case "BlackMetroTouch":
                _heightForTopMenu = new Unit(74, UnitType.Pixel);
                _heightForH4 = new Unit(40, UnitType.Pixel);
                _heightForToolbar = new Unit(40, UnitType.Pixel);
                _heightForDocInfo = new Unit(70, UnitType.Pixel);
                break;

            case "Glow":
            case "Silk":
                _heightForTopMenu = new Unit(74, UnitType.Pixel);
                _heightForH4 = new Unit(40, UnitType.Pixel);
                _heightForToolbar = new Unit(40, UnitType.Pixel);
                _heightForDocInfo = new Unit(70, UnitType.Pixel);
                break;

            case "Material":
                _heightForTopMenu = new Unit(105, UnitType.Pixel);
                _heightForH4 = new Unit(40, UnitType.Pixel);
                _heightForToolbar = new Unit(40, UnitType.Pixel);
                _heightForDocInfo = new Unit(70, UnitType.Pixel);
                break;

            default:
                _heightForTopMenu = new Unit(74, UnitType.Pixel);
                _heightForH4 = new Unit(40, UnitType.Pixel);
                _heightForToolbar = new Unit(40, UnitType.Pixel);
                _heightForDocInfo = new Unit(70, UnitType.Pixel);
                break;
        }

        try
        {
            // change height
            foreach (Control c in Page.Form.Controls)
            {
                FindRadPaneControl(c);
            }
        }
        catch (Exception ex)
        {
            Debug.Print(ex.Message);
        }
    }

    private void FindRadPaneControl(Control control)
    {
        foreach (Control c in control.Controls)
        {
            // Top Menu
            if (c.GetType() == typeof(RadPane))
            {
                RadPane pane = (RadPane)c;
                if (pane.ID == "topPane" && pane.Height.Value == 74)
                {
                    pane.Height = _heightForTopMenu;
                }
            }
            // RadToolBar
            else if (c.GetType() == typeof(RadToolBar))
            {
                if (c.Parent.GetType() == typeof(RadPane))
                {
                    RadPane pane = (RadPane)c.Parent;
                    if (pane.Height.Value == 40)
                    {
                        pane.Height = _heightForToolbar;
                        return;
                    }
                }
            }
            // H4
            else if (c.GetType() == typeof(LiteralControl))
            {
                if (c.Parent.GetType() == typeof(RadPane))
                {
                    RadPane pane = (RadPane)c.Parent;
                    if (pane.Height.Value == 27)
                    {
                        LiteralControl literal = (LiteralControl)c;
                        if (literal.Text.Contains("<h4>"))
                        {
                            pane.Height = _heightForH4;
                            return;
                        }
                    }
                }
            }
            // DocInfo
            else if (c.GetType() == typeof(RadGrid))
            {
                if (c.Parent.GetType() == typeof(RadPane))
                {
                    RadPane pane = (RadPane)c.Parent;
                    if (pane.Height.Value == 70)
                    {
                        pane.Height = _heightForDocInfo;
                        return;
                    }
                }
            }
            if (typeof(Control).IsAssignableFrom(c.GetType()))
                FindRadPaneControl(c);
        }
    }

    public static void SetFilterCheckListItems(GridFilterCheckListItemsRequestedEventArgs e)
    {
        object dataSource = null;
        string dataField = (e.Column as IGridDataColumn).GetActiveDataField();

        switch (dataField)
        {
            // Common
            case "SiteName":
                dataSource = new CSite().GetSiteNameList();
                break;
            case "SiteLocationName":
                dataSource = new CSiteLocation().GetSiteLocationNameList();
                break;
            case "CountryName":
                dataSource = new CCountry().GetCountryNameList();
                break;
            case "AgencyName":
                dataSource = new CAgency().GetAgencyNameList();
                break;
            case "ProgramName":
                dataSource = new CProgram().GetProgramNameList();
                break;
            case "InvoiceCoaItemId":
                dataSource = new CInvoiceCoaItem().GetInvoiceCoaItemIdNameList();
                break;
            case "InvoiceName":
                dataSource = new CProgram().GetInvoiceNameList();
                break;
            case "StudentName":
                dataSource = new CStudent().GetStudentNameList();
                break;
            case "UserName":
                dataSource = new CUser().GetUserNameList();
                break;
            case "Status":
                dataSource = new CApproval().GetStatusNameList();
                break;
            case "ApprovalUserName":
                dataSource = new CUser().GetApprovalUserNameList();
                break;
            case "InstructorName":
                dataSource = new CUser().GetInstructorNameList();
                break;
            case "ProgramStatusName":
                dataSource = new CProgramRegistration().GetProgramStatusList();
                break;

            // Dashboard
            case "Type":
                dataSource = new CApproval().GetApprovalTypeNameList();
                break;

            // Invoice
            case "InvoiceType":
                dataSource = new CInvoice().GetInvoiceTypeList();
                break;
            case "InvoiceStatus":
                dataSource = new CInvoice().GetInvoiceStatusList();
                break;

            // Deposit
            case "DepositStatus":
                dataSource = new CDeposit().GetDepositStatusNameList();
                break;
            case "DepositBank":
                dataSource = new CDeposit().GetDepositBankNameList();
                break;
            case "PaidMethod":
                dataSource = new CDeposit().GetPaidMethodNameList();
                break;
            case "ExtraTypeName":
                dataSource = new CDeposit().GetExtraTypeNameList();
                break;

            // CreditMemo
            case "CreditMemoType":
                dataSource = new CCreditMemo().GetCreditMemoTypeNameList();
                break;
            case "PayoutMethodName":
                dataSource = new CCreditMemoPayout().GetPayoutMethodNameList();
                break;

            // Academic
            case "FacultyName":
                dataSource = new CFaculty().GetFacultyNameList();
                break;
            case "ProgramGroupName":
                dataSource = new CProgramGroup().GetProgramGroupNameList();
                break;

            // Vacation
            case "VacationType":
                dataSource = new CVacation().GetVacationTypeNameList();
                break;

            // User
            case "CreatedUserName":
                dataSource = new CUser().GetCreatedUserNameList();
                break;
            case "UpdatedUserName":
                dataSource = new CUser().GetUpdatedUserNameList();
                break;
            case "PositionName":
                dataSource = new CUser().GetPositionNameList();
                break;
            case "Email":
                dataSource = new CUser().GetEmailNameList();
                break;
            case "LoginId":
                dataSource = new CUser().GetLoginIdNameList();
                break;

            // PurchaseOrder
            case "PurchaseOrderTypeName":
                dataSource = new CPurchaseOrder().GetPurchaseOrderTypeNameList();
                break;
            case "PriorityTypeName":
                dataSource = new CPurchaseOrder().GetPriorityTypeNameList();
                break;
            case "ReviewTypeName":
                dataSource = new CPurchaseOrder().GetReviewTypeNameList();
                break;
            ////Invoice#
            //case "SchoolName":
            //    dataSource = new CSite().GetSiteNameList();
            //    break;

            // Inventory
            case "AssignedUserName":
                dataSource = new CUser().GetAssignedUserNameList();
                break;
            case "InventoryCategoryName":
                dataSource = new CInventory().GetInventoryCategoryNameList();
                break;
            case "InventoryCategoryItemName":
                dataSource = new CInventory().GetInventoryCategoryItemNameList();
                break;
            case "ConditionName":
                dataSource = new CInventory().GetConditionNameList();
                break;
            case "InUseName":
                dataSource = new CInventory().GetInUseNameList();
                break;
        }

        if (dataSource != null)
            SetFilter(e, dataField, dataSource);
    }

    private static void SetFilter(GridFilterCheckListItemsRequestedEventArgs e, string dataField, object dataSource)
    {
        e.ListBox.DataSource = dataSource;
        e.ListBox.DataKeyField = dataField;
        e.ListBox.DataTextField = dataField;
        e.ListBox.DataValueField = dataField;
        e.ListBox.DataBind();
    }

    public bool UpdateHomestayStudentStatus(int HomestayStudentId, int Status)
    {
        //Requested=1, Placed by School=2, Placed by Agency =3, Canceled =6,Schedule Changed=5,Rejected 7
        bool updateStatus = false;
        var cHomestayStudent = new CHomestayStudentRequest();
        HomestayStudentBasic HS = cHomestayStudent.GetHomestayStudentRequest(HomestayStudentId);
        HS.HomestayStudentStatus = Status;
        HS.UpdateUserId = CurrentUserId;
        HS.UpdatedDate = DateTime.Now;
        if (Status == 2 || Status == 3)
        {
            HS.PlacedUserId = CurrentUserId;
            HS.PlacedDate = DateTime.Now;
        }
        updateStatus = cHomestayStudent.Update(HS);

        return updateStatus;
    }

    public bool UpdateDormitoryStudentStatus(int DormitoryRegistrationId, int Status)
    {
        //Requested=1, Placed by School=2, Placed by Agency =3, Canceled =4,Schedule Changed=5
        bool updateStatus = false;
        var cDormitoryStudent = new CDormitoryRegistrations();
        DormitoryRegistration HS = cDormitoryStudent.GetDormitoryStudentRequest(DormitoryRegistrationId);
        HS.DormitoryStudentStatus = Status;
        HS.UpdatedId = CurrentUserId;
        HS.UpdatedDate = DateTime.Now;
        if (Status == 2 || Status == 3)
        {
            HS.PlacedUserId = CurrentUserId;
            HS.PlacedDate = DateTime.Now;
        }
        updateStatus = cDormitoryStudent.Update(HS);

        return updateStatus;
    }
    public static Control GetPostBackControl(Page page)
    {
        Control control = null;

        string ctrlname = page.Request.Params.Get("__EVENTTARGET");
        if (ctrlname != null && ctrlname != string.Empty)
        {
            control = page.FindControl(ctrlname);
        }
        else
        {
            foreach (string ctl in page.Request.Form)
            {
                Control c = page.FindControl(ctl);
                if (c is System.Web.UI.WebControls.Button)
                {
                    control = c;
                    break;
                }
            }
        }
        return control;
    }

    public void SetViewType(LinqDataSource linqDataSource, RadGrid radGrid)
    {
        string whereString = string.Empty;

        object selectedText = ViewState["SelectedTextView"];
        if (selectedText == null)
            selectedText = "All";

        linqDataSource.TableName = GetTableNameForViewType(selectedText.ToString());

        var approvalUserNameColumn = radGrid.Columns.FindByUniqueNameSafe("ApprovalUserName");

        switch (selectedText.ToString())
        {
            // All
            case "All":
                linqDataSource.OrderBy = "CreatedDate Desc";
                if (approvalUserNameColumn != null)
                    approvalUserNameColumn.Visible = false;
                break;

            // My Request
            case "My Request":
                linqDataSource.WhereParameters.Add("CreatedId", DbType.Int32, CurrentUserId.ToString());
                whereString = "CreatedId == @CreatedId";
                if (linqDataSource.Where.Length > 0)
                    whereString += " && ";
                linqDataSource.Where = linqDataSource.Where.Insert(0, whereString);
                linqDataSource.OrderBy = "CreatedDate Desc";
                if (approvalUserNameColumn != null)
                    approvalUserNameColumn.Visible = false;
                break;

            // My Approval
            case "My Approval":
                linqDataSource.WhereParameters.Add("ApprovalUser", DbType.Int32, CurrentUserId.ToString());
                linqDataSource.WhereParameters.Add("IsApprovalRequest", DbType.Boolean, bool.TrueString);
                linqDataSource.WhereParameters.Add("CreatedId", DbType.Int32, CurrentUserId.ToString());
                whereString = "(ApprovalUser == @ApprovalUser && IsApprovalRequest == @IsApprovalRequest && CreatedId != @CreatedId)";
                if (linqDataSource.Where.Length > 0)
                    whereString += " && ";
                linqDataSource.Where = linqDataSource.Where.Insert(0, whereString);

                linqDataSource.OrderBy = "ApprovalStatus, CreatedDate Desc";
                if (approvalUserNameColumn != null)
                    approvalUserNameColumn.Visible = true;
                break;
        }
    }

    private string GetTableNameForViewType(string selectedText)
    {
        bool isApprovalList = selectedText == "My Approval" ? true : false;

        string tableName = string.Empty;
        switch (_menuId)
        {
            case (int)CConstValue.Menu.PackageProgram:
                var cPackageProgram = new CPackageProgram();
                tableName = isApprovalList ? cPackageProgram.GetTableNameForVwPackageProgramApprovalList() : cPackageProgram.GetTableNameForVwPackageProgram();
                break;
            case (int)CConstValue.Menu.CreditMemo:
                var cCreditMemo = new CCreditMemoPayout();
                tableName = isApprovalList ? cCreditMemo.GetTableNameForvwCreditMemoPayoutApprovalList() : cCreditMemo.GetTableNameForVwCreditMemoPayout();
                break;
            case (int)CConstValue.Menu.Refund:
                var cRefund = new CRefund();
                tableName = isApprovalList ? cRefund.GetTableNameForVwRefundApprovalList() : cRefund.GetTableNameForVwRefund();
                break;
            case (int)CConstValue.Menu.Scholarship:
                var cScholarship = new CScholarship();
                tableName = isApprovalList ? cScholarship.GetTableNameForVwScholarshipApprovalList() : cScholarship.GetTableNameForVwScholarship();
                break;
            case (int)CConstValue.Menu.Promotion:
                var cPromotion = new CPromotion();
                tableName = isApprovalList ? cPromotion.GetTableNameForVwPromotionApprovalList() : cPromotion.GetTableNameForVwPromotion();
                break;
            case (int)CConstValue.Menu.Agency:
                var cAgency = new CAgency();
                tableName = isApprovalList ? cAgency.GetTableNameForVwAgencyApprovalList() : cAgency.GetTableNameForVwAgency();
                break;
        }

        return tableName;
    }

    protected StringBuilder ConvertFilterExpressionToSqlExpression(GridColumnCollection columns, bool isDynamicQuery = false)
    {
        var querySb = new StringBuilder();
        foreach (GridColumn column in columns)
        {
            if (column.ListOfFilterValues != null)
            {
                if (querySb.Length > 0)
                    querySb.Append(" AND ");

                querySb.Append("(");
                foreach (var c in column.ListOfFilterValues)
                {
                    querySb.Append(GetFilterExpression(GridKnownFunction.EqualTo, column.UniqueName, c, isDynamicQuery) + " OR ");
                }
                querySb.Remove(querySb.Length - 3, 3);
                querySb.Append(")");
            }

            var currentFilterExpression = GetFilterExpression(column.CurrentFilterFunction, column.UniqueName, column.CurrentFilterValue, isDynamicQuery);
            var andCurrentFilterExpression = GetFilterExpression(column.AndCurrentFilterFunction, column.UniqueName, column.AndCurrentFilterValue, isDynamicQuery);

            if (!string.IsNullOrEmpty(currentFilterExpression) || !string.IsNullOrEmpty(andCurrentFilterExpression))
            {
                if (querySb.Length > 0)
                    querySb.Append(" AND ");
            }

            switch (column.CurrentFilterFunction)
            {
                case GridKnownFunction.Between:
                    querySb.Append(currentFilterExpression + " AND " + andCurrentFilterExpression);
                    break;
                case GridKnownFunction.NotBetween:
                    querySb.Append(currentFilterExpression + " OR " + andCurrentFilterExpression);
                    break;
                default:
                    if (!string.IsNullOrEmpty(currentFilterExpression))
                        querySb.Append(currentFilterExpression);
                    if (!string.IsNullOrEmpty(andCurrentFilterExpression))
                        querySb.Append(andCurrentFilterExpression);
                    break;
            }
        }
        return querySb;
    }

    protected virtual string GetFilterExpression(GridKnownFunction filterFunction, string columnName, string columnValue, bool isDynamicQuery)
    {
        var quatationMark = isDynamicQuery ? "''" : "'";

        switch (filterFunction)
        {
            case GridKnownFunction.NoFilter:
                break;
            case GridKnownFunction.Contains:
                if (!string.IsNullOrEmpty(columnValue))
                    return $@"([{columnName}] LIKE {quatationMark}%{columnValue}%{quatationMark})";
                break;
            case GridKnownFunction.DoesNotContain:
                if (!string.IsNullOrEmpty(columnValue))
                    return $@"([{columnName}] NOT LIKE {quatationMark}%{columnValue}%{quatationMark})";
                break;
            case GridKnownFunction.StartsWith:
                if (!string.IsNullOrEmpty(columnValue))
                    return $@"([{columnName}] LIKE {quatationMark}{columnValue}%{quatationMark})";
                break;
            case GridKnownFunction.EndsWith:
                if (!string.IsNullOrEmpty(columnValue))
                    return $@"([{columnName}] LIKE {quatationMark}%{columnValue}{quatationMark})";
                break;
            case GridKnownFunction.EqualTo:
                if (!string.IsNullOrEmpty(columnValue))
                    return $@"([{columnName}] = {quatationMark}{columnValue}{quatationMark})";
                break;
            case GridKnownFunction.NotEqualTo:
                if (!string.IsNullOrEmpty(columnValue))
                    return $@"([{columnName}] <> {quatationMark}{columnValue}{quatationMark})";
                break;
            case GridKnownFunction.GreaterThan:
                if (!string.IsNullOrEmpty(columnValue))
                    return $@"([{columnName}] > {quatationMark}{columnValue}{quatationMark})";
                break;
            case GridKnownFunction.LessThan:
                if (!string.IsNullOrEmpty(columnValue))
                    return $@"([{columnName}] < {quatationMark}{columnValue}{quatationMark})";
                break;
            case GridKnownFunction.GreaterThanOrEqualTo:
                if (!string.IsNullOrEmpty(columnValue))
                    return $@"([{columnName}] >= {quatationMark}{columnValue}{quatationMark})";
                break;
            case GridKnownFunction.LessThanOrEqualTo:
                if (!string.IsNullOrEmpty(columnValue))
                    return $@"([{columnName}] <= {quatationMark}{columnValue}{quatationMark})";
                break;
            case GridKnownFunction.Between:
                break;
            case GridKnownFunction.NotBetween:
                break;
            case GridKnownFunction.IsEmpty:
                return $@"([{columnName}] = {quatationMark}{quatationMark})";
            case GridKnownFunction.NotIsEmpty:
                return $@"([{columnName}] <> {quatationMark}{quatationMark})";
            case GridKnownFunction.IsNull:
                return $@"([{columnName}] IS NULL)";
            case GridKnownFunction.NotIsNull:
                return $@"(NOT ([{columnName}] IS NULL))";
            case GridKnownFunction.Custom:
                break;
            default:
                break;
        }

        return string.Empty;
    }

}