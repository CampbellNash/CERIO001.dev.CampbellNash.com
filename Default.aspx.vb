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
Partial Class _Default
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
                panWarning.Visible = True
                lblWarning.Text = "Incorrect username or password, please check your details and try again [1]"
            Case -2
                panWarning.Visible = True
                lblWarning.Text = "Incorrect username or password, please check your details and try again [2]"
            Case Else
                'Incorrect password
                panWarning.Visible = True
                lblWarning.Text = "You have not verified your account, please check for your verification mail or contact us for more help [3]"
        End Select

    End Sub

    Protected Sub btnForgot_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnForgot.Click
        'User requesting to be sent their username and password
        lblWarning.Text = ""
        panFormWrapper.Visible = False
        panForgot.Visible = True
        txtEmailAddress.Text = ""
    End Sub

    Protected Sub btnCancelForgot_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelForgot.Click, btnReturn.Click
        'User is cancelling password request
        panFormWrapper.Visible = True
        panForgot.Visible = False
        lblLoginError.Text = ""
        panWarning.Visible = False
        panNotActivated.Visible = False
    End Sub

    Protected Sub btnSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSend.Click
        Dim MailBody As String = ""
        Dim Subject As String = ""
        Dim RecipientList As String = ""
        'Try
        Dim UserData As String = NashBLL.SendUserAndPassword(txtEmailAddress.Text)
        If UserData = "-1" Then
            'This account has not been verified yet so halt and advise
            panForgot.Visible = False
            panNotActivated.Visible = True
            Return
        ElseIf UserData = "-2" Then
            'Account was not found
            lblLoginError.Text = "Sorry we were unable to find an account matching the email address you entered. Please verify &amp; try again."
            Return
        Else
            Dim MyArray As Array = Split(UserData, ",")
            'We found a user for the entered email address so we can send the mail now
            MailBody = "Dear " & MyArray(2) & "," & vbCrLf & vbLf & _
            "Please find your login details for our site below." & vbCrLf & vbLf & _
            "Username: " & MyArray(0) & vbCrLf & _
            "Password: " & MyArray(1) & vbCrLf & vbLf & _
            "Visit 'web site address here' and login with these details." & vbCrLf & vbLf & _
            "If you have any further problems then please consult your support team."
            Subject = "Login details from CERICO"
            RecipientList = MyArray(0)
            'Now send the mail
            NashBLL.SendMail(RecipientList, "", "", MailBody, Subject, "", False)
            'Inform the user now
            lblLoginError.Text = ""
            panForgot.Visible = False
            panConfirmSend.Visible = True
        End If

        'Catch

        'End Try
    End Sub

End Class