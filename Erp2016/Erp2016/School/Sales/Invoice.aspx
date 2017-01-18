<%@ Page Title="Invoice" Language="C#" MasterPageFile="~/School/Shared/Base.master" AutoEventWireup="true" CodeFile="Invoice.aspx.cs" Inherits="School.Sales.Invoice" %>


<%@ Register TagPrefix="UserControl" TagName="InvoiceItemGrid" Src="~/App_Data/InvoiceItemGrid.ascx" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>


<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadButtonExcel">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadButtonExcel" LoadingPanelID="RadButtonExcel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadButtonExcelDetail">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadButtonExcelDetail" LoadingPanelID="RadButtonExcelDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">

        <div style="display: none">
            <asp:Button ID="ButtonGridRefresh" runat="server" OnClick="ButtonGridRefresh_OnClick" />
        </div>

        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="Radpane9" runat="server" Height="27px" Scrolling="None">
                <h4>Invoice List</h4>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane5" runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="RadToolBar1" runat="server" OnClientButtonClicking="ToolbarButtonClick" OnButtonClick="InvoiceToolbarButtonClicked">
                    <Items>
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_save.png" Text="Confirm" ToolTip="Invoice Confirm" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/icon_s_edit.png" Text="Modify" ToolTip="Invoice Modify" Value="Modify" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_delete.png" Text="Cancel" ToolTip="Invoice Cancel" Value="Cancel" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_print.png" Text="Student Invoice" ToolTip="Student Invoice" Value="Student" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_print.png" Text="Agency Invoice" ToolTip="Agency Invoice" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_reg.png" Text="New Simple Invoice" ToolTip="New Simple Invoice" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/Students-20.png" Text="Student Page" ToolTip="Student Page" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/Paid-20.png" Text="Payment Page" ToolTip="Payment Page" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/Debt-20.png" Text="Deposit Page" ToolTip="Deposit Page" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/Sales Performance-20.png" Text="CreditMemo Page" ToolTip="CreditMemo Page" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/Receive Cash-20.png" Text="Refund Page" ToolTip="Refund Page" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                        <telerik:RadToolBarButton runat="server" Text="Excel" Visible="false">
                            <ItemTemplate>
                                <telerik:RadButton runat="server" ID="RadButtonExcel" OnClick="RadButtonExcel_OnClick" Text="Export to excel" />
                            </ItemTemplate>
                        </telerik:RadToolBarButton>
                        <telerik:RadToolBarButton runat="server" Text="ExcelDetail" Visible="false">
                            <ItemTemplate>
                                <telerik:RadButton runat="server" ID="RadButtonExcelDetail" OnClick="RadButtonExcelDetail_OnClick" Text="Export to excel for pivot" />
                            </ItemTemplate>
                        </telerik:RadToolBarButton>
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane1" runat="server" Height="40%" Scrolling="None">
                <telerik:RadGrid ID="RadGridInvoice" runat="server" AllowFilteringByColumn="True" Height="100%" AllowMultiRowSelection="True"
                    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="20" OnPreRender="RadGridInvoice_OnPreRender" OnSelectedIndexChanged="RadGridInvoice_OnSelectedIndexChanged"
                    DataSourceID="LinqDataSourceInvoice" ShowFooter="false" OnFilterCheckListItemsRequested="RadGridInvoice_OnFilterCheckListItemsRequested"
                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                    <MasterTableView DataKeyNames="InvoiceId, StudentId" TableLayout="Fixed" DataSourceID="LinqDataSourceInvoice">
                        <Columns>
                            <telerik:GridBoundColumn
                                HeaderText="Invoice No" DataField="InvoiceNumber" SortExpression="InvoiceNumber" UniqueName="InvoiceNumber"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Site" DataField="SiteName" SortExpression="SiteName" UniqueName="SiteName"
                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Site Location" DataField="SiteLocationName" SortExpression="SiteLocationName" UniqueName="SiteLocationName"
                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Status" DataField="InvoiceStatus" SortExpression="InvoiceStatus" UniqueName="InvoiceStatus"
                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Invoice Type" DataField="InvoiceType" SortExpression="InvoiceType" UniqueName="InvoiceType"
                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Student No" DataField="StudentNo" SortExpression="StudentNo" UniqueName="StudentNo"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Student Name" DataField="StudentName" SortExpression="StudentName" UniqueName="StudentName"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Country" DataField="CountryName" SortExpression="CountryName" UniqueName="CountryName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Agency" DataField="AgencyName" SortExpression="AgencyName" UniqueName="AgencyName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Program" DataField="InvoiceName" SortExpression="InvoiceName" UniqueName="InvoiceName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
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
                            <telerik:GridCheckBoxColumn
                                HeaderText="FG" DataField="IsFinancialGurantee" SortExpression="IsFinancialGurantee" UniqueName="IsFinancialGurantee"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Created User" DataField="CreatedUsername" SortExpression="CreatedUsername" UniqueName="CreatedUsername"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn
                                HeaderText="Created Date" DataField="CreatedDate" SortExpression="CreatedDate" UniqueName="CreatedDate"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy} {0:t}">
                            </telerik:GridDateTimeColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="True">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                        <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                            ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                    </ClientSettings>
                    <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                </telerik:RadGrid>
                <asp:LinqDataSource ID="LinqDataSourceInvoice" runat="server"
                    ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName="" OrderBy="InvoiceId Descending"
                    TableName="vwInvoices"
                    Where="InvoiceId == @InvoiceId">
                    <WhereParameters>
                        <asp:Parameter DefaultValue="0" Name="InvoiceId" Type="Int32" />
                    </WhereParameters>
                </asp:LinqDataSource>
            </telerik:RadPane>

            <telerik:RadSplitBar ID="Radsplitbar1" runat="server" CollapseMode="Both" EnableResize="true" />

            <telerik:RadPane ID="Radpane2" runat="server" Scrolling="None">
                <telerik:RadSplitter ID="RadSplitterInvoice" runat="server" Orientation="Vertical">
                    <telerik:RadPane ID="RadPaneInvoice" runat="server" Scrolling="None">
                        <telerik:RadSplitter ID="RadSplitterItems" runat="server" Orientation="Horizontal">
                            <telerik:RadPane ID="RadpaneInvoiceItems" runat="server" Scrolling="None">
                                <UserControl:InvoiceItemGrid ID="InvoiceItemGrid1" runat="server" />
                            </telerik:RadPane>
                        </telerik:RadSplitter>
                    </telerik:RadPane>

                    <telerik:RadSplitBar ID="RadSplitBar2" runat="server" CollapseMode="None" EnableResize="true" />

                    <telerik:RadPane ID="RadPane4" runat="server" Width="50%" Scrolling="None">

                        <telerik:RadSplitter runat="server" Orientation="Horizontal" ResizeMode="EndPane">

                            <telerik:RadPane ID="RadPane7" runat="server" Height="250px">

                                <telerik:RadDockLayout runat="server" ID="AgencyDockLayout">
                                    <telerik:RadDockZone ID="RadDockZone1" runat="server" Orientation="Vertical">

                                        <telerik:RadDock ID="RadDock2" runat="server" Title="Financial Guarantee" Width="100%" EnableAnimation="true" EnableRoundedCorners="true" Resizable="False" Closed="false" DefaultCommands="None" EnableDrag="False">
                                            <ContentTemplate>
                                                <div class="formStyle3">
                                                    <label>Financial Guarantee</label>
                                                    <telerik:RadComboBox ID="ddlFG" CssClass="RadSizeMiddle" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFG_OnSelectedIndexChanged">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="No" Value="False" />
                                                            <telerik:RadComboBoxItem Text="Yes" Value="True" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                </div>
                                            </ContentTemplate>
                                        </telerik:RadDock>

                                        <telerik:RadDock ID="AgencyDock" runat="server" Title="Agency Info" Width="100%" EnableAnimation="true" EnableRoundedCorners="true" Resizable="False" Closed="false" DefaultCommands="None" EnableDrag="False">
                                            <ContentTemplate>
                                                <div class="formStyle3">
                                                    <div style="float: left; width: 100%;">
                                                        <div>
                                                            <div style="float: left; width: 50%;">
                                                                <label>Agency Name</label>
                                                                <telerik:RadTextBox ID="tbAgencyName" runat="server" CssClass="RadSizeMiddle" Text="" ReadOnly="true"></telerik:RadTextBox>
                                                                <br style="clear: both;" />
                                                            </div>
                                                            <div style="float: left; width: 50%;">
                                                                <label>Country</label>
                                                                <telerik:RadTextBox ID="tbCountryCity" runat="server" CssClass="RadSizeMiddle" Text="" ReadOnly="true"></telerik:RadTextBox>
                                                                <br style="clear: both;" />
                                                            </div>
                                                        </div>
                                                        <div>
                                                            <div style="float: left; width: 50%;">
                                                                <label>Contract Period</label>
                                                                <telerik:RadTextBox ID="tbContractDate" runat="server" CssClass="RadSizeMiddle" Text="" ReadOnly="true"></telerik:RadTextBox>
                                                                <br style="clear: both;" />
                                                            </div>
                                                            <div style="float: left; width: 50%;">
                                                                <label>Commission Rate</label>
                                                                <telerik:RadTextBox ID="tbCommissionRate" runat="server" CssClass="RadSizeMiddle" Text="" ReadOnly="true"></telerik:RadTextBox>
                                                                <br style="clear: both;" />
                                                            </div>
                                                        </div>
                                                        <div>
                                                            <label>Description</label>
                                                            <telerik:RadTextBox ID="tbDescription" runat="server" TextMode="MultiLine" CssClass="RadSizeMultiLine" Text="" ReadOnly="true"></telerik:RadTextBox>
                                                            <br style="clear: both;" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </telerik:RadDock>

                                    </telerik:RadDockZone>

                                </telerik:RadDockLayout>
                            </telerik:RadPane>

                            <telerik:RadSplitBar ID="RadSplitBar3" runat="server" CollapseMode="None" EnableResize="true" />

                            <telerik:RadPane ID="RadPane6" runat="server" Height="27px" Scrolling="None">
                                <h4>Invoice history</h4>
                            </telerik:RadPane>

                            <telerik:RadPane ID="RadPane8" runat="server" Scrolling="None">

                                <telerik:RadGrid ID="RadGridInvoiceHistory" runat="server" AllowFilteringByColumn="True" Height="100%"
                                    AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False" PageSize="20" OnSelectedIndexChanged="RadGridInvoiceHistory_OnSelectedIndexChanged"
                                    DataSourceID="LinqDataSourceInvoiceHistory" ShowFooter="false" OnFilterCheckListItemsRequested="RadGridInvoiceHistory_OnFilterCheckListItemsRequested"
                                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                                    <MasterTableView DataKeyNames="InvoiceId" TableLayout="Fixed" DataSourceID="LinqDataSourceInvoiceHistory">
                                        <Columns>
                                            <telerik:GridBoundColumn
                                                HeaderText="Invoice No" DataField="InvoiceNumber" SortExpression="InvoiceNumber" UniqueName="InvoiceNumber"
                                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn
                                                HeaderText="Site" DataField="SiteName" SortExpression="SiteName" UniqueName="SiteName"
                                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn
                                                HeaderText="Site Location" DataField="SiteLocationName" SortExpression="SiteLocationName" UniqueName="SiteLocationName"
                                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn
                                                HeaderText="Status" DataField="InvoiceStatus" SortExpression="InvoiceStatus" UniqueName="InvoiceStatus"
                                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn
                                                HeaderText="Invoice Type" DataField="InvoiceType" SortExpression="InvoiceType" UniqueName="InvoiceType"
                                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn
                                                HeaderText="Student No" DataField="StudentNo" SortExpression="StudentNo" UniqueName="StudentNo"
                                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn
                                                HeaderText="Student Name" DataField="StudentName" SortExpression="StudentName" UniqueName="StudentName"
                                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn
                                                HeaderText="Country" DataField="CountryName" SortExpression="CountryName" UniqueName="CountryName"
                                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn
                                                HeaderText="Agency" DataField="AgencyName" SortExpression="AgencyName" UniqueName="AgencyName"
                                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn
                                                HeaderText="Program" DataField="InvoiceName" SortExpression="InvoiceName" UniqueName="InvoiceName"
                                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                            </telerik:GridBoundColumn>
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
                                            <telerik:GridCheckBoxColumn
                                                HeaderText="FG" DataField="IsFinancialGurantee" SortExpression="IsFinancialGurantee" UniqueName="IsFinancialGurantee"
                                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" HeaderStyle-Font-Size="X-Small">
                                            </telerik:GridCheckBoxColumn>
                                            <telerik:GridBoundColumn
                                                HeaderText="Created User" DataField="CreatedUsername" SortExpression="CreatedUsername" UniqueName="CreatedUsername"
                                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridDateTimeColumn
                                                HeaderText="Created Date" DataField="CreatedDate" SortExpression="CreatedDate" UniqueName="CreatedDate"
                                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                                PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy} {0:t}">
                                            </telerik:GridDateTimeColumn>
                                        </Columns>
                                    </MasterTableView>

                                    <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="true" />
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                                        <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                                            ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                                    </ClientSettings>
                                    <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                                </telerik:RadGrid>

                                <asp:LinqDataSource ID="LinqDataSourceInvoiceHistory" runat="server"
                                    ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName="" OrderBy="InvoiceId Descending"
                                    TableName="vwInvoices"
                                    Where="InvoiceId == @InvoiceId">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="0" Name="InvoiceId" Type="Int32" />
                                    </WhereParameters>
                                </asp:LinqDataSource>
                            </telerik:RadPane>
                        </telerik:RadSplitter>

                    </telerik:RadPane>
                </telerik:RadSplitter>

            </telerik:RadPane>
        </telerik:RadSplitter>
    </telerik:RadAjaxPanel>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            //            // call when page load.
            //            function pageLoad() {
            //                var grid = $find("<%= RadGridInvoice.ClientID %>");
            //                var columns = grid.get_masterTableView().get_columns();
            //                for (var i = 0; i < columns.length; i++) {
            //                    columns[i].resizeToFit(false, true);
            //                }
            //            }

            function ToolbarButtonClick(sender, args) {
                //function toolbarButtonCallbackFn(arg) {
                //    if (arg) {

                //    }
                //}
                var button = args.get_item();
                if (button.get_text() === "Confirm") {
                    if (!confirm('Do you want to confirm the Invoice?' + '\n\n' + 'Important!!! You need to save first when you modified items.'))
                        args.set_cancel(true);
                }
                else if (button.get_text() === "Cancel") {
                    if (!confirm('Do you want to cancel the Invoice?'))
                        args.set_cancel(true);
                }
                else if (button.get_text() === "Modify") {
                    //window.radconfirm("Do you want to modify the Invoice?", toolbarButtonCallbackFn, 330, 180, null, button.get_text());
                    if (!confirm('Do you want to modify the Invoice?'))
                        args.set_cancel(true);
                }
                else if (button.get_text() === "New Manual Invoice") {
                    if (!confirm('Do you want to create Manual Invoice?'))
                        args.set_cancel(true);
                }

            }

            function ShowCancelWindow(invoiceId) {
                var oWnd = window.radopen('StudentCancelPop?id=' + invoiceId, 0, 0, 0, 0);
                //var displayWidth = $(window).width() * 0.95;
                //var displayHeight = $(window).height() * 0.95;
                //if (displayWidth > 1500)
                //    displayWidth = 1500;
                var displayWidth = 500;
                var displayHeight = 400;
                oWnd.setSize(displayWidth, displayHeight);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Maximize + Telerik.Web.UI.WindowBehaviors.Resize + Telerik.Web.UI.WindowBehaviors.Move);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowReportPop(invoiceIdArray, reportType) {
                var oWnd = window.radopen('ReportPop?id=' + invoiceIdArray + '&reportType=' + reportType, 0, 0, 0, 0);
                var displayWidth = $(window).width() * 0.95;
                var displayHeight = $(window).height() * 0.95;

                if (displayWidth > 850)
                    displayWidth = 850;

                oWnd.setSize(displayWidth, displayHeight);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Maximize + Telerik.Web.UI.WindowBehaviors.Resize + Telerik.Web.UI.WindowBehaviors.Move);
                //oWnd.add_close(OnClientCloseHandler);
                return false;
            }

            function ShowInvoiceWindow(invoiceId) {
                var oWnd = window.radopen('InvoiceItemGridPop?id=' + invoiceId, 0, 0, 0, 0);
                //var displayWidth = $(window).width() * 0.95;
                //var displayHeight = $(window).height() * 0.95;

                var displayWidth = 800;
                var displayHeight = 500;

                if (displayWidth > 1500)
                    displayWidth = 1500;
                oWnd.setSize(displayWidth, displayHeight);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Maximize + Telerik.Web.UI.WindowBehaviors.Resize + Telerik.Web.UI.WindowBehaviors.Move);
                //oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowNewSimpleInvoice() {
                var oWnd = window.radopen('SimpleInvoiceNewPop?', 0, 0, 0, 0);
                //var displayWidth = $(window).width() * 0.95;
                //var displayHeight = $(window).height() * 0.95;

                var displayWidth = 1200;
                var displayHeight = 700;

                if (displayWidth > 1500)
                    displayWidth = 1500;
                oWnd.setSize(displayWidth, displayHeight);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Maximize + Telerik.Web.UI.WindowBehaviors.Resize + Telerik.Web.UI.WindowBehaviors.Move);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function OnClientClose(oWnd, args) {
                var arg = args.get_argument();
                <%= Page.GetPostBackEventReference(ButtonGridRefresh) %>;
            }

        </script>
    </telerik:RadCodeBlock>

</asp:Content>
