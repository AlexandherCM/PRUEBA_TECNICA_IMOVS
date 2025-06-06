using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using PRUEBA_TECNICA_IMOVS.Models;
using PRUEBA_TECNICA_IMOVS.Models.Entities;
using System.Data.Entity;

namespace PRUEBA_TECNICA_IMOVS.Views
{
    public partial class Productos : System.Web.UI.Page
    {
        private Context db = new Context();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            gvProductos.DataSource = db.Productos.ToList();
            gvProductos.DataBind();
        }

        protected void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            int productoId = Convert.ToInt32(hfProductoId.Value);

            Producto producto;

            if (productoId == 0) 
            {
                producto = new Producto();
                db.Productos.Add(producto);
            }
            else 
            {
                producto = db.Productos.Find(productoId);
                if (producto == null)
                {
                    
                    return;
                }
                db.Entry(producto).State = EntityState.Modified; 
            }

            producto.Nombre = txtNombre.Text;
            producto.Tipo = txtTipo.Text;
            producto.PrecioUnitario = Convert.ToDecimal(txtPrecioUnitario.Text);
            producto.Stock = Convert.ToInt32(txtStock.Text);
            producto.Estatus = chkEstatus.Checked;

            db.SaveChanges();

            BindGrid(); 
            
            ScriptManager.RegisterStartupScript(this, GetType(), "HideProductModal", "$(function(){ $('#productModal').modal('hide'); });", true);
        }

        protected void gvProductos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvProductos.EditIndex = e.NewEditIndex; 

            
            int productoId = Convert.ToInt32(gvProductos.DataKeys[e.NewEditIndex].Value);
            Producto producto = db.Productos.Find(productoId);

            if (producto != null)
            {
                
                hfProductoId.Value = producto.ProductoId.ToString();
                txtNombre.Text = producto.Nombre;
                txtTipo.Text = producto.Tipo;
                txtPrecioUnitario.Text = producto.PrecioUnitario.ToString();
                txtStock.Text = producto.Stock.ToString();
                chkEstatus.Checked = producto.Estatus;

                
                string script = $"editProduct({producto.ProductoId}, '{producto.Nombre}', '{producto.Tipo}', {producto.PrecioUnitario}, {producto.Stock}, {producto.Estatus.ToString().ToLower()});";
                ScriptManager.RegisterStartupScript(this, GetType(), "EditProductModal", script, true);
            }
            
        }


        protected void gvProductos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int productoId = Convert.ToInt32(gvProductos.DataKeys[e.RowIndex].Value);
            Producto producto = db.Productos.Find(productoId);

            if (producto != null)
            {
                db.Productos.Remove(producto);
                db.SaveChanges();
                BindGrid(); 
            }
        }

        protected void gvProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProductos.PageIndex = e.NewPageIndex;
            BindGrid();
        }
    }
}