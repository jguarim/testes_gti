<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForms._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>Clientes</h2>
    <table>
    <thead>
        <tr><th>Name</th><th>Price</th></tr>
    </thead>
    <tbody id="clientes">
        <script type="text/javascript">
            function getProducts() {
                $.getJSON("api/products",
                    function (data) {
                        $('#clientes').empty(); // Clear the table body.

                        // Loop through the list of products.
                        $.each(data, function (key, val) {
                            // Add a table row for the product.
                            var row = '<td>' + val.Name + '</td><td>' + val.Price + '</td>';
                            $('<tr/>', { html: row })  // Append the name.
                                .appendTo($('#clientes'));
                        });
                    });
            }

            $(document).ready(getProducts);
        </script>
    </tbody>
    </table>
</asp:Content>