<%@ Page Title="Reutilização do Campo" Language="C#" MasterPagefile="~/Site.Master" AutoEventWireUp="true" CodeBehind="SecondScreen.aspx.cs" Inherits="CampoReutilizavel.Pages.SecondScreen" %>
<%@ Register TagPrefix="uc" TagName="ContribuinteField" Src="~/Controls/ContribuinteField.ascx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
           <section class="page-container" aria-labelledby="gettingStartedTitle">
                <div class="card">
                <h2 id="gettingStartedTitle">Verificação de Contribuinte Premium</h2>

                <uc:ContribuinteField ID="ctrlContribuinte" runat="server" />
                </div>
            </section>
        </div>
    </main>
</asp:Content>
