﻿<%@ Page Title="ERP" Language="C#" MasterPageFile="~/School/Shared/Base.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="School.Home.Dashboard" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">

        <div style="display: none">
            <asp:Button ID="ButtonRefresh" runat="server" OnClick="ButtonGridRefresh_OnClick" />
        </div>

        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal" ResizeMode="EndPane">

            <telerik:RadPane ID="Radpane1" runat="server" Height="50%" Scrolling="None">
                <telerik:RadSplitter runat="server" ID="RadSplitter2" Orientation="Vertical">

                    <telerik:RadPane ID="Radpane5" runat="server" Width="50%" Scrolling="None">

                        <telerik:RadSplitter runat="server" ID="RadSplitter6" Orientation="Horizontal">

                            <telerik:RadPane ID="Radpane11" runat="server" Height="27px" Scrolling="None">
                                <h4>BulletinBoard</h4>
                            </telerik:RadPane>

                            <telerik:RadPane ID="Radpane15" runat="server" Height="40px" Scrolling="None">
                                <telerik:RadToolBar ID="RadToolBarBulletinBoard" runat="server" OnButtonClick="RadToolBarBulletinBoard_OnButtonClick">
                                    <Items>
                                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_new.png" Text="New post" ToolTip="New BulletinBoard" />
                                        <telerik:RadToolBarButton IsSeparator="true" />
                                    </Items>
                                </telerik:RadToolBar>
                            </telerik:RadPane>

                            <telerik:RadPane ID="Radpane12" runat="server" Scrolling="None">
                                <telerik:RadGrid ID="GridBulletinBoard" DataSourceID="LinqDataSourceBulletinBoard" runat="server" PageSize="20" AutoGenerateColumns="false" AllowPaging="true" Height="100%" 
                                    AllowCustomPaging="false" AllowSorting="true" AllowFilteringByColumn="True" EnableLinqExpressions="false" OnSelectedIndexChanged="GridBulletinBoard_OnSelectedIndexChanged" OnFilterCheckListItemsRequested="GridBulletinBoard_OnFilterCheckListItemsRequested"
                                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                                    <MasterTableView DataSourceID="LinqDataSourceBulletinBoard" TableLayout="Fixed" DataKeyNames="BulletinBoardId">
                                        <Columns>
                                            <telerik:GridBoundColumn
                                                HeaderText="No" DataField="BulletinBoardId" SortExpression="BulletinBoardId" UniqueName="BulletinBoardId"
                                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn
                                                HeaderText="Subject" DataField="Subject" SortExpression="Subject" UniqueName="Subject" HeaderStyle-Width="50%"
                                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn
                                                HeaderText="Created User" DataField="UserName" SortExpression="UserName" UniqueName="UserName"
                                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridDateTimeColumn
                                                HeaderText="Created Date" DataField="CreatedDate" SortExpression="CreatedDate" UniqueName="CreatedDate"
                                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                                PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy} {0:t}">
                                            </telerik:GridDateTimeColumn>
                                            <telerik:GridBoundColumn
                                                HeaderText="Views" DataField="Views" SortExpression="Views" UniqueName="Views"
                                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>

                                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                                        <Selecting AllowRowSelect="true" />
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="True" />
                                        <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                                            ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                                    </ClientSettings>
                                    <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="true" />
                                </telerik:RadGrid>
                            </telerik:RadPane>

                        </telerik:RadSplitter>

                        <asp:LinqDataSource ID="LinqDataSourceBulletinBoard" runat="server"
                            ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName="" TableName="vwBulletinBoards" OrderBy="CreatedDate DESC">
                        </asp:LinqDataSource>

                    </telerik:RadPane>

                    <%--<telerik:RadSplitBar ID="RadSplitBar1" runat="server" EnableResize="True" CollapseMode="None" />

                    <telerik:RadPane ID="Radpane6" runat="server" Scrolling="None">

                      <telerik:RadSplitter runat="server" ID="RadSplitter7" Orientation="Horizontal">

                            <telerik:RadPane ID="Radpane13" runat="server" Height="27px" Scrolling="None">
                                <h4>Infomation for each school</h4>
                            </telerik:RadPane>

                            <telerik:RadPane ID="Radpane14" runat="server" Scrolling="None">

                                <div style="float: left; width: 50%; height: 100%">
                                    <telerik:RadHtmlChart runat="server" ID="RadHtmlChartUsers" Width="100%" Height="100%">
                                        <ChartTitle Text="User count">
                                            <Appearance Align="Center" Position="Bottom">
                                            </Appearance>
                                        </ChartTitle>
                                        <Legend>
                                            <Appearance Position="Right" Visible="true">
                                            </Appearance>
                                        </Legend>

                                        <PlotArea>
                                            <Series>

                                                <telerik:FunnelSeries DynamicHeightEnabled="False" DynamicSlopeEnabled="False" SegmentSpacing="5">
                                                    <SeriesItems>
                                                    </SeriesItems>
                                                    <LabelsAppearance Align="Center" Position="Center" Color="Black"></LabelsAppearance>
                                                    <TooltipsAppearance>
                                                        <ClientTemplate>#= category #</ClientTemplate>
                                                    </TooltipsAppearance>
                                                </telerik:FunnelSeries>

                                            </Series>
                                        </PlotArea>
                                    </telerik:RadHtmlChart>
                                </div>

                                <div style="float: left; width: 50%; height: 100%">

                                    <telerik:RadHtmlChart runat="server" ID="RadHtmlChartStudents" Width="100%" Height="100%" Transitions="true">
                                        <ChartTitle Text="Student count">
                                            <Appearance Align="Center" Position="Bottom">
                                            </Appearance>
                                        </ChartTitle>
                                        <Legend>
                                            <Appearance Position="Right" Visible="true">
                                            </Appearance>
                                        </Legend>

                                        <PlotArea>
                                            <Series>

                                                <telerik:FunnelSeries DynamicHeightEnabled="False" DynamicSlopeEnabled="false" SegmentSpacing="5" NeckRatio="3">
                                                    <SeriesItems>
                                                    </SeriesItems>
                                                    <LabelsAppearance Align="Center" Position="Center" Color="Black"></LabelsAppearance>
                                                    <TooltipsAppearance>
                                                        <ClientTemplate>#= category #</ClientTemplate>
                                                    </TooltipsAppearance>
                                                </telerik:FunnelSeries>

                                            </Series>
                                        </PlotArea>
                                    </telerik:RadHtmlChart>
                                </div>
                            </telerik:RadPane>
                        </telerik:RadSplitter>
                    </telerik:RadPane>--%>
                </telerik:RadSplitter>
            </telerik:RadPane>

            <telerik:RadSplitBar ID="RadSplitBar2" runat="server" EnableResize="True" CollapseMode="None" />

            <telerik:RadPane ID="Radpane4" runat="server" Scrolling="None">

                <telerik:RadSplitter runat="server" ID="RadSplitter3" Orientation="Vertical">

                    <telerik:RadPane ID="Radpane7" runat="server" Width="50%" Scrolling="None">

                        <telerik:RadSplitter runat="server" ID="RadSplitter5" Orientation="Horizontal">

                            <telerik:RadPane ID="Radpane9" runat="server" Height="27px" Scrolling="None">
                                <h4>Waiting for request</h4>
                            </telerik:RadPane>

                            <telerik:RadPane ID="Radpane10" runat="server" Height="40%" Scrolling="None">

                                <telerik:RadGrid ID="RadGridRequest" runat="server" AutoGenerateColumns="False" AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" PageSize="20" DataSourceID="LinqDataSourceRequest" Height="100%"
                                    OnSelectedIndexChanged="RadGridApprovalHistory_OnSelectedIndexChanged" OnFilterCheckListItemsRequested="RadGridRequest_OnFilterCheckListItemsRequested"
                                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                                    <MasterTableView DataKeyNames="TypeId, No" TableLayout="Fixed" DataSourceID="LinqDataSourceRequest" AllowMultiColumnSorting="True" ShowHeadersWhenNoRecords="false">
                                        <Columns>
                                            <telerik:GridBoundColumn
                                                HeaderText="No" DataField="No" SortExpression="No" UniqueName="No"
                                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn
                                                HeaderText="Type" DataField="Type" SortExpression="Type" UniqueName="Type"
                                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
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
                                            <telerik:GridBoundColumn
                                                HeaderText="Status" DataField="Status" SortExpression="Status" UniqueName="Status"
                                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn
                                                HeaderText="Approval User Name" DataField="ApprovalUserName" SortExpression="ApprovalUserName" UniqueName="ApprovalUserName"
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
                                    <%--<PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />--%>
                                </telerik:RadGrid>

                                <asp:LinqDataSource ID="LinqDataSourceRequest" runat="server"
                                    ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName=""
                                    TableName="vwApprovalHistoryForWaitings" OrderBy="CreatedDate Descending"
                                    Where="No == @No">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="0" Name="No" Type="Int32" />
                                    </WhereParameters>
                                </asp:LinqDataSource>

                            </telerik:RadPane>

                        </telerik:RadSplitter>
                    </telerik:RadPane>

                    <telerik:RadSplitBar ID="RadSplitBar3" runat="server" EnableResize="True" CollapseMode="None" />

                    <telerik:RadPane ID="Radpane8" runat="server" Scrolling="None">

                        <telerik:RadSplitter runat="server" ID="RadSplitter4" Orientation="Horizontal">

                            <telerik:RadPane ID="Radpane2" runat="server" Height="27px" Scrolling="None">
                                <h4>Waiting for approval</h4>
                            </telerik:RadPane>

                            <telerik:RadPane ID="Radpane3" runat="server" Height="40%" Scrolling="None">

                                <telerik:RadGrid ID="RadGridApproval" runat="server" AutoGenerateColumns="False" AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" PageSize="20" DataSourceID="LinqDataSourceApproval" Height="100%"
                                    OnSelectedIndexChanged="RadGridApprovalHistory_OnSelectedIndexChanged" OnFilterCheckListItemsRequested="RadGridApproval_OnFilterCheckListItemsRequested"
                                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                                    <MasterTableView DataKeyNames="TypeId, No" TableLayout="Fixed" DataSourceID="LinqDataSourceApproval" AllowMultiColumnSorting="True" ShowHeadersWhenNoRecords="false">
                                        <Columns>
                                            <telerik:GridBoundColumn
                                                HeaderText="No" DataField="No" SortExpression="No" UniqueName="No"
                                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn
                                                HeaderText="Type" DataField="Type" SortExpression="Type" UniqueName="Type"
                                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
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
                                            <telerik:GridBoundColumn
                                                HeaderText="Status" DataField="Status" SortExpression="Status" UniqueName="Status"
                                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn
                                                HeaderText="Approval User Name" DataField="ApprovalUserName" SortExpression="ApprovalUserName" UniqueName="ApprovalUserName"
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
                                    <%--<PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />--%>
                                </telerik:RadGrid>

                                <asp:LinqDataSource ID="LinqDataSourceApproval" runat="server"
                                    ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName=""
                                    TableName="vwApprovalHistoryForWaitings" OrderBy="CreatedDate Descending"
                                    Where="No == @No">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="0" Name="No" Type="Int32" />
                                    </WhereParameters>
                                </asp:LinqDataSource>

                            </telerik:RadPane>

                        </telerik:RadSplitter>

                    </telerik:RadPane>

                </telerik:RadSplitter>
            </telerik:RadPane>

        </telerik:RadSplitter>
    </telerik:RadAjaxPanel>

    <telerik:RadCodeBlock ID="RadCodeBlock" runat="server">
        <script type="text/javascript">

            function ShowPop(bulletinBoardId, createOrListType) {
                var oWnd = window.radopen('BulletinBoardPop?id=' + bulletinBoardId + '&createOrListType=' + createOrListType, 0, 0, 0, 0);
                var displayWidth = $(window).width() * 0.95;
                var displayHeight = $(window).height() * 0.95;
                if (displayWidth > 800)
                    displayWidth = 800;

                //oWnd.setSize(displayWidth * getZoomScale(), displayHeight * getZoomScale());
                //oWnd.moveTo(0, 0);

                oWnd.setSize(displayWidth, displayHeight);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Maximize + Telerik.Web.UI.WindowBehaviors.Resize + Telerik.Web.UI.WindowBehaviors.Move);
                oWnd.add_close(OnClientCloseHandler);
                return false;
            }

            function OnClientCloseHandler(sender, args) {
                <%= Page.GetPostBackEventReference(ButtonRefresh) %>;
            }

        </script>
    </telerik:RadCodeBlock>

</asp:Content>
