using ApiEmpresa.Models;
using ApiEmpresa.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ApiEmpresa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        [HttpGet("[action]")]
        public IEnumerable<Cidade> SelecionarCidades()
        {
            return new CidadeService().SelecionarCidades();
        }
        [HttpPost("[action]")]
        public Cidade SelecionarCidade([FromBody]Cidade cidade)
        {
            return new CidadeService().SelecionarCidade(cidade);
        }
        [HttpPost("[action]")]
        public string InsertOrUpdate([FromBody]Cidade cidade)
        {
           return new CidadeService().InsertOrUpdate(cidade);
        }
        [HttpDelete("[action]/{id}")]
        public string Delete(int id)
        {
           return new CidadeService().Delete(id);
        }
    }
}