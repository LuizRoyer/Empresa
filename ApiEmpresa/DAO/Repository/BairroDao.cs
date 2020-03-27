using ApiEmpresa.DAO.IRepository;
using ApiEmpresa.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ApiEmpresa.DAO.Repository
{
    public class BairroDao : IBairroDao
    {
        private readonly SqlConnection _conn;
        private readonly SqlTransaction _trans;

        public BairroDao(SqlConnection conn, SqlTransaction trans)
        {
            this._conn = conn;
            this._trans = trans;
        }

        public List<Bairro> BairrosList()
        {
            List<Bairro> listaBairro = new List<Bairro>();
            StringBuilder sqlSelect = new StringBuilder();

            sqlSelect.Append(@"SELECT Id_Bairro ,
                                    Nome , Id_Cidade
                                    FROM dbo.Bairro
                                   ORDER BY Id_Bairro ");

            SqlCommand cmd = new SqlCommand(sqlSelect.ToString(), _conn);
            cmd.Transaction = _trans;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Bairro bairro = new Bairro
                    {
                        Id_Bairro = Convert.ToInt32(reader["ID_BAIRRO"].ToString()),
                        Nome = reader["NOME"].ToString(),
                        Cidade = Convert.ToInt32(reader["ID_CIDADE"].ToString())
                    };

                    listaBairro.Add(bairro);
                }
            }

            return listaBairro;
        }

        public bool GetAsExist(int id)
        {
            StringBuilder sqlSelect = new StringBuilder();

            sqlSelect.Append(@"SELECT 
                                    Nome 
                                    FROM dbo.Bairro
                                       WHERE Id_Bairro =@id 
                                   ORDER BY Id_Bairro ");

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

        public int Insert(Bairro obj)
        {
            StringBuilder sqlInsert = new StringBuilder();

            sqlInsert.Append(@"INSERT INTO dbo.Bairro
                             (NOME, Id_Cidade)
                                values
                             (@nome, @cidade)");

            SqlCommand cmd = new SqlCommand(sqlInsert.ToString(), _conn);
            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("@nome", obj.Nome));
            cmd.Parameters.Add(new SqlParameter("@cidade", obj.Cidade));

            return cmd.ExecuteNonQuery();
        }

        public int Update(Bairro obj)
        {
            StringBuilder sqlUpdate = new StringBuilder();

            sqlUpdate.Append(@"UPDATE dbo.Bairro
                                SET NOME =@nome
                                ,Id_Cidade=@cidade
                             WHERE Id_Bairro =@id ");

            SqlCommand cmd = new SqlCommand(sqlUpdate.ToString(), _conn);

            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("@nome", obj.Nome));
            cmd.Parameters.Add(new SqlParameter("@cidade", obj.Cidade));
            cmd.Parameters.Add(new SqlParameter("@id", obj.Id_Bairro));

            return cmd.ExecuteNonQuery();
        }
        public int Delete(int id)
        {
            StringBuilder sqlDelete = new StringBuilder();

            sqlDelete.Append(@"DELETE FROM dbo.Bairro
                                WHERE Id_Bairro =@id ");

            SqlCommand cmd = new SqlCommand(sqlDelete.ToString(), _conn);
            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            return cmd.ExecuteNonQuery();
        }

        public bool GetExistDependent(int idCidade)
        {
            StringBuilder sqlSelect = new StringBuilder();

            sqlSelect.Append(@"SELECT 
                                    Nome 
                                    FROM dbo.Bairro
                                        WHERE Id_Cidade =@id
                                   ORDER BY Id_Bairro ");

            SqlCommand cmd = new SqlCommand(sqlSelect.ToString(), _conn);
            cmd.Parameters.Add(new SqlParameter("@id", idCidade));
            cmd.Transaction = _trans;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                    return true;
                return false;
            }
        }

        public Bairro GetAsObject(Bairro obj)
        {
            StringBuilder sqlSelect = new StringBuilder();

            sqlSelect.Append(@"SELECT Id_Bairro ,
                                    Nome , Id_Cidade
                                    FROM dbo.Bairro 
                                       WHERE 1=1");

            if (obj.Id_Bairro > 0)
                sqlSelect.Append(" AND Id_Bairro = @id");
            if (!string.IsNullOrWhiteSpace(obj.Nome))
                sqlSelect.Append(" AND Nome = @nome");
            if (obj.Cidade > 0)
                sqlSelect.Append(" AND Id_Cidade = @cidade");

            sqlSelect.Append(" ORDER BY Id_Bairro ");

            SqlCommand cmd = new SqlCommand(sqlSelect.ToString(), _conn);

            if (!string.IsNullOrWhiteSpace(obj.Nome))
                cmd.Parameters.Add(new SqlParameter("@nome", obj.Nome));
            if (obj.Cidade > 0)
                cmd.Parameters.Add(new SqlParameter("@cidade", obj.Cidade));
            if (obj.Id_Bairro > 0)
                cmd.Parameters.Add(new SqlParameter("@id", obj.Id_Bairro));

            cmd.Transaction = _trans;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    return new Bairro
                    {
                        Id_Bairro = Convert.ToInt32(reader["ID_BAIRRO"].ToString()),
                        Nome = reader["NOME"].ToString(),
                        Cidade = Convert.ToInt32(reader["ID_CIDADE"].ToString())
                    };
                }
            }

            return new Bairro();
        }

    }
}
