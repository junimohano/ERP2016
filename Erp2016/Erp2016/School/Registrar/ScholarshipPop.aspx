<%@ Page Title="Scholarship" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="ScholarshipPop.aspx.cs" Inherits="School.Registrar.ScholarshipPop" %>



<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">

        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="Radpane3" runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="RadToolBar1" runat="server"
                    OnClientButtonClicking=" ToolbarButtonClick " OnButtonClick="StudentButtonClicked">
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

            <telerik:RadPane ID="Radpane1" runat="server">
                <div class="formStyle3">
                    <div style="float: left; width: 100%;">
                        <fieldset>
                            <legend>Scholarship Info</legend>
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
                        </fieldset>
                    </div>

                    <div style="float: left; width: 100%;">
                        <fieldset>
                            <legend>Agency</legend>
                            <div>
                                <label><b style="color: red">*</b> Agency Name</label>
                                <telerik:RadComboBox ID="ddlAgencyName" CssClass="RadSizeLarge" runat="server" AppendDataBoundItems="true" EmptyMessage="Choose Agency" AutoPostBack="true"></telerik:RadComboBox>
                                <br style="clear: both;" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlAgencyName" Display="Dynamic" ErrorMessage="Agency Name Required" ForeColor="Red" ValidationGroup="Info" />
                            </div>
                        </fieldset>

                        <fieldset>
                            <legend>Scholarship Type</legend>
                            <div>
                                <label>Amount</label>
                                <telerik:RadButton GroupName="radio" ID="rbAmount" runat="server" Text="" ToggleType="Radio" ButtonType="ToggleButton" Checked="True" AutoPostBack="False" />
                                <telerik:RadNumericTextBox runat="server" CssClass="RadSizeSmall" Type="Currency" ID="RadNumericTextBoxAmount"></telerik:RadNumericTextBox>
                                <br style="clear: both;" />
                            </div>

                            <br style="clear: both;" />

                            <div>
                                <label>Weeks / Semester</label>
                                <telerik:RadButton GroupName="radio" ID="rbWeeks" runat="server" Text="" ToggleType="Radio" ButtonType="ToggleButton" AutoPostBack="False" />
                                <telerik:RadTextBox ID="tbWeeks" CssClass="RadSizeSmall" runat="server" />
                                <br style="clear: both;" />
                            </div>

                            <br style="clear: both;" />

                            <div>
                                <label><b style="color: red">*</b> Expire Date</label><telerik:RadDatePicker DateInput-DisplayDateFormat="MMM dd, yyyy" ID="tbExpireDate" runat="server" CssClass="RadSizeMiddle" DateInput-EmptyMessage="Exemption Date" MinDate="01/01/1900" MaxDate="01/01/3000"></telerik:RadDatePicker>
                                <br style="clear: both;" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbExpireDate" Display="Dynamic" ErrorMessage="Expire Date Required" ForeColor="Red" ValidationGroup="Info" />
                            </div>
                            <div>
                                <label><b style="color: red">*</b> Minimum Registration</label>

                                <telerik:RadComboBox runat="server" ID="RadComboBoxMinimum" CssClass="RadSizeMiddle">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="2 Weeks" Value="2" />
                                        <telerik:RadComboBoxItem runat="server" Text="4 Weeks" Value="4" />
                                        <telerik:RadComboBoxItem runat="server" Text="6 Weeks" Value="6" />
                                        <telerik:RadComboBoxItem runat="server" Text="8 Weeks" Value="8" />
                                        <telerik:RadComboBoxItem runat="server" Text="10 Weeks" Value="10" />
                                        <telerik:RadComboBoxItem runat="server" Text="12 Weeks" Value="12" />
                                    </Items>
                                </telerik:RadComboBox>

                                <br style="clear: both;" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadComboBoxMinimum" Display="Dynamic" ErrorMessage="Minimum Registration Required" ForeColor="Red" ValidationGroup="Info" />
                            </div>
                        </fieldset>
                        <br style="clear: both;" />
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
