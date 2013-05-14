<%@ Control Language="VB" AutoEventWireup="false" CodeFile="submenu1.ascx.vb" Inherits="controls_submenu1" %>
<script type="text/javascript">
        //<![CDATA[
    function openRadWin(URL) {
        var oWindow = radopen(URL, "rwMyWindow");
        //oWindow.moveTo(400, 50); 
    }
    
    function RefreshPage(){
        window.location = 'mycerico.aspx';
    }
 //]]>                                                                        
</script>
<Telerik:RadWindowManager ID="UserWindowManager1" runat="server" Skin="Black">
    <Windows>
        <Telerik:RadWindow runat="server" ID="rwMyWindow" DestroyOnClose="false" Width="1000px"
            Height="800px" Modal="true" InitialBehaviors="Reload" VisibleStatusbar="false"
            Behaviors="Close" ReloadOnShow="true" OnClientClose="RefreshPage" />
    </Windows>
</Telerik:RadWindowManager>
<div class="well sidebar-nav">
                    <ul class="nav nav-list">
                        <li class="nav-header">Main Navigation</li>
                        <li class="active"><a href="../mycerico.aspx">My CERICO</a></li>
                        <li><a href="mycompanies.aspx">Manage my companies</a></li>
                        <li><a href="../MyDetails.aspx">Manage my Personal Details</a></li>
                        <li><a href="#">Invite Supplier</a></li>
                        <li><a href="#">Request a Customer</a></li>
                        <li></li>
                       
                    </ul>
                    
                    <ul class="nav nav-list">
                        <li class="nav-header">Latest Assigned Actions</li>
                        <li class="active"><asp:HyperLink ID="hypQuestionnaire" runat="server" NavigateUrl="JavaScript:openRadWin('questionnairepopup.aspx?ci=2');" Text="Complete Conflict Minerals with Petrofac" /></li>
                        <li><a href="#">Start Due Dilligence with Apple</a></li>
                        <li><a href="#">Respond to supplier request from Campbell Nash</a></li>
                        <li><a href="#">Respond to supplier request from Apple</a></li>
                        <li><a href="#">Upload your Company Logo</a></li>
 </ul>
    <h4>Click<a href="#"> here</a> to view all your tasks</h4>
   
</div>

<div class="well sidebar-nav">
    <ul class="nav nav-list">     
    <li class="nav-header">Your latest news</li>
                        <li><a href="#">Bullwood consultants has added a new member of staff</a></li>
                        <li><a href="#">Apple has updated their Due Dilligence Process</a></li>
                        <li><a href="#">Petrofac has updated their Conflict Minerals Questionnaire</a></li>
    </ul>
        </div>

<div class="well sidebar-nav">
    <ul class="nav nav-list">     
    <li class="nav-header">CERICO Latest news</li>
                        <li><a href="#">System update scheduled for 05/11/2013 at 02:00</a></li>
                        <li><a href="#">New Module added to e-training</a></li>
                        <li><a href="#">Review your company details</a></li>
    </ul>
        </div>
<!--/.well -->