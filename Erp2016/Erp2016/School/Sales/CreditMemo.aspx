<%@ Page Title="Credit Memo" Language="C#" MasterPageFile="~/School/Shared/Base.master" AutoEventWireup="true" CodeFile="CreditMemo.aspx.cs" Inherits="School.Sales.CreditMemo" %>

<%@ Import Namespace="Erp2016.Lib" %>


<%@ Register TagPrefix="UserControl" TagName="CreditMemoPayout" Src="~/App_Data/CreditMemoPayout.ascx" %>
<%@ Register TagPrefix="UserControl" TagName="ApprovalLine" Src="~/App_Data/ApprovalLine.ascx" %>
<%@ Register TagPrefix="UserControl" TagName="FileDownloadList" Src="~/App_Data/FileDownloadList.ascx" %>

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
            <asp:Button ID="btnRefreshPayoutHistory" runat="server" OnClick="RefreshPayoutHistory" />
        </div>

        <telerik:RadSplitter runat="server" ID="RadSplitter1" Orientation="Horizontal">

            <telerik:RadPane ID="Radpane3" runat="server" Height="27px" Scrolling="None">
                <h4>Credit Memo List</h4>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane5" runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="RadToolBarCreditMemo" runat="server" OnButtonClick="RadToolBarCreditMemo_OnButtonClick" OnClientButtonClicking="ToolbarButtonClick">
                    <Items>
                        <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_reg.png" Text="Disbursement" Enabled="False" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                        <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_search.png" Text="View Invoice" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                        <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_search.png" Text="View Payment" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                        <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_search.png" Text="View Deposit" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/Students-20.png" Text="Student Page" ToolTip="Student Page" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/Invoice-20.png" Text="Invoice Page" ToolTip="Invoice Page" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/Paid-20.png" Text="Payment Page" ToolTip="Payment Page" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/Debt-20.png" Text="Deposit Page" ToolTip="Deposit Page" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/Receive Cash-20.png" Text="Refund Page" ToolTip="Refund Page" />
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
                <telerik:RadSplitter runat="server" ID="RadSplitter2" Orientation="Horizontal">

                    <telerik:RadPane ID="Radpane4" runat="server" Scrolling="None">
                        <telerik:RadGrid ID="RadGridCreditMemo" runat="server" AllowFilteringByColumn="True" OnSelectedIndexChanged="RadGridCreditMemo_OnSelectedIndexChanged" OnPreRender="RadGridCreditMemo_OnPreRender"
                            AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" OnItemDataBound="RadGridCreditMemo_ItemDataBound"
                            Height="100%" PageSize="20" DataSourceID="LinqDataSourceCreditMemo" ShowFooter="false" OnFilterCheckListItemsRequested="RadGridCreditMemo_OnFilterCheckListItemsRequested"
                            FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                            <MasterTableView DataKeyNames="CreditMemoId, InvoiceId, PaymentId, StudentId" TableLayout="Fixed" DataSourceID="LinqDataSourceCreditMemo">
                                <Columns>
                                    <telerik:GridBoundColumn
                                        HeaderText="CreditMemo No" DataField="CreditMemoNumber" SortExpression="CreditMemoNumber" UniqueName="CreditMemoNumber"
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
                                        HeaderText="CreditMemo Type" DataField="CreditMemoType" SortExpression="CreditMemoType" UniqueName="CreditMemoType"
                                        FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn
                                        HeaderText="Invoice No" DataField="OriginalInvoiceNumber" SortExpression="OriginalInvoiceNumber" UniqueName="OriginalInvoiceNumber"
                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn
                                        HeaderText="Invoice Type" DataField="InvoiceType" SortExpression="InvoiceType" UniqueName="InvoiceType"
                                        FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
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
                                    <telerik:GridNumericColumn
                                        HeaderText="Original Credit Amount" DataField="OriginalCreditMemoAmount" SortExpression="OriginalCreditMemoAmount" UniqueName="OriginalCreditMemoAmount"
                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" DataFormatString="{0:$#,##0.00}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                    </telerik:GridNumericColumn>
                                    <telerik:GridNumericColumn
                                        HeaderText="Available Credit Amount" DataField="AvailableCreditAmount" SortExpression="AvailableCreditAmount" UniqueName="AvailableCreditAmount"
                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" DataFormatString="{0:$#,##0.00}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                    </telerik:GridNumericColumn>
                                    <telerik:GridDateTimeColumn
                                        HeaderText="Start Date" DataField="CreditMemoStartDate" SortExpression="CreditMemoStartDate" UniqueName="CreditMemoStartDate"
                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                        PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy} {0:t}">
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridDateTimeColumn
                                        HeaderText="End Date" DataField="CreditMemoEndDate" SortExpression="CreditMemoEndDate" UniqueName="CreditMemoEndDate"
                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                        PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy} {0:t}">
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridCheckBoxColumn
                                        HeaderText="Disbursement" DataField="Disbursement" SortExpression="Disbursement" UniqueName="Disbursement"
                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridCheckBoxColumn>
                                    <telerik:GridDateTimeColumn
                                        HeaderText="Disbursement Date" DataField="DisbursementDate" SortExpression="DisbursementDate" UniqueName="DisbursementDate"
                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                        PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy} {0:t}">
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridBoundColumn
                                        HeaderText="Created User" DataField="UserName" SortExpression="UserName" UniqueName="UserName"
                                        FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
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
                        <asp:LinqDataSource ID="LinqDataSourceCreditMemo" runat="server"
                            ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName=""
                            TableName="vwCreditMemos" OrderBy="CreatedDate DESC"
                            Where="CreditMemoId == @CreditMemoId">
                            <WhereParameters>
                                <asp:Parameter DefaultValue="0" Name="CreditMemoId" Type="Int32" />
                            </WhereParameters>
                        </asp:LinqDataSource>
                    </telerik:RadPane>
                </telerik:RadSplitter>
            </telerik:RadPane>

            <telerik:RadSplitBar ID="Radsplitbar1" runat="server" CollapseMode="Both" EnableResize="true" />

            <telerik:RadPane ID="Radpane2" runat="server" Scrolling="None">

                <telerik:RadSplitter runat="server" ID="RadSplitter5" Orientation="Vertical">

                    <telerik:RadPane ID="Radpane6" runat="server" Width="65%" Scrolling="None">

                        <telerik:RadTabStrip ID="RadTabStript1" runat="server" MultiPageID="RadMultiPage1" AutoPostBack="True" OnTabClick="RadTabStript1_OnTabClick">
                            <Tabs>
                                <telerik:RadTab Text="Credit" PageViewID="RadPageView1" />
                                <telerik:RadTab Text="Payout" PageViewID="RadPageView2" Selected="True" />
                                <telerik:RadTab Text="Payout History" PageViewID="RadPageView3" />
                            </Tabs>
                        </telerik:RadTabStrip>

                        <telerik:RadMultiPage ID="RadMultiPage1" runat="server" RenderSelectedPageOnly="True">
                            <telerik:RadPageView ID="RadPageView1" runat="server">
                                <telerik:RadSplitter ID="RadSplitter4" runat="server" Orientation="Horizontal">

                                    <telerik:RadPane runat="server" Scrolling="None">
                                        <telerik:RadGrid ID="RadGridCreditMemoCreditHistory" runat="server" DataSourceID="LinqDataSource2" PageSize="20" Height="100%" AllowPaging="true" AllowFilteringByColumn="True" AllowSorting="True"
                                            AutoGenerateColumns="false" OnPreRender="RadGridCreditMemoHistory_PreRender" OnItemDataBound="RadGridCreditMemoHistory_ItemDataBound" OnFilterCheckListItemsRequested="RadGridCreditMemoCreditHistory_OnFilterCheckListItemsRequested"
                                            FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                                            <MasterTableView DataKeyNames="CreditMemoCreditHistoryId" DataSourceID="LinqDataSource2" ShowFooter="True" TableLayout="Fixed">
                                                <Columns>
                                                    <telerik:GridBoundColumn
                                                        HeaderText="No" DataField="CreditMemoPayoutHistoryId" SortExpression="CreditMemoPayoutHistoryId" UniqueName="CreditMemoPayoutHistoryId"
                                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn
                                                        HeaderText="CreditMemo No" DataField="CreditMemoNumber" SortExpression="CreditMemoNumber" UniqueName="CreditMemoNumber" FooterText="Total"
                                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn
                                                        HeaderText="Credit Amount" DataField="CreditAmount" SortExpression="CreditAmount" UniqueName="CreditAmount"
                                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCreditAmount" Text='<%# string.Format("{0:$#,##0.00}", Eval("CreditAmount")) %>' Width="100%" runat="server" Style="text-align: right;" />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <telerik:RadNumericTextBox ID="TotalCreditAmount" runat="server" Width="100%" Type="Currency" ReadOnly="true" ReadOnlyStyle-HorizontalAlign="Right" ReadOnlyStyle-BorderStyle="None" HoveredStyle-HorizontalAlign="Right">
                                                                <ClientEvents OnLoad="TotalCreditAmountLoad" />
                                                            </telerik:RadNumericTextBox>
                                                        </FooterTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridDateTimeColumn
                                                        HeaderText="Credit Date" DataField="CreditDate" SortExpression="CreditDate" UniqueName="CreditDate"
                                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                                        PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy} {0:t}">
                                                    </telerik:GridDateTimeColumn>
                                                    <telerik:GridBoundColumn
                                                        HeaderText="Payment No" DataField="PaymentNumber" SortExpression="PaymentNumber" UniqueName="PaymentNumber"
                                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn
                                                        HeaderText="PayoutMethod" DataField="PayoutMethodName" SortExpression="PayoutMethodName" UniqueName="PayoutMethodName"
                                                        FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                            <ClientSettings EnableRowHoverStyle="True">
                                                <ClientEvents OnGridCreated="RadGridCreditMemoCreditHistory_GridCreated" />
                                                <Selecting AllowRowSelect="true" />
                                                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                                                <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                                                    ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                                            </ClientSettings>
                                            <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                                        </telerik:RadGrid>
                                        <asp:LinqDataSource ID="LinqDataSource2" runat="server"
                                            ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName=""
                                            TableName="vwCreditMemoCreditHistoryLists"
                                            Where="CreditMemoCreditHistoryId == @CreditMemoCreditHistoryId">
                                            <WhereParameters>
                                                <asp:Parameter DefaultValue="0" Name="CreditMemoCreditHistoryId" Type="Int32" />
                                            </WhereParameters>
                                        </asp:LinqDataSource>
                                    </telerik:RadPane>
                                </telerik:RadSplitter>
                            </telerik:RadPageView>

                            <telerik:RadPageView ID="RadPageView2" runat="server" Selected="True">
                                <telerik:RadSplitter ID="RadSplitter3" runat="server" Orientation="Horizontal">

                                    <telerik:RadPane ID="Radpane9" runat="server" Height="40px" Scrolling="None">
                                        <telerik:RadToolBar ID="RadToolBarCreditMemoPayout" runat="server" OnButtonClick="RadToolBarCreditMemoPayout_OnButtonClick" OnClientButtonClicked="ToolbarButtonClick">
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
                                                <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_new.png" Text="New Payout" />
                                                <telerik:RadToolBarButton IsSeparator="true" />
                                                <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_mark.png" Text="Request" Enabled="False" />
                                                <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_delete.png" Text="Cancel" Enabled="False" />
                                                <telerik:RadToolBarButton IsSeparator="true" />
                                                <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_excute.png" Text="Approve" Enabled="False" />
                                                <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_clear.png" Text="Reject" Enabled="False" />
                                                <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_back.png" Text="Revise" Enabled="False" />
                                                <telerik:RadToolBarButton IsSeparator="true" />
                                            </Items>
                                        </telerik:RadToolBar>
                                    </telerik:RadPane>

                                    <telerik:RadPane ID="RadPane7" runat="server" Scrolling="None">
                                        <telerik:RadGrid ID="RadGridCreditMemoPayout" runat="server"
                                            AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False" Height="92%" PageSize="20" AllowFilteringByColumn="True"
                                            DataSourceID="LinqDataSourceCreditMemoPayout" ShowFooter="True" OnPreRender="RadGridCreditMemoPayout_OnPreRender" OnItemDataBound="RadGridCreditMemoPayout_OnItemDataBound" OnFilterCheckListItemsRequested="RadGridCreditMemoPayout_OnFilterCheckListItemsRequested"
                                            FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                                            <MasterTableView DataKeyNames="CreditMemoPayoutId" TableLayout="Fixed" DataSourceID="LinqDataSourceCreditMemoPayout">
                                                <Columns>
                                                    <telerik:GridBoundColumn
                                                        HeaderText="No" DataField="CreditMemoPayoutId" SortExpression="CreditMemoPayoutId" UniqueName="CreditMemoPayoutId"
                                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn
                                                        HeaderText="Status" DataField="Status" SortExpression="Status" UniqueName="Status"
                                                        FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                                    </telerik:GridBoundColumn>
                                                    <%--  <telerik:GridTemplateColumn
                                                        HeaderText="ApprovalStatus" DataField="ApprovalStatus" SortExpression="ApprovalStatus" UniqueName="ApprovalStatus"
                                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblApprovalStatus" Text='<%# Eval("ApprovalStatus") %>' Width="100%" runat="server" Style="text-align: right;" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>--%>
                                                    <telerik:GridBoundColumn
                                                        HeaderText="HQ Status" DataField="HqStatus" SortExpression="HqStatus" UniqueName="HqStatus"
                                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn
                                                        HeaderText="PayoutMethod Type" DataField="PayoutMethodType" SortExpression="PayoutMethodType" UniqueName="PayoutMethodType" FooterText="Total"
                                                        FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn
                                                        HeaderText="Amount" DataField="Amount" SortExpression="Amount" UniqueName="Amount"
                                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" Text='<%# string.Format("{0:$#,##0.00}", Eval("Amount")) %>' Width="100%" runat="server" Style="text-align: right;" />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <telerik:RadNumericTextBox ID="TotalAmount" runat="server" Width="100%" Type="Currency" ReadOnly="true" ReadOnlyStyle-HorizontalAlign="Right" ReadOnlyStyle-BorderStyle="None" HoveredStyle-HorizontalAlign="Right">
                                                                <ClientEvents OnLoad="TotalAmountLoad" />
                                                            </telerik:RadNumericTextBox>
                                                        </FooterTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn
                                                        HeaderText="Available Amount" DataField="AvailableAmount" SortExpression="AvailableAmount" UniqueName="AvailableAmount"
                                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAvailableAmount" Text='<%# string.Format("{0:$#,##0.00}", Eval("AvailableAmount")) %>' Width="100%" runat="server" Style="text-align: right;" />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <telerik:RadNumericTextBox ID="TotalAvailableAmount" runat="server" Width="100%" Type="Currency" ReadOnly="true" ReadOnlyStyle-HorizontalAlign="Right" ReadOnlyStyle-BorderStyle="None" HoveredStyle-HorizontalAlign="Right">
                                                                <ClientEvents OnLoad="TotalAvailableAmountLoad" />
                                                            </telerik:RadNumericTextBox>
                                                        </FooterTemplate>
                                                    </telerik:GridTemplateColumn>

                                                    <telerik:GridCheckBoxColumn
                                                        HeaderText="Disbursement" DataField="Disbursement" SortExpression="Disbursement" UniqueName="Disbursement"
                                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" HeaderStyle-Font-Size="X-Small">
                                                    </telerik:GridCheckBoxColumn>
                                                    <telerik:GridDateTimeColumn
                                                        HeaderText="Disbursement Date" DataField="DisbursementDate" SortExpression="DisbursementDate" UniqueName="DisbursementDate"
                                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                                        PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy} {0:t}">
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
                                                    <telerik:GridBoundColumn
                                                        HeaderText="Memo" DataField="ApprovalMemo" SortExpression="ApprovalMemo" UniqueName="ApprovalMemo"
                                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn
                                                        HeaderText="Approval UserName" DataField="ApprovalUserName" SortExpression="ApprovalUserName" UniqueName="ApprovalUserName" Visible="False"
                                                        FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                            <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="True">
                                                <ClientEvents OnGridCreated="RadGridCreditMemoPayout_GridCreated" />
                                                <Selecting AllowRowSelect="true" />
                                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True"></Scrolling>
                                                <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                                                    ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                                            </ClientSettings>
                                            <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                                        </telerik:RadGrid>
                                        <asp:LinqDataSource ID="LinqDataSourceCreditMemoPayout" runat="server"
                                            ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName=""
                                            TableName="vwCreditMemoPayoutLists" OrderBy="CreatedDate Descending"
                                            Where="CreditMemoPayoutId == @CreditMemoPayoutId">
                                            <WhereParameters>
                                                <asp:Parameter DefaultValue="0" Name="CreditMemoPayoutId" Type="Int32" />
                                            </WhereParameters>
                                        </asp:LinqDataSource>
                                    </telerik:RadPane>

                                </telerik:RadSplitter>
                            </telerik:RadPageView>

                            <telerik:RadPageView ID="RadPageView3" runat="server">
                                <telerik:RadSplitter ID="RadSplitter7" runat="server" Orientation="Horizontal">

                                    <telerik:RadPane ID="Radpane12" runat="server" Height="40px" Scrolling="None">
                                        <telerik:RadToolBar ID="RadToolBarCreditMemoPayoutHistory" runat="server" OnButtonClick="RadToolBarCreditMemoPayoutHistory_OnButtonClick" OnClientButtonClicked="ToolbarButtonClick">
                                            <Items>
                                                <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_new.png" Text="Add Payout" Enabled="False" />
                                                <telerik:RadToolBarButton IsSeparator="true" />
                                                <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_back.png" Text="Payout Reverse" Enabled="false" />
                                                <telerik:RadToolBarButton IsSeparator="true" />
                                            </Items>
                                        </telerik:RadToolBar>
                                    </telerik:RadPane>

                                    <telerik:RadPane ID="RadPane13" runat="server" Scrolling="None">
                                        <telerik:RadGrid ID="RadGridCreditMemoPayoutHistory" runat="server"
                                            AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False" Height="92%" PageSize="20" AllowFilteringByColumn="True" OnSelectedIndexChanged="RadGridCreditMemoPayoutHistory_OnSelectedIndexChanged"
                                            DataSourceID="LinqDataSourceCreditMemoPayoutHistory" ShowFooter="True" OnItemDataBound="CreditMemoPayoutHistory_OnItemDataBound" OnFilterCheckListItemsRequested="CreditMemoPayoutHistory_OnFilterCheckListItemsRequested"
                                            FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                                            <MasterTableView DataKeyNames="CreditMemoPayoutHistoryId" TableLayout="Fixed" DataSourceID="LinqDataSourceCreditMemoPayoutHistory">
                                                <Columns>
                                                    <telerik:GridBoundColumn
                                                        HeaderText="No" DataField="CreditMemoPayoutHistoryId" SortExpression="CreditMemoPayoutHistoryId" UniqueName="CreditMemoPayoutHistoryId"
                                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn
                                                        HeaderText="Payout No" DataField="CreditMemoPayoutId" SortExpression="CreditMemoPayoutId" UniqueName="CreditMemoPayoutId"
                                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn
                                                        HeaderText="Check No" DataField="CheckNo" SortExpression="CheckNo" UniqueName="CheckNo"
                                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn
                                                        HeaderText="Wire Transfer No" DataField="WireTransferNo" SortExpression="WireTransferNo" UniqueName="WireTransferNo"
                                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn
                                                        HeaderText="Payout Amount" DataField="PayoutAmount" SortExpression="PayoutAmount" UniqueName="PayoutAmount"
                                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPayoutAmount" Text='<%# string.Format("{0:$#,##0.00}", Eval("PayoutAmount")) %>' Width="100%" runat="server" Style="text-align: right;" />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <telerik:RadNumericTextBox ID="TotalPayoutAmount" runat="server" Width="100%" Type="Currency" ReadOnly="true" ReadOnlyStyle-HorizontalAlign="Right" ReadOnlyStyle-BorderStyle="None" HoveredStyle-HorizontalAlign="Right">
                                                                <ClientEvents OnLoad="TotalPayoutAmountLoad" />
                                                            </telerik:RadNumericTextBox>
                                                        </FooterTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridDateTimeColumn
                                                        HeaderText="Payout Date" DataField="PayoutDate" SortExpression="PayoutDate" UniqueName="PayoutDate"
                                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                                        PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy} {0:t}">
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
                                            <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="True">
                                                <ClientEvents OnGridCreated="RadGridCreditMemoPayoutHistory_GridCreated" />
                                                <Selecting AllowRowSelect="true" />
                                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True"></Scrolling>
                                                <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                                                    ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                                            </ClientSettings>
                                            <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                                        </telerik:RadGrid>
                                        <asp:LinqDataSource ID="LinqDataSourceCreditMemoPayoutHistory" runat="server"
                                            ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName=""
                                            TableName="vwCreditMemoPayoutHistoryLists" OrderBy="CreatedDate Descending"
                                            Where="CreditMemoPayoutHistoryId == @CreditMemoPayoutHistoryId">
                                            <WhereParameters>
                                                <asp:Parameter DefaultValue="0" Name="CreditMemoPayoutHistoryId" Type="Int32" />
                                            </WhereParameters>
                                        </asp:LinqDataSource>
                                    </telerik:RadPane>

                                </telerik:RadSplitter>
                            </telerik:RadPageView>

                        </telerik:RadMultiPage>

                    </telerik:RadPane>

                    <telerik:RadSplitBar ID="Radsplitbar2" runat="server" CollapseMode="None" EnableResize="true" />

                    <telerik:RadPane ID="RadPane8" runat="server" Scrolling="None">
                        <telerik:RadSplitter ID="RadSplitter6" runat="server" Orientation="Horizontal">

                            <telerik:RadPane ID="RadPane10" runat="server" Height="125px">
                                <UserControl:ApprovalLine ID="ApprovalLine1" runat="server" />
                            </telerik:RadPane>

                            <telerik:RadSplitBar ID="Radsplitbar3" runat="server" CollapseMode="None" EnableResize="true" />

                            <telerik:RadPane ID="RadPane11" runat="server">
                                <UserControl:CreditMemoPayout ID="CreditMemoPayout1" runat="server" />
                            </telerik:RadPane>

                            <telerik:RadSplitBar ID="Radsplitbar4" runat="server" CollapseMode="None" EnableResize="true" />

                            <telerik:RadPane runat="server" Height="115px">
                                <UserControl:FileDownloadList ID="FileDownloadList1" runat="server" />
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
                var grid = $find("<%= RadGridCreditMemo.ClientID %>");
                var columns = grid.get_masterTableView().get_columns();
                for (var i = 0; i < columns.length; i++) {
                    columns[i].resizeToFit(false, true);
                }

               var grid1 = $find("<%= RadGridCreditMemoPayout.ClientID %>");
                columns = grid1.get_masterTableView().get_columns();
                for (var i = 0; i < columns.length; i++) {
                    columns[i].resizeToFit(false, true);
                }
            }

            // jQuery
            $(window).bind("load", function () {
                SetValueCreditHistory();
                SetValuePayout();
                SetValuePayoutHistory();
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

            var sumTotalCreditAmountInput = null;
            function TotalCreditAmountLoad(sender, args) {
                sumTotalCreditAmountInput = sender;
            }

            var sumTotalAmountInput = null;
            function TotalAmountLoad(sender, args) {
                sumTotalAmountInput = sender;
            }

            var sumTotalAvailableAmountInput = null;
            function TotalAvailableAmountLoad(sender, args) {
                sumTotalAvailableAmountInput = sender;
            }

            var sumTotalPayoutAmountInput = null;
            function TotalPayoutAmountLoad(sender, args) {
                sumTotalPayoutAmountInput = sender;
            }

            function SetValueCreditHistory() {
                var grid = $find("<%= RadGridCreditMemoCreditHistory.ClientID %>"); //grid id

                if (grid) {
                    var masterTable = grid.get_masterTableView();
                    var rows = masterTable.get_dataItems();

                    var totalAmount = 0.0;
                    for (var i = 0; i < rows.length; i++) {
                        totalAmount = totalAmount + GetCellValue(rows[i], "CreditAmount", "lblCreditAmount");
                    }
                    sumTotalCreditAmountInput.set_value(totalAmount);
                }
            }

            function SetValuePayout() {
                var grid = $find("<%= RadGridCreditMemoPayout.ClientID %>"); //grid id

                if (grid) {
                    var masterTable = grid.get_masterTableView();
                    var rows = masterTable.get_dataItems();

                    var totalAmount = 0.0;
                    var totalAmount1 = 0.0;
                    for (var i = 0; i < rows.length; i++) {
                        // don't cal
                        if (rows[i].get_cell("Status").innerText === "Rejected" ||
                            rows[i].get_cell("Status").innerText === "Canceled")
                            continue;

                        totalAmount = totalAmount + GetCellValue(rows[i], "Amount", "lblAmount");
                        totalAmount1 = totalAmount1 + GetCellValue(rows[i], "AvailableAmount", "lblAvailableAmount");
                    }
                    sumTotalAmountInput.set_value(totalAmount);
                    sumTotalAvailableAmountInput.set_value(totalAmount1);
                }
            }

            function SetValuePayoutHistory() {
                var grid = $find("<%= RadGridCreditMemoPayoutHistory.ClientID %>"); //grid id

                if (grid) {
                    var masterTable = grid.get_masterTableView();
                    var rows = masterTable.get_dataItems();

                    var totalAmount = 0.0;
                    for (var i = 0; i < rows.length; i++) {
                        totalAmount = totalAmount + GetCellValue(rows[i], "PayoutAmount", "lblPayoutAmount");
                    }
                    sumTotalPayoutAmountInput.set_value(totalAmount);
                }
            }

            // when grid created
            function RadGridCreditMemoCreditHistory_GridCreated(sender, eventArgs) {
                SetValueCreditHistory();
            }

            function RadGridCreditMemoPayout_GridCreated(sender, eventArgs) {
                SetValuePayout();
            }

            function RadGridCreditMemoPayoutHistory_GridCreated(sender, eventArgs) {
                SetValuePayoutHistory();
            }
            // == end total sum ==

            function ToolbarButtonClick(sender, args) {
                var button = args.get_item();
                if (button.get_text() === "Disbursement") {
                    if (!confirm('Do you want to change flag of disbursement?'))
                        args.set_cancel(true);
                }
                else if (button.get_text() === "Payout Reverse") {
                    if (!confirm('Do you want to reverse Payout?'))
                        args.set_cancel(true);
                }
            }

            function ShowPayoutPop(id, type) {
                var oWnd = window.radopen('CreditMemoPayoutPop?id=' + id + '&type=' + type, 0, 0, 0, 0);
                oWnd.setSize(700, 700);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move + Telerik.Web.UI.WindowBehaviors.Maximize + Telerik.Web.UI.WindowBehaviors.Resize);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowPayoutHistoryPop(id, type) {
                var oWnd = window.radopen('CreditMemoPayoutHistoryPop?id=' + id + '&type=' + type, 0, 0, 0, 0);
                oWnd.setSize(700, 400);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move + Telerik.Web.UI.WindowBehaviors.Maximize + Telerik.Web.UI.WindowBehaviors.Resize);
                oWnd.add_close(OnClientClosePayoutHistory);
                return false;
            }

            function OnClientClosePayoutHistory(oWnd, args) {
                <%=Page.GetPostBackEventReference(btnRefreshPayoutHistory)%>
            }

            function OnClientClose(oWnd, args) {
                <%=Page.GetPostBackEventReference(btnRefresh)%>
            }

            function Close() {
                var wnd = GetRadWindow();
                wnd.close();
            }

            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement && window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }

            function ShowApprovalWindow(id) {
                var oWnd = window.radopen('ApprovalApprovePop?type=' + <%= (int)CConstValue.Approval.CreditMemoPayout %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalRejectWindow(id) {
                var oWnd = window.radopen('ApprovalRejectPop?type=' + <%= (int)CConstValue.Approval.CreditMemoPayout %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalReviseWindow(id) {
                var oWnd = window.radopen('ApprovalRevisePop?type=' + <%= (int)CConstValue.Approval.CreditMemoPayout %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalCancelWindow(id) {
                var oWnd = window.radopen('ApprovalCancelPop?type=' + <%= (int)CConstValue.Approval.CreditMemoPayout %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowInvoiceWindow(invoiceId) {
                var oWnd = window.radopen('InvoiceItemGridPop?id=' + invoiceId, 0, 0, 0, 0);
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

            function ShowDepositPaymentWindow(depositId) {
                var oWnd = window.radopen('DepositPaymentGridPop?id=' + depositId, 0, 0, 0, 0);
                var displayWidth = $(window).width() * 0.95;
                //var displayHeight = $(window).height() * 0.95;

                //var displayWidth = 1000;
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
