namespace ApiEmpresa.Models.ViewModels
{
    public class EmpresaView
    {
        public int IdEmpresa { get; set; }
        public int NomeFantasia { get; set; }
        public int Endereco { get; set; }
        public string CNPJ { get; set; }
        public string Cep { get; set; }
        public int IdEstado { get; set; }
        public string Uf { get; set; }
        public string NomeEstado { get; set; }
        public int IdCidade { get; set; }
        public string Localidade { get; set; }
        public int IdBairro { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
    }
}
