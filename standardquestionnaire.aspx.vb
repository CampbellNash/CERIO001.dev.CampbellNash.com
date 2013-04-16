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
            'Now get our countries
            cboCountries.DataSource = NashBLL.GetCountries
            cboCountries.DataTextField = "CountryName"
            cboCountries.DataValueField = "CountryID"
            cboCountries.DataBind()
            'Now get our Minerals list for table popop
            Dim Minerals As DataSet = NashBLL.GetMinerals()
            rptMineralsPopup.DataSource = Minerals
            rptMineralsPopup.DataBind()
            'Now get our Smeleter list
            Dim SmelterList As DataSet = NashBLL.GetSmelterList()
            rptSmelterList.DataSource = SmelterList
            rptSmelterList.DataBind()
            'Now get out relationship categories
            Dim RelationshipCategory As DataSet = NashBLL.GetrelationshipCategories()
            rptrelationshipCategories.DataSource = RelationshipCategory
            rptrelationshipCategories.DataBind()
            'Now add in a please select
            Dim NewItem As New ListItem With {.Text = "--- Please Select ---", .Value = ""}
            cboBusinessType.Items.Insert(0, NewItem)
            cboCountries.Items.Insert(0, newItem)

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
            'Reset the loop count and go and get the directors list
            gbLoopCount = 0
            Dim Directors As DataSet = NashBLL.QuestionnaireGetDirectorDetails(2) 'This value will need replaced by querystring
            rptDirectors.DataSource = Directors
            rptDirectors.DataBind()
            'Reset the loop count and go and get the relatives list
            gbLoopCount = 0
            Dim Relatives As DataSet = NashBLL.QuestionnaireGetGovtEmployeeDetails(2) 'This value will need replaced by querystring
            rptGovtEmployees.DataSource = Relatives
            rptGovtEmployees.DataBind()
            Dim DangerousCountries As DataSet = NashBLL.GetDangerousCountries
            rptDangerousCountries.DataSource = DangerousCountries
            rptDangerousCountries.DataBind()

        End If
    End Sub


    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Threading.Thread.Sleep(5000)
        txtCompanyName.Text = "Bullwood Business Consultants"
    End Sub

#Region " Navigation Buttons "

    Protected Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Select Case sender.CommandArgument
            Case 1
                panPage1.Visible = False
                panPage2.Visible = True
                btnNext.CommandArgument = 2
                btnPrev.Visible = True
                btnPrev.CommandArgument = 1
                lblProgress.Width = "333"
            Case 2
                panPage2.Visible = False
                panPage3.Visible = True
                btnNext.CommandArgument = 3
                btnPrev.Visible = True
                btnPrev.CommandArgument = 2
                lblProgress.Width = "499"
            Case 3
                panPage3.Visible = False
                panPage4.Visible = True
                btnNext.Visible = True
                btnNext.CommandArgument = 4
                btnPrev.Visible = True
                btnPrev.CommandArgument = 3
                lblProgress.Width = "632"
            Case 4
                panPage4.Visible = False
                panPage5.Visible = True
                btnNext.Visible = True
                btnNext.CommandArgument = 5
                btnPrev.Visible = True
                btnPrev.CommandArgument = 4
                lblProgress.Width = "798"
            Case 5
                panPage5.Visible = False
                panPage6.Visible = True
                btnNext.Visible = False
                btnPrev.Visible = True
                btnPrev.CommandArgument = 5
                lblProgress.Width = "1000"

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
                lblProgress.Width = "166"
            Case 2
                panPage2.Visible = True
                panPage3.Visible = False
                btnNext.CommandArgument = 2
                btnPrev.Visible = True
                btnPrev.CommandArgument = 1
                lblProgress.Width = "333"
            Case 3
                panPage3.Visible = True
                panPage4.Visible = False
                btnNext.CommandArgument = 3
                btnPrev.Visible = True
                btnNext.Visible = True
                btnPrev.CommandArgument = 2
                lblProgress.Width = "499"
            Case 4
                panPage4.Visible = True
                panPage5.Visible = False
                btnNext.CommandArgument = 4
                btnPrev.Visible = True
                btnNext.Visible = True
                btnPrev.CommandArgument = 3
                lblProgress.Width = "632"
            Case 5
                panPage5.Visible = True
                panPage6.Visible = False
                btnNext.CommandArgument = 5
                btnPrev.Visible = True
                btnNext.Visible = True
                btnPrev.CommandArgument = 4
                lblProgress.Width = "798"
            Case Else

        End Select
    End Sub

#End Region

#Region " Manage Minerals "

    Protected Sub rblCassiterite_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblCassiterite.SelectedIndexChanged
        If rblCassiterite.SelectedIndex = 1 Then
            'Selected yes to this option so show the rest of the form
            Dim Purposes As DataSet = NashBLL.QuestionnaireGetMineralPurposeDetails(2) 'This value needs replaced by querystring
            rptPurpose.DataSource = Purposes
            rptPurpose.DataBind()
            Dim Processes As DataSet = NashBLL.QuestionnaireGetMineralProcessDetails(2) 'This value needs replaced by querystring
            rptProcess.DataSource = Processes
            rptProcess.DataBind()
            Dim Components As DataSet = NashBLL.QuestionnaireGetMineralComponentDetails(2) 'This value needs replaced by querystring
            rptComponent.DataSource = Components
            rptComponent.DataBind()
            panCassiterite.Visible = True
            panMineralPurpose.Visible = True
        Else
            'Turning this mineral off so check to see if it was the last one selected and if so, then hide the rest of the form
            CheckMinerals()
            panCassiterite.Visible = False
        End If
    End Sub

    Protected Sub rblColumbite_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblColumbite.SelectedIndexChanged
        If rblColumbite.SelectedIndex = 1 Then
            'Selected yes to this option so show the rest of the form
            Dim Purposes As DataSet = NashBLL.QuestionnaireGetMineralPurposeDetails(2) 'This value needs replaced by querystring
            rptPurpose.DataSource = Purposes
            rptPurpose.DataBind()
            Dim Processes As DataSet = NashBLL.QuestionnaireGetMineralProcessDetails(2) 'This value needs replaced by querystring
            rptProcess.DataSource = Processes
            rptProcess.DataBind()
            Dim Components As DataSet = NashBLL.QuestionnaireGetMineralComponentDetails(2) 'This value needs replaced by querystring
            rptComponent.DataSource = Components
            rptComponent.DataBind()
            panColumbite.Visible = True
            panMineralPurpose.Visible = True
        Else
            'Turning this mineral off so check to see if it was the last one selected and if so, then hide the rest of the form
            CheckMinerals()
            panColumbite.Visible = False
        End If
    End Sub

    Protected Sub rblGold_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblGold.SelectedIndexChanged
        If rblGold.SelectedIndex = 1 Then
            'Selected yes to this option so show the rest of the form
            Dim Purposes As DataSet = NashBLL.QuestionnaireGetMineralPurposeDetails(2) 'This value needs replaced by querystring
            rptPurpose.DataSource = Purposes
            rptPurpose.DataBind()
            Dim Processes As DataSet = NashBLL.QuestionnaireGetMineralProcessDetails(2) 'This value needs replaced by querystring
            rptProcess.DataSource = Processes
            rptProcess.DataBind()
            Dim Components As DataSet = NashBLL.QuestionnaireGetMineralComponentDetails(2) 'This value needs replaced by querystring
            rptComponent.DataSource = Components
            rptComponent.DataBind()
            panGold.Visible = True
            panMineralPurpose.Visible = True
        Else
            'Turning this mineral off so check to see if it was the last one selected and if so, then hide the rest of the form
            CheckMinerals()
            panGold.Visible = False
        End If
    End Sub

    Protected Sub rblTantalum_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblTantalum.SelectedIndexChanged
        If rblTantalum.SelectedIndex = 1 Then
            'Selected yes to this option so show the rest of the form
            Dim Purposes As DataSet = NashBLL.QuestionnaireGetMineralPurposeDetails(2) 'This value needs replaced by querystring
            rptPurpose.DataSource = Purposes
            rptPurpose.DataBind()
            Dim Processes As DataSet = NashBLL.QuestionnaireGetMineralProcessDetails(2) 'This value needs replaced by querystring
            rptProcess.DataSource = Processes
            rptProcess.DataBind()
            Dim Components As DataSet = NashBLL.QuestionnaireGetMineralComponentDetails(2) 'This value needs replaced by querystring
            rptComponent.DataSource = Components
            rptComponent.DataBind()
            panTantalum.Visible = True
            panMineralPurpose.Visible = True
        Else
            'Turning this mineral off so check to see if it was the last one selected and if so, then hide the rest of the form
            CheckMinerals()
            panTantalum.Visible = False
        End If
    End Sub

    Protected Sub rblTungsten_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblTungsten.SelectedIndexChanged
        If rblTungsten.SelectedIndex = 1 Then
            'Selected yes to this option so show the rest of the form
            Dim Purposes As DataSet = NashBLL.QuestionnaireGetMineralPurposeDetails(2) 'This value needs replaced by querystring
            rptPurpose.DataSource = Purposes
            rptPurpose.DataBind()
            Dim Processes As DataSet = NashBLL.QuestionnaireGetMineralProcessDetails(2) 'This value needs replaced by querystring
            rptProcess.DataSource = Processes
            rptProcess.DataBind()
            Dim Components As DataSet = NashBLL.QuestionnaireGetMineralComponentDetails(2) 'This value needs replaced by querystring
            rptComponent.DataSource = Components
            rptComponent.DataBind()
            panTungsten.Visible = True
            panMineralPurpose.Visible = True
        Else
            'Turning this mineral off so check to see if it was the last one selected and if so, then hide the rest of the form
            CheckMinerals()
            panTungsten.Visible = False
        End If
    End Sub

    Protected Sub rblWolframite_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblWolframite.SelectedIndexChanged
        If rblWolframite.SelectedIndex = 1 Then
            'Selected yes to this option so show the rest of the form
            Dim Purposes As DataSet = NashBLL.QuestionnaireGetMineralPurposeDetails(2) 'This value needs replaced by querystring
            rptPurpose.DataSource = Purposes
            rptPurpose.DataBind()
            Dim Processes As DataSet = NashBLL.QuestionnaireGetMineralProcessDetails(2) 'This value needs replaced by querystring
            rptProcess.DataSource = Processes
            rptProcess.DataBind()
            Dim Components As DataSet = NashBLL.QuestionnaireGetMineralComponentDetails(2) 'This value needs replaced by querystring
            rptComponent.DataSource = Components
            rptComponent.DataBind()
            panWolframite.Visible = True
            panMineralPurpose.Visible = True
        Else
            'Turning this mineral off so check to see if it was the last one selected and if so, then hide the rest of the form
            CheckMinerals()
            panWolframite.Visible = False
        End If
    End Sub

    Private Sub CheckMinerals()
        If rblCassiterite.SelectedIndex = 0 AndAlso _
            rblColumbite.SelectedIndex = 0 AndAlso _
            rblGold.SelectedIndex = 0 AndAlso _
            rblTantalum.SelectedIndex = 0 AndAlso _
            rblTungsten.SelectedIndex = 0 AndAlso _
            rblWolframite.SelectedIndex = 0 Then
            'None of these elements have been chosen so we can hide the rest of the form
            panMineralPurpose.Visible = False
            panQuestion5.Visible = False
        
        End If
    End Sub

    Protected Sub btnAddPurpose_Click(sender As Object, e As EventArgs) Handles btnAddPurpose.Click
        'Adds a new row to the table
        Dim LoopCount As Integer = 1
        Dim cboMinerals As DropDownList
        Dim txtDescription As TextBox
        Dim MineralID As Integer
        Dim hidItemID As HiddenField
        'Check what values we have already as we need to preserve them
        For Each Item As RepeaterItem In rptPurpose.Items
            txtDescription = Item.FindControl("txtPurpose")
            cboMinerals = Item.FindControl("cboMinerals")
            hidItemID = Item.FindControl("hidItemID")
            'Now update this to the DB
            If txtDescription.Text = "" Then
                txtDescription.Text = "None"
            End If
            If cboMinerals.SelectedIndex = 0 Then
                MineralID = 0
            Else
                MineralID = cboMinerals.SelectedValue
            End If
            'Now update this line to the DB
            NashBLL.UpdatePurposeLine(hidItemID.Value, _
                                           MineralID, _
                                           txtDescription.Text)
        Next
        'Now we can finally add our new line
        NashBLL.AddPurposeLine(2) 'This value needs replaced by querystring

        'Now rebind everything
        Dim Purposes As DataSet = NashBLL.QuestionnaireGetMineralPurposeDetails(2) 'This value needs replaced by querystring
        rptPurpose.DataSource = Purposes
        rptPurpose.DataBind()
    End Sub

    Protected Sub DeletePurposeLine(sender As Object, e As EventArgs)
        'Deletes a row from the table
        Dim LoopCount As Integer = 1
        Dim cboMinerals As DropDownList
        Dim txtDescription As TextBox
        Dim MineralID As Integer
        Dim hidItemID As HiddenField
        'Check what values we have already as we need to preserve them
        For Each Item As RepeaterItem In rptPurpose.Items
            txtDescription = Item.FindControl("txtPurpose")
            cboMinerals = Item.FindControl("cboMinerals")
            hidItemID = Item.FindControl("hidItemID")
            'Now update this to the DB
            If txtDescription.Text = "" Then
                txtDescription.Text = "None"
            End If
            If cboMinerals.SelectedIndex = 0 Then
                MineralID = 0
            Else
                MineralID = cboMinerals.SelectedValue
            End If
            'Now update this line to the DB
            NashBLL.UpdatePurposeLine(hidItemID.Value, _
                                           MineralID, _
                                           txtDescription.Text)
        Next
        'Now we can finally remove our line
        NashBLL.DeletePurposeLine(sender.CommandArgument)

        'Now rebind everything
        Dim Purposes As DataSet = NashBLL.QuestionnaireGetMineralPurposeDetails(2) 'This value needs replaced by querystring
        rptPurpose.DataSource = Purposes
        rptPurpose.DataBind()
    End Sub

    Protected Sub btnAddProcess_Click(sender As Object, e As EventArgs) Handles btnAddProcess.Click
        'Adds a new row to the table
        Dim LoopCount As Integer = 1
        Dim cboMinerals As DropDownList
        Dim txtDescription As TextBox
        Dim MineralID As Integer
        Dim hidItemID As HiddenField
        'Check what values we have already as we need to preserve them
        For Each Item As RepeaterItem In rptProcess.Items
            txtDescription = Item.FindControl("txtProcess")
            cboMinerals = Item.FindControl("cboMinerals")
            hidItemID = Item.FindControl("hidItemID")
            'Now update this to the DB
            If txtDescription.Text = "" Then
                txtDescription.Text = "None"
            End If
            If cboMinerals.SelectedIndex = 0 Then
                MineralID = 0
            Else
                MineralID = cboMinerals.SelectedValue
            End If
            'Now update this line to the DB
            NashBLL.UpdateProcessLine(hidItemID.Value, _
                                           MineralID, _
                                           txtDescription.Text)
        Next
        'Now we can finally add our new line
        NashBLL.AddProcessLine(2) 'This value needs replaced by querystring

        'Now rebind everything
        Dim Processes As DataSet = NashBLL.QuestionnaireGetMineralProcessDetails(2) 'This value needs replaced by querystring
        rptProcess.DataSource = Processes
        rptProcess.DataBind()
    End Sub

    Protected Sub DeleteProcessLine(sender As Object, e As EventArgs)
        'Deletes a row from the table
        Dim LoopCount As Integer = 1
        Dim cboMinerals As DropDownList
        Dim txtDescription As TextBox
        Dim MineralID As Integer
        Dim hidItemID As HiddenField
        'Check what values we have already as we need to preserve them
        For Each Item As RepeaterItem In rptProcess.Items
            txtDescription = Item.FindControl("txtProcess")
            cboMinerals = Item.FindControl("cboMinerals")
            hidItemID = Item.FindControl("hidItemID")
            'Now update this to the DB
            If txtDescription.Text = "" Then
                txtDescription.Text = "None"
            End If
            If cboMinerals.SelectedIndex = 0 Then
                MineralID = 0
            Else
                MineralID = cboMinerals.SelectedValue
            End If
            'Now update this line to the DB
            NashBLL.UpdateProcessLine(hidItemID.Value, _
                                           MineralID, _
                                           txtDescription.Text)
        Next
        'Now we can finally remove our line
        NashBLL.DeleteProcessLine(sender.CommandArgument)

        'Now rebind everything
        Dim Processes As DataSet = NashBLL.QuestionnaireGetMineralProcessDetails(2) 'This value needs replaced by querystring
        rptProcess.DataSource = Processes
        rptProcess.DataBind()
    End Sub

    Protected Sub btnAddComponent_Click(sender As Object, e As EventArgs) Handles btnAddComponent.Click
        'Adds a new row to the table
        Dim LoopCount As Integer = 1
        Dim cboMinerals As DropDownList
        Dim txtDescription As TextBox
        Dim MineralID As Integer
        Dim hidItemID As HiddenField
        'Check what values we have already as we need to preserve them
        For Each Item As RepeaterItem In rptComponent.Items
            txtDescription = Item.FindControl("txtComponent")
            cboMinerals = Item.FindControl("cboMinerals")
            hidItemID = Item.FindControl("hidItemID")
            'Now update this to the DB
            If txtDescription.Text = "" Then
                txtDescription.Text = "None"
            End If
            If cboMinerals.SelectedIndex = 0 Then
                MineralID = 0
            Else
                MineralID = cboMinerals.SelectedValue
            End If
            'Now update this line to the DB
            NashBLL.UpdateComponentLine(hidItemID.Value, _
                                           MineralID, _
                                           txtDescription.Text)
        Next
        'Now we can finally add our new line
        NashBLL.AddComponentLine(2) 'This value needs replaced by querystring

        'Now rebind everything
        Dim Components As DataSet = NashBLL.QuestionnaireGetMineralComponentDetails(2) 'This value needs replaced by querystring
        rptComponent.DataSource = Components
        rptComponent.DataBind()
    End Sub

    Protected Sub DeleteComponentLine(sender As Object, e As EventArgs)
        'Deletes a row from the table
        Dim LoopCount As Integer = 1
        Dim cboMinerals As DropDownList
        Dim txtDescription As TextBox
        Dim MineralID As Integer
        Dim hidItemID As HiddenField
        'Check what values we have already as we need to preserve them
        For Each Item As RepeaterItem In rptComponent.Items
            txtDescription = Item.FindControl("txtComponent")
            cboMinerals = Item.FindControl("cboMinerals")
            hidItemID = Item.FindControl("hidItemID")
            'Now update this to the DB
            If txtDescription.Text = "" Then
                txtDescription.Text = "None"
            End If
            If cboMinerals.SelectedIndex = 0 Then
                MineralID = 0
            Else
                MineralID = cboMinerals.SelectedValue
            End If
            'Now update this line to the DB
            NashBLL.UpdateComponentLine(hidItemID.Value, _
                                           MineralID, _
                                           txtDescription.Text)
        Next
        'Now we can finally remove our line
        NashBLL.DeleteComponentLine(sender.CommandArgument)

        'Now rebind everything
        Dim Components As DataSet = NashBLL.QuestionnaireGetMineralComponentDetails(2) 'This value needs replaced by querystring
        rptComponent.DataSource = Components
        rptComponent.DataBind()
    End Sub


#End Region

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

#Region " Manage shareholders "

    Protected Sub btnAddNewShareholder_Click(sender As Object, e As EventArgs) Handles btnAddNewShareholder.Click
        'Adds a new row to the table
        Dim LoopCount As Integer = 1
        Dim txtShareholderName As TextBox
        Dim txtShareholderNationality As TextBox
        Dim txtPercentOwned As TextBox
        Dim hidItemID As HiddenField
        'Check what values we have already as we need to preserve them
        For Each Item As RepeaterItem In rptShareholders.Items
            txtShareholderName = Item.FindControl("txtShareholderName")
            txtShareholderNationality = Item.FindControl("txtShareholderNationality")
            txtPercentOwned = Item.FindControl("txtPercentOwned")
            hidItemID = Item.FindControl("hidItemID")
            'Now update this to the DB
            If txtShareholderName.Text = "" Then
                txtShareholderName.Text = "None"
            End If
            If txtShareholderNationality.Text = "" Then
                txtShareholderNationality.Text = "None"
            End If
            If txtPercentOwned.Text = "" Then
                txtPercentOwned.Text = "0"
            End If
            'Now update this line to the DB
            NashBLL.UpateShareholderLine(hidItemID.Value, _
                                           txtShareholderName.Text, _
                                           txtShareholderNationality.Text, _
                                           txtPercentOwned.Text)
        Next
        'Now we can finally add our new line
        NashBLL.AddShareholderLine(2) 'This value needs replaced by querystring

        'Now rebind everything
        Dim Shareholders As DataSet = NashBLL.QuestionnaireGetParentShareholderDetails(2) 'This value needs replaced by querystring
        rptShareholders.DataSource = Shareholders
        rptShareholders.DataBind()
    End Sub

    Protected Sub DeleteShareholderLine(sender As Object, e As EventArgs)
        'Deletes a row from the table
        Dim LoopCount As Integer = 1
        Dim txtShareholderName As TextBox
        Dim txtShareholderNationality As TextBox
        Dim txtPercentOwned As TextBox
        Dim hidItemID As HiddenField
        'Check what values we have already as we need to preserve them
        For Each Item As RepeaterItem In rptShareholders.Items
            txtShareholderName = Item.FindControl("txtShareholderName")
            txtShareholderNationality = Item.FindControl("txtShareholderNationality")
            txtPercentOwned = Item.FindControl("txtPercentOwned")
            hidItemID = Item.FindControl("hidItemID")
            'Now update this to the DB
            If txtShareholderName.Text = "" Then
                txtShareholderName.Text = "None"
            End If
            If txtShareholderNationality.Text = "" Then
                txtShareholderNationality.Text = "None"
            End If
            If txtPercentOwned.Text = "" Then
                txtPercentOwned.Text = "0"
            End If
            'Now update this line to the DB
            NashBLL.UpateShareholderLine(hidItemID.Value, _
                                           txtShareholderName.Text, _
                                           txtShareholderNationality.Text, _
                                           txtPercentOwned.Text)
        Next
        'Now we can finally remove our line
        NashBLL.DeleteShareholderLine(sender.CommandArgument)

        'Now rebind everything
        Dim Shareholders As DataSet = NashBLL.QuestionnaireGetParentShareholderDetails(2) 'This value needs replaced by querystring
        rptShareholders.DataSource = Shareholders
        rptShareholders.DataBind()
    End Sub

#End Region

#Region " Manage Directors "

    Protected Sub btnAddNewDirector_Click(sender As Object, e As EventArgs) Handles btnAddNewDirector.Click
        'Adds a new row to the table
        Dim LoopCount As Integer = 1
        Dim txtShareholderName As TextBox
        Dim txtShareholderNationality As TextBox
        Dim txtPercentOwned As TextBox
        Dim hidItemID As HiddenField
        'Check what values we have already as we need to preserve them
        For Each Item As RepeaterItem In rptShareholders.Items
            txtShareholderName = Item.FindControl("txtShareholderName")
            txtShareholderNationality = Item.FindControl("txtShareholderNationality")
            txtPercentOwned = Item.FindControl("txtPercentOwned")
            hidItemID = Item.FindControl("hidItemID")
            'Now update this to the DB
            If txtShareholderName.Text = "" Then
                txtShareholderName.Text = "None"
            End If
            If txtShareholderNationality.Text = "" Then
                txtShareholderNationality.Text = "None"
            End If
            If txtPercentOwned.Text = "" Then
                txtPercentOwned.Text = "0"
            End If
            'Now update this line to the DB
            NashBLL.UpateDirectorLine(hidItemID.Value, _
                                           txtShareholderName.Text, _
                                           txtShareholderNationality.Text, _
                                           txtPercentOwned.Text)
        Next
        'Now we can finally add our new line
        NashBLL.AddDirectorLine(2) 'This value needs replaced by querystring

        'Now rebind everything
        Dim Directors As DataSet = NashBLL.QuestionnaireGetDirectorDetails(2) 'This value needs replaced by querystring
        rptDirectors.DataSource = Directors
        rptDirectors.DataBind()
    End Sub

    Protected Sub DeleteDirectorLine(sender As Object, e As EventArgs)
        'Deletes a row from the table
        Dim LoopCount As Integer = 1
        Dim txtDirectorName As TextBox
        Dim txtDirectorNationality As TextBox
        Dim txtDirectorJobTitle As TextBox
        Dim hidItemID As HiddenField
        'Check what values we have already as we need to preserve them
        For Each Item As RepeaterItem In rptDirectors.Items
            txtDirectorName = Item.FindControl("txtDirectorName")
            txtDirectorNationality = Item.FindControl("txtDirectorNationality")
            txtDirectorJobTitle = Item.FindControl("txtDirectorJobTitle")
            hidItemID = Item.FindControl("hidItemID")
            'Now update this to the DB
            If txtDirectorName.Text = "" Then
                txtDirectorName.Text = "None"
            End If
            If txtDirectorNationality.Text = "" Then
                txtDirectorNationality.Text = "None"
            End If
            If txtDirectorJobTitle.Text = "" Then
                txtDirectorJobTitle.Text = ""
            End If
            'Now update this line to the DB
            NashBLL.UpateDirectorLine(hidItemID.Value, _
                                           txtDirectorName.Text, _
                                           txtDirectorNationality.Text, _
                                           txtDirectorJobTitle.Text)
        Next
        'Now we can finally remove our line
        NashBLL.DeleteDirectorLine(sender.CommandArgument)

        'Now rebind everything
        Dim Directors As DataSet = NashBLL.QuestionnaireGetDirectorDetails(2) 'This value needs replaced by querystring
        rptDirectors.DataSource = Directors
        rptDirectors.DataBind()
    End Sub


#End Region

#Region " Manage Government Employees "

    Protected Sub chkGovernmentEmployee_CheckedChanged(sender As Object, e As EventArgs) Handles chkGovernmentEmployee.CheckedChanged
        If chkGovernmentEmployee.Checked Then
            panGovernmanetEmployee.Visible = True
            gbLoopCount = 0
            Dim Relatives As DataSet = NashBLL.QuestionnaireGetGovtEmployeeDetails(2) 'This value needs replaced by querystring
            rptGovtEmployees.DataSource = Relatives
            rptGovtEmployees.DataBind()
        Else
            panGovernmanetEmployee.Visible = False
        End If
    End Sub

    Protected Sub btnAddNewRelative_Click(sender As Object, e As EventArgs) Handles btnAddNewRelative.Click
        'Adds a new row to the table
        Dim txtPersonName As TextBox
        Dim txtRelativeName As TextBox
        Dim txtRelationshipType As TextBox
        Dim txtLastJob As TextBox
        Dim txtJobCountry As TextBox
        Dim rdpDateEnded As Telerik.Web.UI.RadDatePicker
        Dim hidItemID As HiddenField
        'Check what values we have already as we need to preserve them
        For Each Item As RepeaterItem In rptGovtEmployees.Items
            txtPersonName = Item.FindControl("txtPersonName")
            txtRelativeName = Item.FindControl("txtRelativeName")
            txtRelationshipType = Item.FindControl("txtRelationshipType")
            txtLastJob = Item.FindControl("txtLastJob")
            txtJobCountry = Item.FindControl("txtJobCountry")
            rdpDateEnded = Item.FindControl("rdpDateEnded")
            hidItemID = Item.FindControl("hidItemID")
            'Now update this to the DB
            If txtPersonName.Text = "" Then
                txtPersonName.Text = "None"
            End If
            If txtRelativeName.Text = "" Then
                txtRelativeName.Text = "None"
            End If
            If txtRelationshipType.Text = "" Then
                txtRelationshipType.Text = ""
            End If
            If txtLastJob.Text = "" Then
                txtLastJob.Text = "None"
            End If
            If txtJobCountry.Text = "" Then
                txtJobCountry.Text = ""
            End If

            'Now update this line to the DB
            NashBLL.UpdateRelativeLine(hidItemID.Value, _
                                      txtPersonName.Text, _
                                      txtRelativeName.Text, _
                                      txtRelationshipType.Text, _
                                      txtLastJob.Text, _
                                      txtJobCountry.Text, _
                                      rdpDateEnded.DbSelectedDate)
        Next
        'Now we can finally add our new line
        NashBLL.AddRelativeLine(2) 'This value needs replaced by querystring

        'Now rebind everything
        Dim Relatives As DataSet = NashBLL.QuestionnaireGetGovtEmployeeDetails(2) 'This value needs replaced by querystring
        rptGovtEmployees.DataSource = Relatives
        rptGovtEmployees.DataBind()
    End Sub

    Protected Sub DeleteRelativeLine(sender As Object, e As EventArgs)
        'Deletes a row from the table
        Dim txtPersonName As TextBox
        Dim txtRelativeName As TextBox
        Dim txtRelationshipType As TextBox
        Dim txtLastJob As TextBox
        Dim txtJobCountry As TextBox
        Dim rdpDateEnded As Telerik.Web.UI.RadDatePicker
        Dim hidItemID As HiddenField
        'Check what values we have already as we need to preserve them
        For Each Item As RepeaterItem In rptGovtEmployees.Items
            txtPersonName = Item.FindControl("txtPersonName")
            txtRelativeName = Item.FindControl("txtRelativeName")
            txtRelationshipType = Item.FindControl("txtRelationshipType")
            txtLastJob = Item.FindControl("txtLastJob")
            txtJobCountry = Item.FindControl("txtJobCountry")
            rdpDateEnded = Item.FindControl("rdpDateEnded")
            hidItemID = Item.FindControl("hidItemID")
            'Now update this to the DB
            If txtPersonName.Text = "" Then
                txtPersonName.Text = "None"
            End If
            If txtRelativeName.Text = "" Then
                txtRelativeName.Text = "None"
            End If
            If txtRelationshipType.Text = "" Then
                txtRelationshipType.Text = ""
            End If
            If txtLastJob.Text = "" Then
                txtLastJob.Text = "None"
            End If
            If txtJobCountry.Text = "" Then
                txtJobCountry.Text = ""
            End If

            'Now update this line to the DB
            NashBLL.UpdateRelativeLine(hidItemID.Value, _
                                      txtPersonName.Text, _
                                      txtRelativeName.Text, _
                                      txtRelationshipType.Text, _
                                      txtLastJob.Text, _
                                      txtJobCountry.Text, _
                                      rdpDateEnded.DbSelectedDate)
        Next
        'Now we can finally remove our line
        NashBLL.DeleteRelativeLine(sender.CommandArgument)

        'Now rebind everything
        Dim Relatives As DataSet = NashBLL.QuestionnaireGetGovtEmployeeDetails(2) 'This value needs replaced by querystring
        rptGovtEmployees.DataSource = Relatives
        rptGovtEmployees.DataBind()
    End Sub

#End Region

#Region " Manage Scrap "

    Protected Sub rblScrap_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblScrap.SelectedIndexChanged
        If rblScrap.SelectedIndex = 1 Then
            panScrap.Visible = True
        Else
            panScrap.Visible = False
        End If
    End Sub

#End Region

#Region " Manage Recycled "

    Protected Sub rblRecycled_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblRecycled.SelectedIndexChanged
        If rblRecycled.SelectedIndex = 1 Then
            panRecycled.Visible = True
        Else
            panRecycled.Visible = False
        End If
    End Sub

#End Region

#Region " Manage Countries "

    Protected Sub CheckCountry(sender As Object, e As EventArgs)
        Dim NoCountry As Boolean = True
        For Each Item As RepeaterItem In rptDangerousCountries.Items
            Dim ButtonList As RadioButtonList = Item.FindControl("rblDangerousCountry")
            If ButtonList.SelectedIndex = 1 Then
                NoCountry = False
            End If
        Next
        'Now check to see if we can show or hide our panel
        If NoCountry Then
            panQuestion5.Visible = False
        Else
            panQuestion5.Visible = True
        End If
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
                'A value was written to the DB so we need to re-populate the item now
                txtParentCompanyName.Text = drv("ParentCompanyName")
            Else
                'No value entered yet so show empty box
                txtParentCompanyName.Text = ""
            End If

            If UCase(drv("ParentCompanyNumber")) <> "NONE" Then
                'A value was written to the DB so we need to re-populate the item now
                txtParentCompanyNumber.Text = drv("ParentCompanyNumber")
            Else
                'No value entered yet so show empty box
                txtParentCompanyNumber.Text = ""
            End If

            If UCase(drv("ParentCompanyCountry")) <> "NONE" Then
                'A value was written to the DB so we need to re-populate the item now
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
                txtPercentOwned.Text = ""
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
                'A value was written to the DB so we need to re-populate the item now
                txtShareholderName.Text = drv("ShareholderName")
            Else
                'No value entered yet so show empty box
                txtShareholderName.Text = ""
            End If

            If UCase(drv("Nationality")) <> "NONE" Then
                'A value was written to the DB so we need to re-populate the item now
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
                txtPercentOwned.Text = ""
            End If
            'Now set our delete button
            btnDeleteShareholder.CommandArgument = drv("ItemID")
            If gbLoopCount = 0 Then
                'This is the first item in the list and that cannot be deleted
                btnDeleteShareholder.Visible = False
            End If
            gbLoopCount += 1
        End If
    End Sub

    Protected Sub rptDirectors_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptDirectors.ItemDataBound
        Dim txtDirectorName As TextBox
        Dim txtDirectorNationality As TextBox
        Dim txtDirectorJobTitle As TextBox
        Dim btnDeleteDirector As Button
        Dim hidItemID As HiddenField
        Dim drv As DataRowView

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            'This is a data item so we can populate the text boxes now
            txtDirectorName = e.Item.FindControl("txtDirectorName")
            txtDirectorJobTitle = e.Item.FindControl("txtDirectorJobTitle")
            txtDirectorNationality = e.Item.FindControl("txtDirectorNationality")
            btnDeleteDirector = e.Item.FindControl("btnDeleteDirector")
            hidItemID = e.Item.FindControl("hidItemID")
            drv = e.Item.DataItem
            'Now complete our details
            hidItemID.Value = drv("ItemID")
            If UCase(drv("DirectorName")) <> "NONE" Then
                'A value was writtent to the DB so we need to re-populate the item now
                txtDirectorName.Text = drv("DirectorName")
            Else
                'No value entered yet so show empty box
                txtDirectorName.Text = ""
            End If

            If UCase(drv("DirectorNationality")) <> "NONE" Then
                'A value was written to the DB so we need to re-populate the item now
                txtDirectorNationality.Text = drv("Nationality")
            Else
                'No value entered yet so show empty box
                txtDirectorNationality.Text = ""
            End If

            If UCase(drv("DirectorJobTitle")) <> "NONE" Then
                'A value was written to the DB so we need to re-populate the item now
                txtDirectorJobTitle.Text = drv("DirectorJobTitle")
            Else
                'No value entered yet so show empty box
                txtDirectorJobTitle.Text = ""
            End If


            'Now set our delete button
            btnDeleteDirector.CommandArgument = drv("ItemID")
            If gbLoopCount = 0 Then
                'This is the first item in the list and that cannot be deleted
                btnDeleteDirector.Visible = False
            End If
            gbLoopCount += 1
        End If
    End Sub

    Protected Sub rptGovtEmployees_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptGovtEmployees.ItemDataBound
        Dim txtPersonName As TextBox
        Dim txtRelativeName As TextBox
        Dim txtRelationshipType As TextBox
        Dim txtLastJob As TextBox
        Dim txtJobCountry As TextBox
        Dim rdpDateEnded As Telerik.Web.UI.RadDatePicker
        Dim btnDeleteRelative As Button
        Dim hidItemID As HiddenField
        Dim drv As DataRowView

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            'This is a data item so we can populate the text boxes now
            txtPersonName = e.Item.FindControl("txtPersonName")
            txtRelativeName = e.Item.FindControl("txtRelativeName")
            txtRelationshipType = e.Item.FindControl("txtRelationshipType")
            txtLastJob = e.Item.FindControl("txtLastJob")
            txtJobCountry = e.Item.FindControl("txtJobCountry")
            rdpDateEnded = e.Item.FindControl("rdpDateEnded")
            hidItemID = e.Item.FindControl("hidItemID")
            btnDeleteRelative = e.Item.FindControl("btnDeleteRelative")
            drv = e.Item.DataItem
            'Now complete our details
            hidItemID.Value = drv("ItemID")
            If UCase(drv("PersonName")) <> "NONE" Then
                'A value was written to the DB so we need to re-populate the item now
                txtPersonName.Text = drv("PersonName")
            Else
                'No value entered yet so show empty box
                txtPersonName.Text = ""
            End If

            If UCase(drv("RelativeName")) <> "NONE" Then
                'A value was written to the DB so we need to re-populate the item now
                txtRelativeName.Text = drv("RelativeName")
            Else
                'No value entered yet so show empty box
                txtRelativeName.Text = ""
            End If

            If UCase(drv("RelationshipType")) <> "NONE" Then
                'A value was written to the DB so we need to re-populate the item now
                txtRelationshipType.Text = drv("RelationshipType")
            Else
                'No value entered yet so show empty box
                txtRelationshipType.Text = ""
            End If

            If UCase(drv("LastJobHeld")) <> "NONE" Then
                'A value was written to the DB so we need to re-populate the item now
                txtLastJob.Text = drv("LastJobHeld")
            Else
                'No value entered yet so show empty box
                txtLastJob.Text = ""
            End If

            If UCase(drv("JobCountry")) <> "NONE" Then
                'A value was written to the DB so we need to re-populate the item now
                txtJobCountry.Text = drv("JobCountry")
            Else
                'No value entered yet so show empty box
                txtJobCountry.Text = ""
            End If

            If Not IsDBNull(drv("DateEnded")) Then
                rdpDateEnded.DbSelectedDate = CDate(drv("DateEnded")).ToString("dd MMM yyyy")
            Else
                rdpDateEnded.SelectedDate = Now
            End If

            'Now set our delete button
            btnDeleteRelative.CommandArgument = drv("ItemID")
            If gbLoopCount = 0 Then
                'This is the first item in the list and that cannot be deleted
                btnDeleteRelative.Visible = False
            End If
            gbLoopCount += 1
        End If
    End Sub

    Protected Sub rptPurpose_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptPurpose.ItemDataBound
        Dim LoopCount As Integer = 1
        Dim cboMinerals As DropDownList
        Dim txtDescription As TextBox
        Dim hidItemID As HiddenField
        Dim btnDeletePurpose As Button
        Dim drv As DataRowView
        Dim Minerals As DataSet = NashBLL.GetMinerals

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            'This is a data item so we can populate the form boxes now
            cboMinerals = e.Item.FindControl("cboMinerals")
            txtDescription = e.Item.FindControl("txtPurpose")
            hidItemID = e.Item.FindControl("hidItemID")
            btnDeletePurpose = e.Item.FindControl("btnDeletePurpose")
            'Get our data row now
            drv = e.Item.DataItem
            'Now we need to set our minerals menu
            cboMinerals.DataSource = Minerals
            cboMinerals.DataValueField = "MineralID"
            cboMinerals.DataTextField = "MineralName"
            cboMinerals.DataBind()
            cboMinerals.SelectedValue = drv("MineralID")
            Dim NewItem As New ListItem With {.Text = "--- Please Select ---", .Value = ""}
            cboMinerals.Items.Insert(0, NewItem)
            If UCase(drv("Description")) <> "NONE" Then
                txtDescription.Text = drv("Description")
            Else
                txtDescription.Text = ""
            End If
            'Set our managed values
            btnDeletePurpose.CommandArgument = drv("ItemID")
            hidItemID.Value = drv("ItemID")
        End If
    End Sub

    Protected Sub rptProcess_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptProcess.ItemDataBound
        Dim LoopCount As Integer = 1
        Dim cboMinerals As DropDownList
        Dim txtDescription As TextBox
        Dim hidItemID As HiddenField
        Dim btnDeleteProcess As Button
        Dim drv As DataRowView
        Dim Minerals As DataSet = NashBLL.GetMinerals

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            'This is a data item so we can populate the form boxes now
            cboMinerals = e.Item.FindControl("cboMinerals")
            txtDescription = e.Item.FindControl("txtProcess")
            hidItemID = e.Item.FindControl("hidItemID")
            btnDeleteProcess = e.Item.FindControl("btnDeleteProcess")
            'Get our data row now
            drv = e.Item.DataItem
            'Now we need to set our minerals menu
            cboMinerals.DataSource = Minerals
            cboMinerals.DataValueField = "MineralID"
            cboMinerals.DataTextField = "MineralName"
            cboMinerals.DataBind()
            cboMinerals.SelectedValue = drv("MineralID")
            Dim NewItem As New ListItem With {.Text = "--- Please Select ---", .Value = ""}
            cboMinerals.Items.Insert(0, NewItem)
            If UCase(drv("Description")) <> "NONE" Then
                txtDescription.Text = drv("Description")
            Else
                txtDescription.Text = ""
            End If
            'Set our managed values
            btnDeleteProcess.CommandArgument = drv("ItemID")
            hidItemID.Value = drv("ItemID")
        End If
    End Sub

    Protected Sub rptComponent_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptComponent.ItemDataBound
        Dim LoopCount As Integer = 1
        Dim cboMinerals As DropDownList
        Dim txtDescription As TextBox
        Dim hidItemID As HiddenField
        Dim btnDeleteComponent As Button
        Dim drv As DataRowView
        Dim Minerals As DataSet = NashBLL.GetMinerals

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            'This is a data item so we can populate the form boxes now
            cboMinerals = e.Item.FindControl("cboMinerals")
            txtDescription = e.Item.FindControl("txtComponent")
            hidItemID = e.Item.FindControl("hidItemID")
            btnDeleteComponent = e.Item.FindControl("btnDeleteComponent")
            'Get our data row now
            drv = e.Item.DataItem
            'Now we need to set our minerals menu
            cboMinerals.DataSource = Minerals
            cboMinerals.DataValueField = "MineralID"
            cboMinerals.DataTextField = "MineralName"
            cboMinerals.DataBind()
            cboMinerals.SelectedValue = drv("MineralID")
            Dim NewItem As New ListItem With {.Text = "--- Please Select ---", .Value = ""}
            cboMinerals.Items.Insert(0, NewItem)
            If UCase(drv("Description")) <> "NONE" Then
                txtDescription.Text = drv("Description")
            Else
                txtDescription.Text = ""
            End If
            'Set our managed values
            btnDeleteComponent.CommandArgument = drv("ItemID")
            hidItemID.Value = drv("ItemID")
        End If
    End Sub

    Protected Sub rptMineralsPopup_itemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptMineralsPopup.ItemDataBound
        Dim LoopCount As Integer = 1
        Dim litMineralNamePopup As Literal
        Dim drv As DataRowView

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            'This is a data item so lets populate
            litMineralNamePopup = e.Item.FindControl("litMineralNamePopup")
            'get our data now
            drv = e.Item.DataItem
            litMineralNamePopup.Text = drv("MineralName")
        End If


    End Sub

    Protected Sub rptSmelterList_itemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptSmelterList.ItemDataBound
        Dim LoopCount As Integer = 1
        Dim litSmelterName As Literal
        Dim litSmelterLocation As Literal
        Dim litSmeltereffectiveDate As Literal
        Dim litSmelterMineral As Literal
        Dim drv As DataRowView

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            'This is a data item so lets populate
            litSmelterName = e.Item.FindControl("litSmelterName")
            litSmelterLocation = e.Item.FindControl("litSmelterLocation")
            litSmeltereffectiveDate = e.Item.FindControl("litSmeltereffectiveDate")
            'get our data now
            drv = e.Item.DataItem
            litSmelterName.Text = drv("CompanyName")
            litSmelterLocation.Text = drv("Location")
            litSmeltereffectiveDate.Text = drv("EffectiveDate")
        End If
    End Sub

    Protected Sub rptDangerousCountries_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptDangerousCountries.ItemDataBound
        Dim litCountryName As Literal
        Dim drv As DataRowView
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            litCountryName = e.Item.FindControl("litCountryName")
            drv = e.Item.DataItem
            litCountryName.Text = drv("CountryName")
        End If
    End Sub

    Protected Sub rptrelationshipCategories_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptrelationshipCategories.ItemDataBound
        Dim litRelationshipCategory As Literal
        Dim drv As DataRowView
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            litRelationshipCategory = e.Item.FindControl("litRelationshipCategory")
            drv = e.Item.DataItem
            litRelationshipCategory.Text = drv("Description")
        End If
    End Sub

#End Region














End Class
