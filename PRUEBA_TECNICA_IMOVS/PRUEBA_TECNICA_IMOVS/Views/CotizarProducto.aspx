<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CotizarProducto.aspx.cs" Inherits="PRUEBA_TECNICA_IMOVS.Views.CotizarProducto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <h2>Generar Cotización</h2>

    <asp:DropDownList ID="ddlProductos" runat="server" AutoPostBack="false" />
    <asp:TextBox ID="txtCantidad" runat="server" Placeholder="Cantidad" />
    <asp:Button ID="btnAgregarProducto" runat="server" Text="Agregar" OnClick="btnAgregarProducto_Click" />

    <br /><br />

    <asp:GridView ID="gvCotizacion" runat="server" AutoGenerateColumns="False" OnRowCommand="gvCotizacion_RowCommand">
        <Columns>
            <asp:BoundField DataField="NombreProducto" HeaderText="Producto" />
            <asp:BoundField DataField="Precio" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
            <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C}" />
            <asp:ButtonField Text="Eliminar" CommandName="EliminarFila" ButtonType="Button" />
        </Columns>
    </asp:GridView>

    <br />

    <asp:Label ID="lblSubtotal" runat="server" Text="Subtotal: " /> <br /><br />
    <asp:Label ID="lblIVA" runat="server" Text="IVA: " /><br /><br />
    <asp:Label ID="lblTotal" runat="server" Text="Total: " /><br /><br />

    <asp:Button ID="btnConfirmarVenta" runat="server" Text="Confirmar Cotización" OnClick="btnConfirmarVenta_Click" />
</form>
</body>
</html>
