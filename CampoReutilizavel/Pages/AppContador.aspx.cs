using CampoReutilizavel.Model;
using System;
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

        }

        protected void btnAdicionarContribuinte_Click(object sender, EventArgs e)
        {
            string cnpj = txtCnpj.Text.Trim();
            string nome = txtNomeEmpresarial.Text.Trim();

            if (!string.IsNullOrEmpty(cnpj) && !string.IsNullOrEmpty(nome))
            {
                bool inseriu = ContribuinteRepository.SalvarIndividual(cnpj, nome);

                if (inseriu)
                {
                    lblMensagem.Text = "Contribuinte cadastrado manualmente!";
                    lblMensagem.ForeColor = System.Drawing.Color.Green;

                    txtCnpj.Text = "";
                    txtNomeEmpresarial.Text = "";
                }
                else
                {
                    lblMensagem.Text = "CNPJ já cadastrado!";
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