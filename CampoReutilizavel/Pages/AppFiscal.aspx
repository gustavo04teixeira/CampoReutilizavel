<%@ Page Title="Área do fiscal" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AppFiscal.aspx.cs" Inherits="CampoReutilizavel.Pages.SecondScreen" %>

<%@ Register TagPrefix="uc" TagName="ListaCnpjsSelecionados" Src="~/Controls/ListaCnpjsSelecionados.ascx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="container py-5">
            <section class="page-container d-flex flex-column align-items-center" style="min-height: 70vh;">

                <div class="card p-4 shadow-sm w-100" style="max-width: 800px;">

                    <div class="text-center">
                        <uc:ListaCnpjsSelecionados ID="ctrlListaCnpjsSelecionados" runat="server" />
                    </div>

                    <asp:UpdatePanel ID="upImportacao" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Panel ID="pnlPreview" runat="server" Visible="false" CssClass="mt-4 text-center">
                                <h4 class="mb-3">Contribuintes encontrados:</h4>
                                <div class="table-responsive mb-3">
                                    <asp:GridView ID="gvPreview" runat="server" AutoGenerateColumns="False"
                                        CssClass="table table-striped table-bordered mx-auto">
                                        <Columns>
                                            <asp:BoundField DataField="CNPJ" HeaderText="CNPJ" />
                                            <asp:BoundField DataField="NomeEmpresarial" HeaderText="Nome Empresarial" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar Importação"
                                    OnClick="btnConfirmar_Click" CssClass="btn btn-success px-4" />
                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnSubir" />
                        </Triggers>
                    </asp:UpdatePanel>

                    <div class="upload-controls text-center mt-5 border-top pt-4">
                        <div class="d-inline-block text-start">
                            <label class="form-label fw-bold">Importar novo arquivo:</label>
                            <asp:FileUpload ID="flSubirArquivo" runat="server" accept=".xml,.json" CssClass="form-control mb-3" />
                        </div>
                        <br />
                        <asp:Button ID="btnSubir" runat="server" OnClick="btnSubir_Click" Text="Subir Arquivo" CssClass="btn btn-primary px-5" />
                        <br />
                        <asp:Label ID="lblMensagem" runat="server" Text="" CssClass="mt-3 d-block font-weight-bold"></asp:Label>
                    </div>

                </div>
            </section>
        </div>
    </main>
</asp:Content>
