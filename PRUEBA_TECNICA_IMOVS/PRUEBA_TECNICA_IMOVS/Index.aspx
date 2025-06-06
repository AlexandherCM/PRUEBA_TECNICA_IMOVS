<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="PRUEBA_TECNICA_IMOVS.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center;">
        <div class="titulo">Productos Registrados</div>

        <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="False" BorderWidth="1px" BorderColor="#ccc" Width="80%" HorizontalAlign="Center">
            <Columns>
                <asp:BoundField DataField="IdProducto" HeaderText="ID" />
                <asp:BoundField DataField="NombreProducto" HeaderText="Nombre" />
                <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" />
                <asp:BoundField DataField="Stock" HeaderText="Stock" />
                <asp:BoundField DataField="TipoProducto" HeaderText="Tipo" />
                <asp:BoundField DataField="Estatus" HeaderText="Activo" />
            </Columns>
        </asp:GridView>

        <br />
        <a class="btn-link" href="Views/AgregarProducto.aspx">Agregar Producto</a>
        <a class="btn-link" href="Views/Cotizar.aspx">Realizar Cotización</a>
    </div>
</form>
</body>
</html>
