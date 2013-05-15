Option Strict Off
Option Explicit On
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  Page Title       : questionnairepopup.aspx.vb        '
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
Imports System.Reflection

Partial Class standardquestionnaire
    Inherits System.Web.UI.Page
    Public gbLoopCount As Integer
    Public CompanyID As Integer
    Public ReadOnlyMode As Boolean = False

#Region " User Defined Functions "

    Private Function CheckMinerals() As Boolean
        'We need to check the minerals and the countries as a pair to determine what we show next
        Dim NoConflictSelected = True
        Dim NoCountrySelected = True

        For Each Item As RepeaterItem In rptDangerousCountries.Items
            'Check to see if any of our countries were chosen
            Dim ButtonList As RadioButtonList = Item.FindControl("rblDangerousCountry")
            If ButtonList.SelectedIndex = 1 Then
                NoCountrySelected = False
            End If
        Next

        For Each Item As RepeaterItem In rptMinerals.Items
            'Check to see if any of our minerals were chosen
            Dim ButtonList As RadioButtonList = Item.FindControl("rblMineral")
            Dim panMineral As Panel = Item.FindControl("panMineral")
            If ButtonList.SelectedIndex = 1 Then
                NoConflictSelected = False
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
            btnNext.CommandArgument = 5
            'btnPrev.CommandArgument = 1
            Return False
        End If
    End Function

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        'Always reset our global loopcount
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

        If NashBLL.ThisIsNotOneOfMyCompanies(Session("ContactID"), CompanyID) Then
            hidReadOnly.Value = "True"
        End If

        If hidReadOnly.Value = "True" Then
            ReadOnlyMode = True
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

            'Now bind all the repeaters
            BindRepeaters()
            'Now go and get the questionnaire data
            LoadQuestionnaire()
            'Set our initial navigation configuration
            panPage1.Visible = True
            btnNext.CommandArgument = 1
            btnTopNext.CommandArgument = 1
            btnPrev.Visible = False
            btnTopPrev.Visible = False
            divProgressbar.Attributes.Add("style", "width: 16.6%;")
            'We need to check if our viewer can fill any items on this form
            If ReadOnlyMode Then
                'This is someone from another company viewing my company questionnaire so make it read only
                'or the original company viewing after it was saved & closed
                For Each Control In panForm.Controls
                    DisableControls(Control)
                Next
                'Hide these buttons as we're now read only
                btnSave.Visible = False
                btnClose.Visible = False

            End If
        End If
    End Sub

    Private Sub DisableControls(ByVal control As System.Web.UI.Control)
       
        For Each Item As System.Web.UI.Control In control.Controls

            'Get the Enabled property by reflection.
            Dim ControlType As Type = Item.GetType
            Dim ControlProperty As PropertyInfo = ControlType.GetProperty("Enabled")
            'Set it to False to disable the control or hide it if its a button or linkbutton.
            If Not ControlProperty Is Nothing Then
                If ControlType.Name.ToLower = "linkbutton" OrElse ControlType.Name.ToLower = "button" Then
                    'This is a linkbutton or a button inside a repeater or panel
                    Item.Visible = False
                Else
                    'Not a button or linkbutton so show it but make it disabled
                    ControlProperty.SetValue(Item, False, Nothing)
                End If
            End If
            ' Recurse into child controls.
            If Item.Controls.Count > 0 Then
                'This is possibly a repeater or panel control that has other child elements
                Me.DisableControls(Item)
            End If

        Next

    End Sub

    Private Sub LoadQuestionnaire()
        Dim MyData As DataSet = NashBLL.GetMyStandardQuestionnaire(CompanyID, Session("ContactID"))
        Dim FormData As DataRow = MyData.Tables(0).Rows(0)
        Dim LoopCount As Integer = 0
        If UCase(FormData("CompanyName")) <> "NONE" Then
            txtCompanyName.Text = FormData("CompanyName")
        Else
            txtCompanyName.Text = ""
        End If

        If UCase(FormData("CompanyNumber")) <> "NONE" Then
            txtCompanyNumber.Text = FormData("CompanyNumber")
        Else
            txtCompanyNumber.Text = ""
        End If

        If UCase(FormData("Address1")) <> "NONE" Then
            txtAddress1.Text = FormData("Address1")
        Else
            txtAddress1.Text = ""
        End If

        If UCase(FormData("Address2")) <> "NONE" Then
            txtAddress2.Text = FormData("Address2")
        Else
            txtAddress2.Text = ""
        End If

        If UCase(FormData("District")) <> "NONE" Then
            txtDistrict.Text = FormData("District")
        Else
            txtDistrict.Text = ""
        End If

        If UCase(FormData("CityName")) <> "NONE" Then
            txtCity.Text = FormData("CityName")
        Else
            txtCity.Text = ""
        End If

        If UCase(FormData("Postcode")) <> "NONE" Then
            txtPostcode.Text = FormData("Postcode")
        Else
            txtPostcode.Text = ""
        End If

        If UCase(FormData("StateRegion")) <> "NONE" Then
            txtState.Text = FormData("StateRegion")
        Else
            txtState.Text = ""
        End If

        If CInt(FormData("CountryID")) <> 0 Then
            cboCountries.SelectedValue = FormData("CountryID")
        Else
            cboCountries.SelectedIndex = 0
        End If

        If UCase(FormData("TelephoneNumber")) <> "NONE" Then
            txtTelephone.Text = FormData("TelephoneNumber")
        Else
            txtTelephone.Text = ""
        End If

        If UCase(FormData("FaxNumber")) <> "NONE" Then
            txtFax.Text = FormData("FaxNumber")
        Else
            txtFax.Text = ""
        End If

        If UCase(FormData("WebAddress")) <> "NONE" Then
            txtWebAddress.Text = FormData("WebAddress")
        Else
            txtWebAddress.Text = ""
        End If

        If CInt(FormData("BusinessTypeID")) <> 0 Then
            cboBusinessType.SelectedValue = FormData("BusinessTypeID")
        Else
            cboBusinessType.SelectedIndex = 0
        End If

        If UCase(FormData("ContactPerson")) <> "NONE" Then
            txtContact.Text = FormData("ContactPerson")
        Else
            txtContact.Text = ""
        End If

        If UCase(FormData("ContactEmail")) <> "NONE" Then
            txtContactEmail.Text = FormData("ContactEmail")
        Else
            txtContactEmail.Text = ""
        End If

        If UCase(FormData("CountryList")) <> "NONE" Then
            txtCountryList.Text = FormData("CountryList")
        Else
            txtCountryList.Text = ""
        End If

        If CInt(FormData("ParentCompany")) <> 0 Then
            rblParent.SelectedIndex = 1
            panParentCompanies.Visible = True
        Else
            rblParent.SelectedIndex = 0
            panParentCompanies.Visible = False
        End If

        If CInt(FormData("GovernmentEmployees")) <> 0 Then
            chkGovernmentEmployee.Checked = True
            panGovernmanetEmployee.Visible = True
        Else
            chkGovernmentEmployee.Checked = False
            panGovernmanetEmployee.Visible = False
        End If

        If FormData("ConflictMinerals") <> "0" Then
            'Only check these if there was one chosen previously
            Dim ConflictMinerals As Array = Split(FormData("ConflictMinerals"), ",")
            For Each Item As RepeaterItem In rptMinerals.Items
                Dim ButtonList As RadioButtonList = Item.FindControl("rblMineral")
                Dim panMineral As Panel = Item.FindControl("panMineral")
                For LoopCount = LBound(ConflictMinerals) To UBound(ConflictMinerals)
                    If CInt(ConflictMinerals(LoopCount)) = CInt(ButtonList.Attributes("MineralID")) Then
                        'This was selected so we need to enable it and show its panel
                        ButtonList.SelectedIndex = 1
                        panMineral.Visible = True
                    End If
                Next
            Next
        End If

        If FormData("DangerousCountries") <> "0" Then
            'Only check these if there was one chosen previously
            Dim DangerousCountries As Array = Split(FormData("DangerousCountries"), ",")
            For Each Item As RepeaterItem In rptDangerousCountries.Items
                Dim ButtonList As RadioButtonList = Item.FindControl("rblDangerousCountry")
                For LoopCount = LBound(DangerousCountries) To UBound(DangerousCountries)
                    If CInt(DangerousCountries(LoopCount)) = CInt(ButtonList.Attributes("CountryID")) Then
                        'This was selected so we need to enable it and show its panel
                        ButtonList.SelectedIndex = 1
                    End If
                Next
            Next
        End If

        If CInt(FormData("SmelterList")) = 0 Then
            rblSmelterList.SelectedIndex = 0
        Else
            rblSmelterList.SelectedIndex = 1
        End If

        If CInt(FormData("Scrap")) = 0 Then
            rblScrap.SelectedIndex = 0
            panScrap.Visible = False
        Else
            rblScrap.SelectedIndex = 1
            panScrap.Visible = True
        End If

        If CInt(FormData("Recycled")) = 0 Then
            rblRecycled.SelectedIndex = 0
            panRecycled.Visible = False
        Else
            rblRecycled.SelectedIndex = 1
            panRecycled.Visible = True
        End If

        If UCase(FormData("IndependentAudit")) <> "NONE" Then
            rblIndependent.SelectedIndex = 1
            panIndependentAudit.Visible = True
            txtIndependentAudit.Text = FormData("IndependentAudit")
        Else
            rblIndependent.SelectedIndex = 0
            panIndependentAudit.Visible = False
            txtIndependentAudit.Text = ""
        End If

        If UCase(FormData("TransportCountries")) <> "NONE" Then
            txtTransportCountries.Text = FormData("TransportCountries")
        Else
            txtTransportCountries.Text = ""
        End If

        If UCase(FormData("SupplyChain")) <> "NONE" Then
            txtIntermediaries.Text = FormData("SupplyChain")
        Else
            txtIntermediaries.Text = ""
        End If
        'Now we need to decide if we have it closed or not
        If Not IsDBNull(FormData("DateCompleted")) Then
            'This questionnaire is now closed
            chkSignOff.Checked = True
            hidReadOnly.Value = "True"
            ReadOnlyMode = True
            litUsername.Text = NashBLL.GetQuestionnaireCloser(CompanyID)
            btnClose.Visible = False
        Else
            litUsername.Text = Session("FirstName") & " " & Session("Surname")
        End If

    End Sub

    Private Sub BindRepeaters()
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
        Dim Shareholders As DataSet = NashBLL.QuestionnaireGetParentShareholderDetails(CompanyID) 'This value will need replaced by querystring
        rptShareholders.DataSource = Shareholders
        rptShareholders.DataBind()
        gbLoopCount = 0
        Dim ParentCompanies As DataSet = NashBLL.QuestionnaireGetParentCompanyDetails(CompanyID)
        rptParentCompany.DataSource = ParentCompanies
        rptParentCompany.DataBind()
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
        If Not IsPostBack Then
            'Go and get the dangerous countries list
            Dim DangerousCountries As DataSet = NashBLL.GetDangerousCountries
            rptDangerousCountries.DataSource = DangerousCountries
            rptDangerousCountries.DataBind()
            'Go and get the conflict minerals list
            Dim ConflictList As DataSet = NashBLL.GetQuestionnaireConflictMinerals(CompanyID)
            rptMinerals.DataSource = ConflictList
            rptMinerals.DataBind()
        End If
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
        'Go and get the purposes
        Dim Purposes As DataSet = NashBLL.QuestionnaireGetMineralPurposeDetails(CompanyID)
        rptPurpose.DataSource = Purposes
        rptPurpose.DataBind()
        'Go and get the processes
        Dim Processes As DataSet = NashBLL.QuestionnaireGetMineralProcessDetails(CompanyID)
        rptProcess.DataSource = Processes
        rptProcess.DataBind()
        'Go and get the components
        Dim Components As DataSet = NashBLL.QuestionnaireGetMineralComponentDetails(CompanyID)
        rptComponent.DataSource = Components
        rptComponent.DataBind()
    End Sub

    Private Sub UpdateNewLines()
        AddPurpose()
        AddProcess()
        AddComponent()
        AddScrapSource()
        AddRecycled()
        AddExtraction()
        AddFacility()
        AddTransporter()
        AddOtherPayment()
        AddOtherTax()
        AddTax()
        AddNewParent()
        AddNewShareholder()
        AddNewRelative()
        AddNewDirector()
        'Finally we bind the repeaters
        BindRepeaters()
    End Sub

    Protected Sub rblIndependent_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rblIndependent.SelectedIndexChanged
        If rblIndependent.SelectedIndex = 1 Then
            panIndependentAudit.Visible = True
        Else
            panIndependentAudit.Visible = False
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click, _
                                                                                btnClose.Click
        'Do our update to the db and then show the confirmation panels
        Dim LoopCount As Integer = 0
        Dim CompanyName As String = ""
        Dim CompanyNumber As String = ""
        Dim Address1 As String = ""
        Dim Address2 As String = ""
        Dim District As String = ""
        Dim CityName As String = ""
        Dim Postcode As String = ""
        Dim StateRegion As String = ""
        Dim CountryID As Integer = 0
        Dim TelephoneNumber As String = ""
        Dim FaxNumber As String = ""
        Dim WebAddress As String = ""
        Dim BusinessTypeID As Integer = 0
        Dim ContactPerson As String = ""
        Dim ContactEmail As String = ""
        Dim CountryList As String = ""
        Dim ParentCompany As Integer = 0
        Dim GovernmentEmployees As Integer = 0
        Dim ConflictMinerals As String = ""
        Dim DangerousCountries As String = ""
        Dim SmelterList As Integer = 0
        Dim Scrap As Integer = 0
        Dim Recycled As Integer = 0
        Dim IndependentAudit As String = ""
        Dim TransportCountries As String = ""
        Dim SupplyChain As String = ""
        Dim SignOff As Integer = 0
        Dim UpdatedBy As Integer = Session("ContactID")
        Dim CurrentPage As Integer = 0

        'Check what has been completed and adjust accordingly
        If txtCompanyName.Text = "" Then
            CompanyName = "None"
        Else
            CompanyName = txtCompanyName.Text
        End If

        If txtCompanyNumber.Text = "" Then
            CompanyNumber = "None"
        Else
            CompanyNumber = txtCompanyNumber.Text
        End If

        If txtAddress1.Text = "" Then
            Address1 = "None"
        Else
            Address1 = txtAddress1.Text
        End If

        If txtAddress2.Text = "" Then
            Address2 = "None"
        Else
            Address2 = txtAddress2.Text
        End If

        If txtDistrict.Text = "" Then
            District = "None"
        Else
            District = txtDistrict.Text
        End If

        If txtCity.Text = "" Then
            CityName = "None"
        Else
            CityName = txtCity.Text
        End If

        If txtPostcode.Text = "" Then
            Postcode = "None"
        Else
            Postcode = txtPostcode.Text
        End If

        If txtState.Text = "" Then
            StateRegion = "None"
        Else
            StateRegion = txtState.Text
        End If

        If cboCountries.SelectedIndex = 0 Then
            CountryID = 0
        Else
            CountryID = cboCountries.SelectedValue
        End If

        If txtTelephone.Text = "" Then
            TelephoneNumber = "None"
        Else
            TelephoneNumber = txtTelephone.Text
        End If

        If txtFax.Text = "" Then
            FaxNumber = "None"
        Else
            FaxNumber = txtFax.Text
        End If

        If txtWebAddress.Text = "" Then
            WebAddress = "None"
        Else
            WebAddress = txtWebAddress.Text
        End If

        If cboBusinessType.SelectedIndex = 0 Then
            BusinessTypeID = 0
        Else
            BusinessTypeID = cboBusinessType.SelectedValue
        End If

        If txtContact.Text = "" Then
            ContactPerson = "None"
        Else
            ContactPerson = txtContact.Text
        End If

        If txtContactEmail.Text = "" Then
            ContactEmail = "None"
        Else
            ContactEmail = txtContactEmail.Text
        End If

        If txtCountryList.Text = "" Then
            CountryList = "None"
        Else
            CountryList = txtCountryList.Text
        End If

        If txtTransportCountries.Text = "" Then
            TransportCountries = "None"
        Else
            TransportCountries = txtTransportCountries.Text
        End If

        If txtIntermediaries.Text = "" Then
            SupplyChain = "None"
        Else
            SupplyChain = txtIntermediaries.Text
        End If

        If rblParent.SelectedIndex = 0 Then
            ParentCompany = 0
        Else
            ParentCompany = 1
        End If

        If chkGovernmentEmployee.Checked Then
            GovernmentEmployees = 1
        Else
            GovernmentEmployees = 0
        End If

        Scrap = rblScrap.SelectedIndex
        Recycled = rblRecycled.SelectedIndex

        'Now get our selected minerals if there are any
        For Each Item As RepeaterItem In rptMinerals.Items
            Dim RadioButtonList As RadioButtonList = Item.FindControl("rblMineral")
            If RadioButtonList.SelectedIndex = 1 Then
                'We need to add this item to our string
                If ConflictMinerals = "" Then
                    'This is our first item
                    ConflictMinerals = RadioButtonList.Attributes("MineralID")
                Else
                    ConflictMinerals &= "," & RadioButtonList.Attributes("MineralID")
                End If
            End If
        Next
        'Finally see what our value is
        If ConflictMinerals = "" Then
            ConflictMinerals = "0"
        End If

        'Now get our selected dangerous countries if there are any
        For Each Item As RepeaterItem In rptDangerousCountries.Items
            Dim RadioButtonList As RadioButtonList = Item.FindControl("rblDangerousCountry")
            If RadioButtonList.SelectedIndex = 1 Then
                'We need to add this item to our string
                If DangerousCountries = "" Then
                    'This is our first item
                    DangerousCountries = RadioButtonList.Attributes("CountryID")
                Else
                    DangerousCountries &= "," & RadioButtonList.Attributes("CountryID")
                End If
            End If
        Next
        'Finally see what our value is
        If DangerousCountries = "" Then
            DangerousCountries = "0"
        End If

        If rblSmelterList.SelectedIndex = 0 Then
            SmelterList = "0"
        Else
            SmelterList = "1"
        End If

        If txtIndependentAudit.Text = "" Or rblIndependent.SelectedIndex = 0 Then
            IndependentAudit = "None"
        Else
            IndependentAudit = txtIndependentAudit.Text
        End If

        If chkSignOff.Checked Then
            SignOff = 1
        Else
            SignOff = 0
        End If

        CurrentPage = btnSave.CommandArgument

        'Now perform our update
        NashBLL.UpdateStandardQuestionnaire(CompanyID, _
                                            CompanyName, _
                                            CompanyNumber, _
                                            Address1, _
                                            Address2, _
                                            District, _
                                            CityName, _
                                            Postcode, _
                                            StateRegion, _
                                            CountryID, _
                                            TelephoneNumber, _
                                            FaxNumber, _
                                            WebAddress, _
                                            BusinessTypeID, _
                                            ContactPerson, _
                                            ContactEmail, _
                                            CountryList, _
                                            ParentCompany, _
                                            GovernmentEmployees, _
                                            ConflictMinerals, _
                                            DangerousCountries, _
                                            SmelterList, _
                                            Scrap, _
                                            Recycled, _
                                            IndependentAudit, _
                                            TransportCountries, _
                                            SupplyChain, _
                                            SignOff, _
                                            UpdatedBy, _
                                            CurrentPage)
        'Now we need to update the textboxes for the conflict minerals
        For Each Item As RepeaterItem In rptMinerals.Items
            Dim RadioButtonList As RadioButtonList = Item.FindControl("rblMineral")
            Dim panMineral As Panel = Item.FindControl("panMineral")
            Dim txtMineralDetails As TextBox = panMineral.FindControl("txtMineraldetails")
            If RadioButtonList.SelectedIndex = 1 Then
                'We need to capture the contents of the textbox
                NashBLL.UpdateQuestionnaireMinerals(CompanyID, _
                                                    RadioButtonList.Attributes("MineralID"), _
                                                    txtMineralDetails.Text)
            End If
        Next
        'Update any new lines that were added
        UpdateNewLines()
        'Finally show our confirmation panels
        If sender.CommandName = "Close" Then
            'We are closing our form now
            If chkSignOff.Checked Then
                'Close the questionnaire
                NashBLL.CloseQuestionnaire(CompanyID, Session("ContactID"))
                panForm.Visible = False
                panClosed.Visible = True
            Else
                'Not checked the box to finalise
                lblErrorMessage.Text = "Please check the sign off box and try again!"
            End If
        Else
            panSaveDraft.Visible = True
            panForm.Visible = False
        End If

    End Sub

    Protected Sub btnReOpen_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReOpen.Click
        Response.Redirect("~/questionnairepopup.aspx?ci=" & CompanyID)
        'ReOpen(sender.CommandArgument)
    End Sub

    Private Sub ReOpen(ByVal PageNumber As Integer)

        panSaveDraft.Visible = False
        panForm.Visible = True
        Select Case CInt(PageNumber)
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

    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNext.Click, btnTopNext.Click
        Dim CompanyPercentage As Double = 0
        Dim ThereWasAnError As Boolean = False

        Select Case sender.CommandArgument
            Case 1
                panPage1.Visible = False
                panPage2.Visible = True
                panPage3.Visible = False
                panPage4.Visible = False
                panPage5.Visible = False
                panPage6.Visible = False
                btnTopNext.CommandArgument = 2
                btnNext.CommandArgument = 2
                btnTopPrev.Visible = True
                btnPrev.Visible = True
                btnPrev.CommandArgument = 1
                btnPrev.CommandArgument = 1
                lblProgress.Width = "333"
                divProgressbar.Attributes.Add("style", "width: 33.2%;")
                'Save the current page for saving and re-opening this form
                btnSave.CommandArgument = 2
                btnTopSave.CommandArgument = 2
                RadAjaxPanel1.FocusControl(lblProgress.ClientID)
            Case 2

                'First we need to check our director percentage
                For Each Item As RepeaterItem In rptShareholders.Items
                    Dim txtPercentOwned As TextBox = Item.FindControl("txtPercentOwned")
                    If Not IsNumeric(txtPercentOwned.Text) Then
                        ThereWasAnError = True
                        Exit For
                    Else
                        CompanyPercentage += CDbl(txtPercentOwned.Text)
                    End If
                Next
                If CompanyPercentage < 100 Or ThereWasAnError Then
                    RadAjaxPanel1.Alert("Please check the directors shareholding!\n\nCompany ownership must total 100%")
                    'Just exit now at this point
                    Return
                End If
                If CheckMinerals() Then
                    btnNext.CommandArgument = 5
                    btnTopNext.CommandArgument = 5
                    btnSave.CommandArgument = 3
                    btnTopSave.CommandArgument = 3
                Else
                    btnNext.CommandArgument = 3
                    btnTopNext.CommandArgument = 3
                    btnSave.CommandArgument = 3
                    btnTopSave.CommandArgument = 3
                End If
                panPage1.Visible = False
                panPage2.Visible = False
                panPage3.Visible = True
                panPage4.Visible = False
                panPage5.Visible = False
                panPage6.Visible = False
                btnPrev.Visible = True
                btnTopPrev.Visible = True
                divProgressbar.Attributes.Add("style", "width: 49.4%;")
                lblProgress.Width = "499"
                btnPrev.CommandArgument = 2
                btnTopPrev.CommandArgument = 2
                RadAjaxPanel1.FocusControl(lblProgress.ClientID)
            Case 3
                panPage1.Visible = False
                panPage2.Visible = False
                panPage3.Visible = False
                panPage4.Visible = True
                panPage5.Visible = False
                panPage6.Visible = False
                btnNext.Visible = True
                btnTopNext.Visible = True
                btnNext.CommandArgument = 4
                btnTopNext.CommandArgument = 4
                btnTopPrev.Visible = True
                btnPrev.Visible = True
                btnPrev.CommandArgument = 3
                btnPrev.CommandArgument = 3
                lblProgress.Width = "632"
                divProgressbar.Attributes.Add("style", "width: 65.4%;")
                'Save the current page for saving and re-opening this form
                btnSave.CommandArgument = sender.CommandArgument
                btnTopSave.CommandArgument = sender.CommandArgument
                RadAjaxPanel1.FocusControl(lblProgress.ClientID)

            Case 4
                panPage1.Visible = False
                panPage2.Visible = False
                panPage3.Visible = False
                panPage4.Visible = False
                panPage5.Visible = True
                panPage6.Visible = False
                btnNext.Visible = True
                btnTopNext.Visible = True
                btnNext.CommandArgument = 5
                btnTopNext.CommandArgument = 5
                btnTopPrev.Visible = True
                btnPrev.Visible = True
                btnTopPrev.CommandArgument = 4
                btnPrev.CommandArgument = 4
                lblProgress.Width = "798"
                divProgressbar.Attributes.Add("style", "width: 81.6%;")
                'Save the current page for saving and re-opening this form
                btnSave.CommandArgument = sender.CommandArgument
                btnTopSave.CommandArgument = sender.CommandArgument
                RadAjaxPanel1.FocusControl(lblProgress.ClientID)
            Case 5
                panPage1.Visible = False
                panPage2.Visible = False
                panPage3.Visible = False
                panPage4.Visible = False
                panPage5.Visible = False
                panPage6.Visible = True
                btnNext.Visible = False
                btnTopNext.Visible = False
                btnTopPrev.Visible = True
                btnPrev.Visible = True
                If CheckMinerals() Then
                    btnPrev.CommandArgument = 3
                    btnTopPrev.CommandArgument = 3
                    btnSave.CommandArgument = 6
                    btnTopSave.CommandArgument = 6
                Else
                    btnPrev.CommandArgument = 5
                    btnTopPrev.CommandArgument = 5
                    btnSave.CommandArgument = 6
                    btnTopSave.CommandArgument = 6
                End If
                btnClose.Visible = True
                divProgressbar.Attributes.Add("style", "width: 100%;")
                lblProgress.Width = "1000"
                RadAjaxPanel1.FocusControl(lblProgress.ClientID)
                'Now rebind our files list
                Dim FileList As DataSet = NashBLL.GetCompanyFiles(CompanyID)
                rptFiles.DataSource = FileList
                rptFiles.DataBind()
            Case Else

        End Select
        If ReadOnlyMode Then
            'Make sure we don't ever show the save or close buttons
            btnSave.Visible = False
            btnTopSave.Visible = False
            btnClose.Visible = False
        End If
        'lblErrorMessage.Text &= "Next Page value = " & btnNext.CommandArgument & "<br />"
    End Sub

    Protected Sub btnPrev_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPrev.Click, btnTopPrev.Click
        btnClose.Visible = False
        Select Case sender.CommandArgument
            Case 1
                panPage1.Visible = True
                panPage2.Visible = False
                panPage3.Visible = False
                panPage4.Visible = False
                panPage5.Visible = False
                panPage6.Visible = False
                btnNext.CommandArgument = 1
                btnTopNext.CommandArgument = 1
                btnPrev.Visible = False
                btnTopPrev.Visible = False
                divProgressbar.Attributes.Add("style", "width: 16.6%;")
                lblProgress.Width = "166"
                'Save the current page for saving and re-opening this form
                btnSave.CommandArgument = sender.CommandArgument
                btnTopSave.CommandArgument = sender.CommandArgument
                RadAjaxPanel1.FocusControl(lblProgress.ClientID)
            Case 2
                panPage1.Visible = False
                panPage2.Visible = True
                panPage3.Visible = False
                panPage4.Visible = False
                panPage5.Visible = False
                panPage6.Visible = False
                btnNext.CommandArgument = 2
                btnTopNext.CommandArgument = 2
                btnPrev.Visible = True
                btnTopPrev.Visible = True
                btnPrev.CommandArgument = 1
                btnTopPrev.CommandArgument = 1
                lblProgress.Width = "333"
                divProgressbar.Attributes.Add("style", "width: 33.2%;")
                'Save the current page for saving and re-opening this form
                btnSave.CommandArgument = sender.CommandArgument
                btnTopSave.CommandArgument = sender.CommandArgument
                RadAjaxPanel1.FocusControl(lblProgress.ClientID)
            Case 3
                panPage1.Visible = False
                panPage2.Visible = False
                panPage3.Visible = True
                panPage4.Visible = False
                panPage5.Visible = False
                panPage6.Visible = False
                If CheckMinerals() Then
                    btnNext.CommandArgument = 5
                    btnTopNext.CommandArgument = 5
                    btnSave.CommandArgument = 3
                    btnTopSave.CommandArgument = 3
                Else
                    btnNext.CommandArgument = 3
                    btnTopNext.CommandArgument = 3
                    btnSave.CommandArgument = 3
                    btnTopSave.CommandArgument = 3
                End If
                btnPrev.Visible = True
                btnTopPrev.Visible = True
                btnNext.Visible = True
                btnTopNext.Visible = True
                btnPrev.CommandArgument = 2
                btnTopPrev.CommandArgument = 2
                lblProgress.Width = "499"
                divProgressbar.Attributes.Add("style", "width: 49.4%;")
                RadAjaxPanel1.FocusControl(lblProgress.ClientID)
            Case 4
                panPage1.Visible = False
                panPage2.Visible = False
                panPage3.Visible = False
                panPage4.Visible = True
                panPage5.Visible = False
                panPage6.Visible = False
                btnNext.CommandArgument = 4
                btnTopNext.CommandArgument = 4
                btnPrev.Visible = True
                btnTopPrev.Visible = True
                btnNext.Visible = True
                btnTopNext.Visible = True
                btnPrev.CommandArgument = 3
                btnTopPrev.CommandArgument = 3
                lblProgress.Width = "632"
                divProgressbar.Attributes.Add("style", "width: 65.4%;")
                'Save the current page for saving and re-opening this form
                btnSave.CommandArgument = sender.CommandArgument
                btnTopSave.CommandArgument = sender.CommandArgument
                RadAjaxPanel1.FocusControl(lblProgress.ClientID)
            Case 5
                panPage1.Visible = False
                panPage2.Visible = False
                panPage3.Visible = False
                panPage4.Visible = False
                panPage5.Visible = True
                panPage6.Visible = False
                btnNext.CommandArgument = 5
                btnTopNext.CommandArgument = 5
                btnPrev.Visible = True
                btnTopPrev.Visible = True
                btnNext.Visible = True
                btnTopNext.Visible = True
                If CheckMinerals() Then
                    btnPrev.CommandArgument = 3
                    btnTopPrev.CommandArgument = 3
                Else
                    btnPrev.CommandArgument = 4
                    btnTopPrev.CommandArgument = 4
                End If
                lblProgress.Width = "798"
                divProgressbar.Attributes.Add("style", "width: 81.6%;")
                RadAjaxPanel1.FocusControl(lblProgress.ClientID)
            Case Else

        End Select
        'lblErrorMessage.Text &= "Prev Page Value = " & btnPrev.CommandArgument & "<br />"
        If ReadOnlyMode Then
            'Make sure we don't ever show the save or close buttons
            btnSave.Visible = False
            btnTopSave.Visible = False
            btnClose.Visible = False
        End If
    End Sub

#End Region

#Region " Select Minerals & Countries "

    Protected Sub SelectMineral(ByVal sender As Object, ByVal e As EventArgs)
        For Each Item As RepeaterItem In rptMinerals.Items
            Dim ButtonList As RadioButtonList = Item.FindControl("rblMineral")
            Dim panMineral As Panel = Item.FindControl("panMineral")
            If ButtonList.SelectedIndex = 1 Then
                panMineral.Visible = True
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
                panQuestion5.Visible = True
            Else
                panMineral.Visible = False
            End If
        Next
        'Always check to see if we can hide or show the rest of the form
        CheckMinerals()
    End Sub

    Protected Sub CheckCountry(ByVal sender As Object, ByVal e As EventArgs)
        CheckMinerals()
    End Sub


#End Region

#Region " Manage New Lines "

    Protected Sub btnAddPurpose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddPurpose.Click
        'Call the main routine
        AddPurpose()
        'Now we can finally add our new line
        NashBLL.AddPurposeLine(CompanyID)

        'Now rebind everything
        Dim Purposes As DataSet = NashBLL.QuestionnaireGetMineralPurposeDetails(CompanyID)
        rptPurpose.DataSource = Purposes
        rptPurpose.DataBind()
    End Sub

    Private Sub AddPurpose()
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

    End Sub

    Protected Sub DeletePurposeLine(ByVal sender As Object, ByVal e As EventArgs)
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

    Protected Sub btnAddProcess_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddProcess.Click
        AddProcess()
        'Now we can finally add our new line
        NashBLL.AddProcessLine(CompanyID)

        'Now rebind everything
        Dim Processes As DataSet = NashBLL.QuestionnaireGetMineralProcessDetails(CompanyID)
        rptProcess.DataSource = Processes
        rptProcess.DataBind()
    End Sub

    Private Sub AddProcess()
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

    End Sub

    Protected Sub DeleteProcessLine(ByVal sender As Object, ByVal e As EventArgs)
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

    Protected Sub btnAddComponent_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddComponent.Click
        AddComponent()
        'Now we can finally add our new line
        NashBLL.AddComponentLine(CompanyID)

        'Now rebind everything
        Dim Components As DataSet = NashBLL.QuestionnaireGetMineralComponentDetails(CompanyID)
        rptComponent.DataSource = Components
        rptComponent.DataBind()
    End Sub

    Private Sub AddComponent()
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

    End Sub

    Protected Sub DeleteComponentLine(ByVal sender As Object, ByVal e As EventArgs)
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

    Protected Sub btnAddScrapSource_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddScrapSource.Click
        AddScrapSource()
        'Now we can finally add our new line
        NashBLL.AddScrapLine(CompanyID)

        'Now rebind everything
        Dim ScrapList As DataSet = NashBLL.QuestionnaireGetMineralScrapDetails(CompanyID)
        rptScrap.DataSource = ScrapList
        rptScrap.DataBind()
    End Sub

    Private Sub AddScrapSource()
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

    End Sub

    Protected Sub DeleteScrapLine(ByVal sender As Object, ByVal e As EventArgs)
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

    Protected Sub btnAddRecycled_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddRecycled.Click
        AddRecycled()
        'Now we can finally add our new line
        NashBLL.AddRecycleLine(CompanyID)

        'Now rebind everything
        Dim RecycleList As DataSet = NashBLL.QuestionnaireGetMineralRecycleDetails(CompanyID)
        rptRecycled.DataSource = RecycleList
        rptRecycled.DataBind()
    End Sub

    Private Sub AddRecycled()
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

    End Sub

    Protected Sub DeleteRecyleLine(ByVal sender As Object, ByVal e As EventArgs)
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

    Protected Sub btnAddExtraction_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddExtraction.Click
        AddExtraction()
        'Now we can finally add our new line
        NashBLL.AddExtractionLine(CompanyID)

        'Now rebind everything
        Dim ExtractionList As DataSet = NashBLL.QuestionnaireGetExtractionDetails(CompanyID)
        rptExtraction.DataSource = ExtractionList
        rptExtraction.DataBind()
    End Sub

    Private Sub AddExtraction()
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

    End Sub

    Protected Sub DeleteExtractionLine(ByVal sender As Object, ByVal e As EventArgs)
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

    Protected Sub btnAddFacility_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddFacility.Click
        AddFacility()
        'Now we can finally add our new line
        NashBLL.AddFacilityLine(CompanyID)

        'Now rebind everything
        Dim FacilityList As DataSet = NashBLL.QuestionnaireGetFacilityDetails(CompanyID)
        rptFacility.DataSource = FacilityList
        rptFacility.DataBind()
    End Sub

    Private Sub AddFacility()
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

    End Sub

    Protected Sub DeleteFacilityLine(ByVal sender As Object, ByVal e As EventArgs)
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

    Protected Sub btnAddTransporter_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddTransporter.Click
        AddTransporter()
        'Now we can finally add our new line
        NashBLL.AddTransportLine(CompanyID)

        'Now rebind everything
        Dim TransporterList As DataSet = NashBLL.QuestionnaireGetTransportDetails(CompanyID)
        rptTransport.DataSource = TransporterList
        rptTransport.DataBind()
    End Sub

    Private Sub AddTransporter()
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

    End Sub

    Protected Sub DeleteTransportLine(ByVal sender As Object, ByVal e As EventArgs)
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

    Protected Sub btnAddOtherPayment_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddOtherPayment.Click
        AddOtherPayment()
        'Now we can finally add our new line
        NashBLL.AddOtherPaymentLine(CompanyID)

        'Now rebind everything
        Dim OtherPaymentList As DataSet = NashBLL.QuestionnaireGetOtherPaymentDetails(CompanyID)
        rptOtherPayment.DataSource = OtherPaymentList
        rptOtherPayment.DataBind()
    End Sub

    Private Sub AddOtherPayment()
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

    End Sub

    Protected Sub DeleteOtherPaymentLine(ByVal sender As Object, ByVal e As EventArgs)
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

    Protected Sub btnAddOtherTax_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddOtherTax.Click
        AddOtherTax()
        'Now we can finally add our new line
        NashBLL.AddOtherTaxLine(CompanyID)

        'Now rebind everything
        Dim OtherTaxList As DataSet = NashBLL.QuestionnaireGetOtherTaxDetails(CompanyID)
        rptOtherTaxes.DataSource = OtherTaxList
        rptOtherTaxes.DataBind()
    End Sub

    Private Sub AddOtherTax()
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

    End Sub

    Protected Sub DeleteOtherTaxLine(ByVal sender As Object, ByVal e As EventArgs)
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

    Protected Sub btnAddTax_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddTax.Click
        AddTax()
        'Now we can finally add our new line
        NashBLL.AddTaxLine(CompanyID)

        'Now rebind everything
        Dim TaxList As DataSet = NashBLL.QuestionnaireGetTaxDetails(CompanyID)
        rptTaxes.DataSource = TaxList
        rptTaxes.DataBind()
    End Sub

    Private Sub AddTax()
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

    End Sub

    Protected Sub DeleteTaxLine(ByVal sender As Object, ByVal e As EventArgs)
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

    Protected Sub rblParent_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rblParent.SelectedIndexChanged
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

    Protected Sub btnAddNewParent_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddNewParent.Click
        AddNewParent()
        'Now we can finally add our new line
        NashBLL.AddParentCompanyLine(CompanyID)

        'Now rebind everything
        Dim ParentCompanies As DataSet = NashBLL.QuestionnaireGetParentCompanyDetails(CompanyID)
        rptParentCompany.DataSource = ParentCompanies
        rptParentCompany.DataBind()
    End Sub

    Private Sub AddNewParent()
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

    End Sub

    Protected Sub DeleteParentLine(ByVal sender As Object, ByVal e As EventArgs)
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

    Protected Sub btnAddNewShareholder_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddNewShareholder.Click
        AddNewShareholder()
        'Now we can finally add our new line
        NashBLL.AddShareholderLine(CompanyID)

        'Now rebind everything
        Dim Shareholders As DataSet = NashBLL.QuestionnaireGetParentShareholderDetails(CompanyID)
        rptShareholders.DataSource = Shareholders
        rptShareholders.DataBind()
    End Sub

    Private Sub AddNewShareholder()
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

    End Sub

    Protected Sub DeleteShareholderLine(ByVal sender As Object, ByVal e As EventArgs)
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

    Protected Sub btnAddNewDirector_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddNewDirector.Click
        AddNewDirector()
        'Now we can finally add our new line
        NashBLL.AddDirectorLine(CompanyID)

        'Now rebind everything
        Dim Directors As DataSet = NashBLL.QuestionnaireGetDirectorDetails(CompanyID)
        rptDirectors.DataSource = Directors
        rptDirectors.DataBind()
    End Sub

    Private Sub AddNewDirector()
        'Adds a new row to the table
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

    End Sub

    Protected Sub DeleteDirectorLine(ByVal sender As Object, ByVal e As EventArgs)
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

    Protected Sub chkGovernmentEmployee_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkGovernmentEmployee.CheckedChanged
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

    Protected Sub btnAddNewRelative_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddNewRelative.Click
        AddNewRelative()
        'Now we can finally add our new line
        NashBLL.AddRelativeLine(CompanyID)

        'Now rebind everything
        Dim Relatives As DataSet = NashBLL.QuestionnaireGetGovtEmployeeDetails(CompanyID)
        rptGovtEmployees.DataSource = Relatives
        rptGovtEmployees.DataBind()
    End Sub

    Private Sub AddNewRelative()
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

    End Sub

    Protected Sub DeleteRelativeLine(ByVal sender As Object, ByVal e As EventArgs)
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

    Protected Sub rblScrap_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rblScrap.SelectedIndexChanged
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

    Protected Sub rblRecycled_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rblRecycled.SelectedIndexChanged
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

#Region " Manage Files "

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload.Click
        'lblErrorMessage.Text = "Executed the upload function"
        For Each File As UploadedFile In rauUploader.UploadedFiles
            File.SaveAs(MapPath("~/userfiles/" & CompanyID & Now.ToString("_ddMMyyyyHHmmss_") & Replace(File.FileName, " ", "_")), True)
            'Now we have to save the file in the db
            NashBLL.UploadFile(CompanyID, Session("ContactID"), CompanyID & Now.ToString("_ddMMyyyyHHmmss_") & Replace(File.FileName, " ", "_"))
        Next
        'Now rebind our list
        Dim FileList As DataSet = NashBLL.GetCompanyFiles(CompanyID)
        rptFiles.DataSource = FileList
        rptFiles.DataBind()
    End Sub

    Protected Sub DownloadFile(ByVal sender As Object, ByVal e As EventArgs)
        'This will force a download of a file
    End Sub

    Protected Sub DeleteFile(ByVal sender As Object, ByVal e As EventArgs)
        'First we need to try to delete the file from the file I/O 
        File.Delete(MapPath("~/userfiles/") & sender.CommandName)
        'Now delete from the DB
        NashBLL.DeleteFile(sender.CommandArgument)
        'Finally rebind our list
        Dim FileList As DataSet = NashBLL.GetCompanyFiles(CompanyID)
        rptFiles.DataSource = FileList
        rptFiles.DataBind()
    End Sub

#End Region

#Region " Databindings "

    Protected Sub rptParentCompany_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles rptParentCompany.ItemDataBound
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

    Protected Sub rptShareholders_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles rptShareholders.ItemDataBound
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

    Protected Sub rptDirectors_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles rptDirectors.ItemDataBound
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
                txtDirectorNationality.Text = drv("DirectorNationality")
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

    Protected Sub rptGovtEmployees_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles rptGovtEmployees.ItemDataBound
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

    Protected Sub rptPurpose_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles rptPurpose.ItemDataBound
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

    Protected Sub rptProcess_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles rptProcess.ItemDataBound
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

    Protected Sub rptComponent_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles rptComponent.ItemDataBound
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

    Protected Sub rptMineralsPopup_itemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles rptMineralsPopup.ItemDataBound
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

    Protected Sub rptSmelterList_itemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles rptSmelterList.ItemDataBound
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

    Protected Sub rptDangerousCountries_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles rptDangerousCountries.ItemDataBound
        Dim litCountryName As Literal
        Dim rblDangerousCounrty As RadioButtonList
        Dim drv As DataRowView
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            litCountryName = e.Item.FindControl("litCountryName")
            rblDangerousCounrty = e.Item.FindControl("rblDangerousCountry")
            drv = e.Item.DataItem
            litCountryName.Text = drv("CountryName")
            rblDangerousCounrty.Attributes.Add("CountryID", drv("CountryID"))
        End If
    End Sub

    Protected Sub rptrelationshipCategories_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles rptrelationshipCategories.ItemDataBound
        Dim litRelationshipCategory As Literal
        Dim drv As DataRowView
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            litRelationshipCategory = e.Item.FindControl("litRelationshipCategory")
            drv = e.Item.DataItem
            litRelationshipCategory.Text = drv("Description")
        End If
    End Sub

    Protected Sub rptScrap_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles rptScrap.ItemDataBound
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

    Protected Sub rptRecycled_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles rptRecycled.ItemDataBound
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

    Protected Sub rptExtraction_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles rptExtraction.ItemDataBound
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

    Protected Sub rptFacility_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles rptFacility.ItemDataBound
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

    Protected Sub rptTransport_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles rptTransport.ItemDataBound
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

    Protected Sub rptOtherPayment_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles rptOtherPayment.ItemDataBound
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

    Protected Sub rptOtherTaxes_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles rptOtherTaxes.ItemDataBound
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

    Protected Sub rptTaxes_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles rptTaxes.ItemDataBound
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

    Protected Sub rptMinerals_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles rptMinerals.ItemDataBound
        Dim litMineralName As Literal
        Dim rblMineral As RadioButtonList
        Dim txtMineralDetails As TextBox
        Dim drv As DataRowView
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            litMineralName = e.Item.FindControl("litMineralName")
            rblMineral = e.Item.FindControl("rblMineral")
            txtMineralDetails = e.Item.FindControl("txtMineralDetails")
            drv = e.Item.DataItem
            'Now populate the items
            litMineralName.Text = drv("MineralName")
            rblMineral.SelectedIndex = 0
            rblMineral.Attributes.Add("MineralID", drv("MineralID"))
            If UCase(drv("MineralDetails")) <> "NONE" Then
                txtMineralDetails.Text = drv("MineralDetails")
            Else
                txtMineralDetails.Text = ""
            End If
        End If
    End Sub

    Protected Sub rptFiles_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptFiles.ItemDataBound
        Dim btnViewFile As LinkButton
        Dim btnDeleteFile As LinkButton
        Dim drv As DataRowView
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            'This is the datarow so we can bind our data
            btnViewFile = e.Item.FindControl("btnViewFile")
            btnDeleteFile = e.Item.FindControl("btnDeleteFile")
            drv = e.Item.DataItem
            btnViewFile.Text = drv("FileName")
            btnDeleteFile.CommandArgument = drv("FileID")
            btnDeleteFile.CommandName = drv("FileName")
            btnViewFile.Enabled = True
            If ReadOnlyMode Then
                btnDeleteFile.Visible = False
            End If
        End If
    End Sub

#End Region



End Class
