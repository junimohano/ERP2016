<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PaymentHistoryGrid.ascx.cs" Inherits="App_Data.PaymentHistoryGrid" %>


<telerik:RadGrid ID="RadGridPaymentHistory" runat="server"
    AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False" Height="100%" PageSize="20" AllowFilteringByColumn="True" AllowMultiRowSelection="true"
    DataSourceID="LinqDataSourcePaymentHistory" ShowFooter="True" OnItemDataBound="RadGridPaymentHistory_OnItemDataBound" OnFilterCheckListItemsRequested="RadGridPaymentHistory_OnFilterCheckListItemsRequested"
    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
    <MasterTableView DataKeyNames="PaymentId" TableLayout="Fixed" DataSourceID="LinqDataSourcePaymentHistory">
        <Columns>
            <telerik:GridBoundColumn
                HeaderText="Payment No" DataField="PaymentNumber" SortExpression="PaymentNumber" UniqueName="PaymentNumber" FooterText="Total"
                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
            </telerik:GridBoundColumn>
            <telerik:GridTemplateColumn
                HeaderText="Payment Date" DataField="PaymentDate" SortExpression="PaymentDate" UniqueName="PaymentDate"
                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label Text='<%# string.Format("{0:MM-dd-yyyy}", DataBinder.Eval(Container.DataItem, "PaymentDate")) %>' Width="100%" runat="server" />
                </ItemTemplate>
                <FooterTemplate>
                    <telerik:RadNumericTextBox runat="server" Width="100%" Type="Currency" ReadOnly="true" ReadOnlyStyle-HorizontalAlign="Right" ReadOnlyStyle-BorderStyle="None" HoveredStyle-HorizontalAlign="Right">
                        <ClientEvents OnLoad="TotalPayAmountLoad" />
                    </telerik:RadNumericTextBox>
                </FooterTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn
                HeaderText="Paid Method" DataField="PaidMethod" SortExpression="PaidMethod" UniqueName="PaidMethod" FooterText="SubTotal"
                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
            </telerik:GridBoundColumn>
            <telerik:GridTemplateColumn
                HeaderText="Pay Amount" DataField="PayAmount" SortExpression="PayAmount" UniqueName="PayAmount"
                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="lblPayAmount" Text='<%# string.Format("{0:$#,##0.00}", Eval("PayAmount")) %>' Width="100%" runat="server" Style="text-align: right;" />
                </ItemTemplate>
                <FooterTemplate>
                    <telerik:RadNumericTextBox runat="server" Width="100%" Type="Currency" ReadOnly="true" ReadOnlyStyle-HorizontalAlign="Right" ReadOnlyStyle-BorderStyle="None" HoveredStyle-HorizontalAlign="Right">
                        <ClientEvents OnLoad="PayAmountLoad" />
                    </telerik:RadNumericTextBox>
                </FooterTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn
                HeaderText="CreditMemo No" DataField="CreditMemoNumber" SortExpression="CreditMemoNumber" UniqueName="CreditMemoNumber"
                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn
                HeaderText="Remark" DataField="Remark" SortExpression="Remark" UniqueName="Remark"
                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn
                HeaderText="Deposit No" DataField="DepositNumber" SortExpression="DepositNumber" UniqueName="DepositNumber"
                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn
                HeaderText="Deposit Status" DataField="DepositStatus" SortExpression="DepositStatus" UniqueName="DepositStatus"
                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" HeaderStyle-Font-Size="X-Small">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn
                HeaderText="Extra Type" DataField="ExtraTypeName" SortExpression="ExtraTypeName" UniqueName="ExtraTypeName" FooterText="SubTotal"
                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
            </telerik:GridBoundColumn>
            <telerik:GridTemplateColumn
                HeaderText="Extra Amount" DataField="ExtraAmount" SortExpression="ExtraAmount" UniqueName="ExtraAmount"
                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="lblExtraAmount" Text='<%# string.Format("{0:$#,##0.00}", Eval("ExtraAmount")) %>' Width="100%" runat="server" Style="text-align: right;" />
                </ItemTemplate>
                <FooterTemplate>
                    <telerik:RadNumericTextBox runat="server" Width="100%" Type="Currency" ReadOnly="true" ReadOnlyStyle-HorizontalAlign="Right" ReadOnlyStyle-BorderStyle="None" HoveredStyle-HorizontalAlign="Right">
                        <ClientEvents OnLoad="ExtraAmountLoad" />
                    </telerik:RadNumericTextBox>
                </FooterTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridDateTimeColumn
                HeaderText="Extra Date" DataField="ExtraDate" SortExpression="ExtraDate" UniqueName="ExtraDate"
                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy}">
            </telerik:GridDateTimeColumn>
            <telerik:GridBoundColumn
                HeaderText="User Name" DataField="UserName" SortExpression="UserName" UniqueName="UserName"
                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
            </telerik:GridBoundColumn>
            <telerik:GridDateTimeColumn
                HeaderText="Created Date" DataField="CreatedDate" SortExpression="CreatedDate" UniqueName="CreatedDate"
                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy} {0:t}">
            </telerik:GridDateTimeColumn>
        </Columns>
    </MasterTableView>
    <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
        <ClientEvents OnGridCreated="RadGridPayment_GridCreated" />
        <Selecting AllowRowSelect="true" />
        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true" />
        <Resizing AllowColumnResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
            ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
    </ClientSettings>
</telerik:RadGrid>
<asp:LinqDataSource ID="LinqDataSourcePaymentHistory" runat="server"
    ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName=""
    TableName="vwPaymentHistories"
    Where="InvoiceId == @InvoiceId">
    <WhereParameters>
        <asp:Parameter DefaultValue="0" Name="InvoiceId" Type="Int32" />
    </WhereParameters>
</asp:LinqDataSource>

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        // jQuery
        $(window).bind("load", function () {
            SetValue();
        });
        var sumTotalPayAmountInput = null;
        function TotalPayAmountLoad(sender, args) {
            sumTotalPayAmountInput = sender;
        }

        var sumPayAmountInput = null;
        function PayAmountLoad(sender, args) {
            sumPayAmountInput = sender;
        }

        var sumExtraAmountInput = null;
        function ExtraAmountLoad(sender, args) {
            sumExtraAmountInput = sender;
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

        function SetValue() {
            var grid = $find("<%= RadGridPaymentHistory.ClientID %>"); //grid id

            if (grid) {
                var masterTable = grid.get_masterTableView();
                var rows = masterTable.get_dataItems();

                var totalMountPayAmount = 0.0;
                var totalMountExtraAmount = 0.0;
                for (var i = 0; i < rows.length; i++) {
                    totalMountPayAmount = totalMountPayAmount + GetCellValue(rows[i], "PayAmount", "lblPayAmount");
                    totalMountExtraAmount = totalMountExtraAmount + GetCellValue(rows[i], "ExtraAmount", "lblExtraAmount");
                }
                sumPayAmountInput.set_value(totalMountPayAmount);
                sumExtraAmountInput.set_value(totalMountExtraAmount);
                sumTotalPayAmountInput.set_value(totalMountPayAmount + totalMountExtraAmount);
            }
        }

        // when grid created
        function RadGridPayment_GridCreated(sender, eventArgs) {
            SetValue();
        }

        // == end total sum ==
    </script>
</telerik:RadCodeBlock>
