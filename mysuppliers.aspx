<%@ Page Title="" Language="VB" MasterPageFile="~/masterpages/templatefull.master" AutoEventWireup="false" CodeFile="mysuppliers.aspx.vb" Inherits="mysuppliers" %>
<%@ Register src="controls/submenu1.ascx" tagname="submenu1" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpcMainContent" Runat="Server">
   
  
          <Telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
            <div class="span9">
                <h2>Manage Suppliers</h2>
                <asp:Label runat="server" ID="lblManageCompaniesPageTitle" Visible="false" />
                <asp:Panel ID="panMyCompanies" runat="server">
                
                  <div class="alert alert-info">
                    <div class="form-horizontal">
                      <div class="control-group">
                      <label class="control-label">Select from your companies:</label>
                                <div class="controls">
                                        <asp:DropDownList runat="server" ID="cboCompanies" AutoPostBack="true" OnSelectedIndexChanged="GetMyRelationShipDropDown" />
                                        
                                </div>
                        </div>
                        </div>
                </div>      
                <!-- Now using a drop down menu to select your companies
               <p>To manage your supplier's please select one of your companies from the table below:</p>
                <table class="table table-bordered">
                            <thead>
                                <tr>
                                    <th>My Companies</th>
                                    <th>Number of Suppliers</th>
                                    <th>Status</th>
                                 </tr>
                            </thead>
                            <tbody>
                            <asp:Repeater ID="rptMyCompanies" runat="server" OnItemDataBound="BindCompanies">
                                <ItemTemplate>
                                   <tr>
                                       <td><asp:LinkButton ID="btnCompanyName" runat="server" OnClick="GetMyRelationships" /></td> 
                                       <td><asp:Label ID="lblTotalSuppliers" runat="server" Text="2" /></td>
                                       <td><asp:Label ID="lblStatus" runat="server" /></td>
                                       
                                   </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            </tbody>
                        </table>
                   <div class="pagination">
                            <ul>
    <li><a href="#">Prev</a></li>
    <li><a href="#">1</a></li>
    <li><a href="#">2</a></li>
   
    <li><a href="#">Next</a></li>
  </ul>
                            </div>
                        <p>
                            <asp:Label ID="lblNoCompanies" runat="server" CssClass="label-nodata" EnableViewState="false" /> <asp:Label runat="server" ID="lblNoCompaniesHelp" />
                        </p>
                      -->
                     </asp:Panel>
                <hr />

              <asp:Panel ID="panSuppliers" runat="server">
                     <div class="span9">
                          
                        <h4>Suppliers to <asp:Label runat="server" ID="lblCompanySuppliers" /></h4>
                            <div runat="server" id="divSuppliers">
                            <table class="table table-bordered">
                            <thead>
                                <tr>
                                    <th>Supplier Name</th>
                                    
                                    <th>System Status</th>
                                    <th>Compliance Status (against selected company)</th>
                                    <th>Actions</th>
                                    
                                    
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptSuppliers" runat="server" OnItemDataBound="BindSuppliers">
                                    <ItemTemplate>
                                        <tr>
                                            <td><asp:LinkButton ID="btnCompanyName" runat="server"  /></td>
                                            <td>
                                            <asp:Label ID="lblStatus" runat="server" />
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
                                                </td>
                                            <td>
                                                <span class="label label-important">Non Compliant</span>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="btnSupplierDetails" runat="server" CssClass="btn btn-mini btn-primary" Text="View details" OnClick="GetSupplierDetails" />
                                               
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                                 </table>
                         <h4>Want to add more suppliers? Click the button below.</h4>
                         </div>
                        <p>
                            <asp:Label ID="lblNoSuppliers" runat="server" CssClass="failureNotification" EnableViewState="false" />
                        </p>
                        <p>
                            <asp:Button ID="btnAddSupplier" runat="server" Text="Add Supplier" CssClass="btn btn-success pull-left gapright" />
                        </p>
                            <asp:Button ID="btnCancelAddSupplier" runat="server" Text="Cancel" CssClass="btn btn-danger pull-left" Visible="false" />
                    </div>
                </asp:Panel>
                
              <asp:Panel ID="panAddSupplier" runat="server" DefaultButton="btnSearchSuppliers">
                    <div class="span9">
                        <asp:Button ID="btnCancelSearch" runat="server" CssClass="btn-small btn-danger" Text="Cancel Search" CausesValidation="False"  />
                        <h4>Add a new supplier</h4> 
                        <p>The supplier you are looking for may already exist on our system, please use the search feature below to find them.</p>
                        Supplier search: <asp:TextBox ID="txtSupplierSearch" runat="server" CssClass="form-search search-query" placeholder="Search..." TabIndex="1" /> 
                        <asp:Button ID="btnSearchSuppliers" runat="server" ValidationGroup="search"  CssClass="btn-small btn-warning" Text="Search" /><br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="search"
                            ControlToValidate="txtSupplierSearch" CssClass="error" ForeColor="red"
                            runat="server" Display="Dynamic" ErrorMessage="Please enter a search term" />
                        <ul class="unstyled">
                            <asp:Repeater ID="rptSupplierSearch" runat="server">
                                <HeaderTemplate><h4>Supplier Search Results</h4></HeaderTemplate>
                                <ItemTemplate>
                                    <li>
                                       <span class="btn btn-success"><asp:LinkButton ID="btnCompanyName" runat="server" OnClick="ApplyForSupplier"  /></span>
                                    </li>
                                    <asp:Panel CssClass="popover" ID="panPopup" runat="server">
                                        <div class="popover-content">
                                             <h5>Quick Veiw for <asp:Literal ID="litCompanyName" runat="server" /></h5>
                                            <asp:Image ID="imgCompanyLogo" runat="server" ImageUrl="img/apple-touch-icon-144-precomposed.png" />
                                               <h6>
                                                <asp:Literal ID="litCompanyAddress" runat="server" /></h6>
                                        </div>
                                    </asp:Panel>
                                    <AjaxToolkit:HoverMenuExtender ID="HoverMenuExtender2" runat="Server" TargetControlID="btnCompanyName"
                                        PopupControlID="panPopUp" HoverCssClass="popupHover" PopupPosition="Right" OffsetX="0"
                                        OffsetY="0" PopDelay="50" />
                                </ItemTemplate>
                                <FooterTemplate>
                                  
                                </FooterTemplate>
                            </asp:Repeater>
                        </ul>
                          <br />
                                    <div class="alert alert-info">
                                <button type="button" class="close" data-dismiss="alert">&times;</button>
                                <h4>Can't find the supplier your looking for?</h4>
                                If you cannot find the supplier you wish to add then you can invite them to the system by clicking the "Invite Supplier" button. In doing so they will be automatically connected to you once they setup their account.<br />
                                <asp:LinkButton ID="btnInviteSuppliers" runat="server" CssClass="btn-small btn-success pull-left" Text="Invite Supplier &raquo;" />
                                
                                <br />
                            </div>
                        <ajaxToolkit:ModalPopupExtender ID="MPE1" runat="server"
                        TargetControlID="btnInviteSuppliers"
                        PopupControlID="panInviteSupplier"
                        BackgroundCssClass="modal-backdrop" 
                        DropShadow="true" 
                        OkControlID="OkButton" 
                        OnOkScript="onOk()"
                        PopupDragHandleControlID="Panel3" >
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:Panel ID="panInviteSupplier" runat="server" CssClass="modal-body">
                                <legend><asp:LinkButton ID="btnClosePopUp" runat="server" CssClass="btn btn-danger pull-right" Text="Close Invite" />Invite Supplier</legend>
                                
                                <asp:Panel ID="panInviteStart" runat="server" Visible="true">
                                    Please enter the supplier details below and click the submit button. Items marked with <span class="alert-error">* </span> are required.
                                    <fieldset>
                                        
                                    <label><span class="alert-error">* </span> Firstname:</label><asp:TextBox ID="txtSupplierFirstname" CssClass="input-large" runat="server" />
                                                 <asp:RequiredFieldValidator ID="rfvSupplier1" runat="server" ControlToValidate="txtSupplierFirstname"
                                                    CssClass="alert-error" ErrorMessage="First Name is required." ToolTip="First Name is required."
                                                    ValidationGroup="InviteSupplier" Display="Dynamic" />
                                    <label><span class="alert-error">* </span> Surname:</label><asp:TextBox ID="txtSupplierSurname" CssClass="input-large" runat="server" />
                                                 <asp:RequiredFieldValidator ID="rfvSupplier2" runat="server" ControlToValidate="txtSupplierSurname"
                                                CssClass="alert-error" ErrorMessage="Surname is required." ToolTip="Surname is required."
                                                ValidationGroup="InviteSupplier" Display="Dynamic" />
                                    <label><span class="alert-error">* </span> Email:</label><asp:TextBox ID="txtSupplierEmailAddress" CssClass="input-xxlarge" runat="server" />
                                                 <asp:RequiredFieldValidator ID="rfvSupplier3" runat="server" ControlToValidate="txtSupplierEmailAddress"
                                                     CssClass="alert-error" ErrorMessage="Email address is required." ToolTip="Email address is required."
                                                     ValidationGroup="InviteSupplier" Display="Dynamic" />
                                                 <asp:RegularExpressionValidator ID="rfvSupplier4" runat="server" ControlToValidate="txtSupplierEmailAddress"
                                                     ErrorMessage="Please enter a valid email" ValidationGroup="InviteSupplier"
                                                     ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="alert-error" Display="Dynamic" />
                                                 <br />
                                                <label><span class="alert-error">* </span>Company name:</label><asp:TextBox ID="txtSupplierCompanyName" CssClass="input-xlarge" runat="server" />
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSupplierCompanyName"
                                                    CssClass="alert-error" ErrorMessage="Company Name is required." ToolTip="Company Name is required."
                                                    ValidationGroup="InviteSupplier" Display="Dynamic" />
                                    <asp:LinkButton ID="btnInvite" runat="server" CssClass="btn btn-success pull-right" ValidationGroup="InviteSupplier" Text="Send Invite &raquo;" />
                                        </fieldset>
                                        
                                </asp:Panel>
                                
                                <asp:Panel ID="panInviteSent" runat="server" Visible="false">
                                    <p>Invite has been sent to the company being invited.</p>    
                                </asp:Panel>

                                <asp:Panel ID="panUserExists" runat="server" Visible="false">
                                    <p>User is already a registered user and has been invited to add their company.</p>
                                </asp:Panel>
                            
                            </asp:Panel>
                        
                        <asp:Panel ID="panNoResults3" runat="server" Visible="false">
                            
                            <div class="alert">
                                <button type="button" class="close" data-dismiss="alert">&times;</button>
                                <h4>Search results</h4>
                                No results found for the search you entered, please adjust and try again.
                            </div>    
                            
                              
                             
    
                
                          
                             
                       
                        </asp:Panel>
                        <asp:Panel ID="panTooManyRecords3" runat="server" Visible="false">
                            <p>
                                Too many records found, please narrow your search and try again.</p>
                        </asp:Panel>
                       </div>
                </asp:Panel>
               
              <asp:Panel ID="panSupplierDetails" runat="server" Visible="false">
                    
                        <div class="span12">
                            <hr />
                            <h4 ><asp:Label runat="server" ID="lblCompanyNameDetail" /> Details</h4>
                            <div class="tabbable">
                              <ul class="nav nav-tabs">
                                <li class="active"><a href="#pane1" data-toggle="tab"> Company Details</a></li>
                                <li><a href="#pane2" data-toggle="tab">Company Users</a></li>
                                <li><a href="#pane3" data-toggle="tab">Company Certifcations</a></li>
                                
                              </ul>
                              <div class="tab-content">
                                <div id="pane1" class="tab-pane active">
                                    <dl class="dl-horizontal">
                                      <dt>Business Area:</dt>
                                      <dd><asp:literal runat="server" ID="litBusinessAreaDetail" /></dd>
                                        <dt>test</dt>
                                        <dd>test</dd>
                                    </dl>
                                 
                                </div>
                                <div id="pane2" class="tab-pane">
                                <h4>Company Users for company name</h4>
                                  <asp:Repeater ID="rptStaffMembers" runat="server" />
                                </div>
                                <div id="pane3" class="tab-pane">
                                  <h4>Certifcation status</h4>
                                    <table class="table table-bordered table-condensed">
                                        <thead>
                                            <tr>
                                                <th>Certification name</th>
                                                <th>Mandatory</th>
                                                <th>Issue Date</th>
                                                <th>Due date</th>
                                                <th>Status</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>Conflict Minerals</td>
                                                <td>
                                                    <div class="onoffswitch">
    <input type="checkbox" name="onoffswitch" class="onoffswitch-checkbox" id="myonoffswitch" checked>
    <label class="onoffswitch-label" for="myonoffswitch">
        <div class="onoffswitch-inner"></div>
        <div class="onoffswitch-switch"></div>
    </label>
</div>

                                                </td>
                                                <td>12 May 2013</td>
                                                <td>12 Jun 2013</td>
                                                <td>
                                                 In Progress (33%)
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                
                              </div><!-- /.tab-content -->
                            </div><!-- /.tabbable -->
                        </div>
                        
                    
                    
                </asp:Panel>
              
        </div>
       
            <div class="span3">
            <asp:Panel runat="server" ID="panSubNav">
                <uc1:submenu1 ID="submenu11" runat="server" />      
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
    
    
        
</asp:Content>

