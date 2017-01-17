<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InvoiceItemGrid.ascx.cs" Inherits="App_Data.InvoiceItemGrid" %>

<div style="display: none">
    <asp:HiddenField runat="server" ID="HiddenFieldGridData" />
</div>

<telerik:RadGrid ID="RadGridInvoiceItems" runat="server" AllowAutomaticDeletes="true" AllowAutomaticInserts="true" AllowAutomaticUpdates="true" Height="100%" OnItemDataBound="RadGridInvoiceItems_ItemDataBound"
    PageSize="20" AllowPaging="false" AutoGenerateColumns="false" OnBatchEditCommand="RadGridInvoiceItems_BatchEditCommand" DataSourceID="sqlDataSourceInvoiceItems" ShowFooter="True" AllowFilteringByColumn="True" OnFilterCheckListItemsRequested="RadGridInvoiceItems_OnFilterCheckListItemsRequested"
    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
    <MasterTableView CommandItemDisplay="Top" DataKeyNames="InvoiceItemId" ClientDataKeyNames="InvoiceItemId" DataSourceID="sqlDataSourceInvoiceItems"
        HorizontalAlign="NotSet" EditMode="Batch" AutoGenerateColumns="false" CommandItemSettings-AddNewRecordText="Add Invoice Item" CommandItemSettings-SaveChangesText="Save Changes Invoice Item">
        <BatchEditingSettings EditType="Row" />
        <Columns>
            <telerik:GridTemplateColumn
                HeaderText="Invoice Item" DataField="InvoiceCoaItemId" SortExpression="InvoiceCoaItemId" UniqueName="InvoiceCoaItemId" DefaultInsertValue="Commission" HeaderStyle-HorizontalAlign="Center"
                AllowFiltering="False" FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" HeaderStyle-Width="30%">
                <ItemTemplate>
                    <telerik:RadDropDownList runat="server" ID="ddlInvoiceItems" DataValueField="InvoiceCoaItemId" DataTextField="ItemDetail" DataSourceID="sqlDataSourceInvoiceCoaItems" SelectedValue='<%# Eval("InvoiceCoaItemId") %>' Enabled="false" Width="100%" ToolTip='<%# Eval("InvoiceCoaItemId") %>' />
                </ItemTemplate>
                <EditItemTemplate>
                    <%--<telerik:RadDropDownList runat="server" ID="ddlInvoiceItem" DataValueField="InvoiceCoaItemId" DataTextField="ItemDetail" DataSourceID="sqlDataSourceInvoiceCoaItems" Width="100%" DropDownHeight="200px" />--%>
                    <telerik:RadComboBox ID="ddlInvoiceItem" Width="100%" runat="server" AutoPostBack="False" DataValueField="InvoiceCoaItemId" DataTextField="ItemDetail" DataSourceID="sqlDataSourceInvoiceCoaItems" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                </EditItemTemplate>
                <FooterTemplate>
                    Total
                </FooterTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn
                HeaderText="Standard Price" DataField="StandardPrice" SortExpression="StandardPrice" UniqueName="StandardPrice"
                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="lblStandardPrice" Text='<%# string.Format("{0:$#,##0.00}", Eval("StandardPrice")) %>' Width="100%" runat="server" Style="text-align: right;" />
                </ItemTemplate>
                <EditItemTemplate>
                    <span>
                        <telerik:RadNumericTextBox runat="server" ID="tbStandardPrice" ReadOnly="true" Type="Currency" Width="100%" ReadOnlyStyle-HorizontalAlign="Right" />
                    </span>
                </EditItemTemplate>
                <FooterTemplate>
                    <telerik:RadNumericTextBox ID="TotalStandard" runat="server" Type="Currency" ReadOnly="true" ReadOnlyStyle-HorizontalAlign="Right" ReadOnlyStyle-BorderStyle="None" HoveredStyle-HorizontalAlign="Right" Width="100%">
                        <ClientEvents OnLoad="StandardPriceLoad" />
                    </telerik:RadNumericTextBox>
                </FooterTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn
                HeaderText="Student Price" DataField="StudentPrice" SortExpression="StudentPrice" UniqueName="StudentPrice"
                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="lblStudentPrice" Text='<%# string.Format("{0:$#,##0.00}", Eval("StudentPrice")) %>' Width="100%" runat="server" Style="text-align: right;" />
                </ItemTemplate>
                <EditItemTemplate>
                    <span>
                        <telerik:RadNumericTextBox runat="server" ID="tbStudentPrice" Type="Currency" Width="100%" ReadOnlyStyle-HorizontalAlign="Right" />
                    </span>
                </EditItemTemplate>
                <FooterTemplate>
                    <telerik:RadNumericTextBox ID="TotalStudent" runat="server" Type="Currency" ReadOnly="true" ReadOnlyStyle-HorizontalAlign="Right" ReadOnlyStyle-BorderStyle="None" HoveredStyle-HorizontalAlign="Right" Width="100%">
                        <ClientEvents OnLoad="StudentPriceLoad" />
                    </telerik:RadNumericTextBox>
                </FooterTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn
                HeaderText="Agency Price" DataField="AgencyPrice" SortExpression="AgencyPrice" UniqueName="AgencyPrice"
                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="lblAgencyPrice" Text='<%# string.Format("{0:$#,##0.00}", Eval("AgencyPrice")) %>' Width="100%" runat="server" Style="text-align: right;" />
                </ItemTemplate>
                <EditItemTemplate>
                    <span>
                        <telerik:RadNumericTextBox runat="server" ID="tbAgencyPrice" Type="Currency" Width="100%" ReadOnlyStyle-HorizontalAlign="Right" />
                    </span>
                </EditItemTemplate>
                <FooterTemplate>
                    <telerik:RadNumericTextBox ID="TotalAgency" runat="server" Type="Currency" ReadOnly="true" ReadOnlyStyle-HorizontalAlign="Right" HoveredStyle-HorizontalAlign="Right" ReadOnlyStyle-BorderStyle="None" Width="100%">
                        <ClientEvents OnLoad="AgencyPriceLoad" />
                    </telerik:RadNumericTextBox>
                </FooterTemplate>
            </telerik:GridTemplateColumn>

            <telerik:GridTemplateColumn
                HeaderText="Remark" DataField="Remark" SortExpression="Remark" UniqueName="Remark"
                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblRemark" Text='<%# Eval("Remark") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <span>
                        <telerik:RadTextBox runat="server" ID="tbRemark"></telerik:RadTextBox>
                    </span>
                </EditItemTemplate>
            </telerik:GridTemplateColumn>

            <telerik:GridButtonColumn ConfirmText="Delete this Item?" ConfirmDialogType="RadWindow" ConfirmDialogHeight="150px"
                ConfirmTitle="Delete" HeaderText="Del" HeaderStyle-Width="5%" ButtonType="ImageButton"
                CommandName="Delete" Text="Delete" UniqueName="DeleteColumn">
            </telerik:GridButtonColumn>
        </Columns>
    </MasterTableView>
    <ClientSettings AllowKeyboardNavigation="true" EnableRowHoverStyle="True">
        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
        <ClientEvents OnCommand="RadGridInvoiceItems_Command" OnGridCreated="RadGridInvoiceItems_GridCreated" OnRowDeleted="RadGridInvoiceItems_RowDeleted" OnBatchEditCellValueChanged="RadGridInvoiceItems_BatchEditCellValueChanged" />
        <Resizing AllowColumnResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
            ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
    </ClientSettings>
</telerik:RadGrid>

<asp:LinqDataSource ID="sqlDataSourceInvoiceItems" runat="server" ContextTypeName="Erp2016.Lib.linqDBDataContext" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" TableName="InvoiceItems"
    Where="InvoiceItemId == @InvoiceItemId">
    <WhereParameters>
        <asp:Parameter DefaultValue="0" Name="InvoiceItemId" Type="Int32" />
    </WhereParameters>
</asp:LinqDataSource>

<asp:LinqDataSource ID="sqlDataSourceInvoiceCoaItems" runat="server" ContextTypeName="Erp2016.Lib.linqDBDataContext" EntityTypeName="" Select="new (InvoiceCoaItemId, ItemDetail)" TableName="InvoiceCoaItems"
    Where="IsOnlySystem == @IsOnlySystem">
    <WhereParameters>
        <asp:Parameter DefaultValue="False" Name="IsOnlySystem" Type="Boolean" />
    </WhereParameters>
</asp:LinqDataSource>


<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        // jQuery
        $(window).bind("load", function () {
            SetValueInvoice();
        });

        // =====================
        // total sum
        // =====================
        var sumStandardInput = null;

        function StandardPriceLoad(sender, args) {
            sumStandardInput = sender;
        }

        var sumStudentPriceInput = null;

        function StudentPriceLoad(sender, args) {
            sumStudentPriceInput = sender;
        }

        var sumAgencyPriceInput = null;

        function AgencyPriceLoad(sender, args) {
            sumAgencyPriceInput = sender;
        }

        function GetCellValueInvoice(row, columnUniqueName, controlId) {
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

        // set value
        function SetValueInvoice() {
            var grid = $find("<%= RadGridInvoiceItems.ClientID %>"); //grid id

            if (grid) {
                var masterTable = grid.get_masterTableView();
                var rows = masterTable.get_dataItems();

                var totalMountStandard = 0.0;
                var totalMountStudent = 0.0;
                var totalMountAgency = 0.0;
                for (var i = 0; i < rows.length; i++) {
                    var standardValue = GetCellValueInvoice(rows[i], "StandardPrice", "tbStandardPrice");
                    var studentValue = GetCellValueInvoice(rows[i], "StudentPrice", "tbStudentPrice");
                    var agencyValue = GetCellValueInvoice(rows[i], "AgencyPrice", "tbAgencyPrice");

                    totalMountStandard = totalMountStandard + standardValue;
                    totalMountStudent = totalMountStudent + studentValue;
                    totalMountAgency = totalMountAgency + agencyValue;

                    //CheckValue(masterTable.get_id(), i, "lblStandardPrice", standardValue);
                    //CheckValue(masterTable.get_id(), i, "lblStudentPrice", studentValue);
                    //CheckValue(masterTable.get_id(), i, "lblAgencyPrice", agencyValue);
                }

                sumStandardInput.set_value(totalMountStandard);
                sumStudentPriceInput.set_value(totalMountStudent);
                sumAgencyPriceInput.set_value(totalMountAgency);

            }

            GetGridData();
        }

        //function zeroFill(number, width) {
        //    width -= number.toString().length;
        //    if (width > 0) {
        //        return new Array(width + (/\./.test(number) ? 2 : 1)).join('0') + number;
        //    }
        //    return number + ""; // always return a string
        //}

        //function CheckValue(gridId, rowNum, controlId, value) {
        //    var id = gridId + "_ctl" + zeroFill((i + 4), 2) + "_" + controlId;
        //    var control = $('#' + id);
        //    if (control.length > 0) {
        //        control[0].attributes[1].value = control[0].attributes[1].value.replace(" color: orangered;", "");

        //        if (value < 0) {
        //            control[0].attributes[1].value = control[0].attributes[1].value.replace("width: 100%; text-align: right; ", "width: 100%; text-align: right; color: orangered;");
        //        }
        //    }
        //}

        // when grid created
        function RadGridInvoiceItems_GridCreated(sender, eventArgs) {
            SetValueInvoice();
        }

        // action
        function RadGridInvoiceItems_Command(sender, eventArgs) {
            if (eventArgs.get_commandName() == "Rebind")
                SetValueInvoice();
        }

        // delete
        function RadGridInvoiceItems_RowDeleted(sender, eventArgs) {
            SetValueInvoice();
        }

        function RadGridInvoiceItems_BatchEditCellValueChanged(sender, eventArgs) {
            SetValueInvoice();
        }

        // == end total sum ==

        function GetGridData() {
            var grid = $find("<%=RadGridInvoiceItems.ClientID %>");
            if (grid) {
                var masterTable = grid.get_masterTableView();
                var batchManager = grid.get_batchEditingManager();
                if (batchManager == null)
                    return;
                var rows = masterTable.get_dataItems();
                var mapValues = [];
                for (var i = 0; i < rows.length; i++) {
                    var mapCellInvoiceCoaId = rows[i].get_cell("InvoiceCoaItemId");
                    var mapCellStandardPrice = rows[i].get_cell("StandardPrice");
                    var mapCellStudentPrice = rows[i].get_cell("StudentPrice");
                    var mapCellAgencyPrice = rows[i].get_cell("AgencyPrice");
                    var mapCellRemark = rows[i].get_cell("Remark");

                    if (batchManager.getCellValue(mapCellInvoiceCoaId) == null)
                        return;

                    mapValues.push(batchManager.getCellValue(mapCellInvoiceCoaId));
                    mapValues.push(batchManager.getCellValue(mapCellStandardPrice).replace(",", ""));
                    mapValues.push(batchManager.getCellValue(mapCellStudentPrice).replace(",", ""));
                    mapValues.push(batchManager.getCellValue(mapCellAgencyPrice).replace(",", ""));
                    mapValues.push(batchManager.getCellValue(mapCellRemark).replace(",", ""));
                    mapValues.push("|");
                }
                document.getElementById('<%= HiddenFieldGridData.ClientID%>').value = mapValues.toString();
            }
        }

    </script>
</telerik:RadCodeBlock>
