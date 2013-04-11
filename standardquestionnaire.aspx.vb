Option Strict Off
Option Explicit On
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  Page Title       : standardquestionnaire.aspx.vb     '
'  Description      : Shows open questionnaire          ' 
'  Author           : Brian McAulay                     '
'  Creation Date    : 10 Apr 2013                       '
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
Partial Class standardquestionnaire
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Session("UserLoggedIn") Then
            'User is not logged in so send to login page
            Response.Redirect("~/login.aspx")
        End If

        If Not IsPostBack Then
            'Populate the form elements
            cboBusinessType.DataSource = NashBLL.GetBusinessAreas
            cboBusinessType.DataValueField = "BusinessAreaID"
            cboBusinessType.DataTextField = "BusinessArea"
            cboBusinessType.DataBind()
            Dim NewItem As New ListItem With {.Text = "--- Please Select ---", .Value = ""}
            cboBusinessType.Items.Insert(0, NewItem)
            panPage1.Visible = True
            btnNext.CommandArgument = 1
            btnPrev.Visible = False
            btnAddNewParent.CommandArgument = 2
        End If
    End Sub

    
    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Threading.Thread.Sleep(5000)
        txtCompanyName.Text = "Bullwood Business Consultants"
    End Sub

    Protected Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Select Case sender.CommandArgument
            Case 1
                panPage1.Visible = False
                panPage2.Visible = True
                btnNext.CommandArgument = 2
                btnPrev.Visible = True
                btnPrev.CommandArgument = 1
            Case 2

            Case Else

        End Select
    End Sub

    Protected Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        Select Case sender.CommandArgument
            Case 1
                panPage1.Visible = True
                panPage2.Visible = False
                btnNext.CommandArgument = 1
                btnPrev.Visible = False

            Case 2

            Case Else

        End Select
    End Sub

    Protected Sub rblParent_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblParent.SelectedIndexChanged
        If rblParent.SelectedIndex = 0 Then
            tblParent.Visible = False
            btnAddNewParent.Visible = False
        Else
            tblParent.Visible = True
            btnAddNewParent.Visible = True
        End If
    End Sub

    Protected Sub btnAddNewParent_Click(sender As Object, e As EventArgs) Handles btnAddNewParent.Click
        'Add our rows to the table
        Dim tblRow As New TableRow
        Dim tblCell1 As New TableCell
        Dim tblCell2 As New TableCell
        Dim tblCell3 As New TableCell
        Dim tblCell4 As New TableCell
        Dim tblCell5 As New TableCell
        Dim rfv1 As New RequiredFieldValidator
        Dim rfv2 As New RequiredFieldValidator
        Dim rfv3 As New RequiredFieldValidator
        Dim rfv4 As New RequiredFieldValidator
        Dim txt1 As New TextBox
        Dim txt2 As New TextBox
        Dim txt3 As New TextBox
        Dim txt4 As New TextBox
        Dim DeletMe As New LinkButton
        Dim LoopCount As Integer = sender.CommandArgument * 10
        'Add the first table cell
        tblRow.ID = sender.CommandArgument
        txt1.ID = "TextBox" & LoopCount
        txt1.CssClass = "input-xlarge"
        rfv1.ID = "rfv" & LoopCount
        rfv1.ControlToValidate = "TextBox" & LoopCount
        rfv1.ErrorMessage = "Enter company name"
        rfv1.CssClass = "alert-error"
        rfv1.ValidationGroup = "Questions"
        tblCell1.Controls.Add(txt1)
        tblCell1.Controls.Add(rfv1)
        tblRow.Cells.Add(tblCell1)

        'Add the next table cell
        txt2.ID = "TextBox" & LoopCount + 1
        txt2.CssClass = "input-xlarge"
        rfv2.ID = "rfv" & LoopCount + 1
        rfv2.ControlToValidate = "TextBox" & LoopCount + 1
        rfv2.ErrorMessage = "Please enter company number"
        rfv2.CssClass = "alert-error"
        rfv2.ValidationGroup = "Questions"
        tblCell2.Controls.Add(txt2)
        tblCell2.Controls.Add(rfv2)
        tblRow.Cells.Add(tblCell2)

        'Add the next table cell
        txt3.ID = "TextBox" & LoopCount + 2
        txt3.CssClass = "input-xlarge"
        rfv3.ID = "rfv" & LoopCount + 2
        rfv3.ControlToValidate = "TextBox" & LoopCount + 2
        rfv3.ErrorMessage = "Please enter country of registration"
        rfv3.CssClass = "alert-error"
        rfv3.ValidationGroup = "Questions"
        tblCell3.Controls.Add(txt3)
        tblCell3.Controls.Add(rfv3)
        tblRow.Cells.Add(tblCell3)

        'Add the next table cell
        txt4.ID = "TextBox" & LoopCount + 3
        txt4.CssClass = "input-xlarge"
        rfv4.ID = "rfv" & LoopCount + 3
        rfv4.ControlToValidate = "TextBox" & LoopCount + 3
        rfv4.ErrorMessage = "Please enter &#37; of company owned"
        rfv4.CssClass = "alert-error"
        rfv4.ValidationGroup = "Questions"
        tblCell4.Controls.Add(txt4)
        tblCell4.Controls.Add(rfv4)
        tblRow.Cells.Add(tblCell4)

        'Set our delete button to remove this row
        DeletMe.Text = "Delete"
        tblCell5.Controls.Add(DeletMe)
        tblRow.Cells.Add(tblCell5)

        'Now add our complete row
        tblParent.Rows.Add(tblRow)

        'Increase our command argument for the next row to be added
        btnAddNewParent.CommandArgument = sender.CommandArgument + 1


    End Sub
End Class
