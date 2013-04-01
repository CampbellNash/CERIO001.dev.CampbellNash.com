Option Strict Off
Option Explicit On
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  Page Title       : myquestionnaire.aspx.vb           '
'  Description      : Shows open questionnaire          ' 
'  Author           : Brian McAulay                     '
'  Creation Date    : 27 Mar 2013                       '
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
Partial Class myquestionnaire
    Inherits System.Web.UI.Page
    Public MyQuestions As DataSet
    Public MyRow As DataRow
    Dim LoopCount As Integer = 1



#Region " Read the form elements from SQL "

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        Dim SectionName As String = ""
        'Load our dynamic form controls
        MyQuestions = NashBLL.GetMyQuestionnaire(2)
        'Iterate through the form items and add them to the placeholder
        For Each Me.MyRow In MyQuestions.Tables(0).Rows
            If LoopCount = 1 Then
                'This is the first section so we can add that to our form
                Dim Section As New Literal
                Section.Text = "<h3>" & MyRow("SectionName") & "</h3>"
                phMain.Controls.Add(Section)
                'Store this to compare with the next section
                SectionName = MyRow("SectionName")
            ElseIf MyRow("SectionName") <> SectionName Then
                'We need to change our section now
                Dim Section As New Literal
                Section.Text = "<h3>" & MyRow("SectionName") & "</h3>"
                phMain.Controls.Add(Section)
                'Store this to compare with the next section
                SectionName = MyRow("SectionName")
            End If
            Select Case UCase(MyRow("FieldType"))
                Case "TEXTAREA"
                    AddTextBox(True)
                Case "TEXTBOX"
                    AddTextBox(False)
                Case "DROPDOWNLIST"
                    AddDropDownList()
                Case "CHECKBOXLIST"
                    AddCheckBoxList()
                Case "RADIOBUTTONLIST"
                    AddRadioButtonList()
                Case Else

            End Select
            LoopCount += 1
        Next
    End Sub

#End Region

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Session("UserLoggedIn") Then
            'User is not logged in so send to login page
            Response.Redirect("~/login.aspx")
        End If

        If Not IsPostBack Then
            'Execute the rules now in case this is a previously filled form where the rules were triggered
            ExecuteRules()

        End If
    End Sub

#Region " Execute Question Rules "

    Private Sub ExecuteRules()
        For Each Control In phMain.Controls
            If Control.GetType().Name.ToLower = "panel" Then
                Dim Panel As Panel = Control
                For Each Item In Panel.Controls
                    Select Case Item.GetType().Name.ToLower
                        Case "textbox"
                            'No rules applied to text boxes yet
                        Case "dropdownlist"
                            'Control was a dropdownlist so we need to test the rule for this item
                            Dim DropDown As DropDownList = Item
                            DropDownIndexChanged(Item, New System.EventArgs())
                        Case "checkboxlist"
                            'Control was a checkbox list, so we need to test the rule for this item
                            Dim CheckBoxList As CheckBoxList = Item
                            CheckboxIndexChanged(Item, New System.EventArgs())
                        Case "radiobuttonlist"
                            'Control was a radio button list, so we need to test the rule for this item
                            Dim RadioButtonList As RadioButtonList = Item
                            RadioButtonIndexChanged(Item, New System.EventArgs())
                        Case Else
                            'Need this to be able to escape without errors
                    End Select
                Next
            End If
        Next

    End Sub

#End Region

#Region " Add Form Elements "

    Private Sub AddDropDownList()
        'Adds a dropdownlist to the form
        Dim Div1 As New Literal
        Dim Div2 As New Literal
        Dim CarriageReturn As New Literal
        Dim rfv As New RequiredFieldValidator
        Dim CloseDivs As New Literal
        Dim DropDownList As New DropDownList
        Dim QuestionPanel As New Panel
        Dim LogID As New HiddenField
        Dim ContentID As New HiddenField
        Dim Rule As Array = Split(MyRow("QuestionRule"), ",")
        If CBool(Rule(1)) Then
            'This has an autopostback requirement so add the handler
            AddHandler DropDownList.SelectedIndexChanged, AddressOf DropDownIndexChanged
            DropDownList.AutoPostBack = True
        End If
        'Set our main panel
        QuestionPanel.ID = "panQuestion" & LoopCount
        'Set our div wrappers
        Div1.Text = "<div class=""control-group"">" & _
                                "<label class=""control-label""><span class=""alert-error"">*</span>" & MyRow("QuestionText") & ":</label>"
        Div2.Text = "<div class=""controls"">"
        'Add a BR for the required field validator
        CarriageReturn.Text = "<br />"
        'Set the required field validator
        rfv.ControlToValidate = "DropDownList" & LoopCount
        rfv.ErrorMessage = "Please complete the item above"
        rfv.ForeColor = Drawing.Color.Red
        rfv.ValidationGroup = "Questionnaire"
        rfv.Display = ValidatorDisplay.Dynamic
        'Set the wrapper close divs
        CloseDivs.Text = "</div></div>"
        'Now set up the textbox
        DropDownList.ID = "DropDownList" & LoopCount
        'Now we need to get our options
        Dim MyOptions As Array = Split(MyRow("Options"), ",")
        'Now add these to the dropdownlist
        For ItemCount = LBound(MyOptions) To UBound(MyOptions)
            DropDownList.Items.Add(New ListItem(MyOptions(ItemCount), MyOptions(ItemCount)))
        Next
        If CInt(MyRow("LogID")) = 0 Then
            'This form was not previously completed so show the default value
            DropDownList.SelectedValue = MyRow("DefaultValue")
        Else
            'Already been submitted so show the answer that was entered
            DropDownList.SelectedValue = MyRow("NewValue")
        End If
        If CBool(Rule(0)) Then
            'This has an autopostback requirement so add the handler
            AddHandler DropDownList.SelectedIndexChanged, AddressOf DropDownIndexChanged
            DropDownList.AutoPostBack = True
        End If
        'Set our hidden field values to update the question
        LogID.ID = "hidLogID" & LoopCount
        LogID.Value = MyRow("LogID")
        ContentID.ID = "hidContentID" & LoopCount
        ContentID.Value = MyRow("ContentID")
        'Now check our question rule to see if this panel should be visible
        If Rule(0) Then
            'This panel should be visible
            QuestionPanel.Visible = True
        Else
            'This panel should be hidden as its a trigger item
            QuestionPanel.Visible = False
        End If
        'Finally add these items to the placeholder
        QuestionPanel.Controls.Add(Div1)
        QuestionPanel.Controls.Add(Div2)
        QuestionPanel.Controls.Add(LogID)
        QuestionPanel.Controls.Add(ContentID)
        QuestionPanel.Controls.Add(DropDownList)
        QuestionPanel.Controls.Add(CarriageReturn)
        QuestionPanel.Controls.Add(rfv)
        QuestionPanel.Controls.Add(CloseDivs)
        phMain.Controls.Add(QuestionPanel)
        'Now write the panel name into the DB to enforce the rule and aidentify the panel we will switch on or off
        NashBLL.UpdateQuestionPanelReference(MyRow("ContentID"), QuestionPanel.ID)
    End Sub

    Private Sub AddCheckBoxList()
        'Adds a checkboxlist to the form
        Dim Div1 As New Literal
        Dim Div2 As New Literal
        Dim CarriageReturn As New Literal
        Dim rfv As New RequiredFieldValidator
        Dim CloseDivs As New Literal
        Dim NewCheckBoxList As New CheckBoxList
        Dim QuestionPanel As New Panel
        Dim LogID As New HiddenField
        Dim ContentID As New HiddenField
        Dim ItemLoopCount As Integer = 0
        Dim Rule As Array = Split(MyRow("QuestionRule"), ",")
        If CBool(Rule(1)) Then
            'This has an autopostback requirement so add the handler
            AddHandler NewCheckBoxList.SelectedIndexChanged, AddressOf CheckboxIndexChanged
            NewCheckBoxList.AutoPostBack = True
        End If
        'Set our main panel
        QuestionPanel.ID = "panQuestion" & LoopCount
        'Set our div wrappers
        Div1.Text = "<div class=""control-group"">" & _
                                "<label class=""control-label""><span class=""alert-error"">*</span>" & MyRow("QuestionText") & ":</label>"
        Div2.Text = "<div class=""controls"">"
        'Add a BR for the required field validator
        CarriageReturn.Text = "<br />"
        CloseDivs.Text = "</div></div>"
        'Now set up the textbox
        NewCheckBoxList.ID = "CheckBoxList" & LoopCount
        'Now we need to get our options
        Dim MyOptions As Array = Split(MyRow("Options"), ",")
        'Now add these to the dropdownlist
        For ItemCount = LBound(MyOptions) To UBound(MyOptions)
            NewCheckBoxList.Items.Add(New ListItem(MyOptions(ItemCount), MyOptions(ItemCount)))
        Next
        NewCheckBoxList.RepeatLayout = RepeatLayout.Table
        NewCheckBoxList.RepeatDirection = RepeatDirection.Horizontal
        NewCheckBoxList.RepeatColumns = 3
        If CInt(MyRow("LogID")) = 0 Then
            'This form was not previously completed so show the default value
            NewCheckBoxList.SelectedValue = NewCheckBoxList.Items.FindByValue(MyRow("DefaultValue")).ToString
        Else
            'Already been submitted so show the answer[s] that were previously entered
            For Each CheckBox As ListItem In NewCheckBoxList.Items
                'First uncheck every item
                CheckBox.Selected = False
            Next
            Dim MyArray As Array = Split(MyRow("NewValue"), ",")
            'Now we inspect all our checboxes to the stored value[s] and check them again accordingly
            For Each CheckBox As ListItem In NewCheckBoxList.Items
                'Now check this option against our selected items answer
                For ItemLoopCount = LBound(MyArray) To UBound(MyArray)
                    If CheckBox.Value = MyArray(ItemLoopCount) Then
                        'This item was selected so select it again
                        CheckBox.Selected = True
                    End If
                Next
            Next
        End If
        'Set our hidden field values to update the question
        LogID.ID = "hidLogID" & LoopCount
        LogID.Value = MyRow("LogID")
        ContentID.ID = "hidContentID" & LoopCount
        ContentID.Value = MyRow("ContentID")
        'Now check our question rule to see if this panel should be visible
        If Rule(0) Then
            'This panel should be visible
            QuestionPanel.Visible = True
        Else
            'This panel should be hidden as its a trigger item
            QuestionPanel.Visible = False
        End If
        'Finally add these items to the placeholder
        QuestionPanel.Controls.Add(Div1)
        QuestionPanel.Controls.Add(Div2)
        QuestionPanel.Controls.Add(LogID)
        QuestionPanel.Controls.Add(ContentID)
        QuestionPanel.Controls.Add(NewCheckBoxList)
        QuestionPanel.Controls.Add(CarriageReturn)
        'QuestionPanel.Controls.Add(rfv)
        QuestionPanel.Controls.Add(CloseDivs)
        phMain.Controls.Add(QuestionPanel)
        'Now write the panel name into the DB to enforce the rule and aidentify the panel we will switch on or off
        NashBLL.UpdateQuestionPanelReference(MyRow("ContentID"), QuestionPanel.ID)
    End Sub

    Private Sub AddRadioButtonList()
        'Adds a radiobuttonlist to the form
        Dim Div1 As New Literal
        Dim Div2 As New Literal
        Dim CarriageReturn As New Literal
        Dim rfv As New RequiredFieldValidator
        Dim CloseDivs As New Literal
        Dim NewRadioButtonList As New RadioButtonList
        Dim QuestionPanel As New Panel
        Dim LogID As New HiddenField
        Dim ContentID As New HiddenField
        Dim Rule As Array = Split(MyRow("QuestionRule"), ",")
        If CBool(Rule(1)) Then
            'This has an autopostback requirement so add the handler
            AddHandler NewRadioButtonList.SelectedIndexChanged, AddressOf RadioButtonIndexChanged
            NewRadioButtonList.AutoPostBack = True
        End If
        'Set our main panel
        QuestionPanel.ID = "panQuestion" & LoopCount
        'Set our div wrappers
        Div1.Text = "<div class=""control-group"">" & _
                                "<label class=""control-label""><span class=""alert-error"">*</span>" & MyRow("QuestionText") & ":</label>"
        Div2.Text = "<div class=""controls"">"
        'Add a BR for the required field validator
        CarriageReturn.Text = "<br />"
        CloseDivs.Text = "</div></div>"
        'Now set up the textbox
        NewRadioButtonList.ID = "RadioButtonList" & LoopCount
        'Now we need to get our options
        Dim MyOptions As Array = Split(MyRow("Options"), ",")
        'Now add these to the dropdownlist
        For ItemCount = LBound(MyOptions) To UBound(MyOptions)
            NewRadioButtonList.Items.Add(New ListItem(MyOptions(ItemCount), MyOptions(ItemCount)))
        Next
        NewRadioButtonList.RepeatLayout = RepeatLayout.Table
        NewRadioButtonList.RepeatDirection = RepeatDirection.Horizontal
        NewRadioButtonList.RepeatColumns = 3
        If CInt(MyRow("LogID")) = 0 Then
            'This form was not previously completed so show the default value
            NewRadioButtonList.SelectedValue = NewRadioButtonList.Items.FindByValue(MyRow("DefaultValue")).ToString
        Else
            'Already been submitted so show the answer[s] that were previously entered
            For Each RadioButton As ListItem In NewRadioButtonList.Items
                'First uncheck every item
                RadioButton.Selected = False
            Next
            Dim MyArray As Array = Split(MyRow("NewValue"), ",")
            'Now we inspect all our checboxes to the stored value[s] and check them again accordingly
            For Each RadioButton As ListItem In NewRadioButtonList.Items
                'Now check this option against our selected items answer
                For ItemLoopCount = LBound(MyArray) To UBound(MyArray)
                    If RadioButton.Value = MyArray(ItemLoopCount) Then
                        'This item was selected so select it again
                        RadioButton.Selected = True
                    End If
                Next
            Next
        End If
        'Set our hidden field values to update the question
        LogID.ID = "hidLogID" & LoopCount
        LogID.Value = MyRow("LogID")
        ContentID.ID = "hidContentID" & LoopCount
        ContentID.Value = MyRow("ContentID")
        'Now check our question rule to see if this panel should be visible
        If Rule(0) Then
            'This panel should be visible
            QuestionPanel.Visible = True
        Else
            'This panel should be hidden as its a trigger item
            QuestionPanel.Visible = False
        End If
        'Finally add these items to the placeholder
        QuestionPanel.Controls.Add(Div1)
        QuestionPanel.Controls.Add(Div2)
        QuestionPanel.Controls.Add(LogID)
        QuestionPanel.Controls.Add(ContentID)
        QuestionPanel.Controls.Add(NewRadioButtonList)
        QuestionPanel.Controls.Add(CarriageReturn)
        'QuestionPanel.Controls.Add(rfv)
        QuestionPanel.Controls.Add(CloseDivs)
        phMain.Controls.Add(QuestionPanel)
        'Now write the panel name into the DB to enforce the rule and aidentify the panel we will switch on or off
        NashBLL.UpdateQuestionPanelReference(MyRow("ContentID"), QuestionPanel.ID)
    End Sub

    Private Sub AddTextBox(ByVal Textarea As Boolean)
        'Adds a textbox or textarea box to the form
        Dim Div1 As New Literal
        Dim Div2 As New Literal
        Dim CarriageReturn As New Literal
        Dim rfv As New RequiredFieldValidator
        Dim CloseDivs As New Literal
        Dim NewTextBox As New TextBox
        Dim QuestionPanel As New Panel
        Dim LogID As New HiddenField
        Dim ContentID As New HiddenField
        Dim Rule As Array = Split(MyRow("QuestionRule"), ",")

        'Set our main panel
        QuestionPanel.ID = "panQuestion" & LoopCount
        'Set our div wrappers
        Div1.Text = "<div class=""control-group"">" & _
                                "<label class=""control-label""><span class=""alert-error"">*</span>" & MyRow("QuestionText") & ":</label>"
        Div2.Text = "<div class=""controls"">"
        'Add a BR for the required field validator
        CarriageReturn.Text = "<br />"
        'Set the required field validator
        rfv.ControlToValidate = "TextBox" & LoopCount
        rfv.ErrorMessage = "Please complete the item above"
        rfv.ForeColor = Drawing.Color.Red
        rfv.ValidationGroup = "Questionnaire"
        rfv.Display = ValidatorDisplay.Dynamic
        'Set the wrapper close divs
        CloseDivs.Text = "</div></div>"
        'Now set up the textbox
        NewTextBox.ID = "TextBox" & LoopCount
        If Textarea Then
            NewTextBox.TextMode = TextBoxMode.MultiLine
            NewTextBox.Rows = 5
        Else
            NewTextBox.CssClass = "input-xxlarge"
        End If
        If CInt(MyRow("LogID")) = 0 Then
            'This form was not previously completed so show the default value
            NewTextBox.Text = MyRow("DefaultValue")
        Else
            'Already been submitted so show the answer that was entered
            NewTextBox.Text = MyRow("NewValue")
        End If
        'Set our hidden field values to update the question
        LogID.ID = "hidLogID" & LoopCount
        LogID.Value = MyRow("LogID")
        ContentID.ID = "hidContentID" & LoopCount
        ContentID.Value = MyRow("ContentID")
        'Now check our question rule to see if this panel should be visible
        If Rule(0) Then
            'This panel should be visible
            QuestionPanel.Visible = True
        Else
            'This panel should be hidden as its a trigger item
            QuestionPanel.Visible = False
        End If
        'Finally add these items to the placeholder
        QuestionPanel.Controls.Add(Div1)
        QuestionPanel.Controls.Add(Div2)
        QuestionPanel.Controls.Add(LogID)
        QuestionPanel.Controls.Add(ContentID)
        QuestionPanel.Controls.Add(NewTextBox)
        QuestionPanel.Controls.Add(CarriageReturn)
        QuestionPanel.Controls.Add(rfv)
        QuestionPanel.Controls.Add(CloseDivs)
        phMain.Controls.Add(QuestionPanel)
        'Now write the panel name into the DB to enforce the rule and aidentify the panel we will switch on or off
        NashBLL.UpdateQuestionPanelReference(MyRow("ContentID"), QuestionPanel.ID)
    End Sub

#End Region

#Region " Generic Handlers "

    
    Private Sub DropDownIndexChanged(sender As Object, e As EventArgs)
        'We need to find the rule that has been applied here
        Dim DropDownList As DropDownList = sender
        Dim QuestionPanel As Panel = DropDownList.Parent
        Dim PostedValue As String = ""
        Dim Panel As New Panel
        'Now we need to get the value of our dropdownlist
        For Each Item As ListItem In DropDownList.Items
            If Item.Selected Then
                'This was the one that was chosen so we need its value
                PostedValue = Item.Value
            End If
        Next
        'Now go and get the rule[s] that apply to this question
        Dim QuestionRules As DataSet = NashBLL.GetDependentQuestionRule(QuestionPanel.ID)
        'Check each rule against our selected value
        For Each Rule In QuestionRules.Tables(0).Rows
            Dim QuestionRule As Array = Split(Rule("QuestionRule"), ",")
            'Now look for it in the placeholder
            Panel = phMain.FindControl(Rule("PanelName"))
            If QuestionRule(3) = PostedValue Then
                'Our posted value equals the trigger so show the sub question
                Panel.Visible = True
            Else
                'Our posted value does not equal trigger so hide the sub question
                Panel.Visible = False
            End If
        Next

    End Sub

    Private Sub CheckboxIndexChanged(sender As Object, e As EventArgs)
        'We need to find the rule that has been applied here
        Dim CheckBoxList As CheckBoxList = sender
        Dim QuestionPanel As Panel = CheckBoxList.Parent
        Dim PostedValue As String = ""
        Dim Panel As New Panel
        Dim ItemCount As Integer = 0
        Dim TriggerCount As Integer = 0
        Dim PostedValueCount As Integer = 0
        'Now we need to get the value of our radio button
        For Each Item As ListItem In CheckBoxList.Items
            If Item.Selected Then
                If ItemCount = 0 Then
                    'This is the first item so no comma required
                    PostedValue &= Item.Value
                    ItemCount += 1
                Else
                    PostedValue &= "," & Item.Value
                End If
            End If
        Next
        'Now go and get the rule[s] that apply to this question
        Dim QuestionRules As DataSet = NashBLL.GetDependentQuestionRule(QuestionPanel.ID)
        'Split our answers now
        Dim PostedAnswer As Array = Split(PostedValue, ",")
        'Check each rule against our selected value
        For Each Rule In QuestionRules.Tables(0).Rows
            Dim QuestionRule As Array = Split(Rule("QuestionRule"), ",")
            'Now look for it in the placeholder
            Panel = phMain.FindControl(Rule("PanelName"))
            'Always hide unless its triggered
            Panel.Visible = False
            Dim TriggerValues As Array = Split(QuestionRule(3), "|")
            For TriggerCount = LBound(TriggerValues) To UBound(TriggerValues)
                For PostedValueCount = LBound(PostedAnswer) To UBound(PostedAnswer)
                    If PostedAnswer(PostedValueCount) = TriggerValues(TriggerCount) Then
                        Panel.Visible = True
                    End If
                Next
            Next
        Next

    End Sub

    Private Sub RadioButtonIndexChanged(sender As Object, e As EventArgs)
        'We need to find the rule that has been applied here
        Dim RadioButton As RadioButtonList = sender
        Dim QuestionPanel As Panel = RadioButton.Parent
        Dim PostedValue As String = ""
        Dim Panel As New Panel
        'Now we need to get the value of our radio button
        For Each Item As ListItem In RadioButton.Items
            If Item.Selected Then
                'This was the one that was chosen so we need its value
                PostedValue = Item.Value
            End If
        Next
        'Now go and get the rule[s] that apply to this question
        Dim QuestionRules As DataSet = NashBLL.GetDependentQuestionRule(QuestionPanel.ID)
        'Check each rule against our selected value
        For Each Rule In QuestionRules.Tables(0).Rows
            Dim QuestionRule As Array = Split(Rule("QuestionRule"), ",")
            'Now look for it in the placeholder
            Panel = phMain.FindControl(Rule("PanelName"))
            If QuestionRule(3) = PostedValue Then
                'Our posted value equals the trigger so show the sub question
                Panel.Visible = True
            Else
                'Our posted value does not equal trigger so hide the sub question
                Panel.Visible = False
            End If
        Next
    End Sub

#End Region

#Region " Update Form "

    Protected Sub btnSaveDraft_Click(sender As Object, e As EventArgs) Handles btnSaveDraft.Click
        LoopCount = 1
        Dim LogID As Integer
        Dim ContentID As Integer
        Dim NewValue As String = ""
        Dim UpdatedBy As Integer = Session("ContactID")
        Dim ItemLoopCount As Integer = 1
        For Each Control In phMain.Controls
            If Control.GetType().Name.ToLower = "panel" Then
                Dim Panel As Panel = Control
                Dim hidContentID As HiddenField = Panel.FindControl("hidContentID" & LoopCount)
                Dim hidLogID As HiddenField = Panel.FindControl("hidLogID" & LoopCount)
                LogID = hidLogID.Value
                ContentID = hidContentID.Value
                If Panel.Visible = True Then
                    'Only include items that were on display on the form
                    'this avoids updating dependencies that were not included or needed
                    For Each Item In Panel.Controls
                        Select Case Item.GetType().Name.ToLower
                            Case "textbox"
                                'The control was a textbox so we need the text value from that
                                Dim TextBox As TextBox = Item
                                NewValue = TextBox.Text
                                'Now we can update our questionnaire for this question
                                Try
                                    NashBLL.UpdateQuestionAnswer(LogID, ContentID, NewValue, UpdatedBy)
                                Catch
                                    litStatus.Text &= "Textbox error for " & LoopCount & "</br>" & Err.Description & "<br>"
                                End Try
                            Case "dropdownlist"
                                'Control was a dropdownlist so we need the selected value from that
                                Dim DropDown As DropDownList = Item
                                NewValue = DropDown.SelectedValue
                                'Now we can update our questionnaire for this question
                                Try
                                    NashBLL.UpdateQuestionAnswer(LogID, ContentID, NewValue, UpdatedBy)
                                Catch
                                    litStatus.Text &= "Dropdownlist error for " & LoopCount & "</br>" & Err.Description & "<br>"
                                End Try
                            Case "checkboxlist"
                                'Control was a checkbox list, so we need the checked values from that
                                Dim CheckBoxList As CheckBoxList = Item
                                For Each CheckBox As ListItem In CheckBoxList.Items
                                    'Check to see what items were checked
                                    If CheckBox.Selected Then
                                        If ItemLoopCount = 0 Then
                                            'This is the first item in our list so it goes in by itself
                                            NewValue = CheckBox.Value
                                            ItemLoopCount += 1
                                        Else
                                            'There is more than one item so we need to precede with a comma
                                            NewValue &= "," & CheckBox.Value
                                        End If
                                    End If
                                Next
                                'Now we can update our questionnaire for this question
                                Try
                                    NashBLL.UpdateQuestionAnswer(LogID, ContentID, NewValue, UpdatedBy)
                                Catch
                                    litStatus.Text &= "Checkboxlist error for " & LoopCount & "</br>" & Err.Description & "<br>"
                                End Try
                            Case "radiobuttonlist"
                                'Control was a radio button list, so we need to use the selected button's value
                                Dim RadioButtonList As RadioButtonList = Item
                                NewValue = RadioButtonList.SelectedIndex
                                'Now we can update our questionnaire for this question
                                For Each RadioButton As ListItem In RadioButtonList.Items
                                    'Check to see what items were checked
                                    If RadioButton.Selected Then
                                        If ItemLoopCount = 0 Then
                                            'This is the first item in our list so it goes in by itself
                                            NewValue = RadioButton.Value
                                            ItemLoopCount += 1
                                        Else
                                            'There is more than one item so we need to precede with a comma
                                            NewValue &= "," & RadioButton.Value
                                        End If
                                    End If
                                Next
                                Try
                                    NashBLL.UpdateQuestionAnswer(LogID, ContentID, NewValue, UpdatedBy)
                                Catch
                                    litStatus.Text &= "Radiobuttonlist error for " & LoopCount & "</br>" & Err.Description & "<br>"
                                End Try
                            Case Else
                                'Need this to be able to escape without errors
                        End Select
                        'Reset our values and loops ready for the next form item
                        NewValue = ""
                        ItemLoopCount = 0
                    Next
                    LoopCount += 1
                End If
            End If
        Next
        panMain.Visible = False
        panConfirm.Visible = True
    End Sub

#End Region

    

End Class
