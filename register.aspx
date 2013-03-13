<%@ Page Title="" Language="VB" MasterPageFile="~/masterpages/templatefull.master" AutoEventWireup="false" CodeFile="register.aspx.vb" Inherits="register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpcMainContent" Runat="Server">

            <div class="register">
                <h2 class="form-signin-heading">Please register</h2>
                <h3>Items marked with * are requried</h3>
                    <div class="form-signin">
                        <label>Username or email</label>
                        <input type="text" class="input-block-level" placeholder="Email address" />
                        <label>Password (<a href="#">forgot password</a>)</label>
                        <input type="password" class="input-block-level" placeholder="Password" />
                        <label class="checkbox">
                          <input type="checkbox" value="remember-me" /> Register
                        </label>
                        <a class="btn" href="template.aspx">Sign in</a>
                    </div>
            </div>

</asp:Content>

