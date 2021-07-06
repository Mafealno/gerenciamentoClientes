<%@ Page Title="Formulario" Async="true" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="Default.aspx.vb" Inherits="gerenciamentoClientes.Defaut" %>

<asp:Content ID="Content" ContentPlaceHolderID="ConteudoPrincipal" runat="server">
    <form id="formCliente" runat="server">
        <div class="container-form-cliente h-100-p w-100-p">
            <div class="row mb-1">
                <div class="col-2 text-center">
                    <asp:Label ID="lblIdCliente" runat="server" Text="Id"></asp:Label>
                </div>
                <div class="col-2">
                    <asp:TextBox ID="txtIdCliente" runat="server" CssClass="w-100-p form-control"></asp:TextBox>
                </div>
                <div class="col-1">
                    <asp:Button ID="btnBuscarCliente" runat="server" Text="Buscar" CssClass="btn btn-primary"/>
                </div>
                <div class="col">
                    <%Response.Write(htmlMostrarIni)%>
                        <asp:Button ID="btnLimpar" runat="server" Text="Limpar" CssClass="btn btn-danger"/>
                    <%Response.Write(htmlMostrarFim)%>
                </div>
            </div>
            <div class="row mb-1">
                <div class="col-2 text-center">
                    <asp:Label ID="lblNomeCLiente" runat="server" Text="Nome *"></asp:Label>
                </div>
                <div class="col">
                    <asp:TextBox ID="txtNomeCliente" runat="server" CssClass="w-100-p form-control"></asp:TextBox>
                </div>
                <div class="col-2 text-center">
                    <asp:Label ID="lblSobrenomeCliente" runat="server" Text="Sobrenome *"></asp:Label>
                </div>
                <div class="col">
                    <asp:TextBox ID="txtSobrenomeCliente" runat="server" CssClass="w-100-p form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row mb-1">
                <div class="col-2 text-center">
                    <asp:Label ID="lblTipoCliente" runat="server" Text="Tipo de cliente *"></asp:Label>
                </div>
                <div class="col">
                    <asp:DropDownList ID="ddlTipoCliente" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged">
                        <asp:ListItem Value="F">Física</asp:ListItem>
                        <asp:ListItem Value="J">Juridica</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row mb-4">
                 <div class="col-2 text-center">
                    <asp:Label ID="lblRgCliente" runat="server" Text="RG"></asp:Label>
                </div>
                <div class="col">
                    <asp:TextBox ID="txtRgCliente" runat="server" CssClass="w-100-p form-control"></asp:TextBox>
                </div>
                <div class="col-2 text-center">
                    <asp:Label ID="lblCpfCliente" runat="server" Text="CPF"></asp:Label>
                </div>
                <div class="col">
                    <asp:TextBox ID="txtCpfCliente" runat="server" CssClass="w-100-p form-control"></asp:TextBox>
                </div>
                <div class="col-2 text-center">
                    <asp:Label ID="lblCnpjCliente" runat="server" Text="CNPJ"></asp:Label>
                </div>
                <div class="col">
                    <asp:TextBox ID="txtCnpjCliente" runat="server" CssClass="w-100-p form-control"></asp:TextBox>
                </div>
             </div>
            <div class="row mb-1">
                <div class="col-2 text-center">
                    <asp:Label ID="lblTipoTelefone" runat="server" Text="Tipo de telefone"></asp:Label>
                </div>
                <div class="col">
                    <asp:DropDownList ID="ddlTipoTelefone" runat="server" CssClass="form-select" AutoPostBack="true">
                        <asp:ListItem Value="N">Não Selecionado</asp:ListItem>
                        <asp:ListItem Value="T">Telefone</asp:ListItem>
                        <asp:ListItem Value="C">Celular</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row mb-4">
                <div class="col-2 text-center">
                    <asp:Label ID="lblDddTelefone" runat="server" Text="DDD *"></asp:Label>
                </div>
                <div class="col-2">
                    <asp:TextBox ID="txtDddTelefone" runat="server" CssClass="w-100-p form-control"></asp:TextBox>
                </div>
                <div class="col-2 text-center">
                    <asp:Label ID="lblNumeroTelefone" runat="server" Text="Número *"></asp:Label>
                </div>
                <div class="col">
                    <asp:TextBox ID="txtNumeroTelefone" runat="server" CssClass="w-100-p form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row mb-1">
                <div class="col-2 text-center">
                    <asp:Label ID="lblCepEndereco" runat="server" Text="CEP"></asp:Label>
                </div>
                <div class="col-2">
                    <asp:TextBox ID="txtCepEndereco" runat="server" CssClass="w-100-p form-control"></asp:TextBox>
                </div>
                <div class="col-4">
                    <asp:Button ID="btnBuscarCep" runat="server" Text="Buscar" class="btn btn-primary"/>
                </div>
            </div>
            <div class="row mb-1">
                <div class="col-2 text-center">
                    <asp:Label ID="lblLogradouroEndereco" runat="server" Text="Logradouro *"></asp:Label>
                </div>
                <div class="col">
                    <asp:TextBox ID="txtLogradouroEndereco" runat="server" CssClass="w-100-p form-control" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-2 text-center">
                    <asp:Label ID="lblNumeroEndereco" runat="server" Text="Número"></asp:Label>
                </div>
                <div class="col-2">
                    <asp:TextBox ID="txtNumeroEndereco" runat="server" CssClass="w-100-p form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row mb-1">
                <div class="col-2 text-center">
                    <asp:Label ID="lblComplementoEndereco" runat="server" Text="Complemento"></asp:Label>
                </div>
                <div class="col">
                    <asp:TextBox ID="txtComplementoEndereco" runat="server" CssClass="w-100-p form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row mb-4">
                <div class="col-2 text-center">
                    <asp:Label ID="lblBairroEndereco" runat="server" Text="Bairro *"></asp:Label>
                </div>
                <div class="col">
                    <asp:TextBox ID="txtBairroEndereco" runat="server" CssClass="w-100-p form-control" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-2 text-center">
                    <asp:Label ID="lblCidadeEndereco" runat="server" Text="Cidade *"></asp:Label>
                </div>
                <div class="col">
                    <asp:TextBox ID="txtCidadeEndereco" runat="server" CssClass="w-100-p form-control" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-1 text-center">
                    <asp:Label ID="lblUfEndereco" runat="server" Text="UF *"></asp:Label>
                </div>
                <div class="col-1">
                    <asp:TextBox ID="txtUfEndereco" runat="server" CssClass="w-100-p form-control" Enabled="false"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="btn btn-success w-100-p"/>
                </div>
                <div class="col">
                    <asp:Button ID="btnAtualizar" runat="server" Text="Atualizar" CssClass="btn btn-warning w-100-p text-white"/>
                </div>
                <div class="col">
                    <asp:Button ID="btnDeletar" runat="server" Text="Deletar" CssClass="btn btn-danger w-100-p"/>
                </div>
            </div>
        </div>
    </form>
    <div class="container w-100-p">

    </div>
</asp:Content>
