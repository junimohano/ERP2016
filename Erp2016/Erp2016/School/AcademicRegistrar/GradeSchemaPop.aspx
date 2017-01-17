<%@ Page Title="Grade schema name" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="GradeSchemaPop.aspx.cs" Inherits="School.AcademicRegistrar.GradeSchemaPop" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">

        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal">

            <telerik:RadPane ID="RadPane2" runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="RadToolBar1" runat="server" OnButtonClick="RadToolBar1_OnButtonClick">
                    <Items>
                        <telerik:RadToolBarButton ImageUrl="~/assets/img/bt_new.png" Text="Save" />
                        <telerik:RadToolBarButton IsSeparator="True" />
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <telerik:RadPane runat="server">

                <div class="formStyle3">
                    <fieldset>
                        <legend>Grade Info</legend>

                        <div>
                            <label>Grade Name</label>
                            <telerik:RadTextBox runat="server" CssClass="RadSizeMiddle" ID="RadTextBoxName"></telerik:RadTextBox>
                        </div>

                        <div>
                            <label>Grade Type</label>
                            <telerik:RadComboBox runat="server" CssClass="RadSizeMiddle" ID="RadComboBoxIsGlobal">
                                <Items>
                                    <telerik:RadComboBoxItem runat="server" Text="Global" Value="1" />
                                    <telerik:RadComboBoxItem runat="server" Text="Local" Value="0" />
                                </Items>
                            </telerik:RadComboBox>
                        </div>
                    </fieldset>

                    <fieldset>
                        <legend>Program Info</legend>
                        <div>
                            <label>Faculty</label>
                            <telerik:RadComboBox ID="RadComboBoxFaculty" CssClass="RadSizeLarge" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxFaculty_OnSelectedIndexChanged" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                        </div>
                        <div>
                            <label>Program Group</label>
                            <telerik:RadComboBox ID="RadComboBoxProgramGroup" CssClass="RadSizeLarge" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxProgramGroup_OnSelectedIndexChanged" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                        </div>
                        <div>
                            <label>Program</label>
                            <telerik:RadComboBox ID="RadComboBoxProgram" CssClass="RadSizeLarge" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxProgram_OnSelectedIndexChanged" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                        </div>
                        <div>
                            <label>Course</label>
                            <telerik:RadComboBox ID="RadComboBoxProgramCourse" CssClass="RadSizeLarge" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxProgramCourse_OnSelectedIndexChanged" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                        </div>
                        <div>
                            <label>Level</label>
                            <telerik:RadComboBox ID="RadComboBoxProgramCourseLevel" CssClass="RadSizeLarge" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxProgramCourseLevel_OnSelectedIndexChanged" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                        </div>
                        <div>
                            <label>Class</label>
                            <telerik:RadComboBox ID="RadComboBoxProgramClass" CssClass="RadSizeLarge" runat="server" AutoPostBack="False" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
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

        </script>
    </telerik:RadCodeBlock>
</asp:Content>
