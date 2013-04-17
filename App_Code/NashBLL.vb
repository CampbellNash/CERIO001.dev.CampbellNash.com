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

        Public Shared Function AddShareholderLine(ByVal CompanyID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("AddNewShareholderLine", Conn)
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

        Public Shared Function DeleteShareholderLine(ByVal ItemID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("DeleteShareholderLine", Conn)
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

        Public Shared Function UpateShareholderLine(ByVal ItemID As Integer, _
                                                    ByVal ShareholderName As String, _
                                                    ByVal ShareholderNationality As String, _
                                                    ByVal PercentageOwned As Double) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("UpdateShareholderLine", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@ItemID", ItemID)
            ObjCmd.Parameters.AddWithValue("@ShareholderName", ShareholderName)
            ObjCmd.Parameters.AddWithValue("@ShareholderNationality", ShareholderNationality)
            ObjCmd.Parameters.AddWithValue("@PercentageOwned", PercentageOwned)
            Try
                Conn.Open()
                ObjCmd.ExecuteNonQuery()
            Finally
                Conn.Close()
            End Try
            Return 0
        End Function

        Public Shared Function QuestionnaireGetDirectorDetails(ByVal CompanyID As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetDirectorList", Conn)
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
                sqlMyAdapter.Fill(MyDataSet, "DirectorList")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function UpateDirectorLine(ByVal ItemID As Integer, _
                                                    ByVal DirectorName As String, _
                                                    ByVal DirectorNationality As String, _
                                                    ByVal DirectorJobTitle As String) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("UpdateDirectorLine", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@ItemID", ItemID)
            ObjCmd.Parameters.AddWithValue("@DirectorName", DirectorName)
            ObjCmd.Parameters.AddWithValue("@DirectorNationality", DirectorNationality)
            ObjCmd.Parameters.AddWithValue("@DirectorJobTitle", DirectorJobTitle)
            Try
                Conn.Open()
                ObjCmd.ExecuteNonQuery()
            Finally
                Conn.Close()
            End Try
            Return 0
        End Function

        Public Shared Function DeleteDirectorLine(ByVal ItemID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("DeleteDirectorLine", Conn)
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

        Public Shared Function AddDirectorLine(ByVal CompanyID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("AddNewDirectorLine", Conn)
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

        Public Shared Function QuestionnaireGetGovtEmployeeDetails(ByVal CompanyID As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetGovtEmployeeList", Conn)
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
                sqlMyAdapter.Fill(MyDataSet, "EmployeeList")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function DeleteRelativeLine(ByVal ItemID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("DeleteRelativeLine", Conn)
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

        Public Shared Function UpdateRelativeLine(ByVal ItemID As Integer, _
                                                    ByVal PersonName As String, _
                                                    ByVal RelativeName As String, _
                                                    ByVal RelationshipType As String, _
                                                    ByVal LastJob As String, _
                                                    ByVal JobCountry As String, _
                                                    ByVal DateEnded As Date) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("UpdateRelativeLine", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@ItemID", ItemID)
            ObjCmd.Parameters.AddWithValue("@PersonName", PersonName)
            ObjCmd.Parameters.AddWithValue("@RelativeName", RelativeName)
            ObjCmd.Parameters.AddWithValue("@RelationshipType", RelationshipType)
            ObjCmd.Parameters.AddWithValue("@LastJob", LastJob)
            ObjCmd.Parameters.AddWithValue("@JobCountry", JobCountry)
            ObjCmd.Parameters.AddWithValue("@DateEnded", DateEnded)
            Try
                Conn.Open()
                ObjCmd.ExecuteNonQuery()
            Finally
                Conn.Close()
            End Try
            Return 0
        End Function

        Public Shared Function AddRelativeLine(ByVal CompanyID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("AddNewRelativeLine", Conn)
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

        Public Shared Function GetMinerals() As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetMinerals", Conn)
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
                sqlMyAdapter.Fill(MyDataSet, "Minerals")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function QuestionnaireGetMineralPurposeDetails(ByVal CompanyID As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetPurposeList", Conn)
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
                sqlMyAdapter.Fill(MyDataSet, "PurposeList")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function DeletePurposeLine(ByVal ItemID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("DeletePurposeLine", Conn)
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

        Public Shared Function UpdatePurposeLine(ByVal ItemID As Integer, _
                                                    ByVal MineralID As Integer, _
                                                    ByVal Description As String) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("UpdatePurposeLine", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@ItemID", ItemID)
            ObjCmd.Parameters.AddWithValue("@MineralID", MineralID)
            ObjCmd.Parameters.AddWithValue("@Description", Description)
            Try
                Conn.Open()
                ObjCmd.ExecuteNonQuery()
            Finally
                Conn.Close()
            End Try
            Return 0
        End Function

        Public Shared Function AddPurposeLine(ByVal CompanyID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("AddNewPurposeLine", Conn)
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

        Public Shared Function QuestionnaireGetMineralProcessDetails(ByVal CompanyID As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetProcessList", Conn)
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
                sqlMyAdapter.Fill(MyDataSet, "ProcessList")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function DeleteProcessLine(ByVal ItemID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("DeleteProcessLine", Conn)
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

        Public Shared Function UpdateProcessLine(ByVal ItemID As Integer, _
                                                    ByVal MineralID As Integer, _
                                                    ByVal Description As String) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("UpdateProcessLine", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@ItemID", ItemID)
            ObjCmd.Parameters.AddWithValue("@MineralID", MineralID)
            ObjCmd.Parameters.AddWithValue("@Description", Description)
            Try
                Conn.Open()
                ObjCmd.ExecuteNonQuery()
            Finally
                Conn.Close()
            End Try
            Return 0
        End Function

        Public Shared Function AddProcessLine(ByVal CompanyID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("AddNewProcessLine", Conn)
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

        Public Shared Function QuestionnaireGetMineralComponentDetails(ByVal CompanyID As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetComponentList", Conn)
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
                sqlMyAdapter.Fill(MyDataSet, "ComponentList")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function DeleteComponentLine(ByVal ItemID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("DeleteComponentLine", Conn)
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

        Public Shared Function UpdateComponentLine(ByVal ItemID As Integer, _
                                                    ByVal MineralID As Integer, _
                                                    ByVal Description As String) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("UpdateComponentLine", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@ItemID", ItemID)
            ObjCmd.Parameters.AddWithValue("@MineralID", MineralID)
            ObjCmd.Parameters.AddWithValue("@Description", Description)
            Try
                Conn.Open()
                ObjCmd.ExecuteNonQuery()
            Finally
                Conn.Close()
            End Try
            Return 0
        End Function

        Public Shared Function AddComponentLine(ByVal CompanyID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("AddNewComponentLine", Conn)
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

        Public Shared Function GetSmelterList() As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetSmelterList", Conn)
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
                sqlMyAdapter.Fill(MyDataSet, "SmelterList")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function GetDangerousCountries() As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetDangerousCountryList", Conn)
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
                sqlMyAdapter.Fill(MyDataSet, "CountryList")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function GetrelationshipCategories() As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetRelationshipCategories", Conn)
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
                sqlMyAdapter.Fill(MyDataSet, "relationshipCategories")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function QuestionnaireGetMineralScrapDetails(ByVal CompanyID As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetScrapList", Conn)
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
                sqlMyAdapter.Fill(MyDataSet, "ScrapList")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function DeleteScrapLine(ByVal ItemID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("DeleteScrapLine", Conn)
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

        Public Shared Function UpdateScrapLine(ByVal ItemID As Integer, _
                                                    ByVal MineralID As Integer, _
                                                    ByVal Description As String) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("UpdateScrapLine", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@ItemID", ItemID)
            ObjCmd.Parameters.AddWithValue("@MineralID", MineralID)
            ObjCmd.Parameters.AddWithValue("@Description", Description)
            Try
                Conn.Open()
                ObjCmd.ExecuteNonQuery()
            Finally
                Conn.Close()
            End Try
            Return 0
        End Function

        Public Shared Function AddScrapLine(ByVal CompanyID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("AddNewScrapLine", Conn)
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

        Public Shared Function QuestionnaireGetMineralRecycleDetails(ByVal CompanyID As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetRecycleList", Conn)
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
                sqlMyAdapter.Fill(MyDataSet, "RecycleList")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function DeleteRecycleLine(ByVal ItemID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("DeleteRecycleLine", Conn)
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

        Public Shared Function UpdateRecycleLine(ByVal ItemID As Integer, _
                                                    ByVal MineralID As Integer, _
                                                    ByVal Description As String) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("UpdateRecycleLine", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@ItemID", ItemID)
            ObjCmd.Parameters.AddWithValue("@MineralID", MineralID)
            ObjCmd.Parameters.AddWithValue("@Description", Description)
            Try
                Conn.Open()
                ObjCmd.ExecuteNonQuery()
            Finally
                Conn.Close()
            End Try
            Return 0
        End Function

        Public Shared Function AddRecycleLine(ByVal CompanyID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("AddNewRecycleLine", Conn)
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

        Public Shared Function QuestionnaireGetExtractionDetails(ByVal CompanyID As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetExtractionList", Conn)
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
                sqlMyAdapter.Fill(MyDataSet, "ExtractionList")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function DeleteExtractionLine(ByVal ItemID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("DeleteExtractionLine", Conn)
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

        Public Shared Function UpdateExtractionLine(ByVal ItemID As Integer, _
                                                    ByVal MineralID As Integer, _
                                                    ByVal Quantity As Integer, _
                                                    ByVal DateOfExtraction As Date, _
                                                    ByVal MethodID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("UpdateExtractionLine", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@ItemID", ItemID)
            ObjCmd.Parameters.AddWithValue("@MineralID", MineralID)
            ObjCmd.Parameters.AddWithValue("@Quantity", Quantity)
            ObjCmd.Parameters.AddWithValue("@DateOfExtraction", DateOfExtraction)
            ObjCmd.Parameters.AddWithValue("@MethodID", MethodID)
            Try
                Conn.Open()
                ObjCmd.ExecuteNonQuery()
            Finally
                Conn.Close()
            End Try
            Return 0
        End Function

        Public Shared Function AddExtractionLine(ByVal CompanyID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("AddNewExtractionLine", Conn)
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

        Public Shared Function GetExtractionMethods() As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetExtractionMethods", Conn)
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
                sqlMyAdapter.Fill(MyDataSet, "MethodList")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function QuestionnaireGetFacilityDetails(ByVal CompanyID As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetFacilityList", Conn)
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
                sqlMyAdapter.Fill(MyDataSet, "FacilityList")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function DeleteFacilityLine(ByVal ItemID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("DeleteFacilityLine", Conn)
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

        Public Shared Function UpdateFacilityLine(ByVal ItemID As Integer, _
                                                    ByVal FacilityName As String, _
                                                    ByVal FacilityLocation As String) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("UpdateFacilityLine", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@ItemID", ItemID)
            ObjCmd.Parameters.AddWithValue("@FacilityName", FacilityName)
            ObjCmd.Parameters.AddWithValue("@FacilityLocation", FacilityLocation)
            Try
                Conn.Open()
                ObjCmd.ExecuteNonQuery()
            Finally
                Conn.Close()
            End Try
            Return 0
        End Function

        Public Shared Function AddFacilityLine(ByVal CompanyID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("AddNewFacilityLine", Conn)
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

#End Region

    End Class
End Namespace
