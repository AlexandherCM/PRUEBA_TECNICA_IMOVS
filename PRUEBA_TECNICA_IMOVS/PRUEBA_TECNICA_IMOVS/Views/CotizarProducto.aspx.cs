using PRUEBA_TECNICA_IMOVS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PRUEBA_TECNICA_IMOVS.Models;

namespace PRUEBA_TECNICA_IMOVS.Views
{
    public partial class CotizarProducto : System.Web.UI.Page
    {
        static DataTable Cotizacion = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Cotizacion.Columns.Add("IdProducto", typeof(int));
                Cotizacion.Columns.Add("NombreProducto", typeof(string));
                Cotizacion.Columns.Add("Precio", typeof(decimal));
                Cotizacion.Columns.Add("Cantidad", typeof(int));
                Cotizacion.Columns.Add("Total", typeof(decimal));
                ViewState["Cotizacion"] = Cotizacion;


                using (var db = new Context())
                {
                    var productos = db.Productos
                        .Where(p => p.Estatus && p.Stock > 0)
                        .ToList();

                    ddlProductos.DataSource = productos;
                    ddlProductos.DataTextField = "NombreProducto";
                    ddlProductos.DataValueField = "IdProducto";
                    ddlProductos.DataBind();
                }
            }
        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            var tabla = ViewState["Cotizacion"] as DataTable;

            int idProd = int.Parse(ddlProductos.SelectedValue);
            int cantidad = int.Parse(txtCantidad.Text);

            using (var db = new Context())
            {
                var producto = db.Productos.Find(idProd);

                if (producto != null && producto.Stock >= cantidad)
                {
                    decimal total = producto.Precio * cantidad;

                    tabla.Rows.Add(
                        producto.IdProducto,
                        producto.NombreProducto,
                        producto.Precio,
                        cantidad,
                        total
                    );

                    gvCotizacion.DataSource = tabla;
                    gvCotizacion.DataBind();

                    ViewState["Cotizacion"] = tabla;

                    CalcularTotales();
                }
            }
        }

        private void CalcularTotales()
        {
            var tabla = ViewState["Cotizacion"] as DataTable;
            decimal total = 0;

            foreach (DataRow row in tabla.Rows)
                total += Convert.ToDecimal(row["Total"]);

            decimal subtotal = total / 1.16m;
            decimal iva = total - subtotal;

            lblSubtotal.Text = $"Subtotal: {subtotal:C}";
            lblIVA.Text = $"IVA: {iva:C}";
            lblTotal.Text = $"Total: {total:C}";
        }

        protected void gvCotizacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EliminarFila")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                var tabla = ViewState["Cotizacion"] as DataTable;

                if (index >= 0 && index < tabla.Rows.Count)
                {
                    tabla.Rows.RemoveAt(index);
                    gvCotizacion.DataSource = tabla;
                    gvCotizacion.DataBind();
                    ViewState["Cotizacion"] = tabla;
                    CalcularTotales();
                }
            }
        }

        protected void btnConfirmarVenta_Click(object sender, EventArgs e)
        {
            var tabla = ViewState["Cotizacion"] as DataTable;

            if (tabla.Rows.Count == 0)
                return;

            decimal total = tabla.AsEnumerable().Sum(row => row.Field<decimal>("Total"));
            decimal subtotal = total / 1.16m;
            decimal iva = total - subtotal;

            using (var db = new Context())
            {
                var cotizacion = new Cotizacion
                {
                    Subtotal = subtotal,
                    Iva = iva,
                    Total = (int)total,
                    Estado = true
                };

                db.Cotizaciones.Add(cotizacion);
                db.SaveChanges();

                foreach (DataRow row in tabla.Rows)
                {
                    var detalle = new DetalleCotizacion
                    {
                        IdCotizacion = cotizacion.IdCotizacion,
                        IdProducto = Convert.ToInt32(row["IdProducto"]),
                        Cantidad = Convert.ToInt32(row["Cantidad"]),
                        Precio = Convert.ToDecimal(row["Precio"]),
                        TotalCotizacion = Convert.ToDecimal(row["Total"])
                    };

                    db.DetalleCotizaciones.Add(detalle);
                }

                db.SaveChanges();
            }

            Cotizacion.Clear();
            gvCotizacion.DataSource = null;
            gvCotizacion.DataBind();
            lblSubtotal.Text = "Subtotal: ";
            lblIVA.Text = "IVA: ";
            lblTotal.Text = "Total: ";
        }

    }
}