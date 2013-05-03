<%@ Page Title="" Language="VB" MasterPageFile="~/masterpages/templatefull.master" AutoEventWireup="false" CodeFile="register.aspx.vb" Inherits="register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpcMainContent" Runat="Server">
    <div class="span6">
        <Telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
            <h2 class="form-signin-heading">CERICO Registration</h2>
             <asp:Panel ID="panMain" runat="server" Visible="true">
                <h3>Supplier&#39;s get certified and customers gain confidence, make the connections for NO upfront costs.</h3>
                <p>Items marked with <span class="alert-error">*</span> are requried</p>
                    <div class="form-signin form-horizontal">
                        <div class="control-group">
                            <label class="control-label">
                                <asp:Label ID="Label1" runat="server" AssociatedControlID="cboTitle"><span class="alert-error">* </span>Title:
                                </asp:Label></label>
                            <div class="controls">
                                <asp:DropDownList ID="cboTitle" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="cboTitle"
                                    CssClass="alert-error" ErrorMessage="Please choose a title." ToolTip="Title is required."
                                    ValidationGroup="RegisterValidationGroup" Display="Dynamic" />
                            </div>
                        </div>
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
                                <asp:TextBox ID="txtSurname" runat="server" CssClass="input-xlarge"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSurname"
                                CssClass="alert-error" ErrorMessage="Surname is required." ToolTip="Surname is required."
                                ValidationGroup="RegisterValidationGroup" Display="Dynamic" />
                                    </div>
                       </div>
                         <div class="control-group">
                             <label class="control-label"><asp:Label ID="EmailLabel" runat="server" AssociatedControlID="txtEmailAddress"><span class="alert-error">* </span>Email: <asp:HyperLink ID="hypMailMe" runat="server" Text="Mail Me" Visible="false" /></asp:Label></label><div class="controls">
                            <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="input-xxlarge" /><br />
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEmailAddress"
                                     CssClass="alert-error" ErrorMessage="Email address is required." ToolTip="Email address is required."
                                     ValidationGroup="RegisterValidationGroup" Display="Dynamic" />
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmailAddress"
                                     ErrorMessage="Please enter a valid email" ValidationGroup="RegisterValidationGroup"
                                     ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="alert-error" Display="Dynamic" />
                          </div>
                       </div>
                        
                         <div class="control-group">
                             <label class="control-label"><asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="txtRegisterUserName"><span class="alert-error">* </span>Username:</asp:Label></label><div class="controls">
                       <asp:TextBox ID="txtRegisterUserName" runat="server" CssClass="input-xlarge"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtRegisterUserName"
                                CssClass="alert-error" ErrorMessage="User Name is required." ToolTip="User Name is required."
                                ValidationGroup="RegisterValidationGroup" Display="Dynamic" />
                         </div>
                       </div>
                       <div class="control-group">
                             <label class="control-label"><asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="txtRegisterPassword"><span class="alert-error">* </span>Password:</asp:Label></label><div class="controls">
                           <asp:TextBox ID="txtRegisterPassword" runat="server" CssClass="input-xlarge" TextMode="Password" ></asp:TextBox><br />
                            <ajaxToolkit:PasswordStrength ID="PS" runat="server"
    TargetControlID="txtRegisterPassword"
    DisplayPosition="RightSide"
    StrengthIndicatorType="Text"
    PreferredPasswordLength="6"
    PrefixText="Strength:"
    TextCssClass="alert-error"
    MinimumNumericCharacters="1"
    MinimumSymbolCharacters="0"
    RequiresUpperAndLowerCaseCharacters="false"
    TextStrengthDescriptions="Very Poor;Weak;Average;Strong;Excellent"
    TextStrengthDescriptionStyles="label label-important;label label-info;label label-inverse;label label-warning;label label-success"
    CalculationWeightings="70;30;0;0" />
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtRegisterPassword"
                                CssClass="alert-error" ErrorMessage="Password is required." ToolTip="Password is required."
                                ValidationGroup="RegisterValidationGroup" Display="Dynamic" />
                        </div>
                       </div>
                        <div class="control-group">
                             <label class="control-label"><span class="alert-error">* </span>Confirm Password:</label><div class="controls">
                           <asp:TextBox ID="txtPasswordConfirm" runat="server" CssClass="input-xlarge" TextMode="Password" ></asp:TextBox><br />
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPasswordConfirm"
                                     CssClass="alert-error" ErrorMessage="Password is required." ToolTip="Confirm Password is required."
                                     ValidationGroup="RegisterValidationGroup" Display="Dynamic" />
                           <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Passwords must match!"
                               CssClass="alert-error" ControlToCompare="txtRegisterPassword" ControlToValidate="txtPasswordConfirm"
                               ValidationGroup="RegisterValidationGroup" Display="Dynamic" />
                        </div>
                       </div>
                       <p><asp:Label ID="lblResult" runat="server" CssClass="alert-error" EnableViewState="false" /></p>
                         
                        <asp:Button ID="btnRegister" runat="server" Text="Register with CERICO" ValidationGroup="RegisterValidationGroup" CssClass="btn btn-large btn-success pull-right" />
                         <asp:HyperLink ID="hypCancelRegister" runat="server" CssClass="btn btn-danger btn-large pull-right gapright" NavigateUrl="~/Default.aspx">Cancel</asp:HyperLink>
                    </div>
                    </asp:panel>
                    <asp:Panel ID="panSuccess" runat="server" Visible="false">
                        <p>Registration completed!</p>
                        <p>We have sent you an email to the address you registered with, please check your mailboxes for this and then follow any instructions given.</p>
                        <p>If you don't receive this mail, please use the contact us page and quote Ref: - <strong><asp:Literal ID="litReference" runat="server" /></strong> </p>
                    </asp:Panel>
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
                <Telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Telerik"
                    Transparency="0" IsSticky="False" />
            </div> 
                         
    <div class="span6">
        <h3>Why Register with CERICO</h3><p>Simplify supplier vetting and compliance with company policies on supplier due diligence and risk assessment.</p><h3>Key Features</h3><ul>
                    <li>Customisable supplier (external) and purchasing (internal) department questionnaires</li><li>Import commercial research data such as Dow Jones’ Factiva or other research results</li><li>Trigger workfl ows and decision requests based on both internal and external questionnaire results</li><li>Lock results in an auditable archive</li><li>Secure and controlled access to the questionnaires and results based on user role</li></ul><h3>Business Benefits</h3><ul>
                 <li>Apply best practice due diligence to all
supplier relationships</li><li>Translate policies into concrete action</li><li>Manage compliance and reputational risks</li><li>Company policy is reinforced and embedded</li><li>Set up a company-wide auditable archive to
demonstrate compliance</li></ul><h3>Industry Endorsed</h3><p>“<i>We are accelerating the deployment of the Campbell Nash Due Diligence portal, as the creation of a dedicated compliance function and delivering the highest standards of transparency and integrity is the way forward. In the Oil &amp; Gas industry in which we operate, the sector faces unprecedented risk, legal and compliance challenges both in highly competitive andregulated markets and under-regulated countries.</i>” <b>says Marcelo Cardoso, group Head of Compliance at Petrofac</b></p></div>
   

</asp:Content>