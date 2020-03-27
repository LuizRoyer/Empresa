using ApiEmpresa.Models.ViewModels;

namespace ApiEmpresa.Models
{
    public class Empresa
    {
        public int IdEmpresa { get; set; }      
        public string NomeFantasia { get; set; }
        public EnderecoView Endereco { get; set; }
        public string CNPJ { get; set; }
    }
}
