<%@ Page Title="Log In" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="CNash.Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Log In
    </h2>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <p>
            Please enter your username and password.
            <asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="false">Register</asp:HyperLink> if you don't have an account.
        </p>
            <div class="accountInfo">
                    <fieldset class="login">
                        <legend>Account Information</legend>
                        
                        <p>
                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="txtUserName">Username:</asp:Label>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="userNameEntry"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtUserName" 
                                 CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                                 ValidationGroup="LoginUserValidationGroup" Display="Dynamic" />
                        </p>
                        <p>
                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="txtPassword">Password:</asp:Label>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtPassword" 
                                 CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                                 ValidationGroup="LoginUserValidationGroup" Display="Dynamic" />
                        </p>
                    </fieldset>
                    <p class="submitButton">
                        <asp:Button ID="btnLogin" runat="server" CommandName="Login" Text="Log In" 
                            ValidationGroup="LoginUserValidationGroup" />
                    </p>
                    <p><asp:Label ID="lblWarning" runat="server" CssClass="failureNotification"  /></p>
                </div>
           
        
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>