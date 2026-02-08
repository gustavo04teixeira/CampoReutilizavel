using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CampoReutilizavel.Controls
{
    public partial class ListaCnpjsSelecionados : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarGrid();
            }

            if(Session["ContribuintesSelecionados"] == null)
            {
                lbNenhumContribuinteSelecionado.Visible = true;
            }
        }
        private void CarregarGrid()
        {
            DataTable dt = (DataTable)Session["ContribuintesSelecionados"];
            if (dt != null)
            {
                gvExibirCnpjs.DataSource = dt;
                gvExibirCnpjs.DataBind();
            }
        }
    }
}