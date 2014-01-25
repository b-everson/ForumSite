<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateAccount.aspx.cs" Inherits="CreateAccount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="FormsStyles.css" rel="stylesheet" />
    <style type ="text/css">
        #btnCreate {
            clear: left;
            float: left; 
            margin-left: 30%;
        }

        #accountForm *{
            padding-top: 4px;
            padding-bottom: 4px;
        }
    </style>
</head>
<body>
    <form id="accountForm" runat="server">
        <h1 id ="accountHeader">Create Account</h1>
        <label id="Label13" class="label">(*Required Field)</label>
        <label id="Label1" class="label">User Name*</label>
        <asp:TextBox ID="txtUserName" CssClass="textbox" runat="server" AutoPostBack="True"></asp:TextBox>
        <label id="Label2"  class="label">Password*</label>
        <asp:TextBox ID="txtPassword" CssClass="textbox" TextMode = "Password" runat="server"></asp:TextBox>
        <label id="Label3"  class="label">Confirm Password*</label>
        <asp:TextBox ID="txtConfirmPassword" CssClass="textbox" TextMode ="Password" runat="server"></asp:TextBox>
        <label id="Label4"  class="label">First Name</label>
        <asp:TextBox ID="txtFirstName" CssClass="textbox" runat="server"></asp:TextBox>
        <label id="Label5" class="label">Last Name</label>
        <asp:TextBox ID="txtLastName" CssClass="textbox" runat="server"></asp:TextBox>
        <label id="Label6" class="label">E-mail*</label>
        <asp:TextBox ID="txtEmail" CssClass="textbox" runat="server"></asp:TextBox>
        <label id="Label7" class="label">Phone</label>
        <asp:TextBox ID="txtPhone" CssClass="textbox" TextMode ="Phone" runat="server"></asp:TextBox>
        <label id="Label8" class="label">Street 1</label>
        <asp:TextBox ID="txtStreet1" CssClass="textbox" runat="server"></asp:TextBox>
        <label id="Label9" class="label">Street 2</label>
        <asp:TextBox ID="txtStreet2" CssClass="textbox" runat="server"></asp:TextBox>
        <label id="Label10" class="label">City</label>
        <asp:TextBox ID="txtCity" CssClass="textbox" runat="server"></asp:TextBox>
        <label id="Label11" class="label">State</label>
        <asp:DropDownList ID="ddlState" runat="server"></asp:DropDownList>
        <label id="Label12" class="label">Zip</label>
        <asp:TextBox ID="txtZip" CssClass="textbox" runat="server"></asp:TextBox>


        <asp:Label ID="lblMessage" CssClass="label" runat="server"></asp:Label>
        <br />
        <asp:Button ID="btnCreate"  CssClass="accountButton" Text="Create Account" runat="server" UseSubmitBehavior="False" />
    </form>
</body>
</html>
