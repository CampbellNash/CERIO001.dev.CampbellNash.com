﻿Option Strict Off
Option Explicit On
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  Page Title       : NashBLL.vb                        '
'  Description      : Data access and BLL routines      ' 
'  Author           : Brian McAulay                     '
'  Creation Date    : 06 Mar 2013                       '
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
Imports System.Net
Imports System.IO
Imports System.Configuration
Imports Microsoft.VisualBasic
Imports System.Net.Mail

Namespace MasterClass
    Public Class NashBLL
        Public Shared strConnString As String = System.Configuration.ConfigurationManager.ConnectionStrings("CNashConnection").ConnectionString

#Region " General Items "

        Public Shared Function LoginUser(ByVal UserName As String, ByVal Password As String) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("LoginUser", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@UserName", UserName)
            ObjCmd.Parameters.AddWithValue("@Password", Password)
            paramReturn = ObjCmd.Parameters.AddWithValue("ReturnValue", DbType.Int32)
            paramReturn.Direction = ParameterDirection.ReturnValue
            Dim MyDataSet As DataSet
            Dim sqlMyAdapter As SqlDataAdapter
            'Build our dataset
            sqlMyAdapter = New SqlDataAdapter
            MyDataSet = New DataSet
            sqlMyAdapter.SelectCommand = ObjCmd
            Try
                sqlMyAdapter.SelectCommand.Connection.Open()
                sqlMyAdapter.Fill(MyDataSet, "LoginDetails")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function GetBusinessAreas() As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetBusinessAreas", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            paramReturn = ObjCmd.Parameters.AddWithValue("ReturnValue", DbType.Int32)
            paramReturn.Direction = ParameterDirection.ReturnValue
            Dim MyDataSet As DataSet
            Dim sqlMyAdapter As SqlDataAdapter
            'Build our dataset
            sqlMyAdapter = New SqlDataAdapter
            MyDataSet = New DataSet
            sqlMyAdapter.SelectCommand = ObjCmd
            Try
                sqlMyAdapter.SelectCommand.Connection.Open()
                sqlMyAdapter.Fill(MyDataSet, "BusinessAreas")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function GetCountries() As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetCountries", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            paramReturn = ObjCmd.Parameters.AddWithValue("ReturnValue", DbType.Int32)
            paramReturn.Direction = ParameterDirection.ReturnValue
            Dim MyDataSet As DataSet
            Dim sqlMyAdapter As SqlDataAdapter
            'Build our dataset
            sqlMyAdapter = New SqlDataAdapter
            MyDataSet = New DataSet
            sqlMyAdapter.SelectCommand = ObjCmd
            Try
                sqlMyAdapter.SelectCommand.Connection.Open()
                sqlMyAdapter.Fill(MyDataSet, "Countries")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function



#End Region

#Region " User Forms "

        Public Shared Function GetMyDetails(ByVal ContactID As String) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetMyDetails", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@ContactID", ContactID)
            paramReturn = ObjCmd.Parameters.AddWithValue("ReturnValue", DbType.Int32)
            paramReturn.Direction = ParameterDirection.ReturnValue
            Dim MyDataSet As DataSet
            Dim sqlMyAdapter As SqlDataAdapter
            'Build our dataset
            sqlMyAdapter = New SqlDataAdapter
            MyDataSet = New DataSet
            sqlMyAdapter.SelectCommand = ObjCmd
            Try
                sqlMyAdapter.SelectCommand.Connection.Open()
                sqlMyAdapter.Fill(MyDataSet, "MyDetails")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function GetUserTitles() As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetUserTitles", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            paramReturn = ObjCmd.Parameters.AddWithValue("ReturnValue", DbType.Int32)
            paramReturn.Direction = ParameterDirection.ReturnValue
            Dim MyDataSet As DataSet
            Dim sqlMyAdapter As SqlDataAdapter
            'Build our dataset
            sqlMyAdapter = New SqlDataAdapter
            MyDataSet = New DataSet
            sqlMyAdapter.SelectCommand = ObjCmd
            Try
                sqlMyAdapter.SelectCommand.Connection.Open()
                sqlMyAdapter.Fill(MyDataSet, "UserTitles")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function GetContactTypes() As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetContactTypes", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            paramReturn = ObjCmd.Parameters.AddWithValue("ReturnValue", DbType.Int32)
            paramReturn.Direction = ParameterDirection.ReturnValue
            Dim MyDataSet As DataSet
            Dim sqlMyAdapter As SqlDataAdapter
            'Build our dataset
            sqlMyAdapter = New SqlDataAdapter
            MyDataSet = New DataSet
            sqlMyAdapter.SelectCommand = ObjCmd
            Try
                sqlMyAdapter.SelectCommand.Connection.Open()
                sqlMyAdapter.Fill(MyDataSet, "ContactTypes")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function UpdateMyDetails(ByVal ContactID As Integer, _
                                ByVal TitleID As Integer, _
                                ByVal FirstName As String, _
                                ByVal Surname As String, _
                                ByVal EmailAddress As String, _
                                ByVal TelephoneNumber As String, _
                                ByVal MobileNumber As String, _
                                ByVal FaceBook As String, _
                                ByVal Twitter As String, _
                                ByVal UserName As String, _
                                ByVal Password As String, _
                                ByVal ContactTypeID As Integer, _
                                ByVal JobTitle As String) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("UpdateMyDetails", Conn)
            Dim paramReturn As SqlParameter = Nothing
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@ContactID", ContactID)
            ObjCmd.Parameters.AddWithValue("@TitleID", TitleID)
            ObjCmd.Parameters.AddWithValue("@FirstName", FirstName)
            ObjCmd.Parameters.AddWithValue("@Surname", Surname)
            ObjCmd.Parameters.AddWithValue("@EmailAddress", EmailAddress)
            ObjCmd.Parameters.AddWithValue("@TelephoneNumber", TelephoneNumber)
            ObjCmd.Parameters.AddWithValue("@MobileNumber", MobileNumber)
            ObjCmd.Parameters.AddWithValue("@FaceBook", FaceBook)
            ObjCmd.Parameters.AddWithValue("@Twitter", Twitter)
            ObjCmd.Parameters.AddWithValue("@UserName", UserName)
            ObjCmd.Parameters.AddWithValue("@Password", Password)
            ObjCmd.Parameters.AddWithValue("@ContactTypeID", ContactTypeID)
            ObjCmd.Parameters.AddWithValue("@JobTitle", JobTitle)
            paramReturn = ObjCmd.Parameters.AddWithValue("ReturnValue", DbType.Int32)
            paramReturn.Direction = ParameterDirection.ReturnValue
            Try
                Conn.Open()
                ObjCmd.ExecuteNonQuery()
            Finally
                Conn.Close()
            End Try
            Return paramReturn.Value
        End Function

#End Region

#Region " Eastern Tests "

        Public Shared Function GetEasternText() As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetEasternText", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            paramReturn = ObjCmd.Parameters.AddWithValue("ReturnValue", DbType.Int32)
            paramReturn.Direction = ParameterDirection.ReturnValue
            Dim MyDataSet As DataSet
            Dim sqlMyAdapter As SqlDataAdapter
            'Build our dataset
            sqlMyAdapter = New SqlDataAdapter
            MyDataSet = New DataSet
            sqlMyAdapter.SelectCommand = ObjCmd
            Try
                sqlMyAdapter.SelectCommand.Connection.Open()
                sqlMyAdapter.Fill(MyDataSet, "EasternText")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function UpdateEasternText(ByVal TestString As String, _
                                ByVal TestPassword As String, _
                                ByVal TestBlob As String) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("UpdateEasternTests", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@TestString", TestString)
            ObjCmd.Parameters.AddWithValue("@TestPassword", TestPassword)
            ObjCmd.Parameters.AddWithValue("@TestBlob", TestBlob)
            Try
                Conn.Open()
                ObjCmd.ExecuteNonQuery()
            Finally
                Conn.Close()
            End Try
            Return 0
        End Function

#End Region

#Region " Customers & Suppliers "

        Public Shared Function GetMySuppliers(ByVal CompanyID As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetMySuppliers", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@CompanyID", CompanyID)
            paramReturn = ObjCmd.Parameters.AddWithValue("ReturnValue", DbType.Int32)
            paramReturn.Direction = ParameterDirection.ReturnValue
            Dim MyDataSet As DataSet
            Dim sqlMyAdapter As SqlDataAdapter
            'Build our dataset
            sqlMyAdapter = New SqlDataAdapter
            MyDataSet = New DataSet
            sqlMyAdapter.SelectCommand = ObjCmd
            Try
                sqlMyAdapter.SelectCommand.Connection.Open()
                sqlMyAdapter.Fill(MyDataSet, "MySuppliers")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function GetMyCustomers(ByVal CompanyID As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetMyCustomers", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@CompanyID", CompanyID)
            paramReturn = ObjCmd.Parameters.AddWithValue("ReturnValue", DbType.Int32)
            paramReturn.Direction = ParameterDirection.ReturnValue
            Dim MyDataSet As DataSet
            Dim sqlMyAdapter As SqlDataAdapter
            'Build our dataset
            sqlMyAdapter = New SqlDataAdapter
            MyDataSet = New DataSet
            sqlMyAdapter.SelectCommand = ObjCmd
            Try
                sqlMyAdapter.SelectCommand.Connection.Open()
                sqlMyAdapter.Fill(MyDataSet, "MyCustomers")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function GetMyCompanies(ByVal ContactID As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetMyCompanies", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@ContactID", ContactID)
            paramReturn = ObjCmd.Parameters.AddWithValue("ReturnValue", DbType.Int32)
            paramReturn.Direction = ParameterDirection.ReturnValue
            Dim MyDataSet As DataSet
            Dim sqlMyAdapter As SqlDataAdapter
            'Build our dataset
            sqlMyAdapter = New SqlDataAdapter
            MyDataSet = New DataSet
            sqlMyAdapter.SelectCommand = ObjCmd
            Try
                sqlMyAdapter.SelectCommand.Connection.Open()
                sqlMyAdapter.Fill(MyDataSet, "MyCompanies")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

#End Region

#Region " Questionnaires "

        Public Shared Function GetMyQuestionnaire(ByVal CompanyID As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetMyQuestionnaire", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@CompanyID", CompanyID)
            paramReturn = ObjCmd.Parameters.AddWithValue("ReturnValue", DbType.Int32)
            paramReturn.Direction = ParameterDirection.ReturnValue
            Dim MyDataSet As DataSet
            Dim sqlMyAdapter As SqlDataAdapter
            'Build our dataset
            sqlMyAdapter = New SqlDataAdapter
            MyDataSet = New DataSet
            sqlMyAdapter.SelectCommand = ObjCmd
            Try
                sqlMyAdapter.SelectCommand.Connection.Open()
                sqlMyAdapter.Fill(MyDataSet, "MyQuestions")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function UpdateQuestionAnswer(ByVal LogID As Integer, _
                                                    ByVal ContentID As Integer, _
                                                    ByVal NewValue As String, _
                                                    ByVal UpdatedBy As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("UpdateQuestionAnswer", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@LogID", LogID)
            ObjCmd.Parameters.AddWithValue("@ContentID", ContentID)
            ObjCmd.Parameters.AddWithValue("@NewValue", NewValue)
            ObjCmd.Parameters.AddWithValue("@ModifiedBy", UpdatedBy)
            Try
                Conn.Open()
                ObjCmd.ExecuteNonQuery()
            Finally
                Conn.Close()
            End Try
            Return 0
        End Function

        Public Shared Function UpdateQuestionPanelReference(ByVal ContentID As Integer, _
                                                            ByVal PanelName As String) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("UpdateQuestionPanelReference", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@ContentID", ContentID)
            ObjCmd.Parameters.AddWithValue("@PanelName", PanelName)
            Try
                Conn.Open()
                ObjCmd.ExecuteNonQuery()
            Finally
                Conn.Close()
            End Try
            Return 0
        End Function

        Public Shared Function GetDependentQuestionRule(ByVal PanelName As String) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetDependentQuestionRule", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@PanelName", PanelName)
            paramReturn = ObjCmd.Parameters.AddWithValue("ReturnValue", DbType.Int32)
            paramReturn.Direction = ParameterDirection.ReturnValue
            Dim MyDataSet As DataSet
            Dim sqlMyAdapter As SqlDataAdapter
            'Build our dataset
            sqlMyAdapter = New SqlDataAdapter
            MyDataSet = New DataSet
            sqlMyAdapter.SelectCommand = ObjCmd
            Try
                sqlMyAdapter.SelectCommand.Connection.Open()
                sqlMyAdapter.Fill(MyDataSet, "NewQuestionList")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function QuestionnaireGetParentCompanyDetails(ByVal CompanyID As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetParentCompanyDetails", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@CompanyID", CompanyID)
            paramReturn = ObjCmd.Parameters.AddWithValue("ReturnValue", DbType.Int32)
            paramReturn.Direction = ParameterDirection.ReturnValue
            Dim MyDataSet As DataSet
            Dim sqlMyAdapter As SqlDataAdapter
            'Build our dataset
            sqlMyAdapter = New SqlDataAdapter
            MyDataSet = New DataSet
            sqlMyAdapter.SelectCommand = ObjCmd
            Try
                sqlMyAdapter.SelectCommand.Connection.Open()
                sqlMyAdapter.Fill(MyDataSet, "ParentsList")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function UpateParentCompanyLine(ByVal ItemID As Integer, _
                                                    ByVal ParentCompanyName As String, _
                                                    ByVal ParentCompanyNumber As String, _
                                                    ByVal ParentCompanyCountry As String, _
                                                    ByVal PercentageOwned As Double) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("UpdateParentCompanyLine", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@ItemID", ItemID)
            ObjCmd.Parameters.AddWithValue("@ParentCompanyName", ParentCompanyName)
            ObjCmd.Parameters.AddWithValue("@ParentCompanyNumber", ParentCompanyNumber)
            ObjCmd.Parameters.AddWithValue("@ParentCompanyCountry", ParentCompanyCountry)
            ObjCmd.Parameters.AddWithValue("@PercentageOwned", PercentageOwned)
            Try
                Conn.Open()
                ObjCmd.ExecuteNonQuery()
            Finally
                Conn.Close()
            End Try
            Return 0
        End Function

        Public Shared Function AddParentCompanyLine(ByVal CompanyID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("AddNewParentCompanyLine", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@CompanyID", CompanyID)
            Try
                Conn.Open()
                ObjCmd.ExecuteNonQuery()
            Finally
                Conn.Close()
            End Try
            Return 0
        End Function

        Public Shared Function DeleteParentCompanyLine(ByVal ItemID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("DeleteParentCompanyLine", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@ItemID", ItemID)
            Try
                Conn.Open()
                ObjCmd.ExecuteNonQuery()
            Finally
                Conn.Close()
            End Try
            Return 0
        End Function

        Public Shared Function QuestionnaireGetParentShareholderDetails(ByVal CompanyID As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetShareholderList", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@CompanyID", CompanyID)
            paramReturn = ObjCmd.Parameters.AddWithValue("ReturnValue", DbType.Int32)
            paramReturn.Direction = ParameterDirection.ReturnValue
            Dim MyDataSet As DataSet
            Dim sqlMyAdapter As SqlDataAdapter
            'Build our dataset
            sqlMyAdapter = New SqlDataAdapter
            MyDataSet = New DataSet
            sqlMyAdapter.SelectCommand = ObjCmd
            Try
                sqlMyAdapter.SelectCommand.Connection.Open()
                sqlMyAdapter.Fill(MyDataSet, "ShareholderList")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

#End Region

    End Class
End Namespace
