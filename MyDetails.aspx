<%@ Page Title="" Language="VB" AutoEventWireup="false" MasterPageFile="~/masterpages/templatefull.master" CodeFile="MyDetails.aspx.vb" Inherits="mydetails"  %>
<%@ Register src="controls/submenu1.ascx" tagname="submenu1" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpcMainContent" Runat="Server">
    <div class="span9">
    <h2>My Account details</h2>
    
    <Telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
        
            <asp:Panel ID="panMain" runat="server" Visible="false">
                <h4>Use the form below to update your details. Items marked with <span class="alert-error">*</span> are requried</h4>
               <div class="form-signin form-horizontal">
                    
                         <legend>1. Your details</legend>
                            <div class="control-group">
                                <label class="control-label">Company Name:</label><asp:Label ID="CompanyLabel" runat="server" AssociatedControlID="btnCompanyName" />
                                <div class="controls">
                                    <strong><asp:LinkButton ID="btnCompanyName" runat="server" Text="Company Name" CssClass="btn btn-primary" /></strong>
                                </div>
                           
                            </div>
                        
                                
                            
                   
                         <div class="control-group">
                            <label class="control-label"><asp:Label ID="TitleLabel" runat="server" AssociatedControlID="cboTitle"><span class="alert-error">*</span>Title:</asp:Label></label>
                             <div class="controls">
                             <asp:DropDownList ID="cboTitle" runat="server" />
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cboTitle"
                                CssClass="alert-error" ErrorMessage="Title is required." ToolTip="Title is required."
                                ValidationGroup="LoginUserValidationGroup" Display="Dynamic" />
                             </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label"><asp:Label ID="FirstNameLabel" runat="server" AssociatedControlID="txtFirstName"><span class="alert-error">*</span>First Name:
                            </asp:Label></label>
                            <div class="controls">
                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="input-xlarge"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFirstName"
                                    CssClass="alert-error" ErrorMessage="First Name is required." ToolTip="First Name is required."
                                    ValidationGroup="LoginUserValidationGroup" Display="Dynamic" />
                            </div>
                       </div>
                    <div class="control-group">
                            <label class="control-label"><asp:Label ID="SurnameLabel" runat="server" AssociatedControlID="txtSurname"><span class="alert-error">*</span>Surname:</asp:Label></label>
                                <div class="controls">
                                <asp:TextBox ID="txtSurname" runat="server" CssClass="input-xlarge"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSurname"
                                CssClass="alert-error" ErrorMessage="Surname is required." ToolTip="Surname is required."
                                ValidationGroup="LoginUserValidationGroup" Display="Dynamic" />
                                    </div>
                       </div>
                  <div class="control-group">
                            <label class="control-label"><asp:Label ID="JobLabel" runat="server" AssociatedControlID="txtJobTitle"><span class="alert-error">*</span>Job Title:</asp:Label></label>
                      
                      
                             <div class="controls">
                                 <asp:TextBox ID="txtJobTitle" runat="server" CssClass="input-xlarge"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtJobTitle"
                                CssClass="alert-error" ErrorMessage="Job title is required." ToolTip="Job title is required."
                                ValidationGroup="LoginUserValidationGroup" Display="Dynamic" />
                                 </div>
                        </div>
                    <div class="control-group">
                             <label class="control-label"><asp:Label ID="ContactLabel" runat="server" AssociatedControlID="cboTitle"><span class="alert-error">*</span>Job Type:</asp:Label></label>
                           <div class="controls">
                           <asp:DropDownList ID="cboContactType" runat="server"  />
                          
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="cboContactType"
                                CssClass="alert-error" ErrorMessage="Contact type is required." ToolTip="Contact type is required."
                                ValidationGroup="LoginUserValidationGroup" Display="Dynamic" />
                          </div>
                       </div>
                        <legend>2. Login details</legend>
                   <div class="control-group">
                             <label class="control-label"><asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="txtUserName"><span class="alert-error">*</span>Username:</asp:Label></label>
                       <div class="controls">
                       <asp:TextBox ID="txtUserName" runat="server" CssClass="input-xlarge"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtUserName"
                                CssClass="alert-error" ErrorMessage="User Name is required." ToolTip="User Name is required."
                                ValidationGroup="LoginUserValidationGroup" Display="Dynamic" />
                         </div>
                       </div>
                       <div class="control-group">
                             <label class="control-label"><asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="txtPassword"><span class="alert-error">*</span>Password:</asp:Label></label>
                           <div class="controls">
                           <asp:TextBox ID="txtPassword" runat="server" CssClass="input-xlarge" TextMode="Password" ></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtPassword"
                                CssClass="alert-error" ErrorMessage="Password is required." ToolTip="Password is required."
                                ValidationGroup="LoginUserValidationGroup" Display="Dynamic" />
                        </div>
                       </div>
                        <legend>3. Contact details</legend>
                  <div class="control-group">
                             <label class="control-label"><asp:Label ID="EmailLabel" runat="server" AssociatedControlID="txtEmailAddress"><span class="alert-error">*</span>Email: <asp:HyperLink ID="hypMailMe" runat="server" Text="Mail Me" Visible="false" /></asp:Label></label>
                      <div class="controls">
                      <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="input-xxlarge" />
                          </div>
                       </div>
                       <div class="control-group">
                             <label class="control-label"><asp:Label ID="TelephoneLabel" runat="server" AssociatedControlID="txtTelephoneNumber"><span class="alert-error">*</span>Tel No:</asp:Label></label>
                           <div class="controls">
                           <asp:TextBox ID="txtTelephoneNumber" runat="server" CssClass="input-xlarge"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtTelephoneNumber"
                                CssClass="alert-error" ErrorMessage="Phone number is required." ToolTip="Phone number is required."
                                ValidationGroup="LoginUserValidationGroup" Display="Dynamic" />
                          </div>
                       </div>
                       <div class="control-group">
                             <label class="control-label"><asp:Label ID="MobileLabel" runat="server" AssociatedControlID="txtMobile">Mobile No:</asp:Label></label>
                           <div class="controls">
                           <asp:TextBox ID="txtMobile" runat="server" CssClass="input-xlarge" />
                
                          </div>
                       </div>
                        <legend>4. Social Media</legend>
                       <div class="control-group">
                            <label class="control-label"> <asp:Label ID="FacebookLabel" runat="server" AssociatedControlID="txtFaceBook">Facebook:</asp:Label></label>
                           <div class="controls">
                               <div class="input-prepend">
  <span class="add-on">http://www.facebook.com/</span>
                           <asp:TextBox ID="txtFaceBook" runat="server" CssClass="input-xlarge" />
                                   </div>
                         </div>
                       </div>
                        <div class="control-group">
                             <label class="control-label"><asp:Label ID="TwitterLabel" runat="server" AssociatedControlID="txtTwitter">Twitter:</asp:Label></label>
                            <div class="controls">
                                <div class="input-prepend">
  <span class="add-on">@</span>
                            <asp:TextBox ID="txtTwitter" runat="server" CssClass="input-xlarge" />
                                    </div>
                          </div>
                       </div>
                      
                        
                       
                        <p></p>
                    <p>
                    
                    
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" ValidationGroup="LoginUserValidationGroup" CssClass="btn btn-success" />
                   </p>
                    <p>
                        <asp:Label ID="lblWarning" runat="server" CssClass="alert-error" /></p>
                </div>
            </asp:Panel>

            <asp:Panel ID="panConfirm" runat="server" Visible="false">
                <p>Details were updated successfully!</p>
                <p><asp:HyperLink ID="hypBack" runat="server" NavigateUrl="~/MyDetails.aspx" Text="Click Here" /> to return to form.</p>
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
    <Telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Telerik" Transparency="0" IsSticky="False" />     
        </div>
    <div class="span3">
          <asp:Panel runat="server" ID="panSubNav">
            <uc1:submenu1 ID="submenu11" runat="server" />      
        </asp:Panel>
    </div>
</asp:Content>
