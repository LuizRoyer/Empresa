using ApiEmpresa.DAO.IRepository;
using ApiEmpresa.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ApiEmpresa.DAO.Repository
{
    public class PessoaDao : IPessoaDao
    {
        private readonly SqlConnection _conn;
        private readonly SqlTransaction _trans;

        public PessoaDao(SqlConnection conn, SqlTransaction trans)
        {
            this._conn = conn;
            this._trans = trans;
        }

        public List<Pessoa> PessoasList(int id)
        {
            List<Pessoa> listaPessoa = new List<Pessoa>();
            StringBuilder sqlSelect = new StringBuilder();

            sqlSelect.Append(@"SELECT P.ID_PESSOA
                                      ,P.NOME
                                      ,P.SOBRENOME                                      
                                      ,P.TIPO                                     
                                  FROM dbo.Pessoa P									   
                                     WHERE 1=1");
            if (id > 0)
                sqlSelect.Append(" AND P.Id_Pessoa =@id ");
            sqlSelect.Append(" ORDER BY P.ID_PESSOA ");

            SqlCommand cmd = new SqlCommand(sqlSelect.ToString(), _conn);
            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("id", id));
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Pessoa pessoa = new Pessoa
                    {
                        IdPessoa = Convert.ToInt32(reader["ID_PESSOA"].ToString()),
                        Nome = reader["NOME"].ToString(),
                        Sobrenome = reader["SOBRENOME"].ToString(),
                        Tipo = reader["TIPO"].ToString(),
                    };

                    listaPessoa.Add(pessoa);
                }
            }

            return listaPessoa;
        }
        public bool GetAsExist(int id)
        {
            StringBuilder sqlSelect = new StringBuilder();

            sqlSelect.Append(@"SELECT
                                     NOME                                      
                                  FROM dbo.Pessoa
                                      WHERE ID_PESSOA =@id
                                   ORDER BY ID_PESSOA ");

            SqlCommand cmd = new SqlCommand(sqlSelect.ToString(), _conn);
            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                    return true;               
            }
            return false;
        }
        public int Insert(Pessoa obj)
        {
            StringBuilder sqlInsert = new StringBuilder();

            sqlInsert.Append(@"INSERT INTO dbo.Pessoa
                             (NOME, SOBRENOME,TIPO)
                                values
                             (@nome, @sobrenome,@tipo)");

            SqlCommand cmd = new SqlCommand(sqlInsert.ToString(), _conn);
            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("@nome", obj.Nome));
            cmd.Parameters.Add(new SqlParameter("@sobrenome", obj.Sobrenome));
            cmd.Parameters.Add(new SqlParameter("@tipo", obj.Tipo));
            
            return cmd.ExecuteNonQuery();
        }
        public int Update(Pessoa obj)
        {
            StringBuilder sqlUpdate = new StringBuilder();

            sqlUpdate.Append(@"UPDATE dbo.Pessoa
                                SET NOME =@nome
                                ,SOBRENOME=@sobrenome                               
                                ,TIPO=@tipo
                             WHERE ID_PESSOA =@id ");

            SqlCommand cmd = new SqlCommand(sqlUpdate.ToString(), _conn);

            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("@nome", obj.Nome));
            cmd.Parameters.Add(new SqlParameter("@sobrenome", obj.Sobrenome));
            cmd.Parameters.Add(new SqlParameter("@tipo", obj.Tipo));
            cmd.Parameters.Add(new SqlParameter("@id", obj.IdPessoa));

            return cmd.ExecuteNonQuery();
        }
        public int Delete(int idPessoa)
        {
            StringBuilder sqlDelete = new StringBuilder();

            sqlDelete.Append(@"DELETE FROM dbo.Pessoa
                                WHERE ID_PESSOA =@id ");

            SqlCommand cmd = new SqlCommand(sqlDelete.ToString(), _conn);
            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("@id", idPessoa));
            return cmd.ExecuteNonQuery();
        }

        public Pessoa GetAsObject(Pessoa obj)
        {
            StringBuilder sqlSelect = new StringBuilder();

            sqlSelect.Append(@"SELECT P.ID_PESSOA
                                      ,P.NOME
                                      ,P.SOBRENOME                                      
                                      ,P.TIPO                                     
                                  FROM dbo.Pessoa P									   
                                     WHERE 1=1");
            if (obj.IdPessoa > 0)
                sqlSelect.Append(" AND P.Id_Pessoa =@id ");
            if (!string.IsNullOrWhiteSpace(obj.Nome))
                sqlSelect.Append(" AND P.NOME =@nome ");
            if (!string.IsNullOrWhiteSpace(obj.Sobrenome))
                sqlSelect.Append(" AND P.SOBRENOME =@sobrenome ");
            if (!string.IsNullOrWhiteSpace(obj.Tipo))
                sqlSelect.Append(" AND P.TIPO =@tipo ");



            sqlSelect.Append(" ORDER BY P.ID_PESSOA ");

            SqlCommand cmd = new SqlCommand(sqlSelect.ToString(), _conn);
            cmd.Transaction = _trans;
            if (obj.IdPessoa > 0)
                cmd.Parameters.Add(new SqlParameter("id", obj.IdPessoa));
            if (!string.IsNullOrWhiteSpace(obj.Nome))
                cmd.Parameters.Add(new SqlParameter("@nome", obj.Nome));
            if (!string.IsNullOrWhiteSpace(obj.Sobrenome))
                cmd.Parameters.Add(new SqlParameter("@sobrenome", obj.Sobrenome));
            if (!string.IsNullOrWhiteSpace(obj.Tipo))
                cmd.Parameters.Add(new SqlParameter("@tipo", obj.Tipo));
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    return new Pessoa
                    {
                        IdPessoa = Convert.ToInt32(reader["ID_PESSOA"].ToString()),
                        Nome = reader["NOME"].ToString(),
                        Sobrenome = reader["SOBRENOME"].ToString(),
                        Tipo = reader["TIPO"].ToString(),
                    };


                }
            }

            return new Pessoa();
        }
    }
}
