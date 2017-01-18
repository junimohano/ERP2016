<%@ Page Title="" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="NewDormitoryStudentPop.aspx.cs" Inherits="School_StudentHousing_NewDormitoryStudentPop" %>

<%@ Register TagPrefix="UserControl" TagName="InvoiceItemGrid" Src="~/App_Data/InvoiceItemGrid.ascx" %>
<%@ Register TagPrefix="UserControl" TagName="FileDownloadList" Src="~/App_Data/FileDownloadList.ascx" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">
        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="RadpaneBasic" runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="UpdateToolBar" runat="server" OnButtonClick="UpdateToolBar_ButtonClick" OnClientButtonClicking="ToolbarButtonClick">
                    <Items>
                        <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_save.png" Text="Save Dormitory Placement Request" ToolTip="Save" ValidationGroup="SaveValidationGroup" />
                        <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_back.png" Text="Close Window" ToolTip="Close Window"></telerik:RadToolBarButton>
                        <telerik:RadToolBarButton IsSeparator="true" />
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane5" runat="server" Scrolling="None">
                <telerik:RadSplitter runat="server" ID="RadSplitter2" Orientation="Vertical">
                    <telerik:RadPane ID="Radpane1" runat="server" Width="68%">
                        <br />
                        <telerik:RadLabel ID="Label53" runat="server" BackColor="#121212" BorderColor="#3366CC" Font-Bold="False" ForeColor="#E0E0E0" Text="Airport Information:     "></telerik:RadLabel>
                        <table width="96%" border="0" cellpadding="1" cellspacing="1" frame="border">
                            <tr>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="lbl_9" runat="server" Text="Student:">
                                    </telerik:RadLabel>

                                </td>
                                <td class="auto-styleTxt" colspan="7">

                                    <telerik:RadComboBox ID="ddlStudent" runat="server" Width="80%" AppendDataBoundItems="true" EmptyMessage="Please choose Student">
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator runat="server" ID="ddlStudentValidation" ControlToValidate="ddlStudent" Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="info">Choose a student</asp:RequiredFieldValidator>
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
                                    <telerik:RadTimePicker ID="TimePickerArrivalTime" runat="server">
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
                        <telerik:RadLabel ID="RadLabel20" runat="server" BackColor="#121212" BorderColor="#3366CC" Font-Bold="False" ForeColor="#E0E0E0" Text="General Information:     "></telerik:RadLabel>
                        <table width="96%" border="0" cellpadding="1" cellspacing="1" frame="border" style="margin-bottom: 0px">
                            <tr>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="RadLabel21" runat="server" Text="Start Date:"></telerik:RadLabel>
                                    <telerik:RadLabel ID="lbl01" runat="server" Text="*" ForeColor="Red"></telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadDatePicker ID="DatePickerStartDate" runat="server" Culture="en-US" Height="25px" MinDate="1900-01-01" Width="160px">
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
                                        </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="DatePickerStartDate" Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SaveValidationGroup">Required</asp:RequiredFieldValidator></td>
                                <td class="auto-styleSpan">&nbsp;</td>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="RadLabel22" runat="server" Text="End Date:"></telerik:RadLabel>
                                    <telerik:RadLabel ID="RadLabel17" runat="server" Text="*" ForeColor="Red"></telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadDatePicker ID="DatePickerEndDate" runat="server" Culture="en-US" Height="25px" MinDate="1900-01-01" Width="160px" OnSelectedDateChanged="DatePickerEndDate_SelectedDateChanged" AutoPostBack="true">
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
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="DatePickerEndDate" Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="SaveValidationGroup">Required</asp:RequiredFieldValidator></td>
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
                                    <telerik:RadLabel ID="RadLabel24" runat="server" Text="Dormitory Weeks:"></telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadTextBox ID="TxtDormitoryWeeks" runat="server" Width="160px" Text="0" Enabled="false">
                                    </telerik:RadTextBox>

                                </td>
                                <td class="auto-styleSpan"></td>
                                <td class="auto-styleLbl">
                                    <telerik:RadLabel ID="RadLabel25" runat="server" Text="Extra Days:"></telerik:RadLabel>
                                </td>
                                <td class="auto-styleTxt">
                                    <telerik:RadTextBox ID="TxtExtraDays" runat="server" Width="160px" Text="0" Enabled="False"></telerik:RadTextBox></td>
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
                                <td class="auto-style1">
                                    <telerik:RadLabel ID="RadLabel30" runat="server" Text="Special Request:"></telerik:RadLabel>
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
                            <tr>
                                <td class="auto-style2">
                                    <telerik:RadLabel ID="lblScheduleChange" runat="server" Text="Comment of Schedule Change:">
                                    </telerik:RadLabel>
                                </td>
                                <td class="auto-style3" colspan="7">
                                    <telerik:RadTextBox ID="txtScheduleComment" runat="server" Height="81px" TextMode="MultiLine" Width="600px">
                                        <EmptyMessageStyle Resize="None" />
                                        <ReadOnlyStyle Resize="None" />
                                        <FocusedStyle Resize="None" />
                                        <DisabledStyle Resize="None" />
                                        <InvalidStyle Resize="None" />
                                        <HoveredStyle Resize="None" />
                                        <EnabledStyle Resize="None" />
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style2" colspan="8">

                                    <telerik:RadGrid ID="Grid_ChangeList" Visible="false" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" GroupPanelPosition="Top" CellSpacing="-1" GridLines="Both" OnItemDataBound="Grid_ChangeList_ItemDataBound" OnNeedDataSource="Grid_ChangeList_NeedDataSource">
                                        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                        <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                                            <Selecting AllowRowSelect="True" />
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                        </ClientSettings>
                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                                        <MasterTableView DataKeyNames="DormitoryCancelSchdeduleId">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="CreatedDate" DataFormatString="{0:MM-dd-yyyy}" FilterControlAltText="Filter column column" HeaderText="Request Date" UniqueName="column">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="OriginalStudentBasicId" FilterControlAltText="Filter column1 column" HeaderText="Original Request Id" UniqueName="column1">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="UpdatedDate" DataFormatString="{0:MM-dd-yyyy}" FilterControlAltText="Filter column1 column" HeaderText="Requested Date" UniqueName="UpdatedDate">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="Comment" FilterControlAltText="Filter column5 column" HeaderText="Comment" UniqueName="HostBedId">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="RequestStatus" FilterControlAltText="Filter column5 column" HeaderText="Schedule Change Status" UniqueName="RequestStatus">
                                                </telerik:GridBoundColumn>

                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>

                                </td>
                            </tr>
                        </table>
                    </telerik:RadPane>
                    <telerik:RadSplitBar ID="Radsplitbar1" runat="server" CollapseMode="Both" EnableResize="true" />
                    <telerik:RadPane ID="Radpane2" runat="server" Scrolling="None" Width="30%">
                        <telerik:RadSplitter runat="server" ID="RadSplitter3" Orientation="Horizontal">
                            <telerik:RadPane ID="Radpane3" runat="server" Height="27px" Scrolling="None">
                                <h4>Dormitory Invoice</h4>
                            </telerik:RadPane>
                            <telerik:RadPane ID="Radpane4" runat="server" Scrolling="None">
                                <UserControl:InvoiceItemGrid ID="InvoiceItemGrid1" runat="server" />


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
                if (button.get_text() == "Save Dormitory Placement Request") {
                    if (!confirm('Do you want to Save Dormitory Placement Request?'))
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
