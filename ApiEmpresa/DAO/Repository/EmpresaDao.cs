using ApiEmpresa.DAO.IRepository;
using ApiEmpresa.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ApiEmpresa.DAO.Repository
{
    public class EmpresaDao : IEmpresaDao
    {
        private readonly SqlConnection _conn;
        private readonly SqlTransaction _trans;

        public EmpresaDao(SqlConnection conn, SqlTransaction trans)
        {
            this._conn = conn;
            this._trans = trans;
        }

        public List<Empresa> EmpresaList()
        {
            List<Empresa> listaEmpresa = new List<Empresa>();
            StringBuilder sqlSelect = new StringBuilder();

            sqlSelect.Append(@"SELECT EN.Id_Empresa
                                      ,EN.NomeFantasia
                                      ,EN.Endereco
                                      ,EN.CNPJ
                                      ,E.Id_Endereco 
                                      ,E.Cep ,
                                      E.Id_Estado ,
                                      ES.Uf ,
                                      ES.Nome AS NomeEstado ,
                                      E.Id_Cidade ,
                                      C.Nome AS NomeCidade ,
                                      E.Id_Bairro ,
                                      B.Nome AS NomeBairro ,
                                      E.Logradouro ,
                                      E.Complemento
                                FROM dbo.Empresa EN,
                                     dbo.Endereco E,
                                     dbo.Estado ES,
                                     dbo.Cidade C,
                                     dbo.Bairro B
                                WHERE EN.Endereco = E.Id_Endereco
                                  AND E.Id_Estado = ES.Id_Estado
                                  AND E.Id_Estado = C.Id_Estado
                                  AND E.id_Cidade = C.Id_Cidade
                                  AND E.Id_Bairro = B.Id_Bairro
                                  AND E.id_Cidade = B.Id_Cidade
                            ORDER BY Id_Empresa ");

            SqlCommand cmd = new SqlCommand(sqlSelect.ToString(), _conn);
            cmd.Transaction = _trans;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Empresa empresa = new Empresa
                    {
                        IdEmpresa = Convert.ToInt32(reader["Id_Empresa"].ToString()),
                        NomeFantasia = reader["NOMEFANTASIA"].ToString(),
                        CNPJ = reader["CNPJ"].ToString(),
                        Endereco = new EnderecoDao(_conn, _trans).PopularObjetoEnderecoView(reader)
                    };

                    listaEmpresa.Add(empresa);
                }
            }

            return listaEmpresa;
        }

        public bool GetASExist(int id)
        {
            StringBuilder sqlSelect = new StringBuilder();

            sqlSelect.Append(@"SELECT 
                                      NomeFantasia                                    
                                  FROM dbo.Empresa
                                      WHERE Id_Empresa=@id
                                   ORDER BY Id_Empresa ");

            SqlCommand cmd = new SqlCommand(sqlSelect.ToString(), _conn);
            cmd.Parameters.Add(new SqlParameter("@id", id));
            cmd.Transaction = _trans;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                    return true;
            }
            return false;
        }

        public Empresa GetAsObject(int id)
        {
            StringBuilder sqlSelect = new StringBuilder();

            sqlSelect.Append(@"SELECT EN.Id_Empresa
                                      ,EN.NomeFantasia
                                      ,EN.Endereco
                                      ,EN.CNPJ
                                      ,E.Id_Endereco 
                                      ,E.Cep ,
                                      E.Id_Estado ,
                                      ES.Uf ,
                                      ES.Nome AS NomeEstado ,
                                      E.Id_Cidade ,
                                      C.Nome AS NomeCidade ,
                                      E.Id_Bairro ,
                                      B.Nome AS NomeBairro ,
                                      E.Logradouro ,
                                      E.Complemento
                                FROM dbo.Empresa EN,
                                     dbo.Endereco E,
                                     dbo.Estado ES,
                                     dbo.Cidade C,
                                     dbo.Bairro B
                                WHERE EN.Endereco = E.Id_Endereco
                                  AND E.Id_Estado = ES.Id_Estado
                                  AND E.Id_Estado = C.Id_Estado
                                  AND E.id_Cidade = C.Id_Cidade
                                  AND E.Id_Bairro = B.Id_Bairro
                                  AND E.id_Cidade = B.Id_Cidade
                                  AND EN.Id_Empresa = @id
                            ORDER BY Id_Empresa ");

            SqlCommand cmd = new SqlCommand(sqlSelect.ToString(), _conn);
            cmd.Parameters.Add(new SqlParameter("@id", id));
            cmd.Transaction = _trans;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    return new Empresa
                    {
                        IdEmpresa = Convert.ToInt32(reader["Id_Empresa"].ToString()),
                        NomeFantasia = reader["NOMEFANTASIA"].ToString(),
                        CNPJ = reader["CNPJ"].ToString(),
                        Endereco = new EnderecoDao(_conn, _trans).PopularObjetoEnderecoView(reader)
                    };
                }
            }

            return new Empresa();
        }

        public int Insert(Empresa obj)
        {
            StringBuilder sqlInsert = new StringBuilder();

            sqlInsert.Append(@"INSERT INTO dbo.Empresa
                             (NOMEFANTASIA, CNPJ,ENDERECO)
                                values
                             (@nome, @cnpj , @endereco)");

            SqlCommand cmd = new SqlCommand(sqlInsert.ToString(), _conn);
            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("@nome", obj.NomeFantasia));
            cmd.Parameters.Add(new SqlParameter("@cnpj", obj.CNPJ));
            cmd.Parameters.Add(new SqlParameter("@endereco", obj.Endereco.IdEndereco));

            return cmd.ExecuteNonQuery();
        }

        public int Update(Empresa obj)
        {
            StringBuilder sqlUpdate = new StringBuilder();

            sqlUpdate.Append(@"UPDATE dbo.Empresa
                                SET NOMEFANTASIA =@nome
                                ,CNPJ=@cnpj
                                ,ENDERECO =@endereco
                             WHERE Id_Empresa =@id ");

            SqlCommand cmd = new SqlCommand(sqlUpdate.ToString(), _conn);

            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("@nome", obj.NomeFantasia));
            cmd.Parameters.Add(new SqlParameter("@cnpj", obj.CNPJ));
            cmd.Parameters.Add(new SqlParameter("@endereco", obj.Endereco.IdEndereco));
            cmd.Parameters.Add(new SqlParameter("@id", obj.IdEmpresa));

            return cmd.ExecuteNonQuery();
        }
        public int Delete(int id)
        {
            StringBuilder sqlDelete = new StringBuilder();

            sqlDelete.Append(@"DELETE FROM dbo.Empresa
                                WHERE Id_Empresa =@id ");

            SqlCommand cmd = new SqlCommand(sqlDelete.ToString(), _conn);
            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            return cmd.ExecuteNonQuery();
        }
    }
}
