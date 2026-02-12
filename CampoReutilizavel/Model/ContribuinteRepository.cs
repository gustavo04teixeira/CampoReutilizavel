using CampoReutilizavel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CampoReutilizavel.Model
{
    public class ContribuinteRepository
    {
        public static List<Contribuinte> buscar(string termo)
        {
            List<Contribuinte> resultados = new List<Contribuinte>();
            if (string.IsNullOrWhiteSpace(termo)) return resultados;

            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            termo = termo.Trim().ToLower(); 

            string termoLimpo = termo.Replace(".", "").Replace("/", "").Replace("-", "");

            using (SqlConnection con = new SqlConnection(constr))
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


    }
}