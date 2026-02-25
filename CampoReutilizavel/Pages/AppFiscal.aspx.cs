using CampoReutilizavel.Model;
using CampoReutilizavel.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CampoReutilizavel.Pages
{
    public class ContribuinteImportacao
    {
        public string CNPJ { get; set; }
        public string NomeEmpresarial { get; set; }
    }
    public partial class SecondScreen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnSubir_Click(object sender, EventArgs e)
        {
            if (flSubirArquivo.HasFile)
            {
                try
                {
                    string extensao = System.IO.Path.GetExtension(flSubirArquivo.FileName);

                    List<Contribuinte> listaPreview = ContribuinteRepository.LerListaCnpjsDoArquivo(flSubirArquivo.FileContent, extensao);

                    gvPreview.DataSource = listaPreview;
                    gvPreview.DataBind();

                    pnlPreview.Visible = true;

                    Session["ListaImportacao"] = listaPreview;

                    lblMensagem.Text = "Importação realizada com sucesso!";
                    lblMensagem.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = "Erro ao importar: " + ex.Message;
                    lblMensagem.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblMensagem.Text = "Por favor, selecione um arquivo XML ou JSON";
                lblMensagem.ForeColor = System.Drawing.Color.Orange;
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                var lista = Session["ListaImportacao"] as List<Contribuinte>;

                if (lista != null)
                {
                    foreach (var item in lista)
                    {
                        ContribuinteRepository.SalvarIndividual(
                            item.CNPJ,
                            item.NomeEmpresarial
                        );
                    }

                    lblMensagem.Text = "Todos os contribuintes foram importados para o banco!";
                    lblMensagem.ForeColor = System.Drawing.Color.Green;
                    pnlPreview.Visible = false; 
                    Session["ListaImportacao"] = null;
                }
            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Erro ao salvar no banco: " + ex.Message;
                lblMensagem.ForeColor = System.Drawing.Color.Red;
            }
        }

    }
}