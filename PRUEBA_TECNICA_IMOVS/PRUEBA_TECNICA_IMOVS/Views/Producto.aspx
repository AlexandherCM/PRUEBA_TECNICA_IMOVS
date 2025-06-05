<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Producto.aspx.cs" Inherits="PRUEBA_TECNICA_IMOVS.Views.Producto" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestión de Productos</title>
    <style>
        body { font-family: Arial; background-color: #f9f9f9; padding: 30px; }
        .form-control { margin: 5px 0; padding: 8px; width: 300px; }
        .btn { padding: 10px 20px; background: #007bff; color: white; border: none; margin-top: 10px; cursor: pointer; }
        .btn:hover { background-color: #0056b3; }
        table { border-collapse: collapse; margin-top: 20px; width: 100%; }
        th, td { border: 1px solid #ccc; padding: 8px; text-align: left; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Gestión de Productos</h1>

        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Nombre del producto"></asp:TextBox><br />
        <asp:TextBox ID="txtTipo" runat="server" CssClass="form-control" placeholder="Tipo de producto"></asp:TextBox><br />
        <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" placeholder="Stock"></asp:TextBox><br />
        <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" placeholder="Precio unitario"></asp:TextBox><br />
        <asp:CheckBox ID="chkActivo" runat="server" Text="Activo" /><br />
        <asp:Button ID="btnAgregar" runat="server" CssClass="btn" Text="Agregar producto" OnClick="btnAgregar_Click" /><br />

        <asp:GridView ID="GridViewProductos" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="ProductoId" HeaderText="ID" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                <asp:BoundField DataField="Stock" HeaderText="Stock" />
                <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio ($)" DataFormatString="{0:N2}" />
                <asp:CheckBoxField DataField="Estatus" HeaderText="Activo" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>