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
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var db = new Context())
            {
                var productos = db.Productos
                    .Where(p => p.Estatus && p.Stock > 0)
                    .Select(p => new { p.IdProducto, p.NombreProducto })
                    .ToList();

                ddlProductos.DataSource = productos;
                ddlProductos.DataTextField = "NombreProducto";
                ddlProductos.DataValueField = "IdProducto";
                ddlProductos.DataBind();
            }
        }

    }
}