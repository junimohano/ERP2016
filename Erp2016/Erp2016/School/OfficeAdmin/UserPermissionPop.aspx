<%@ Page Title="User Permission" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="UserPermissionPop.aspx.cs" Inherits="School.OfficeAdmin.UserPermissionPop" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">

        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal" ResizeMode="EndPane">

            <telerik:RadPane runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="RadToolBarPermission" runat="server" OnClientButtonClicking="ToolbarButtonClick" OnButtonClick="RadToolBarPermission_OnButtonClick">
                    <Items>
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_back.png" Text="Reset" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                        <telerik:RadToolBarButton Text="Close" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <telerik:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <telerik:RadSplitter ID="Radsplitter3" runat="server" Orientation="Vertical">

                    <telerik:RadPane ID="RadPane3" runat="server" Width="20%" Scrolling="Y">
                        <fieldset>
                            <legend>Menu Type</legend>
                            <telerik:RadComboBox ID="RadComboBoxMenu" Width="100%" Height="500px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadComboBoxMenu_OnSelectedIndexChanged" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" />
                            <asp:Literal ID="itemsClientSide" runat="server" />
                        </fieldset>
                    </telerik:RadPane>

                    <telerik:RadSplitBar ID="Radsplitbar2" runat="server" CollapseMode="Both" EnableResize="true" />

                    <telerik:RadPane ID="RadPane1" runat="server" Scrolling="None">
                        <telerik:RadSplitter ID="Radsplitter2" runat="server" Orientation="Horizontal">

                            <telerik:RadPane ID="RadPane4" runat="server" Height="80px">
                                <fieldset>
                                    <legend>Permission</legend>
                                    <telerik:RadCheckBox runat="server" ID="RadCheckBoxAccess" Text="Access Permission" Value="0" AutoPostBack="True" OnCheckedChanged="RadCheckBoxAccess_OnCheckedChanged" OnClientCheckedChanging="RadCheckBoxAccess_OnClientCheckedChanging" />
                                    <telerik:RadCheckBox runat="server" ID="RadCheckBoxModify" Text="Modify Permission" Value="1" AutoPostBack="True" OnCheckedChanged="RadCheckBoxModify_OnCheckedChanged" OnClientCheckedChanging="RadCheckBoxModify_OnClientCheckedChanging" />
                                </fieldset>
                            </telerik:RadPane>

                            <telerik:RadPane ID="RadPane12" runat="server" Scrolling="None" Height="40%">
                                <telerik:RadSplitter ID="Radsplitter6" runat="server" Orientation="Horizontal">

                                    <telerik:RadPane ID="RadPane6" runat="server" Height="27px" Scrolling="None">
                                        <h4>Search Permission</h4>
                                    </telerik:RadPane>

                                    <telerik:RadPane ID="RadPane7" runat="server" Height="40%" Scrolling="None">
                                        <telerik:RadSplitter ID="Radsplitter5" runat="server" Orientation="Vertical">

                                            <telerik:RadPane ID="RadPane9" runat="server" Height="50%" Scrolling="None">

                                                <telerik:RadGrid ID="RadGridSiteLocation" runat="server" AllowFilteringByColumn="True" OnPreRender="RadGridSiteLocation_OnPreRender" OnRowDrop="RadGridSiteLocation_OnRowDrop"
                                                    AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False" Height="100%" PageSize="20" AllowMultiRowSelection="True"
                                                    ShowFooter="False" OnFilterCheckListItemsRequested="RadGridSiteLocation_OnFilterCheckListItemsRequested"
                                                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                                                    <MasterTableView DataKeyNames="SiteLocationId" TableLayout="Fixed">
                                                        <Columns>
                                                            <telerik:GridBoundColumn
                                                                HeaderText="Site" DataField="SiteName" SortExpression="SiteName" UniqueName="SiteName"
                                                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn
                                                                HeaderText="Site Location" DataField="SiteLocationName" SortExpression="SiteLocationName" UniqueName="SiteLocationName"
                                                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                                            </telerik:GridBoundColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings EnableRowHoverStyle="True" AllowRowsDragDrop="True">
                                                        <Scrolling AllowScroll="true" SaveScrollPosition="true" UseStaticHeaders="true" />
                                                        <Selecting AllowRowSelect="True" />
                                                        <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                                                            ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                                                        <ClientEvents OnRowDropping="RadGridSiteLocation_OnRowDropping"></ClientEvents>
                                                    </ClientSettings>
                                                    <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                                                </telerik:RadGrid>
                                            </telerik:RadPane>

                                            <telerik:RadPane ID="RadPane8" runat="server" Scrolling="None">
                                                <telerik:RadGrid ID="RadGridSiteLocationUser" runat="server" AllowFilteringByColumn="True" OnRowDrop="RadGridSiteLocationUser_OnRowDrop"
                                                    AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False" Height="100%" PageSize="20" AllowMultiRowSelection="True" OnPreRender="RadGridSiteLocationUser_OnPreRender"
                                                    DataSourceID="LinqDataSourceSiteLocationUser" ShowFooter="False" OnFilterCheckListItemsRequested="RadGridSiteLocationUser_OnFilterCheckListItemsRequested"
                                                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                                                    <MasterTableView DataKeyNames="UserPermissionId, SiteLocationId" TableLayout="Fixed" DataSourceID="LinqDataSourceSiteLocationUser">
                                                        <Columns>
                                                            <telerik:GridBoundColumn
                                                                HeaderText="Site" DataField="SiteName" SortExpression="SiteName" UniqueName="SiteName"
                                                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn
                                                                HeaderText="Site Location" DataField="SiteLocationName" SortExpression="SiteLocationName" UniqueName="SiteLocationName"
                                                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                                            </telerik:GridBoundColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings EnableRowHoverStyle="True" AllowRowsDragDrop="True">
                                                        <Scrolling AllowScroll="true" SaveScrollPosition="true" UseStaticHeaders="true" />
                                                        <Selecting AllowRowSelect="True" />
                                                        <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                                                            ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                                                        <ClientEvents OnRowDropping="RadGridSiteLocationUser_OnRowDropping"></ClientEvents>
                                                    </ClientSettings>
                                                    <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                                                </telerik:RadGrid>
                                                <asp:LinqDataSource ID="LinqDataSourceSiteLocationUser" runat="server"
                                                    ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName=""
                                                    TableName="vwUserPermissionSiteLocationUserLists"
                                                    Where="UserPermissionId == @UserPermissionId">
                                                    <WhereParameters>
                                                        <asp:Parameter DefaultValue="0" Name="UserPermissionId" Type="Int32" />
                                                    </WhereParameters>
                                                </asp:LinqDataSource>
                                            </telerik:RadPane>

                                        </telerik:RadSplitter>
                                    </telerik:RadPane>
                                </telerik:RadSplitter>
                            </telerik:RadPane>

                            <telerik:RadSplitBar ID="Radsplitbar1" runat="server" CollapseMode="Both" EnableResize="true" />

                            <telerik:RadPane ID="RadPane5" runat="server" Scrolling="None">
                                <telerik:RadSplitter ID="Radsplitter4" runat="server" Orientation="Horizontal">

                                    <telerik:RadPane ID="RadPane11" runat="server" Height="27px" Scrolling="None">
                                        <h4>Total Confirm View</h4>
                                    </telerik:RadPane>

                                    <telerik:RadPane ID="RadPane10" runat="server" Scrolling="None">
                                        <telerik:RadGrid ID="RadGridConfirm" runat="server" AllowFilteringByColumn="True" OnItemDataBound="RadGridConfirm_OnItemDataBound"
                                            AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False" Height="100%" PageSize="20" OnPreRender="RadGridConfirm_OnPreRender"
                                            DataSourceID="LinqDataSourceConfirm" ShowFooter="False" OnFilterCheckListItemsRequested="RadGridConfirm_OnFilterCheckListItemsRequested"
                                            FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                                            <MasterTableView DataKeyNames="UserPermissionId" TableLayout="Fixed" DataSourceID="LinqDataSourceConfirm">
                                                <Columns>
                                                    <telerik:GridBoundColumn
                                                        HeaderText="Site" DataField="SiteName" SortExpression="SiteName" UniqueName="SiteName"
                                                        FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn
                                                        HeaderText="Site Location" DataField="SiteLocationName" SortExpression="SiteLocationName" UniqueName="SiteLocationName"
                                                        FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn
                                                        HeaderText="Permission Type" DataField="PermissionTypeName" SortExpression="PermissionTypeName" UniqueName="PermissionTypeName"
                                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn
                                                        HeaderText="Menu" DataField="MenuId" SortExpression="MenuId" UniqueName="MenuId"
                                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridDateTimeColumn
                                                        HeaderText="Created Date" DataField="CreatedDate" SortExpression="CreatedDate" UniqueName="CreatedDate"
                                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                                        PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy} {0:t}">
                                                    </telerik:GridDateTimeColumn>
                                                </Columns>
                                            </MasterTableView>
                                            <ClientSettings EnablePostBackOnRowClick="False">
                                                <Selecting AllowRowSelect="true" />
                                                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                                                <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                                                    ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                                            </ClientSettings>
                                            <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                                        </telerik:RadGrid>
                                        <asp:LinqDataSource ID="LinqDataSourceConfirm" runat="server"
                                            ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName="" OrderBy="PermissionTypeName, MenuId, SiteName, SiteLocationName"
                                            TableName="vwUserPermissionConfirmLists"
                                            Where="UserPermissionId == @UserPermissionId">
                                            <WhereParameters>
                                                <asp:Parameter DefaultValue="0" Name="UserPermissionId" Type="Int32" />
                                            </WhereParameters>
                                        </asp:LinqDataSource>
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


            function RadCheckBoxAccess_OnClientCheckedChanging(sender, args) {
                if (!confirm('Do you want to save it?'))
                    args.set_cancel(true);
            }
            function RadCheckBoxModify_OnClientCheckedChanging(sender, args) {
                if (!confirm('Do you want to save it?'))
                    args.set_cancel(true);
            }

            function RadGridSiteLocation_OnRowDropping(sender, args) {
                if (!confirm('Do you want to add it?'))
                    args.set_cancel(true);
            }

            function RadGridSiteLocationUser_OnRowDropping(sender, args) {
                if (!confirm('Do you want to remove it?'))
                    args.set_cancel(true);
            }

            function ToolbarButtonClick(sender, args) {
                var button = args.get_item();
                if (button.get_text() === "Reset") {
                    if (!confirm('Do you want to reset permissions depends on your position?'))
                        args.set_cancel(true);
                }
            }

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
