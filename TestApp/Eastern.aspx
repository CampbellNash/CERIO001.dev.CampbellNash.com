<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Eastern.aspx.vb" Inherits="CNash.Eastern" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Eastern Language & SQL Tests</h2>
<p>Click the update button below to submit the chars to the DB</p>
<p><asp:TextBox ID="txtTestString" runat="server" CssClass="textEntry" /></p>
<p><asp:TextBox ID="txtTestPassword" runat="server" CssClass="textEntry" TextMode="Password" /></p>
<p><asp:TextBox ID="txtTestBlob" runat="server" CssClass="textEntry" TextMode="MultiLine"
        Columns="50" Rows="8" /></p>
  <p><asp:Button ID="btnSubmit" runat="server" CssClass="submitButton" Text="Submit" /></p>
  <h2>Ouputs</h2> 
  <p>String: <asp:Literal ID="litTestString" runat="server" /></p>
  <p>Password: <asp:Literal ID="litTestPassword" runat="server" /></p>
  <p>Blob: <asp:Literal ID="litTestBlob" runat="server" /></p>
</asp:Content>
