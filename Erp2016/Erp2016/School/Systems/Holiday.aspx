<%@ Page Language="C#" MasterPageFile="~/School/Shared/Base.master" AutoEventWireup="true" CodeFile="Holiday.aspx.cs" Inherits="School.Systems.Holiday" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">

        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="TopPane" runat="server" Height="27px" Scrolling="None">
                <h4>Holiday Information</h4>
            </telerik:RadPane>

            <telerik:RadPane ID="GridPane" runat="server" Scrolling="Both">
                <telerik:RadCalendar ID="RadCalendar1" runat="server" Width="100%" Height="100%" EnableWeekends="False" AutoPostBack="True" OnLoad="RadCalendar1_OnLoad" MultiViewColumns="4" MultiViewRows="3" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                </telerik:RadCalendar>
            </telerik:RadPane>

            <telerik:RadSplitBar ID="Radslitbar1" runat="server" CollapseMode="Both" EnableResize="True"></telerik:RadSplitBar>

            <telerik:RadPane ID="RadPane1" runat="server" Scrolling="None">

                <telerik:RadGrid ID="RadGridHoliday" Height="100%" runat="server" AllowAutomaticDeletes="true" AllowAutomaticInserts="true" AllowAutomaticUpdates="true" OnPreRender="RadGridHoliday_PreRender"
                    PageSize="20" AllowPaging="false" AutoGenerateColumns="false" OnBatchEditCommand="RadGridHoliday_BatchEditCommand" DataSourceID="sqlDataSource1" ShowFooter="false" OnItemDataBound="RadGridHoliday_ItemDataBound" AllowFilteringByColumn="True" OnFilterCheckListItemsRequested="RadGridHoliday_OnFilterCheckListItemsRequested"
                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="HolidayId" ClientDataKeyNames="HolidayId" DataSourceID="sqlDataSource1"
                        HorizontalAlign="NotSet" EditMode="Batch" AutoGenerateColumns="false" CommandItemSettings-AddNewRecordText="Add Holiday" CommandItemSettings-SaveChangesText="Save Changes Holiday Information">
                        <BatchEditingSettings EditType="Row" />
                        <Columns>
                            <telerik:GridTemplateColumn
                                HeaderText="Holiday" DataField="Name" SortExpression="Name" UniqueName="Name"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbHoliday" Text='<%# Eval("Name") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <span>
                                        <telerik:RadTextBox runat="server" ID="tbHoliday"></telerik:RadTextBox>
                                    </span>
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn
                                HeaderText="Abbreviation" DataField="Abbreviation" SortExpression="Abbreviation" UniqueName="Abbreviation"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbAbbreviation" Text='<%# Eval("Abbreviation") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <span>
                                        <telerik:RadTextBox runat="server" ID="txtAbbreviation"></telerik:RadTextBox>
                                    </span>
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn
                                HeaderText="Holiday" DataField="HolidayDate" SortExpression="HolidayDate" UniqueName="HolidayDate"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                <ItemTemplate>
                                    <asp:Label Text='<%# string.Format("{0:MM-dd-yyyy}", DataBinder.Eval(Container.DataItem, "HolidayDate")) %>' Width="100%" runat="server" />

                                </ItemTemplate>
                                <EditItemTemplate>
                                    <telerik:RadDatePicker runat="server" ID="txtHolidayDate" Width="100%" Culture="English (Canada)">
                                        <ClientEvents OnDateSelected="DayTime_OnDateSelected" />
                                        <DateInput DateFormat="MM-dd-yyyy" />
                                    </telerik:RadDatePicker>
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn
                                HeaderText="Province" DataField="Province" SortExpression="Province" UniqueName="Province"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbProvince" Text='<%# Eval("Province") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <telerik:RadComboBox runat="server" ID="txtProvince">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="ON" Value="ON" />
                                            <telerik:RadComboBoxItem Text="BC" Value="BC" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn
                                HeaderText="Year" DataField="Year" SortExpression="Year" UniqueName="Year"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("Year") %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridButtonColumn ConfirmText="Delete this Item?" ConfirmDialogType="RadWindow" ConfirmDialogHeight="150px"
                                ConfirmTitle="Delete" HeaderText="Del" HeaderStyle-Width="30px" ButtonType="ImageButton"
                                CommandName="Delete" Text="Delete" UniqueName="DeleteColumn">
                            </telerik:GridButtonColumn>
                        </Columns>
                    </MasterTableView>

                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="True" />
                        <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                            ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                    </ClientSettings>
                    <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                </telerik:RadGrid>
                <asp:LinqDataSource ID="sqlDataSource1" runat="server" ContextTypeName="Erp2016.Lib.linqDBDataContext" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" TableName="Holidays">
                </asp:LinqDataSource>

            </telerik:RadPane>
        </telerik:RadSplitter>

    </telerik:RadAjaxPanel>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            // jQuery
            $(window).bind("load", function () {
                SetValue();
            });

            function ToolbarClick(sender, args) {
                var button = args.get_item();

                if (button.get_text() == "Delete") {
                    if (!confirm('Do you want to delete it?'))
                        args.set_cancel(true);
                }
            }

            function SetValue() {
                var grid = $find("<%= RadGridHoliday.ClientID %>"); //grid id

                if (grid) {
                    var masterTable = grid.get_masterTableView();
                    var rows = masterTable.get_dataItems();

                    for (var i = 0; i < rows.length; i++) {

                        var holy = GetCellValueForRadDatePicker(rows[i], "HolidayDate", "txtHolidayDate");

                        var tempHoli = new Date(holy);

                        var holyYear = tempHoli.getFullYear();

                        SetCellValue(rows[i], "Year", holyYear);
                    }
                }
            }

            function DayTime_OnDateSelected(sender, eventArgs) {
                SetValue();
            }

            function GetCellValueForRadDatePicker(row, columnUniqueName, controlId) {
                var value;
                var testControl = row.findControl(controlId);
                if (testControl) {
                    value = testControl.get_selectedDate();
                } else {
                    value = new Date(row.get_cell(columnUniqueName).innerText);
                }
                return value;
            }

            function SetCellValue(row, columnUniqueName, value) {
                row.get_cell(columnUniqueName).innerText = value;
            }

        </script>
    </telerik:RadCodeBlock>

</asp:Content>
