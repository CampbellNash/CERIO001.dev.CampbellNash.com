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
    Public gbLoopCount As Integer
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Always reset our gloabl loopcount
        gbLoopCount = 0
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
            Dim Shareholders As DataSet = NashBLL.QuestionnaireGetParentShareholderDetails(2) 'This value will need replaced by querystring
            rptShareholders.DataSource = Shareholders
            rptShareholders.DataBind()
            'Set the default panel up
            rblParent.SelectedIndex = 1
            gbLoopCount = 0
            Dim ParentCompanies As DataSet = NashBLL.QuestionnaireGetParentCompanyDetails(2) 'This value needs replaced by querystring
            rptParentCompany.DataSource = ParentCompanies
            rptParentCompany.DataBind()
            panParentCompanies.Visible = True
            btnAddNewParent.Visible = True

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

#Region " Manage parent company "

    Protected Sub rblParent_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblParent.SelectedIndexChanged
        If rblParent.SelectedIndex = 0 Then
            'Hide the current panels
            panParentCompanies.Visible = False
            btnAddNewParent.Visible = False
        Else
            'Go and get any previously entered items, this will always return at least 1 row
            Dim ParentCompanies As DataSet = NashBLL.QuestionnaireGetParentCompanyDetails(2) 'This value needs replaced by querystring
            rptParentCompany.DataSource = ParentCompanies
            rptParentCompany.DataBind()
            panParentCompanies.Visible = True
            btnAddNewParent.Visible = True
        End If
    End Sub

    Protected Sub btnAddNewParent_Click(sender As Object, e As EventArgs) Handles btnAddNewParent.Click
        'Adds a new row to the table
        Dim LoopCount As Integer = 1
        Dim txtParentCompanyName As TextBox
        Dim txtParentCompanyNumber As TextBox
        Dim txtParentCountry As TextBox
        Dim txtPercentOwned As TextBox
        Dim hidItemID As HiddenField
        'Check what values we have already as we need to preserve them
        For Each Item As RepeaterItem In rptParentCompany.Items
            txtParentCompanyName = Item.FindControl("txtParentCompanyName")
            txtParentCompanyNumber = Item.FindControl("txtParentCompanyNumber")
            txtParentCountry = Item.FindControl("txtParentCountry")
            txtPercentOwned = Item.FindControl("txtPercentOwned")
            hidItemID = Item.FindControl("hidItemID")
            'Now update this to the DB
            If txtParentCompanyName.Text = "" Then
                txtParentCompanyName.Text = "None"
            End If
            If txtParentCompanyNumber.Text = "" Then
                txtParentCompanyNumber.Text = "None"
            End If
            If txtParentCountry.Text = "" Then
                txtParentCountry.Text = "None"
            End If
            If txtPercentOwned.Text = "" Then
                txtPercentOwned.Text = "0"
            End If
            'Now update this line to the DB
            NashBLL.UpateParentCompanyLine(hidItemID.Value, _
                                           txtParentCompanyName.Text, _
                                           txtParentCompanyNumber.Text, _
                                           txtParentCountry.Text, _
                                           txtPercentOwned.Text)
        Next
        'Now we can finally add our new line
        NashBLL.AddParentCompanyLine(2) 'This value needs replaced by querystring

        'Now rebind everything
        Dim ParentCompanies As DataSet = NashBLL.QuestionnaireGetParentCompanyDetails(2) 'This value needs replaced by querystring
        rptParentCompany.DataSource = ParentCompanies
        rptParentCompany.DataBind()
    End Sub

    Protected Sub DeleteParentLine(sender As Object, e As EventArgs)
        'Deletes a row from the table
        Dim LoopCount As Integer = 1
        Dim txtParentCompanyName As TextBox
        Dim txtParentCompanyNumber As TextBox
        Dim txtParentCountry As TextBox
        Dim txtPercentOwned As TextBox
        Dim hidItemID As HiddenField
        'Check what values we have already as we need to preserve them
        For Each Item As RepeaterItem In rptParentCompany.Items
            txtParentCompanyName = Item.FindControl("txtParentCompanyName")
            txtParentCompanyNumber = Item.FindControl("txtParentCompanyNumber")
            txtParentCountry = Item.FindControl("txtParentCountry")
            txtPercentOwned = Item.FindControl("txtPercentOwned")
            hidItemID = Item.FindControl("hidItemID")
            'Now update this to the DB
            If txtParentCompanyName.Text = "" Then
                txtParentCompanyName.Text = "None"
            End If
            If txtParentCompanyNumber.Text = "" Then
                txtParentCompanyNumber.Text = "None"
            End If
            If txtParentCountry.Text = "" Then
                txtParentCountry.Text = "None"
            End If
            If txtPercentOwned.Text = "" Then
                txtPercentOwned.Text = "0"
            End If
            'Now update this line to the DB
            NashBLL.UpateParentCompanyLine(hidItemID.Value, _
                                           txtParentCompanyName.Text, _
                                           txtParentCompanyNumber.Text, _
                                           txtParentCountry.Text, _
                                           txtPercentOwned.Text)
        Next
        'Now we can finally remove our line
        NashBLL.DeleteParentCompanyLine(sender.CommandArgument)

        'Now rebind everything
        Dim ParentCompanies As DataSet = NashBLL.QuestionnaireGetParentCompanyDetails(2) 'This value needs replaced by querystring
        rptParentCompany.DataSource = ParentCompanies
        rptParentCompany.DataBind()
    End Sub

#End Region

#Region " Databindings "

    Protected Sub rptParentCompany_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptParentCompany.ItemDataBound
        Dim txtParentCompanyName As TextBox
        Dim txtParentCompanyNumber As TextBox
        Dim txtParentCountry As TextBox
        Dim txtPercentOwned As TextBox
        Dim btnDeleteParent As Button
        Dim hidItemID As HiddenField
        Dim drv As DataRowView

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            'This is a data item so we can populate the text boxes now
            txtParentCompanyName = e.Item.FindControl("txtParentCompanyName")
            txtParentCompanyNumber = e.Item.FindControl("txtParentCompanyNumber")
            txtParentCountry = e.Item.FindControl("txtParentCountry")
            txtPercentOwned = e.Item.FindControl("txtPercentOwned")
            btnDeleteParent = e.Item.FindControl("btnDeleteParent")
            hidItemID = e.Item.FindControl("hidItemID")
            drv = e.Item.DataItem
            'Now complete our details
            hidItemID.Value = drv("ItemID")
            If UCase(drv("ParentCompanyName")) <> "NONE" Then
                'A value was writtent to the DB so we need to re-populate the item now
                txtParentCompanyName.Text = drv("ParentCompanyName")
            Else
                'No value entered yet so show empty box
                txtParentCompanyName.Text = ""
            End If

            If UCase(drv("ParentCompanyNumber")) <> "NONE" Then
                'A value was writtent to the DB so we need to re-populate the item now
                txtParentCompanyNumber.Text = drv("ParentCompanyNumber")
            Else
                'No value entered yet so show empty box
                txtParentCompanyNumber.Text = ""
            End If

            If UCase(drv("ParentCompanyCountry")) <> "NONE" Then
                'A value was writtent to the DB so we need to re-populate the item now
                txtParentCountry.Text = drv("ParentCompanyCountry")
            Else
                'No value entered yet so show empty box
                txtParentCountry.Text = ""
            End If

            If CInt(drv("PercentageOwned")) <> 0 Then
                'A value was written to the DB so we need to re-populate the item now
                txtPercentOwned.Text = drv("PercentageOwned")
            Else
                'No value entered yet so show empty box
                txtPercentOwned.Text = "0"
            End If
            'Now set opur delete button
            btnDeleteParent.CommandArgument = drv("ItemID")
            If gbLoopCount = 0 Then
                'This is the first item in the list and that cannot be deleted
                btnDeleteParent.Visible = False
            End If
            gbLoopCount += 1
        End If
    End Sub

    Protected Sub rptShareholders_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptShareholders.ItemDataBound
        Dim txtShareholderName As TextBox
        Dim txtShareholderNationality As TextBox
        Dim txtPercentOwned As TextBox
        Dim btnDeleteShareholder As Button
        Dim hidItemID As HiddenField
        Dim drv As DataRowView

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            'This is a data item so we can populate the text boxes now
            txtShareholderName = e.Item.FindControl("txtShareholderName")
            txtShareholderNationality = e.Item.FindControl("txtShareholderNationality")
            txtPercentOwned = e.Item.FindControl("txtPercentOwned")
            btnDeleteShareholder = e.Item.FindControl("btnDeleteShareholder")
            hidItemID = e.Item.FindControl("hidItemID")
            drv = e.Item.DataItem
            'Now complete our details
            hidItemID.Value = drv("ItemID")
            If UCase(drv("ShareholderName")) <> "NONE" Then
                'A value was writtent to the DB so we need to re-populate the item now
                txtShareholderName.Text = drv("ShareholderName")
            Else
                'No value entered yet so show empty box
                txtShareholderName.Text = ""
            End If

            If UCase(drv("Nationality")) <> "NONE" Then
                'A value was writtent to the DB so we need to re-populate the item now
                txtShareholderNationality.Text = drv("Nationality")
            Else
                'No value entered yet so show empty box
                txtShareholderNationality.Text = ""
            End If


            If CInt(drv("PercentageOwned")) <> 0 Then
                'A value was written to the DB so we need to re-populate the item now
                txtPercentOwned.Text = drv("PercentageOwned")
            Else
                'No value entered yet so show empty box
                txtPercentOwned.Text = "0"
            End If
            'Now set opur delete button
            btnDeleteShareholder.CommandArgument = drv("ItemID")
            If gbLoopCount = 0 Then
                'This is the first item in the list and that cannot be deleted
                btnDeleteShareholder.Visible = False
            End If
            gbLoopCount += 1
        End If
    End Sub

#End Region


End Class
