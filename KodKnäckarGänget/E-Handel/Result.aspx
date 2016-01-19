﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Result.aspx.cs" Inherits="E_Handel.Result" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="PlaceholderHead" runat="server">
</asp:Content>
<asp:Content ID="ContentMain" ContentPlaceHolderID="PlaceholderMain" runat="server">
    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span12">
                <h1 id="ResultTitle" runat="server">Search result: </h1>
                <img id="ResultImage" runat="server" />
                <p id="ResultDescription" runat="server"></p>
                <hr />
                <asp:Panel ID="ResultPanel" runat="server">
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
