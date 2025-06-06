using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PRUEBA_TECNICA_IMOVS
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnProducto_Click(object sender, EventArgs e)
        {
            Response.Redirect("Views/Producto.aspx");
        }

        protected void btnCotizacion_Click(object sender, EventArgs e)
        {
            Response.Redirect("Views/Cotizaciones.aspx");
        }
    }
}