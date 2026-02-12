using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampoReutilizavel.Models
{
    public class Contribuinte
    {
        public string CNPJ { get; set; }
        public string NomeEmpresarial { get; set; }

        public Contribuinte()
        {

        }
        public Contribuinte(string cnpj, string nomeEmpresarial)
        {
            CNPJ = cnpj;
            NomeEmpresarial = nomeEmpresarial;
        }
    }
}