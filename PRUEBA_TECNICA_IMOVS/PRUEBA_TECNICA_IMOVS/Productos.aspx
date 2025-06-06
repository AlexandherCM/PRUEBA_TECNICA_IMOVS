<%@ Page Title="Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="PRUEBA_TECNICA_IMOVS.Views.Productos" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .form-container {
            border: 1px solid #e0e0e0;
            border-radius: 5px;
            padding: 20px;
            background-color: #f9f9f9;
            margin-bottom: 20px;
        }
        .btn-green {
            background-color: #28a745;
            color: white;
        }
        .btn-green:hover {
            background-color: #218838;
        }
    </style>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2>Gestión de Productos</h2>
        <p>Aquí podrás ver, agregar, editar y eliminar productos.</p>

        <div class="form-container">
            <h5>Agregar Nuevo Producto</h5>
            <asp:HiddenField ID="hfProductoId" runat="server" Value="0" />
            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" Required="true"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label for="txtTipo" class="form-label">Tipo</label>
                <asp:TextBox ID="txtTipo" runat="server" CssClass="form-control" Required="true"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label for="txtPrecioUnitario" class="form-label">Precio Unitario</label>
                <asp:TextBox ID="txtPrecioUnitario" runat="server" TextMode="Number" Step="0.01" CssClass="form-control" Required="true"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label for="txtStock" class="form-label">Stock</label>
                <asp:TextBox ID="txtStock" runat="server" TextMode="Number" Step="1" CssClass="form-control" Required="true"></asp:TextBox>
            </div>
            <div class="form-check mb-3">
                <asp:CheckBox ID="chkEstatus" runat="server" CssClass="form-check-input" Text="Activo" />
                <label class="form-check-label" for="chkEstatus">Activo</label>
            </div>
            <asp:Button ID="btnGuardarProducto" runat="server" Text="Agregar Producto" CssClass="btn btn-green" OnClick="btnGuardarProducto_Click" />
        </div>

        <div class="table-responsive">
            <asp:GridView ID="gvProductos" runat="server"
                CssClass="table table-striped table-bordered"
                AutoGenerateColumns="False" DataKeyNames="ProductoId"
                AllowPaging="True" PageSize="10" OnPageIndexChanging="gvProductos_PageIndexChanging"
                OnRowEditing="gvProductos_RowEditing" OnRowDeleting="gvProductos_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="ProductoId" HeaderText="ID" ReadOnly="True" SortExpression="ProductoId" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                    <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" />
                    <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" DataFormatString="{0:C}" SortExpression="PrecioUnitario" />
                    <asp:BoundField DataField="Stock" HeaderText="Stock" SortExpression="Stock" />
                    <asp:CheckBoxField DataField="Estatus" HeaderText="Activo" SortExpression="Estatus" />
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditar" runat="server" CommandName="Edit" CommandArgument='<%# Eval("ProductoId") %>' CssClass="btn btn-sm btn-info me-1">Editar</asp:LinkButton>
                            <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Delete" CommandArgument='<%# Eval("ProductoId") %>' CssClass="btn btn-sm btn-danger" OnClientClick="return confirm('¿Estás seguro de que quieres eliminar este producto?');">Eliminar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pagination-ys" />
            </asp:GridView>
        </div>
    </div>

    <script type="text/javascript">
        function clearProductForm() {
            document.getElementById('<%= hfProductoId.ClientID %>').value = '0';
            document.getElementById('<%= txtNombre.ClientID %>').value = '';
            document.getElementById('<%= txtTipo.ClientID %>').value = '';
            document.getElementById('<%= txtPrecioUnitario.ClientID %>').value = '';
            document.getElementById('<%= txtStock.ClientID %>').value = '';
            document.getElementById('<%= chkEstatus.ClientID %>').checked = true;
        }

        function editProduct(productId, name, type, unitPrice, stock, isActive) {
            document.getElementById('<%= hfProductoId.ClientID %>').value = productId;
            document.getElementById('<%= txtNombre.ClientID %>').value = name;
            document.getElementById('<%= txtTipo.ClientID %>').value = type;
            document.getElementById('<%= txtPrecioUnitario.ClientID %>').value = unitPrice;
            document.getElementById('<%= txtStock.ClientID %>').value = stock;
            document.getElementById('<%= chkEstatus.ClientID %>').checked = isActive;
        }
    </script>
</asp:Content>