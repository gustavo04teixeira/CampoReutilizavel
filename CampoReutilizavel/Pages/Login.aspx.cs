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

                string sql = "SELECT COUNT(*) FROM Cadastros WHERE email = @login AND senha = @senha";

                var comando = new System.Data.SqlClient.SqlCommand(sql, conexao);

                comando.Parameters.AddWithValue("@login", TermoLimpoLogin);
                comando.Parameters.AddWithValue("@senha", TermoLimpoSenha);

                try
                {
                    conexao.Open();

                    int count = (int)comando.ExecuteScalar();
                    return count > 0;
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    return false;
                }
            }
        }
    }
}