Option Strict Off
Option Explicit On
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  Page Title       : supplieractions.aspx.vb           '
'  Description      : Manages & approves items in my    '
'                     cerico page                       '
'  Author           : Brian McAulay                     '
'  Creation Date    : 17 May 2013                       '
'  Version No       : 1                                 '
'  Revision         :                                   '
'  Revision Reason  :              		                '
'  Revisor          :            		  			    '
'  Date Revised     :                     			    '  
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Imports System
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Net
Imports System.IO
Imports System.Configuration
Imports System.Net.Mail
Imports Microsoft.VisualBasic
Imports MasterClass
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Security
Partial Class supplieractions
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Session("UserLoggedIn") Then
            panMain.Visible = False
            panCloseWindow.Visible = True
            Return
        End If
        If Not IsPostBack Then
            panMain.Visible = True
        End If
    End Sub
End Class
