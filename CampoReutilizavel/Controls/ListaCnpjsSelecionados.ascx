<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListaCnpjsSelecionados.ascx.cs" Inherits="CampoReutilizavel.Controls.ListaCnpjsSelecionados" %>

   <div>
        <h2 id="gettingStartedTitle">Lista de empresas selecionadas</h2>

        <asp:GridView ID="gvExibirCnpjs" runat="server" class="table table-striped table-hover" AutoGenerateColumns="true" ClientIDMode="Static"></asp:GridView>

        
        <asp:Label ID="lbNenhumContribuinteSelecionado" runat="server" Text="Nenhum Contribunte Selecionado" Visible="False"></asp:Label>

        
   </div>

<script src="/Scripts/CustomerScripts/Contribuinte-fields.js"></script>
