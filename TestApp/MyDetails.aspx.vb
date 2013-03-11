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
Imports CNash.MasterClass
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Security
Public Class MyDetails
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Session("UserLoggedIn") Then
            'User is not logged in so send to login page
            Response.Redirect("~/Account/login.aspx")
        End If
        If Not IsPostBack Then
            'Populate our form dropdowns starting with the titles
            cboTitle.DataSource = NashBLL.GetUserTitles
            cboTitle.DataTextField = "Title"
            cboTitle.DataValueField = "TitleID"
            cboTitle.DataBind()
            'Now get our contact types
            cboContactType.DataSource = NashBLL.GetContactTypes
            cboContactType.DataTextField = "ContactType"
            cboContactType.DataValueField = "ContactTypeID"
            cboContactType.DataBind()
            Dim NewItem As New ListItem
            NewItem.Text = "--- Please Select --"
            NewItem.Value = ""
            cboTitle.Items.Insert(0, NewItem)
            cboContactType.Items.Insert(0, NewItem)

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

            txtFaceBook.Text = dr("FaceBook")
            txtJobTitle.Text = dr("JobTitle")
            txtTelephoneNumber.Text = dr("TelephoneNumber")
            txtTwitter.Text = dr("Twitter")
            txtMobile.Text = dr("MobileNumber")
            txtPassword.Attributes.Add("Value", dr("Password"))
            cboTitle.SelectedValue = dr("TitleID")
            cboContactType.SelectedValue = dr("ContactTypeID")
           panMain.Visible = True
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As System.EventArgs) Handles btnUpdate.Click
        'Update this user's record, set the data elements
        Dim UserName As String
        Dim Password As String
        Dim TitleID As Integer
        Dim FirstName As String
        Dim Surname As String
        Dim EmailAddress As String
        Dim TelephoneNumber As String
        Dim MobileNumber As String
        Dim FaceBook As String
        Dim Twitter As String
        Dim ContactTypeID As Integer
        Dim JobTitle As String
        If txtEmailAddress.Text = "" Then
            EmailAddress = "None"
        Else
            EmailAddress = txtEmailAddress.Text
        End If
        If txtMobile.Text = "" Then
            MobileNumber = "None"
        Else
            MobileNumber = txtMobile.Text
        End If
        If txtFaceBook.Text = "" Then
            FaceBook = "None"
        Else
            FaceBook = txtFaceBook.Text
        End If
        If txtTwitter.Text = "" Then
            Twitter = "None"
        Else
            Twitter = txtTwitter.Text
        End If
        UserName = txtUserName.Text
        Password = txtPassword.Text
        TitleID = cboTitle.SelectedValue
        FirstName = txtFirstName.Text
        Surname = txtSurname.Text
        TelephoneNumber = txtTelephoneNumber.Text
        ContactTypeID = cboContactType.SelectedValue
        JobTitle = txtJobTitle.Text
        'Now attempt our update
        Dim Result As Integer = NashBLL.UpdateMyDetails(Session("ContactID"), _
                                                        TitleID, _
                                                        FirstName, _
                                                        Surname, _
                                                        EmailAddress, _
                                                        TelephoneNumber, _
                                                        MobileNumber, _
                                                        FaceBook, _
                                                        Twitter, _
                                                        UserName, _
                                                        Password, _
                                                        ContactTypeID, _
                                                        JobTitle)
        'Now see if we updated
        Select Case Result
            Case 0
                'Successfully updated
                panMain.Visible = False
                panConfirm.Visible = True
            Case Else
                'Duplicated username
                lblWarning.Text = "Duplicated username, please amend and try again."
        End Select
    End Sub
End Class