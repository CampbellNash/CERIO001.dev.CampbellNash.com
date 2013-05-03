Option Strict Off
Option Explicit On
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  Page Title       : login.aspx.vb                     '
'  Description      : Sign the user in                  ' 
'  Author           : Brian McAulay                     '
'  Creation Date    : 23 Mar 2013                       '
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
Partial Class login
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'TODO: If anything relies on this page then place it here

        End If
    End Sub


    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        'We can try to log our user in now
        Dim loginDetails As DataSet = NashBLL.LoginUser(txtUserName.Text, txtPassword.Text)
        Dim dr As DataRow = loginDetails.Tables(0).Rows(0)
        Select Case CInt(dr("Result"))
            Case 0
                'We can login this user now
                Session("UserLoggedIn") = True
                Session("FirstName") = dr("FirstName")
                Session("ContactID") = CInt(dr("ContactID"))
                Session("Surname") = dr("Surname")
                Session("EmailAddress") = dr("EmailAddress")
                'Now we can redirect to the home page
                Response.Redirect("~/mycerico.aspx")
            Case -1
                'Incorrect user name
                lblWarning.Text = "<br />Incorrect username or password, please check your details and try again [1]"
            Case -2
                lblWarning.Text = "<br />Incorrect username or password, please check your details and try again [2]"
            Case Else
                'Incorrect password
                lblWarning.Text = "<br />You have not verified your account, please check for your verification mail or contact us for more help [3]"
        End Select

    End Sub
End Class
