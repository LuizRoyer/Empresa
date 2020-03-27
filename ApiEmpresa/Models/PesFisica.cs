using System;

namespace ApiEmpresa.Models
{
    public class PesFisica:Pessoa
    {
        public int IdPesFisica { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Rg { get; set; }
    }
}
