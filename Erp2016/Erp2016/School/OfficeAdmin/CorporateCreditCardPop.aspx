<%@ Page Title="Corporate Credit Card" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="CorporateCreditCardPop.aspx.cs" Inherits="School.OfficeAdmin.CorporateCreditCardPop" %>

<%@ Import Namespace="Erp2016.Lib" %>

<%@ Register TagPrefix="usercontrol" TagName="approvalline" Src="~/App_Data/ApprovalLine.ascx" %>
<%@ Register TagPrefix="UserControl" TagName="FileDownloadList" Src="~/App_Data/FileDownloadList.ascx" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>


<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGridCorporateCreditCardDetail">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridCorporateCreditCardDetail" LoadingPanelID="RadAjaxLoadingPanel1" />
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


            <telerik:RadPane ID="RadPanePrint" runat="server" Scrolling="Both">

                <div id="divPrint">

                    <telerik:RadGrid ID="RadGridInfo" runat="server" GroupPanelPosition="Top" />

                    <UserControl:approvalline ID="ApprovalLine1" runat="server" OnLoad="ApprovalLine1_OnLoad" />

                    <fieldset>
                        <legend>Corporate Credit Card Info</legend>
                        <table>
                            <tr>
                                <td>
                                    <label><b style="color: red">*</b> Period Start</label>
                                    <telerik:RadDatePicker ID="RadDatePickerStart" CssClass="RadSizeMiddle" runat="server" Culture="English (Canada)">
                                        <DateInput DateFormat="MM-dd-yyyy" />
                                    </telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="RadDatePickerStart" Display="Dynamic" ErrorMessage="Period Start Required" ForeColor="Red" ValidationGroup="Info" />
                                    <label><b style="color: red">*</b> Period End</label>
                                    <telerik:RadDatePicker ID="RadDatePickerEnd" CssClass="RadSizeMiddle" runat="server" Culture="English (Canada)">
                                        <DateInput DateFormat="MM-dd-yyyy" />
                                    </telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="RadDatePickerEnd" Display="Dynamic" ErrorMessage="Period End Required" ForeColor="Red" ValidationGroup="Info" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>

                    <UserControl:FileDownloadList ID="FileDownloadList1" runat="server" />

                    <h4>Corporate Credit Card Detail List</h4>

                    <telerik:RadGrid ID="RadGridCorporateCreditCardDetail" runat="server" AllowAutomaticDeletes="true" AllowAutomaticInserts="true" AllowAutomaticUpdates="true" AllowPaging="False" AutoGenerateColumns="false" DataSourceID="LinqDataSourceCorporateCreditCardDetail" OnBatchEditCommand="RadGridCorporateCreditCardDetail_OnBatchEditCommand" OnLoad="RadGridCorporateCreditCardDetail_Load" PageSize="20" ShowFooter="True">
                        <MasterTableView AutoGenerateColumns="false" CommandItemDisplay="Top" CommandItemSettings-AddNewRecordText="Add Item" CommandItemSettings-SaveChangesText="Save Changes Item" DataKeyNames="CorporateCreditCardDetailId" DataSourceID="LinqDataSourceCorporateCreditCardDetail" EditMode="Batch">
                            <BatchEditingSettings EditType="Row" />
                            <CommandItemSettings ShowSaveChangesButton="False" AddNewRecordText="Add Item" SaveChangesText="Save Changes Item" />
                            <Columns>
                                <telerik:GridTemplateColumn DataField="Date" HeaderText="Date" UniqueName="Date">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# string.Format("{0:MM-dd-yyyy}", DataBinder.Eval(Container.DataItem, "Date")) %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadDatePicker runat="server" Width="100%" Culture="English (Canada)">
                                            <DateInput DateFormat="MM-dd-yyyy" />
                                        </telerik:RadDatePicker>
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn
                                    HeaderText="Site Location" DataField="SiteLocationId" SortExpression="SiteLocationId" UniqueName="SiteLocationId" HeaderStyle-HorizontalAlign="Center"
                                    AllowFiltering="False" FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                    <ItemTemplate>
                                        <telerik:RadDropDownList runat="server" DataValueField="SiteLocationId" DataTextField="SiteAndSiteLocationName" DataSourceID="LinqDataSourceSiteLocation" SelectedValue='<%# Eval("SiteLocationId") %>' Enabled="false" Width="100%" ToolTip='<%# Eval("SiteLocationId") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadComboBox Width="100%" runat="server" AutoPostBack="False" DataValueField="SiteLocationId" DataTextField="SiteAndSiteLocationName" DataSourceID="LinqDataSourceSiteLocation" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn DataField="Description" HeaderText="Description of expense" UniqueName="Description">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadTextBox runat="server" Width="100%" />
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn DataField="Web" HeaderText="($)Web" UniqueName="Web">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# string.Format("{0:$#,##0.00}", DataBinder.Eval(Container.DataItem, "Web")) %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadNumericTextBox ID="radNumTextBoxWeb" runat="server" Type="Currency" Width="100%">
                                            <ClientEvents OnValueChanged="RadGridCorporateCreditCardDetail_ValueChanged" />
                                        </telerik:RadNumericTextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <telerik:RadNumericTextBox runat="server" HoveredStyle-HorizontalAlign="Left" ReadOnly="true" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Left" Type="Currency" Width="100%">
                                            <ClientEvents OnLoad=" WebLoad " />
                                        </telerik:RadNumericTextBox>
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn DataField="Ground" HeaderText="($)Ground transportation" UniqueName="Ground">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# string.Format("{0:$#,##0.00}", DataBinder.Eval(Container.DataItem, "Ground")) %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadNumericTextBox ID="radNumTextBoxGround" runat="server" Type="Currency" Width="100%">
                                            <ClientEvents OnValueChanged="RadGridCorporateCreditCardDetail_ValueChanged" />
                                        </telerik:RadNumericTextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <telerik:RadNumericTextBox runat="server" HoveredStyle-HorizontalAlign="Left" ReadOnly="true" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Left" Type="Currency" Width="100%">
                                            <ClientEvents OnLoad=" GroundLoad " />
                                        </telerik:RadNumericTextBox>
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn DataField="Office" HeaderText="($)Office Supplies" UniqueName="Office">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# string.Format("{0:$#,##0.00}", DataBinder.Eval(Container.DataItem, "Office")) %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadNumericTextBox ID="radNumTextBoxOffice" runat="server" Type="Currency" Width="100%">
                                            <ClientEvents OnValueChanged="RadGridCorporateCreditCardDetail_ValueChanged" />
                                        </telerik:RadNumericTextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <telerik:RadNumericTextBox runat="server" HoveredStyle-HorizontalAlign="Left" ReadOnly="true" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Left" Type="Currency" Width="100%">
                                            <ClientEvents OnLoad=" OfficeLoad " />
                                        </telerik:RadNumericTextBox>
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn DataField="Meals" HeaderText="($)Meals and Entertainment" UniqueName="Meals">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# string.Format("{0:$#,##0.00}", DataBinder.Eval(Container.DataItem, "Meals")) %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadNumericTextBox ID="radNumTextBoxMeals" runat="server" Type="Currency" Width="100%">
                                            <ClientEvents OnValueChanged="RadGridCorporateCreditCardDetail_ValueChanged" />
                                        </telerik:RadNumericTextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <telerik:RadNumericTextBox runat="server" HoveredStyle-HorizontalAlign="Left" ReadOnly="true" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Left" Type="Currency" Width="100%">
                                            <ClientEvents OnLoad=" MealsLoad " />
                                        </telerik:RadNumericTextBox>
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn DataField="Promotion" HeaderText="($)Promotion and Advertising" UniqueName="Promotion">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# string.Format("{0:$#,##0.00}", DataBinder.Eval(Container.DataItem, "Promotion")) %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadNumericTextBox ID="radNumTextBoxPromotion" runat="server" Type="Currency" Width="100%">
                                            <ClientEvents OnValueChanged="RadGridCorporateCreditCardDetail_ValueChanged" />
                                        </telerik:RadNumericTextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <telerik:RadNumericTextBox runat="server" HoveredStyle-HorizontalAlign="Left" ReadOnly="true" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Left" Type="Currency" Width="100%">
                                            <ClientEvents OnLoad=" PromotionLoad " />
                                        </telerik:RadNumericTextBox>
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn DataField="Membership" HeaderText="($)Membership" UniqueName="Membership">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# string.Format("{0:$#,##0.00}", DataBinder.Eval(Container.DataItem, "Membership")) %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadNumericTextBox ID="radNumTextBoxMembership" runat="server" Type="Currency" Width="100%">
                                            <ClientEvents OnValueChanged="RadGridCorporateCreditCardDetail_ValueChanged" />
                                        </telerik:RadNumericTextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <telerik:RadNumericTextBox runat="server" HoveredStyle-HorizontalAlign="Left" ReadOnly="true" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Left" Type="Currency" Width="100%">
                                            <ClientEvents OnLoad=" MembershipLoad " />
                                        </telerik:RadNumericTextBox>
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn DataField="Mail" HeaderText="($)Mail & Postal PMT" UniqueName="Mail">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# string.Format("{0:$#,##0.00}", DataBinder.Eval(Container.DataItem, "Mail")) %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadNumericTextBox ID="radNumTextBoxMail" runat="server" Type="Currency" Width="100%">
                                            <ClientEvents OnValueChanged="RadGridCorporateCreditCardDetail_ValueChanged" />
                                        </telerik:RadNumericTextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <telerik:RadNumericTextBox runat="server" HoveredStyle-HorizontalAlign="Left" ReadOnly="true" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Left" Type="Currency" Width="100%">
                                            <ClientEvents OnLoad=" MailLoad " />
                                        </telerik:RadNumericTextBox>
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn DataField="Legal" HeaderText="($)Legal" UniqueName="Legal">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "Legal")) %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadNumericTextBox ID="radNumTextBoxLegal" runat="server" Type="Number" Width="100%">
                                            <ClientEvents OnValueChanged="RadGridCorporateCreditCardDetail_ValueChanged" />
                                        </telerik:RadNumericTextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <telerik:RadNumericTextBox runat="server" HoveredStyle-HorizontalAlign="Left" ReadOnly="true" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Left" Type="Currency" Width="100%">
                                            <ClientEvents OnLoad=" LegalLoad " />
                                        </telerik:RadNumericTextBox>
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn DataField="Student" HeaderText="($)Student Supplies" UniqueName="Student">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# string.Format("{0:$#,##0.00}", DataBinder.Eval(Container.DataItem, "Student")) %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadNumericTextBox ID="radNumTextBoxStudent" runat="server" Type="Currency" Width="100%">
                                            <ClientEvents OnValueChanged="RadGridCorporateCreditCardDetail_ValueChanged" />
                                        </telerik:RadNumericTextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <telerik:RadNumericTextBox runat="server" HoveredStyle-HorizontalAlign="Left" ReadOnly="true" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Left" Type="Currency" Width="100%">
                                            <ClientEvents OnLoad=" StudentLoad " />
                                        </telerik:RadNumericTextBox>
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn DataField="Miscellaneous" HeaderText="($)Miscellaneous" UniqueName="Miscellaneous">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# string.Format("{0:$#,##0.00}", DataBinder.Eval(Container.DataItem, "Miscellaneous")) %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadNumericTextBox ID="radNumTextBoxMiscellaneous" runat="server" Type="Currency" Width="100%">
                                            <ClientEvents OnValueChanged="RadGridCorporateCreditCardDetail_ValueChanged" />
                                        </telerik:RadNumericTextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <telerik:RadNumericTextBox runat="server" HoveredStyle-HorizontalAlign="Left" ReadOnly="true" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Left" Type="Currency" Width="100%">
                                            <ClientEvents OnLoad=" MiscellaneousLoad " />
                                        </telerik:RadNumericTextBox>
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn HeaderText="CAD" UniqueName="Cad">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Width="100%" />
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
                            <ClientEvents OnGridDestroying="RadGridCorporateCreditCardDetail_GridDestroying" OnCommand="RadGridCorporateCreditCardDetail_Command" OnGridCreated="RadGridCorporateCreditCardDetail_GridCreated" OnRowDeleted="RadGridCorporateCreditCardDetail_RowDeleted" />
                            <%--<Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>--%>
                            <Resizing AllowColumnResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                                ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                            <%--AllowRowResize="True"--%>
                        </ClientSettings>
                        <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                    </telerik:RadGrid>
                    <asp:LinqDataSource ID="LinqDataSourceCorporateCreditCardDetail" runat="server" ContextTypeName="Erp2016.Lib.linqDBDataContext" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" TableName="CorporateCreditCardDetails"
                        Where="CorporateCreditCardDetailId == @CorporateCreditCardDetailId">
                        <WhereParameters>
                            <asp:Parameter DefaultValue="0" Name="CorporateCreditCardDetailId" Type="Int32" />
                        </WhereParameters>
                    </asp:LinqDataSource>

                    <asp:LinqDataSource ID="LinqDataSourceSiteLocation" runat="server"
                        ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName=""
                        TableName="vwSiteLocationLists">
                    </asp:LinqDataSource>

                    <fieldset>
                        <legend></legend>
                        <div style="float: right;">
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

        <%--    function pageLoad() {
                var grid = $find("<%= RadGridCorporateCreditCardDetail.ClientID %>");
                if (grid != null) {
                    var columns = grid.get_masterTableView().get_columns();
                    for (var i = 0; i < columns.length; i++) {
                        columns[i].resizeToFit();
                    }
                }
            }--%>

            // jQuery
            $(window).bind("load", function () {
                SetCorporateCreditCardValue();
            });

            // =====================
            // total sum
            // =====================
            var sumWebInput = null;
            var sumGroundInput = null;
            var sumOfficeInput = null;
            var sumMealsInput = null;
            var sumPromotionInput = null;
            var sumMembershipInput = null;
            var sumMailInput = null;
            var sumLegalInput = null;
            var sumStudentInput = null;
            var sumMiscellaneousInput = null;

            function WebLoad(sender, args) {
                sumWebInput = sender;
            }
            function GroundLoad(sender, args) {
                sumGroundInput = sender;
            }
            function OfficeLoad(sender, args) {
                sumOfficeInput = sender;
            }
            function MealsLoad(sender, args) {
                sumMealsInput = sender;
            }
            function PromotionLoad(sender, args) {
                sumPromotionInput = sender;
            }
            function MembershipLoad(sender, args) {
                sumMembershipInput = sender;
            }
            function MailLoad(sender, args) {
                sumMailInput = sender;
            }
            function LegalLoad(sender, args) {
                sumLegalInput = sender;
            }
            function StudentLoad(sender, args) {
                sumStudentInput = sender;
            }
            function MiscellaneousLoad(sender, args) {
                sumMiscellaneousInput = sender;
            }

            var sumCadInput = null;
            function CadLoad(sender, args) {
                sumCadInput = sender;
            }

            // total
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
            function SetCorporateCreditCardValue() {
                var grid = $find("<%= RadGridCorporateCreditCardDetail.ClientID %>"); //grid id

                if (grid) {
                    var masterTable = grid.get_masterTableView();
                    var rows = masterTable.get_dataItems();

                    var totalWeb = 0.0;
                    var totalGround = 0.0;
                    var totalOffice = 0.0;
                    var totalMeals = 0.0;
                    var totalPromotion = 0.0;
                    var totalMembership = 0.0;
                    var totalMail = 0.0;
                    var totalLegal = 0.0;
                    var totalStudent = 0.0;
                    var totalMiscellaneous = 0.0;

                    var totalMount = 0.0;
                    for (var i = 0; i < rows.length; i++) {

                        var tempWeb = GetCellValue(rows[i], "Web", "radNumTextBoxWeb");
                        var tempGround = GetCellValue(rows[i], "Ground", "radNumTextBoxGround");
                        var tempOffice = GetCellValue(rows[i], "Office", "radNumTextBoxOffice");
                        var tempMeals = GetCellValue(rows[i], "Meals", "radNumTextBoxMeals");
                        var tempPromotion = GetCellValue(rows[i], "Promotion", "radNumTextBoxPromotion");
                        var tempMembership = GetCellValue(rows[i], "Membership", "radNumTextBoxMembership");
                        var tempMail = GetCellValue(rows[i], "Mail", "radNumTextBoxMail");
                        var tempLegal = GetCellValue(rows[i], "Legal", "radNumTextBoxLegal");
                        var tempStudent = GetCellValue(rows[i], "Student", "radNumTextBoxStudent");
                        var tempMiscellaneous = GetCellValue(rows[i], "Miscellaneous", "radNumTextBoxMiscellaneous");

                        // sum total
                        var tempTotal = tempWeb +
                            tempGround +
                            tempOffice +
                            tempMeals +
                            tempPromotion +
                            tempMembership +
                            tempMail +
                            tempLegal +
                            tempStudent +
                            tempMiscellaneous;
                        SetCellValue(rows[i], "Cad", tempTotal);

                        totalWeb = totalWeb + parseFloat(tempWeb);
                        totalGround = totalGround + parseFloat(tempGround);
                        totalOffice = totalOffice + parseFloat(tempOffice);
                        totalMeals = totalMeals + parseFloat(tempMeals);
                        totalPromotion = totalPromotion + parseFloat(tempPromotion);
                        totalMembership = totalMembership + parseFloat(tempMembership);
                        totalMail = totalMail + parseFloat(tempMail);
                        totalLegal = totalLegal + parseFloat(tempLegal);
                        totalStudent = totalStudent + parseFloat(tempStudent);
                        totalMiscellaneous = totalMiscellaneous + parseFloat(tempMiscellaneous);
                        totalMount = totalMount + parseFloat(tempTotal);
                    }

                    sumWebInput.set_value(totalWeb);
                    sumGroundInput.set_value(totalGround);
                    sumOfficeInput.set_value(totalOffice);
                    sumMealsInput.set_value(totalMeals);
                    sumPromotionInput.set_value(totalPromotion);
                    sumMembershipInput.set_value(totalMembership);
                    sumMailInput.set_value(totalMail);
                    sumLegalInput.set_value(totalLegal);
                    sumStudentInput.set_value(totalStudent);
                    sumMiscellaneousInput.set_value(totalMiscellaneous);

                    sumCadInput.set_value(totalMount);
                    SetTotalValue();
                }
            }

            function SetTotalValue() {
                if (sumGrandTotalInput)
                    sumGrandTotalInput.set_value(sumCadInput.get_value());
            }

            // when grid created
            function RadGridCorporateCreditCardDetail_GridCreated(sender, eventArgs) {
                SetCorporateCreditCardValue();
            }

            // action
            function RadGridCorporateCreditCardDetail_Command(sender, eventArgs) {
                if (eventArgs.get_commandName() === "Rebind")
                    SetCorporateCreditCardValue();
            }

            // delete
            function RadGridCorporateCreditCardDetail_RowDeleted(sender, eventArgs) {
                SetCorporateCreditCardValue();
            }

            // change value
            function RadGridCorporateCreditCardDetail_ValueChanged(sender, eventArgs) {
                SetCorporateCreditCardValue();
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
                var grid = $find("<%= RadGridCorporateCreditCardDetail.ClientID %>"); //grid id
                grid.get_batchEditingManager().saveChanges(grid.get_masterTableView());
                // call flight_gridDestroying when grid Updated
            }

            function RadGridCorporateCreditCardDetail_GridDestroying(sender, eventArgs) {
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
                var oWnd = window.radopen('ApprovalApprovePop?type=' + <%= (int)CConstValue.Approval.CorporateCreditCard %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalRejectWindow(id) {

                var oWnd = window.radopen('ApprovalRejectPop?type=' + <%= (int)CConstValue.Approval.CorporateCreditCard %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalReviseWindow(id) {
                var oWnd = window.radopen('ApprovalRevisePop?type=' + <%= (int)CConstValue.Approval.CorporateCreditCard %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }


            function ShowApprovalAcceptWindow(id) {
                var oWnd = window.radopen('ApprovalAcceptPop?type=' + <%= (int)CConstValue.Approval.CorporateCreditCard %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalCompleteWindow(id) {
                var oWnd = window.radopen('ApprovalCompletePop?type=' + <%= (int)CConstValue.Approval.CorporateCreditCard %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 300);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalCancelWindow(id) {
                var oWnd = window.radopen('ApprovalCancelPop?type=' + <%= (int)CConstValue.Approval.CorporateCreditCard %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function OnClientClose(oWnd, args) {
                Close();
                //var arg = args.get_argument();
        <%--<%=Page.GetPostBackEventReference(btnRefresh)%>--%>
            }

            function getOuterHTML(obj) {
                if (typeof (obj.outerHTML) == "undefined") {
                    var divWrapper = document.createElement("div");
                    var copyOb = obj.cloneNode(true);
                    divWrapper.appendChild(copyOb);
                    return divWrapper.innerHTML
                }
                else {
                    return obj.outerHTML;
                } n
            }

            function PrintRadGrid() {
                var radGrid = $find('<%= RadGridCorporateCreditCardDetail.ClientID %>');
                var previewWindow = window.open('about:blank', '', '', false);
                var styleSheet = '<%= Telerik.Web.SkinRegistrar.GetWebResourceUrl(this, RadGridCorporateCreditCardDetail.GetType(), String.Format("Telerik.Web.UI.Skins.{0}.Grid.{0}.css", RadGridCorporateCreditCardDetail.Skin)) %>';
                var baseStyleSheet = '<%= Telerik.Web.SkinRegistrar.GetWebResourceUrl(this, RadGridCorporateCreditCardDetail.GetType(), "Telerik.Web.UI.Skins.Grid.css") %>';
                var htmlContent = "<html><head><link href = '" + styleSheet + "' rel='stylesheet' type='text/css'></link>";

                htmlContent += "<link href = '" + baseStyleSheet + "' rel='stylesheet' type='text/css'></link></head>";

                htmlContent = "<html><head>";

                htmlContent = htmlContent + "<body>" + getOuterHTML(radGrid.get_element()) + "</body></html>";
                previewWindow.document.open();
                previewWindow.document.write(htmlContent);
                previewWindow.document.close();
                previewWindow.print();

                if (!$telerik.isChrome) {
                    previewWindow.close();
                }
            }

            function ShowPrint() {
                $("#divPrint").printArea();
            }

        </script>

    </telerik:RadCodeBlock>
</asp:Content>
