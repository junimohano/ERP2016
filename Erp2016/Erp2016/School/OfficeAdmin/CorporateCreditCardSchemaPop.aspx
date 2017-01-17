<%@ Page Title="Corporate Credit Card Schema" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="CorporateCreditCardSchemaPop.aspx.cs" Inherits="School.OfficeAdmin.CorporateCreditCardSchemaPop" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">

        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="RadPane2" runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="RadToolBar1" runat="server" OnClientButtonClicking="ToolbarButtonClick" OnButtonClick="RadToolBar1_OnButtonClick">
                    <Items>
                        <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_new.png" Text="Save" ValidationGroup="Info" />
                        <telerik:RadToolBarButton IsSeparator="True" />
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane1" runat="server" Scrolling="None">
                <telerik:RadGrid ID="RadGridUser" runat="server" AllowFilteringByColumn="True" AutoGenerateColumns="False" PageSize="20" Height="100%"
                    AllowPaging="True" AllowSorting="True" DataSourceID="LinqDataSourceUser" ShowFooter="False" OnSelectedIndexChanged="RadGridUser_OnSelectedIndexChanged" OnFilterCheckListItemsRequested="RadGridUser_OnFilterCheckListItemsRequested"
                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                    <MasterTableView DataKeyNames="UserId" TableLayout="Fixed" DataSourceID="LinqDataSourceUser">
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
                                HeaderText="Employee Number" DataField="EmployeeNumber" SortExpression="EmployeeNumber" UniqueName="EmployeeNumber"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="User Name" DataField="UserName" SortExpression="UserName" UniqueName="UserName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Position" DataField="PositionName" SortExpression="PositionName" UniqueName="PositionName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnablePostBackOnRowClick="true">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                        <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                            ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                    </ClientSettings>
                    <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                </telerik:RadGrid>
                <asp:LinqDataSource ID="LinqDataSourceUser" runat="server"
                    ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName="" OrderBy="CreatedDate DESC"
                    TableName="vwUsers"
                    Where="UserId == @UserId">
                    <WhereParameters>
                        <asp:Parameter DefaultValue="0" Name="UserId" Type="Int32" />
                    </WhereParameters>
                </asp:LinqDataSource>
            </telerik:RadPane>

            <telerik:RadPane runat="server" Height="150px">
                <div class="formStyle3">
                    <fieldset>
                        <legend>Corporate Credit Card Info</legend>
                        <div>
                            <label><b style="color: red">*</b> Credit Card Number</label>
                            <telerik:RadTextBox ID="RadTextBoxCreditCardNumber" CssClass="RadSizeMiddle" runat="server" />
                            <br style="clear: both;" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RadTextBoxCreditCardNumber" Display="Dynamic" ErrorMessage="Credit Card Number Required" ForeColor="Red" ValidationGroup="Info" />
                        </div>
                        <div>
                            <label><b style="color: red">*</b> Credit Limit</label>
                            <telerik:RadNumericTextBox ID="RadNumericTextBoxCreditLimit" Type="Currency" Value="0" CssClass="RadSizeMiddle" runat="server"></telerik:RadNumericTextBox>
                            <br style="clear: both;" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RadNumericTextBoxCreditLimit" Display="Dynamic" ErrorMessage="Credit Limit Required" ForeColor="Red" ValidationGroup="Info" />
                        </div>
                    </fieldset>
                </div>
            </telerik:RadPane>

        </telerik:RadSplitter>
    </telerik:RadAjaxPanel>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

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

            function ToolbarButtonClick(sender, args) {
                var button = args.get_item();
                if (button.get_text() === "Save") {
                    if (!confirm('Do you want to save it?'))
                        args.set_cancel(true);
                }
            }

        </script>
    </telerik:RadCodeBlock>
</asp:Content>
