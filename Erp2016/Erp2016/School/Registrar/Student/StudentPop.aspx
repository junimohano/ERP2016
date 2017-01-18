<%@ Page Title="Student Register" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="StudentPop.aspx.cs" Inherits="School.Registrar.Student.StudentPop" %>

<%@ Register TagPrefix="UserControl" TagName="FileDownloadList" Src="~/App_Data/FileDownloadList.ascx" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>


<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">

        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">
            <telerik:RadPane ID="Radpane5" runat="server" Scrolling="None">
                <telerik:RadSplitter ID="Radsplitter4" runat="server" Orientation="Horizontal">

                    <telerik:RadPane runat="server" Height="40px" Scrolling="None">
                        <telerik:RadToolBar ID="RadToolBar1" runat="server" OnClientButtonClicking="ToolbarButtonClick" OnButtonClick="StudentButtonClicked">
                            <Items>
                                <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_save.png" Text="Save" ToolTip="Save" Enabled="true" ValidationGroup="Info" />
                                <telerik:RadToolBarButton IsSeparator="true" />
                            </Items>
                        </telerik:RadToolBar>
                    </telerik:RadPane>

                    <telerik:RadPane ID="Radpane1" runat="server">
                        <div class="formStyle3">
                            <asp:HiddenField ID="hfID" runat="server" />
                            <asp:HiddenField ID="hfLeadID" runat="server" />

                            <div style="float: left; width: 100%;">
                                <fieldset>
                                    <legend>School</legend>
                                    <div style="float: left; width: 450px;">
                                        <div>
                                            <label><b style="color: red">*</b> Site</label>
                                            <telerik:RadComboBox ID="RadComboBoxSite" EmptyMessage="Choose a Site" CssClass="RadSizeMiddle" runat="server" AutoPostBack="true" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" OnSelectedIndexChanged="RadComboBoxSite_OnSelectedIndexChanged" />
                                            <br style="clear: both;" />
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RadComboBoxSite" Display="Dynamic" ErrorMessage="Site Required" ForeColor="Red" ValidationGroup="Info" />
                                        </div>
                                        <div>
                                            <label><b style="color: red">*</b> Site Location</label>
                                            <telerik:RadComboBox ID="RadComboBoxSiteLocation" CssClass="RadSizeMiddle" runat="server" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True"></telerik:RadComboBox>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RadComboBoxSiteLocation" Display="Dynamic" ErrorMessage="Required" ForeColor="Red" ValidationGroup="Info">Required</asp:RequiredFieldValidator>
                                            <br style="clear: both;" />
                                        </div>
                                        <div>
                                            <label>Marketer Name</label>
                                            <telerik:RadComboBox ID="ddlmarketer" CssClass="RadSizeMiddle" runat="server" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" EmptyMessage="Choose a marketer">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="None" Value="0" />
                                                </Items>
                                            </telerik:RadComboBox>
                                            <br style="clear: both;" />
                                        </div>
                                    </div>
                                </fieldset>
                            </div>

                            <div style="float: left; width: 100%;">

                                <fieldset>
                                    <legend>Basic Info</legend>
                                    <div style="float: left; width: 450px;">
                                        <div>
                                            <label><b style="color: red">*</b> First Name</label>
                                            <telerik:RadTextBox ID="tbFirstName" CssClass="RadSizeMiddle" runat="server" />
                                            <br style="clear: both;" />
                                            <asp:RequiredFieldValidator runat="server" ID="tbFirstNameVali" ControlToValidate="tbFirstName" Display="Dynamic" ErrorMessage="First Name Required" ForeColor="Red" ValidationGroup="Info" />
                                        </div>

                                        <div>
                                            <label>Middle Name 1</label>
                                            <telerik:RadTextBox ID="tbMiddleName1" CssClass="RadSizeMiddle" runat="server" EmptyMessage="(Optional)" /><br style="clear: both;" />
                                        </div>
                                        <div>
                                            <label>Middle Name 2</label>
                                            <telerik:RadTextBox ID="tbMiddleName2" CssClass="RadSizeMiddle" runat="server" EmptyMessage="(Optional)" /><br style="clear: both;" />
                                        </div>

                                        <div>
                                            <label><b style="color: red">*</b> Last Name1</label>
                                            <telerik:RadTextBox ID="tbLastName1" CssClass="RadSizeMiddle" runat="server" />
                                            <br style="clear: both;" />
                                            <asp:RequiredFieldValidator runat="server" ID="tbLastName1Vali" ControlToValidate="tbLastName1" Display="Dynamic" ErrorMessage="Last Name Required" ForeColor="Red" ValidationGroup="Info" />
                                        </div>
                                        <div>
                                            <label>Last Name2</label>
                                            <telerik:RadTextBox ID="tbLastName2" CssClass="RadSizeMiddle" runat="server" EmptyMessage="(Optional)" /><br style="clear: both;" />
                                        </div>
                                    </div>

                                    <div style="float: left; width: 450px;">
                                        <div>
                                            <label><b style="color: red">*</b> Gender</label>
                                            <telerik:RadComboBox ID="ddlGender" CssClass="RadSizeMiddle" runat="server" AppendDataBoundItems="true" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Male" Value="False" />
                                                    <telerik:RadComboBoxItem Text="Female" Value="True" />
                                                </Items>
                                            </telerik:RadComboBox>
                                            <br style="clear: both;" />
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlGender" Display="Dynamic" ErrorMessage="Gender Required" ForeColor="Red" ValidationGroup="Info" />
                                        </div>

                                        <div>
                                            <label><b style="color: red">*</b> Date of Birth</label>
                                            <telerik:RadDatePicker DateInput-DisplayDateFormat="MMM dd, yyyy" ID="tbDateOfBirth" runat="server" CssClass="RadSizeMiddle" DateInput-EmptyMessage="Input Date" MinDate="01/01/1900" MaxDate="01/01/3000" AutoPostBack="true" OnSelectedDateChanged="tbDateOfBirth_OnSelectedDateChanged"></telerik:RadDatePicker>
                                            <br style="clear: both;" />
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="tbDateOfBirth" Display="Dynamic" ErrorMessage="Birth day Required" ForeColor="Red" ValidationGroup="Info" />
                                        </div>

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
                                            <label>Passport No.</label>
                                            <telerik:RadTextBox ID="tbPassport" CssClass="RadSizeMiddle" runat="server" EmptyMessage="(Optional)" /><br style="clear: both;" />
                                        </div>

                                        <div>
                                            <label>Loan No.</label>
                                            <telerik:RadTextBox ID="tbLoanNo" CssClass="RadSizeMiddle" runat="server" EmptyMessage="(Optional)" /><br style="clear: both;" />
                                        </div>
                                    </div>

                                    <div style="float: left; width: 450px;">
                                        <div>
                                            <label><b style="color: red">*</b> Student Type</label>
                                            <telerik:RadComboBox ID="ddlStudentType" CssClass="RadSizeMiddle" runat="server" AppendDataBoundItems="true" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="International" Value="1" />
                                                    <telerik:RadComboBoxItem Text="Domestic" Value="0" />
                                                </Items>
                                            </telerik:RadComboBox>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlStudentType" Display="Dynamic" ErrorMessage="Student Type Required" ForeColor="Red" ValidationGroup="Info" />
                                            <br style="clear: both;" />
                                        </div>

                                        <div>
                                            <label><b style="color: red">*</b> Country</label>
                                            <telerik:RadComboBox ID="ddlCountry" CssClass="RadSizeMiddle" runat="server" AppendDataBoundItems="true" EmptyMessage="Choose a Country"></telerik:RadComboBox>
                                            <br style="clear: both;" />
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlCountry" Display="Dynamic" ErrorMessage="Country Required" ForeColor="Red" ValidationGroup="Info" />
                                        </div>
                                    </div>
                                </fieldset>
                            </div>

                            <div style="float: left; width: 100%;">

                                <fieldset>
                                    <legend>Status</legend>
                                    <div style="float: left; width: 450px;">
                                        <div>
                                            <label><b style="color: red">*</b> Status In Canada</label><telerik:RadComboBox ID="ddlStatus" CssClass="RadSizeMiddle" runat="server" AppendDataBoundItems="true" EmptyMessage="Choose a Visa" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True"></telerik:RadComboBox>
                                            <br style="clear: both;" />
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlStatus" Display="Dynamic" ErrorMessage="Status In Canada Required" ForeColor="Red" ValidationGroup="Info" />
                                        </div>
                                        <div>
                                            <label>Start Date</label><telerik:RadDatePicker DateInput-DisplayDateFormat="MMM dd, yyyy" ID="tbStatusStartDate" runat="server" CssClass="RadSizeMiddle" DateInput-EmptyMessage="Input Date" MinDate="01/01/1900" MaxDate="01/01/3000"></telerik:RadDatePicker>
                                            <br style="clear: both;" />
                                        </div>
                                        <div>
                                            <label>End Date</label><telerik:RadDatePicker DateInput-DisplayDateFormat="MMM dd, yyyy" ID="tbStatusEndDate" runat="server" CssClass="RadSizeMiddle" DateInput-EmptyMessage="Input Date" MinDate="01/01/1900" MaxDate="01/01/3000"></telerik:RadDatePicker>
                                            <br style="clear: both;" />
                                        </div>
                                    </div>

                                    <div style="float: left; width: 450px;">
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
                                    </div>
                                </fieldset>

                            </div>

                            <div style="float: left; width: 100%;">
                                <fieldset>
                                    <legend>Contact</legend>
                                    <div style="float: left; width: 450px;">
                                        <div>
                                            <label>Phone 1</label><telerik:RadTextBox ID="tbPhone1" CssClass="RadSizeMiddle" runat="server" EmptyMessage="(Optional)" />
                                            <br style="clear: both;" />
                                        </div>
                                        <div>
                                            <label>Phone 2</label><telerik:RadTextBox ID="tbPhone2" CssClass="RadSizeMiddle" runat="server" EmptyMessage="(Optional)" /><br style="clear: both;" />
                                        </div>

                                        <div>
                                            <label>Fax</label><telerik:RadTextBox ID="tbFax" CssClass="RadSizeMiddle" runat="server" EmptyMessage="(Optional)" /><br style="clear: both;" />
                                        </div>
                                    </div>

                                    <div style="float: left; width: 450px;">
                                        <asp:Panel ID="pnEmail" runat="server">
                                            <div>
                                                <label>Email 1</label><telerik:RadTextBox ID="tbEmail1" CssClass="RadSizeMiddle" runat="server" EmptyMessage="(Optional)" />
                                                <br style="clear: both;" />
                                            </div>
                                            <div>
                                                <label>Email 2</label><telerik:RadTextBox ID="tbEmail2" CssClass="RadSizeMiddle" runat="server" EmptyMessage="(Optional)" /><br style="clear: both;" />
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </fieldset>
                            </div>

                            <div style="float: left; width: 100%;">
                                <fieldset>
                                    <legend>Insurance</legend>
                                    <div style="float: left; width: 450px;">
                                        <div>
                                            <label><b style="color: red">*</b> Insurance Required</label><telerik:RadComboBox ID="ddlInsurance" CssClass="RadSizeMiddle" OnSelectedIndexChanged="ddlInsurance_SelectedIndexChanged" AutoPostBack="true" runat="server" AppendDataBoundItems="true" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="No" Value="False" />
                                                    <telerik:RadComboBoxItem Text="Yes" Value="True" />
                                                </Items>
                                            </telerik:RadComboBox>
                                            <br style="clear: both;" />
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlInsurance" Display="Dynamic" ErrorMessage="Insurance Required" ForeColor="Red" ValidationGroup="Info" />
                                        </div>
                                        <div>
                                            <label>Start Date</label><telerik:RadDatePicker DateInput-DisplayDateFormat="MMM dd, yyyy" ID="tbInsuranceStartDate" runat="server" CssClass="RadSizeMiddle" DateInput-EmptyMessage="Exemption Date" MinDate="01/01/1900" MaxDate="01/01/3000">
                                            </telerik:RadDatePicker>
                                            <br style="clear: both;" />
                                        </div>
                                        <div>
                                            <label>End Date</label><telerik:RadDatePicker DateInput-DisplayDateFormat="MMM dd, yyyy" ID="tbInsuranceEndtDate" runat="server" CssClass="RadSizeMiddle" DateInput-EmptyMessage="Exemption Date" MinDate="01/01/1900" MaxDate="01/01/3000">
                                            </telerik:RadDatePicker>
                                            <br style="clear: both;" />
                                        </div>
                                    </div>

                                    <div style="float: left; width: 450px;">
                                        <div>
                                            <label>Daily Fee</label><telerik:RadNumericTextBox ID="tbInsuranceDayFee" CssClass="RadSizeMiddle" runat="server" AutoPostBack="true" OnTextChanged="tbInsuranceDayFee_TextChanged" EmptyMessage="(Optional)">
                                            </telerik:RadNumericTextBox><br style="clear: both;" />
                                        </div>
                                        <div>
                                            <label>Total Amount</label><telerik:RadNumericTextBox ID="tbInsuranceTotalAmt" CssClass="RadSizeMiddle" runat="server" EmptyMessage="(Optional)">
                                            </telerik:RadNumericTextBox><br style="clear: both;" />
                                        </div>
                                    </div>
                                </fieldset>
                            </div>

                            <div style="float: left; width: 100%;">
                                <fieldset>
                                    <legend>Permanent Address</legend>
                                    <div style="float: left; width: 450px;">
                                        <div>
                                            <label><b style="color: red">*</b> Address</label><telerik:RadTextBox ID="tbPerAddress" CssClass="RadSizeMultiLine" runat="server" />
                                            <br style="clear: both;" />
                                            <asp:RequiredFieldValidator runat="server" ID="tbPerAddressVali" ControlToValidate="tbPerAddress" Display="Dynamic" ErrorMessage="Address Required" ForeColor="Red" ValidationGroup="Info" />
                                        </div>
                                        <div>
                                            <label><b style="color: red">*</b> City</label><telerik:RadTextBox ID="tbPerCity" CssClass="RadSizeMiddle" runat="server" />
                                            <br style="clear: both;" />
                                            <asp:RequiredFieldValidator runat="server" ID="tbPerCityVali" ControlToValidate="tbPerCity" Display="Dynamic" ErrorMessage="City Required" ForeColor="Red" ValidationGroup="Info" />
                                        </div>
                                        <div>
                                            <label><b style="color: red">*</b> Province</label><telerik:RadTextBox ID="tbPerState" CssClass="RadSizeMiddle" runat="server" />
                                            <br style="clear: both;" />
                                            <asp:RequiredFieldValidator runat="server" ID="tbPerStateVali" ControlToValidate="tbPerState" Display="Dynamic" ErrorMessage="Province Required" ForeColor="Red" ValidationGroup="Info" />
                                        </div>
                                        <div>
                                            <label><b style="color: red">*</b> Postal Code</label><telerik:RadTextBox ID="tbPerZiocode" CssClass="RadSizeMiddle" runat="server" />
                                            <br style="clear: both;" />
                                            <asp:RequiredFieldValidator runat="server" ID="tbPerZiocodeVali" ControlToValidate="tbPerZiocode" Display="Dynamic" ErrorMessage="Postal Code Required" ForeColor="Red" ValidationGroup="Info" />
                                        </div>
                                        <div>
                                            <label><b style="color: red">*</b> Country</label><telerik:RadComboBox ID="ddlPerCountry" CssClass="RadSizeMiddle" runat="server" AppendDataBoundItems="true" EmptyMessage="Choose a Country"></telerik:RadComboBox>
                                            <br style="clear: both;" />
                                            <asp:RequiredFieldValidator runat="server" ID="ddlPerCountryVali" ControlToValidate="ddlPerCountry" Display="Dynamic" ErrorMessage="Choose a country" ForeColor="Red" ValidationGroup="Info" />
                                        </div>
                                    </div>
                                </fieldset>
                            </div>

                            <div style="float: left; width: 100%;">
                                <fieldset>
                                    <legend>Address in Canada</legend>
                                    <div style="float: left; width: 450px;">
                                        <div>
                                            <label><b style="color: red">*</b> Address</label><telerik:RadTextBox ID="tbCadAddress" CssClass="RadSizeMultiLine" runat="server" />
                                            <br style="clear: both;" />
                                            <asp:RequiredFieldValidator runat="server" ID="tbCadAddressVali" ControlToValidate="tbCadAddress" Display="Dynamic" ErrorMessage="Address Required" ForeColor="Red" ValidationGroup="Info"></asp:RequiredFieldValidator>
                                        </div>
                                        <div>
                                            <label><b style="color: red">*</b> City</label><telerik:RadTextBox ID="tbCadCity" CssClass="RadSizeMiddle" runat="server" />
                                            <br style="clear: both;" />
                                            <asp:RequiredFieldValidator runat="server" ID="tbCadCityVali" ControlToValidate="tbCadCity" Display="Dynamic" ErrorMessage="City Required" ForeColor="Red" ValidationGroup="Info"></asp:RequiredFieldValidator>
                                        </div>
                                        <div>
                                            <label><b style="color: red">*</b> Province</label><telerik:RadTextBox ID="tbCadProvince" CssClass="RadSizeMiddle" runat="server" />
                                            <br style="clear: both;" />
                                            <asp:RequiredFieldValidator runat="server" ID="tbCadProvinceVali" ControlToValidate="tbCadProvince" Display="Dynamic" ErrorMessage="Province Required" ForeColor="Red" ValidationGroup="Info"></asp:RequiredFieldValidator>
                                        </div>
                                        <div>
                                            <label><b style="color: red">*</b> Postal Code</label><telerik:RadTextBox ID="tbCadZipcode" CssClass="RadSizeMiddle" runat="server" />
                                            <br style="clear: both;" />
                                            <asp:RequiredFieldValidator runat="server" ID="tbCadZipcodeVali" ControlToValidate="tbCadZipcode" Display="Dynamic" ErrorMessage="Postal Code Required" ForeColor="Red" ValidationGroup="Info"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>

                            <div style="float: left; width: 100%;">
                                <fieldset>
                                    <legend>Emergency Contact</legend>
                                    <div style="float: left; width: 450px;">
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
                                            <asp:RequiredFieldValidator runat="server" ID="tbContactPhoneVali" ControlToValidate="tbContactPhone" Display="Dynamic" ErrorMessage="E.C Phone Required" ForeColor="Red" ValidationGroup="Info"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>

                            <div style="float: left; width: 100%;">
                                <fieldset>
                                    <legend>Comments</legend>
                                    <div style="float: left; width: 450px;">
                                        <telerik:RadTextBox ID="tbComment" TextMode="MultiLine" CssClass="RadSizeMultiLine" runat="server" />
                                        <br style="clear: both;" />
                                    </div>
                                </fieldset>
                            </div>

                            <UserControl:FileDownloadList ID="FileDownloadList1" runat="server" />
                        </div>
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

            function OnClientClose(oWnd, args) {
                Close();
                //var arg = args.get_argument();
        <%--<%=Page.GetPostBackEventReference(btnRefresh)%>--%>
            }

            function Save() {
                alert('created a student successfully');
                window.parent.location.href = window.parent.location.href;
                //Close();
                //var arg = args.get_argument();
        <%--<%=Page.GetPostBackEventReference(btnRefresh)%>--%>
            }

            function ToolbarButtonClick(sender, args) {
                var button = args.get_item();
                if (button.get_text() === "Delete") {
                    if (!confirm('Do you want to delete it?'))
                        args.set_cancel(true);
                }
                if (button.get_text() == "Save") {
                    if (!confirm('Do you want to save it?'))
                        args.set_cancel(true);
                }
            }

        </script>
    </telerik:RadCodeBlock>

</asp:Content>
