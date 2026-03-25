using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampoReutilizavel.Model
{
    public class RespostaAcao
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }

        public RespostaAcao Falha(string mensagem) => new RespostaAcao { Sucesso = false, Mensagem = mensagem };
        public RespostaAcao SucessoOk(string mensagem) => new RespostaAcao { Sucesso = true, Mensagem = mensagem };
    }
}