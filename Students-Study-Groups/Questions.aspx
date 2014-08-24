<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Questions.aspx.cs" Inherits="Students_Study_Groups.Questions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Questions.css" type="text/css" rel="stylesheet" />
    <script src='<%= ResolveUrl("~/Scripts/jquery-1.4.1.min.js")%>' type="text/javascript"></script>
    <script src='<%= ResolveUrl("~/Scripts/Questions.js")%>' type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="subheader">
        <div class="questionsname">All Questions</div>
        <div class="sortmenu">
            <ul class="ul-nav-bottom">
                <li><asp:LinkButton ID="lbNewest" runat="server" onclick="lbNewest_Click">Newest</asp:LinkButton></li>
                <li><asp:LinkButton ID="lbVotes" runat="server" onclick="lbVotes_Click">Votes</asp:LinkButton></li>
                <li><asp:LinkButton ID="lbViews" runat="server" onclick="lbViews_Click">Views</asp:LinkButton></li>
                <li><asp:LinkButton ID="lbUnanswered" runat="server" onclick="lbUnanswered_Click">Unanswered</asp:LinkButton></li>
            </ul>
        </div>
        <div class="askquestion">
            <ul class="ul-nav-bottom">
                <li><a href="AskQuestion.aspx?SID=<%=Request.QueryString["SID"]%>">Ask question</a></li>
            </ul>
        </div>
    </div>
    <div ID="dvQuestions" class="questions" runat="server"></div>
    <div class="popularTags"></div>

</asp:Content>
