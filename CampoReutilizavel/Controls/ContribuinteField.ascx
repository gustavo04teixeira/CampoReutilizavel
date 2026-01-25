<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContribuinteField.ascx.cs" Inherits="CampoReutilizavel.Controls.ContribuinteField" %>
<p>
    <input id="txtContribuinte" placeholder="Digite aqui o CNPJ completo ou parte do nome da empresa" type="text" 
        CssClass="form-control"  ClientIDMode="Static"/></p>

<div id="listaSugestoes" class="list-group"></div>

<select id="dpContribuintes" class="form-control">

</select>

<script src="/Scripts/CustomerScripts/Contribuinte-fields.js"></script>
