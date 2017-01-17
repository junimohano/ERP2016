<%@ Page Title="Corporate Credit Card For HQ" Language="C#" MasterPageFile="~/School/Shared/Base.master" AutoEventWireup="true" CodeFile="CorporateCreditCardForHq.aspx.cs" Inherits="School.OfficeAdmin.CorporateCreditCardForHq" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>


<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">

        <div style="display: none">
            <asp:Button ID="ButtonGridRefresh" runat="server" OnClick="ButtonGridRefresh_OnClick" />
        </div>

        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal" ResizeMode="EndPane">

            <telerik:RadPane ID="Radpane3" runat="server" Height="27px" Scrolling="None">
                <div>
                    <div style="float: left;">
                        <h4>Corporate Credit Card for HQ</h4>
                    </div>
                    <%--<div style="float: right;">
                        <telerik:RadButton runat="server" ID="RadButtonExcel" OnClick="RadButtonExcel_OnClick" Text="List Download" />
                        <telerik:RadButton runat="server" ID="RadButtonDetailExcel" OnClick="RadButtonDetailExcel_OnClick" Text="Detail List Download" />
                    </div>--%>
                </div>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane1" runat="server" Scrolling="None">
                <telerik:RadGrid ID="RadGridList" runat="server" AutoGenerateColumns="False" AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" PageSize="50" DataSourceID="LinqDataSourceHq" Height="100%"
                    OnSelectedIndexChanged="RadGridList_OnSelectedIndexChanged" OnFilterCheckListItemsRequested="RadGridList_OnFilterCheckListItemsRequested"
                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true" ExportSettings-IgnorePaging="true" ClientSettings-EnableClientPrint="true" ExportSettings-OpenInNewWindow="True">
                    <MasterTableView DataKeyNames="No, ApprovalStatus" TableLayout="Fixed" DataSourceID="LinqDataSourceHq" AllowMultiColumnSorting="True" CommandItemDisplay="Bottom">
                        <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="false" ShowCancelChangesButton="False" ShowSaveChangesButton="False"
                            ShowExportToExcelButton="True" ShowExportToCsvButton="True" ShowExportToPdfButton="True" ShowExportToWordButton="True" ShowPrintButton="True" />
                        <Columns>
                            <telerik:GridBoundColumn
                                HeaderText="No" DataField="No" SortExpression="No" UniqueName="No"
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
                                HeaderText="User Name" DataField="UserName" SortExpression="UserName" UniqueName="UserName"
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
                                HeaderText="Check No" DataField="CheckNo" SortExpression="CheckNo" UniqueName="CheckNo"
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
                <asp:LinqDataSource ID="LinqDataSourceHq" runat="server"
                    ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName=""
                    TableName="vwCorporateCreditCardHqLists" OrderBy="CreatedDate Descending"
                    Where="No == @No">
                    <WhereParameters>
                        <asp:Parameter DefaultValue="0" Name="No" Type="Int32" />
                    </WhereParameters>
                </asp:LinqDataSource>
            </telerik:RadPane>

        </telerik:RadSplitter>
    </telerik:RadAjaxPanel>


    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement && window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }

            function ShowNewPop(expenseId, createOrListType, requestOrApprovalType, approvalType) {
                var oWnd = window.radopen('CorporateCreditCardPop?id=' + expenseId + '&createOrListType=' + createOrListType + '&requestOrApprovalType=' + requestOrApprovalType + '&approvalType=' + approvalType + '', 0, 0, 0, 0);
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

            function OnClientClose(sender, args) {
                <%= Page.GetPostBackEventReference(ButtonGridRefresh) %>;
            }

            function Close() {
                var wnd = GetRadWindow();
                wnd.close();
            }

        </script>

    </telerik:RadCodeBlock>

</asp:Content>
