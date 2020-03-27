namespace ApiEmpresa.Models
{
    public class Endereco
    {
        public int Id_Endereco { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public int Id_Bairro { get; set; }
        public int Id_Estado { get; set; }
        public int Id_Cidade { get; set; }
        public string Cep { get; set; }
    }
}
