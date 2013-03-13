<%@ Page Title="" Language="VB" MasterPageFile="~/masterpages/login.master" AutoEventWireup="false" CodeFile="login.aspx.vb" Inherits="login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpcTopContent" Runat="Server">
     <div class="span4"></div>
        <div class="span4">
            <div class="standard-login">
                <h2 class="form-signin-heading">Please sign in</h2>
                    <div class="form-signin">
                        <label>Username or email</label>
                        <input type="text" class="input-block-level" placeholder="Email address" />
                        <label>Password (<a href="#">forgot password</a>)</label>
                        <input type="password" class="input-block-level" placeholder="Password" />
                        <label class="checkbox">
                          <input type="checkbox" value="remember-me" /> Remember me
                        </label>
                        <a class="btn" href="template.aspx">Sign in</a>
                    </div>
            </div>
        </div>
        <div class="span4"></div>
</asp:Content>

