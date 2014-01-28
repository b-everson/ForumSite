<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Posts.aspx.cs" Inherits="Topics" MasterPageFile="~/MasterPage.master" %>
    <asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat ="server">
    <div id="Posts">
        <asp:Panel ID="pnlPosts" runat="server">
            <asp:Button ID ="btnPost" runat="server" Text="Create Post" OnClick="btnPost_Click" />
            <asp:Table ID="tblPosts" runat="server" GridLines="Horizontal">
                <asp:TableRow runat="server">
                    <asp:TableHeaderCell runat="server" CssClass="titleCell">Post Title</asp:TableHeaderCell>
                    <asp:TableHeaderCell runat="server" ColumnSpan="2" CssClass="descCell">Post Text</asp:TableHeaderCell>
                    <asp:TableHeaderCell runat="server" CssClass="nameCell">User</asp:TableHeaderCell>
                    <asp:TableHeaderCell runat="server" CssClass="dateCell">Date</asp:TableHeaderCell>
                </asp:TableRow>
            </asp:Table>
        </asp:Panel>
    </div>
    </asp:Content>