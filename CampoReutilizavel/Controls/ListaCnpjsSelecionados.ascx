<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListaCnpjsSelecionados.ascx.cs" Inherits="CampoReutilizavel.Controls.ListaCnpjsSelecionados"%>
<style>
    #gvExibirCnpjs th a {
        color: #333 !important;
        text-decoration: none !important;
        display: block;
        width: 100%;
        position: relative;
        padding-right: 20px;
    }

    #gvExibirCnpjs th a::after {
        content: " \21D5"; 
        position: absolute;
        right: 5px;
        color: #bbb;
        font-size: 14px;
    }

    #gvExibirCnpjs th a:hover {
        color: #007bff !important;
    }
</style>
   <div>
        <h2 id="gettingStartedTitle">Lista de empresas selecionadas</h2>

        <asp:GridView ID="gvExibirCnpjs" runat="server" class="table table-striped table-hover" AutoGenerateColumns="true" ClientIDMode="Static" 
            OnRowCommand="gvExibirCnpjs_RowCommand"
            OnSorting="gvExibirCnpjs_Sorting"
            AllowSorting="true">

            <Columns>
        <asp:TemplateField HeaderText="Ações">
            <ItemTemplate>
                <asp:Button ID="btnExcluir" runat="server" 
                    CommandName="Excluir" 
                    CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' 
                    Text="Remover" 
                    CssClass="btn btn-danger btn-sm" 
                    OnClientClick="return confirm('Deseja remover este item?');" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>

        </asp:GridView>

        
        <asp:Label ID="lbNenhumContribuinteSelecionado" ClientIDMode="Static" runat="server" Text="Nenhum Contribunte Selecionado" Visible="False"></asp:Label>
        <asp:Label ID="lbContribuinteExcluido" ClientIDMode="Static" runat="server" Text="Contribuinte Removido Com Sucesso!" Style= "display: none;"></asp:Label>

        <div class="d-flex gap-3 mt-3">

        <asp:Button ID="btnExportarXml" runat="server" Text="Exportar Arquivo XML" CssClass="btn btn-outline-dark" OnClick="btnExportarXml_Click"/>
        <asp:Button ID="btnExportarJson" runat="server" Text="Exportar Arquivo JSON" CssClass="btn btn-outline-primary" OnClick="btnExportarJson_Click" />

        </div>
   </div>



<script src="/Scripts/CustomerScripts/Contribuinte-fields.js"></script>