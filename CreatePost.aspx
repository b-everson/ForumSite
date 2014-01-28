<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreatePost.aspx.cs" Inherits="CreatePost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Create A Post</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- Need to get title and content from user only, rest can be taken from page data -->
    <label>Title</label>
    <br />
    <asp:TextBox ID ="txtTitle" runat="server"></asp:TextBox>
    <br />
    <label>Content</label>
    <br />
    <asp:TextBox ID="txtContent" runat="server" Rows="10" TextMode="MultiLine" Width="60%"></asp:TextBox>
    <br />
    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" UseSubmitBehavior="False" />
</asp:Content>

