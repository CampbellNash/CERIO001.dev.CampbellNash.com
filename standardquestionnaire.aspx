<%@ Page Title="" Language="VB" MasterPageFile="~/masterpages/templatefull.master" AutoEventWireup="false" CodeFile="standardquestionnaire.aspx.vb" Inherits="standardquestionnaire" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpcMainContent" Runat="Server">
   
       <h1>Conflict Minerals – Due Diligence Questionnaire</h1>
    <p>[Company Name]'s policy is to conduct business in a legal and ethical manner, to further human rights and to not do anything which contributes to conflict.</p>
    <p> It is therefore important to identify the existence of any "conflict minerals" in our supply chain.  We expect our suppliers to adhere to this to statement of principle and to work with us in fulfilling our commitment.</p> 
    <p> Our suppliers are required to answer all questions honestly and thoroughly following the making of inquiries within their own business and with their supply chain.</p>
    <Telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="1000"> 
        <asp:Literal ID="litTest" runat="server" />
            <div class="register">
               <div class="progress">
                   <div class="bar" style="width: 20%;"></div>
               </div>
               <h3>Items marked with * are requried</h3>
               <div class="form-signin form-horizontal">
                    <asp:Panel ID="panPage1" runat="server" Visible="false">
                        <h4>1. Company Details</h4>
                        <label>Company Name:</label>
                        <span class="alert-error">*</span>
                        <asp:TextBox ID="txtCompanyName" runat="server" TextMode="SingleLine" CssClass="input-xxlarge" placeholder="Enter company name" />
                        <asp:RequiredFieldValidator ID="rfvCompanyName" runat="server" ControlToValidate="txtCompanyName" Display="Dynamic" ErrorMessage="Please enter company name" ValidationGroup="Questions" CssClass="alert-error" />
                        <label>Company Number:</label>
                        <span class="alert-error">*</span>
                        <asp:TextBox ID="txtCompanyNumber" runat="server" TextMode="SingleLine" CssClass="input-xlarge" placeholder="Enter company number" />
                        <asp:RequiredFieldValidator ID="rfvCompanyNumber" runat="server" ControlToValidate="txtCompanyNumber" Display="Dynamic" ErrorMessage="Please enter company number" ValidationGroup="Questions" CssClass="alert-error" />
                        <label>Street Address:</label>
                        <span class="alert-error">*</span>
                        <asp:TextBox ID="txtAddress1" runat="server" TextMode="SingleLine" CssClass="input-xlarge" placeholder="Enter company address" />
                        <asp:RequiredFieldValidator ID="rfvAddress1" runat="server" ControlToValidate="txtAddress1" Display="Dynamic" ErrorMessage="Please enter address" ValidationGroup="Questions" CssClass="alert-error" />
                        <label>District:</label>
                        <asp:TextBox ID="txtDistrict" runat="server" TextMode="SingleLine" CssClass="input-xlarge" placeholder="Enter company address" />
                        <label>City:</label>
                        <span class="alert-error">*</span>
                        <asp:TextBox ID="txtCity" runat="server" TextMode="SingleLine" CssClass="input-xlarge" placeholder="Enter company city" />
                        <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="txtCity" Display="Dynamic" ErrorMessage="Please enter city" ValidationGroup="Questions" CssClass="alert-error" />
                        <label>Region/State/Province:</label>
                        <span class="alert-error">*</span>
                        <asp:TextBox ID="txtState" runat="server" TextMode="SingleLine" CssClass="input-xlarge" placeholder="Enter company state or region" />
                        <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="txtState" Display="Dynamic" ErrorMessage="Please enter state or region" ValidationGroup="Questions" CssClass="alert-error" />
                        <label>Telephone:</label>
                        <span class="alert-error">*</span>
                        <asp:TextBox ID="txtTelephone" runat="server" TextMode="SingleLine" CssClass="input-xlarge" placeholder="Enter company telephone number" />
                        <asp:RequiredFieldValidator ID="rfvTelephone" runat="server" ControlToValidate="txtState" Display="Dynamic" ErrorMessage="Please enter telephone number" ValidationGroup="Questions" CssClass="alert-error" />
                        <label>Facsimilie:</label>
                        <span class="alert-error">*</span>
                        <asp:TextBox ID="txtFax" runat="server" TextMode="SingleLine" CssClass="input-xlarge" placeholder="Enter company fax number" />
                        <asp:RequiredFieldValidator ID="rfvFax" runat="server" ControlToValidate="txtState" Display="Dynamic" ErrorMessage="Please enter fax number" ValidationGroup="Questions" CssClass="alert-error" />
                        <label>Web Address:</label>
                        <span class="alert-error">*</span>
                        <asp:TextBox ID="txtWebAddress" runat="server" TextMode="SingleLine" CssClass="input-xxlarge" placeholder="Enter company web address" />
                        <asp:RequiredFieldValidator ID="rfvWebAddress" runat="server" ControlToValidate="txtWebAddress" Display="Dynamic" ErrorMessage="Please enter web address" ValidationGroup="Questions" CssClass="alert-error" />
                        <label>Type of business:</label>
                        <span class="alert-error">*</span>
                        <asp:DropDownList ID="cboBusinessType" runat="server" />
                        <asp:RequiredFieldValidator ID="rfvBusinessType" runat="server" ControlToValidate="cboBusinessType" Display="Dynamic" ErrorMessage="Please choose business type" ValidationGroup="Questions" CssClass="alert-error" />
                        <label>Contact Person:</label>
                        <span class="alert-error">*</span>
                        <asp:TextBox ID="txtContact" runat="server" TextMode="SingleLine" CssClass="input-xlarge" placeholder="Enter company conytact" />
                        <asp:RequiredFieldValidator ID="rfvContact" runat="server" ControlToValidate="txtContact" Display="Dynamic" ErrorMessage="Please enter company contact" ValidationGroup="Questions" CssClass="alert-error" />
                        <label>Contact Email:</label>
                        <span class="alert-error">*</span>
                        <asp:TextBox ID="txtContactEmail" runat="server" TextMode="SingleLine" CssClass="input-xxlarge" placeholder="Enter company conytact email" />
                        <asp:RequiredFieldValidator ID="rfvContactEmail" runat="server" ControlToValidate="txtContactEmail" Display="Dynamic" ErrorMessage="Please enter company contact email" ValidationGroup="Questions" CssClass="alert-error" />
                   </asp:Panel>

                    <asp:Panel ID="panPage2" runat="server" Visible="false">
                        <h4>2. Please list the countries in which the supplier operates or sources materials from</h4>
                        <label>Countries: Enter a country on each line</label>
                        <span class="alert-error">*</span>
                        <asp:TextBox ID="txtCountryList" runat="server" TextMode="MultiLine" CssClass="input-xlarge" Columns="50" Rows="4" placeholder="Enter at least one country" />
                        <asp:RequiredFieldValidator ID="rfvCountryList" runat="server" ControlToValidate="txtCountryList" Display="Dynamic" ErrorMessage="Please enter at least one country" ValidationGroup="Questions" CssClass="alert-error" />
                        <h4>3. Does the Supplier have any subsidiaries or a parent company?</h4>
                        <asp:RadioButtonList ID="rblParent" runat="server" AutoPostBack="true" RepeatColumns="2">
                            <asp:ListItem Text="No" Selected="True" />
                            <asp:ListItem Text="Yes" Selected="False" />
                        </asp:RadioButtonList>
                        <asp:Panel ID="panParentCompanies" runat="server" Visible="false">
                            <table width="900" border="1">
                                <tr>
                                    <th>Name of subsidiary/parent</th>
                                    <th>Registered number of subsidiary</th>
                                    <th>Country of registration of subsidiary</th>
                                    <th colspan="2">&#37; of subsidiary owned by Service </th>
                                </tr>
                                <asp:Repeater ID="rptParentCompany" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtParentCompanyName" runat="server" TextMode="SingleLine" CssClass="input-xlarge" placeholder="Enter company name" />
                                                <asp:RequiredFieldValidator ID="rfvParent" runat="server" ControlToValidate="txtParentCompanyName" Display="Dynamic" ErrorMessage="Please enter company name" ValidationGroup="Questions" CssClass="alert-error" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtParentCompanyNumber" runat="server" TextMode="SingleLine" CssClass="input-xlarge" placeholder="Enter company number" />
                                                <asp:RequiredFieldValidator ID="rfvCompanyNumber" runat="server" ControlToValidate="txtParentCompanyNumber" Display="Dynamic" ErrorMessage="Please enter company number" ValidationGroup="Questions" CssClass="alert-error" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtParentCountry" runat="server" TextMode="SingleLine" CssClass="input-xlarge" placeholder="Enter country of registration" />
                                                <asp:RequiredFieldValidator ID="rfvParentCountry" runat="server" ControlToValidate="txtParentCountry" Display="Dynamic" ErrorMessage="Please enter country of registration" ValidationGroup="Questions" CssClass="alert-error" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPercentOwned" runat="server" TextMode="SingleLine" CssClass="input-xlarge" placeholder="&#37; company owned" />
                                                <asp:RequiredFieldValidator ID="rfvPercentOwned" runat="server" ControlToValidate="txtPercentOwned" Display="Dynamic" ErrorMessage="Please enter &#37; of company owned" ValidationGroup="Questions" CssClass="alert-error" />
                                            </td>
                                            <td><asp:Button ID="btnDeleteParent" runat="server" CssClass="Btn" Text="Delete" OnClick="DeleteParentLine" /><asp:HiddenField ID="hidItemID" runat="server" /></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </asp:Panel>
                        <asp:LinkButton ID="btnAddNewParent" runat="server" Text="Add New Line" Visible="false" />
                        <h4>4.List all owners, partners or shareholders of the Supplier (percentage should total 100%):</h4>
                        <table width="900" border="1">
                            <tr>
                                <th>Name</th>
                                <th>Nationality</th>
                                <th colspan="2">Ownership &#37;</th>
                            </tr>
                            <asp:Repeater ID="rptShareholders" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtShareholderName" runat="server" TextMode="SingleLine" CssClass="input-xlarge" placeholder="Enter company name" />
                                            <asp:RequiredFieldValidator ID="rfvParent" runat="server" ControlToValidate="txtShareholderName" Display="Dynamic" ErrorMessage="Please enter shareholder name" ValidationGroup="Questions" CssClass="alert-error" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtShareholderNationality" runat="server" TextMode="SingleLine" CssClass="input-xlarge" placeholder="Enter company number" />
                                            <asp:RequiredFieldValidator ID="rfvCompanyNumber" runat="server" ControlToValidate="txtShareholderNationality" Display="Dynamic" ErrorMessage="Please enter nationality" ValidationGroup="Questions" CssClass="alert-error" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPercentOwned" runat="server" TextMode="SingleLine" CssClass="input-xlarge" placeholder="&#37; company owned" />
                                            <asp:RequiredFieldValidator ID="rfvPercentOwned" runat="server" ControlToValidate="txtPercentOwned" Display="Dynamic" ErrorMessage="Please enter &#37; of company owned" ValidationGroup="Questions" CssClass="alert-error" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnDeleteShareholder" runat="server" CssClass="Btn" Text="Delete" OnClick="DeleteParentLine" /><asp:HiddenField ID="hidItemID" runat="server" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <asp:LinkButton ID="btnAddNewShareholder" runat="server" Text="Add New Line" /> 
                    </asp:Panel>

                    <p></p>
                    <p>
                        <asp:Button ID="btnPrev" runat="server" CssClass="btn" Text=" &lt;&lt; Prev Page" />&nbsp;&nbsp;<asp:Button ID="btnNext" runat="server" CssClass="btn" Text="Next Page &gt;&gt;" />&nbsp;&nbsp;<asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save Draft" ValidationGroup="Questions" /></p>
               </div>
            </div>
       
  </Telerik:RadAjaxPanel>
    <Telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <Telerik:AjaxSetting AjaxControlID="RadAjaxPanel1">
                <UpdatedControls>
                    <Telerik:AjaxUpdatedControl ControlID="RadAjaxPanel1" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelRenderMode="Block"/>
                </UpdatedControls>
            </Telerik:AjaxSetting>
        </AjaxSettings>
    </Telerik:RadAjaxManager>
    <Telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Telerik" Transparency="0" IsSticky="False" />
    
</asp:Content>

