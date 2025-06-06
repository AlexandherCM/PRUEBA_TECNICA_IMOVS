<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cotizaciones.aspx.cs" Inherits="PRUEBA_TECNICA_IMOVS.Views.Cotizaciones" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Nueva Cotización</title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Crear Nueva Cotización</h2>

        <asp:DropDownList ID="ddlProductos" runat="server" />
        <asp:TextBox ID="txtCantidad" runat="server" TextMode="Number" />
        <asp:Button ID="btnAgregarProducto" runat="server" Text="Agregar Producto" OnClick="btnAgregarProducto_Click" />
        <br /><br />

        <asp:GridView ID="gvDetalle" runat="server" AutoGenerateColumns="False" OnRowCommand="gvDetalle_RowCommand">
            <Columns>
                <asp:BoundField DataField="ProductoNombre" HeaderText="Producto" />
                <asp:BoundField DataField="Unidades" HeaderText="Cantidad" />
                <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio" DataFormatString="{0:C}" />
                <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C}" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar" CommandArgument="<%# Container.DataItemIndex %>" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <br />
        <asp:Label ID="lblTotal" runat="server" Font-Bold="true" Text="Total: $0.00"></asp:Label>
        <br /><br />
        <asp:Button ID="btnGuardarCotizacion" runat="server" Text="Guardar Cotización" OnClick="btnGuardarCotizacion_Click" />
    </form>
</body>
</html>
