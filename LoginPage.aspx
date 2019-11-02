<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="LoginPage.aspx.cs" Inherits="LoginPage" %>

<asp:Content runat="server" ContentPlaceHolderID="contentTitle">
    <title>MyBus | Login</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="contentMain">
    Username:&nbsp;<asp:TextBox runat="server" ID="textUsername" />
    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="textUsername" ErrorMessage="Field Required" ForeColor="Red" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    Password:&nbsp;<asp:TextBox runat="server" ID="textPassword" />
    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="textPassword" ErrorMessage="Field Required" ForeColor="Red" />
    <br /><br />
    <asp:Label ID="labelResults" runat="server" />
    <br />
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button runat="server" ID="buttonLogin" Text="Login" OnClick="buttonLogin_Click" />
    &nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button runat="server" ID="buttonSignup" Text="Not a member? Sign Up!" Style="width:300px" CausesValidation="false" OnClick="buttonSignup_Click" />
</asp:Content>