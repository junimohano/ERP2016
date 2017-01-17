<%@ Page Title="Expense" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="ExpensePop.aspx.cs" Inherits="School.OfficeAdmin.ExpensePop" %>

<%@ Import Namespace="Erp2016.Lib" %>

<%@ Register TagPrefix="usercontrol" TagName="approvalline" Src="~/App_Data/ApprovalLine.ascx" %>
<%@ Register TagPrefix="UserControl" TagName="FileDownloadList" Src="~/App_Data/FileDownloadList.ascx" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGridExpenseDetail">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridExpenseDetail" LoadingPanelID="RadAjaxLoadingPanel1" />
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

            <telerik:RadPane ID="RadPane1" runat="server" Scrolling="Both">

                <div id="divPrint">

                    <telerik:RadGrid ID="RadGridInfo" runat="server" GroupPanelPosition="Top" />

                    <UserControl:approvalline ID="ApprovalLine1" runat="server" OnLoad="ApprovalLine1_OnLoad" />

                    <fieldset>
                        <legend>Expense List</legend>
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

                    <h4>Expense Detail List</h4>

                    <telerik:RadGrid ID="RadGridExpenseDetail" runat="server" AllowAutomaticDeletes="true" AllowAutomaticInserts="true" AllowAutomaticUpdates="true" AllowPaging="False" AutoGenerateColumns="false" DataSourceID="LinqDataSourceExpenseDetail" OnBatchEditCommand="RadGridExpenseDetail_OnBatchEditCommand" OnLoad="RadGridExpenseDetail_Load" PageSize="20" ShowFooter="True">
                        <MasterTableView AutoGenerateColumns="false" CommandItemDisplay="Top" CommandItemSettings-AddNewRecordText="Add Item" CommandItemSettings-SaveChangesText="Save Changes Item" DataKeyNames="ExpenseDetailId" DataSourceID="LinqDataSourceExpenseDetail" EditMode="Batch" HorizontalAlign="NotSet">
                            <BatchEditingSettings EditType="Row" />
                            <CommandItemSettings ShowSaveChangesButton="False" AddNewRecordText="Add Item" SaveChangesText="Save Changes Item" />
                            <Columns>
                                <telerik:GridTemplateColumn DataField="Date" HeaderText="Date" UniqueName="Date" FooterText="Sub Total">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# string.Format("{0:MM-dd-yyyy}", DataBinder.Eval(Container.DataItem, "Date")) %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadDatePicker runat="server" Width="100%" Culture="English (Canada)">
                                            <DateInput DateFormat="MM-dd-yyyy" />
                                        </telerik:RadDatePicker>
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

                                <telerik:GridTemplateColumn DataField="Office" HeaderText="($)Office Supplies" UniqueName="Office">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# string.Format("{0:$#,##0.00}", DataBinder.Eval(Container.DataItem, "Office")) %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadNumericTextBox ID="radNumTextBoxOffice" runat="server" Type="Currency" Width="100%">
                                            <ClientEvents OnValueChanged="RadGridExpenseDetail_ValueChanged" />
                                        </telerik:RadNumericTextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <telerik:RadNumericTextBox runat="server" HoveredStyle-HorizontalAlign="Left" ReadOnly="true" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Left" Type="Currency" Width="100%">
                                            <ClientEvents OnLoad=" OfficeLoad " />
                                        </telerik:RadNumericTextBox>
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn DataField="Lodging" HeaderText="($)Lodging" UniqueName="Lodging">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# string.Format("{0:$#,##0.00}", DataBinder.Eval(Container.DataItem, "Lodging")) %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadNumericTextBox ID="radNumTextBoxLodging" runat="server" Type="Currency" Width="100%">
                                            <ClientEvents OnValueChanged="RadGridExpenseDetail_ValueChanged" />
                                        </telerik:RadNumericTextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <telerik:RadNumericTextBox runat="server" HoveredStyle-HorizontalAlign="Left" ReadOnly="true" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Left" Type="Currency" Width="100%">
                                            <ClientEvents OnLoad=" LodgingLoad " />
                                        </telerik:RadNumericTextBox>
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn DataField="Ground" HeaderText="($)Ground transportation" UniqueName="Ground">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# string.Format("{0:$#,##0.00}", DataBinder.Eval(Container.DataItem, "Ground")) %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadNumericTextBox ID="radNumTextBoxGround" runat="server" Type="Currency" Width="100%">
                                            <ClientEvents OnValueChanged="RadGridExpenseDetail_ValueChanged" />
                                        </telerik:RadNumericTextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <telerik:RadNumericTextBox runat="server" HoveredStyle-HorizontalAlign="Left" ReadOnly="true" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Left" Type="Currency" Width="100%">
                                            <ClientEvents OnLoad=" GroundLoad " />
                                        </telerik:RadNumericTextBox>
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn DataField="Meals" HeaderText="($)Meals and Entertainment" UniqueName="Meals">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# string.Format("{0:$#,##0.00}", DataBinder.Eval(Container.DataItem, "Meals")) %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadNumericTextBox ID="radNumTextBoxMeals" runat="server" Type="Currency" Width="100%">
                                            <ClientEvents OnValueChanged="RadGridExpenseDetail_ValueChanged" />
                                        </telerik:RadNumericTextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <telerik:RadNumericTextBox runat="server" HoveredStyle-HorizontalAlign="Left" ReadOnly="true" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Left" Type="Currency" Width="100%">
                                            <ClientEvents OnLoad=" MealsLoad " />
                                        </telerik:RadNumericTextBox>
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn DataField="Advertising" HeaderText="($)Advertising and Promotion" UniqueName="Advertising">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# string.Format("{0:$#,##0.00}", DataBinder.Eval(Container.DataItem, "Advertising")) %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadNumericTextBox ID="radNumTextBoxAdvertising" runat="server" Type="Currency" Width="100%">
                                            <ClientEvents OnValueChanged="RadGridExpenseDetail_ValueChanged" />
                                        </telerik:RadNumericTextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <telerik:RadNumericTextBox runat="server" HoveredStyle-HorizontalAlign="Left" ReadOnly="true" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Left" Type="Currency" Width="100%">
                                            <ClientEvents OnLoad=" AdvertisingLoad " />
                                        </telerik:RadNumericTextBox>
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn DataField="Mail" HeaderText="($)Mail & Postal" UniqueName="Mail">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# string.Format("{0:$#,##0.00}", DataBinder.Eval(Container.DataItem, "Mail")) %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadNumericTextBox ID="radNumTextBoxMail" runat="server" Type="Currency" Width="100%">
                                            <ClientEvents OnValueChanged="RadGridExpenseDetail_ValueChanged" />
                                        </telerik:RadNumericTextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <telerik:RadNumericTextBox runat="server" HoveredStyle-HorizontalAlign="Left" ReadOnly="true" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Left" Type="Currency" Width="100%">
                                            <ClientEvents OnLoad=" MailLoad " />
                                        </telerik:RadNumericTextBox>
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn DataField="Telephone" HeaderText="($)Telephone" UniqueName="Telephone">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# string.Format("{0:$#,##0.00}", DataBinder.Eval(Container.DataItem, "Telephone")) %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadNumericTextBox ID="radNumTextBoxTelephone" runat="server" Type="Currency" Width="100%">
                                            <ClientEvents OnValueChanged="RadGridExpenseDetail_ValueChanged" />
                                        </telerik:RadNumericTextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <telerik:RadNumericTextBox runat="server" HoveredStyle-HorizontalAlign="Left" ReadOnly="true" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Left" Type="Currency" Width="100%">
                                            <ClientEvents OnLoad=" TelephoneLoad " />
                                        </telerik:RadNumericTextBox>
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn DataField="Km" HeaderText="Km (personal car only)" UniqueName="Km">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# string.Format("{0:N}", DataBinder.Eval(Container.DataItem, "Km")) %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadNumericTextBox ID="radNumTextBoxKm" runat="server" Type="Number" Width="100%">
                                            <ClientEvents OnValueChanged="RadGridExpenseDetail_ValueChanged" />
                                        </telerik:RadNumericTextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <telerik:RadNumericTextBox runat="server" HoveredStyle-HorizontalAlign="Left" ReadOnly="true" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Left" Type="Currency" Width="100%">
                                            <ClientEvents OnLoad=" KmLoad " />
                                        </telerik:RadNumericTextBox>
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn HeaderText="Kilometrage reimbursement" UniqueName="Kilometrage">
                                    <ItemTemplate>
                                        <asp:Label Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <telerik:RadNumericTextBox runat="server" HoveredStyle-HorizontalAlign="Left" ReadOnly="true" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Left" Type="Currency" Width="100%">
                                            <ClientEvents OnLoad=" KilometrageLoad " />
                                        </telerik:RadNumericTextBox>
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn DataField="Miscellaneous" HeaderText="($)Miscellaneous" UniqueName="Miscellaneous">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# string.Format("{0:$#,##0.00}", DataBinder.Eval(Container.DataItem, "Miscellaneous")) %>' Width="100%" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadNumericTextBox ID="radNumTextBoxMiscellaneous" runat="server" Type="Currency" Width="100%">
                                            <ClientEvents OnValueChanged="RadGridExpenseDetail_ValueChanged" />
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
                            <ClientEvents OnGridDestroying="RadGridExpenseDetail_GridDestroying" OnCommand="RadGridExpenseDetail_Command" OnGridCreated="RadGridExpenseDetail_GridCreated" OnRowDeleted="RadGridExpenseDetail_RowDeleted" />
                            <%--<Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>--%>
                            <Resizing AllowColumnResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                                ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                            <%--AllowRowResize="True"--%>
                        </ClientSettings>
                        <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                    </telerik:RadGrid>
                    <asp:LinqDataSource ID="LinqDataSourceExpenseDetail" runat="server" ContextTypeName="Erp2016.Lib.linqDBDataContext" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" TableName="ExpenseDetails"
                        Where="ExpenseDetailId == @ExpenseDetailId">
                        <WhereParameters>
                            <asp:Parameter DefaultValue="0" Name="ExpenseDetailId" Type="Int32" />
                        </WhereParameters>
                    </asp:LinqDataSource>

                    <fieldset>
                        <legend></legend>
                        <div style="float: right;">
                            <label>($)Cash Advance</label>
                            <telerik:RadNumericTextBox ID="RadNumericTextBoxCashAdvance" runat="server" HoveredStyle-HorizontalAlign="Right" EnabledStyle-HorizontalAlign="Right" ReadOnly="False" ReadOnlyStyle-BorderStyle="None" ReadOnlyStyle-HorizontalAlign="Right" Type="Currency" Width="100px">
                                <ClientEvents OnLoad=" CashAdvanceLoad " OnValueChanged="CashAdvanceValueChanged" />
                            </telerik:RadNumericTextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RadNumericTextBoxCashAdvance" Display="Dynamic" ErrorMessage="CashAdvance Required" ForeColor="Red" ValidationGroup="Info" />

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

            <%--function pageLoad() {
                var grid = $find("<%= RadGridExpenseDetail.ClientID %>");
                if (grid != null) {
                    var columns = grid.get_masterTableView().get_columns();
                    for (var i = 0; i < columns.length; i++) {
                        columns[i].resizeToFit();
                    }
                }
            }--%>

            // jQuery
            $(window).bind("load", function () {
                SetExpenseValue();
            });

            // =====================
            // total sum
            // =====================
            var sumOfficeInput = null;
            var sumLodgingInput = null;
            var sumGroundInput = null;
            var sumMealsInput = null;
            var sumAdvertisingInput = null;
            var sumMailInput = null;
            var sumTelephoneInput = null;
            var sumKmInput = null;
            var sumMiscellaneousInput = null;

            function OfficeLoad(sender, args) {
                sumOfficeInput = sender;
            }
            function LodgingLoad(sender, args) {
                sumLodgingInput = sender;
            }
            function GroundLoad(sender, args) {
                sumGroundInput = sender;
            }
            function MealsLoad(sender, args) {
                sumMealsInput = sender;
            }
            function AdvertisingLoad(sender, args) {
                sumAdvertisingInput = sender;
            }
            function MailLoad(sender, args) {
                sumMailInput = sender;
            }
            function TelephoneLoad(sender, args) {
                sumTelephoneInput = sender;
            }
            function KmLoad(sender, args) {
                sumKmInput = sender;
            }
            function MiscellaneousLoad(sender, args) {
                sumMiscellaneousInput = sender;
            }

            var sumKilometrageInput = null;
            function KilometrageLoad(sender, args) {
                sumKilometrageInput = sender;
            }

            var sumCadInput = null;
            function CadLoad(sender, args) {
                sumCadInput = sender;
            }

            var sumCashAdvanceInput = null;
            function CashAdvanceLoad(sender, args) {
                sumCashAdvanceInput = sender;
            }

            function CashAdvanceValueChanged(sender, args) {
                SetTotalValue();
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
                if (value == "")
                    value = 0;
                return parseFloat(value);
            }

            function isNumeric(n) {
                return !isNaN(parseFloat(n)) && isFinite(n);
            }

            function SetCellValue(row, columnUniqueName, value) {

                row.get_cell(columnUniqueName).innerText = value;

                ////var cell = row.findControl(controlId);
                ////if (cell) {
                ////    cell.blur();
                ////    cell.focus();
                ////    if (cell.Set != true) {
                ////        cell.Set = true;
                ////        cell.beginUpdate();
                ////        cell.set_value(value);
                ////        cell.endUpdate();
                ////        cell.updateDisplayValue();
                ////    }
                ////}

                //var tempHtml = row.get_cell(columnUniqueName).innerHTML;
                //var tempIndexStart = tempHtml.indexOf("<div>");
                //var tempIndexEnd = tempHtml.indexOf("</div>");
                //var tempValue = tempHtml.substring(tempIndexStart + "<div>".length, tempIndexEnd);
                //var tempCurrency = "";

                //if (isNumeric(tempValue) == false) {
                //    tempCurrency = tempValue.substring(0, 1);
                //    if (tempCurrency == "&") {
                //        tempCurrency = "$";
                //    }
                //}

                //tempHtml = tempHtml.replace("<div>" + tempValue + "</div>", "<div>" + tempCurrency + value + "</div>");
                //tempHtml = tempHtml.replace("<div style=\"display: none;\">" + tempValue + "</div>", "<div style=\"display: none;\">" + tempCurrency + value + "</div>");
                //if (tempValue == "&nbsp;") {
                //    tempValue = "";
                //}

                //tempHtml = tempHtml.replace("validationText\":\"" + tempValue.replace(".00", "").replace(tempCurrency, ""), "validationText\":\"" + value.toString().replace(".00", ""));
                //tempHtml = tempHtml.replace("valueAsString\":\"" + tempValue.replace(".00", "").replace(tempCurrency, ""), "valueAsString\":\"" + value.toString().replace(".00", ""));
                //tempHtml = tempHtml.replace("lastSetTextBoxValue\":\"" + tempValue, "lastSetTextBoxValue\":\"" + tempCurrency + value);

                //row.get_cell(columnUniqueName).innerHTML = tempHtml;


                ////tempValue.replace(tempValue.slice(tempValue.indexOf('.')), "").replace(tempCurrency, "")
                ////validationText\":\"22\",\"valueAsString\":\"22\",\"minValue\":-70368744177664,\"maxValue\":70368744177664,\"lastSetTextBoxValue\":\"$22.00\"}' autocomplete=\"off\"></span>\n    
                ////var tempValue = row.get_cell(columnUniqueName).textContent;
                ////var startIndex = tempValue.indexOf(' ');
                ////tempValue = tempValue.slice(startIndex);
                ////tempValue = tempValue.insert(0, value);
                ////row.get_cell(columnUniqueName).textContent = tempValue;
            }

            //String.prototype.insert = function (index, string) {
            //    if (index > 0)
            //        return this.substring(0, index) + string + this.substring(index, this.length);
            //    else
            //        return string + this;
            //};

            // set value
            function SetExpenseValue() {
                var grid = $find("<%= RadGridExpenseDetail.ClientID %>"); //grid id

                if (grid) {
                    var masterTable = grid.get_masterTableView();
                    var rows = masterTable.get_dataItems();

                    var totalOffice = 0.0;
                    var totalLodging = 0.0;
                    var totalGround = 0.0;
                    var totalMeals = 0.0;
                    var totalAdvertising = 0.0;
                    var totalMail = 0.0;
                    var totalTelephone = 0.0;
                    var totalKm = 0.0;
                    var totalMiscellaneous = 0.0;

                    var totalMount1 = 0.0;
                    var totalMount2 = 0.0;
                    for (var i = 0; i < rows.length; i++) {

                        //var num = parseFloat(document.getElementById(amtid4).innerHTML, 10).toFixed(2);

                        var tempOffice = GetCellValue(rows[i], "Office", "radNumTextBoxOffice");
                        var tempLodging = GetCellValue(rows[i], "Lodging", "radNumTextBoxLodging");
                        var tempGround = GetCellValue(rows[i], "Ground", "radNumTextBoxGround");
                        var tempMeals = GetCellValue(rows[i], "Meals", "radNumTextBoxMeals");
                        var tempAdvertising = GetCellValue(rows[i], "Advertising", "radNumTextBoxAdvertising");
                        var tempMail = GetCellValue(rows[i], "Mail", "radNumTextBoxMail");
                        var tempTelephone = GetCellValue(rows[i], "Telephone", "radNumTextBoxTelephone");
                        var tempKm = GetCellValue(rows[i], "Km", "radNumTextBoxKm");
                        var tempMiscellaneous = GetCellValue(rows[i], "Miscellaneous", "radNumTextBoxMiscellaneous");

                        // cal km
                        var tempCal = tempKm * 0.35;
                        tempCal = parseFloat(tempCal, 10).toFixed(2);
                        SetCellValue(rows[i], "Kilometrage", tempCal);

                        // sum total
                        var tempTotal = tempOffice +
                            tempLodging +
                            tempGround +
                            tempMeals +
                            tempAdvertising +
                            tempMail +
                            tempTelephone +
                            parseFloat(tempCal) +
                            tempMiscellaneous;

                        SetCellValue(rows[i], "Cad", tempTotal);

                        totalOffice = totalOffice + parseFloat(tempOffice);
                        totalLodging = totalLodging + parseFloat(tempLodging);
                        totalGround = totalGround + parseFloat(tempGround);
                        totalMeals = totalMeals + parseFloat(tempMeals);
                        totalAdvertising = totalAdvertising + parseFloat(tempAdvertising);
                        totalMail = totalMail + parseFloat(tempMail);
                        totalTelephone = totalTelephone + parseFloat(tempTelephone);
                        totalKm = totalKm + parseFloat(tempKm);
                        totalMiscellaneous = totalMiscellaneous + parseFloat(tempMiscellaneous);

                        totalMount1 = totalMount1 + parseFloat(tempCal);
                        totalMount2 = totalMount2 + parseFloat(tempTotal);
                    }

                    sumOfficeInput.set_value(totalOffice);
                    sumLodgingInput.set_value(totalLodging);
                    sumGroundInput.set_value(totalGround);
                    sumMealsInput.set_value(totalMeals);
                    sumAdvertisingInput.set_value(totalAdvertising);
                    sumMailInput.set_value(totalMail);
                    sumTelephoneInput.set_value(totalTelephone);
                    sumKmInput.set_value(totalKm);
                    sumMiscellaneousInput.set_value(totalMiscellaneous);

                    sumKilometrageInput.set_value(totalMount1);
                    sumCadInput.set_value(totalMount2);
                    SetTotalValue();
                }
            }

            function SetTotalValue() {
                if (sumGrandTotalInput)
                    sumGrandTotalInput.set_value(sumCadInput.get_value() - sumCashAdvanceInput.get_value());
            }

            // when grid created
            function RadGridExpenseDetail_GridCreated(sender, eventArgs) {
                SetExpenseValue();
            }

            // action
            function RadGridExpenseDetail_Command(sender, eventArgs) {
                if (eventArgs.get_commandName() === "Rebind")
                    SetExpenseValue();
            }

            // delete
            function RadGridExpenseDetail_RowDeleted(sender, eventArgs) {
                SetExpenseValue();
            }

            // change value
            function RadGridExpenseDetail_ValueChanged(sender, eventArgs) {
                SetExpenseValue();
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
                var grid = $find("<%= RadGridExpenseDetail.ClientID %>"); //grid id
                grid.get_batchEditingManager().saveChanges(grid.get_masterTableView());
                // call flight_gridDestroying when grid Updated
            }

            function RadGridExpenseDetail_GridDestroying(sender, eventArgs) {
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
                var oWnd = window.radopen('ApprovalApprovePop?type=' + <%= (int)CConstValue.Approval.Expense %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalRejectWindow(id) {

                var oWnd = window.radopen('ApprovalRejectPop?type=' + <%= (int)CConstValue.Approval.Expense %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalReviseWindow(id) {
                var oWnd = window.radopen('ApprovalRevisePop?type=' + <%= (int)CConstValue.Approval.Expense %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }


            function ShowApprovalAcceptWindow(id) {
                var oWnd = window.radopen('ApprovalAcceptPop?type=' + <%= (int)CConstValue.Approval.Expense %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalCompleteWindow(id) {
                var oWnd = window.radopen('ApprovalCompletePop?type=' + <%= (int)CConstValue.Approval.Expense %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 300);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalCancelWindow(id) {
                var oWnd = window.radopen('ApprovalCancelPop?type=' + <%= (int)CConstValue.Approval.Expense %> + '&id=' + id, 0, 0, 0, 0);
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

            function ShowPrint() {
                $("#divPrint").printArea();
            }

        </script>

    </telerik:RadCodeBlock>
</asp:Content>
