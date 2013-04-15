<%@ Page Title="" Language="VB" MasterPageFile="~/masterpages/templatefull.master" AutoEventWireup="false" CodeFile="standardquestionnaire.aspx.vb" Inherits="standardquestionnaire" %>
<%@ Register src="controls/submenu1.ascx" tagname="submenu1" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpcMainContent" Runat="Server">
     <div class="span9">
   <script type="text/javascript" language="javascript">
       function UploadFile()
       {
           document.getElementById('ctl00_ctl00_cphMainContent_cpcMainContent_btnUpload').click();// This used to purge uploaded files to the target folder
       }
   </script>
       <h2>Conflict Minerals – Due Diligence Questionnaire</h2>
    <p>[Company Name]'s policy is to conduct business in a legal and ethical manner, to further human rights and to not do anything which contributes to conflict.</p>
    <p> It is therefore important to identify the existence of any "conflict minerals" in our supply chain.  We expect our suppliers to adhere to this to statement of principle and to work with us in fulfilling our commitment.</p> 
    <p> Our suppliers are required to answer all questions honestly and thoroughly following the making of inquiries within their own business and with their supply chain.</p>
    <Telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
        <asp:Button id-="btnUpload" runat="server" style="visibility:hidden;float:right" />
        <asp:Literal ID="litTest" runat="server" />
                <h4>Progress</h4>
               <div class="progress">
                   <div class="bar" runat="server" id="progressbar"></div>
                   <asp:Label ID="lblProgress" runat="server" CssClass="bar" Width="166px" />
               </div> 
               <h4>Items marked with * are requried</h4>
               <div class="form-signin form-horizontal">
                    <asp:Panel ID="panPage1" runat="server" Visible="false">
                       <legend>1. Corporate details</legend>
                       <label>Full name and address of supplier</label>
                       <div class="control-group">
                           <label class="control-label"><span class="alert-error">*</span>Company Name:</label>
                           <div class="controls">
                                <asp:TextBox ID="txtCompanyName" runat="server" TextMode="SingleLine" CssClass="input-xxlarge" placeholder="Enter company name" />
                                <asp:RequiredFieldValidator ID="rfvCompanyName" runat="server" ControlToValidate="txtCompanyName" Display="Dynamic" ErrorMessage="Please enter company name" ValidationGroup="Questions" CssClass="alert-error" />
                           </div>
                       </div>
                        
                        <div class="control-group">
                            <label class="control-label"><span class="alert-error">*</span> Company Number:</label>
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
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPostcode" Display="Dynamic" ErrorMessage="Please enter city" ValidationGroup="Questions" CssClass="alert-error" />
                   
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
                                    <asp:ListItem Text="No" Selected="True" />
                                    <asp:ListItem Text="Yes" Selected="False" />
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
                             <label>2.5 Is the Supplier or any person listed at question 4 or 5 above a person who falls within one of the categories listed below?  Do any of the people listed have an ownership interest, directly or indirectly, in the Supplier?  Do any of the employees of the Supplier who would be involved in or in any way connected to the contract with [company name] fall within one of the categories listed below or are they relatives of a person who falls within one of the categories listed below? </label>
                             <div>
                                 <table class="table table-bordered">
                                     <tr>
                                         <th>Category</th>
                                         <th>Check</th>
                                     </tr>
                                     <tr>
                                         <td>A current  or former government or public official (teachers, school assistants, nurses, librarians and public officials who only carry out low level administrative functions);</td>
                                         <td><asp:CheckBox ID="chkGovernmentEmployee" runat="server" AutoPostBack="true"/></td>
                                     </tr>
                                     <tr>
                                         <td>A current or former employee of a government or public agency (exclusions as above);</td>
                                         <td></td>
                                     </tr>
                                     <tr>
                                         <td>
                                             etc
                                         </td>
                                         <td></td>
                                     </tr>
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
                       <!-- Preferably these should come from the DB -->
                       <div class="control-group">
                           <label>
                               3.1 Does your product/component contain any of the following minerals:
                           </label>
                           <label class="control-label">Cassiterite:</label>
                           <div class="controls">
                               <asp:RadioButtonList ID="rblCassiterite" runat="server" AutoPostBack="true" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Table" >
                                    <asp:ListItem Text="No" Selected="True" />
                                    <asp:ListItem Text="Yes" Selected="False" />
                                </asp:RadioButtonList>
                               <asp:Panel ID="panCassiterite" runat="server" Visible="false">
                                   <asp:TextBox ID="txtCassiterite" runat="server" TextMode="MultiLine" CssClass="input-xxlarge" Rows="4" placeholder="Enter the details for Cassiterite" />
                                   <span class="help-block">Enter details</span>
                                   <asp:RequiredFieldValidator ID="rfvCassiterite" runat="server" ControlToValidate="txtCassiterite" Display="Dynamic" ErrorMessage="Enter the details for Cassiterite" ValidationGroup="Questions" CssClass="alert-error" />
                               </asp:Panel>
                           </div>
                           <label class="control-label">Columbite-tantalite (coltan):</label>
                           <div class="controls">
                               <asp:RadioButtonList ID="rblColumbite" runat="server" AutoPostBack="true" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Table">
                                    <asp:ListItem Text="No" Selected="True" />
                                    <asp:ListItem Text="Yes" Selected="False" />
                                </asp:RadioButtonList>
                               <asp:Panel ID="panColumbite" runat="server" Visible="false">
                                   <asp:TextBox ID="txtColumbite" runat="server" TextMode="MultiLine" CssClass="input-xxlarge" Rows="4" placeholder="Enter the details for Columbite" />
                                   <span class="help-block">Enter details</span>
                                   <asp:RequiredFieldValidator ID="rfvColumbite" runat="server" ControlToValidate="txtCassiterite" Display="Dynamic" ErrorMessage="Enter the details for Columbite" ValidationGroup="Questions" CssClass="alert-error" />
                               </asp:Panel>
                           </div>
                           <label class="control-label">Wolframite:</label>
                           <div class="controls">
                               <asp:RadioButtonList ID="rblWolframite" runat="server" AutoPostBack="true" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Table">
                                    <asp:ListItem Text="No" Selected="True" />
                                    <asp:ListItem Text="Yes" Selected="False" />
                                </asp:RadioButtonList>
                               <asp:Panel ID="panWolframite" runat="server" Visible="false">
                                   <asp:TextBox ID="txtWolframite" runat="server" TextMode="MultiLine" CssClass="input-xxlarge" Rows="4" placeholder="Enter the details for Wolframite" />
                                   <span class="help-block">Enter details</span>
                                   <asp:RequiredFieldValidator ID="rfvWolframite" runat="server" ControlToValidate="txtWolframite" Display="Dynamic" ErrorMessage="Enter the details for Wolframite" ValidationGroup="Questions" CssClass="alert-error" />
                               </asp:Panel>
                           </div>
                           <label class="control-label">tantalum:</label>
                           <div class="controls">
                               <asp:RadioButtonList ID="rblTantalum" runat="server" AutoPostBack="true" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Table">
                                   <asp:ListItem Text="No" Selected="True" />
                                   <asp:ListItem Text="Yes" Selected="False" />
                               </asp:RadioButtonList>
                               <asp:Panel ID="panTantalum" runat="server" Visible="false">
                                   <asp:TextBox ID="txtTantalum" runat="server" TextMode="MultiLine" CssClass="input-xxlarge" Rows="4" placeholder="Enter the details for Tantalum" />
                                   <span class="help-block">Enter details</span>
                                   <asp:RequiredFieldValidator ID="rfvTantalum" runat="server" ControlToValidate="txtTantalum" Display="Dynamic" ErrorMessage="Enter the details for Tantalum" ValidationGroup="Questions" CssClass="alert-error" />
                               </asp:Panel>
                           </div>
                           <label class="control-label">tungsten:</label>
                           <div class="controls">
                               <asp:RadioButtonList ID="rblTungsten" runat="server" AutoPostBack="true" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Table">
                                   <asp:ListItem Text="No" Selected="True" />
                                   <asp:ListItem Text="Yes" Selected="False" />
                               </asp:RadioButtonList>
                               <asp:Panel ID="panTungsten" runat="server" Visible="false">
                                   <asp:TextBox ID="txtTungsten" runat="server" TextMode="MultiLine" CssClass="input-xxlarge" Rows="4" placeholder="Enter the details for Tungsten" />
                                   <span class="help-block">Enter details</span>
                                   <asp:RequiredFieldValidator ID="rfvTungsten" runat="server" ControlToValidate="txtTungsten" Display="Dynamic" ErrorMessage="Enter the details for Tungsten" ValidationGroup="Questions" CssClass="alert-error" />
                               </asp:Panel>
                           </div>
                           <label class="control-label">Gold:</label>
                           <div class="controls">
                               <asp:RadioButtonList ID="rblGold" runat="server" AutoPostBack="true" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Table">
                                   <asp:ListItem Text="No" Selected="True" />
                                   <asp:ListItem Text="Yes" Selected="False" />
                               </asp:RadioButtonList>
                               <asp:Panel ID="panGold" runat="server" Visible="false">
                                   <asp:TextBox ID="txtGold" runat="server" TextMode="MultiLine" CssClass="input-xxlarge" Rows="4" placeholder="Enter the details for Gold" />
                                   <span class="help-block">Enter details</span>
                                   <asp:RequiredFieldValidator ID="rfvGold" runat="server" ControlToValidate="txtGold" Display="Dynamic" ErrorMessage="Enter the details for Gold" ValidationGroup="Questions" CssClass="alert-error" />
                               </asp:Panel>
                           </div>
                          
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
                   </asp:Panel>
                   
                   <asp:Panel ID="panPage4" runat="server" Visible="False" >
                       <legend>4. Country of origin, processing and transportation</legend>
                       <div class="control-group">
                           <label>4.1 Did any of the <asp:LinkButton ID="btnMineralsListed" runat="server">Minerals listed</asp:LinkButton> in Question 3, <strong>originate</strong> from mines or suppliers in any of the following countries, and/or are any of the minerals <strong>processed</strong> or <strong>transported</strong> through any of the countries listed below:</label>
                           <!-- Like the minerals i think these should come from the DB -->
                           <label class="control-label">DR Congo:</label>
                           <div class="controls">
                               <asp:RadioButtonList ID="rblCongo" runat="server" AutoPostBack="true" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Table" >
                                    <asp:ListItem Text="No" Selected="True" />
                                    <asp:ListItem Text="Yes" Selected="False" />
                                </asp:RadioButtonList>

                           </div>
                           <label class="control-label">Angola:</label>
                           <div class="controls">
                               <asp:RadioButtonList ID="rblAngola" runat="server" AutoPostBack="true" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Table" >
                                    <asp:ListItem Text="No" Selected="True" />
                                    <asp:ListItem Text="Yes" Selected="False" />
                                </asp:RadioButtonList>
                           </div>
                           <label class="control-label">Burindi:</label>
                           <div class="controls">
                                <asp:RadioButtonList ID="rblBurindi" runat="server" AutoPostBack="true" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Table" >
                                    <asp:ListItem Text="No" Selected="True" />
                                    <asp:ListItem Text="Yes" Selected="False" />
                                </asp:RadioButtonList>
                           </div>
                           <label class="control-label">Central African Republic:</label>
                           <div class="controls">
                                <asp:RadioButtonList ID="rblCar" runat="server" AutoPostBack="true" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Table" >
                                    <asp:ListItem Text="No" Selected="True" />
                                    <asp:ListItem Text="Yes" Selected="False" />
                                </asp:RadioButtonList>
                           </div>
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
                       <div class="control-group">
                           <label class="control-label">You have Selected yes, please provide details</label>
                           <div class="controls">
                               <table class="table table-bordered">
                                   <tr>
                                       <th>Mineral name</th>
                                       <th>Details</th>
                                   </tr>
                                   <tr>
                                       <td>
                                        <asp:DropDownList ID="cbo1" runat="server" />  
                                       </td>
                                       <td>
                                           sdf
                                       </td>
                                   </tr>
                               </table>
                               <asp:LinkButton ID="btnAddScrapSource" runat="server" Text="Add New Line" CssClass="btn btn-success" />
                           </div>


                       </div>
                       <div class="control-group">
                           <label class="control-label">4.3 Does the mineral content come from recycled sources?</label>
                           <div class="controls">
                               <asp:RadioButtonList ID="rblRecycled" runat="server" AutoPostBack="true" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Table" >
                                    <asp:ListItem Text="No" Selected="True" />
                                    <asp:ListItem Text="Yes" Selected="False" />
                                </asp:RadioButtonList>

                           </div>
                           </div>
                       
                       
                       <div class="control-group">
                           <label class="control-label">You have Selected yes, please provide details</label>
                           <div class="controls">
                               <table class="table table-bordered">
                                   <tr>
                                       <th>Mineral name</th>
                                       <th>Details</th>
                                   </tr>
                                   <tr>
                                       <td>
                                       <asp:DropDownList ID="cbo2" runat="server" />  
                                       </td>
                                       <td>
                                           sdf
                                       </td>
                                   </tr>
                               </table>
                               <asp:LinkButton ID="btnAddRecycled" runat="server" Text="Add New Line" CssClass="btn btn-success" />
                           </div>


                       </div>
                       
                       <legend>5. Quantity, date and method of extraction </legend>
                       <div class="control-group">
                           <label>5.1 If you answered yes to questions 2.2 and 2.4 (system automated), please provide details of the quantity of minerals, date of extraction</label>
                           <div>
                               <table class="table table-bordered">
                                   <tr>
                                       <th>Mineral</th>
                                       <th>Quantity</th>
                                       <th>Date of extraction</th>
                                       <th>Method Extraction</th>
                                   </tr>
                                   <tr>
                                        <td><asp:DropDownList ID="DropDownList1" runat="server" /></td>
                                        <td><asp:TextBox runat="server" ID="txtExtractionQuantity" CssClass="input-mini"></asp:TextBox></td>
                                        <td>Date picker</td>
                                        <td><asp:DropDownList ID="cboExtractionMethod" runat="server" /><br />Drop down from DB using artisanal|small-scale|large-scale mining</td>
                                   </tr>

                               </table>
                           <asp:LinkButton ID="btnAddExtraction" runat="server" Text="Add New Line" CssClass="btn btn-success" />

                           </div>
                       </div>
                       <legend>6. Processing Facility</legend>
                       <div class="control-group">
                           <label><strong>If you answered yes to questions 2.2 and 2.4 (system automated):</strong></label>
                           <label>At which facility are the minerals processed?</label>
                           <div>
                               <table class="table table-bordered">
                                   <tr>
                                       <th>Name</th>
                                       <th>Location</th>
                                       
                                   </tr>
                                   <tr>
                                        <td></td>
                                        <td></td>
                                        
                                   </tr>

                               </table>
                               <span class="help-block">state name and location of facilitate</span>
                           <asp:LinkButton ID="btnAddFacility" runat="server" Text="Add New Line" CssClass="btn btn-success" />
                           </div>
                       </div>
                       
                       <div class="control-group">
                           <label class="control-label">Is the facility included in the Conflict Free Smelter list?</label>
                           show list todo, need to get full list from http://www.conflictfreesmelter.org/ - jennifer on the case!
                       </div>
                       <div class="control-group">
                           <label>Has the facility been subject to an independent audit that has led it being designated "conflict-free" (in relation to conflict minerals)? </label>
                           Yes no and details table
                           <table class="table table-bordered">
                               <tr>
                                   <th></th>
                                    <th></th>
                               </tr>
                               <tr>
                                   <td></td>
                                   <td></td>
                               </tr>
                           </table>
                           <asp:LinkButton ID="btnAddAuditDetails" runat="server" Text="Add New Line" CssClass="btn btn-success" />
                       </div>


                   </asp:Panel>
                   
                   <asp:Panel ID="panPage5" Visible="False" runat="server">
                      <legend>8. Transport and supply of conflict </legend> 
                       <label><strong>If you answered yes to question 3 and 5:</strong></label>
                       <div class="control-group">
                       <label>Who supplies you with the minerals or products containing the minerals? Please provide name and address details</label>
                        <table class="table table-bordered">
                               <tr>
                                   <th>Name</th>
                                   <th>Address Details</th>
                               </tr>
                               <tr>
                                   <td></td>
                                   <td></td>
                               </tr>
                           </table>
                           <asp:LinkButton ID="btnAddSuppliers" runat="server" Text="Add New Line" CssClass="btn btn-success" />
                       </div>
                       <div class="control-group">
                           <label>What countries are the minerals transported through? </label>
                        <table class="table table-bordered">
                               <tr>
                                   <th>Countries</th>
                                   <th>Address Details</th>
                               </tr>
                               <tr>
                                   <td></td>
                                   <td></td>
                               </tr>
                           </table>
                           <asp:LinkButton ID="LinkButton4" runat="server" Text="Add New Line" CssClass="btn btn-success" />
                       </div>
                      <legend>9. Supply Chain</legend>
                      <label>16	If you answered yes to questions 3 and 5, please identify all upstream intermediaries, consolidators and other actors in your supply chain</label>
                      <table class="table table-bordered">
                               <tr>
                                   <th>upstream intermediaries</th>
                                   <th>other Details</th>
                               </tr>
                               <tr>
                                   <td></td>
                                   <td></td>
                               </tr>
                           </table>
                           <asp:LinkButton ID="LinkButton5" runat="server" Text="Add New Line" CssClass="btn btn-success" />
                       <legend>10. Taxes & payments</legend>
                       <label><strong>If you answered yes to questions 3 and 5, please disclose:</strong></label>
                       <div class="control-group">
                           <label>all taxes, fees or royalties paid to the government of a country listed in question 10 for the purposes of extraction, trade, transport and export of minerals</label>
                            <table class="table table-bordered">
                               <tr>
                                   <th>Country</th>
                                   <th>Details</th>
                               </tr>
                               <tr>
                                   <td></td>
                                   <td></td>
                               </tr>
                           </table>
                           <asp:LinkButton ID="LinkButton6" runat="server" Text="Add New Line" CssClass="btn btn-success" />
                       </div>
                       
                       <div class="control-group">
                           <label>Any other payments made to government officials for the purposes of extraction, trade, transport or export of minerals</label>
                           <table class="table table-bordered">
                               <tr>
                                   <th>Payment</th>
                                   <th>Details</th>
                               </tr>
                               <tr>
                                   <td></td>
                                   <td></td>
                               </tr>
                           </table>
                           <asp:LinkButton ID="LinkButton7" runat="server" Text="Add New Line" CssClass="btn btn-success" />

                       </div>
                       <div class="control-group">
                           <label>All taxes and any other payments made to public or private security forces or other armed groups at all points in the supply chain from extraction onwards</label>
                            <table class="table table-bordered">
                               <tr>
                                   <th>Payment</th>
                                   <th>Details</th>
                               </tr>
                               <tr>
                                   <td></td>
                                   <td></td>
                               </tr>
                           </table>
                           <asp:LinkButton ID="LinkButton8" runat="server" Text="Add New Line" CssClass="btn btn-success" />
                       </div>

                   </asp:Panel>
                   
                   <asp:Panel ID="panPage6" runat="server" Visible="False">
                       <legend>11. Your Policies</legend>
                       <label>If you answered yes to question 3:</label>
                       <div class="control-group">
                           <label>Do you have a policy on the sourcing of conflict minerals? Yes/No – if yes, please provide </label>
                           <Telerik:RadProgressManager runat="server" ID="RadProgressManager1" />
                           <Telerik:RadAsyncUpload ID="RadAsyncUpload1" runat="server"  MultipleFileSelection="Automatic" TargetFolder="~/userfiles" OnClientFilesUploaded="UploadFile" />
                           <Telerik:RadProgressArea runat="server" ID="RadProgressArea1" />
                       </div>
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
                   </asp:Panel>
                  <p></p>
                    <p>
                        <asp:Button ID="btnPrev" runat="server" CssClass="btn" Text=" &lt;&lt; Prev Page" />&nbsp;&nbsp;<asp:Button ID="btnNext" runat="server" CssClass="btn" Text="Next Page &gt;&gt;" />&nbsp;&nbsp;<asp:Button ID="btnSave" runat="server" CssClass="btn btn-warning" Text="Save Draft" ValidationGroup="Questions" /></p>

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
    </div>
      <div class="span3">
                
                <uc1:submenu1 ID="submenu11" runat="server" />
                
            </div>
</asp:Content>

