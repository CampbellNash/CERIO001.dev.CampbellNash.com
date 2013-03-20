<%@ Page Title="" Language="VB" MasterPageFile="~/masterpages/login.master" AutoEventWireup="false" CodeFile="login.aspx.vb" Inherits="login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpcTopContent" Runat="Server">
     <div class="span4"></div>
        <div class="span4">
            <div class="standard-login">
                <h2 class="form-signin-heading">Please sign in now</h2>
                    <div class="form-signin">
                        <label>Username or email</label>
                        <asp:TextBox ID="txtEmailaddress" runat="server" class="input-block-level" placeholder="Email address"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEmailAddress" ControlToValidate="txtEmailaddress" CssClass="error" ForeColor="red" runat="server" Display="Dynamic" ErrorMessage="please enter your email address"></asp:RequiredFieldValidator>
                        <label>Password (<a href="#">forgot password</a>)</label>
                        <input type="password" class="input-block-level" placeholder="Password" />
                        <label class="checkbox">
                          <input type="checkbox" value="remember-me" /> Remember me?
                        </label>
                        <asp:LinkButton ID="btnLogin" runat="server" class="btn">Sign in</asp:LinkButton>
                        
                    </div>
            </div>
        </div>
        <div class="span4"></div>
</asp:Content>

