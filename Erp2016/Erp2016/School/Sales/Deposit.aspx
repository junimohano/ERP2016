<%@ Page Title="Deposit" Language="C#" MasterPageFile="~/School/Shared/Base.master" AutoEventWireup="true" CodeFile="Deposit.aspx.cs" Inherits="School.Sales.Deposit" %>

<%@ Register TagPrefix="UserControl" TagName="FileDownloadList" Src="~/App_Data/FileDownloadList.ascx" %>
<%@ Register TagPrefix="UserControl" TagName="DepositInfomation" Src="~/App_Data/DepositInfomation.ascx" %>
<%@ Register TagPrefix="UserControl" TagName="DepositPaymentGrid" Src="~/App_Data/DepositPaymentGrid.ascx" %>

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
            <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" />
            <asp:Button ID="btnDepositPaymentRefresh" runat="server" OnClick="btnDepositPaymentRefresh_Click" />
        </div>

        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="Radpane10" runat="server" Height="27px" Scrolling="None">
                <h4>Deposit List</h4>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane14" runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="DepositInfoToolbar" runat="server" OnButtonClick="DepositInfoToolbar_ButtonClick" OnClientButtonClicking="ToolbarButtonClick">
                    <Items>
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_new.png" Text="Add Deposit" Enabled="True" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/icon_s_edit.png" Text="Modify Deposit" Enabled="False" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_save.png" Text="Confirm" Enabled="False" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_delete.png" Text="Cancel" Enabled="False" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                        <telerik:RadToolBarButton runat="server" Text="Excel" Visible="false">
                            <ItemTemplate>
                                <telerik:RadButton runat="server" ID="RadButtonExcel" OnClick="RadButtonExcel_OnClick" Text="Export to excel" />
                            </ItemTemplate>
                        </telerik:RadToolBarButton>
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane1" runat="server" Height="30%" Scrolling="None">

                <telerik:RadGrid ID="RadGridDepositList" runat="server" AllowFilteringByColumn="True"
                    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                    OnPreRender="RadGridDepositList_PreRender" OnSelectedIndexChanged="RadGridDepositList_OnSelectedIndexChanged"
                    OnItemDataBound="RadGridDepositList_ItemDataBound" Height="100%" PageSize="20" DataSourceID="LinqDataSource1" ShowFooter="False" OnFilterCheckListItemsRequested="RadGridDepositList_OnFilterCheckListItemsRequested"
                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                    <GroupingSettings CaseSensitive="false"></GroupingSettings>
                    <MasterTableView DataKeyNames="DepositId" TableLayout="Fixed" DataSourceID="LinqDataSource1">
                        <Columns>
                            <telerik:GridBoundColumn
                                HeaderText="Deposit No" DataField="DepositNumber" SortExpression="DepositNumber" UniqueName="DepositNumber"
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
                                HeaderText="Status" DataField="DepositStatus" SortExpression="DepositStatus" UniqueName="DepositStatus"
                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn
                                HeaderText="Deposit Date" DataField="DepositDate" SortExpression="DepositDate" UniqueName="DepositDate"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy}">
                            </telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Bank" DataField="DepositBank" SortExpression="DepositBank" UniqueName="DepositBank"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridNumericColumn
                                HeaderText="Deposited Amount" DataField="DepositAmount" SortExpression="DepositAmount" UniqueName="DepositAmount"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" DataFormatString="{0:$#,##0.00}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                            </telerik:GridNumericColumn>
                            <telerik:GridBoundColumn
                                HeaderText="# of Reciept(s)" DataField="PaymentCount" SortExpression="PaymentCount" UniqueName="PaymentCount"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn
                                HeaderText="HQConfirm Date" DataField="HQConfirmDate" SortExpression="HQConfirmDate" UniqueName="HQConfirmDate"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy} {0:t}">
                            </telerik:GridDateTimeColumn>
                            <%--           <telerik:GridBoundColumn
                                HeaderText="Comment" DataField="Comment" SortExpression="Comment" UniqueName="Comment"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn
                                HeaderText="Created User" DataField="Username" SortExpression="Username" UniqueName="Username"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn
                                HeaderText="Created Date" DataField="CreatedDate" SortExpression="CreatedDate" UniqueName="CreatedDate"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy} {0:t}">
                            </telerik:GridDateTimeColumn>
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
                <asp:LinqDataSource ID="LinqDataSource1" runat="server"
                    ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName="" OrderBy="DepositId Descending"
                    TableName="vwDepositLists"
                    Where="DepositId == @DepositId">
                    <WhereParameters>
                        <asp:Parameter DefaultValue="0" Name="DepositId" Type="Int32" />
                    </WhereParameters>
                </asp:LinqDataSource>
            </telerik:RadPane>

            <telerik:RadSplitBar ID="Radsplitbar1" runat="server" CollapseMode="Both" EnableResize="true" />

            <telerik:RadPane ID="Radpane18" runat="server" Scrolling="None">
                <telerik:RadSplitter ID="Radsplitter7" runat="server" Orientation="Horizontal">

                    <telerik:RadPane ID="Radpane4" runat="server" Scrolling="None">
                        <telerik:RadSplitter ID="Radsplitter4" runat="server" Orientation="Horizontal">

                            <telerik:RadPane ID="Radpane7" runat="server" Scrolling="None">
                                <telerik:RadSplitter ID="Radsplitter5" runat="server" Orientation="Vertical">

                                    <telerik:RadPane ID="Radpane2" runat="server" Scrolling="None">
                                        <telerik:RadSplitter ID="Radsplitter2" runat="server" Orientation="Vertical">

                                            <telerik:RadPane ID="Radpane6" runat="server" Scrolling="None">
                                                <telerik:RadSplitter ID="Radsplitter8" runat="server" Orientation="Horizontal">

                                                    <telerik:RadPane ID="Radpane12" runat="server" Height="40px" Scrolling="None">
                                                        <telerik:RadToolBar ID="ApprovedPaymentsToolBar" runat="server" OnButtonClick="ApprovedPaymentsToolBar_ButtonClick">
                                                            <Items>
                                                                <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_search.png" Text="View Invoice" ToolTip="View Invoice" Value="View Invoice" />
                                                                <telerik:RadToolBarButton IsSeparator="true" />
                                                                <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_search.png" Text="View Payment" ToolTip="View Payment" Value="View Payment" />
                                                                <telerik:RadToolBarButton IsSeparator="true" />
                                                                <telerik:RadToolBarButton ImageUrl="../../assets/img/icon_s_edit.png" Text="Add Extra Payment" Value="Add Extra Payment" ToolTip="Add Extra Payment" />
                                                                <telerik:RadToolBarButton IsSeparator="true" />
                                                                <telerik:RadToolBarButton ImageUrl="../../assets/img/Students-20.png" Text="Student Page" ToolTip="Student Page" />
                                                                <telerik:RadToolBarButton ImageUrl="../../assets/img/Invoice-20.png" Text="Invoice Page" ToolTip="Invoice Page" />
                                                                <telerik:RadToolBarButton ImageUrl="../../assets/img/Paid-20.png" Text="Payment Page" ToolTip="Payment Page" />
                                                                <telerik:RadToolBarButton ImageUrl="../../assets/img/Sales Performance-20.png" Text="CreditMemo Page" ToolTip="CreditMemo Page" />
                                                                <telerik:RadToolBarButton ImageUrl="../../assets/img/Receive Cash-20.png" Text="Refund Page" ToolTip="Refund Page" />
                                                                <telerik:RadToolBarButton IsSeparator="true" />
                                                            </Items>
                                                        </telerik:RadToolBar>
                                                    </telerik:RadPane>

                                                    <telerik:RadPane ID="Radpane13" runat="server" Scrolling="None">
                                                        <UserControl:DepositPaymentGrid ID="DepositPaymentGrid1" runat="server" />
                                                    </telerik:RadPane>

                                                </telerik:RadSplitter>

                                            </telerik:RadPane>

                                        </telerik:RadSplitter>
                                    </telerik:RadPane>

                                    <telerik:RadSplitBar ID="Radsplitbar4" runat="server" CollapseMode="Both" EnableResize="true" />

                                    <telerik:RadPane ID="Radpane3" runat="server" Width="30%" Scrolling="None">
                                        <telerik:RadSplitter ID="Radsplitter3" runat="server" Orientation="Horizontal">
                                            <telerik:RadPane ID="Radpane5" runat="server" Height="150px">
                                                <UserControl:DepositInfomation ID="DepositInfomation1" runat="server" />
                                            </telerik:RadPane>

                                            <telerik:RadSplitBar ID="Radsplitbar3" runat="server" CollapseMode="None" EnableResize="true" />

                                            <telerik:RadPane ID="Radpane11" runat="server" Height="115px">
                                                <UserControl:FileDownloadList ID="FileDownloadList1" runat="server" />
                                            </telerik:RadPane>

                                        </telerik:RadSplitter>
                                    </telerik:RadPane>

                                </telerik:RadSplitter>
                            </telerik:RadPane>

                            <telerik:RadSplitBar ID="Radsplitbar2" runat="server" CollapseMode="None" EnableResize="true" />

                            <telerik:RadPane ID="Radpane8" runat="server" Scrolling="None">

                                <telerik:RadSplitter ID="Radsplitter6" runat="server" Orientation="Horizontal">

                                    <telerik:RadPane ID="Radpane15" runat="server" Height="27px" Scrolling="None">
                                        <h4>Unassigned Payments</h4>
                                    </telerik:RadPane>

                                    <telerik:RadPane ID="Radpane16" runat="server" Height="40px" Scrolling="None">
                                        <telerik:RadToolBar ID="UnApprovedPaymentsToolBar" runat="server" OnButtonClick="UnApprovedPaymentsToolBar_ButtonClick">
                                            <Items>
                                                <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_search.png" Text="View Invoice" ToolTip="View Invoice" Value="View Invoice" />
                                                <telerik:RadToolBarButton IsSeparator="true" />
                                                <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_search.png" Text="View Payment" ToolTip="View Payment" Value="View Payment" />
                                                <telerik:RadToolBarButton IsSeparator="true" />
                                            </Items>
                                        </telerik:RadToolBar>
                                    </telerik:RadPane>

                                    <telerik:RadPane ID="Radpane17" runat="server" Scrolling="None">

                                        <telerik:RadGrid ID="RadGridUnDepositPayment" runat="server" DataSourceID="LinqDataSourceUnDepositPayment" PageSize="20" Height="100%" OnItemDataBound="RadGridUnDepositPayment_OnItemDataBound" AllowFilteringByColumn="True"
                                            AutoGenerateColumns="false" AllowMultiRowSelection="true" OnRowDrop="RadGridUnDepositPayment_OnRowDrop" OnFilterCheckListItemsRequested="RadGridUnDepositPayment_OnFilterCheckListItemsRequested"
                                            FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                                            <MasterTableView DataKeyNames="PaymentId, InvoiceId" DataSourceID="LinqDataSourceUnDepositPayment">
                                                <Columns>
                                                    <telerik:GridBoundColumn
                                                        HeaderText="Payment No" DataField="PaymentNumber" SortExpression="PaymentNumber" UniqueName="PaymentNumber"
                                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn
                                                        HeaderText="Student No" DataField="StudentNo" SortExpression="StudentNo" UniqueName="StudentNo"
                                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn
                                                        HeaderText="Student Name" DataField="StudentName" SortExpression="StudentName" UniqueName="StudentName"
                                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn
                                                        HeaderText="Agency Name" DataField="AgencyName" SortExpression="AgencyName" UniqueName="AgencyName"
                                                        FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridNumericColumn
                                                        HeaderText="Paid Amount" DataField="PaidAmount" SortExpression="PaidAmount" UniqueName="PaidAmount"
                                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" DataFormatString="{0:$#,##0.00}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                    </telerik:GridNumericColumn>
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
                                                    <telerik:GridNumericColumn
                                                        HeaderText="Student Amount" DataField="StudentPriceSum" SortExpression="StudentPriceSum" UniqueName="StudentPriceSum"
                                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" DataFormatString="{0:$#,##0.00}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                    </telerik:GridNumericColumn>
                                                    <telerik:GridNumericColumn
                                                        HeaderText="Agency Amount" DataField="AgencyPriceSum" SortExpression="AgencyPriceSum" UniqueName="AgencyPriceSum"
                                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" DataFormatString="{0:$#,##0.00}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                    </telerik:GridNumericColumn>
                                                </Columns>
                                            </MasterTableView>
                                            <ClientSettings EnableRowHoverStyle="True" AllowRowsDragDrop="True">
                                                <Selecting AllowRowSelect="true" />
                                                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                                                <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                                                    ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                                                <ClientEvents OnRowDropping="RadGridUnDepositPayment_OnRowDropping"></ClientEvents>
                                            </ClientSettings>
                                            <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                                        </telerik:RadGrid>
                                        <asp:LinqDataSource ID="LinqDataSourceUnDepositPayment" runat="server"
                                            ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName=""
                                            TableName="vwPreDepositPayments"
                                            Where="SiteLocationId == @SiteLocationId">
                                            <WhereParameters>
                                                <asp:Parameter DefaultValue="0" Name="SiteLocationId" Type="Int32" />
                                            </WhereParameters>
                                        </asp:LinqDataSource>
                                    </telerik:RadPane>

                                </telerik:RadSplitter>
                            </telerik:RadPane>

                        </telerik:RadSplitter>
                    </telerik:RadPane>

                </telerik:RadSplitter>
            </telerik:RadPane>

        </telerik:RadSplitter>
    </telerik:RadAjaxPanel>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function OnClientClose(oWnd, args) {
                var arg = args.get_argument();
                <%= Page.GetPostBackEventReference(btnRefresh) %>;
            }

            function ToolbarButtonClick(sender, args) {
                var button = args.get_item();
                if (button.get_text() === "Confirm") {
                    if (!confirm('Do you want to confirm it?'))
                        args.set_cancel(true);
                } else if (button.get_text() === "Cancel") {
                    if (!confirm('Do you want to cancel it?'))
                        args.set_cancel(true);
                }
            }

            function ShowPop(depositId, type) {
                var oWnd = window.radopen('DepositPop?id=' + depositId + '&type=' + type, 0, 0, 0, 0);
                oWnd.setSize(700, 500);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move + Telerik.Web.UI.WindowBehaviors.Maximize);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowAddeExtraPaymentWindow(paymentId) {
                var oWnd = window.radopen('DepositAddExtraPaymentPop?id=' + paymentId, 0, 0, 0, 0);
                oWnd.setSize(500, 300);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move + Telerik.Web.UI.WindowBehaviors.Maximize);
                oWnd.add_close(OnClientAddExtraPaymentClose);
                return false;
            }

            function OnClientAddExtraPaymentClose(oWnd, args) {
                var arg = args.get_argument();
                <%= Page.GetPostBackEventReference(btnDepositPaymentRefresh) %>;
            }

            function ToolbarButtonClick(sender, args) {
                var button = args.get_item();
                if (button.get_text() === "Confirm") {
                    if (!confirm('Do you want to confirm it?'))
                        args.set_cancel(true);
                } else if (button.get_text() === "Cancel") {
                    if (!confirm('Do you want to cancel it?'))
                        args.set_cancel(true);
                }
            }

            function RadGridUnDepositPayment_OnRowDropping(sender, args) {
                if (!confirm('Do you want to add it?'))
                    args.set_cancel(true);
            }

            function ShowInvoiceWindow(invoiceId) {
                var oWnd = window.radopen('InvoiceItemGridPop?id=' + invoiceId, 0, 0, 0, 0);
                //var displayWidth = $(window).width() * 0.95;
                //var displayHeight = $(window).height() * 0.95;

                var displayWidth = 800;
                var displayHeight = 500;

                if (displayWidth > 1500)
                    displayWidth = 1500;
                oWnd.setSize(displayWidth, displayHeight);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Maximize + Telerik.Web.UI.WindowBehaviors.Resize + Telerik.Web.UI.WindowBehaviors.Move);
                //oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowPaymentHistoryWindow(invoiceId) {
                var oWnd = window.radopen('PaymentHistoryGridPop?id=' + invoiceId, 0, 0, 0, 0);
                //var displayWidth = $(window).width() * 0.95;
                //var displayHeight = $(window).height() * 0.95;

                var displayWidth = 1000;
                var displayHeight = 500;

                if (displayWidth > 1500)
                    displayWidth = 1500;
                oWnd.setSize(displayWidth, displayHeight);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Maximize + Telerik.Web.UI.WindowBehaviors.Resize + Telerik.Web.UI.WindowBehaviors.Move);
                //oWnd.add_close(OnClientClose);
                return false;
            }

        </script>
    </telerik:RadCodeBlock>

</asp:Content>
