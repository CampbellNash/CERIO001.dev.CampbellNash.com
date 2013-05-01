Option Strict Off
Option Explicit On
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  Page Title       : verify.aspx.vb                    '
'  Description      : Verifies a new user               ' 
'  Author           : Brian McAulay                     '
'  Creation Date    : 01 May 2013                       '
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
Partial Class verify
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Result As Integer
        If Not IsPostBack Then
            If Request.QueryString("v") = "" Then
                lblErrorMessage.Text = "<P>Sorry no verification code was supplied, please contact us for more information."
                Exit Sub
            End If
            Result = NashBLL.VerifyUser(Request.QueryString("v"))
            'Now check our verification result
            Select Case Result
                Case 0
                    panMain.Visible = True
                Case -1
                    lblErrorMessage.Text = "<P>Sorry this code has already been used.  You may have already clicked the verification link or previously approved your account, before contacting us please try to login as normal. If you continue to have problems then please contact us.</P>"
                Case -2
                    lblErrorMessage.Text = "<P>Sorry no such code has been issued, please contact us for more information.</P>"
            End Select
        End If
    End Sub
End Class
