<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Posts.aspx.cs" Inherits="Posts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="pnlPosts" runat="server">
        <br />
        <asp:Label ID="lblPostTitle" runat="server" Text="Title"></asp:Label>
        <asp:Label ID="lblPostAuthor" runat="server" Text ="Author"></asp:Label>
        <asp:Label ID="lblPostTime" runat="server" Text="Time Posted"></asp:Label>
        <br />
        <asp:Label ID="lblPostContent" runat="server" Text="Content"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="pnlReplies" runat="server" CssClass="replyPanel" >

    </asp:Panel>
</asp:Content>

