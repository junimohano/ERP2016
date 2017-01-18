<%@ Page Title="Inventory" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="InventoryPop.aspx.cs" Inherits="School.OfficeAdmin.InventoryPop" %>

<%@ Import Namespace="Erp2016.Lib" %>
<%@ Register TagPrefix="UserControl" TagName="FileDownloadList" Src="~/App_Data/FileDownloadList.ascx" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">
        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal" ResizeMode="EndPane">

            <telerik:RadPane ID="RadPane7" runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="RadToolBar1" runat="server" OnButtonClick="RadToolBar1_ButtonClick" OnClientButtonClicking="RadToolBar1_OnClientButtonClicking">
                    <Items>
                        <telerik:RadToolBarButton runat="server" ImageUrl="../../assets/img/bt_save.png" Text="Save" ValidationGroup="Info" />
                        <telerik:RadToolBarButton runat="server" Text="Print" />
                        <telerik:RadToolBarButton runat="server" Text="Close" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <telerik:RadPane ID="RadPane1" runat="server" Scrolling="Both">

                <div id="divPrint">

                    <div class="formStyle3">

                        <div style="float: left; width: 450px;">
                            <fieldset>
                                <legend>Code Information</legend>

                                <div>
                                    <label><b style="color: red">*</b> Category</label>
                                    <telerik:RadComboBox ID="RadComboBoxInventoryCategory" EmptyMessage="Choose a Site" CssClass="RadSizeMiddle" runat="server" AutoPostBack="true" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" OnSelectedIndexChanged="RadComboBoxCategory_OnSelectedIndexChanged" />
                                    <br style="clear: both;" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="RadComboBoxInventoryCategory" Display="Dynamic" ErrorMessage="Site Required" ForeColor="Red" ValidationGroup="Info" />
                                </div>
                                <div>
                                    <label><b style="color: red">*</b> Category Item</label>
                                    <telerik:RadComboBox ID="RadComboBoxInventoryCategoryItem" CssClass="RadSizeMiddle" runat="server" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True"></telerik:RadComboBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="RadComboBoxInventoryCategoryItem" Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Info">Required</asp:RequiredFieldValidator>
                                    <br style="clear: both;" />
                                </div>
                            </fieldset>
                        </div>

                        <div style="float: left; width: 450px;">
                            <fieldset>
                                <legend>Assigned User And Location</legend>

                                <div>
                                    <label><b style="color: red">*</b> Site</label>
                                    <telerik:RadComboBox ID="RadComboBoxSite" EmptyMessage="Choose a Site" CssClass="RadSizeMiddle" runat="server" AutoPostBack="true" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" OnSelectedIndexChanged="RadComboBoxSite_OnSelectedIndexChanged" />
                                    <br style="clear: both;" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="RadComboBoxSite" Display="Dynamic" ErrorMessage="Site Required" ForeColor="Red" ValidationGroup="Info" />
                                </div>
                                <div>
                                    <label><b style="color: red">*</b> Site Location</label>
                                    <telerik:RadComboBox ID="RadComboBoxSiteLocation" CssClass="RadSizeMiddle" runat="server" AutoPostBack="True" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" OnSelectedIndexChanged="RadComboBoxSiteLocation_OnSelectedIndexChanged"></telerik:RadComboBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="RadComboBoxSiteLocation" Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Info">Required</asp:RequiredFieldValidator>
                                    <br style="clear: both;" />
                                </div>
                                <div>
                                    <label><b style="color: red">*</b> Assigned User</label>
                                    <telerik:RadComboBox ID="RadComboBoxAssignedUser" CssClass="RadSizeMiddle" runat="server" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True"></telerik:RadComboBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="RadComboBoxAssignedUser" Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Info">Required</asp:RequiredFieldValidator>
                                    <br style="clear: both;" />
                                </div>
                                <div>
                                    <label>Department / Area</label>
                                    <telerik:RadTextBox ID="RadTextBoxDepartment" CssClass="RadSizeMiddle" runat="server" />
                                    <br style="clear: both;" />
                                </div>

                            </fieldset>
                        </div>

                        <div style="float: left; width: 450px;">
                            <fieldset>
                                <legend>Purchase Detail Information</legend>

                                <div>
                                    <label>Company</label>
                                    <telerik:RadTextBox ID="RadTextBoxCompany" CssClass="RadSizeMiddle" runat="server" />
                                    <br style="clear: both;" />
                                </div>
                                <div>
                                    <label>Price</label>
                                    <telerik:RadNumericTextBox ID="RadNumericTextBoxPrice" runat="server" CssClass="RadSizeMiddle" Type="Number" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                                    <br style="clear: both;" />
                                </div>
                                <div>
                                    <label>Date of Purchase</label>
                                    <telerik:RadDatePicker ID="RadDatePickerPurchased" CssClass="RadSizeMiddle" runat="server" Culture="English (Canada)">
                                        <DateInput DateFormat="MM-dd-yyyy" />
                                    </telerik:RadDatePicker>
                                    <br style="clear: both;" />
                                </div>
                                <div>
                                    <label>Date of Expire</label>
                                    <telerik:RadDatePicker ID="RadDatePickerExpire" CssClass="RadSizeMiddle" runat="server" Culture="English (Canada)">
                                        <DateInput DateFormat="MM-dd-yyyy" />
                                    </telerik:RadDatePicker>
                                    <br style="clear: both;" />
                                </div>
                            </fieldset>
                        </div>

                        <div style="float: left; width: 450px;">
                            <fieldset>
                                <legend>Item Details</legend>

                                <div>
                                    <label>Model No</label>
                                    <telerik:RadTextBox ID="RadTextBoxModelNo" CssClass="RadSizeMiddle" runat="server" />
                                    <br style="clear: both;" />
                                </div>
                                <div>
                                    <label>Serial No</label>
                                    <telerik:RadTextBox ID="RadTextBoxSerialNo" CssClass="RadSizeMiddle" runat="server" />
                                    <br style="clear: both;" />
                                </div>
                                <div>
                                    <label><b style="color: red">*</b> Condition</label>
                                    <telerik:RadComboBox ID="RadComboBoxCondition" EmptyMessage="Choose a Condition" CssClass="RadSizeMiddle" runat="server" AutoPostBack="False" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                    <br style="clear: both;" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="RadComboBoxCondition" Display="Dynamic" ErrorMessage="Site Required" ForeColor="Red" ValidationGroup="Info" />
                                </div>
                                <div>
                                    <label><b style="color: red">*</b> In Use</label>
                                    <telerik:RadComboBox ID="RadComboBoxInUse" EmptyMessage="Choose a In Use" CssClass="RadSizeMiddle" runat="server" AutoPostBack="False" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                    <br style="clear: both;" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="RadComboBoxInUse" Display="Dynamic" ErrorMessage="Site Required" ForeColor="Red" ValidationGroup="Info" />
                                </div>
                            </fieldset>
                        </div>
                        
                        <UserControl:FileDownloadList ID="FileDownloadList1" runat="server" />

                    </div>
                    
                </div>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </telerik:RadAjaxPanel>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            <%--function pageLoad() {
                var grid = $find("<%= RadGridExpenseDetail.ClientID %>");
                if (grid != null) {
                    var columns = grid.get_masterTableView().get_columns();
                    for (var i = 0; i < columns.length; i++) {
                        columns[i].resizeToFit();
                    }
                }
            }--%>

            function RadToolBar1_OnClientButtonClicking(sender, args) {
                var button = args.get_item();
                if (button.get_text() === "Save") {
                    if (!confirm('Do you want to save?'))
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

            function ShowPrint() {
                $("#divPrint").printArea();
            }

        </script>

    </telerik:RadCodeBlock>
</asp:Content>
