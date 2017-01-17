<%@ Page Title="Purchase Order" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="PurchaseOrderPop.aspx.cs" Inherits="School.OfficeAdmin.PurchaseOrderPop" %>

<%@ Import Namespace="Erp2016.Lib" %>

<%@ Register TagPrefix="usercontrol" TagName="approvalline" Src="~/App_Data/ApprovalLine.ascx" %>
<%@ Register TagPrefix="UserControl" TagName="FileDownloadList" Src="~/App_Data/FileDownloadList.ascx" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGridPurchaseOrderDetail">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridPurchaseOrderDetail" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadToolBar1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">
        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal" ResizeMode="EndPane">

            <telerik:RadPane ID="RadPane7" runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="RadToolBar1" runat="server" OnButtonClick="RadToolBar1_ButtonClick" OnClientButtonClicking="RadToolBar1_OnClientButtonClicking">
                    <Items>
                        <telerik:RadToolBarButton runat="server" ImageUrl="../../assets/img/bt_save.png" Text="TempSave" ValidationGroup="Info" />
                        <telerik:RadToolBarButton runat="server" ImageUrl="../../assets/img/bt_mark.png" Text="Request" ValidationGroup="Info" />
                        <telerik:RadToolBarButton runat="server" ImageUrl="../../assets/img/bt_excute.png" Text="Approve" />
                        <telerik:RadToolBarButton runat="server" Text="Accept" />
                        <telerik:RadToolBarButton runat="server" Text="Complete" />
                        <telerik:RadToolBarButton runat="server" ImageUrl="../../assets/img/bt_back.png" Text="Revise" />
                        <telerik:RadToolBarButton runat="server" ImageUrl="../../assets/img/bt_clear.png" Text="Reject" />
                        <telerik:RadToolBarButton runat="server" ImageUrl="../../assets/img/bt_delete.png" Text="Cancel" />
                        <telerik:RadToolBarButton runat="server" Text="Print" />
                        <telerik:RadToolBarButton runat="server" Text="Close" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <telerik:RadPane runat="server" Scrolling="Both">

                <div id="divPrint">

                    <telerik:RadGrid ID="RadGridInfo" runat="server" GroupPanelPosition="Top" />

                    <UserControl:approvalline ID="ApprovalLine1" runat="server" OnLoad="ApprovalLine1_OnLoad" />

                    <fieldset id="review">
                        <legend>Review</legend>
                        <table width="100%">
                            <tr>
                                <td>
                                    <label>Review User</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="RadTextBoxReviewUser" CssClass="RadSizeSmall" runat="server" ReadOnly="True" />
                                </td>
                                <td>
                                    <label>Review Date</label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="RadDatePickerReviewDate" CssClass="RadSizeSmall" runat="server" Culture="English (Canada)" Enabled="False">
                                        <DateInput DateFormat="MM-dd-yyyy" />
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label><b style="color: red">*</b> Review Type</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadComboBoxReviewType" CssClass="RadSizeSmall" runat="server" AutoPostBack="False" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                </td>
                                <td>
                                    <label>Review Memo</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="RadTextBoxReviewMemo" TextMode="MultiLine" CssClass="RadSizeMultiLine" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>

                    <fieldset>
                        <legend>Purchase Order Info</legend>

                        <table width="100%">

                            <tr>
                                <td>
                                    <label><b style="color: red">*</b> Type</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadComboBoxType" CssClass="RadSizeMiddle" runat="server" AutoPostBack="False" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="RadComboBoxType" Display="Dynamic" ErrorMessage="Type Required" ForeColor="Red" ValidationGroup="Info" />
                                </td>
                                <td>
                                    <label><b style="color: red">*</b> Priority</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadComboBoxPriority" CssClass="RadSizeMiddle" runat="server" AutoPostBack="False" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="RadComboBoxPriority" Display="Dynamic" ErrorMessage="Priority Required" ForeColor="Red" ValidationGroup="Info" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label><b style="color: red">*</b> Shipping Method</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadComboBoxShippingMethod" CssClass="RadSizeMiddle" runat="server" AutoPostBack="False" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="RadComboBoxShippingMethod" Display="Dynamic" ErrorMessage="Shipping Method Required" ForeColor="Red" ValidationGroup="Info" />
                                </td>
                                <td>
                                    <label><b style="color: red">*</b> Shipping Terms</label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadNumericTextBoxShippingTerms" CssClass="RadSizeMiddle" runat="server" EmptyMessage="" Type="Number" Enabled="true" AutoPostBack="False" MinValue="1" MaxValue="999999999">
                                        <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                        <ClientEvents OnKeyPress="NewKeyPress" />
                                    </telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="RadNumericTextBoxShippingTerms" Display="Dynamic" ErrorMessage="Shipping Terms Required" ForeColor="Red" ValidationGroup="Info" />
                                </td>
                                <td>
                                    <label><b style="color: red">*</b> Delivery Date</label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="RadDatePickerDeliveryDate" runat="server" Culture="English (Canada)" CssClass="RadSizeMiddle">
                                        <DateInput DateFormat="MM-dd-yyyy" />
                                    </telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="RadDatePickerDeliveryDate" Display="Dynamic" ErrorMessage="Delivery Date Required" ForeColor="Red" ValidationGroup="Info" />
                                </td>
                            </tr>
                        </table>

                    </fieldset>

                    <table width="100%">
                        <tr>
                            <td>
                                <fieldset>
                                    <legend>Vendor Information</legend>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <label><b style="color: red">*</b> Name</label>
                                            </td>
                                            <td colspan="3">
                                                <telerik:RadTextBox ID="RadTextBoxVendorName" Width="100%" runat="server" />
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadTextBoxVendorName" Display="Dynamic" ErrorMessage="Name Required" ForeColor="Red" ValidationGroup="Info" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label><b style="color: red">*</b> Address</label>
                                            </td>
                                            <td colspan="3">
                                                <telerik:RadTextBox ID="RadTextBoxVendorAddress" Width="100%" runat="server" />
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadTextBoxVendorAddress" Display="Dynamic" ErrorMessage="Address Required" ForeColor="Red" ValidationGroup="Info" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label><b style="color: red">*</b> City</label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="RadTextBoxVendorCity" Width="100%" runat="server" />
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadTextBoxVendorCity" Display="Dynamic" ErrorMessage="City Required" ForeColor="Red" ValidationGroup="Info" />
                                            </td>
                                            <td>
                                                <label><b style="color: red">*</b> Province</label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="RadTextBoxVendorProvince" Width="100%" runat="server" />
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadTextBoxVendorProvince" Display="Dynamic" ErrorMessage="Province Required" ForeColor="Red" ValidationGroup="Info" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label><b style="color: red">*</b> PostalCode</label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="RadTextBoxVendorPostalCode" Width="100%" runat="server" />
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadTextBoxVendorPostalCode" Display="Dynamic" ErrorMessage="PostalCode Required" ForeColor="Red" ValidationGroup="Info" />
                                            </td>
                                            <td>
                                                <label><b style="color: red">*</b> Phone</label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="RadTextBoxVendorPhone" Width="100%" runat="server" />
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadTextBoxVendorPhone" Display="Dynamic" ErrorMessage="Phone Required" ForeColor="Red" ValidationGroup="Info" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label><b style="color: red">*</b> Email</label>
                                            </td>
                                            <td colspan="3">
                                                <telerik:RadTextBox ID="RadTextBoxVendorEmail" Width="100%" runat="server" />
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadTextBoxVendorEmail" Display="Dynamic" ErrorMessage="Email Required" ForeColor="Red" ValidationGroup="Info" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                            <td>
                                <fieldset>
                                    <legend>Ship To</legend>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <label><b style="color: red">*</b> Name</label>
                                            </td>
                                            <td colspan="3">
                                                <telerik:RadTextBox ID="RadTextBoxShipToName" Width="100%" runat="server" />
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadTextBoxShipToName" Display="Dynamic" ErrorMessage="Name Required" ForeColor="Red" ValidationGroup="Info" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label><b style="color: red">*</b> Address</label>
                                            </td>
                                            <td colspan="3">
                                                <telerik:RadTextBox ID="RadTextBoxShipToAddress" Width="100%" runat="server" />
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadTextBoxShipToAddress" Display="Dynamic" ErrorMessage="Address Required" ForeColor="Red" ValidationGroup="Info" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label><b style="color: red">*</b> City</label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="RadTextBoxShipToCity" Width="100%" runat="server" />
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadTextBoxShipToCity" Display="Dynamic" ErrorMessage="City Required" ForeColor="Red" ValidationGroup="Info" />
                                            </td>
                                            <td>
                                                <label><b style="color: red">*</b> Province</label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="RadTextBoxShipToProvince" Width="100%" runat="server" />
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadTextBoxShipToProvince" Display="Dynamic" ErrorMessage="Province Required" ForeColor="Red" ValidationGroup="Info" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label><b style="color: red">*</b> PostalCode</label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="RadTextBoxShipToPostalCode" Width="100%" runat="server" />
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadTextBoxShipToPostalCode" Display="Dynamic" ErrorMessage="PostalCode Required" ForeColor="Red" ValidationGroup="Info" />
                                            </td>
                                            <td>
                                                <label><b style="color: red">*</b> Phone</label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="RadTextBoxShipToPhone" Width="100%" runat="server" />
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadTextBoxShipToPhone" Display="Dynamic" ErrorMessage="Phone Required" ForeColor="Red" ValidationGroup="Info" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label><b style="color: red">*</b> Email</label>
                                            </td>
                                            <td colspan="3">
                                                <telerik:RadTextBox ID="RadTextBoxShipToEmail" Width="100%" runat="server" />
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadTextBoxShipToEmail" Display="Dynamic" ErrorMessage="Email Required" ForeColor="Red" ValidationGroup="Info" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                    </table>

                    <fieldset>
                        <legend>Description</legend>
                        <div style="float: left; width: 100%;">
                            <div>
                                <telerik:RadTextBox ID="RadTextBoxDescription" TextMode="MultiLine" CssClass="RadSizeMultiLine" runat="server" />
                            </div>
                        </div>
                        <br style="clear: both;" />
                    </fieldset>

                    <UserControl:FileDownloadList ID="FileDownloadList1" runat="server" />

                    <h4>Purchase Order Detail List</h4>

                    <telerik:RadGrid ID="RadGridPurchaseOrderDetail" runat="server" AllowAutomaticDeletes="true" AllowAutomaticInserts="true" AllowAutomaticUpdates="true" AllowPaging="False" AutoGenerateColumns="false" DataSourceID="LinqDataSourcePurchaseOrderDetail" OnBatchEditCommand="RadGridPurchaseOrderDetail_OnBatchEditCommand" OnLoad="RadGridPurchaseOrderDetail_Load" PageSize="20" ShowFooter="True">
                        <MasterTableView AutoGenerateColumns="false" CommandItemDisplay="Top" CommandItemSettings-AddNewRecordText="Add Item" CommandItemSettings-SaveChangesText="Save Changes Item" DataKeyNames="PurchaseOrderDetailId" DataSourceID="LinqDataSourcePurchaseOrderDetail" EditMode="Batch" HorizontalAlign="NotSet">
                            <BatchEditingSettings EditType="Row" />
                            <CommandItemSettings ShowSaveChangesButton="False" AddNewRecordText="Add Item" SaveChangesText="Save Changes Item" />
                            <Columns>

                                <telerik:GridTemplateColumn DataField="Quantity" HeaderText="Quantity" UniqueName="Quantity" FooterText="Sub Total">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# DataBinder.Eval(Container.DataItem, "Quantity") %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadNumericTextBox ID="radNumTextBoxQuantity" runat="server" Width="100%" />
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn DataField="Item" HeaderText="Item" UniqueName="Item">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# DataBinder.Eval(Container.DataItem, "Item") %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadTextBox runat="server" Width="100%" />
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn DataField="Description" HeaderText="Description" UniqueName="Description">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadTextBox runat="server" Width="100%" />
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn DataField="Price" HeaderText="($)Price" UniqueName="Price">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# string.Format("{0:$#,##0.00}", DataBinder.Eval(Container.DataItem, "Price")) %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadNumericTextBox ID="radNumTextBoxPrice" runat="server" Type="Currency" Width="100%" />
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn HeaderText="CAD" UniqueName="Cad">
                                    <ItemTemplate>
                                        <asp:Label Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <telerik:RadNumericTextBox runat="server" HoveredStyle-HorizontalAlign="Left" ReadOnly="true" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Left" Type="Currency" Width="100%">
                                            <ClientEvents OnLoad=" CadLoad " />
                                        </telerik:RadNumericTextBox>
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="150px" ConfirmDialogType="RadWindow" ConfirmText="Delete this Item?" ConfirmTitle="Delete" HeaderStyle-Width="30px" HeaderText="Del" Text="Delete" UniqueName="DeleteColumn">
                                    <HeaderStyle Width="50px" />
                                </telerik:GridButtonColumn>
                            </Columns>

                        </MasterTableView>

                        <ClientSettings EnableRowHoverStyle="True">
                            <ClientEvents OnGridDestroying="RadGridPurchaseOrderDetail_GridDestroying" OnCommand="RadGridPurchaseOrderDetail_Command" OnGridCreated="RadGridPurchaseOrderDetail_GridCreated" OnRowDeleted="RadGridPurchaseOrderDetail_RowDeleted" OnBatchEditCellValueChanged="RadGridPurchaseOrderDetail_BatchEditCellValueChanged" />
                            <%--<Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>--%>
                            <Resizing AllowColumnResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                                ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                            <%--AllowRowResize="True"--%>
                        </ClientSettings>
                        <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                    </telerik:RadGrid>
                    <asp:LinqDataSource ID="LinqDataSourcePurchaseOrderDetail" runat="server" ContextTypeName="Erp2016.Lib.linqDBDataContext" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" TableName="PurchaseOrderDetails"
                        Where="PurchaseOrderDetailId == @PurchaseOrderDetailId">
                        <WhereParameters>
                            <asp:Parameter DefaultValue="0" Name="PurchaseOrderDetailId" Type="Int32" />
                        </WhereParameters>
                    </asp:LinqDataSource>

                    <fieldset>
                        <legend></legend>
                        <div style="float: right;">
                            <label>Tax</label>
                            <telerik:RadNumericTextBox ID="RadNumericTextBoxTax" runat="server" Value="0" HoveredStyle-HorizontalAlign="Right" EnabledStyle-HorizontalAlign="Right" ReadOnly="False" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Right" Type="Currency" Width="100px">
                                <ClientEvents OnLoad=" TaxLoad " OnValueChanged="TaxValueChanged" />
                            </telerik:RadNumericTextBox>

                            <label>Grand Total</label>
                            <telerik:RadNumericTextBox ID="RadNumericTextBoxGrandTotal" runat="server" HoveredStyle-HorizontalAlign="Right" ReadOnly="true" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Right" Type="Currency" Width="100px">
                                <ClientEvents OnLoad=" GrandTotalLoad " />
                            </telerik:RadNumericTextBox>
                        </div>
                    </fieldset>

                </div>
            </telerik:RadPane>
        </telerik:RadSplitter>

    </telerik:RadAjaxPanel>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            // jQuery
            $(window).bind("load", function () {
                SetPurchaseOrderValue();
            });

            // =====================
            // total sum
            // =====================
            var sumCadInput = null;

            function CadLoad(sender, args) {
                sumCadInput = sender;
            }

            // tax
            var sumTaxInput = null;

            function TaxLoad(sender, args) {
                sumTaxInput = sender;
            }

            function TaxValueChanged(sender, args) {
                SetPurchaseOrderValue();
            }

            // grand total
            var sumGrandTotalInput = null;

            function GrandTotalLoad(sender, args) {
                sumGrandTotalInput = sender;
            }

            function GetCellValue(row, columnUniqueName, controlId) {
                var value;
                var testControl = row.findControl(controlId);
                if (testControl) {
                    value = testControl.get_value();
                } else {
                    value = row.get_cell(columnUniqueName).innerText.replace(/[^\d.-]/g, '');
                }
                if (value === "")
                    value = 0;
                return parseFloat(value);
            }

            function isNumeric(n) {
                return !isNaN(parseFloat(n)) && isFinite(n);
            }

            function SetCellValue(row, columnUniqueName, value) {
                row.get_cell(columnUniqueName).innerText = value;
            }

            // set value
            function SetPurchaseOrderValue() {
                var grid = $find("<%= RadGridPurchaseOrderDetail.ClientID %>"); //grid id

                if (grid) {
                    var masterTable = grid.get_masterTableView();
                    var rows = masterTable.get_dataItems();

                    var totalMount = 0.0;
                    for (var i = 0; i < rows.length; i++) {

                        // sum total
                        var tempTotal = GetCellValue(rows[i], "Quantity", "radNumTextBoxQuantity") * GetCellValue(rows[i], "Price", "radNumTextBoxPrice");
                        tempTotal = parseFloat(tempTotal, 10).toFixed(2);
                        SetCellValue(rows[i], "Cad", tempTotal);

                        totalMount = totalMount + parseFloat(tempTotal);
                    }
                    sumCadInput.set_value(totalMount);
                    SetTotalValue();
                }
            }

            function SetTotalValue() {
                var totalPrice = 0;
                if (sumCadInput)
                    totalPrice = sumCadInput.get_value();
                if (sumGrandTotalInput && sumTaxInput)
                    sumGrandTotalInput.set_value(totalPrice + sumTaxInput.get_value());
            }

            // when grid created
            function RadGridPurchaseOrderDetail_GridCreated(sender, eventArgs) {
                SetPurchaseOrderValue();
            }

            // action
            function RadGridPurchaseOrderDetail_Command(sender, eventArgs) {
                if (eventArgs.get_commandName() === "Rebind")
                    SetPurchaseOrderValue();
            }

            // delete
            function RadGridPurchaseOrderDetail_RowDeleted(sender, eventArgs) {
                SetPurchaseOrderValue();
            }

            function RadGridPurchaseOrderDetail_BatchEditCellValueChanged(sender, eventArgs) {
                SetPurchaseOrderValue();
            }

            // == end total sum ==

            function RadToolBar1_OnClientButtonClicking(sender, args) {
                var button = args.get_item();
                if (button.get_text() === "Request") {
                    if (!confirm('Do you want to request?'))
                        args.set_cancel(true);
                }
            }

            var isSaving = false;

            function SaveChanges() {
                isSaving = true;
                var grid = $find("<%= RadGridPurchaseOrderDetail.ClientID %>"); //grid id
                grid.get_batchEditingManager().saveChanges(grid.get_masterTableView());
                // call flight_gridDestroying when grid Updated
            }

            function RadGridPurchaseOrderDetail_GridDestroying(sender, eventArgs) {
                if (isSaving) {
                    isSaving = false;
                    Close();
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

            function ShowApprovalWindow(id) {
                var oWnd = window.radopen('ApprovalApprovePop?type=' + <%= (int)CConstValue.Approval.PurchaseOrder %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalRejectWindow(id) {

                var oWnd = window.radopen('ApprovalRejectPop?type=' + <%= (int)CConstValue.Approval.PurchaseOrder %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalReviseWindow(id) {
                var oWnd = window.radopen('ApprovalRevisePop?type=' + <%= (int)CConstValue.Approval.PurchaseOrder %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }


            function ShowApprovalAcceptWindow(id) {
                var oWnd = window.radopen('ApprovalAcceptPop?type=' + <%= (int)CConstValue.Approval.PurchaseOrder %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalCompleteWindow(id) {
                var oWnd = window.radopen('ApprovalCompletePop?type=' + <%= (int)CConstValue.Approval.PurchaseOrder %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 300);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalCancelWindow(id) {
                var oWnd = window.radopen('ApprovalCancelPop?type=' + <%= (int)CConstValue.Approval.PurchaseOrder %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function OnClientClose(oWnd, args) {
                Close();
            }

            function NewKeyPress(sender, args) {
                var keyCharacter = args.get_keyCharacter();

                if (keyCharacter == sender.get_numberFormat().DecimalSeparator ||
                    keyCharacter == sender.get_numberFormat().NegativeSign) {
                    args.set_cancel(true);
                }
            }

            function HideReview() {
                $('#review').display = "none";
            }

            function ShowPrint() {
                $("#divPrint").printArea();
            }

        </script>

    </telerik:RadCodeBlock>
</asp:Content>
