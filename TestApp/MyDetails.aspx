<%@ Page Title="My Details" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MyDetails.aspx.vb" Inherits="CNash.MyDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>My Account details</h2>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="panMain" runat="server" Visible="false">
                <h4>Use the form below to update your details.</h4>
                <div class="accountInfo">
                    <fieldset class="login">
                        <legend>Account Information</legend>
                        <p>
                            <asp:Label ID="CompanyLabel" runat="server" AssociatedControlID="btnCompanyName">
                                Company Name: 
                                <strong><asp:LinkButton ID="btnCompanyName" runat="server" Text="Company Name" /></strong>
                            </asp:Label></p><p>
                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="txtUserName">Username:</asp:Label><asp:TextBox ID="txtUserName" runat="server" CssClass="userNameEntry" Columns="10"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtUserName"
                                CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required."
                                ValidationGroup="LoginUserValidationGroup" Display="Dynamic" />
                        </p>
                        <p>
                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="txtPassword">Password:</asp:Label><asp:TextBox ID="txtPassword" runat="server" CssClass="passwordEntry" TextMode="Password" Columns="10"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtPassword"
                                CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                                ValidationGroup="LoginUserValidationGroup" Display="Dynamic" />
                        </p>
                        <p>
                            <asp:Label ID="TitleLabel" runat="server" AssociatedControlID="cboTitle">Title:</asp:Label><asp:DropDownList ID="cboTitle" runat="server" CssClass="textEntry" />
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cboTitle"
                                CssClass="failureNotification" ErrorMessage="Title is required." ToolTip="Title is required."
                                ValidationGroup="LoginUserValidationGroup" Display="Dynamic" />
                        </p>
                        <p>
                            <asp:Label ID="FirstNameLabel" runat="server" AssociatedControlID="txtFirstName">First Name:</asp:Label><asp:TextBox ID="txtFirstName" runat="server" CssClass="textEntry"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFirstName"
                                CssClass="failureNotification" ErrorMessage="First Name is required." ToolTip="First Name is required."
                                ValidationGroup="LoginUserValidationGroup" Display="Dynamic" />
                        </p>
                        <p>
                            <asp:Label ID="SurnameLabel" runat="server" AssociatedControlID="txtSurname">Surname:</asp:Label><asp:TextBox ID="txtSurname" runat="server" CssClass="textEntry"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSurname"
                                CssClass="failureNotification" ErrorMessage="Surname is required." ToolTip="Surname is required."
                                ValidationGroup="LoginUserValidationGroup" Display="Dynamic" />
                        </p>
                        <p>
                            <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="txtEmailAddress">Email: <asp:HyperLink ID="hypMailMe" runat="server" Text="Mail Me" Visible="false" /></asp:Label><asp:TextBox ID="txtEmailAddress" runat="server" CssClass="textEntry" />
                        </p>
                        <p>
                            <asp:Label ID="TelephoneLabel" runat="server" AssociatedControlID="txtTelephoneNumber">Tel No:</asp:Label><asp:TextBox ID="txtTelephoneNumber" runat="server" CssClass="textEntry"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtTelephoneNumber"
                                CssClass="failureNotification" ErrorMessage="Phone number is required." ToolTip="Phone number is required."
                                ValidationGroup="LoginUserValidationGroup" Display="Dynamic" />
                        </p>
                        <p>
                            <asp:Label ID="MobileLabel" runat="server" AssociatedControlID="txtMobile">Mobile No:</asp:Label><asp:TextBox ID="txtMobile" runat="server" CssClass="textEntry" />
                
                        </p>
                        <p>
                            <asp:Label ID="FacebookLabel" runat="server" AssociatedControlID="txtFaceBook">Facebook:</asp:Label><asp:TextBox ID="txtFaceBook" runat="server" CssClass="textEntry" />
                        </p>
                        <p>
                            <asp:Label ID="TwitterLabel" runat="server" AssociatedControlID="txtTwitter">Twitter:</asp:Label><asp:TextBox ID="txtTwitter" runat="server" CssClass="textEntry" />
                        </p>
                        <p>
                            <asp:Label ID="ContactLabel" runat="server" AssociatedControlID="cboTitle">Job Type:</asp:Label><asp:DropDownList ID="cboContactType" runat="server" CssClass="textEntry" />
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="cboContactType"
                                CssClass="failureNotification" ErrorMessage="Contact type is required." ToolTip="Contact type is required."
                                ValidationGroup="LoginUserValidationGroup" Display="Dynamic" />
                        </p>
                        <p>
                            <asp:Label ID="JobLabel" runat="server" AssociatedControlID="txtJobTitle">Job Title:</asp:Label><asp:TextBox ID="txtJobTitle" runat="server" CssClass="textEntry"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtJobTitle"
                                CssClass="failureNotification" ErrorMessage="Job title is required." ToolTip="Job title is required."
                                ValidationGroup="LoginUserValidationGroup" Display="Dynamic" />
                        </p>
                    </fieldset>
                      <p class="submitButton">
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" ValidationGroup="LoginUserValidationGroup" CssClass="login" />
                    </p>
                    <p>
                        <asp:Label ID="lblWarning" runat="server" CssClass="failureNotification" /></p>
                </div>
            </asp:Panel>

            <asp:Panel ID="panConfirm" runat="server" Visible="false">
                <p>Details were updated successfully!</p>
                <p><asp:HyperLink ID="hypBack" runat="server" NavigateUrl="~/MyDetails.aspx" Text="Click Here" /> to return to form.</p>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
