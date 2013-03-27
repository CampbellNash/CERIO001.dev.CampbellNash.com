<%@ Page Title="" Language="VB" MasterPageFile="~/masterpages/templatefull.master" AutoEventWireup="false" CodeFile="myquestionnaire.aspx.vb" Inherits="myquestionnaire" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpcMainContent" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <h1>My Questionnaire</h1>
            <h3>
                <asp:Literal ID="litStatus" runat="server" Text="Status: - " /></h3>
            <p>Provide the answers below to complete &amp; close and submit this questionnaire.</p>
            <div class="register">
                <div class="form-signin">
                    <asp:PlaceHolder ID="phMain" runat="server" />
                </div>
            </div>
            <asp:Button ID="btnSaveDraft" runat="server" CssClass="btn" Text="Save Draft" ValidationGroup="Questionnaire" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

