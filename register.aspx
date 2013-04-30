<%@ Page Title="" Language="VB" MasterPageFile="~/masterpages/templatefull.master" AutoEventWireup="false" CodeFile="register.aspx.vb" Inherits="register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpcMainContent" Runat="Server">
    <div class="span6">
            
                <h2 class="form-signin-heading">CERICO Registration</h2>
                <h3>Supplier&#39;s get certified and customers gain confidence, make the connections all for £20 pa.</h3>
                <p>Items marked with <span class="alert-error">*</span> are requried</p>
                    <div class="form-signin form-horizontal">
                         <div class="control-group">
                            <label class="control-label"><asp:Label ID="FirstNameLabel" runat="server" AssociatedControlID="txtFirstName"><span class="alert-error">* </span>First Name:
                            </asp:Label></label>
                            <div class="controls">
                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="input-xlarge"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFirstName"
                                    CssClass="alert-error" ErrorMessage="First Name is required." ToolTip="First Name is required."
                                    ValidationGroup="RegisterValidationGroup" Display="Dynamic" />
                            </div>
                       </div>
                    <div class="control-group">
                            <label class="control-label"><asp:Label ID="SurnameLabel" runat="server" AssociatedControlID="txtSurname"><span class="alert-error">* </span>Surname:</asp:Label></label>
                                <div class="controls">
                                <asp:TextBox ID="txtSurname" runat="server" CssClass="input-xlarge"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSurname"
                                CssClass="alert-error" ErrorMessage="Surname is required." ToolTip="Surname is required."
                                ValidationGroup="RegisterValidationGroup" Display="Dynamic" />
                                    </div>
                       </div>
                         <div class="control-group">
                             <label class="control-label"><asp:Label ID="EmailLabel" runat="server" AssociatedControlID="txtEmailAddress"><span class="alert-error">* </span>Email: <asp:HyperLink ID="hypMailMe" runat="server" Text="Mail Me" Visible="false" /></asp:Label></label><div class="controls">
                      <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="input-xxlarge" />
                          </div>
                       </div>
                         <div class="control-group">
                             <label class="control-label"><asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="txtUserName"><span class="alert-error">* </span>Username:</asp:Label></label><div class="controls">
                       <asp:TextBox ID="txtUserName" runat="server" CssClass="input-xlarge"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtUserName"
                                CssClass="alert-error" ErrorMessage="User Name is required." ToolTip="User Name is required."
                                ValidationGroup="RegisterValidationGroup" Display="Dynamic" />
                         </div>
                       </div>
                       <div class="control-group">
                             <label class="control-label"><asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="txtPassword"><span class="alert-error">* </span>Password:</asp:Label></label><div class="controls">
                           <asp:TextBox ID="txtPassword" runat="server" CssClass="input-xlarge" TextMode="Password" ></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtPassword"
                                CssClass="alert-error" ErrorMessage="Password is required." ToolTip="Password is required."
                                ValidationGroup="RegisterValidationGroup" Display="Dynamic" />
                        </div>
                       </div>
                        <div class="control-group">
                             <label class="control-label"><span class="alert-error">* </span>Confirm Password:</label><div class="controls">
                           <asp:TextBox ID="txtPasswordConfirm" runat="server" CssClass="input-xlarge" TextMode="Password" ></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                                CssClass="alert-error" ErrorMessage="Password is required." ToolTip="Password is required."
                                ValidationGroup="RegisterValidationGroup" Display="Dynamic" />
                        </div>
                       </div>
                         <asp:Button ID="btnRegister" runat="server" Text="Register with CERICO" ValidationGroup="RegisterValidationGroup" CssClass="btn btn-success pull-right" /> </div></div>
    <div class="span6">
        <h3>Why Register with CERICO</h3><p>Simplify supplier vetting and compliance with company policies on supplier due diligence and risk assessment.</p><h3>Key Features</h3><ul>
                    <li>Customisable supplier (external) and purchasing (internal) department questionnaires</li><li>Import commercial research data such as Dow Jones’ Factiva or other research results</li><li>Trigger workfl ows and decision requests based on both internal and external questionnaire results</li><li>Lock results in an auditable archive</li><li>Secure and controlled access to the questionnaires and results based on user role</li></ul><h3>Business Benefits</h3><ul>
                 <li>Apply best practice due diligence to all
supplier relationships</li><li>Translate policies into concrete action</li><li>Manage compliance and reputational risks</li><li>Company policy is reinforced and embedded</li><li>Set up a company-wide auditable archive to
demonstrate compliance</li></ul><h3>Industry Endorsed</h3><p>“<i>We are accelerating the deployment of the Campbell Nash Due Diligence portal, as the creation of a dedicated compliance function and delivering the highest standards of transparency and integrity is the way forward. In the Oil &amp; Gas industry in which we operate, the sector faces unprecedented risk, legal and compliance challenges both in highly competitive andregulated markets and under-regulated countries.</i>” <b>says Marcelo Cardoso, group Head of Compliance at Petrofac</b></p></div></asp:Content>