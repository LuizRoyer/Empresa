using System;

namespace ApiEmpresa.Models
{
    public class Fornecedor
    {
        public int IdFornecedor { get; set; }
        public int IdEmpresa { get; set; }
        public int IdPessoa { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
