using ApiEmpresa.DAO.Connections;
using ApiEmpresa.DAO.Repository;
using ApiEmpresa.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace ApiEmpresa.Services
{
    public class PesFisicaService
    {
        SqlConnection conn = Connection.ConnectionSQL();

        public List<PesFisica> SelecionarPesFisica(int id)
        {
            try
            {
                conn.Open();
                return new PesFisicaDao(conn, conn.BeginTransaction()).PesFisicasList(id);
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
        public string InsertOrUpdate(PesFisica obj)
        {
            conn.Open();
            var trans = conn.BeginTransaction();
            try
            {
                PesFisicaDao PesFisicaDao = new PesFisicaDao(conn, trans);
                PessoaDao pessoaDao = new PessoaDao(conn, trans);

                if (obj.Cpf.Length > 14)
                    return "CPF Invalido";
                if (PesFisicaDao.GetAsExist(obj.IdPesFisica))
                {
                    pessoaDao.Update(PupularParametrosPessoa(obj));
                    PesFisicaDao.Update(obj);
                }
                else
                {
                    pessoaDao.Insert(PupularParametrosPessoa(obj));
                    obj.IdPessoa = 0;
                    obj.IdPessoa = pessoaDao.GetAsObject(PupularParametrosPessoa(obj)).IdPessoa;
                    PesFisicaDao.Insert(obj);
                }

                trans.Commit();
                return "Sucesso";
            }
            catch (Exception e)
            {
                trans.Rollback();
                return e.Message;
            }
            finally
            {
                conn.Close();
            }
        }

        private Pessoa PupularParametrosPessoa(PesFisica obj)
        {
            Pessoa objPessoa = new Pessoa();
            objPessoa.Nome = obj.Nome;
            objPessoa.IdPessoa = obj.IdPessoa;
            objPessoa.Sobrenome = obj.Sobrenome;
            objPessoa.Tipo = "F";         

            return objPessoa;
        }

        public string Delete(int id)
        {
            conn.Open();
            var trans = conn.BeginTransaction();
            try
            {
                PesFisicaDao pesFisicaDao = new PesFisicaDao(conn, trans);
                List<PesFisica> listapesFisica = pesFisicaDao.PesFisicasList(id);

                if (listapesFisica.Count > 0)
                {
                    new PessoaDao(conn, trans).Delete(listapesFisica[0].IdPessoa);
                    new ContatoDao(conn, trans).Delete(0, listapesFisica[0].IdPessoa);
                    pesFisicaDao.Delete(id);
                    trans.Commit();
                    return "Sucesso";
                }
                return "Erro Exclusao Pessoa não Encontrada";
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
    }
}
