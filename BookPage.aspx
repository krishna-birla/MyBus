<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BookPage.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="BookPage" %>

<asp:Content runat="server" ContentPlaceHolderID="contentTitle">
    <title>MyBus | Book</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="contentMain">
    Results for buses from&nbsp;<asp:Label ID="labelFrom" runat="server" />&nbsp;to&nbsp;<asp:Label ID="labelTo" runat="server" />&nbsp;on&nbsp;<asp:Label ID="labelDate" runat="server" />&nbsp;for&nbsp;<asp:Label ID="labelSeats" runat="server" />&nbsp;seats.
    <br /><br />
    <asp:CheckBox ID="checkAC" AutoPostBack="true" runat="server" Text="AC Only" OnCheckedChanged="filter_Changed" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:CheckBox ID="checkSleeper" runat="server" AutoPostBack="true" Text="Sleeper Only" OnCheckedChanged="filter_Changed" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    Bus Provider:&nbsp;<asp:DropDownList runat="server" AutoPostBack="true" ID="dropdownBus" OnSelectedIndexChanged="filter_Changed" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    Price Range:&nbsp;<asp:TextBox ID="textboxMinPrice" runat="server" />&nbsp;To&nbsp;<asp:TextBox ID="textboxMaxPrice" runat="server" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="buttonPrice" runat="server" OnClick="filter_Changed" Text="Apply Price Filter" />
    <br /><br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateSelectButton="true" OnSorting="GridView1_Sorting" AllowSorting="true" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AutoGenerateColumns="False" DataKeyNames="Id">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Booking ID" ReadOnly="True" SortExpression="Id" />
            <asp:BoundField DataField="Source" HeaderText="Source" SortExpression="Source" />
            <asp:BoundField DataField="Destination" HeaderText="Destination" SortExpression="Destination" />
            <asp:BoundField DataField="BusOperator" HeaderText="Bus Operator" SortExpression="BusOperator" />
            <asp:BoundField DataField="ACType" HeaderText="AC Type" SortExpression="ACType" />
            <asp:BoundField DataField="SleeperType" HeaderText="Sleeper Type" SortExpression="SleeperType" />
            <asp:BoundField DataField="Departure" HeaderText="Departure" SortExpression="Departure" />
            <asp:BoundField DataField="Arrival" HeaderText="Arrival" SortExpression="Arrival" />
            <asp:BoundField DataField="Price" HeaderText="Rate (Rs)" SortExpression="Price" />
        </Columns>
    </asp:GridView>
    <br /><br />
    Route Selected:&nbsp;<asp:Label ID="labelBook" runat="server" Text="Select a route" />
    <br /><br />
    Amount to be paid:&nbsp;<asp:Label ID="labelAmount" runat="server" Text="Select a route" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button runat="server" ID="buttonBook" Text="Book Tickets" OnClick="buttonBook_Click" />
</asp:Content>
