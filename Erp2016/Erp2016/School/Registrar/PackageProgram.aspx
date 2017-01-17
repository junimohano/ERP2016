<%@ Page Title="Packages" Language="C#" MasterPageFile="~/School/Shared/Base.master" AutoEventWireup="true" CodeFile="PackageProgram.aspx.cs" Inherits="School.Registrar.PackageProgram" %>

<%@ Import Namespace="Erp2016.Lib" %>

<%@ Register TagPrefix="usercontrol" TagName="approvalline" Src="~/App_Data/ApprovalLine.ascx" %>
<%@ Register TagPrefix="UserControl" TagName="FileDownloadList" Src="~/App_Data/FileDownloadList.ascx" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">

        <telerik:RadSplitter runat="server" ID="RadSplitter4" Height="100%" Width="100%" Orientation="Horizontal">
            <telerik:RadPane ID="Radpane2" runat="server" Scrolling="None">

                <telerik:RadSplitter ID="Radsplitter2" runat="server" Orientation="Horizontal">

                    <telerik:RadPane ID="Radpane1" runat="server" Height="27px" Scrolling="None">
                        <h4>Package List</h4>
                    </telerik:RadPane>

                    <telerik:RadPane ID="Radpane5" runat="server" Height="40px" Scrolling="None">
                        <telerik:RadToolBar ID="RadToolBarPackageProgram" runat="server" OnButtonClick="RadToolBarPackageProgram_OnButtonClick" OnClientButtonClicking="ToolbarButtonClick">
                            <Items>
                                <telerik:RadToolBarButton runat="server" Text="View">
                                    <ItemTemplate>
                                        <telerik:RadDropDownList runat="server" ID="RadDropDownListView" AutoPostBack="True" OnSelectedIndexChanged="RadDropDownListView_OnSelectedIndexChanged">
                                            <Items>
                                                <telerik:DropDownListItem Text="All" Selected="True" />
                                                <telerik:DropDownListItem Text="My Request" />
                                                <telerik:DropDownListItem Text="My Approval" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </ItemTemplate>
                                </telerik:RadToolBarButton>

                                <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_new.png" Text="New Package" />
                                <telerik:RadToolBarButton IsSeparator="true" />
                                <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_mark.png" Text="Request" Enabled="False" />
                                <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_delete.png" Text="Cancel" Enabled="False" />
                                <telerik:RadToolBarButton IsSeparator="true" />
                                <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_excute.png" Text="Approve" Enabled="False" />
                                <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_clear.png" Text="Reject" Enabled="False" />
                                <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_back.png" Text="Revise" Enabled="False" />
                            </Items>
                        </telerik:RadToolBar>
                    </telerik:RadPane>

                    <telerik:RadPane ID="Radpane3" runat="server" Height="35%" Scrolling="None">
                        <telerik:RadGrid ID="RadGridProgramPackage" runat="server" AutoGenerateColumns="False" AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" PageSize="20" DataSourceID="LinqDataSourceProgramPackage" Height="100%"
                            OnSelectedIndexChanged="RadGrid_OnSelectedIndexChanged" OnFilterCheckListItemsRequested="RadGridProgramPackage_OnFilterCheckListItemsRequested"
                            FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                            <MasterTableView DataKeyNames="PackageProgramId" TableLayout="Fixed" DataSourceID="LinqDataSourceProgramPackage">
                                <Columns>
                                    <telerik:GridBoundColumn
                                        HeaderText="No" DataField="PackageProgramId" SortExpression="PackageProgramId" UniqueName="PackageProgramId"
                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn
                                        HeaderText="Site" DataField="SiteName" SortExpression="SiteName" UniqueName="SiteName"
                                        FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn
                                        HeaderText="Status" DataField="Status" SortExpression="Status" UniqueName="Status"
                                        FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn
                                        HeaderText="Package Program Name" DataField="PackageProgramName" SortExpression="PackageProgramName" UniqueName="PackageProgramName"
                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridNumericColumn
                                        HeaderText="Standard Price" DataField="StandardPrice" SortExpression="StandardPrice" UniqueName="StandardPrice"
                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" DataFormatString="{0:$#,##0.00}" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridNumericColumn>
                                    <telerik:GridNumericColumn
                                        HeaderText="Student Price" DataField="StudentPrice" SortExpression="StudentPrice" UniqueName="StudentPrice"
                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" DataFormatString="{0:$#,##0.00}" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridNumericColumn>
                                    <telerik:GridNumericColumn
                                        HeaderText="Agency Price" DataField="AgencyPrice" SortExpression="AgencyPrice" UniqueName="AgencyPrice"
                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" DataFormatString="{0:$#,##0.00}" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridNumericColumn>
                                    <telerik:GridDateTimeColumn
                                        HeaderText="Start Date" DataField="StartDate" SortExpression="StartDate" UniqueName="StartDate"
                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                        PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy}">
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridDateTimeColumn
                                        HeaderText="End Date" DataField="EndDate" SortExpression="EndDate" UniqueName="EndDate"
                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                        PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy}">
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridBoundColumn
                                        HeaderText="Created User" DataField="UserName" SortExpression="UserName" UniqueName="UserName"
                                        FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridDateTimeColumn
                                        HeaderText="Created Date" DataField="CreatedDate" SortExpression="CreatedDate" UniqueName="CreatedDate"
                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                        PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy} {0:t}">
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridCheckBoxColumn
                                        HeaderText="IsActive" DataField="IsActive" SortExpression="IsActive" UniqueName="IsActive"
                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridCheckBoxColumn>
                                    <telerik:GridBoundColumn
                                        HeaderText="Approval UserName" DataField="ApprovalUserName" SortExpression="ApprovalUserName" UniqueName="ApprovalUserName" Visible="False"
                                        FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="True">
                                <Selecting AllowRowSelect="true" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True"></Scrolling>
                                <ClientEvents OnRowSelected="RadGridProgramPackage_RowSelected"></ClientEvents>
                                <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                                    ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                            </ClientSettings>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                        </telerik:RadGrid>
                        <asp:LinqDataSource ID="LinqDataSourceProgramPackage" runat="server"
                            ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName=""
                            TableName="vwPackagePrograms" OrderBy="CreatedDate Descending"
                            Where="PackageProgramId == @PackageProgramId">
                            <WhereParameters>
                                <asp:Parameter DefaultValue="0" Name="PackageProgramId" Type="Int32" />
                            </WhereParameters>
                        </asp:LinqDataSource>
                    </telerik:RadPane>

                    <telerik:RadSplitBar ID="RadSplitBar1" runat="server" EnableResize="True" CollapseMode="Both" />

                    <telerik:RadPane runat="server" Scrolling="None">
                        <telerik:RadSplitter ID="Radsplitter3" runat="server" Orientation="Vertical">
                            <telerik:RadPane runat="server" Scrolling="None" Width="60%">
                                <telerik:RadSplitter ID="Radsplitter1" runat="server" Orientation="Horizontal">
                                    <telerik:RadPane runat="server" Height="27px" Scrolling="None">
                                        <h4>Package Detail</h4>
                                    </telerik:RadPane>

                                    <telerik:RadPane runat="server" Scrolling="None">
                                        <telerik:RadGrid ID="RadGridPackageProgramDetail" Height="100%" runat="server" AllowAutomaticDeletes="true" AllowAutomaticInserts="true" AllowAutomaticUpdates="true" AllowPaging="False" AutoGenerateColumns="false" DataSourceID="LinqDataSourceProgramDetail" OnBatchEditCommand="RadGridPackageProgramDetail_OnBatchEditCommand" PageSize="20" ShowFooter="True"
                                            OnItemDataBound="RadGridPackageProgramDetail_OnItemDataBound" OnPreRender="RadGridPackageProgramDetail_OnPreRender" AllowFilteringByColumn="True" OnFilterCheckListItemsRequested="RadGridPackageProgramDetail_OnFilterCheckListItemsRequested"
                                            FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                                            <MasterTableView AutoGenerateColumns="false" CommandItemDisplay="Top" CommandItemSettings-AddNewRecordText="Add Item" CommandItemSettings-SaveChangesText="Save Changes Item" DataKeyNames="PackageProgramDetailId" DataSourceID="LinqDataSourceProgramDetail" EditMode="Batch" HorizontalAlign="NotSet">
                                                <BatchEditingSettings EditType="Row" />
                                                <CommandItemSettings ShowSaveChangesButton="True" AddNewRecordText="Add Item" SaveChangesText="Save Changes Item" />
                                                <Columns>

                                                    <telerik:GridTemplateColumn
                                                        HeaderText="Invoice Item" DataField="InvoiceCoaItemId" SortExpression="InvoiceCoaItemId" UniqueName="InvoiceCoaItemId" DefaultInsertValue="Commission" HeaderStyle-HorizontalAlign="Center"
                                                        AllowFiltering="False" FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" HeaderStyle-Width="30%">
                                                        <ItemTemplate>
                                                            <telerik:RadDropDownList runat="server" DropDownHeight="200px" Width="100%" DataValueField="InvoiceCoaItemId" DataTextField="ItemDetail" DataSourceID="LinqDataSourceInvoice" SelectedValue='<%# Eval("InvoiceCoaItemId") %>' CssClass="BodyCell" Enabled="False" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadComboBox ID="ddlInvoiceItem" Width="100%" runat="server" AutoPostBack="False" DataValueField="InvoiceCoaItemId" DataTextField="ItemDetail" DataSourceID="LinqDataSourceInvoice" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            Total
                                                        </FooterTemplate>
                                                    </telerik:GridTemplateColumn>

                                                    <telerik:GridTemplateColumn
                                                        HeaderText="Standard Price" DataField="StandardPrice" SortExpression="StandardPrice" UniqueName="StandardPrice"
                                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStandardPrice" Text='<%# string.Format("{0:$#,##0.00}", Eval("StandardPrice")) %>' Width="100%" runat="server" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadNumericTextBox ID="radNumTextBoxStandardPrice" runat="server" Type="Currency" Width="100%">
                                                                <ClientEvents OnValueChanged="RadGridPackageProgramDetail_ValueChanged" />
                                                            </telerik:RadNumericTextBox>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <telerik:RadNumericTextBox runat="server" HoveredStyle-HorizontalAlign="Right" ReadOnly="true" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Right" Type="Currency" Width="100%">
                                                                <ClientEvents OnLoad=" StandardPriceLoad " />
                                                            </telerik:RadNumericTextBox>
                                                        </FooterTemplate>
                                                    </telerik:GridTemplateColumn>

                                                    <telerik:GridTemplateColumn
                                                        HeaderText="Student Price" DataField="StudentPrice" SortExpression="StudentPrice" UniqueName="StudentPrice"
                                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStudentPrice" Text='<%# string.Format("{0:$#,##0.00}", Eval("StudentPrice")) %>' Width="100%" runat="server" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadNumericTextBox ID="radNumTextBoxStudentPrice" runat="server" Type="Currency" Width="100%">
                                                                <ClientEvents OnValueChanged="RadGridPackageProgramDetail_ValueChanged" />
                                                            </telerik:RadNumericTextBox>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <telerik:RadNumericTextBox runat="server" HoveredStyle-HorizontalAlign="Right" ReadOnly="true" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Right" Type="Currency" Width="100%">
                                                                <ClientEvents OnLoad=" StudentPriceLoad " />
                                                            </telerik:RadNumericTextBox>
                                                        </FooterTemplate>
                                                    </telerik:GridTemplateColumn>

                                                    <telerik:GridTemplateColumn HeaderText="Agency Price" DataField="AgencyPrice" SortExpression="AgencyPrice" UniqueName="AgencyPrice"
                                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAgencyPrice" Text='<%# string.Format("{0:$#,##0.00}", Eval("AgencyPrice")) %>' Width="100%" runat="server" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadNumericTextBox ID="radNumTextBoxAgencyPrice" runat="server" Type="Currency" Width="100%">
                                                                <ClientEvents OnValueChanged="RadGridPackageProgramDetail_ValueChanged" />
                                                            </telerik:RadNumericTextBox>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <telerik:RadNumericTextBox runat="server" HoveredStyle-HorizontalAlign="Right" ReadOnly="true" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Right" Type="Currency" Width="100%">
                                                                <ClientEvents OnLoad=" AgencyPriceLoad " />
                                                            </telerik:RadNumericTextBox>
                                                        </FooterTemplate>
                                                    </telerik:GridTemplateColumn>

                                                    <telerik:GridTemplateColumn
                                                        HeaderText="Remark" DataField="Remark" SortExpression="Remark" UniqueName="Remark"
                                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label Text='<%# DataBinder.Eval(Container.DataItem, "Remark") %>' Width="100%" runat="server" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadTextBox runat="server" Width="100%" />
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>

                                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="150px" ConfirmDialogType="RadWindow" ConfirmText="Delete this Item?" ConfirmTitle="Delete" HeaderStyle-Width="30px" HeaderText="Del" Text="Delete" UniqueName="DeleteColumn">
                                                        <HeaderStyle Width="30px" />
                                                    </telerik:GridButtonColumn>

                                                </Columns>
                                            </MasterTableView>

                                            <ClientSettings EnableRowHoverStyle="True">
                                                <ClientEvents OnCommand="RadGridPackageProgramDetail_Command" OnGridCreated="RadGridPackageProgramDetail_GridCreated" OnRowDeleted="RadGridPackageProgramDetail_RowDeleted" />
                                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True"></Scrolling>
                                                <Resizing AllowColumnResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                                                    ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                                                <%--AllowRowResize="True"--%>
                                            </ClientSettings>
                                            <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                                        </telerik:RadGrid>

                                        <asp:LinqDataSource ID="LinqDataSourceProgramDetail" runat="server" ContextTypeName="Erp2016.Lib.linqDBDataContext" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" TableName="PackageProgramDetails"
                                            Where="PackageProgramDetailId == @PackageProgramDetailId">
                                            <WhereParameters>
                                                <asp:Parameter DefaultValue="0" Name="PackageProgramDetailId" Type="Int32" />
                                            </WhereParameters>
                                        </asp:LinqDataSource>
                                        <asp:LinqDataSource ID="LinqDataSourceInvoice" runat="server" ContextTypeName="Erp2016.Lib.linqDBDataContext" EntityTypeName="" Select="new (InvoiceCoaItemId, ItemDetail)" TableName="InvoiceCoaItems">
                                            <WhereParameters>
                                                <asp:Parameter DefaultValue="1" Name="InvoiceCoaItemId" Type="Int32" />
                                            </WhereParameters>
                                        </asp:LinqDataSource>
                                    </telerik:RadPane>

                                </telerik:RadSplitter>
                            </telerik:RadPane>

                            <telerik:RadSplitBar ID="RadSplitBar2" runat="server" EnableResize="True" CollapseMode="Both" />

                            <telerik:RadPane runat="server" Scrolling="None">
                                <telerik:RadSplitter runat="server" Orientation="Horizontal">

                                    <telerik:RadPane ID="RadPane6" runat="server" Height="125px">
                                        <UserControl:approvalline ID="ApprovalLine1" runat="server" />
                                    </telerik:RadPane>

                                    <telerik:RadPane ID="RadPane4" runat="server">
                                        <div class="formStyle3">
                                            <fieldset>
                                                <legend>Information</legend>

                                                <div style="float: left; width: 100%;">
                                                    <div>
                                                        <label>Site</label>
                                                        <telerik:RadTextBox ID="RadTextBoxSite" CssClass="RadSizeMiddle" runat="server" ReadOnly="True"></telerik:RadTextBox>
                                                        <br style="clear: both;" />
                                                    </div>
                                                    <div>
                                                        <label>Site Location</label>
                                                        <telerik:RadComboBox ID="RadComboBoxSiteLocation" CssClass="RadSizeMiddle" Enabled="False" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxSiteLocation_OnSelectedIndexChanged" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" />
                                                        <asp:Literal ID="itemsClientSide" runat="server" />
                                                        <br style="clear: both;" />
                                                    </div>
                                                    <div>
                                                        <label>Package Name</label>
                                                        <telerik:RadTextBox ID="LabelPackageProgramName" CssClass="RadSizeLarge" runat="server" ReadOnly="True"></telerik:RadTextBox>
                                                        <br style="clear: both;" />
                                                    </div>
                                                    <div>
                                                        <label>Faculty</label>
                                                        <telerik:RadTextBox ID="RadTextBoxFaculty" ReadOnly="True" CssClass="RadSizeLarge" runat="server" />
                                                    </div>
                                                    <div>
                                                        <label>Program Group</label>
                                                        <telerik:RadTextBox ID="RadTextBoxProgramGroup" ReadOnly="True" CssClass="RadSizeLarge" runat="server" />
                                                    </div>
                                                    <div>
                                                        <label>Program</label>
                                                        <telerik:RadTextBox ID="RadTextBoxProgram" ReadOnly="True" CssClass="RadSizeLarge" runat="server" />
                                                    </div>
                                                    <div>
                                                        <label>Package Start Date</label>
                                                        <telerik:RadTextBox ID="LabelPackageStartDate" CssClass="RadSizeMiddle" runat="server" ReadOnly="True"></telerik:RadTextBox>
                                                        <br style="clear: both;" />
                                                    </div>
                                                    <div>
                                                        <label>Package End Date</label>
                                                        <telerik:RadTextBox ID="LabelPackageEndDate" CssClass="RadSizeMiddle" runat="server" ReadOnly="True"></telerik:RadTextBox>
                                                        <br style="clear: both;" />
                                                    </div>
                                                    <div>
                                                        <label style="width: 150px;">Description</label>
                                                        <telerik:RadTextBox ID="LabelDescription" CssClass="RadSizeMultiLine" runat="server" ReadOnly="True" TextMode="MultiLine"></telerik:RadTextBox>
                                                        <br style="clear: both;" />
                                                    </div>
                                                </div>
                                            </fieldset>

                                            <UserControl:FileDownloadList ID="FileDownloadList1" runat="server" />
                                        </div>
                                    </telerik:RadPane>

                                </telerik:RadSplitter>

                            </telerik:RadPane>
                        </telerik:RadSplitter>
                    </telerik:RadPane>

                </telerik:RadSplitter>
            </telerik:RadPane>

        </telerik:RadSplitter>
    </telerik:RadAjaxPanel>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function ToolbarButtonClick(sender, args) {
                var button = args.get_item();
                //if (button.get_text() === "Request") {
                //    if (!confirm('Do you want to request ?'))
                //        args.set_cancel(true);
                //}
            }

            // jQuery
            $(window).bind("load", function () {
                SetProgramPackageValue();
            });

            // =====================
            // total sum
            // =====================
            var sumStandardInput = null;

            function StandardPriceLoad(sender, args) {
                sumStandardInput = sender;
            }

            var sumStudentPriceInput = null;

            function StudentPriceLoad(sender, args) {
                sumStudentPriceInput = sender;
            }

            var sumAgencyPriceInput = null;

            function AgencyPriceLoad(sender, args) {
                sumAgencyPriceInput = sender;
            }

            function GetCellValue(row, columnUniqueName, controlId) {
                var value;
                var testControl = row.findControl(controlId);
                if (testControl) {
                    value = testControl.get_value();
                } else {
                    value = row.get_cell(columnUniqueName).innerText.replace(/[^\d.-]/g, '');
                }
                if (value == "")
                    value = 0;
                return parseFloat(value);
            }

            // set value
            function SetProgramPackageValue() {
                var grid = $find("<%= RadGridPackageProgramDetail.ClientID %>"); //grid id

                if (grid) {
                    var masterTable = grid.get_masterTableView();
                    var rows = masterTable.get_dataItems();

                    var totalMountStandard = 0.0;
                    var totalMountStudent = 0.0;
                    var totalMountAgency = 0.0;
                    for (var i = 0; i < rows.length; i++) {
                        totalMountStandard = totalMountStandard + GetCellValue(rows[i], "StandardPrice", "radNumTextBoxStandardPrice");
                        totalMountStudent = totalMountStudent + GetCellValue(rows[i], "StudentPrice", "radNumTextBoxStudentPrice");
                        totalMountAgency = totalMountAgency + GetCellValue(rows[i], "AgencyPrice", "radNumTextBoxAgencyPrice");
                    }
                    sumStandardInput.set_value(totalMountStandard);
                    sumStudentPriceInput.set_value(totalMountStudent);
                    sumAgencyPriceInput.set_value(totalMountAgency);
                }
            }

            // when grid created
            function RadGridPackageProgramDetail_GridCreated(sender, eventArgs) {
                SetProgramPackageValue();
            }

            // action
            function RadGridPackageProgramDetail_Command(sender, eventArgs) {
                if (eventArgs.get_commandName() == "Rebind")
                    SetProgramPackageValue();
            }

            // delete
            function RadGridPackageProgramDetail_RowDeleted(sender, eventArgs) {
                SetProgramPackageValue();
            }

            // change value
            function RadGridPackageProgramDetail_ValueChanged(sender, eventArgs) {
                SetProgramPackageValue();
            }

            function RadGridProgramPackage_RowSelected(sender, eventArgs) {
                SetProgramPackageValue();
            }

            // == end total sum ==

            function ShowPop(id, type) {
                var oWnd = window.radopen('PackageProgramPop?id=' + id + '&type=' + type, 0, 0, 0, 0);
                oWnd.setSize(700, 700);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move + Telerik.Web.UI.WindowBehaviors.Maximize + Telerik.Web.UI.WindowBehaviors.Resize);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function OnClientClose(oWnd, args) {
                //var arg = args.get_argument();
            <%--<%=Page.GetPostBackEventReference(btnRefresh)%>--%>

                ProgramPackageRebind();

            }

            function ProgramPackageRebind() {
                var masterTable = $find("<%= RadGridProgramPackage.ClientID %>").get_masterTableView();
                masterTable.rebind();
            }

            function OnClientCloseHandler(sender, args) {

            }

            function Close() {
                var wnd = GetRadWindow();
                wnd.close();
            }

            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement && window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }

            function ShowApprovalWindow(id) {
                var oWnd = window.radopen('ApprovalApprovePop?type=' + <%= (int)CConstValue.Approval.Package %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalRejectWindow(id) {
                var oWnd = window.radopen('ApprovalRejectPop?type=' + <%= (int)CConstValue.Approval.Package %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalReviseWindow(id) {
                var oWnd = window.radopen('ApprovalRevisePop?type=' + <%= (int)CConstValue.Approval.Package %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalCancelWindow(id) {
                var oWnd = window.radopen('ApprovalCancelPop?type=' + <%= (int)CConstValue.Approval.Package %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

        </script>

    </telerik:RadCodeBlock>

</asp:Content>
