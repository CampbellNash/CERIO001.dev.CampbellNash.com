<%@ Page Language="VB" AutoEventWireup="false" CodeFile="questionnairepopup.aspx.vb" Inherits="standardquestionnaire" EnableEventValidation="false" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="~/css/bootstrap.min.css" rel="stylesheet" media="screen" runat="server" />
    <link href="~/css/bootstrap-responsive.min.css" rel="stylesheet" media="screen" runat="server" />
    <title>CERICO Questionnaire's</title>
</head>
<body>
    <form id="form1" runat="server">
    <Telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </Telerik:RadScriptManager>

     
   <script type="text/javascript">
       function UploadFile()
       {
           document.getElementById('btnUpload').click();// This used to purge uploaded files to the target folder
       }
       Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoadedHandler)

       function pageLoadedHandler(sender, args) {

           //window.scrollTo(0, 0);

       }

       function GetRadWindow() {
           var oWindow = null;
           if (window.radWindow)
               oWindow = window.radWindow;
           else if (window.frameElement.radWindow)
               oWindow = window.frameElement.radWindow;
           return oWindow;
       }

       function Close() {
           var arg = new Object();
           var oWnd = GetRadWindow();
           oWnd.close(arg);
       }
     
              
       
   
       
   </script>
         
         <Telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
         <div class="container-fluid">
             <div class="row-fluid">
             <div class="span12">
             <asp:Button ID="btnUpload" runat="server" Style="visibility: hidden; float: right" />
             <asp:HiddenField ID="hidReadOnly" runat="server" Value="False" />
             <a href="#" id="moreinfo" class="btn pull-right" rel="popover" data-placement="bottom"  title="Conflict Minerals – Due Diligence Questionnaire" data-original-title="Conflict Minerals – Due Diligence Questionnaire">More info</a>
             <h4 runat="server" id="placeholder">Conflict Minerals – Due Diligence Questionnaire</h4>
        
        <asp:Panel ID="panWindowClose" runat="server" Visible="false">
            <script type="text/javascript">
                // This will force the pop to close in event of no login
                // That in turn will cause the underlying page to be posted back
                // leading to the login page bei9ng displayed. 
                Close();
            </script>
        </asp:Panel> 
        
        <asp:Panel ID="panNoQuery" runat="server" Visible="false">
            <p>Incorrect credentials passed for this function.</p>
            <p>Please use the navigation panel to the right to open this page.</p>
        </asp:Panel>
        
            
        <asp:Panel ID="panSaveDraft" runat="server" Visible="false">
            <p>The data you have entered so far in this form has been saved!</p>
            <p>You may re-open it now by clicking the button below.</p>
            <p>Or you can open it anytime again by using your activity panel on the right.</p>
            <p><asp:LinkButton ID="btnReOpen" runat="server" CssClass="btn btn-warning" Text="Re Open" /></p>
        </asp:Panel>

        <asp:Panel ID="panClosed" runat="server" Visible="false">
            <p>Thank you for your answers!</p>
            <p>Your customers &amp; suppliers can now access your ceritfication</p>
            <p><asp:HyperLink ID="hypMyCerico" runat="server" NavigateUrl="JavaScript:Close();" Text="Click Here" /> to close this window.</p>
        </asp:Panel>
        
        <asp:Panel ID="panForm" runat="server" Visible="true" CssClass="">
            
             
                <h5>Questionnaire Progress</h5>
               <div class="progress">
                   <div class="bar" runat="server" id="divProgressbar"></div>
                   <asp:Label ID="lblProgress" runat="server" CssClass="bar" Width="166px" Visible="true" />
               </div>
            <asp:LinkButton ID="btnTopSave" runat="server" CssClass="btn btn-mini pull-right btn-warning" Text="Save Draft" CausesValidation="false" />
            <asp:LinkButton ID="btnTopNext" runat="server" CssClass="btn btn-mini pull-right" Text="Next Page &raquo;" ValidationGroup="Questions" />
                        <asp:LinkButton ID="btnTopPrev" runat="server" CssClass="btn btn-mini pull-right" Text=" &laquo; Prev Page" />
                        
               <p>Items marked with <span class="alert-error">*</span> are requried</p>
               <div class="form-signin form-horizontal">
                    <asp:Panel ID="panPage1" runat="server" Visible="false">
                       <legend>1. Full name and address of supplier</legend>
                       <div class="control-group">
                           <label class="control-label"><span class="alert-error">*</span>Company Name:</label>
                           <div class="controls">
                                <asp:TextBox ID="txtCompanyName" runat="server" TextMode="SingleLine" CssClass="input-xxlarge" placeholder="Enter company name" />
                                <asp:RequiredFieldValidator ID="rfvCompanyName" runat="server" ControlToValidate="txtCompanyName" Display="Dynamic" ErrorMessage="Please enter company name" ValidationGroup="Questions" CssClass="alert-error" />
                           </div>
                       </div>
                        
                        <div class="control-group">
                            <label class="control-label"><span class="alert-error">*</span>Company Number:</label>
                            <div class="controls">
                                 <asp:TextBox ID="txtCompanyNumber" runat="server" TextMode="SingleLine" CssClass="input-xlarge" placeholder="Enter company number" />
                                 <asp:RequiredFieldValidator ID="rfvCompanyNumber" runat="server" ControlToValidate="txtCompanyNumber" Display="Dynamic" ErrorMessage="Please enter company number" ValidationGroup="Questions" CssClass="alert-error" />
                            </div>

                        </div>
                        
                        <div class="control-group">
                            <label class="control-label"><span class="alert-error">*</span>Address line 1:</label>
                            <div class="controls">
                                 <asp:TextBox ID="txtAddress1" runat="server" TextMode="SingleLine" CssClass="input-xlarge" placeholder="Company Address line 1" />
                        <asp:RequiredFieldValidator ID="rfvAddress1" runat="server" ControlToValidate="txtAddress1" Display="Dynamic" ErrorMessage="Please enter address" ValidationGroup="Questions" CssClass="alert-error" />
                      
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label">Address line 2:</label>
                            <div class="controls">
                                 <asp:TextBox ID="txtAddress2" runat="server" TextMode="SingleLine" CssClass="input-xlarge" placeholder="Company Address line 2" />
                       
                            </div>
                        </div>
                        
                        <div class="control-group">
                            
                            <label class="control-label">District:</label>
                            <div class="controls">
                                  <asp:TextBox ID="txtDistrict" runat="server" TextMode="SingleLine" CssClass="input-xlarge" placeholder="Enter District" />
                     
                            </div>
                        </div>
                        
                        <div class="control-group">
                            <label class="control-label"><span class="alert-error">*</span>City:</label>
                            <div class="controls form-inline">
                                
                                  <asp:TextBox ID="txtCity" runat="server" TextMode="SingleLine" CssClass="input-large" placeholder="Enter company city" />
                                    <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="txtCity" Display="Dynamic" ErrorMessage="Please enter city" ValidationGroup="Questions" CssClass="alert-error" />
                            
                               <label ><span class="alert-error">*</span>Postcode:</label>
                                <asp:TextBox ID="txtPostcode" runat="server" TextMode="SingleLine" CssClass="input-small" placeholder="Postcode" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPostcode" Display="Dynamic" ErrorMessage="Please enter Post code" ValidationGroup="Questions" CssClass="alert-error" />
                   
                            </div>
                        </div>
                        
                         <div class="control-group">
                             
                              <label class="control-label"><span class="alert-error">*</span>Region/State/Province:</label>
                            <div class="controls">
                                <asp:TextBox ID="txtState" runat="server" TextMode="SingleLine" CssClass="input-xlarge" placeholder="Enter company state or region" />
                        <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="txtState" Display="Dynamic" ErrorMessage="Please enter state or region" ValidationGroup="Questions" CssClass="alert-error" />
                      
                            </div>
                         </div>
                        
                        <div class="control-group">
                            <label class="control-label"><span class="alert-error">*</span>Country:</label>
                            <div class="controls">
                                    <asp:DropDownList runat="server" ID="cboCountries"/>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="cboCountries" Display="Dynamic" ErrorMessage="Please enter city" ValidationGroup="Questions" CssClass="alert-error" />
                            </div>
                        </div>
                        
                        <div class="control-group">
                            <label class="control-label"><span class="alert-error">*</span>Telephone:</label>
                            <div class="controls form-inline">
                                <asp:TextBox ID="txtTelephone" runat="server" TextMode="SingleLine" CssClass="input-large" placeholder="Telephone number" />
                                <asp:RequiredFieldValidator ID="rfvTelephone" runat="server" ControlToValidate="txtState" Display="Dynamic" ErrorMessage="Please enter the telephone number" ValidationGroup="Questions" CssClass="alert-error" />
                                <label> <span class="alert-error">*</span>Fax:</label>
                                 <asp:TextBox ID="txtFax" runat="server" TextMode="SingleLine" CssClass="input-large" placeholder="Fax number" />
                        <asp:RequiredFieldValidator ID="rfvFax" runat="server" ControlToValidate="txtState" Display="Dynamic" ErrorMessage="Please enter the Fax number" ValidationGroup="Questions" CssClass="alert-error" />
            
                            </div>
                                
                        </div>
                      
                        
                        <div class="control-group">
                            <label class="control-label"><span class="alert-error">*</span>Web Address:</label>
                            <div class="controls">  
                                <asp:TextBox ID="txtWebAddress" runat="server" TextMode="SingleLine" CssClass="input-xxlarge" placeholder="Enter company web address" />
                                <asp:RequiredFieldValidator ID="rfvWebAddress" runat="server" ControlToValidate="txtWebAddress" Display="Dynamic" ErrorMessage="Please enter web address" ValidationGroup="Questions" CssClass="alert-error" />
                            </div>

                        </div>
                        
                        <div class="control-group">
                            <label class="control-label">Business Type:</label>
                            <div class="controls">
                                <asp:DropDownList ID="cboBusinessType" runat="server" />
                                <asp:RequiredFieldValidator ID="rfvBusinessType" runat="server" ControlToValidate="cboBusinessType" Display="Dynamic" ErrorMessage="Please choose business type" ValidationGroup="Questions" CssClass="alert-error" />
                            </div>
                        </div>
                        
                        <div class="control-group">
                            <label class="control-label"><span class="alert-error">*</span>Contact Person:</label>
                            <div class="controls">
                                <asp:TextBox ID="txtContact" runat="server" TextMode="SingleLine" CssClass="input-xlarge" placeholder="Enter company contact" />
                                <asp:RequiredFieldValidator ID="rfvContact" runat="server" ControlToValidate="txtContact" Display="Dynamic" ErrorMessage="Please enter company contact" ValidationGroup="Questions" CssClass="alert-error" />
                                   </div>
                        </div>
                        
                        <div class="control-group">
                             <label class="control-label"> <span class="alert-error">*</span>Contact Email:</label>
                            <div class="controls"> 
                            <asp:TextBox ID="txtContactEmail" runat="server" TextMode="SingleLine" CssClass="input-xxlarge" placeholder="Enter company conytact email" />
                             <asp:RequiredFieldValidator ID="rfvContactEmail" runat="server" ControlToValidate="txtContactEmail" Display="Dynamic" ErrorMessage="Please enter company contact email" ValidationGroup="Questions" CssClass="alert-error" />
                              <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtContactEmail" ErrorMessage="Please enter a valid email" ValidationGroup="Questions" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="alert-error"></asp:RegularExpressionValidator>
                        
                            </div>
                        </div>

                    </asp:Panel>

                    <asp:Panel ID="panPage2" runat="server" Visible="false">
                        <legend>2. Company details</legend>
                        <div class="control-group">
                            <label class="control-label"><span class="alert-error">*</span>2.1 Please list the countries in which the supplier operates or sources materials from:</label>
                            <div class="controls">
                                 <asp:TextBox ID="txtCountryList" runat="server" TextMode="MultiLine" CssClass="input-xxlarge" Rows="4" placeholder="Enter at least one country" />
                                <span class="help-block">Enter a country on each line</span>
                                <asp:RequiredFieldValidator ID="rfvCountryList" runat="server" ControlToValidate="txtCountryList" Display="Dynamic" ErrorMessage="Please enter at least one country" ValidationGroup="Questions" CssClass="alert-error" />
                       
                                

                            </div>
                        </div>      
                        
                         <div class="control-group">
                            <label class="control-label">2.2 Does the Supplier have any subsidiaries or a parent company?</label>
                            <div class="controls">
                                 <asp:RadioButtonList ID="rblParent" runat="server" AutoPostBack="true" RepeatColumns="4">
                                    <asp:ListItem Text="No" />
                                    <asp:ListItem Text="Yes" />
                                </asp:RadioButtonList>

                            </div>

                        </div>
                        
                        
                            
                            <asp:Panel ID="panParentCompanies" runat="server" Visible="false" CssClass="dependant">
                            <div class="control-group">
                                <label>2.2.1 You have selected YES, please list these in the table provided:</label>
                                <div>
                                    <table class="table table-bordered">
                                <tr>
                                    <th>Name of subsidiary/parent</th>
                                    <th>Registered number of subsidiary</th>
                                    <th>Country of registration of subsidiary</th>
                                    <th>&#37; of subsidiary owned by Service </th>
                                    <th>Control</th>
                                </tr>
                                <asp:Repeater ID="rptParentCompany" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtParentCompanyName" runat="server" TextMode="SingleLine" CssClass="input-large" placeholder="Enter company name" />
                                                <asp:RequiredFieldValidator ID="rfvParent" runat="server" ControlToValidate="txtParentCompanyName" Display="Dynamic" ErrorMessage="Please enter company name" ValidationGroup="Questions" CssClass="alert-error" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtParentCompanyNumber" runat="server" TextMode="SingleLine" CssClass="input-small" placeholder="Enter company number" />
                                                <asp:RequiredFieldValidator ID="rfvCompanyNumber" runat="server" ControlToValidate="txtParentCompanyNumber" Display="Dynamic" ErrorMessage="Please enter company number" ValidationGroup="Questions" CssClass="alert-error" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtParentCountry" runat="server" TextMode="SingleLine" CssClass="input-large" placeholder="Enter country of registration" />
                                                <asp:RequiredFieldValidator ID="rfvParentCountry" runat="server" ControlToValidate="txtParentCountry" Display="Dynamic" ErrorMessage="Please enter country of registration" ValidationGroup="Questions" CssClass="alert-error" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPercentOwned" runat="server" TextMode="SingleLine" CssClass="input-mini" placeholder="&#37; company owned" />
                                                <asp:RequiredFieldValidator ID="rfvPercentOwned" runat="server" ControlToValidate="txtPercentOwned" Display="Dynamic" ErrorMessage="Please enter &#37; of company owned" ValidationGroup="Questions" CssClass="alert-error" />
                                            </td>
                                            <td><asp:Button ID="btnDeleteParent" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="DeleteParentLine" /><asp:HiddenField ID="hidItemID" runat="server" /></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                                    
                                    <asp:LinkButton ID="btnAddNewParent" runat="server" Text="Add New Line" Visible="false" CssClass="btn btn-success" />
                                </div>
                               
                            </div>
                           
                        </asp:Panel>
                        <div class="control-group">
                                <label>2.3 List all owners, partners or shareholders of the Supplier:</label>
                                <div>
                                    <table class="table table-bordered">
                                        <tr>
                                            <th>Name</th>
                                            <th>Nationality</th>
                                            <th colspan="2">Ownership &#37;</th>
                                        </tr>
                                        <asp:Repeater ID="rptShareholders" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtShareholderName" runat="server" TextMode="SingleLine" CssClass="input-xlarge" placeholder="Enter shareholder name" />
                                                        <asp:RequiredFieldValidator ID="rfvParent" runat="server" ControlToValidate="txtShareholderName" Display="Dynamic" ErrorMessage="Please enter shareholder name" ValidationGroup="Questions" CssClass="alert-error" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtShareholderNationality" runat="server" TextMode="SingleLine" CssClass="input-xlarge" placeholder="Enter company number" />
                                                        <asp:RequiredFieldValidator ID="rfvCompanyNumber" runat="server" ControlToValidate="txtShareholderNationality" Display="Dynamic" ErrorMessage="Please enter nationality" ValidationGroup="Questions" CssClass="alert-error" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtPercentOwned" runat="server" TextMode="SingleLine" CssClass="input-xlarge" placeholder="&#37; company owned" />
                                                        <asp:RequiredFieldValidator ID="rfvPercentOwned" runat="server" ControlToValidate="txtPercentOwned" Display="Dynamic" ErrorMessage="Please enter &#37; of company owned" ValidationGroup="Questions" CssClass="alert-error" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnDeleteShareholder" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="DeleteShareholderLine" /><asp:HiddenField ID="hidItemID" runat="server" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                    <span class="help-block">If any of the persons listed are not individuals, please provide the ownership information for those persons. Percentage should total 100%</span>
                                    <asp:LinkButton ID="btnAddNewShareholder" runat="server" Text="Add New Line" CssClass="btn btn-success" />
                                    <hr />
                                </div>

                            </div>
                        

                        <div class="control-group">
                            <label >2.4 List all directors (or equivalent) of the Supplier (including those already listed at above):</label>
                            <div >
                                <table class="table table-bordered">
                                <tr>
                                    <th>Name</th>
                                    <th>Job Title</th>
                                    <th colspan="2">Nationality</th>
                                </tr>
                                    <asp:Repeater ID="rptDirectors" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtDirectorName" runat="server" TextMode="SingleLine" CssClass="input-xlarge" placeholder="Enter director name" />
                                                    <asp:RequiredFieldValidator ID="rfvDirectorName" runat="server" ControlToValidate="txtDirectorName" Display="Dynamic" ErrorMessage="Please enter director name" ValidationGroup="Questions" CssClass="alert-error" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDirectorJobTitle" runat="server" TextMode="SingleLine" CssClass="input-xlarge" placeholder="Enter director job title" />
                                                    <asp:RequiredFieldValidator ID="rfvJobTitle" runat="server" ControlToValidate="txtDirectorJobTitle" Display="Dynamic" ErrorMessage="Please enter director job title" ValidationGroup="Questions" CssClass="alert-error" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDirectorNationality" runat="server" TextMode="SingleLine" CssClass="input-xlarge" placeholder="&#37; company owned" />
                                                    <asp:RequiredFieldValidator ID="rfvNationality" runat="server" ControlToValidate="txtDirectorNationality" Display="Dynamic" ErrorMessage="Please enter director nationality" ValidationGroup="Questions" CssClass="alert-error" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnDeleteDirector" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="DeleteDirectorLine" /><asp:HiddenField ID="hidItemID" runat="server" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                                <asp:LinkButton ID="btnAddNewDirector" runat="server" Text="Add New Line" CssClass="btn btn-success" /> 
                            </div>
                            <hr />
                        </div>
                        
                        <div class="control-group">
                             <label>2.5 Is the Supplier or any person listed at question 2.3 or 2.4 above a person who falls within one of the categories listed below?  Do any of the people listed have an ownership interest, directly or indirectly, in the Supplier?  Do any of the employees of the Supplier who would be involved in or in any way connected to the contract with [company name] fall within one of the categories listed below or are they relatives of a person who falls within one of the categories listed below? </label>
                             <div>
                                 
                                 <table class="table table-striped">
                                     <thead>
                                     <tr>
                                         <th>Category</th>
                                         
                                     </tr>
                                         </thead>
                                     <asp:Repeater ID="rptrelationshipCategories" runat="server">
                                         <ItemTemplate>
                                             <tr>
                                                 <td><asp:Literal ID="litRelationshipCategory" runat="server"></asp:Literal></td>
                                             </tr>
                                         </ItemTemplate>
                                     </asp:Repeater>
                                    <tfoot>
                                        <tr>
                                            <td><strong>Does any of the above categories apply?</strong>  <asp:CheckBox ID="chkGovernmentEmployee" runat="server" AutoPostBack="true"/></td>
                                        </tr>
                                    </tfoot>
                               
                                </table>
                                
                             </div>
                            
                          
                        </div>
                        <asp:Panel ID="panGovernmanetEmployee" runat="server" Visible="false">
                        <div class="control-group">
                            <label >
                                Please complete the information below for each person and list the last government/political job they or their relative held.  If the connection of the Service Provider to a government/political official is through a relative, please state the relative's name and the relationship:
                            </label>
                            <div>
                                <table class="table table-bordered">
                                     <tr>
                                         <th>Person's name</th>
                                         <th>Relative's name</th>
                                         <th>Relationship (if applicable)</th>
                                         <th>Last government/political job held</th>
                                         <th>Country job was held in</th>
                                         <th colspan="2">Date job ended</th>
                                     </tr>
                                    <asp:Repeater ID="rptGovtEmployees" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtPersonName" runat="server" TextMode="SingleLine" CssClass="input-medium" placeholder="Enter name" />
                                                    <asp:RequiredFieldValidator ID="rfvPersonName" runat="server" ControlToValidate="txtPersonName" Display="Dynamic" ErrorMessage="Please enter name" ValidationGroup="Questions" CssClass="alert-error" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRelativeName" runat="server" TextMode="SingleLine" CssClass="input-medium" placeholder="Enter relative name" />
                                                    <asp:RequiredFieldValidator ID="rfvrelativeName" runat="server" ControlToValidate="txtRelativeName" Display="Dynamic" ErrorMessage="Please enter relative name" ValidationGroup="Questions" CssClass="alert-error" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRelationshipType" runat="server" TextMode="SingleLine" CssClass="input-medium" placeholder="&#37; company owned" />
                                                    <asp:RequiredFieldValidator ID="rfvRelationship" runat="server" ControlToValidate="txtRelationshipType" Display="Dynamic" ErrorMessage="Please enter relationship" ValidationGroup="Questions" CssClass="alert-error" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLastJob" runat="server" TextMode="SingleLine" CssClass="input-medium" placeholder="Enter last job title" />
                                                    <asp:RequiredFieldValidator ID="rfvLastJob" runat="server" ControlToValidate="txtLastJob" Display="Dynamic" ErrorMessage="Please enter last job" ValidationGroup="Questions" CssClass="alert-error" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtJobCountry" runat="server" TextMode="SingleLine" CssClass="input-medium" placeholder="Enter country name" />
                                                    <asp:RequiredFieldValidator ID="rfvJobCountry" runat="server" ControlToValidate="txtJobCountry" Display="Dynamic" ErrorMessage="Please enter country name" ValidationGroup="Questions" CssClass="alert-error" />
                                                </td>
                                                <td>
                                                    <Telerik:RadDatePicker ID="rdpDateEnded" runat="server" DateInput-DateFormat="dd MMM yyyy" DateInput-Enabled="false" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnDeleteRelative" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="DeleteRelativeLine" /><asp:HiddenField ID="hidItemID" runat="server" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                 </table>
                                <asp:LinkButton ID="btnAddNewRelative" runat="server" Text="Add New Line" CssClass="btn btn-success" />
                            </div>
                            <hr />
                        </div>

                        </asp:Panel>
                        
                    </asp:Panel>
                   
                   <asp:Panel ID="panPage3" runat="server" Visible="False">
                       <legend>3. Content of products/components</legend>
                       <div class="control-group">
                           <label>
                               3.1 Does your product/component contain any of the following minerals:
                           </label>
                           <asp:Repeater ID="rptMinerals" runat="server">
                               <ItemTemplate>
                                   <label class="control-label"><asp:Literal ID="litMineralName" runat="server" />:</label>
                                   <div class="controls">
                                       <asp:RadioButtonList ID="rblMineral" runat="server" AutoPostBack="true" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Table" OnSelectedIndexChanged="SelectMineral">
                                           <asp:ListItem Text="No" Selected="True" />
                                           <asp:ListItem Text="Yes" Selected="False" />
                                       </asp:RadioButtonList>
                                       <asp:Panel ID="panMineral" runat="server" Visible="false">
                                           <asp:TextBox ID="txtMineralDetails" runat="server" TextMode="MultiLine" CssClass="input-xxlarge" Rows="4" placeholder="Enter the details for Cassiterite" />
                                           <span class="help-block">Enter details</span>
                                           <asp:RequiredFieldValidator ID="rfvMineral" runat="server" ControlToValidate="txtMineralDetails" Display="Dynamic" ErrorMessage="Enter the details for this mineral" ValidationGroup="Questions" CssClass="alert-error" />
                                       </asp:Panel>
                                   </div>
                               </ItemTemplate>
                            </asp:Repeater>
                        </div>
                       <div class="control-group">
                            <label class="control-label">3.2 Are the minerals smelted or fully refined? </label>
                            <div class="controls">
                               <asp:RadioButtonList ID="rblSmelted" runat="server" AutoPostBack="true" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Table">
                                    <asp:ListItem Text="No" Selected="True" />
                                    <asp:ListItem Text="Yes" Selected="False" />
                                </asp:RadioButtonList>

                           </div>
                        </div>
                       <legend>4. Country of origin, processing and transportation</legend>
                       <div class="control-group">
                           <label>4.1 Did any of the <asp:LinkButton ID="btnMineralsListed" runat="server">Minerals listed</asp:LinkButton> in Question 3, <strong>originate</strong> from mines or suppliers in any of the following countries, and/or are any of the minerals <strong>processed</strong> or <strong>transported</strong> through any of the countries listed below:</label>
                           <!-- Like the minerals i think these should come from the DB -->
                           <asp:Repeater ID="rptDangerousCountries" runat="server">
                               <ItemTemplate>
                                   <label class="control-label"><asp:Literal ID="litCountryName" runat="server" /></label>
                                   <div class="controls">
                                       <asp:RadioButtonList ID="rblDangerousCountry" runat="server" AutoPostBack="true" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Table" OnSelectedIndexChanged="CheckCountry">
                                           <asp:ListItem Text="No" Selected="True" />
                                           <asp:ListItem Text="Yes" Selected="False" />
                                       </asp:RadioButtonList>
                                   </div>
                               </ItemTemplate>
                           </asp:Repeater>
                        </div>
                       <ajaxToolkit:HoverMenuExtender ID="hme2" runat="Server"
    TargetControlID="btnMineralsListed"
    PopupControlID="PopupMinerals"
    HoverCssClass="popupHover"
    PopupPosition="Right"
    OffsetX="0"
    OffsetY="-20"
    PopDelay="50" />
                       <asp:Panel CssClass="popover" ID="PopupMinerals" runat="server">
                            <div class="popover-content">
                            <h5>Listed Minerals</h5>
                                <!-- From databse -->
                                <table class="table table-bordered">
                                <asp:Repeater ID="rptMineralsPopup" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><asp:Literal ID="litMineralNamePopup" runat="server"></asp:Literal></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                </table>
                            
                             
                            </div>
                        </asp:Panel>
                       <div class="control-group">
                           <label class="control-label">4.2 Does the mineral content come from scrap sources?</label>
                           <div class="controls">
                               <asp:RadioButtonList ID="rblScrap" runat="server" AutoPostBack="true" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Table" >
                                    <asp:ListItem Text="No" Selected="True" />
                                    <asp:ListItem Text="Yes" Selected="False" />
                                </asp:RadioButtonList>

                           </div>
                           </div>
                       <asp:Panel ID="panScrap" runat="server" Visible="false">
                           <div class="control-group">
                               <label class="control-label">4.2.1 You have Selected yes, please provide details</label>
                               <div class="controls">
                                   <table class="table table-bordered">
                                       <tr>
                                           <th>Mineral name</th>
                                           <th colspan="2">Details</th>
                                       </tr>
                                       <asp:Repeater ID="rptScrap" runat="server">
                                           <ItemTemplate>
                                               <tr>
                                                   <td>
                                                       <asp:DropDownList ID="cboMinerals" runat="server" />
                                                       <asp:RequiredFieldValidator ID="rfvMinerals" runat="server" ControlToValidate="cboMinerals" Display="Dynamic" ErrorMessage="Select a mineral" ValidationGroup="Questions" CssClass="alert-error" />
                                                   </td>
                                                   <td>
                                                       <asp:TextBox ID="txtScrap" runat="server" TextMode="MultiLine" CssClass="input-xxlarge" Rows="4" placeholder="Enter the details for this mineral" />
                                                       <span class="help-block">Enter details</span>
                                                       <asp:RequiredFieldValidator ID="rfvScrap" runat="server" ControlToValidate="txtScrap" Display="Dynamic" ErrorMessage="Enter the details for this mineral" ValidationGroup="Questions" CssClass="alert-error" />
                                                   </td>
                                                   <td><asp:Button ID="btnDeleteScrap" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="DeleteScrapLine" /><asp:HiddenField ID="hidItemID" runat="server" />
                                                   </td>
                                               </tr>
                                           </ItemTemplate>
                                       </asp:Repeater>
                                   </table>
                                   <asp:LinkButton ID="btnAddScrapSource" runat="server" Text="Add New Line" CssClass="btn btn-success" />
                               </div>
                           </div>
                       </asp:Panel>

                       
                           <div class="control-group">
                               <label class="control-label">4.3 Does the mineral content come from recycled sources?</label>
                               <div class="controls">
                                   <asp:RadioButtonList ID="rblRecycled" runat="server" AutoPostBack="true" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Table">
                                       <asp:ListItem Text="No" Selected="True" />
                                       <asp:ListItem Text="Yes" Selected="False" />
                                   </asp:RadioButtonList>

                               </div>
                           </div>
                       
                       <asp:Panel ID="panRecycled" runat="server" Visible="false">
                           <div class="control-group">
                               <label class="control-label">4.3.1 You have Selected yes, please provide details</label>
                               <div class="controls">
                                   <table class="table table-bordered">
                                       <tr>
                                           <th>Mineral name</th>
                                           <th colspan="2">Details</th>
                                       </tr>
                                       <asp:Repeater ID="rptRecycled" runat="server">
                                           <ItemTemplate>
                                               <tr>
                                                   <td>
                                                       <asp:DropDownList ID="cboMinerals" runat="server" />
                                                       <asp:RequiredFieldValidator ID="rfvMinerals" runat="server" ControlToValidate="cboMinerals" Display="Dynamic" ErrorMessage="Select a mineral" ValidationGroup="Questions" CssClass="alert-error" />
                                                   </td>
                                                   <td>
                                                       <asp:TextBox ID="txtRecycled" runat="server" TextMode="MultiLine" CssClass="input-xxlarge" Rows="4" placeholder="Enter the details for this mineral" />
                                                       <span class="help-block">Enter details</span>
                                                       <asp:RequiredFieldValidator ID="rfvRecycled" runat="server" ControlToValidate="txtRecycled" Display="Dynamic" ErrorMessage="Enter the details for this mineral" ValidationGroup="Questions" CssClass="alert-error" />
                                                    </td>
                                                   <td>
                                                      <asp:Button ID="btnDeleteRecycle" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="DeleteRecyleLine" /><asp:HiddenField ID="hidItemID" runat="server" />
                                                    </td>
                                                   
                                               </tr>
                                           </ItemTemplate>
                                       </asp:Repeater>
                                   </table>
                                   <asp:LinkButton ID="btnAddRecycled" runat="server" Text="Add New Line" CssClass="btn btn-success" />
                               </div>
                           </div>
                       </asp:Panel>

                       
                   </asp:Panel>
                   
                   <asp:Panel ID="panPage4" runat="server" Visible="false" >
                    <asp:Panel ID="panMineralPurpose" runat="server" Visible="false">
                           <legend>3.3 Purpose of mineral content</legend>
                           <label><strong>Are any of the minerals listed:</strong></label>
                           <div class="control-group">
                               <label>3.3.1 Necessary for the product or components function, use, or purpose, or in any way useful to any of the product/component's functions?</label>
                               <div>
                                   <table class="table table-bordered">
                                       <tr>
                                           <th>Mineral</th>
                                           <th colspan="2">Function use/Purpose</th>
                                       </tr>
                                       <asp:Repeater ID="rptPurpose" runat="server">
                                           <ItemTemplate>
                                               <tr>
                                                   <td>
                                                       <asp:DropDownList ID="cboMinerals" runat="server" />
                                                       <asp:RequiredFieldValidator ID="rfvMinerals" runat="server" ControlToValidate="cboMinerals" Display="Dynamic" ErrorMessage="Select a mineral" ValidationGroup="Questions" CssClass="alert-error" /></td>
                                                   </td>
                                                <td>
                                                    <asp:TextBox ID="txtPurpose" runat="server" TextMode="MultiLine" CssClass="input-xxlarge" Rows="4" placeholder="Enter the details for this mineral" />
                                                    <span class="help-block">Enter details</span>
                                                    <asp:RequiredFieldValidator ID="rfvPurpose" runat="server" ControlToValidate="txtPurpose" Display="Dynamic" ErrorMessage="Enter the details for this mineral" ValidationGroup="Questions" CssClass="alert-error" /></td>
                                                   <td>
                                                       <asp:Button ID="btnDeletePurpose" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="DeletePurposeLine" /><asp:HiddenField ID="hidItemID" runat="server" />
                                                   </td>
                                               </tr>
                                           </ItemTemplate>
                                       </asp:Repeater>
                                   </table>
                                   <span class="help-block">If yes, please click the button below and explain why in the boxes that will appear.</span>
                                   <asp:LinkButton ID="btnAddPurpose" runat="server" Text="Add New Line" CssClass="btn btn-success" />
                               </div>
                           </div>
                           <hr />
                           <div class="control-group">
                               <label>3.3.2 Intentionally added to the product/component's production process?</label>
                               <div>
                                   <table class="table table-bordered">
                                       <tr>
                                           <th>Mineral Name</th>
                                           <th colspan="2">Explanation</th>
                                       </tr>
                                       <asp:Repeater ID="rptProcess" runat="server">
                                           <ItemTemplate>
                                               <tr>
                                                   <td>
                                                       <asp:DropDownList ID="cboMinerals" runat="server" />
                                                       <asp:RequiredFieldValidator ID="rfvMinerals" runat="server" ControlToValidate="cboMinerals" Display="Dynamic" ErrorMessage="Select a mineral" ValidationGroup="Questions" CssClass="alert-error" /></td>
                                                   </td>
                                                <td>
                                                    <asp:TextBox ID="txtProcess" runat="server" TextMode="MultiLine" CssClass="input-xxlarge" Rows="4" placeholder="Enter the details for this mineral" />
                                                    <span class="help-block">Enter details</span>
                                                    <asp:RequiredFieldValidator ID="rfvPurpose" runat="server" ControlToValidate="txtProcess" Display="Dynamic" ErrorMessage="Enter the details for this mineral" ValidationGroup="Questions" CssClass="alert-error" /></td>
                                                   <td>
                                                       <asp:Button ID="btnDeleteProcess" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="DeleteProcessLine" /><asp:HiddenField ID="hidItemID" runat="server" />
                                                   </td>
                                               </tr>
                                           </ItemTemplate>
                                       </asp:Repeater>

                                   </table>
                                   <span class="help-block">If yes, please click the button below and explain why in the boxes that will appear.</span>
                                   <asp:LinkButton ID="btnAddProcess" runat="server" Text="Add New Line" CssClass="btn btn-success" />
                               </div>
                           </div>
                           <hr />
                           <div class="control-group">
                               <label>3.3.3 Necessary to produce the product/component?</label>
                               <table class="table table-bordered">
                                   <tr>
                                       <th>Mineral Name</th>
                                       <th colspan="2">Explanation</th>

                                   </tr>
                                   <asp:Repeater ID="rptComponent" runat="server">
                                       <ItemTemplate>
                                           <tr>
                                               <td>
                                                   <asp:DropDownList ID="cboMinerals" runat="server" />
                                                   <asp:RequiredFieldValidator ID="rfvMinerals" runat="server" ControlToValidate="cboMinerals" Display="Dynamic" ErrorMessage="Select a mineral" ValidationGroup="Questions" CssClass="alert-error" /></td>
                                               </td>
                                                <td>
                                                    <asp:TextBox ID="txtComponent" runat="server" TextMode="MultiLine" CssClass="input-xxlarge" Rows="4" placeholder="Enter the details for this mineral" />
                                                    <span class="help-block">Enter details</span>
                                                    <asp:RequiredFieldValidator ID="rfvComponent" runat="server" ControlToValidate="txtComponent" Display="Dynamic" ErrorMessage="Enter the details for this mineral" ValidationGroup="Questions" CssClass="alert-error" /></td>
                                               <td>
                                                   <asp:Button ID="btnDeleteComponent" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="DeleteComponentLine" /><asp:HiddenField ID="hidItemID" runat="server" />
                                               </td>
                                           </tr>
                                       </ItemTemplate>
                                   </asp:Repeater>

                               </table>
                               <span class="help-block">If yes, please click the button below and explain why in the boxes that will appear.</span>
                               <asp:LinkButton ID="btnAddComponent" runat="server" Text="Add New Line" CssClass="btn btn-success" />
                           </div>
                       </asp:Panel>
                    <asp:Panel ID="panQuestion5" runat="server" Visible="false">
                           <legend>4.4 Quantity, date and method of extraction </legend>
                           <div class="control-group">
                               <label>4.4.1 Please provide details of the quantity of minerals, date of extraction</label>
                               <div>
                                   <table class="table table-bordered">
                                       <tr>
                                           <th>Mineral</th>
                                           <th>Quantity (Tonnes?)</th>
                                           <th>Date of extraction</th>
                                           <th colspan="2">Method Extraction</th>
                                       </tr>
                                       <asp:Repeater ID="rptExtraction" runat="server">
                                           <ItemTemplate>
                                       <tr>
                                           <td>
                                               <asp:DropDownList ID="cboMinerals" runat="server" />
                                               <asp:RequiredFieldValidator ID="rfvMinerals" runat="server" ControlToValidate="cboMinerals" Display="Dynamic" ErrorMessage="Select a mineral" ValidationGroup="Questions" CssClass="alert-error" />
                                            </td>
                                           <td>
                                               <asp:TextBox runat="server" ID="txtQuantity" CssClass="input-mini" placeholder="Enter the quantity for this mineral" />
                                               <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ControlToValidate="txtQuantity" Display="Dynamic" ErrorMessage="Enter the mineral quantity" ValidationGroup="Questions" CssClass="alert-error" />
                                           </td>
                                           <td>
                                               <Telerik:RadDatePicker ID="rdpExtractionDate" runat="server" DateInput-DateFormat="dd MMM yyyy" DateInput-Enabled="false" />
                                           </td>
                                           <td>
                                               <asp:DropDownList ID="cboExtractionMethod" runat="server" />
                                               <asp:RequiredFieldValidator ID="rfvExtraction" runat="server" ControlToValidate="cboExtractionMethod" Display="Dynamic" ErrorMessage="Select an extraction method" ValidationGroup="Questions" CssClass="alert-error" />
                                            </td>
                                           <td>
                                               <asp:Button ID="btnDeleteExtraction" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="DeleteExtractionLine" /><asp:HiddenField ID="hidItemID" runat="server" />
                                           </td>
                                       </tr>
                                        </ItemTemplate>
                                        </asp:Repeater>
                                   </table>
                                   <asp:LinkButton ID="btnAddExtraction" runat="server" Text="Add New Line" CssClass="btn btn-success" />

                               </div>
                           </div>
                           <legend>4.5 Processing Facility</legend>
                           <div class="control-group">
                               <label>4.5.1 At which facility are the minerals processed?</label>
                               <div>
                                   <table class="table table-bordered">
                                       <tr>
                                           <th>Company Name (e.g Exotech Inc.) </th>
                                           <th colspan="2">Location (e.g Pompano Beach, Florida, USA)</th>
                                       </tr>
                                       <asp:Repeater ID="rptFacility" runat="server">
                                           <ItemTemplate>
                                               <tr>
                                                   <td>
                                                       <asp:TextBox ID="txtFacilityName" runat="server" TextMode="SingleLine" CssClass="input-xlarge" placeholder="Enter company name" />
                                                       <asp:RequiredFieldValidator ID="rfvCompanyName" runat="server" ControlToValidate="txtFacilityName" Display="Dynamic" ErrorMessage="Please enter company name" ValidationGroup="Questions" CssClass="alert-error" />
                                                   </td>
                                                   <td>
                                                       <asp:TextBox ID="txtLocation" runat="server" TextMode="SingleLine" CssClass="input-xlarge" placeholder="Enter location" />
                                                       <asp:RequiredFieldValidator ID="rfvLocation" runat="server" ControlToValidate="txtLocation" Display="Dynamic" ErrorMessage="Please enter location" ValidationGroup="Questions" CssClass="alert-error" />
                                                   </td>
                                                   <td>
                                                       <asp:Button ID="btnDeleteFacility" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="DeleteFacilityLine" /><asp:HiddenField ID="hidItemID" runat="server" />
                                                   </td>
                                                </tr>
                                           </ItemTemplate>
                                       </asp:Repeater>
                                   </table>
                                  <asp:LinkButton ID="btnAddFacility" runat="server" Text="Add New Line" CssClass="btn btn-success" />
                               </div>
                           </div>

                           <div class="control-group">
                               <label class="control-label">4.5.2 Is the facility included in the
                                   <asp:HyperLink ID="hypSmelterList" runat="server">Conflict Free Smelter list? </asp:HyperLink></label>
                               <div class="controls">

                                   <AjaxToolkit:HoverMenuExtender ID="HoverMenuExtender1" runat="Server"
                                       TargetControlID="hypSmelterList"
                                       PopupControlID="popupSmelterList"
                                       HoverCssClass="popupHover"
                                       PopupPosition="Right"
                                       OffsetX="0"
                                       OffsetY="-200"
                                       PopDelay="50" />
                                   <asp:Panel CssClass="SmelterPopup" ID="popupSmelterList" runat="server">
                                       <div class="popover-content">
                                           <h5>Compliant Smelter & Refiner List</h5>
                                           <!-- From databse -->
                                           <table class="table table-bordered">
                                               <tr>
                                                   <th>Company Name</th>
                                                   <th>Location</th>
                                                   <th>Effective Date</th>
                                               </tr>
                                               <asp:Repeater ID="rptSmelterList" runat="server">
                                                   <ItemTemplate>
                                                       <tr>
                                                           <td>
                                                               <asp:Literal ID="litSmelterName" runat="server"></asp:Literal></td>
                                                           <td>
                                                               <asp:Literal ID="litSmelterLocation" runat="server"></asp:Literal></td>
                                                           <td>
                                                               <asp:Literal ID="litSmeltereffectiveDate" runat="server"></asp:Literal></td>

                                                       </tr>
                                                   </ItemTemplate>
                                               </asp:Repeater>
                                           </table>
                                        </div>
                                   </asp:Panel>
                                   <asp:RadioButtonList ID="rblSmelterList" runat="server" AutoPostBack="false" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Table">
                                       <asp:ListItem Text="No" Selected="True" />
                                       <asp:ListItem Text="Yes" Selected="False" />
                                   </asp:RadioButtonList>
                               </div>
                           </div>
                           <div class="control-group">
                               <label class="control-label">4.5.3 Has the facility been subject to an independent audit that has led it being designated "conflict-free" (in relation to conflict minerals)? </label>
                               <div class="controls">
                               <asp:RadioButtonList ID="rblIndependent" runat="server" AutoPostBack="true" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Table">
                                       <asp:ListItem Text="No" Selected="True" />
                                       <asp:ListItem Text="Yes" Selected="False" />
                                   </asp:RadioButtonList>
                                   <asp:Panel ID="panIndependentAudit" runat="server" Visible="false">
                                       <asp:TextBox ID="txtIndependentAudit" runat="server" TextMode="MultiLine" CssClass="input-xxlarge" Rows="4" placeholder="Enter the details for Independent Audit" />
                                       <asp:RequiredFieldValidator ID="rfvIndependent" runat="server" ControlToValidate="txtIndependentAudit" Display="Dynamic" ErrorMessage="Please enter audit details" ValidationGroup="Questions" CssClass="alert-error" />
                                   </asp:Panel>

                               </div>
                          
                           </div>
                        </asp:Panel>
                   </asp:Panel>

                   <asp:Panel ID="panPage5" Visible="False" runat="server">
                      <legend>8. Transport and supply of conflict </legend> 
                       <div class="control-group">
                       <label>8.1 Who supplies you with the minerals or products containing the minerals? Please provide name and address details</label>
                        <table class="table table-bordered">
                               <tr>
                                   <th>Name</th>
                                   <th colspan="2">Address Details</th>
                               </tr>
                            <asp:Repeater ID="rptTransport" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtTransporterName" runat="server" TextMode="SingleLine" CssClass="input-xlarge" placeholder="Enter company/individual name" />
                                            <asp:RequiredFieldValidator ID="rfvTransportName" runat="server" ControlToValidate="txtTransporterName" Display="Dynamic" ErrorMessage="Please enter a name" ValidationGroup="Questions" CssClass="alert-error" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTransporterAddress" runat="server" TextMode="MultiLine" CssClass="input-xxlarge" placeholder="Enter address" Rows="4" />
                                            <asp:RequiredFieldValidator ID="rfvTransportAddress" runat="server" ControlToValidate="txtTransporterAddress" Display="Dynamic" ErrorMessage="Please enter address" ValidationGroup="Questions" CssClass="alert-error" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnDeleteTransport" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="DeleteTransportLine" /><asp:HiddenField ID="hidItemID" runat="server" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                           </table>
                           <asp:LinkButton ID="btnAddTransporter" runat="server" Text="Add New Line" CssClass="btn btn-success" />
                       </div>
                       <div class="control-group">
                           <label>8.2 What countries are the minerals transported through? </label>
                            <asp:TextBox ID="txtTransportCountries" runat="server" TextMode="MultiLine" CssClass="input-xxlarge" Rows="4" placeholder="Please list the countries" />
                           <asp:RequiredFieldValidator ID="rfvTransportCountries" runat="server" ControlToValidate="txtTransportCountries" Display="Dynamic" ErrorMessage="Please enter a country name" ValidationGroup="Questions" CssClass="alert-error" />    
                       </div>
                      <legend>9. Supply Chain</legend>
                      <label>9.1 Please identify all upstream intermediaries, consolidators and other actors in your supply chain</label>
                       <asp:TextBox ID="txtIntermediaries" runat="server" TextMode="MultiLine" CssClass="input-xxlarge" Rows="4" placeholder="Please identify all upstream intermediaries, consolidators and other actors" />
                       <asp:RequiredFieldValidator ID="rfvIntermediaries" runat="server" ControlToValidate="txtIntermediaries" Display="Dynamic" ErrorMessage="Please enter details" ValidationGroup="Questions" CssClass="alert-error" />
                       <legend>10. Taxes & payments</legend>
                       <label><strong>Please disclose:</strong></label>
                       <div class="control-group">
                           <label>10.1 all taxes, fees or royalties paid to the government of a country listed in question 10 for the purposes of extraction, trade, transport and export of minerals</label>
                            <table class="table table-bordered">
                               <tr>
                                   <th>Country</th>
                                   <th colspan="2">Details</th>
                               </tr>
                                <asp:Repeater ID="rptTaxes" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="cboCountryID" runat="server"  />
                                                <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ControlToValidate="cboCountryID" Display="Dynamic" ErrorMessage="Please select country" ValidationGroup="Questions" CssClass="alert-error" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTaxDetails" runat="server" TextMode="MultiLine" CssClass="input-xxlarge" placeholder="Enter payment details" Rows="4" />
                                                <asp:RequiredFieldValidator ID="rfvPaymentDetails" runat="server" ControlToValidate="txtTaxDetails" Display="Dynamic" ErrorMessage="Please enter details" ValidationGroup="Questions" CssClass="alert-error" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnDeleteTax" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="DeleteTaxLine" /><asp:HiddenField ID="hidItemID" runat="server" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                           </table>
                           <asp:LinkButton ID="btnAddTax" runat="server" Text="Add New Line" CssClass="btn btn-success" />
                       </div>
                       
                       <div class="control-group">
                           <label>10.2 Any other payments made to government officials for the purposes of extraction, trade, transport or export of minerals</label>
                           <table class="table table-bordered">
                               <tr>
                                   <th>Payment</th>
                                   <th colspan="2">Details</th>
                               </tr>
                               <asp:Repeater ID="rptOtherPayment" runat="server">
                                   <ItemTemplate>
                                       <tr>
                                           <td>
                                               <div class="input-prepend input-append">
                                                   <span class="add-on">$</span>
                                                    <asp:TextBox ID="txtPaymentAmount" runat="server" TextMode="SingleLine" CssClass="input-medium" placeholder="Enter payment amount" />
                                                    <span class="add-on">.00</span>
                                                </div>
                                                   <asp:RequiredFieldValidator ID="rfvPaymentAmount" runat="server" ControlToValidate="txtPaymentAmount" Display="Dynamic" ErrorMessage="Please enter payment amount" ValidationGroup="Questions" CssClass="alert-error" />
                                           </td>
                                           <td>
                                               <asp:TextBox ID="txtPaymentDetails" runat="server" TextMode="MultiLine" CssClass="input-xxlarge" placeholder="Enter payment details" Rows="4" />
                                               <asp:RequiredFieldValidator ID="rfvPaymentDetails" runat="server" ControlToValidate="txtPaymentDetails" Display="Dynamic" ErrorMessage="Please enter details" ValidationGroup="Questions" CssClass="alert-error" />
                                           </td>
                                           <td>
                                               <asp:Button ID="btnDeleteOtherPayment" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="DeleteOtherPaymentLine" /><asp:HiddenField ID="hidItemID" runat="server" />
                                           </td>
                                       </tr>
                                   </ItemTemplate>
                               </asp:Repeater>
                           </table>
                           <asp:LinkButton ID="btnAddOtherPayment" runat="server" Text="Add New Line" CssClass="btn btn-success" />

                       </div>
                       <div class="control-group">
                           <label>10.3 All taxes and any other payments made to public or private security forces or other armed groups at all points in the supply chain from extraction onwards</label>
                            <table class="table table-bordered">
                               <tr>
                                   <th>Payment</th>
                                   <th colspan="2">Details</th>
                               </tr>
                                <asp:Repeater ID="rptOtherTaxes" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <div class="input-prepend input-append">
                                                   <span class="add-on">$</span>
                                                <asp:TextBox ID="txtPaymentAmount" runat="server" TextMode="SingleLine" CssClass="input-medium" placeholder="Enter payment amount" />
                                                   <span class="add-on">.00</span>
                                                </div>
                                                    <asp:RequiredFieldValidator ID="rfvPaymentAmount" runat="server" ControlToValidate="txtPaymentAmount" Display="Dynamic" ErrorMessage="Please enter payment amount" ValidationGroup="Questions" CssClass="alert-error" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPaymentDetails" runat="server" TextMode="MultiLine" CssClass="input-xxlarge" placeholder="Enter payment details" Rows="4" />
                                                <asp:RequiredFieldValidator ID="rfvPaymentDetails" runat="server" ControlToValidate="txtPaymentDetails" Display="Dynamic" ErrorMessage="Please enter details" ValidationGroup="Questions" CssClass="alert-error" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnDeleteOtherTax" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="DeleteOtherTaxLine" /><asp:HiddenField ID="hidItemID" runat="server" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                           </table>
                           <asp:LinkButton ID="btnAddOtherTax" runat="server" Text="Add New Line" CssClass="btn btn-success" />
                       </div>

                   </asp:Panel>
                   
                   <asp:Panel ID="panPage6" runat="server" Visible="False">
                       <asp:Panel ID="panUpload" runat="server" Visible="false">
                           <legend>11. Your Policies</legend>
                           <div class="control-group">
                                <label>Do you have a policy on the sourcing of conflict minerals? Yes/No – if yes, please provide </label>
                                <br />
                                <asp:Repeater ID="rptFiles" runat="server">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnViewFile" runat="server" OnClick="DownloadFile" />&nbsp;&nbsp;
                                        <asp:LinkButton ID="btnDeleteFile" runat="server" OnClick="DeleteFile" Text="Delete" CssClass="btn btn-danger" /><br />
                                        <AjaxToolkit:ConfirmButtonExtender ID="cbeDecline" runat="server" TargetControlID="btnDeleteFile"
                                            ConfirmText="This will permanently remove this file!&#10;&#10;Are you sure?" />
                                    </ItemTemplate>
                                </asp:Repeater><br />
                               <Telerik:RadProgressManager runat="server" ID="RadProgressManager1" />
                               <Telerik:RadAsyncUpload ID="rauUploader" runat="server" MultipleFileSelection="Automatic" OnClientFilesUploaded="UploadFile" />
                               <Telerik:RadProgressArea runat="server" ID="RadProgressArea1" />
                           </div>
                       </asp:Panel>
                       <legend>12. Certification / Consent</legend>
                       <ol>
                           <li>The information provided above is, to the best of my knowledge and belief, accurate, current and complete.</li>
                           <li>I understand that any inaccuracy in the foregoing statements and representations may result in [company name] deciding not to do business with us, the termination of the Supplier's relationship with [company name], and loss of any compensation owed by [company name].</li>
                           <li>I agree to notify [company name] promptly of any material changes to the information provided in this Questionnaire, both before the Supplier enters into a contract with [company name] and during the term of the contract between [company name] and the Supplier, if applicable.</li>
                           <li>I agree that [company name] may:
                               <ul>
                                   <li>store and move the information provided in this Questionnaire electronically; and</li>
                                   <li>provide a copy of this Questionnaire to any other company in the [company name]. </li>
                               </ul>
                           </li>
                           <li>On behalf of the Supplier, its officers, directors and employees, I agree to comply with [company name]'s policy on conflict minerals, anti-bribery and business ethics. </li>
                       </ol>
                      <legend>13. Sign off</legend>
                       <div class="control-group">
                       <label>I have the authority to bind the Supplier</label>
                       <label>Name:</label><asp:Literal ID="litUsername" runat="server" Text="Stephen Davidson" />
                       <label>Signature checkbox:</label>
                           <asp:CheckBox ID="chkSignOff" runat="server" />
                       </div>
                   </asp:Panel>
                 
                    <p>
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-mini pull-right btn-warning" Text="Save Draft" CausesValidation="false" />
                        <asp:Button ID="btnPrev" runat="server" CssClass="btn btn-mini pull-right" Text=" &laquo; Prev Page" />
                        <asp:Button ID="btnNext" runat="server" CssClass="btn btn-mini pull-right" Text="Next Page &raquo;" ValidationGroup="Questions" />
                        
                        <asp:Button ID="btnClose" runat="server" CssClass="btn btn-mini btn-success" Text="Save &amp; Close" Visible="false" CommandName="Close" />
                        <AjaxToolkit:ConfirmButtonExtender ID="cbeClose" runat="server" TargetControlID="btnClose"
                            ConfirmText="This will close this questionnaire!&#10;&#10;All your customers &amp; suppliers will be notified.&#10;&#10;Are you sure?" />
                    </p>
                   <p><asp:Label ID="lblErrorMessage" runat="server" CssClass="alert-danger"  /></p>
               </div>
               <!-- Le javascript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="/js/jquery.js" type="text/javascript"></script>
    <script src="/js/bootstrap.js" type="text/javascript"></script>
    <script src="/js/bootstrap-transition.js" type="text/javascript"></script>
    <script src="/js/bootstrap-alert.js" type="text/javascript"></script>
    <script src="/js/bootstrap-modal.js" type="text/javascript"></script>
    <script src="/js/bootstrap-dropdown.js" type="text/javascript"></script>
    <script src="/js/bootstrap-scrollspy.js" type="text/javascript"></script>
    <script src="/js/bootstrap-tab.js" type="text/javascript"></script>
    <script src="/js/bootstrap-tooltip.js" type="text/javascript"></script>
    <script src="/js/bootstrap-popover.js" type="text/javascript"></script>
    <script src="/js/bootstrap-button.js" type="text/javascript"></script>
    <script src="/js/bootstrap-collapse.js" type="text/javascript"></script>
    <script src="/js/bootstrap-carousel.js" type="text/javascript"></script>
    <script src="/js/bootstrap-typeahead.js" type="text/javascript"></script>
    <script src="/js/highcharts.js"></script>
    <script src="/js/modules/exporting.js"></script>
        <script>
            $(function () {
                $("#moreinfo").popover({ html: true, trigger: 'hover', content: '<p><span class="label label-info">Petrofacs</span> policy is to conduct business in a legal and ethical manner, to further human rights and to not do anything which contributes to conflict.</p><p>It is therefore important to identify the existence of any "conflict minerals" in our supply chain.  We expect our suppliers to adhere to this to statement of principle and to work with us in fulfilling our commitment.</p><p>Our suppliers are required to answer all questions honestly and thoroughly following the making of inquiries within their own business and with their supply chain.</p>' });

            });

        </script>
        </asp:Panel>
                 </div>
                 </div>
             </div>
</Telerik:RadAjaxPanel>
    <Telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <Telerik:AjaxSetting AjaxControlID="RadAjaxPanel1">
                <UpdatedControls>
                    <Telerik:AjaxUpdatedControl ControlID="RadAjaxPanel1" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelRenderMode="Block"/>
                </UpdatedControls>
            </Telerik:AjaxSetting>
        </AjaxSettings>
    </Telerik:RadAjaxManager>
    <Telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Telerik" Transparency="0" IsSticky="False" />
    
    
</form>
</body>
</html>

      

