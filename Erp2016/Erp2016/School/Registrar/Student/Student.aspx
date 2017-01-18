<%@ Page Title="Student" Language="C#" MasterPageFile="~/School/Shared/Base.master" AutoEventWireup="true" CodeFile="Student.aspx.cs" Inherits="School.Registrar.Student.Student" %>

<%@ Register TagPrefix="UserControl" TagName="FileDownloadList" Src="~/App_Data/FileDownloadList.ascx" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">
        <%-- ClientEvents-OnRequestStart="onRequestStart" --%>

        <div style="display: none">
            <asp:Button ID="btnRefresh" runat="server" OnClick="Refresh" />
            <asp:Button ID="ButtonGridRefresh" runat="server" OnClick="ButtonGridRefresh_OnClick" />
        </div>

        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="Radpane1" runat="server" Height="40%" Scrolling="None">

                <telerik:RadSplitter runat="server" ID="RadSplitter5" Orientation="Horizontal">

                    <telerik:RadPane ID="Radpane7" runat="server" Height="27px" Scrolling="None">
                        <h4>Student list</h4>
                    </telerik:RadPane>

                    <telerik:RadPane ID="Radpane3" runat="server" Height="40px" Scrolling="None">
                        <telerik:RadToolBar ID="RadToolBar3" runat="server" OnButtonClick="RadToolBar3_OnButtonClick">
                            <Items>
                                <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_new.png" Text="New Student" />
                                <telerik:RadToolBarButton IsSeparator="true" />
                                <telerik:RadToolBarButton ImageUrl="~/assets/img/Invoice-20.png" Text="Invoice Page" ToolTip="Invoice Page" />
                                <telerik:RadToolBarButton ImageUrl="~/assets/img/Paid-20.png" Text="Payment Page" ToolTip="Payment Page" />
                                <telerik:RadToolBarButton ImageUrl="~/assets/img/Debt-20.png" Text="Deposit Page" ToolTip="Deposit Page" />
                                <telerik:RadToolBarButton ImageUrl="~/assets/img/Sales Performance-20.png" Text="CreditMemo Page" ToolTip="CreditMemo Page" />
                                <telerik:RadToolBarButton ImageUrl="~/assets/img/Receive Cash-20.png" Text="Refund Page" ToolTip="Refund Page" />
                                <telerik:RadToolBarButton IsSeparator="true" />
                            </Items>
                        </telerik:RadToolBar>
                    </telerik:RadPane>

                    <telerik:RadPane ID="Radpane6" runat="server" Scrolling="None">

                        <telerik:RadGrid ID="RadGridStudentList" runat="server" AllowFilteringByColumn="True"
                            AllowPaging="True" AllowSorting="True" AllowMultiRowSelection="False" AutoGenerateColumns="False" OnSelectedIndexChanged="RadGridStudentList_OnSelectedIndexChanged"
                            PageSize="20" DataSourceID="LinqDataSourceStudents" ShowFooter="false" Height="100%" OnFilterCheckListItemsRequested="RadGridStudentList_OnFilterCheckListItemsRequested"
                            FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                            <MasterTableView DataKeyNames="StudentId, StudentNo" ClientDataKeyNames="StudentId" TableLayout="Fixed" DataSourceID="LinqDataSourceStudents" AllowMultiColumnSorting="True">
                                <Columns>
                                    <telerik:GridBoundColumn
                                        HeaderText="Student No" DataField="StudentNo" SortExpression="StudentNo" UniqueName="StudentNo"
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
                                        HeaderText="Student Name" DataField="StudentName" SortExpression="StudentName" UniqueName="StudentName"
                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn
                                        HeaderText="Segregation" DataField="Segregation" SortExpression="Segregation" UniqueName="Segregation"
                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn
                                        HeaderText="Marketer Name" DataField="MarketerName" SortExpression="MarketerName" UniqueName="MarketerName"
                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn
                                        HeaderText="Country" DataField="CountryName" SortExpression="CountryName" UniqueName="CountryName"
                                        FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridDateTimeColumn
                                        HeaderText="Date of birth" DataField="DOB" SortExpression="DOB" UniqueName="DOB"
                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                        PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy}">
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
                                    <telerik:GridCheckBoxColumn
                                        HeaderText="IsActive" DataField="IsActive" SortExpression="IsActive" UniqueName="IsActive"
                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridCheckBoxColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings EnablePostBackOnRowClick="true">
                                <Selecting AllowRowSelect="true" />
                                <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
                                <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                                    ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                            </ClientSettings>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                        </telerik:RadGrid>
                        <asp:LinqDataSource ID="LinqDataSourceStudents" runat="server"
                            ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName="" OrderBy="StudentId Descending"
                            TableName="vwStudents"
                            Where="StudentId == @StudentId">
                            <WhereParameters>
                                <asp:Parameter DefaultValue="0" Name="StudentId" Type="Int32" />
                            </WhereParameters>
                        </asp:LinqDataSource>

                    </telerik:RadPane>
                </telerik:RadSplitter>
            </telerik:RadPane>

            <telerik:RadSplitBar ID="Radsplitbar1" runat="server" CollapseMode="Both" EnableResize="true" />

            <telerik:RadPane ID="Radpane5" runat="server" Scrolling="None">
                <telerik:RadTabStrip ID="RadTabStript1" runat="server" MultiPageID="RadMultiPage1" AutoPostBack="True" OnTabClick="RadTabStript1_OnTabClick">
                    <Tabs>
                        <telerik:RadTab Text="Profile" PageViewID="RadPageView1" Selected="True" />
                        <telerik:RadTab Text="Contract" PageViewID="RadPageView2" />
                    </Tabs>
                </telerik:RadTabStrip>

                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" RenderSelectedPageOnly="True">

                    <telerik:RadPageView ID="RadPageView1" runat="server" Selected="True">

                        <telerik:RadSplitter ID="RadSplitter4" runat="server" Orientation="Horizontal">

                            <telerik:RadPane runat="server" Height="40px" Scrolling="None">
                                <telerik:RadToolBar ID="RadToolBar1" runat="server"
                                    OnClientButtonClicking="ToolbarButtonClick" OnButtonClick="StudentButtonClicked">
                                    <Items>
                                        <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_save.png" Text="Update" Enabled="True" ValidationGroup="Info" />
                                        <telerik:RadToolBarButton IsSeparator="true" />
                                        <telerik:RadToolBarButton Enabled="false" ImageUrl="~/assets/img/bt_delete.png" Text="Delete" ToolTip="Delete" Value="Confirm" />
                                        <telerik:RadToolBarButton IsSeparator="true" />
                                    </Items>
                                </telerik:RadToolBar>
                            </telerik:RadPane>

                            <telerik:RadPane runat="server">
                                <div class="formStyle3">

                                    <div style="float: left; width: 450px;">
                                        <fieldset>
                                            <legend>Basic Information 1</legend>

                                            <div>
                                                <label><b style="color: red">*</b> Site</label>
                                                <telerik:RadComboBox ID="RadComboBoxSite" EmptyMessage="Choose a Site" CssClass="RadSizeMiddle" runat="server" AutoPostBack="true" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" OnSelectedIndexChanged="RadComboBoxSite_OnSelectedIndexChanged" />
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator20" ControlToValidate="RadComboBoxSite" Display="Dynamic" ErrorMessage="Site Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>
                                            <div>
                                                <label><b style="color: red">*</b> Site Location</label>
                                                <telerik:RadComboBox ID="RadComboBoxSiteLocation" EmptyMessage="Choose a Site Location" CssClass="RadSizeMiddle" runat="server" AutoPostBack="False" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="RadComboBoxSiteLocation" Display="Dynamic" ErrorMessage="Site Location Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>

                                            <div>
                                                <label>Marketer Name</label>
                                                <telerik:RadComboBox ID="ddlmarketer" CssClass="RadSizeMiddle" runat="server" EmptyMessage="Choose a marketer" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                                <br style="clear: both;" />
                                            </div>
                                            <%--           <div>
                                                <label>Student Master No.</label><telerik:RadTextBox ID="tbStudentMasterNo" Width="50%" runat="server" ReadOnly="true" />
                                            </div>--%>
                                            <div>
                                                <label><b style="color: red">*</b> First Name</label>
                                                <telerik:RadTextBox ID="tbFirstName" CssClass="RadSizeMiddle" runat="server" />
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ID="tbFirstNameVali" ControlToValidate="tbFirstName" Display="Dynamic" ErrorMessage="First Name Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>

                                            <div>
                                                <label>Middle Name 1</label>
                                                <telerik:RadTextBox ID="tbMiddleName1" CssClass="RadSizeMiddle" runat="server" EmptyMessage="(Optional)" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Middle Name 2</label>
                                                <telerik:RadTextBox ID="tbMiddleName2" CssClass="RadSizeMiddle" runat="server" EmptyMessage="(Optional)" />
                                                <br style="clear: both;" />
                                            </div>

                                            <div>
                                                <label><b style="color: red">*</b> Last Name1</label>
                                                <telerik:RadTextBox ID="tbLastName1" CssClass="RadSizeMiddle" runat="server" />
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ID="tbLastName1Vali" ControlToValidate="tbLastName1" Display="Dynamic" ErrorMessage="Last Name Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>
                                            <div>
                                                <label>Last Name2</label>
                                                <telerik:RadTextBox ID="tbLastName2" CssClass="RadSizeMiddle" runat="server" EmptyMessage="(Optional)" />
                                                <br style="clear: both;" />
                                            </div>

                                            <div>
                                                <label><b style="color: red">*</b> Gender</label>
                                                <telerik:RadComboBox ID="ddlGender" CssClass="RadSizeMiddle" runat="server" AppendDataBoundItems="true" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Male" Value="False" />
                                                        <telerik:RadComboBoxItem Text="Female" Value="True" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                                <br style="clear: both;" />
                                            </div>

                                            <div>
                                                <label><b style="color: red">*</b> Date of Birth</label>
                                                <telerik:RadDatePicker DateInput-DisplayDateFormat="MMM dd, yyyy" ID="tbDateOfBirth" runat="server" CssClass="RadSizeMiddle" DateInput-EmptyMessage="Input Date" MinDate="01/01/1900" MaxDate="01/01/3000" AutoPostBack="False"></telerik:RadDatePicker>
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="tbDateOfBirth" Display="Dynamic" ErrorMessage="Date of Birth Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>
                                        </fieldset>

                                        <fieldset>
                                            <legend>Upload</legend>
                                            <UserControl:FileDownloadList ID="FileDownloadList1" runat="server" />
                                        </fieldset>

                                    </div>

                                    <div style="float: left; width: 450px;">

                                        <fieldset>
                                            <legend>Basic Information 2</legend>
                                            <div>
                                                <label>Age Segregation</label>
                                                <telerik:RadComboBox ID="ddlAgeSegregation" CssClass="RadSizeMiddle" runat="server" Enabled="false" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Adult" Value="1" />
                                                        <telerik:RadComboBoxItem Text="Junior" Value="2" />
                                                        <telerik:RadComboBoxItem Text="Child" Value="3" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                                <br style="clear: both;" />
                                            </div>

                                            <div>
                                                <label><b style="color: red">*</b> Student Type</label>
                                                <telerik:RadComboBox ID="ddlStudentType" CssClass="RadSizeMiddle" runat="server" AppendDataBoundItems="true" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="International" Value="1" />
                                                        <telerik:RadComboBoxItem Text="Domestic" Value="0" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                                <br style="clear: both;" />
                                            </div>

                                            <div>
                                                <label><b style="color: red">*</b> Country</label>
                                                <telerik:RadComboBox ID="ddlCountry" CssClass="RadSizeMiddle" runat="server" AppendDataBoundItems="true" EmptyMessage="Choose a Country"></telerik:RadComboBox>
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlCountry" Display="Dynamic" ErrorMessage="Country Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>

                                            <div>
                                                <label>Phone 1</label>
                                                <telerik:RadTextBox ID="tbPhone1" CssClass="RadSizeMiddle" runat="server" EmptyMessage="(Optional)" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Phone 2</label>
                                                <telerik:RadTextBox ID="tbPhone2" CssClass="RadSizeMiddle" runat="server" EmptyMessage="(Optional)" />
                                                <br style="clear: both;" />
                                            </div>

                                            <div>
                                                <label>Fax</label>
                                                <telerik:RadTextBox ID="tbFax" CssClass="RadSizeMiddle" runat="server" EmptyMessage="(Optional)" />
                                                <br style="clear: both;" />
                                            </div>

                                            <div>
                                                <label>Email 1</label>
                                                <telerik:RadTextBox ID="tbEmail1" CssClass="RadSizeMiddle" runat="server" EmptyMessage="(Optional)" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Email 2</label>
                                                <telerik:RadTextBox ID="tbEmail2" CssClass="RadSizeMiddle" runat="server" EmptyMessage="(Optional)" />
                                                <br style="clear: both;" />
                                            </div>

                                            <div>
                                                <label>Passport No.</label>
                                                <telerik:RadTextBox ID="tbPassport" CssClass="RadSizeMiddle" runat="server" EmptyMessage="(Optional)" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Loan No.</label>
                                                <telerik:RadTextBox ID="tbLoanNo" CssClass="RadSizeMiddle" runat="server" EmptyMessage="(Optional)" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Comment</label>
                                                <telerik:RadTextBox ID="tbComment" TextMode="MultiLine" Height="100" runat="server" />
                                                <br style="clear: both;" />
                                            </div>
                                        </fieldset>
                                    </div>

                                    <div style="float: left; width: 450px;">
                                        <fieldset>
                                            <legend>Address in Canada</legend>
                                            <div>
                                                <label><b style="color: red">*</b> Address</label>
                                                <telerik:RadTextBox ID="tbCadAddress" TextMode="MultiLine" Height="100" runat="server" />
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ID="tbCadAddressVali" ControlToValidate="tbCadAddress" Display="Dynamic" ErrorMessage="Address Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>
                                            <div>
                                                <label><b style="color: red">*</b> City</label>
                                                <telerik:RadTextBox ID="tbCadCity" CssClass="RadSizeMiddle" runat="server" />
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ID="tbCadCityVali" ControlToValidate="tbCadCity" Display="Dynamic" ErrorMessage="City Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>
                                            <div>
                                                <label><b style="color: red">*</b> Province</label>
                                                <telerik:RadTextBox ID="tbCadProvince" CssClass="RadSizeMiddle" runat="server" />
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ID="tbCadProvinceVali" ControlToValidate="tbCadProvince" Display="Dynamic" ErrorMessage="Province Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>
                                            <div>
                                                <label><b style="color: red">*</b> Postal Code</label>
                                                <telerik:RadTextBox ID="tbCadZipcode" CssClass="RadSizeMiddle" runat="server" />
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ID="tbCadZipcodeVali" ControlToValidate="tbCadZipcode" Display="Dynamic" ErrorMessage="Postal Code Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>
                                        </fieldset>

                                        <fieldset>
                                            <legend>Permanent Address</legend>
                                            <div>
                                                <label><b style="color: red">*</b> Address</label>
                                                <telerik:RadTextBox ID="tbPerAddress" Height="100" TextMode="MultiLine" runat="server" />
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbPerAddress" Display="Dynamic" ErrorMessage="Address Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>
                                            <div>
                                                <label><b style="color: red">*</b> City</label>
                                                <telerik:RadTextBox ID="tbPerCity" CssClass="RadSizeMiddle" runat="server" />
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbPerCity" Display="Dynamic" ErrorMessage="City Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>
                                            <div>
                                                <label><b style="color: red">*</b> Province</label>
                                                <telerik:RadTextBox ID="tbPerState" CssClass="RadSizeMiddle" runat="server" />
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbPerState" Display="Dynamic" ErrorMessage="Province Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>
                                            <div>
                                                <label><b style="color: red">*</b> Postal Code</label>
                                                <telerik:RadTextBox ID="tbPerZiocode" CssClass="RadSizeMiddle" runat="server" />
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbPerZiocode" Display="Dynamic" ErrorMessage="Postal Code Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>
                                            <div>
                                                <label><b style="color: red">*</b> Country</label>
                                                <telerik:RadComboBox ID="ddlPerCountry" CssClass="RadSizeMiddle" runat="server" AppendDataBoundItems="true" EmptyMessage="Choose a Country"></telerik:RadComboBox>
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlPerCountry" Display="Dynamic" ErrorMessage="Country Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>
                                        </fieldset>

                                        <fieldset>
                                            <legend>Residential Status</legend>
                                            <div>
                                                <label><b style="color: red">*</b> Status In Canada</label>
                                                <telerik:RadComboBox ID="ddlStatus" CssClass="RadSizeMiddle" runat="server" AppendDataBoundItems="true" EmptyMessage="Choose a Visa" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True"></telerik:RadComboBox>
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlStatus" Display="Dynamic" ErrorMessage="Status In Canada Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>

                                            <div>
                                                <label>Start Date</label>
                                                <telerik:RadDatePicker DateInput-DisplayDateFormat="MMM dd, yyyy" ID="tbStatusStartDate" runat="server" CssClass="RadSizeMiddle" DateInput-EmptyMessage="Input Date" MinDate="01/01/1900" MaxDate="01/01/3000"></telerik:RadDatePicker>
                                                <br style="clear: both;" />
                                            </div>

                                            <div>
                                                <label>End Date</label>
                                                <telerik:RadDatePicker DateInput-DisplayDateFormat="MMM dd, yyyy" ID="tbStatusEndDate" runat="server" CssClass="RadSizeMiddle" DateInput-EmptyMessage="Input Date" MinDate="01/01/1900" MaxDate="01/01/3000"></telerik:RadDatePicker>
                                                <br style="clear: both;" />
                                            </div>
                                        </fieldset>
                                        <br />
                                        <br />
                                    </div>

                                    <div style="float: left; width: 450px;">
                                        <fieldset>
                                            <legend>Work Permit Status</legend>
                                            <div>
                                                <label>Permit Type</label><telerik:RadComboBox ID="ddlPermit" CssClass="RadSizeMiddle" runat="server" AppendDataBoundItems="true" EmptyMessage="Choose a Permit" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True"></telerik:RadComboBox>
                                                <br style="clear: both;" />
                                            </div>

                                            <div>
                                                <label>Start Date</label><telerik:RadDatePicker DateInput-DisplayDateFormat="MMM dd, yyyy" ID="tbPermitStartDate" runat="server" CssClass="RadSizeMiddle" DateInput-EmptyMessage="Input Date" MinDate="01/01/1900" MaxDate="01/01/3000"></telerik:RadDatePicker>
                                                <br style="clear: both;" />
                                            </div>

                                            <div>
                                                <label>End Date</label><telerik:RadDatePicker DateInput-DisplayDateFormat="MMM dd, yyyy" ID="tbPermitEndDate" runat="server" CssClass="RadSizeMiddle" DateInput-EmptyMessage="Input Date" MinDate="01/01/1900" MaxDate="01/01/3000"></telerik:RadDatePicker>
                                                <br style="clear: both;" />
                                            </div>
                                        </fieldset>

                                        <fieldset>
                                            <legend>Insurance</legend>
                                            <div>
                                                <label><b style="color: red">*</b> Insurance Required</label><telerik:RadComboBox ID="ddlInsurance" CssClass="RadSizeMiddle" runat="server" AppendDataBoundItems="true" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="No" Value="False" />
                                                        <telerik:RadComboBoxItem Text="Yes" Value="True" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                                <br style="clear: both;" />
                                            </div>

                                            <div>
                                                <label>Start Date</label><telerik:RadDatePicker DateInput-DisplayDateFormat="MMM dd, yyyy" ID="tbInsuranceStartDate" runat="server" CssClass="RadSizeMiddle" DateInput-EmptyMessage="Exemption Date" MinDate="01/01/1900" MaxDate="01/01/3000"></telerik:RadDatePicker>
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>End Date</label><telerik:RadDatePicker DateInput-DisplayDateFormat="MMM dd, yyyy" ID="tbInsuranceEndtDate" runat="server" CssClass="RadSizeMiddle" DateInput-EmptyMessage="Exemption Date" MinDate="01/01/1900" MaxDate="01/01/3000"></telerik:RadDatePicker>
                                                <br style="clear: both;" />
                                            </div>

                                            <div>
                                                <label>Daily Fee</label><telerik:RadTextBox ID="tbInsuranceDayFee" CssClass="RadSizeMiddle" runat="server" EmptyMessage="(Optional)" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Total Amount</label><telerik:RadTextBox ID="tbInsuranceTotalAmt" CssClass="RadSizeMiddle" runat="server" EmptyMessage="(Optional)" />
                                                <br style="clear: both;" />
                                            </div>
                                        </fieldset>

                                        <fieldset>
                                            <legend>Emergency Contact</legend>
                                            <div>
                                                <label><b style="color: red">*</b> E.C. Name</label><telerik:RadTextBox ID="tbContactName" CssClass="RadSizeMiddle" runat="server" />
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ID="tbContactNameVali" ControlToValidate="tbContactName" Display="Dynamic" ErrorMessage="E.C Name Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>
                                            <div>
                                                <label><b style="color: red">*</b> E.C. Relationship</label><telerik:RadTextBox ID="tbContactRelationship" CssClass="RadSizeMiddle" runat="server" />
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ID="tbContactRelationshipVali" ControlToValidate="tbContactRelationship" Display="Dynamic" ErrorMessage="E.C Relationship Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>
                                            <div>
                                                <label><b style="color: red">*</b> E.C. Phone</label><telerik:RadTextBox ID="tbContactPhone" CssClass="RadSizeMiddle" runat="server" />
                                                <br style="clear: both;" />
                                                <asp:RequiredFieldValidator runat="server" ID="tbContactPhoneVali" ControlToValidate="tbContactPhone" Display="Dynamic" ErrorMessage="E.C Phone Required" ForeColor="Red" ValidationGroup="Info" />
                                            </div>

                                        </fieldset>
                                    </div>
                                </div>
                            </telerik:RadPane>

                        </telerik:RadSplitter>
                    </telerik:RadPageView>

                    <telerik:RadPageView ID="RadPageView2" runat="server">
                        <telerik:RadSplitter ID="RadSplitterContract" runat="server" Orientation="Horizontal">

                            <telerik:RadPane ID="RadPane4" runat="server" Height="40px" Scrolling="None">
                                <telerik:RadToolBar ID="RadToolBarStudentContract" runat="server" OnButtonClick="RadToolBarStudentContract_OnButtonClick" OnClientButtonClicking="ToolbarButtonClick">
                                    <Items>
                                        <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_reg.png" Text="New Program" Enabled="False" />
                                        <telerik:RadToolBarButton IsSeparator="True" />
                                        <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_reg.png" Text="New Package" Enabled="False" />
                                        <telerik:RadToolBarButton IsSeparator="true" />
                                        <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_reg.png" Text="New Manual Invoice" ToolTip="New Manual Invoice" Enabled="False" />
                                        <telerik:RadToolBarButton IsSeparator="true" />
                                        <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_reg.png" Text="New Homestay" ToolTip="New Homestay" Enabled="false" Visible="False" />
<%--                                        <telerik:RadToolBarButton IsSeparator="true" />--%>
                                        <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_reg.png" Text="New Dormitory" ToolTip="New Dormitory" Enabled="false" Visible="False" />
<%--                                        <telerik:RadToolBarButton IsSeparator="true" />--%>
                                        <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_search.png" Text="View Invoice" ToolTip="View Invoice" Value="View Invoice" />
                                        <telerik:RadToolBarButton IsSeparator="true" />
                                        <telerik:RadToolBarButton ImageUrl="~/assets/img/Receive Cash-20.png" Text="Refund" ToolTip="Refund" Value="Refund" Enabled="false" />
                                        <telerik:RadToolBarButton IsSeparator="true" />
                                        <telerik:RadToolBarButton ImageUrl="~/assets/img/Cancel-20.png" Text="Cancel" ToolTip="Cancel" Value="Cancel" Enabled="false" />
                                        <telerik:RadToolBarButton ImageUrl="~/assets/img/Pause-20.png" Text="Break" ToolTip="Break" Value="Break" Enabled="false" />
                                        <telerik:RadToolBarButton ImageUrl="~/assets/img/Calendar-20.png" Text="Schedule Change" ToolTip="Schedule Change" Value="ScheduleChange" Enabled="false" />
                                        <telerik:RadToolBarButton IsSeparator="true" />
                                        <telerik:RadToolBarSplitButton ImageUrl="~/assets/img/Document-20.png" EnableDefaultButton="False" Text="Report" ToolTip="Report">
                                            <Buttons>
                                                <telerik:RadToolBarButton ImageUrl="~/assets/img/Document-20.png" Text="Letter Of Acceptance" ToolTip="Letter Of Acceptance" />
                                                <telerik:RadToolBarButton IsSeparator="true" />
                                                <telerik:RadToolBarButton ImageUrl="~/assets/img/Document-20.png" Text="Letter Of Acceptance in table" ToolTip="Letter Of Acceptance in table" />
                                                <telerik:RadToolBarButton IsSeparator="true" />
                                                <telerik:RadToolBarButton ImageUrl="~/assets/img/Document-20.png" Text="Student Contract" ToolTip="Student Contract" />
                                                <telerik:RadToolBarButton IsSeparator="true" />
                                                <telerik:RadToolBarButton ImageUrl="~/assets/img/Document-20.png" Text="Orientation Form" ToolTip="Orientation Form" />
                                                <telerik:RadToolBarButton IsSeparator="true" />
                                                <telerik:RadToolBarButton ImageUrl="~/assets/img/Document-20.png" Text="Confirmation Of Completion Letter" ToolTip="Confirmation Of Completion Letter" />
                                                <telerik:RadToolBarButton IsSeparator="true" />
                                                <telerik:RadToolBarButton ImageUrl="~/assets/img/Document-20.png" Text="Confirmation Of Enrollment" ToolTip="Confirmation Of Enrollment" />
                                                <telerik:RadToolBarButton IsSeparator="true" />
                                                <telerik:RadToolBarButton ImageUrl="~/assets/img/Document-20.png" Text="Certification" ToolTip="Certification" />
                                            </Buttons>
                                        </telerik:RadToolBarSplitButton>
                                        <telerik:RadToolBarButton IsSeparator="true" />

                                        <telerik:RadToolBarButton ImageUrl="~/assets/img/Change Theme-20.png" Text="Program Change" ToolTip="Program Change" Value="ProgramChange" Enabled="false" Visible="False" />
                                        <telerik:RadToolBarButton ImageUrl="~/assets/img/Piping-20.png" Text="Transfer" ToolTip="Transfer" Value="Transfer" Enabled="false" Visible="False" />
                                        <telerik:RadToolBarButton IsSeparator="true" Visible="False" />
                                    </Items>
                                </telerik:RadToolBar>
                            </telerik:RadPane>

                            <telerik:RadPane ID="RadPaneStudentContract" runat="server" Scrolling="None">
                                <telerik:RadGrid ID="RadGridStudentContract" runat="server"
                                    AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False" Height="92%" OnSelectedIndexChanged="RadGridStudentContract_OnSelectedIndexChanged" AllowFilteringByColumn="True"
                                    DataSourceID="LinqDataSourceStudentContract" ShowFooter="false" OnFilterCheckListItemsRequested="RadGridStudentList_OnFilterCheckListItemsRequested"
                                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                                    <MasterTableView DataKeyNames="InvoiceId" ClientDataKeyNames="InvoiceId" TableLayout="Fixed" DataSourceID="LinqDataSourceStudentContract">
                                        <Columns>
                                            <telerik:GridBoundColumn
                                                HeaderText="Invoice No" DataField="InvoiceNumber" SortExpression="InvoiceNumber" UniqueName="InvoiceNumber"
                                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn
                                                HeaderText="Status" DataField="InvoiceStatus" SortExpression="InvoiceStatus" UniqueName="InvoiceStatus"
                                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
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
                                                HeaderText="Program" DataField="ProgramName" SortExpression="ProgramName" UniqueName="ProgramName"
                                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn
                                                HeaderText="Program Status" DataField="ProgramStatusName" SortExpression="ProgramStatusName" UniqueName="ProgramStatusName"
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
                                                HeaderText="Agency" DataField="AgencyName" SortExpression="AgencyName" UniqueName="AgencyName"
                                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridNumericColumn
                                                HeaderText="Student Net Amount" DataField="StudentPriceSum" SortExpression="StudentPriceSum" UniqueName="StudentPriceSum"
                                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" DataFormatString="{0:$#,##0.00}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                            </telerik:GridNumericColumn>
                                            <telerik:GridNumericColumn
                                                HeaderText="Agency Net Amount" DataField="AgencyPriceSum" SortExpression="AgencyPriceSum" UniqueName="AgencyPriceSum"
                                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" DataFormatString="{0:$#,##0.00}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                            </telerik:GridNumericColumn>
                                            <telerik:GridNumericColumn
                                                HeaderText="Paid Amount" DataField="PayAmount" SortExpression="PayAmount" UniqueName="PayAmount"
                                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" DataFormatString="{0:$#,##0.00}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                            </telerik:GridNumericColumn>
                                            <telerik:GridNumericColumn
                                                HeaderText="Balance" DataField="Balance" SortExpression="Balance" UniqueName="Balance"
                                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small" DataFormatString="{0:$#,##0.00}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                            </telerik:GridNumericColumn>
                                            <telerik:GridBoundColumn
                                                HeaderText="Payment Count" DataField="PaymentCnt" SortExpression="PaymentCnt" UniqueName="PaymentCnt"
                                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn
                                                HeaderText="Deposit Confirm Count" DataField="DepositConfirmCnt" SortExpression="DepositConfirmCnt" UniqueName="DepositConfirmCnt"
                                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridCheckBoxColumn
                                                HeaderText="FG" DataField="IsFinancialGurantee" SortExpression="IsFinancialGurantee" UniqueName="IsFinancialGurantee"
                                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="EqualTo" HeaderStyle-Font-Size="X-Small">
                                            </telerik:GridCheckBoxColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="true" />
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true" />
                                        <%--<ClientEvents OnRowSelected="RowSelected" />--%>
                                        <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                                            ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                                    </ClientSettings>
                                    <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                                </telerik:RadGrid>
                                <asp:LinqDataSource ID="LinqDataSourceStudentContract" runat="server"
                                    ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName=""
                                    TableName="vwStudentContracts" OrderBy="InvoiceId Descending"
                                    Where="InvoiceId == @InvoiceId">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="0" Name="InvoiceId" Type="Int32" />
                                    </WhereParameters>
                                </asp:LinqDataSource>
                            </telerik:RadPane>

                        </telerik:RadSplitter>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </telerik:RadPane>

        </telerik:RadSplitter>
    </telerik:RadAjaxPanel>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //function removePanel() {
            //    $telerik.findControl(document, "RadAjaxLoadingPanel1").hide();
            //}

            //function onRequestStart(sender, args) {
            //    if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0 ||
            //            args.get_eventTarget().indexOf("ExportToWordButton") >= 0 ||
            //            args.get_eventTarget().indexOf("ExportToPdfButton") >= 0 ||
            //            args.get_eventTarget().indexOf("ExportToCsvButton") >= 0) {
            //        setTimeout(removePanel, 10);
            //        args.set_enableAjax(false);
            //    }
            //}

            // call when page load.
            <%-- function pageLoad() {
                var grid = $find("<%= RadGridStudentContract.ClientID %>");
                if (grid != null) {
                    var columns = grid.get_masterTableView().get_columns();
                    for (var i = 0; i < columns.length; i++) {
                        columns[i].resizeToFit();
                    }
                }
             }--%>

            function ShowNewPop() {
                var oWnd = window.radopen('StudentPop', 0, 0, 0, 0);
                var displayWidth = window.innerWidth * 0.95;
                var displayHeight = window.innerHeight * 0.95;
                if (displayWidth > 1500)
                    displayWidth = 1500;
                oWnd.setSize(displayWidth, displayHeight);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Maximize + Telerik.Web.UI.WindowBehaviors.Resize + Telerik.Web.UI.WindowBehaviors.Move);
                oWnd.add_close(OnClientCloseHandler);
                return false;
            }

            function OnClientCloseHandler(sender, args) {
                <%= Page.GetPostBackEventReference(ButtonGridRefresh) %>;
            }

            function OnClientClose(oWnd, args) {
                var arg = args.get_argument();
                <%= Page.GetPostBackEventReference(btnRefresh) %>;
            }

            function ShowRegProgramNewWindow(studId) {
                var oWnd = window.radopen('ProgramNewPop?id=' + studId, 0, 0, 0, 0);
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


            function ShowNewHomestayNewWindow(HomestayId, studId, ScheduleChange) {
                var oWnd = window.radopen('NewHomestayStudentPop?id=' + HomestayId + '&StudentId=' + studId + '&ScheduleChange=' + ScheduleChange, 0, 0, 0, 0);
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

            function ShowNewDormitoryNewWindow(DormitoryRegistrationId, studId, ScheduleChange) {
                var oWnd = window.radopen('NewDormitoryStudentPop?id=' + DormitoryRegistrationId + '&StudentId=' + studId + '&ScheduleChange=' + ScheduleChange, 0, 0, 0, 0);
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
            function ShowRegPackageProgramNewWindow(studId) {
                var oWnd = window.radopen('PackageProgramNewPop?id=' + studId, 0, 0, 0, 0);
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

            function ShowRefundWindow(invoiceId) {
                var oWnd = window.radopen('StudentRefundPop?id=' + invoiceId, 0, 0, 0, 0);
                //var displayWidth = $(window).width() * 0.95;
                //var displayHeight = $(window).height() * 0.95;
                //if (displayWidth > 1500)
                //    displayWidth = 1500;
                var displayWidth = 700;
                var displayHeight = 550;
                oWnd.setSize(displayWidth, displayHeight);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Maximize + Telerik.Web.UI.WindowBehaviors.Resize + Telerik.Web.UI.WindowBehaviors.Move);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowTransferWindow(invoiceId) {
                var oWnd = window.radopen('StudentTransferPop?id=' + invoiceId, 0, 0, 0, 0);
                //var displayWidth = $(window).width() * 0.95;
                //var displayHeight = $(window).height() * 0.95;
                //if (displayWidth > 1500)
                //    displayWidth = 1500;
                var displayWidth = 700;
                var displayHeight = 700;
                oWnd.setSize(displayWidth, displayHeight);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Maximize + Telerik.Web.UI.WindowBehaviors.Resize + Telerik.Web.UI.WindowBehaviors.Move);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowBreakWindow(invoiceId) {
                var oWnd = window.radopen('StudentBreakPop?id=' + invoiceId, 0, 0, 0, 0);
                //var displayWidth = $(window).width() * 0.95;
                //var displayHeight = $(window).height() * 0.95;
                //if (displayWidth > 1500)
                //    displayWidth = 1500;
                var displayWidth = 700;
                var displayHeight = 600;
                oWnd.setSize(displayWidth, displayHeight);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Maximize + Telerik.Web.UI.WindowBehaviors.Resize + Telerik.Web.UI.WindowBehaviors.Move);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowCancelWindow(invoiceId) {
                var oWnd = window.radopen('StudentCancelPop?id=' + invoiceId, 0, 0, 0, 0);
                //var displayWidth = $(window).width() * 0.95;
                //var displayHeight = $(window).height() * 0.95;
                //if (displayWidth > 1500)
                //    displayWidth = 1500;
                var displayWidth = 700;
                var displayHeight = 400;
                oWnd.setSize(displayWidth, displayHeight);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Maximize + Telerik.Web.UI.WindowBehaviors.Resize + Telerik.Web.UI.WindowBehaviors.Move);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowProgramChangeWindow(invoiceId) {
                var oWnd = window.radopen('StudentProgramChangePop?id=' + invoiceId, 0, 0, 0, 0);
                //var displayWidth = $(window).width() * 0.95;
                //var displayHeight = $(window).height() * 0.95;
                //if (displayWidth > 1500)
                //    displayWidth = 1500;
                var displayWidth = 700;
                var displayHeight = 500;
                oWnd.setSize(displayWidth, displayHeight);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Maximize + Telerik.Web.UI.WindowBehaviors.Resize + Telerik.Web.UI.WindowBehaviors.Move);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowScheduleChangeWindow(invoiceId) {
                var oWnd = window.radopen('StudentScheduleChangePop?id=' + invoiceId, 0, 0, 0, 0);
                //var displayWidth = $(window).width() * 0.95;
                //var displayHeight = $(window).height() * 0.95;
                //if (displayWidth > 1500)
                //    displayWidth = 1500;
                var displayWidth = 700;
                var displayHeight = 550;
                oWnd.setSize(displayWidth, displayHeight);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Maximize + Telerik.Web.UI.WindowBehaviors.Resize + Telerik.Web.UI.WindowBehaviors.Move);
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

            function ShowReportPop(invoiceId, reportType) {
                var oWnd = window.radopen('ReportPop?id=' + invoiceId + '&reportType=' + reportType, 0, 0, 0, 0);
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

            function ToolbarButtonClick(sender, args) {
                var button = args.get_item();
                if (button.get_text() === "New Manual Invoice") {
                    if (!confirm('Do you want to create Manual Invoice?'))
                        args.set_cancel(true);
                }
            }

        </script>
    </telerik:RadCodeBlock>

</asp:Content>
