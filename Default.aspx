<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Trace ="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="FormsStyles.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID ="pnlUser" runat ="server">
    </asp:Panel>
        <asp:SqlDataSource ID="sdsUser" runat="server" CancelSelectOnNullParameter="False" ConnectionString="<%$ ConnectionStrings:ForumDatabaseConnectionString %>" SelectCommand="select dbo.fnGetUser(@username, @password)">
            <SelectParameters>
                <asp:ControlParameter ControlID="hiddenUserName" Name="username" PropertyName="Value" />
                <asp:ControlParameter ControlID="hiddenPassword" Name="password" PropertyName="Value" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:HiddenField ID="hiddenUserName" runat="server" />
        <asp:HiddenField ID="hiddenPassword" runat="server" />
    <div id ="Topics">
        
        <asp:Panel ID="pnlPosts" runat="server">
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ForumDatabaseConnectionString %>" SelectCommand="SELECT [TopicID], [Title], [Content] FROM [Topic]"></asp:SqlDataSource>
            <br />
        </asp:Panel>
    </div>
    </form>
</body>
</html>
