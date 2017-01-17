<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ApprovalLine.ascx.cs" Inherits="App_Data.ApprovalLine" %>


<fieldset>
    <legend>Approval line</legend>

    <telerik:RadListView runat="server" ID="ApprovalListView" DataKeyNames="ApprovalHistoryId" DataSourceID="LinqDataSourceApprovalList">
        <AlternatingItemTemplate>
            <td class="rlvA" style="width: 200px;">
                <legend><%# Eval("UserName") %></legend>
                <legend><%# Eval("ApprovalStepName") %></legend>
                <telerik:RadButton runat="server" Text='<%# Eval("ApprovalDate", "{0:g}") %>' CommandName="Select" />
            </td>
        </AlternatingItemTemplate>
        <ItemTemplate>
            <td class="rlvI" style="width: 200px;">
                <legend id="Label1"><%# Eval("UserName") %></legend>
                <legend><%# Eval("ApprovalStepName") %></legend>
                <telerik:RadButton runat="server" Text='<%# Eval("ApprovalDate", "{0:g}") %>' CommandName="Select" />
            </td>
        </ItemTemplate>
        <EmptyDataTemplate>
            <div class="RadListView RadListView_<%# Container.Skin %>">
                <div class="rlvEmpty">
                    There is no approval list.
                </div>
            </div>
        </EmptyDataTemplate>
        <SelectedItemTemplate>
            <td class="rlvI" style="width: 200px;">
                <legend id="LinkButton3"><%# Eval("UserName") %></legend>
                <telerik:RadButton runat="server" Text='<%# Eval("ApprovalMemo") %>' CommandName="Deselect" />
            </td>
        </SelectedItemTemplate>
        <LayoutTemplate>
            <div class="RadListView RadListView_<%# Container.Skin %>">
                <table>
                    <tr>
                        <td id="itemPlaceholder" runat="server"></td>
                    </tr>
                </table>
            </div>
        </LayoutTemplate>
    </telerik:RadListView>
    <asp:LinqDataSource ID="LinqDataSourceApprovalList" runat="server"
        ContextTypeName="Erp2016.Lib.linqViewDataContext" EntityTypeName=""
        TableName="vwApprovalHistories"
        Where="ApprovalHistoryId == @ApprovalHistoryId">
        <WhereParameters>
            <asp:Parameter DefaultValue="0" Name="ApprovalHistoryId" Type="Int32" />
        </WhereParameters>
    </asp:LinqDataSource>
</fieldset>
