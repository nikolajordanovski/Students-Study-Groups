<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SignUp.aspx.cs" Inherits="Students_Study_Groups.SignUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/SignUp.css" type="text/css" rel="stylesheet" />
    <script src='<%= ResolveUrl("~/Scripts/jquery-1.4.1.min.js")%>' type="text/javascript"></script>
    <script src='<%= ResolveUrl("~/Scripts/SignUp.js")%>' type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="signup-container">
        <table>
            <tr class="spaceUnder">
                <td colspan="2"><asp:Label ID="lblError" runat="server" Visible="False" ForeColor="Red"></asp:Label> </td>
            </tr>
            <tr>
                <td>
                    Name:
                </td>
                <td>
                    <asp:TextBox ID="tbName" CssClass="text-input" placeholder="Name" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rfvName" ControlToValidate="tbName" runat="server" ErrorMessage="Name is required and must consist of only characters."></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Surname:
                </td>
                <td>
                    <asp:TextBox ID="tbSurname" CssClass="text-input" placeholder="Surname" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rfvSurname" ControlToValidate="tbSurname" runat="server" ErrorMessage="Surname is required and must consist of only characters."></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Username:
                </td>
                <td>
                    <asp:TextBox ID="tbUsername" CssClass="text-input" placeholder="Username" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rfvUsername" ControlToValidate="tbUsername" runat="server" ErrorMessage="Username is required."></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Email:
                </td>
                <td>
                    <asp:TextBox ID="tbEmail" CssClass="text-input" placeholder="Email" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RegularExpressionValidator ID="revEmail" ControlToValidate="tbEmail" ValidationExpression="[-0-9a-zA-Z.+_]+@[-0-9a-zA-Z.+_]+\.[a-zA-Z]{2,4}" runat="server" ErrorMessage="The email is not in valid format."></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Password:
                </td>
                <td>
                    <asp:TextBox ID="tbPassword" CssClass="text-input" TextMode="Password" placeholder="Password"
                        runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rfvPassword" ControlToValidate="tbPassword" runat="server" ErrorMessage="Password is required."></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="spaceUnder">
                <td>
                    Confirm password:
                </td>
                <td>
                    <asp:TextBox ID="tbConfirmPass" CssClass="text-input" TextMode="Password" placeholder="Confirm password"
                        runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:CompareValidator ID="cvConfirmPass" ControlToCompare="tbPassword" ControlToValidate="tbConfirmPass" runat="server" ErrorMessage="The passwords must match."></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:LinkButton ID="lbSignUp" CssClass="button-input" runat="server" 
                        onclick="lbSignUp_Click">Sign Up</asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
