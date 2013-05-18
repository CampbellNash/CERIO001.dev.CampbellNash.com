<%@ Page Title="" Language="VB" AutoEventWireup="false" MasterPageFile="~/masterpages/templatefull.master" CodeFile="MyDetails.aspx.vb" Inherits="mydetails"  %>
<%@ Register src="controls/submenu1.ascx" tagname="submenu1" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpcMainContent" Runat="Server">
    <div class="span9">
        <Telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
        <asp:HiddenField ID="hidEmailAddress" runat="server" />
            <h2 class="form-signin-heading">
                CERICO Personal Account Details</h2>
            <asp:Panel ID="panMain" runat="server" Visible="true">
                <h3>
                    Use this section to keep your details up to date</h3>
                <p>
                    Items marked with <span class="alert-error">*</span> are requried</p>
                <div class="form-signin form-horizontal">
                    <div class="control-group">
                        <label class="control-label">
                            <asp:Label ID="Label1" runat="server" AssociatedControlID="cboTitle"><span class="alert-error">* </span>Title:
                            </asp:Label></label>
                        <div class="controls">
                            <asp:DropDownList ID="cboTitle" runat="server" />
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="cboTitle"
                                CssClass="alert-error" ErrorMessage="Please choose a title." ToolTip="Title is required."
                                ValidationGroup="RegisterValidationGroup" Display="Dynamic" />
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                            <asp:Label ID="FirstNameLabel" runat="server" AssociatedControlID="txtFirstName"><span class="alert-error">* </span>First Name:
                            </asp:Label></label>
                        <div class="controls">
                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="input-xlarge"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFirstName"
                                CssClass="alert-error" ErrorMessage="First Name is required." ToolTip="First Name is required."
                                ValidationGroup="RegisterValidationGroup" Display="Dynamic" />
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                            <asp:Label ID="SurnameLabel" runat="server" AssociatedControlID="txtSurname"><span class="alert-error">* </span>Surname:</asp:Label></label>
                        <div class="controls">
                            <asp:TextBox ID="txtSurname" runat="server" CssClass="input-xlarge"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSurname"
                                CssClass="alert-error" ErrorMessage="Surname is required." ToolTip="Surname is required."
                                ValidationGroup="RegisterValidationGroup" Display="Dynamic" />
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                            <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="txtEmailAddress"><span
                                class="alert-error">* </span>Email:
                                <asp:HyperLink ID="hypMailMe" runat="server" Text="Mail Me" Visible="false" /></asp:Label></label><div
                                    class="controls">
                                    <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="input-xxlarge" /><br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEmailAddress"
                                        CssClass="alert-error" ErrorMessage="Email address is required." ToolTip="Email address is required."
                                        ValidationGroup="RegisterValidationGroup" Display="Dynamic" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmailAddress"
                                        ErrorMessage="Please enter a valid email" ValidationGroup="RegisterValidationGroup"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="alert-error" />
                                </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="txtUserName"><span class="alert-error">* </span>Username:</asp:Label></label><div
                                class="controls">
                                <asp:TextBox ID="txtUserName" runat="server" CssClass="input-xlarge"></asp:TextBox><br />
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtUserName"
                                    CssClass="alert-error" ErrorMessage="User Name is required." ToolTip="User Name is required."
                                    ValidationGroup="RegisterValidationGroup" Display="Dynamic" />
                            </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="txtPassword"><span class="alert-error">* </span>Password:</asp:Label></label><div
                                class="controls">
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="input-xlarge" TextMode="Password"></asp:TextBox><br />
                                 <ajaxToolkit:PasswordStrength ID="PS" runat="server"
    TargetControlID="txtPassword"
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
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtPassword"
                                    CssClass="alert-error" ErrorMessage="Password is required." ToolTip="Password is required."
                                    ValidationGroup="RegisterValidationGroup" Display="Dynamic" />
                            </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                            <span class="alert-error">* </span>Confirm Password:</label><div class="controls">
                                <asp:TextBox ID="txtPasswordConfirm" runat="server" CssClass="input-xlarge" TextMode="Password"></asp:TextBox><br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPasswordConfirm"
                                    CssClass="alert-error" ErrorMessage="Password is required." ToolTip="Password is required."
                                    ValidationGroup="RegisterValidationGroup" Display="Dynamic" />
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Passwords must match!"
                                    CssClass="alert-error" ControlToCompare="txtPassword" ControlToValidate="txtPasswordConfirm"
                                    ValidationGroup="RegisterValidationGroup" Display="Dynamic" />
                            </div>
                    </div>
                    <p class="alert-error">Email address changes will require that you will not be able to login again until you respond to the verification mail that we will send to the newly registered address.</p>
                    <p>
                        <asp:Label ID="lblResult" runat="server" CssClass="alert-error" EnableViewState="false" /></p>
                    <asp:Button ID="btnUpdate" runat="server" Text="Update My Details" ValidationGroup="RegisterValidationGroup"
                        CssClass="btn btn-success pull-right" />
                </div>
            </asp:Panel>
            <asp:Panel ID="panSuccess" runat="server" Visible="false">
                <p>
                    You details were updated successfully!</p>
                
                <p><asp:HyperLink ID="hypGoBack" runat="server" NavigateUrl="~/mydetails.aspx" Text="Click Here" /> to make more edits or review your data.</p>
            </asp:Panel>

            <asp:Panel ID="panEmailChanged" runat="server" Visible="false">
                <p>We have logged you out of your account since you updated your email address.</p>
                <p>A new verification mail has been sent to your new email address, please read that and follow any instructions given.</p>
                <p>If you don't find or receive this mail please contact us quoting your account ref: - <strong><asp:Literal ID="litReference" runat="server" /></strong> and we'll try to help you</p>
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
        <Telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="MetroTouch"
            Transparency="0" IsSticky="False" />
    </div>
    <div class="span3">
          <asp:Panel runat="server" ID="panSubNav">
            <uc1:submenu1 ID="submenu11" runat="server" />      
        </asp:Panel>
    </div>
</asp:Content>
