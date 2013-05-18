<%@ Page Title="" Language="VB" MasterPageFile="~/masterpages/templatefull.master" AutoEventWireup="false" CodeFile="verify.aspx.vb" Inherits="verify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpcMainContent" Runat="Server">
   <h2 class="form-signin-heading">
            CERICO Registration</h2>
    <asp:Label ID="lblErrorMessage" runat="server" CssClass="alert-error" />
    <asp:Panel ID="panMain" Visible="false" runat="server">
        <p>
            <strong>Thank you for confirming your registration</strong>.</p>
        <p>
            Please <asp:HyperLink ID="hypLogin" runat="server" NavigateUrl="~/default.aspx" Text="Click Here" /> to login now.
        </p>
        
    </asp:Panel>

</asp:Content>

