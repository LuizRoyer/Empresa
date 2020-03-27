using ApiEmpresa.Models;
using ApiEmpresa.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ApiEmpresa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        [HttpGet("[action]")]
        public IEnumerable<Empresa> SelecionarEmpresas()
        {
            return new EmpresaService().SelecionarEmpresas();
        }
        [HttpPost("[action]")]
        public string InsertOrUpdate([FromBody]Empresa empresa)
        {
            return new EmpresaService().InsertOrUpdate(empresa);
        }
        [HttpDelete("[action]/{id}")]
        public string Delete(int id)
        {
           return new EmpresaService().Delete(id);
        }
    }
}