<%@ Page Title="" Language="VB" MasterPageFile="~/masterpages/templatefull.master" AutoEventWireup="false" CodeFile="mycompanies.aspx.vb" Inherits="mycompanies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpcMainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="span9">
                <h2><asp:Label runat="server" ID="lblManageCompaniesPageTitle" /></h2>
                <p>Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui. </p>
                
                    <asp:Panel ID="panMyCompanies" runat="server">
                        
                        <p><b>The list below shows the list of companies that you are resonsible for.</b></p>
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
                    <asp:LinkButton ID="btnGoToAdd" runat="server">Can't find your company? Click here to add a new one.</asp:LinkButton> <br />
                    <asp:LinkButton ID="btnCancelSearch" runat="server">Don't want to search right now? Click here to return to your home page</asp:LinkButton> 

                </asp:Panel>

                <asp:Panel ID="panAddCompany" runat="server" Visible="false">
                    <div class="form-signin form-horizontal">
                        
                        <asp:Button ID="btnGoback" runat="server" CausesValidation="False" CssClass="btn btn-danger pull-right" Text="Go back and Search" />
                        <h4>Enter company details</h4>
                        <p>Use the form below to add your company. Please ensure you have <a href="#">seached</a> for your company first. Items marked with a <span class="alert-error">*</span> are required.</p>
                        
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
                            <label><span class="alert-error">*</span> Address line 1:</label>
                            <asp:TextBox runat="server" ID="txtAddAddressLine1" CssClass="input-xlarge"></asp:TextBox>
                            <label>Address line 2:</label><asp:TextBox runat="server" ID="TextBox1" CssClass="input-xlarge"></asp:TextBox>
                            <label>Address line 3:</label><asp:TextBox runat="server" ID="TextBox2" CssClass="input-xlarge"></asp:TextBox>
                            <label>Address line 4:</label><asp:TextBox runat="server" ID="TextBox3" CssClass="input-xlarge"></asp:TextBox>
                            <label><span class="alert-error">*</span> City:</label><asp:TextBox runat="server" ID="TextBox4" CssClass="input-large"></asp:TextBox>
                            <label><span class="alert-error">*</span> Post code/Zip code:</label><asp:TextBox runat="server" ID="txtPostCode" CssClass="input-small"></asp:TextBox>
                            <label><span class="alert-error">*</span> Country:</label>
                            <asp:DropDownList runat="server" ID="cboCountries"/>
                            <label><span class="alert-error">*</span> Telephone Number:</label></label><asp:TextBox runat="server" ID="TextBox5" CssClass="input-large"></asp:TextBox>
                            <label>Fax number:</label></label><asp:TextBox runat="server" ID="TextBox6" CssClass="input-large"></asp:TextBox>
                            <label>Telex:</label></label><asp:TextBox runat="server" ID="TextBox7" CssClass="input-large"></asp:TextBox>
                            <label>Website URL:</label></label><asp:TextBox runat="server" ID="TextBox8" CssClass="input-large"></asp:TextBox>
                            <label><span class="alert-error">*</span> Email address:</label></label><asp:TextBox runat="server" ID="TextBox9" CssClass="input-xxlarge"></asp:TextBox>
                            <label>Twitter:</label></label>
                             <div class="input-prepend">
                                  <span class="add-on">@</span>
                                  <input class="span2" id="prependedInput" type="text" placeholder="Username">
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
                                        <li>
                                            <asp:LinkButton ID="btnCompanyName" runat="server" /></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        <p>
                            <asp:Label ID="lblNoCustomers" runat="server" CssClass="failureNotification" />
                        </p>
                        <p>
                            <asp:Button ID="btnAddCustomer" runat="server" Text="Add Customer" CssClass="btn" />
                        </p>

                    </div>
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

            </div>

            <div class="span3">
                <div class="well sidebar-nav">
                    <ul class="nav nav-list">
                        <li class="nav-header">Main Navigation</li>
                        <li class="active"><a href="home.aspx">Your home page</a></li>
                        <li><a href="mycompanies.aspx">Manage my companies</a></li>
                        <li><a href="#">Manage my data</a></li>
                       
                    </ul>
                    <ul class="nav nav-list">
                        <li class="nav-header">My latest activities</li>
                        <li class="active"><a href="#">Link</a></li>
                        <li><a href="#">Link</a></li>
                        <li><a href="#">Link</a></li>
                        <li><a href="#">Link</a></li>
                        <li class="nav-header">Another bit of info</li>
                        <li><a href="#">Link</a></li>
                        <li><a href="#">Link</a></li>
                        <li><a href="#">Link</a></li>
                        <li><a href="#">Link</a></li>
                        <li><a href="#">Link</a></li>
                        <li><a href="#">Link</a></li>

                    </ul>
                </div>
                <!--/.well -->

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>




</asp:Content>

