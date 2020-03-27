using ApiEmpresa.Models;
using ApiEmpresa.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ApiEmpresa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BairroController : ControllerBase
    {
        [HttpGet("[action]")]
        public IEnumerable<Bairro> SelecionarBairros()
        {
            return new BairroService().SelecionarBairros();
        }
        [HttpPost("[action]")]
        public Bairro SelecionarBairro([FromBody]Bairro bairro)
        {
            return new BairroService().SelecionarBairro(bairro);
        }
        [HttpPost("[action]")]
        public string InsertOrUpdate([FromBody]Bairro bairro)
        {
           return new BairroService().InsertOrUpdate(bairro);
        }
        [HttpDelete("[action]/{id}")]
        public string Delete(int id)
        {
           return new BairroService().Delete(id);
        }
    }
}