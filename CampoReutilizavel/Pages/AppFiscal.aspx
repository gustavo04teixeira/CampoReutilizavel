<%@ Page Title="Área do fiscal" Language="C#" MasterPagefile="~/Site.Master" AutoEventWireUp="true" CodeBehind="AppFiscal.aspx.cs" Inherits="CampoReutilizavel.Pages.SecondScreen" %>
<%@ Register TagPrefix="uc" TagName="ListaCnpjsSelecionados" Src="~/Controls/ListaCnpjsSelecionados.ascx"%>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
           <section class="page-container" aria-labelledby="gettingStartedTitle">
                <div class="card">
                
                <uc:ListaCnpjsSelecionados ID="ctrlListaCnpjsSelecionados" runat="server" />
                    <br />
                    <br />
                    <asp:FileUpload ID="flSubirArquivo" runat="server" accept=".xml,.json"/>
                    <br />
                    <br />
                    <asp:Button ID="btnSubir" runat="server" OnClick="btnSubir_Click" Text="Subir" />
                    <br />
                    <asp:Label ID="lblMensagem" runat="server" Text=""></asp:Label>
            </section>
        </div>
    </main>
</asp:Content>
