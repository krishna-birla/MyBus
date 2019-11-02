<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignUp.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="SignUp" %>

<asp:Content ContentPlaceHolderID="contentTitle" runat="server">
    <title>MyBus | SignUp</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="contentMain">
    Full Name:&nbsp;<asp:TextBox runat="server" ID="textboxName" />
    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ErrorMessage="Required Field" ControlToValidate="textboxName" ForeColor="Red" />
    <br /><br />
    Username:&nbsp;<asp:TextBox runat="server" ID="textboxUsername" />
    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ErrorMessage="Required Field" ControlToValidate="textboxUsername" ForeColor="Red" />
    <br /><br />
    Password:&nbsp;<asp:TextBox runat="server" ID="textboxPassword" />
    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ErrorMessage="Required Field" ControlToValidate="textboxPassword" ForeColor="Red" />
    <br /><br />
    Confirm Password:&nbsp;<asp:TextBox runat="server" ID="textboxConfirmPassword" />
    <asp:RequiredFieldValidator runat="server" ErrorMessage="Required Field" ControlToValidate="textboxConfirmPassword" ForeColor="Red" />
    <asp:CompareValidator runat="server" Display="Dynamic" ControlToCompare="textboxPassword" ControlToValidate="textboxConfirmPassword" ErrorMessage="Passwords donot match" ForeColor="Red" />
    <br /><br />
    <asp:Label runat="server" ID="labelResult" />
    <br />
    <br />
    <asp:Button runat="server" Text="Sign Up" ID="buttonSignup" OnClick="buttonSignup_Click" />
</asp:Content>
