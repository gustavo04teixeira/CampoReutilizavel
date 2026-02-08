<%@ Page Language="C#" Title="Área do contador" AutoEventWireup="true" MasterPagefile="~/Site.Master" CodeBehind="AppContador.aspx.cs" Inherits="CampoReutilizavel._Default" %>
<%@ Register TagPrefix="uc" TagName="ListaCnpjsSelecionados" Src="~/Controls/ListaCnpjsSelecionados.ascx"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <div>
           <section class="page-container" aria-labelledby="gettingStartedTitle">
                <div class="card">

                <uc:ListaCnpjsSelecionados ID="ctrlListaCnpjsSelecionados" runat="server" />               

                </div>
            </section>
        </div>
</asp:Content>