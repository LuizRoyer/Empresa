using ApiEmpresa.DAO.Connections;
using ApiEmpresa.DAO.Repository;
using ApiEmpresa.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ApiEmpresa.Services
{
    public class ContatoService
    {
        SqlConnection conn = Connection.ConnectionSQL();

        public List<Contato> SelecionarContatos(int idPessoa)
        {
            try
            {
                conn.Open();
                return new ContatoDao(conn, conn.BeginTransaction()).ContatosList(idPessoa);
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
        public string InsertOrUpdate(Contato obj)
        {
            conn.Open();
            var trans = conn.BeginTransaction();
            try
            {
                ContatoDao contatoDao = new ContatoDao(conn, trans);
                
                if (!new PessoaDao(conn, trans).GetAsExist(obj.IdPessoa))
                    return "Identificador da Pessoa Invalido";

                if (contatoDao.GetASExist(obj.IdContato))
                {
                    contatoDao.Update(obj);
                }
                else
                {
                    contatoDao.Insert(obj);

                }

                trans.Commit();
                return "Sucesso";
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
        public int Delete(int id, int idPessoa)
        {
            conn.Open();
            var trans = conn.BeginTransaction();
            try
            {
                new ContatoDao(conn, trans).Delete(id, idPessoa);
                trans.Commit();
                return 200;
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
        public void ValidarContato(Contato contato, SqlTransaction trans)
        {
            if (!new ContatoDao(conn, trans).GetASExist(contato.IdContato))
                this.InsertOrUpdate(contato);
        }
    }
}
