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

            DataTable dt = (DataTable)Session["ContribuintesSelecionados"];

            if (dt == null ||dt.Rows.Count == 0)
            {
                lbNenhumContribuinteSelecionado.Visible = true;
            }
            else
            {
                lbNenhumContribuinteSelecionado.Visible = false;
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

        protected void gvExibirCnpjs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Excluir")
            {
                int index;
                
                if (int.TryParse(e.CommandArgument.ToString(), out index))
                {
                    DataTable dt = (DataTable)Session["ContribuintesSelecionados"];

                    if (dt != null && dt.Rows.Count > index)
                    {
                        dt.Rows.RemoveAt(index);
                        Session["ContribuintesSelecionados"] = dt;

                        CarregarGrid();

                        lbNenhumContribuinteSelecionado.Visible = (dt.Rows.Count == 0);
                        lbContribuinteExcluido.Style["display"] = "block";
                    }
                }
            }
        }

        protected void gvExibirCnpjs_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt = (DataTable)Session["ContribuintesSelecionados"];
            if (dt != null)
            {
                DataView dv = new DataView(dt);

                string direcao = GetSortDirection(e.SortExpression);
                dv.Sort = e.SortExpression + " " + direcao;

                CarregarGrid();

                Session["ContribuintesSelecionados"] = dv.ToTable();
            }
        }

        private string GetSortDirection(string coluna)
        {
            string direcao = "ASC";
            string ultimaColunaOrdenada = ViewState["SortExpression"] as string;

            if (ultimaColunaOrdenada != null && ultimaColunaOrdenada == coluna)
            {
                string ultimaDirecao = ViewState["SortDirection"] as string;
                direcao = (ultimaDirecao == "ASC") ? "DESC" : "ASC";
            }

            ViewState["SortExpression"] = coluna;
            ViewState["SortDirection"] = direcao;

            return direcao;
        }
    }
}