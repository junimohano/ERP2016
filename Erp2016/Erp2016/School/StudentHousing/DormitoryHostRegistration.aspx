<%@ Page Title="" Language="C#" MasterPageFile="~/School/Shared/Base.master" AutoEventWireup="true" CodeFile="DormitoryHostRegistration.aspx.cs" Inherits="School.StudentHousing.DormitoryHostRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%" Width="100%">

        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="RadPane3" runat="server" Height="40%" Scrolling="None">
                <telerik:RadSplitter runat="server" Orientation="Horizontal" Width="100%">


                    <telerik:RadPane ID="RadPaneTop" runat="server" Height="27px" Scrolling="None">

                        <table width="100%">
                            <tr>
                                <td style="width: 80%" align="left">

                                    <h4>Domitory Host Registration List</h4>
                                </td>
                                <td style="width: 20%" align="right">
                                    <telerik:RadButton ID="btn_registration" runat="server" Text="New Dormitory Registration" OnClick="btn_registration_Click"></telerik:RadButton>
                                </td>
                            </tr>

                        </table>
                    </telerik:RadPane>

                    <telerik:RadPane ID="RadPane2" runat="server" Scrolling="None">


                        <telerik:RadGrid ID="Grid_HostList" Visible="True" runat="server" AutoGenerateColumns="False"
                            AllowPaging="True" Height="100%" AllowSorting="True"
                            OnSelectedIndexChanged="Grid_HostList_SelectedIndexChanged" OnItemDataBound="Grid_HostList_ItemDataBound" OnNeedDataSource="Grid_HostList_NeedDataSource" GroupPanelPosition="Top"
                            AllowFilteringByColumn="true" FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">

                            <MasterTableView TableLayout="Fixed" DataKeyNames="DormitoryHostId">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="DormitoryHostId" DataType="System.Int32" FilterControlAltText="Filter DormitoryHostId column" HeaderText="Dormitory Host ID" SortExpression="DormitoryHostId" UniqueName="DormitoryHostId">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn
                                        HeaderText="Site" DataField="SiteName" SortExpression="SiteName" UniqueName="SiteName"
                                        FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn
                                        HeaderText="SiteLocation" DataField="SiteLocationName" SortExpression="SiteLocationName" UniqueName="SiteLocationName"
                                        FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="CreatedDate" FilterControlAltText="Filter CreatedDate column"
                                        HeaderText="Registration Date" SortExpression="CreatedDate" UniqueName="CreatedDate" DataType="System.DateTime" DataFormatString="{0: MM-dd-yyyy}">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="HostStatus" FilterControlAltText="Filter CreatedDate column" HeaderText="Host Status" SortExpression="HostStatus" UniqueName="HostStatus" DataType="System.Int32">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="DormitoryHostName" FilterControlAltText="Filter DormitoryHostName column" HeaderText="Dormitory Host Name" SortExpression="DormitoryHostName" UniqueName="DormitoryHostName">
                                    </telerik:GridBoundColumn>


                                    <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn1 column" HeaderText="Number of Room" UniqueName="TemplateColumn3" AllowFiltering="false">
                                        <ItemTemplate>
                                            <telerik:RadLabel runat="server" ID="lbl_RoomNumber" Text="0"></telerik:RadLabel>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn1 column" HeaderText="Number of Bed" UniqueName="TemplateColumn4" AllowFiltering="false">
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
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                        </telerik:RadGrid>


                    </telerik:RadPane>
                </telerik:RadSplitter>
            </telerik:RadPane>

            <telerik:RadSplitBar ID="Radslitbar1" runat="server" CollapseMode="None" EnableResize="True"></telerik:RadSplitBar>

            <telerik:RadPane ID="Radpane4" runat="server" Scrolling="Both" Width="899px">

                <table>
                    <tr>
                        <td style="width: 80%">
                            <telerik:RadTabStrip runat="server" Width="100%" SelectedIndex="0" MultiPageID="MultiPage_Registration" ID="Tab_Host" OnTabClick="Tab_Host_TabClick">
                                <Tabs>
                                    <telerik:RadTab runat="server" TabIndex="0" Text="Dormitory Host Information" PageViewID="PageView_Basic" Selected="True">
                                    </telerik:RadTab>

                                    <telerik:RadTab runat="server" TabIndex="2" Text="Dormitory Room" PageViewID="PageView_Room" Enabled="false">
                                    </telerik:RadTab>
                                    <telerik:RadTab runat="server" TabIndex="3" Text="Dormitory Bed" PageViewID="PageView_Bed" Enabled="false">
                                    </telerik:RadTab>

                                    <telerik:RadTab runat="server" TabIndex="5" Text="Update Registration" PageViewID="PageView_Update" Enabled="false">
                                    </telerik:RadTab>

                                    <telerik:RadTab runat="server" TabIndex="6" Text="Placement History" PageViewID="PageView_Placement" Enabled="false">
                                    </telerik:RadTab>

                                </Tabs>
                            </telerik:RadTabStrip>
                        </td>
                        <td style="width: 20%"></td>

                    </tr>

                </table>


                <telerik:RadMultiPage runat="server" ID="MultiPage_Registration" SelectedIndex="0" Width="100%" Height="300px">
                    <telerik:RadPageView runat="server" ID="PageView_Basic" Visible="true">
                        <br />
                        <table border="0" width="70%">
                            <tr>
                                <td style="width: 80%" align="center">
                                    <telerik:RadButton ID="btn_basic_save" runat="server" ValidationGroup="Info" Text="Save Dormitory Host Information" OnClick="btn_basic_save_Click" BorderStyle="Outset">
                                    </telerik:RadButton>
                                    <telerik:RadButton ID="btn_basic_cancel" runat="server" Text="Cancel" OnClick="btn_basic_cancel_Click">
                                    </telerik:RadButton>
                                </td>
                            </tr>
                        </table>

                        <table border="0" width="70%">
                            <tr>
                                <td style="width: 40%" align="left">
                                    <telerik:RadLabel ID="Label12" runat="server" BackColor="#121212" BorderColor="#3366CC" Font-Bold="False" ForeColor="#E0E0E0" Text="Dormitory Information:     "></telerik:RadLabel>
                                </td>
                                <td style="width: 60%" align="right">&nbsp;</td>
                            </tr>
                        </table>

                        <table width="70%" border="0" cellpadding="1" cellspacing="1" frame="border">
                            <tr>
                                <td class="auto-style8">
                                    <telerik:RadLabel ID="Label4" runat="server" Text="Dormitory Name:">
                                    </telerik:RadLabel>
                                </td>
                                <td class="auto-style9">
                                    <telerik:RadTextBox ID="txtDormitoryName" runat="server" Width="80%">
                                    </telerik:RadTextBox>
                                    <asp:Label ID="Label60" runat="server" Font-Bold="true" ForeColor="Red" Text="*"></asp:Label>
                                    <br style="clear: both;" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDormitoryName" Display="Dynamic" ErrorMessage="Dormitory Name Required" ForeColor="Red" ValidationGroup="Info" />
                                </td>
                                <td class="auto-style17">
                                    <telerik:RadLabel ID="Label5" runat="server" Text="Dormitory Type:">
                                    </telerik:RadLabel>
                                </td>
                                <td class="auto-style19">
                                    <telerik:RadDropDownList ID="ddlHomeType" runat="server" SelectedText="Please Select Home Type" SelectedValue="0">
                                        <Items>
                                            <telerik:DropDownListItem Text="Condo" Value="1" Selected="True" />
                                            <telerik:DropDownListItem Text="Apartment" Value="2" />
                                            <telerik:DropDownListItem Text="TownHouse" Value="3" />
                                            <telerik:DropDownListItem Text="Semi-Detached" Value="4" />
                                            <telerik:DropDownListItem Text="Detached" Value="5" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                    <asp:Label ID="Label61" runat="server" Font-Bold="true" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td class="auto-style8">
                                    <telerik:RadLabel ID="Label22" runat="server" Text="Address:">
                                    </telerik:RadLabel>
                                </td>
                                <td class="auto-style9">
                                    <telerik:RadTextBox ID="txtHomeAddress" runat="server" Width="80%">
                                    </telerik:RadTextBox>
                                    <asp:Label ID="Label64" runat="server" Font-Bold="true" ForeColor="Red" Text="*"></asp:Label>
                                    <br style="clear: both;" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtHomeAddress" Display="Dynamic" ErrorMessage="Home Address Required" ForeColor="Red" ValidationGroup="Info" />
                                </td>
                                <td class="auto-style17">
                                    <telerik:RadLabel ID="Label23" runat="server" Text="City:">
                                    </telerik:RadLabel>
                                </td>
                                <td class="auto-style19">
                                    <telerik:RadTextBox ID="txtCity" runat="server" Width="80%">
                                    </telerik:RadTextBox>
                                    <asp:Label ID="Label65" runat="server" Font-Bold="true" ForeColor="Red" Text="*"></asp:Label>
                                    <br style="clear: both;" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCity" Display="Dynamic" ErrorMessage="City Required" ForeColor="Red" ValidationGroup="Info" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style8">
                                    <telerik:RadLabel ID="Label70" runat="server" Text="Provice:">
                                    </telerik:RadLabel>
                                </td>
                                <td class="auto-style9">
                                    <telerik:RadDropDownList ID="ddlProvice" runat="server">
                                    </telerik:RadDropDownList>
                                    <asp:Label ID="Label66" runat="server" Font-Bold="true" ForeColor="Red" Text="*"></asp:Label>
                                    <br style="clear: both;" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlProvice" Display="Dynamic" ErrorMessage="Province Required" ForeColor="Red" ValidationGroup="Info" />
                                </td>
                                <td class="auto-style17">
                                    <telerik:RadLabel ID="Label71" runat="server" Text="Postal Code:">
                                    </telerik:RadLabel>
                                </td>
                                <td class="auto-style19">
                                    <telerik:RadTextBox ID="txtPostalCode" runat="server" Width="70%">
                                    </telerik:RadTextBox>
                                    <asp:Label ID="Label72" runat="server" Font-Bold="true" ForeColor="Red" Text="*"></asp:Label>
                                    <br style="clear: both;" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPostalCode" Display="Dynamic" ErrorMessage="PostalCode Required" ForeColor="Red" ValidationGroup="Info" />
                                </td>
                            </tr>

                            <tr>
                                <td class="auto-style8">
                                    <telerik:RadLabel ID="Label24" runat="server" Text="Phone No:"></telerik:RadLabel>
                                </td>
                                <td class="auto-style9">
                                    <telerik:RadTextBox ID="txtPhone" runat="server" Width="80%" EmptyMessage="416-123-1234 ext.123" LabelWidth="40%" Resize="None">
                                    </telerik:RadTextBox>
                                    <asp:Label ID="Label68" runat="server" ForeColor="Red" Text="*" Font-Bold="true"></asp:Label>
                                    <br style="clear: both;" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPhone" Display="Dynamic" ErrorMessage="Phone Required" ForeColor="Red" ValidationGroup="Info" />
                                </td>
                                <td class="auto-style17">
                                    <telerik:RadLabel ID="Label26" runat="server" Text="Additional Phone No.:" EmptyMessage="416-123-1234 ext.123"></telerik:RadLabel>
                                </td>
                                <td class="auto-style19">
                                    <telerik:RadTextBox ID="txtAdditionalPhone" runat="server" EmptyMessage="416-123-1234 ext.123" Width="80%">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>

                        </table>
                        <br />
                        <table border="0" width="70%">
                            <tr>
                                <td style="width: 40%" align="left">
                                    <telerik:RadLabel ID="RadLabel5" runat="server" BackColor="#121212" BorderColor="#3366CC" Font-Bold="False" ForeColor="#E0E0E0" Text="Lease Information:     "></telerik:RadLabel>

                                </td>
                                <td style="width: 60%" align="right">&nbsp;</td>
                            </tr>

                        </table>

                        <table border="0" width="70%" cellpadding="1" cellspacing="1" frame="border">
                            <tr>
                                <td class="auto-style21">
                                    <telerik:RadLabel ID="Label73" runat="server" Text="Start Date:">
                                    </telerik:RadLabel>
                                </td>
                                <td class="auto-style22">
                                    <telerik:RadDatePicker ID="DatePickerLeaseStartDate" runat="server" Culture="en-US" Height="25px" MinDate="1900-01-01" Width="178px">
                                        <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                        </Calendar>
                                        <DateInput DateFormat="MM-dd-yyyy" DisplayDateFormat="MM-dd-yyyy" Height="25px" LabelWidth="40%">
                                            <EmptyMessageStyle Resize="None" />
                                            <ReadOnlyStyle Resize="None" />
                                            <FocusedStyle Resize="None" />
                                            <DisabledStyle Resize="None" />
                                            <InvalidStyle Resize="None" />
                                            <HoveredStyle Resize="None" />
                                            <EnabledStyle Resize="None" />
                                        </DateInput>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>

                                </td>
                                <td class="auto-style23">
                                    <telerik:RadLabel ID="Label74" runat="server" Text="End Date:">
                                    </telerik:RadLabel>
                                </td>
                                <td class="auto-style24">
                                    <telerik:RadDatePicker ID="DatePickerLeaseEndDate" runat="server" Culture="en-US" Height="25px" MinDate="1900-01-01" Width="178px">
                                        <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                        </Calendar>
                                        <DateInput DateFormat="MM-dd-yyyy" DisplayDateFormat="MM-dd-yyyy" Height="25px" LabelWidth="40%">
                                            <EmptyMessageStyle Resize="None" />
                                            <ReadOnlyStyle Resize="None" />
                                            <FocusedStyle Resize="None" />
                                            <DisabledStyle Resize="None" />
                                            <InvalidStyle Resize="None" />
                                            <HoveredStyle Resize="None" />
                                            <EnabledStyle Resize="None" />
                                        </DateInput>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>

                        </table>
                        <br />
                        <table border="0" width="70%">
                            <tr>
                                <td style="width: 40%" align="left">
                                    <telerik:RadLabel ID="RadLabel6" runat="server" BackColor="#121212" BorderColor="#3366CC" Font-Bold="False" ForeColor="#E0E0E0" Text="Dormitory Coordianator Information:     "></telerik:RadLabel>

                                </td>
                                <td style="width: 60%" align="right">&nbsp;</td>
                            </tr>
                        </table>

                        <table border="0" width="70%" cellpadding="1" cellspacing="1" frame="border">
                            <tr>
                                <td class="auto-style25">
                                    <telerik:RadLabel ID="Label75" runat="server" Text=" Name:">
                                    </telerik:RadLabel>
                                </td>
                                <td class="auto-style11">
                                    <telerik:RadTextBox ID="txtCoordianatorName" runat="server" Width="80%" LabelWidth="40%" Resize="None">
                                    </telerik:RadTextBox>

                                </td>
                                <td class="auto-style18">
                                    <telerik:RadLabel ID="Label76" runat="server" Text="Phone Number:">
                                    </telerik:RadLabel>
                                </td>
                                <td class="auto-style20">
                                    <telerik:RadTextBox ID="txtCoordianatorPhone" runat="server" Width="80%" EmptyMessage="416-123-1234 ext.123" LabelWidth="40%" Resize="None">
                                    </telerik:RadTextBox>

                                </td>
                            </tr>

                            <tr>
                                <td class="auto-style25">
                                    <telerik:RadLabel ID="RadLabel7" runat="server" Text=" Registered School Name:">
                                    </telerik:RadLabel>
                                </td>
                                <td class="auto-style11" colspan="3">
                                    <telerik:RadDropDownList ID="ddlSchoolName" runat="server" AutoPostBack="false" Enabled="false" Width="80%">
                                    </telerik:RadDropDownList>

                                </td>
                            </tr>


                        </table>
                    </telerik:RadPageView>


                    <telerik:RadPageView runat="server" ID="PageView_Room" Visible="true">

                        <br />
                        <telerik:RadLabel ID="Label44" runat="server" BackColor="#121212" BorderColor="#3366CC" Font-Bold="False" ForeColor="#E0E0E0" Text="Room Information:   "></telerik:RadLabel>

                        <table width="90%" border="0" cellpadding="1" cellspacing="1" frame="border">


                            <tr>
                                <td style="width: 5%">&nbsp;</td>
                                <td class="auto-style1">&nbsp;</td>
                                <td style="width: 5%">&nbsp;</td>
                                <td style="width: 20%">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>


                            <tr>
                                <td style="width: 5%">
                                    <telerik:RadLabel ID="Label48" runat="server" Text="Room Name:"></telerik:RadLabel>
                                </td>
                                <td class="auto-style1">
                                    <telerik:RadTextBox ID="txtRoomName" runat="server" Width="80%"></telerik:RadTextBox>
                                </td>
                                <td style="width: 5%">
                                    <telerik:RadLabel ID="Label49" runat="server" Visible="false" Text="Room Location:"></telerik:RadLabel>
                                </td>
                                <td style="width: 20%">
                                    <telerik:RadDropDownList ID="ddlRoomLocation" runat="server" Visible="false">
                                        <Items>

                                            <telerik:DropDownListItem runat="server" Text="First Floor" Value="1" />
                                            <telerik:DropDownListItem runat="server" Text="Basement" Value="5" />
                                            <telerik:DropDownListItem runat="server" Text="Second Floor" Value="2" />
                                            <telerik:DropDownListItem runat="server" Text="Third Floor" Value="3" />
                                            <telerik:DropDownListItem runat="server" Text="Other Floor" Value="4" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                                <td>
                                    <telerik:RadLabel ID="Label69" Visible="false" runat="server" Text="Room Type:">
                                    </telerik:RadLabel>

                                </td>
                                <td>
                                    <telerik:RadDropDownList ID="ddlRoomType" runat="server" Visible="false">
                                        <Items>

                                            <telerik:DropDownListItem runat="server" Text="Private Room" Selected="true" Value="1" />
                                            <telerik:DropDownListItem runat="server" Text="Shared Room" Value="0" />

                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btn_AddRoom" runat="server" OnClick="btn_AddRoom_Click" Text="Save Room">
                                    </telerik:RadButton>
                                    <telerik:RadButton ID="btn_CancelRoom" runat="server" Text="Cancel" OnClick="btn_CancelRoom_Click">
                                    </telerik:RadButton>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">&nbsp;</td>
                                <td class="auto-style1">&nbsp;</td>
                                <td style="width: 5%">&nbsp;</td>
                                <td style="width: 20%">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>


                            <tr>
                                <td colspan="7">
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
                                                <telerik:GridBoundColumn DataField="HostRoomFloor" Visible="false" FilterControlAltText="Filter column1 column" HeaderText="Floor Location" UniqueName="HostRoomFloor">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="HostRoomType" Visible="false" FilterControlAltText="Filter column2 column" HeaderText="Room Type" UniqueName="HostRoomType">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="HostRoomId" FilterControlAltText="Filter column5 column" HeaderText="HostRoomId" UniqueName="column5" Visible="False">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>
                            </tr>


                        </table>

                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="PageView_Bed" Visible="true">
                        <br />
                        <telerik:RadLabel ID="Label50" runat="server" BackColor="#121212" BorderColor="#3366CC" Font-Bold="False" ForeColor="#E0E0E0" Text="Bed Information:   "></telerik:RadLabel>

                        <table width="90%" border="0" cellpadding="1" cellspacing="1" frame="border">


                            <tr>
                                <td style="width: 5%">&nbsp;</td>
                                <td class="auto-style1">&nbsp;</td>
                                <td style="width: 5%">&nbsp;</td>
                                <td style="width: 20%">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>


                            <tr>
                                <td style="width: 5%">
                                    <telerik:RadLabel ID="Label52" runat="server" Text="Room Name:"></telerik:RadLabel>

                                </td>
                                <td class="auto-style1">
                                    <telerik:RadDropDownList ID="ddlRoom" runat="server">
                                        <Items>
                                        </Items>
                                    </telerik:RadDropDownList>

                                </td>
                                <td style="width: 5%">
                                    <telerik:RadLabel ID="Label51" runat="server" Text="Bed Name:"></telerik:RadLabel>
                                </td>
                                <td style="width: 20%">
                                    <telerik:RadTextBox ID="txtBedName" runat="server" Width="80%"></telerik:RadTextBox>
                                </td>
                                <td>&nbsp;&nbsp;<telerik:RadButton ID="btn_AddBed" runat="server" Text="Save Bed" OnClick="btn_AddBed_Click">
                                </telerik:RadButton>
                                    <telerik:RadButton ID="btn_CancelBed" runat="server" Text="Cancel" OnClick="btn_CancelBed_Click">
                                    </telerik:RadButton>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">&nbsp;</td>
                                <td class="auto-style1">&nbsp;</td>
                                <td style="width: 5%">&nbsp;</td>
                                <td style="width: 20%">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>


                            <tr>
                                <td colspan="5">
                                    <telerik:RadGrid ID="Grid_HostBed" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" GroupPanelPosition="Top" OnItemDataBound="Grid_HostBed_ItemDataBound" OnNeedDataSource="Grid_HostBed_NeedDataSource" OnSelectedIndexChanged="Grid_HostBed_SelectedIndexChanged" CellSpacing="-1" GridLines="Both">
                                        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                        <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                                            <Selecting AllowRowSelect="True" />
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                        </ClientSettings>
                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                                        <MasterTableView DataKeyNames="HostBedId">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="BedName" FilterControlAltText="Filter column1 column" HeaderText="Bed Name" UniqueName="column1">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="HostRoomName" FilterControlAltText="Filter column column" HeaderText="Room Name" UniqueName="column">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="HostRoomFloor" Visible="false" FilterControlAltText="Filter column1 column" HeaderText="Floor Location" UniqueName="HostRoomFloor">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="HostBedId" FilterControlAltText="Filter column5 column" HeaderText="HostBedId" UniqueName="HostBedId">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>
                            </tr>


                        </table>


                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="PageView_School" Visible="true">
                        <br />
                        <telerik:RadLabel ID="Label53" runat="server" BackColor="#121212" BorderColor="#3366CC" Font-Bold="False" ForeColor="#E0E0E0" Text="Dormitory Preferred School:     "></telerik:RadLabel>
                        <table width="90%" border="0" cellpadding="1" cellspacing="1" frame="border">
                            <tr>

                                <td class="auto-style5" colspan="5"></td>
                            </tr>

                            <tr>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="Label58" runat="server" Text="Shcool:"></telerik:RadLabel>
                                </td>
                                <td colspan="4"></td>
                            </tr>

                            <tr>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="Label55" runat="server" Text="Contact Person:">
                                    </telerik:RadLabel>
                                </td>
                                <td colspan="4">

                                    <telerik:RadDropDownList ID="ddlSchoolContactName" runat="server" Width="80%">
                                        <Items>
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="RadLabel2" runat="server" Text=" Total Distance to School:">
                                    </telerik:RadLabel>
                                </td>
                                <td style="width: 10%">
                                    <telerik:RadTextBox ID="txtDistanceToSchool" runat="server" Width="65%"></telerik:RadTextBox>
                                </td>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="RadLabel3" runat="server" Text="Major Intersection:">
                                    </telerik:RadLabel>
                                </td>
                                <td style="width: 10%">
                                    <telerik:RadTextBox ID="txtMajorIntersection" runat="server"></telerik:RadTextBox>
                                </td>
                                <td style="width: 30%">
                                    <telerik:RadLabel ID="RadLabel4" runat="server" Text="Distance to Closest Bus Stop/Subway Station:">
                                    </telerik:RadLabel>
                                    &nbsp;&nbsp;
                      <telerik:RadTextBox ID="txtDistanceBusSubway" runat="server" LabelWidth="10%" Width="24%"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    <telerik:RadDropDownList ID="ddlSiteLocation" runat="server" Visible="False">
                                    </telerik:RadDropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td align="right" colspan="5">
                                    <telerik:RadButton ID="btn_shcool" runat="server" OnClick="btn_shcool_Click" Text="Save Preferred School">
                                    </telerik:RadButton>
                                    <telerik:RadButton ID="btn_cancel_shcool" runat="server" OnClick="btn_cancel_shcool_Click" Text="Cancel">
                                    </telerik:RadButton>
                                </td>
                            </tr>


                        </table>
                        <br />
                        <table width="90%" border="0">
                            <tr>
                                <td>

                                    <telerik:RadGrid ID="Grid_School" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" GroupPanelPosition="Top" OnItemDataBound="Grid_School_ItemDataBound" OnNeedDataSource="Grid_School_NeedDataSource" OnSelectedIndexChanged="Grid_School_SelectedIndexChanged">
                                        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                        <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                                            <Selecting AllowRowSelect="True" />
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                        </ClientSettings>
                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                                        <MasterTableView DataKeyNames="HostSchoolId">
                                            <Columns>

                                                <telerik:GridBoundColumn DataField="Abbreviation" FilterControlAltText="Filter column column" HeaderText="School Name" UniqueName="column">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="City" FilterControlAltText="Filter column1 column" HeaderText="School City" UniqueName="column1">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="ContactName" FilterControlAltText="Filter column3 column" HeaderText="Contact Name" UniqueName="column3">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ContactPhone" FilterControlAltText="Filter column4 column" HeaderText="Contact Phone" UniqueName="column4">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ContactPosition" FilterControlAltText="Filter column5 column" HeaderText="Contact Postion" UniqueName="column5">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DistanceSchool" FilterControlAltText="Filter column6 column" HeaderText="Distance to School" UniqueName="column6">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DistanceStation" FilterControlAltText="Filter column7 column" HeaderText="Distance to Bus Stop/Subway" UniqueName="column7">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="MajorIntersection" FilterControlAltText="Filter column8 column" HeaderText="Major Intersection" UniqueName="column8">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Address" FilterControlAltText="Filter column2 column" HeaderText="School Address" UniqueName="column2">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="HostSchoolId" FilterControlAltText="Filter column9 column" UniqueName="column9" Visible="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DormitoryHostId" FilterControlAltText="Filter column10 column" UniqueName="column10" Visible="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="SiteLocationId" FilterControlAltText="Filter column11 column" UniqueName="column11" Visible="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ContactId" FilterControlAltText="Filter column12 column" UniqueName="column12" Visible="false">
                                                </telerik:GridBoundColumn>

                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>

                                    <br />
                                </td>
                            </tr>

                        </table>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPage_Register" runat="server">
                        <br />
                        <telerik:RadLabel ID="Label59" runat="server" BackColor="#121212" BorderColor="#3366CC" Font-Bold="False" ForeColor="#E0E0E0" Text="Domitory Status:   ">
                        </telerik:RadLabel>
                        <table border="0" width="90%">
                            <tr>

                                <td align="center">
                                    <telerik:RadButton ID="RadButton16" runat="server" Text="Register Homestay Host">
                                    </telerik:RadButton>
                                </td>
                            </tr>
                        </table>
                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="PageView_Update" Visible="true">
                        <br />
                        <table border="0" width="80%" cellpadding="1" cellspacing="1" frame="border">
                            <tr>
                                <td style="width: 20%">
                                    <telerik:RadLabel ID="RadLabel14" runat="server" Text="Active Date: ">
                                    </telerik:RadLabel>

                                </td>
                                <td style="width: 30%">

                                    <telerik:RadDatePicker ID="DatePickActiveDate" runat="server" Culture="en-US" Height="25px" MinDate="1900-01-01" Width="178px" Enabled="false">
                                        <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                        </Calendar>
                                        <DateInput DateFormat="MM-dd-yyyy" DisplayDateFormat="MM-dd-yyyy" Height="25px" LabelWidth="40%">
                                            <EmptyMessageStyle Resize="None" />
                                            <ReadOnlyStyle Resize="None" />
                                            <FocusedStyle Resize="None" />
                                            <DisabledStyle Resize="None" />
                                            <InvalidStyle Resize="None" />
                                            <HoveredStyle Resize="None" />
                                            <EnabledStyle Resize="None" />
                                        </DateInput>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>

                                </td>
                                <td style="width: 20%">
                                    <telerik:RadLabel ID="RadLabel13" runat="server" Text="Inactive Date: ">
                                    </telerik:RadLabel>
                                </td>
                                <td style="width: 30%">

                                    <telerik:RadDatePicker ID="DatePickInactiveDate" runat="server" Culture="en-US" Height="25px" MinDate="1900-01-01" Width="178px" Enabled="false">
                                        <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                        </Calendar>
                                        <DateInput DateFormat="MM-dd-yyyy" DisplayDateFormat="MM-dd-yyyy" Height="25px" LabelWidth="40%">
                                            <EmptyMessageStyle Resize="None" />
                                            <ReadOnlyStyle Resize="None" />
                                            <FocusedStyle Resize="None" />
                                            <DisabledStyle Resize="None" />
                                            <InvalidStyle Resize="None" />
                                            <HoveredStyle Resize="None" />
                                            <EnabledStyle Resize="None" />
                                        </DateInput>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                </td>
                                <td>&nbsp;</td>
                            </tr>

                            <tr>
                                <td style="width: 20%">&nbsp;</td>
                                <td style="width: 30%">&nbsp;</td>
                                <td style="width: 20%">&nbsp;</td>
                                <td style="width: 30%">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
                                    <telerik:RadLabel ID="RadLabel1" runat="server" Text="Host Status: ">
                                    </telerik:RadLabel>
                                </td>
                                <td style="width: 30%">
                                    <telerik:RadDropDownList ID="ddlHostStatus" runat="server" SelectedText="Active" SelectedValue="1">
                                        <Items>
                                            <telerik:DropDownListItem runat="server" Text="Pending" Value="0" />
                                            <telerik:DropDownListItem runat="server" Text="Active" Value="1" />
                                            <telerik:DropDownListItem runat="server" Text="Inactive" Value="2" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                                <td style="width: 20%">
                                    <telerik:RadLabel ID="RadLabel12" runat="server" Text="Date: ">
                                    </telerik:RadLabel>
                                </td>
                                <td style="width: 30%">
                                    <telerik:RadDatePicker ID="DatePickStatusDate" runat="server" Culture="en-US" Height="25px" MinDate="1900-01-01" Width="178px">
                                        <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                        </Calendar>
                                        <DateInput DateFormat="MM-dd-yyyy" DisplayDateFormat="MM-dd-yyyy" Height="25px" LabelWidth="40%">
                                            <EmptyMessageStyle Resize="None" />
                                            <ReadOnlyStyle Resize="None" />
                                            <FocusedStyle Resize="None" />
                                            <DisabledStyle Resize="None" />
                                            <InvalidStyle Resize="None" />
                                            <HoveredStyle Resize="None" />
                                            <EnabledStyle Resize="None" />
                                        </DateInput>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btn_UpdateRegistration" runat="server" OnClick="btn_register_Click" Text="Update Registration">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                        </table>




                    </telerik:RadPageView>
                    <telerik:RadPageView ID="PageView_Placement" runat="server">
                        <telerik:RadGrid ID="Grid_DormitoryPlacement" runat="server" AutoGenerateColumns="False"
                            AllowPaging="True" AllowSorting="True" OnNeedDataSource="Grid_DormitoryPlacement_NeedDataSource"
                            GroupPanelPosition="Top" OnItemDataBound="Grid_DormitoryPlacement_ItemDataBound">

                            <MasterTableView TableLayout="Fixed" DataKeyNames="PlacementId">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="HostId" FilterControlAltText="Filter HostId column" HeaderText="Host ID" SortExpression="HostId" UniqueName="HostId">
                                    </telerik:GridBoundColumn>







                                    <telerik:GridBoundColumn DataField="HostRoomFloor" FilterControlAltText="Filter HostRoomFloor column" HeaderText="Floor Name" Visible="false" SortExpression="HostRoomFloor" UniqueName="HostRoomFloor">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="HostRoomName" FilterControlAltText="Filter HostRoomName column" HeaderText="Room Name" SortExpression="HostRoomName" UniqueName="HostRoomName">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="HostRoomType" FilterControlAltText="Filter HostRoomType column" HeaderText="Room Type" Visible="false" SortExpression="HostRoomType" UniqueName="HostRoomType">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="BedName" FilterControlAltText="Filter BedName column" HeaderText="Request ID " SortExpression="PlacementRequestId" UniqueName="PlacementRequestId">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PlacementRequestId" DataType="System.Int32" FilterControlAltText="Filter PlacementId column" HeaderText="Placement Request ID" SortExpression="PlacementId" UniqueName="PlacementId">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="PlacementId" DataType="System.Int32" FilterControlAltText="Filter PlacementId column" HeaderText="Placement ID" SortExpression="PlacementId" UniqueName="PlacementId">
                                    </telerik:GridBoundColumn>


                                    <telerik:GridBoundColumn DataField="PlacementDate" FilterControlAltText="Filter PlacementDate column"
                                        HeaderText="PlacementDate" SortExpression="CreatedDate" UniqueName="PlacementDate" DataType="System.DateTime" DataFormatString="{0: MM-dd-yyyy}">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="PlacementStatus" FilterControlAltText="Filter PlacementStatus column" HeaderText="Placement Status" SortExpression="PlacementStatus" UniqueName="PlacementStatus" DataType="System.Int32">
                                    </telerik:GridBoundColumn>


                                    <telerik:GridBoundColumn DataField="StartDate" DataFormatString="{0:MM-dd-yyyy}" FilterControlAltText="Filter StartDate column" HeaderText="Start Date" UniqueName="StartDate">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="EndDate" DataFormatString="{0:MM-dd-yyyy}" FilterControlAltText="Filter EndDate column" HeaderText="End Date" UniqueName="EndDate">
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
                    </telerik:RadPageView>
                </telerik:RadMultiPage>

            </telerik:RadPane>

        </telerik:RadSplitter>

    </telerik:RadAjaxPanel>



</asp:Content>

