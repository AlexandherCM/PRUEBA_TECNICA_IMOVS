<%@ Page Title="Cotizaciones" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cotizaciones.aspx.cs" Inherits="PRUEBA_TECNICA_IMOVS.Views.Cotizaciones" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2>Crear y Gestionar Cotizaciones</h2>
        <p>Selecciona productos y genera nuevas cotizaciones.</p>

        <div class="card mb-4">
            <div class="card-header bg-primary text-white">
                Nueva Cotización
            </div>
            <div class="card-body">
                <div class="row mb-3">
                    <div class="col-md-6">
                        <label for="ddlProductos" class="form-label">Seleccionar Producto:</label>
                        <asp:DropDownList ID="ddlProductos" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <label for="txtCantidad" class="form-label">Cantidad:</label>
                        <asp:TextBox ID="txtCantidad" runat="server" TextMode="Number" Text="1" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3 d-flex align-items-end">
                        <asp:Button ID="btnAgregarProducto" runat="server" Text="Agregar a Cotización" CssClass="btn btn-success w-100" OnClick="btnAgregarProducto_Click" />
                    </div>
                </div>

                <div class="table-responsive">
                    <asp:GridView ID="gvDetallesCotizacion" runat="server"
                        CssClass="table table-bordered table-sm"
                        AutoGenerateColumns="False" ShowFooter="True"
                        OnRowDeleting="gvDetallesCotizacion_RowDeleting"
                        OnRowDataBound="gvDetallesCotizacion_RowDataBound">

                        <Columns>
                            <asp:BoundField DataField="Producto.Nombre" HeaderText="Producto" />
                            <asp:BoundField DataField="Producto.PrecioUnitario" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                            <asp:BoundField DataField="Subtotal" HeaderText="Total Línea" DataFormatString="{0:C}" />

                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEliminarDetalle" runat="server" CommandName="Delete"
                                        CommandArgument='<%# Container.DataItemIndex %>'
                                        CssClass="btn btn-sm btn-danger">Eliminar</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%-- Footer columns --%>
                            <asp:TemplateField HeaderText="Subtotal (Footer)">
                                <FooterTemplate>
                                    <asp:Label ID="lblSubtotalCotizacion" runat="server" Text=""></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="IVA (Footer)">
                                <FooterTemplate>
                                    <asp:Label ID="lblIVACotizacion" runat="server" Text=""></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Total con IVA (Footer)">
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalConIVACotizacion" runat="server" Text=""></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>


                        <FooterStyle Font-Bold="True" />
                    </asp:GridView>
                </div>


                <div class="mt-3 text-end">
                    <asp:Button ID="btnGuardarCotizacion" runat="server" Text="Guardar Cotización" CssClass="btn btn-primary me-2" OnClick="btnGuardarCotizacion_Click" />
                    <asp:Button ID="btnConfirmarVenta" runat="server" Text="Confirmar Venta" CssClass="btn btn-success" OnClick="btnConfirmarVenta_Click" OnClientClick="return confirm('¿Estás seguro de que quieres confirmar esta cotización como venta? Esto actualizará el stock de los productos.');" />
                </div>
            </div>
        </div>

        <div class="card">
            <div class="card-header bg-info text-white">
                Cotizaciones Guardadas
            </div>
            <div class="card-body">
                <asp:GridView ID="gvCotizacionesGuardadas" runat="server"
                    CssClass="table table-striped table-bordered"
                    AutoGenerateColumns="False" DataKeyNames="Id"
                    AllowPaging="True" PageSize="5" OnPageIndexChanging="gvCotizacionesGuardadas_PageIndexChanging"
                    OnRowCommand="gvCotizacionesGuardadas_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID Cotización" ReadOnly="True" SortExpression="Id" />
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:d}" SortExpression="Fecha" />
                        <asp:CheckBoxField DataField="Confirmada" HeaderText="Confirmada como Venta" SortExpression="Confirmada" />
                        <asp:BoundField DataField="TotalConIVA" HeaderText="Total con IVA" DataFormatString="{0:C}" SortExpression="TotalConIVA" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnVerDetalles" runat="server" CommandName="VerDetalles" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-sm btn-info me-1">Ver Detalles</asp:LinkButton>
                                <asp:LinkButton ID="btnConfirmarVentaGrid" runat="server" CommandName="ConfirmarVentaGrid" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-sm btn-success" OnClientClick="return confirm('¿Estás seguro de que quieres confirmar esta cotización como venta? Esto actualizará el stock de los productos.');" Visible='<%# !(bool)Eval("Confirmada") %>'>Confirmar Venta</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pagination-ys" />
                </asp:GridView>

                <div class="modal fade" id="detalleCotizacionModal" tabindex="-1" aria-labelledby="detalleCotizacionModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="detalleCotizacionModalLabel">Detalles de Cotización #<span id="lblModalCotizacionId"></span></h5>
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div class="modal-body">
                                <p><strong>Fecha:</strong> <span id="lblModalFecha"></span></p>
                                <p><strong>Estado:</strong> <span id="lblModalEstado"></span></p>
                                <hr />
                                <h6>Productos en la Cotización:</h6>
                                <asp:GridView ID="gvModalDetalles" runat="server"
                                    CssClass="table table-bordered table-sm"
                                    AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="Producto.Nombre" HeaderText="Producto" />
                                        <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
                                        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                                        <asp:BoundField DataField="Subtotal" HeaderText="Total Línea" DataFormatString="{0:C}" />
                                    </Columns>
                                </asp:GridView>
                                <hr />
                                <div class="text-end">
                                    <p><strong>Subtotal:</strong> <span id="lblModalSubtotal"></span></p>
                                    <p><strong>IVA:</strong> <span id="lblModalIVA"></span></p>
                                    <h4><strong>Total con IVA:</strong> <span id="lblModalTotal"></span></h4>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function showDetalleCotizacionModal(cotizacionId) {
            $.ajax({
                type: "POST",
                url: "Cotizaciones.aspx/GetCotizacionDetails",
                data: JSON.stringify({ cotizacionId: cotizacionId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var cotizacion = response.d;
                    if (cotizacion) {
                        $('#lblModalCotizacionId').text(cotizacion.Id);
                        $('#lblModalFecha').text(new Date(parseInt(cotizacion.Fecha.substr(6))).toLocaleDateString());
                        $('#lblModalEstado').text(cotizacion.Confirmada ? 'Confirmada (Venta)' : 'Pendiente');
                        $('#lblModalSubtotal').text(cotizacion.TotalSinIVA.toLocaleString('es-MX', { style: 'currency', currency: 'MXN' }));
                        $('#lblModalIVA').text(cotizacion.IVA.toLocaleString('es-MX', { style: 'currency', currency: 'MXN' }));
                        $('#lblModalTotal').text(cotizacion.TotalConIVA.toLocaleString('es-MX', { style: 'currency', currency: 'MXN' }));

                        var detalleModal = new bootstrap.Modal(document.getElementById('detalleCotizacionModal'));
                        detalleModal.show();
                    }
                },
                error: function (xhr, status, error) {
                    console.error("Error al obtener detalles de cotización:", error);
                    alert("Error al cargar los detalles de la cotización.");
                }
            });
        }
    </script>

</asp:Content>
