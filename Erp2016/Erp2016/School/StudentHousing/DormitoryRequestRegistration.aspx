<%@ Page Title="" Language="C#" MasterPageFile="~/School/Shared/Base.master" AutoEventWireup="true" CodeFile="DormitoryRequestRegistration.aspx.cs" Inherits="School_StudentHousing_DormitoryRequestRegistration" %>

<%@ Register TagPrefix="UserControl" TagName="FileDownloadList" Src="~/App_Data/FileDownloadList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%" Width="100%">
        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="RadPane3" runat="server" Height="40%" Scrolling="None">
                <div style="display: none">

                    <asp:Button ID="ButtonGridRefresh" runat="server" OnClick="ButtonGridRefresh_OnClick" Text="Refresh" />
                </div>
                <telerik:RadSplitter runat="server" Orientation="Horizontal" Width="100%">


                    <telerik:RadPane ID="RadPaneTop" runat="server" Height="27px" Scrolling="None" >
                <h4>Dormitory Placement Request List</h4>
                    </telerik:RadPane>
                    <telerik:RadPane ID="Radpane1" runat="server" Height="40px" Width="100%">
                        <telerik:RadToolBar ID="RadToolBar3" runat="server" OnButtonClick="RadToolBar3_OnButtonClick">
                            <Items>
                                <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_new.png" Text="New Placement Request" />
                                <telerik:RadToolBarButton IsSeparator="true" />
                                <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_choose.png" Text=" Placement" Enabled="false"></telerik:RadToolBarButton>
                                <telerik:RadToolBarButton IsSeparator="true" />
                                <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_calendar.png" Text=" Schedule Change" Enabled="false" Visible="false"></telerik:RadToolBarButton>
                                <telerik:RadToolBarButton IsSeparator="true" Visible="false"  />
                                <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_delete.png" Text=" Cancel" Enabled="false" Visible ="false"></telerik:RadToolBarButton>
                                <telerik:RadToolBarButton IsSeparator="true" Visible="false" ></telerik:RadToolBarButton>
                                <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_reg.png" Text="Invoice/Pamyments" Enabled="false"></telerik:RadToolBarButton>
                            </Items>
                        </telerik:RadToolBar>
                    </telerik:RadPane>
                    <telerik:RadPane ID="RadPane2" runat="server" Scrolling="None">


                        <telerik:RadGrid ID="Grid_StudentList" runat="server" AutoGenerateColumns="false"
                            AllowPaging="True" Height="100%" AllowSorting="True"
                            OnSelectedIndexChanged="Grid_StudentList_SelectedIndexChanged" OnItemDataBound="Grid_StudentList_ItemDataBound" OnNeedDataSource="Grid_StudentList_NeedDataSource" GroupPanelPosition="Top"
                            AllowFilteringByColumn ="true" FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled ="true">

                            <MasterTableView TableLayout="Fixed" DataKeyNames="DormitoryRegistrationId">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="DormitoryRegistrationId" FilterControlAltText="Filter DormitoryRegistrationId" UniqueName="DormitoryRegistrationId" HeaderText="Placemnt Request ID">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="SchoolName" FilterControlAltText="Filter column01 column" HeaderText="Site" UniqueName="SchoolName">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Campus" FilterControlAltText="Filter Campus column" HeaderText="Site Location" UniqueName="Campus">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FirstName" FilterControlAltText="Filter FirstName column" HeaderText="First Name" UniqueName="FirstName">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="LastName" FilterControlAltText="Filter LastName column" HeaderText="Last Name" UniqueName="LastName">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DormitoryStudentStatus" FilterControlAltText="Filter DormitoryStudentStatus column" HeaderText="Placement Status" UniqueName="DormitoryStudentStatus">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Nationality" FilterControlAltText="Filter Nationality column" HeaderText="Nationality" UniqueName="Nationality">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="RegistrationDate" DataFormatString="{0:MM-dd-yyyy}" FilterControlAltText="Filter RegistrationDate column" HeaderText="Registration Date" UniqueName="RegistrationDate">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="StartDate" DataFormatString="{0:MM-dd-yyyy}" FilterControlAltText="Filter StartDate column" HeaderText="Start Date" UniqueName="StartDate">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="EndDate" DataFormatString="{0:MM-dd-yyyy}" FilterControlAltText="Filter EndDate column" HeaderText="End Date" UniqueName="EndDate">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="UrgentFlag" FilterControlAltText="Filter UrgentFlag column" HeaderText="Urgent" UniqueName="UrgentFlag">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ExtensionFlag" FilterControlAltText="Filter ExtensionFlag column" HeaderText="Extension" UniqueName="ExtensionFlag">
                                    </telerik:GridBoundColumn>


                                    <telerik:GridBoundColumn DataField="PickUp" FilterControlAltText="Filter PickUp column" HeaderText="Pick Up" UniqueName="PickUp">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ArrivalDate" DataFormatString="{0:MM-dd-yyyy}" FilterControlAltText="Filter ArrivalDate column" HeaderText="Arrival Date" UniqueName="ArrivalDate">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ArrivalAirLine" FilterControlAltText="Filter ArrivalAirLine column" HeaderText="Arrival AirLine" UniqueName="ArrivalAirLine">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DropOff" FilterControlAltText="Filter DropOff column" HeaderText="Drop Off" UniqueName="DropOff">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DepartingFrom" FilterControlAltText="Filter DepartureAirLine column" HeaderText="Departing From" UniqueName="DepartureAirLine">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="LeavingFrom" FilterControlAltText="Filter LivingFrom column" HeaderText="Leaving From" UniqueName="LeavingFrom">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PlacedUserId" FilterControlAltText="Filter PlacedUserId column" HeaderText="Placed By" UniqueName="PlacedUserId">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="StudentId" FilterControlAltText="Filter StudentId" UniqueName="StudentId" Visible="false">
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


            <telerik:RadPane ID="Radpane4" runat="server" Scrolling="Both"  Width="899px">

                <table width="100%">
                    <tr>
                        <td style="width: 80%">
                            <telerik:RadTabStrip runat="server" Width="100%" SelectedIndex="0" MultiPageID="MultiPage_Registration" ID="Tab_Host" OnTabClick="Tab_Host_TabClick">
                                <Tabs>
                                    <telerik:RadTab runat="server" TabIndex="0" Text="Placement Request Information" PageViewID="PageView_Basic" Selected="True">
                                    </telerik:RadTab>
                                    <telerik:RadTab runat="server" TabIndex="1" Text="Placement Request History" PageViewID="PageView_History">
                                    </telerik:RadTab>


                                    <telerik:RadTab runat="server" TabIndex="2" Text="Dormitory Placement History" PageViewID="PageView_Placement">
                                    </telerik:RadTab>




                                </Tabs>
                            </telerik:RadTabStrip>
                        </td>
                        <td style="width: 20%"></td>

                    </tr>

                </table>

                <telerik:RadMultiPage runat="server" ID="MultiPage_Registration" SelectedIndex="0" Width="100%">
                    <telerik:RadPageView runat="server" ID="PageView_Basic" Visible="true">

                        <br />
                        <telerik:RadLabel ID="Label53" runat="server" BackColor="#121212" BorderColor="#3366CC" Font-Bold="False" ForeColor="#E0E0E0" Text="Airport Information:     "></telerik:RadLabel>
                        <table width="96%" border="0" cellpadding="1" cellspacing="1" frame="border">
                            <tr>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="lbl_9" runat="server" Text="Student:">
                                    </telerik:RadLabel>

                                </td>
                                <td class="auto-styleTxt" colspan="11">

                                    <telerik:RadDropDownList ID="ddlStudent" runat="server" Width="80%" Enabled="false">
                                    </telerik:RadDropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="lbl_1" runat="server" Text="Airport Pick Up:">
                                    </telerik:RadLabel>
                                </td>
                                <td class="auto-style5">

                                    <telerik:RadDropDownList ID="ddl_pickup" runat="server">
                                        <Items>
                                            <telerik:DropDownListItem Selected="true" Text="Please Select" Value="0" />
                                            <telerik:DropDownListItem Text="No" Value="1" />
                                            <telerik:DropDownListItem Text="Yes" Value="2" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                                <td class="auto-style3">
                                    <telerik:RadLabel ID="lbl_2" runat="server" Text="Arrival Date:">
                                    </telerik:RadLabel>
                                </td>
                                <td class="auto-style7">
                                    <telerik:RadDatePicker ID="DatePickArrivalDate" runat="server" Culture="en-US" Height="25px" MinDate="1900-01-01" Width="160px">
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
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="lbl_6" runat="server" Text="Arrival Time">
                                    </telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadTimePicker ID="TimePickerArrivalTime" runat="server">
                                    </telerik:RadTimePicker>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadLabel ID="lbl_10" runat="server" Text="Arrival AirLine">
                                    </telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadTextBox ID="txtArrivalAirLine" runat="server" Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadLabel ID="lbl_3" runat="server" Text="Arrival Flight No.">
                                    </telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadTextBox ID="txtArrivalFlilghtNo" runat="server" Width="80%">
                                    </telerik:RadTextBox>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadLabel ID="lbl_8" runat="server" Text="Departing From">
                                    </telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadTextBox ID="txtDepartingFrom" runat="server" Width="80%">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="RadLabel1" runat="server" Text="Airport Drop Off:"></telerik:RadLabel>
                                </td>
                                <td class="auto-style5">
                                    <telerik:RadDropDownList ID="ddlDropoff" runat="server" Width="120px" Font-Size="Small">
                                        <Items>
                                            <telerik:DropDownListItem Text="Please Select" Value="0" Selected="true" />
                                            <telerik:DropDownListItem Text="No" Value="1" />
                                            <telerik:DropDownListItem Text="Yes" Value="2" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                                <td class="auto-style3">
                                    <telerik:RadLabel ID="RadLabel2" runat="server" Text="Departure Date:"></telerik:RadLabel>
                                </td>
                                <td class="auto-style7">
                                    <telerik:RadDatePicker ID="DatePicker_DepartureDate" runat="server" Culture="en-US" Height="25px" MinDate="1900-01-01" Width="150px">
                                        <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"></Calendar>
                                        <DateInput DateFormat="MM-dd-yyyy" DisplayDateFormat="MM-dd-yyyy" Height="25px" LabelWidth="40%">
                                            <EmptyMessageStyle Resize="None" />
                                            <ReadOnlyStyle Resize="None" />
                                            <FocusedStyle Resize="None" />
                                            <DisabledStyle Resize="None" />
                                            <InvalidStyle Resize="None" />
                                            <HoveredStyle Resize="None" />
                                            <EnabledStyle Resize="None" />
                                        </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                </td>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="RadLabel7" runat="server" Text="Departure Time"></telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadTimePicker ID="TimePickerDepartureTime" runat="server" Width="150px"></telerik:RadTimePicker>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadLabel ID="RadLabel5" runat="server" Text="Departure AirLine">
                                    </telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadTextBox ID="txtDepartureAirline" runat="server" Width="80%">
                                    </telerik:RadTextBox>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadLabel ID="RadLabel4" runat="server" Text="Departure Flight No.">
                                    </telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadTextBox ID="txtDepartureFlightNo" runat="server" Width="80%">
                                    </telerik:RadTextBox>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadLabel ID="RadLabel6" runat="server" Text="Leaving From">
                                    </telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadTextBox ID="txtLeavingFrom" runat="server" Width="80%">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>

                        </table>
                        <br />
                        <telerik:RadLabel ID="RadLabel20" runat="server" BackColor="#121212" BorderColor="#3366CC" Font-Bold="False" ForeColor="#E0E0E0" Text="General Information:     "></telerik:RadLabel>
                        <table width="96%" border="0" cellpadding="1" cellspacing="1" frame="border">
                            <tr>
                                <td class="auto-style11">
                                    <telerik:RadLabel ID="RadLabel21" runat="server" Text="Start Date:"></telerik:RadLabel>
                                    <telerik:RadLabel ID="lbl01" runat="server" Text="*" ForeColor="Red"></telerik:RadLabel>
                                </td>
                                <td class="auto-style12">
                                    <telerik:RadDatePicker ID="DatePickerStartDate" runat="server" Culture="en-US" Height="25px" MinDate="1900-01-01" Width="160px">
                                        <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"></Calendar>
                                        <DateInput DateFormat="MM-dd-yyyy" DisplayDateFormat="MM-dd-yyyy" Height="25px" LabelWidth="40%">
                                            <EmptyMessageStyle Resize="None" />
                                            <ReadOnlyStyle Resize="None" />
                                            <FocusedStyle Resize="None" />
                                            <DisabledStyle Resize="None" />
                                            <InvalidStyle Resize="None" />
                                            <HoveredStyle Resize="None" />
                                            <EnabledStyle Resize="None" />
                                        </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                </td>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="RadLabel22" runat="server" Text="End Date:"></telerik:RadLabel>
                                    <telerik:RadLabel ID="RadLabel17" runat="server" Text="*" ForeColor="Red"></telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadDatePicker ID="DatePickerEndDate" runat="server" Culture="en-US" Height="25px" MinDate="1900-01-01" Width="160px" AutoPostBack="true" OnSelectedDateChanged="DatePickerEndDate_SelectedDateChanged">
                                        <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"></Calendar>
                                        <DateInput DateFormat="MM-dd-yyyy" DisplayDateFormat="MM-dd-yyyy" Height="25px" LabelWidth="40%">
                                            <EmptyMessageStyle Resize="None" />
                                            <ReadOnlyStyle Resize="None" />
                                            <FocusedStyle Resize="None" />
                                            <DisabledStyle Resize="None" />
                                            <InvalidStyle Resize="None" />
                                            <HoveredStyle Resize="None" />
                                            <EnabledStyle Resize="None" />
                                        </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                </td>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="RadLabel24" runat="server" Text="Dormitory Weeks:">
                                    </telerik:RadLabel>
                                </td>
                                <td class="auto-styleLbl">
                                    <telerik:RadTextBox ID="TxtHomestayWeeks" runat="server" Enabled="False" ReadOnly="True" Value="0" Width="160px">
                                    </telerik:RadTextBox>
                                </td>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="RadLabel25" runat="server" Text="Extra Days:">
                                    </telerik:RadLabel>
                                </td>
                                <td class="auto-styleLbl">
                                    <telerik:RadTextBox ID="TxtExtraDays" runat="server" Enabled="False" Value="0" Width="160px">
                                    </telerik:RadTextBox>
                                </td>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="RadLabel23" runat="server" Text="Extension Flag:">
                                    </telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadDropDownList runat="server" ID="ddlExtention">
                                        <Items>
                                            <telerik:DropDownListItem Text="Please Select" Value="0" Selected="true" />
                                            <telerik:DropDownListItem Text="No" Value="1" />
                                            <telerik:DropDownListItem Text="Yes" Value="2" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadLabel ID="RadLabel26" runat="server" Text="Urgent Request:">
                                    </telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadDropDownList ID="ddlUrgent" runat="server">
                                        <Items>
                                            <telerik:DropDownListItem Selected="true" Text="Please Select" Value="0" />
                                            <telerik:DropDownListItem Text="No" Value="1" />
                                            <telerik:DropDownListItem Text="Yes" Value="2" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style13">
                                    <telerik:RadLabel ID="RadLabel30" runat="server" Text="Special Request:"></telerik:RadLabel>
                                </td>
                                <td class="auto-style14" colspan="11">
                                    <telerik:RadTextBox ID="txtComments" runat="server" Height="90px" TextMode="MultiLine" Width="600px">
                                    </telerik:RadTextBox>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style15">
                                    <telerik:RadLabel ID="RadLabel29" runat="server" Text="File Upload:"></telerik:RadLabel>

                                </td>
                                <td class="auto-style16" colspan="9">
                                    <UserControl:FileDownloadList ID="file_upload" runat="server" />
                                </td>
                                <td class="auto-style16" colspan="2"></td>
                            </tr>
                        </table>
                    </telerik:RadPageView>

                    <telerik:RadPageView runat="server" ID="PageView_History" Visible="false">
                        <table width="96%" border="0" cellpadding="1" cellspacing="1" frame="border">
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="GridHistory" runat="server" AutoGenerateColumns="False"
                                        AllowPaging="True" AllowSorting="True"
                                        OnItemDataBound="GridHistory_ItemDataBound" OnNeedDataSource="GridHistory_NeedDataSource" GroupPanelPosition="Top">

                                        <MasterTableView TableLayout="Fixed" DataKeyNames="DormitoryRegistrationId">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="DormitoryRegistrationId" FilterControlAltText="Filter DormitoryRegistrationId" UniqueName="DormitoryRegistrationId" HeaderText="Placemnt Request ID">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="SchoolName" FilterControlAltText="Filter column01 column" HeaderText="School" UniqueName="SchoolName">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Campus" FilterControlAltText="Filter Campus column" HeaderText="Campus" UniqueName="Campus">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FirstName" FilterControlAltText="Filter FirstName column" HeaderText="First Name" UniqueName="FirstName">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="LastName" FilterControlAltText="Filter LastName column" HeaderText="Last Name" UniqueName="LastName">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DormitoryStudentStatus" FilterControlAltText="Filter DormitoryStudentStatus column" HeaderText="Status" UniqueName="DormitoryStudentStatus">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Nationality" FilterControlAltText="Filter Nationality column" HeaderText="Nationality" UniqueName="Nationality">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="RegistrationDate" DataFormatString="{0:MM-dd-yyyy}" FilterControlAltText="Filter RegistrationDate column" HeaderText="Registration Date" UniqueName="RegistrationDate">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="StartDate" DataFormatString="{0:MM-dd-yyyy}" FilterControlAltText="Filter StartDate column" HeaderText="Start Date" UniqueName="StartDate">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="EndDate" DataFormatString="{0:MM-dd-yyyy}" FilterControlAltText="Filter EndDate column" HeaderText="End Date" UniqueName="EndDate">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="UrgentFlag" FilterControlAltText="Filter UrgentFlag column" HeaderText="Urgent" UniqueName="UrgentFlag">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ExtensionFlag" FilterControlAltText="Filter ExtensionFlag column" HeaderText="Extension" UniqueName="ExtensionFlag">
                                                </telerik:GridBoundColumn>


                                                <telerik:GridBoundColumn DataField="PickUp" FilterControlAltText="Filter PickUp column" HeaderText="Pick Up" UniqueName="PickUp">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ArrivalDate" DataFormatString="{0:MM-dd-yyyy}" FilterControlAltText="Filter ArrivalDate column" HeaderText="Arrival Date" UniqueName="ArrivalDate">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ArrivalAirLine" FilterControlAltText="Filter ArrivalAirLine column" HeaderText="Arrival AirLine" UniqueName="ArrivalAirLine">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DropOff" FilterControlAltText="Filter DropOff column" HeaderText="Drop Off" UniqueName="DropOff">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DepartureAirLine" FilterControlAltText="Filter DepartureAirLine column" HeaderText="DepartingFrom" UniqueName="DepartureAirLine">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="LeavingFrom" FilterControlAltText="Filter LivingFrom column" HeaderText="LeavingFrom" UniqueName="LeavingFrom">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PlacedUserId" FilterControlAltText="Filter PlacedUserId column" HeaderText="Placed By" UniqueName="PlacedUserId">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="StudentId" FilterControlAltText="Filter StudentId" UniqueName="StudentId" Visible="false">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>

                                        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                        <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                                            <Selecting AllowRowSelect="true" />
                                            <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="True" />
                                            <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                                                ClipCellContentOnResize="true"
                                                EnableRealTimeResize="True" AllowResizeToFit="true" />
                                        </ClientSettings>
                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                                    </telerik:RadGrid>

                                </td>
                            </tr>

                        </table>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="PageView_Placement" runat="server" Visible="false">
                        <table width="96%" border="0" cellpadding="1" cellspacing="1" frame="border">
                            <tr>
                                <td>

                                    <telerik:RadGrid ID="Grid_DormitoryPlacement" runat="server" AutoGenerateColumns="False"
                                        AllowPaging="True" AllowSorting="True" OnNeedDataSource="Grid_DormitoryPlacement_NeedDataSource"
                                        GroupPanelPosition="Top" OnItemDataBound="Grid_DormitoryPlacement_ItemDataBound">

                                        <MasterTableView TableLayout="Fixed" DataKeyNames="PlacementId">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="PlacementId" DataType="System.Int32" FilterControlAltText="Filter PlacementId column" HeaderText="Placement ID" SortExpression="HostPlacementId" UniqueName="HostPlacementId">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="PlacementDate" FilterControlAltText="Filter PlacementDate column"
                                                    HeaderText="PlacementDate" SortExpression="CreatedDate" UniqueName="PlacementDate" DataType="System.DateTime" DataFormatString="{0: MM-dd-yyyy}">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="PlacementStatus" FilterControlAltText="Filter PlacementStatus column" HeaderText="Placement Status" SortExpression="PlacementStatus" UniqueName="PlacementStatus" DataType="System.Int32">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DormitoryHostName" FilterControlAltText="Filter DormitoryHostName column" HeaderText="Dormitory Host Name" SortExpression="DormitoryHostName" UniqueName="DormitoryHostName">
                                                </telerik:GridBoundColumn>

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
                                                <telerik:GridBoundColumn DataField="HostId" FilterControlAltText="Filter HostId column" HeaderText="Host ID" SortExpression="HostId" UniqueName="HostId">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="HostRoomFloor" FilterControlAltText="Filter HostRoomFloor column" HeaderText="Floor Level" Visible="false" SortExpression="HostRoomFloor" UniqueName="HostRoomFloor">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="HostRoomName" FilterControlAltText="Filter HostRoomName column" HeaderText="Room Name" SortExpression="HostRoomName" UniqueName="HostRoomName">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="BedName" FilterControlAltText="Filter BedName column" HeaderText="Bed Name " SortExpression="BedName" UniqueName="BedName">
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

                                </td>
                            </tr>

                        </table>

                    </telerik:RadPageView>

                </telerik:RadMultiPage>

            </telerik:RadPane>

        </telerik:RadSplitter>

    </telerik:RadAjaxPanel>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">



            function NewDormitoryNewWindow(DormitoryId, studId, ScheduleChange) {
                var oWnd = window.radopen('NewDormitoryStudentPop?id=' + DormitoryId + '&StudentId=' + studId + '&ScheduleChange=' + ScheduleChange, 0, 0, 0, 0);
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


            function PlacementbySchoolNewWindow(DormitoryRegistrationId) {
                var oWnd = window.radopen('PlacementDormitoryPop?id=' + DormitoryRegistrationId, 0, 0, 0, 0);
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



            function CancelDormitoryNewWindow(DormitoryRegistrationId) {
                var oWnd = window.radopen('CancelDormitoryPop?id=' + DormitoryRegistrationId, 0, 0, 0, 0);
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

            function PaymentNewWindow(DormitoryRegistrationId) {
                var oWnd = window.radopen('PaymentDormitoryPop?id=' + DormitoryRegistrationId, 0, 0, 0, 0);
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
