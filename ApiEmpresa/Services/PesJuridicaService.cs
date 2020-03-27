using ApiEmpresa.DAO.Connections;
using ApiEmpresa.DAO.Repository;
using ApiEmpresa.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ApiEmpresa.Services
{
    public class PesJuridicaService
    {
        SqlConnection conn = Connection.ConnectionSQL();

        public List<PesJuridica> SelecionarPesJuridica(int id)
        {
            try
            {
                conn.Open();
                return new PesJuridicaDao(conn, conn.BeginTransaction()).PesJuridicasList(id);
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
        public string InsertOrUpdate(PesJuridica obj)
        {
            conn.Open();
            var trans = conn.BeginTransaction();
            try
            {
                PesJuridicaDao PesJuridicaDao = new PesJuridicaDao(conn, trans);
                PessoaDao pessoaDao = new PessoaDao(conn, trans);

                if (obj.Cnpj.Length > 18)
                    return "CNPJ Invalido";
                if (PesJuridicaDao.GetAsExist(obj.IdPesJuridica))
                {                                         
                    pessoaDao.Update(PupularParametrosPessoa(obj));
                    PesJuridicaDao.Update(obj);
                }
                else
                {
                    pessoaDao.Insert(PupularParametrosPessoa(obj));
                    obj.IdPessoa = 0;
                    obj.IdPessoa = pessoaDao.GetAsObject(PupularParametrosPessoa(obj)).IdPessoa;
                    PesJuridicaDao.Insert(obj);
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
        
        private Pessoa PupularParametrosPessoa(PesJuridica obj)
        {
            Pessoa objPessoa = new Pessoa();
            objPessoa.Nome = obj.Nome;
            objPessoa.IdPessoa = obj.IdPessoa;
            objPessoa.Sobrenome = obj.Sobrenome;
            objPessoa.Tipo = "J";

            return objPessoa;
        }

        public string Delete(int id)
        {
            conn.Open();
            var trans = conn.BeginTransaction();
            try
            {
                PesJuridicaDao pesJuridicaDao = new PesJuridicaDao(conn, trans);
                List<PesJuridica> listaPessoaJur = pesJuridicaDao.PesJuridicasList(id);
                
                if (listaPessoaJur.Count > 0)
                {
                    new PessoaDao(conn, trans).Delete(listaPessoaJur[0].IdPessoa);
                    new ContatoDao(conn, trans).Delete(0, listaPessoaJur[0].IdPessoa);
                    pesJuridicaDao.Delete(id);
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
