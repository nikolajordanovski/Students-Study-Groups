<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Students_Study_Groups.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Profile.css" type="text/css" rel="stylesheet" />
    <script src='<%= ResolveUrl("~/Scripts/Profile.js")%>' type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div ID="username" class="infoline" runat="server"></div>
    <asp:Table ID="tblInfo" CssClass="info" runat="server"></asp:Table>
    <div ID="questionsNumber" class="questionsline" runat="server"></div>
    <div ID="questions" class="questions" runat="server"></div>
    <div ID="answersNumber" class="answersline" runat="server"></div>
    <div ID="answers" class="answers" runat="server"></div>

</asp:Content>
