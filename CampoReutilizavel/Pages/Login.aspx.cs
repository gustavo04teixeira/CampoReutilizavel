using CampoReutilizavel.DAL;
using CampoReutilizavel.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace CampoReutilizavel.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string login = txtUsername.Text.Trim();
            string senha = txtPassword.Text.Trim();

            if (UsuarioDAL.cadastroExistente(login))
            {
                if (UsuarioDAL.verificarSenha(login, senha))
                {
                    Response.Redirect("~/Pages/App.aspx");
                }
                else
                {
                    mensagemErro("Senha e/ou e-mail incorretos. Por favor, tente novamente");
                }
            }
            else
            {
                mensagemErro("Senha e/ou e-mail incorretos. Por favor, tente novamente");
            }

        }

        protected void btnEsqueceuSenha_Click(object sender, EventArgs e)
        {
            pnAlterarSenha.Visible = true;
        }

        protected void btnAlterarSenha_Click(object sender, EventArgs e)
        {
            string login = txtConfirmarEmail.Text.Trim();
            string senha = txtNovaSenha.Text.Trim();
            int codigoVerificacao = int.Parse(txtCodigoVerificacao.Text.Trim());

            if (senha != txtNovaSenha1.Text)
            {
                mensagemErro("As senhas digitadas não coincidem. Por favor, verifique e tente novamente.");
                return;
            }

            var UsuarioService = new UsuarioService();

            var resultadoCodigo = UsuarioService.obterCodigoVerificacao(login, codigoVerificacao);

            if(resultadoCodigo.Sucesso)
            {
                mensagemSucesso(resultadoCodigo.Mensagem);
                pnAlterarSenha.Visible = false;
            }
            else
            {
                mensagemErro(resultadoCodigo.Mensagem);
            }

            if (UsuarioDAL.cadastroExistente(login) == true)
            {
                try
                {
                    UsuarioDAL.alterarSenha(login, senha);

                    mensagemSucesso("Senha alterada com sucesso! Acesse com a nova senha.");
                    pnAlterarSenha.Visible = false;
                }
                catch (Exception ex)
                {
                    mensagemErro("Erro ao conectar ao banco de dados.Por favor, tente novamente mais tarde.");
                }
            }
        }

        protected void btnEnviarCodigo_Click(object sender, EventArgs e)
        {
            string email = txtConfirmarEmail.Text.Trim();
            Random random = new Random();
            int codigo = random.Next(10000, 99999);

            UsuarioDAL.alterarCodigoVerificacao(email, codigo);

            enviarEmail(email, codigo);
        }

        protected void enviarEmail(string email, int codigo)
        {

            var mensagem = new MimeMessage();
            mensagem.From.Add(new MailboxAddress("Sistema Campo Reutilizável", "gtestrategiadigital@gmail.com"));
            mensagem.To.Add(new MailboxAddress("", email));
            mensagem.Subject = "Código de Recuperação de Senha";

            mensagem.Body = new TextPart("plain")
            {
                Text = $"Olá,\n\nSeu código de recuperação de senha é: {codigo}\n\nSe você não solicitou essa alteração, por favor ignore este email."
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    client.Authenticate("gtestrategiadigital@gmail.com", "mfrr esws ybsm wxyo\r\n");

                    client.Send(mensagem);
                    client.Disconnect(true);

                    mensagemSucesso("Código de recuperação enviado para o email fornecido.");
                }
                catch (Exception ex)
                {
                    mensagemErro("Erro ao enviar email. Por favor, tente novamente mais tarde.");
                }
            }
        }

        protected void mensagemErro(string mensagem)
        {
            lbMensagem.Text = mensagem;
            lbMensagem.ForeColor = System.Drawing.Color.Red;
        }

        protected void mensagemSucesso(string mensagem)
        {
            lbMensagem.Text = mensagem;
            lbMensagem.ForeColor = System.Drawing.Color.Green;
        }


    }
}