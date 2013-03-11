Option Strict Off
Option Explicit On
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  Page Title       : Eastern.aspx.vb                   '
'  Description      : Chinese/Japanese lang test        ' 
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
Public Class Eastern
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Session("UserLoggedIn") Then
            'User is not logged in so send to login page
            Response.Redirect("~/Account/login.aspx")
        End If
        If Not IsPostBack Then
            'Go and get our data first
            Dim EasternChars As DataSet = NashBLL.GetEasternText
            Dim dr As DataRow = EasternChars.Tables(0).Rows(0)
            txtTestString.Text = dr("TestString")
            txtTestPassword.Attributes.Add("Value", dr("TestPassword"))
            txtTestBlob.Text = dr("TestBlob")
        End If
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
        Dim Result As Integer = NashBLL.UpdateEasternText(txtTestString.Text, txtTestPassword.Text, txtTestBlob.Text)
        'Now update the literals from the DB using the data we just inserted
        Dim EasternChars As DataSet = NashBLL.GetEasternText
        Dim dr As DataRow = EasternChars.Tables(0).Rows(0)
        litTestString.Text = dr("TestString")
        litTestPassword.Text = dr("TestPassword")
        litTestBlob.Text = Replace(dr("TestBlob"), vbCr, "<br />")
    End Sub
End Class