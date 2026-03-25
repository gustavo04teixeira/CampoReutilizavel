<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CampoReutilizavel.Pages.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>

   <style>
        /* Estilo para garantir que o fundo ocupe a tela toda e centralize o conteúdo */
        html, body { 
            height: 100%; 
        }
        body { 
            background-color: #f8f9fa; 
            display: flex;
            align-items: center; /* Centraliza verticalmente */
            justify-content: center; /* Centraliza horizontalmente */
        }
        form {
            width: 100%;
        }
        .login-container { 
            max-width: 400px; 
            margin: auto; 
            padding: 30px; 
            background: #fff; 
            border-radius: 4px; 
            box-shadow: 0 2px 10px rgba(0,0,0,0.1); 
            border-top: 4px solid #004a80; 
        }
        .btn-gov { background-color: #004a80; color: white; border: none; }
        .btn-gov:hover { background-color: #00335a; color: white; }
        .label-gov { font-weight: 600; color: #333; font-size: 0.9rem; }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="login-container">
                <h2 class="text-center mb-4" style="color: #004a80; font-weight: bold;">Login</h2>
                
                <div class="mb-3">
                    <asp:Label ID="lblUsername" runat="server" CssClass="label-gov" Text="E-mail:"></asp:Label>
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="exemplo@email.com"></asp:TextBox>
                </div>
                <br />
                <div class="mb-3">
                    <asp:Label ID="lblPassword" runat="server" CssClass="label-gov" Text="Senha:"></asp:Label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                </div>
                <br />
                <div class="d-grid gap-2 mt-4">
                    <asp:Button ID="btnLogin" runat="server" Text="Entrar" CssClass="btn btn-gov shadow-sm" OnClick="btnLogin_Click" />
                </div>
                <br />
                <div class="text-center mt-3">
                    <asp:LinkButton ID="btnEsqueceuSenha" runat="server" CssClass="text-decoration-none small" OnClick="btnEsqueceuSenha_Click">Esqueceu a senha?</asp:LinkButton>
                    <hr />
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Pages/CriarCadastro.aspx" CssClass="btn btn-outline-secondary btn-sm w-100">Criar Nova Conta</asp:HyperLink>
                </div>

                <%-- Painel de Alteração de Senha --%>
                <asp:Panel Visible="false" runat="server" ID="pnAlterarSenha" CssClass="mt-4 p-3 border rounded bg-light">
                    <h4 class="mb-3 text-secondary text-center">Recuperar Senha</h4>
                    
                    <div class="mb-2">
                        <asp:TextBox ID="txtConfirmarEmail" runat="server" CssClass="form-control form-control-sm" placeholder="Confirme seu e-mail"></asp:TextBox>
                        <asp:Button ID="btnEnviarCodigo" runat="server" OnClick="btnEnviarCodigo_Click" Text="Enviar código" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtConfirmarEmail" Display="Dynamic" ForeColor="Red" Font-Size="XX-Small">Campo Obrigatório!</asp:RequiredFieldValidator>
                        <br />
                        <asp:TextBox ID="txtCodigoVerificacao" runat="server" placeholder="Código de verificação" TextMode="Number"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ForeColor="Red" Font-Size="XX-Small" ControlToValidate="txtCodigoVerificacao">Campo Obrigatório!</asp:RequiredFieldValidator>
                    </div>
                    <div class="mb-2">
                        <asp:TextBox ID="txtNovaSenha" runat="server" CssClass="form-control form-control-sm" TextMode="Password" placeholder="Nova senha"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNovaSenha" Display="Dynamic" ForeColor="Red" Font-Size="XX-Small">Campo Obrigatório!</asp:RequiredFieldValidator>
                    </div>
                    <div class="mb-3">
                        <asp:TextBox ID="txtNovaSenha1" runat="server" CssClass="form-control form-control-sm" TextMode="Password" placeholder="Confirme a senha"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNovaSenha1" Display="Dynamic" ForeColor="Red" Font-Size="XX-Small">Campo Obrigatório!</asp:RequiredFieldValidator>
                    </div>
                    <br />
                    <asp:Button ID="btnAlterarSenha" runat="server" Text="Atualizar Senha" CssClass="btn btn-success btn-sm w-100" OnClick="btnAlterarSenha_Click" />
                </asp:Panel>

                <div class="mt-3 text-center">
                    <br />
                    <asp:Label ID="lbMensagem" runat="server" CssClass="small fw-bold text-danger" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
