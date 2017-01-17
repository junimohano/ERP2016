<%@ Page Title="Inventory" Language="C#" MasterPageFile="~/School/Shared/Base.master" AutoEventWireup="true" CodeFile="Inventory.aspx.cs" Inherits="School.OfficeAdmin.Inventory" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">

        <div style="display: none">
            <asp:Button ID="ButtonGridRefresh" runat="server" OnClick="ButtonGridRefresh_OnClick" />
        </div>

        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal" ResizeMode="EndPane">

            <telerik:RadPane ID="Radpane4" runat="server" Height="27px" Scrolling="None">
                <h4>Inventory</h4>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane5" runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="RadToolBar1" runat="server" OnButtonClick="ToolbarButtonClicked">
                    <Items>
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_new.png" Text="New Inventory" ToolTip="New Inventory" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                    </Items>
                </telerik:RadToolBar>

            </telerik:RadPane>

            <telerik:RadPane ID="Radpane3" runat="server" Scrolling="None">

                <telerik:RadGrid ID="RadGridInventory" runat="server" AutoGenerateColumns="False" AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" PageSize="20" DataSourceID="LinqDataSourceInventory" Height="100%"
                    OnSelectedIndexChanged="RadGrid_OnSelectedIndexChanged" OnFilterCheckListItemsRequested="RadGridInventory_OnFilterCheckListItemsRequested"
                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true" ExportSettings-IgnorePaging="true" ClientSettings-EnableClientPrint="true" ExportSettings-OpenInNewWindow="True">
                    <MasterTableView DataKeyNames="InventoryId" TableLayout="Fixed" DataSourceID="LinqDataSourceInventory" AllowMultiColumnSorting="True" CommandItemDisplay="Bottom">
                        <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="false" ShowCancelChangesButton="False" ShowSaveChangesButton="False"
                            ShowExportToExcelButton="True" ShowExportToCsvButton="True" ShowExportToPdfButton="True" ShowExportToWordButton="True" ShowPrintButton="True" />
                        <Columns>
                            <telerik:GridBoundColumn
                                HeaderText="Inventory Code" DataField="InventoryCode" SortExpression="InventoryCode" UniqueName="InventoryCode"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Site" DataField="SiteName" SortExpression="SiteName" UniqueName="SiteName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Site Location" DataField="SiteLocationName" SortExpression="SiteLocationName" UniqueName="SiteLocationName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Category" DataField="InventoryCategoryName" SortExpression="InventoryCategoryName" UniqueName="InventoryCategoryName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Category Item" DataField="InventoryCategoryItemName" SortExpression="InventoryCategoryItemName" UniqueName="InventoryCategoryItemName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Assigned User" DataField="AssignedUserName" SortExpression="AssignedUserName" UniqueName="AssignedUserName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Department / Area" DataField="Department" SortExpression="Department" UniqueName="Department"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn
                                HeaderText="Purchased Date" DataField="PurchasedDate" SortExpression="PurchasedDate" UniqueName="PurchasedDate"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy} {0:t}">
                            </telerik:GridDateTimeColumn>
                            <telerik:GridNumericColumn
                                HeaderText="Price" DataField="Price" SortExpression="Price" UniqueName="Price"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" DataFormatString="{0:$#,##0.00}" ItemStyle-HorizontalAlign="Right">
                            </telerik:GridNumericColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Condition" DataField="ConditionName" SortExpression="ConditionName" UniqueName="ConditionName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="In Use" DataField="InUseName" SortExpression="InUseName" UniqueName="InUseName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Created User" DataField="CreatedUserName" SortExpression="CreatedUserName" UniqueName="CreatedUserName"
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
                        <Selecting AllowRowSelect="True" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                        <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                            ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                    </ClientSettings>
                    <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                </telerik:RadGrid>
                <asp:LinqDataSource ID="LinqDataSourceInventory" runat="server"
                    ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName=""
                    TableName="vwInventories" OrderBy="CreatedDate Descending">
                </asp:LinqDataSource>
            </telerik:RadPane>

        </telerik:RadSplitter>
    </telerik:RadAjaxPanel>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function ShowNewPop(inventoryId, createOrListType) {
                var oWnd = window.radopen('InventoryPop?id=' + inventoryId + '&createOrListType=' + createOrListType, 0, 0, 0, 0);
                oWnd.setSize(1000, 700);
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
