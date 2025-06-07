<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cotizaciones.aspx.cs" Inherits="PRUEBA_TECNICA_IMOVS.Views.Cotizaciones" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lista de Cotizaciones</title>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(function () {
            $.getJSON('/api/Cotizaciones', function (data) {
                let html = '<table border="1"><thead><tr><th>Id</th><th>Fecha</th><th>Estado</th><th>Detalles</th></tr></thead><tbody>';
                data.forEach(c => {
                    html += `<tr>
                            <td>${c.Id}</td>
                            <td>${new Date(c.Fecha).toLocaleDateString()}</td>
                            <td>${c.Estado}</td>
                            <td>${c.Detalles.length}</td>
                        </tr>`;
                });
                html += '</tbody></table>';
                $('#cotizacionesContainer').html(html);
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="cotizacionesContainer">Cargando cotizaciones...</div>
    </form>
</body>
</html>
