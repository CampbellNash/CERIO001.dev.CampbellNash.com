Option Strict Off
Option Explicit On
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  Page Title       : mysuppliers.aspx.vb               '
'  Description      : Shows the suppliers lists         ' 
'  Author           : Stephen Davidson                  '
'  Creation Date    : 2 May 2013                        '
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
Partial Class mysuppliers
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
                lblNoCompaniesHelp.Text = "- You have no companies associated, please visit the mycerico page to associate yourself with a company."

            End If
            'Hide the other panels until clicked

            panSuppliers.Visible = False
            panAddSupplier.Visible = False

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
        lblManageCompaniesPageTitle.Text = "<a href=""mysuppliers.aspx"">Back to All My Companies</a> &raquo; <span class=""label label-info"">" & CompanyName & " </span>  &raquo; Suppliers "
        'Set the search button parameter

        btnSearchSuppliers.CommandArgument = CompanyID

        'Go and see if we can get any suppliers
        Dim MySuppliers As DataSet = NashBLL.GetMySuppliers(CompanyID)
        If MySuppliers.Tables(0).Rows.Count > 0 Then
            'We found some customers so we can show them
            rptSuppliers.DataSource = MySuppliers
            rptSuppliers.DataBind()
            rptSuppliers.Visible = True
            lblCompanySuppliers.Text = CompanyName
        Else
            'No customers were found
            lblNoSuppliers.Text = "No suppliers found!"
            lblCompanySuppliers.Text = CompanyName
            rptSuppliers.Visible = False
            divSuppliers.Visible = False

        End If
        panMyCompanies.Visible = False
        panSuppliers.Visible = True

    End Sub

    Protected Sub GetMyRelationships(ByVal sender As Object, ByVal e As EventArgs)
        GetMyRelationshipsByID(sender.commandargument, sender.commandname)
    End Sub

    Protected Sub ApplyForSupplier(ByVal sender As Object, ByVal e As System.EventArgs)
        'First we make our request
        Dim MailData As DataSet = NashBLL.RequestSupplier(btnSearchSuppliers.CommandArgument, sender.CommandArgument, Session("ContactID"))
        Dim dr As DataRow = MailData.Tables(0).Rows(0)
        'Create the main mail body
        Dim MailBody As String = "Dear " & dr("FirstName") & "," & vbCrLf & vbLf & _
            "The company" & dr("CompanyName") & " has added you to it's supplier list." & vbCrLf & vbLf & _
            "Please login to your account at our web site to find out more details." & vbCrLf & vbLf & _
            "Thank you," & vbCrLf & vbLf & _
            "CERICO Admin Team."
        'Now send this mail to the company owner
        NashBLL.SendMail(dr("EmailAddress"), "", "", MailBody, "New Customer Addded", "", False)
        'Now rebind the repeater
        Dim MySuppliers As DataSet = NashBLL.GetMySuppliers(btnSearchSuppliers.CommandArgument)
        rptSuppliers.DataSource = MySuppliers
        rptSuppliers.DataBind()
        rptSuppliers.Visible = True
        panAddSupplier.Visible = False
        panSuppliers.Visible = True

        panMyCompanies.CssClass = ""
        panSubNav.CssClass = ""
        txtSupplierSearch.Text = ""
    End Sub



#End Region

#Region " My Suppliers "

    Protected Sub GetSupplierDetails(ByVal sender As Object, ByVal e As System.EventArgs)
        'lets go get the company details
        Dim SupplierDetails As DataSet = NashBLL.GetCompanyDetailsByID(sender.CommandArgument)
        Dim dr As DataRow = SupplierDetails.Tables(0).Rows(0)
        lblCompanyNameDetail.Text = dr("CompanyName")
        lblCompanyNameDetailTab.Text = dr("CompanyName")
        lblBusinessAreaDetail.Text = dr("BusinessArea")
        'This is for the users
        Dim companyusers As DataTable = SupplierDetails.Tables(1)
        rptStaffMembers.DataSource = companyusers.DefaultView
        rptStaffMembers.DataBind()

        panSupplierDetails.Visible = True

    End Sub

    Protected Sub btnAddSupplier_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddSupplier.Click
        panAddSupplier.Visible = True
        panSuppliers.Visible = False
        panSupplierDetails.Visible = False
        panMyCompanies.CssClass = "fadePanel"
        panSubNav.CssClass = "fadePanel"
    End Sub

    Protected Sub btnCancelAddSupplier_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelAddSupplier.Click
        panAddSupplier.Visible = False
        panSuppliers.Visible = False
        panSupplierDetails.Visible = False
        panMyCompanies.Visible = True

        panMyCompanies.CssClass = ""
        panSubNav.CssClass = ""
        rptSupplierSearch.Visible = False
        txtSupplierSearch.Text = ""
        lblManageCompaniesPageTitle.Text = ""
    End Sub

    Protected Sub btnCancelSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelSearch.Click
        panAddSupplier.Visible = False
        panSuppliers.Visible = False
        panMyCompanies.Visible = True

        panMyCompanies.CssClass = ""
        panSubNav.CssClass = ""
        rptSupplierSearch.Visible = False
        txtSupplierSearch.Text = ""
        lblManageCompaniesPageTitle.Text = ""
    End Sub


#End Region



#Region " Manage Searches "

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

    Protected Sub btnInvite_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInvite.Click
        'Make sure the pop up stays alive until we're finished checking the situation
        MPE1.Show()
        Dim Result As Integer = NashBLL.CheckEmailAddressExists(txtSupplierEmailAddress.Text)
        Select Case Result
            Case 0
                'User was found so we need to do something else here
                panUserExists.Visible = True
            Case Else
                'User was not found so we need to invite them and create their company
                panInviteSent.Visible = True
        End Select
        panInviteStart.Visible = False
    End Sub

    Protected Sub btnClosePopUp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClosePopUp.Click
        'Clear our modal window ready for next search
        panInviteStart.Visible = True
        panUserExists.Visible = False
        panInviteSent.Visible = False
        txtSupplierFirstname.Text = ""
        txtSupplierSurname.Text = ""
        txtSupplierEmailAddress.Text = ""

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

    Protected Sub BindSuppliers(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
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



    Protected Sub rptFoundCompanies_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptSupplierSearch.ItemDataBound, rptSuppliers.ItemDataBound

        Dim btnCompanyName As LinkButton
        Dim btnSupplierDetails As LinkButton
        Dim panPopUp As Panel
        Dim imgCompanyLogo As Image
        Dim litCompanyName As Literal
        Dim litCompanyAddress As Literal
        Dim lblStatus As Label

        Dim drv As DataRowView

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            btnCompanyName = e.Item.FindControl("btnCompanyName")
            btnSupplierDetails = e.Item.FindControl("btnSupplierDetails")
            lblStatus = e.Item.FindControl("lblStatus")
            panPopUp = e.Item.FindControl("panPopUp")
            litCompanyName = panPopUp.FindControl("litCompanyName")
            litCompanyAddress = panPopUp.FindControl("litCompanyAddress")
            imgCompanyLogo = panPopUp.FindControl("imgCompanyLogo")
            drv = e.Item.DataItem
            btnCompanyName.Text = "Select " & drv("CompanyName")
            btnCompanyName.ToolTip = "Select " & drv("CompanyName")
            btnCompanyName.CommandArgument = drv("CompanyID")
            'btnSupplierDetails.Text = "View " & drv("CompanyName")
            'btnSupplierDetails.CommandArgument = drv("CompanyID")

            litCompanyName.Text = drv("CompanyName")
            litCompanyAddress.Text = drv("Address1") & "<br />" & _
                drv("City") & "<br />" & drv("PostZipCode")
            Dim MyRepeater As Repeater = sender
            If MyRepeater.ID = "rptCustomers" Or MyRepeater.ID = "rptSuppliers" Then
                If UCase(drv("Approved")) = "Y" Then
                    lblStatus.Text = "Approved"
                Else
                    lblStatus.Text = "Awaiting Approval"
                End If
            End If
        End If
    End Sub



#End Region




End Class
