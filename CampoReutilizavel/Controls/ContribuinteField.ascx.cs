using CampoReutilizavel.Model;
using CampoReutilizavel.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CampoReutilizavel.Controls
{
    public partial class ContribuinteField : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Armazenamento da lista de contribuinte na Session
        protected void btnAdicionarLista_Click(object sender, EventArgs e)
        {
            AdicionarLista("Session");

        }

        protected void btnAdicionarListaVS_Click(object sender, EventArgs e)
        {
            AdicionarLista("ViewState");
        }

        protected void AdicionarLista(string tipoArmazenamento)
        {
            DataTable dt;

            if (tipoArmazenamento == "Session")
                dt = (DataTable)Session["ContribuintesSelecionados"] ?? new DataTable();
            else
                dt = (DataTable)ViewState["ContribuintesSelecionados"] ?? new DataTable();

            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add("CNPJ", typeof(string)); 
                dt.Columns.Add("NomeEmpresarial", typeof(string));
            }

            string nomeEmpresa = hfNomeEmpresa.Value;
            string cnpjDigitado = txtContribuinte.Text;
            string cnpjLimpo = new string(cnpjDigitado.Where(char.IsDigit).ToArray());

            bool jaExiste = dt.AsEnumerable().Any(r =>
                new string(r["CNPJ"].ToString().Where(char.IsDigit).ToArray()) == cnpjLimpo);

            if (!jaExiste)
            {
                dt.Rows.Add(cnpjDigitado, nomeEmpresa);

                if (tipoArmazenamento == "Session")
                {
                    Session["ContribuintesSelecionados"] = dt;
                }
                else
                {
                    ViewState["ContribuintesSelecionados"] = dt;
                   
                }
                RegistrarCnpjNoCookiePermanente(cnpjLimpo);

                txtContribuinte.Text = "";
                hfNomeEmpresa.Value = "";
            }
        }

        private void RegistrarCnpjNoCookiePermanente(string cnpj)
        {
            HttpCookie cookie = Request.Cookies["CnpjsValidados"] ?? new HttpCookie("CnpjsValidados");

            List<string> lista = string.IsNullOrEmpty(cookie.Value)
                ? new List<string>()
                : cookie.Value.Split(',').ToList();

            if (!lista.Contains(cnpj))
            {
                lista.Add(cnpj);
                cookie.Value = string.Join(",", lista);
                cookie.Expires = DateTime.Now.AddDays(7);
                cookie.HttpOnly = false; 
                Response.Cookies.Add(cookie);
            }
        }

    }

}

