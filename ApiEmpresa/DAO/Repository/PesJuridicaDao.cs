using ApiEmpresa.DAO.IRepository;
using ApiEmpresa.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ApiEmpresa.DAO.Repository
{
    public class PesJuridicaDao : IPesJuridica
    {
        private readonly SqlConnection _conn;
        private readonly SqlTransaction _trans;

        public PesJuridicaDao(SqlConnection conn, SqlTransaction trans)
        {
            this._conn = conn;
            this._trans = trans;
        }

        public List<PesJuridica> PesJuridicasList(int id)
        {
            List<PesJuridica> listaPesJuridica = new List<PesJuridica>();
            StringBuilder sqlSelect = new StringBuilder();

            sqlSelect.Append(@"SELECT
                              J.Id_PesJuridica
                              ,J.Id_Pessoa
	                          ,P.Nome
                              ,P.Sobrenome                              
                              ,P.tipo
                              ,J.Cnpj                             
                          FROM dbo.PesJuridica J,
		                        dbo.Pessoa P								
	                                WHERE J.Id_Pessoa = p.Id_Pessoa");
            if (id > 0)
                sqlSelect.Append(" AND J.Id_PesJuridica = @id");
            sqlSelect.Append(" ORDER BY Id_PesJuridica ");

            SqlCommand cmd = new SqlCommand(sqlSelect.ToString(), _conn);
            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("id", id));
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    PesJuridica pesJuridica = new PesJuridica
                    {
                        IdPesJuridica = Convert.ToInt32(reader["ID_PESJURIDICA"].ToString()),
                        IdPessoa = Convert.ToInt32(reader["ID_PESSOA"].ToString()),
                        Nome = reader["NOME"].ToString().Trim(),
                        Sobrenome = reader["SOBRENOME"].ToString().Trim(),
                        Tipo = reader["TIPO"].ToString().Trim(),
                        Cnpj = reader["CNPJ"].ToString().Trim()
                    };

                    listaPesJuridica.Add(pesJuridica);
                }
            }

            return listaPesJuridica;
        }

        public bool GetAsExist(int id)
        {
            StringBuilder sqlSelect = new StringBuilder();

            sqlSelect.Append(@"SELECT                              	                         
                                    Cnpj                             
                          FROM dbo.PesJuridica 		                        
	                                WHERE Id_PesJuridica =@id
                          ORDER BY Id_PesJuridica ");

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

        public int Insert(PesJuridica obj)
        {
            StringBuilder sqlInsert = new StringBuilder();

            sqlInsert.Append(@"INSERT INTO dbo.PesJuridica
                             (ID_PESSOA ,CNPJ)
                                values
                             (@id_Pessoa ,@cnpj)");

            SqlCommand cmd = new SqlCommand(sqlInsert.ToString(), _conn);
            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("@id_Pessoa", obj.IdPessoa));
            cmd.Parameters.Add(new SqlParameter("@cnpj", obj.Cnpj));

            return cmd.ExecuteNonQuery();
        }
        public int Update(PesJuridica obj)
        {
            StringBuilder sqlUpdate = new StringBuilder();

            sqlUpdate.Append(@"UPDATE dbo.PesJuridica
                                SET CNPJ =@cnpj                                
                             WHERE ID_PESJURIDICA =@id ");

            SqlCommand cmd = new SqlCommand(sqlUpdate.ToString(), _conn);

            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("@cnpj", obj.Cnpj));
            cmd.Parameters.Add(new SqlParameter("@id", obj.IdPesJuridica));

            return cmd.ExecuteNonQuery();
        }
        public int Delete(int id)
        {
            StringBuilder sqlDelete = new StringBuilder();

            sqlDelete.Append(@"DELETE FROM dbo.PesJuridica
                                WHERE ID_PESJURIDICA =@id ");

            SqlCommand cmd = new SqlCommand(sqlDelete.ToString(), _conn);
            cmd.Transaction = _trans;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            return cmd.ExecuteNonQuery();
        }
    }
}
