<%@ Page Title="Purchase Order" Language="C#" MasterPageFile="~/School/Shared/Base.master" AutoEventWireup="true" CodeFile="PurchaseOrder.aspx.cs" Inherits="School.OfficeAdmin.PurchaseOrder" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">

        <div style="display: none">
            <asp:Button ID="ButtonGridRefresh" runat="server" OnClick="ButtonGridRefresh_OnClick" />
        </div>

        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal" ResizeMode="EndPane">

            <telerik:RadPane ID="Radpane4" runat="server" Height="27px" Scrolling="None">
                <h4>1. My Purchase Order Request</h4>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane5" runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="RadToolBar1" runat="server"
                    OnButtonClick="ToolbarButtonClicked">
                    <Items>
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_new.png" Text="New Purchase Order" ToolTip="Request" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                    </Items>
                </telerik:RadToolBar>

            </telerik:RadPane>

            <telerik:RadPane ID="Radpane3" runat="server" Scrolling="None">

                <telerik:RadGrid ID="RadGridRequest" runat="server" AutoGenerateColumns="False" AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" PageSize="20" DataSourceID="LinqDataSourceRequest" Height="100%"
                    OnSelectedIndexChanged="RadGrid_OnSelectedIndexChanged" OnFilterCheckListItemsRequested="RadGridRequest_OnFilterCheckListItemsRequested"
                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                    <MasterTableView DataKeyNames="PurchaseOrderId, ApprovalStatus" TableLayout="Fixed" DataSourceID="LinqDataSourceRequest" AllowMultiColumnSorting="True">
                        <Columns>
                            <telerik:GridBoundColumn
                                HeaderText="No" DataField="PurchaseOrderId" SortExpression="PurchaseOrderId" UniqueName="PurchaseOrderId"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Type" DataField="PurchaseOrderTypeName" SortExpression="PurchaseOrderTypeName" UniqueName="PurchaseOrderTypeName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Priority" DataField="PriorityTypeName" SortExpression="PriorityTypeName" UniqueName="PriorityTypeName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridNumericColumn
                                HeaderText="Amount" DataField="Amount" SortExpression="Amount" UniqueName="Amount" HeaderStyle-HorizontalAlign="Right"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" DataFormatString="{0:$#,##0.00}" ItemStyle-HorizontalAlign="Right">
                            </telerik:GridNumericColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Review" DataField="ReviewTypeName" SortExpression="ReviewTypeName" UniqueName="ReviewTypeName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn
                                HeaderText="Created Date" DataField="CreatedDate" SortExpression="CreatedDate" UniqueName="CreatedDate"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy} {0:t}">
                            </telerik:GridDateTimeColumn>
                            <telerik:GridDateTimeColumn
                                HeaderText="Approval Date" DataField="ApprovalDate" SortExpression="ApprovalDate" UniqueName="ApprovalDate"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy} {0:t}">
                            </telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Status" DataField="Status" SortExpression="Status" UniqueName="Status"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Memo" DataField="ApprovalMemo" SortExpression="ApprovalMemo" UniqueName="ApprovalMemo"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
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
                <asp:LinqDataSource ID="LinqDataSourceRequest" runat="server"
                    ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName=""
                    TableName="vwPurchaseOrderRequestLists" OrderBy="CreatedDate Descending"
                    Where="PurchaseOrderId == @PurchaseOrderId">
                    <WhereParameters>
                        <asp:Parameter DefaultValue="0" Name="PurchaseOrderId" Type="Int32" />
                    </WhereParameters>
                </asp:LinqDataSource>

            </telerik:RadPane>

            <telerik:RadSplitBar ID="RadSplitBar1" runat="server" EnableResize="True" CollapseMode="Both" />

            <telerik:RadPane ID="Radpane2" runat="server" Height="27px" Scrolling="None">
                <h4>2. My Purchase Order Approval</h4>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane1" runat="server" Scrolling="None">
                <telerik:RadGrid ID="RadGridApproval" runat="server" AutoGenerateColumns="False" AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" PageSize="20" DataSourceID="LinqDataSourceApproval" Height="100%"
                    OnSelectedIndexChanged="RadGrid_OnSelectedIndexChanged" OnFilterCheckListItemsRequested="RadGridApproval_OnFilterCheckListItemsRequested"
                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                    <MasterTableView DataKeyNames="PurchaseOrderId, ApprovalStatus" TableLayout="Fixed" DataSourceID="LinqDataSourceApproval" AllowMultiColumnSorting="True">
                        <Columns>
                            <telerik:GridBoundColumn
                                HeaderText="No" DataField="PurchaseOrderId" SortExpression="PurchaseOrderId" UniqueName="PurchaseOrderId"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="UserName" DataField="UserName" SortExpression="UserName" UniqueName="UserName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Type" DataField="PurchaseOrderTypeName" SortExpression="PurchaseOrderTypeName" UniqueName="PurchaseOrderTypeName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Priority" DataField="PriorityTypeName" SortExpression="PriorityTypeName" UniqueName="PriorityTypeName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridNumericColumn
                                HeaderText="Amount" DataField="Amount" SortExpression="Amount" UniqueName="Amount" HeaderStyle-HorizontalAlign="Right"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" DataFormatString="{0:$#,##0.00}" ItemStyle-HorizontalAlign="Right">
                            </telerik:GridNumericColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Review" DataField="ReviewTypeName" SortExpression="ReviewTypeName" UniqueName="ReviewTypeName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn
                                HeaderText="Created Date" DataField="CreatedDate" SortExpression="CreatedDate" UniqueName="CreatedDate"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy} {0:t}">
                            </telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Status" DataField="Status" SortExpression="Status" UniqueName="Status"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Memo" DataField="ApprovalMemo" SortExpression="ApprovalMemo" UniqueName="ApprovalMemo"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Next Approval UserName" DataField="ApprovalUserName" SortExpression="ApprovalUserName" UniqueName="ApprovalUserName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
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
                <asp:LinqDataSource ID="LinqDataSourceApproval" runat="server"
                    ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName=""
                    TableName="vwPurchaseOrderApprovalLists" OrderBy="CreatedDate Descending"
                    Where="PurchaseOrderId == @PurchaseOrderId">
                    <WhereParameters>
                        <asp:Parameter DefaultValue="0" Name="PurchaseOrderId" Type="Int32" />
                    </WhereParameters>
                </asp:LinqDataSource>

            </telerik:RadPane>

        </telerik:RadSplitter>
    </telerik:RadAjaxPanel>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function ShowNewPop(purchaseOrderId, createOrListType, requestOrApprovalType, approvalType) {
                var oWnd = window.radopen('PurchaseOrderPop?id=' + purchaseOrderId + '&createOrListType=' + createOrListType + '&requestOrApprovalType=' + requestOrApprovalType + '&approvalType=' + approvalType + '', 0, 0, 0, 0);
                var displayWidth = $(window).width() * 0.95;
                var displayHeight = $(window).height() * 0.95;
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
