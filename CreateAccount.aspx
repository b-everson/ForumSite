<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateAccount.aspx.cs" Inherits="CreateAccount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="FormsStyles.css" rel="stylesheet" />
</head>
<body>
    <form id="accountForm" runat="server">
    <div>
        <h1 id ="accountHeader">Create Account</h1>
        <asp:Label ID="Label13" CssClass="label" Text="(*Required Field)" runat="server"></asp:Label>
        <asp:Label ID="Label1" CssClass="label" runat="server" Text="User Name*"></asp:Label>
        <asp:TextBox ID="txtUserName" CssClass="textbox" runat="server" AutoPostBack="True"></asp:TextBox>
        <asp:Label ID="Label2"  CssClass="label" runat="server" Text="Password*"></asp:Label>
        <asp:TextBox ID="txtPassword" CssClass="textbox" TextMode = "Password" runat="server"></asp:TextBox>
        <asp:Label ID="Label3"  CssClass="label" runat="server" Text="Confirm Password*"></asp:Label>
        <asp:TextBox ID="txtConfirmPassword" CssClass="textbox" TextMode ="Password" runat="server"></asp:TextBox>
        <asp:Label ID="Label4"  CssClass="label" runat="server" Text="First Name"></asp:Label>
        <asp:TextBox ID="txtFirstName" CssClass="textbox" runat="server"></asp:TextBox>
        <asp:Label ID="Label5" CssClass="label"  runat="server" Text="Last Name"></asp:Label>
        <asp:TextBox ID="txtLastName" CssClass="textbox" runat="server"></asp:TextBox>
        <asp:Label ID="Label6" CssClass="label"  runat="server" Text="E-mail*"></asp:Label>
        <asp:TextBox ID="txtEmail" CssClass="textbox" runat="server"></asp:TextBox>
        <asp:Label ID="Label7" CssClass="label"  runat="server" Text="Phone"></asp:Label>
        <asp:TextBox ID="txtPhone" CssClass="textbox" TextMode ="Phone" runat="server"></asp:TextBox>
        <asp:Label ID="Label8" CssClass="label"  runat="server" Text="Street 1"></asp:Label>
        <asp:TextBox ID="txtStreet1" CssClass="textbox" runat="server"></asp:TextBox>
        <asp:Label ID="Label9" CssClass="label"  runat="server" Text="Street 2"></asp:Label>
        <asp:TextBox ID="txtStreet2" CssClass="textbox" runat="server"></asp:TextBox>
        <asp:Label ID="Label10" CssClass="label"  runat="server" Text="City"></asp:Label>
        <asp:TextBox ID="txtCity" CssClass="textbox" runat="server"></asp:TextBox>
        <asp:Label ID="Label11" CssClass="label"  runat="server" Text="State"></asp:Label>
        <asp:DropDownList ID="ddlState" runat="server"></asp:DropDownList>
        <asp:Label ID="Label12" CssClass="label"  runat="server" Text="Zip"></asp:Label>
        <asp:TextBox ID="txtZip" CssClass="textbox" runat="server"></asp:TextBox>


        <asp:Label ID="lblMessage" CssClass="label" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
