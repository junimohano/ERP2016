<%@ Page Title="New Payment" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="PaymentNewPop.aspx.cs" Inherits="School.Sales.PaymentNewPop" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">

        <asp:HiddenField ID="hfInvoiceId" runat="server" Value="" />

        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="RadpaneBasic" runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="mainToolBar" runat="server" OnButtonClick="ToolbarButtonClick" OnClientButtonClicking="ToolbarButtonClick">
                    <Items>
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_save.png" Text="Payment Confirm" ToolTip="Save" ValidationGroup="Info" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_delete.png" Text="Cancel" ToolTip="Cancel" Value="Cancel" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane1" runat="server">

                <div class="formStyle3">
                    <div style="float: left; width: 100%;">
                        <div>
                            <telerik:RadTextTile ID="ttInvoice" runat="server" Height="35px" Width="100%" Font-Size="Large" />
                            <br style="clear: both;" />
                        </div>
                        <fieldset>
                            <legend>Pay Select</legend>
                            <div style="float: left; width: 50%;">
                                <label>General Pay</label>
                                <telerik:RadButton GroupName="pay" ID="btCheckPayment" runat="server" Text="" ToggleType="Radio" ButtonType="ToggleButton" OnCheckedChanged="btCheckPayment_CheckedChanged" />
                                <br style="clear: both;" />
                            </div>
                            <div style="float: left; width: 50%;">
                                <label>Credit Pay</label>
                                <telerik:RadButton GroupName="pay" ID="btCheckCredit" runat="server" Text="" ToggleType="Radio" ButtonType="ToggleButton" OnCheckedChanged="btCheckCredit_CheckedChanged" />
                                <br style="clear: both;" />
                            </div>
                        </fieldset>

                        <fieldset>
                            <legend>Payment Date</legend>
                            <div>
                                <label><b style="color: red">*</b> Payment Date</label>
                                <telerik:RadDatePicker ID="tbPaymentDate" DateInput-DisplayDateFormat="MMM dd, yyyy" runat="server" CssClass="RadSizeMiddle" DateInput-EmptyMessage="Input Date" MinDate="01/01/2000" MaxDate="01/01/2100"></telerik:RadDatePicker>
                                <br style="clear: both;" />
                            </div>
                        </fieldset>

                        <fieldset>
                            <legend>General Payment</legend>
                            <div>
                                <label>Payment Method</label>
                                <telerik:RadComboBox ID="ddlPyamentMethod" CssClass="RadSizeMiddle" runat="server" />
                                <br style="clear: both;" />
                            </div>
                            <div>
                                <label>Payment Amount</label>
                                <telerik:RadNumericTextBox ID="tbPaymentAmount" Value="0" CssClass="RadSizeMiddle" runat="server" EmptyMessage="" Type="Currency" AutoPostBack="true" OnTextChanged="tbPaymentAmount_TextChanged" />
                                <br style="clear: both;" />
                            </div>
                            <div>
                                <label>Currency</label>
                                <telerik:RadComboBox ID="ddlCurrency" CssClass="RadSizeMiddle" runat="server" />
                                <br style="clear: both;" />
                            </div>
                        </fieldset>

                        <fieldset>
                            <legend>Credit Payment</legend>
                            <div>
                                <label>Credit No</label>
                                <telerik:RadComboBox runat="server" ID="ddlCreditMemo" MarkFirstMatch="true" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlCreditMemo_SelectedIndexChanged" ShowDropDownOnTextboxClick="true"
                                    OnItemDataBound="ddlCreditMemo_ItemDataBound" OnItemsRequested="ddlCreditMemo_ItemsRequested"
                                    DataSourceID="LinqDataSource1" DropDownWidth="800px" Height="200px">
                                    <HeaderTemplate>
                                        <table style="width: 700px">
                                            <tr>
                                                <td style="width: 150px;">Credit Number
                                                </td>
                                                <td style="width: 100px;">CreditMemo Type
                                                </td>
                                                <td style="width: 150px;">Student Number
                                                </td>
                                                <td style="width: 200px;">Agency
                                                </td>
                                                <td style="width: 150px;">Available Amount
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table style="width: 700px">
                                            <tr>
                                                <td style="width: 150px;">
                                                    <%# DataBinder.Eval(Container.DataItem, "CreditMemoNumber") %>
                                                </td>
                                                <td style="width: 100px;">
                                                    <%# DataBinder.Eval(Container.DataItem, "CreditMemoType") %>
                                                </td>
                                                <td style="width: 150px;">
                                                    <%# DataBinder.Eval(Container.DataItem, "StudentMasterNo") %>
                                                </td>
                                                <td style="width: 200px;">
                                                    <%# DataBinder.Eval(Container.DataItem, "AgencyName") %>
                                                </td>
                                                <td style="width: 150px;">
                                                    <%# DataBinder.Eval(Container.DataItem, "AvailableCreditAmount", "{0:C}") %>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </telerik:RadComboBox>
                                <br style="clear: both;" />
                            </div>

                            <div>
                                <label>Credit Amount</label>
                                <telerik:RadNumericTextBox ID="tbCreditAmount" Value="0" CssClass="RadSizeMiddle" runat="server" EmptyMessage="" Type="Currency" AutoPostBack="true" OnTextChanged="tbCreditAmount_TextChanged"></telerik:RadNumericTextBox>
                            </div>
                        </fieldset>
                    </div>

                    <fieldset>
                        <legend>Total</legend>
                        <div>
                            <label>Total Pay Amount</label>
                            <telerik:RadNumericTextBox ID="tbTotalPayAmount" Value="0" CssClass="RadSizeMiddle" runat="server" EmptyMessage="" Type="Currency" AutoPostBack="true" ReadOnly="true"></telerik:RadNumericTextBox>
                        </div>
                    </fieldset>

                    <fieldset>
                        <legend>Etc</legend>
                        <div>
                            <label>Remark</label>
                            <telerik:RadTextBox ID="tbRemark" TextMode="MultiLine" CssClass="RadSizeMultiLine" Text="" runat="server"></telerik:RadTextBox>
                            <br style="clear: both;" />
                        </div>
                    </fieldset>
                </div>

                <asp:LinqDataSource ID="LinqDataSource1" runat="server"
                    ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName=""
                    TableName="vwCreditMemos"
                    Where="StudentId == @StudentId">
                    <WhereParameters>
                        <asp:Parameter DefaultValue="0" Name="StudentId" Type="Int32" />
                    </WhereParameters>
                </asp:LinqDataSource>
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
                if (button.get_text() == "Payment Confirm") {
                    if (!confirm('Do you want to confirm it?'))
                        args.set_cancel(true);
                }
            }

        </script>
    </telerik:RadCodeBlock>
</asp:Content>
