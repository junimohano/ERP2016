<%@ Page Title="" Language="C#" MasterPageFile="~/School/Shared/Base.master" AutoEventWireup="true" CodeFile="HomestayHostRegistration.aspx.cs" Inherits="School.StudentHousing.HomestayHostRegistration" %>

<%@ Register TagPrefix="UserControl" TagName="FileDownloadList" Src="~/App_Data/FileDownloadList.ascx" %>

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
                                    <h4>Host Registration List</h4>
                                </td>
                                <td style="width: 20%" align="right">
                                    <telerik:RadButton ID="btn_registration" runat="server" Text="New Registration" OnClick="btn_registration_Click"></telerik:RadButton>
                                </td>
                            </tr>

                        </table>
                    </telerik:RadPane>

                    <telerik:RadPane ID="RadPane2" runat="server" Scrolling="None">


                        <telerik:RadGrid ID="Grid_HostList" runat="server" AutoGenerateColumns="false"
                            AllowPaging="True" Height="100%" AllowSorting="True" AllowFilteringByColumn="true"
                            OnSelectedIndexChanged="Grid_HostList_SelectedIndexChanged" OnItemDataBound="Grid_HostList_ItemDataBound" OnNeedDataSource="Grid_HostList_NeedDataSource"
                            FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">

                            <MasterTableView TableLayout="Fixed" DataKeyNames="HostId">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="HostId" DataType="System.Int32" FilterControlAltText="Filter HostId column" HeaderText="Host ID" SortExpression="HostId" UniqueName="HostId">
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

                                    <telerik:GridBoundColumn DataField="HouseActiveStutas" FilterControlAltText="Filter CreatedDate column" HeaderText="Host Status" SortExpression="HouseActiveStutas" UniqueName="HouseActiveStutas" DataType="System.Int32">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="FatherFirstName" FilterControlAltText="Filter FatherFirstName column" HeaderText="Father's First Name" SortExpression="FatherFirstName" UniqueName="FatherFirstName">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FatherLastName" FilterControlAltText="Filter FatherLastName column" HeaderText="Father's Last Name" SortExpression="FatherLastName" UniqueName="FatherLastName">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="MotherFirstName" FilterControlAltText="Filter MotherFirstName column" HeaderText="Mother's First Name" SortExpression="MotherFirstName" UniqueName="MotherFirstName">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="MotherLastName" FilterControlAltText="Filter MotherLastName column" HeaderText="Mother's Last Name" SortExpression="MotherLastName" UniqueName="MotherLastName">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn1 column" HeaderText="Family Member" UniqueName="TemplateColumn5" AllowFiltering="false">
                                        <ItemTemplate>
                                            <telerik:RadLabel runat="server" ID="lbl_FamilyMember" Text="0"></telerik:RadLabel>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
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
                                    <telerik:GridBoundColumn DataField="HostRanking" FilterControlAltText="Filter HostRanking column" HeaderText="Ranking" SortExpression="HostRanking" UniqueName="HostRanking">
                                    </telerik:GridBoundColumn>

                                </Columns>
                                <PagerStyle AlwaysVisible="True" />
                            </MasterTableView><GroupingSettings CollapseAllTooltip="Collapse all groups" />
                            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" AllowColumnsReorder="True" ReorderColumnsOnClient="True">
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
                            <telerik:RadTabStrip runat="server" Width="100%" SelectedIndex="3" MultiPageID="MultiPage_Registration" ID="Tab_Host" OnTabClick="Tab_Host_TabClick">
                                <Tabs>
                                    <telerik:RadTab runat="server" TabIndex="0" Text="Host Basic Information" PageViewID="PageView_Basic" Selected="True">
                                    </telerik:RadTab>
                                    <telerik:RadTab runat="server" TabIndex="1" Text="Host Family Member" PageViewID="PageView_FamilyMember">
                                    </telerik:RadTab>
                                    <telerik:RadTab runat="server" TabIndex="2" Text="Host Home Room" PageViewID="PageView_Room">
                                    </telerik:RadTab>
                                    <telerik:RadTab runat="server" TabIndex="3" Text="Host Home Bed" PageViewID="PageView_Bed">
                                    </telerik:RadTab>
                                    <telerik:RadTab runat="server" TabIndex="4" Text="School" PageViewID="PageView_School">
                                    </telerik:RadTab>

                                    <telerik:RadTab runat="server" TabIndex="5" Text="Update Registration" PageViewID="PageView_Update">
                                    </telerik:RadTab>

                                    <telerik:RadTab runat="server" TabIndex="6" Text="Placement History" PageViewID="PageView_Placement">
                                    </telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>
                        </td>
                        <td style="width: 20%"></td>

                    </tr>

                </table>

                <telerik:RadMultiPage runat="server" ID="MultiPage_Registration" SelectedIndex="3" Width="100%">
                    <telerik:RadPageView runat="server" ID="PageView_Basic" Visible="true">

                        <br />
                        <table border="0">
                            <tr>
                                <td style="width: 20%" align="left">
                                    <telerik:RadLabel ID="Label12" runat="server" BackColor="#121212" Font-Bold="False" ForeColor="#E0E0E0" Text="Host Father's Information:"></telerik:RadLabel>

                                </td>
                                <td style="width: 80%" align="right">

                                    <telerik:RadButton ID="btn_basic_save" runat="server" Text="Save Basic Information" OnClick="btn_basic_save_Click" BorderStyle="Outset" ValidationGroup="Info">
                                    </telerik:RadButton>
                                    &nbsp;<telerik:RadButton ID="btn_basic_cancel" runat="server" Text="Cancel" OnClick="btn_basic_cancel_Click">
                                    </telerik:RadButton>
                                </td>

                            </tr>

                        </table>

                        <table width="90%" border="0" cellpadding="1" cellspacing="1" frame="border">


                            <tr>
                                <td style="width: 5%">
                                    <telerik:RadLabel ID="Label4" runat="server" Text="Father's First Name:"></telerik:RadLabel>
                                </td>
                                <td style="width: 20%">
                                    <telerik:RadTextBox ID="txtFatherFirstName" runat="server" Width="80%"></telerik:RadTextBox>
                                    <asp:Label ID="Label60" runat="server" ForeColor="Red" Text="*" Font-Bold="true"></asp:Label>
                                    <br style="clear: both;" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFatherFirstName" Display="Dynamic" ErrorMessage="Father's Firstname Required" ForeColor="Red" ValidationGroup="Info" />
                                </td>
                                <td style="width: 5%">
                                    <telerik:RadLabel ID="Label5" runat="server" Text="Father's Last Name:"></telerik:RadLabel>
                                </td>
                                <td style="width: 20%">
                                    <telerik:RadTextBox ID="txtFatherLastName" runat="server" Width="80%"></telerik:RadTextBox>
                                    <asp:Label ID="Label61" runat="server" ForeColor="Red" Text="*" Font-Bold="true"></asp:Label>
                                    <br style="clear: both;" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFatherLastName" Display="Dynamic" ErrorMessage="Father's Lastname Required" ForeColor="Red" ValidationGroup="Info" />
                                </td>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="Label7" runat="server" Text="Father's D.O.B.:"></telerik:RadLabel>
                                </td>
                                <td style="width: 10%">
                                    <telerik:RadDatePicker ID="DatePickFatherDOB" runat="server" Culture="en-US" Height="25px" MinDate="1900-01-01" Width="178px">
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
                                <td style="width: 10%" align="right">
                                    <telerik:RadLabel ID="Label6" runat="server" Text="Father's Occupation:"></telerik:RadLabel>
                                </td>
                                <td style="width: 20%">
                                    <telerik:RadTextBox ID="txtFatherOccupation" runat="server" Width="70%"></telerik:RadTextBox>
                                </td>
                            </tr>


                            <tr>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="Label11" runat="server" Text="CRC Required:"></telerik:RadLabel>
                                </td>
                                <td style="width: 20%">&nbsp;<telerik:RadDropDownList ID="ddlFatherCRC" runat="server" SelectedText="No" SelectedValue="0">
                                    <Items>
                                        <telerik:DropDownListItem runat="server" Selected="True" Text="No" Value="0" />
                                        <telerik:DropDownListItem runat="server" Text="Yes" Value="1" />
                                    </Items>
                                </telerik:RadDropDownList>
                                </td>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="Label9" runat="server" Text="Being Guardian:"></telerik:RadLabel>
                                </td>
                                <td style="width: 20%">
                                    <telerik:RadDropDownList ID="ddlFatherGuardian" runat="server" SelectedText="No" SelectedValue="0">
                                        <Items>
                                            <telerik:DropDownListItem runat="server" Selected="True" Text="No" Value="0" />
                                            <telerik:DropDownListItem runat="server" Text="Yes" Value="1" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="Label10" runat="server" Text="Father's Email:"></telerik:RadLabel>
                                </td>
                                <td style="width: 30%" colspan="3">
                                    <telerik:RadTextBox ID="txtFatherEmail" runat="server" Width="90%"></telerik:RadTextBox>
                                </td>
                            </tr>


                        </table>
                        <br />
                        <telerik:RadLabel ID="Label2" runat="server" BackColor="#121212" BorderColor="#3366CC" Font-Bold="False" ForeColor="#E0E0E0" Text="Host Mother's Information:     "></telerik:RadLabel>

                        <table width="90%" border="0" cellpadding="1" cellspacing="1" frame="border">


                            <tr>
                                <td style="width: 5%">
                                    <telerik:RadLabel ID="Label8" runat="server" Text="Mother's First Name:"></telerik:RadLabel>

                                </td>
                                <td style="width: 20%">
                                    <telerik:RadTextBox ID="txtMotherFirstName" runat="server" Width="80%"></telerik:RadTextBox>
                                    <asp:Label ID="Label62" runat="server" ForeColor="Red" Text="*" Font-Bold="true"></asp:Label>
                                    <br style="clear: both;" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMotherFirstName" Display="Dynamic" ErrorMessage="Mother's Firstname Required" ForeColor="Red" ValidationGroup="Info" />
                                </td>
                                <td style="width: 5%">
                                    <telerik:RadLabel ID="Label15" runat="server" Text="Mother's Last Name:"></telerik:RadLabel>
                                </td>
                                <td style="width: 20%">
                                    <telerik:RadTextBox ID="txtMotherLastName" runat="server" Width="80%"></telerik:RadTextBox>
                                    <asp:Label ID="Label63" runat="server" ForeColor="Red" Text="*" Font-Bold="true"></asp:Label>
                                    <br style="clear: both;" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMotherLastName" Display="Dynamic" ErrorMessage="Mother's Lastname Required" ForeColor="Red" ValidationGroup="Info" />
                                </td>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="Label16" runat="server" Text="Mother's D.O.B.:"></telerik:RadLabel>
                                </td>
                                <td style="width: 10%">
                                    <telerik:RadDatePicker ID="DatePickMotherDOB" runat="server" Culture="en-US" Height="25px" MinDate="1900-01-01" Width="178px">
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
                                <td style="width: 10%" align="right">
                                    <telerik:RadLabel ID="Label17" runat="server" Text="Mother's Occupation:"></telerik:RadLabel>
                                </td>
                                <td style="width: 20%">
                                    <telerik:RadTextBox ID="txtMotherOccupation" runat="server" Width="70%"></telerik:RadTextBox>
                                </td>
                            </tr>


                            <tr>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="Label18" runat="server" Text="CRC Required:"></telerik:RadLabel>
                                </td>
                                <td style="width: 20%">
                                    <telerik:RadDropDownList ID="ddlMotherCRC" runat="server" SelectedText="No" SelectedValue="0">
                                        <Items>
                                            <telerik:DropDownListItem runat="server" Selected="True" Text="No" Value="0" />
                                            <telerik:DropDownListItem runat="server" Text="Yes" Value="1" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="Label19" runat="server" Text="Being Guardian:"></telerik:RadLabel>
                                </td>
                                <td style="width: 20%">
                                    <telerik:RadDropDownList ID="ddlMotherGuardian" runat="server" SelectedText="No" SelectedValue="0">
                                        <Items>
                                            <telerik:DropDownListItem runat="server" Selected="True" Text="No" Value="0" />
                                            <telerik:DropDownListItem runat="server" Text="Yes" Value="1" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="Label20" runat="server" Text="Mother's Email:"></telerik:RadLabel>
                                </td>
                                <td style="width: 30%" colspan="3">
                                    <telerik:RadTextBox ID="txtMotherEmail" runat="server" Width="90%"></telerik:RadTextBox>
                                </td>
                            </tr>


                        </table>
                        <br />
                        <telerik:RadLabel ID="Label21" runat="server" BackColor="#121212" BorderColor="#3366CC" Font-Bold="False" ForeColor="#E0E0E0" Text="Host Home Information:     "></telerik:RadLabel>

                        <table width="90%" border="0" cellpadding="1" cellspacing="1" frame="border">

                            <tr>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="RadLabel6" runat="server" Text="Site:">
                                    </telerik:RadLabel>
                                </td>
                                <td style="width: 15%">
                                    <telerik:RadComboBox ID="RadComboBoxSite" EmptyMessage="Choose a Site" CssClass="RadSizeMiddle" runat="server" AutoPostBack="true" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" OnSelectedIndexChanged="RadComboBoxSite_OnSelectedIndexChanged" />
                                    <asp:Label ID="Label57" runat="server" ForeColor="Red" Text="*" Font-Bold="true"></asp:Label>
                                    <br style="clear: both;" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="RadComboBoxSite" Display="Dynamic" ErrorMessage="Site Required" ForeColor="Red" ValidationGroup="Info" />
                                </td>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="RadLabel13" runat="server" Text="Site Location:">
                                    </telerik:RadLabel>
                                </td>
                                <td colspan="2">
                                    <telerik:RadComboBox ID="RadComboBoxSiteLocation" EmptyMessage="Choose a Site Location" CssClass="RadSizeMiddle" runat="server" AutoPostBack="False" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                    <asp:Label ID="Label56" runat="server" ForeColor="Red" Text="*" Font-Bold="true"></asp:Label>
                                    <br style="clear: both;" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="RadComboBoxSiteLocation" Display="Dynamic" ErrorMessage="Site Location Required" ForeColor="Red" ValidationGroup="Info" />
                                </td>
                                <td style="width: 15%">&nbsp;</td>
                                <td style="width: 10%" align="right">&nbsp;</td>
                                <td style="width: 15%">&nbsp;</td>
                            </tr>

                            <tr>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="Label22" runat="server" Text="Home Address:">
                                    </telerik:RadLabel>
                                </td>
                                <td style="width: 15%">
                                    <telerik:RadTextBox ID="txtHomeAddress" runat="server" Width="80%">
                                    </telerik:RadTextBox>
                                    <asp:Label ID="Label64" runat="server" Font-Bold="true" ForeColor="Red" Text="*"></asp:Label>
                                    <br style="clear: both;" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtHomeAddress" Display="Dynamic" ErrorMessage="Home Address Required" ForeColor="Red" ValidationGroup="Info" />
                                </td>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="Label23" runat="server" Text="City:">
                                    </telerik:RadLabel>
                                </td>
                                <td style="width: 15%">
                                    <telerik:RadTextBox ID="txtCity" runat="server" Width="80%">
                                    </telerik:RadTextBox>
                                    <asp:Label ID="Label65" runat="server" Font-Bold="true" ForeColor="Red" Text="*"></asp:Label>
                                    <br style="clear: both;" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCity" Display="Dynamic" ErrorMessage="City Required" ForeColor="Red" ValidationGroup="Info" />
                                </td>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="RadLabel16" runat="server" Text="Provice:">
                                    </telerik:RadLabel>
                                </td>
                                <td style="width: 15%">
                                    <telerik:RadDropDownList ID="ddlProvice" runat="server">
                                    </telerik:RadDropDownList>
                                    <asp:Label ID="Label66" runat="server" Font-Bold="true" ForeColor="Red" Text="*"></asp:Label>
                                    <br style="clear: both;" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlProvice" Display="Dynamic" ErrorMessage="Province Required" ForeColor="Red" ValidationGroup="Info" />
                                </td>
                                <td align="right" style="width: 10%">
                                    <telerik:RadLabel ID="Label25" runat="server" Text="Postal Code:">
                                    </telerik:RadLabel>
                                </td>
                                <td style="width: 15%">
                                    <telerik:RadTextBox ID="txtPostalCode" runat="server" Width="70%">
                                    </telerik:RadTextBox>
                                    <asp:Label ID="Label67" runat="server" Font-Bold="true" ForeColor="Red" Text="*"></asp:Label>
                                    <br style="clear: both;" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPostalCode" Display="Dynamic" ErrorMessage="PostalCode Required" ForeColor="Red" ValidationGroup="Info" />
                                </td>
                            </tr>

                            <tr>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="Label24" runat="server" Text="Home Phone No:"></telerik:RadLabel>
                                </td>
                                <td style="width: 15%">
                                    <telerik:RadTextBox ID="txtHomePhone" runat="server" Width="80%" EmptyMessage="416-123-1234 ext.123" LabelWidth="40%" Resize="None">
                                    </telerik:RadTextBox>
                                    <asp:Label ID="Label68" runat="server" ForeColor="Red" Text="*" Font-Bold="true"></asp:Label>
                                    <br style="clear: both;" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPostalCode" Display="Dynamic" ErrorMessage="Home Phone Required" ForeColor="Red" ValidationGroup="Info" />
                                </td>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="Label26" runat="server" Text="Work Phone No.:" EmptyMessage="416-123-1234 ext.123"></telerik:RadLabel>
                                </td>
                                <td style="width: 15%">
                                    <telerik:RadTextBox ID="txtWorkPhone" runat="server" Width="80%" EmptyMessage="416-123-1234 ext.123"></telerik:RadTextBox>
                                </td>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="RadLabel17" runat="server" Text="Cell Phone No.:" EmptyMessage="416-123-1234 ext.123"></telerik:RadLabel>
                                </td>
                                <td style="width: 15%">
                                    <telerik:RadTextBox ID="txtCellPhone" runat="server" Width="80%" EmptyMessage="416-123-1234 ext.123"></telerik:RadTextBox>
                                </td>
                                <td style="width: 10%" align="right">
                                    <telerik:RadLabel ID="Label27" runat="server" Text="Additional Phone No.:" EmptyMessage="416-123-1234 ext.123"></telerik:RadLabel>
                                </td>
                                <td style="width: 15%">
                                    <telerik:RadTextBox ID="txtAdditionalPhone" runat="server" Width="80%" EmptyMessage="416-123-1234 ext.123"></telerik:RadTextBox>
                                </td>
                            </tr>


                            <tr>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="Label28" runat="server" Text="English at home:"></telerik:RadLabel>
                                </td>
                                <td style="width: 20%">
                                    <telerik:RadDropDownList ID="ddlEnligsh" runat="server" SelectedText="No" SelectedValue="0">
                                        <Items>
                                            <telerik:DropDownListItem runat="server" Selected="True" Text="No" Value="0" />
                                            <telerik:DropDownListItem runat="server" Text="Yes" Value="1" />
                                        </Items>
                                    </telerik:RadDropDownList>

                                </td>
                                <td style="width: 10%">

                                    <telerik:RadLabel ID="Label29" runat="server" Text="Language daily: "></telerik:RadLabel>

                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtLanguage" runat="server" Width="80%"></telerik:RadTextBox>

                                </td>
                                <td style="width: 10%">

                                    <telerik:RadLabel ID="RadLabel5" runat="server" Text="Home Style: "></telerik:RadLabel>

                                </td>
                                <td style="width: 20%" colspan="3">
                                    <telerik:RadDropDownList ID="ddlHomeType" runat="server">
                                        <Items>
                                            <telerik:DropDownListItem Text="Condo" Value="1" Selected="True" />
                                            <telerik:DropDownListItem Text="Apartment" Value="2" />
                                            <telerik:DropDownListItem Text="TownHouse" Value="3" />
                                            <telerik:DropDownListItem Text="Semi-Detached" Value="4" />
                                            <telerik:DropDownListItem Text="Detached" Value="5" />
                                        </Items>
                                    </telerik:RadDropDownList>

                                </td>

                            </tr>


                        </table>

                        <br />
                        <telerik:RadLabel ID="RadLabel7" runat="server" BackColor="#121212" BorderColor="#3366CC" Font-Bold="False" ForeColor="#E0E0E0" Text="Host Options Information:     "></telerik:RadLabel>

                        <table width="90%" border="0" cellpadding="1" cellspacing="1" frame="border">


                            <tr>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="RadLabel8" runat="server" Text="Smoking Permitted:"></telerik:RadLabel>
                                </td>
                                <td style="width: 15%">
                                    <telerik:RadDropDownList ID="ddlSmoking" runat="server" SelectedText="No" SelectedValue="0">
                                        <Items>
                                            <telerik:DropDownListItem runat="server" Selected="True" Text="No" Value="0" />
                                            <telerik:DropDownListItem runat="server" Text="Yes" Value="1" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="RadLabel9" runat="server" Text="Pet Allowed:"></telerik:RadLabel>
                                </td>
                                <td style="width: 10%">
                                    <telerik:RadDropDownList ID="ddlPet" runat="server" SelectedText="No" SelectedValue="0">
                                        <Items>
                                            <telerik:DropDownListItem runat="server" Selected="True" Text="No" Value="0" />
                                            <telerik:DropDownListItem runat="server" Text="Yes" Value="1" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                                <td style="width: 15%">
                                    <telerik:RadLabel ID="RadLabel10" runat="server" Text="House Alarm System:"></telerik:RadLabel>
                                </td>
                                <td style="width: 20%">
                                    <telerik:RadDropDownList ID="ddlHomeAlarm" runat="server" SelectedText="No" SelectedValue="0">
                                        <Items>
                                            <telerik:DropDownListItem runat="server" Selected="True" Text="No" Value="0" />
                                            <telerik:DropDownListItem runat="server" Text="Yes" Value="1" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="RadLabel11" runat="server" Text="Internet Offered:"></telerik:RadLabel>
                                </td>
                                <td style="width: 10%">

                                    <telerik:RadDropDownList ID="ddlInternetOffered" runat="server" SelectedText="Yes" SelectedValue="1">
                                        <Items>

                                            <telerik:DropDownListItem runat="server" Selected="True" Text="Yes" Value="1" />
                                            <telerik:DropDownListItem runat="server" Text="No" Value="0" />
                                        </Items>
                                    </telerik:RadDropDownList>


                                </td>
                            </tr>


                            <tr>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="Label30" runat="server" Text="Internet Type:"></telerik:RadLabel>
                                </td>
                                <td style="width: 15%">
                                    <telerik:RadDropDownList ID="ddlInternetType" runat="server" SelectedText="Wireless" SelectedValue="Wireless">
                                        <Items>
                                            <telerik:DropDownListItem runat="server" Selected="True" Text="Wireless" Value="Wireless" />
                                            <telerik:DropDownListItem runat="server" Text="Wire" Value="Wire" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="Label31" runat="server" Text="Internet Extra Charge:"></telerik:RadLabel>
                                </td>
                                <td style="width: 10%">&nbsp;
                          <telerik:RadTextBox ID="txtExtraCharge" runat="server" Width="45px" Text="0"></telerik:RadTextBox>
                                    &nbsp;
                          <telerik:RadLabel ID="Label32" runat="server" Text="Per Month"></telerik:RadLabel>
                                </td>
                                <td style="width: 15%">
                                    <telerik:RadLabel ID="Label34" runat="server" Text="Usage Allowance:"></telerik:RadLabel>
                                </td>
                                <td style="width: 20%">
                                    <telerik:RadDropDownList ID="ddlUsage" runat="server" SelectedText="Unlimited">
                                        <Items>
                                            <telerik:DropDownListItem runat="server" Selected="True" Text="Unlimited" Value="Unlimited" />
                                            <telerik:DropDownListItem runat="server" Text="Limited" Value="Limited" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                                <td style="width: 10%">

                                    <telerik:RadLabel ID="Label35" runat="server" Text="Internet Included:"></telerik:RadLabel>

                                </td>
                                <td style="width: 10%">


                                    <telerik:RadDropDownList ID="ddlInternetIncluded" runat="server" SelectedText="No" SelectedValue="0">
                                        <Items>
                                            <telerik:DropDownListItem runat="server" Selected="True" Text="No" Value="0" />
                                            <telerik:DropDownListItem runat="server" Text="Yes" Value="1" />
                                        </Items>
                                    </telerik:RadDropDownList>


                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="RadLabel14" runat="server" Text="Preferred Gender:"></telerik:RadLabel>
                                </td>
                                <td style="width: 15%">
                                    <telerik:RadDropDownList ID="RDLHostGender" runat="server" SelectedText="No">
                                        <Items>
                                            <telerik:DropDownListItem runat="server" Selected="True" Text="No Matter" Value="0" />
                                            <telerik:DropDownListItem runat="server" Text="Male" Value="1" />
                                            <telerik:DropDownListItem runat="server" Text="Female" Value="2" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="RadLabel15" runat="server" Text="Ranking:"></telerik:RadLabel>
                                </td>
                                <td style="width: 10%">
                                    <telerik:RadTextBox ID="txtRanking" runat="server" Width="45px" Text="0"></telerik:RadTextBox>
                                </td>
                                <td style="width: 15%">&nbsp;</td>
                                <td style="width: 20%">&nbsp;</td>
                                <td style="width: 10%">&nbsp;</td>
                                <td style="width: 10%">&nbsp;</td>
                            </tr>


                        </table>
                        <br />
                        <telerik:RadLabel ID="Label1" runat="server" BackColor="#121212" BorderColor="#3366CC" Font-Bold="False" ForeColor="#E0E0E0" Text="Host Meal Plan and Fridge Accesibility:     "></telerik:RadLabel>

                        <table width="90%" border="0" cellpadding="1" cellspacing="1" frame="border">


                            <tr>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="Label3" runat="server" Text="Breakfast Type:"></telerik:RadLabel>
                                </td>
                                <td style="width: 15%">
                                    <telerik:RadDropDownList ID="ddlBreakfast" runat="server" SelectedText="Self Serve" SelectedValue="0">
                                        <Items>
                                            <telerik:DropDownListItem runat="server" Selected="True" Text="Self Serve" Value="0" />
                                            <telerik:DropDownListItem runat="server" Text="Offered" Value="1" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="Label13" runat="server" Text="Lunch Type:"></telerik:RadLabel>
                                </td>
                                <td style="width: 10%">
                                    <telerik:RadDropDownList ID="ddlLunch" runat="server" SelectedText="Self Serve" SelectedValue="0">
                                        <Items>
                                            <telerik:DropDownListItem runat="server" Selected="True" Text="Self Serve" Value="0" />
                                            <telerik:DropDownListItem runat="server" Text="Offered" Value="1" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                                <td style="width: 15%">
                                    <telerik:RadLabel ID="Label14" runat="server" Text="Dinner Type:"></telerik:RadLabel>
                                </td>
                                <td style="width: 20%">
                                    <telerik:RadDropDownList ID="ddlDinner" runat="server" SelectedText="With Family" SelectedValue="0">
                                        <Items>
                                            <telerik:DropDownListItem runat="server" Selected="True" Text="With Family" Value="0" />
                                            <telerik:DropDownListItem runat="server" Text="Curfew Time" Value="1" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="Label33" runat="server" Text="Fridge:"></telerik:RadLabel>
                                </td>
                                <td style="width: 10%">

                                    <telerik:RadDropDownList ID="ddlFridge" runat="server">
                                        <Items>
                                            <telerik:DropDownListItem runat="server" Selected="True" Text="Shared Anytime " Value="0" />
                                            <telerik:DropDownListItem runat="server" Text="Shared Limited" Value="1" />
                                            <telerik:DropDownListItem runat="server" Text="Seperated Anytime" Value="2" />
                                            <telerik:DropDownListItem runat="server" Text="Seperated Limited" Value="3" />
                                        </Items>
                                    </telerik:RadDropDownList>


                                </td>
                            </tr>
                        </table>
                        <br />


                        <telerik:RadLabel ID="Label36" runat="server" BackColor="#121212" BorderColor="#3366CC" Font-Bold="False" ForeColor="#E0E0E0" Text="Host Emergency Contact:     "></telerik:RadLabel>
                        <table width="90%" border="0" cellpadding="1" cellspacing="1" frame="border">
                            <tr>

                                <td style="width: 8%">
                                    <telerik:RadLabel ID="Label37" runat="server" Text="Contact Person Name:"></telerik:RadLabel>
                                </td>
                                <td style="width: 15%">
                                    <telerik:RadTextBox ID="txtContactName" runat="server" Width="80%"></telerik:RadTextBox>
                                </td>
                                <td style="width: 4%">
                                    <telerik:RadLabel ID="Label38" runat="server" Text="Phone No.:" EmptyMessage="416-123-1234 ext.123"></telerik:RadLabel>
                                </td>
                                <td style="width: 15%">
                                    <telerik:RadTextBox ID="txtContactPhone" runat="server" Width="80%" EmptyMessage="416-123-1234 ext.123"></telerik:RadTextBox>
                                </td>
                                <td style="width: 2%">
                                    <telerik:RadLabel ID="Label39" runat="server" Text="Relationship:" Width="80%"></telerik:RadLabel>
                                </td>
                                <td style="width: 20%">
                                    <telerik:RadTextBox ID="txtRelationship" runat="server"></telerik:RadTextBox>
                                </td>
                                <td style="width: 10%">&nbsp;</td>
                            </tr>

                        </table>
                        <br />

                        <telerik:RadLabel ID="RadLabel18" runat="server" BackColor="#121212" BorderColor="#3366CC" Font-Bold="False" ForeColor="#E0E0E0" Text="Homesaty Host's File:     "></telerik:RadLabel>

                        <table width="90%" border="0" cellpadding="1" cellspacing="1" frame="border">


                            <tr>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="RadLabel19" runat="server" Text="CRC File:"></telerik:RadLabel>
                                </td>
                                <td>
                                    <UserControl:FileDownloadList ID="file_upload" runat="server" />

                                </td>
                            </tr>
                        </table>

                    </telerik:RadPageView>

                    <telerik:RadPageView runat="server" ID="PageView_FamilyMember" Visible="true">
                        <br />
                        <telerik:RadLabel ID="Label40" runat="server" BackColor="#121212" BorderColor="#3366CC" Font-Bold="False" ForeColor="#E0E0E0" Text="Children or other family information:     "></telerik:RadLabel>

                        <table width="90%" border="0" cellpadding="1" cellspacing="1" frame="border">


                            <tr>
                                <td style="width: 5%">&nbsp;</td>
                                <td style="width: 20%">&nbsp;</td>
                                <td style="width: 5%">&nbsp;</td>
                                <td style="width: 20%">&nbsp;</td>
                                <td style="width: 10%">&nbsp;</td>
                                <td style="width: 10%">&nbsp;</td>
                                <td style="width: 10%" align="right">&nbsp;</td>
                                <td style="width: 20%">&nbsp;</td>
                            </tr>


                            <tr>
                                <td style="width: 5%">
                                    <telerik:RadLabel ID="Label41" runat="server" Text="First Name:"></telerik:RadLabel>
                                </td>
                                <td style="width: 20%">
                                    <telerik:RadTextBox ID="txtMemberFirstName" runat="server" Width="80%"></telerik:RadTextBox>
                                </td>
                                <td style="width: 5%">
                                    <telerik:RadLabel ID="Label42" runat="server" Text="Last Name:"></telerik:RadLabel>
                                </td>
                                <td style="width: 20%">
                                    <telerik:RadTextBox ID="txtMemberLastName" runat="server" Width="80%"></telerik:RadTextBox>
                                </td>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="Label43" runat="server" Text="D.O.B.:"></telerik:RadLabel>
                                </td>
                                <td style="width: 10%">
                                    <telerik:RadDatePicker ID="DatePickMemberDOB" runat="server" Culture="en-US" Height="25px" MinDate="1900-01-01" Width="178px">
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
                                <td align="right" style="width: 10%">
                                    <telerik:RadLabel ID="Label54" runat="server" Text="Gender:"></telerik:RadLabel>
                                </td>
                                <td style="width: 20%">
                                    <telerik:RadDropDownList ID="ddlMemberGender" runat="server" SelectedText="No">
                                        <Items>
                                            <telerik:DropDownListItem runat="server" Selected="True" Text="Male" Value="1" />
                                            <telerik:DropDownListItem runat="server" Text="Female" Value="0" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                            </tr>


                            <tr>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="Label45" runat="server" Text="CRC Required:"></telerik:RadLabel>
                                </td>
                                <td style="width: 20%">
                                    <telerik:RadDropDownList ID="ddlMemberCRC" runat="server" SelectedText="No">
                                        <Items>
                                            <telerik:DropDownListItem runat="server" Selected="True" Text="No" Value="0" />
                                            <telerik:DropDownListItem runat="server" Text="Yes" Value="1" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="Label46" runat="server" Text="Living at home:"></telerik:RadLabel>
                                </td>
                                <td style="width: 20%">
                                    <telerik:RadDropDownList ID="ddlLivingHome" runat="server" SelectedText="Yes">
                                        <Items>
                                            <telerik:DropDownListItem runat="server" Selected="True" Text="Yes" Value="1" />
                                            <telerik:DropDownListItem runat="server" Text="No" Value="0" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="Label47" runat="server" Text="Relationship:"></telerik:RadLabel>
                                </td>
                                <td style="width: 30%" colspan="3">
                                    <telerik:RadTextBox ID="txtMemberRelationship" runat="server" Width="93%"></telerik:RadTextBox>
                                </td>
                            </tr>

                            <tr>
                                <td style="width: 10%">&nbsp;</td>
                                <td style="width: 20%">&nbsp;</td>
                                <td style="width: 10%">&nbsp;</td>
                                <td style="width: 20%">&nbsp;</td>
                                <td style="width: 10%">&nbsp;</td>
                                <td colspan="3" style="width: 30%" align="right">
                                    <telerik:RadButton ID="btn_Member" runat="server" OnClick="btn_Member_Click" Style="right: 0px; top: 0px;" Text="Save">
                                    </telerik:RadButton>
                                    <telerik:RadButton ID="btn_cancel_familyMember0" runat="server" OnClick="btn_cancel_familyMember_Click" Text="Clear">
                                    </telerik:RadButton>
                                </td>
                            </tr>

                        </table>
                        <br />
                        <table border="0" width="90%">
                            <tr>

                                <td style="width: 100%">
                                    <telerik:RadGrid ID="Grid_FamilyMember" runat="server" AutoGenerateColumns="False" GroupPanelPosition="Top" OnItemDataBound="Grid_FamilyMember_ItemDataBound" OnNeedDataSource="Grid_FamilyMember_NeedDataSource" OnSelectedIndexChanged="Grid_FamilyMember_SelectedIndexChanged" AllowPaging="True" AllowSorting="True">
                                        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                        <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                                            <Selecting AllowRowSelect="True" />
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                        </ClientSettings>
                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                                        <MasterTableView DataKeyNames="FamilyMemberId">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="MemberFirstName" FilterControlAltText="Filter column column" HeaderText="First Name" UniqueName="column">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="MemberLastName" FilterControlAltText="Filter column1 column" HeaderText="Last Name" UniqueName="column1">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Relationship" HeaderText="Relationship" FilterControlAltText="Filter column2 column" UniqueName="column2">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridCheckBoxColumn DataField="MemberCRC" DataType="System.Boolean" FilterControlAltText="Filter column3 column" HeaderText="CRC Required" UniqueName="column3">
                                                </telerik:GridCheckBoxColumn>
                                                <telerik:GridCheckBoxColumn DataField="MemberLivingHome" DataType="System.Boolean" FilterControlAltText="Filter column4 column" HeaderText="Living at Home" UniqueName="column4">
                                                </telerik:GridCheckBoxColumn>
                                                <telerik:GridBoundColumn DataField="HostId" FilterControlAltText="Filter column5 column" HeaderText="HostId" UniqueName="column5" Visible="False">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridDateTimeColumn DataField="MemberDOB" DataFormatString="{0:MM-dd-yyy}" FilterControlAltText="Filter column6 column" HeaderText="Member D.O.B" UniqueName="column6">
                                                </telerik:GridDateTimeColumn>
                                                <telerik:GridBoundColumn DataField="MemberGender" FilterControlAltText="Filter column7 column" HeaderText="Gender" UniqueName="FamilyMember_Gender">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>

                            </tr>

                        </table>
                        <table border="0" width="90%">
                            <tr>
                                <td align="center">&nbsp;</td>

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
                                    <telerik:RadLabel ID="Label49" runat="server" Text="Room Location:"></telerik:RadLabel>
                                </td>
                                <td style="width: 20%">
                                    <telerik:RadDropDownList ID="ddlRoomLocation" runat="server">
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
                                    <telerik:RadLabel ID="Label69" runat="server" Text="Room Type:">
                                    </telerik:RadLabel>
                                    &nbsp;
                                </td>
                                <td>
                                    <telerik:RadDropDownList ID="ddlRoomType" runat="server">
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
                                                <telerik:GridBoundColumn DataField="HostRoomFloor" FilterControlAltText="Filter column1 column" HeaderText="Floor Location" UniqueName="HostRoomFloor">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="HostBedId" FilterControlAltText="Filter column5 column" HeaderText="HostBedId" UniqueName="HostBedId">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Homestay Placement" UniqueName="Temp" Visible="false">
                                                    <ItemTemplate>

                                                        <telerik:RadGrid ID="GridPlaced" runat="server" AutoGenerateColumns="False" GroupPanelPosition="Top">
                                                            <MasterTableView DataKeyNames="BedId">
                                                                <Columns>
                                                                    <telerik:GridBoundColumn DataField="StartDate" FilterControlAltText="Filter column5 column" DataFormatString="{0:MM-dd-yyyy}" HeaderText="Start Date" UniqueName="StartDate">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="EndDate" FilterControlAltText="Filter column5 column" DataFormatString="{0:MM-dd-yyyy}" HeaderText="End Date" UniqueName="EndDate">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="StudentNo" FilterControlAltText="Filter column5 column" HeaderText="StudentNo." UniqueName="StudentNo">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Student" FilterControlAltText="Filter column5 column" HeaderText="Student Name" UniqueName="Student">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="PlacedBy" FilterControlAltText="Filter column5 column" HeaderText="Placed By" UniqueName="PlacedBy">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="PlacedDate" FilterControlAltText="Filter column5 column" DataFormatString="{0: MM-dd-yyyy}" HeaderText="Placed Date" UniqueName="PlacedDate">
                                                                    </telerik:GridBoundColumn>

                                                                </Columns>
                                                            </MasterTableView>

                                                        </telerik:RadGrid>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>








                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>
                            </tr>


                        </table>


                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="PageView_School" Visible="true">
                        <br />
                        <telerik:RadLabel ID="Label53" runat="server" BackColor="#121212" BorderColor="#3366CC" Font-Bold="False" ForeColor="#E0E0E0" Text="Host Preferred School:     "></telerik:RadLabel>
                        <table width="90%" border="0" cellpadding="1" cellspacing="1" frame="border">
                            <tr>

                                <td class="auto-style5" colspan="5"></td>
                            </tr>

                            <tr>
                                <td style="width: 10%">
                                    <telerik:RadLabel ID="Label58" runat="server" Text="Shcool:"></telerik:RadLabel>
                                </td>
                                <td colspan="4">
                                    <telerik:RadDropDownList ID="ddlSchoolName" runat="server" Width="80%" AutoPostBack="True" OnSelectedIndexChanged="ddlSchoolName_SelectedIndexChanged">
                                    </telerik:RadDropDownList>
                                </td>
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
                                                <telerik:GridBoundColumn DataField="HostId" FilterControlAltText="Filter column10 column" UniqueName="column10" Visible="false">
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
                        <telerik:RadLabel ID="Label59" runat="server" BackColor="#121212" BorderColor="#3366CC" Font-Bold="False" ForeColor="#E0E0E0" Text="Register Homestay Host:   ">
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
                                    <telerik:RadLabel ID="RadLabel1" runat="server" Text="Host Status: "></telerik:RadLabel>

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
                                    <telerik:RadLabel ID="RadLabel12" runat="server" Text="Active Date: "></telerik:RadLabel>
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
                                    <telerik:RadButton runat="server" ID="btn_UpdateRegistration" Text="Update Registration" OnClick="btn_register_Click">
                                    </telerik:RadButton>

                                </td>
                            </tr>

                        </table>




                    </telerik:RadPageView>
                    <telerik:RadPageView ID="PageView_Placement" runat="server">
                        <telerik:RadGrid ID="Grid_HomestayPlacement" runat="server" AutoGenerateColumns="False"
                            AllowPaging="True" AllowSorting="True" OnNeedDataSource="Grid_HomestayPlacement_NeedDataSource"
                            GroupPanelPosition="Top" OnItemDataBound="Grid_HomestayPlacement_ItemDataBound">

                            <MasterTableView TableLayout="Fixed" DataKeyNames="PlacementId">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="HostId" FilterControlAltText="Filter HostId column" HeaderText="Host ID" SortExpression="HostId" UniqueName="HostId">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="HostRoomFloor" FilterControlAltText="Filter HostRoomFloor column" HeaderText="Floor Name" SortExpression="HostRoomFloor" UniqueName="HostRoomFloor">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="HostRoomName" FilterControlAltText="Filter HostRoomName column" HeaderText="Room Name" SortExpression="HostRoomName" UniqueName="HostRoomName">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="HostRoomType" FilterControlAltText="Filter HostRoomType column" HeaderText="Room Type" SortExpression="HostRoomType" UniqueName="HostRoomType">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="BedName" FilterControlAltText="Filter BedName column" HeaderText="Request ID " SortExpression="PlacementRequestId" UniqueName="PlacementRequestId">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PlacementRequestId" DataType="System.Int32" FilterControlAltText="Filter PlacementId column" HeaderText="Placement ID" SortExpression="PlacementId" UniqueName="PlacementId">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="PlacementId" DataType="System.Int32" FilterControlAltText="Filter PlacementId column" HeaderText="Placement ID" SortExpression="PlacementId" UniqueName="PlacementId">
                                    </telerik:GridBoundColumn>


                                    <telerik:GridBoundColumn DataField="PlacementDate" FilterControlAltText="Filter PlacementDate column"
                                        HeaderText="PlacementDate" SortExpression="CreatedDate" UniqueName="PlacementDate" DataType="System.DateTime" DataFormatString="{0: MM-dd-yyyy}">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="PlacementStatus" FilterControlAltText="Filter PlacementStatus column" HeaderText="Placement Status" SortExpression="PlacementStatus" UniqueName="PlacementStatus" DataType="System.Int32">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="PlacementType" FilterControlAltText="Filter PlacementType column" HeaderText="Placement Type" SortExpression="PlacementType" UniqueName="PlacementType" DataType="System.Int32">
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

