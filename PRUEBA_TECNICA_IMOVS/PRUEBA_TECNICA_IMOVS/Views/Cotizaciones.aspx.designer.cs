using System;
using System.Collections.Generic;
using System.Linq;
using PRUEBA_TECNICA_IMOVS.Models.Entities;

namespace PRUEBA_TECNICA_IMOVS.Views
{
    public partial class NuevaCotizacion : System.Web.UI.Page
    {
        public class DetalleTemporal
        {
            public int ProductoId { get; set; }
            public string ProductoNombre { get; set; }
            public decimal PrecioUnitario { get; set; }
            public int Unidades { get; set; }
            public decimal Subtotal { get; set; }
        }

        private List<DetalleTemporal> ListaDetalle
        {
            get
            {
                if (ViewState["Detalle"] == null)
                    ViewState["Detalle"] = new List<DetalleTemporal>();
                return (List<DetalleTemporal>)ViewState["Detalle"];
            }
            set
            {
                ViewState["Detalle"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
            }
        }

        private void CargarProductos()
        {
            using (var ctx = new Context())
            {
                ddlProductos.DataSource = ctx.Productos
                    .Where(p => p.Estatus && p.Stock > 0)
                    .Select(p => new { p.ProductoId, p.Nombre })
                    .ToList();

                ddlProductos.DataTextField = "Nombre";
                ddlProductos.DataValueField = "ProductoId";
                ddlProductos.DataBind();
            }
        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            int productoId = int.Parse(ddlProductos.SelectedValue);
            int cantidad = int.Parse(txtCantidad.Text);

            using (var ctx = new Context())
            {
                var prod = ctx.Productos.Find(productoId);
                if (prod == null || cantidad <= 0 || cantidad > prod.Stock)
                    return;

                var detalle = new DetalleTemporal
                {
                    ProductoId = prod.ProductoId,
                    ProductoNombre = prod.Nombre,
                    PrecioUnitario = prod.PrecioUnitario,
                    Unidades = cantidad,
                    Subtotal = prod.PrecioUnitario * cantidad
                };

                var lista = ListaDetalle;
                lista.Add(detalle);
                ListaDetalle = lista;

                gvDetalle.DataSource = lista;
                gvDetalle.DataBind();

                ActualizarTotal();
            }
        }

        private void ActualizarTotal()
        {
            decimal total = ListaDetalle.Sum(x => x.Subtotal);
            lblTotal.Text = "Total: $" + total.ToString("F2");
        }

        protected void gvDetalle_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                var lista = ListaDetalle;
                lista.RemoveAt(index);
                ListaDetalle = lista;

                gvDetalle.DataSource = lista;
                gvDetalle.DataBind();

                ActualizarTotal();
            }
        }

        protected void btnGuardarCotizacion_Click(object sender, EventArgs e)
        {
            var lista = ListaDetalle;
            if (lista.Count == 0)
                return;

            using (var ctx = new Context())
            {
                var cot = new Cotizacion
                {
                    Fecha = DateTime.Now,
                    Estado = "Cotización",
                    Total = lista.Sum(x => x.Subtotal)
                };

                ctx.Cotizaciones.Add(cot);
                ctx.SaveChanges(); // para generar CotizacionId

                foreach (var item in lista)
                {
                    ctx.DetalleCotizaciones.Add(new DetalleCotizacion
                    {
                        CotizacionId = cot.CotizacionId,
                        ProductoId = item.ProductoId,
                        Unidades = item.Unidades,
                        PrecioUnitario = item.PrecioUnitario,
                        Subtotal = item.Subtotal
                    });
                }

                ctx.SaveChanges();
            }

            // Limpia todo
            ViewState["Detalle"] = null;
            Response.Redirect("Cotizaciones.aspx");
        }
    }
}
