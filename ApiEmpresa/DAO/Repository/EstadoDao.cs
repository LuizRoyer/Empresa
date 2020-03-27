using ApiEmpresa.DAO.IRepository;
using ApiEmpresa.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ApiEmpresa.DAO.Repository
{
    public class EstadoDao : IEstadoDao
    {
        private readonly SqlConnection _conn;
        private readonly SqlTransaction _trans;

        public EstadoDao(SqlConnection conn, SqlTransaction trans)
        {
            this._conn = conn;
            this._trans = trans;
        }

        public List<Estado> EstadosList()
        {
            List<Estado> listaEstados = new List<Estado>();
            StringBuilder sqlSelect = new StringBuilder();

            sqlSelect.Append(@"SELECT Id_Estado ,
                                    Nome , UF
                                    FROM dbo.Estado
                                   ORDER BY Id_Estado ");

            SqlCommand cmd = new SqlCommand(sqlSelect.ToString(), _conn);
            cmd.Transaction = _trans;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Estado estado = new Estado
                    {
                        Id_Estado = Convert.ToInt32(reader["ID_ESTADO"].ToString()),
                        Nome = reader["NOME"].ToString(),
                        Uf = reader["UF"].ToString().Trim()
                    };

                    listaEstados.Add(estado);
                }
            }

            return listaEstados;
        }

        public bool GetAsExist(int id)
        {
            StringBuilder sqlSelect = new StringBuilder();

            sqlSelect.Append(@"SELECT 
                                    Nome 
                                    FROM dbo.Estado");
            sqlSelect.Append(" WHERE  Id_Estado =@id");
            sqlSelect.Append("         ORDER BY Id_Estado ");

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

        public int Insert(Estado obj)
        {
            StringBuilder sqlInsert = new StringBuilder();

            sqlInsert.Append(@"INSERT INTO dbo.Estado
                             (NOME, UF)
                                values
                             (@nome, @uf)");

            SqlCommand cmd = new SqlCommand(sqlInsert.ToString(), _conn);
            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("@nome", obj.Nome));
            cmd.Parameters.Add(new SqlParameter("@uf", obj.Uf));

            return cmd.ExecuteNonQuery();
        }

        public int Update(Estado obj)
        {
            StringBuilder sqlUpdate = new StringBuilder();

            sqlUpdate.Append(@"UPDATE dbo.Estado
                                SET NOME =@nome
                                ,UF=@uf
                             WHERE ID_ESTADO =@id ");

            SqlCommand cmd = new SqlCommand(sqlUpdate.ToString(), _conn);

            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("@nome", obj.Nome));
            cmd.Parameters.Add(new SqlParameter("@uf", obj.Uf));
            cmd.Parameters.Add(new SqlParameter("@id", obj.Id_Estado));

            return cmd.ExecuteNonQuery();
        }
        public int Delete(int id)
        {
            StringBuilder sqlDelete = new StringBuilder();

            sqlDelete.Append(@"DELETE FROM dbo.Estado
                                WHERE ID_ESTADO =@id ");

            SqlCommand cmd = new SqlCommand(sqlDelete.ToString(), _conn);
            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            return cmd.ExecuteNonQuery();
        }

        public Estado GetAsObject(Estado obj)
        {
            StringBuilder sqlSelect = new StringBuilder();
            sqlSelect.Append(@"SELECT Id_Estado ,
                                    Nome , UF
                                    FROM dbo.Estado");
            sqlSelect.Append(" WHERE 1=1 ");

            if (obj.Id_Estado > 0)
                sqlSelect.Append(" AND  Id_Estado = @id ");
            if (!string.IsNullOrWhiteSpace(obj.Nome))
                sqlSelect.Append(" AND  NOME = @nome ");
            if (!string.IsNullOrWhiteSpace(obj.Uf))
                sqlSelect.Append(" AND  UF = @uf ");

            sqlSelect.Append("         ORDER BY Id_Estado ");

            SqlCommand cmd = new SqlCommand(sqlSelect.ToString(), _conn);

            if (!string.IsNullOrWhiteSpace(obj.Nome))
                cmd.Parameters.Add(new SqlParameter("@nome", obj.Nome));
            if (!string.IsNullOrWhiteSpace(obj.Uf))
                cmd.Parameters.Add(new SqlParameter("@uf", obj.Uf));
            if (obj.Id_Estado > 0)
                cmd.Parameters.Add(new SqlParameter("@id", obj.Id_Estado));

            cmd.Transaction = _trans;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    return new Estado
                    {
                        Id_Estado = Convert.ToInt32(reader["ID_ESTADO"].ToString()),
                        Nome = reader["NOME"].ToString(),
                        Uf = reader["UF"].ToString().Trim()
                    };
                }
            }

            return new Estado();
        }
    }
}
