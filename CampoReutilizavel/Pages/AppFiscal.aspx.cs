using CampoReutilizavel.Model;
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
                    ContribuinteRepository.ImportarDadosArquivo(flSubirArquivo.FileContent, extensao);

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
    }
}