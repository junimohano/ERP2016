<%@ Page Title="" Language="C#" MasterPageFile="~/School/Shared/Base.master" AutoEventWireup="true" CodeFile="HomestayRequestRegistration.aspx.cs" Inherits="School_StudentHousing_HomestayRequestRegistration" %>

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

                    <telerik:RadPane ID="RadPaneTop" runat="server" Height="27px" Scrolling="None">
                        <h4>Homestay Placement Request List</h4>
                    </telerik:RadPane>

                    <telerik:RadPane ID="Radpane1" runat="server" Height="40px" Width="100%">
                        <telerik:RadToolBar ID="RadToolBar3" runat="server" OnButtonClick="RadToolBar3_OnButtonClick">
                            <Items>
                                <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_new.png" Text="New Placement" />
                                <telerik:RadToolBarButton IsSeparator="true" />
                                <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_manage.png" Text="Request" />
                                <telerik:RadToolBarButton IsSeparator="true" />
                                <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_choose.png" Text=" Placement By School" Enabled="false"></telerik:RadToolBarButton>
                                <telerik:RadToolBarButton IsSeparator="true"></telerik:RadToolBarButton>
                                <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_pdf.png" Text=" Placement By Agency" Enabled="false"></telerik:RadToolBarButton>
                                <telerik:RadToolBarButton IsSeparator="true"></telerik:RadToolBarButton>
                                <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_calendar.png" Text=" Schedule Change" Visible="false" Enabled="false"></telerik:RadToolBarButton>
                                <telerik:RadToolBarButton IsSeparator="true" Visible="false"></telerik:RadToolBarButton>
                                <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_delete.png" Text=" Cancel" Enabled="false" Visible="false"></telerik:RadToolBarButton>
                                <telerik:RadToolBarButton IsSeparator="true" Visible="false"></telerik:RadToolBarButton>
                                <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_reg.png" Text="Invoice/Pamyments" Enabled="false"></telerik:RadToolBarButton>
                            </Items>
                        </telerik:RadToolBar>
                    </telerik:RadPane>
                    <telerik:RadPane ID="RadPane2" runat="server" Scrolling="None">

                        <telerik:RadGrid ID="Grid_StudentList" runat="server" AutoGenerateColumns="False"
                            AllowPaging="True" Height="100%" AllowSorting="True" AllowFilteringByColumn="true"
                            OnSelectedIndexChanged="Grid_StudentList_SelectedIndexChanged" OnItemDataBound="Grid_StudentList_ItemDataBound" OnNeedDataSource="Grid_StudentList_NeedDataSource"
                            FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                            <MasterTableView TableLayout="Fixed" DataKeyNames="HomestayStudentId">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="HomestayStudentId" FilterControlAltText="Filter HomestayStudentId" UniqueName="HomestayStudentId" HeaderText="Placemnt Request ID">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="InvoiceNumber" FilterControlAltText="Filter invoicecolumn column" HeaderText="Invoice#" UniqueName="invoicecolumn">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="SchoolName" FilterControlAltText="Filter column01 column" HeaderText="Site" UniqueName="SchoolName">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Campus" FilterControlAltText="Filter Campus column" HeaderText="Site Location" UniqueName="Campus">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FirstName" FilterControlAltText="Filter FirstName column" HeaderText="First Name" UniqueName="FirstName">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="LastName" FilterControlAltText="Filter LastName column" HeaderText="Last Name" UniqueName="LastName">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="HomestayStudentStatus" FilterControlAltText="Filter HomestayStudentStatus column" HeaderText="Homestay Status" UniqueName="HomestayStudentStatus">
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
                                    <telerik:GridBoundColumn DataField="GuardianRequired" FilterControlAltText="Filter GuardianRequired column" HeaderText="Guardian">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="MealType" FilterControlAltText="Filter MealType column" HeaderText="Meal Type" UniqueName="MealType">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Internet" FilterControlAltText="Filetr Internet column" HeaderText="Internet"></telerik:GridBoundColumn>
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
                                    <telerik:GridBoundColumn DataField="LeavingFrom" FilterControlAltText="Filter LivingFrom column" HeaderText="LeavingFrom" UniqueName="LeavingFrom">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PlacedUserId" FilterControlAltText="Filter PlacedUserId column" HeaderText="Placed By" UniqueName="PlacedUserId">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="StudentId" FilterControlAltText="Filter StudentId" UniqueName="StudentId" Visible="false">
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

                <table width="100%">
                    <tr>
                        <td style="width: 80%">
                            <telerik:RadTabStrip runat="server" Width="100%" SelectedIndex="0" MultiPageID="MultiPage_Registration" ID="Tab_Host" OnTabClick="Tab_Host_TabClick">
                                <Tabs>
                                    <telerik:RadTab runat="server" TabIndex="0" Text="Placement Request Information" PageViewID="PageView_Basic" Selected="true">
                                    </telerik:RadTab>
                                    <telerik:RadTab runat="server" TabIndex="1" Text="Placement Request History" PageViewID="PageView_History">
                                    </telerik:RadTab>
                                    <telerik:RadTab runat="server" TabIndex="2" Text="Homestay Placement History by School" PageViewID="PageView_Placement">
                                    </telerik:RadTab>
                                    <telerik:RadTab runat="server" Text="Homestaty Placement History by Agency" PageViewID="PageView_PlacementAgency">
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
                        <table width="70%" border="0" cellpadding="1" cellspacing="1" frame="border">
                            <tr>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="lbl_9" runat="server" Text="Student:">
                                    </telerik:RadLabel>

                                </td>
                                <td class="auto-styleTxt" colspan="7">

                                    <telerik:RadDropDownList ID="ddlStudent" runat="server" Width="80%" Enabled="false">
                                    </telerik:RadDropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="lbl_1" runat="server" Text="Airport Pick Up:">
                                    </telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">

                                    <telerik:RadDropDownList ID="ddl_pickup" runat="server">
                                        <Items>
                                            <telerik:DropDownListItem Selected="true" Text="Please Select" Value="0" />
                                            <telerik:DropDownListItem Text="No" Value="1" />
                                            <telerik:DropDownListItem Text="Yes" Value="2" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                                <td class="auto-styleSpan">&nbsp;</td>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="lbl_2" runat="server" Text="Arrival Date:">
                                    </telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
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
                                <td class="auto-styleSpan">&nbsp;</td>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="lbl_6" runat="server" Text="Arrival Time">
                                    </telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadTimePicker ID="TimePickerArrivalTime" runat="server" OnSelectedDateChanged="TimePickerArrivalTime_SelectedDateChanged">
                                    </telerik:RadTimePicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="lbl_7" runat="server" Text="Arrival AirLine"></telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadTextBox ID="txtArrivalAirLine" runat="server" Width="150px"></telerik:RadTextBox></td>
                                <td class="auto-styleSpan"></td>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="lbl_3" runat="server" Text="Arrival Flight No."></telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadTextBox runat="server" ID="txtArrivalFlilghtNo" Width="80%"></telerik:RadTextBox></td>
                                <td class="auto-styleSpan"></td>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="lbl_8" runat="server" Text="Departing From"></telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadTextBox ID="txtDepartingFrom" runat="server" Width="80%"></telerik:RadTextBox></td>
                            </tr>
                            <tr>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="RadLabel1" runat="server" Text="Airport Drop Off:"></telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadDropDownList ID="ddlDropoff" runat="server" Width="120px" Font-Size="Small">
                                        <Items>
                                            <telerik:DropDownListItem Text="Please Select" Value="0" Selected="true" />
                                            <telerik:DropDownListItem Text="No" Value="1" />
                                            <telerik:DropDownListItem Text="Yes" Value="2" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                                <td class="auto-styleSpan">&#160;</td>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="RadLabel2" runat="server" Text="Departure Date:"></telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
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
                                <td class="auto-styleSpan">&nbsp;</td>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="RadLabel7" runat="server" Text="Departure Time"></telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadTimePicker ID="TimePickerDepartureTime" runat="server" Width="150px"></telerik:RadTimePicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="RadLabel5" runat="server" Text="Departure AirLine"></telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadTextBox ID="txtDepartureAirline" runat="server" Width="80%"></telerik:RadTextBox></td>
                                <td class="auto-styleSpan">&#160;</td>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="RadLabel4" runat="server" Text="Departure Flight No."></telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadTextBox runat="server" ID="txtDepartureFlightNo" Width="80%"></telerik:RadTextBox></td>
                                <td class="auto-styleSpan">&nbsp;</td>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="RadLabel6" runat="server" Text="Leaving From"></telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadTextBox ID="txtLeavingFrom" runat="server" Width="80%"></telerik:RadTextBox></td>
                            </tr>

                        </table>
                        <br />
                        <telerik:RadLabel ID="RadLabel3" runat="server" BackColor="#121212" BorderColor="#3366CC" Font-Bold="False" ForeColor="#E0E0E0" Text="Preference:     "></telerik:RadLabel>
                        <table width="70%" border="0" cellpadding="1" cellspacing="1" frame="border">
                            <tr>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="RadLabel8" runat="server" Text="Smoking:"></telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadDropDownList ID="ddlSmoking" runat="server">
                                        <Items>
                                            <telerik:DropDownListItem Text="Please Select" Value="0" Selected="true" />
                                            <telerik:DropDownListItem Text="No" Value="1" />
                                            <telerik:DropDownListItem Text="Yes" Value="2" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                                <td class="auto-styleSpan">&nbsp;</td>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="RadLabel9" runat="server" Text="Internet:"></telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadDropDownList runat="server" ID="ddlInternet">
                                        <Items>
                                            <telerik:DropDownListItem Text="Please Select" Value="0" Selected="true" />
                                            <telerik:DropDownListItem Text="No" Value="1" />
                                            <telerik:DropDownListItem Text="Yes" Value="2" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                                <td class="auto-styleSpan">&nbsp;</td>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="RadLabel10" runat="server" Text="Room Type:"></telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadDropDownList runat="server" ID="ddlRoomType">
                                        <Items>
                                            <telerik:DropDownListItem Text="Please Select" Value="0" Selected="true" />
                                            <telerik:DropDownListItem Text="Shared" Value="1" />
                                            <telerik:DropDownListItem Text="Private" Value="2" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="RadLabel11" runat="server" Text="Meal Type:"></telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadDropDownList runat="server" ID="ddlMealType">
                                        <Items>
                                            <telerik:DropDownListItem Text="Please Select" Value="0" Selected="true" />
                                            <telerik:DropDownListItem Text="Half" Value="1" />
                                            <telerik:DropDownListItem Text="Full" Value="2" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                                <td class="auto-styleSpan"></td>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="RadLabel12" runat="server" Text="Guardian Required:"></telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadDropDownList runat="server" ID="ddlGuardian">
                                        <Items>
                                            <telerik:DropDownListItem Text="Please Select" Value="0" Selected="true" />
                                            <telerik:DropDownListItem Text="No" Value="1" />
                                            <telerik:DropDownListItem Text="Yes" Value="2" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                                <td class="auto-styleSpan"></td>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="RadLabel13" runat="server" Text="Pet:"></telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadDropDownList runat="server" ID="ddlPet">
                                        <Items>
                                            <telerik:DropDownListItem Text="Please Select" Value="0" Selected="true" />
                                            <telerik:DropDownListItem Text="No" Value="1" />
                                            <telerik:DropDownListItem Text="Yes" Value="2" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="RadLabel14" runat="server" Text="Children:"></telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadDropDownList runat="server" ID="ddlChildren">
                                        <Items>
                                            <telerik:DropDownListItem Text="Please Select" Value="0" Selected="true" />
                                            <telerik:DropDownListItem Text="No" Value="1" />
                                            <telerik:DropDownListItem Text="Yes" Value="2" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                                <td class="auto-styleSpan">&#160;</td>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="RadLabel15" runat="server" Text="Medication:"></telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadTextBox ID="txtMedication" runat="server" Width="150px"></telerik:RadTextBox></td>
                                <td class="auto-styleSpan">&nbsp;</td>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="RadLabel16" runat="server" Text="Diet:"></telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadTextBox ID="txtDiet" runat="server" Width="150px"></telerik:RadTextBox></td>
                            </tr>

                        </table>
                        <br />
                        <telerik:RadLabel ID="RadLabel20" runat="server" BackColor="#121212" BorderColor="#3366CC" Font-Bold="False" ForeColor="#E0E0E0" Text="General Information:     "></telerik:RadLabel>
                        <table width="70%" border="0" cellpadding="1" cellspacing="1" frame="border">
                            <tr>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="RadLabel21" runat="server" Text="Start Date:"></telerik:RadLabel>
                                    <telerik:RadLabel ID="lbl01" runat="server" Text="*" ForeColor="Red"></telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
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
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="DatePickerStartDate" Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Info">Required</asp:RequiredFieldValidator></td>
                                <td class="auto-styleSpan">&nbsp;</td>
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
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="DatePickerEndDate" Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Info">Required</asp:RequiredFieldValidator></td>
                                <td class="auto-styleSpan">&nbsp;</td>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="RadLabel23" runat="server" Text="Extension Flag:"></telerik:RadLabel>
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
                            </tr>
                            <tr>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="RadLabel24" runat="server" Text="Homestay Weeks:"></telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadTextBox ID="TxtHomestayWeeks" runat="server" Width="160px" Value="0" ReadOnly="True" Enabled="False">
                                    </telerik:RadTextBox>

                                </td>
                                <td class="auto-styleSpan"></td>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="RadLabel25" runat="server" Text="Extra Days:"></telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadTextBox ID="TxtExtraDays" runat="server" Width="160px" Value="0" Enabled="False"></telerik:RadTextBox></td>
                                <td class="auto-styleSpan"></td>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="RadLabel26" runat="server" Text="Urgent Request:"></telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadDropDownList runat="server" ID="ddlUrgent">
                                        <Items>
                                            <telerik:DropDownListItem Text="Please Select" Value="0" Selected="true" />
                                            <telerik:DropDownListItem Text="No" Value="1" />
                                            <telerik:DropDownListItem Text="Yes" Value="2" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="RadLabel27" runat="server" Text="Allergy"></telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadDropDownList ID="ddlAllergy" runat="server" Width="120px" Font-Size="Small">
                                        <Items>
                                            <telerik:DropDownListItem Text="Please Select" Value="0" Selected="true" />
                                            <telerik:DropDownListItem Text="No" Value="1" />
                                            <telerik:DropDownListItem Text="Yes" Value="2" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </td>
                                <td class="auto-styleSpan">&#160;</td>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="RadLabel28" runat="server" Text="Allergy Type:"></telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadTextBox ID="txtAllergyType" runat="server" Width="80%"></telerik:RadTextBox></td>
                                <td class="auto-styleSpan">&nbsp;</td>
                                <td class="auto-styleLbl">&#160;</td>
                                <td class="auto-styleTxt">&#160;</td>
                            </tr>
                            <tr>
                                <td class="auto-style1">
                                    <telerik:RadLabel ID="RadLabel30" runat="server" Text="Comments:"></telerik:RadLabel>
                                </td>
                                <td class="auto-style1" colspan="7">
                                    <telerik:RadTextBox ID="txtComments" runat="server" Height="109px" TextMode="MultiLine" Width="600px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="RadLabel29" runat="server" Text="File Upload:"></telerik:RadLabel>

                                </td>
                                <td class="auto-styleTxt" colspan="7">
                                    <UserControl:FileDownloadList ID="file_upload" runat="server" />
                                </td>
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

                                        <MasterTableView TableLayout="Fixed" DataKeyNames="HomestayStudentId">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="HomestayStudentId" FilterControlAltText="Filter HomestayStudentId" UniqueName="HomestayStudentId" HeaderText="Placement Request ID">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="SchoolName" FilterControlAltText="Filter column01 column" HeaderText="School" UniqueName="SchoolName">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Campus" FilterControlAltText="Filter Campus column" HeaderText="Campus" UniqueName="Campus">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FirstName" FilterControlAltText="Filter FirstName column" HeaderText="First Name" UniqueName="FirstName">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="LastName" FilterControlAltText="Filter LastName column" HeaderText="Last Name" UniqueName="LastName">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="HomestayStudentStatus" FilterControlAltText="Filter HomestayStudentStatus column" HeaderText="Homestay Status" UniqueName="HomestayStudentStatus">
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
                                                <telerik:GridBoundColumn DataField="GuardianRequired" FilterControlAltText="Filter GuardianRequired column" HeaderText="Guardian">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="MealType" FilterControlAltText="Filter MealType column" HeaderText="Meal Type" UniqueName="MealType">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Internet" FilterControlAltText="Filetr Internet column" HeaderText="Internet"></telerik:GridBoundColumn>
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
                                                <telerik:GridBoundColumn DataField="AgencyId" FilterControlAltText="Filter AgencyName column" HeaderText="Agency ID" UniqueName="AgencyId">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="AgencyName" FilterControlAltText="Filter AgencyName column" HeaderText="Agency Name" UniqueName="AgencyName">
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

                                </td>
                            </tr>

                        </table>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="PageView_Placement" runat="server" Visible="false">
                        <telerik:RadGrid ID="Grid_HomestayPlacement" runat="server" AutoGenerateColumns="False"
                            AllowPaging="True" AllowSorting="True" OnNeedDataSource="Grid_HomestayPlacement_NeedDataSource"
                            GroupPanelPosition="Top" OnItemDataBound="Grid_HomestayPlacement_ItemDataBound">

                            <MasterTableView TableLayout="Fixed" DataKeyNames="PlacementId">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="PlacementId" DataType="System.Int32" FilterControlAltText="Filter PlacementId column" HeaderText="Placement ID" SortExpression="PlacementId" UniqueName="PlacementId">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="PlacementDate" FilterControlAltText="Filter PlacementDate column"
                                        HeaderText="PlacementDate" SortExpression="CreatedDate" UniqueName="PlacementDate" DataType="System.DateTime" DataFormatString="{0: MM-dd-yyyy}">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="PlacementStatus" FilterControlAltText="Filter PlacementStatus column" HeaderText="Placement Status" SortExpression="PlacementStatus" UniqueName="PlacementStatus" DataType="System.Int32">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="PlacementType" FilterControlAltText="Filter PlacementType column" HeaderText="Placement Type" SortExpression="PlacementType" UniqueName="PlacementType" DataType="System.Int32">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="FatherFirstName" FilterControlAltText="Filter FatherFirstName column" HeaderText="Father's First Name" SortExpression="FatherFirstName" UniqueName="FatherFirstName">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FatherLastName" FilterControlAltText="Filter FatherLastName column" HeaderText="Father's Last Name" SortExpression="FatherLastName" UniqueName="FatherLastName">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="MotherFirstName" FilterControlAltText="Filter MotherFirstName column" HeaderText="Mother's First Name" SortExpression="MotherFirstName" UniqueName="MotherFirstName">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="MotherLastName" FilterControlAltText="Filter MotherLastName column" HeaderText="Mother's Last Name" SortExpression="MotherLastName" UniqueName="MotherLastName">
                                    </telerik:GridBoundColumn>

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


                                    <telerik:GridBoundColumn DataField="HostId" FilterControlAltText="Filter HostId column" HeaderText="Host ID" SortExpression="HostId" UniqueName="HostId">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="HostRoomFloor" FilterControlAltText="Filter HostRoomFloor column" HeaderText="Floor Level" SortExpression="HostRoomFloor" UniqueName="HostRoomFloor">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="HostRoomName" FilterControlAltText="Filter HostRoomName column" HeaderText="Room Name" SortExpression="HostRoomName" UniqueName="HostRoomName">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="HostRoomType" FilterControlAltText="Filter HostRoomType column" HeaderText="Room Type" SortExpression="HostRoomType" UniqueName="HostRoomType">
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
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="PageView_PlacementAgency" runat="server" Visible="false">
                        <table width="100%" style="height: 400px">
                            <tr>
                                <td style="width: 100%; height: 400px">


                                    <telerik:RadGrid ID="Grid_PlacementAgency" runat="server" AutoGenerateColumns="False"
                                        AllowPaging="True" Height="100%" AllowSorting="True" OnNeedDataSource="Grid_PlacementAgency_NeedDataSource"
                                        GroupPanelPosition="Top" OnItemDataBound="Grid_PlacementAgency_ItemDataBound" OnItemCommand="Grid_PlacementAgency_ItemCommand">

                                        <MasterTableView TableLayout="Auto" DataKeyNames="PlacementId" ItemStyle-Width="80px">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="PlacementId" DataType="System.Int32" FilterControlAltText="Filter PlacementId column" HeaderText="Placement ID" SortExpression="PlacementId" UniqueName="PlacementId">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="PlacementDate" FilterControlAltText="Filter PlacementDate column"
                                                    HeaderText="PlacementDate" SortExpression="CreatedDate" UniqueName="PlacementDate" DataType="System.DateTime" DataFormatString="{0: MM-dd-yyyy}">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="PlacementStatus" FilterControlAltText="Filter PlacementStatus column" HeaderText="Placement Status" SortExpression="PlacementStatus" UniqueName="PlacementStatus" DataType="System.Int32">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="PlacementType" FilterControlAltText="Filter PlacementType column" HeaderText="Placement Type" SortExpression="PlacementType" UniqueName="PlacementType" DataType="System.Int32">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn
                                                    HeaderText="Agency Id" DataField="UserId" SortExpression="UserId" UniqueName="UserId"
                                                    FilterControlAltText="Filter column">
                                                </telerik:GridBoundColumn>



                                                <telerik:GridBoundColumn
                                                    HeaderText="First Name" DataField="FirstName" SortExpression="FirstName" UniqueName="FirstName"
                                                    FilterControlAltText="Filter column">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Last Name" DataField="LastName" SortExpression="LastName" UniqueName="LastName"
                                                    FilterControlAltText="Filter column">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn
                                                    HeaderText="Phone" DataField="Phone" SortExpression="Phone" UniqueName="Phone"
                                                    FilterControlAltText="Filter column">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn
                                                    HeaderText="Email" DataField="Email" SortExpression="Email" UniqueName="Email" HeaderStyle-Width="190px" ItemStyle-Width="190px"
                                                    FilterControlAltText="Filter column">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn
                                                    HeaderText="Position" DataField="PositionName" SortExpression="PositionName" UniqueName="PositionName"
                                                    FilterControlAltText="Filter column">
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
                            <tr>
                                <td></td>
                            </tr>
                        </table>
                        <UserControl:FileDownloadList ID="Download" runat="server" Visible="false" />

                    </telerik:RadPageView>

                </telerik:RadMultiPage>

            </telerik:RadPane>

        </telerik:RadSplitter>

    </telerik:RadAjaxPanel>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function NewHomestayNewWindow(HomestayId, studId, ScheduleChange) {
                var oWnd = window.radopen('NewHomestayStudentPop?id=' + HomestayId + '&StudentId=' + studId + '&ScheduleChange=' + ScheduleChange, 0, 0, 0, 0);
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

            function PlacementbySchoolNewWindow(HomestayStudentId) {
                var oWnd = window.radopen('PlacementbySchoolPop?id=' + HomestayStudentId, 0, 0, 0, 0);
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

            function PlacementbyAgencyNewWindow(HomesatyStudentId) {
                var oWnd = window.radopen('PlacementbyAgencyPop?id=' + HomesatyStudentId, 0, 0, 0, 0);
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


            function CancelHomestayNewWindow(HomestayStudentId) {
                var oWnd = window.radopen('CancelHomestayPop?id=' + HomestayStudentId, 0, 0, 0, 0);
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

            function PaymentNewWindow(HomestayStudentId) {
                var oWnd = window.radopen('PaymentHomestayPop?id=' + HomestayStudentId, 0, 0, 0, 0);
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

