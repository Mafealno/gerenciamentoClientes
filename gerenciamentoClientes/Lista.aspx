<%@ Page Title="Lista" Language="vb" AutoEventWireup="false" CodeBehind="Lista.aspx.vb" MasterPageFile="~/Master.Master" Inherits="gerenciamentoClientes.Lista" %>

<asp:Content ID="Content" ContentPlaceHolderID="ConteudoPrincipal" runat="server">
    <div class="container-lista w-100-p h-100-p">
        <header class="item-lista">
            <div class="col-item-id">ID</div>
            <div class="col-item-lista">Nome</div>
            <div class="col-item-lista">Sobrenome</div>
            <div class="col-item-lista">RG</div>
            <div class="col-item-lista">CPF</div>
            <div class="col-item-lista">CNPJ</div>
            <div class="col-item-lista">Telefone</div>
        </header>
        <%Response.Write(htmlListaClientes)%>
    </div>
</asp:Content>

