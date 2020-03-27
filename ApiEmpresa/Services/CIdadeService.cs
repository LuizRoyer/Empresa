using ApiEmpresa.DAO.Connections;
using ApiEmpresa.DAO.Repository;
using ApiEmpresa.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ApiEmpresa.Services
{
    public class CidadeService
    {
        SqlConnection conn = Connection.ConnectionSQL();

        public List<Cidade> SelecionarCidades()
        {
            try
            {
                conn.Open();
                return new CidadeDao(conn, conn.BeginTransaction()).CidadeList();
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
        public string InsertOrUpdate(Cidade obj, SqlTransaction trans = null, SqlConnection conn = null)
        {
            bool IsOpen = true;
            try
            {               
                if (conn == null)
                {
                    IsOpen = false;
                    conn = Connection.ConnectionSQL();
                    conn.Open();
                    trans = conn.BeginTransaction();
                }

                CidadeDao cidadeDao = new CidadeDao(conn, trans);
                string retorno = "sucesso";
                if (new EstadoDao(conn, trans).GetAsObject(new Estado { Id_Estado = obj.Estado }).Id_Estado > 0)
                {
                    if (cidadeDao.GetAsObject(new Cidade { Nome = obj.Nome, Estado = obj.Estado }).Id_Cidade != 0)
                    {
                        if (cidadeDao.ValidarId(obj.Id_Cidade, obj.Nome))
                            cidadeDao.Update(obj);
                        else
                            retorno = "Cidade Não Encontrada";
                    }
                    else
                    {
                        cidadeDao.Insert(obj);
                    }

                    if(!IsOpen)
                    trans.Commit();
                }
                else
                    retorno = "Dados do Estado Invalidos";
                return retorno;
            }
            catch (Exception e)
            {
                trans.Rollback();
                throw new Exception(e.Message);
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
                CidadeDao cidadeDao = new CidadeDao(conn, trans);
                Cidade cidade = cidadeDao.GetAsObject(new Cidade { Id_Cidade = id });
                if (cidade != null)
                {
                    if (!new BairroDao(conn, trans).GetExistDependent(cidade.Id_Cidade))
                    {
                        new CidadeDao(conn, trans).Delete(id);
                        trans.Commit();
                        return "Sucesso";
                    }
                    else
                        return " Erro de Exclusão Bairro Vinculado a Cidade";
                }
                else
                    return "Erro Cidade Não Encontrada";
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
        public Cidade SelecionarCidade(Cidade cidade)
        {
            try
            {
                conn.Open();
                return new CidadeDao(conn, conn.BeginTransaction()).GetAsObject(cidade);
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
