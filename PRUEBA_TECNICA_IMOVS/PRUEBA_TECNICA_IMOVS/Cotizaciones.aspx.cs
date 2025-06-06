using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using PRUEBA_TECNICA_IMOVS.Models;
using PRUEBA_TECNICA_IMOVS.Models.Entities;
using System.Data.Entity;

namespace PRUEBA_TECNICA_IMOVS.Views
{
    public partial class Cotizaciones : System.Web.UI.Page
    {
        private Context db = new Context();
        private const decimal TasaIVA = 0.16m;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductosEnDesplegable();
                Session["DetallesCotizacionActual"] = new List<DetallesCotizacion>();
                CargarDetallesCotizacionEnTabla();
                CargarCotizacionesGuardadasEnTabla();
            }
        }

        private void CargarProductosEnDesplegable()
        {
            
            var productosActivos = db.Productos
                                    .Where(p => p.Estatus && p.Stock > 0)
                                    .OrderBy(p => p.Nombre)
                                    .ToList();

            ddlProductos.DataSource = productosActivos;
            ddlProductos.DataValueField = "ProductoId";
            ddlProductos.DataTextField = "Nombre";
            ddlProductos.DataBind();

            if (!productosActivos.Any())
            {
                ddlProductos.Items.Insert(0, new ListItem("No hay productos disponibles", "0"));
                btnAgregarProducto.Enabled = false;
            }
            else
            {
                btnAgregarProducto.Enabled = true;
            }
        }

        private List<DetallesCotizacion> DetallesCotizacionActual
        {
            get
            {
                
                if (Session["DetallesCotizacionActual"] == null)
                {
                    Session["DetallesCotizacionActual"] = new List<DetallesCotizacion>();
                }
                return (List<DetallesCotizacion>)Session["DetallesCotizacionActual"];
            }
            set { Session["DetallesCotizacionActual"] = value; }
        }

        private void CargarDetallesCotizacionEnTabla()
        {
            gvDetallesCotizacion.DataSource = DetallesCotizacionActual;
            gvDetallesCotizacion.DataBind();
            CalcularTotalesCotizacion();
        }

        private void CalcularTotalesCotizacion()
        {
            decimal subtotal = DetallesCotizacionActual.Sum(d => d.Subtotal);
            decimal iva = subtotal * TasaIVA;
            decimal totalConIVA = subtotal + iva;

            if (gvDetallesCotizacion.FooterRow != null)
            {
                ((Label)gvDetallesCotizacion.FooterRow.FindControl("lblSubtotalCotizacion")).Text = subtotal.ToString("C");
                ((Label)gvDetallesCotizacion.FooterRow.FindControl("lblIVACotizacion")).Text = iva.ToString("C");
                ((Label)gvDetallesCotizacion.FooterRow.FindControl("lblTotalConIVACotizacion")).Text = totalConIVA.ToString("C");
            }
        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            int idProducto = Convert.ToInt32(ddlProductos.SelectedValue);
            int cantidad = Convert.ToInt32(txtCantidad.Text);

            if (idProducto <= 0 || cantidad <= 0)
            {
                
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Selecciona un producto y una cantidad válida.');", true);
                return;
            }

            var producto = db.Productos.Find(idProducto);
            if (producto == null || !producto.Estatus || producto.Stock < cantidad)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Producto no disponible o stock insuficiente.');", true);
                return;
            }

            
            var detalleExistente = DetallesCotizacionActual.FirstOrDefault(d => d.ProductoId == idProducto);
            if (detalleExistente != null)
            {
                if (producto.Stock < (detalleExistente.Cantidad + cantidad))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No hay stock suficiente para agregar más de este producto.');", true);
                    return;
                }
                detalleExistente.Cantidad += cantidad;
                detalleExistente.Subtotal = detalleExistente.Cantidad * detalleExistente.PrecioUnitario;
            }
            else
            {
                var detalle = new DetallesCotizacion
                {
                    ProductoId = producto.ProductoId,
                    Producto = producto,
                    Cantidad = cantidad,
                    PrecioUnitario = producto.PrecioUnitario,
                    Subtotal = cantidad * producto.PrecioUnitario
                };
                DetallesCotizacionActual.Add(detalle);
            }

            CargarDetallesCotizacionEnTabla();
            txtCantidad.Text = "1"; 
        }

        protected void gvDetallesCotizacion_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int indice = e.RowIndex;
            if (indice >= 0 && indice < DetallesCotizacionActual.Count)
            {
                DetallesCotizacionActual.RemoveAt(indice);
                CargarDetallesCotizacionEnTabla();
            }
        }

        protected void btnGuardarCotizacion_Click(object sender, EventArgs e)
        {
            if (!DetallesCotizacionActual.Any())
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No puedes guardar una cotización vacía.');", true);
                return;
            }

            Cotizacion nuevaCotizacion = new Cotizacion
            {
                Fecha = DateTime.Now,
                Confirmada = false,
                TotalSinIVA = DetallesCotizacionActual.Sum(d => d.Subtotal),
                IVA = DetallesCotizacionActual.Sum(d => d.Subtotal) * TasaIVA,
                TotalConIVA = DetallesCotizacionActual.Sum(d => d.Subtotal) * (1 + TasaIVA),
                Detalles = DetallesCotizacionActual 
            };

            foreach (var detalle in nuevaCotizacion.Detalles)
            {

            }

            db.Cotizaciones.Add(nuevaCotizacion);
            db.SaveChanges(); 

            DetallesCotizacionActual = new List<DetallesCotizacion>();
            CargarDetallesCotizacionEnTabla();
            CargarCotizacionesGuardadasEnTabla(); 
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Cotización guardada exitosamente!');", true);
        }

        protected void btnConfirmarVenta_Click(object sender, EventArgs e)
        {
            
            if (!DetallesCotizacionActual.Any())
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No hay cotización actual para confirmar.');", true);
                return;
            }

            foreach (var detalle in DetallesCotizacionActual)
            {
                var productoEnBD = db.Productos.Find(detalle.ProductoId);
                if (productoEnBD == null || productoEnBD.Stock < detalle.Cantidad)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('Stock insuficiente para el producto: {detalle.Producto.Nombre}.');", true);
                    return;
                }
            }

            Cotizacion cotizacionVenta = new Cotizacion
            {
                Fecha = DateTime.Now,
                Confirmada = true,
                TotalSinIVA = DetallesCotizacionActual.Sum(d => d.Subtotal),
                IVA = DetallesCotizacionActual.Sum(d => d.Subtotal) * TasaIVA,
                TotalConIVA = DetallesCotizacionActual.Sum(d => d.Subtotal) * (1 + TasaIVA),
                Detalles = new List<DetallesCotizacion>() 
            };

            foreach (var detalleSesion in DetallesCotizacionActual)
            {
                var productoOriginal = db.Productos.Find(detalleSesion.ProductoId);
                if (productoOriginal != null)
                {

                    productoOriginal.Stock -= detalleSesion.Cantidad;
                    db.Entry(productoOriginal).State = EntityState.Modified; 
                    var nuevoDetalle = new DetallesCotizacion
                    {
                        ProductoId = detalleSesion.ProductoId,
                        Producto = productoOriginal,
                        Cantidad = detalleSesion.Cantidad,
                        PrecioUnitario = detalleSesion.PrecioUnitario,
                        Subtotal = detalleSesion.Subtotal
                    };
                    cotizacionVenta.Detalles.Add(nuevoDetalle);
                }
            }
            db.Cotizaciones.Add(cotizacionVenta);
            db.SaveChanges();
            DetallesCotizacionActual = new List<DetallesCotizacion>();
            CargarDetallesCotizacionEnTabla();
            CargarCotizacionesGuardadasEnTabla();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('¡Venta confirmada exitosamente! Stock actualizado.');", true);
            CargarProductosEnDesplegable();
        }


        private void CargarCotizacionesGuardadasEnTabla()
        {
            gvCotizacionesGuardadas.DataSource = db.Cotizaciones.OrderByDescending(c => c.Fecha).ToList();
            gvCotizacionesGuardadas.DataBind();
        }

        protected void gvCotizacionesGuardadas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCotizacionesGuardadas.PageIndex = e.NewPageIndex;
            CargarCotizacionesGuardadasEnTabla();
        }

        protected void gvCotizacionesGuardadas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VerDetalles")
            {
                int idCotizacion = Convert.ToInt32(e.CommandArgument);
                var cotizacion = db.Cotizaciones
                                   .Include(c => c.Detalles.Select(d => d.Producto))
                                   .FirstOrDefault(c => c.Id == idCotizacion);

                if (cotizacion != null)
                {
                    gvModalDetalles.DataSource = cotizacion.Detalles.ToList();
                    gvModalDetalles.DataBind();

                    string estado = cotizacion.Confirmada ? "Confirmada (Venta)" : "Pendiente";
                    string script = $"$('#lblModalCotizacionId').text('{cotizacion.Id}');" +
                                    $"$('#lblModalFecha').text('{cotizacion.Fecha.ToShortDateString()}');" +
                                    $"$('#lblModalEstado').text('{estado}');" +
                                    $"$('#lblModalSubtotal').text('{cotizacion.TotalSinIVA.ToString("C")}');" +
                                    $"$('#lblModalIVA').text('{cotizacion.IVA.ToString("C")}');" +
                                    $"$('#lblModalTotal').text('{cotizacion.TotalConIVA.ToString("C")}');" +
                                    "var detalleModal = new bootstrap.Modal(document.getElementById('detalleCotizacionModal'));" +
                                    "detalleModal.show();";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowDetalleModal", script, true);
                }
            }
            else if (e.CommandName == "ConfirmarVentaGrid")
            {
                int idCotizacion = Convert.ToInt32(e.CommandArgument);
                Cotizacion cotizacionAConfirmar = db.Cotizaciones
                                                    .Include(c => c.Detalles.Select(d => d.Producto)) 
                                                    .FirstOrDefault(c => c.Id == idCotizacion);

                if (cotizacionAConfirmar != null && !cotizacionAConfirmar.Confirmada)
                {
                    
                    foreach (var detalle in cotizacionAConfirmar.Detalles)
                    {
                        var productoEnBD = db.Productos.Find(detalle.ProductoId);
                        if (productoEnBD == null || productoEnBD.Stock < detalle.Cantidad)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('Stock insuficiente para el producto: {detalle.Producto.Nombre}. No se puede confirmar la venta.');", true);
                            return;
                        }
                    }

                    cotizacionAConfirmar.Confirmada = true;
                    foreach (var detalle in cotizacionAConfirmar.Detalles)
                    {
                        var productoOriginal = db.Productos.Find(detalle.ProductoId);
                        if (productoOriginal != null)
                        {
                            productoOriginal.Stock -= detalle.Cantidad;
                            db.Entry(productoOriginal).State = EntityState.Modified;
                        }
                    }

                    db.Entry(cotizacionAConfirmar).State = EntityState.Modified;
                    db.SaveChanges();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('¡Cotización confirmada como venta y stock actualizado!');", true);
                    CargarCotizacionesGuardadasEnTabla(); 
                    CargarProductosEnDesplegable();
                }
                else if (cotizacionAConfirmar.Confirmada)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Esta cotización ya ha sido confirmada como venta.');", true);
                }
            }
        }
    }
}