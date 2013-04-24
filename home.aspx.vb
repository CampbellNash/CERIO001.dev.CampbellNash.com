Option Strict Off
Option Explicit On
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  Page Title       : home.aspx.vb                      '
'  Description      : Home page of logged in user       ' 
'  Author           : Stephen Davidson                  '
'  Creation Date    : 25 Mar 2013                       '
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


Partial Class home
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Session("UserLoggedIn") Then
            'User is not logged in so send to login page
            Response.Redirect("~/login.aspx")
        End If
        If Not IsPostBack Then
            
            LoadData()
            
           
        End If
    End Sub
    Private isExport As Boolean = True

    Protected Sub RadGrid1_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)

        isExport = True

        RadGrid1.MasterTableView.HierarchyDefaultExpanded = True
        ' for the first level
        ' for the second level  
        RadGrid1.MasterTableView.DetailTables(0).HierarchyDefaultExpanded = True


    End Sub

    Private Sub LoadData()
        RadGrid1.DataSource = GetDataTable("SELECT CompanyID, CompanyName FROM Companies")
    End Sub

    Private Sub RadGrid1_PageIndexChanged(ByVal source As Object, ByVal e As GridPageChangedEventArgs) Handles RadGrid1.PageIndexChanged
        LoadData()
    End Sub

    Protected Sub RadGrid1_PageSizeChanged(ByVal source As Object, ByVal e As GridPageSizeChangedEventArgs) Handles RadGrid1.PageSizeChanged
        LoadData()
    End Sub

    Private Sub RadGrid1_SortCommand(ByVal source As Object, ByVal e As GridSortCommandEventArgs) Handles RadGrid1.SortCommand
        LoadData()
    End Sub

    Public Function GetDataTable(ByVal query As String) As DataTable
        Dim ConnString As String = ConfigurationManager.ConnectionStrings("CNashConnection").ConnectionString
        Dim conn As SqlConnection = New SqlConnection(ConnString)
        Dim adapter As SqlDataAdapter = New SqlDataAdapter
        adapter.SelectCommand = New SqlCommand(query, conn)
        Dim table1 As New DataTable
        conn.Open()
        Try
            adapter.Fill(table1)
        Finally
            conn.Close()
        End Try
        Return table1
    End Function

    

End Class
