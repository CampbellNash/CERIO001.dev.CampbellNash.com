<%@ Page Title="" Language="VB" MasterPageFile="~/masterpages/login.master" AutoEventWireup="false" CodeFile="login.aspx.vb" Inherits="login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpcTopContent" Runat="Server">
    <asp:UpdatePanel ID="udpMain" runat="server" >
        <ContentTemplate>
      <div class="span4"></div>
        <div class="span4">
            <div class="standard-login">
                <h2 class="form-signin-heading">Please sign in now</h2>
                    <div class="form-signin">
                        <asp:Panel ID="panFormWrapper" runat="server" defaultbutton="btnLogin">
                        <label>Username or email</label>
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="input-block-level" placeholder="Email address" TabIndex="1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEmailAddress" ControlToValidate="txtUserName" CssClass="error" ForeColor="red" runat="server" Display="Dynamic" ErrorMessage="please enter your email address or username"></asp:RequiredFieldValidator>
                        <label>Password (<a href="#">forgot password</a>)</label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="input-block-level" placeholder="Password" TabIndex="2" TextMode="Password" />
                        <asp:RequiredFieldValidator ID="rfvPassword" ControlToValidate="txtPassword" CssClass="error" ForeColor="red" runat="server" Display="Dynamic" ErrorMessage="please enter your password"></asp:RequiredFieldValidator>
                        <label class="checkbox">
                          <input type="checkbox" value="remember-me" /> Remember me
                        </label>
                        <asp:LinkButton ID="btnLogin" runat="server" class="btn">Sign in</asp:LinkButton>
                        <asp:Label ID="lblWarning" runat="server" CssClass="alert-error" />
                        </asp:Panel>
                    </div>
            </div>
        </div>
        <div class="span4"></div>
        </ContentTemplate>
    </asp:UpdatePanel>
 </asp:Content>

