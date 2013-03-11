<%@ Page Title="My Companies" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MyCompanies.aspx.vb" Inherits="CNash.MyCompanies" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>My Companies &amp; Contacts</h2>
    <p>Use the information below to explore &amp; manage your companies &amp; contacts.</p>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <fieldset>
                    <legend>My Companies:</legend>
                    <asp:Panel ID="panMyCompanies" runat="server">
                        <ul>
                            <asp:Repeater ID="rptMyCompanies" runat="server" OnItemDataBound="BindCompanies">
                                <ItemTemplate>
                                    <li>
                                        <asp:LinkButton ID="btnCompanyName" runat="server" OnClick="GetMyRelationships" /></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </asp:Panel>
                    <p>
                        <asp:Label ID="lblNoCompanies" runat="server" CssClass="failureNotification" /></p>
                    <p class="submitButton">
                        <asp:Button ID="btnAddCompany" runat="server" Text="Add Company" /></p>
                </fieldset>
            </div>
            <asp:Panel ID="panCustomers" runat="server">
                <div style="float:left; margin-left:50px; width:350px">
                    <fieldset>
                        <legend>My Customers:</legend>
                         <ul>
                            <asp:Repeater ID="rptCustomers" runat="server" OnItemDataBound="BindCompanies">
                                <ItemTemplate>
                                    <li><asp:LinkButton ID="btnCompanyName" runat="server" /></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                        <p><asp:Label ID="lblNoCustomers" runat="server" CssClass="failureNotification" /></p>
                        <p class="submitButton"><asp:Button ID="btnAddCustomer" runat="server" Text="Add Customer" /></p>
                    </fieldset>
                </div>
             </asp:Panel>
            <asp:Panel ID="panSuppliers" runat="server">
                <div style="float:right; margin-right:50px; width:350px">
                    <fieldset>
                        <legend>My Suppliers:</legend>
                    
                            <ul>
                                <asp:Repeater ID="rptSuppliers" runat="server" OnItemDataBound="BindCompanies">
                                    <ItemTemplate>
                                        <li>
                                            <asp:LinkButton ID="btnCompanyName" runat="server" /></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                    
                        <p><asp:Label ID="lblNoSuppliers" runat="server" CssClass="failureNotification" /></p>
                        <p class="submitButton"><asp:Button ID="btnAddSupplier" runat="server" Text="Add Supplier" /></p>
                    </fieldset>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
