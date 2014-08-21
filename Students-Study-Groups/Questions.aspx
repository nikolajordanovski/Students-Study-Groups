<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Questions.aspx.cs" Inherits="Students_Study_Groups.Questions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Questions.css" type="text/css" rel="stylesheet" />
    <script src='<%= ResolveUrl("~/Scripts/jquery-1.4.1.min.js")%>' type="text/javascript"></script>
    <script src='<%= ResolveUrl("~/Scripts/Questions.js")%>' type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div ID="questions" class="questions" runat="server">
    </div>

    <div class="popularTags">
    </div>

</asp:Content>
