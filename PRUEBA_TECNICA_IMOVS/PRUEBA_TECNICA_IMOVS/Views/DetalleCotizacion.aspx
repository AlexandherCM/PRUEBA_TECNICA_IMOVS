<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetalleCotizacion.aspx.cs" Inherits="PRUEBA_TECNICA_IMOVS.Views.DetalleCotizacion" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detalle de Cotización</title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Detalle de Cotización</h2>

        <asp:Label ID="lblFecha" runat="server" Text=""></asp:Label><br />
        <asp:Label ID="lblEstado" runat="server" Text=""></asp:Label><br />
        <asp:Label ID="lblTotal" runat="server" Font-Bold="true" Text=""></asp:Label><br /><br />

        <asp:GridView ID="gvDetalle" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Producto" HeaderText="Producto" />
                <asp:BoundField DataField="Unidades" HeaderText="Cantidad" />
                <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio" DataFormatString="{0:C}" />
                <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C}" />
            </Columns>
        </asp:GridView>

        <br />
        <asp:Button ID="btnVolver" runat="server" Text="Volver" PostBackUrl="Cotizaciones.aspx" />
    </form>
</body>
</html>