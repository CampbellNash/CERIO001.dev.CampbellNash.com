﻿<%@ Page Title="" Language="VB" MasterPageFile="~/masterpages/templatefull.master" AutoEventWireup="false" CodeFile="mycompanies.aspx.vb" Inherits="mycompanies" %>
<%@ Register src="controls/submenu1.ascx" tagname="submenu1" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpcMainContent" runat="Server">
    
            
            <div class="span9">
              <asp:Panel ID="panMyCompanies" runat="server">
                         <h2><asp:Label runat="server" ID="lblManageCompaniesPageTitle" /></h2>
               
                        <p><b>The list below shows the list of companies that you are responsible for.</b></p>
                        <ul>
                            <asp:Repeater ID="rptMyCompanies" runat="server" OnItemDataBound="BindCompanies">
                                <ItemTemplate>
                                    <li>
                                        <asp:LinkButton ID="btnCompanyName" runat="server" OnClick="GetMyRelationships" />
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                        <p>
                            <asp:Label ID="lblNoCompanies" runat="server" CssClass="label-nodata" /> <asp:Label runat="server" ID="lblNoCompaniesHelp" />
                        </p>
                        <p>
                            If you wish to associate yourself with another company then click the button below.
                        </p>
                       
                            <asp:Button ID="btnAddCompany" runat="server" Text="Start Company Association process &raquo;" CssClass="btn btn-success" />
                     </asp:Panel>

                <asp:Panel runat="server" ID="panSearchCompanies" Visible="False">
                    <h3>Search for your Company</h3>
                    Enter your Search term: <asp:TextBox ID="txtSearch" runat="server" CssClass="form-search search-query" placeholder="Search..." TabIndex="1" /> <asp:Button ID="btnSearch" runat="server" ValidationGroup="search"  CssClass="btn btn-warning" Text="Search" /><br />
                    <asp:RequiredFieldValidator ID="rfvSearch" ValidationGroup="search" ControlToValidate="txtSearch" CssClass="error" ForeColor="red" runat="server" Display="Dynamic" ErrorMessage="Please enter a search term"></asp:RequiredFieldValidator>
                    <br />
                    <h4>
                        <asp:Label runat="server" ID="Label1" />
                        Results found:</h4>
                    <ul>
                        <asp:Repeater ID="rptFoundCompanies" runat="server">
                            <ItemTemplate>
                                <li>
                                    <asp:LinkButton ID="btnCompanyName" runat="server" />
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
                            <AjaxToolkit:HoverMenuExtender ID="HoverMenuExtender2" runat="Server" TargetControlID="btnCompanyName"
                                PopupControlID="panPopUp" HoverCssClass="popupHover" PopupPosition="Right" OffsetX="0"
                                OffsetY="-300" PopDelay="50" />
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    
                    <asp:Panel ID="panNoResults1" runat="server" Visible="false">
                        <p>No results found for the search you entered, please adjust and try again or <asp:LinkButton ID="btnAddCompany1" runat="server" Text="Click Here" /> to add one yourself.</p>
                    </asp:Panel>
                    
                    <asp:Panel ID="panTooManyRecords1" runat="server" Visible="false">
                        <p>Too many records found, please narrow your search and try again.</p>
                    </asp:Panel>
                    
                    <asp:LinkButton ID="btnCancelSearch" runat="server">Don't want to search right now? Click here to return to your home page</asp:LinkButton> 

                </asp:Panel>

                <asp:Panel ID="panAddCompany" runat="server" Visible="false">
                    <div class="form-signin form-horizontal">
                        
                        <asp:Button ID="btnGoback" runat="server" CausesValidation="False" CssClass="btn btn-danger pull-right" Text="Go back and Search" />
                        <h4>Enter company details</h4>
                        <p>Use the form below to add your company. Please ensure you have <a href="#">searched</a> for your company first. Items marked with a <span class="alert-error">*</span> are required.</p>
                        
                            <div class="control-group">
                                <label class="control-label"><span class="alert-error">*</span> Title:</label>
                                <div class="controls"><asp:TextBox ID="txtAddCompanayName" runat="server" CssClass="input-xxlarge" placeholder="Company name" TabIndex="1"></asp:TextBox><br />
                                <asp:RequiredFieldValidator ID="rfvAddCompanayName" ControlToValidate="txtAddCompanayName" CssClass="error" ForeColor="red" runat="server" Display="Dynamic" ErrorMessage="please enter the Company Name"></asp:RequiredFieldValidator>
                                </div>
                            </div>  
                            <div class="control-group">
                                <label class="control-label"><span class="alert-error">*</span> Business area:</label>
                                <div class="controls"><asp:DropDownList runat="server" ID="cboBusinessArea"/></div>
                            </div>
                            <div class="control-group">
                                <label class="control-label"><span class="alert-error">*</span> Address line 1:</label>
                                <div class="controls"><asp:TextBox runat="server" ID="txtAddAddressLine1" CssClass="input-xlarge"></asp:TextBox></div>
                            </div>
                            <div class="control-group">
                                
                                <label class="control-label">Address line 2:</label>
                                <div class="controls"><asp:TextBox runat="server" ID="TextBox1" CssClass="input-xlarge"></asp:TextBox></div>
                            </div>
                            <div class="control-group">
                                
                                <label class="control-label">Address line 3:</label>
                                <div class="controls"><asp:TextBox runat="server" ID="TextBox2" CssClass="input-xlarge"></asp:TextBox></div>

                            </div>
                            <div class="control-group">
                                <label class="control-label">Address line 4:</label>
                                <div class="controls"><asp:TextBox runat="server" ID="TextBox3" CssClass="input-xlarge"></asp:TextBox></div>
                            </div>
                            
                            <div class="control-group">
                                <label class="control-label"><span class="alert-error">*</span> City:</label>
                                <div class="controls">
                                <asp:TextBox runat="server" ID="TextBox4" CssClass="input-large"></asp:TextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label"><span class="alert-error">*</span> Post code/Zip code:</label>
                                    <div class="controls"><asp:TextBox runat="server" ID="txtPostCode" CssClass="input-small"></asp:TextBox></div>
                            </div>
                        
                            <div class="control-group">
                                 <label class="control-label"><span class="alert-error">*</span> Country:</label>
                                 <div class="controls"><asp:DropDownList runat="server" ID="cboCountries"/></div>
                            </div>
                            
                            <div class="control-group">
                                
                                <label class="control-label"><span class="alert-error">*</span> Telephone Number:</label>
                                <div class="controls"><asp:TextBox runat="server" ID="TextBox5" CssClass="input-large"></asp:TextBox></div>
                            </div> 
                           
                            <div class="control-group">
                                
                                <label class="control-label">Fax number:</label>
                                
                                <div class="controls"><asp:TextBox runat="server" ID="TextBox6" CssClass="input-large"></asp:TextBox></div>

                            </div>
                        
                            <div class="control-group">
                                <label class="control-label">Telex:</label>
                                <div class="controls"><asp:TextBox runat="server" ID="TextBox7" CssClass="input-large"></asp:TextBox></div>
                            </div>
                        
                            <div class="control-group">
                                <label class="control-label">Website URL:</label>
                                <div class="controls"><asp:TextBox runat="server" ID="TextBox8" CssClass="input-large"></asp:TextBox></div>
                            </div>
                        
                        <div class="control-group">
                            <label class="control-label"><span class="alert-error">*</span> Email address:</label>
                            <div class="controls"><asp:TextBox runat="server" ID="TextBox9" CssClass="input-xxlarge"></asp:TextBox></div>

                        </div>
                            
                          <div class="control-group">
                               <label class="control-label">Twitter:</label>
                              <div class="controls">
                              <div class="input-prepend">
                                  <span class="add-on">@</span>
                                  <input class="span10" id="prependedInput" type="text" placeholder="Username">
                            </div>
                                  </div>
                          </div>
                        
                          <br />
                           <asp:Button ID="btnAddNewCompany" runat="server" CssClass="btn btn-warning" Text="Add New Company" />&nbsp;&nbsp;<asp:Button ID="btnCancelAdd" runat="server" CssClass="btn btn-danger" Text="Cancel" CausesValidation="False" />
                    </div> 
                   
                </asp:Panel>
                <hr />
                <asp:Panel ID="panCustomers" runat="server">
                    <div class="span6">
                        <h4><asp:Label runat="server" ID="lblCompanyCustomers" /> Customers:</h4>
                            <ul>
                                <asp:Repeater ID="rptCustomers" runat="server" OnItemDataBound="BindCompanies">
                                    <ItemTemplate>
                                        <li><asp:LinkButton ID="btnCompanyName" runat="server" /></li>
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
                                                OffsetY="-300" PopDelay="50" />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        <p>
                            <asp:Label ID="lblNoCustomers" runat="server" CssClass="failureNotification" />
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
                                        <asp:LinkButton ID="btnCompanyName" runat="server" />
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
                                    <AjaxToolkit:HoverMenuExtender ID="HoverMenuExtender2" runat="Server" TargetControlID="btnCompanyName"
                                        PopupControlID="panPopUp" HoverCssClass="popupHover" PopupPosition="Right" OffsetX="0"
                                        OffsetY="-300" PopDelay="50" />
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
                                            <asp:LinkButton ID="btnCompanyName" runat="server" /></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>

                        <p>
                            <asp:Label ID="lblNoSuppliers" runat="server" CssClass="failureNotification" />
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
                                        <asp:LinkButton ID="btnCompanyName" runat="server" />
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
                                    <AjaxToolkit:HoverMenuExtender ID="HoverMenuExtender2" runat="Server" TargetControlID="btnCompanyName"
                                        PopupControlID="panPopUp" HoverCssClass="popupHover" PopupPosition="Right" OffsetX="0"
                                        OffsetY="-300" PopDelay="50" />
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
            <div class="span3">
            <asp:Panel runat="server" ID="panSubNav">
                
                    <uc1:submenu1 ID="submenu11" runat="server" />      
               
            </asp:Panel>
                 </div>
        




</asp:Content>

