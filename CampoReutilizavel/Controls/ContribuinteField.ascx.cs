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

        protected void btnAdicionarLista_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["ContribuintesSelecionados"] ?? new DataTable();

            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add("CnpjContribuinte", typeof(string));
                dt.Columns.Add("NomeEmpresarial", typeof(string));
            }

            string nomeEmpresa = hfNomeEmpresa.Value;
            string cnpjDigitado = txtContribuinte.Text;

            if (string.IsNullOrEmpty(nomeEmpresa))
                nomeEmpresa = "Empresa selecionada";

            string cnpjLimpo = new string(cnpjDigitado.Where(char.IsDigit).ToArray());
            bool jaExiste = dt.AsEnumerable().Any(r =>
                new string(r["CnpjContribuinte"].ToString().Where(char.IsDigit).ToArray()) == cnpjLimpo);

            if (!jaExiste)
            {
                dt.Rows.Add(cnpjDigitado, nomeEmpresa);
                Session["ContribuintesSelecionados"] = dt;

                txtContribuinte.Text = "";
                hfNomeEmpresa.Value = "";

                lbMensagemCnpjDuplicado.Style["display"] = "none";
                btnAdicionarLista.Enabled = false;
            }
            else
            {
                txtContribuinte.Text = "";
                hfNomeEmpresa.Value = "";
                lbMensagemCnpjDuplicado.Style["display"] = "block";
                btnAdicionarLista.Enabled = false;
            }
        }

    }
}

