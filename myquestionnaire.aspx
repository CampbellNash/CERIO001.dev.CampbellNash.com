<%@ Page Title="" Language="VB" MasterPageFile="~/masterpages/templatefull.master" AutoEventWireup="false" CodeFile="myquestionnaire.aspx.vb" Inherits="myquestionnaire" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpcMainContent" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <h1>My Questionnaire</h1>
            <asp:Panel ID="panMain" runat="server" Visible="true">
                <h3>
                    <asp:Literal ID="litStatus" runat="server" Text="Status: - " /></h3>
                <div class="progress">
            <div class="bar" style="width: 10%;"></div>
        </div>
                <p>Provide the answers below to complete &amp; close and submit this questionnaire.</p>

                <div class="form-signin form-horizontal">
                    <asp:PlaceHolder ID="phMain" runat="server" />


                </div>
                <asp:Button ID="btnSaveDraft" runat="server" CssClass="btn" Text="Save Draft" ValidationGroup="Questionnaire" />
            </asp:Panel>
            <asp:Panel ID="panConfirm" runat="server" Visible="false">
                <p>Questionnaire was saved!</p>
                <p>You can either <asp:LinkButton ID="btnClose" runat="server" Text="Close &amp; Save" CssClass="btn" /></p>
                <p>Or <asp:HyperLink ID="hypChange" runat="server" Text="Make Changes" CssClass="btn" NavigateUrl="~/myquestionnaire.aspx" /></p>

            </asp:Panel>
            <asp:CheckBoxList ID="chkList" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

