using ApiEmpresa.Models;
using ApiEmpresa.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ApiEmpresa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PesFisicaController : ControllerBase
    {
        [HttpGet("[action]/{id}")]
        public IEnumerable<PesFisica> SelecionarPesFisicas(int id)
        {
            return new PesFisicaService().SelecionarPesFisica(id);
        }
        [HttpPost("[action]")]
        public void InsertOrUpdate([FromBody]PesFisica pesFisica)
        {
            new PesFisicaService().InsertOrUpdate(pesFisica);
        }
        [HttpDelete("[action]/{id}")]
        public void Delete(int id)
        {
            new PesFisicaService().Delete(id);
        }
    }
}