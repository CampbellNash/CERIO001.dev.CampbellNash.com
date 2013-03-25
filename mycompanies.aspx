<%@ Page Title="" Language="VB" MasterPageFile="~/masterpages/templatefull.master" AutoEventWireup="false" CodeFile="mycompanies.aspx.vb" Inherits="mycompanies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpcMainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="span9">
                <h2>My Companies</h2>
                <h3>Manage My Companies</h3>
                <div style="width: auto; border-style: double; padding: 20px;">
                    <asp:Panel ID="panMyCompanies" runat="server">

                        <p>The list below shows the list of companies that you are resonsible for.</p>
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
                            <asp:Label ID="lblNoCompanies" runat="server" CssClass="label-warning" />
                        </p>
                        <p>
                            <asp:Button ID="btnAddCompany" runat="server" Text="Add Company" CssClass="btn" />
                        </p>


                    </asp:Panel>
                <asp:Panel ID="panAddCompany" runat="server" Visible="false">
                   
                        <p>Use the form below to add your company.</p>
                        <div class="form-signin">
                            <label>Title:</label>
                            <asp:DropDownList ID="cboTitles" runat="server" />
                            <p>
                                <asp:Button ID="btnAddNewCompany" runat="server" CssClass="btn" Text="Add New Company" />&nbsp;&nbsp;<asp:Button ID="btnCancelAdd" runat="server" CssClass="btn" Text="Cancel" />
                            </p>
                        </div>
                </asp:Panel>

                </div>
                <hr />
                <asp:Panel ID="panCustomers" runat="server">
                    <div style="float: left; margin-left: 50px; width: 450px; border-style: double; padding: 20px;">
                        My Customers:
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
                    <div style="float: right; margin-right: 550px; width: 450px; border-style: double; padding:20px;">
                        My Suppliers:

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

