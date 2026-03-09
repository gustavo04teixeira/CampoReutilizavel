<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CriarCadastro.aspx.cs" Inherits="CampoReutilizavel.Pages.CriarCadastro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Criar Cadastro</h2>
            <asp:Label ID="lblEmail" runat="server" Text="Email: "></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="Campo E-mail está vazio!" ForeColor="Red"></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="lblSenha" runat="server" Text="Senha: "></asp:Label>
            <asp:TextBox ID="txtSenha" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSenha" ErrorMessage="Campo Senha está vazio!" ForeColor="Red"></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="lblConfirmarSenha" runat="server" Text="Confirmar Senha: "></asp:Label>
            <asp:TextBox ID="txtConfirmarSenha" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtConfirmarSenha" ErrorMessage="Campo Confirmação de Senha está vazio!" ForeColor="Red"></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="lblMensagem" runat="server" />
            <br />
            <br />
            <asp:Button ID="btnCriarCadastro" runat="server" Text="Criar Cadastro" OnClick="btnCriarCadastro_Click" />
        </div>
    </form>
</body>
</html>
