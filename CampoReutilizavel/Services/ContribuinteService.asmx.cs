using CampoReutilizavel.Model;
using CampoReutilizavel.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;


namespace CampoReutilizavel.Services
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    [ScriptService]
    public class ContribuinteService : System.Web.Services.WebService
    {

        [WebMethod]
        public List<Contribuinte> BuscarContribuinte(string termo)
        {
            return CampoReutilizavel.Model.ContribuinteRepository.buscar(termo);
        }

        [WebMethod]
        public string UploadArquivo(string base64Xml, string extensao)
        {
            try
            {
                byte[] arquivoBytes = Convert.FromBase64String(base64Xml);
                using (MemoryStream ms = new MemoryStream(arquivoBytes))
                {
                    ContribuinteRepository.ImportarDadosArquivo(ms, extensao);
                }
                return "Arquivo " + extensao +" importado com sucesso!";
            }
            catch (Exception ex)
            {
                return "Erro ao importar: " + ex.Message;
            }
        }
    }
}
