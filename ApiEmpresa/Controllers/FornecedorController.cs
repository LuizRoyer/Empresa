using ApiEmpresa.Models;
using ApiEmpresa.Models.ViewModels;
using ApiEmpresa.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ApiEmpresa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FornecedorController : ControllerBase
    {
        [HttpGet("[action]")]
        public IEnumerable<FornecedorView> SelecionarFornecedores()
        {
            return new FornecedorService().SelecionarFornecedores();
        }
        [HttpPost("[action]")]
        public void InsertOrUpdate([FromBody]Fornecedor fornecedor)
        {
            new FornecedorService().InsertOrUpdate(fornecedor);
        }
        [HttpDelete("[action]/{id}")]
        public void Delete(int id)
        {
            new FornecedorService().Delete(id);
        }
    }
}