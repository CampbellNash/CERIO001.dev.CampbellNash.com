<%@ Page Title="" Language="VB" MasterPageFile="~/masterpages/templatefull.master" AutoEventWireup="false" CodeFile="mysuppliers.aspx.vb" Inherits="mysuppliers" %>
<%@ Register src="controls/submenu1.ascx" tagname="submenu1" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpcMainContent" Runat="Server">
    <Telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
         
            <div class="span9">
                <h2>Manage Suppliers</h2>
                <asp:Label runat="server" ID="lblManageCompaniesPageTitle" />
              <asp:Panel ID="panMyCompanies" runat="server">
                     
                  
                  
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
                       
                       
                            
                     </asp:Panel>
                

                

                
                <hr />

               
                
                

                

                

                <asp:Panel ID="panSuppliers" runat="server">
                     <div class="span9">
                          
                        <h4><asp:Label runat="server" ID="lblCompanySuppliers" /> Suppliers:</h4>

                            <ul>
                                <asp:Repeater ID="rptSuppliers" runat="server" OnItemDataBound="BindCompanies">
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
                                            PopupControlID="panPopUp" HoverCssClass="popupHover" PopupPosition="Right" OffsetX="0"
                                            OffsetY="0" PopDelay="50" />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>

                        <p>
                            <asp:Label ID="lblNoSuppliers" runat="server" CssClass="failureNotification" EnableViewState="false" />
                        </p>
                        <p>
                            <asp:Button ID="btnAddSupplier" runat="server" Text="Add Supplier" CssClass="btn btn-success pull-left gapright" />
                        </p>
                            <asp:Button ID="btnCancelAddSupplier" runat="server" Text="Cancel" CssClass="btn btn-danger pull-left" />
                    </div>
                </asp:Panel>
                
                <asp:Panel ID="panAddSupplier" runat="server" DefaultButton="btnSearchSuppliers">
                    <div class="span9">
                        <asp:Button ID="btnCancelSearch" runat="server" CssClass="btn-small btn-danger" Text="Cancel" CausesValidation="False"  />
                        <h4>Add a new supplier</h4> 
                        <p>The supplier you are looking for may already exist on our system, please use the search feature below to find them.</p>
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
                                        PopupControlID="panPopUp" HoverCssClass="popupHover" PopupPosition="Right" OffsetX="0"
                                        OffsetY="0" PopDelay="50" />
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                        <asp:Panel ID="panNoResults3" runat="server" Visible="false">
                            
                            <div class="alert">
                                <button type="button" class="close" data-dismiss="alert">&times;</button>

                                <h4>Search results</h4>
                                No results found for the search you entered, please adjust and try again.</div>    
                            <div class="alert alert-info">
                                <button type="button" class="close" data-dismiss="alert">&times;</button>
                                <h4>The Supplier Does not exist!</h4>
                                If you cannot find the supplier you wish to add then you can invite them to the system by clicking the "Invite Supplier" button. In doing so they will be automatically connected to you once they setup thier account.<br />
                                <asp:LinkButton ID="btnInviteSuppliers" runat="server" CssClass="btn-small btn-success pull-left" Text="Invite Supplier &raquo;" />
                                
                                <br />
                            </div>
                              
                             <asp:Panel ID="panInviteSupplier" runat="server" CssClass="modal-body">
                    <asp:LinkButton ID="btnClosePopUp" runat="server" CssClass="btn btn-danger pull-right" Text="Close Invite" />
                    <h4>Invite Supplier</h4>
                    Please enter the supplier details below and click the submit button. <br />
                                 Items marked with <span class="alert-error">* </span> are required.
                    <label><span class="alert-error">* </span> Firstname:</label><asp:TextBox ID="txtSupplierFirstname" CssClass="input-large" runat="server" />
                                 <asp:RequiredFieldValidator ID="rfvSupplier1" runat="server" ControlToValidate="txtSupplierFirstname"
                                    CssClass="alert-error" ErrorMessage="First Name is required." ToolTip="First Name is required."
                                    ValidationGroup="InviteSupplier" Display="Dynamic" />
                    <label><span class="alert-error">* </span> Surname:</label><asp:TextBox ID="txtSupplierSurname" CssClass="input-large" runat="server" />
                                 <asp:RequiredFieldValidator ID="rfvSupplier2" runat="server" ControlToValidate="txtSupplierSurname"
                                CssClass="alert-error" ErrorMessage="Surname is required." ToolTip="Surname is required."
                                ValidationGroup="InviteSupplier" Display="Dynamic" />
                    <label><span class="alert-error">* </span> Email:</label><asp:TextBox ID="txtSupplierEmailAddress" CssClass="input-xlarge" runat="server" />
                                 <asp:RequiredFieldValidator ID="rfvSupplier3" runat="server" ControlToValidate="txtSupplierEmailAddress"
                                     CssClass="alert-error" ErrorMessage="Email address is required." ToolTip="Email address is required."
                                     ValidationGroup="InviteSupplier" Display="Dynamic" />
                                 <asp:RegularExpressionValidator ID="rfvSupplier4" runat="server" ControlToValidate="txtSupplierEmailAddress"
                                     ErrorMessage="Please enter a valid email" ValidationGroup="InviteSupplier"
                                     ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="alert-error" Display="Dynamic" />
                                 <br />
                    <asp:LinkButton ID="btnInvite" runat="server" CssClass="btn btn-success pull-right" ValidationGroup="InviteSupplier" Text="Send Invite &raquo;" />
                                
                </asp:Panel>
                 <ajaxToolkit:ModalPopupExtender ID="MPE" runat="server"
    TargetControlID="btnInviteSuppliers"
    PopupControlID="panInviteSupplier"
    BackgroundCssClass="modal-backdrop" 
    DropShadow="true" 
    OkControlID="OkButton" 
    OnOkScript="onOk()"
    CancelControlID="btnClosePopUp" 
    PopupDragHandleControlID="Panel3" ></ajaxToolkit:ModalPopupExtender>
                
                          
                             
                       
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

