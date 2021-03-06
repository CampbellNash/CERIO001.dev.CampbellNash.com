﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="supplieractions.aspx.vb" Inherits="supplieractions" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="~/css/bootstrap.min.css" rel="stylesheet" media="screen" />
        
    <title>CERICO Compliance and Due Diligence</title>
    <script type="text/javascript">

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow)
                oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog       
            else if (window.frameElement.radWindow)
                oWindow = window.frameElement.radWindow; //IE (and Moz as well)       
            return oWindow;
        }

        function WindowClose() {
            GetRadWindow().Close();
        }
     
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <Telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </Telerik:RadScriptManager>
    <Telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
    <div>
       <asp:Panel ID="panCloseWindow" runat="server" Visible="false">
            <script type="text/javascript">
                WindowClose();
            </script>
        </asp:Panel>
    
        <asp:Panel ID="panStaff" runat="server" Visible="false">
            <h4>Please see below for further action</h4>
              <div class="popover-content">
                    <h4>Details for new staff member</h4>
                    <h5>
                        <asp:Literal ID="litStaffName" runat="server" Text="Brian McAulay" />
                    </h5>
                    <h5>
                        <asp:Literal ID="litStaffDetails" runat="server" Text="6A Darroch Way<br>Glasgow<br>G67 1PZ<br>brian@zungalow.com" /></h5>
                    <p><asp:LinkButton ID="btnApprove" runat="server" Text="Approve" CssClass="btn btn-info" />&nbsp;&nbsp;<asp:LinkButton
                            ID="btnDecline" runat="server" Text="Decline" CssClass="btn btn-danger" /></p>
                </div>
            
        </asp:Panel>

        <asp:Panel ID="panSuccess" runat="server" Visible="false">
            <p>You request has been completed.</p>
            <p>Click <a href="JavaScript:WindowClose();">Here</a> to close this window to complete the process.</p>
        </asp:Panel>
    
    </div>
    </Telerik:RadAjaxPanel>
    
    <Telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <Telerik:AjaxSetting AjaxControlID="RadAjaxPanel1">
                <UpdatedControls>
                    <Telerik:AjaxUpdatedControl ControlID="RadAjaxPanel1" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelRenderMode="Block" />
                </UpdatedControls>
            </Telerik:AjaxSetting>
        </AjaxSettings>
    </Telerik:RadAjaxManager>
    <Telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="MetroTouch"
        Transparency="0" IsSticky="False" />
  </form>
</body>
</html>
