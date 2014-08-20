﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Question.aspx.cs" Inherits="Students_Study_Groups.Question" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Question.css" type="text/css" rel="stylesheet" />
    <link href="Styles/prettify.css" type="text/css" rel="stylesheet" />
    <script src='<%= ResolveUrl("~/Scripts/Question.js")%>' type="text/javascript"></script>
    <script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="question-container">
        <div class="question-header">
            <div class="float-left">
                <h1 style="display:inline;"><%=QuestionMod.Title %></h1>
            </div>
            <div class="float-right">   
                <h2 style="display:inline;">Views:      <%=QuestionMod.Views %></h2>
                <h2 style="display:inline;">Date asked: <%=DateTime.Parse(QuestionMod.DateAsked).ToShortDateString() %></h2>
            </div>
        </div>
        <hr />
        <div class="question-content">
            <div class="question-votes">
                
            </div>
            <div class="question-body" runat="server" id="QuestionBody">
                <%=QuestionMod.Body %>
                <div>
                    <div class="question-tags" style="display:inline">
                        <div class="tag">Tag1</div>
                        <div class="tag">Tag2</div>
                        <div class="tag">Tag3</div>
                    </div>
                    <div style="float:right;">Asked by: Fico</div>
                </div>
                <hr />
                <div class="question-comments">
                    <a href="javascript:void(0)" id="add-comment" class="button-comment">add comment</a>

                </div>
            </div>
        </div>
    </div>
</asp:Content>

