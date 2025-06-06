using PRUEBA_TECNICA_IMOVS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PRUEBA_TECNICA_IMOVS.Models;

namespace PRUEBA_TECNICA_IMOVS.Views
{
    public partial class AgregarProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            using (var context = new Context())
            {
                Producto nuevoProducto = new Producto
                {
                    NombreProducto = txtNombre.Text,
                    Precio = Convert.ToDecimal(txtPrecio.Text),
                    Stock = Convert.ToInt32(txtStock.Text),
                    Estatus = chkActivo.Checked,
                    TipoProducto = txtTipo.Text
                };

                context.Productos.Add(nuevoProducto);
                context.SaveChanges();

                lblMensaje.Text = "Producto agregado exitosamente.";
            }
        }
    }
}