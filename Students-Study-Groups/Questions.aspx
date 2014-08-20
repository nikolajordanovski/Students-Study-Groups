<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Questions.aspx.cs" Inherits="Students_Study_Groups.Questions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Questions.css" type="text/css" rel="stylesheet" />
    <script src='<%= ResolveUrl("~/Scripts/jquery-1.4.1.min.js")%>' type="text/javascript"></script>
    <script src='<%= ResolveUrl("~/Scripts/Questions.js")%>' type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div id="container">

    <div id="questions">
        <!--
        <div class="question">
            <div class="left">
                <div>2922</div>
                <div>Votes</div>
                <div>15</div>
                <div>Answers</div>
            </div>
            <div class="right">
                <div class="title">
                    What is the correct JSON content type?
                </div>
                <div class="description">
                This question attempts to collect the few pearls among the dozens of bad C++ books that are published every year. Unlike many other programming languages, which are often picked up on the go from ...
                </div>
                <div class="tags">
                    <div class="tag">Java</div>
                    <div class="tag">JSON</div>
                    <div class="tag">Android</div>
                    <div class="tag">GitHub</div>
                </div>
            </div>
        </div>
        -->

        <div class="question">
            <div class="left">
            </div>
            <div class="right">
                <div class="title">
                    What is the correct JSON content type?
                </div>
                <div class="description">
                This question attempts to collect the few pearls among the dozens of bad C++ books that are published every year. Unlike many other programming languages, which are often picked up on the go from ...
                </div>
                <div class="tags">
                    <div class="tag">Java</div>
                    <div class="tag">JSON</div>
                    <div class="tag">Android</div>
                    <div class="tag">GitHub</div>
                </div>
            </div>
        </div>
    </div>

    <div id="popularTags">
    </div>

</div>

</asp:Content>
