<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DepositPaymentGrid.ascx.cs" Inherits="App_Data.DepositPaymentGrid" %>


<telerik:RadGrid ID="RadGridDepositPayment" runat="server" DataSourceID="LinqDataSourceDepositPayment" PageSize="20" Height="100%" AllowSorting="True" AllowFilteringByColumn="True"
    AutoGenerateColumns="false" AllowMultiRowSelection="true" ShowFooter="True" OnItemDataBound="RadGridDepositPayment_OnItemDataBound" OnFilterCheckListItemsRequested="RadGridDepositPayment_OnFilterCheckListItemsRequested"
    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
    <MasterTableView DataKeyNames="DepositPaymentId, PaymentId, InvoiceId, StudentId" DataSourceID="LinqDataSourceDepositPayment">
        <Columns>
            <telerik:GridBoundColumn
                HeaderText="Payment No" DataField="PaymentNumber" SortExpression="PaymentNumber" UniqueName="PaymentNumber" FooterText="Total"
                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
            </telerik:GridBoundColumn>
            <telerik:GridTemplateColumn
                HeaderText="Student No" DataField="StudentNo" SortExpression="StudentNo" UniqueName="StudentNo"
                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("StudentNo") %>' Width="100%" runat="server" />
                </ItemTemplate>
                <FooterTemplate>
                    <telerik:RadNumericTextBox runat="server" Width="100%" Type="Currency" ReadOnly="true" ReadOnlyStyle-HorizontalAlign="Right" ReadOnlyStyle-BorderStyle="None" HoveredStyle-HorizontalAlign="Right">
                        <ClientEvents OnLoad="TotalAmountLoad" />
                    </telerik:RadNumericTextBox>
                </FooterTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn
                HeaderText="Student Name" DataField="StudentName" SortExpression="StudentName" UniqueName="StudentName"
                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn
                HeaderText="Agency" DataField="AgencyName" SortExpression="AgencyName" UniqueName="AgencyName"
                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn
                HeaderText="Country" DataField="CountryName" SortExpression="CountryName" UniqueName="CountryName"
                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
            </telerik:GridBoundColumn>
            <telerik:GridDateTimeColumn
                HeaderText="Start Date" DataField="StartDate" SortExpression="StartDate" UniqueName="StartDate"
                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy}">
            </telerik:GridDateTimeColumn>
            <telerik:GridDateTimeColumn
                HeaderText="End Date" DataField="EndDate" SortExpression="EndDate" UniqueName="EndDate" FooterText="SubTotal"
                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy}">
            </telerik:GridDateTimeColumn>
            <telerik:GridTemplateColumn
                HeaderText="Paid Amount" DataField="PaidAmount" SortExpression="PaidAmount" UniqueName="PaidAmount"
                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="lblPaidAmount" Text='<%# string.Format("{0:$#,##0.00}", Eval("PaidAmount")) %>' Width="100%" runat="server" Style="text-align: right;" />
                </ItemTemplate>
                <FooterTemplate>
                    <telerik:RadNumericTextBox ID="TotalPaidAmount" runat="server" Width="100%" Type="Currency" ReadOnly="true" ReadOnlyStyle-HorizontalAlign="Right" ReadOnlyStyle-BorderStyle="None" HoveredStyle-HorizontalAlign="Right">
                        <ClientEvents OnLoad="PaidAmountLoad" />
                    </telerik:RadNumericTextBox>
                </FooterTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn
                HeaderText="Invoice No" DataField="InvoiceNumber" SortExpression="InvoiceNumber" UniqueName="InvoiceNumber"
                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn
                HeaderText="Invoice Type" DataField="InvoiceType" SortExpression="InvoiceType" UniqueName="InvoiceType"
                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
            </telerik:GridBoundColumn>
            <telerik:GridDateTimeColumn
                HeaderText="Payment Date" DataField="PaymentDate" SortExpression="PaymentDate" UniqueName="PaymentDate"
                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy} {0:t}">
            </telerik:GridDateTimeColumn>
            <telerik:GridBoundColumn
                HeaderText="Paid Method" DataField="PaidMethod" SortExpression="PaidMethod" UniqueName="PaidMethod"
                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
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
                    <telerik:RadNumericTextBox ID="TotalExtraAmount" runat="server" Width="100%" Type="Currency" ReadOnly="true" ReadOnlyStyle-HorizontalAlign="Right" ReadOnlyStyle-BorderStyle="None" HoveredStyle-HorizontalAlign="Right">
                        <ClientEvents OnLoad="ExtraAmountLoad" />
                    </telerik:RadNumericTextBox>
                </FooterTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridDateTimeColumn
                HeaderText="Extra Date" DataField="ExtraDate" SortExpression="ExtraDate" UniqueName="ExtraDate"
                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy}">
            </telerik:GridDateTimeColumn>
        </Columns>
    </MasterTableView>
    <ClientSettings EnableRowHoverStyle="True" AllowRowsDragDrop="True">
        <ClientEvents OnGridCreated="RadGridDepositPayment_GridCreated" />
        <Selecting AllowRowSelect="true" />
        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
        <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
            ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
        <ClientEvents OnRowDropping="RadGridDepositPayment_OnRowDropping"></ClientEvents>
    </ClientSettings>
</telerik:RadGrid>
<asp:LinqDataSource ID="LinqDataSourceDepositPayment" runat="server"
    ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName=""
    TableName="vwDepositPayments"
    Where="DepositId == @DepositId">
    <WhereParameters>
        <asp:Parameter DefaultValue="0" Name="DepositId" Type="Int32" />
    </WhereParameters>
</asp:LinqDataSource>

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        // call when page load.
        function pageLoad() {
            var grid = $find("<%= RadGridDepositPayment.ClientID %>");
            var columns = grid.get_masterTableView().get_columns();
            for (var i = 0; i < columns.length; i++) {
                columns[i].resizeToFit(false, true);
            }
        }

        // jQuery
        $(window).bind("load", function () {
            SetValue();
        });

        // =====================
        // total sum
        // =====================
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

        var sumTotalAmountInput = null;
        function TotalAmountLoad(sender, args) {
            sumTotalAmountInput = sender;
        }

        var sumPaidAmountInput = null;
        function PaidAmountLoad(sender, args) {
            sumPaidAmountInput = sender;
        }

        var sumExtraAmountInput = null;
        function ExtraAmountLoad(sender, args) {
            sumExtraAmountInput = sender;
        }

        function SetValue() {
            var grid = $find("<%= RadGridDepositPayment.ClientID %>"); //grid id

            if (grid) {
                var masterTable = grid.get_masterTableView();
                var rows = masterTable.get_dataItems();

                var totalMountPaidAmount = 0.0;
                var totalMountExtraAmount = 0.0;
                for (var i = 0; i < rows.length; i++) {
                    totalMountPaidAmount = totalMountPaidAmount + GetCellValue(rows[i], "PaidAmount", "lblPaidAmount");
                    totalMountExtraAmount = totalMountExtraAmount + GetCellValue(rows[i], "ExtraAmount", "lblExtraAmount");
                }
                sumPaidAmountInput.set_value(totalMountPaidAmount);
                sumExtraAmountInput.set_value(totalMountExtraAmount);

                sumTotalAmountInput.set_value(totalMountPaidAmount - totalMountExtraAmount);
            }
        }

        // when grid created
        function RadGridDepositPayment_GridCreated(sender, eventArgs) {
            SetValue();
        }

        // == end total sum ==

        function RadGridDepositPayment_OnRowDropping(sender, args) {
            if (!confirm('Do you want to remove it?'))
                args.set_cancel(true);
        }
    </script>
</telerik:RadCodeBlock>
