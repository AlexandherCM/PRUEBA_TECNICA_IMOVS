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
    
</form>
</body>
</html>
