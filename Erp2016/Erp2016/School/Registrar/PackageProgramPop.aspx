<%@ Page Title="Package" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="PackageProgramPop.aspx.cs" Inherits="School.Registrar.PackageProgramPop" %>


<%@ Register TagPrefix="UserControl" TagName="FileDownloadList" Src="~/App_Data/FileDownloadList.ascx" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <asp:HiddenField runat="server" ID="hfId" />
    <asp:HiddenField runat="server" ID="hfType" />

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">
        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="mainToolBar" runat="server" OnButtonClick="mainToolBar_ButtonClick" OnClientButtonClicking="ToolbarButtonClick">
                    <Items>

                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_save.png" Text="TempSave" ToolTip="TempSave" ValidationGroup="Info" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_mark.png" Text="Request" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                        <telerik:RadToolBarButton Text="Close" ToolTip="Close" Value="Close" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <telerik:RadPane ID="RadpaneBasic" runat="server">

                <div class="formStyle3">
                    <div style="float: left; width: 100%;">
                        <fieldset>
                            <legend>Edit</legend>
                            <div>
                                <label>Site</label><telerik:RadTextBox ID="RadTextBoxSite" CssClass="RadSizeMiddle" runat="server" ReadOnly="True"></telerik:RadTextBox>
                                <br style="clear: both;" />
                            </div>
                            <div>
                                <label><b style="color: red">*</b> Site Location</label>
                                <telerik:RadComboBox ID="RadComboBoxSiteLocation" CssClass="RadSizeMiddle" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxSiteLocation_OnSelectedIndexChanged" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" EmptyMessage="Choose a Site Location" />
                                <asp:Literal ID="itemsClientSide" runat="server" />
                                <br style="clear: both;" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadComboBoxSiteLocation" Display="Dynamic" ErrorMessage="Site Location Required" ForeColor="Red" ValidationGroup="Info" />
                            </div>
                            <div>
                                <label><b style="color: red">*</b> Package Name</label>
                                <telerik:RadTextBox ID="RadTextBoxPackageProgramName" CssClass="RadSizeLarge" TextMode="SingleLine" Text="" runat="server"></telerik:RadTextBox>
                                <br style="clear: both;" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadTextBoxPackageProgramName" Display="Dynamic" ErrorMessage="Package program name Required" ForeColor="Red" ValidationGroup="Info" />
                            </div>
                            <div>
                                <label>Faculty</label>
                                <telerik:RadComboBox ID="RadComboBoxFaculty" CssClass="RadSizeLarge" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxFaculty_OnSelectedIndexChanged" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                            </div>
                            <div>
                                <label>Program Group</label>
                                <telerik:RadComboBox ID="RadComboBoxProgramGroup" CssClass="RadSizeLarge" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxProgramGroup_OnSelectedIndexChanged" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                            </div>
                            <div>
                                <label><b style="color: red">*</b> Program</label>
                                <telerik:RadComboBox ID="RadComboBoxProgram" CssClass="RadSizeLarge" runat="server" AutoPostBack="true" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                <br style="clear: both;" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadComboBoxProgram" Display="Dynamic" ErrorMessage="Program Required" ForeColor="Red" ValidationGroup="Info" />
                            </div>
                            <div>
                                <label><b style="color: red">*</b> Start Date</label>
                                <telerik:RadDatePicker ID="RadDatePickerStartDate" CssClass="RadSizeMiddle" Culture="English (Canada)" runat="server" MinDate="01/01/2000" MaxDate="01/01/2100" AutoPostBack="False">
                                    <DateInput DateFormat="MM-dd-yyyy" EmptyMessage="Start Date" />
                                    <ClientEvents OnDateSelected="RadDatePickerStartDate_DateSelected" />
                                </telerik:RadDatePicker>
                                <br style="clear: both;" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadDatePickerStartDate" Display="Dynamic" ErrorMessage="Start Date Required" ForeColor="Red" ValidationGroup="Info" />
                            </div>
                            <div>
                                <label><b style="color: red">*</b> End Date</label>
                                <telerik:RadDatePicker ID="RadDatePickerEndDate" CssClass="RadSizeMiddle" Culture="English (Canada)" runat="server" MinDate="01/01/2000" MaxDate="01/01/2100">
                                    <DateInput DateFormat="MM-dd-yyyy" EmptyMessage="End Date" />
                                    <ClientEvents OnDateSelected="RadDatePickerEndDate_DateSelected" />
                                </telerik:RadDatePicker>
                                <br style="clear: both;" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadDatePickerEndDate" Display="Dynamic" ErrorMessage="End Date Required" ForeColor="Red" ValidationGroup="Info" />
                            </div>
                            <div>
                                <label>Description</label>
                                <telerik:RadTextBox ID="RadTextBoxDescription" CssClass="RadSizeMultiLine" TextMode="MultiLine" Text="" runat="server"></telerik:RadTextBox>
                            </div>
                            <%--<div>
                                        <label>IsActive</label>
                                        <telerik:RadButton ID="btnToggleActive" runat="server" Text="" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="False" />
                                </div>--%>
                        </fieldset>

                        <UserControl:FileDownloadList ID="FileDownloadList1" runat="server" />

                    </div>
                </div>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </telerik:RadAjaxPanel>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            // date change
            function RadDatePickerStartDate_DateSelected(sender, eventArgs) {
                try {
                    var datePickerStartDate = $telerik.findControl(document, "RadDatePickerStartDate");
                    var datePickerEndDate = $telerik.findControl(document, "RadDatePickerEndDate");

                    var startDate = datePickerStartDate.get_selectedDate();
                    var endDate = datePickerEndDate.get_selectedDate();

                    if (startDate.getTime() > endDate.getTime()) {
                        endDate.setTime(startDate.getTime());
                        datePickerEndDate.set_selectedDate(endDate);
                    }
                } catch (e) {
                }
            }
            // date change
            function RadDatePickerEndDate_DateSelected(sender, eventArgs) {
                try {
                    var datePickerStartDate = $telerik.findControl(document, "RadDatePickerStartDate");
                    var datePickerEndDate = $telerik.findControl(document, "RadDatePickerEndDate");

                    var startDate = datePickerStartDate.get_selectedDate();
                    var endDate = datePickerEndDate.get_selectedDate();

                    if (startDate.getTime() > endDate.getTime()) {
                        startDate.setTime(endDate.getTime());
                        datePickerStartDate.set_selectedDate(startDate);
                    }
                } catch (e) {
                }
            }
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
                if (button.get_text() === "TempSave") {
                    if (!confirm('Do you want to save it?'))
                        args.set_cancel(true);
                } else if (button.get_text() === "Request") {
                    if (!confirm('Do you want to request it?'))
                        args.set_cancel(true);
                }
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
