<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Search.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="Search" %>

<asp:Content runat="server" ContentPlaceHolderID="contentTitle">
    <title>MyBus | Search</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="contentMain">
    From:&nbsp;<asp:DropDownList runat="server" AutoPostBack="true" ID="dropdownFrom" OnSelectedIndexChanged="dropdownFrom_SelectedIndexChanged" />
    <asp:RequiredFieldValidator runat="server" ControlToValidate="dropdownFrom" ErrorMessage="Required field" Display="Dynamic" ForeColor="Red" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    To:&nbsp;<asp:DropDownList runat="server" AutoPostBack="true" ID="dropdownTo" OnSelectedIndexChanged="dropdownTo_SelectedIndexChanged" />
    <asp:RequiredFieldValidator runat="server" ControlToValidate="dropdownTo" ErrorMessage="Required field" Display="Dynamic" ForeColor="Red" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    Date (yyyy-MM-dd):&nbsp;<asp:TextBox runat="server" ID="textboxDate" />
    <asp:CustomValidator runat="server" ControlToValidate="textboxDate" ValidateEmptyText="true" Display="Dynamic" OnServerValidate="dateValid_ServerValidate" ErrorMessage="Not a valid date" ForeColor="Red" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    Seats:&nbsp;<asp:TextBox runat="server" ID="textboxSeats" />
    <asp:RangeValidator runat="server" ControlToValidate="textboxSeats" MinimumValue="1" MaximumValue="10" Type="Integer" ErrorMessage="Enter valid data" Display="Dynamic" ForeColor="Red" />
    <br /><br />
    <asp:Button ID="buttonSearch" runat="server" Text="Search" OnClick="buttonSearch_Click" />
</asp:Content>
