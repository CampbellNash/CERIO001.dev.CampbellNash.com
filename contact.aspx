<%@ Page Title="" Language="VB" MasterPageFile="~/masterpages/landing.master" AutoEventWireup="false" CodeFile="contact.aspx.vb" Inherits="contact" %>

<%@ Register src="controls/homepageLogin.ascx" tagname="homepageLogin" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpcHeroLeft" Runat="Server">
     <h1>Contact CERICO</h1>
    <h3>a dueDILIGENCE solution</h3>
       
            
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphHeroRight" Runat="Server">

    <uc1:homepageLogin ID="homepageLogin1" runat="server" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphLowerSection" Runat="Server">
    
    <h2>Get in touch</h2>
   <p>
For more information on the new
       CORICO Compliance Framework
    </p>
    <p>
        email:
<a href="mailto:tom.stocker@pinsentmasons.com">tom.stocker@pinsentmasons.com</a></p>
    <p>
        <b>Princes Exchange 1 Earl Grey Street Edinburgh EH3 9AQ</b></p>
    <p>
        or
    </p>
    <p>
        email:
        <a href="mailto:james.armstron@campbellnash.com">james.armstron@campbellnash.com</a></p>
    <p>
&nbsp;<b>The Pentagon Centre
Washington Street
Glasgow G3 8AZ
    
    </b> </p>
    <p>
        &nbsp;</p>
</asp:Content>

