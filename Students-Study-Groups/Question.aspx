<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Question.aspx.cs" Inherits="Students_Study_Groups.Question" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Question.css" type="text/css" rel="stylesheet" />
    <script src='<%= ResolveUrl("~/Scripts/Question.js")%>' type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="question-container">
        <div class="question-header">
            <h1><%=QuestionTitle %></h1>
             
        </div>
        <hr />

    </div>
</asp:Content>

