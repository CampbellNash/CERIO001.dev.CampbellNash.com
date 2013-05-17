<%@ Page Language="VB" AutoEventWireup="false" CodeFile="supplieractions.aspx.vb" Inherits="supplieractions" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
    <link id="Link1" href="~/css/bootstrap.min.css" rel="stylesheet" media="screen" runat="server" />
    <link id="Link2" href="~/css/bootstrap-responsive.min.css" rel="stylesheet" media="screen"
        runat="server" />
    <title>CERICO Supplier Management</title>
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
    <asp:Panel ID="panMain" runat="server" Visible="true">
        <h4>Please see below for further action</h4>
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
    <Telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Telerik"
        Transparency="0" IsSticky="False" />
  </form>
</body>
</html>
