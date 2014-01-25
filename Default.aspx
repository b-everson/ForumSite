<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Trace ="true" MasterPageFile="~/MasterPage.master" %>

    <asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat ="server"> 
    
    <div id ="Topics">
        
        <asp:Panel ID="pnlPosts" runat="server">
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ForumDatabaseConnectionString %>" SelectCommand="SELECT [TopicID], [Title], [Content] FROM [Topic]"></asp:SqlDataSource>
            <br />
        </asp:Panel>
    </div>
    
    </asp:Content>
