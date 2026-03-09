using CampoReutilizavel.Model;
using CampoReutilizavel.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CampoReutilizavel.Pages
{
    public partial class AppContador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lbMensagemGridView.Text = "";
                lblMensagem.Text = "";
                txtCnpj.Text = string.Empty;
                txtNomeEmpresarial.Text = string.Empty;
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {


            try
            {
                lbMensagemGridView.Text = "";

                string cnpj = gvPreview.Rows[0].Cells[0].Text.Replace(".", "").Replace("-", "").Replace("/", "");

                string nomeEmpresarial = gvPreview.Rows[0].Cells[1].Text;

                bool inseriu = ContribuinteRepository.SalvarIndividual(cnpj, nomeEmpresarial);

                if (inseriu)
                {
                    lbMensagemGridView.Text = "Contribuinte cadastrado manualmente!";
                    lbMensagemGridView.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lbMensagemGridView.Text = "CNPJ já cadastrado!";
                    lbMensagemGridView.ForeColor = System.Drawing.Color.Red;
                }

                Session["ListaImportacao"] = null;
                gvPreview.DataSource = null;
                gvPreview.DataBind();
                pnlPreview.Visible = false;

            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Erro ao salvar: " + ex.Message;
                lblMensagem.ForeColor = System.Drawing.Color.Red;
            }

        }

        protected void btnSubir_Click(object sender, EventArgs e)
        {
            string cnpj = txtCnpj.Text.Trim();
            string nome = txtNomeEmpresarial.Text.Trim();
            lbMensagemGridView.Text = "";

            if (gvPreview.Rows.Count >= 1)
            {
                lblMensagem.Text = "Apenas um contribuinte por vez pode ser adicionado manualmente!";
                lblMensagem.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (!string.IsNullOrEmpty(cnpj) && !string.IsNullOrEmpty(nome))
            {
                string cnpjLimpo = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

                if (cnpjLimpo.Length == 14)
                {
                    List<Contribuinte> lista = (List<Contribuinte>)Session["ListaImportacao"] ?? new List<Contribuinte>();

                    lista.Add(new Contribuinte { CNPJ = cnpjLimpo, NomeEmpresarial = txtNomeEmpresarial.Text.Trim() });

                    Session["ListaImportacao"] = lista;

                    gvPreview.DataSource = lista;
                    gvPreview.DataBind();

                    pnlPreview.Visible = true;

                    txtCnpj.Text = string.Empty;
                    txtNomeEmpresarial.Text = string.Empty;
                }
                else
                {
                    lblMensagem.Text = "Campo CNPJ precisa ter 14 caracteres!";
                    lblMensagem.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblMensagem.Text = "Preencha ambos os campos!";
                lblMensagem.ForeColor = System.Drawing.Color.Red;
            }
        }


    }
}