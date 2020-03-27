using ApiEmpresa.DAO.Connections;
using ApiEmpresa.DAO.Repository;
using ApiEmpresa.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ApiEmpresa.Services
{
    public class BairroService
    {
        SqlConnection conn = Connection.ConnectionSQL();

        public List<Bairro> SelecionarBairros()
        {
            try
            {
                conn.Open();
                return new BairroDao(conn, conn.BeginTransaction()).BairrosList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        public string InsertOrUpdate(Bairro obj, SqlTransaction trans = null, SqlConnection conn = null)
        {
            string retorno = "Sucesso"; bool IsOpen = true;
            try
            {
                if (conn == null)
                {
                    IsOpen = false;
                    conn = Connection.ConnectionSQL();
                    conn.Open();
                    trans = conn.BeginTransaction();
                }
                if (new CidadeDao(conn, trans).GetAsObject(new Cidade { Id_Cidade = obj.Cidade }).Id_Cidade > 0)
                {
                    BairroDao BairroDao = new BairroDao(conn, trans);

                    if (BairroDao.GetAsObject(new Bairro {Nome = obj.Nome , Cidade= obj.Cidade }).Id_Bairro != 0)
                    {
                        BairroDao.Update(obj);
                    }
                    else
                    {
                        BairroDao.Insert(obj);
                    }

                    if (!IsOpen)
                        trans.Commit();
                }
                else
                    retorno = "Cidade não Cadastrada";

                return retorno;
            }
            catch (Exception e)
            {
                trans.Rollback();

                return e.Message;
            }
            finally
            {
                if (!IsOpen)
                    conn.Close();
            }
        }
        public string Delete(int id)
        {
            conn.Open();
            var trans = conn.BeginTransaction();
            try
            {
                BairroDao bairroDao = new BairroDao(conn, trans);
                if (bairroDao.GetAsExist(id))
                {
                    bairroDao.Delete(id);

                    trans.Commit();
                    return "Sucesso";
                }
                else
                    return "Erro ao Excluir Bairro, bairro não Encontrado";

            }
            catch (Exception e)
            {
                trans.Rollback();
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        public Bairro SelecionarBairro(Bairro bairro)
        {
            try
            {
                conn.Open();
                return new BairroDao(conn, conn.BeginTransaction()).GetAsObject(bairro);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
