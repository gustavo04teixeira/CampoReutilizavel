<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContribuinteField.ascx.cs" Inherits="CampoReutilizavel.Controls.ContribuinteField" %>

<div class="contribuinte-field">
<input id="txtContribuinte" placeholder="Digite aqui o CNPJ completo ou parte do nome da empresa" type="text" 
        class="form-control"  ClientIDMode="Static"/>

<div id="listaSugestoes" class="list-group"></div>

<select id="dpContribuintes" class="form-control"> </select>

</div>

<script src="/Scripts/CustomerScripts/Contribuinte-fields.js"></script>
