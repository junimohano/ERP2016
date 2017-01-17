<%@ Page Title="Agency" Language="C#" MasterPageFile="~/School/Shared/Base.master" AutoEventWireup="true" CodeFile="Agency.aspx.cs" Inherits="School.Registrar.Agency" %>

<%@ Import Namespace="Erp2016.Lib" %>

<%@ Register TagPrefix="usercontrol" TagName="approvalline" Src="~/App_Data/ApprovalLine.ascx" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>


<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">
        <div style="display: none">
            <asp:Button ID="ButtonGridRefresh" runat="server" OnClick="ButtonGridRefresh_OnClick" />
        </div>
        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="Radpane4" runat="server" Height="27px" Scrolling="None">
                <h4>Agency Information</h4>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane6" runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="RadToolBarAgency" runat="server" OnClientButtonClicking="ToolbarButtonClick" OnButtonClick="RadToolBarAgency_OnButtonClick">
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
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_mark.png" Text="Request" Enabled="False" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_delete.png" Text="Cancel" Enabled="False" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_excute.png" Text="Approve" Enabled="False" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_clear.png" Text="Reject" Enabled="False" />
                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_back.png" Text="Revise" Enabled="False" />
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <telerik:RadPane ID="Radpane1" runat="server" Height="35%" Scrolling="None">
                <telerik:RadGrid ID="RadGridAgency" runat="server" AllowFilteringByColumn="True" OnSelectedIndexChanged="RadGridAgency_OnSelectedIndexChanged"
                    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" Height="100%" PageSize="20" OnPreRender="RadGridAgency_OnPreRender"
                    DataSourceID="LinqDataSource1" OnFilterCheckListItemsRequested="RadGridAgency_OnFilterCheckListItemsRequested"
                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                    <MasterTableView DataKeyNames="AgencyId" TableLayout="Fixed" DataSourceID="LinqDataSource1">
                        <Columns>
                            <telerik:GridBoundColumn
                                HeaderText="Agency No" DataField="AgencyNumber" SortExpression="AgencyNumber" UniqueName="AgencyNumber"
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
                            <telerik:GridBoundColumn
                                HeaderText="Agency" DataField="AgencyName" SortExpression="AgencyName" UniqueName="AgencyName"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn
                                HeaderText="Contract Start Date" DataField="ContractStartDate" SortExpression="ContractStartDate" UniqueName="ContractStartDate"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy}">
                            </telerik:GridDateTimeColumn>
                            <telerik:GridDateTimeColumn
                                HeaderText="Contract End Date" DataField="ContractEndDate" SortExpression="ContractEndDate" UniqueName="ContractEndDate"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small"
                                PickerType="DatePicker" EnableTimeIndependentFiltering="True" DataFormatString="{0:MM-dd-yyyy}">
                            </telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn
                                HeaderText="Business Location" DataField="BusinessLocation" SortExpression="BusinessLocation" UniqueName="BusinessLocation"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn
                                HeaderText="MainTarget Location" DataField="MainTargetLocation" SortExpression="MainTargetLocation" UniqueName="MainTargetLocation"
                                FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
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
                            <telerik:GridBoundColumn
                                HeaderText="Approval UserName" DataField="ApprovalUserName" SortExpression="ApprovalUserName" UniqueName="ApprovalUserName" Visible="False"
                                FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnablePostBackOnRowClick="true">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="True"></Scrolling>
                        <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                            ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                    </ClientSettings>
                    <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="True" />
                </telerik:RadGrid>
                <asp:LinqDataSource ID="LinqDataSource1" runat="server"
                    ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName=""
                    TableName="vwAgencies"
                    Where="AgencyId == @AgencyId">
                    <WhereParameters>
                        <asp:Parameter DefaultValue="0" Name="AgencyId" Type="Int32" />
                    </WhereParameters>
                </asp:LinqDataSource>
            </telerik:RadPane>

            <telerik:RadSplitBar ID="Radsplitbar1" runat="server" CollapseMode="Both" EnableResize="true" />

            <telerik:RadPane ID="Radpane2" runat="server" Scrolling="None">
                <telerik:RadSplitter ID="Radsplitter4" runat="server" Orientation="Horizontal">

                    <telerik:RadPane ID="Radpane3" runat="server" Height="40px" Scrolling="None">
                        <telerik:RadToolBar ID="RadToolBar1" runat="server" OnClientButtonClicking="ToolbarButtonClick"
                            OnButtonClick="AgencyButtonClicked">
                            <Items>
                                <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_save.png" Text="Save" ToolTip="Save" ValidationGroup="Info" />
                                <telerik:RadToolBarButton IsSeparator="true" />
                                <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_new.png" Enabled="false" Text="New" ToolTip="New" />
                                <telerik:RadToolBarButton IsSeparator="true" />
                                <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_search.png" Enabled="True" Text="Old Agency Lookup" ToolTip="Old Agency Lookup" />
                                <telerik:RadToolBarButton IsSeparator="true" />
                            </Items>
                        </telerik:RadToolBar>
                    </telerik:RadPane>

                    <telerik:RadPane ID="Radpane5" runat="server" Scrolling="None">

                        <telerik:RadSplitter runat="server" Orientation="Vertical">

                            <telerik:RadPane ID="RadPane8" runat="server" Width="70%">
                                <div class="formStyle3">

                                    <div style="float: left; width: 100%">
                                        <fieldset>
                                            <legend>Agency Information</legend>
                                            <div style="float: left; width: 50%;">
                                                <div>
                                                    <label>Site</label><telerik:RadTextBox ID="RadTextBoxSite" CssClass="RadSizeMiddle" runat="server" Enabled="false"></telerik:RadTextBox>
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label><b style="color: red">*</b> Site Location</label><telerik:RadComboBox ID="RadComboBoxSiteLocation" CssClass="RadSizeMiddle" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxSiteLocation_OnSelectedIndexChanged" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" EmptyMessage="Choose a Site Location" />
                                                    <asp:Literal ID="itemsClientSide" runat="server" />
                                                    <br style="clear: both;" />
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="RadComboBoxSiteLocation" Display="Dynamic" ErrorMessage="Site Location Required" ForeColor="Red" ValidationGroup="Info" />
                                                </div>
                                                <div>
                                                    <label><b style="color: red">*</b> Agency Full Name</label>
                                                    <telerik:RadComboBox ID="RadComboBoxAgencyName" CssClass="RadSizeLarge" runat="server" AutoPostBack="True" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" AllowCustomText="True" OnTextChanged="RadComboBoxAgencyName_OnTextChanged"></telerik:RadComboBox>
                                                    <br style="clear: both;" />
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="RadComboBoxAgencyName" Display="Dynamic" ErrorMessage="Agency Full Name Required" ForeColor="Red" ValidationGroup="Info" />
                                                </div>
                                                <div>
                                                    <label>Choose a Parent Agency</label>
                                                    <telerik:RadComboBox ID="ddlPAgency" CssClass="RadSizeLarge" runat="server" EmptyMessage="Choose Parent" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True"></telerik:RadComboBox>
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Agency Short Name</label>
                                                    <telerik:RadTextBox ID="tbAgencyShortName" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Agency Group Name</label>
                                                    <telerik:RadTextBox ID="tbAgencyGroupName" CssClass="RadSizeLarge" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Agency Printing Name</label>
                                                    <telerik:RadTextBox ID="tbAgencyPrintName" CssClass="RadSizeLarge" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Agency Type</label>
                                                    <telerik:RadTextBox ID="tbAgencyType" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label><b style="color: red">*</b> Commission Rate - Basic</label>
                                                    <telerik:RadNumericTextBox ID="tbCommissionRateBasic" CssClass="RadSizeMiddle" Type="Percent" NumberFormat-DecimalDigits="2" runat="server" />
                                                    <br style="clear: both;" />
                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbCommissionRateBasic" Display="Dynamic" ErrorMessage="Commission Rate - Basic Required" ForeColor="Red" ValidationGroup="Info" />
                                                </div>
                                                <div>
                                                    <label><b style="color: red">*</b> Commission Rate - Seasonal</label>
                                                    <telerik:RadNumericTextBox ID="tbCommissionRateSeason" CssClass="RadSizeMiddle" Type="Percent" NumberFormat-DecimalDigits="2" runat="server" />
                                                    <br style="clear: both;" />
                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbCommissionRateSeason" Display="Dynamic" ErrorMessage="Commission Rate - Seasonal Required" ForeColor="Red" ValidationGroup="Info" />
                                                </div>
                                                <div>
                                                    <label><b style="color: red">*</b> Credit Limit</label>
                                                    <telerik:RadNumericTextBox ID="tbCreditLimit" CssClass="RadSizeMiddle" runat="server" Type="Currency" />
                                                    <br style="clear: both;" />
                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbCreditLimit" Display="Dynamic" ErrorMessage="Credit Limit Required" ForeColor="Red" ValidationGroup="Info" />
                                                </div>
                                                <div>
                                                    <label><b style="color: red">*</b> Business Location</label>
                                                    <telerik:RadComboBox ID="ddlBusinessLocation" CssClass="RadSizeMiddle" runat="server" EmptyMessage="Choose a country" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True"></telerik:RadComboBox>
                                                    <br style="clear: both;" />
                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlBusinessLocation" Display="Dynamic" ErrorMessage="Business Location Required" ForeColor="Red" ValidationGroup="Info" />
                                                </div>
                                                <div>
                                                    <label><b style="color: red">*</b> Main Target Country</label>
                                                    <telerik:RadComboBox ID="ddlMainTarget" CssClass="RadSizeMiddle" runat="server" EmptyMessage="Choose a country" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True"></telerik:RadComboBox>
                                                    <br style="clear: both;" />
                                                    <asp:RequiredFieldValidator runat="server" ID="ddlMainTargetVali" ControlToValidate="ddlMainTarget" Display="Dynamic" ErrorMessage="Main Target Country Required" ForeColor="Red" ValidationGroup="Info" />
                                                </div>
                                                <div>
                                                    <label>Contract Start Date</label>
                                                    <telerik:RadDatePicker DateInput-DisplayDateFormat="MMM dd, yyyy" ID="tbContractStart" runat="server" CssClass="RadSizeMiddle" DateInput-EmptyMessage="Start Date" MinDate="01/01/1900" MaxDate="01/01/3000"></telerik:RadDatePicker>
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Contract End Date</label>
                                                    <telerik:RadDatePicker DateInput-DisplayDateFormat="MMM dd, yyyy" ID="tbContractEnd" runat="server" CssClass="RadSizeMiddle" DateInput-EmptyMessage="End Date" MinDate="01/01/1900" MaxDate="01/01/3000"></telerik:RadDatePicker>
                                                    <br style="clear: both;" />
                                                </div>
                                            </div>
                                            <div style="float: left; width: 50%;">
                                                <div>
                                                    <label>AP Payment Term</label>
                                                    <telerik:RadTextBox ID="tbAPPayTerm" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>AP Payment Method</label>
                                                    <telerik:RadTextBox ID="tbAPPayMethod" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>AP Billing Type</label>
                                                    <telerik:RadTextBox ID="tbAPBillingType" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>AP Payment Priority</label>
                                                    <telerik:RadTextBox ID="tbAPPAymentPriority" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>AP Payment Schedule</label>
                                                    <telerik:RadTextBox ID="tbAPPaySchedule" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>AR Collection Term</label>
                                                    <telerik:RadTextBox ID="tbARCollectionTerm" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>AR Type</label>
                                                    <telerik:RadTextBox ID="tbARType" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>AR Collection Priority</label>
                                                    <telerik:RadTextBox ID="tbARCollection" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>AR Collection Schedule</label>
                                                    <telerik:RadTextBox ID="tbARCollectionSchedule" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>AR Collection Method</label>
                                                    <telerik:RadTextBox ID="tbARCollectionMethod" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Agency Register No</label>
                                                    <telerik:RadTextBox ID="tbAgencyNo" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Currency</label>
                                                    <telerik:RadTextBox ID="tbCurrency" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Comment</label>
                                                    <telerik:RadTextBox ID="tbComment" TextMode="MultiLine" CssClass="RadSizeMultiLine" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Active</label>
                                                    <telerik:RadButton ID="RadButtonActive" runat="server" AutoPostBack="False" ToggleType="CheckBox" ButtonType="ToggleButton" />
                                                    <br style="clear: both;" />
                                                </div>
                                            </div>
                                        </fieldset>
                                        <br />
                                    </div>

                                    <div style="float: left; width: 100%">
                                        <fieldset>
                                            <legend>Contact Information</legend>
                                            <div style="float: left; width: 50%;">
                                                <br style="clear: both;" />
                                                <hr />
                                                <label>Contact person 1</label>
                                                <br style="clear: both;" />
                                                <br style="clear: both;" />
                                                <div>
                                                    <label>First Name</label>
                                                    <telerik:RadTextBox ID="agFname" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Last Name</label>
                                                    <telerik:RadTextBox ID="agLname" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Title(MR, Miss, Mrs..)</label>
                                                    <telerik:RadTextBox ID="agTitle" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Phone No</label>
                                                    <telerik:RadTextBox ID="agPhone" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Mobile No</label>
                                                    <telerik:RadTextBox ID="agMobile" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Fax No</label>
                                                    <telerik:RadTextBox ID="agFax" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Primary Email</label>
                                                    <telerik:RadTextBox ID="agPEmail" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Secondary Email</label>
                                                    <telerik:RadTextBox ID="agSEmail" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Web Site</label>
                                                    <telerik:RadTextBox ID="agWebsite" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Address</label>
                                                    <telerik:RadTextBox ID="agAddress" CssClass="RadSizeLarge" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>City</label>
                                                    <telerik:RadTextBox ID="agCity" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Province</label>
                                                    <telerik:RadTextBox ID="agProvince" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Postal Code</label>
                                                    <telerik:RadTextBox ID="agPostal" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Country</label>
                                                    <telerik:RadComboBox ID="ddlAgencyCountry" CssClass="RadSizeMiddle" runat="server" EmptyMessage="Choose a country" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True"></telerik:RadComboBox>
                                                    <br style="clear: both;" />
                                                </div>
                                            </div>
                                            <div style="float: left; width: 50%;">
                                                <br style="clear: both;" />
                                                <hr />
                                                <label>Contact person 2</label>
                                                <br style="clear: both;" />
                                                <br style="clear: both;" />
                                                <div>
                                                    <label>First Name</label>
                                                    <telerik:RadTextBox ID="agFname1" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Last Name</label>
                                                    <telerik:RadTextBox ID="agLname1" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Title(MR, Miss, Mrs..)</label>
                                                    <telerik:RadTextBox ID="agTitle1" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Phone No</label>
                                                    <telerik:RadTextBox ID="agPhone1" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Mobile No</label>
                                                    <telerik:RadTextBox ID="agMobile1" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Fax No</label>
                                                    <telerik:RadTextBox ID="agFax1" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Primary Email</label>
                                                    <telerik:RadTextBox ID="agPEmail1" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Secondary Email</label>
                                                    <telerik:RadTextBox ID="agSEmail1" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Web Site</label>
                                                    <telerik:RadTextBox ID="agWebsite1" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Address</label>
                                                    <telerik:RadTextBox ID="agAddress1" CssClass="RadSizeLarge" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>City</label>
                                                    <telerik:RadTextBox ID="agCity1" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Province</label>
                                                    <telerik:RadTextBox ID="agProvince1" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Postal Code</label>
                                                    <telerik:RadTextBox ID="agPostal1" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Country</label>
                                                    <telerik:RadComboBox ID="ddlAgencyCountry1" CssClass="RadSizeMiddle" runat="server" EmptyMessage="Choose a country" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True"></telerik:RadComboBox>
                                                    <br style="clear: both;" />
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                            </telerik:RadPane>

                            <telerik:RadSplitBar ID="Radsplitbar2" runat="server" CollapseMode="Both" EnableResize="true" />

                            <telerik:RadPane ID="RadPane7" runat="server" Height="125px">
                                <usercontrol:approvalline ID="ApprovalLine1" runat="server" />
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
                if (button.get_text() === "Delete") {
                    if (!confirm('Do you want to delete it?'))
                        args.set_cancel(true);
                } else if (button.get_text() === "Request") {
                    if (!confirm('Do you want to request it?'))
                        args.set_cancel(true);
                } else if (button.get_text() === "Save") {
                    if (!confirm('Do you want to save it?'))
                        args.set_cancel(true);
                } else if (button.get_text() === "Update") {
                    if (!confirm('Do you want to update it?'))
                        args.set_cancel(true);
                }
            }

            function ShowApprovalWindow(id) {
                var oWnd = window.radopen('ApprovalApprovePop?type=' + <%= (int)CConstValue.Approval.Agency %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }
            function ShowApprovalRejectWindow(id) {

                var oWnd = window.radopen('ApprovalRejectPop?type=' + <%= (int)CConstValue.Approval.Agency %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }
            function ShowApprovalReviseWindow(id) {
                var oWnd = window.radopen('ApprovalRevisePop?type=' + <%= (int)CConstValue.Approval.Agency %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalCancelWindow(id) {
                var oWnd = window.radopen('ApprovalCancelPop?type=' + <%= (int)CConstValue.Approval.Agency %> + '&id=' + id, 0, 0, 0, 0);
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

            function ShowAgencyOldInfoWindow() {
                var oWnd = window.radopen('AgencyOldInfoPop', 0, 0, 0, 0);
                var displayWidth = $(window).width() * 0.95;
                var displayHeight = $(window).height() * 0.95;

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
