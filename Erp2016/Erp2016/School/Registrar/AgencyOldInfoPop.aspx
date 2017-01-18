<%@ Page Title="Old Agency Lookup" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="AgencyOldInfoPop.aspx.cs" Inherits="School.Registrar.AgencyOldInfoPop" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">
        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal" ResizeMode="EndPane">

            <telerik:RadPane ID="RadPane7" runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="RadToolBar1" runat="server" OnButtonClick="RadToolBar1_ButtonClick">
                    <Items>
                        <telerik:RadToolBarButton runat="server" Text="Close" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <telerik:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <telerik:RadGrid ID="RadGridAgencyOldInfo" runat="server" AllowFilteringByColumn="True"
                    AllowPaging="True" AllowSorting="True" AllowMultiRowSelection="False" AutoGenerateColumns="False"
                    PageSize="20" DataSourceID="LinqDataSourceAgencyOldInfo" ShowFooter="false" Height="100%" OnFilterCheckListItemsRequested="RadGridAgencyOldInfo_OnFilterCheckListItemsRequested"
                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                    <MasterTableView EditMode="Batch" DataKeyNames="AgencyOldInfoId" TableLayout="Fixed" DataSourceID="LinqDataSourceAgencyOldInfo" AllowMultiColumnSorting="True">
                        <BatchEditingSettings EditType="Cell"></BatchEditingSettings>
                        <Columns>
                            <telerik:GridBoundColumn
                                HeaderText="No" DataField="AgencyOldInfoId" SortExpression="AgencyOldInfoId" UniqueName="AgencyOldInfoId"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Site" DataField="Site" SortExpression="Site" UniqueName="Site"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Old Agency Name" DataField="OldAgencyName" SortExpression="OldAgencyName" UniqueName="OldAgencyName"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="New Agency Name" DataField="NewAgencyName" SortExpression="NewAgencyName" UniqueName="NewAgencyName"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Biz Location" DataField="BizLocation" SortExpression="BizLocation" UniqueName="BizLocation"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnablePostBackOnRowClick="False">
                        <Selecting CellSelectionMode="SingleCell" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
                        <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                            ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                    </ClientSettings>
                    <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                </telerik:RadGrid>

                <asp:LinqDataSource ID="LinqDataSourceAgencyOldInfo" runat="server" ContextTypeName="Erp2016.Lib.linqDBDataContext" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" TableName="AgencyOldInfos">
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

            function Close() {
                var wnd = GetRadWindow();
                wnd.close();
            }

        </script>

    </telerik:RadCodeBlock>
</asp:Content>
