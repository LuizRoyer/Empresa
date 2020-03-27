using ApiEmpresa.DAO.Connections;
using ApiEmpresa.DAO.Repository;
using ApiEmpresa.Models;
using ApiEmpresa.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ApiEmpresa.Services
{
    public class EmpresaService
    {
        SqlConnection conn = Connection.ConnectionSQL();

        public List<Empresa> SelecionarEmpresas()
        {
            try
            {
                conn.Open();
                return new EmpresaDao(conn, conn.BeginTransaction()).EmpresaList();
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
        public string InsertOrUpdate(Empresa obj)
        {
            conn.Open();
            var trans = conn.BeginTransaction();
            try
            {
                EmpresaDao empresaDao = new EmpresaDao(conn, trans);

                string validacao = Validadar(obj, trans);

                if (string.IsNullOrWhiteSpace(validacao))
                {
                    if (empresaDao.GetASExist(obj.IdEmpresa))
                    {
                        empresaDao.Update(obj);
                    }
                    else
                    {
                        empresaDao.Insert(obj);
                    }

                    trans.Commit();
                    return "Sucesso";
                }
                else
                    return validacao;
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

        private string Validadar(Empresa obj, SqlTransaction trans)
        {
            if (obj.CNPJ.Length > 18)
                return "CNPJ Invalido";

            if (!new EnderecoDao(conn, trans).GetAsExist(obj.Endereco.IdEndereco))
            {
                return "Validar Endereco , Endereco Não Encontrado";
            }

            return string.Empty;
        }


        public string Delete(int id)
        {
            conn.Open();
            var trans = conn.BeginTransaction();
            try
            {
                EmpresaDao empresaDao = new EmpresaDao(conn, trans);
                Empresa empresa = empresaDao.GetAsObject(id);
                if (empresa.IdEmpresa > 0)
                {
                    if (!new EnderecoDao(conn, trans).GetExistDependent(empresa.Endereco.IdEndereco))
                        new EnderecoService().Delete(empresa.Endereco.IdEndereco);
                    empresaDao.Delete(id);

                    trans.Commit();
                    return "Sucesso";

                }
                else
                    return "Empresa não Encontrada";
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
    }
}
