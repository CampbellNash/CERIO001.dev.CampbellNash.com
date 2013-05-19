<%@ Page Title="" Language="VB" MasterPageFile="~/masterpages/templatefull.master" AutoEventWireup="false" CodeFile="mycustomers.aspx.vb" Inherits="mycustomers" %>
<%@ Register src="controls/submenu1.ascx" tagname="submenu1" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpcMainContent" runat="Server">
 
            <div class="span9" runat="server" id="divMain" >
             
                  <h2>Manage Customers</h2>
                <asp:Label runat="server" ID="lblManageCompaniesPageTitle" />
                 <asp:Label ID="lblNoCompanies" runat="server" CssClass="label-nodata" EnableViewState="false" /> <asp:Label runat="server" ID="lblNoCompaniesHelp" />
             
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
                   <!--
                  <p>To manage your customers please select one of your companies from the table below:</p>
                        
                   
                        
                        <table class="table table-bordered">
                            <thead>
                                <tr>
                                    <th>My Companies</th>
                                    <th>Number of Customers</th>
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
                              </p>
                      -->
                            
                     </asp:Panel>
                

                

                
                <hr />

               
                
               

                <asp:Panel ID="panCustomers" runat="server">
                    <div class="span12">
                        <div class="alert alert-success" runat="server" id="divCustomerRequesrSuccess">
                            <button type="button" class="close" data-dismiss="alert">&times;</button>
                            <h3>Supplier request successful!</h3>
                            <p>You have successfully requested to be a supplier, when the customer involved accepts you will be notified.</p>
                        </div>
                        <h4>Companies <span class="text-info"><asp:Label runat="server" ID="lblCompanyCustomers" /></span> supplies to</h4>
                        <div  runat="server" id="tblCustomers">
                           <table class="table table-bordered">
                            <thead>
                                <tr>
                                    <th>Customer name</th>
                                    <th>System status</th>
                                    <th>Your Status (against this company)</th>
                                    <th>Your Compliance Status (against this company)</th>
                                    <th>Portal Link</th>
                                </tr>
                            </thead>
                            <tbody>
                                
                                <asp:Repeater ID="rptCustomers" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><asp:LinkButton ID="btnCompanyName" runat="server" /></td>
                                            <td><asp:label ID="lblStatus" runat="server" /></td> 
                                            <td><asp:label ID="lblYourStatus" runat="server" Text="Connected" /></td>
                                            <td><asp:label ID="lblComplianceStatus" runat="server" Text="Non Compliant" /></td>
                                            <td><asp:HyperLink ID="hypPortalLink" runat="server" CssClass="btn btn-success" /></td>
                                        </tr>
                                        <asp:Panel CssClass="popover" ID="panPopup" runat="server">
                                            <div class="popover-content">
                                                <h5>
                                                    Quick Veiw for <asp:Literal ID="litCompanyName" runat="server" /></h5>
                                                <asp:Image ID="imgCompanyLogo" runat="server" ImageUrl="img/apple-touch-icon-144-precomposed.png" />
                                               <h6><asp:Literal ID="litCompanyAddress" runat="server" /></h6>
                                            </div>
                                        </asp:Panel>
                                            <AjaxToolkit:HoverMenuExtender ID="hme2" runat="Server" TargetControlID="btnCompanyName"
                                                PopupControlID="panPopUp" HoverCssClass="popupHover" PopupPosition="Right" OffsetX="0"
                                                OffsetY="0" PopDelay="50" />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                               </table>
                        </div>
                        <p>
                            <asp:Label ID="lblNoCustomers" runat="server" CssClass="failureNotification" EnableViewState="false" />
                        </p>
                        <p>
                            <asp:Button ID="btnAddCustomer" runat="server" Text="Apply to be a Supplier to a customer" CssClass="btn btn-success" />
                        </p>

                    </div>
                    
                </asp:Panel>

                <asp:Panel runat="server" ID="panApplyCustomer" Visible="False" DefaultButton="btnSearchCustomerCompany">
                    <div class="span6">
                    <asp:Button ID="btnCancelApplyCustomer" runat="server" CssClass="btn-small btn-danger" Text="Cancel" CausesValidation="False" />
                    <h4>Apply to be a Customer</h4>
                    <p>The customer you are looking to supply to may already exist on our system, please use the search feature below to find them.</p>
                    Customer search: <asp:TextBox ID="txtSearchCustomerCompany" runat="server" CssClass="form-search search-query" placeholder="Search..." TabIndex="1" /> <asp:Button ID="btnSearchCustomerCompany" runat="server" ValidationGroup="search"  CssClass="btn-small btn-warning" Text="Search" /><br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="search"
                        ControlToValidate="txtSearchCustomerCompany" CssClass="error" ForeColor="red"
                        runat="server" Display="Dynamic" ErrorMessage="Please enter a search term"></asp:RequiredFieldValidator>
                       <br />
                        <div class="alert alert-success" runat="server" id="divSearchResults">
                       <ul class="unstyled">
                            <asp:Repeater ID="rptCustomerSearch" runat="server">
                                <HeaderTemplate> <h4>Supplier Search Results</h4>
                                    If the company you are looking for is listed below, just click on the name to apply to be a supplier to them. You can also hover over their name to view more details.<br /><br /></HeaderTemplate>
                                <ItemTemplate>
                                    <li>
                                        <span class="btn btn-success"><asp:LinkButton ID="btnCompanyName" runat="server" OnClick="ApplyForCustomer" /></span><br /><br />
                                    </li>
                                    <asp:Panel CssClass="popover" ID="panPopup" runat="server">
                                        <div class="popover-content">
                                            <h5>Quick Veiw for <asp:Literal ID="litCompanyName" runat="server" />  </h5>
                                            <asp:Image ID="imgCompanyLogo" runat="server" ImageUrl="img/apple-touch-icon-144-precomposed.png" />
                                           
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
                        </div>
                        <asp:Panel ID="panNoResults2" runat="server" Visible="false">
                            <br />
                            <div class="alert">
                                <button type="button" class="close" data-dismiss="alert">&times;</button>

                                <h4>Search results</h4>
                                No results found for the search you entered, please adjust and try again.</div>    
                           
                        </asp:Panel>
                        <asp:Panel ID="panTooManyRecords2" runat="server" Visible="false">
                            <p>
                                Too many records found, please narrow your search and try again.</p>
                        </asp:Panel>
                        </div>
                </asp:Panel>


                
                
                
        </div>
    <Telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <Telerik:AjaxSetting AjaxControlID="divMain">
                <UpdatedControls>
                    <Telerik:AjaxUpdatedControl ControlID="divMain" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelRenderMode="Block" />
                </UpdatedControls>
            </Telerik:AjaxSetting>
           <Telerik:AjaxSetting AjaxControlID="RadAjaxPanel1">
                <UpdatedControls>
                    <Telerik:AjaxUpdatedControl ControlID="RadAjaxPanel1" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelRenderMode="Block" />
                </UpdatedControls>
            </Telerik:AjaxSetting>
              
          
        </AjaxSettings>
    </Telerik:RadAjaxManager>
    <Telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="MetroTouch" Transparency="0" IsSticky="False" />          
    
    
    <div class="span3">
        <asp:Panel runat="server" ID="panSubNav">
            <uc1:submenu1 ID="submenu11" runat="server" />      
        </asp:Panel>
    </div>
        
    

</asp:Content>

