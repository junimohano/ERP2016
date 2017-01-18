<%@ Page Title="Hire" Language="C#" MasterPageFile="~/School/Shared/BasePop.master" AutoEventWireup="true" CodeFile="HirePop.aspx.cs" Inherits="School.OfficeAdmin.HirePop" %>

<%@ Import Namespace="Erp2016.Lib" %>

<%@ Register TagPrefix="UserControl" TagName="ApprovalLine" Src="~/App_Data/ApprovalLine.ascx" %>
<%@ Register TagPrefix="UserControl" TagName="FileDownloadList" Src="~/App_Data/FileDownloadList.ascx" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>


<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">
        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Horizontal" ResizeMode="EndPane">

            <telerik:RadPane ID="RadPane4" runat="server" Height="40px" Scrolling="None">
                <telerik:RadToolBar ID="RadToolBar1" runat="server" OnButtonClick="RadToolBar1_ButtonClick" OnClientButtonClicking="RadToolBar1_OnClientButtonClicking">
                    <Items>
                        <telerik:RadToolBarButton runat="server" ImageUrl="../../assets/img/bt_save.png" Text="TempSave" ValidationGroup="Info" />
                        <telerik:RadToolBarButton runat="server" ImageUrl="../../assets/img/bt_mark.png" Text="Request" ValidationGroup="Info" />
                        <telerik:RadToolBarButton runat="server" ImageUrl="../../assets/img/bt_excute.png" Text="Approve" />
                        <telerik:RadToolBarButton runat="server" Text="Accept" />
                        <telerik:RadToolBarButton runat="server" Text="Complete" />
                        <telerik:RadToolBarButton runat="server" ImageUrl="../../assets/img/bt_back.png" Text="Revise" />
                        <telerik:RadToolBarButton runat="server" ImageUrl="../../assets/img/bt_clear.png" Text="Reject" />
                        <telerik:RadToolBarButton runat="server" ImageUrl="../../assets/img/bt_delete.png" Text="Cancel" />
                        <telerik:RadToolBarButton runat="server" Text="Print" />
                        <telerik:RadToolBarButton runat="server" Text="Close" />
                        <telerik:RadToolBarButton IsSeparator="true" />
                    </Items>
                </telerik:RadToolBar>
            </telerik:RadPane>

            <telerik:RadPane ID="RadPane1" runat="server" Scrolling="Both">

                <div id="divPrint">

                    <telerik:RadGrid ID="RadGridInfo" runat="server" GroupPanelPosition="Top" />

                    <UserControl:ApprovalLine ID="ApprovalLine1" runat="server" OnLoad="ApprovalLine1_OnLoad" />

                    <fieldset>
                        <legend>Hire Information</legend>

                        <label><b style="color: red">*</b> Department</label>&nbsp;<telerik:RadComboBox runat="server" ID="RadComboBoxDepartment" CssClass="RadSizeLarge" DataTextField="Name" DataValueField="Value" DataSourceID="LinqDataSourceDepartment" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                        <asp:LinqDataSource ID="LinqDataSourceDepartment" runat="server" ContextTypeName="Erp2016.Lib.linqDBDataContext" EntityTypeName="" TableName="Dicts"
                            Where="DictType == @DictType">
                            <WhereParameters>
                                <asp:Parameter DefaultValue="1359" Name="DictType" Type="Int32" />
                            </WhereParameters>
                        </asp:LinqDataSource>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <label><b style="color: red">*</b> Genre</label>&nbsp;<telerik:RadComboBox runat="server" ID="RadComboBoxGenre" CssClass="RadSizeSmall" DataTextField="Name" DataValueField="Value" DataSourceID="LinqDataSourceGenre" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                        <asp:LinqDataSource ID="LinqDataSourceGenre" runat="server" ContextTypeName="Erp2016.Lib.linqDBDataContext" EntityTypeName="" TableName="Dicts"
                            Where="DictType == @DictType">
                            <WhereParameters>
                                <asp:Parameter DefaultValue="1361" Name="DictType" Type="Int32" />
                            </WhereParameters>
                        </asp:LinqDataSource>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <label><b style="color: red">*</b> Condition</label>&nbsp;<telerik:RadComboBox runat="server" ID="RadComboBoxCondition" CssClass="RadSizeSmall" DataTextField="Name" DataValueField="Value" DataSourceID="LinqDataSourceCondition" Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="True" />
                        <asp:LinqDataSource ID="LinqDataSourceCondition" runat="server" ContextTypeName="Erp2016.Lib.linqDBDataContext" EntityTypeName="" TableName="Dicts"
                            Where="DictType == @DictType">
                            <WhereParameters>
                                <asp:Parameter DefaultValue="1362" Name="DictType" Type="Int32" />
                            </WhereParameters>
                        </asp:LinqDataSource>

                    </fieldset>

                    <UserControl:FileDownloadList ID="FileDownloadList1" runat="server" />

                    <h4>Expense Detail List</h4>

                    <table>
                        <col width="150px" />

                        <tr>
                            <th>
                                <label><b style="color: red">*</b> Job Title</label>
                            </th>
                            <td>
                                <telerik:RadTextBox runat="server" ID="RadTextBoxJobTitle" Width="100%"></telerik:RadTextBox>
                                <br style="clear: both;" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadTextBoxJobTitle" Display="Dynamic" ErrorMessage="Job Title Required" ForeColor="Red" ValidationGroup="Info" />
                            </td>
                        </tr>

                        <tr>
                            <th>
                                <label><b style="color: red">*</b> Reason For Hiring</label>
                            </th>
                            <td>
                                <telerik:RadTextBox runat="server" ID="RadTextBoxReasonForHiring" Width="100%"></telerik:RadTextBox>
                                <br style="clear: both;" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadTextBoxReasonForHiring" Display="Dynamic" ErrorMessage="Reason For Hiring Required" ForeColor="Red" ValidationGroup="Info" />
                            </td>
                        </tr>

                        <tr>
                            <th>
                                <label><b style="color: red">*</b> Duties & Responsibilities</label>
                            </th>
                            <td>
                                <telerik:RadEditor runat="server" ID="RadEditorDuties" ToolbarMode="ShowOnFocus" Width="100%" />
                                <br style="clear: both;" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadEditorDuties" Display="Dynamic" ErrorMessage="Duties & Responsibilities" ForeColor="Red" ValidationGroup="Info" />
                            </td>
                        </tr>

                        <tr>
                            <th>
                                <label><b style="color: red">*</b> Skills, Experience & Qualification</label>
                            </th>
                            <td>
                                <telerik:RadEditor runat="server" ID="RadEditorSkills" ToolbarMode="ShowOnFocus" Width="100%"></telerik:RadEditor>
                                <br style="clear: both;" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadEditorSkills" Display="Dynamic" ErrorMessage="Skills, Experience & Qualification Required" ForeColor="Red" ValidationGroup="Info" />
                            </td>
                        </tr>

                        <tr>
                            <th>
                                <label><b style="color: red">*</b> Salary / Wage</label>
                            </th>
                            <td>
                                <telerik:RadTextBox runat="server" ID="RadTextBoxSalary" Width="100%"></telerik:RadTextBox>
                                <br style="clear: both;" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadTextBoxSalary" Display="Dynamic" ErrorMessage="Salary / Wage Required" ForeColor="Red" ValidationGroup="Info" />
                            </td>
                        </tr>

                        <tr>
                            <th>
                                <label><b style="color: red">*</b> Employment Category</label>
                            </th>
                            <td>
                                <telerik:RadTextBox runat="server" ID="RadTextBoxEmployment" Width="100%"></telerik:RadTextBox>
                                <br style="clear: both;" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadTextBoxEmployment" Display="Dynamic" ErrorMessage="Employment Category Required" ForeColor="Red" ValidationGroup="Info" />
                            </td>
                        </tr>

                        <tr>
                            <th>
                                <label><b style="color: red">*</b> Hours / Days of work</label>
                            </th>
                            <td>
                                <telerik:RadTextBox runat="server" ID="RadTextBoxHours" Width="100%"></telerik:RadTextBox>
                                <br style="clear: both;" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadTextBoxHours" Display="Dynamic" ErrorMessage="Hours / Days of work Required" ForeColor="Red" ValidationGroup="Info" />
                            </td>
                        </tr>

                        <tr>
                            <th>
                                <label><b style="color: red">*</b> Additional Comments</label>
                            </th>
                            <td>
                                <telerik:RadEditor runat="server" ID="RadEditorAdditional" ToolbarMode="ShowOnFocus" Width="100%"></telerik:RadEditor>
                                <br style="clear: both;" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="RadEditorAdditional" Display="Dynamic" ErrorMessage="Additional Comments Required" ForeColor="Red" ValidationGroup="Info" />
                            </td>
                        </tr>

                    </table>

                </div>
            </telerik:RadPane>

        </telerik:RadSplitter>
    </telerik:RadAjaxPanel>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function RadToolBar1_OnClientButtonClicking(sender, args) {
                var button = args.get_item();
                if (button.get_text() == "Request") {
                    if (!confirm('Do you want to Request?'))
                        args.set_cancel(true);
                }
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

            function ShowApprovalWindow(id) {
                var oWnd = window.radopen('ApprovalApprovePop?type=' + <%= (int)CConstValue.Approval.Hire %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalRejectWindow(id) {
                var oWnd = window.radopen('ApprovalRejectPop?type=' + <%= (int)CConstValue.Approval.Hire %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalReviseWindow(id) {
                var oWnd = window.radopen('ApprovalRevisePop?type=' + <%= (int)CConstValue.Approval.Hire %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }


            function ShowApprovalAcceptWindow(id) {
                var oWnd = window.radopen('ApprovalAcceptPop?type=' + <%= (int)CConstValue.Approval.Hire %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalCompleteWindow(id) {
                var oWnd = window.radopen('ApprovalCompletePop?type=' + <%= (int)CConstValue.Approval.Hire %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 300);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function ShowApprovalCancelWindow(id) {
                var oWnd = window.radopen('ApprovalCancelPop?type=' + <%= (int)CConstValue.Approval.Hire %> + '&id=' + id, 0, 0, 0, 0);
                oWnd.setSize(300, 250);
                oWnd.center();
                oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
                oWnd.add_close(OnClientClose);
                return false;
            }

            function OnClientClose(oWnd, args) {
                Close();
                //var arg = args.get_argument();
            <%--<%=Page.GetPostBackEventReference(btnRefresh)%>--%>
            }

            function ShowPrint() {
                $("#divPrint").printArea();
            }

        </script>

    </telerik:RadCodeBlock>
</asp:Content>
