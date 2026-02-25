<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContribuinteField.ascx.cs" Inherits="CampoReutilizavel.Controls.ContribuinteField" %>

<div class="contribuinte-field">

<asp:TextBox ID="txtContribuinte" runat="server" placeholder="Digite aqui o CNPJ completo ou parte do nome da empresa" class="form-control" ClientIDMode="Static"></asp:TextBox>


<div id="listaSugestoes" class="list-group"></div>
<div id="mensagemCnpj" class="invalid-feedback"></div>

<asp:HiddenField ID="hfNomeEmpresa" runat="server" ClientIDMode="Static"/>

<select id="dpContribuintes" class="form-control" name="dpContribuintes" ClientIDMode="Static"> </select>

    <br />

    <div class="d-flex gap-2">
     
    <asp:Button ID="btnAdicionarLista" runat="server" Text="Adicionar a lista Session" CssClass="btn btn-dark" ClientIDMode="Static" OnClick="btnAdicionarLista_Click" disabled="disabled"/>
    <asp:Button ID="btnAdicionarListaVS" runat="server" Text="Adicionar a lista VS" CssClass="btn btn-dark" ClientIDMode="Static" OnClick="btnAdicionarListaVS_Click" disabled="disabled"/>

    </div> 

    <br />
    <asp:Label ID="lbMensagemCnpjDuplicado" ClientIDMode="Static" class="invalid-feedback" runat="server" style="display: none; color: red; margin-top: 5px;">Cnpj já adicionado a lista!</asp:Label>
    <asp:Label ID="lbMensagemCnpjAdicionado" ClientIDMode="Static" class="invalid-feedback" runat="server" style="display: none; color: green; margin-top: 5px;">Cnpj adicionado com sucesso na lista!</asp:Label>

</div>

<script src="/Scripts/CustomerScripts/Contribuinte-fields.js"></script>
