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
            Else
                'No customers were found
                lblNoCompanies.Text = "No companies found!"
                lblNoCompaniesHelp.Text = "- You have no companies associated, click the button add company so find or add your company"

            End If
            'Hide the other panels until clicked
            panCustomers.Visible = False
            panSuppliers.Visible = False
            'Set the page title 
            lblManageCompaniesPageTitle.Text = "Manage My Companies"
        End If
    End Sub

#Region " Manage My Companies "

    Protected Sub GetMyRelationships(ByVal sender As Object, ByVal e As EventArgs)
        'First lets update the page title to refelct the Company we are dealing with.
        lblManageCompaniesPageTitle.Text = "Manage " & sender.CommandName
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


    Protected Sub btnAddCompany_Click(sender As Object, e As EventArgs) Handles btnAddCompany.Click
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


    Protected Sub btnCancelAdd_Click(sender As Object, e As EventArgs) Handles btnCancelAdd.Click
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

    Protected Sub btnGoToAdd_Click(sender As Object, e As EventArgs) Handles btnGoToAdd.Click

        panSearchCompanies.Visible = False
        panAddCompany.Visible = True



    End Sub

    Protected Sub btnCancelSearch_Click(sender As Object, e As EventArgs) Handles btnCancelSearch.Click

        panSearchCompanies.Visible = False
        panMyCompanies.Visible = True

    End Sub

#End Region

#Region "My Suppliers"


    Protected Sub btnAddSupplier_Click(sender As Object, e As EventArgs) Handles btnAddSupplier.Click

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
        panSuppliers.CssClass = ""
        panMyCompanies.CssClass = ""
        panSubNav.CssClass = ""
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


#End Region






    
   
End Class
