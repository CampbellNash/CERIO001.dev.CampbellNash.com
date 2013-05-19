Option Strict Off
Option Explicit On
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  Page Title       : demoreset.aspx.vb                 '
'  Description      : Clears out items for demo script  ' 
'  Author           : Brian McAulay                     '
'  Creation Date    : 19 May 2013                       '
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
Partial Class demoreset
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Session("UserLoggedIn") Then
            'User is not logged in so send to login page
            Response.Redirect("~/default.aspx")
        End If
        If Not IsPostBack Then
            'Go and get our companies for actions
            Dim CompanyList As DataSet = NashBLL.ListAllDemoCompanies()
            rptCompanies.DataSource = CompanyList
            rptCompanies.DataBind()
            'List our available users
            Dim Userlist As DataSet = NashBLL.ListAllDemoUsers()
            rptUsers.DataSource = Userlist
            rptUsers.DataBind()
        End If
    End Sub

    Protected Sub DeleteCompany(ByVal sender As Object, ByVal e As System.EventArgs)
        NashBLL.DeleteCompany(sender.CommandArgument)
        BindRepeaters()
        RadAjaxPanel1.Alert("Company deleted")
    End Sub

    Protected Sub DeleteQuestionnaire(ByVal sender As Object, ByVal e As System.EventArgs)
        NashBLL.DeleteQuestionnaire(sender.CommandArgument)
        BindRepeaters()
        RadAjaxPanel1.Alert("Questionnaire Deleted")
    End Sub

    Protected Sub DeleteAssoc(ByVal sender As Object, ByVal e As System.EventArgs)
        NashBLL.DeleteCompanyConnections(sender.CommandArgument)
        BindRepeaters()
        RadAjaxPanel1.Alert("Company Connections Deleted")
    End Sub

    Protected Sub DeleteUser(ByVal sender As Object, ByVal e As System.EventArgs)
        NashBLL.DeleteUser(sender.CommandArgument)
        BindRepeaters()
        RadAjaxPanel1.Alert("User Deleted")
    End Sub

    Private Sub BindRepeaters()
        'Go and get our companies for actions
        Dim CompanyList As DataSet = NashBLL.ListAllDemoCompanies()
        rptCompanies.DataSource = CompanyList
        rptCompanies.DataBind()
        'List our available users
        Dim Userlist As DataSet = NashBLL.ListAllDemoUsers()
        rptUsers.DataSource = Userlist
        rptUsers.DataBind()
    End Sub

    Protected Sub rptCompanies_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptCompanies.ItemDataBound
        Dim litCompanyName As Literal
        Dim btnDeleteCompany As LinkButton
        Dim btnDeleteQuestionnaire As LinkButton
        Dim btnDeleteAssoc As LinkButton
        Dim drv As DataRowView

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            'This is a data item so we can bind
            litCompanyName = e.Item.FindControl("litCompanyName")
            btnDeleteAssoc = e.Item.FindControl("btnDeleteAssoc")
            btnDeleteCompany = e.Item.FindControl("btnDeleteCompany")
            btnDeleteQuestionnaire = e.Item.FindControl("btnDeleteQuestionnaire")
            drv = e.Item.DataItem
            litCompanyName.Text = drv("CompanyName")
            btnDeleteAssoc.CommandArgument = drv("CompanyID")
            btnDeleteCompany.CommandArgument = drv("CompanyID")
            btnDeleteQuestionnaire.CommandArgument = drv("CompanyID")
            If UCase(drv("HasQuestions")) = "Y" Then
                btnDeleteQuestionnaire.Visible = True
            Else
                btnDeleteQuestionnaire.Visible = False
            End If
            If UCase(drv("HasAssociations")) = "Y" Then
                btnDeleteAssoc.Visible = True
            Else
                btnDeleteAssoc.Visible = False
            End If
        End If
    End Sub

    Protected Sub rptUsers_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptUsers.ItemDataBound
        Dim litUserName As Literal
        Dim litEmailAddress As Literal
        Dim btnDeleteUser As LinkButton
        Dim drv As DataRowView

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            'This is a data item so we can bind
            litUserName = e.Item.FindControl("litUserName")
            litEmailAddress = e.Item.FindControl("litEmailAddress")
            btnDeleteUser = e.Item.FindControl("btnDeleteUser")
            drv = e.Item.DataItem
            litUserName.Text = drv("Surname") & ", " & drv("FirstName")
            litEmailAddress.Text = drv("EmailAddress")
            btnDeleteUser.CommandArgument = drv("ContactID")
            
        End If
    End Sub
End Class
