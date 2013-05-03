<%@ Page Title="" Language="VB" MasterPageFile="~/masterpages/landing.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register src="controls/homepageLogin.ascx" tagname="homepageLogin" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpcHeroLeft" Runat="Server">
     <h1>CERICO / A Compliance and Due Diligence solution</h1>
            <p>Ensuring compliance with UK and International Anti-Bribery and Corruption legislation can be a challenge. In particular, you need to be sure that your supply chain is compliant with your company policies</p>
            <p><a href="#" class="btn btn-info btn-large">Learn more &raquo;</a></p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphHeroRight" Runat="Server">
     <uc1:homepageLogin ID="homepageLogin1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphLowerSection" Runat="Server">
     <div class="row-fluid">
            <div class="span4">
              <h2>Introducing CERICO</h2>
              <p>Ensuring compliance with UK and International Anti-Bribery and Corruption legislation can be a challenge. In particular, you need to be sure that your supply chain is compliant with your company policies. </p>
                <p>This is where Pinsent Masons CERICO comes in. It gives you a process for vetting new and existing suppliers and, in turn, their suppliers.</p>
              <p><a class="btn" href="default.aspx#Introducing">Find out more &raquo;</a></p>
            </div><!--/span-->
            <div class="span4">
              <h2>How it works</h2>
              <p>When you start to do business with a new supplier, the system requires the purchasing department and the supplier to complete questionnaires to check compliance with your company policies. </p>
                <p>In addition, it allows the purchasing department to upload the results of third party checks, such as Factiva and incorporate these into the overall checks for reputational risk..</p>
              <p><a class="btn" href="Default.aspx#How">Find out more&nbsp;&raquo;</a></p>
            </div><!--/span-->
            <div class="span4">
              <h2>Features &amp; Benefits</h2>
              <p>Key Features</p>
                <ul><li>Import commercial research data such as Dow Jones’ Factiva or other research results</li>
                <li>Lock results in an auditable archive</li>
                    </ul>
                <p>Business benefits</p>
                <ul style="padding: 0px; margin: 0px 0px 10px 25px; color: rgb(51, 51, 51); font-family: 'Open Sans', sans-serif; font-size: 14px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: 20px; orphans: auto; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: auto; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">
                    <li style="line-height: 20px;">Apply best practice due diligence to all supplier relationships</li>
                    <li style="line-height: 20px;">Translate policies into concrete action</li>
                </ul>
              <p><a class="btn" href="#">View details &raquo;</a></p>
            </div><!--/span-->
          </div><!--/row-->
      <div class="row-fluid">
            <div class="span4">
              <h2>Industry Endorsed</h2>
              <p>“<i>We are accelerating the deployment of the Campbell Nash Due Diligence portal, as the creation of a dedicated compliance function and delivering the highest standards of transparency and integrity is the way forward. In the Oil &amp; Gas industry in which we operate, the sector faces unprecedented risk, legal and compliance challenges both in highly competitive andregulated markets and under-regulated countries.</i>” <b>says Marcelo Cardoso, group Head of Compliance at Petrofac</b></p>
              <p><a class="btn" href="#">View details &raquo;</a></p>
            </div><!--/span-->
            <div class="span4">
              <h2>Why CERICO?</h2>
              <p>The Act applies to ALL UK companies, irrespective of size or the sector in which they operate, and any international companies with a business in the UK. On the international front adding to the due diligence burden are new US regulations required product manufacturers and suppliers to carry out due diligigence on their supply chain, to avoid purchasing “conflict materials”.</p>
              <p><a class="btn" href="#">Full details &raquo;</a></p>
            </div><!--/span-->
            <div class="span4">
              <h2>What can I do on CERICO?</h2>
              <p>It forms part of a wider compliance service endowed with strong vetting procedures helping clients and their suppliers - at times thousands in number - to meet the challenges of supply chain compliance, whilst preserving their good name and reputation - so vital in these still highly uncertain economic times when competitition for new business remains fi erce.</p>
              <p><a class="btn" href="#">View details &raquo;</a></p>
            </div><!--/span-->
          </div><!--/row-->
    <div class="row-fluid">
        <div class="span12">
            <div class="gap"></div>
        </div>
    </div>
    <div class="row-fluid">
        <div class="span12">
            <section id="Introducing">
            <h2>Introducing CERICO</h2>
            <p>Ensuring compliance with UK and International Anti-Bribery and
Corruption legislation can be a challenge.
In particular, you need to be sure that your supply chain is compliant with
your company policies. This is where Pinsent Masons CERICO comes in.
It gives you a process for vetting new and existing suppliers and, in turn,
their suppliers. </p>
            <p>It ensures consistent application of company policies
and keeps an auditable record of the results as a shield against future
problems.
When your company’s reputation is at risk, never mind the maximum
penalty of 10 years imprisonment under the Act, you can’t afford to take
a chance.</p>
            </section>
            
            

        </div>

    </div>
     <div class="row-fluid">
        <div class="span12">
            <div class="gap"></div>
        </div>
    </div>
    <div class="row-fluid">
        <div class="span12">
            <section id="How">
                <h2>How it Works</h2>
                <p>When you start to do business with a new supplier, the system requires
the purchasing department and the supplier to complete questionnaires
to check compliance with your company policies.
In addition, it allows the purchasing department to upload the results of
third party checks, such as Factiva and incorporate these into the overall
checks for reputational risk.
The questionnaires can be customised and they are designed to highlight
contradictory responses between in-house and supplier statements.
It is also possible to trigger customisable workfl ows based on the
questionnaire responses.</p>
                <p>For example, high-risk responses can trigger
requests for exemptions or further analysis.
The supplier’s responses can be made legally binding and part of the
contract with the supplier to enforce compliance. Once the questionnaires
have been completed, they are locked and uploaded to the system so
that they cannot be altered later. </p>
                <p>This prevents collusion and tampering
to provide a formal archive.</p>
            </section>

        </div>

    </div>
</asp:Content>

