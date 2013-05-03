<%@ Page Title="" Language="VB" MasterPageFile="~/masterpages/templatefull.master" AutoEventWireup="false" CodeFile="mycompanies.aspx.vb" Inherits="mycompanies" %>
<%@ Register src="controls/submenu1.ascx" tagname="submenu1" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpcMainContent" runat="Server">
        <Telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
            <div class="span9">
              <asp:Panel ID="panMyCompanies" runat="server">
                  <asp:Button ID="btnAddCompany" runat="server" Text="Start Company Association process &raquo;" CssClass="btn btn-success pull-right" />       
                  <h2><asp:Label runat="server" ID="lblManageCompaniesPageTitle" /></h2>
                        
                        <p><b>The list below shows the list of companies that you are responsible for. Click on the Company to view more details.</b></p>
                        <ul>
                            <asp:Repeater ID="rptMyCompanies" runat="server" OnItemDataBound="BindCompanies">
                                <ItemTemplate>
                                    <li>
                                        <asp:LinkButton ID="btnCompanyName" runat="server" OnClick="GetMyRelationships" />&nbsp;<asp:Label ID="lblStatus" runat="server" />
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                        <p>
                            <asp:Label ID="lblNoCompanies" runat="server" CssClass="label-nodata" EnableViewState="false" /> <asp:Label runat="server" ID="lblNoCompaniesHelp" />
                        </p>
                        <p>
                            If you wish to associate yourself with another company then click the "Start Company Association process" button.
                        </p>
                       
                            
                     </asp:Panel>
                

                <asp:Panel runat="server" ID="panSearchCompanies" Visible="False">
                    <h3>Search for your Company</h3>
                    Enter your Search term: <asp:TextBox ID="txtSearch" runat="server" CssClass="form-search search-query" placeholder="Search..." TabIndex="1" /> <asp:Button ID="btnSearch" runat="server" ValidationGroup="search"  CssClass="btn btn-warning" Text="Search" /><br />
                    <asp:RequiredFieldValidator ID="rfvSearch" ValidationGroup="search" ControlToValidate="txtSearch" CssClass="error" ForeColor="red" runat="server" Display="Dynamic" ErrorMessage="Please enter a search term"></asp:RequiredFieldValidator>
                    <br />
                    <h4><asp:Label runat="server" ID="Label1" />Results found:</h4>
                    <ul>
                        <asp:Repeater ID="rptFoundCompanies" runat="server">
                            <ItemTemplate>
                                <li>
                                    <asp:HyperLink ID="hypCompanyNameSR" runat="server" /> - <asp:LinkButton ID="btnCompanyName" CssClass="btn-small btn-info" runat="server" OnClick="JoinCompany" />
                                </li>
                            
                            <asp:Panel CssClass="popover" ID="panPopup" runat="server">
                                <div class="popover-content">
                                    <h5>
                                        Full details for company</h5>
                                    <asp:Image ID="imgCompanyLogo" runat="server" ImageUrl="img/apple-touch-icon-144-precomposed.png" />
                                    <h6>
                                        <asp:Literal ID="litCompanyName" runat="server" />
                                    </h6>
                                    <h6>
                                        <asp:Literal ID="litCompanyAddress" runat="server" /></h6>
                                </div>
                            </asp:Panel>
                            <AjaxToolkit:HoverMenuExtender ID="HoverMenuExtender2" runat="Server" TargetControlID="hypCompanyNameSR"
                                PopupControlID="panPopUp" HoverCssClass="popupHover" PopupPosition="Right" OffsetX="0"
                                OffsetY="0" PopDelay="50" />
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    
                    <asp:Panel ID="panNoResults1" runat="server" Visible="false">
                        <p>No results found for the search you entered, please adjust and try again.</p>
                    </asp:Panel>
                    
                    <asp:Panel ID="panTooManyRecords1" runat="server" Visible="false">
                        <p>Too many records found, please narrow your search and try again.</p>
                    </asp:Panel>
                    
                    <p><asp:LinkButton ID="btnAddCompany1" runat="server" Text="Click Here" /> to add your company yourself.<br />
                        Don't want to search right now?<asp:LinkButton ID="btnCancelSearch" runat="server"> Click here</asp:LinkButton> to return to your home page.
                    </p> 
                    
                </asp:Panel>

                <asp:Panel ID="panAddCompany" runat="server" Visible="false">
                    <div class="form-signin form-horizontal">
                        
                        <asp:Button ID="btnGoback" runat="server" CausesValidation="False" CssClass="btn btn-danger pull-right" Text="Go back and Search" />
                        <h4>Enter company details</h4>
                        <p>Use the form below to add your company. Please ensure you have 
                            <asp:LinkButton ID="btnAddCompany2" runat="server" Text="Searched" /> for your company first. Items marked with a <span class="alert-error">*</span> are required.</p>
                        
                            <div class="control-group">
                                <label class="control-label"><span class="alert-error">*</span> Company Name:</label>
                                <div class="controls"><asp:TextBox ID="txtCompanyName" runat="server" CssClass="input-xxlarge" placeholder="Company name" TabIndex="1" /><br />
                                <asp:RequiredFieldValidator ID="rfvAddCompanayName" ControlToValidate="txtCompanyName" 
                                    CssClass="error" ForeColor="red" runat="server" Display="Dynamic" ErrorMessage="please enter the Company Name" ValidationGroup="AddCompany" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label">Parent Company:</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtParentCompany" runat="server" CssClass="input-xxlarge" 
                                    TabIndex="2" Enabled="false" placeholder="No Parent Company Assigned" /><br />
                                    [<asp:LinkButton ID="btnParentCompany" runat="server" Text="Choose Company" />]&nbsp;&nbsp;
                                    [<asp:LinkButton ID="btnRemoveParent" runat="server" Text="Remove Company" Enabled="false" />]
                                    <asp:HiddenField ID="hidParentCompanyID" runat="server" Value="0" />
                                </div>
                            </div>
                            <asp:Panel ID="panParent" runat="server" Visible="false">
                                <div class="control-group">
                                    <div class="controls">
                                <asp:Button ID="btnCancelParent" runat="server" CssClass="btn-small btn-danger" Text="Cancel"
                                    CausesValidation="False" />
                                <h4>
                                    Find parent company</h4>
                                Company search:
                                <asp:TextBox ID="txtParentSearch" runat="server" CssClass="form-search search-query" placeholder="Search..."
                                    TabIndex="1" />
                                <asp:Button ID="btnParentSearch" runat="server" ValidationGroup="search" CssClass="btn-small btn-warning"
                                    Text="Search" /><br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="search"
                                    ControlToValidate="txtParentSearch" CssClass="error" ForeColor="red"
                                    runat="server" Display="Dynamic" ErrorMessage="Please enter a search term" />
                                <h5>
                                    Search Results:
                                </h5>
                                <ul>
                                    <asp:Repeater ID="rptParentCompany" runat="server">
                                        <ItemTemplate>
                                            <li>
                                                <asp:HyperLink ID="hypCompanyNameSR" runat="server" /> - <asp:LinkButton ID="btnCompanyName" runat="server" OnClick="ChooseParentCompany" />
                                            </li>
                                            <asp:Panel CssClass="popover" ID="panPopup" runat="server">
                                                <div class="popover-content">
                                                    <h5>
                                                        Full details for company</h5>
                                                    <asp:Image ID="imgCompanyLogo" runat="server" ImageUrl="img/apple-touch-icon-144-precomposed.png" />
                                                    <h6>
                                                        <asp:Literal ID="litCompanyName" runat="server" />
                                                    </h6>
                                                    <h6>
                                                        <asp:Literal ID="litCompanyAddress" runat="server" /></h6>
                                                </div>
                                            </asp:Panel>
                                            <AjaxToolkit:HoverMenuExtender ID="HoverMenuExtender2" runat="Server" TargetControlID="hypCompanyNameSR"
                                                PopupControlID="panPopUp" HoverCssClass="popupHover" PopupPosition="Right" OffsetX="0"
                                                OffsetY="0" PopDelay="50" />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                                <p><asp:Label ID="lblParentError" runat="server" EnableViewState="false" CssClass="error" /></p>
                              </div>
                            </div>
                            </asp:Panel>
                            <div class="control-group">
                                <label class="control-label"><span class="alert-error">*</span> Business area:</label>
                                <div class="controls"><asp:DropDownList runat="server" ID="cboBusinessArea"/><br />
                                    <asp:RequiredFieldValidator ID="rfvBusinessAreaID" ControlToValidate="cboBusinessArea"
                                        CssClass="error" ForeColor="red" runat="server" Display="Dynamic" ErrorMessage="please chose a business area"
                                        ValidationGroup="AddCompany" /></div>
                            </div>
                            <div class="control-group">
                                <label class="control-label"><span class="alert-error">*</span> Address 1:</label>
                                <div class="controls"><asp:TextBox runat="server" ID="txtAddress1" CssClass="input-xlarge" /><br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtAddress1"
                                        CssClass="error" ForeColor="red" runat="server" Display="Dynamic" ErrorMessage="please enter the company street address"
                                        ValidationGroup="AddCompany" /></div>
                            </div>
                            <div class="control-group">
                                
                                <label class="control-label">Address line 2:</label>
                                <div class="controls"><asp:TextBox runat="server" ID="txtAddress2" CssClass="input-xlarge" /></div>
                            </div>
                            <div class="control-group">
                                
                                <label class="control-label">Address line 3:</label>
                                <div class="controls"><asp:TextBox runat="server" ID="txtAddress3" CssClass="input-xlarge" /></div>

                            </div>
                            <div class="control-group">
                                <label class="control-label">Address line 4:</label>
                                <div class="controls"><asp:TextBox runat="server" ID="txtAddress4" CssClass="input-xlarge"></asp:TextBox></div>
                            </div>
                            
                            <div class="control-group">
                                <label class="control-label"><span class="alert-error">*</span> City:</label>
                                <div class="controls">
                                <asp:TextBox runat="server" ID="txtCity" CssClass="input-large" /><br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtCity"
                                        CssClass="error" ForeColor="red" runat="server" Display="Dynamic" ErrorMessage="please enter the company city"
                                        ValidationGroup="AddCompany" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label"><span class="alert-error">*</span> Post code/Zip code:</label>
                                    <div class="controls"><asp:TextBox runat="server" ID="txtPostcode" CssClass="input-small" /><br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtPostcode"
                                            CssClass="error" ForeColor="red" runat="server" Display="Dynamic" ErrorMessage="please enter the company postcode"
                                            ValidationGroup="AddCompany" /></div>
                            </div>
                        
                            <div class="control-group">
                                 <label class="control-label"><span class="alert-error">*</span> Country:</label>
                                 <div class="controls"><asp:DropDownList runat="server" ID="cboCountries" />
                                     <br />
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="cboCountries"
                                         CssClass="error" ForeColor="red" runat="server" Display="Dynamic" ErrorMessage="please set the company country"
                                         ValidationGroup="AddCompany" /></div>
                            </div>
                            
                            <div class="control-group">
                                
                                <label class="control-label"><span class="alert-error">*</span> Telephone Number:</label>
                                <div class="controls"><asp:TextBox runat="server" ID="txtTelephone" CssClass="input-large" /><br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtTelephone"
                                        CssClass="error" ForeColor="red" runat="server" Display="Dynamic" ErrorMessage="please enter the company telephone number"
                                        ValidationGroup="AddCompany" /></div>
                            </div> 
                           
                            <div class="control-group">
                               <label class="control-label">Fax number:</label>
                                <div class="controls"><asp:TextBox runat="server" ID="txtFaxNumber" CssClass="input-large" /></div>

                            </div>
                        
                            <div class="control-group">
                                <label class="control-label">Telex:</label>
                                <div class="controls"><asp:TextBox runat="server" ID="txtTelex" CssClass="input-large" /></div>
                            </div>
                        
                            <div class="control-group">
                                <label class="control-label">Website URL:</label>
                                <div class="controls"><asp:TextBox runat="server" ID="txtWebsite" CssClass="input-xxlarge" /></div>
                            </div>
                        <div class="control-group">
                            <label class="control-label">
                                Facebook URL:</label>
                            <div class="controls">
                                <asp:TextBox runat="server" ID="txtFaceBook" CssClass="input-xxlarge" /></div>
                        </div>
                        
                        <div class="control-group">
                            <label class="control-label"><span class="alert-error">*</span> Email address:</label>
                            <div class="controls"><asp:TextBox runat="server" ID="txtEmailAddress" CssClass="input-xxlarge" /><br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtEmailAddress"
                                    CssClass="error" ForeColor="red" runat="server" Display="Dynamic" ErrorMessage="please enter a valid email address"
                                    ValidationGroup="AddCompany" /></div>

                        </div>
                            
                          <div class="control-group">
                               <label class="control-label">Twitter:</label>
                              <div class="controls">
                              <div class="input-prepend">
                                  <span class="add-on">@</span>
                                  <asp:TextBox runat="server" ID="txtTwitter" CssClass="input-xxlarge" placeholder="Username" />
                            </div>
                                  </div>
                          </div>
                        
                          <br />
                           <asp:Button ID="btnAddNewCompany" runat="server" CssClass="btn btn-warning" Text="Add New Company" ValidationGroup="AddCompany" />&nbsp;&nbsp;<asp:Button ID="btnCancelAdd" runat="server" CssClass="btn btn-danger" Text="Cancel" CausesValidation="False" />
                           <p><asp:Label ID="lblAddCompany" runat="server" EnableViewState="false" CssClass="error" /></p>
                    </div> 
                   
                </asp:Panel>
                <hr />

               
                
                <asp:Panel ID="panConfirmAdd" runat="server" Visible="false">
                    <h2>Add New Company</h2>
                    <p>Your new company has been added!</p>
                    <p><asp:HyperLink ID="hypRefreshPage" runat="server" NavigateUrl="~/mycompanies.aspx" Text="Click Here" /> to return to your home page.</p>
                </asp:Panel>

                <asp:Panel ID="panCustomers" runat="server">
                    <div class="span6">
                        <h4><asp:Label runat="server" ID="lblCompanyCustomers" /> Customers:</h4>
                            <ul>
                                <asp:Repeater ID="rptCustomers" runat="server">
                                    <ItemTemplate>
                                        <li><asp:HyperLink ID="hypCompanyNameSR" runat="server" /> - <asp:LinkButton ID="btnCompanyName" runat="server" />&nbsp;<asp:label ID="lblStatus" runat="server" /></li>
                                        <asp:Panel CssClass="popover" ID="panPopup" runat="server">
                                            <div class="popover-content">
                                                <h5>
                                                    Full details for company</h5>
                                                <asp:Image ID="imgCompanyLogo" runat="server" ImageUrl="img/apple-touch-icon-144-precomposed.png" />
                                                <h6>
                                                    <asp:Literal ID="litCompanyName" runat="server" />
                                                </h6>
                                                <h6><asp:Literal ID="litCompanyAddress" runat="server" /></h6>
                                            </div>
                                        </asp:Panel>
                                            <AjaxToolkit:HoverMenuExtender ID="hme2" runat="Server" TargetControlID="btnCompanyName"
                                                PopupControlID="panPopUp" HoverCssClass="popupHover" PopupPosition="Right" OffsetX="0"
                                                OffsetY="0" PopDelay="50" />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        <p>
                            <asp:Label ID="lblNoCustomers" runat="server" CssClass="failureNotification" EnableViewState="false" />
                        </p>
                        <p>
                            <asp:Button ID="btnAddCustomer" runat="server" Text="Apply to be Customer" CssClass="btn" />
                        </p>

                    </div>
                    
                </asp:Panel>

                <asp:Panel runat="server" ID="panApplyCustomer" Visible="False">
                    <div class="span6">
                    <asp:Button ID="btnCancelApplyCustomer" runat="server" CssClass="btn-small btn-danger" Text="Cancel" CausesValidation="False" />
                    <h4>Apply to be a Customer</h4>
                        Customer search: <asp:TextBox ID="txtSearchCustomerCompany" runat="server" CssClass="form-search search-query" placeholder="Search..." TabIndex="1" /> <asp:Button ID="btnSearchCustomerCompany" runat="server" ValidationGroup="search"  CssClass="btn-small btn-warning" Text="Search" /><br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="search"
                        ControlToValidate="txtSearchCustomerCompany" CssClass="error" ForeColor="red"
                        runat="server" Display="Dynamic" ErrorMessage="Please enter a search term"></asp:RequiredFieldValidator>
                        <h5>Search Results: </h5>
                        <ul>
                            <asp:Repeater ID="rptCustomerSearch" runat="server">
                                <ItemTemplate>
                                    <li>
                                       <asp:HyperLink ID="hypCompanyNameSR" runat="server" /> -  <asp:LinkButton ID="btnCompanyName" runat="server" OnClick="ApplyForCustomer" />
                                    </li>
                                    <asp:Panel CssClass="popover" ID="panPopup" runat="server">
                                        <div class="popover-content">
                                            <h5>Full Company details </h5>
                                            <asp:Image ID="imgCompanyLogo" runat="server" ImageUrl="img/apple-touch-icon-144-precomposed.png" />
                                            <h6>
                                                <asp:Literal ID="litCompanyName" runat="server" />
                                            </h6>
                                            <h6>
                                                <asp:Literal ID="litCompanyAddress" runat="server" /></h6>
                                        </div>
                                    </asp:Panel>
                                    <AjaxToolkit:HoverMenuExtender ID="HoverMenuExtender2" runat="Server" TargetControlID="btnCompanyName"
                                        PopupControlID="panPopUp" HoverCssClass="popupHover" PopupPosition="Right" OffsetX="0"
                                        OffsetY="0" PopDelay="50" />
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                        <asp:Panel ID="panNoResults2" runat="server" Visible="false">
                            <p>
                                No results found for the search you entered, please adjust and try again.</p>
                        </asp:Panel>
                        <asp:Panel ID="panTooManyRecords2" runat="server" Visible="false">
                            <p>
                                Too many records found, please narrow your search and try again.</p>
                        </asp:Panel>
                </asp:Panel>

                <asp:Panel ID="panSuppliers" runat="server">
                     <div class="span6">
                        <h4><asp:Label runat="server" ID="lblCompanySuppliers" /> Suppliers:</h4>

                            <ul>
                                <asp:Repeater ID="rptSuppliers" runat="server" OnItemDataBound="BindCompanies">
                                    <ItemTemplate>
                                        <li>
                                            <asp:HyperLink ID="hypCompanyNameSR" runat="server" /> - <asp:LinkButton ID="btnCompanyName" runat="server" />&nbsp;<asp:Label ID="lblStatus" runat="server" /></li>
                                        <asp:Panel CssClass="popover" ID="panPopup" runat="server">
                                            <div class="popover-content">
                                                 <h5>Full Company details </h5>
                                                <asp:Image ID="imgCompanyLogo" runat="server" ImageUrl="img/apple-touch-icon-144-precomposed.png" />
                                                <h6>
                                                    <asp:Literal ID="litCompanyName" runat="server" />
                                                </h6>
                                                <h6>
                                                    <asp:Literal ID="litCompanyAddress" runat="server" /></h6>
                                            </div>
                                        </asp:Panel>
                                        <AjaxToolkit:HoverMenuExtender ID="hme2" runat="Server" TargetControlID="btnCompanyName"
                                            PopupControlID="panPopUp" HoverCssClass="popupHover" PopupPosition="Right" OffsetX="0"
                                            OffsetY="0" PopDelay="50" />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>

                        <p>
                            <asp:Label ID="lblNoSuppliers" runat="server" CssClass="failureNotification" EnableViewState="false" />
                        </p>
                        <p>
                            <asp:Button ID="btnAddSupplier" runat="server" Text="Add Supplier" CssClass="btn" />
                        </p>

                    </div>
                </asp:Panel>
                
                <asp:Panel ID="panAddSupplier" runat="server">
                    <div class="span6">
                        <asp:Button ID="btnCancelAddSupplier" runat="server" CssClass="btn-small btn-danger" Text="Cancel" CausesValidation="False" />
                        <h4>Add a new supplier</h4> 
                        Supplier search: <asp:TextBox ID="txtSupplierSearch" runat="server" CssClass="form-search search-query" placeholder="Search..." TabIndex="1" /> 
                        <asp:Button ID="btnSearchSuppliers" runat="server" ValidationGroup="search"  CssClass="btn-small btn-warning" Text="Search" /><br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="search"
                            ControlToValidate="txtSupplierSearch" CssClass="error" ForeColor="red"
                            runat="server" Display="Dynamic" ErrorMessage="Please enter a search term" />
                        <ul>
                            <asp:Repeater ID="rptSupplierSearch" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <asp:HyperLink ID="hypCompanyNameSR" runat="server" /> -  <asp:LinkButton ID="btnCompanyName" runat="server" OnClick="ApplyForSupplier" />
                                    </li>
                                    <asp:Panel CssClass="popover" ID="panPopup" runat="server">
                                        <div class="popover-content">
                                             <h5>Full Company details </h5>
                                            <asp:Image ID="imgCompanyLogo" runat="server" ImageUrl="img/apple-touch-icon-144-precomposed.png" />
                                            <h6>
                                                <asp:Literal ID="litCompanyName" runat="server" />
                                            </h6>
                                            <h6>
                                                <asp:Literal ID="litCompanyAddress" runat="server" /></h6>
                                        </div>
                                    </asp:Panel>
                                    <AjaxToolkit:HoverMenuExtender ID="HoverMenuExtender2" runat="Server" TargetControlID="btnCompanyName"
                                        PopupControlID="panPopUp" HoverCssClass="popupHover" PopupPosition="Right" OffsetX="0"
                                        OffsetY="0" PopDelay="50" />
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                        <asp:Panel ID="panNoResults3" runat="server" Visible="false">
                            <p>
                                No results found for the search you entered, please adjust and try again.</p>
                        </asp:Panel>
                        <asp:Panel ID="panTooManyRecords3" runat="server" Visible="false">
                            <p>
                                Too many records found, please narrow your search and try again.</p>
                        </asp:Panel>

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
    <Telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Telerik" Transparency="0" IsSticky="False" />          
    
    <div class="span3">
        <asp:Panel runat="server" ID="panSubNav">
            <uc1:submenu1 ID="submenu11" runat="server" />      
        </asp:Panel>
    </div>
        
    

</asp:Content>

