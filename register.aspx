<%@ Page Title="" Language="VB" MasterPageFile="~/masterpages/templatefull.master" AutoEventWireup="false" CodeFile="register.aspx.vb" Inherits="register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpcMainContent" Runat="Server">
    <div class="span6">
            <div class="register">
                <h2 class="form-signin-heading">Please register</h2>
                <h3>Items marked with * are requried</h3>
                    <div class="form-signin">
                        <label>First name:</label>
                        <input type="text" class="input-block-level" placeholder="Enter your first name" />
                        <label>Surname</label>
                        <input type="password" class="input-block-level" placeholder="Enter your surname" />
                        <label>Company name:</label>
                        <input type="text" class="input-block-level" placeholder="Enter your company name" />
                        <a class="btn" href="template.aspx">Register with CERIO</a>
                    </div>
            </div>
    </div>
    <div class="span6">
        <h3>This can be some content on right</h3>
        <p>Instructions are always good. </p>
    </div>
</asp:Content>

