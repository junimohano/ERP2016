<%@ Page Title="" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="PaymentDormitoryPop.aspx.cs" Inherits="School_StudentHousing_PaymentDormitoryPop" %>

<%@ Register TagPrefix="UserControl" TagName="PaymentHistoryGrid" Src="~/App_Data/PaymentHistoryGrid.ascx" %>
<%@ Register TagPrefix="UserControl" TagName="InvoiceItemGrid" Src="~/App_Data/InvoiceItemGrid.ascx" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">

</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">
        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="RadpaneBasic" runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="UpdateToolBar" runat="server" OnButtonClick="UpdateToolBar_ButtonClick" OnClientButtonClicking="ToolbarButtonClick">
                    <Items>

                        <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_back.png" Text="Close Window" ToolTip="Close Window"></telerik:RadToolBarButton>
                        <telerik:RadToolBarButton IsSeparator="true" />
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane5" runat="server" Scrolling="None">
                <telerik:RadSplitter runat="server" ID="RadSplitter2" Orientation="Horizontal">
                    <telerik:RadPane ID="Radpane1" runat="server" Height="150px">
                        <telerik:RadLabel runat="server" ID="lblhostlist" Text="Dormitory Invoice "></telerik:RadLabel>
                        <telerik:RadGrid ID="RadGridInvoice" runat="server" AllowFilteringByColumn="True"
                            AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="20"
                            ShowFooter="false"
                            FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true" OnNeedDataSource="RadGridInvoice_NeedDataSource">
                            <MasterTableView DataKeyNames="InvoiceId" TableLayout="Fixed">
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
                                        FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
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
                                        HeaderText="Country" DataField="CountryName" SortExpression="CountryName" UniqueName="CountryName"
                                        FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn
                                        HeaderText="Agency" DataField="AgencyName" SortExpression="AgencyName" UniqueName="AgencyName"
                                        FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn
                                        HeaderText="Program" DataField="InvoiceName" SortExpression="InvoiceName" UniqueName="InvoiceName"
                                        FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridDateTimeColumn
                                        HeaderText="Start Date" DataField="Start_Date" SortExpression="Start_Date" UniqueName="Start_Date"
                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                        PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy}">
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridDateTimeColumn
                                        HeaderText="End Date" DataField="End_Date" SortExpression="End_Date" UniqueName="End_Date"
                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                        PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy}">
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridCheckBoxColumn
                                        HeaderText="FG" DataField="IsFinancialGurantee" SortExpression="IsFinancialGurantee" UniqueName="IsFinancialGurantee"
                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridCheckBoxColumn>
                                    <telerik:GridBoundColumn
                                        HeaderText="Created User" DataField="CreatedUsername" SortExpression="CreatedUsername" UniqueName="CreatedUsername"
                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridDateTimeColumn
                                        HeaderText="Created Date" DataField="CreatedDate" SortExpression="CreatedDate" UniqueName="CreatedDate"
                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                        PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy} {0:t}">
                                    </telerik:GridDateTimeColumn>
                                </Columns>
                            </MasterTableView>

                        </telerik:RadGrid>

                    </telerik:RadPane>

                    <telerik:RadSplitBar ID="Radsplitbar1" runat="server" CollapseMode="Both" EnableResize="true" />
                    <telerik:RadPane ID="Radpane2" runat="server" Width="30%">
                        <telerik:RadSplitter runat="server" ID="RadSplitter3" Orientation="Horizontal">
                            <telerik:RadPane ID="Radpane3" runat="server" Height="27px" Scrolling="None">
                                <h4>Dormitory Payment</h4>

                            </telerik:RadPane>
                            <telerik:RadPane ID="Radpane4" runat="server">
                                <UserControl:PaymentHistoryGrid ID="PaymentHistoryGrid1" runat="server" />

                            </telerik:RadPane>

                        </telerik:RadSplitter>
                    </telerik:RadPane>
                </telerik:RadSplitter>
            </telerik:RadPane>
            <telerik:RadSplitBar ID="RadSplitBar2" runat="server" CollapseMode="Both" EnableResize="true" />

            <telerik:RadPane ID="RadPane6" runat="server" Width="40%" Scrolling="None">

                <telerik:RadSplitter runat="server" Orientation="Horizontal" ResizeMode="EndPane">

                    <telerik:RadPane ID="RadPane9" runat="server" Height="27px" Scrolling="None">
                        <h4>Invoice items</h4>
                    </telerik:RadPane>

                    <telerik:RadPane ID="RadPane8" runat="server">
                        <UserControl:InvoiceItemGrid ID="InvoiceItemGrid1" runat="server" />
                    </telerik:RadPane>

                </telerik:RadSplitter>

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
                if (button.get_text() == "Save") {
                    if (!confirm('Do you want to save it?'))
                        args.set_cancel(true);
                }
                if (button.get_text() == "Close Window") {
                    Close();
                }
                function OnClientClose(oWnd, args) {
                    Close();
                    //var arg = args.get_argument();
                    <%--<%=Page.GetPostBackEventReference(btnRefresh)%>--%>
                }
            }



        </script>
    </telerik:RadCodeBlock>
</asp:Content>
