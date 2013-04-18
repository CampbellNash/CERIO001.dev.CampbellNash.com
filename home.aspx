<%@ Page Title="" Language="VB" MasterPageFile="~/masterpages/templatefull.master" AutoEventWireup="false" CodeFile="home.aspx.vb" Inherits="home" %>
<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=7.0.13.220, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
<%@ Register src="controls/submenu1.ascx" tagname="submenu1" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpcMainContent" Runat="Server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="span9">
                <h1>Welcome to CERICO <em><asp:Label runat="server" ID="lblFirstname"></asp:Label></em>, you are now logged in</h1>
               
                <asp:Label runat="server" ID="lblFullname"></asp:Label>, your ContactID is  <asp:Label runat="server" ID="lblContactId"></asp:Label>
                <h2>Reporting dashboard</h2>
                <Telerik:ReportViewer runat="server"></Telerik:ReportViewer>
            </div>
            
            <div class="span3">
                
                <uc1:submenu1 ID="submenu11" runat="server" />
                
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

