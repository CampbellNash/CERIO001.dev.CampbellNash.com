Option Strict Off
Option Explicit On
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  Page Title       : site.master.vb                    '
'  Description      : Controls items on the main pages  ' 
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
Partial Class masterpages_site
    Inherits System.Web.UI.MasterPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not Session("UserLoggedIn") Then
                btnGotoLogin.Visible = True
                btnLogout.Visible = False
                navloggedOut.Visible = True
                navloggedin.Visible = False
            Else
                'lets add the user's firstname
                hypHeaderUserFullName.Text = Session("Firstname") & " " & Session("Surname")
                hypHeaderUserFullName.NavigateUrl = "~/mydetails.aspx"
                panloggedInHeader.Visible = True
                btnLogout.Visible = True
                btnGotoLogin.Visible = False
                navloggedOut.Visible = False
                navloggedin.Visible = True
                'now lets manage the styles on the active page
                If System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath).ToLower = "default.aspx" Then
                    menuhome.Attributes.Add("class", "active")
                ElseIf System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath).ToLower = "about-cerico.aspx" Then
                    menuabout.Attributes.Add("class", "active")
                ElseIf System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath).ToLower = "cerico-features-benefits.aspx" Then
                    menufeatures.Attributes.Add("class", "active")
                ElseIf System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath).ToLower = "pricing.aspx" Then
                    menupricing.Attributes.Add("class", "active")
                ElseIf System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath).ToLower = "contact.aspx" Then
                    menucontact.Attributes.Add("class", "active")
                End If
                'now lets manage the styles on the active page when logged in
                If System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath).ToLower = "mycerico.aspx" Then
                    menumycerico.Attributes.Add("class", "active")
                ElseIf System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath).ToLower = "mysuppliers.aspx" Then
                    menusuppliers.Attributes.Add("class", "active")
                ElseIf System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath).ToLower = "mycustomers.aspx" Then
                    menucustomers.Attributes.Add("class", "active")
                End If
            End If
        End If
    End Sub

    Protected Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        'Logs our user out
        Session.Abandon()
        Response.Redirect("~/default.aspx")
    End Sub
End Class

