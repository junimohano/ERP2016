<%@ Page Title="" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="PlacementbySchoolPop.aspx.cs" Inherits="School_StudentHousing_PlacementbySchoolHomestayPop" %>


<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">
        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="RadpaneBasic" runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="UpdateToolBar" runat="server" OnButtonClick="UpdateToolBar_ButtonClick" OnClientButtonClicking="ToolbarButtonClick">
                    <Items>
                        <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_save.png" Text="Save Homestay Placement by School" ToolTip="Save" ValidationGroup="Info" />
                        <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_back.png" Text="Close Window" ToolTip="Close Window"></telerik:RadToolBarButton>
                        <telerik:RadToolBarButton IsSeparator="true" />
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane5" runat="server" Scrolling="None">
                <telerik:RadSplitter runat="server" ID="RadSplitter2" Orientation="Vertical">
                    <telerik:RadPane ID="Radpane1" runat="server" Width="68%">
                        <telerik:RadGrid ID="Grid_HostList" runat="server" AutoGenerateColumns="False"
                            AllowPaging="True" Height="100%" AllowSorting="True"
                            OnSelectedIndexChanged="Grid_HostList_SelectedIndexChanged" OnItemDataBound="Grid_HostList_ItemDataBound" OnNeedDataSource="Grid_HostList_NeedDataSource" GroupPanelPosition="Top">

                            <MasterTableView TableLayout="Fixed" DataKeyNames="HostId">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="HostId" DataType="System.Int32" FilterControlAltText="Filter HostId column" HeaderText="Host ID" SortExpression="HostId" UniqueName="HostId">
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
                                    <telerik:GridBoundColumn DataField="HouseActiveDate" FilterControlAltText="Filter HouseActiveDate column"
                                        HeaderText="Registration Date" SortExpression="HouseActiveDate" UniqueName="HouseActiveDate" DataType="System.DateTime" DataFormatString="{0: MM-dd-yyyy}">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="HouseActiveStutas" FilterControlAltText="Filter HouseActiveDate column" HeaderText="Host Status" SortExpression="HouseActiveStutas" UniqueName="HouseActiveStutas" DataType="System.Int32">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="FatherFirstName" FilterControlAltText="Filter FatherFirstName column" HeaderText="Father's First Name" SortExpression="FatherFirstName" UniqueName="FatherFirstName">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FatherLastName" FilterControlAltText="Filter FatherLastName column" HeaderText="Father's Last Name" SortExpression="FatherLastName" UniqueName="FatherLastName">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="MotherFirstName" FilterControlAltText="Filter MotherFirstName column" HeaderText="Mother's First Name" SortExpression="MotherFirstName" UniqueName="MotherFirstName">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="MotherLastName" FilterControlAltText="Filter MotherLastName column" HeaderText="Mother's Last Name" SortExpression="MotherLastName" UniqueName="MotherLastName">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn1 column" HeaderText="Family Member" UniqueName="TemplateColumn5">
                                        <ItemTemplate>
                                            <telerik:RadLabel runat="server" ID="lbl_FamilyMember" Text="0"></telerik:RadLabel>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
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

                                    <telerik:GridBoundColumn DataField="HousePhone" FilterControlAltText="Filter HousePhone column" HeaderText="Home Phone" SortExpression="HousePhone" UniqueName="HousePhone">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="HouseAddress" FilterControlAltText="Filter HouseAddress column" HeaderText="Home Address" SortExpression="HouseAddress" UniqueName="HouseAddress">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="HouseCity" FilterControlAltText="Filter HouseCity column" HeaderText="City" SortExpression="HouseCity" UniqueName="HouseCity">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="HouseProvince" FilterControlAltText="Filter HouseProvince column" HeaderText="Province" SortExpression="HouseProvince" UniqueName="HouseProvince">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="HousePostalCode" FilterControlAltText="Filter HousePostalCode column" HeaderText="Postal Code" SortExpression="HousePostalCode" UniqueName="HousePostalCode">
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
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                        </telerik:RadGrid>



                    </telerik:RadPane>

            <telerik:RadSplitBar ID="Radsplitbar1" runat="server" CollapseMode="Both" EnableResize="true" />
            <telerik:RadPane ID="Radpane2" runat="server" Scrolling="None" Width="30%">
                <telerik:RadSplitter runat="server" ID="RadSplitter3" Orientation="Horizontal">


                    <telerik:RadPane ID="RadPan4" runat="server">
                        <telerik:RadLabel ID="lbl_room" runat="server" BackColor="#121212" BorderColor="#3366CC" Font-Bold="False" ForeColor="#E0E0E0" Text="Homestay Host's Room Information:     "></telerik:RadLabel>

                        <table width="100%">
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="Grid_HostRoom" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" GroupPanelPosition="Top" OnItemDataBound="Grid_HostRoom_ItemDataBound" OnNeedDataSource="Grid_HostRoom_NeedDataSource" OnSelectedIndexChanged="Grid_HostRoom_SelectedIndexChanged">
                                        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                        <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                                            <Selecting AllowRowSelect="True" />
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                        </ClientSettings>
                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                                        <MasterTableView DataKeyNames="HostRoomId">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="HostRoomName" FilterControlAltText="Filter column column" HeaderText="Room Name" UniqueName="column">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="HostRoomFloor" FilterControlAltText="Filter column1 column" HeaderText="Floor Location" UniqueName="HostRoomFloor">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="HostRoomType" FilterControlAltText="Filter column2 column" HeaderText="Room Type" UniqueName="HostRoomType">
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
                                    <telerik:RadLabel ID="lbl_bed" runat="server" BackColor="#121212" BorderColor="#3366CC" Font-Bold="False" ForeColor="#E0E0E0" Text="Homestay Host's Bed Information :     "></telerik:RadLabel>

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
                if (button.get_text() == "Save Homestay Placement by School") {
                    if (!confirm('Do you want to Save Homestay Placement by School it?'))
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

