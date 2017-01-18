<%@ Page Title="" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="PlacementDormitorybySchoolPop.aspx.cs" Inherits="School_StudentHousing_PlacementDormitorybySchoolPop" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">
        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="RadpaneBasic" runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="UpdateToolBar" runat="server" OnButtonClick="UpdateToolBar_ButtonClick" OnClientButtonClicking="ToolbarButtonClick">
                    <Items>
                        <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_save.png" Text="Save Dormitory Placement by School" ToolTip="Save" ValidationGroup="Info" />
                        <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_back.png" Text="Close Window" ToolTip="Close Window"></telerik:RadToolBarButton>
                        <telerik:RadToolBarButton IsSeparator="true" />
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane5" runat="server" Scrolling="None">
                <telerik:RadSplitter runat="server" ID="RadSplitter2" Orientation="Vertical">
                    <telerik:RadPane ID="Radpane1" runat="server" Width="68%">
                        <telerik:RadGrid ID="Grid_HostList" Visible="True" runat="server" AutoGenerateColumns="False"
                            AllowPaging="True" Height="100%" AllowSorting="True"
                            OnSelectedIndexChanged="Grid_HostList_SelectedIndexChanged" OnItemDataBound="Grid_HostList_ItemDataBound" OnNeedDataSource="Grid_HostList_NeedDataSource" GroupPanelPosition="Top">

                            <MasterTableView TableLayout="Fixed" DataKeyNames="DormitoryHostId">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="DormitoryHostId" DataType="System.Int32" FilterControlAltText="Filter DormitoryHostId column" HeaderText="Dormitory Host ID" SortExpression="DormitoryHostId" UniqueName="DormitoryHostId">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn1 column" HeaderText="School" UniqueName="TemplateColumn1">
                                        <ItemTemplate>
                                            <telerik:RadLabel runat="server" ID="lbl_HostTopSchool" Text="-"></telerik:RadLabel>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn column" HeaderText="Campus" UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <telerik:RadLabel runat="server" ID="lbl_HostTopCampus" Text="-"></telerik:RadLabel>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="ActiveDate" FilterControlAltText="Filter ActiveDate column"
                                        HeaderText="Registration Date" SortExpression="ActiveDate" UniqueName="ActiveDate" DataType="System.DateTime" DataFormatString="{0: MM-dd-yyyy}">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="HostStatus" FilterControlAltText="Filter HostStatus column" HeaderText="Host Status" SortExpression="HostStatus" UniqueName="HostStatus" DataType="System.Int32">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="DormitoryHostName" FilterControlAltText="Filter DormitoryHostName column" HeaderText="Dormitory Host Name" SortExpression="DormitoryHostName" UniqueName="DormitoryHostName">
                                    </telerik:GridBoundColumn>


                                    <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn1 column" HeaderText="Number of Room" UniqueName="TemplateColumn3">
                                        <ItemTemplate>
                                            <telerik:RadLabel runat="server" ID="lbl_RoomNumber" Text="0"></telerik:RadLabel>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn1 column" HeaderText="Number of Bed" UniqueName="TemplateColumn4">
                                        <ItemTemplate>
                                            <telerik:RadLabel runat="server" ID="lbl_BedNumber" Text="0"></telerik:RadLabel>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridBoundColumn DataField="PhoneNumber" FilterControlAltText="Filter PhoneNumber column" HeaderText="Phone No." SortExpression="PhoneNumber" UniqueName="PhoneNumber">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="Address" FilterControlAltText="Filter Address column" HeaderText="Address" SortExpression="Address" UniqueName="Address">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="City" FilterControlAltText="Filter City column" HeaderText="City" SortExpression="City" UniqueName="City">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="Province" FilterControlAltText="Filter Province column" HeaderText="Province" SortExpression="Province" UniqueName="Province">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="PostalCode" FilterControlAltText="Filter PostalCode column" HeaderText="Postal Code" SortExpression="PostalCode" UniqueName="PostalCode">
                                    </telerik:GridBoundColumn>

                                </Columns>
                            </MasterTableView><GroupingSettings CollapseAllTooltip="Collapse all groups" />
                            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                                <Selecting AllowRowSelect="true" />
                                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="True" />
                                <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                                    ClipCellContentOnResize="true"
                                    EnableRealTimeResize="True" AllowResizeToFit="true" />
                            </ClientSettings>
                        </telerik:RadGrid>



                    </telerik:RadPane>

                    <telerik:RadSplitBar ID="Radsplitbar1" runat="server" CollapseMode="Both" EnableResize="true" />
                    <telerik:RadPane ID="Radpane2" runat="server" Scrolling="None" Width="30%">
                        <telerik:RadSplitter runat="server" ID="RadSplitter3" Orientation="Horizontal">


                            <telerik:RadPane ID="RadPan4" runat="server">
                                <telerik:RadLabel ID="lbl_room" runat="server" BackColor="#121212" BorderColor="#3366CC" Font-Bold="False" ForeColor="#E0E0E0" Text="Dormitory Host's Room Information:     "></telerik:RadLabel>

                                <table width="100%">
                                    <tr>
                                        <td>
                                            <telerik:RadGrid ID="Grid_HostRoom" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" GroupPanelPosition="Top" OnItemDataBound="Grid_HostRoom_ItemDataBound" OnNeedDataSource="Grid_HostRoom_NeedDataSource" OnSelectedIndexChanged="Grid_HostRoom_SelectedIndexChanged">
                                                <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                                <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                                                    <Selecting AllowRowSelect="True" />
                                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                                </ClientSettings>
                                                <MasterTableView DataKeyNames="HostRoomId">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="HostRoomName" FilterControlAltText="Filter column column" HeaderText="Room Name" UniqueName="column">
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn DataField="HostRoomId" FilterControlAltText="Filter column5 column" HeaderText="HostRoomId" UniqueName="column5" Visible="False">
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <telerik:RadLabel ID="lbl_bed" runat="server" BackColor="#121212" BorderColor="#3366CC" Font-Bold="False" ForeColor="#E0E0E0" Text="Dormitory Host's Bed Information :     "></telerik:RadLabel>

                                            <telerik:RadGrid ID="Grid_HostBed" runat="server" Visible="false" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" GroupPanelPosition="Top" OnItemDataBound="Grid_HostBed_ItemDataBound" OnNeedDataSource="Grid_HostBed_NeedDataSource" OnSelectedIndexChanged="Grid_HostBed_SelectedIndexChanged" CellSpacing="-1" GridLines="Both">
                                                <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                                <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true" Selecting-AllowRowSelect="true">
                                                    <Selecting AllowRowSelect="True" />
                                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                                </ClientSettings>
                                                <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                                                <MasterTableView DataKeyNames="HostBedId">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="BedName" FilterControlAltText="Filter column1 column" HeaderText="Bed Name" UniqueName="column1">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="HostBedId" FilterControlAltText="Filter column5 column" HeaderText="HostBedId" UniqueName="column5" Visible="False">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn column" HeaderText="Avalible" UniqueName="TemplateColumn">
                                                            <ItemTemplate>
                                                                <telerik:RadLabel ID="lbl_avalible" runat="server" Text="Yes"></telerik:RadLabel>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumPlacemnt column" HeaderText="Placement" UniqueName="TemplateColumPlacemnt">
                                                            <ItemTemplate>
                                                                <telerik:RadCheckBox ID="chb_placement" Checked="false" runat="server" Enabled="false"></telerik:RadCheckBox>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>

                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>

                            </telerik:RadPane>

                        </telerik:RadSplitter>
                    </telerik:RadPane>
                </telerik:RadSplitter>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </telerik:RadAjaxPanel>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }

            function Close() {
                var oWnd = GetRadWindow();
                oWnd.close();
            }

            function ToolbarButtonClick(sender, args) {
                var button = args.get_item();
                if (button.get_text() == "Save Dormitory Placement by School") {
                    if (!confirm('Do you want to Save Dormitory Placement by School it?'))
                        args.set_cancel(true);
                }
                if (button.get_text() == "Close Window") {
                    Close();
                }
                function OnClientClose(oWnd, args) {
                    Close();
                    //var arg = args.get_argument();
                    <%--<%=Page.GetPostBackEventReference(btnRefresh)%>--%>
                }
            }

        </script>
    </telerik:RadCodeBlock>
</asp:Content>

