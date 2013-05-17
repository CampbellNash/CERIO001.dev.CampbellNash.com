<%@ Page Language="VB" AutoEventWireup="false" CodeFile="supplieractions.aspx.vb" Inherits="supplieractions" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
    
    <title>CERICO Supplier Management</title>
    <script type="text/javascript">

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow)
                oWindow = window.radWindow;
            else if (window.frameElement.radWindow)
                oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function Close() {
            var arg = new Object();
            var oWnd = GetRadWindow();
            oWnd.close(arg);
            
        }
     
    </script>
</head>
<body>
    <form id="form1" runat="server">
    
    
 
    <div>
       <asp:Panel ID="panMain" runat="server" Visible="True">
            <div class="container-fluid">
                <div class="row-fluid">
                    <div class="span12">
                       <h4 runat="server" id="placeholder">
                            My Cerico – Manage My Companies</h4>
                        <script type="text/javascript">
                            Close();
                        </script>
                     </div>
                </div>
            </div>
            </asp:Panel>
            
        </div>
    </form>
</body>
</html>
