Option Strict Off
Option Explicit On
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  Page Title       : myquestionnaire.aspx.vb           '
'  Description      : Shows open questionnaire          ' 
'  Author           : Brian McAulay                     '
'  Creation Date    : 27 Mar 2013                       '
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
Partial Class myquestionnaire
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        'Load our dynamic form controls
        litStatus.Text &= "Newly started"
        Dim MyString As String = "Mr,Mrs,Miss,MS"
        Dim MyArray = Split(MyString, ",")
        Dim rfv As New RequiredFieldValidator
        Dim TextBox As New TextBox
        Dim DropDown As New DropDownList
        DropDown.ID = "cboTitles"
        DropDown.Items.Add(New ListItem("-- Please Select --", ""))
        For LoopCount = LBound(MyArray) To UBound(MyArray)
            DropDown.Items.Add(New ListItem(MyArray(LoopCount), MyArray(LoopCount)))
        Next
        DropDown.AutoPostBack = True
        DropDown.ToolTip = "Choose a title"
        'Add our handler to deal with the selected changes
        AddHandler DropDown.SelectedIndexChanged, AddressOf cboTitles_SelectedIndexChanged
        rfv.ControlToValidate = "cboTitles"
        rfv.ErrorMessage = "Please choose your title"
        rfv.ForeColor = Drawing.Color.Red
        rfv.ValidationGroup = "Questionnaire"
        rfv.Display = ValidatorDisplay.Dynamic
        
        TextBox.Columns = 40
        TextBox.TextMode = TextBoxMode.Password
        TextBox.ID = "txtPassword"
        TextBox.CssClass = "input-block-level"
        Dim rfv2 As New RequiredFieldValidator
        rfv2.ControlToValidate = "txtPassword"
        rfv2.ErrorMessage = "Please enter password"
        rfv2.ForeColor = Drawing.Color.Red
        rfv2.ValidationGroup = "Questionnaire"
        rfv2.Display = ValidatorDisplay.Dynamic
        phMain.Controls.Add(DropDown)
        phMain.Controls.Add(rfv)
        phMain.Controls.Add(TextBox)
        phMain.Controls.Add(rfv2)

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Session("UserLoggedIn") Then
            'User is not logged in so send to login page
            Response.Redirect("~/login.aspx")
        End If

        If Not IsPostBack Then


        End If
    End Sub

    Protected Sub cboTitles_SelectedIndexChanged(sender As Object, e As EventArgs)
        litStatus.Text = "Updated by a dynamic control"
    End Sub

    
End Class
