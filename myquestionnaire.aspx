<%@ Page Title="" Language="VB" MasterPageFile="~/masterpages/templatefull.master" AutoEventWireup="false" CodeFile="myquestionnaire.aspx.vb" Inherits="myquestionnaire" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpcMainContent" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <h1>My Questionnaire</h1>
            <h3>
                <asp:Literal ID="litStatus" runat="server" Text="Status: - " /></h3>
            <p>Provide the answers below to complete &amp; close and submit this questionnaire.</p>
            
               <div class="form-signin form-horizontal">
                    <asp:PlaceHolder ID="phMain" runat="server" />
                   
                   <h3>Section Name</h3>
            <div class="control-group">
                                <label class="control-label"><span class="alert-error">*</span> This is the question text:</label>
                                <div class="controls"><asp:TextBox ID="txtAddCompanayName" runat="server" CssClass="input-xxlarge" placeholder="Company name" TabIndex="1"></asp:TextBox><br />
                                <asp:RequiredFieldValidator ID="rfvAddCompanayName" ControlToValidate="txtAddCompanayName" CssClass="error" ForeColor="red" runat="server" Display="Dynamic" ErrorMessage="please enter the Company Name"></asp:RequiredFieldValidator>
                                </div>
                            </div>  
                            <div class="control-group">
                                <label class="control-label"><span class="alert-error">*</span> This is another question text:</label>
                                <div class="controls"><asp:DropDownList runat="server" ID="cboBusinessArea"/></div>
                            </div>
                </div>
            
            <asp:Button ID="btnSaveDraft" runat="server" CssClass="btn" Text="Save Draft" ValidationGroup="Questionnaire" />
            
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

