<%@ Page Title="" Language="VB" MasterPageFile="~/masterpages/templatefull.master" AutoEventWireup="false" CodeFile="mycerico.aspx.vb" Inherits="mycerico" %>
<%@ Register src="controls/submenu1.ascx" tagname="submenu1" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpcMainContent" runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
	
    <script type="text/javascript">
        $(function () {

            // Radialize the colors
            Highcharts.getOptions().colors = Highcharts.map(Highcharts.getOptions().colors, function (color) {
                return {
                    radialGradient: { cx: 0.5, cy: 0.3, r: 0.7 },
                    stops: [
                        [0, color],
                        [1, Highcharts.Color(color).brighten(-0.3).get('rgb')] // darken
                    ]
                };
            });

            // Build the chart
            $('#container').highcharts({
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false
                },
                title: {
                    text: 'Supplier Certication status'
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage}%</b>',
                    percentageDecimals: 1
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            color: '#000000',
                            connectorColor: '#000000',
                            formatter: function () {
                                return '<b>' + this.point.name + '</b>: ' + this.percentage + ' %';
                            }
                        }
                    }
                },
                series: [{
                    type: 'pie',
                    name: 'Certified Suppliers',
                    data: [
                        ['Completed', 45.0],
                       
                        {
                            name: 'In progress',
                            y: 30.0,
                            sliced: true,
                            selected: true
                        },
                        ['Not Started', 25.0],
                      
                    ]
                }]
            });

            $('#container2').highcharts({
                chart: {
                },
                title: {
                    text: 'Combination chart'
                },
                xAxis: {
                    categories: ['Apples', 'Oranges', 'Pears', 'Bananas', 'Plums']
                },
                tooltip: {
                    formatter: function () {
                        var s;
                        if (this.point.name) { // the pie chart
                            s = '' +
                                this.point.name + ': ' + this.y + ' fruits';
                        } else {
                            s = '' +
                                this.x + ': ' + this.y;
                        }
                        return s;
                    }
                },
                labels: {
                    items: [{
                        html: 'Total fruit consumption',
                        style: {
                            left: '40px',
                            top: '8px',
                            color: 'black'
                        }
                    }]
                },
                series: [{
                    type: 'column',
                    name: 'Jane',
                    data: [3, 2, 1, 3, 4]
                }, {
                    type: 'column',
                    name: 'John',
                    data: [2, 3, 5, 7, 6]
                }, {
                    type: 'column',
                    name: 'Joe',
                    data: [4, 3, 3, 9, 0]
                }, {
                    type: 'spline',
                    name: 'Average',
                    data: [3, 2.67, 3, 6.33, 3.33],
                    marker: {
                        lineWidth: 2,
                        lineColor: Highcharts.getOptions().colors[3],
                        fillColor: 'white'
                    }
                }, {
                    type: 'pie',
                    name: 'Total consumption',
                    data: [{
                        name: 'Jane',
                        y: 13,
                        color: Highcharts.getOptions().colors[0] // Jane's color
                    }, {
                        name: 'John',
                        y: 23,
                        color: Highcharts.getOptions().colors[1] // John's color
                    }, {
                        name: 'Joe',
                        y: 19,
                        color: Highcharts.getOptions().colors[2] // Joe's color
                    }],
                    center: [100, 80],
                    size: 100,
                    showInLegend: false,
                    dataLabels: {
                        enabled: false
                    }
                }]
            });
        });


		</script> 
   <script type="text/javascript">
       function onRequestStart(sender, args) {
           if (args.get_eventTarget().indexOf("Button1") >= 0)
               args.set_enableAjax(false);
           if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0 ||
                   args.get_eventTarget().indexOf("ExportToWordButton") >= 0 ||
                   args.get_eventTarget().indexOf("ExportToPdfButton") >= 0) {
               args.set_enableAjax(false);
           }
       }
    </script>
        <Telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
            <div class="span9">
              <asp:Panel ID="panMyCompanies" runat="server">
                  <div class="span7">
                      <asp:Button ID="btnAddCompany" runat="server" Text="Start Company Association process &raquo;" CssClass="btn btn-success pull-right" /> 
 <h2>My CERICO</h2>
                  <h3><asp:Label runat="server" ID="lblManageCompaniesPageTitle" /></h3>
                        
                        <p><b>The list below shows the list of companies that you are responsible for. Click on the Company to view more details.</b></p>
                        <table class="table table-bordered">
                            <thead>
                                <tr>
                                    <th>Company Name - Status</th>
                                   
                                    <th colspan="3">
                                        Suppliers
                                       
                                    </th>

                                    <th colspan="3">Customers</th>
                                   
                                    <th>Actions</th>
                                    
                                </tr>
                            </thead>
                            <tbody>
                            <asp:Repeater ID="rptMyCompanies" runat="server" OnItemDataBound="BindCompanies">
                                <ItemTemplate>
                                    <tr>
                                        <td><asp:LinkButton ID="btnCompanyName" runat="server" OnClick="GetMyRelationships" /> - <asp:Label ID="lblStatus" runat="server" /></td>
                                        <td><asp:Label ID="lblTotalSuppliers" runat="server" Text="2" /></td>
                                        <td><asp:Label ID="lblApprovedSuppliers" runat="server" Text="1" CssClass="label label-success" /></td>
                                        <td><asp:Label ID="lblUnapprovedSuppliers" runat="server" Text="1" CssClass="label" /></td>
                                        <td><asp:Label ID="lblTotalCustomers" runat="server" Text="1" /></td>
                                        <td><asp:Label ID="lblApprovedCustomers" runat="server" Text="1" CssClass="label label-success" /></td>
                                        <td><asp:Label ID="lblUnapprovedCustomers" runat="server" Text="1" CssClass="label" /></td>
                                        <td><asp:LinkButton ID="btnViewApproved" runat="server" CssClass="btn btn-small">View Details</asp:LinkButton> <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-small">View Certifications</asp:LinkButton></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            </tbody>
                       </table>
                        <p>
                            <asp:Label ID="lblNoCompanies" runat="server" CssClass="label-nodata" EnableViewState="false" /> <asp:Label runat="server" ID="lblNoCompaniesHelp" />
                        </p>
                        <p>
                            If you wish to associate yourself with another company then click the "Start Company Association process" button.
                        </p>

                  </div>
                  <div class="span5">
                         
                     
                       <div class="pull-right">
                     
                      <div id="container" style="min-width: 500px; height: 200px; margin: 0 auto"></div>
                           </div>
                  </div>
                     
                 
                       
                            
                     </asp:Panel>
                

                <asp:Panel runat="server" ID="panSearchCompanies" Visible="False" DefaultButton="btnSearch">
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
                <asp:Panel ID="panCompanyCertification" runat="server" Visible="false">
                    <asp:Button ID="btnCancelCompanyCert" runat="server" Text="Back" CssClass="btn btn-danger pull-right" />
                    <h3><asp:Literal ID="litCompanyRef" runat="server" /></h3>
                    <table class="table table-condensed">
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
                                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-small btn-primary" NavigateUrl="#">GO</asp:HyperLink></td>
                                  
                                </tr>

                            </tbody>
                           

                        </table>

                </asp:Panel>
                <hr />
                <asp:Panel runat="server" ID="panAllActionsDashbaord">
                    <div class="row-fluid">
                     <div class="span12">
                        <h4>My actions [All] - Actions relating to your companies</h4>
                        <table class="table table-condensed">
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
                                        <tr class="success">
                                            <td><asp:Literal ID="litDescription" runat="server" /></td>
                                            <td><asp:LinkButton ID="btnCompanyName" runat="server" /></td>
                                            <td><asp:Literal ID="litDateCreated" runat="server" /></td>
                                            <td><asp:LinkButton ID="btnAction" runat="server" Text="View" CssClass="btn btn-small btn-primary" /></td>
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
                        <h4>My Supplier Actions [All]</h4>
                            <table class="table table-bordered">
                            <thead>
                                <tr>
                                    <th>Action Title</th>
                                    <th>Company</th>
                                    <th>Date</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr class="warning">
                                    <td>Et inum vivem ortes auscepo straet</td>
                                    <td>Campbell Nash</td>
                                    <td>31/10/203</td>
                                    
                                    <td><asp:LinkButton ID="LinkButton3" runat="server" Text="Click here" CssClass="btn btn-small btn-primary" /></td>
                                </tr>
                                 <tr class="warning">
                                  <td>Et inum vivem ortes auscepo straet</td>
                                    <td>Campbell Nash</td>
                                    <td>31/10/203</td>
                                    
                                    <td><asp:LinkButton ID="LinkButton4" runat="server" Text="Click here" CssClass="btn btn-small btn-primary" /></td>
                                </tr>
                                 <tr class="warning">
                                   <td>Et inum vivem ortes auscepo straet</td>
                                    <td>Campbell Nash</td>
                                    <td>31/10/203</td>
                                    
                                    <td><asp:LinkButton ID="LinkButton5" runat="server" Text="Click here" CssClass="btn btn-small btn-primary" /></td>
                                </tr>
                                <tr class="info">
                                  <td>Et inum vivem ortes auscepo straet</td>
                                    <td>Campbell Nash</td>
                                    <td>31/10/203</td>
                                    
                                    <td><asp:LinkButton ID="LinkButton6" runat="server" Text="Click here" CssClass="btn btn-small btn-primary" /></td>
                                </tr>
                                <tr class="info">
                                   <td>Et inum vivem ortes auscepo straet</td>
                                    <td>Campbell Nash</td>
                                    <td>31/10/203</td>
                                    
                                    <td><asp:LinkButton ID="LinkButton7" runat="server" Text="Click here" CssClass="btn btn-small btn-primary" /></td>
                                </tr>
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
                        <h4>My Customer Actions [All]</h4>
                        <table class="table table-bordered">
                            <thead>
                                <tr>
                                    <th>Action Title</th>
                                    <th>Company</th>
                                    <th>Status</th>
                                    <th>Date</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr class="success">
                                    <td>Et inum vivem ortes auscepo straet</td>
                                    <td>Petrofac</td>
                                    <td>In Progress</td>
                                    <td>31/10/203</td>
                                    <td><asp:LinkButton ID="LinkButton8" runat="server" Text="Click here" CssClass="btn btn-small btn-primary" /></td>
                                </tr>
                                 <tr class="warning">
                                   <td>Et inum vivem ortes auscepo straet</td>
                                    <td>Petrofac</td>
                                    <td>In Progress</td>
                                    <td>31/10/203</td>
                                    <td><asp:LinkButton ID="LinkButton9" runat="server" Text="Click here" CssClass="btn btn-small btn-primary" /></td>
                                </tr>
                                 <tr class="error">
                                   <td>Et inum vivem ortes auscepo straet</td>
                                    <td>Petrofac</td>
                                    <td>In Progress</td>
                                    <td>31/10/203</td>
                                    <td><asp:LinkButton ID="LinkButton10" runat="server" Text="Click here" CssClass="btn btn-small btn-primary" /></td>
                                </tr>
                                <tr class="error">
                                 <td>Et inum vivem ortes auscepo straet</td>
                                    <td>Petrofac</td>
                                    <td>In Progress</td>
                                    <td>31/10/203</td>
                                    <td><asp:LinkButton ID="LinkButton11" runat="server" Text="Click here" CssClass="btn btn-small btn-primary" /></td>
                                </tr>
                                <tr class="info">
                                   <td>Et inum vivem ortes auscepo straet</td>
                                    <td>Petrofac</td>
                                    <td>In Progress</td>
                                    <td>31/10/203</td>
                                    <td><asp:LinkButton ID="LinkButton12" runat="server" Text="Click here" CssClass="btn btn-small btn-primary" /></td>
                                </tr>
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

