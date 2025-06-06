<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgregarProducto.aspx.cs" Inherits="PRUEBA_TECNICA_IMOVS.Views.AgregarProducto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Agregar Producto</h2>

        <label>Nombre del Producto:</label><br />
        <asp:TextBox ID="txtNombre" runat="server" /><br />

        <label>Tipo de Producto:</label><br />
        <asp:TextBox ID="txtTipo" runat="server" /><br />

        <label>Precio Unitario (con IVA):</label><br />
        <asp:TextBox ID="txtPrecio" runat="server" /><br />

        <label>Stock:</label><br />
        <asp:TextBox ID="txtStock" runat="server" /><br />

        <asp:CheckBox ID="chkActivo" runat="server" Checked="true" Text="Activo" /><br /><br />

        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" /><br />

        <asp:Label ID="lblMensaje" runat="server" ForeColor="Green" />
    </form>
</body>
</html>
