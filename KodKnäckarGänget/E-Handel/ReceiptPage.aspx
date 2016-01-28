﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceiptPage.aspx.cs" Inherits="E_Handel.ReceiptPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Style/Receipt.css" rel="stylesheet" />
    <script src="/Scripts/bootstrap.js"></script>
    <script src="/Scripts/jquery-1.9.1.js"></script>
    <script src="/Scripts/cartScript.js"></script>
    <link href="/Content/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <form id="ReceiptForm" runat="server">
        <div class="container-fluid">
            <div class="row-fluid">
                <div id="ReceiptOutput">
                    <h1>Thank you for shopping with us!</h1>
                    <table runat="server">
                        <tr>
                            <th>OrderId: </th>
                            <td id="orderIdOutput"></td>
                        </tr>
                        <tr>
                            <th>TotalPrice: </th>
                            <td id="totalPrice"></td>
                        </tr>
                        <tr>
                            <th>Postage: </th>
                            <td id="postage"></td>
                        </tr>
                        <tr>
                            <th>Adress: </th>
                            <td id="adress"></td>
                        </tr>
                        <tr>
                            <th>PostalCode: </th>
                            <td id="postalCode"></td>
                        </tr>
                        <tr>
                            <th>City: </th>
                            <td id="city"></td>
                        </tr>
                        <tr>
                            <th>Country: </th>
                            <td id="country"></td>
                        </tr>
                        <tr>
                            <th>Email: </th>
                            <td id="email"></td>
                        </tr>
                        <tr>
                            <th>PhoneNumber: </th>
                            <td id="phoneNumber"></td>
                        </tr>
                        <tr>
                            <th>Payment Options: </th>
                            <td id="paymentOptions"></td>
                        </tr>
                        <tr>
                            <th>Delivery Option: </th>
                            <td id="deliveryOptions"></td>
                        </tr>
                        <tr>
                            <th>VAT: </th>
                            <td id="vatAmount"></td>
                        </tr>
                    </table>
                    <ul id="ulReceipt" runat="server">

                    </ul>
                    <div id="receiptPageButtons">
                    <asp:Button ID="receiptHomeButton" runat="server" class="btn btn-primary" text="Return to shop" OnClick="receiptHomeButton_Click"/> <br />    
                    <asp:Button ID="receiptPrintButton" runat="server" OnClick="receiptPrintButton_Click" />
                    </div>
                </div>

            </div>
        </div>
    </form>
</body>
</html>