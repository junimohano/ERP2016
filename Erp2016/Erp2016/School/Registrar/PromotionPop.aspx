<%@ Page Title="Promotion" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="PromotionPop.aspx.cs" Inherits="School.Registrar.PromotionPop" %>


<%@ Register TagPrefix="UserControl" TagName="FileDownloadList" Src="~/App_Data/FileDownloadList.ascx" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">

        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="Radpane3" runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="RadToolBar2" runat="server" OnClientButtonClicking=" ToolbarButtonClick " OnButtonClick="StudentButtonClicked">
                    <Items>
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_save.png" Text="TempSave" ToolTip="TempSave" Enabled="true" ValidationGroup="Info" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                        <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_mark.png" Text="Request" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                        <telerik:RadToolBarButton Text="Close" ToolTip="Close" Value="Close" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane1" runat="server" Scrolling="Y">
                <div class="formStyle3">
                    <div style="float: left; width: 100%;">
                        <fieldset>
                            <legend>Location Info</legend>
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
                                <label><b style="color: red">*</b> Country</label>
                                <telerik:RadComboBox ID="RadComboBoxCountry" CssClass="RadSizeMiddle" runat="server" AppendDataBoundItems="true" EmptyMessage="Choose a Country"></telerik:RadComboBox>
                                <br style="clear: both;" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadComboBoxCountry" Display="Dynamic" ErrorMessage="Country Required" ForeColor="Red" ValidationGroup="Info" />
                            </div>
                        </fieldset>
                    </div>
                    <div style="float: left; width: 100%;">
                        <fieldset>
                            <legend>Promotion Information</legend>
                            <div>
                                <label><b style="color: red">*</b> Amount</label>
                                <telerik:RadNumericTextBox ID="RadNumericTextBoxAmount" CssClass="RadSizeMiddle" runat="server" />
                                <br style="clear: both;" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadNumericTextBoxAmount" Display="Dynamic" ErrorMessage="Amount Required" ForeColor="Red" ValidationGroup="Info" />
                            </div>
                            <div>
                                <label><b style="color: red">*</b> Start Date</label>
                                <telerik:RadDatePicker DateInput-DisplayDateFormat="MMM dd, yyyy" ID="RadDatePickerStartDate" runat="server" CssClass="RadSizeMiddle" DateInput-EmptyMessage="Exemption Date" MinDate="01/01/1900" MaxDate="01/01/3000"></telerik:RadDatePicker>
                                <br style="clear: both;" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadDatePickerStartDate" Display="Dynamic" ErrorMessage="Start Date Required" ForeColor="Red" ValidationGroup="Info" />
                            </div>
                            <div>
                                <label><b style="color: red">*</b> End Date</label>
                                <telerik:RadDatePicker DateInput-DisplayDateFormat="MMM dd, yyyy" ID="RadDatePickerEndDate" runat="server" CssClass="RadSizeMiddle" DateInput-EmptyMessage="Exemption Date" MinDate="01/01/1900" MaxDate="01/01/3000"></telerik:RadDatePicker>
                                <br style="clear: both;" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadDatePickerEndDate" Display="Dynamic" ErrorMessage="End Date Required" ForeColor="Red" ValidationGroup="Info" />
                            </div>
                        </fieldset>

                        <UserControl:FileDownloadList ID="FileDownloadList1" runat="server" />

                    </div>
                    <div style="float: left; width: 100%;">
                        <fieldset>
                            <legend>Comments</legend>
                            <telerik:RadTextBox ID="tbComment" CssClass="RadSizeMultiLine" TextMode="MultiLine" runat="server" />
                            <br style="clear: both;" />
                        </fieldset>
                    </div>
                <br style="clear: both;" />
                </div>
            </telerik:RadPane>

        </telerik:RadSplitter>

    </telerik:RadAjaxPanel>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
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
