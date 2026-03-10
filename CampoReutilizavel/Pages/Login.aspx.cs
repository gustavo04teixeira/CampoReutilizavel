using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CampoReutilizavel.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        private static string getConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string login = txtUsername.Text;
            string senha = txtPassword.Text;

            if (CriarCadastro.cadastroExistente(login))
            {
                if(verificarSenha(login, senha) == true)
                {
                    Response.Redirect("~/Pages/App.aspx");
                }
                else
                {
                    lbMensagem.Text = "Senha e/ou e-mail incorretos. Por favor, tente novamente";
                    lbMensagem.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lbMensagem.Text = "Login não encontrado. Por favor, verifique suas credenciais.";
                lbMensagem.ForeColor = System.Drawing.Color.Red;
                return;
            }
            
        }

        protected bool verificarSenha(string login, string senha)
        {
            using (var conexao = new System.Data.SqlClient.SqlConnection(getConnectionString()))
            {
                string TermoLimpoLogin = login.Trim(); 
                string TermoLimpoSenha = senha.Trim();

                string sql = "SELECT senha FROM Cadastros WHERE email = @login";
                var comando = new System.Data.SqlClient.SqlCommand(sql, conexao);
                comando.Parameters.AddWithValue("@login", TermoLimpoLogin);

                try
                {
                    conexao.Open();

                    object resultado = comando.ExecuteScalar();

                    if(resultado != null)
                    {
                        string hashDoBanco = resultado.ToString();
                        return BCrypt.Net.BCrypt.Verify(TermoLimpoSenha, hashDoBanco);

                    }

                    return false;
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    return false;
                }
            }
        }

        protected void btnEsqueceuSenha_Click(object sender, EventArgs e)
        {
            pnAlterarSenha.Visible = true;
        }

        protected void btnAlterarSenha_Click(object sender, EventArgs e)
        {
            string login = txtConfirmarEmail.Text.Trim();
            string senha1 = txtNovaSenha.Text;
            string senha2 = txtNovaSenha1.Text;

            if(CriarCadastro.cadastroExistente(login) == true)
            {
                if(txtNovaSenha.Text == txtNovaSenha1.Text)
                {
                    using (var conexao = new System.Data.SqlClient.SqlConnection(getConnectionString()))
                    {
                        string TermoLimpoLogin = login.Trim();
                        string TermoLimpoSenha = senha1.Trim();

                        string senhaHash = BCrypt.Net.BCrypt.HashPassword(TermoLimpoSenha);
                        string sql = "UPDATE Cadastros SET senha = @senha WHERE email = @login";
                        var comando = new System.Data.SqlClient.SqlCommand(sql, conexao);
                        comando.Parameters.AddWithValue("@login", TermoLimpoLogin);
                        comando.Parameters.AddWithValue("@senha", senhaHash);
                        try
                        {
                            conexao.Open();
                            int rowsAffected = comando.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                lbMensagem.Text = "Senha alterada com sucesso!";
                                lbMensagem.ForeColor = System.Drawing.Color.Green;
                                pnAlterarSenha.Visible = false;
                            }
                            
                        }
                        catch (System.Data.SqlClient.SqlException ex)
                        {
                            lbMensagem.Text = "Erro ao conectar ao banco de dados. Por favor, tente novamente mais tarde.";
                            lbMensagem.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
                else
                {
                    lbMensagem.Text = "As senhas não coincidem. Por favor, verifique e tente novamente.";
                    lbMensagem.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lbMensagem.Text = "Email não encontrado. Por favor, verifique suas credenciais.";
                lbMensagem.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}