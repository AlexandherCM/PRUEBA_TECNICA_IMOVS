using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using PRUEBA_TECNICA_IMOVS.Models;

namespace PRUEBA_TECNICA_IMOVS
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarProductos();
        }

        private void CargarProductos()
        {
            using (var context = new Context())
            {
                var productos = context.Productos.ToList();
                gvProductos.DataSource = productos;
                gvProductos.DataBind();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Views/AgregarProducto.aspx");
        }

        protected void btnCotizar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Views/CotizarProducto.aspx");
        }
    }
}