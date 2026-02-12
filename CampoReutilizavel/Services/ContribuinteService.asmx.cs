using CampoReutilizavel.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
    }
}
