<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Students_Study_Groups.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Index.css" type="text/css" rel="stylesheet" />
    <script src='<%= ResolveUrl("~/Scripts/Index.js")%>' type="text/javascript"></script>
    <script src='<%= ResolveUrl("~/Scripts/jquery-1.11.1.min.js")%>' type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <table>
        <tr>
            <td>
                <div id="subject" class="subject">
                    <div class="name">Probability and Statistics</div>
                    <div class="views">231 views</div>
                    <div class="questions">124 questions</div>
                </div>
            </td>
            <td><div class="subject"></div></td>
            <td><div class="subject"></div></td>
            <td><div class="subject"></div></td>
            <td><div class="subject"></div></td>
            <td><div class="subject"></div></td>
        </tr>
        <tr>
            <td><div class="subject"></div></td>
            <td><div class="subject"></div></td>
            <td><div class="subject"></div></td>
            <td><div class="subject"></div></td>
            <td><div class="subject"></div></td>
            <td><div class="subject"></div></td>
        </tr>
        <tr>
            <td><div class="subject"></div></td>
            <td><div class="subject"></div></td>
            <td><div class="subject"></div></td>
            <td><div class="subject"></div></td>
            <td><div class="subject"></div></td>
            <td><div class="subject"></div></td>
        </tr>
    </table>
</asp:Content>
