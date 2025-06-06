<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="PRUEBA_TECNICA_IMOVS.Index" MasterPageFile="~/Site.Master" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="p-5 mb-4 bg-light rounded-3"> 
        <div class="container-fluid py-5">
            <h1 class="display-5 fw-bold">PRUEBA TÉCNICA, COTIZACIÓN DE COMPRA DE PRODUCTOS</h1>
            <p class="col-md-8 fs-4">Bienvenido al sistema de gestión de productos y cotizaciones. Utiliza los enlaces a continuación o el menú de navegación para comenzar.</p>
            <p>
                <a href="/Productos.aspx" class="btn btn-primary btn-lg me-2">Gestionar Productos &raquo;</a>
                <a href="/Cotizaciones.aspx" class="btn btn-success btn-lg">Crear y Gestionar Cotizaciones &raquo;</a>
            </p>
        </div>
    </div>

    <div class="row">
        <div class="col-md-6 mb-3">
            <div class="card">
                <div class="card-body">
                    <h5 class="card-title">Gestión de Productos</h5>
                    <p class="card-text">Administra tus productos, precios, stock y estado (activo/inactivo) para que estén disponibles en tus cotizaciones.</p>
                    <a href="/Productos.aspx" class="btn btn-outline-secondary">Ir a Productos &raquo;</a>
                </div>
            </div>
        </div>
        <div class="col-md-6 mb-3">
            <div class="card">
                <div class="card-body">
                    <h5 class="card-title">Creación de Cotizaciones</h5>
                    <p class="card-text">Genera cotizaciones para clientes, selecciona productos, gestiona cantidades, calcula totales e IVA, y confirma ventas.</p>
                    <a href="/Cotizaciones.aspx" class="btn btn-outline-secondary">Ir a Cotizaciones &raquo;</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>