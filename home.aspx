﻿<%@ Page Title="" Language="VB" MasterPageFile="~/masterpages/templatefull.master" AutoEventWireup="false" CodeFile="home.aspx.vb" Inherits="home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpcMainContent" Runat="Server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="span9">
                <h1>Welcome to CERIO <em><asp:Label runat="server" ID="lblFirstname"></asp:Label></em>, you are now logged in</h1>
                
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
