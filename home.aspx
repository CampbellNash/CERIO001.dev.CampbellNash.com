<%@ Page Title="" Language="VB" MasterPageFile="~/masterpages/templatefull.master" AutoEventWireup="false" CodeFile="home.aspx.vb" Inherits="home" %>
<%@ Register src="controls/submenu1.ascx" tagname="submenu1" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpcMainContent" Runat="Server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="span9">
                <h1>Welcome to CERIO <em><asp:Label runat="server" ID="lblFirstname"></asp:Label></em>, you are now logged in</h1>
               
                <asp:Label runat="server" ID="lblFullname"></asp:Label>, your ContactID is  <asp:Label runat="server" ID="lblContactId"></asp:Label>

            </div>
            
            <div class="span3">
                
                <uc1:submenu1 ID="submenu11" runat="server" />
                
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

