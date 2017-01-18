<%@ Page Title="Refund" Language="C#" MasterPageFile="~/School/Shared/Base.master" AutoEventWireup="true" CodeFile="Refund.aspx.cs" Inherits="School.Registrar.Refund" %>

<%@ Import Namespace="Erp2016.Lib" %>

<%@ Register TagPrefix="UserControl" TagName="CreditMemoPayout" Src="~/App_Data/CreditMemoPayout.ascx" %>
<%@ Register TagPrefix="UserControl" TagName="ApprovalLine" Src="~/App_Data/ApprovalLine.ascx" %>
<%@ Register TagPrefix="UserControl" TagName="InvoiceItemGrid" Src="~/App_Data/InvoiceItemGrid.ascx" %>
<%@ Register TagPrefix="UserControl" TagName="FileDownloadList" Src="~/App_Data/FileDownloadList.ascx" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">

        <div style="display: none">
            <asp:Button ID="btnRefresh" runat="server" OnClick="Refresh" />
        </div>

        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="Radpane14" runat="server" Height="27px" Scrolling="None">
                <h4>Refund list</h4>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane7" runat="server" Height="40px" Scrolling="X">
                <telerik:RadToolBar ID="RadToolBarApproval" runat="server" OnButtonClick="RadToolBarApproval_OnButtonClick" OnClientButtonClicking="RadToolBarRefundToolbarButtonClick">
                    <Items>
                        <telerik:RadToolBarButton runat="server" Text="View">
                            <ItemTemplate>
                                <telerik:RadDropDownList runat="server" ID="RadDropDownListView" AutoPostBack="True" OnSelectedIndexChanged="RadDropDownListView_OnSelectedIndexChanged">
                                    <Items>
                                        <telerik:DropDownListItem Text="All" Selected="True" />
                                        <telerik:DropDownListItem Text="My Request" />
                                        <telerik:DropDownListItem Text="My Approval" />
                                    </Items>
                                </telerik:RadDropDownList>
                            </ItemTemplate>
                        </telerik:RadToolBarButton>
                        <telerik:RadToolBarButton IsSeparator="true" />
                        <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_mark.png" Text="Request" Enabled="False" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_delete.png" Text="Cancel" Enabled="False" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_excute.png" Text="Approve" Enabled="False" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_clear.png" Text="Reject" Enabled="False" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_back.png" Text="Revise" Enabled="False" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                        <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_search.png" Text="View Original Invoice" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/Students-20.png" Text="Student Page" ToolTip="Student Page" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/Invoice-20.png" Text="Invoice Page" ToolTip="Invoice Page" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/Paid-20.png" Text="Payment Page" ToolTip="Payment Page" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/Debt-20.png" Text="Deposit Page" ToolTip="Deposit Page" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/Sales Performance-20.png" Text="CreditMemo Page" ToolTip="CreditMemo Page" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane1" runat="server" Height="35%" Scrolling="None">
                <telerik:RadGrid ID="RadGridRefund" runat="server" AllowFilteringByColumn="True"
                    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" OnItemDataBound="RadGridRefund_OnItemDataBound" OnPreRender="RadGridRefund_OnPreRender" OnSelectedIndexChanged="RadGridRefund_OnSelectedIndexChanged"
                    Height="100%" PageSize="20" DataSourceID="LinqDataSourceRefund" ShowFooter="false" OnFilterCheckListItemsRequested="RadGridRefund_OnFilterCheckListItemsRequested"
                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                    <MasterTableView DataKeyNames="RefundId, CreditMemoPayoutId, StudentId" TableLayout="Fixed" DataSourceID="LinqDataSourceRefund">
                        <Columns>
                            <telerik:GridBoundColumn
                                HeaderText="No" DataField="RefundId" SortExpression="RefundId" UniqueName="RefundId"
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
                                HeaderText="Status" DataField="Status" SortExpression="Status" UniqueName="Status"
                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Invoice No" DataField="InvoiceNumber" SortExpression="InvoiceNumber" UniqueName="InvoiceNumber"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Invoice Type" DataField="InvoiceType" SortExpression="InvoiceType" UniqueName="InvoiceType"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn
                                HeaderText="Refund Date" DataField="RefundDate" SortExpression="RefundDate" UniqueName="RefundDate"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy} {0:t}">
                            </telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Student No" DataField="StudentNo" SortExpression="StudentNo" UniqueName="StudentNo"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Student Name" DataField="StudentName" SortExpression="StudentName" UniqueName="StudentName"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Country" DataField="CountryName" SortExpression="CountryName" UniqueName="CountryName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Agency" DataField="AgencyName" SortExpression="AgencyName" UniqueName="AgencyName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Program" DataField="ProgramName" SortExpression="ProgramName" UniqueName="ProgramName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
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
                                HeaderText="Refund Rate" DataField="RefundRate" SortExpression="RefundRate" UniqueName="RefundRate"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridNumericColumn
                                HeaderText="Refund Amt" DataField="RefundAmount" SortExpression="RefundAmount" UniqueName="RefundAmount"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" DataFormatString="{0:$#,##0.00}" ItemStyle-HorizontalAlign="Right">
                            </telerik:GridNumericColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Refund Reason" DataField="RefundReason" SortExpression="RefundReason" UniqueName="RefundReason"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Credit No" DataField="CreditMemoNumber" SortExpression="CreditMemoNumber" UniqueName="CreditMemoNumber"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Payout Method" DataField="PayoutMethodName" SortExpression="PayoutMethodName" UniqueName="PayoutMethodName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Created User" DataField="UserName" SortExpression="UserName" UniqueName="UserName"
                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn
                                HeaderText="Created Date" DataField="CreatedDate" SortExpression="CreatedDate" UniqueName="CreatedDate"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy} {0:t}">
                            </telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Approval UserName" DataField="ApprovalUserName" SortExpression="ApprovalUserName" UniqueName="ApprovalUserName" Visible="False"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="True">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="0" SaveScrollPosition="true" />
                        <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                            ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                    </ClientSettings>
                    <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                </telerik:RadGrid>
                <asp:LinqDataSource ID="LinqDataSourceRefund" runat="server"
                    ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName="" OrderBy="RefundId Descending"
                    TableName="vwRefunds"
                    Where="RefundId == @RefundId">
                    <WhereParameters>
                        <asp:Parameter DefaultValue="0" Name="RefundId" Type="Int32" />
                    </WhereParameters>
                </asp:LinqDataSource>
            </telerik:RadPane>

            <telerik:RadSplitBar ID="Radsplitbar1" runat="server" CollapseMode="Both" EnableResize="true" />

            <telerik:RadPane ID="Radpane2" runat="server" Scrolling="None">
                <telerik:RadSplitter ID="Radsplitter2" runat="server" Orientation="Vertical">

                    <telerik:RadPane ID="Radpane4" runat="server" Scrolling="None">
                        <telerik:RadSplitter ID="Radsplitter3" runat="server" Orientation="Horizontal">

                            <telerik:RadPane ID="Radpane6" runat="server" Scrolling="None">
                                <telerik:RadSplitter ID="Radsplitter4" runat="server" Orientation="Vertical">

                                    <telerik:RadPane ID="Radpane12" runat="server" Scrolling="None" Width="50%">
                                        <telerik:RadSplitter runat="server" Orientation="Horizontal">

                                            <telerik:RadPane ID="Radpane13" runat="server" Height="27px" Scrolling="None">
                                                <h4>Refund Invoice Detail</h4>
                                            </telerik:RadPane>

                                            <telerik:RadPane ID="RadpaneInvoiceItems" runat="server" Scrolling="None">
                                                <UserControl:InvoiceItemGrid ID="InvoiceItemGridNew" runat="server" />
                                            </telerik:RadPane>

                                        </telerik:RadSplitter>
                                    </telerik:RadPane>

                                </telerik:RadSplitter>

                            </telerik:RadPane>
                        </telerik:RadSplitter>
                    </telerik:RadPane>

                    <telerik:RadSplitBar ID="Radsplitbar2" runat="server" CollapseMode="Both" EnableResize="true" />

                    <telerik:RadPane ID="Radpane3" runat="server" Width="40%" Scrolling="None">
                        <telerik:RadSplitter ID="Radsplitter5" runat="server" Orientation="Horizontal">

                            <telerik:RadPane ID="RadPane10" runat="server" Height="125px">
                                <UserControl:ApprovalLine ID="ApprovalLine1" runat="server" />
                            </telerik:RadPane>

                            <telerik:RadSplitBar ID="Radsplitbar3" runat="server" CollapseMode="None" EnableResize="true" />

                            <telerik:RadPane ID="RadPane15" runat="server">
                                <UserControl:CreditMemoPayout ID="CreditMemoPayout1" runat="server" OnPreRender="CreditMemoPayout1_OnPreRender" />
                            </telerik:RadPane>

                            <telerik:RadSplitBar ID="Radsplitbar4" runat="server" CollapseMode="None" EnableResize="true" />

                            <telerik:RadPane ID="Radpane8" runat="server" Height="115px">
                                <UserControl:FileDownloadList ID="FileDownloadList1" runat="server" OnPreRender="FileDownloadList1_OnPreRender" />
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
                var grid = $find("<%= RadGridRefund.ClientID %>");
                var columns = grid.get_masterTableView().get_columns();
                for (var i = 0; i < columns.length; i++) {
                    columns[i].resizeToFit(false, true);
                }
            }

            function RadToolBarRefundToolbarButtonClick(sender, args) {
                var button = args.get_item();
                //if (button.get_text() === "Request") {
                //    if (!confirm('Do you want to request it?'))
                //        args.set_cancel(true);
                //}
            }

            function ShowApprovalWindow(id) {
                var oWnd = window.radopen('ApprovalApprovePop?type=' + <%= (int)CConstValue.Approval.Refund %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalRejectWindow(id) {
                var oWnd = window.radopen('ApprovalRejectPop?type=' + <%= (int)CConstValue.Approval.Refund %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalReviseWindow(id) {
                var oWnd = window.radopen('ApprovalRevisePop?type=' + <%= (int)CConstValue.Approval.Refund %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalCancelWindow(id) {
                var oWnd = window.radopen('ApprovalCancelPop?type=' + <%= (int)CConstValue.Approval.Refund %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function OnClientClose(oWnd, args) {
                //var arg = args.get_argument();
                <%= Page.GetPostBackEventReference(btnRefresh) %>;
            }

            function ShowPop(refundId, type) {
                var oWnd = window.radopen('RefundPop?id=' + refundId + '&type=' + type, 0, 0, 0, 0);
                var displayWidth = $(window).width() * 0.95;
                var displayHeight = $(window).height() * 0.95;
                if (displayWidth > 1500)
                    displayWidth = 1500;
                oWnd.setSize(displayWidth, displayHeight);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move + Telerik.Web.UI.WindowBehaviors.Maximize);
                oWnd.add_close(OnClientClose);
                return false;
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

        </script>
    </telerik:RadCodeBlock>

</asp:Content>
