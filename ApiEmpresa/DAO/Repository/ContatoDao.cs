using ApiEmpresa.DAO.IRepository;
using ApiEmpresa.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ApiEmpresa.DAO.Repository
{
    public class ContatoDao : IContatoDao
    {
        private readonly SqlConnection _conn;
        private readonly SqlTransaction _trans;

        public ContatoDao(SqlConnection conn, SqlTransaction trans)
        {
            this._conn = conn;
            this._trans = trans;
        }

        public List<Contato> ContatosList(int idPessoa)
        {
            List<Contato> listaContato = new List<Contato>();
            StringBuilder sqlSelect = new StringBuilder();

            sqlSelect.Append(@"SELECT ID_CONTATO ,
                                    TELEFONE , CELULAR , ID_PESSOA
                                    FROM dbo.Contato");
            if (idPessoa > 0)
                sqlSelect.Append(" WHERE ID_PESSOA = @id");
            sqlSelect.Append(" ORDER BY Id_Contato ");

            SqlCommand cmd = new SqlCommand(sqlSelect.ToString(), _conn);
            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("@id", idPessoa));
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Contato contato = new Contato
                    {
                        IdContato = Convert.ToInt32(reader["ID_CONTATO"].ToString()),
                        Telefone = reader["TELEFONE"].ToString(),
                        Celular = reader["CELULAR"].ToString(),
                        IdPessoa = Convert.ToInt32(reader["ID_PESSOA"].ToString()),
                    };

                    listaContato.Add(contato);
                }
            }

            return listaContato;
        }

        public bool GetASExist(int id)
        {
            StringBuilder sqlSelect = new StringBuilder();

            sqlSelect.Append(@"SELECT 
                                    TELEFONE 
                                    FROM dbo.Contato
                                        WHERE ID_CONTATO =@id 
                                   ORDER BY Id_Contato ");

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

        public int Insert(Contato obj)
        {
            StringBuilder sqlInsert = new StringBuilder();

            sqlInsert.Append("INSERT INTO dbo.Contato ");
            if (!string.IsNullOrWhiteSpace(obj.Telefone) && !string.IsNullOrWhiteSpace(obj.Celular))
            {
                sqlInsert.Append(@"(TELEFONE,CELULAR, ID_Pessoa)
                                values
                             (@telefone, @celular,@idPessoa)");
            }
            else if (!string.IsNullOrWhiteSpace(obj.Telefone))
            {
                sqlInsert.Append(@"(TELEFONE, ID_Pessoa)
                                values
                             (@telefone,@idPessoa)");
            }
            else
            {
                sqlInsert.Append(@"(CELULAR, ID_Pessoa)
                                values
                             (@celular,@idPessoa)");
            }


            SqlCommand cmd = new SqlCommand(sqlInsert.ToString(), _conn);
            cmd.Transaction = _trans;
            if (!string.IsNullOrWhiteSpace(obj.Telefone))
                cmd.Parameters.Add(new SqlParameter("@telefone", obj.Telefone));
            if (!string.IsNullOrWhiteSpace(obj.Celular))
                cmd.Parameters.Add(new SqlParameter("@celular", obj.Celular));
            cmd.Parameters.Add(new SqlParameter("@idPessoa", obj.IdPessoa));

            return cmd.ExecuteNonQuery();
        }

        public int Update(Contato obj)
        {
            StringBuilder sqlUpdate = new StringBuilder();

            sqlUpdate.Append(@"UPDATE dbo.Contato
                                SET Telefone =@telefone
                                ,CELULAR=@celular
                             WHERE ID_CONTATO =@id ");

            SqlCommand cmd = new SqlCommand(sqlUpdate.ToString(), _conn);

            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("@telefone", obj.Telefone));
            cmd.Parameters.Add(new SqlParameter("@celular", obj.Celular));
            cmd.Parameters.Add(new SqlParameter("@id", obj.IdContato));

            return cmd.ExecuteNonQuery();
        }
        public int Delete(int id, int idPessoa)
        {
            StringBuilder sqlDelete = new StringBuilder();

            sqlDelete.Append("DELETE FROM dbo.Contato");
            if (id > 0)
                sqlDelete.Append(" WHERE ID_CONTATO =@id ");
            else
                sqlDelete.Append(" WHERE ID_PESSOA =@idPessoa ");

            SqlCommand cmd = new SqlCommand(sqlDelete.ToString(), _conn);
            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            cmd.Parameters.Add(new SqlParameter("@idPessoa", idPessoa));
            return cmd.ExecuteNonQuery();
        }


        public Contato PopularObjetoContato(SqlDataReader reader)
        {
            Contato contato = new Contato();
            contato.IdContato = Convert.ToInt32(reader["CONTATO"].ToString().Trim());
            contato.Telefone = reader["TELEFONE"].ToString().Trim();
            contato.Celular = reader["CELULAR"].ToString().Trim();

            return contato;
        }

        public Contato GetAsObject(Contato obj)
        {
            StringBuilder sqlSelect = new StringBuilder();

            sqlSelect.Append(@"SELECT ID_CONTATO ,
                                    TELEFONE , CELULAR , ID_PESSOA
                                FROM dbo.Contato
                                    WHERE 1=1");
            if (obj.IdPessoa > 0)
                sqlSelect.Append(" AND ID_PESSOA = @pessoa");
            if (obj.IdPessoa > 0)
                sqlSelect.Append(" AND ID_CONTATO = @contato");
            sqlSelect.Append(" ORDER BY Id_Contato ");

            SqlCommand cmd = new SqlCommand(sqlSelect.ToString(), _conn);
            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("@pessoa", obj.IdPessoa));
            cmd.Parameters.Add(new SqlParameter("@contato", obj.IdContato));
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    return new Contato
                    {
                        IdContato = Convert.ToInt32(reader["ID_CONTATO"].ToString()),
                        Telefone = reader["TELEFONE"].ToString(),
                        Celular = reader["CELULAR"].ToString(),
                        IdPessoa = Convert.ToInt32(reader["ID_PESSOA"].ToString()),
                    };
                }
                return new Contato();
            }
        }
    }
}
