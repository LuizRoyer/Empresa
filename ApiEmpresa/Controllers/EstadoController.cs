using ApiEmpresa.Models;
using ApiEmpresa.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ApiEmpresa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {        
        [HttpGet("[action]")]
        public IEnumerable<Estado> SelecionarEstados()
        {
            return new EstadoService().SelecionarEstados();
        }
        [HttpPost("[action]")]
        public Estado SelecionarEstado([FromBody]Estado estado)
        {
            return new EstadoService().SelecionarEstado(estado);
        }
        [HttpPost("[action]")]
        public string InsertOrUpdate([FromBody]Estado estado)
        {
            return new EstadoService().InsertOrUpdate(estado);
        }
        [HttpDelete("[action]/{id}")]
        public string Delete(int id)
        {
            return new EstadoService().Delete(id);
        }
    }
}
