<%@ Page Language="C#" MasterPageFile="~/School/Shared/Base.master" AutoEventWireup="true" CodeFile="Promotion.aspx.cs" Inherits="School.Registrar.Promotion" %>

<%@ Import Namespace="Erp2016.Lib" %>

<%@ Register TagPrefix="usercontrol" TagName="approvalline" Src="~/App_Data/ApprovalLine.ascx" %>
<%@ Register TagPrefix="UserControl" TagName="FileDownloadList" Src="~/App_Data/FileDownloadList.ascx" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">
        <div style="display: none">
            <asp:Button ID="ButtonGridRefresh" runat="server" OnClick="ButtonGridRefresh_OnClick" />
        </div>

        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="Radpane3" runat="server" Height="27px" Scrolling="None">
                <h4>Promotion list</h4>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane4" runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="RadToolBar1" runat="server" OnClientButtonClicking="ToolbarButtonClick" OnButtonClick="RadToolBar1_OnButtonClick">
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
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_new.png" Text="New Promotion" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_mark.png" Text="Request" Enabled="False" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_delete.png" Text="Cancel" Enabled="False" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_excute.png" Text="Approve" Enabled="False" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_clear.png" Text="Reject" Enabled="False" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_back.png" Text="Revise" Enabled="False" />
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane1" runat="server" Height="40%" Scrolling="None">
                <telerik:RadGrid ID="RadGrid1" runat="server" AllowFilteringByColumn="True"
                    AllowPaging="True" AllowSorting="True" AllowMultiRowSelection="False" AutoGenerateColumns="False" OnPreRender="RadGrid1_PreRender" OnSelectedIndexChanged="RadGrid1_OnSelectedIndexChanged"
                    Height="100%" PageSize="20" DataSourceID="LinqDataSource1" ShowFooter="False" OnFilterCheckListItemsRequested="RadGrid1_OnFilterCheckListItemsRequested"
                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                    <MasterTableView DataKeyNames="PromotionId" TableLayout="Fixed" DataSourceID="LinqDataSource1">
                        <Columns>
                            <telerik:GridBoundColumn
                                HeaderText="Promotion No" DataField="PromotionMasterNo" SortExpression="PromotionMasterNo" UniqueName="PromotionMasterNo"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Site" DataField="SiteName" SortExpression="SiteName" UniqueName="SiteName"
                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Status" DataField="Status" SortExpression="Status" UniqueName="Status"
                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridNumericColumn
                                HeaderText="Amount" DataField="Amount" SortExpression="Amount" UniqueName="Amount"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" DataFormatString="{0:$#,##0.00}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                            </telerik:GridNumericColumn>
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
                                HeaderText="Created User" DataField="UserName" SortExpression="UserName" UniqueName="UserName"
                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn
                                HeaderText="Created Date" DataField="CreatedDate" SortExpression="CreatedDate" UniqueName="CreatedDate"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy} {0:t}">
                            </telerik:GridDateTimeColumn>
                            <telerik:GridCheckBoxColumn
                                HeaderText="IsActive" DataField="IsActive" SortExpression="IsActive" UniqueName="IsActive"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Approval UserName" DataField="ApprovalUserName" SortExpression="ApprovalUserName" UniqueName="ApprovalUserName" Visible="False"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnablePostBackOnRowClick="true">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True"></Scrolling>
                        <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                            ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                    </ClientSettings>
                    <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                </telerik:RadGrid>
                <asp:LinqDataSource ID="LinqDataSource1" runat="server"
                    ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName=""
                    TableName="vwPromotions"
                    Where="PromotionId == @PromotionId">
                    <WhereParameters>
                        <asp:Parameter DefaultValue="0" Name="PromotionId" Type="Int32" />
                    </WhereParameters>
                </asp:LinqDataSource>
            </telerik:RadPane>

            <telerik:RadSplitBar ID="Radsplitbar1" runat="server" CollapseMode="Both" EnableResize="true" />

            <telerik:RadPane ID="Radpane2" runat="server" Scrolling="Y">
                <telerik:RadSplitter ID="RadSplitterStud" runat="server" Orientation="Vertical">

                    <telerik:RadPane ID="RadPaneStud" runat="server" Width="60%" Scrolling="None">

                        <telerik:RadSplitter ID="RadSplitter3" runat="server" Orientation="Horizontal">

                            <telerik:RadPane ID="RadPane8" runat="server" Height="27px" Scrolling="None">
                                <h4>Used Promotion History</h4>
                            </telerik:RadPane>

                            <telerik:RadPane ID="RadPane9" runat="server" Scrolling="None">
                                <telerik:RadGrid ID="RadGridPromotionHistory" runat="server" AllowFilteringByColumn="True"
                                    AllowPaging="True" AllowSorting="True" AllowMultiRowSelection="False" AutoGenerateColumns="False" Height="100%" PageSize="20" DataSourceID="LinqDataSourcePromotionHistory" ShowFooter="False"
                                    OnFilterCheckListItemsRequested="RadGridPromotionHistory_OnFilterCheckListItemsRequested"
                                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                                    <MasterTableView DataKeyNames="PromotionId" TableLayout="Fixed" DataSourceID="LinqDataSourcePromotionHistory">
                                        <Columns>
                                            <telerik:GridBoundColumn
                                                HeaderText="Site" DataField="SiteName" SortExpression="SiteName" UniqueName="SiteName"
                                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn
                                                HeaderText="Site Location" DataField="SiteLocationName" SortExpression="SiteLocationName" UniqueName="SiteLocationName"
                                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn
                                                HeaderText="Invoice No" DataField="InvoiceNumber" SortExpression="InvoiceNumber" UniqueName="InvoiceNumber"
                                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn
                                                HeaderText="Invoice Type" DataField="InvoiceType" SortExpression="InvoiceType" UniqueName="InvoiceType"
                                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn
                                                HeaderText="Invoice Status" DataField="InvoiceStatus" SortExpression="InvoiceStatus" UniqueName="InvoiceStatus"
                                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn
                                                HeaderText="Student Name" DataField="StudentName" SortExpression="StudentName" UniqueName="StudentName"
                                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn
                                                HeaderText="Agency" DataField="AgencyName" SortExpression="AgencyName" UniqueName="AgencyName"
                                                FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridNumericColumn
                                                HeaderText="Used Amount" DataField="PromotionAmount" SortExpression="PromotionAmount" UniqueName="PromotionAmount"
                                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" DataFormatString="{0:$#,##0.00}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                            </telerik:GridNumericColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="true" />
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True"></Scrolling>
                                        <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                                            ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                                    </ClientSettings>
                                    <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                                </telerik:RadGrid>

                                <asp:LinqDataSource ID="LinqDataSourcePromotionHistory" runat="server"
                                    ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName="" OrderBy="CreatedDate Desc"
                                    TableName="vwPromotionHistories"
                                    Where="PromotionId == @PromotionId">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="0" Name="PromotionId" Type="Int32" />
                                    </WhereParameters>
                                </asp:LinqDataSource>
                            </telerik:RadPane>

                        </telerik:RadSplitter>
                    </telerik:RadPane>

                    <telerik:RadSplitBar ID="RadSplitBarStud" runat="server" CollapseMode="None" EnableResize="true" />

                    <telerik:RadPane ID="RadPane6" runat="server" Scrolling="None">
                        <telerik:RadSplitter ID="RadSplitter2" runat="server" Orientation="Horizontal">

                            <telerik:RadPane ID="RadPane5" runat="server" Height="125px">
                                <UserControl:approvalline ID="ApprovalLine1" runat="server" />
                            </telerik:RadPane>

                            <telerik:RadSplitBar ID="Radsplitbar2" runat="server" CollapseMode="None" EnableResize="true" />

                            <telerik:RadPane ID="RadPane7" runat="server">
                                <div class="formStyle3">
                                    <fieldset>
                                        <legend>Information</legend>

                                        <div style="float: left; width: 100%;">
                                            <div>
                                                <label>Master Number</label>
                                                <telerik:RadTextBox ID="tbMaster" CssClass="RadSizeMiddle" runat="server" ReadOnly="True" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Site</label><telerik:RadTextBox ID="RadTextBoxSite" CssClass="RadSizeMiddle" runat="server" ReadOnly="True"></telerik:RadTextBox>
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Site Location</label><telerik:RadComboBox ID="RadComboBoxSiteLocation" Enabled="False" CssClass="RadSizeMiddle" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxSiteLocation_OnSelectedIndexChanged" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" />
                                                <asp:Literal ID="itemsClientSide" runat="server" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Country</label>
                                                <telerik:RadTextBox ID="RadTextBoxCountry" CssClass="RadSizeMiddle" runat="server" ReadOnly="false" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Amount</label>
                                                <telerik:RadNumericTextBox ID="RadNumericTextBoxAmount" CssClass="RadSizeMiddle" runat="server" ReadOnly="True" Type="Currency" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Memo</label>
                                                <telerik:RadTextBox ID="tbComment" CssClass="RadSizeMultiLine" TextMode="MultiLine" runat="server" ReadOnly="True" />
                                                <br style="clear: both;" />
                                            </div>
                                        </div>
                                    </fieldset>

                                    <UserControl:FileDownloadList ID="FileDownloadList1" runat="server" />
                                </div>
                            </telerik:RadPane>

                        </telerik:RadSplitter>
                    </telerik:RadPane>

                </telerik:RadSplitter>

            </telerik:RadPane>
        </telerik:RadSplitter>
    </telerik:RadAjaxPanel>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function ToolbarButtonClick(sender, args) {
                var button = args.get_item();
                //if (button.get_text() == "Request") {
                //    if (!confirm('Do you want to request it?'))
                //        args.set_cancel(true);
                //}
            }

            function ShowPop(id, type) {
                var oWnd = window.radopen('PromotionPop?id=' + id + '&type=' + type, 0, 0, 0, 0);
                oWnd.setSize(700, 700);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move + Telerik.Web.UI.WindowBehaviors.Maximize + Telerik.Web.UI.WindowBehaviors.Resize);
                oWnd.add_close(OnClientClose);
                return false;
            }
            function ShowApprovalWindow(id) {
                var oWnd = window.radopen('ApprovalApprovePop?type=' + <%= (int)CConstValue.Approval.Promotion %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }
            function ShowApprovalRejectWindow(id) {
                var oWnd = window.radopen('ApprovalRejectPop?type=' + <%= (int)CConstValue.Approval.Promotion %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }
            function ShowApprovalReviseWindow(id) {
                var oWnd = window.radopen('ApprovalRevisePop?type=' + <%= (int)CConstValue.Approval.Promotion %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalCancelWindow(id) {
                var oWnd = window.radopen('ApprovalCancelPop?type=' + <%= (int)CConstValue.Approval.Promotion %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function OnClientClose(oWnd, args) {
                var arg = args.get_argument();
                <%= Page.GetPostBackEventReference(ButtonGridRefresh) %>;
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
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
