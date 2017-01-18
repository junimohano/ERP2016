<%@ Page Title="Programs" Language="C#" MasterPageFile="~/School/Shared/Base.master" AutoEventWireup="true" CodeFile="Program.aspx.cs" Inherits="School.AcademicRegistrar.Program" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">

        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="RadPane3" runat="server" Height="40%" Scrolling="None">

                <telerik:RadSplitter runat="server" Orientation="Horizontal">
                    <telerik:RadPane ID="GridPane" runat="server" Height="27px" Scrolling="None">
                        <h4>Program List</h4>
                    </telerik:RadPane>

                    <telerik:RadPane ID="RadPane2" runat="server" Scrolling="None">
                        <telerik:RadGrid ID="Grid" DataSourceID="LinqDataSourceProgram" runat="server" PageSize="20" AutoGenerateColumns="false" AllowPaging="true" Height="100%"
                            AllowCustomPaging="false" AllowSorting="true" AllowFilteringByColumn="true"
                            OnSelectedIndexChanged="SelectProgram" OnFilterCheckListItemsRequested="Grid_OnFilterCheckListItemsRequested"
                            FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                            <MasterTableView DataSourceID="LinqDataSourceProgram" TableLayout="Fixed" DataKeyNames="ProgramId">
                                <Columns>
                                    <telerik:GridBoundColumn
                                        HeaderText="No" DataField="ProgramId" SortExpression="ProgramId" UniqueName="ProgramId"
                                        FilterCheckListEnableLoadOnDemand="False" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn
                                        HeaderText="Site" DataField="SiteName" SortExpression="SiteName" UniqueName="SiteName"
                                        FilterCheckListEnableLoadOnDemand="true" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn
                                        HeaderText="Faculty" DataField="FacultyName" SortExpression="FacultyName" UniqueName="FacultyName"
                                        FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn
                                        HeaderText="Program Group" DataField="ProgramGroupName" SortExpression="ProgramGroupName" UniqueName="ProgramGroupName"
                                        FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn
                                        HeaderText="Program" DataField="ProgramName" SortExpression="ProgramName" UniqueName="ProgramName"
                                        FilterCheckListEnableLoadOnDemand="True" FilterControlAltText="Filter column" AutoPostBackOnFilter="True" CurrentFilterFunction="StartsWith" HeaderStyle-Font-Size="X-Small">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn
                                        HeaderText="Program Short Name" DataField="ProgramShort" SortExpression="ProgramShort" UniqueName="ProgramShort"
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
                                </Columns>
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                                <Selecting AllowRowSelect="true" />
                                <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="True" />
                                <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                                    ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                            </ClientSettings>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="true" />
                        </telerik:RadGrid>

                        <asp:LinqDataSource ID="LinqDataSourceProgram" runat="server"
                            ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName="" TableName="vwPrograms" OrderBy="CreatedDate DESC"
                            Where="ProgramId == @ProgramId">
                            <WhereParameters>
                                <asp:Parameter DefaultValue="0" Name="ProgramId" Type="Int32" />
                            </WhereParameters>
                        </asp:LinqDataSource>
                    </telerik:RadPane>
                </telerik:RadSplitter>

            </telerik:RadPane>

            <telerik:RadSplitBar ID="Radslitbar1" runat="server" CollapseMode="Both" EnableResize="True"></telerik:RadSplitBar>

            <telerik:RadPane ID="Radpane4" runat="server" Scrolling="None">

                <telerik:RadSplitter ID="RadSplitter3" runat="server" Orientation="Vertical">

                    <telerik:RadPane ID="Radpane6" runat="server" Scrolling="None" Width="70%">
                        <telerik:RadSplitter ID="RadSplitter2" runat="server" Orientation="Horizontal">

                            <telerik:RadPane ID="RadPane1" runat="server" Height="40px" Scrolling="None">
                                <telerik:RadToolBar ID="RadToolBarProgram" runat="server" OnButtonClick="ToolbarClick" OnClientButtonClicking="ToolbarClick">
                                    <Items>
                                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_save.png" Text="Save" ValidationGroup="Info" />
                                        <telerik:RadToolBarButton IsSeparator="true" />
                                        <telerik:RadToolBarButton ImageUrl="../../assets/img/bt_new.png" Text="New" Enabled="false" />
                                        <telerik:RadToolBarButton IsSeparator="true" />
                                    </Items>
                                </telerik:RadToolBar>
                            </telerik:RadPane>

                            <telerik:RadPane ID="RadPaneProgram" runat="server">

                                <div class="formStyle3">

                                    <div style="float: left; width: 100%;">

                                        <fieldset>
                                            <legend>Program General Information</legend>
                                            <div style="float: left; width: 50%;">
                                                <div>
                                                    <label>Site</label><telerik:RadTextBox ID="RadTextBoxSite" CssClass="RadSizeMiddle" runat="server" ReadOnly="True"></telerik:RadTextBox>
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label><b style="color: red">*</b> Site Location</label>
                                                    <telerik:RadComboBox ID="RadComboBoxSiteLocation" CssClass="RadSizeMiddle" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxSiteLocation_OnSelectedIndexChanged" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" EmptyMessage="Choose a Site Location" />
                                                    <asp:Literal ID="itemsClientSide" runat="server" />
                                                    <br style="clear: both;" />
                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="RadComboBoxSiteLocation" Display="Dynamic" ErrorMessage="Site Location Required" ForeColor="Red" ValidationGroup="Info" />
                                                </div>
                                                <div>
                                                    <label>Faculty Name</label>
                                                    <telerik:RadComboBox ID="RadComboBoxFaculty" CssClass="RadSizeLarge" runat="server" AutoPostBack="true" AllowCustomText="false" EnableLoadOnDemand="true" EmptyMessage="Choose a Faculty" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" OnSelectedIndexChanged="RadComboBoxFaculty_OnSelectedIndexChanged">
                                                    </telerik:RadComboBox>
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Program Group Name</label>
                                                    <telerik:RadComboBox ID="RadComboBoxProgramGroup" CssClass="RadSizeLarge" runat="server" AutoPostBack="true" EnableLoadOnDemand="true" EmptyMessage="Choose a Program Group" DataValueField="" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" OnSelectedIndexChanged="RadComboBoxProgramGroup_OnSelectedIndexChanged">
                                                    </telerik:RadComboBox>
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label><b style="color: red">*</b> Program Full Name</label>
                                                    <telerik:RadTextBox ID="tbProgramFullName" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbProgramFullName" Display="Dynamic" ErrorMessage="Program Full Name Required" ForeColor="Red" ValidationGroup="Info" />
                                                </div>
                                                <div>
                                                    <label>Program Web Name</label>
                                                    <telerik:RadTextBox ID="tbProgramWebName" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Program Short Name</label>
                                                    <telerik:RadTextBox ID="tbProgramShortName" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Program Description</label>
                                                    <telerik:RadTextBox ID="tbDescript" Height="100" TextMode="MultiLine" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Program Type(Main Biz)</label>
                                                    <telerik:RadTextBox ID="tbProgramType" CssClass="RadSizeMiddle" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <%-- <div>
                                                <label>Program Total Weeks</label><telerik:RadNumericTextBox ID="tbProgramWeek" runat="server" Type="Number" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox><br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>
                                                    Program Total Semester<br />
                                                    (If Applicable)
                                                </label>
                                                <telerik:RadNumericTextBox ID="tbProgramSemester" runat="server" Type="Number" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox><br style="clear: both;" />
                                            </div>
                                           <div>
                                                <label>Program Total Months</label><telerik:RadNumericTextBox ID="tbProgramMonth" runat="server" Type="Number" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox><br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Program Hours Per Day</label><telerik:RadNumericTextBox ID="tbProgramHoursDay" runat="server" Type="Number" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox><br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Program Hours Per Week</label>
                                                <telerik:RadComboBox ID="ddlHours" Width="150" runat="server" AppendDataBoundItems="true"></telerik:RadComboBox>
                                                <br style="clear: both;" />
                                            </div>--%>
                                                <div>
                                                    <label>Estimated Program Start Date</label>
                                                    <telerik:RadDatePicker DateInput-DisplayDateFormat="MMMM-dd-yyyy" ID="tbProgramStartDate" runat="server" CssClass="RadSizeMiddle" DateInput-EmptyMessage="Start Date" MinDate="01/01/1900" MaxDate="01/01/3000"></telerik:RadDatePicker>
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Admission Requirement</label>
                                                    <telerik:RadTextBox ID="tbProgramAdmission" TextMode="MultiLine" Height="100" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Diploma/Certification Requirement</label>
                                                    <telerik:RadTextBox ID="tbProgramDiploma" TextMode="MultiLine" Height="100" runat="server" />
                                                    <br style="clear: both;" />
                                                </div>
                                            </div>

                                            <div style="float: left; width: 50%;">
                                                <div>
                                                    <label><b style="color: red">*</b> Program Active</label>
                                                    <telerik:RadButton ID="RadButtonActive" runat="server" AutoPostBack="False" ToggleType="CheckBox" ButtonType="ToggleButton" />
                                                </div>
                                                <div>
                                                    <label>Program Active Date</label>
                                                    <telerik:RadDatePicker DateInput-DisplayDateFormat="MMMM-dd-yyyy" ID="tbProgramActiveDate" runat="server" CssClass="RadSizeMiddle" DateInput-EmptyMessage="Active Date" MinDate="01/01/1900" MaxDate="01/01/3000"></telerik:RadDatePicker>
                                                    <br style="clear: both;" />
                                                </div>
                                                <div>
                                                    <label>Program In-Active Date</label>
                                                    <telerik:RadDatePicker DateInput-DisplayDateFormat="MMMM-dd-yyyy" ID="tbProgramInActiveDate" runat="server" CssClass="RadSizeMiddle" DateInput-EmptyMessage="In-Active Date" MinDate="01/01/1900" MaxDate="01/01/3000"></telerik:RadDatePicker>
                                                    <br style="clear: both;" />
                                                </div>

                                                <fieldset>
                                                    <h4>Option</h4>
                                                    <div>
                                                        <div>
                                                            <label>Practicum Week(s)</label>
                                                            <telerik:RadNumericTextBox ID="tbPracticum" CssClass="RadSizeMiddle" runat="server" Type="Number" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                                                            <br style="clear: both;" />
                                                        </div>
                                                        <div>
                                                            <label>Intership Week(s)</label>
                                                            <telerik:RadNumericTextBox ID="tbIntership" runat="server" CssClass="RadSizeMiddle" Type="Number" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                                                            <br style="clear: both;" />
                                                        </div>
                                                    </div>
                                                </fieldset>

                                                <br style="clear: both;" />

                                                <fieldset>
                                                    <h4>UIS Only </h4>
                                                    <div>
                                                        <label>Program Type</label>
                                                        <telerik:RadComboBox ID="ddlProgramType" CssClass="RadSizeMiddle" runat="server" AppendDataBoundItems="true" EmptyMessage="Choose a Program Type"></telerik:RadComboBox>
                                                        <br style="clear: both;" />
                                                    </div>
                                                    <div>
                                                        <label>Com / Elec Type</label>
                                                        <telerik:RadComboBox ID="ddlComType" CssClass="RadSizeMiddle" runat="server" AppendDataBoundItems="true" EmptyMessage="Choose a Type"></telerik:RadComboBox>
                                                        <br style="clear: both;" />
                                                    </div>
                                                    <div>
                                                        <label>Earning Credit</label>
                                                        <telerik:RadNumericTextBox ID="tbEarningCredit" CssClass="RadSizeMiddle" runat="server" Type="Currency"></telerik:RadNumericTextBox>
                                                        <br style="clear: both;" />
                                                    </div>
                                                </fieldset>
                                            </div>
                                        </fieldset>
                                    </div>

                                    <div style="float: left; width: 50%;">

                                        <fieldset>
                                            <legend>Other Information</legend>

                                            <div>
                                                <label>Local CRC</label>
                                                <telerik:RadTextBox ID="tbLocalCRC" CssClass="RadSizeMiddle" runat="server" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Doctor's Note</label>
                                                <telerik:RadTextBox ID="tbDoctorNote" CssClass="RadSizeMiddle" runat="server" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>NOC</label>
                                                <telerik:RadTextBox ID="tbNoc" runat="server" CssClass="RadSizeMiddle" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>HRSDC</label>
                                                <telerik:RadTextBox ID="tbHrsdc" runat="server" CssClass="RadSizeMiddle" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Reference Letter 2</label>
                                                <telerik:RadTextBox ID="tbReference2" runat="server" CssClass="RadSizeMiddle" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Reference Letter 3</label>
                                                <telerik:RadTextBox ID="tbReference3" runat="server" CssClass="RadSizeMiddle" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>English 10</label>
                                                <telerik:RadTextBox ID="tbEng10" runat="server" CssClass="RadSizeMiddle" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Math 10</label>
                                                <telerik:RadTextBox ID="tbMath10" runat="server" CssClass="RadSizeMiddle" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Science 11</label>
                                                <telerik:RadTextBox ID="tbSience11" runat="server" CssClass="RadSizeMiddle" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>English 12</label>
                                                <telerik:RadTextBox ID="tbEng12" runat="server" CssClass="RadSizeMiddle" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Biology 12</label>
                                                <telerik:RadTextBox ID="tbBio12" runat="server" CssClass="RadSizeMiddle" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Senior Science</label>
                                                <telerik:RadTextBox ID="tbSSience" runat="server" CssClass="RadSizeMiddle" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Senior Math</label>
                                                <telerik:RadTextBox ID="tbSMath" runat="server" CssClass="RadSizeMiddle" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Senior English</label>
                                                <telerik:RadTextBox ID="tbSEng" runat="server" CssClass="RadSizeMiddle" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Senior Level Biology</label>
                                                <telerik:RadTextBox ID="tbSLBio" runat="server" CssClass="RadSizeMiddle" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Senior Level Chemistry</label>
                                                <telerik:RadTextBox ID="tbSLChemi" runat="server" CssClass="RadSizeMiddle" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Immunization Records</label>
                                                <telerik:RadTextBox ID="tbImmun" runat="server" CssClass="RadSizeMiddle" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Help B</label>
                                                <telerik:RadTextBox ID="tbHelpB" runat="server" CssClass="RadSizeMiddle" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Comment</label>
                                                <telerik:RadTextBox ID="tbOthercomment" TextMode="MultiLine" Height="100" runat="server" />
                                                <br style="clear: both;" />
                                            </div>
                                        </fieldset>
                                    </div>

                                    <div style="float: left; width: 50%;">

                                        <fieldset>
                                            <legend>Other Fee Information</legend>
                                            <div>
                                                <label>Registration Fee</label>
                                                <telerik:RadNumericTextBox ID="tbFee1" Width="150" runat="server" EmptyMessage="" Type="Currency" CssClass="RadSizeMiddle"></telerik:RadNumericTextBox>
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>JSHINE Registration Fee</label>
                                                <telerik:RadNumericTextBox ID="tbFee2" Width="150" runat="server" EmptyMessage="" Type="Currency" CssClass="RadSizeMiddle"></telerik:RadNumericTextBox>
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Material (Academic) Fee</label>
                                                <telerik:RadNumericTextBox ID="tbFee3" Width="150" runat="server" EmptyMessage="" Type="Currency" CssClass="RadSizeMiddle"></telerik:RadNumericTextBox>
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Material (Industry) Fee</label>
                                                <telerik:RadNumericTextBox ID="tbFee4" Width="150" runat="server" EmptyMessage="" Type="Currency" CssClass="RadSizeMiddle"></telerik:RadNumericTextBox>
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Test Fee</label>
                                                <telerik:RadNumericTextBox ID="tbFee5" Width="150" runat="server" EmptyMessage="" Type="Currency" CssClass="RadSizeMiddle"></telerik:RadNumericTextBox>
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Practicum Fee</label>
                                                <telerik:RadNumericTextBox ID="tbFee6" Width="150" runat="server" EmptyMessage="" Type="Currency" CssClass="RadSizeMiddle"></telerik:RadNumericTextBox>
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Exam Fee</label>
                                                <telerik:RadNumericTextBox ID="tbFee7" Width="150" runat="server" EmptyMessage="" Type="Currency" CssClass="RadSizeMiddle"></telerik:RadNumericTextBox>
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Administration Fee</label>
                                                <telerik:RadNumericTextBox ID="tbFee8" Width="150" runat="server" EmptyMessage="" Type="Currency" CssClass="RadSizeMiddle"></telerik:RadNumericTextBox>
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Internship Fee</label>
                                                <telerik:RadNumericTextBox ID="tbFee9" Width="150" runat="server" EmptyMessage="" Type="Currency" CssClass="RadSizeMiddle"></telerik:RadNumericTextBox>
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Language Coop Fee</label>
                                                <telerik:RadNumericTextBox ID="tbFee10" Width="150" runat="server" EmptyMessage="" Type="Currency" CssClass="RadSizeMiddle"></telerik:RadNumericTextBox>
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Self Directed Language Coop Fee</label>
                                                <telerik:RadNumericTextBox ID="tbFee11" Width="150" runat="server" EmptyMessage="" Type="Currency" CssClass="RadSizeMiddle"></telerik:RadNumericTextBox>
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Unpaid Language Coop Fee</label>
                                                <telerik:RadNumericTextBox ID="tbFee12" Width="150" runat="server" EmptyMessage="" Type="Currency" CssClass="RadSizeMiddle"></telerik:RadNumericTextBox>
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Application Change Fee</label>
                                                <telerik:RadNumericTextBox ID="tbFee13" Width="150" runat="server" EmptyMessage="" Type="Currency" CssClass="RadSizeMiddle"></telerik:RadNumericTextBox>
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Certificate Fee</label>
                                                <telerik:RadNumericTextBox ID="tbFee14" Width="150" runat="server" EmptyMessage="" Type="Currency" CssClass="RadSizeMiddle"></telerik:RadNumericTextBox>
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Supply Fee</label>
                                                <telerik:RadNumericTextBox ID="tbFee15" Width="150" runat="server" EmptyMessage="" Type="Currency" CssClass="RadSizeMiddle"></telerik:RadNumericTextBox>
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Uniform Fee</label>
                                                <telerik:RadNumericTextBox ID="tbFee16" Width="150" runat="server" EmptyMessage="" Type="Currency" CssClass="RadSizeMiddle"></telerik:RadNumericTextBox>
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>University Application Guidance Fee</label>
                                                <telerik:RadNumericTextBox ID="tbFee17" Width="150" runat="server" EmptyMessage="" Type="Currency" CssClass="RadSizeMiddle"></telerik:RadNumericTextBox>
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Other Fees</label>
                                                <telerik:RadNumericTextBox ID="tbFee18" Width="150" runat="server" EmptyMessage="" Type="Currency" CssClass="RadSizeMiddle"></telerik:RadNumericTextBox>
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Comment</label>
                                                <telerik:RadTextBox ID="tbFeeComment" TextMode="MultiLine" Height="100" runat="server" />
                                                <br style="clear: both;" />
                                            </div>
                                        </fieldset>
                                    </div>



                                </div>
                            </telerik:RadPane>

                        </telerik:RadSplitter>
                    </telerik:RadPane>

                    <telerik:RadSplitBar ID="Radsplitbar2" runat="server" CollapseMode="Both" EnableResize="true" />

                    <telerik:RadPane ID="Radpane7" runat="server" Scrolling="None">
                        <telerik:RadSplitter ID="RadSplitterContract" runat="server" Orientation="Horizontal">

                            <telerik:RadPane ID="RadPane8" runat="server" Height="27px" Scrolling="None">
                                <h4>Program Tuition</h4>
                            </telerik:RadPane>

                            <telerik:RadPane ID="Radpane10" runat="server" Height="28px" Scrolling="None">
                                <telerik:RadComboBox runat="server" ID="RadComboBoxCountryMarket" Width="100%" DataSourceID="LinqDataSourceCountryMarket" DataValueField="CountryMarketId" DataCheckedField="" DataTextField="Name" AutoPostBack="True" OnPreRender="RadComboBoxCountryMarket_OnPreRender"></telerik:RadComboBox>

                                <asp:LinqDataSource ID="LinqDataSourceCountryMarket" runat="server"
                                    ContextTypeName="Erp2016.Lib.linqDBDataContext" EntityTypeName=""
                                    TableName="CountryMarkets" OrderBy="Name">
                                </asp:LinqDataSource>

                            </telerik:RadPane>

                            <telerik:RadPane runat="server" Scrolling="None">

                                <telerik:RadGrid ID="RadGridTuition" runat="server" AllowAutomaticDeletes="true" AllowAutomaticInserts="true" AllowAutomaticUpdates="true" Height="100%" OnPreRender="RadGridTuition_OnPreRender" OnItemDataBound="RadGridTuition_ItemDataBound"
                                    PageSize="20" AllowPaging="false" AutoGenerateColumns="false" OnBatchEditCommand="RadGridTuition_BatchEditCommand" DataSourceID="sqlDataSource1" AllowFilteringByColumn="True" OnFilterCheckListItemsRequested="RadGridTuition_OnFilterCheckListItemsRequested"
                                    FilterType="HeaderContext" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" GroupingEnabled="true">
                                    <MasterTableView CommandItemDisplay="Top" DataSourceID="sqlDataSource1"
                                        EditMode="Batch" AutoGenerateColumns="false" CommandItemSettings-AddNewRecordText="Add Item" CommandItemSettings-SaveChangesText="Save Changes Item">
                                        <BatchEditingSettings EditType="Row" />
                                        <Columns>

                                            <telerik:GridTemplateColumn HeaderText="Weeks / Semester" HeaderStyle-Width="100px" SortExpression="Weeks" HeaderStyle-HorizontalAlign="Center" UniqueName="Weeks" DataField="Weeks">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("Weeks") %>' Width="100%" runat="server" Style="text-align: left;" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadComboBox ID="RadComboBoxWeeks" Width="100%" runat="server" AppendDataBoundItems="true" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="1" Value="1" />
                                                            <telerik:RadComboBoxItem Text="2" Value="2" />
                                                            <telerik:RadComboBoxItem Text="3" Value="3" />
                                                            <telerik:RadComboBoxItem Text="4" Value="4" Selected="True" />
                                                            <telerik:RadComboBoxItem Text="5" Value="5" />
                                                            <telerik:RadComboBoxItem Text="6" Value="6" />
                                                            <telerik:RadComboBoxItem Text="7" Value="7" />
                                                            <telerik:RadComboBoxItem Text="8" Value="8" />
                                                            <telerik:RadComboBoxItem Text="9" Value="9" />
                                                            <telerik:RadComboBoxItem Text="10" Value="10" />
                                                            <telerik:RadComboBoxItem Text="11" Value="11" />
                                                            <telerik:RadComboBoxItem Text="12" Value="12" />
                                                            <telerik:RadComboBoxItem Text="13" Value="13" />
                                                            <telerik:RadComboBoxItem Text="14" Value="14" />
                                                            <telerik:RadComboBoxItem Text="15" Value="15" />
                                                            <telerik:RadComboBoxItem Text="16" Value="16" />
                                                            <telerik:RadComboBoxItem Text="17" Value="17" />
                                                            <telerik:RadComboBoxItem Text="18" Value="18" />
                                                            <telerik:RadComboBoxItem Text="19" Value="19" />
                                                            <telerik:RadComboBoxItem Text="20" Value="20" />
                                                            <telerik:RadComboBoxItem Text="21" Value="21" />
                                                            <telerik:RadComboBoxItem Text="22" Value="22" />
                                                            <telerik:RadComboBoxItem Text="23" Value="23" />
                                                            <telerik:RadComboBoxItem Text="24" Value="24" />
                                                            <telerik:RadComboBoxItem Text="25" Value="25" />
                                                            <telerik:RadComboBoxItem Text="26" Value="26" />
                                                            <telerik:RadComboBoxItem Text="27" Value="27" />
                                                            <telerik:RadComboBoxItem Text="28" Value="28" />
                                                            <telerik:RadComboBoxItem Text="29" Value="29" />
                                                            <telerik:RadComboBoxItem Text="30" Value="30" />
                                                            <telerik:RadComboBoxItem Text="31" Value="31" />
                                                            <telerik:RadComboBoxItem Text="32" Value="32" />
                                                            <telerik:RadComboBoxItem Text="33" Value="33" />
                                                            <telerik:RadComboBoxItem Text="34" Value="34" />
                                                            <telerik:RadComboBoxItem Text="35" Value="35" />
                                                            <telerik:RadComboBoxItem Text="36" Value="36" />
                                                            <telerik:RadComboBoxItem Text="37" Value="37" />
                                                            <telerik:RadComboBoxItem Text="38" Value="38" />
                                                            <telerik:RadComboBoxItem Text="39" Value="39" />
                                                            <telerik:RadComboBoxItem Text="40" Value="40" />
                                                            <telerik:RadComboBoxItem Text="41" Value="41" />
                                                            <telerik:RadComboBoxItem Text="42" Value="42" />
                                                            <telerik:RadComboBoxItem Text="43" Value="43" />
                                                            <telerik:RadComboBoxItem Text="44" Value="44" />
                                                            <telerik:RadComboBoxItem Text="45" Value="45" />
                                                            <telerik:RadComboBoxItem Text="46" Value="46" />
                                                            <telerik:RadComboBoxItem Text="47" Value="47" />
                                                            <telerik:RadComboBoxItem Text="48" Value="48" />
                                                            <telerik:RadComboBoxItem Text="49" Value="49" />
                                                            <telerik:RadComboBoxItem Text="50" Value="50" />
                                                            <telerik:RadComboBoxItem Text="51" Value="51" />
                                                            <telerik:RadComboBoxItem Text="52" Value="52" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridTemplateColumn HeaderText="Hours" UniqueName="HrsStatus" DataField="HrsStatus" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <telerik:RadDropDownList runat="server" DataValueField="Value" DataTextField="Name" DataSourceID="LinqDataSource3" SelectedValue='<%# Eval("HrsStatus") %>' Enabled="false" Width="100%" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadComboBox Width="100%" runat="server" AutoPostBack="False" DataValueField="Value" DataTextField="Name" DataSourceID="LinqDataSource3" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    Total
                                                </FooterTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridTemplateColumn HeaderText="Tuition" SortExpression="Tuition" UniqueName="Tuition" DataField="Tuition" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150px">
                                                <ItemTemplate>
                                                    <asp:Label Width="100%" Text='<%# string.Format("{0:$#,##0.00}", Eval("Tuition")) %>' runat="server" Style="text-align: right;" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadNumericTextBox ID="RadNumericTextBoxTuition" runat="server" Type="Currency" Width="100%" EnabledStyle-HorizontalAlign="Right" />
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridButtonColumn ConfirmText="Delete this Item?" ConfirmDialogType="RadWindow" ConfirmDialogHeight="150px"
                                                ConfirmTitle="Delete" HeaderText="Del" HeaderStyle-Width="30px" ButtonType="ImageButton"
                                                CommandName="Delete" Text="Delete" UniqueName="DeleteColumn">
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings AllowKeyboardNavigation="true" EnableRowHoverStyle="True">
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true"></Scrolling>
                                        <Resizing AllowColumnResize="True" AllowRowResize="True" ResizeGridOnColumnResize="True" ShowRowIndicatorColumn="False"
                                            ClipCellContentOnResize="true" EnableRealTimeResize="True" AllowResizeToFit="true" />
                                    </ClientSettings>
                                    <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="true" />
                                </telerik:RadGrid>
                                <asp:LinqDataSource ID="sqlDataSource1" runat="server" ContextTypeName="Erp2016.Lib.linqDBDataContext" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" TableName="ProgramTuitions" OrderBy="Weeks, HrsStatus"
                                       Where="ProgramTuitionId == @ProgramTuitionId">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="0" Name="ProgramTuitionId" Type="Int32" />
                                    </WhereParameters>
                                </asp:LinqDataSource>
                                
                                <asp:LinqDataSource ID="LinqDataSource3" runat="server" ContextTypeName="Erp2016.Lib.linqDBDataContext" EntityTypeName="" TableName="Dicts"
                                    Where="DictType == @DictType">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="150" Name="DictType" Type="Int32" />
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

            function ToolbarClick(sender, args) {
                var button = args.get_item();

                if (button.get_text() === "Update") {
                    if (!confirm('Do you want to update it?'))
                        args.set_cancel(true);
                } else if (button.get_text() === "Save") {
                    if (!confirm('Do you want to save it?'))
                        args.set_cancel(true);
                }
            }

        </script>
    </telerik:RadCodeBlock>

</asp:Content>
