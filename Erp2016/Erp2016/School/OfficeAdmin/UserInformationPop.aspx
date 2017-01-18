<%@ Page Title="User Permission" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="UserInformationPop.aspx.cs" Inherits="School.OfficeAdmin.UserInformationPop" %>

<%@ Register TagPrefix="UserControl" TagName="FileDownloadList" Src="~/App_Data/FileDownloadList.ascx" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">

        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal" ResizeMode="EndPane">

            <telerik:RadPane runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="RadToolBarUserInformation" runat="server" OnClientButtonClicking="ToolbarButtonClick" OnButtonClick="RadToolBarUserInformation_OnButtonClick">
                    <Items>
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_save.png" Text="Save" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                        <telerik:RadToolBarButton Text="Close" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <telerik:RadPane ID="RadPane2" runat="server" Height="50%" Scrolling="None">

                <telerik:RadSplitter runat="server" ID="RadSplitter2" Height="100%" Width="100%" Orientation="Vertical">

                    <telerik:RadPane runat="server" Height="50%">
                        <div class="formStyle3">
                            <fieldset>
                                <legend><b>Employee Information</b></legend>
                                <div>
                                    <label><b style="color: red">*</b> Actual Hire Date</label>
                                    <telerik:RadDatePicker runat="server" ID="RadDatePickerActualHireDate" DateInput-DisplayDateFormat="MMM dd, yyyy" CssClass="RadSizeMiddle" DateInput-EmptyMessage="Actual Hire Date" MinDate="01/01/1900" MaxDate="01/01/3000" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="RadDatePickerActualHireDate" Display="Dynamic" ErrorMessage="Actual Hire Date Required" ForeColor="Red" ValidationGroup="Info" />
                                    <br style="clear: both;" />
                                </div>
                                <div>
                                    <label><b style="color: red">*</b> Job Type</label>
                                    <telerik:RadComboBox runat="server" ID="RadComboBoxJobType" CssClass="RadSizeMiddle" DataTextField="Name" DataValueField="Value" DataSourceID="LinqDataSourceCondition"></telerik:RadComboBox>
                                    <asp:LinqDataSource ID="LinqDataSourceCondition" runat="server" ContextTypeName="Erp2016.Lib.linqDBDataContext" EntityTypeName="" TableName="Dicts"
                                        Where="DictType == @DictType">
                                        <WhereParameters>
                                            <asp:Parameter DefaultValue="1362" Name="DictType" Type="Int32" />
                                        </WhereParameters>
                                    </asp:LinqDataSource>
                                </div>
                                <div>
                                    <label>1.</label>
                                    <telerik:RadCheckBox runat="server" ID="CheckBoxBank" Text="Bank Info Attached" Value="0" AutoPostBack="False" />
                                    <br style="clear: both;" />
                                </div>
                                <div>
                                    <label>2.</label>
                                    <telerik:RadCheckBox runat="server" ID="CheckBoxOfferLetter" Text="Offer Letter Signed" Value="0" AutoPostBack="False" />
                                    <br style="clear: both;" />
                                </div>
                                <div>
                                    <label>3.</label>
                                    <telerik:RadCheckBox runat="server" ID="CheckBoxDiploma" Text="Copy of Diploma" Value="0" AutoPostBack="False" />
                                    <br style="clear: both;" />
                                </div>
                                <div>
                                    <label>4.</label>
                                    <telerik:RadCheckBox runat="server" ID="CheckBoxResume" Text="Copy of Resume" Value="0" AutoPostBack="False" />
                                    <br style="clear: both;" />
                                </div>
                            </fieldset>

                            <UserControl:FileDownloadList ID="FileDownloadList1" runat="server" />
                        </div>
                    </telerik:RadPane>

                    <telerik:RadSplitBar ID="Radsplitbar2" runat="server" CollapseMode="Both" EnableResize="true" />

                    <telerik:RadPane runat="server" Scrolling="None">
                        <telerik:RadSplitter runat="server" ID="RadSplitter6" Height="100%" Width="100%" Orientation="Horizontal">

                            <telerik:RadPane runat="server" Height="27px" Scrolling="None">
                                <h4>User Status</h4>
                            </telerik:RadPane>

                            <telerik:RadPane runat="server" Scrolling="None">
                                <telerik:RadGrid ID="RadGridUserStatus" runat="server" AllowAutomaticDeletes="true" AllowAutomaticInserts="true" AllowAutomaticUpdates="true" Height="100%"
                                    PageSize="20" AllowPaging="false" AutoGenerateColumns="false" OnBatchEditCommand="RadGridUserStatus_OnBatchEditCommand" DataSourceID="LinqDataSourceUserStatus" ShowFooter="False" AllowFilteringByColumn="True" OnFilterCheckListItemsRequested="RadGridUserStatus_OnFilterCheckListItemsRequested"
                                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="UserStatusId" DataSourceID="LinqDataSourceUserStatus"
                                        EditMode="Batch" AutoGenerateColumns="false">
                                        <BatchEditingSettings EditType="Row" />
                                        <Columns>
                                            <telerik:GridTemplateColumn
                                                HeaderText="Type" DataField="Type" SortExpression="Type" UniqueName="Type" HeaderStyle-HorizontalAlign="Center" DefaultInsertValue="Dismissal"
                                                AllowFiltering="False" FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" HeaderStyle-Width="30%">
                                                <ItemTemplate>
                                                    <telerik:RadDropDownList runat="server" DataValueField="Value" DataTextField="Name" DataSourceID="LinqDataSourceUserStatusType" SelectedValue='<%# Eval("Type") %>' Enabled="false" Width="100%" ToolTip='<%# Eval("Type") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadComboBox Width="100%" runat="server" AutoPostBack="False" DataValueField="Value" DataTextField="Name" DataSourceID="LinqDataSourceUserStatusType" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridTemplateColumn
                                                HeaderText="Issued Date" DataField="IssuedDate" SortExpression="IssuedDate" UniqueName="IssuedDate"
                                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# string.Format("{0:MM-dd-yyyy}", DataBinder.Eval(Container.DataItem, "IssuedDate")) %>' Width="100%" runat="server" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <span>
                                                        <telerik:RadDatePicker DateInput-DisplayDateFormat="MMM dd, yyyy" runat="server" Width="100%" />
                                                    </span>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridTemplateColumn
                                                HeaderText="ROE Issue" DataField="ROEIssue" SortExpression="ROEIssue" UniqueName="ROEIssue" HeaderStyle-HorizontalAlign="Center" DefaultInsertValue="A - Shortage of Work"
                                                AllowFiltering="False" FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" HeaderStyle-Width="30%">
                                                <ItemTemplate>
                                                    <telerik:RadDropDownList runat="server" DataValueField="Value" DataTextField="Name" DataSourceID="LinqDataSourceUserStatusRoeIssue" SelectedValue='<%# Eval("ROEIssue") %>' Enabled="false" Width="100%" ToolTip='<%# Eval("ROEIssue") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadComboBox Width="100%" runat="server" AutoPostBack="False" DataValueField="Value" DataTextField="Name" DataSourceID="LinqDataSourceUserStatusRoeIssue" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridTemplateColumn
                                                HeaderText="Reason" DataField="Reason" SortExpression="Reason" UniqueName="Reason"
                                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Eval("Reason") %>' Width="100%"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <span>
                                                        <telerik:RadTextBox runat="server" Width="100%"></telerik:RadTextBox>
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
                                        <Resizing AllowColumnResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                                            ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                                    </ClientSettings>
                                </telerik:RadGrid>

                                <asp:LinqDataSource ID="LinqDataSourceUserStatus" runat="server" ContextTypeName="Erp2016.Lib.linqDBDataContext" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" TableName="UserStatus"
                                    Where="UserId == @UserId" OrderBy="IssuedDate">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="0" Name="UserId" Type="Int32" />
                                    </WhereParameters>
                                </asp:LinqDataSource>

                                <asp:LinqDataSource ID="LinqDataSourceUserStatusType" runat="server" ContextTypeName="Erp2016.Lib.linqDBDataContext" EntityTypeName="" TableName="Dicts"
                                    Where="DictType == @DictType">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="1406" Name="DictType" Type="Int32" />
                                    </WhereParameters>
                                </asp:LinqDataSource>

                                <asp:LinqDataSource ID="LinqDataSourceUserStatusRoeIssue" runat="server" ContextTypeName="Erp2016.Lib.linqDBDataContext" EntityTypeName="" TableName="Dicts"
                                    Where="DictType == @DictType">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="1412" Name="DictType" Type="Int32" />
                                    </WhereParameters>
                                </asp:LinqDataSource>

                            </telerik:RadPane>

                        </telerik:RadSplitter>
                    </telerik:RadPane>
                </telerik:RadSplitter>
            </telerik:RadPane>

            <telerik:RadSplitBar ID="Radsplitbar1" runat="server" CollapseMode="Both" EnableResize="true" />

            <telerik:RadPane runat="server" Scrolling="None">

                <telerik:RadSplitter runat="server" ID="RadSplitter4" Height="100%" Width="100%" Orientation="Vertical">
                    <telerik:RadPane runat="server" Scrolling="None">
                        <telerik:RadSplitter runat="server" ID="RadSplitter3" Height="100%" Width="100%" Orientation="Horizontal">

                            <telerik:RadPane runat="server" Height="27px" Scrolling="None">
                                <h4>Vacation Information</h4>
                            </telerik:RadPane>

                            <telerik:RadPane runat="server" Scrolling="None">
                                <telerik:RadGrid ID="RadGridVacationSchema" runat="server" AllowAutomaticDeletes="true" AllowAutomaticInserts="true" AllowAutomaticUpdates="true" Height="100%"
                                    PageSize="20" AllowPaging="false" AutoGenerateColumns="false" OnBatchEditCommand="RadGridVacationSchema_OnBatchEditCommand" DataSourceID="sqlDataSourceVacationSchema" ShowFooter="False" AllowFilteringByColumn="True" OnFilterCheckListItemsRequested="RadGridVacationSchema_OnFilterCheckListItemsRequested"
                                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="VacationSchemaId" DataSourceID="sqlDataSourceVacationSchema"
                                        EditMode="Batch" AutoGenerateColumns="false">
                                        <BatchEditingSettings EditType="Row" />
                                        <Columns>
                                            <telerik:GridTemplateColumn
                                                HeaderText="Vacation Type" DataField="VacationType" SortExpression="VacationType" UniqueName="VacationType" HeaderStyle-HorizontalAlign="Center" DefaultInsertValue="Paid Vacation"
                                                AllowFiltering="False" FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" HeaderStyle-Width="30%">
                                                <ItemTemplate>
                                                    <telerik:RadDropDownList runat="server" DataValueField="Value" DataTextField="Name" DataSourceID="sqlDataSourceVacationType" SelectedValue='<%# Eval("VacationType") %>' Enabled="false" Width="100%" ToolTip='<%# Eval("VacationType") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadComboBox Width="100%" runat="server" AutoPostBack="False" DataValueField="Value" DataTextField="Name" DataSourceID="sqlDataSourceVacationType" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridTemplateColumn
                                                HeaderText="Date" DataField="Date" SortExpression="Date" UniqueName="Date"
                                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# string.Format("{0:MM-dd-yyyy}", DataBinder.Eval(Container.DataItem, "Date")) %>' Width="100%" runat="server" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <span>
                                                        <telerik:RadDatePicker DateInput-DisplayDateFormat="MMM dd, yyyy" runat="server" Width="100%" />
                                                    </span>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridTemplateColumn
                                                HeaderText="Total Days" DataField="TotalDays" SortExpression="TotalDays" UniqueName="TotalDays"
                                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="LabelTotalDays" Text='<%# Eval("TotalDays") %>' Width="100%" runat="server" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <span>
                                                        <telerik:RadNumericTextBox runat="server" Type="Number" Width="100%" />
                                                    </span>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridTemplateColumn
                                                HeaderText="Remark" DataField="Remark" SortExpression="Remark" UniqueName="Remark"
                                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Eval("Remark") %>' Width="100%"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <span>
                                                        <telerik:RadTextBox runat="server" Width="100%" />
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
                                        <Resizing AllowColumnResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                                            ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                                    </ClientSettings>
                                </telerik:RadGrid>

                                <asp:LinqDataSource ID="sqlDataSourceVacationSchema" runat="server" ContextTypeName="Erp2016.Lib.linqDBDataContext" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" TableName="VacationSchemas"
                                    Where="UserId == @UserId" OrderBy="VacationType, Date">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="0" Name="UserId" Type="Int32" />
                                    </WhereParameters>
                                </asp:LinqDataSource>

                                <asp:LinqDataSource ID="sqlDataSourceVacationType" runat="server" ContextTypeName="Erp2016.Lib.linqDBDataContext" EntityTypeName="" TableName="Dicts"
                                    Where="DictType == @DictType">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="1376" Name="DictType" Type="Int32" />
                                    </WhereParameters>
                                </asp:LinqDataSource>
                            </telerik:RadPane>

                        </telerik:RadSplitter>
                    </telerik:RadPane>

                    <telerik:RadSplitBar ID="Radsplitbar3" runat="server" CollapseMode="Both" EnableResize="true" />

                    <telerik:RadPane runat="server" Scrolling="None">
                        <telerik:RadSplitter runat="server" ID="RadSplitter5" Height="100%" Width="100%" Orientation="Horizontal">

                            <telerik:RadPane runat="server" Height="27px" Scrolling="None">
                                <h4>Salary Information</h4>
                            </telerik:RadPane>

                            <telerik:RadPane runat="server" Scrolling="None">
                                <telerik:RadGrid ID="RadGridUserSalary" runat="server" AllowAutomaticDeletes="true" AllowAutomaticInserts="true" AllowAutomaticUpdates="true" Height="100%"
                                    PageSize="20" AllowPaging="false" AutoGenerateColumns="false" OnBatchEditCommand="RadGridUserSalary_OnBatchEditCommand" DataSourceID="LinqDataSourceUserSalary" ShowFooter="False" AllowFilteringByColumn="True" OnFilterCheckListItemsRequested="RadGridUserSalary_OnFilterCheckListItemsRequested"
                                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="UserSalaryId" DataSourceID="LinqDataSourceUserSalary"
                                        EditMode="Batch" AutoGenerateColumns="false">
                                        <BatchEditingSettings EditType="Row" />
                                        <Columns>
                                            <telerik:GridTemplateColumn
                                                HeaderText="Salary Type" DataField="SalaryType" SortExpression="SalaryType" UniqueName="SalaryType" HeaderStyle-HorizontalAlign="Center" DefaultInsertValue="Salary"
                                                AllowFiltering="False" FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" HeaderStyle-Width="30%">
                                                <ItemTemplate>
                                                    <telerik:RadDropDownList runat="server" DataValueField="Value" DataTextField="Name" DataSourceID="LinqDataSourceSalaryType" SelectedValue='<%# Eval("SalaryType") %>' Enabled="false" Width="100%" ToolTip='<%# Eval("SalaryType") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadComboBox Width="100%" runat="server" AutoPostBack="False" DataValueField="Value" DataTextField="Name" DataSourceID="LinqDataSourceSalaryType" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridTemplateColumn
                                                HeaderText="Effect Date" DataField="EffectDate" SortExpression="EffectDate" UniqueName="EffectDate"
                                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# string.Format("{0:MM-dd-yyyy}", DataBinder.Eval(Container.DataItem, "EffectDate")) %>' Width="100%" runat="server" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <span>
                                                        <telerik:RadDatePicker DateInput-DisplayDateFormat="MMM dd, yyyy" runat="server" Width="100%" />
                                                    </span>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridTemplateColumn
                                                HeaderText="Salary" DataField="Salary" SortExpression="Salary" UniqueName="Salary"
                                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# string.Format("{0:$#,##0.00}", Eval("Salary")) %>' Width="100%" runat="server" Style="text-align: right;" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <span>
                                                        <telerik:RadNumericTextBox runat="server" Type="Number" Width="100%" />
                                                    </span>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridTemplateColumn
                                                HeaderText="Remark" DataField="Remark" SortExpression="Remark" UniqueName="Remark"
                                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Eval("Remark") %>' Width="100%"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <span>
                                                        <telerik:RadTextBox runat="server" Width="100%"></telerik:RadTextBox>
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
                                        <Resizing AllowColumnResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                                            ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                                    </ClientSettings>
                                </telerik:RadGrid>

                                <asp:LinqDataSource ID="LinqDataSourceUserSalary" runat="server" ContextTypeName="Erp2016.Lib.linqDBDataContext" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" TableName="UserSalaries"
                                    Where="UserId == @UserId" OrderBy="EffectDate">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="0" Name="UserId" Type="Int32" />
                                    </WhereParameters>
                                </asp:LinqDataSource>

                                <asp:LinqDataSource ID="LinqDataSourceSalaryType" runat="server" ContextTypeName="Erp2016.Lib.linqDBDataContext" EntityTypeName="" TableName="Dicts"
                                    Where="DictType == @DictType">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="1399" Name="DictType" Type="Int32" />
                                    </WhereParameters>
                                </asp:LinqDataSource>
                            </telerik:RadPane>

                        </telerik:RadSplitter>
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
                else if (window.frameElement && window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }

            function Close() {
                var wnd = GetRadWindow();
                wnd.close();
            }

            function ToolbarButtonClick(sender, args) {
                var button = args.get_item();
                //if (button.get_text() === "Update") {
                //    if (!confirm('Do you want to update?'))
                //        args.set_cancel(true);
                //}
            }


        </script>
    </telerik:RadCodeBlock>

</asp:Content>
