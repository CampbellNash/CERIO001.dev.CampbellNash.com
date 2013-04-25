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
Partial Class mycompanies
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
                lblNoCompaniesHelp.Text = "- You have no companies associated, click the button add company so find or add your company"

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
            panCustomers.Visible = True
            lblCompanyCustomers.Text = sender.CommandName

        Else
            'No customers were found
            lblNoCustomers.Text = "No customers found!"
        End If

        'Go and see if we can get any suppliers
        Dim MySuppliers As DataSet = NashBLL.GetMySuppliers(sender.CommandArgument)
        If MySuppliers.Tables(0).Rows.Count > 0 Then
            'We found some customers so we can show them
            rptSuppliers.DataSource = MySuppliers
            rptSuppliers.DataBind()
            panSuppliers.Visible = True
            lblCompanySuppliers.Text = sender.CommandName
        Else
            'No customers were found
            lblNoSuppliers.Text = "No suppliers found!"
        End If
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



#End Region

#Region "My Suppliers"


    Protected Sub btnAddSupplier_Click(sender As Object, e As EventArgs) Handles btnAddSupplier.Click
        panAddSupplier.Visible = True
        panSuppliers.Visible = False
        panCustomers.CssClass = "fadePanel"
        panMyCompanies.CssClass = "fadePanel"
        panSubNav.CssClass = "fadePanel"
    End Sub

    Protected Sub btnCancelAddSupplier_Click(sender As Object, e As EventArgs) Handles btnCancelAddSupplier.Click
        panAddSupplier.Visible = False
        panSuppliers.Visible = True
        panCustomers.CssClass = ""
        panMyCompanies.CssClass = ""
        panSubNav.CssClass = ""
        rptSupplierSearch.Visible = False
        txtSupplierSearch.Text = ""
    End Sub



#End Region

#Region "My Customers"


    Protected Sub btnAddCustomer_Click(sender As Object, e As EventArgs) Handles btnAddCustomer.Click
        panApplyCustomer.Visible = True
        panCustomers.Visible = False
        panSuppliers.CssClass = "fadePanel"
        panMyCompanies.CssClass = "fadePanel"
        panSubNav.CssClass = "fadePanel"
    End Sub

    Protected Sub btnCancelApplyCustomer_Click(sender As Object, e As EventArgs) Handles btnCancelApplyCustomer.Click
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
            'Either we have found 10 or less companiesso check to see if its more than one
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

#End Region


#Region " Databindings "

    Protected Sub BindCompanies(sender As Object, e As RepeaterItemEventArgs)
        Dim btnCompanyName As LinkButton
        Dim drv As DataRowView
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            'This is a data item so we can populate our items
            btnCompanyName = e.Item.FindControl("btnCompanyName")
            drv = e.Item.DataItem
            btnCompanyName.Text = drv("CompanyName")
            btnCompanyName.CommandArgument = drv("CompanyID")
            btnCompanyName.CommandName = drv("CompanyName")


        End If
    End Sub

    Protected Sub rptFoundCompanies_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptFoundCompanies.ItemDataBound, _
                                                                                                                                                rptCustomers.ItemDataBound, _
                                                                                                                                                rptCustomerSearch.ItemDataBound, _
                                                                                                                                                rptSupplierSearch.ItemDataBound
        Dim btnCompanyName As LinkButton
        Dim panPopUp As Panel
        Dim imgCompanyLogo As Image
        Dim litCompanyName As Literal
        Dim litCompanyAddress As Literal
        Dim drv As DataRowView

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            btnCompanyName = e.Item.FindControl("btnCompanyName")
            panPopUp = e.Item.FindControl("panPopUp")
            litCompanyName = panPopUp.FindControl("litCompanyName")
            litCompanyAddress = panPopUp.FindControl("litCompanyAddress")
            imgCompanyLogo = panPopUp.FindControl("imgCompanyLogo")
            drv = e.Item.DataItem
            btnCompanyName.Text = drv("CompanyName")
            litCompanyName.Text = drv("CompanyName")
            litCompanyAddress.Text = drv("Address1") & "<br />" & _
                drv("City") & "<br />" & drv("PostZipCode")
        End If
    End Sub


#End Region






    
   

    

    
    
End Class
