using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;


namespace CampoReutilizavel.Controllers
{
    public class CnpjController : ApiController
    {
        private static readonly HttpClient client = new HttpClient();

        [HttpGet]
        [Route("api/cnpj/{cnpj}")]
        public async Task<IHttpActionResult> Consultar(string cnpj)
        {
            string cnpjLimpo = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpjLimpo.Length != 14)
                return BadRequest("Cnpj deve ter no minímo 14 digitos");

            try
            {
                string url = $"https://publica.cnpj.ws/cnpj/{cnpjLimpo}";

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    return Ok(new JavaScriptSerializer().DeserializeObject(json));
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }

        }
    }
}