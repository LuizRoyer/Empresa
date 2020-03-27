using ApiEmpresa.DAO.IRepository;
using ApiEmpresa.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ApiEmpresa.DAO.Repository
{
    public class CidadeDao : ICidadeDao
    {
        private readonly SqlConnection _conn;
        private readonly SqlTransaction _trans;

        public CidadeDao(SqlConnection conn, SqlTransaction trans)
        {
            this._conn = conn;
            this._trans = trans;
        }
        public List<Cidade> CidadeList()
        {
            List<Cidade> listaCidades = new List<Cidade>();
            StringBuilder sqlSelect = new StringBuilder();

            sqlSelect.Append(@"SELECT Id_Cidade ,
                                    Nome , Id_Estado
                                    FROM dbo.Cidade
                                   ORDER BY Id_Cidade ");

            SqlCommand cmd = new SqlCommand(sqlSelect.ToString(), _conn);
            cmd.Transaction = _trans;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Cidade cidade = new Cidade
                    {
                        Id_Cidade = Convert.ToInt32(reader["ID_CIDADE"].ToString()),
                        Nome = reader["NOME"].ToString(),
                        Estado = Convert.ToInt32(reader["ID_ESTADO"].ToString()),
                    };

                    listaCidades.Add(cidade);
                }
            }

            return listaCidades;
        }

        public int Delete(int id)
        {
            StringBuilder sqlDelete = new StringBuilder();

            sqlDelete.Append(@"DELETE FROM dbo.Cidade
                                WHERE ID_Cidade =@id ");

            SqlCommand cmd = new SqlCommand(sqlDelete.ToString(), _conn);
            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            return cmd.ExecuteNonQuery();
        }

        public bool GetAsExist(int id)
        {
            StringBuilder sqlSelect = new StringBuilder();

            sqlSelect.Append(@"SELECT 
                                    Nome 
                                    FROM dbo.Cidade
                                        WHERE ID_CIDADE =@id
                                   ORDER BY Id_Cidade ");

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

        public Cidade GetAsObject(Cidade cidade)
        {
            StringBuilder sqlSelect = new StringBuilder();

            sqlSelect.Append(@"SELECT Id_Cidade ,
                                    Nome , Id_Estado
                                    FROM dbo.Cidade
                                        WHERE 1=1");

            if (cidade.Id_Cidade > 0)
                sqlSelect.Append(" AND Id_Cidade = @id");

            if (!string.IsNullOrWhiteSpace(cidade.Nome))
                sqlSelect.Append(" AND NOME = @nome");

            if (cidade.Estado > 0)
                sqlSelect.Append(" AND Id_Estado = @estado");


            sqlSelect.Append(" ORDER BY Id_Cidade ");

            SqlCommand cmd = new SqlCommand(sqlSelect.ToString(), _conn);

            if (!string.IsNullOrWhiteSpace(cidade.Nome))
                cmd.Parameters.Add(new SqlParameter("@nome", cidade.Nome));
            if (cidade.Estado > 0)
                cmd.Parameters.Add(new SqlParameter("@estado", cidade.Estado));
            if (cidade.Id_Cidade > 0)
                cmd.Parameters.Add(new SqlParameter("@id", cidade.Id_Cidade));

            cmd.Transaction = _trans;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    return new Cidade
                    {
                        Id_Cidade = Convert.ToInt32(reader["ID_CIDADE"].ToString()),
                        Nome = reader["NOME"].ToString(),
                        Estado = Convert.ToInt32(reader["ID_ESTADO"].ToString()),
                    };
                }
            }

            return new Cidade();
        }

        public bool GetExistDependent(int idEstado)
        {
            StringBuilder sqlSelect = new StringBuilder();

            sqlSelect.Append(@"SELECT 
                                    Nome 
                                    FROM dbo.Cidade
                                        WHERE ID_ESTADO =@id
                                   ORDER BY Id_Cidade ");

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

        public int Insert(Cidade obj)
        {
            StringBuilder sqlInsert = new StringBuilder();

            sqlInsert.Append(@"INSERT INTO dbo.Cidade
                             (NOME, ID_ESTADO)
                                values
                             (@nome, @estado)");

            SqlCommand cmd = new SqlCommand(sqlInsert.ToString(), _conn);
            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("@nome", obj.Nome));
            cmd.Parameters.Add(new SqlParameter("@estado", obj.Estado));


            return cmd.ExecuteNonQuery();
        }

        public int Update(Cidade obj)
        {
            StringBuilder sqlUpdate = new StringBuilder();

            sqlUpdate.Append(@"UPDATE dbo.Cidade
                                SET NOME =@nome
                                ,ID_ESTADO=@estado
                             WHERE ID_CIDADE =@id ");

            SqlCommand cmd = new SqlCommand(sqlUpdate.ToString(), _conn);

            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("@nome", obj.Nome));
            cmd.Parameters.Add(new SqlParameter("@estado", obj.Estado));
            cmd.Parameters.Add(new SqlParameter("@id", obj.Id_Cidade));

            return cmd.ExecuteNonQuery();
        }

        public bool ValidarId(int id, string nome)
        {
            StringBuilder sqlSelect = new StringBuilder();

            sqlSelect.Append(@"SELECT 
                                    Nome 
                                    FROM dbo.Cidade
                                        WHERE ID_Cidade =@id 
                                            AND Nome =@nome
                                   ORDER BY Id_Cidade ");

            SqlCommand cmd = new SqlCommand(sqlSelect.ToString(), _conn);
            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            cmd.Parameters.Add(new SqlParameter("@nome", nome));
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                    return true;
                return false;
            }
        }
    }
}
