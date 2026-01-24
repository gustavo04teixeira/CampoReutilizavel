using CampoReutilizavel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;


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
            return Model.ContribuinteRepository.buscar(termo);
        }
    }
}
