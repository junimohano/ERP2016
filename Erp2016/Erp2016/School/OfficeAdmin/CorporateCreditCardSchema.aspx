<%@ Page Title="Corporate Credit Card Schema" Language="C#" MasterPageFile="~/School/Shared/Base.master" AutoEventWireup="true" CodeFile="CorporateCreditCardSchema.aspx.cs" Inherits="School.OfficeAdmin.CorporateCreditCardSchema" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">

        <div id="test" style="display: none">
            <asp:Button ID="ButtonGridRefresh" runat="server" OnClick="ButtonGridRefresh_OnClick" />
        </div>

        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal" ResizeMode="EndPane">

            <telerik:RadPane runat="server" Scrolling="None">

                <telerik:RadSplitter runat="server" ID="RadSplitter3" Height="100%" Width="100%" Orientation="Horizontal">
                    <telerik:RadPane ID="RadPane3" runat="server" Height="27px" Scrolling="None">
                        <h4>Corporate Credit Card Schema</h4>
                    </telerik:RadPane>

                    <telerik:RadPane ID="RadPane1" runat="server" Height="40px" Scrolling="None">
                        <telerik:RadToolBar ID="RadToolBar1" runat="server" OnButtonClick="RadToolBar1_OnButtonClick">
                            <Items>
                                <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_new.png" Text="Add Corporate Credit Card" />
                                <telerik:RadToolBarButton IsSeparator="True" />
                                <telerik:RadToolBarButton ImageUrl="~/assets/img/icon_s_edit.png" Text="Modify Corporate Credit Card" />
                                <telerik:RadToolBarButton IsSeparator="True" />
                            </Items>
                        </telerik:RadToolBar>
                    </telerik:RadPane>

                    <telerik:RadPane ID="RadPane2" runat="server" Scrolling="None">
                        <telerik:RadGrid ID="RadGridCorporateCreditCardSchema" DataSourceID="LinqDataSourceCorporateCreditCardSchema" runat="server" PageSize="20" AutoGenerateColumns="false" AllowPaging="true" Height="100%"
                            AllowCustomPaging="false" AllowSorting="true" AllowFilteringByColumn="True" EnableLinqExpressions="false" OnFilterCheckListItemsRequested="RadGridGradeName_OnFilterCheckListItemsRequested"
                            FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                            <MasterTableView DataSourceID="LinqDataSourceCorporateCreditCardSchema" TableLayout="Fixed" DataKeyNames="CorporateCreditCardSchemaId">
                                <Columns>
                                    <telerik:GridBoundColumn
                                        HeaderText="No" DataField="CorporateCreditCardSchemaId" SortExpression="CorporateCreditCardSchemaId" UniqueName="CorporateCreditCardSchemaId"
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
                                        HeaderText="User Name" DataField="UserName" SortExpression="UserName" UniqueName="UserName"
                                        FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn
                                        HeaderText="Employee No" DataField="EmpID" SortExpression="EmpID" UniqueName="EmpID"
                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn
                                        HeaderText="Credit Card Number" DataField="CreditCardNumber" SortExpression="CreditCardNumber" UniqueName="CreditCardNumber"
                                        FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridNumericColumn
                                        HeaderText="Credit Limit" DataField="CreditLimit" SortExpression="CreditLimit" UniqueName="CreditLimit"
                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" DataFormatString="{0:$#,##0.00}" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridNumericColumn>
                                    <telerik:GridBoundColumn
                                        HeaderText="Created User" DataField="CreatedUserName" SortExpression="CreatedUserName" UniqueName="CreatedUserName"
                                        FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridDateTimeColumn
                                        HeaderText="Created Date" DataField="CreatedDate" SortExpression="CreatedDate" UniqueName="CreatedDate"
                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                        PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy} {0:t}">
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridBoundColumn
                                        HeaderText="Updated User" DataField="UpdatedUserName" SortExpression="UpdatedUserName" UniqueName="UpdatedUserName"
                                        FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridDateTimeColumn
                                        HeaderText="Updated Date" DataField="UpdatedDate" SortExpression="UpdatedDate" UniqueName="UpdatedDate"
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
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="true" />
                        </telerik:RadGrid>

                        <asp:LinqDataSource ID="LinqDataSourceCorporateCreditCardSchema" runat="server"
                            ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName="" TableName="vwCorporateCreditCardSchemas">
                        </asp:LinqDataSource>

                    </telerik:RadPane>
                </telerik:RadSplitter>
            </telerik:RadPane>

        </telerik:RadSplitter>
    </telerik:RadAjaxPanel>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function ShowPop(id, type) {
                var oWnd = window.radopen('CorporateCreditCardSchemaPop?id=' + id + '&type=' + type, 0, 0, 0, 0);
                var displayWidth = $(window).width() * 0.95;
                var displayHeight = $(window).height() * 0.95;

                if (displayWidth > 1500)
                    displayWidth = 1500;

                oWnd.setSize(displayWidth, displayHeight);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Maximize + Telerik.Web.UI.WindowBehaviors.Resize + Telerik.Web.UI.WindowBehaviors.Move);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function OnClientClose(sender, args) {
                <%= Page.GetPostBackEventReference(ButtonGridRefresh) %>;

            }
        </script>


    </telerik:RadCodeBlock>
</asp:Content>
