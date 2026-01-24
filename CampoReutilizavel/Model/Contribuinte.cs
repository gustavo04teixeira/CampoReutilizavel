using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampoReutilizavel.Models
{
    public class Contribuinte
    {
        public string CNPJ;
        public string NomeEmpresarial;

        public Contribuinte(string cnpj, string nomeEmpresarial)
        {
            CNPJ = cnpj;
            NomeEmpresarial = nomeEmpresarial;
        }
    }
}