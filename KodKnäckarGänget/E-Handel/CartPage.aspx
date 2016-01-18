﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CartPage.aspx.cs" Inherits="E_Handel.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceholderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceholderMain" runat="server">
    <div class="container-fluid">
        <div class="row-fluid">
            <div id="ProductList">
                <table class="span9" id="ProductTable">
                    <tr>
                        <th class="span1">Image: </th>
                        <th class="span1">Productname</th>
                        <th class="span1">Price</th>
                        <th class="span1">Amount</th>
                        <th class="span1">Total cost</th>

                    </tr>
                </table>
            </div>
            <div class="span3">
                <div class="well sidebar-nav">
                    <ul class="nav nav-list">
                        <li class="nav-header">Ads</li>
                        <li><a href="#">Product 1</a></li>
                        <li><a href="#">Product 2</a></li>
                        <li><a href="#">Product 3</a></li>
                        <li><a href="#">Product 4</a></li>
                        <li><a href="#">Product 5</a></li>
                    </ul>
                </div>
            </div>
            <div>
                <input type="button" value="Go to Checkout"/>
                <input type="button" value="Keep shopping"/>
            </div>
        </div>
    </div>
</asp:Content>
