<%@ Control Language="VB" AutoEventWireup="false" CodeFile="homepageLogin.ascx.vb" Inherits="controls_homepageLogin" %>
<div class="hpform-signin">
         <asp:Label ID="lblLoginError" runat="server" CssClass="requried" />
          <asp:Panel ID="panFormWrapper" runat="server" defaultbutton="btnLogin">
                        <label>Username</label>
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="input-block-level" placeholder="Enter Username" TabIndex="1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEmailAddress" ControlToValidate="txtUserName" CssClass="error" ForeColor="red" runat="server" Display="Dynamic" ErrorMessage="please enter your email address or username"></asp:RequiredFieldValidator>
                        <label>Password (<asp:LinkButton ID="btnForgot" runat="server" Text="forgot password!" CausesValidation="false" />)</label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="input-block-level" placeholder="Password" TabIndex="2" TextMode="Password" />
                        <asp:RequiredFieldValidator ID="rfvPassword" ControlToValidate="txtPassword" CssClass="error" ForeColor="red" runat="server" Display="Dynamic" ErrorMessage="please enter your password"></asp:RequiredFieldValidator>
                        <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-large btn-success" Text="Login to CERICO &raquo;" />
                        <asp:Panel ID="panWarning" runat="server" Visible="false" CssClass="alert alert-block gaptop">
                            <button type="button" class="close" data-dismiss="alert">&times;</button>
                            <asp:Label ID="lblWarning" runat="server" />
                        </asp:Panel>                      
                        <label><a href="register.aspx">Not Registered? Sign up now</a></label>
                        <label><a href="pricing.aspx" >See plans and pricing</a></label>              
          </asp:Panel>
          <asp:Panel ID="panForgot" runat="server" Visible="false">
                <h4>Enter your email address that you registered with into the box below.</h4>
                <label>Email Address:</label>
                <asp:TextBox ID="txtEmailAddress" runat="server" TextMode="SingleLine" CssClass="input-block-level" MaxLength="50" />
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmailAddress" Display="Dynamic" ForeColor="#ffffcc" ValidationGroup="Forgot" ErrorMessage="Please enter email address" />
               <br />
                <asp:Button ID="btnSend" Text="Send password &raquo;" runat="server" CssClass="btn btn-large btn-success" ValidationGroup="Forgot" AlternateText="Send password" ToolTip="Send password" ImageAlign="Right" />&nbsp;&nbsp;<asp:Button ID="btnCancelForgot" runat="server" Text="Cancel" CssClass="btn btn-danger btn-large" AlternateText="Cancel" ToolTip="Cancel" ImageAlign="Right"  />
            </asp:Panel>
            
            <asp:Panel ID="panConfirmSend" runat="server" Visible="false">
                <p>Thank you!</p>
                <p>We have sent a mail containing your login credentials to the address you entered, please check for this mail in all your mailbox folders and follow the instructions given.</p>
            </asp:Panel>
         <asp:Panel ID="panNotActivated" runat="server" Visible="false" CssClass="alert alert-block gaptop">
             <asp:Button ID="btnReturn" runat="server" Text="&times;" type="button" class="close"  />  
              According to our records you have a pending account with us.
                Your account is still awaiting approval, when this happens you will receive a mail confirming your account is ready to use. Click the button below to return to the login page.
             
            </asp:Panel>
                        
                       
                       
                        
     </div>