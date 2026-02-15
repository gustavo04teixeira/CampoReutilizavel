using CampoReutilizavel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace CampoReutilizavel.Model
{
    public class ContribuinteRepository
    {
        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }

        public static List<Contribuinte> buscar(string termo)
        {
            List<Contribuinte> resultados = new List<Contribuinte>();
            if (string.IsNullOrWhiteSpace(termo)) return resultados;

            termo = termo.Trim().ToLower();
            string termoLimpo = termo.Replace(".", "").Replace("/", "").Replace("-", "");

            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                string sql = "";

                if (termoLimpo.Length == 14 && termoLimpo.All(char.IsDigit))
                {
                    sql = @"SELECT CNPJ, NomeEmpresarial FROM Contribuintes 
                            WHERE REPLACE(REPLACE(REPLACE(CNPJ, '.', ''), '/', ''), '-', '') = @termoLimpo";
                }
                else if (termoLimpo.Length == 14 && termoLimpo.All(char.IsLetterOrDigit))
                {
                    sql = @"SELECT CNPJ, NomeEmpresarial FROM Contribuintes 
                            WHERE LOWER(REPLACE(REPLACE(REPLACE(CNPJ, '.', ''), '/', ''), '-', '')) = @termoLimpo";
                }
                else
                {
                    sql = @"SELECT CNPJ, NomeEmpresarial FROM Contribuintes 
                            WHERE NomeEmpresarial LIKE '%' + @termo + '%'";
                }

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@termo", termo);
                cmd.Parameters.AddWithValue("@termoLimpo", termoLimpo);

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        resultados.Add(new Contribuinte(
                            rdr["CNPJ"].ToString(),
                            rdr["NomeEmpresarial"].ToString()
                        ));
                    }
                }
            }
            return resultados;
        }

        public static void ImportarDadosArquivo(Stream streamArquivo, string extensao)
        {
            if (extensao.ToLower() == ".xml")
            {
                DataSet ds = new DataSet();
                ds.ReadXml(streamArquivo);
                if (ds.Tables.Count > 0)
                {
                    ProcessarTabela(ds.Tables[0]);
                }
            }
            else if (extensao.ToLower() == ".json")
            {
                using (StreamReader reader = new StreamReader(streamArquivo))
                {
                    string json = reader.ReadToEnd();
                    var serializer = new JavaScriptSerializer();
                    var lista = serializer.Deserialize<List<Dictionary<string, string>>>(json);
                    ProcessarListaDicionario(lista);
                }
            }
        }

        private static void ProcessarTabela(DataTable dt)
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                con.Open();
                foreach (DataRow linha in dt.Rows)
                {
                    ExecutarInsert(linha["CNPJ"].ToString(), linha["NomeEmpresarial"].ToString(), con);
                }
            }
        }

        private static void ProcessarListaDicionario(List<Dictionary<string, string>> lista)
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                con.Open();
                foreach (var item in lista)
                {
                    ExecutarInsert(item["CNPJ"], item["NomeEmpresarial"], con);
                }
            }
        }

        private static void ExecutarInsert(string cnpj, string nome, SqlConnection con)
        {
            string sql = @"IF NOT EXISTS (SELECT 1 FROM Contribuintes WHERE CNPJ = @cnpj)
                           INSERT INTO Contribuintes (CNPJ, NomeEmpresarial) VALUES (@cnpj, @nome)";

            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@cnpj", cnpj);
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.ExecuteNonQuery();
            }
        }
    }
}