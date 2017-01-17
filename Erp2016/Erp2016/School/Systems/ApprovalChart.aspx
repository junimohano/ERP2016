<%@ Page Title="Approval Chart" Language="C#" MasterPageFile="~/School/Shared/Base.master" AutoEventWireup="true" CodeFile="ApprovalChart.aspx.cs" Inherits="School.Systems.ApprovalChart" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">
        <asp:HiddenField runat="server" ID="hfApproveType" Value="1" />

        <telerik:RadSplitter runat="server" ID="RadSplitter1" Height="100%" Width="100%" Orientation="Vertical">
            <telerik:RadPane ID="RadPane1" runat="server" Scrolling="Y">
                <telerik:RadGrid ID="RadGridSupervisor" runat="server" OnNeedDataSource="RadGridSupervisor_OnNeedDataSource">
                    <MasterTableView ShowHeadersWhenNoRecords="true">
                        <NoRecordsTemplate>
                            Drop a user here to change supervisor
                        </NoRecordsTemplate>
                    </MasterTableView>
                </telerik:RadGrid>

                <div>&nbsp;</div>

                <telerik:RadComboBox ID="RadComboBoxChartList" Width="100%" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadComboBoxChartList_SelectedIndexChanged"></telerik:RadComboBox>

                <telerik:RadTreeView ID="trSupervisorChart" runat="server" OnNodeDrop="HandleDrop" MultipleSelect="True" EnableDragAndDrop="true" EnableDragAndDropBetweenNodes="False" OnNodeClick="trSupervisorChart_OnNodeClick" OnClientNodeDropping=" onNodeDropping ">
                </telerik:RadTreeView>

            </telerik:RadPane>

            <telerik:RadSplitBar ID="RadSplitBar1" runat="server"></telerik:RadSplitBar>

            <telerik:RadPane ID="RadPane2" runat="server" Scrolling="Y" Width="75%">
                <telerik:RadGrid ID="RadGridInfo" runat="server" DataSourceID="LinqDataSourceUser">
                    <MasterTableView AutoGenerateColumns="false" ShowHeadersWhenNoRecords="true" DataSourceID="LinqDataSourceUser">
                        <NoRecordsTemplate>
                            no selected
                        </NoRecordsTemplate>

                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Site">
                                <ItemTemplate>
                                    <%# Eval("SiteName") %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Site Location">
                                <ItemTemplate>
                                    <%# Eval("SiteLocationName") %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Login Id">
                                <ItemTemplate>
                                    <%# Eval("LoginId") %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="User Name">
                                <ItemTemplate>
                                    <%# Eval("UserName") %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Position">
                                <ItemTemplate>
                                    <%# Eval("PositionName") %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="IsActive">
                                <ItemTemplate>
                                    <%# Eval("IsActive") %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Phone">
                                <ItemTemplate>
                                    <%# Eval("Phone") %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Email">
                                <ItemTemplate>
                                    <%# Eval("Email") %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>

                    </MasterTableView>
                </telerik:RadGrid>
                <asp:LinqDataSource ID="LinqDataSourceUser" runat="server"
                    ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName=""
                    TableName="vwUsers"
                    Where="UserId == @UserId">
                    <WhereParameters>
                        <asp:Parameter DefaultValue="0" Name="UserId" Type="Int32" />
                    </WhereParameters>
                </asp:LinqDataSource>
            </telerik:RadPane>
        </telerik:RadSplitter>

    </telerik:RadAjaxPanel>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            /* <![CDATA[ */
            var gridId = "<%= RadGridSupervisor.ClientID %>";

            //Handles the OnNodeDropping event of RadTreeView.
            function onNodeDropping(sender, args) {
                var dest = args.get_destNode();

                if (dest) {
                    //Handle dropping on a Node if required.
                } else {
                    dropOnHtmlElement(args);
                }
            }

            //Handles the case when a Node is dropped onto an HTML element.
            function dropOnHtmlElement(args) {
                if (droppedOnGrid(args))
                    return;
            }

            //Checks whether a Node is being dropped onto the RadGrid with ID: 'gridId'.
            //If not, the OnClientDropping event is canceled. 
            function droppedOnGrid(args) {
                var target = args.get_htmlElement();

                while (target) {
                    if (target.id == gridId) {
                        args.set_htmlElement(target);

                        return;
                    }

                    target = target.parentNode;
                }

                args.set_cancel(true);
            }
            /* ]]> */
        </script>
    </telerik:RadCodeBlock>

</asp:Content>
