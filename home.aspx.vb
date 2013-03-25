Option Strict Off
Option Explicit On
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  Page Title       : home.aspx.vb                      '
'  Description      : Home page of logged in user       ' 
'  Author           : Stephen Davidson                  '
'  Creation Date    : 25 Mar 2013                       '
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

Partial Class home
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Session("UserLoggedIn") Then
            'User is not logged in so send to login page
            Response.Redirect("~/login.aspx")
        End If
        If Not IsPostBack Then
            'lets add the user's firstname
            lblFirstname.Text = Session("FirstName")
            lblFullname.Text = Session("Firstname") & " " & Session("Surname")
            lblContactId.Text = Session("ContactID")
           
        End If
    End Sub

End Class
