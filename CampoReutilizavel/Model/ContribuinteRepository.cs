using CampoReutilizavel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CampoReutilizavel.Model
{
    public class ContribuinteRepository
    {

        private static List<Contribuinte> contribuintes = new List<Contribuinte>
            {
            new Contribuinte("13.038.169/0001-82", "Aurora Tecnologia Digital Ltda."),
            new Contribuinte("16.188.285/0001-76", "Verde Vale Alimentos Naturais S.A."),
            new Contribuinte("09.148.191/0001-08", "Atlas Engenharia e Projetos ME"),
            new Contribuinte("57.207.841/0001-91", "Nexus Soluções Empresariais EPP"),
            new Contribuinte("66.100.693/0001-00", "Horizonte Logística Integrada S.A."),
            new Contribuinte("45.658.346/0001-10", "Prime Saúde Equipamentos Médicos ME"),
            new Contribuinte("05.122.332/0001-62", "Solaris Energia Sustentável EPP"),
            new Contribuinte("27.440.154/0001-50", "Fênix Comércio de Tecnologia Ltda."),
            new Contribuinte("86.394.595/0001-22", "Nova Rota Transportes Urbanos S.A."),
            new Contribuinte("58.474.756/0001-52", "gueio meio Urbanos S.A."),
            new Contribuinte("48.085.299/0001-50", "brita japonesaS.A."),


            };

        public static List<Contribuinte> buscar(string termo)
        {
            if (string.IsNullOrWhiteSpace(termo))
            {
                return new List<Contribuinte>();
            }

            termo = termo.Trim().ToLower();

            
            string termoLimpo = termo
                .Replace(".", "")
                .Replace("/", "")
                .Replace("-", "");

            
            if (termoLimpo.Length == 14 && termoLimpo.All(char.IsDigit))
            {
                
                return contribuintes
                    .Where(c => c.CNPJ.Replace(".", "").Replace("/", "").Replace("-", "").Equals(termoLimpo))
                    .ToList();
            }
            else if (termoLimpo.Length == 14 && termoLimpo.All(char.IsLetterOrDigit))
            {
               
                return contribuintes
                    .Where(c => c.CNPJ.Replace(".", "").Replace("/", "").Replace("-", "").ToLower().Equals(termoLimpo))
                    .ToList();
            }
            else
            {
               
                return contribuintes
                    .Where(c => c.NomeEmpresarial.ToLower().Contains(termo))
                    .ToList();
            }
        }


    }
}