<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AskQuestion.aspx.cs" Inherits="Students_Study_Groups.AskQuestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/AskQuestion.css" type="text/css" rel="stylesheet" />
    <link href="Styles/tokens.css" type="text/css" rel="stylesheet" />
    <link href="Styles/wmd.css" type="text/css" rel="stylesheet" />
    <script src='<%= ResolveUrl("~/Scripts/AskQuestion.js")%>' type="text/javascript"></script>
    <script src='<%= ResolveUrl("~/Scripts/showdown.js")%>' type="text/javascript"></script>
    <script src='<%= ResolveUrl("~/Scripts/wmd.js")%>' type="text/javascript"></script>
    <script src='<%= ResolveUrl("~/Scripts/tokens.min.js")%>' type="text/javascript"></script>
    <script type="text/javascript">
        var userId    = <%=UID %>;
        var subjectId = <%=SID %>;
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hfTags" runat="server" Value="asd" />
    <script type="text/javascript">
        var tagsList = $("#<%=hfTags.ClientID %>").val();
        var tagsList = tagsList.split(",");
    </script>
    <div class="ask-question-container">
        <h1>Ask a question</h1>
        <hr />
        <asp:Label ID="lblError" runat="server" Visible="false"></asp:Label>
        <asp:TextBox ID="tbTitle" placeholder="Title" CssClass="text-input" Style="width: 480px !important;
            margin-bottom: 10px;" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvTitle" ControlToValidate="tbTitle" runat="server"
            ErrorMessage="You must add a title."></asp:RequiredFieldValidator>
        <div id="wmd-editor" class="wmd-panel" style="float: left; clear: both;">
            <div id="wmd-button-bar">
            </div>
            <textarea id="wmd-input"></textarea>
        </div>
        <div id="wmd-preview" class="wmd-panel" style="margin-bottom: 10px;">
        </div>
        <asp:TextBox ID="tbTags" placeholder="Tags (separate with a comma)" CssClass="text-input"
            Style="width: 480px !important; margin-top: 10px;" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvTags" ControlToValidate="tbTags" runat="server"
            ErrorMessage="You must add atleast one tag."></asp:RequiredFieldValidator>
        <br />
        <br />
        <a href="javascript:void(0)" id="lbAskQuestion" class="button-input">Ask the question</a>
        <div id="wmdOutput" class="wmd-panel" style="display: none;">
        </div>
    </div>
</asp:Content>
