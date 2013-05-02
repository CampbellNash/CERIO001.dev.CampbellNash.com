Option Strict Off
Option Explicit On
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  Page Title       : mycompanies.aspx.vb               '
'  Description      : Shows the companies lists         ' 
'  Author           : Brian McAulay                     '
'  Creation Date    : 24 Mar 2013                       '
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
Partial Class mycustomers
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Session("UserLoggedIn") Then
            'User is not logged in so send to login page
            Response.Redirect("~/login.aspx")
        End If
        If Not IsPostBack Then
            'Go and see if we can get any companies
            Dim MyCompanies As DataSet = NashBLL.GetMyCompanies(Session("ContactID"))
            If MyCompanies.Tables(0).Rows.Count > 0 Then
                'We found some customers so we can show them
                rptMyCompanies.DataSource = MyCompanies
                rptMyCompanies.DataBind()
                rptFoundCompanies.Visible = True
            Else
                'No customers were found
                lblNoCompanies.Text = "No companies found!"
                lblNoCompaniesHelp.Text = "- You have no companies associated, click the button add company to find or add your company"

            End If
            'Hide the other panels until clicked
            panCustomers.Visible = False
            panSuppliers.Visible = False
            panAddSupplier.Visible = False
            'Set the page title 
            lblManageCompaniesPageTitle.Text = "Manage My Companies"
        End If
    End Sub

#Region " Manage My Companies "

    Protected Sub GetMyRelationships(ByVal sender As Object, ByVal e As EventArgs)
        'First lets update the page title to refelct the Company we are dealing with.
        lblManageCompaniesPageTitle.Text = "Manage " & sender.CommandName
        'Set the search button parameter
        btnSearchCustomerCompany.CommandArgument = sender.CommandArgument
        btnSearchSuppliers.CommandArgument = sender.CommandArgument
        'Go and see if we can get any customers
        Dim MyCustomers As DataSet = NashBLL.GetMyCustomers(sender.CommandArgument)
        If MyCustomers.Tables(0).Rows.Count > 0 Then
            'We found some customers so we can show them
            rptCustomers.DataSource = MyCustomers
            rptCustomers.DataBind()
            rptCustomers.Visible = True
            lblCompanyCustomers.Text = sender.CommandName
        Else
            'No customers were found
            lblNoCustomers.Text = "No customers found!"
            rptCustomers.Visible = False
        End If

        'Go and see if we can get any suppliers
        Dim MySuppliers As DataSet = NashBLL.GetMySuppliers(sender.CommandArgument)
        If MySuppliers.Tables(0).Rows.Count > 0 Then
            'We found some customers so we can show them
            rptSuppliers.DataSource = MySuppliers
            rptSuppliers.DataBind()
            rptSuppliers.Visible = True
            lblCompanySuppliers.Text = sender.CommandName
        Else
            'No customers were found
            lblNoSuppliers.Text = "No suppliers found!"
            rptSuppliers.Visible = False
        End If
        panCustomers.Visible = True
        panSuppliers.Visible = True
    End Sub

    Protected Sub btnAddCompany_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddCompany.Click
        'Show the correct panels for this view
        panAddCompany.Visible = False
        panMyCompanies.Visible = False
        panCustomers.Visible = False
        panSuppliers.Visible = False
        panSearchCompanies.Visible = True
        'Set the page title 
        lblManageCompaniesPageTitle.Text = "Company Association"

        'Populate our form dropdowns starting with the Business areas
        cboBusinessArea.DataSource = NashBLL.GetBusinessAreas
        cboBusinessArea.DataTextField = "BusinessArea"
        cboBusinessArea.DataValueField = "BusinessAreaID"
        cboBusinessArea.DataBind()
        'Now get our countries
        cboCountries.DataSource = NashBLL.GetCountries
        cboCountries.DataTextField = "CountryName"
        cboCountries.DataValueField = "CountryID"
        cboCountries.DataBind()
        'Now add in a please select
        Dim newItem As New ListItem
        newItem.Text = "--- Please Select ---"
        newItem.Value = ""
        cboBusinessArea.Items.Insert(0, newItem)
        cboCountries.Items.Insert(0, newItem)

    End Sub

    Protected Sub btnCancelAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelAdd.Click
        'Show the correct panels for this view
        panMyCompanies.Visible = True
        panAddCompany.Visible = False
        'Set the page title 
        lblManageCompaniesPageTitle.Text = "Manage My Companies"
    End Sub

    Protected Sub btngoBack_Click(sender As Object, e As EventArgs) Handles btnGoback.Click
        'Show the correct panels for this view
        panMyCompanies.Visible = False
        panAddCompany.Visible = False
        panSearchCompanies.Visible = True

        'Set the page title 
        lblManageCompaniesPageTitle.Text = "Manage My Companies"
    End Sub

    Protected Sub btnGoToAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddCompany1.Click
        panSearchCompanies.Visible = False
        panAddCompany.Visible = True
    End Sub

    Protected Sub btnAddCompany2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddCompany2.Click
        'Return to the company search screen
        panAddCompany.Visible = False
        panMyCompanies.Visible = False
        panCustomers.Visible = False
        panSuppliers.Visible = False
        panSearchCompanies.Visible = True

    End Sub

    Protected Sub btnAddNewCompany_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNewCompany.Click
        Dim ParentCompanyID As Integer
        Dim ContactID As Integer
        Dim CompanyName As String
        Dim Address1 As String
        Dim Address2 As String
        Dim Address3 As String
        Dim Address4 As String
        Dim City As String
        Dim Postcode As String
        Dim TelephoneNumber As String
        Dim FaxNumber As String
        Dim Telex As String
        Dim WebURL As String
        Dim EmailAddress As String
        Dim FaceBookURL As String
        Dim Twitter As String
        Dim CountryID As Integer
        Dim BusinessAreaID As Integer

        ParentCompanyID = hidParentCompanyID.Value
        ContactID = Session("ContactID")
        CompanyName = txtCompanyName.Text
        Address1 = txtAddress1.Text
        If txtAddress2.Text = "" Then
            Address2 = "None"
        Else
            Address2 = txtAddress2.Text
        End If
        If txtAddress3.Text = "" Then
            Address3 = "None"
        Else
            Address3 = txtAddress3.Text
        End If
        If txtAddress4.Text = "" Then
            Address4 = "None"
        Else
            Address4 = txtAddress4.Text
        End If
        City = txtCity.Text
        Postcode = txtPostcode.Text
        TelephoneNumber = txtTelephone.Text
        If txtFaxNumber.Text = "" Then
            FaxNumber = "None"
        Else
            FaxNumber = txtFaxNumber.Text
        End If
        If txtTelex.Text = "" Then
            Telex = "None"
        Else
            Telex = txtTelex.Text
        End If
        If txtWebsite.Text = "" Then
            WebURL = "None"
        Else
            WebURL = txtWebsite.Text
        End If
        EmailAddress = txtEmailAddress.Text
        If txtTwitter.Text = "" Then
            Twitter = "None"
        Else
            Twitter = txtTwitter.Text
        End If
        If txtFaceBook.Text = "" Then
            FaceBookURL = "None"
        Else
            FaceBookURL = txtFaceBook.Text
        End If
        CountryID = cboCountries.SelectedValue
        BusinessAreaID = cboBusinessArea.SelectedValue
        'Now perform our update
        Dim Result As Integer = NashBLL.AddNewCompany(ParentCompanyID, _
                              ContactID, _
                              CompanyName, _
                              Address1, _
                              Address2, _
                              Address3, _
                              Address4, _
                              City, _
                              Postcode, _
                              TelephoneNumber, _
                              FaxNumber, _
                              Telex, _
                              WebURL, _
                              EmailAddress, _
                              FaceBookURL, _
                              Twitter, _
                              CountryID, _
                              BusinessAreaID)
        If Result = -1 Then
            'This update has failed so advise
            lblAddCompany.Text = "There was an error with your form please check the completed items and try again."
        Else
            panAddCompany.Visible = False
            panConfirmAdd.Visible = True
        End If
    End Sub

    Protected Sub ApplyForCustomer(ByVal sender As Object, ByVal e As System.EventArgs)
        'First we make our request
        Dim MailData As DataSet = NashBLL.RequestCustomer(btnSearchCustomerCompany.CommandArgument, sender.CommandArgument)
        Dim dr As DataRow = MailData.Tables(0).Rows(0)
        'Create the main mail body
        Dim MailBody As String = "Dear " & dr("FirstName") & "," & vbCrLf & vbLf & _
            "A user on CERICO has requested to be accepted as a customer of your company " & dr("CompanyName") & "." & vbCrLf & vbLf & _
            "Please login to your account at our web site to find out more details." & vbCrLf & vbLf & _
            "Thank you," & vbCrLf & vbLf & _
            "CERICO Admin Team."
        'Now send this mail to the company owner
        NashBLL.SendMail(dr("EmailAddress"), "", "", MailBody, "Company Customer Request", "", False)
        'Now rebind the repeater
        Dim MyCustomers As DataSet = NashBLL.GetMyCustomers(btnSearchCustomerCompany.CommandArgument)
        rptCustomers.DataSource = MyCustomers
        rptCustomers.DataBind()
        rptCustomers.Visible = True
        panApplyCustomer.Visible = False
        panCustomers.Visible = True
        panSuppliers.CssClass = ""
        panMyCompanies.CssClass = ""
        panSubNav.CssClass = ""
        txtSearchCustomerCompany.Text = ""
    End Sub

    Protected Sub ApplyForSupplier(ByVal sender As Object, ByVal e As System.EventArgs)
        'First we make our request
        Dim MailData As DataSet = NashBLL.RequestSupplier(btnSearchSuppliers.CommandArgument, sender.CommandArgument)
        Dim dr As DataRow = MailData.Tables(0).Rows(0)
        'Create the main mail body
        Dim MailBody As String = "Dear " & dr("FirstName") & "," & vbCrLf & vbLf & _
            "A user on CERICO has requested to be accepted as a supplier to your company " & dr("CompanyName") & "." & vbCrLf & vbLf & _
            "Please login to your account at our web site to find out more details." & vbCrLf & vbLf & _
            "Thank you," & vbCrLf & vbLf & _
            "CERICO Admin Team."
        'Now send this mail to the company owner
        NashBLL.SendMail(dr("EmailAddress"), "", "", MailBody, "Company Supplier Request", "", False)
        'Now rebind the repeater
        Dim MySuppliers As DataSet = NashBLL.GetMySuppliers(btnSearchSuppliers.CommandArgument)
        rptSuppliers.DataSource = MySuppliers
        rptSuppliers.DataBind()
        rptSuppliers.Visible = True
        panAddSupplier.Visible = False
        panSuppliers.Visible = True
        panCustomers.CssClass = ""
        panMyCompanies.CssClass = ""
        panSubNav.CssClass = ""
        txtSupplierSearch.Text = ""
    End Sub

    Protected Sub JoinCompany(ByVal sender As Object, ByVal e As System.EventArgs)
        'First we make our request
        Dim MailData As DataSet = NashBLL.JoinCustomer(Session("ContactID"), sender.CommandArgument)
        Dim dr As DataRow = MailData.Tables(0).Rows(0)
        'Create the main mail body
        Dim MailBody As String = "Dear " & dr("FirstName") & "," & vbCrLf & vbLf & _
            "A user on CERICO has requested to be a member of your company " & dr("CompanyName") & "." & vbCrLf & vbLf & _
            "Please login to your account at our web site to find out more details." & vbCrLf & vbLf & _
            "Thank you," & vbCrLf & vbLf & _
            "CERICO Admin Team."
        'Now send this mail to the company owner
        NashBLL.SendMail(dr("EmailAddress"), "", "", MailBody, "New Company Member Application", "", False)

        'Now rebind the repeater
        Dim MyCompanies As DataSet = NashBLL.GetMyCompanies(Session("ContactID"))
        rptMyCompanies.DataSource = MyCompanies
        rptMyCompanies.DataBind()
        panAddCompany.Visible = False
        panMyCompanies.Visible = True
        panCustomers.Visible = False
        panSuppliers.Visible = False
        panSearchCompanies.Visible = False
    End Sub


#End Region

#Region " My Suppliers "


    Protected Sub btnAddSupplier_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddSupplier.Click
        panAddSupplier.Visible = True
        panSuppliers.Visible = False
        panCustomers.CssClass = "fadePanel"
        panMyCompanies.CssClass = "fadePanel"
        panSubNav.CssClass = "fadePanel"
    End Sub

    Protected Sub btnCancelAddSupplier_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelAddSupplier.Click
        panAddSupplier.Visible = False
        panSuppliers.Visible = True
        panCustomers.CssClass = ""
        panMyCompanies.CssClass = ""
        panSubNav.CssClass = ""
        rptSupplierSearch.Visible = False
        txtSupplierSearch.Text = ""
    End Sub



#End Region

#Region " My Customers "


    Protected Sub btnAddCustomer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddCustomer.Click
        panApplyCustomer.Visible = True
        panCustomers.Visible = False
        panSuppliers.CssClass = "fadePanel"
        panMyCompanies.CssClass = "fadePanel"
        panSubNav.CssClass = "fadePanel"
    End Sub

    Protected Sub btnCancelApplyCustomer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelApplyCustomer.Click
        panApplyCustomer.Visible = False
        panCustomers.Visible = True
        txtSearchCustomerCompany.Text = ""
        rptCustomerSearch.Visible = False
        panSuppliers.CssClass = ""
        panMyCompanies.CssClass = ""
        panSubNav.CssClass = ""
    End Sub

#End Region

#Region " Manage Searches "

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim FoundCompanies As DataSet = NashBLL.SearchForCompanies(txtSearch.Text, Session("ContactID"))
        Dim ReturnValue As Integer = FoundCompanies.Tables(1).Rows(0)("ReturnValue")
        If ReturnValue = 0 Then
            'Either we have found 10 or less companiesso check to see if its more than one
            If FoundCompanies.Tables(0).Rows.Count > 0 Then
                'We have results that we can display so show them
                rptFoundCompanies.DataSource = FoundCompanies
                rptFoundCompanies.DataBind()
                rptFoundCompanies.Visible = True
                panNoResults1.Visible = False
            Else
                'No results were found so display and advise
                panNoResults1.Visible = True
                rptFoundCompanies.Visible = False
            End If
        Else
            'Too many results found so advise to narrow the search
            panTooManyRecords1.Visible = True
        End If
    End Sub

    Protected Sub btnSearchCustomerCompany_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchCustomerCompany.Click
        Dim FoundCompanies As DataSet = NashBLL.SearchForCustomers(sender.CommandArgument, Session("ContactID"), txtSearchCustomerCompany.Text)
        Dim ReturnValue As Integer = FoundCompanies.Tables(1).Rows(0)("ReturnValue")
        If ReturnValue = 0 Then
            'Either we have found 10 or less companiesso check to see if its more than one
            If FoundCompanies.Tables(0).Rows.Count > 0 Then
                'We have results that we can display so show them
                rptCustomerSearch.DataSource = FoundCompanies
                rptCustomerSearch.DataBind()
                rptCustomerSearch.Visible = True
                panNoResults2.Visible = False
            Else
                'No results were found so display and advise
                panNoResults2.Visible = True
                rptCustomerSearch.Visible = False
            End If
        Else
            'Too many results found so advise to narrow the search
            panTooManyRecords2.Visible = True
        End If
    End Sub

    Protected Sub btnSearchSuppliers_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchSuppliers.Click
        Dim FoundCompanies As DataSet = NashBLL.SearchForSuppliers(sender.CommandArgument, Session("ContactID"), txtSupplierSearch.Text)
        Dim ReturnValue As Integer = FoundCompanies.Tables(1).Rows(0)("ReturnValue")
        If ReturnValue = 0 Then
            'Either we have found 10 or less companies so check to see if its more than one
            If FoundCompanies.Tables(0).Rows.Count > 0 Then
                'We have results that we can display so show them
                rptSupplierSearch.DataSource = FoundCompanies
                rptSupplierSearch.DataBind()
                rptSupplierSearch.Visible = True
                panNoResults3.Visible = False
            Else
                'No results were found so display and advise
                panNoResults3.Visible = True
                rptSupplierSearch.Visible = False
            End If
        Else
            'Too many results found so advise to narrow the search
            panTooManyRecords3.Visible = True
        End If
    End Sub

    Protected Sub btnCancelSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelSearch.Click
        'Cancel the main company search
        panSearchCompanies.Visible = False
        panMyCompanies.Visible = True
        txtSearch.Text = ""
        rptFoundCompanies.Visible = False
    End Sub

    Protected Sub btnParentCompany_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnParentCompany.Click
        panParent.Visible = True
    End Sub

    Protected Sub btnCancelParent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelParent.Click
        panParent.Visible = False
    End Sub

    Protected Sub btnParentSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnParentSearch.Click
        Dim FoundCompanies As DataSet = NashBLL.SearchForParentCompanies(txtParentSearch.Text)
        Dim ReturnValue As Integer = FoundCompanies.Tables(1).Rows(0)("ReturnValue")
        If ReturnValue = 0 Then
            'Either we have found 10 or less companies so check to see if its more than one
            If FoundCompanies.Tables(0).Rows.Count > 0 Then
                'We have results that we can display so show them
                rptParentCompany.DataSource = FoundCompanies
                rptParentCompany.DataBind()
                rptParentCompany.Visible = True
            Else
                'No results were found so display and advise
                lblParentError.Text = "No companies found for the term you entered."
                rptSupplierSearch.Visible = False
            End If
        Else
            'Too many results found so advise to narrow the search
            lblParentError.Text = "Too many records foundk, please narrow your search and try again"
            rptSupplierSearch.Visible = False
        End If
    End Sub

    Protected Sub ChooseParentCompany(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim MyButton As LinkButton = sender
        txtParentCompany.Text = MyButton.Text
        hidParentCompanyID.Value = sender.CommandArgument
        txtParentSearch.Text = ""
        rptParentCompany.Visible = False
        panParent.Visible = False
        btnRemoveParent.Enabled = True
    End Sub

    Protected Sub btnRemoveParent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemoveParent.Click
        hidParentCompanyID.Value = "0"
        txtParentCompany.Text = "No Parent Company"
        btnRemoveParent.Enabled = False
    End Sub

#End Region

#Region " Databindings "

    Protected Sub BindCompanies(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        Dim btnCompanyName As LinkButton

        Dim lblStatus As Label
        Dim drv As DataRowView
        Dim MyRepeater As Repeater = sender
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            'This is a data item so we can populate our items
            btnCompanyName = e.Item.FindControl("btnCompanyName")

            lblStatus = e.Item.FindControl("lblStatus")
            drv = e.Item.DataItem
            btnCompanyName.Text = drv("CompanyName")
            btnCompanyName.CommandArgument = drv("CompanyID")
            btnCompanyName.CommandName = drv("CompanyName")

            If UCase(drv("Approved")) = "Y" Then
                lblStatus.Text = "Approved"
                lblStatus.CssClass = "label label-success"

            Else
                lblStatus.Text = "Awaiting approval"
                lblStatus.CssClass = "label"
                btnCompanyName.Enabled = False
                btnCompanyName.Attributes.Remove("href")
                btnCompanyName.CssClass = "disabled"


            End If
        End If
    End Sub

    Protected Sub rptFoundCompanies_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptFoundCompanies.ItemDataBound, _
                                                                                                                                                rptCustomers.ItemDataBound, _
                                                                                                                                                rptCustomerSearch.ItemDataBound, _
                                                                                                                                                rptSupplierSearch.ItemDataBound, _
                                                                                                                                                rptSuppliers.ItemDataBound, _
                                                                                                                                                rptParentCompany.ItemDataBound
        Dim btnCompanyName As LinkButton
        Dim panPopUp As Panel
        Dim imgCompanyLogo As Image
        Dim litCompanyName As Literal
        Dim litCompanyAddress As Literal
        Dim litStatus As Literal
        Dim hypCompanyNameSR As HyperLink
        Dim drv As DataRowView

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            btnCompanyName = e.Item.FindControl("btnCompanyName")
            hypCompanyNameSR = e.Item.FindControl("hypCompanyNameSR")
            litStatus = e.Item.FindControl("litStatus")
            panPopUp = e.Item.FindControl("panPopUp")
            litCompanyName = panPopUp.FindControl("litCompanyName")
            litCompanyAddress = panPopUp.FindControl("litCompanyAddress")
            imgCompanyLogo = panPopUp.FindControl("imgCompanyLogo")
            drv = e.Item.DataItem
            btnCompanyName.Text = "Associate with " & drv("CompanyName")
            btnCompanyName.ToolTip = "Associate with " & drv("CompanyName")
            btnCompanyName.CommandArgument = drv("CompanyID")
            hypCompanyNameSR.Text = drv("CompanyName")
            hypCompanyNameSR.NavigateUrl = "#"
            litCompanyName.Text = drv("CompanyName")
            litCompanyAddress.Text = drv("Address1") & "<br />" & _
                drv("City") & "<br />" & drv("PostZipCode")
            Dim MyRepeater As Repeater = sender
            If MyRepeater.ID = "rptCustomers" Or MyRepeater.ID = "rptSuppliers" Then
                If UCase(drv("Approved")) = "Y" Then
                    litStatus.Text = "Approved"
                Else
                    litStatus.Text = "Awaiting Approval"
                End If
            End If
        End If
    End Sub


#End Region

End Class
