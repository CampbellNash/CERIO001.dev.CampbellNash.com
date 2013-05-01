Option Strict Off
Option Explicit On
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  Page Title       : mydetails.aspx.vb                 '
'  Description      : Account details page              ' 
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
Imports MasterClass
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Security

Partial Class mydetails
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


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Session("UserLoggedIn") Then
            'User is not logged in so send to login page
            Response.Redirect("~/login.aspx")
        End If
        If Not IsPostBack Then
            'Populate our form dropdowns starting with the titles
            cboTitle.DataSource = NashBLL.GetUserTitles
            cboTitle.DataTextField = "Title"
            cboTitle.DataValueField = "TitleID"
            cboTitle.DataBind()
            
            Dim NewItem As New ListItem
            NewItem.Text = "--- Please Select --"
            NewItem.Value = ""
            cboTitle.Items.Insert(0, NewItem)


            'Now we can go and get our record
            Dim MyDetails As DataSet = NashBLL.GetMyDetails(Session("ContactID"))
            Dim dr As DataRow = MyDetails.Tables(0).Rows(0)
            txtFirstName.Text = dr("FirstName")
            txtUserName.Text = dr("UserName")
            txtSurname.Text = dr("Surname")
            If UCase(dr("EmailAddress")) <> "NONE" Then
                'There is an email address so we can show it and a link
                txtEmailAddress.Text = dr("EmailAddress")
                hypMailMe.NavigateUrl = "mailto:" & dr("EmailAddress") & "?Subject=Campbell Nash Contacts"
                hypMailMe.Visible = True
            Else
                'No email address exists so hide the link and empty the box
                txtEmailAddress.Text = ""
                hypMailMe.Visible = False
            End If
            litReference.Text = Session("ContactID")
            hidEmailAddress.Value = dr("EmailAddress")
            txtPassword.Attributes.Add("Value", dr("Password"))
            txtPasswordConfirm.Attributes.Add("Value", dr("Password"))
            cboTitle.SelectedValue = dr("TitleID")
            panMain.Visible = True
        End If
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As System.EventArgs) Handles btnUpdate.Click
        'Update this user's record, set the data elements
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
        
        'Now attempt our update
        Dim Result As Integer = NashBLL.UpdateMyDetails(Session("ContactID"), _
                                                        cboTitle.SelectedValue, _
                                                    txtFirstName.Text, _
                                                    txtSurname.Text, _
                                                    txtEmailAddress.Text, _
                                                    txtUserName.Text, _
                                                    txtPassword.Text)
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
                'Everything was OK so now we need to check if the user changed their email address
                If UCase(txtEmailAddress.Text) <> UCase(hidEmailAddress.Value) Then
                    'User changed their email address so we need to update their record and send them a mail
                    Dim MailBody As String = ""
                    MailBody = "Dear " & txtFirstName.Text & "," & vbCrLf & vbLf & _
                            "Thank you for maintaining your details with Cerico!" & vbCrLf & vbLf & _
                            "In order to complete your updated registration, please click on the link below, or copy & paste it into a browser window." & vbCrLf & vbLf & _
                            ContentURL & vbCrLf & vbLf & _
                            "Thank you," & vbCrLf & vbLf & _
                            "Cerico Admin Team."

                    Dim MailResult As Integer = NashBLL.SendMail(txtEmailAddress.Text, "", "", MailBody, "New email address confirmation from Cerico", "", False)
                    Select Case MailResult
                        Case -1
                            'Some kind of trouble with the mail service so advise
                            lblResult.Text = " Your data update was successful, but the mail we needed to send you has not been sent.<br /><br />Please contact us quoting Ref: - " & Session("ContactID")
                        Case Else
                            'Registration process was completed successfully so we can advise the user
                            panMain.Visible = False
                            panEmailChanged.Visible = True
                    End Select
                    'Update their registration now so that they can't login until they verify
                    NashBLL.UnVerifyAccount(Session("ContactID"), VerificationCode)
                    'Log the user out now
                    Session("UserLoggedIn") = False
                Else
                    'User just updated some details but not an email change so just confirm
                    panMain.Visible = False
                    panSuccess.Visible = True
                End If
        End Select


    End Sub

End Class