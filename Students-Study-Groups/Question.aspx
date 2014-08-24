<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Question.aspx.cs" Inherits="Students_Study_Groups.Question" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Question.css" type="text/css" rel="stylesheet" />
    <link href="Styles/prettify.css" type="text/css" rel="stylesheet" />
    <link href="Styles/wmd.css" type="text/css" rel="stylesheet" />
    <script src='<%= ResolveUrl("~/Scripts/Question.js")%>' type="text/javascript"></script>
    <script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>
    <script src='<%= ResolveUrl("~/Scripts/showdown.js")%>' type="text/javascript"></script>
    <script src='<%= ResolveUrl("~/Scripts/wmd.js")%>' type="text/javascript"></script>
    <script type="text/javascript">
        var userId     = <%=UID %>;
        var questionId = <%=QID %>;
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="question-container">
        <div class="question-header">
            <div class="float-left">
                <h1 style="display: inline;">
                    <%=QuestionMod.Title %></h1>
            </div>
            <div class="float-right">
                <h2 style="display: inline;">
                    Views:
                    <%=QuestionMod.Views %></h2>
                <h2 style="display: inline;">
                    Date asked:
                    <%=DateTime.Parse(QuestionMod.DateAsked).ToShortDateString() %></h2>
            </div>
        </div>
        <!-- QUESTION -->
        <div class="question-content">
            <div class="question-votes">
                <a href="javascript:void(0)" id="upvote-question"><img src="Images/upvote-arrow.png" /></a> 
                <div class="votes">
                    <span>2</span>
                </div>
                <a href="javascript:void(0)" id="downvote-question"><img src="Images/downvote-arrow.png" /></a> 
            </div>
            <div class="question-body" runat="server" id="QuestionBody"></div>
        </div>
        <!-- ANSWERS -->
        <div class="answers-content" id="AnswersContent" runat="server"></div>
        <!-- YOUR ANSWER -->
        <div class="your-answer-content">
            <div class="answers-header">
                <h1>Your answer</h1>
            </div>
            <asp:Label ID="lblError" runat="server" Visible="false"></asp:Label>
            <div id="wmd-editor" class="wmd-panel" style="float: left; clear: both;">
                <div id="wmd-button-bar">
                </div>
                <textarea id="wmd-input"></textarea>
            </div>
            <div id="wmd-preview" class="wmd-panel" style="margin-bottom: 10px;">
            </div>
            <a href="javascript:void(0)" id="lbPostAnswer" class="button-input" style="float: left;
                clear: both;">Post your answer</a>
            <div id="wmdOutput" class="wmd-panel" style="display: none;">
            </div>
        </div>
    </div>
</asp:Content>
