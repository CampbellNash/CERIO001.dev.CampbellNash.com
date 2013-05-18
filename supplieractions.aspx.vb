Option Strict Off
Option Explicit On
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  Page Title       : supplieractions.aspx.vb           '
'  Description      : Manages & approves items in my    '
'                     cerico page                       '
'  Author           : Brian McAulay                     '
'  Creation Date    : 17 May 2013                       '
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
Partial Class supplieractions
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Session("UserLoggedIn") Then
            panCloseWindow.Visible = True
            Return
        End If
        If Not IsPostBack Then

            Select Case Request.QueryString("Type")
                'See what type of request we're handling
                Case "Staff"
                    Dim PersonaDetails As DataSet = NashBLL.GetStaffMemberDetails(Request.QueryString("ID"))
                    Dim MyDetails As DataRow = PersonaDetails.Tables(0).Rows(0)
                    Dim CompanyName As String = PersonaDetails.Tables(1).Rows(0)("CompanyName")
                    Dim EmailAddress = MyDetails("EmailAddress")
                    litStaffName.Text = MyDetails("FirstName") & " " & MyDetails("Surname")
                    litStaffDetails.Text = MyDetails("EmailAddress") & "<br />" & MyDetails("TelephoneNumber")
                    panStaff.Visible = True
                    
                Case "Supplier"

                Case Else
                    'This is the customer area
            End Select
        End If
    End Sub

    Protected Sub btnApprove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApprove.Click
        'We're approving a new staff member so send the mail
        Dim PersonaDetails As DataSet = NashBLL.GetStaffMemberDetails(Request.QueryString("ID"))
        Dim MyDetails As DataRow = PersonaDetails.Tables(0).Rows(0)
        Dim CompanyName As String = PersonaDetails.Tables(1).Rows(0)("CompanyName")
        Dim EmailAddress = MyDetails("EmailAddress")
        NashBLL.ApproveStaffMember(Request.QueryString("ID"), Session("ContactID"))
        Dim MailBody As String = "Dear " & MyDetails("FirstName") & "," & vbCrLf & vbLf & _
        "Your apllication to join '" & CompanyName & "' has been accepted and approved '" & vbCrLf & vbLf & _
        "Please login to your account at our web site using the link below to find out more details." & vbCrLf & vbLf & _
        "http://cerio-live.azurewebsites.net/default.aspx" & vbCrLf & vbLf & _
        "Thank you," & vbCrLf & vbLf & _
        "CERICO Admin Team."
        'Now send this mail to the new staff member
        NashBLL.SendMail(EmailAddress, "", "", MailBody, "Application to join company", "", False)
        panStaff.Visible = False
        panSuccess.Visible = True
    End Sub
End Class
