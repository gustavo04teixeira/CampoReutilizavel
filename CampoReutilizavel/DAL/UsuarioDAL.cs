using CampoReutilizavel.Model;
using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace CampoReutilizavel.DAL
{
    public class UsuarioDAL
    {
        private static readonly string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public static bool verificarSenha(string login, string senha)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                string TermoLimpoLogin = login.Trim();
                string TermoLimpoSenha = senha.Trim();

                string sql = "SELECT senha FROM Cadastros WHERE email = @login";
                var comando = new SqlCommand(sql, conexao);
                comando.Parameters.AddWithValue("@login", TermoLimpoLogin);

                try
                {
                    conexao.Open();

                    object resultado = comando.ExecuteScalar();

                    if (resultado != null)
                    {
                        string hashDoBanco = resultado.ToString();
                        return BCrypt.Net.BCrypt.Verify(TermoLimpoSenha, hashDoBanco);

                    }

                    return false;
                }
                catch (SqlException ex)
                {
                    return false;
                }
            }
        }

        public static void alterarCodigoVerificacao(string email, int codigo)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {

                string sql = "UPDATE Cadastros SET codigoVerificacao = @codigo WHERE email = @login";
                var comando = new System.Data.SqlClient.SqlCommand(sql, conexao);
                comando.Parameters.AddWithValue("@login", email);
                comando.Parameters.AddWithValue("@codigo", codigo.ToString());
                try
                {
                    conexao.Open();
                    comando.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception();
                }

            }
        }

        public static void alterarSenha(string login, string senha)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                string TermoLimpoLogin = login.Trim();
                string TermoLimpoSenha = senha.Trim();

                string senhaHash = BCrypt.Net.BCrypt.HashPassword(TermoLimpoSenha);
                string sqlSenha = "UPDATE Cadastros SET senha = @senha WHERE email = @login";
                var comandoSenha = new SqlCommand(sqlSenha, conexao);
                comandoSenha.Parameters.AddWithValue("@login", TermoLimpoLogin);
                comandoSenha.Parameters.AddWithValue("@senha", senhaHash);

                conexao.Open();

                int rows = comandoSenha.ExecuteNonQuery();

                if (rows == 0) throw new Exception("Nenhum registro atualizado. Verifique se o login existe.");
            }
        }

        public static int obterCodigoVerificacao(string login)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                var sql = "SELECT codigoVerificacao FROM Cadastros WHERE email  = @Login";
                var comando = new SqlCommand(sql, conexao);
                comando.Parameters.AddWithValue("@Login", login.Trim());

                conexao.Open();
                var resultado = comando.ExecuteScalar();

                if (resultado == null || resultado == DBNull.Value)
                {
                    throw new Exception("Código de verificação não encontrado para o login fornecido.");
                }

                return Convert.ToInt32(resultado);
            }
        }

        public static bool cadastroExistente(string login)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {

                string termoLimpo = login.Trim();

                string sql = "SELECT COUNT(*) FROM Cadastros WHERE email = @login";

                var comando = new SqlCommand(sql, conexao);

                comando.Parameters.AddWithValue("@login", termoLimpo);

                try
                {
                    conexao.Open();

                    int count = (int)comando.ExecuteScalar();

                    return count > 0;
                }
                catch (SqlException ex)
                {

                    return false;
                }
            }
        }
    }
}