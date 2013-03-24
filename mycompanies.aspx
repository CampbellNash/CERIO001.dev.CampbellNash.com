<%@ Page Title="" Language="VB" MasterPageFile="~/masterpages/templatefull.master" AutoEventWireup="false" CodeFile="mycompanies.aspx.vb" Inherits="mycompanies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpcMainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:LinkButton ID="btTest" runat="server" Text="Test Postback" /><br />
            <asp:Literal ID="litTest" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <span class="span9">
        <h2>This is the page title</h2>
        <h3>We can have a sub title</h3>
        <p>And this is the main copy</p>
    </span>

    <span class="span3">
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

    </span>





</asp:Content>

