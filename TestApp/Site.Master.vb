Option Strict Off
Option Explicit On
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  Page Title       : site.master.aspx.vb               '
'  Description      : Manages our basic template        ' 
'  Author           : Brian McAulay                     '
'  Creation Date    : 06 Mar 2013                       '
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
Imports CNash.MasterClass
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Security
Public Class Site
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not Session("UserLoggedIn") Then
                'User is not logged in so show the login panels
                panLoggedIn.Visible = False
                panLogin.Visible = True
                NavigationMenu.Visible = False
            Else
                'User is logged in so show the logout panels
                panLoggedIn.Visible = True
                panLogin.Visible = False
                NavigationMenu.Visible = True
                LitHeadLoginName.Text = Session("FirstName")
            End If
        End If
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As System.EventArgs) Handles btnLogout.Click
        'Logs our user out
        Session.Abandon()
        'Now redirect to the login page
        Response.Redirect("~/Account/login.aspx")
    End Sub
End Class