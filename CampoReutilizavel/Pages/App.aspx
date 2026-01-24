<%@ Page Title="Campo Reutilizável" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="App.aspx.cs" Inherits="CampoReutilizavel._Default" %>
<%@ Register TagPrefix="uc" TagName="ContribuinteField" Src="~/Controls/ContribuinteField.ascx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
           <section class="page-container" aria-labelledby="gettingStartedTitle">
                <div class="card">
                <h2 id="gettingStartedTitle">Consulta de Contribuinte</h2>

                <uc:ContribuinteField ID="ctrlContribuinte" runat="server" />
                </div>
            </section>
        </div>
    </main>
</asp:Content>
