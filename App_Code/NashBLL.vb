Option Strict Off
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

        Public Shared Function SendMail(ByVal RecipientList As String, _
                                        ByVal CCList As String, _
                                        ByVal BCCList As String, _
                                        ByVal MailBody As String, _
                                        ByVal Subject As String, _
                                        ByVal Attachment As String, _
                                        ByVal IsHTML As Boolean) As Integer

            Dim MyMailer As New MailMessage
            Dim MySmtpClient As New SmtpClient()
            Dim RecipientArray As Array = Split(RecipientList, ",")
            Dim CCArray As Array = Split(CCList, ",")
            Dim BCCArray As Array = Split(BCCList, ",")
            Dim AttachmentArray = Split(Attachment, ",")
            Dim LoopCount As Integer = 0
            Try
                'Send the email with details
                MyMailer = New MailMessage
                MyMailer.From = New MailAddress("support@campbellnash.com")
                For LoopCount = LBound(RecipientArray) To UBound(RecipientArray)
                    MyMailer.To.Add(New MailAddress(RecipientArray(LoopCount)))
                Next
                If CCList <> "" Then
                    For LoopCount = LBound(CCArray) To UBound(CCArray)
                        MyMailer.CC.Add(New MailAddress(CCArray(LoopCount)))
                    Next
                End If
                If BCCList <> "" Then
                    For LoopCount = LBound(BCCArray) To UBound(BCCArray)
                        MyMailer.Bcc.Add(New MailAddress(BCCArray(LoopCount)))
                    Next
                End If
                If Attachment <> "" Then
                    For LoopCount = LBound(AttachmentArray) To UBound(AttachmentArray)
                        MyMailer.Attachments.Add(New Attachment(AttachmentArray(LoopCount)))
                    Next
                End If
                MyMailer.Subject = Subject
                MyMailer.Body = MailBody
                If IsHTML Then
                    'Send a HTML formatted mail
                    MyMailer.IsBodyHtml = True
                Else
                    'Send a non HTML mail
                    MyMailer.IsBodyHtml = False
                End If
                'Now send the mail
                MySmtpClient.Send(MyMailer)
                Return 0
            Catch
                'Some kind of problem occurred
                Return -1
            End Try
        End Function

        Public Shared Function SendUserAndPassword(ByVal EmailAddress As String) As String
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("SendUserAndPassword", Conn)
            Dim rsMain As SqlDataReader
            Dim EmailResult As String = ""
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@EmailAddress", EmailAddress)
            Conn.Open()
            rsMain = ObjCmd.ExecuteReader()
            rsMain.Read()
            If rsMain("Email") = "-1" Then
                'This account has not been verified
                EmailResult = "-1"
            ElseIf rsMain("Email") = "-2" Then
                'No such user was found
                EmailResult = "-2"
            Else
                'We got a record back so we can send the items back to .NET
                EmailResult = rsMain("Email") & "," & rsMain("Username") & "," & rsMain("Password") & "," & rsMain("FirstName")
            End If

            'Close any connections & recordsets now
            rsMain.Close()
            Conn.Close()
            'Inform our calling site of the result
            SendUserAndPassword = EmailResult
        End Function

        Public Shared Function UploadFile(ByVal CompanyID As Integer, _
                                          ByVal ContactID As Integer, _
                                          ByVal FileName As String) As Integer

            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("AddNewFile", Conn)
            Dim paramReturn As SqlParameter = Nothing
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@CompanyID", CompanyID)
            ObjCmd.Parameters.AddWithValue("@ContactID", ContactID)
            ObjCmd.Parameters.AddWithValue("@FileName", FileName)
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

        Public Shared Function GetCompanyFiles(ByVal CompanyID As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetCompanyFiles", Conn)
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
                sqlMyAdapter.Fill(MyDataSet, "FileList")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet

        End Function

        Public Shared Function DeleteFile(ByVal FileID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("DeleteFile", Conn)
            Dim paramReturn As SqlParameter = Nothing
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@FileID", FileID)
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

        Public Shared Function GetCompanyDetailsByID(ByVal CompanyID As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetCompanyDetailsByID", Conn)
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
                sqlMyAdapter.Fill(MyDataSet, "CompanyDetails")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet

        End Function

#End Region

#Region " Demo Scripts "

        Public Shared Function ListAllDemoCompanies() As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("ListAllDemoCompanies", Conn)
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
                sqlMyAdapter.Fill(MyDataSet, "CompanyList")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function ListAllDemoUsers() As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("ListAllDemoUsers", Conn)
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
                sqlMyAdapter.Fill(MyDataSet, "UserList")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function DeleteCompany(ByVal CompanyID As String) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("DeleteCompany", Conn)
            Dim paramReturn As SqlParameter = Nothing
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@CompanyID", CompanyID)
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

        Public Shared Function DeleteCompanyConnections(ByVal CompanyID As String) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("DeleteCompanyConnections", Conn)
            Dim paramReturn As SqlParameter = Nothing
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@CompanyID", CompanyID)
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

        Public Shared Function DeleteQuestionnaire(ByVal CompanyID As String) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("DeleteStandardQuestionnaire", Conn)
            Dim paramReturn As SqlParameter = Nothing
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@CompanyID", CompanyID)
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

        Public Shared Function DeleteUser(ByVal ContactID As String) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("DeleteUser", Conn)
            Dim paramReturn As SqlParameter = Nothing
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@ContactID", ContactID)
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
                                          ByVal UserName As String, _
                                          ByVal Password As String) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("UpdateMyDetails", Conn)
            Dim paramReturn As SqlParameter = Nothing
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@ContactID", ContactID)
            ObjCmd.Parameters.AddWithValue("@TitleID", TitleID)
            ObjCmd.Parameters.AddWithValue("@FirstName", FirstName)
            ObjCmd.Parameters.AddWithValue("@Surname", Surname)
            ObjCmd.Parameters.AddWithValue("@EmailAddress", EmailAddress)
            ObjCmd.Parameters.AddWithValue("@UserName", UserName)
            ObjCmd.Parameters.AddWithValue("@Password", Password)
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

        Public Shared Function SearchForCompanies(ByVal SearchString As String, ByVal ContactID As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("SearchCompanies", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@SearchString", SearchString)
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
                sqlMyAdapter.Fill(MyDataSet, "CompanyList")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function SearchForCustomers(ByVal CompanyID As Integer, _
                                                 ByVal ContactID As Integer, _
                                                 ByVal SearchString As String) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("SearchCustomers", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@CompanyID", CompanyID)
            ObjCmd.Parameters.AddWithValue("@ContactID", ContactID)
            ObjCmd.Parameters.AddWithValue("@SearchString", SearchString)
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
                sqlMyAdapter.Fill(MyDataSet, "CompanyList")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function SearchForSuppliers(ByVal CompanyID As Integer, _
                                                 ByVal ContactID As Integer, _
                                                 ByVal SearchString As String) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("SearchSuppliers", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@CompanyID", CompanyID)
            ObjCmd.Parameters.AddWithValue("@ContactID", ContactID)
            ObjCmd.Parameters.AddWithValue("@SearchString", SearchString)
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
                sqlMyAdapter.Fill(MyDataSet, "CompanyList")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function AddNewCompany(ByVal ParentCompanyID As Integer, _
                                                ByVal ContactID As Integer, _
                                                ByVal CompanyName As String, _
                                                ByVal Address1 As String, _
                                                ByVal Address2 As String, _
                                                ByVal Address3 As String, _
                                                ByVal Address4 As String, _
                                                ByVal City As String, _
                                                ByVal Postcode As String, _
                                                ByVal TelephoneNumber As String, _
                                                ByVal FaxNumber As String, _
                                                ByVal Telex As String, _
                                                ByVal WebURL As String, _
                                                ByVal EmailAddress As String, _
                                                ByVal FaceBookURL As String, _
                                                ByVal Twitter As String, _
                                                ByVal CountryID As Integer, _
                                                ByVal BusinessAreaID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("AddNewCompany", Conn)
            Dim paramReturn As SqlParameter = Nothing
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@ParentCompanyID", ParentCompanyID)
            ObjCmd.Parameters.AddWithValue("@ContactID", ContactID)
            ObjCmd.Parameters.AddWithValue("@CompanyName", CompanyName)
            ObjCmd.Parameters.AddWithValue("@Address1", Address1)
            ObjCmd.Parameters.AddWithValue("@Address2", Address2)
            ObjCmd.Parameters.AddWithValue("@Address3", Address3)
            ObjCmd.Parameters.AddWithValue("@Address4", Address4)
            ObjCmd.Parameters.AddWithValue("@City", City)
            ObjCmd.Parameters.AddWithValue("@Postcode", Postcode)
            ObjCmd.Parameters.AddWithValue("@TelephoneNumber", TelephoneNumber)
            ObjCmd.Parameters.AddWithValue("@FaxNumber", FaxNumber)
            ObjCmd.Parameters.AddWithValue("@Telex", Telex)
            ObjCmd.Parameters.AddWithValue("@WebURL", WebURL)
            ObjCmd.Parameters.AddWithValue("@EmailAddress", EmailAddress)
            ObjCmd.Parameters.AddWithValue("@FaceBookURL", FaceBookURL)
            ObjCmd.Parameters.AddWithValue("@Twitter", Twitter)
            ObjCmd.Parameters.AddWithValue("@CountryID", CountryID)
            ObjCmd.Parameters.AddWithValue("@BusinessAreaID", BusinessAreaID)
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

        Public Shared Function SearchForParentCompanies(ByVal SearchString As String) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("SearchParent", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@SearchString", SearchString)
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
                sqlMyAdapter.Fill(MyDataSet, "CompanyList")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function AddNewUser(ByVal TitleID As Integer, _
                                          ByVal FirstName As String, _
                                          ByVal Surname As String, _
                                          ByVal EmailAddress As String, _
                                          ByVal UserName As String, _
                                          ByVal Password As String, _
                                         ByVal VerificationCode As String) As Integer

            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("AddNewUser", Conn)
            Dim paramReturn As SqlParameter = Nothing
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@TitleID", TitleID)
            ObjCmd.Parameters.AddWithValue("@FirstName", FirstName)
            ObjCmd.Parameters.AddWithValue("@Surname", Surname)
            ObjCmd.Parameters.AddWithValue("@EmailAddress", EmailAddress)
            ObjCmd.Parameters.AddWithValue("@UserName", UserName)
            ObjCmd.Parameters.AddWithValue("@Password", Password)
            ObjCmd.Parameters.AddWithValue("@VerificationCode", VerificationCode)
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

        Public Shared Function VerifyUser(ByVal VerificationCode As String) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("VerifyUser", Conn)
            Dim paramReturn As SqlParameter = Nothing
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@VerificationCode", VerificationCode)
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

        Public Shared Function UnVerifyAccount(ByVal ContactID As Integer, _
                                               ByVal VerificationCode As String) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("UnVerifyUser", Conn)
            Dim paramReturn As SqlParameter = Nothing
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@ContactID", ContactID)
            ObjCmd.Parameters.AddWithValue("@VerificationCode", VerificationCode)
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

        Public Shared Function CheckEmailAddressExists(ByVal EmailAddress As String) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("CheckEmailAddress", Conn)
            Dim paramReturn As SqlParameter = Nothing
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@EmailAddress", EmailAddress)
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

        Public Shared Function RequestCustomer(ByVal CompanyID As Integer, _
                                               ByVal SupplierCompanyID As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("RequestCustomer", Conn)
            Dim paramReturn As SqlParameter = Nothing
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@CompanyID", CompanyID)
            ObjCmd.Parameters.AddWithValue("@SupplierCompanyID",
                                           SupplierCompanyID)
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
                sqlMyAdapter.Fill(MyDataSet, "CompanyDetails")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function RequestSupplier(ByVal CompanyID As Integer, _
                                               ByVal SupplierCompanyID As Integer, _
                                               ByVal ContactID As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("RequestSupplier", Conn)
            Dim paramReturn As SqlParameter = Nothing
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@CompanyID", CompanyID)
            ObjCmd.Parameters.AddWithValue("@SupplierCompanyID", SupplierCompanyID)
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
                sqlMyAdapter.Fill(MyDataSet, "CompanyDetails")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function JoinCustomer(ByVal ContactID As Integer, ByVal CompanyID As Integer) As DataSet
            'This is the request to join an existing company as a member of it e.g. employee or director etc
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("JoinCompany", Conn)
            Dim paramReturn As SqlParameter = Nothing
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@ContactID", ContactID)
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
                sqlMyAdapter.Fill(MyDataSet, "CompanyDetails")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function ThisIsNotOneOfMyCompanies(ByVal ContactID As Integer, _
                                                         ByVal CompanyID As Integer) As Boolean
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("CheckMyCompanies", Conn)
            Dim paramReturn As SqlParameter = Nothing
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@ContactID", ContactID)
            ObjCmd.Parameters.AddWithValue("@CompanyID", CompanyID)
            paramReturn = ObjCmd.Parameters.AddWithValue("ReturnValue", DbType.Int32)
            paramReturn.Direction = ParameterDirection.ReturnValue
            Try
                Conn.Open()
                ObjCmd.ExecuteNonQuery()
            Finally
                Conn.Close()
            End Try
            Select Case paramReturn.Value
                Case 0
                    'This is not one of my companies so return true
                    Return True
                Case Else
                    'This is one of my companies so return false
                    Return False
            End Select

        End Function

        Public Shared Function GetAllMyUnApprovedCompanies(ByVal ContactID As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetAllMyUnapprovedCompanies", Conn)
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

        Public Shared Function GetAllMyUnapprovedSuppliers(ByVal ContactID As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetAllMyUnapprovedSuppliers", Conn)
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

        Public Shared Function GetAllMyUnapprovedCustomers(ByVal ContactID As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetAllMyUnapprovedCustomers", Conn)
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

        Public Shared Function GetMyUnapprovedSuppliers(ByVal CompanyID As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetMyUnapprovedSuppliers", Conn)
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
                sqlMyAdapter.Fill(MyDataSet, "MyCompanies")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function GetMyUnapprovedCustomers(ByVal CompanyID As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetMyUnapprovedCustomers", Conn)
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
                sqlMyAdapter.Fill(MyDataSet, "MyCompanies")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function GetMyUnapprovedCompanies(ByVal CompanyID As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetMyUnapprovedCompanies", Conn)
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
                sqlMyAdapter.Fill(MyDataSet, "MyCompanies")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function GetMyCompliantSupplierCount(ByVal CompanyID As Integer) As String
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("GetMyCompliantSupplierCount", Conn)
            Dim rsMain As SqlDataReader
            Dim Compliant As String = ""
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@CompanyID", CompanyID)
            Conn.Open()
            rsMain = ObjCmd.ExecuteReader()
            rsMain.Read()
            
            'We got a record back so we can send the items back to .NET
            Compliant = rsMain("CompliantSuppliers") & "," & rsMain("NonCompliantSuppliers") & "," & rsMain("CompliantCustomers") & "," & rsMain("NonCompliantCustomers")


            'Close any connections & recordsets now
            rsMain.Close()
            Conn.Close()
            Return Compliant
        End Function

        Public Shared Function GetStaffMemberDetails(ByVal MYID As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetStaffMemberDetails", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@MYID", MYID)
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

        Public Shared Function ApproveStaffMember(ByVal MYID As Integer, ByVal ContactID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("ApproveStaffMember", Conn)
            Dim paramReturn As SqlParameter = Nothing
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@MYID", MYID)
            ObjCmd.Parameters.AddWithValue("@ContactID", ContactID)
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

#Region " Questionnaires "

        Public Shared Function CloseQuestionnaire(ByVal CompanyID As Integer, _
                                                  ByVal ContactID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("CloseQuestionnaire", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@CompanyID", CompanyID)
            ObjCmd.Parameters.AddWithValue("@ContactID", ContactID)
            Try
                Conn.Open()
                ObjCmd.ExecuteNonQuery()
            Finally
                Conn.Close()
            End Try
            Return 0

        End Function

        Public Shared Function GetQuestionnaireCloser(ByVal CompanyID As Integer) As String
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("GetQuestionnaireCloser", Conn)
            Dim rsMain As SqlDataReader
            Dim Name As String = ""
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@CompanyID", CompanyID)
            Try
                Conn.Open()
                rsMain = ObjCmd.ExecuteReader(CommandBehavior.CloseConnection)
                'Get our record
                rsMain.Read()
                Name = rsMain("CloserName")
            Finally
                'Destroy object
                ObjCmd.Dispose()
                Conn.Close()
            End Try
            'Send login details back to page
            Return Name
        End Function

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

        Public Shared Function QuestionnaireGetTransportDetails(ByVal CompanyID As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetTransportList", Conn)
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
                sqlMyAdapter.Fill(MyDataSet, "TransportList")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function DeleteTransportLine(ByVal ItemID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("DeleteTransportLine", Conn)
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

        Public Shared Function UpdateTransportLine(ByVal ItemID As Integer, _
                                                    ByVal TransporterName As String, _
                                                    ByVal TransporterAddress As String) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("UpdateTransportLine", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@ItemID", ItemID)
            ObjCmd.Parameters.AddWithValue("@TransporterName", TransporterName)
            ObjCmd.Parameters.AddWithValue("@TransporterAddress", TransporterAddress)
            Try
                Conn.Open()
                ObjCmd.ExecuteNonQuery()
            Finally
                Conn.Close()
            End Try
            Return 0
        End Function

        Public Shared Function AddTransportLine(ByVal CompanyID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("AddNewTransportLine", Conn)
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

        Public Shared Function QuestionnaireGetOtherPaymentDetails(ByVal CompanyID As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetOtherPaymentList", Conn)
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
                sqlMyAdapter.Fill(MyDataSet, "PaymentList")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function DeleteOtherPaymentLine(ByVal ItemID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("DeleteOtherPaymentLine", Conn)
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

        Public Shared Function UpdateOtherPaymentLine(ByVal ItemID As Integer, _
                                                    ByVal PaymentAmount As String, _
                                                    ByVal PaymentDetails As String) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("UpdateOtherPaymentLine", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@ItemID", ItemID)
            ObjCmd.Parameters.AddWithValue("@PaymentAmount", PaymentAmount)
            ObjCmd.Parameters.AddWithValue("@PaymentDetails", PaymentDetails)
            Try
                Conn.Open()
                ObjCmd.ExecuteNonQuery()
            Finally
                Conn.Close()
            End Try
            Return 0
        End Function

        Public Shared Function AddOtherPaymentLine(ByVal CompanyID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("AddNewOtherPaymentLine", Conn)
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

        Public Shared Function QuestionnaireGetOtherTaxDetails(ByVal CompanyID As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetOtherTaxList", Conn)
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
                sqlMyAdapter.Fill(MyDataSet, "PaymentList")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function DeleteOtherTaxLine(ByVal ItemID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("DeleteOtherTaxLine", Conn)
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

        Public Shared Function UpdateOtherTaxLine(ByVal ItemID As Integer, _
                                                    ByVal PaymentAmount As String, _
                                                    ByVal PaymentDetails As String) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("UpdateOtherTaxLine", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@ItemID", ItemID)
            ObjCmd.Parameters.AddWithValue("@PaymentAmount", PaymentAmount)
            ObjCmd.Parameters.AddWithValue("@PaymentDetails", PaymentDetails)
            Try
                Conn.Open()
                ObjCmd.ExecuteNonQuery()
            Finally
                Conn.Close()
            End Try
            Return 0
        End Function

        Public Shared Function AddOtherTaxLine(ByVal CompanyID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("AddNewOtherTaxLine", Conn)
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

        Public Shared Function QuestionnaireGetTaxDetails(ByVal CompanyID As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetTaxList", Conn)
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
                sqlMyAdapter.Fill(MyDataSet, "PaymentList")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function DeleteTaxLine(ByVal ItemID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("DeleteTaxLine", Conn)
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

        Public Shared Function UpdateTaxLine(ByVal ItemID As Integer, _
                                                    ByVal CountryID As Integer, _
                                                    ByVal TaxDetails As String) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("UpdateTaxLine", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@ItemID", ItemID)
            ObjCmd.Parameters.AddWithValue("@CountryID", CountryID)
            ObjCmd.Parameters.AddWithValue("@TaxDetails", TaxDetails)
            Try
                Conn.Open()
                ObjCmd.ExecuteNonQuery()
            Finally
                Conn.Close()
            End Try
            Return 0
        End Function

        Public Shared Function AddTaxLine(ByVal CompanyID As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("AddNewTaxLine", Conn)
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

        Public Shared Function GetQuestionnaireConflictMinerals(ByVal CompanyID As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetQuestionnaireMinerals", Conn)
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
                sqlMyAdapter.Fill(MyDataSet, "MineralsList")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function UpdateStandardQuestionnaire(ByVal CompanyID As Integer, _
                                                           ByVal CompanyName As String, _
                                                           ByVal CompanyNumber As String, _
                                                           ByVal Address1 As String, _
                                                           ByVal Address2 As String, _
                                                           ByVal District As String, _
                                                           ByVal CityName As String, _
                                                           ByVal Postcode As String, _
                                                           ByVal StateRegion As String, _
                                                           ByVal CountryID As Integer, _
                                                           ByVal TelephoneNumber As String, _
                                                           ByVal FaxNumber As String, _
                                                           ByVal WebAddress As String, _
                                                           ByVal BusinessTypeID As Integer, _
                                                           ByVal ContactPerson As String, _
                                                           ByVal ContactEmail As String, _
                                                           ByVal CountryList As String, _
                                                           ByVal ParentCompany As Integer, _
                                                           ByVal GovernmentEmployees As Integer, _
                                                           ByVal ConflictMinerals As String, _
                                                           ByVal DangerousCountries As String, _
                                                           ByVal SmelterList As Integer, _
                                                           ByVal Scrap As Integer,
                                                           ByVal Recycled As Integer, _
                                                           ByVal IndependentAudit As String, _
                                                           ByVal TransportCountries As String, _
                                                           ByVal SupplyChain As String, _
                                                           ByVal SignOff As Integer, _
                                                           ByVal UpdatedBy As Integer, _
                                                           ByVal CurrentPage As Integer) As Integer
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("UpdateMyStandardQuestionnaire", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@CompanyID", CompanyID)
            ObjCmd.Parameters.AddWithValue("@CompanyName", CompanyName)
            ObjCmd.Parameters.AddWithValue("@CompanyNumber", CompanyNumber)
            ObjCmd.Parameters.AddWithValue("@Address1", Address1)
            ObjCmd.Parameters.AddWithValue("@Address2", Address2)
            ObjCmd.Parameters.AddWithValue("@District", District)
            ObjCmd.Parameters.AddWithValue("@CityName", CityName)
            ObjCmd.Parameters.AddWithValue("@Postcode", Postcode)
            ObjCmd.Parameters.AddWithValue("@StateRegion", StateRegion)
            ObjCmd.Parameters.AddWithValue("@CountryID", CountryID)
            ObjCmd.Parameters.AddWithValue("@TelephoneNumber", TelephoneNumber)
            ObjCmd.Parameters.AddWithValue("@FaxNumber", FaxNumber)
            ObjCmd.Parameters.AddWithValue("@WebAddress", WebAddress)
            ObjCmd.Parameters.AddWithValue("@BusinessTypeID", BusinessTypeID)
            ObjCmd.Parameters.AddWithValue("@ContactPerson", ContactPerson)
            ObjCmd.Parameters.AddWithValue("@ContactEmail", ContactEmail)
            ObjCmd.Parameters.AddWithValue("@CountryList", CountryList)
            ObjCmd.Parameters.AddWithValue("@ParentCompany", ParentCompany)
            ObjCmd.Parameters.AddWithValue("@GovernmentEmployees", GovernmentEmployees)
            ObjCmd.Parameters.AddWithValue("@ConflictMinerals", ConflictMinerals)
            ObjCmd.Parameters.AddWithValue("@DangerousCountries", DangerousCountries)
            ObjCmd.Parameters.AddWithValue("@SmelterList", SmelterList)
            ObjCmd.Parameters.AddWithValue("@Scrap", Scrap)
            ObjCmd.Parameters.AddWithValue("@Recycled", Recycled)
            ObjCmd.Parameters.AddWithValue("@IndependentAudit", IndependentAudit)
            ObjCmd.Parameters.AddWithValue("@TransportCountries", TransportCountries)
            ObjCmd.Parameters.AddWithValue("@SupplyChain", SupplyChain)
            ObjCmd.Parameters.AddWithValue("@SignOff", SignOff)
            ObjCmd.Parameters.AddWithValue("@UpdatedBy", UpdatedBy)
            ObjCmd.Parameters.AddWithValue("@CurrentPage", CurrentPage)
            Try
                Conn.Open()
                ObjCmd.ExecuteNonQuery()
            Finally
                Conn.Close()
            End Try
            Return 0
        End Function

        Public Shared Function UpdateQuestionnaireMinerals(ByVal CompanyID As Integer, _
                                                           ByVal MineralID As Integer, _
                                                           ByVal MineralDetails As String) As Integer

            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim ObjCmd As SqlCommand = New SqlCommand("UpdateMyQuestionnaireMinerals", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@CompanyID", CompanyID)
            ObjCmd.Parameters.AddWithValue("@MineralID", MineralID)
            ObjCmd.Parameters.AddWithValue("@MineralDetails", MineralDetails)
            Try
                Conn.Open()
                ObjCmd.ExecuteNonQuery()
            Finally
                Conn.Close()
            End Try
            Return 0
        End Function

        Public Shared Function GetMyStandardQuestionnaire(ByVal CompanyID As Integer, ByVal CreatedBy As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("GetMyStandardQuestionnaire", Conn)
            ObjCmd.CommandType = CommandType.StoredProcedure
            ObjCmd.Parameters.AddWithValue("@CompanyID", CompanyID)
            ObjCmd.Parameters.AddWithValue("@CreatedBy", CreatedBy)
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
                sqlMyAdapter.Fill(MyDataSet, "MyQuestionnaire")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
        End Function

        Public Shared Function CheckMyCertifications(ByVal CompanyID As Integer) As DataSet
            Dim Conn As SqlConnection = New SqlConnection(strConnString)
            Dim paramReturn As SqlParameter = Nothing
            Dim ObjCmd As SqlCommand = New SqlCommand("CheckMyCertifications", Conn)
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
                sqlMyAdapter.Fill(MyDataSet, "MyCertificates")
            Finally
                sqlMyAdapter.SelectCommand.Connection.Close()
            End Try

            'Send our dataset back to calling class
            Return MyDataSet
            'Test checkins
        End Function

#End Region

    End Class
End Namespace
