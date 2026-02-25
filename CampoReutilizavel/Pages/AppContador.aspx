<%@ Page Language="C#" Title="Área do contador" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="AppContador.aspx.cs" Inherits="CampoReutilizavel.Pages.AppContador" EnableEventValidation="false" %>

<%@ Register TagPrefix="uc" TagName="ListaCnpjsSelecionados" Src="~/Controls/ListaCnpjsSelecionados.ascx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .page-wrapper {
            display: flex;
            flex-direction: column;
            align-items: center;
            padding-top: 40px;
            gap: 20px; 
        }

        .custom-card {
            width: 100%;
            max-width: 800px;
        }

        .cadastro-retratil {
            max-height: 0;
            overflow: hidden;
            transition: all 0.4s ease-in-out;
            width: 100%;
            max-width: 800px;
            opacity: 0;
        }

        .cadastro-retratil.show {
            max-height: 400px;
            opacity: 1;
            margin-top: 10px;
        }
    </style>

    <div class="page-wrapper">
        <section class="custom-card shadow-sm border rounded bg-white p-4">
             <uc:ListaCnpjsSelecionados ID="ctrlListaCnpjsSelecionados" runat="server" />
        </section>

        <div>
            <button type="button" class="btn btn-outline-primary" onclick="toggleCadastroManual()">
                <i class="fas fa-plus"></i> Inserir novos cadastros
            </button>
        </div>

        <div id="painelCadastroManual" class="cadastro-retratil">
            <div class="card card-body bg-light shadow-sm">
                <h5 class="mb-3 text-secondary">Novo Contribuinte</h5>
                <div class="row g-3">
                    <div class="col-md-5">
                        <asp:TextBox ID="txtCnpj" runat="server" CssClass="form-control" placeholder="Digite o CNPJ"></asp:TextBox>
                    </div>
                    <div class="col-md-5">
                        <asp:TextBox ID="txtNomeEmpresarial" runat="server" CssClass="form-control" placeholder="Nome Empresarial"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnAdicionarContribuinte" runat="server" Text="Salvar" 
                            CssClass="btn btn-success w-100" OnClick="btnAdicionarContribuinte_Click"/>
                    </div>
                </div>
                <div class="mt-2 text-center">
                    <asp:Label ID="lblMensagem" runat="server" Text="" Font-Bold="true"></asp:Label>
                </div>
            </div>
        </div>
    </div>

    <script>
        function toggleCadastroManual() {
            var element = document.getElementById("painelCadastroManual");
            element.classList.toggle("show");
        }
    </script>
</asp:Content>