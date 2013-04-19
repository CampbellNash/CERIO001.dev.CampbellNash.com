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
Imports Telerik.Web.UI
Partial Class standardquestionnaire
    Inherits System.Web.UI.Page
    Public gbLoopCount As Integer
    Public CompanyID As Integer

#Region " User Defined Functions "

    Private Function CheckMinerals() As Boolean
        'We need to check the minerals and the countries as a pair to determine what we show next
        Dim NoConflictSelected = False
        Dim NoCountrySelected = True
        If rblCassiterite.SelectedIndex = 0 AndAlso _
            rblColumbite.SelectedIndex = 0 AndAlso _
            rblGold.SelectedIndex = 0 AndAlso _
            rblTantalum.SelectedIndex = 0 AndAlso _
            rblTungsten.SelectedIndex = 0 AndAlso _
            rblWolframite.SelectedIndex = 0 Then
            'None of these elements have been chosen so we can hide the rest of the form
            NoConflictSelected = True
            panMineralPurpose.Visible = False
        End If

        For Each Item As RepeaterItem In rptDangerousCountries.Items
            Dim ButtonList As RadioButtonList = Item.FindControl("rblDangerousCountry")
            If ButtonList.SelectedIndex = 1 Then
                NoCountrySelected = False
            End If
        Next
        'Now check to see if we can show or hide our panels
        If NoConflictSelected AndAlso NoCountrySelected Then
            'No minerals or countries were selected so hide the panels
            panQuestion5.Visible = False
            panMineralPurpose.Visible = False
            panUpload.Visible = False
            'TODO: adjust prev & next logic as this would mean the next page is the last one
            btnNext.CommandArgument = 5
            btnPrev.CommandArgument = 3
            Return True
        Else
            'Either a mineral or a country was chosen, so show the rest of the form
            panQuestion5.Visible = True
            panMineralPurpose.Visible = True
            panUpload.Visible = True
            'TODO: adjust prev & next logic as this would mean the next page is the last one
            btnNext.CommandArgument = 3
            Return False
        End If
    End Function

#End Region

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Always reset our gloabl loopcount
        gbLoopCount = 0
        If Not Session("UserLoggedIn") Then
            'User is not logged in so send to login page
            Response.Redirect("~/login.aspx")
        End If

        'Check our query string value
        If Not Request.QueryString.HasKeys Or Not IsNumeric(Request.QueryString("ci")) Then
            panNoQuery.Visible = True
            panForm.Visible = False
            Return
        Else
            CompanyID = Request.QueryString("ci")
        End If

        If Not IsPostBack Then
            btnSave.CommandArgument = 1
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
            cboCountries.Items.Insert(0, NewItem)

            panPage1.Visible = True
            btnNext.CommandArgument = 1
            btnPrev.Visible = False
            Dim Shareholders As DataSet = NashBLL.QuestionnaireGetParentShareholderDetails(CompanyID) 'This value will need replaced by querystring
            rptShareholders.DataSource = Shareholders
            rptShareholders.DataBind()
            'Set the default panel up
            rblParent.SelectedIndex = 1
            gbLoopCount = 0
            Dim ParentCompanies As DataSet = NashBLL.QuestionnaireGetParentCompanyDetails(CompanyID)
            rptParentCompany.DataSource = ParentCompanies
            rptParentCompany.DataBind()
            panParentCompanies.Visible = True
            btnAddNewParent.Visible = True
            'Reset the loop count and go and get the directors list
            gbLoopCount = 0
            Dim Directors As DataSet = NashBLL.QuestionnaireGetDirectorDetails(CompanyID) 'This value will need replaced by querystring
            rptDirectors.DataSource = Directors
            rptDirectors.DataBind()
            'Reset the loop count and go and get the relatives list
            gbLoopCount = 0
            Dim Relatives As DataSet = NashBLL.QuestionnaireGetGovtEmployeeDetails(CompanyID) 'This value will need replaced by querystring
            rptGovtEmployees.DataSource = Relatives
            rptGovtEmployees.DataBind()
            Dim DangerousCountries As DataSet = NashBLL.GetDangerousCountries
            rptDangerousCountries.DataSource = DangerousCountries
            rptDangerousCountries.DataBind()
            'Reset the loop count and go and get the scrap list
            gbLoopCount = 0
            Dim ScrapList As DataSet = NashBLL.QuestionnaireGetMineralScrapDetails(CompanyID)
            rptScrap.DataSource = ScrapList
            rptScrap.DataBind()
            'Reset the loop count and go and get the recycled list
            gbLoopCount = 0
            Dim RecycleList As DataSet = NashBLL.QuestionnaireGetMineralRecycleDetails(CompanyID)
            rptRecycled.DataSource = RecycleList
            rptRecycled.DataBind()
            'Reset the loop count and go and get the recycled list
            gbLoopCount = 0
            Dim ExtractionList As DataSet = NashBLL.QuestionnaireGetExtractionDetails(CompanyID)
            rptExtraction.DataSource = ExtractionList
            rptExtraction.DataBind()
            'Reset the loop count and go and get the facility list
            gbLoopCount = 0
            Dim FacilityList As DataSet = NashBLL.QuestionnaireGetFacilityDetails(CompanyID)
            rptFacility.DataSource = FacilityList
            rptFacility.DataBind()
            'Reset the loop count and go and get the transport list
            gbLoopCount = 0
            Dim TransporterList As DataSet = NashBLL.QuestionnaireGetTransportDetails(CompanyID)
            rptTransport.DataSource = TransporterList
            rptTransport.DataBind()
            'Reset the loop count and go and get the other payment list
            gbLoopCount = 0
            Dim OtherPaymentList As DataSet = NashBLL.QuestionnaireGetOtherPaymentDetails(CompanyID)
            rptOtherPayment.DataSource = OtherPaymentList
            rptOtherPayment.DataBind()
            'Reset the loop count and go and get the other taxes list
            gbLoopCount = 0
            Dim OtherTaxList As DataSet = NashBLL.QuestionnaireGetOtherTaxDetails(CompanyID)
            rptOtherTaxes.DataSource = OtherTaxList
            rptOtherTaxes.DataBind()
            'Reset the loop count and go and get the tax list
            gbLoopCount = 0
            Dim TaxList As DataSet = NashBLL.QuestionnaireGetTaxDetails(CompanyID)
            rptTaxes.DataSource = TaxList
            rptTaxes.DataBind()
        End If
    End Sub

    Protected Sub rblIndependent_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblIndependent.SelectedIndexChanged
        If rblIndependent.SelectedIndex = 1 Then
            panIndependentAudit.Visible = True
        Else
            panIndependentAudit.Visible = False
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        panSaveDraft.Visible = True
        panForm.Visible = False
    End Sub

    Protected Sub btnReOpen_Click(sender As Object, e As EventArgs) Handles btnReOpen.Click
        panSaveDraft.Visible = False
        panForm.Visible = True
        Select Case CInt(btnSave.CommandArgument)
            Case 1
                panPage1.Visible = True
                panPage2.Visible = False
                panPage3.Visible = False
                panPage4.Visible = False
                panPage5.Visible = False
                panPage6.Visible = False
            Case 2
                panPage1.Visible = False
                panPage2.Visible = True
                panPage3.Visible = False
                panPage4.Visible = False
                panPage5.Visible = False
                panPage6.Visible = False
            Case 3
                panPage1.Visible = False
                panPage2.Visible = False
                panPage3.Visible = True
                panPage4.Visible = False
                panPage5.Visible = False
                panPage6.Visible = False
            Case 4
                panPage1.Visible = False
                panPage2.Visible = False
                panPage3.Visible = False
                panPage4.Visible = True
                panPage5.Visible = False
                panPage6.Visible = False
            Case 5
                panPage1.Visible = False
                panPage2.Visible = False
                panPage3.Visible = False
                panPage4.Visible = False
                panPage5.Visible = True
                panPage6.Visible = False
            Case Else
                panPage1.Visible = False
                panPage2.Visible = False
                panPage3.Visible = False
                panPage4.Visible = False
                panPage5.Visible = False
                panPage6.Visible = True
        End Select
    End Sub


#Region " Navigation Buttons "

    Protected Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Select Case sender.CommandArgument
            Case 1
                panPage1.Visible = False
                panPage2.Visible = True
                panPage3.Visible = False
                panPage4.Visible = False
                panPage5.Visible = False
                panPage6.Visible = False
                btnNext.CommandArgument = 2
                btnPrev.Visible = True
                btnPrev.CommandArgument = 1
                lblProgress.Width = "333"
                'Save the current page for saving and re-opening this form
                btnSave.CommandArgument = sender.CommandArgument
            Case 2
                panPage1.Visible = False
                panPage2.Visible = False
                panPage3.Visible = True
                panPage4.Visible = False
                panPage5.Visible = False
                panPage6.Visible = False
                If CheckMinerals() Then
                    btnNext.CommandArgument = 5
                    btnSave.CommandArgument = 3
                Else
                    btnNext.CommandArgument = 3
                    btnSave.CommandArgument = 3
                End If
                btnPrev.Visible = True
                btnPrev.CommandArgument = 2
                lblProgress.Width = "499"
                'Save the current page for saving and re-opening this form
                'btnSave.CommandArgument = sender.CommandArgument
            Case 3
                panPage1.Visible = False
                panPage2.Visible = False
                panPage3.Visible = False
                panPage4.Visible = True
                panPage5.Visible = False
                panPage6.Visible = False
                btnNext.Visible = True
                btnNext.CommandArgument = 4
                btnPrev.Visible = True
                btnPrev.CommandArgument = 3
                lblProgress.Width = "632"
                'Save the current page for saving and re-opening this form
                btnSave.CommandArgument = sender.CommandArgument
            Case 4
                panPage1.Visible = False
                panPage2.Visible = False
                panPage3.Visible = False
                panPage4.Visible = False
                panPage5.Visible = True
                panPage6.Visible = False
                btnNext.Visible = True
                btnNext.CommandArgument = 5
                btnPrev.Visible = True
                btnPrev.CommandArgument = 4
                lblProgress.Width = "798"
                'Save the current page for saving and re-opening this form
                btnSave.CommandArgument = sender.CommandArgument
            Case 5
                panPage1.Visible = False
                panPage2.Visible = False
                panPage3.Visible = False
                panPage4.Visible = False
                panPage5.Visible = False
                panPage6.Visible = True
                btnNext.Visible = False
                btnPrev.Visible = True
                If CheckMinerals() Then
                    btnPrev.CommandArgument = 3
                    btnSave.CommandArgument = 6
                Else
                    btnPrev.CommandArgument = 5
                    btnSave.CommandArgument = 6
                End If
                lblProgress.Width = "1000"
                'Save the current page for saving and re-opening this form
                'btnSave.CommandArgument = sender.CommandArgument
            Case Else
                
        End Select
        lblErrorMessage.Text = btnSave.CommandArgument
    End Sub

    Protected Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        Select Case sender.CommandArgument
            Case 1
                panPage1.Visible = True
                panPage2.Visible = False
                panPage3.Visible = False
                panPage4.Visible = False
                panPage5.Visible = False
                panPage6.Visible = False
                btnNext.CommandArgument = 1
                btnPrev.Visible = False
                lblProgress.Width = "166"
                'Save the current page for saving and re-opening this form
                btnSave.CommandArgument = sender.CommandArgument
            Case 2
                panPage1.Visible = False
                panPage2.Visible = True
                panPage3.Visible = False
                panPage4.Visible = False
                panPage5.Visible = False
                panPage6.Visible = False
                btnNext.CommandArgument = 2
                btnPrev.Visible = True
                btnPrev.CommandArgument = 1
                lblProgress.Width = "333"
                'Save the current page for saving and re-opening this form
                btnSave.CommandArgument = sender.CommandArgument
            Case 3
                panPage1.Visible = False
                panPage2.Visible = False
                panPage3.Visible = True
                panPage4.Visible = False
                panPage5.Visible = False
                panPage6.Visible = False
                If CheckMinerals() Then
                    btnNext.CommandArgument = 5
                    btnSave.CommandArgument = 3
                Else
                    btnNext.CommandArgument = 3
                    btnSave.CommandArgument = 3
                End If

                btnPrev.Visible = True
                btnNext.Visible = True
                btnPrev.CommandArgument = 2
                lblProgress.Width = "499"
           Case 4
                panPage1.Visible = False
                panPage2.Visible = False
                panPage3.Visible = False
                panPage4.Visible = True
                panPage5.Visible = False
                panPage6.Visible = False
                btnNext.CommandArgument = 4
                btnPrev.Visible = True
                btnNext.Visible = True
                btnPrev.CommandArgument = 3
                lblProgress.Width = "632"
                'Save the current page for saving and re-opening this form
                btnSave.CommandArgument = sender.CommandArgument
            Case 5
                panPage1.Visible = False
                panPage2.Visible = False
                panPage3.Visible = False
                panPage4.Visible = False
                panPage5.Visible = True
                panPage6.Visible = False
                btnNext.CommandArgument = 5
                btnPrev.Visible = True
                btnNext.Visible = True
                If CheckMinerals() Then
                    btnPrev.CommandArgument = 3
                Else
                    btnPrev.CommandArgument = 4
                End If
                lblProgress.Width = "798"
                'Save the current page for saving and re-opening this form
                btnSave.CommandArgument = sender.CommandArgument
            Case Else
                
        End Select
        lblErrorMessage.Text = btnSave.CommandArgument
    End Sub

#End Region

#Region " Manage New Lines "

    Protected Sub rblCassiterite_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblCassiterite.SelectedIndexChanged
        If rblCassiterite.SelectedIndex = 1 Then
            'Selected yes to this option so show the rest of the form
            Dim Purposes As DataSet = NashBLL.QuestionnaireGetMineralPurposeDetails(CompanyID)
            rptPurpose.DataSource = Purposes
            rptPurpose.DataBind()
            Dim Processes As DataSet = NashBLL.QuestionnaireGetMineralProcessDetails(CompanyID)
            rptProcess.DataSource = Processes
            rptProcess.DataBind()
            Dim Components As DataSet = NashBLL.QuestionnaireGetMineralComponentDetails(CompanyID)
            rptComponent.DataSource = Components
            rptComponent.DataBind()
            panCassiterite.Visible = True
            panMineralPurpose.Visible = True
            panQuestion5.Visible = True
            CheckMinerals()
        Else
            'Turning this mineral off so check to see if it was the last one selected and if so, then hide the rest of the form
            CheckMinerals()
            panCassiterite.Visible = False
        End If
    End Sub

    Protected Sub rblColumbite_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblColumbite.SelectedIndexChanged
        If rblColumbite.SelectedIndex = 1 Then
            'Selected yes to this option so show the rest of the form
            Dim Purposes As DataSet = NashBLL.QuestionnaireGetMineralPurposeDetails(CompanyID)
            rptPurpose.DataSource = Purposes
            rptPurpose.DataBind()
            Dim Processes As DataSet = NashBLL.QuestionnaireGetMineralProcessDetails(CompanyID)
            rptProcess.DataSource = Processes
            rptProcess.DataBind()
            Dim Components As DataSet = NashBLL.QuestionnaireGetMineralComponentDetails(CompanyID)
            rptComponent.DataSource = Components
            rptComponent.DataBind()
            panColumbite.Visible = True
            panMineralPurpose.Visible = True
            panQuestion5.Visible = True
            CheckMinerals()
        Else
            'Turning this mineral off so check to see if it was the last one selected and if so, then hide the rest of the form
            CheckMinerals()
            panColumbite.Visible = False
        End If
    End Sub

    Protected Sub rblGold_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblGold.SelectedIndexChanged
        If rblGold.SelectedIndex = 1 Then
            'Selected yes to this option so show the rest of the form
            Dim Purposes As DataSet = NashBLL.QuestionnaireGetMineralPurposeDetails(CompanyID)
            rptPurpose.DataSource = Purposes
            rptPurpose.DataBind()
            Dim Processes As DataSet = NashBLL.QuestionnaireGetMineralProcessDetails(CompanyID)
            rptProcess.DataSource = Processes
            rptProcess.DataBind()
            Dim Components As DataSet = NashBLL.QuestionnaireGetMineralComponentDetails(CompanyID)
            rptComponent.DataSource = Components
            rptComponent.DataBind()
            panGold.Visible = True
            panMineralPurpose.Visible = True
            panQuestion5.Visible = True
            CheckMinerals()
        Else
            'Turning this mineral off so check to see if it was the last one selected and if so, then hide the rest of the form
            CheckMinerals()
            panGold.Visible = False
        End If
    End Sub

    Protected Sub rblTantalum_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblTantalum.SelectedIndexChanged
        If rblTantalum.SelectedIndex = 1 Then
            'Selected yes to this option so show the rest of the form
            Dim Purposes As DataSet = NashBLL.QuestionnaireGetMineralPurposeDetails(CompanyID)
            rptPurpose.DataSource = Purposes
            rptPurpose.DataBind()
            Dim Processes As DataSet = NashBLL.QuestionnaireGetMineralProcessDetails(CompanyID)
            rptProcess.DataSource = Processes
            rptProcess.DataBind()
            Dim Components As DataSet = NashBLL.QuestionnaireGetMineralComponentDetails(CompanyID)
            rptComponent.DataSource = Components
            rptComponent.DataBind()
            panTantalum.Visible = True
            panMineralPurpose.Visible = True
            panQuestion5.Visible = True
            CheckMinerals()
        Else
            'Turning this mineral off so check to see if it was the last one selected and if so, then hide the rest of the form
            CheckMinerals()
            panTantalum.Visible = False
        End If
    End Sub

    Protected Sub rblTungsten_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblTungsten.SelectedIndexChanged
        If rblTungsten.SelectedIndex = 1 Then
            'Selected yes to this option so show the rest of the form
            Dim Purposes As DataSet = NashBLL.QuestionnaireGetMineralPurposeDetails(CompanyID)
            rptPurpose.DataSource = Purposes
            rptPurpose.DataBind()
            Dim Processes As DataSet = NashBLL.QuestionnaireGetMineralProcessDetails(CompanyID)
            rptProcess.DataSource = Processes
            rptProcess.DataBind()
            Dim Components As DataSet = NashBLL.QuestionnaireGetMineralComponentDetails(CompanyID)
            rptComponent.DataSource = Components
            rptComponent.DataBind()
            panTungsten.Visible = True
            panMineralPurpose.Visible = True
            panQuestion5.Visible = True
            CheckMinerals()
        Else
            'Turning this mineral off so check to see if it was the last one selected and if so, then hide the rest of the form
            CheckMinerals()
            panTungsten.Visible = False
        End If
    End Sub

    Protected Sub rblWolframite_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblWolframite.SelectedIndexChanged
        If rblWolframite.SelectedIndex = 1 Then
            'Selected yes to this option so show the rest of the form
            Dim Purposes As DataSet = NashBLL.QuestionnaireGetMineralPurposeDetails(CompanyID)
            rptPurpose.DataSource = Purposes
            rptPurpose.DataBind()
            Dim Processes As DataSet = NashBLL.QuestionnaireGetMineralProcessDetails(CompanyID)
            rptProcess.DataSource = Processes
            rptProcess.DataBind()
            Dim Components As DataSet = NashBLL.QuestionnaireGetMineralComponentDetails(CompanyID)
            rptComponent.DataSource = Components
            rptComponent.DataBind()
            panWolframite.Visible = True
            panMineralPurpose.Visible = True
            panQuestion5.Visible = True
            CheckMinerals()
        Else
            'Turning this mineral off so check to see if it was the last one selected and if so, then hide the rest of the form
            CheckMinerals()
            panWolframite.Visible = False
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
        NashBLL.AddPurposeLine(CompanyID)

        'Now rebind everything
        Dim Purposes As DataSet = NashBLL.QuestionnaireGetMineralPurposeDetails(CompanyID)
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
        Dim Purposes As DataSet = NashBLL.QuestionnaireGetMineralPurposeDetails(CompanyID)
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
        NashBLL.AddProcessLine(CompanyID)

        'Now rebind everything
        Dim Processes As DataSet = NashBLL.QuestionnaireGetMineralProcessDetails(CompanyID)
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
        Dim Processes As DataSet = NashBLL.QuestionnaireGetMineralProcessDetails(CompanyID)
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
        NashBLL.AddComponentLine(CompanyID)

        'Now rebind everything
        Dim Components As DataSet = NashBLL.QuestionnaireGetMineralComponentDetails(CompanyID)
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
        Dim Components As DataSet = NashBLL.QuestionnaireGetMineralComponentDetails(CompanyID)
        rptComponent.DataSource = Components
        rptComponent.DataBind()
    End Sub

    Protected Sub btnAddScrapSource_Click(sender As Object, e As EventArgs) Handles btnAddScrapSource.Click
        'Adds a new row to the table
        Dim LoopCount As Integer = 1
        Dim cboMinerals As DropDownList
        Dim txtDescription As TextBox
        Dim MineralID As Integer
        Dim hidItemID As HiddenField
        'Check what values we have already as we need to preserve them
        For Each Item As RepeaterItem In rptScrap.Items
            txtDescription = Item.FindControl("txtScrap")
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
            NashBLL.UpdateScrapLine(hidItemID.Value, _
                                           MineralID, _
                                           txtDescription.Text)
        Next
        'Now we can finally add our new line
        NashBLL.AddScrapLine(CompanyID)

        'Now rebind everything
        Dim ScrapList As DataSet = NashBLL.QuestionnaireGetMineralScrapDetails(CompanyID)
        rptScrap.DataSource = ScrapList
        rptScrap.DataBind()
    End Sub

    Protected Sub DeleteScrapLine(sender As Object, e As EventArgs)
        'Deletes a row from the table
        Dim LoopCount As Integer = 1
        Dim cboMinerals As DropDownList
        Dim txtDescription As TextBox
        Dim MineralID As Integer
        Dim hidItemID As HiddenField
        'Check what values we have already as we need to preserve them
        For Each Item As RepeaterItem In rptScrap.Items
            txtDescription = Item.FindControl("txtScrap")
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
            NashBLL.UpdateScrapLine(hidItemID.Value, _
                                           MineralID, _
                                           txtDescription.Text)
        Next
        'Now we can finally remove our line
        NashBLL.DeleteScrapLine(sender.CommandArgument)

        'Now rebind everything
        Dim ScrapList As DataSet = NashBLL.QuestionnaireGetMineralScrapDetails(CompanyID)
        rptScrap.DataSource = ScrapList
        rptScrap.DataBind()
    End Sub

    Protected Sub btnAddRecycled_Click(sender As Object, e As EventArgs) Handles btnAddRecycled.Click
        'Adds a new row to the table
        Dim LoopCount As Integer = 1
        Dim cboMinerals As DropDownList
        Dim txtDescription As TextBox
        Dim MineralID As Integer
        Dim hidItemID As HiddenField
        'Check what values we have already as we need to preserve them
        For Each Item As RepeaterItem In rptRecycled.Items
            txtDescription = Item.FindControl("txtRecycled")
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
            NashBLL.UpdateRecycleLine(hidItemID.Value, _
                                           MineralID, _
                                           txtDescription.Text)
        Next
        'Now we can finally add our new line
        NashBLL.AddRecycleLine(CompanyID)

        'Now rebind everything
        Dim RecycleList As DataSet = NashBLL.QuestionnaireGetMineralRecycleDetails(CompanyID)
        rptRecycled.DataSource = RecycleList
        rptRecycled.DataBind()
    End Sub

    Protected Sub DeleteRecyleLine(sender As Object, e As EventArgs)
        'Deletes a row from the table
        Dim LoopCount As Integer = 1
        Dim cboMinerals As DropDownList
        Dim txtDescription As TextBox
        Dim MineralID As Integer
        Dim hidItemID As HiddenField
        'Check what values we have already as we need to preserve them
        For Each Item As RepeaterItem In rptRecycled.Items
            txtDescription = Item.FindControl("txtRecycled")
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
            NashBLL.UpdateRecycleLine(hidItemID.Value, _
                                           MineralID, _
                                           txtDescription.Text)
        Next
        'Now we can finally remove our line
        NashBLL.DeleteRecycleLine(sender.CommandArgument)

        'Now rebind everything
        Dim RecycleList As DataSet = NashBLL.QuestionnaireGetMineralRecycleDetails(CompanyID)
        rptRecycled.DataSource = RecycleList
        rptRecycled.DataBind()
    End Sub

    Protected Sub btnAddExtraction_Click(sender As Object, e As EventArgs) Handles btnAddExtraction.Click
        'Adds a new row to the table
        Dim LoopCount As Integer = 1
        Dim cboMinerals As DropDownList
        Dim txtQuantity As TextBox
        Dim rdpExtractionDate As Telerik.Web.UI.RadDatePicker
        Dim cboMethodID As DropDownList
        Dim MineralID As Integer
        Dim hidItemID As HiddenField
        Dim MethodID As Integer
        'Check what values we have already as we need to preserve them
        For Each Item As RepeaterItem In rptExtraction.Items
            txtQuantity = Item.FindControl("txtQuantity")
            cboMinerals = Item.FindControl("cboMinerals")
            cboMethodID = Item.FindControl("cboExtractionMethod")
            rdpExtractionDate = Item.FindControl("rdpExtractionDate")
            hidItemID = Item.FindControl("hidItemID")
            'Now update this to the DB
            If txtQuantity.Text = "" Then
                txtQuantity.Text = "0"
            End If
            If cboMinerals.SelectedIndex = 0 Then
                MineralID = 0
            Else
                MineralID = cboMinerals.SelectedValue
            End If
            If cboMethodID.SelectedIndex = 0 Then
                MethodID = 0
            Else
                MethodID = cboMethodID.SelectedValue
            End If
            'Now update this line to the DB
            NashBLL.UpdateExtractionLine(hidItemID.Value, _
                                           MineralID, _
                                           txtQuantity.Text, _
                                           rdpExtractionDate.SelectedDate, _
                                           MethodID)
        Next
        'Now we can finally add our new line
        NashBLL.AddExtractionLine(CompanyID)

        'Now rebind everything
        Dim ExtractionList As DataSet = NashBLL.QuestionnaireGetExtractionDetails(CompanyID)
        rptExtraction.DataSource = ExtractionList
        rptExtraction.DataBind()
    End Sub

    Protected Sub DeleteExtractionLine(sender As Object, e As EventArgs)
        'Deletes a row from the table
        Dim LoopCount As Integer = 1
        Dim cboMinerals As DropDownList
        Dim txtQuantity As TextBox
        Dim rdpExtractionDate As Telerik.Web.UI.RadDatePicker
        Dim cboMethodID As DropDownList
        Dim MineralID As Integer
        Dim hidItemID As HiddenField
        Dim MethodID As Integer
        'Check what values we have already as we need to preserve them
        For Each Item As RepeaterItem In rptExtraction.Items
            txtQuantity = Item.FindControl("txtQuantity")
            cboMinerals = Item.FindControl("cboMinerals")
            cboMethodID = Item.FindControl("cboExtractionMethod")
            rdpExtractionDate = Item.FindControl("rdpExtractionDate")
            hidItemID = Item.FindControl("hidItemID")
            'Now update this to the DB
            If txtQuantity.Text = "" Then
                txtQuantity.Text = "0"
            End If
            If cboMinerals.SelectedIndex = 0 Then
                MineralID = 0
            Else
                MineralID = cboMinerals.SelectedValue
            End If
            If cboMethodID.SelectedIndex = 0 Then
                MethodID = 0
            Else
                MethodID = cboMethodID.SelectedValue
            End If
            'Now update this line to the DB
            NashBLL.UpdateExtractionLine(hidItemID.Value, _
                                           MineralID, _
                                           txtQuantity.Text, _
                                           rdpExtractionDate.SelectedDate, _
                                           MethodID)
        Next
        'Now we can finally remove our line
        NashBLL.DeleteExtractionLine(sender.CommandArgument)

        'Now rebind everything
        Dim ExtractionList As DataSet = NashBLL.QuestionnaireGetExtractionDetails(CompanyID)
        rptExtraction.DataSource = ExtractionList
        rptExtraction.DataBind()
    End Sub

    Protected Sub btnAddFacility_Click(sender As Object, e As EventArgs) Handles btnAddFacility.Click
        'Adds a new row to the table
        Dim LoopCount As Integer = 1
        Dim txtFacilityName As TextBox
        Dim txtLocation As TextBox
        Dim hidItemID As HiddenField
        'Check what values we have already as we need to preserve them
        For Each Item As RepeaterItem In rptFacility.Items
            txtFacilityName = Item.FindControl("txtFacilityName")
            txtLocation = Item.FindControl("txtLocation")
            hidItemID = Item.FindControl("hidItemID")
            'Now update this to the DB
            If txtFacilityName.Text = "" Then
                txtFacilityName.Text = "None"
            End If
            If txtLocation.Text = "" Then
                txtLocation.Text = "None"
            End If
            'Now update this line to the DB
            NashBLL.UpdateFacilityLine(hidItemID.Value, _
                                       txtFacilityName.Text, _
                                       txtLocation.Text)

        Next
        'Now we can finally add our new line
        NashBLL.AddFacilityLine(CompanyID)

        'Now rebind everything
        Dim FacilityList As DataSet = NashBLL.QuestionnaireGetFacilityDetails(CompanyID)
        rptFacility.DataSource = FacilityList
        rptFacility.DataBind()
    End Sub

    Protected Sub DeleteFacilityLine(sender As Object, e As EventArgs)
        'Deletes a row from the table
        Dim LoopCount As Integer = 1
        Dim txtFacilityName As TextBox
        Dim txtLocation As TextBox
        Dim hidItemID As HiddenField
        'Check what values we have already as we need to preserve them
        For Each Item As RepeaterItem In rptFacility.Items
            txtFacilityName = Item.FindControl("txtFacilityName")
            txtLocation = Item.FindControl("txtLocation")
            hidItemID = Item.FindControl("hidItemID")
            'Now update this to the DB
            If txtFacilityName.Text = "" Then
                txtFacilityName.Text = "None"
            End If
            If txtLocation.Text = "" Then
                txtLocation.Text = "None"
            End If
            'Now update this line to the DB
            NashBLL.UpdateFacilityLine(hidItemID.Value, _
                                       txtFacilityName.Text, _
                                       txtLocation.Text)

        Next
        'Now we can finally remove our line
        NashBLL.DeleteFacilityLine(sender.CommandArgument)

        'Now rebind everything
        Dim FacilityList As DataSet = NashBLL.QuestionnaireGetFacilityDetails(CompanyID)
        rptFacility.DataSource = FacilityList
        rptFacility.DataBind()
    End Sub

    Protected Sub btnAddTransporter_Click(sender As Object, e As EventArgs) Handles btnAddTransporter.Click
        'Adds a new row to the table
        Dim LoopCount As Integer = 1
        Dim txtTransporterName As TextBox
        Dim txtTransporterAddress As TextBox
        Dim hidItemID As HiddenField
        'Check what values we have already as we need to preserve them
        For Each Item As RepeaterItem In rptTransport.Items
            txtTransporterName = Item.FindControl("txtTransporterName")
            txtTransporterAddress = Item.FindControl("txtTransporterAddress")
            hidItemID = Item.FindControl("hidItemID")
            'Now update this to the DB
            If txtTransporterName.Text = "" Then
                txtTransporterName.Text = "None"
            End If
            If txtTransporterAddress.Text = "" Then
                txtTransporterAddress.Text = "None"
            End If
            'Now update this line to the DB
            NashBLL.UpdateTransportLine(hidItemID.Value, _
                                       txtTransporterName.Text, _
                                       txtTransporterAddress.Text)

        Next
        'Now we can finally add our new line
        NashBLL.AddTransportLine(CompanyID)

        'Now rebind everything
        Dim TransporterList As DataSet = NashBLL.QuestionnaireGetTransportDetails(CompanyID)
        rptTransport.DataSource = TransporterList
        rptTransport.DataBind()
    End Sub

    Protected Sub DeleteTransportLine(sender As Object, e As EventArgs)
        'Deletes a row from the table
        Dim LoopCount As Integer = 1
        Dim txtTransporterName As TextBox
        Dim txtTransporterAddress As TextBox
        Dim hidItemID As HiddenField
        'Check what values we have already as we need to preserve them
        For Each Item As RepeaterItem In rptTransport.Items
            txtTransporterName = Item.FindControl("txtTransporterName")
            txtTransporterAddress = Item.FindControl("txtTransporterAddress")
            hidItemID = Item.FindControl("hidItemID")
            'Now update this to the DB
            If txtTransporterName.Text = "" Then
                txtTransporterName.Text = "None"
            End If
            If txtTransporterAddress.Text = "" Then
                txtTransporterAddress.Text = "None"
            End If
            'Now update this line to the DB
            NashBLL.UpdateTransportLine(hidItemID.Value, _
                                       txtTransporterName.Text, _
                                       txtTransporterAddress.Text)

        Next
        'Now we can finally remove our line
        NashBLL.DeleteTransportLine(sender.CommandArgument)

        'Now rebind everything
        Dim TransporterList As DataSet = NashBLL.QuestionnaireGetTransportDetails(CompanyID)
        rptTransport.DataSource = TransporterList
        rptTransport.DataBind()
    End Sub

    Protected Sub btnAddOtherPayment_Click(sender As Object, e As EventArgs) Handles btnAddOtherPayment.Click
        'Adds a new row to the table
        Dim LoopCount As Integer = 1
        Dim txtPaymentAmount As TextBox
        Dim txtPaymentDetails As TextBox
        Dim hidItemID As HiddenField
        'Check what values we have already as we need to preserve them
        For Each Item As RepeaterItem In rptOtherPayment.Items
            txtPaymentAmount = Item.FindControl("txtPaymentAmount")
            txtPaymentDetails = Item.FindControl("txtPaymentDetails")
            hidItemID = Item.FindControl("hidItemID")
            'Now update this to the DB
            If txtPaymentAmount.Text = "" Then
                txtPaymentAmount.Text = "None"
            End If
            If txtPaymentDetails.Text = "" Then
                txtPaymentDetails.Text = "None"
            End If
            'Now update this line to the DB
            NashBLL.UpdateOtherPaymentLine(hidItemID.Value, _
                                       txtPaymentAmount.Text, _
                                       txtPaymentDetails.Text)

        Next
        'Now we can finally add our new line
        NashBLL.AddOtherPaymentLine(CompanyID)

        'Now rebind everything
        Dim OtherPaymentList As DataSet = NashBLL.QuestionnaireGetOtherPaymentDetails(CompanyID)
        rptOtherPayment.DataSource = OtherPaymentList
        rptOtherPayment.DataBind()
    End Sub

    Protected Sub DeleteOtherPaymentLine(sender As Object, e As EventArgs)
        'Deletes a row from the table
        Dim LoopCount As Integer = 1
        Dim txtPaymentAmount As TextBox
        Dim txtPaymentDetails As TextBox
        Dim hidItemID As HiddenField
        'Check what values we have already as we need to preserve them
        For Each Item As RepeaterItem In rptOtherPayment.Items
            txtPaymentAmount = Item.FindControl("txtPaymentAmount")
            txtPaymentDetails = Item.FindControl("txtPaymentDetails")
            hidItemID = Item.FindControl("hidItemID")
            'Now update this to the DB
            If txtPaymentAmount.Text = "" Then
                txtPaymentAmount.Text = "None"
            End If
            If txtPaymentDetails.Text = "" Then
                txtPaymentDetails.Text = "None"
            End If
            'Now update this line to the DB
            NashBLL.UpdateOtherPaymentLine(hidItemID.Value, _
                                       txtPaymentAmount.Text, _
                                       txtPaymentDetails.Text)

        Next
        'Now we can finally remove our line
        NashBLL.DeleteOtherPaymentLine(sender.CommandArgument)

        'Now rebind everything
        Dim OtherPaymentList As DataSet = NashBLL.QuestionnaireGetOtherPaymentDetails(CompanyID)
        rptOtherPayment.DataSource = OtherPaymentList
        rptOtherPayment.DataBind()
    End Sub

    Protected Sub btnAddOtherTax_Click(sender As Object, e As EventArgs) Handles btnAddOtherTax.Click
        'Adds a new row to the table
        Dim LoopCount As Integer = 1
        Dim txtPaymentAmount As TextBox
        Dim txtPaymentDetails As TextBox
        Dim hidItemID As HiddenField
        'Check what values we have already as we need to preserve them
        For Each Item As RepeaterItem In rptOtherTaxes.Items
            txtPaymentAmount = Item.FindControl("txtPaymentAmount")
            txtPaymentDetails = Item.FindControl("txtPaymentDetails")
            hidItemID = Item.FindControl("hidItemID")
            'Now update this to the DB
            If txtPaymentAmount.Text = "" Then
                txtPaymentAmount.Text = "None"
            End If
            If txtPaymentDetails.Text = "" Then
                txtPaymentDetails.Text = "None"
            End If
            'Now update this line to the DB
            NashBLL.UpdateOtherTaxLine(hidItemID.Value, _
                                       txtPaymentAmount.Text, _
                                       txtPaymentDetails.Text)

        Next
        'Now we can finally add our new line
        NashBLL.AddOtherTaxLine(CompanyID)

        'Now rebind everything
        Dim OtherTaxList As DataSet = NashBLL.QuestionnaireGetOtherTaxDetails(CompanyID)
        rptOtherTaxes.DataSource = OtherTaxList
        rptOtherTaxes.DataBind()
    End Sub

    Protected Sub DeleteOtherTaxLine(sender As Object, e As EventArgs)
        'Deletes a row from the table
        Dim LoopCount As Integer = 1
        Dim txtPaymentAmount As TextBox
        Dim txtPaymentDetails As TextBox
        Dim hidItemID As HiddenField
        'Check what values we have already as we need to preserve them
        For Each Item As RepeaterItem In rptOtherTaxes.Items
            txtPaymentAmount = Item.FindControl("txtPaymentAmount")
            txtPaymentDetails = Item.FindControl("txtPaymentDetails")
            hidItemID = Item.FindControl("hidItemID")
            'Now update this to the DB
            If txtPaymentAmount.Text = "" Then
                txtPaymentAmount.Text = "None"
            End If
            If txtPaymentDetails.Text = "" Then
                txtPaymentDetails.Text = "None"
            End If
            'Now update this line to the DB
            NashBLL.UpdateOtherTaxLine(hidItemID.Value, _
                                       txtPaymentAmount.Text, _
                                       txtPaymentDetails.Text)

        Next
        'Now we can finally remove our line
        NashBLL.DeleteOtherTaxLine(sender.CommandArgument)

        'Now rebind everything
        Dim OtherTaxList As DataSet = NashBLL.QuestionnaireGetOtherTaxDetails(CompanyID)
        rptOtherTaxes.DataSource = OtherTaxList
        rptOtherTaxes.DataBind()
    End Sub

    Protected Sub btnAddTax_Click(sender As Object, e As EventArgs) Handles btnAddTax.Click
        'Adds a new row to the table
        Dim LoopCount As Integer = 1
        Dim cboCountryID As DropDownList
        Dim txtTaxDetails As TextBox
        Dim hidItemID As HiddenField
        Dim CountryID As Integer
        'Check what values we have already as we need to preserve them
        For Each Item As RepeaterItem In rptTaxes.Items
            cboCountryID = Item.FindControl("cboCountryID")
            txtTaxDetails = Item.FindControl("txtTaxDetails")
            hidItemID = Item.FindControl("hidItemID")
            'Now update this to the DB
            If txtTaxDetails.Text = "" Then
                txtTaxDetails.Text = "None"
            End If
            If cboCountryID.SelectedIndex = 0 Then
                CountryID = 0
            Else
                CountryID = cboCountryID.SelectedValue
            End If
            'Now update this line to the DB
            NashBLL.UpdateTaxLine(hidItemID.Value, _
                                       CountryID, _
                                       txtTaxDetails.Text)

        Next
        'Now we can finally add our new line
        NashBLL.AddTaxLine(CompanyID)

        'Now rebind everything
        Dim TaxList As DataSet = NashBLL.QuestionnaireGetTaxDetails(CompanyID)
        rptTaxes.DataSource = TaxList
        rptTaxes.DataBind()
    End Sub

    Protected Sub DeleteTaxLine(sender As Object, e As EventArgs)
        'Deletes a row from the table
        Dim LoopCount As Integer = 1
        Dim cboCountryID As DropDownList
        Dim txtTaxDetails As TextBox
        Dim hidItemID As HiddenField
        Dim CountryID As Integer
        'Check what values we have already as we need to preserve them
        For Each Item As RepeaterItem In rptTaxes.Items
            cboCountryID = Item.FindControl("cboCountryID")
            txtTaxDetails = Item.FindControl("txtTaxDetails")
            hidItemID = Item.FindControl("hidItemID")
            'Now update this to the DB
            If txtTaxDetails.Text = "" Then
                txtTaxDetails.Text = "None"
            End If
            If cboCountryID.SelectedIndex = 0 Then
                CountryID = 0
            Else
                CountryID = cboCountryID.SelectedValue
            End If
            'Now update this line to the DB
            NashBLL.UpdateTaxLine(hidItemID.Value, _
                                       CountryID, _
                                       txtTaxDetails.Text)

        Next
        'Now we can finally remove our line
        NashBLL.DeleteTaxLine(sender.CommandArgument)

        'Now rebind everything
        Dim TaxList As DataSet = NashBLL.QuestionnaireGetTaxDetails(CompanyID)
        rptTaxes.DataSource = TaxList
        rptTaxes.DataBind()
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
            Dim ParentCompanies As DataSet = NashBLL.QuestionnaireGetParentCompanyDetails(CompanyID)
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
        NashBLL.AddParentCompanyLine(CompanyID)

        'Now rebind everything
        Dim ParentCompanies As DataSet = NashBLL.QuestionnaireGetParentCompanyDetails(CompanyID)
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
        Dim ParentCompanies As DataSet = NashBLL.QuestionnaireGetParentCompanyDetails(CompanyID)
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
        NashBLL.AddShareholderLine(CompanyID)

        'Now rebind everything
        Dim Shareholders As DataSet = NashBLL.QuestionnaireGetParentShareholderDetails(CompanyID)
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
        Dim Shareholders As DataSet = NashBLL.QuestionnaireGetParentShareholderDetails(CompanyID)
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
        NashBLL.AddDirectorLine(CompanyID)

        'Now rebind everything
        Dim Directors As DataSet = NashBLL.QuestionnaireGetDirectorDetails(CompanyID)
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
        Dim Directors As DataSet = NashBLL.QuestionnaireGetDirectorDetails(CompanyID)
        rptDirectors.DataSource = Directors
        rptDirectors.DataBind()
    End Sub


#End Region

#Region " Manage Government Employees "

    Protected Sub chkGovernmentEmployee_CheckedChanged(sender As Object, e As EventArgs) Handles chkGovernmentEmployee.CheckedChanged
        If chkGovernmentEmployee.Checked Then
            panGovernmanetEmployee.Visible = True
            gbLoopCount = 0
            Dim Relatives As DataSet = NashBLL.QuestionnaireGetGovtEmployeeDetails(CompanyID)
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
        NashBLL.AddRelativeLine(CompanyID)

        'Now rebind everything
        Dim Relatives As DataSet = NashBLL.QuestionnaireGetGovtEmployeeDetails(CompanyID)
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
        Dim Relatives As DataSet = NashBLL.QuestionnaireGetGovtEmployeeDetails(CompanyID)
        rptGovtEmployees.DataSource = Relatives
        rptGovtEmployees.DataBind()
    End Sub

#End Region

#Region " Manage Scrap Radios "

    Protected Sub rblScrap_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblScrap.SelectedIndexChanged
        If rblScrap.SelectedIndex = 1 Then
            'Reset the loop count and go and get the relatives list
            gbLoopCount = 0
            Dim ScrapList As DataSet = NashBLL.QuestionnaireGetMineralScrapDetails(CompanyID)
            rptScrap.DataSource = ScrapList
            rptScrap.DataBind()
            panScrap.Visible = True
        Else
            panScrap.Visible = False
        End If
    End Sub

#End Region

#Region " Manage Recycled Radios "

    Protected Sub rblRecycled_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblRecycled.SelectedIndexChanged
        If rblRecycled.SelectedIndex = 1 Then
            gbLoopCount = 0
            Dim RecycleList As DataSet = NashBLL.QuestionnaireGetMineralRecycleDetails(CompanyID)
            rptRecycled.DataSource = RecycleList
            rptRecycled.DataBind()
            panRecycled.Visible = True
        Else
            panRecycled.Visible = False
        End If
    End Sub

#End Region

#Region " Manage Countries "

    Protected Sub CheckCountry(sender As Object, e As EventArgs)
        'Dim NoCountry As Boolean = True
        'For Each Item As RepeaterItem In rptDangerousCountries.Items
        'Dim ButtonList As RadioButtonList = Item.FindControl("rblDangerousCountry")
        'If ButtonList.SelectedIndex = 1 Then
        'NoCountry = False
        'End If
        'Next
        'Now check to see if we can show or hide our panel
        'If NoCountry Then
        'panQuestion5.Visible = False
        'Else
        'panQuestion5.Visible = True
        'End If
        CheckMinerals()
    End Sub

#End Region

#Region " Manage Uploads "

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        lblErrorMessage.Text = "Executed the upload function"
        For Each File As UploadedFile In rauUploader.UploadedFiles
            File.SaveAs(MapPath("~/userfiles/" & CompanyID & Now.ToString("_dd_MMM_yyyy_HH_mm_ss_") & File.FileName), True)
        Next

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

    Protected Sub rptScrap_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptScrap.ItemDataBound
        Dim LoopCount As Integer = 1
        Dim cboMinerals As DropDownList
        Dim txtDescription As TextBox
        Dim hidItemID As HiddenField
        Dim btnDeleteScrap As Button
        Dim drv As DataRowView
        Dim Minerals As DataSet = NashBLL.GetMinerals

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            'This is a data item so we can populate the form boxes now
            cboMinerals = e.Item.FindControl("cboMinerals")
            txtDescription = e.Item.FindControl("txtScrap")
            hidItemID = e.Item.FindControl("hidItemID")
            btnDeleteScrap = e.Item.FindControl("btnDeleteScrap")
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
            btnDeleteScrap.CommandArgument = drv("ItemID")
            hidItemID.Value = drv("ItemID")
            If gbLoopCount = 0 Then
                btnDeleteScrap.Visible = False
            End If
            gbLoopCount += 1
        End If
    End Sub

    Protected Sub rptRecycled_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptRecycled.ItemDataBound
        Dim LoopCount As Integer = 1
        Dim cboMinerals As DropDownList
        Dim txtDescription As TextBox
        Dim hidItemID As HiddenField
        Dim btnDeleteRecycle As Button
        Dim drv As DataRowView
        Dim Minerals As DataSet = NashBLL.GetMinerals

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            'This is a data item so we can populate the form boxes now
            cboMinerals = e.Item.FindControl("cboMinerals")
            txtDescription = e.Item.FindControl("txtRecycled")
            hidItemID = e.Item.FindControl("hidItemID")
            btnDeleteRecycle = e.Item.FindControl("btnDeleteRecycle")
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
            btnDeleteRecycle.CommandArgument = drv("ItemID")
            hidItemID.Value = drv("ItemID")
            If gbLoopCount = 0 Then
                btnDeleteRecycle.Visible = False
            End If
            gbLoopCount += 1
        End If
    End Sub

    Protected Sub rptExtraction_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptExtraction.ItemDataBound
        Dim LoopCount As Integer = 1
        Dim cboMinerals As DropDownList
        Dim txtQuantity As TextBox
        Dim rdpExtractionDate As Telerik.Web.UI.RadDatePicker
        Dim cboExtractionMethod As DropDownList
        Dim hidItemID As HiddenField
        Dim btnDeleteExtraction As Button
        Dim drv As DataRowView
        Dim Minerals As DataSet = NashBLL.GetMinerals
        Dim ExtractionMethods = NashBLL.GetExtractionMethods
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            'This is a data item so we can populate the form boxes now
            cboMinerals = e.Item.FindControl("cboMinerals")
            txtQuantity = e.Item.FindControl("txtQuantity")
            rdpExtractionDate = e.Item.FindControl("rdpExtractionDate")
            cboExtractionMethod = e.Item.FindControl("cboExtractionMethod")
            hidItemID = e.Item.FindControl("hidItemID")
            btnDeleteExtraction = e.Item.FindControl("btnDeleteExtraction")
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
            cboExtractionMethod.DataSource = ExtractionMethods
            cboExtractionMethod.DataValueField = "ExtractionID"
            cboExtractionMethod.DataTextField = "Method"
            cboExtractionMethod.DataBind()
            cboExtractionMethod.Items.Insert(0, NewItem)
            cboExtractionMethod.SelectedValue = drv("MethodID")
            If CInt(drv("Quantity")) <> 0 Then
                txtQuantity.Text = drv("Quantity")
            Else
                txtQuantity.Text = ""
            End If

            If Not IsDBNull(drv("DateOfExtraction")) Then
                rdpExtractionDate.DbSelectedDate = CDate(drv("DateOfExtraction")).ToString("dd MMM yyyy")
            Else
                rdpExtractionDate.SelectedDate = Now
            End If
            'Set our managed values
            btnDeleteExtraction.CommandArgument = drv("ItemID")
            hidItemID.Value = drv("ItemID")
            If gbLoopCount = 0 Then
                btnDeleteExtraction.Visible = False
            End If
            gbLoopCount += 1
        End If
    End Sub

    Protected Sub rptFacility_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptFacility.ItemDataBound
        Dim txtFacilityName As TextBox
        Dim txtLocation As TextBox
        Dim btnDeleteFacility As Button
        Dim hidItemID As HiddenField
        Dim drv As DataRowView

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            'This is a data item so we can populate the text boxes now
            txtFacilityName = e.Item.FindControl("txtFacilityName")
            txtLocation = e.Item.FindControl("txtLocation")
            btnDeleteFacility = e.Item.FindControl("btnDeleteFacility")
            hidItemID = e.Item.FindControl("hidItemID")
            drv = e.Item.DataItem
            'Now complete our details
            hidItemID.Value = drv("ItemID")
            If UCase(drv("FacilityName")) <> "NONE" Then
                'A value was writtent to the DB so we need to re-populate the item now
                txtFacilityName.Text = drv("FacilityName")
            Else
                'No value entered yet so show empty box
                txtFacilityName.Text = ""
            End If

            If UCase(drv("FacilityLocation")) <> "NONE" Then
                'A value was written to the DB so we need to re-populate the item now
                txtLocation.Text = drv("FacilityLocation")
            Else
                'No value entered yet so show empty box
                txtLocation.Text = ""
            End If

            'Now set our delete button
            btnDeleteFacility.CommandArgument = drv("ItemID")
            If gbLoopCount = 0 Then
                'This is the first item in the list and that cannot be deleted
                btnDeleteFacility.Visible = False
            End If
            gbLoopCount += 1
        End If
    End Sub

    Protected Sub rptTransport_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptTransport.ItemDataBound
        Dim txtTransporterName As TextBox
        Dim txtTransporterAddress As TextBox
        Dim btnDeleteTransport As Button
        Dim hidItemID As HiddenField
        Dim drv As DataRowView

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            'This is a data item so we can populate the text boxes now
            txtTransporterName = e.Item.FindControl("txtTransporterName")
            txtTransporterAddress = e.Item.FindControl("txtTransporterAddress")
            btnDeleteTransport = e.Item.FindControl("btnDeleteTransport")
            hidItemID = e.Item.FindControl("hidItemID")
            drv = e.Item.DataItem
            'Now complete our details
            hidItemID.Value = drv("ItemID")
            If UCase(drv("TransporterName")) <> "NONE" Then
                'A value was writtent to the DB so we need to re-populate the item now
                txtTransporterName.Text = drv("TransporterName")
            Else
                'No value entered yet so show empty box
                txtTransporterName.Text = ""
            End If

            If UCase(drv("TransporterAddress")) <> "NONE" Then
                'A value was written to the DB so we need to re-populate the item now
                txtTransporterAddress.Text = drv("TransporterAddress")
            Else
                'No value entered yet so show empty box
                txtTransporterAddress.Text = ""
            End If

            'Now set our delete button
            btnDeleteTransport.CommandArgument = drv("ItemID")
            If gbLoopCount = 0 Then
                'This is the first item in the list and that cannot be deleted
                btnDeleteTransport.Visible = False
            End If
            gbLoopCount += 1
        End If
    End Sub

    Protected Sub rptOtherPayment_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptOtherPayment.ItemDataBound
        Dim txtPaymentAmount As TextBox
        Dim txtPaymentDetails As TextBox
        Dim btnDeleteOtherPayment As Button
        Dim hidItemID As HiddenField
        Dim drv As DataRowView

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            'This is a data item so we can populate the text boxes now
            txtPaymentAmount = e.Item.FindControl("txtPaymentAmount")
            txtPaymentDetails = e.Item.FindControl("txtPaymentDetails")
            btnDeleteOtherPayment = e.Item.FindControl("btnDeleteOtherPayment")
            hidItemID = e.Item.FindControl("hidItemID")
            drv = e.Item.DataItem
            'Now complete our details
            hidItemID.Value = drv("ItemID")
            If UCase(drv("PaymentAmount")) <> "NONE" Then
                'A value was written to the DB so we need to re-populate the item now
                txtPaymentAmount.Text = drv("PaymentAmount")
            Else
                'No value entered yet so show empty box
                txtPaymentAmount.Text = ""
            End If

            If UCase(drv("PaymentDetails")) <> "NONE" Then
                'A value was written to the DB so we need to re-populate the item now
                txtPaymentDetails.Text = drv("PaymentDetails")
            Else
                'No value entered yet so show empty box
                txtPaymentDetails.Text = ""
            End If

            'Now set our delete button
            btnDeleteOtherPayment.CommandArgument = drv("ItemID")
            If gbLoopCount = 0 Then
                'This is the first item in the list and that cannot be deleted
                btnDeleteOtherPayment.Visible = False
            End If
            gbLoopCount += 1
        End If
    End Sub

    Protected Sub rptOtherTaxes_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptOtherTaxes.ItemDataBound
        Dim txtPaymentAmount As TextBox
        Dim txtPaymentDetails As TextBox
        Dim btnDeleteOtherTax As Button
        Dim hidItemID As HiddenField
        Dim drv As DataRowView

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            'This is a data item so we can populate the text boxes now
            txtPaymentAmount = e.Item.FindControl("txtPaymentAmount")
            txtPaymentDetails = e.Item.FindControl("txtPaymentDetails")
            btnDeleteOtherTax = e.Item.FindControl("btnDeleteOtherTax")
            hidItemID = e.Item.FindControl("hidItemID")
            drv = e.Item.DataItem
            'Now complete our details
            hidItemID.Value = drv("ItemID")
            If UCase(drv("PaymentAmount")) <> "NONE" Then
                'A value was written to the DB so we need to re-populate the item now
                txtPaymentAmount.Text = drv("PaymentAmount")
            Else
                'No value entered yet so show empty box
                txtPaymentAmount.Text = ""
            End If

            If UCase(drv("PaymentDetails")) <> "NONE" Then
                'A value was written to the DB so we need to re-populate the item now
                txtPaymentDetails.Text = drv("PaymentDetails")
            Else
                'No value entered yet so show empty box
                txtPaymentDetails.Text = ""
            End If

            'Now set our delete button
            btnDeleteOtherTax.CommandArgument = drv("ItemID")
            If gbLoopCount = 0 Then
                'This is the first item in the list and that cannot be deleted
                btnDeleteOtherTax.Visible = False
            End If
            gbLoopCount += 1
        End If
    End Sub

    Protected Sub rptTaxes_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptTaxes.ItemDataBound
        Dim LoopCount As Integer = 1
        Dim cboCountryID As DropDownList
        Dim txtTaxDetails As TextBox
        Dim hidItemID As HiddenField
        Dim btnDeleteTax As Button
        Dim drv As DataRowView
        Dim Countries As DataSet = NashBLL.GetCountries

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            'This is a data item so we can populate the form boxes now
            cboCountryID = e.Item.FindControl("cboCountryID")
            txtTaxDetails = e.Item.FindControl("txtTaxDetails")
            hidItemID = e.Item.FindControl("hidItemID")
            btnDeleteTax = e.Item.FindControl("btnDeleteTax")
            'Get our data row now
            drv = e.Item.DataItem
            'Now we need to set our minerals menu
            cboCountryID.DataSource = Countries
            cboCountryID.DataValueField = "CountryID"
            cboCountryID.DataTextField = "CountryName"
            cboCountryID.DataBind()
            cboCountryID.SelectedValue = drv("CountryID")
            Dim NewItem As New ListItem With {.Text = "--- Please Select ---", .Value = ""}
            cboCountryID.Items.Insert(0, NewItem)
            If UCase(drv("TaxDetails")) <> "NONE" Then
                txtTaxDetails.Text = drv("TaxDetails")
            Else
                txtTaxDetails.Text = ""
            End If
            'Set our managed values
            btnDeleteTax.CommandArgument = drv("ItemID")
            hidItemID.Value = drv("ItemID")
            If gbLoopCount = 0 Then
                btnDeleteTax.Visible = False
            End If
            gbLoopCount += 1
        End If
    End Sub

#End Region

End Class
