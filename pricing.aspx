<%@ Page Title="" Language="VB" MasterPageFile="~/masterpages/landing.master" AutoEventWireup="false" CodeFile="pricing.aspx.vb" Inherits="pricing" %>

<%@ Register src="controls/homepageLogin.ascx" tagname="homepageLogin" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpcHeroLeft" Runat="Server">
      <h1>CERICO Pricing</h1>
           
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphHeroRight" Runat="Server">
    <uc1:homepageLogin ID="homepageLogin1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphLowerSection" Runat="Server">
    <h2>Pricing</h2>
</asp:Content>

