<%@ Page Title="Hire for HQ" Language="C#" MasterPageFile="~/School/Shared/Base.master" AutoEventWireup="true" CodeFile="HireForHq.aspx.cs" Inherits="School.OfficeAdmin.HireForHq" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>


<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">

        <div style="display: none">
            <asp:Button ID="ButtonGridRefresh" runat="server" OnClick="ButtonGridRefresh_OnClick" />
        </div>

        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal" ResizeMode="EndPane">

            <telerik:RadPane ID="Radpane4" runat="server" Height="27px" Scrolling="None">
                <h4>1. Hire for HQ</h4>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane1" runat="server" Scrolling="None">
                <telerik:RadGrid ID="RadGridApproval" runat="server" AutoGenerateColumns="False" AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" PageSize="50" DataSourceID="LinqDataSourceHireHq" Height="100%"
                    OnSelectedIndexChanged="RadGrid_OnSelectedIndexChanged" OnFilterCheckListItemsRequested="RadGridApproval_OnFilterCheckListItemsRequested"
                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true" ExportSettings-IgnorePaging="true" ClientSettings-EnableClientPrint="true" ExportSettings-OpenInNewWindow="True">
                    <GroupingSettings CaseSensitive="false"></GroupingSettings>
                    <MasterTableView DataKeyNames="No, ApprovalStatus" TableLayout="Fixed" DataSourceID="LinqDataSourceHireHq" AllowMultiColumnSorting="True" CommandItemDisplay="Bottom">
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
                            <telerik:GridBoundColumn
                                HeaderText="Position" DataField="Position" SortExpression="Position" UniqueName="Position"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Job Title" DataField="JobTitle" SortExpression="JobTitle" UniqueName="JobTitle"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
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
                <asp:LinqDataSource ID="LinqDataSourceHireHq" runat="server"
                    ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName=""
                    TableName="vwHireHqLists" OrderBy="CreatedDate Descending"
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

            function ShowNewPop(hireId, createOrListType, requestOrApprovalType, approvalType) {
                var oWnd = window.radopen('HirePop?id=' + hireId + '&createOrListType=' + createOrListType + '&requestOrApprovalType=' + requestOrApprovalType + '&approvalType=' + approvalType + '', 0, 0, 0, 0);
                var displayWidth = $(window).width() * 0.95;
                var displayHeight = $(window).height() * 0.95;
                if (displayWidth > 1500)
                    displayWidth = 1500;
                oWnd.setSize(displayWidth, displayHeight);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Maximize + Telerik.Web.UI.WindowBehaviors.Resize + Telerik.Web.UI.WindowBehaviors.Move);
                oWnd.add_close(OnClientCloseHandler);
                return false;
            }

            <%-- function OnClientClose(oWnd, args) {
                    var arg = args.get_argument();
                             <%=Page.GetPostBackEventReference(btnRefresh)%>
                }--%>

            function OnClientCloseHandler(sender, args) {
                <%--   var gridAccom = $find('<%=RadGrid1.ClientID%>');
                    gridAccom.get_batchEditingManager().saveChanges(gridAccom.get_masterTableView());

                    var gridAccom = $find('<%=RadGrid2.ClientID%>');
                    gridAccom.get_batchEditingManager().saveChanges(gridAccom.get_masterTableView());--%>


                //var data = args.get_argument();
                //if (data) {
                //    alert(data);
                //}

                //location.reload();

                <%= Page.GetPostBackEventReference(ButtonGridRefresh) %>;

                <%--var bt = $find("<%= ButtonGridRefresh.ClientID %>");
                    bt.click();--%>

                <%--var masterTable1 = $find("<%= RadGrid1.ClientID %>").get_masterTableView();
                    masterTable1.rebind();
                    var masterTable2 = $find("<%= RadGrid2.ClientID %>").get_masterTableView();
                    masterTable2.rebind();--%>
            }

        </script>

    </telerik:RadCodeBlock>

</asp:Content>
