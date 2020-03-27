using ApiEmpresa.DAO.IRepository;
using ApiEmpresa.Models;
using ApiEmpresa.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ApiEmpresa.DAO.Repository
{
    public class FornecedorDao : IFornecedorDao
    {

        private readonly SqlConnection _conn;
        private readonly SqlTransaction _trans;

        public FornecedorDao(SqlConnection conn, SqlTransaction trans)
        {
            this._conn = conn;
            this._trans = trans;
        }

        public List<FornecedorView> FornecedoresList()
        {
            List<FornecedorView> listaFornecedores = new List<FornecedorView>();
            StringBuilder sqlSelect = new StringBuilder();

            #region SELECT Dos Fornecedores com Os seus Dados
            sqlSelect.Append(@"SELECT F.Id_Empresa,
                                           E.NomeFantasia,
                                           E.CNPJ AS CnpjEmpresa,
                                           E.Endereco,
                                           EN.Cep,
                                           EN.Id_Estado,
                                           ES.Uf,
                                           ES.Nome AS NomeEstado,
                                           EN.id_Cidade,
                                           CDD.Nome AS NomeCidade,
                                           EN.Id_Bairro,
                                           B.Nome AS NomeBairro,
                                           EN.Logradouro,
                                           EN.Complemento,
                                          ISNULL(PF.Id_PesFisica,0) Id_PesFisica ,
                                           P.Id_Pessoa,
                                           P.Nome AS NomeFornecedor,
                                           P.Sobrenome,                                      
                                           CASE   
											  WHEN P.Tipo ='F' THEN 'Físico' 
											  WHEN P.Tipo ='J' THEN 'Jurídico' 
										   END  Tipo,
                                          ISNULL(PF.Cpf,'') as Cpf,
                                           ISNULL(PF.DataNascimento,'') DataNascimento,
                                          ISNULL( PF.Rg,'') Rg,
                                          ISNULL(J.Cnpj,'') Cnpj,
                                          ISNULL(J.Id_PesJuridica,0) Id_PesJuridica ,
                                           F.DataCadastro,
                                           F.Id_Fornecedor
                                    FROM dbo.Fornecedor F,
                                         dbo.Pessoa P
                                    LEFT JOIN dbo.PesFisica PF ON PF.Id_Pessoa = P.Id_Pessoa
                                    LEFT JOIN dbo.PesJuridica J ON J.Id_Pessoa = P.Id_Pessoa,                                                                
                                                                   dbo.Empresa E,
                                                                   dbo.Endereco EN,
                                                                   dbo.Estado Es,
                                                                   dbo.Bairro B,
                                                                   dbo.Cidade CDD
                                    WHERE F.Id_Pessoa = P.Id_Pessoa
                                      AND F.Id_Empresa = E.Id_Empresa                                      
                                      AND F.Id_Empresa = E.Id_Empresa
                                      AND EN.Id_Estado = Es.Id_Estado
                                      AND EN.Id_Estado = Es.Id_Estado
                                      AND EN.id_Cidade = CDD.Id_Cidade
                                      AND EN.Id_Bairro = B.Id_Bairro
                                      AND CDD.Id_Estado = Es.Id_Estado
                                      AND CDD.Id_Cidade = B.Id_Bairro
                                      AND EN.id_Cidade = B.Id_Cidade
                                      AND En.Id_Bairro = B.Id_Bairro");
            #endregion

            SqlCommand cmd = new SqlCommand(sqlSelect.ToString(), _conn);
            cmd.Transaction = _trans;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    FornecedorView fornecedores = new FornecedorView
                    {
                        NomeFantasia = reader["NomeFantasia"].ToString().Trim(),
                        CnpjEmpresa = reader["CnpjEmpresa"].ToString().Trim(),
                        IdEndereco = Convert.ToInt32(reader["Endereco"].ToString().Trim()),
                        Cep = reader["Cep"].ToString().Trim(),
                        IdEstado = Convert.ToInt32(reader["Id_Estado"].ToString()),
                        Uf = reader["UF"].ToString().Trim(),
                        NomeEstado = reader["NomeEstado"].ToString().Trim(),
                        IdCidade = Convert.ToInt32(reader["ID_CIDADE"].ToString()),
                        NomeCidade = reader["NomeCidade"].ToString().Trim(),
                        IdBairro = Convert.ToInt32(reader["Id_Bairro"].ToString()),
                        NomeBairro = reader["NomeBairro"].ToString().Trim(),
                        Complemento = reader["Complemento"].ToString().Trim(),
                        Logradouro = reader["Logradouro"].ToString().Trim(),
                        IdPesFisica = Convert.ToInt32(reader["ID_PESFISICA"].ToString()),
                        IdPessoa = Convert.ToInt32(reader["ID_PESSOA"].ToString()),
                        NomeFornecedor = reader["NomeFornecedor"].ToString().Trim(),
                        Sobrenome = reader["SOBRENOME"].ToString().Trim(),                        
                        Tipo = reader["Tipo"].ToString().Trim(),
                        Cpf = reader["Cpf"].ToString().Trim(),
                        DataNascimento = reader["DataNascimento"] == DBNull.Value ? new DateTime(9999, 01, 01) : Convert.ToDateTime(reader["DataNascimento"]),
                        Rg = reader["RG"].ToString().Trim(),
                        Cnpj = reader["CNPJ"].ToString().Trim(),
                        IdPesJuridica = Convert.ToInt32(reader["ID_PESJURIDICA"].ToString()),
                        DataCadastro = reader["DataCadastro"] == DBNull.Value ? new DateTime(9999, 01, 01) : Convert.ToDateTime(reader["DataCadastro"]),
                        IdFornecedor = Convert.ToInt32(reader["Id_Fornecedor"].ToString()),
                        IdEmpresa = Convert.ToInt32(reader["Id_Empresa"].ToString()),
                    };

                    listaFornecedores.Add(fornecedores);
                }
            }

            return listaFornecedores;
        }
        public Fornecedor GetAsObject(int id)
        {
            StringBuilder sqlSelect = new StringBuilder();

            sqlSelect.Append(@"SELECT Id_Fornecedor
                                      ,Id_Empresa
	                                  ,Id_Pessoa
                                      ,DataCadastro                           
                              FROM dbo.Fornecedor		                     	
	                                WHERE Id_Fornecedor =@id                                      
                          ORDER BY Id_Fornecedor ");

            SqlCommand cmd = new SqlCommand(sqlSelect.ToString(), _conn);
            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("id", id));

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    return new Fornecedor
                    {
                        IdFornecedor = Convert.ToInt32(reader["ID_PESFISICA"].ToString()),
                        IdPessoa = Convert.ToInt32(reader["ID_PESSOA"].ToString()),
                        IdEmpresa = Convert.ToInt32(reader["ID_EMPRESA"].ToString()),
                        DataCadastro = reader["DataCadastro"] == DBNull.Value ? new DateTime(9999, 01, 01) : Convert.ToDateTime(reader["DataCadastro"]),
                    };
                }
                return new Fornecedor();
            }
        }

        public bool GetAsExist(int id)
        {
            StringBuilder sqlSelect = new StringBuilder();

            sqlSelect.Append(@"SELECT Id_Fornecedor                                                         
                              FROM dbo.Fornecedor		                     	
	                                WHERE Id_Fornecedor =@id                                        
                          ORDER BY Id_Fornecedor ");

            SqlCommand cmd = new SqlCommand(sqlSelect.ToString(), _conn);
            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("id", id));
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                    return true;
            }
            return false;
        }


        public int Insert(Fornecedor obj)
        {
            StringBuilder sqlInsert = new StringBuilder();

            sqlInsert.Append(@"INSERT INTO dbo.Fornecedor
                             (ID_PESSOA ,ID_EMPRESA ,DATACADASTRO)
                                values
                             (@idPessoa ,@idEmpresa,@datacadastro)");

            SqlCommand cmd = new SqlCommand(sqlInsert.ToString(), _conn);
            cmd.Transaction = _trans;
          
            cmd.Parameters.Add(new SqlParameter("@idPessoa", obj.IdPessoa));
            cmd.Parameters.Add(new SqlParameter("@idEmpresa", obj.IdEmpresa));
            cmd.Parameters.Add(new SqlParameter("@datacadastro", obj.DataCadastro));

            return cmd.ExecuteNonQuery();
        }

        public int Update(Fornecedor obj)
        {
            StringBuilder sqlUpdate = new StringBuilder();

            sqlUpdate.Append(@"UPDATE dbo.Fornecedor
                                SET ID_PESSOA =@idPessoa
                                 ,ID_EMPRESA =@idEmpresa
                                 ,DATACADASTRO =@datacadastro 
                             WHERE Id_Fornecedor =@id ");

            SqlCommand cmd = new SqlCommand(sqlUpdate.ToString(), _conn);

            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("id", obj.IdFornecedor));
            cmd.Parameters.Add(new SqlParameter("@idPessoa", obj.IdPessoa));
            cmd.Parameters.Add(new SqlParameter("@idEmpresa", obj.IdEmpresa));
            cmd.Parameters.Add(new SqlParameter("@datacadastro", obj.DataCadastro));
            return cmd.ExecuteNonQuery();
        }
        public int Delete(int id)
        {
            StringBuilder sqlDelete = new StringBuilder();

            sqlDelete.Append(@"DELETE FROM dbo.Fornecedor
                                WHERE Id_Fornecedor =@id ");

            SqlCommand cmd = new SqlCommand(sqlDelete.ToString(), _conn);
            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("@id", id));

            return cmd.ExecuteNonQuery();
        }
    }
}
