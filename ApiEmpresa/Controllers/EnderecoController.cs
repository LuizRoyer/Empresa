using ApiEmpresa.Models.ViewModels;
using ApiEmpresa.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ApiEmpresa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        [HttpGet("[action]")]
        public IEnumerable<EnderecoView> SelecionarEnderecos()
        {
            return new EnderecoService().SelecionarEnderecos();
        }
        [HttpPost("[action]")]
        public string InsertOrUpdate([FromBody]EnderecoView endereco)
        {
           return new EnderecoService().InsertOrUpdate(endereco);
        }
        [HttpDelete("[action]/{id}")]
        public string Delete(int id)
        {
            return new EnderecoService().Delete(id);
        }
        [HttpGet("[action]/{id}")]
        public EnderecoView SelecionarEndereco(int id)
        {
            return new EnderecoService().SelecionarEndereco(id);
        }
    }
}