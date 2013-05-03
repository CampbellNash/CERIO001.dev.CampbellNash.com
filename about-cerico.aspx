<%@ Page Title="" Language="VB" MasterPageFile="~/masterpages/landing.master" AutoEventWireup="false" CodeFile="about-CERICO.aspx.vb" Inherits="about_CERICO" %>

<%@ Register src="controls/homepageLogin.ascx" tagname="homepageLogin" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpcHeroLeft" Runat="Server">
     <h1>About CERICO</h1>
    <h3>a dueDILIGENCE solution</h3>
       
            <p><a href="#" class="btn btn-info btn-large">Another option &raquo;</a></p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphHeroRight" Runat="Server">
    
    <uc1:homepageLogin ID="homepageLogin1" runat="server" />
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphLowerSection" Runat="Server">
     <div class="row-fluid">
            <div class="span8">
                <h2> IT Due Diligence and Monitoring Packaged Electronic Framework Solution portal</h2>
    <p>PMCCERICO is a unique IT Due Diligence and Monitoring
Packaged Electronic Framework Solution portal that
comprehensively covers the vital area of GOVERNANCE/
COMPLIANCE, is GROUNDBREAKING in its concept, has
a GLOBAL REACH with an additional strength that it taps
into a perceived GAP IN THE MARKET. </p>
                <p>It is envisaged it
will concentrate initially on the Oil & Gas sector, and then
broaden out into other areas through time.</p>
                <h3>Technology</h3>
                <p>uses secure, reliable cloud-hosting technology from Microsoft. This means that you don’t need to install any additional hardware or software – no need to make work for the IT department. The system creates a Chinese wall between internal and external (supplier) users to ensure privacy. External users access the system using their own login credentials via the internet. They don’t need to access any of your company systems or have a company account to complete the questionnaire.</p>
                <p>&nbsp;</p>
                </div>
         <div class="span4">
             <h3>Business Benefits</h3>
             <ul>
                 <li>Apply best practice due diligence to all
supplier relationships</li>
                 <li>Translate policies into concrete action</li>
                 <li>Manage compliance and reputational risks</li>
                 <li>Company policy is reinforced and embedded</li>
                 <li>Set up a company-wide auditable archive to
demonstrate compliance</li>

             </ul>


         </div>
         </div>
</asp:Content>

