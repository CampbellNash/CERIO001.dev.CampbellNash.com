<%@ Page Title="" Language="VB" MasterPageFile="~/masterpages/templatefull.master" AutoEventWireup="false" CodeFile="template.aspx.vb" Inherits="template" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpcMainContent" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="span9">
                <h1>This is a h1 page title</h1>
                
            </div>
            
            <div class="span3">
                <div class="well sidebar-nav">
                    <ul class="nav nav-list">
                        <li class="nav-header">a list of pages</li>
                        <li class="active"><a href="#">Template</a></li>
                        <li><a href="register.aspx">Register</a></li>
                        <li><a href="mycompanies.aspx">My Companies</a></li>
                    </ul>
                </div>
                <!--/.well -->
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

