using CampoReutilizavel.DAL;
using CampoReutilizavel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampoReutilizavel.Services
{
    public class UsuarioService
    {
        private readonly DAL.UsuarioDAL _usuarioDAL;


        public static RespostaAcao obterCodigoVerificacao(string email, int codigo)
        {
            try
            {
                int codigoBanco = UsuarioDAL.obterCodigoVerificacao(email);

                if(codigoBanco == codigo)
                {
                    return new RespostaAcao().SucessoOk("Código de verificação correto.");
                }
                else
                {
                    return new RespostaAcao().Falha("Código de verificação incorreto.");
                }
            }
            catch (Exception ex)
            {
                return new RespostaAcao().Falha("Ocorreu um erro ao obter o código de verificação: " + ex.Message);
            }
        }
    }
}