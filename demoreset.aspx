<%@ Page Title="" Language="VB" MasterPageFile="~/masterpages/templatefull.master" AutoEventWireup="false" CodeFile="demoreset.aspx.vb" Inherits="demoreset" %>

<%@ Register Src="controls/submenu1.ascx" TagName="submenu1" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpcMainContent" Runat="Server">
    
    <Telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
        <div class="span9" runat="server" id="mainDiv">
        <h1>Demo Settings Reset</h1>
        <h3>Use the buttons below to reset or delete any of the companies below</h3>
        <table width="800" border="2">
            <asp:Repeater ID="rptCompanies" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><asp:Literal ID="litCompanyName" runat="server" /></td>
                        <td><asp:LinkButton ID="btnDeleteCompany" runat="server" Text="Delete Company" CssClass="btn btn-small btn-primary" OnClick="DeleteCompany" />
                            <AjaxToolkit:ConfirmButtonExtender ID="cbeDeleteCompany" runat="server" TargetControlID="btnDeleteCompany"
                                ConfirmText="This will permanently remove this company and all its associations!&#10;&#10;Are you sure?" />
                            </td>
                        <td><asp:LinkButton ID="btnDeleteQuestionnaire" runat="server" Text="Delete Questionnaire" CssClass="btn btn-small btn-primary" OnClick="DeleteQuestionnaire" />
                            <AjaxToolkit:ConfirmButtonExtender ID="cbeDeleteQuestionnaire" runat="server" TargetControlID="btnDeleteQuestionnaire"
                                ConfirmText="This will permanently remove this questionnaire!&#10;&#10;Are you sure?" />
                        </td>
                        <td><asp:LinkButton ID="btnDeleteAssoc" runat="server" Text="Delete Company Connections" CssClass="btn btn-small btn-primary" OnClick="DeleteAssoc" />
                            <AjaxToolkit:ConfirmButtonExtender ID="cbeDeleteAssoc" runat="server" TargetControlID="btnDeleteAssoc"
                                ConfirmText="This will permanently remove the customers &amp; suppliers for this company!&#10;&#10;Are you sure?" />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table> 
        <h3>The users listed below can be safely removed without breaking any company or compliancy rules</h3>
        <p>Only users who have no companies or associations can be removed.</p>
        <p>If the user you wish to delete is not in the list below, then you will need to remove the company[s] they are attached to above to be able to delete them.</p>
            <table width="800" border="2">
                <asp:Repeater ID="rptUsers" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><asp:Literal ID="litUserName" runat="server" /></td>
                            <td><asp:Literal ID="litEmailAddress" runat="server" /></td>
                            <td>
                                <asp:LinkButton ID="btnDeleteUser" runat="server" Text="Delete User"
                                    CssClass="btn btn-small btn-primary" OnClick="DeleteUser" />
                                <AjaxToolkit:ConfirmButtonExtender ID="cbeDeleteUser" runat="server" TargetControlID="btnDeleteUser"
                                    ConfirmText="This will permanently remove this user!&#10;&#10;Are you sure?" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </Telerik:RadAjaxPanel>
   
    <Telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <Telerik:AjaxSetting AjaxControlID="mainDiv">
                <UpdatedControls>
                    <Telerik:AjaxUpdatedControl ControlID="mainDiv" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelRenderMode="Block" />
                </UpdatedControls>
            </Telerik:AjaxSetting>
        </AjaxSettings>
    </Telerik:RadAjaxManager>
    <Telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="MetroTouch"
        Transparency="0" IsSticky="False" />
    <div class="span3">
        <asp:Panel runat="server" ID="panSubNav">
            <uc1:submenu1 id="submenu11" runat="server" />
        </asp:Panel>
    </div>
</asp:Content>

