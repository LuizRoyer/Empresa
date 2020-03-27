using ApiEmpresa.DAO.IRepository;
using ApiEmpresa.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ApiEmpresa.DAO.Repository
{
    public class PesFisicaDao : IPesFisica
    {
        private readonly SqlConnection _conn;
        private readonly SqlTransaction _trans;

        public PesFisicaDao(SqlConnection conn, SqlTransaction trans)
        {
            this._conn = conn;
            this._trans = trans;
        }

        public List<PesFisica> PesFisicasList(int id)
        {
            List<PesFisica> listaPesFisica = new List<PesFisica>();
            StringBuilder sqlSelect = new StringBuilder();

            sqlSelect.Append(@"SELECT F.Id_PesFisica
                              ,F.Id_Pessoa
	                          ,P.Nome
                              ,P.Sobrenome                             
                              ,P.tipo
                              ,F.Cpf
                              ,F.DataNascimento
                              ,F.Rg
                          FROM dbo.PesFisica F,
		                        dbo.Pessoa P
								
	                                   WHERE F.Id_Pessoa =p.Id_Pessoa");
            if (id > 0)
                sqlSelect.Append("     AND F.Id_PesFisica =@id");
            sqlSelect.Append(" ORDER BY Id_PesFisica ");

            SqlCommand cmd = new SqlCommand(sqlSelect.ToString(), _conn);
            cmd.Parameters.Add(new SqlParameter("@id", id));
            cmd.Transaction = _trans;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    PesFisica pesFisica = new PesFisica
                    {
                        IdPesFisica = Convert.ToInt32(reader["ID_PESFISICA"].ToString()),
                        IdPessoa = Convert.ToInt32(reader["ID_PESSOA"].ToString()),
                        Nome = reader["NOME"].ToString().Trim(),
                        Sobrenome = reader["SOBRENOME"].ToString().Trim(),
                        Tipo = reader["TIPO"].ToString().Trim(),
                        Cpf = reader["CPF"].ToString().Trim(),
                        DataNascimento = reader["DATANASCIMENTO"] == DBNull.Value ? new DateTime(9999, 01, 01) : Convert.ToDateTime(reader["DATANASCIMENTO"]),
                        Rg = reader["RG"].ToString().Trim(),
                    };

                    listaPesFisica.Add(pesFisica);
                }
            }

            return listaPesFisica;
        }
        public bool GetAsExist(int id)
        {
            StringBuilder sqlSelect = new StringBuilder();

            sqlSelect.Append(@"SELECT  
                                      Id_Pessoa                                     
                                  FROM dbo.PesFisica
                                    WHERE Id_PesFisica =@id
                                   ORDER BY Id_PesFisica ");

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



        public int Insert(PesFisica obj)
        {
            StringBuilder sqlInsert = new StringBuilder();

            sqlInsert.Append(@"INSERT INTO dbo.PesFisica
                             (ID_PESSOA ,CPF ,DATANASCIMENTO,RG)
                                values
                             (@id_Pessoa ,@cpf,@datanascimento,@rg)");

            SqlCommand cmd = new SqlCommand(sqlInsert.ToString(), _conn);
            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("@id_Pessoa", obj.IdPessoa));
            cmd.Parameters.Add(new SqlParameter("@cpf", obj.Cpf));
            cmd.Parameters.Add(new SqlParameter("@datanascimento", obj.DataNascimento));
            cmd.Parameters.Add(new SqlParameter("@rg", obj.Rg));

            return cmd.ExecuteNonQuery();
        }

        public int Update(PesFisica obj)
        {
            StringBuilder sqlUpdate = new StringBuilder();

            sqlUpdate.Append(@"UPDATE dbo.PesFisica
                                SET CPF =@cpf
                                ,DATANASCIMENTO=@datanascimento
                                ,RG =@rg
                             WHERE ID_PESFISICA =@id ");

            SqlCommand cmd = new SqlCommand(sqlUpdate.ToString(), _conn);

            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("@cpf", obj.Cpf));
            cmd.Parameters.Add(new SqlParameter("@datanascimento", obj.DataNascimento));
            cmd.Parameters.Add(new SqlParameter("@rg", obj.Rg));
            cmd.Parameters.Add(new SqlParameter("@id", obj.IdPesFisica));
            return cmd.ExecuteNonQuery();
        }
        public int Delete(int id)
        {
            StringBuilder sqlDelete = new StringBuilder();

            sqlDelete.Append(@"DELETE FROM dbo.PesFisica
                                WHERE ID_PESFISICA =@id ");

            SqlCommand cmd = new SqlCommand(sqlDelete.ToString(), _conn);
            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            return cmd.ExecuteNonQuery();
        }
    }
}
