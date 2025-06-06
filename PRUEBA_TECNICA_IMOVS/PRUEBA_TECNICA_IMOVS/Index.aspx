<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="PRUEBA_TECNICA_IMOVS.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Inicio - Sistema de Cotización</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            text-align: center;
            padding: 50px;
        }

        h1, h3 {
            color: #333;
        }

        .button-container {
            margin-top: 30px;
        }

        .nav-button {
            display: inline-block;
            margin: 10px;
            padding: 15px 30px;
            font-size: 16px;
            background-color: #007bff;
            color: #fff;
            border: none;
            border-radius: 6px;
            text-decoration: none;
            transition: background-color 0.3s;
        }

        .nav-button:hover {
            background-color: #0056b3;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>PRUEBA TÉCNICA: COTIZACIÓN DE PRODUCTOS</h1>
            <h3>Selecciona una opción para navegar:</h3>

            <div class="button-container">
                <a class="nav-button" href="Views/Producto.aspx">Gestión de Productos</a>
                <a class="nav-button" href="Views/Cotizaciones.aspx">Crear Cotización</a>
            </div>
        </div>
    </form>
</body>
</html>
