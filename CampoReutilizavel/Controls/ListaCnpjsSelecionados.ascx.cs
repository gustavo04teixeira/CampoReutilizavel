using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

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

        public class ContribuinteExport
        {
            public string CNPJ { get; set; }
            public string NomeEmpresarial { get; set; }
        }
        protected void btnExportarJson_Click(object sender, EventArgs e)
        {
            var lista = ObterDadosDoGrid();

            var estruturaComNome = new
            {
                Contribuintes = lista
            };

            var json = new JavaScriptSerializer().Serialize(estruturaComNome);

            BaixarArquivo(json, "contribuintes_exportados.json", "application/json");
        }

        protected void btnExportarXml_Click(object sender, EventArgs e)
        {
            var lista = ObterDadosDoGrid();

            XmlSerializer serializer = new XmlSerializer(typeof(List<ContribuinteExport>));
            using (StringWriter sw = new StringWriter())
            {
                serializer.Serialize(sw, lista);
                BaixarArquivo(sw.ToString(), "contribuintes_exportados.xml", "text/xml");
            }
        }

        private List<ContribuinteExport> ObterDadosDoGrid()
        {
            List<ContribuinteExport> lista = new List<ContribuinteExport>();

            foreach (GridViewRow row in gvExibirCnpjs.Rows)
            {
                lista.Add(new ContribuinteExport
                {
                    CNPJ = row.Cells[1].Text,
                    NomeEmpresarial = row.Cells[2].Text
                });
            }
            return lista;
        }

        private void BaixarArquivo(string conteudo, string nomeArquivo, string tipoConteudo)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + nomeArquivo);
            Response.Charset = "";
            Response.ContentType = tipoConteudo;
            Response.Output.Write(conteudo);
            Response.Flush();
            Response.End();
        }
    }
}