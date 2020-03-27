using ApiEmpresa.DAO.Connections;
using ApiEmpresa.DAO.Repository;
using ApiEmpresa.Models;
using ApiEmpresa.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ApiEmpresa.Services
{
    public class FornecedorService
    {
        SqlConnection conn = Connection.ConnectionSQL();

        public List<FornecedorView> SelecionarFornecedores()
        {
            try
            {
                conn.Open();
                return new FornecedorDao(conn, conn.BeginTransaction()).FornecedoresList();
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
        public int InsertOrUpdate(Fornecedor obj)
        {
            conn.Open();
            var trans = conn.BeginTransaction();
            try
            {
                FornecedorDao fornecedorDao = new FornecedorDao(conn, trans);
                obj.DataCadastro = DateTime.Now;
                if (fornecedorDao.GetAsExist(obj.IdFornecedor))
                {                  
                    fornecedorDao.Update(obj);
                }
                else
                {
                    fornecedorDao.Insert(obj);
                }

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
        public int Delete(int id)
        {
            conn.Open();
            var trans = conn.BeginTransaction();
            try
            {
                FornecedorDao fornecedorDao = new FornecedorDao(conn, trans);                
                fornecedorDao.Delete(id);
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
    }
}

