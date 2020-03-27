using ApiEmpresa.Models;
using ApiEmpresa.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ApiEmpresa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : ControllerBase
    {
        [HttpGet("[action]/{id?}")]
        public IEnumerable<Contato> SelecionarContatos(int id)
        {
            return new ContatoService().SelecionarContatos(id);
        }
        [HttpPost("[action]")]
        public string InsertOrUpdate([FromBody]Contato contato)
        {
            return new ContatoService().InsertOrUpdate(contato);
        }
        [HttpDelete("[action]/{id}/{idPessoa}")]
        public void Delete(int id,int idPessoa)
        {
            new ContatoService().Delete(id,idPessoa);
        }
    }
}