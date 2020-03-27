using ApiEmpresa.Models;
using ApiEmpresa.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ApiEmpresa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PesJuridicaController : ControllerBase
    {
        [HttpGet("[action]/{id?}")]
        public IEnumerable<PesJuridica> SelecionarPesJuridicas(int id)
        {
            return new PesJuridicaService().SelecionarPesJuridica(id);
        }
        [HttpPost("[action]")]
        public string InsertOrUpdate([FromBody]PesJuridica pesJuridica)
        {
           return new PesJuridicaService().InsertOrUpdate(pesJuridica);
        }
        [HttpDelete("[action]/{id}")]
        public string Delete(int id)
        {
          return  new PesJuridicaService().Delete(id);
        }
    }
}