<%@ Page Title="Payment" Language="C#" MasterPageFile="~/School/Shared/Base.master" AutoEventWireup="true" CodeFile="Payment.aspx.cs" Inherits="School.Sales.Payment" %>


<%@ Register TagPrefix="UserControl" TagName="InvoiceItemGrid" Src="~/App_Data/InvoiceItemGrid.ascx" %>
<%@ Register TagPrefix="UserControl" TagName="PaymentHistoryGrid" Src="~/App_Data/PaymentHistoryGrid.ascx" %>


<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadButtonExcel">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadButtonExcel" LoadingPanelID="RadButtonExcel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">

        <div style="display: none">
            <asp:Button ID="btnRefresh" runat="server" OnClick="Refresh" />
        </div>

        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="Radpane10" runat="server" Scrolling="None" Height="27px">
                <h4>Payment Plan List</h4>
            </telerik:RadPane>

            <telerik:RadPane ID="RadPane7" runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="RadToolBarPaymentTop" runat="server" OnButtonClick="RadToolBarPaymentTop_OnButtonClick" OnClientButtonClicked="ToolbarButtonClick">
                    <Items>
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_print.png" Text="Student Reciept" ToolTip="Student Reciept" Value="Student" Enabled="True" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_print.png" Text="Agency Reciept" ToolTip="Agency Reciept" Value="Agency" Enabled="True" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/Students-20.png" Text="Student Page" ToolTip="Student Page" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/Invoice-20.png" Text="Invoice Page" ToolTip="Invoice Page" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/Debt-20.png" Text="Deposit Page" ToolTip="Deposit Page" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/Sales Performance-20.png" Text="CreditMemo Page" ToolTip="CreditMemo Page" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/Receive Cash-20.png" Text="Refund Page" ToolTip="Refund Page" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_transfer.png" Text="Change status of Gross Based" ToolTip="Change status of Gross Based" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                        <telerik:RadToolBarButton runat="server" Text="Excel" Visible="false">
                            <ItemTemplate>
                                <telerik:RadButton runat="server" ID="RadButtonExcel" OnClick="RadButtonExcel_OnClick" Text="Export to excel" />
                            </ItemTemplate>
                        </telerik:RadToolBarButton>
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane1" runat="server" Height="40%" Scrolling="None">
                <telerik:RadGrid ID="RadGridPayment" runat="server" AllowFilteringByColumn="True" OnPreRender="RadGridPayment_OnPreRender" OnSelectedIndexChanged="RadGridPayment_OnSelectedIndexChanged"
                    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" Height="100%" PageSize="20" OnItemDataBound="RadGridPayment_OnItemDataBound"
                    DataSourceID="LinqDataSourcePayment" ShowFooter="false" OnFilterCheckListItemsRequested="RadGridPayment_OnFilterCheckListItemsRequested"
                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                    <MasterTableView DataKeyNames="InvoiceId, StudentId" TableLayout="Fixed" DataSourceID="LinqDataSourcePayment">
                        <Columns>
                            <telerik:GridBoundColumn
                                HeaderText="Invoice No" DataField="InvoiceNumber" SortExpression="InvoiceNumber" UniqueName="InvoiceNumber"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Site" DataField="SiteName" SortExpression="SiteName" UniqueName="SiteName"
                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Site Location" DataField="SiteLocationName" SortExpression="SiteLocationName" UniqueName="SiteLocationName"
                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Status" DataField="InvoiceStatus" SortExpression="InvoiceStatus" UniqueName="InvoiceStatus"
                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Invoice Type" DataField="InvoiceType" SortExpression="InvoiceType" UniqueName="InvoiceType"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn
                                HeaderText="Invoice Date" DataField="UpdatedDate" SortExpression="UpdatedDate" UniqueName="UpdatedDate"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy} {0:t}">
                            </telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Student No" DataField="StudentNo" SortExpression="StudentNo" UniqueName="StudentNo"
                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Student Name" DataField="StudentName" SortExpression="StudentName" UniqueName="StudentName"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Program Name" DataField="ProgramName" SortExpression="ProgramName" UniqueName="ProgramName"
                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn
                                HeaderText="Start Date" DataField="StartDate" SortExpression="StartDate" UniqueName="StartDate"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy}">
                            </telerik:GridDateTimeColumn>
                            <telerik:GridDateTimeColumn
                                HeaderText="End Date" DataField="EndDate" SortExpression="EndDate" UniqueName="EndDate"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy}">
                            </telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Agency" DataField="AgencyName" SortExpression="AgencyName" UniqueName="AgencyName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridNumericColumn
                                HeaderText="Student Net Amount" DataField="StudentPriceSum" SortExpression="StudentPriceSum" UniqueName="StudentPriceSum"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" DataFormatString="{0:$#,##0.00}" ItemStyle-HorizontalAlign="Right">
                            </telerik:GridNumericColumn>
                            <telerik:GridNumericColumn
                                HeaderText="Gross" DataField="Gross" SortExpression="Gross" UniqueName="Gross"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" DataFormatString="{0:$#,##0.00}" ItemStyle-HorizontalAlign="Right">
                            </telerik:GridNumericColumn>
                            <telerik:GridNumericColumn
                                HeaderText="CP" DataField="CP" SortExpression="CP" UniqueName="CP"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" DataFormatString="{0:$#,##0.00}" ItemStyle-HorizontalAlign="Right">
                            </telerik:GridNumericColumn>
                            <telerik:GridNumericColumn
                                HeaderText="MDF" DataField="MDF" SortExpression="MDF" UniqueName="MDF"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" DataFormatString="{0:$#,##0.00}" ItemStyle-HorizontalAlign="Right">
                            </telerik:GridNumericColumn>
                            <telerik:GridNumericColumn
                                HeaderText="Agency Net Amount" DataField="AgencyPriceSum" SortExpression="AgencyPriceSum" UniqueName="AgencyPriceSum"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" DataFormatString="{0:$#,##0.00}" ItemStyle-HorizontalAlign="Right">
                            </telerik:GridNumericColumn>
                            <telerik:GridNumericColumn
                                HeaderText="Paid Amount" DataField="PayAmount" SortExpression="PayAmount" UniqueName="PayAmount"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" DataFormatString="{0:$#,##0.00}" ItemStyle-HorizontalAlign="Right">
                            </telerik:GridNumericColumn>
                            <telerik:GridNumericColumn
                                HeaderText="OverPaid" DataField="OverPaid" SortExpression="OverPaid" UniqueName="OverPaid"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" DataFormatString="{0:$#,##0.00}" ItemStyle-HorizontalAlign="Right">
                            </telerik:GridNumericColumn>
                            <telerik:GridNumericColumn
                                HeaderText="Balance" DataField="Balance" SortExpression="Balance" UniqueName="Balance"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" DataFormatString="{0:$#,##0.00}" ItemStyle-HorizontalAlign="Right">
                            </telerik:GridNumericColumn>
                            <telerik:GridCheckBoxColumn
                                HeaderText="Gross Based" DataField="IsGross" SortExpression="IsGross" UniqueName="IsGross"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridCheckBoxColumn
                                HeaderText="FG" DataField="IsFinancialGurantee" SortExpression="IsFinancialGurantee" UniqueName="IsFinancialGurantee"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridCheckBoxColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="True">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                        <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                            ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                    </ClientSettings>
                    <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                </telerik:RadGrid>
                <asp:LinqDataSource ID="LinqDataSourcePayment" runat="server"
                    ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName="" OrderBy="InvoiceId Descending"
                    TableName="vwPayments"
                    Where="InvoiceId == @InvoiceId">
                    <WhereParameters>
                        <asp:Parameter DefaultValue="0" Name="InvoiceId" Type="Int32" />
                    </WhereParameters>
                </asp:LinqDataSource>
            </telerik:RadPane>

            <telerik:RadSplitBar ID="Radsplitbar1" runat="server" CollapseMode="Both" EnableResize="true" />

            <telerik:RadPane ID="Radpane2" runat="server" Scrolling="None">
                <telerik:RadSplitter ID="RadSplitterPayment" runat="server" Orientation="Vertical">
                    <telerik:RadPane ID="RadPanePayment" runat="server" Scrolling="None">
                        <telerik:RadSplitter ID="RadSplitter2" runat="server" Orientation="Horizontal">

                            <telerik:RadPane ID="RadPane5" runat="server" Height="40px" Scrolling="None">
                                <telerik:RadToolBar ID="RadToolBarPayment" runat="server" OnButtonClick="RadToolBarPayment_OnButtonClick" OnClientButtonClicking="ToolbarPaymentButtonClick">
                                    <Items>
                                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_save.png" Text="New Payment" ToolTip="New Payment" Value="New" />
                                        <telerik:RadToolBarButton IsSeparator="true" />
                                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_print.png" Text="Student Detail Reciept" ToolTip="Student Detail Reciept" Value="Student" Enabled="True" />
                                        <telerik:RadToolBarButton IsSeparator="true" />
                                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_print.png" Text="Agency Detail Reciept" ToolTip="Agency Detail Reciept" Value="Agency" Enabled="True" />
                                        <telerik:RadToolBarButton IsSeparator="true" />
                                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_back.png" Text="Payment Reverse" ToolTip="Payment Reverse" Value="Reverse" Enabled="false" />
                                        <telerik:RadToolBarButton IsSeparator="true" />
                                    </Items>
                                </telerik:RadToolBar>
                            </telerik:RadPane>

                            <telerik:RadPane ID="RadPane6" runat="server" Scrolling="None">
                                <UserControl:PaymentHistoryGrid ID="PaymentHistoryGrid1" runat="server" />
                            </telerik:RadPane>
                        </telerik:RadSplitter>
                    </telerik:RadPane>

                    <telerik:RadSplitBar ID="RadSplitBar2" runat="server" CollapseMode="Both" EnableResize="true" />

                    <telerik:RadPane ID="RadPane4" runat="server" Width="40%" Scrolling="None">

                        <telerik:RadSplitter runat="server" Orientation="Horizontal" ResizeMode="EndPane">
                            <telerik:RadPane ID="RadPane9" runat="server" Height="27px" Scrolling="None">
                                <h4>Invoice items</h4>
                            </telerik:RadPane>

                            <telerik:RadPane ID="RadPane8" runat="server" Scrolling="None">
                                <UserControl:InvoiceItemGrid ID="InvoiceItemGrid1" runat="server" />
                            </telerik:RadPane>
                        </telerik:RadSplitter>

                    </telerik:RadPane>

                </telerik:RadSplitter>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </telerik:RadAjaxPanel>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            // call when page load.
            function pageLoad() {
                var grid = $find("<%= RadGridPayment.ClientID %>");
                if (grid != null) {
                    var columns = grid.get_masterTableView().get_columns();
                    for (var i = 0; i < columns.length; i++) {
                        columns[i].resizeToFit(false, true);
                    }
                }

                grid = $find("<%= PaymentHistoryGrid1.GetRadGridPaymentHistory().ClientID %>");
                if (grid != null) {
                    var columns = grid.get_masterTableView().get_columns();
                    for (var i = 0; i < columns.length; i++) {
                        columns[i].resizeToFit(false, true);
                    }
                }
            }

            function ShowNewPaymentWindow(invoiceId) {
                var oWnd = window.radopen('PaymentNewPop?id=' + invoiceId, 0, 0, 0, 0);
                oWnd.setSize(700, 700);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Maximize + Telerik.Web.UI.WindowBehaviors.Resize + Telerik.Web.UI.WindowBehaviors.Move);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function OnClientClose(oWnd, args) {
                var arg = args.get_argument();

                <%= Page.GetPostBackEventReference(btnRefresh) %>;
            }

            function ShowReportPop(paymentIdArray, reportType) {
                var oWnd = window.radopen('ReportPop?id=' + paymentIdArray + '&reportType=' + reportType, 0, 0, 0, 0);
                var displayWidth = $(window).width() * 0.95;
                var displayHeight = $(window).height() * 0.95;

                if (displayWidth > 850)
                    displayWidth = 850;

                oWnd.setSize(displayWidth, displayHeight);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Maximize + Telerik.Web.UI.WindowBehaviors.Resize + Telerik.Web.UI.WindowBehaviors.Move);
                //oWnd.add_close(OnClientCloseHandler);
                return false;
            }

            function ToolbarPaymentButtonClick(sender, args) {
                var button = args.get_item();

                if (button.get_text() === "Payment Reverse") {
                    if (!confirm('Do you want to Reverse payment?'))
                        args.set_cancel(true);
                }
            }

            function ToolbarButtonClick(sender, args) {
                var button = args.get_item();
                if (button.get_text() === "Change status of Gross Based") {
                    if (!confirm('Do you want to change status of Gross Based?'))
                        args.set_cancel(true);
                }
            }

        </script>
    </telerik:RadCodeBlock>

</asp:Content>
