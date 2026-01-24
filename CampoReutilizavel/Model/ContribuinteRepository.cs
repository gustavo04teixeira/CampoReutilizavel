using CampoReutilizavel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampoReutilizavel.Model
{
    public class ContribuinteRepository
    {

        private static List<Contribuinte> contribuintes = new List<Contribuinte>
            {
            new Contribuinte("14.672.981/0001-32", "Aurora Tecnologia Digital Ltda."),
            new Contribuinte("29.845.103/0001-07", "Verde Vale Alimentos Naturais S.A."),
            new Contribuinte("08.391.774/0001-65", "Atlas Engenharia e Projetos ME"),
            new Contribuinte("51.207.998/0001-44", "Nexus Soluções Empresariais EPP"),
            new Contribuinte("36.954.120/0001-18", "BlueWave Marketing e Comunicação Ltda."),
            new Contribuinte("72.618.405/0001-90", "Horizonte Logística Integrada S.A."),
            new Contribuinte("19.483.660/0001-53", "Prime Saúde Equipamentos Médicos ME"),
            new Contribuinte("64.095.211/0001-81", "Solaris Energia Sustentável EPP"),
            new Contribuinte("47.820.336/0001-26", "Fênix Comércio de Tecnologia Ltda."),
            new Contribuinte("90.174.558/0001-09", "Nova Rota Transportes Urbanos S.A."),


            };

        public static List<Contribuinte> buscar(string termo)
        {
            if (string.IsNullOrWhiteSpace(termo))
            {
                return new List<Contribuinte>();
            }

            string termoLimpo = new string(termo.Where(char.IsDigit).ToArray());
            bool cnpjCompleto = termoLimpo.Length == 14;

            if (cnpjCompleto)
            {
                return contribuintes
                    .Where(c =>
                    new string(c.CNPJ.Where(char.IsDigit).ToArray()) == termoLimpo)
                    .ToList();
            }
            else
            {
                return contribuintes
                .Where(c =>
                c.NomeEmpresarial.ToLower().Contains(termo.ToLower()))
                .ToList();
            }
        }
    }
}