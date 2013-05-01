Option Strict Off
Option Explicit On
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  Page Title       : register.aspx.vb                  '
'  Description      : Registers a new user              ' 
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
Partial Class register
    Inherits System.Web.UI.Page

#Region " User Defined Functions "

    Private Function GenerateCode() As String
        Dim CodeString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim CodeGen As String = ""
        Randomize()
        For LoopCount = 1 To 10
            CodeGen &= Mid(CodeString, Rnd() * 35 + 1, 1)
        Next
        GenerateCode = CodeGen
    End Function

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Populate our dropdown lists
            Dim Titles As DataSet = NashBLL.GetUserTitles
            cboTitle.DataValueField = "TitleID"
            cboTitle.DataTextField = "Title"
            cboTitle.DataSource = Titles
            cboTitle.DataBind()
            Dim NewItem As New ListItem With {
            .Text = "--- Please Select ---",
            .Value = ""}
            cboTitle.Items.Insert(0, NewItem)
            
        End If

        
    End Sub

    Protected Sub btnRegister_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegister.Click
        'Now register our user
        Dim VerificationCode As String = GenerateCode()
        Dim ContentURL As String = ""
        Dim ServerName As String = Request.ServerVariables("SERVER_NAME")
        Dim ServerPort As String = Request.ServerVariables("SERVER_PORT")
        Dim ApplicationName As String = ""
        Dim ApplicationArray As Array
        'The script name starts with a '/' char and contains the application name, any folder names and finally the actual page name
        'we need to remove the initial '/' char and then split into an array separated by the other '/' chars
        'and then accept the first array item as the application/folder name for our content url string
        'so we go right for the full length of the string minus 1
        Dim ScriptName As String = Right(Request.ServerVariables("SCRIPT_NAME"), Request.ServerVariables("SCRIPT_NAME").Length - 1)
        ApplicationArray = Split(ScriptName, "/")
        ApplicationName = ApplicationArray(0)
        If InStr(ApplicationName, ".aspx") Then
            'Makes sure if its a domain named server that the application name is not the page name 
            ApplicationName = ""
        End If
        ContentURL &= Page.Request.Url.Scheme & "://" & ServerName
        If CInt(ServerPort) <> 80 Then
            'Include the port if it's not normal 80
            ContentURL &= ":" & ServerPort
        End If
        If ApplicationName <> "" Then
            ContentURL &= "/" & ApplicationName
        End If
        ContentURL &= "/verify.aspx?v=" & VerificationCode

        'Now attempt to add our user
        Dim Result As Integer = NashBLL.AddNewUser(cboTitle.SelectedValue, _
                                                    txtFirstName.Text, _
                                                    txtSurname.Text, _
                                                    txtEmailAddress.Text, _
                                                    txtRegisterUserName.Text, _
                                                    txtRegisterPassword.Text, _
                                                    VerificationCode)
        Select Case Result
            'See what result we got back from SQL
            Case -3
                'SQL transaction failed
                lblResult.Text = "There was some unexpected error.<br /><br />Please contact us quoting Ref:- " & VerificationCode
            Case -2
                'User has entered an email address that is already in use
                lblResult.Text = "Username is already in use, please amend your details and try again!"
            Case -1
                'User has entered a username that is already in use
                lblResult.Text = "Duplicate email address, please amend your details and try again!"
            Case Else
                'Everything was OK so we can send the mail and confirm the new user panel
                Dim MailBody As String = ""
                MailBody = "Dear " & txtFirstName.Text & "," & vbCrLf & vbLf & _
                        "Thank you for registering with Cerico!" & vbCrLf & vbLf & _
                        "In order to complete your registration, please click on the link below, or copy & paste it into a browser window." & vbCrLf & vbLf & _
                        ContentURL & vbCrLf & vbLf & _
                        "Thank you," & vbCrLf & vbLf & _
                        "Cerico Admin Team."

                Dim MailResult As Integer = NashBLL.SendMail(txtEmailAddress.Text, "", "", MailBody, "Registration confirmation from Cerico", "", False)
                Select Case MailResult
                    Case -1
                        'Some kind of trouble with the mail service so advise
                        lblResult.Text = " Your registration was successful, but the mail we needed to send you has not been sent.<br /><br />Please contact us quoting Ref: - " & Result
                    Case Else
                        'Registration process was completed successfully so we can advise the user
                        litReference.Text = Result
                        panMain.Visible = False
                        panSuccess.Visible = True
                End Select
        End Select
    End Sub
End Class
