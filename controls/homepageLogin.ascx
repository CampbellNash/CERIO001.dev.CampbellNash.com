<%@ Control Language="VB" AutoEventWireup="false" CodeFile="homepageLogin.ascx.vb" Inherits="controls_homepageLogin" %>
 <Telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
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
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmailAddress"
                                     ErrorMessage="Please enter a valid email" ValidationGroup="Forgot"
                                     ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="#ffffcc" Display="Dynamic"  /> 
              <br />
                <asp:Button ID="btnSend" Text="Send password &raquo;" runat="server" CssClass="btn btn-large btn-success" ValidationGroup="Forgot" AlternateText="Send password" ToolTip="Send password" ImageAlign="Right" />&nbsp;&nbsp;<asp:Button ID="btnCancelForgot" runat="server" Text="Cancel" CssClass="btn btn-danger btn-large" AlternateText="Cancel" ToolTip="Cancel" ImageAlign="Right"  />
            </asp:Panel>
          <asp:Panel runat="server" ID="panLoggedIn" Visible="false">
              <h2>You are already logged into the CERICO system, click <a href="../mycerico.aspx">here</a> to goto your home page</h2>
          </asp:Panel>  
         <asp:Panel ID="panConfirmSend" runat="server" Visible="false" CssClass="alert alert-success gaptop">
                <strong>Thank you!</strong><br />
                <small>We have sent a mail containing your login credentials to the address you entered, please check for this mail in all your mailbox folders and follow the instructions given. Click the button below to return to the login page.</small><br />
                <asp:Button ID="btnCancel2" runat="server" Text="Return to Login" CssClass="btn btn-danger btn-large" AlternateText="Return to Login" ToolTip="Return to Login" ImageAlign="Right"  />
            </asp:Panel>
         <asp:Panel ID="panNotActivated" runat="server" Visible="false" CssClass="alert alert-block gaptop">
             <asp:Button ID="btnReturn" runat="server" Text="&times;" type="button" class="close"  />  
              <strong>According to our records you have a pending account with us.</strong><br />
              <small>Your account is still awaiting approval, when this happens you will receive a mail confirming your account is ready to use. Click the button below to return to the login page.</small><br />
             <asp:Button ID="btnCancel3" runat="server" Text="Return to Login" CssClass="btn btn-danger btn-large" AlternateText="Return to Login" ToolTip="Return to Login" ImageAlign="Right"  />
            </asp:Panel>         
     </div>
     </Telerik:RadAjaxPanel>
  <Telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <Telerik:AjaxSetting AjaxControlID="RadAjaxPanel1">
                <UpdatedControls>
                    <Telerik:AjaxUpdatedControl ControlID="RadAjaxPanel1" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelRenderMode="Block" />
                </UpdatedControls>
            </Telerik:AjaxSetting>
        </AjaxSettings>
    </Telerik:RadAjaxManager>
    <Telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="MetroTouch" Transparency="0" IsSticky="False" />    