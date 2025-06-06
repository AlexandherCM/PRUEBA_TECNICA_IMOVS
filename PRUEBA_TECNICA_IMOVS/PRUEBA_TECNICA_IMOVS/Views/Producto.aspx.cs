using System;
using System.Linq;
using System.Web.UI;
using PRUEBA_TECNICA_IMOVS.Models;
using PRUEBA_TECNICA_IMOVS.Models.Entities;

namespace PRUEBA_TECNICA_IMOVS.Views
{
    public partial class Producto : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) CargarProductos();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            using (var db = new Context())
            {
                var nuevo = new Models.Entities.Producto
                {
                    Nombre = txtNombre.Text,
                    Tipo = txtTipo.Text,
                    Stock = int.Parse(txtStock.Text),
                    Estatus = chkActivo.Checked,
                    PrecioUnitario = decimal.Parse(txtPrecio.Text)
                };

                db.Productos.Add(nuevo);
                db.SaveChanges();
                CargarProductos();
            }
        }

        private void CargarProductos()
        {
            using (var db = new Context())
            {
                GridViewProductos.DataSource = db.Productos.ToList();
                GridViewProductos.DataBind();
            }
        }
    }
}
