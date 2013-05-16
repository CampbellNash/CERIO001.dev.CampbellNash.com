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
            Response.Redirect("~/default.aspx")
        End If
        If Not IsPostBack Then
            'Go and see if we can get any companies
            Dim MyCompanies As DataSet = NashBLL.GetMyCompanies(Session("ContactID"))
            If MyCompanies.Tables(0).Rows.Count > 0 Then
                'We found some customers so we can show them
                rptMyCompanies.DataSource = MyCompanies
                rptMyCompanies.DataBind()
                'Populate the drop down
                cboCompanies.DataSource = NashBLL.GetMyCompanies(Session("ContactID"))
                cboCompanies.DataValueField = "CompanyID"
                cboCompanies.DataTextField = "Companyname"
                cboCompanies.DataBind()
                Dim NewItem As New ListItem
                NewItem.Text = "--- Please Select --"
                NewItem.Value = ""
                cboCompanies.Items.Insert(0, NewItem)

            Else
                'No customers were found
                lblNoCompanies.Text = "No companies found!"
                lblNoCompaniesHelp.Text = "- You have no companies associated, click the button add company to find or add your company"

            End If
            'Hide the other panels until clicked
            panCustomers.Visible = False
          
            'Set the page title 
            lblManageCompaniesPageTitle.Text = ""
        End If
    End Sub

#Region " Manage My Companies "

    Protected Sub GetMyRelationShipDropDown(ByVal sender As Object, ByVal e As EventArgs)
        GetMyRelationshipsByID(cboCompanies.SelectedValue, cboCompanies.SelectedItem.Text)
    End Sub

    Private Sub GetMyRelationshipsByID(ByVal CompanyID As Integer, ByVal CompanyName As String)
        'First lets update the page title to refelct the Company we are dealing with.
        lblManageCompaniesPageTitle.Text = "<a href=""mycustomers.aspx"">Back to All My Companies</a> &raquo; <span class=""label label-info"">" & CompanyName & " </span>  &raquo; Suppliers "
        'Set the search button parameter

        btnSearchCustomerCompany.CommandArgument = CompanyID

        'Go and see if we can get any suppliers
        Dim MySuppliers As DataSet = NashBLL.GetMyCustomers(CompanyID)
        If MySuppliers.Tables(0).Rows.Count > 0 Then
            'We found some customers so we can show them
            rptCustomers.DataSource = MySuppliers
            rptCustomers.DataBind()
            rptCustomers.Visible = True
            lblCompanyCustomers.Text = CompanyName
            'divSuppliers.Visible = True
        Else
            'No customers were found
            lblNoCustomers.Text = "No customers found!"
            lblCompanyCustomers.Text = CompanyName
            rptCustomers.Visible = False
            'divSuppliers.Visible = False
            lblCompanyCustomers.Text = CompanyName


        End If
        panMyCompanies.Visible = False
        panCustomers.Visible = True


    End Sub

    Protected Sub GetMyRelationships(ByVal sender As Object, ByVal e As EventArgs)
        GetMyRelationshipsByID(sender.commandargument, sender.commandname)
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

        panMyCompanies.CssClass = ""
        panSubNav.CssClass = ""
        txtSearchCustomerCompany.Text = ""
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

        panMyCompanies.Visible = True
        panCustomers.Visible = False

    End Sub


#End Region



#Region " My Customers "


    Protected Sub btnAddCustomer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddCustomer.Click
        panApplyCustomer.Visible = True
        panCustomers.Visible = False

        panMyCompanies.CssClass = "fadePanel"
        panSubNav.CssClass = "fadePanel"
    End Sub

    Protected Sub btnCancelApplyCustomer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelApplyCustomer.Click
        panApplyCustomer.Visible = False
        panCustomers.Visible = True
        txtSearchCustomerCompany.Text = ""
        rptCustomerSearch.Visible = False

        panMyCompanies.CssClass = ""
        panSubNav.CssClass = ""
    End Sub

#End Region

#Region " Manage Searches "

   

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

    

   

   

   

#End Region

#Region " Databindings "

    Protected Sub BindCompanies(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        Dim btnCompanyName As LinkButton
        Dim lblTotalSuppliers As Label
        Dim lblStatus As Label
        Dim drv As DataRowView
        Dim MyRepeater As Repeater = sender
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            'This is a data item so we can populate our items
            btnCompanyName = e.Item.FindControl("btnCompanyName")
            lblTotalSuppliers = e.Item.FindControl("lblTotalSuppliers")
            lblStatus = e.Item.FindControl("lblStatus")
            drv = e.Item.DataItem
            btnCompanyName.Text = drv("CompanyName")
            btnCompanyName.CommandArgument = drv("CompanyID")
            btnCompanyName.CommandName = drv("CompanyName")
            lblTotalSuppliers.Text = drv("TotalSuppliers")
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

    


#End Region

End Class
