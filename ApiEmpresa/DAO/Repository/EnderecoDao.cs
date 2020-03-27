using ApiEmpresa.DAO.IRepository;
using ApiEmpresa.Models;
using ApiEmpresa.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ApiEmpresa.DAO.Repository
{
    public class EnderecoDao : IEnderecoDao
    {
        private readonly SqlConnection _conn;
        private readonly SqlTransaction _trans;

        public EnderecoDao(SqlConnection conn, SqlTransaction trans)
        {
            this._conn = conn;
            this._trans = trans;
        }

        public List<EnderecoView> EnderecosList()
        {
            List<EnderecoView> listaEndereco = new List<EnderecoView>();
            StringBuilder sqlSelect = new StringBuilder();

            sqlSelect.Append(@"SELECT E.Id_Endereco ,
                                      E.Cep ,
                                      E.Id_Estado ,
                                      ES.Uf ,
                                      ES.Nome AS NomeEstado ,
                                      E.Id_Cidade ,
                                      C.Nome AS NomeCidade ,
                                      E.Id_Bairro ,
                                      B.Nome AS NomeBairro ,
                                      E.Logradouro ,
                                      E.Complemento
                                FROM dbo.Endereco E,
                                     dbo.Estado ES,
                                     dbo.Cidade C,
                                     dbo.Bairro B
                                WHERE E.Id_Estado = ES.Id_Estado
                                  AND E.Id_Estado = C.Id_Estado
                                  AND E.id_Cidade = C.Id_Cidade
                                  AND E.Id_Bairro = B.Id_Bairro
                                  AND E.id_Cidade = B.Id_Cidade");

            SqlCommand cmd = new SqlCommand(sqlSelect.ToString(), _conn);
            cmd.Transaction = _trans;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    EnderecoView endereco = PopularObjetoEnderecoView(reader);
                    listaEndereco.Add(endereco);
                }
            }

            return listaEndereco;
        }

        public bool GetAsExist(int id)
        {
            List<EnderecoView> listaEndereco = new List<EnderecoView>();
            StringBuilder sqlSelect = new StringBuilder();

            sqlSelect.Append(@"SELECT CEP
                                 FROM dbo.Endereco
                                    WHERE  Id_Endereco=@id");

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

        public EnderecoView GetAsObject(Endereco obj)
        {
            StringBuilder sqlSelect = new StringBuilder();

            sqlSelect.Append(@"SELECT E.Id_Endereco ,
                                      E.Cep ,
                                      E.Id_Estado ,
                                      ES.Uf ,
                                      ES.Nome AS NomeEstado ,
                                      E.Id_Cidade ,
                                      C.Nome AS NomeCidade ,
                                      E.Id_Bairro ,
                                      B.Nome AS NomeBairro ,
                                      E.Logradouro ,
                                      E.Complemento
                                FROM dbo.Endereco E,
                                     dbo.Estado ES,
                                     dbo.Cidade C,
                                     dbo.Bairro B
                                WHERE E.Id_Estado = ES.Id_Estado
                                  AND E.Id_Estado = C.Id_Estado
                                  AND E.id_Cidade = C.Id_Cidade
                                  AND E.Id_Bairro = B.Id_Bairro
                                  AND E.id_Cidade = B.Id_Cidade");

            if (obj.Id_Endereco > 0)
                sqlSelect.Append(" AND  E.Id_Endereco = @endereco");
            if (obj.Id_Estado > 0)
                sqlSelect.Append("  AND E.Id_Estado = @estado");
            if (obj.Id_Cidade > 0)
                sqlSelect.Append("   AND E.id_Cidade = @cidade");
            if (obj.Id_Bairro > 0)
                sqlSelect.Append("   AND E.Id_Bairro = @bairro");
            if (!string.IsNullOrWhiteSpace(obj.Cep))
                sqlSelect.Append(" AND  E.Cep = @cep");


            SqlCommand cmd = new SqlCommand(sqlSelect.ToString(), _conn);
            cmd.Transaction = _trans;
            if (obj.Id_Endereco > 0)
                cmd.Parameters.Add(new SqlParameter("@endereco", obj.Id_Endereco));
            if (obj.Id_Estado > 0)
                cmd.Parameters.Add(new SqlParameter("@estado", obj.Id_Estado));
            if (obj.Id_Cidade > 0)
                cmd.Parameters.Add(new SqlParameter("@cidade", obj.Id_Cidade));
            if (obj.Id_Bairro > 0)
                cmd.Parameters.Add(new SqlParameter("@bairro", obj.Id_Bairro));
            if (!string.IsNullOrWhiteSpace(obj.Cep))
                cmd.Parameters.Add(new SqlParameter("@cep", obj.Cep));

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    return PopularObjetoEnderecoView(reader);
                }
            }

            return new EnderecoView();
        }

        public EnderecoView PopularObjetoEnderecoView(SqlDataReader reader)
        {
            return new EnderecoView
            {
                IdEndereco = Convert.ToInt32(reader["Id_Endereco"].ToString()),
                Cep = reader["CEP"].ToString(),
                IdEstado = Convert.ToInt32(reader["Id_Estado"].ToString()),
                Uf = reader["UF"].ToString(),
                NomeEstado = reader["NomeEstado"].ToString(),
                IdCidade = Convert.ToInt32(reader["Id_Cidade"].ToString()),
                Localidade = reader["NomeCidade"].ToString(),
                IdBairro = Convert.ToInt32(reader["ID_BAIRRO"].ToString()),
                Bairro = reader["NomeBairro"].ToString(),
                Logradouro = reader["Logradouro"].ToString(),
                Complemento = reader["Complemento"].ToString()
            };
        }

        public int Insert(Endereco obj)
        {
            StringBuilder sqlInsert = new StringBuilder();

            sqlInsert.Append(@"INSERT INTO dbo.Endereco
                             (Logradouro ,Complemento,Id_Bairro
                                ,Id_Estado,id_Cidade,Cep)
                             values
                             (@logradouro ,@complemento,@idBairro
                                ,@idEstado,@idCidade,@cep)");

            SqlCommand cmd = new SqlCommand(sqlInsert.ToString(), _conn);
            cmd.Transaction = _trans;

            cmd.Parameters.Add(new SqlParameter("@logradouro", obj.Logradouro));
            cmd.Parameters.Add(new SqlParameter("@complemento", obj.Complemento));
            cmd.Parameters.Add(new SqlParameter("@idBairro", obj.Id_Bairro));
            cmd.Parameters.Add(new SqlParameter("@idEstado", obj.Id_Estado));
            cmd.Parameters.Add(new SqlParameter("@idCidade", obj.Id_Cidade));
            cmd.Parameters.Add(new SqlParameter("@cep", obj.Cep));

            return cmd.ExecuteNonQuery();
        }

        public int Update(Endereco obj)
        {
            StringBuilder sqlUpdate = new StringBuilder();

            sqlUpdate.Append(@"UPDATE dbo.Endereco
                                SET  
                                 Logradouro=@logradouro                
                                ,Complemento=@complemento
                                ,Id_Bairro=@idBairro
                                ,Id_Estado=@idEstado
                                ,id_Cidade=@idCidade
                                ,Cep=@cep
                             WHERE Id_Endereco =@id ");

            SqlCommand cmd = new SqlCommand(sqlUpdate.ToString(), _conn);

            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("@id", obj.Id_Endereco));
            cmd.Parameters.Add(new SqlParameter("@logradouro", obj.Logradouro));
            cmd.Parameters.Add(new SqlParameter("@complemento", obj.Complemento));
            cmd.Parameters.Add(new SqlParameter("@idBairro", obj.Id_Bairro));
            cmd.Parameters.Add(new SqlParameter("@idEstado", obj.Id_Estado));
            cmd.Parameters.Add(new SqlParameter("@idCidade", obj.Id_Cidade));
            cmd.Parameters.Add(new SqlParameter("@cep", obj.Cep));

            return cmd.ExecuteNonQuery();
        }
        public int Delete(int id)
        {
            StringBuilder sqlDelete = new StringBuilder();

            sqlDelete.Append(@"DELETE FROM dbo.Endereco
                                WHERE Id_Endereco =@id ");

            SqlCommand cmd = new SqlCommand(sqlDelete.ToString(), _conn);
            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            return cmd.ExecuteNonQuery();
        }

        public bool GetExistDependent(int idEstado)
        {
            StringBuilder sqlSelect = new StringBuilder();

            sqlSelect.Append(@"SELECT Id_Endereco      
	                               FROM dbo.Endereco
                                     WHERE Id_Estado = @id");

            SqlCommand cmd = new SqlCommand(sqlSelect.ToString(), _conn);
            cmd.Parameters.Add(new SqlParameter("@id", idEstado));
            cmd.Transaction = _trans;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                    return true;
                return false;

            }
        }
    }
}
