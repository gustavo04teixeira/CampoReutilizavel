using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BCrypt.Net;
using CampoReutilizavel.DAL;

namespace CampoReutilizavel.Pages
{
    public partial class CriarCadastro : System.Web.UI.Page
    {
        private static string getConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCriarCadastro_Click(object sender, EventArgs e)
        {
            string login = txtEmail.Text.Trim();
            string senha1 = txtSenha.Text.Trim();
            string senha2 = txtConfirmarSenha.Text.Trim();


            if (UsuarioDAL.cadastroExistente(login) == true)
            {
                lblMensagem.Text = "Login já existe. Por favor, escolha outro.";
                lblMensagem.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (senha1 == senha2)
            {
                using (var conexao = new System.Data.SqlClient.SqlConnection(getConnectionString()))
                {
                    string senhaHash = BCrypt.Net.BCrypt.HashPassword(senha1);

                    string sql = "INSERT INTO Cadastros (email,senha) VALUES (@login, @senha)";

                    var comando = new System.Data.SqlClient.SqlCommand(sql, conexao);

                    comando.Parameters.AddWithValue("@login", login);
                    comando.Parameters.AddWithValue("@senha", senhaHash);
                    try
                    {
                        conexao.Open();
                        comando.ExecuteNonQuery();

                        lblMensagem.Text = "Login criado com sucesso!";
                        lblMensagem.ForeColor = System.Drawing.Color.Green;

                        txtEmail.Text = "";
                        txtSenha.Text = "";
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        lblMensagem.Text = "Erro ao criar login: " + ex.Message;
                        lblMensagem.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            else
            {
                lblMensagem.Text = "As senhas não coincidem, tente novamente!";
            }
        }

    }
}