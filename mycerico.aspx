<%@ Page Title="" Language="VB" MasterPageFile="~/masterpages/templatefull.master" AutoEventWireup="false" CodeFile="mycerico.aspx.vb" Inherits="mycerico" %>
<%@ Register src="controls/submenu1.ascx" tagname="submenu1" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpcMainContent" runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
    <script type="text/javascript">

        function pageLoad() {
            $(function () {
                $("#tablelegend").popover({ html: true, trigger: 'hover', content: '<label class="label label-inverse">Black</label> - Total</br><label class="label">Gray</label> - Awaiting Approval<br/><label class="label label-success">Green</label> - Compliant </br><label class="label label-important">Red</label> - Non Compliant' });

            });
        }

       
        //<![CDATA[
        function openCertRadWin(URL) {
            var oWindow = radopen(URL, "rwCertificates");
            //oWindow.moveTo(400, 50); 
        }

        function RefreshCertificates() {
            //window.location = 'mycerico.aspx';
            document.getElementById('ctl00_ctl00_cphMainContent_cpcMainContent_btnRefreshCertification').click();
        }
        //]]>           
       
        

    </script>
    <Telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Black">
        <Windows>
            <Telerik:RadWindow runat="server" ID="rwCertificates" DestroyOnClose="false"
                 Modal="true"  VisibleStatusbar="True" 
                Behaviors="Close,Move,Resize,Pin" Skin="MetroTouch" Width="750" Height="500" ReloadOnShow="true" OnClientClose="RefreshCertificates" BackColor="#666666" AutoSizeBehaviors="HeightProportional" AutoSize="False" KeepInScreenBounds="True" Overlay="True" EnableShadow="True" Animation="Fade" ShowContentDuringLoad="False" />
        </Windows>
    </Telerik:RadWindowManager>
    
        <Telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
            <div class="span9">
            <h2>My CERICO</h2>
            <asp:Button ID="btnRefreshCertification" runat="server" CssClass="pull-right" Style="visibility: hidden" />
               
                <asp:Panel ID="panMyCompanies" runat="server" >
                  
                      <asp:Button ID="btnAddCompany" runat="server" Text="Start Company Association process &raquo;" CssClass="btn btn-success pull-right" /> 

                  <h3><asp:Label runat="server" ID="lblManageCompaniesPageTitle" /></h3>
                        
                        The list below shows the list of companies that you are responsible for. Click on the Company to view more details. If you wish to associate yourself with another company then click the "Start Company Association process" button.
                        <table class="table table-striped table-condensed table-bordered" >
                            <caption><a href="#" id="tablelegend" class="btn pull-right" rel="popover" data-placement="left"  title="My Companies table help" data-original-title="My Companies table help">My Companies table help</a></caption>
                            <thead>
                                <tr>
                                    <th>Company Name - Status</th>
                                   
                                    <th colspan="4">
                                        Suppliers
                                       
                                    </th>

                                    <th colspan="4">Customers</th>
                                   
                                    <th>Actions</th>
                                    
                                </tr>
                            </thead>
                            <tbody>
                            <asp:Repeater ID="rptMyCompanies" runat="server" OnItemDataBound="BindCompanies">
                                <ItemTemplate>
                                    <tr>
                                        <td><asp:LinkButton ID="btnCompanyName" runat="server" OnClick="GetMyRelationships" /> - <asp:Label ID="lblStatus" runat="server" /></td>
                                        <td><asp:Label ID="lblTotalSuppliers" runat="server" Text="2" CssClass="label label-inverse" title="Total Number of Suppliers"  /></td>
                                        <td><asp:Label ID="lblUnapprovedSuppliers" runat="server" Text="1" CssClass="label" /></td>
                                        <td><asp:Label ID="lblApprovedSuppliers" runat="server" Text="1" CssClass="label label-success" /></td>
                                        
                                        <td><asp:Label ID="lblNonCompliantSuppliers" runat="server" Text="1" CssClass="label label-important" /></td>
                                        <td><asp:Label ID="lblTotalCustomers" runat="server" Text="1" CssClass="label label-inverse" /></td>
                                         <td><asp:Label ID="lblUnapprovedCustomers" runat="server" Text="1" CssClass="label" /></td>
                                        <td><asp:Label ID="lblApprovedCustomers" runat="server" Text="1" CssClass="label label-success" /></td>
                                       
                                        <td><asp:Label ID="lblNonCompliantCustomers" runat="server" Text="1" CssClass="label label-important" /></td>
                                        <td><asp:LinkButton ID="btnViewApproved" runat="server" CssClass="btn btn-small" OnClick="GetMyRelationships">View Details</asp:LinkButton> 
                                            <asp:LinkButton ID="btnViewCertifications" runat="server" CssClass="btn btn-small" OnClick="GetMyRelationships">View Certifications</asp:LinkButton></td>
                                    </tr>
                                    
                                </ItemTemplate>
                            </asp:Repeater>
                            </tbody>
                       </table>
                        <p>
                            <asp:Label ID="lblNoCompanies" runat="server" CssClass="label-nodata" EnableViewState="false" /> <asp:Label runat="server" ID="lblNoCompaniesHelp" />
                        </p>
                        
                       <hr  />
                 
                 
                  </asp:Panel>
                

                <asp:Panel runat="server" ID="panSearchCompanies" Visible="False" DefaultButton="btnSearch">
                   <a href="mycerico.aspx">Back to All My Companies</a> &raquo; Start Company Association process &raquo; Company Search<br />
                    <h3>Company Search</h3>
                    Enter your Search term: <asp:TextBox ID="txtSearch" runat="server" CssClass="form-search search-query" placeholder="Search..." TabIndex="1" /> <asp:Button ID="btnSearch" runat="server" ValidationGroup="search"  CssClass="btn btn-warning" Text="Search" /><br />
                    <asp:RequiredFieldValidator ID="rfvSearch" ValidationGroup="search" ControlToValidate="txtSearch" CssClass="error" ForeColor="red" runat="server" Display="Dynamic" ErrorMessage="Please enter a search term"></asp:RequiredFieldValidator>
                    <br />
                    <h4><asp:Label runat="server" ID="Label1" />Results found:</h4>
                    <ul>
                        <asp:Repeater ID="rptFoundCompanies" runat="server">
                            <ItemTemplate>
                                <li>
                                    <asp:LinkButton ID="btnCompanyName" CssClass="btn-small btn-info" runat="server" OnClick="JoinCompany" />
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
                                PopupControlID="panPopUp" HoverCssClass="popupHover" PopupPosition="right" OffsetX="10"
                                OffsetY="-100" PopDelay="50" />
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
                        Don't want to search right now?<asp:LinkButton ID="btnCancelSearch" runat="server"> Click here</asp:LinkButton> to return to the mycerico page.
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
                            <asp:Panel ID="panParent" runat="server" Visible="false" DefaultButton="btnParentSearch">
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
                                                <asp:HyperLink ID="hypCompanyNameSR" runat="server" /> - <asp:LinkButton ID="btnCompanyName" CssClass="btn btn-small btn-success" runat="server" OnClick="ChooseParentCompany" />
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
                                            <AjaxToolkit:HoverMenuExtender ID="HoverMenuExtender3" runat="Server" TargetControlID="hypCompanyNameSR"
                                                PopupControlID="panPopUp" HoverCssClass="popupHover" PopupPosition="Top" OffsetX="0"
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
                
                <asp:Panel ID="panCompanyCertification" runat="server" Visible="false">
                    <asp:Button ID="btnCancelCompanyCert" runat="server" Text="Back" CssClass="btn btn-danger pull-right" />
                    <asp:Literal ID="litCompanyRef" runat="server" />
                    <table class="table table-striped table-condensed table-bordered">
                            <thead>
                                <tr>
                                    <th>Certification name</th>
                                    <th>Company</th>
                                    <th>Issue Date</th>
                                    <th>Due Date</th>
                                    <th>Status</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>Conflict Minerals</td>
                                    <td>Petrofac</td>
                                    <td><asp:Literal ID="litDateStarted" runat="server" /></td>   
                                    <td><asp:Literal ID="litDueDate" runat="server" /></td>
                                    <td><asp:Literal ID="litProgress" runat="server"  Text="In progress (75%)" /></td>
                                    <td>
                                        <asp:HyperLink ID="hypCertification" runat="server" CssClass="btn btn-small btn-primary" NavigateUrl="#" Text="Go" /></td>
                                  
                                </tr>

                            </tbody>
                           

                        </table>

                </asp:Panel>
               
                <asp:Panel runat="server" ID="panAllActionsDashbaord">
                    <div class="row-fluid">
                     <div class="span12">
                       
                        <table class="table table-condensed table-bordered">
                            <caption><span class="label label-inverse pull-left"><asp:Literal ID="litActions" runat="server" Text="My actions [All] - Actions relating to your companies" /></span></caption>
                            <thead>
                                <tr>
                                    <th>Action Title</th>
                                    <th>Company</th>
                                    <th>Date</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptUnapproved" runat="server">
                                    <ItemTemplate>
                                        <tr class="info">
                                            <td><asp:Literal ID="litDescription" runat="server" /></td>
                                            <td><asp:LinkButton ID="btnCompanyName" runat="server" /></td>
                                            <td><asp:Literal ID="litDateCreated" runat="server" /></td>
                                            <td><asp:LinkButton ID="btnAction" runat="server" Text="View" CssClass="btn btn-small btn-primary" /></td>
                                        </tr>
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
                                            OffsetY="-50" PopDelay="50" />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                         <div class="pagination pagination-mini">
                            <ul>
    <li><a href="#">Prev</a></li>
    <li><a href="#">1</a></li>
    <li><a href="#">2</a></li>
    <li><a href="#">3</a></li>
    <li><a href="#">4</a></li>
    <li><a href="#">5</a></li>
    <li><a href="#">Next</a></li>
  </ul>
                            </div>
                    </div>
                      
                    </div>
                    <div class="row-fluid">
                        <div class="span6">
                        <table class="table table-condensed table-bordered">
                                <caption><span class="label label-inverse pull-left"><asp:Literal ID="litSupplierActions" runat="server" Text="Companies who supply to my companies [All]" /></span></caption>
                            <thead>
                                <tr>
                                    <th>Action Title</th>
                                    <th>Company</th>
                                    <th>Date</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptUnapprovedSuppliers" runat="server">
                                    <ItemTemplate>
                                        <tr class="info">
                                            <td>
                                                <asp:Literal ID="litDescription" runat="server" />
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="btnCompanyName" runat="server" />
                                            </td>
                                            <td>
                                                <asp:Literal ID="litDateCreated" runat="server" />
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="btnAction" runat="server" Text="View" CssClass="btn btn-small btn-primary" />
                                            </td>
                                        </tr>
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
                                            PopupControlID="panPopUp" HoverCssClass="popupHover" PopupPosition="Top" OffsetX="0"
                                            OffsetY="0" PopDelay="50" />
                                        </ItemTemplate>
                                </asp:Repeater>
                             </tbody>
                           

                        </table>
                            <div class="pagination pagination-mini">
                            <ul>
    <li><a href="#">Prev</a></li>
    <li><a href="#">1</a></li>
    <li><a href="#">2</a></li>
    <li><a href="#">3</a></li>
    <li><a href="#">4</a></li>
    <li><a href="#">5</a></li>
    <li><a href="#">Next</a></li>
  </ul>
                            </div>
                    </div>
                    <div class="span6">
                       
                        <table class="table table-condensed table-bordered">
                            <caption><span class="label label-inverse pull-left"><asp:Literal ID="litCustomerActions" runat="server" Text="Companies my companies supply to [All]" /></span></caption>
                            <thead>
                                <tr>
                                    <th>Action Title</th>
                                    <th>Company</th>
                                    <th>Date</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptUnapprovedCustomers" runat="server">
                                    <ItemTemplate>
                                        <tr class="info">
                                            <td>
                                                <asp:Literal ID="litDescription" runat="server" />
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="btnCompanyName" runat="server" />
                                            </td>
                                            <td>
                                                <asp:Literal ID="litDateCreated" runat="server" />
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="btnAction" runat="server" Text="View" CssClass="btn btn-small btn-primary" />
                                            </td>
                                        </tr>
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
                                            PopupControlID="panPopUp" HoverCssClass="popupHover" PopupPosition="Top" OffsetX="0"
                                            OffsetY="0" PopDelay="50" />
                                    </ItemTemplate>
                                </asp:Repeater>
                                 
                            </tbody>
                           

                        </table>
                        <div class="pagination pagination-mini">
                            <ul>
    <li><a href="#">Prev</a></li>
    <li><a href="#">1</a></li>
    <li><a href="#">2</a></li>
    <li><a href="#">3</a></li>
    <li><a href="#">4</a></li>
    <li><a href="#">5</a></li>
    <li><a href="#">Next</a></li>
  </ul>
                            </div>
                    </div>
                    </div>
                   
                    
                    
                </asp:Panel>
                
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
                                        <li><asp:LinkButton ID="btnCompanyName" runat="server" />&nbsp;<asp:label ID="lblStatus" runat="server" /></li>
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
                                                PopupControlID="panPopUp" HoverCssClass="popupHover" PopupPosition="Top" OffsetX="0"
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
                                        <asp:LinkButton ID="btnCompanyName" runat="server" OnClick="ApplyForCustomer" />
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
                                        PopupControlID="panPopUp" HoverCssClass="popupHover" PopupPosition="Top" OffsetX="0"
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
                    </div>
                </asp:Panel>

                <asp:Panel ID="panSuppliers" runat="server">
                     <div class="span6">
                        <h4><asp:Label runat="server" ID="lblCompanySuppliers" /> Suppliers:</h4>

                            <ul>
                                <asp:Repeater ID="rptSuppliers" runat="server">
                                    <ItemTemplate>
                                        <li>
                                            <asp:LinkButton ID="btnCompanyName" runat="server" />&nbsp;<asp:Label ID="lblStatus" runat="server" /></li>
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
                                            PopupControlID="panPopUp" HoverCssClass="popupHover" PopupPosition="Top" OffsetX="0"
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
                                        <asp:LinkButton ID="btnCompanyName" runat="server" OnClick="ApplyForSupplier" />
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
                                        PopupControlID="panPopUp" HoverCssClass="popupHover" PopupPosition="Top" OffsetX="0"
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
                        </div>
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

