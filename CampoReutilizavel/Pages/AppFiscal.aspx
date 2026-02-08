<%@ Page Title="Área do fiscal" Language="C#" MasterPagefile="~/Site.Master" AutoEventWireUp="true" CodeBehind="AppFiscal.aspx.cs" Inherits="CampoReutilizavel.Pages.SecondScreen" %>
<%@ Register TagPrefix="uc" TagName="ListaCnpjsSelecionados" Src="~/Controls/ListaCnpjsSelecionados.ascx"%>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
           <section class="page-container" aria-labelledby="gettingStartedTitle">
                <div class="card">
                
                <uc:ListaCnpjsSelecionados ID="ctrlListaCnpjsSelecionados" runat="server" />
            </section>
        </div>
    </main>
</asp:Content>
