﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="E_Handel.Product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="http://getbootstrap.com/2.3.2/assets/css/bootstrap.css" rel="stylesheet">
    <link href="http://getbootstrap.com/2.3.2/assets/css/bootstrap-responsive.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Home" runat="server">

    <div class="row-fluid">
        <div class="span4">
            <div class="image_placeholder">Image placeholder</div>
        </div>

        <div class="span8">
            <h1>Product Title</h1>
            <h3 class="price_tag">£7</h3>
            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. </p>
            <p>
                <label for="quantity">Quantity:</label><input id="quantity" type="number" min="1" max="100" />
                <br />
                <select>
                    <option>Standard Edition</option>
                    <option>Blu-Ray Edition</option>
                    <option>Steelbook Edition</option>
                </select>
            </p>
            <p><a class="btn buy_button" href="#">Add to cart</a></p>
        </div>
    </div>
    <hr />

</asp:Content>
