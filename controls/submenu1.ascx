<%@ Control Language="VB" AutoEventWireup="false" CodeFile="submenu1.ascx.vb" Inherits="controls_submenu1" %>
<div class="well sidebar-nav">
                    <ul class="nav nav-list">
                        <li class="nav-header">Main Navigation</li>
                        <li class="active"><a href="home.aspx">Your home page</a></li>
                        <li><a href="mycompanies.aspx">Manage my companies</a></li>
                        <li><a href="#">Manage my data</a></li>
                       
                    </ul>
                    
                    <ul class="nav nav-list">
                        <li class="nav-header">My Task list</li>
                        <li class="active"><asp:HyperLink ID="hypQuestionnaire" runat="server" NavigateUrl="~/standardquestionnaire.aspx?ci=2" Text="Complete Conflict Minerals" /></li>
                        <li><a href="#">Link 2</a></li>
                        <li><a href="#">Link 3</a></li>
                        <li><a href="#">Link 4</a></li>
                        <li><a href="#">Link 5</a></li>
 </ul>
    <h4>Click<a href="#"> here</a> to view all your tasks</h4>
   
</div>
<ul class="nav nav-tabs">
                      <li class="active">
<a href="#">Assigned Tasks</a>
</li>
<li>
<a href="#">Sent Tasks</a>
</li>
                      <li class="dropdown">
                        <a class="dropdown-toggle"
                           data-toggle="dropdown"
                           href="#">
                            Dropdown
                            <b class="caret"></b>
                          </a>
                        <ul class="dropdown-menu">
                          <li><a href="#">action 1</a></li>
                          <li><a href="#">action 2</a></li>
                          <li><a href="#">action 3</a></li>
                        </ul>
                      </li>
                    </ul>
<div class="well sidebar-nav">
    <ul class="nav nav-list">                    
    <li class="nav-header">My Latest events</li>
                        <li>- Invited Supplier <em>Campbell Nash</em> has registerd </li>
                        <li>- event 2</li>
                        <li>- event 3</li>
                        <li>- event 4</li>
                        <li>- event 5</li>
                      

                    </ul>

</div>
<div class="well sidebar-nav">
    <ul class="nav nav-list">     
    <li class="nav-header">My Latest news</li>
                        <li><a href="#">Headline 1</a></li>
                        <li><a href="#">Headline 2</a></li>
                        <li><a href="#">Headline 3</a></li>
    </ul>
        </div>
<!--/.well -->