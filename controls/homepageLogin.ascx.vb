Option Strict Off
Option Explicit On
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  Page Title       : homepageLogin.ascx.vb             '
'  Description      : Login user control                ' 
'  Author           : Stephen Davidson                  '
'  Creation Date    : 03 May 2013                       '
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

Partial Class controls_homepageLogin
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'let checkt to see if the user is logged in and hide the login panel if required.
        If Not IsPostBack Then
            If Not Session("UserLoggedIn") Then
                panFormWrapper.Visible = True
            Else
                panFormWrapper.Visible = False
            End If
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

    Protected Sub btnCancelForgot_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelForgot.Click, btnReturn.Click, btnCancel2.Click, btnCancel3.Click
        'User is cancelling password request
        panFormWrapper.Visible = True
        panForgot.Visible = False
        lblLoginError.Text = ""
        panWarning.Visible = False
        panNotActivated.Visible = False
        panConfirmSend.Visible = False
    End Sub

    Protected Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click
        Dim MailBody As String = ""
        Dim Subject As String = ""
        Dim RecipientList As String = ""
        Try
            Dim EmailResult As String = NashBLL.SendUserAndPassword(txtEmailAddress.Text)
            If EmailResult = "-1" Then
                'This account has not been verified yet so halt and advise
                panForgot.Visible = False
                panNotActivated.Visible = True
                lblLoginError.Text = ""
                Return
            ElseIf EmailResult = "-2" Then
                'Account was not found
                lblLoginError.Text = "Sorry we were unable to find an account matching the email address you entered. Please verify &amp; try again."

                Return
            Else
                Dim MyArray As Array = Split(EmailResult, ",")
                'We found a user for the entered email address so we can send the mail now
                MailBody = "Dear " & MyArray(3) & "," & vbCrLf & vbLf & _
                "Please find your login details for our site below." & vbCrLf & vbLf & _
                "Username: " & MyArray(1) & vbCrLf & vbLf & _
                "Password: " & MyArray(2) & vbCrLf & vbLf & _
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

        Catch
            lblLoginError.Visible = True
            lblLoginError.Text = "There has been an error - " & ErrorToString()
        End Try
    End Sub


End Class
