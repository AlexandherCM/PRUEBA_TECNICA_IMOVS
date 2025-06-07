<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrearCotizacion.aspx.cs" Inherits="PRUEBA_TECNICA_IMOVS.Views.CrearCotizacion" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Crear Cotización</title>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Crear Cotización</h2>

        <label>Producto:</label>
        <select id="productoSelect"></select>

        <label>Cantidad:</label>
        <input type="number" id="cantidadInput" min="1" value="1" />

        <button type="button" id="agregarBtn">Agregar</button>

        <hr />

        <table border="1" id="detalleCotizacion">
            <thead>
                <tr>
                    <th>Producto</th>
                    <th>Precio Unitario</th>
                    <th>Cantidad</th>
                    <th>Total</th>
                    <th>Eliminar</th>
                </tr>
            </thead>
            <tbody></tbody>
        </table>

        <h3>Subtotal (sin IVA): $<span id="subtotal">0.00</span></h3>
        <h3>IVA (16%): $<span id="iva">0.00</span></h3>
        <h3>Total: $<span id="total">0.00</span></h3>
        <button type="button" id="guardarBtn">Guardar Cotización</button>

    </form>

    <script>
        const IVA_RATE = 0.16;
        let productos = [];
        let cotizacionDetalle = [];

        $(document).ready(function () {
            cargarProductos();

            $('#agregarBtn').click(function () {
                const id = parseInt($('#productoSelect').val());
                const cantidad = parseInt($('#cantidadInput').val());
                if (!id || !cantidad || cantidad <= 0) return alert("Selecciona un producto y cantidad válida.");

                const producto = productos.find(p => p.Id === id);
                if (!producto) return;

                cotizacionDetalle.push({
                    Id: producto.Id,
                    Nombre: producto.Nombre,
                    PrecioUnitario: producto.PrecioVenta,
                    Cantidad: cantidad
                });

                renderDetalle();
            });
        });

        function cargarProductos() {
            $.getJSON('/api/productos', function (data) {
                productos = data;
                $('#productoSelect').empty();
                data.forEach(p => {
                    $('#productoSelect').append(`<option value="${p.Id}">${p.Nombre} - $${p.PrecioVenta.toFixed(2)}</option>`);
                });
            });
        }

        function renderDetalle() {
            let html = '';
            let total = 0;

            cotizacionDetalle.forEach((item, index) => {
                const rowTotal = item.PrecioUnitario * item.Cantidad;
                total += rowTotal;

                html += `<tr>
                    <td>${item.Nombre}</td>
                    <td>$${item.PrecioUnitario.toFixed(2)}</td>
                    <td>${item.Cantidad}</td>
                    <td>$${rowTotal.toFixed(2)}</td>
                    <td><button type="button" onclick="eliminar(${index})">X</button></td>
                </tr>`;
            });

            $('#detalleCotizacion tbody').html(html);

            const subtotal = total / (1 + IVA_RATE);
            const iva = total - subtotal;

            $('#subtotal').text(subtotal.toFixed(2));
            $('#iva').text(iva.toFixed(2));
            $('#total').text(total.toFixed(2));
        }

        function eliminar(index) {
            cotizacionDetalle.splice(index, 1);
            renderDetalle();
        }

        $('#guardarBtn').click(function () {
            if (cotizacionDetalle.length === 0) {
                return alert("Agrega al menos un producto.");
            }

            const cotizacion = {
                Estado: "Pendiente",
                Detalles: cotizacionDetalle.map(item => ({
                    ProductoId: item.Id,
                    Cantidad: item.Cantidad
                }))
            };

            $.ajax({
                url: '/api/cotizaciones',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(cotizacion),
                success: function (res) {
                    alert(res.mensaje || "Cotización guardada");
                    location.reload(); // o redirigir a otra página
                },
                error: function (xhr) {
                    alert("Error al guardar: " + xhr.responseText);
                }
            });
        });

    </script>
</body>
</html>
