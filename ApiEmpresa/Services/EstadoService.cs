using ApiEmpresa.DAO.Connections;
using ApiEmpresa.DAO.Repository;
using ApiEmpresa.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ApiEmpresa.Services
{
    public  class EstadoService
    {
        SqlConnection conn = Connection.ConnectionSQL();

        public List<Estado> SelecionarEstados()
        {
            try
            {
                conn.Open();
                return new EstadoDao(conn, conn.BeginTransaction()).EstadosList();
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
        public string InsertOrUpdate(Estado obj, SqlTransaction trans = null, SqlConnection conn = null)
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
                EstadoDao estadoDao = new EstadoDao(conn, trans);

                if (estadoDao.GetAsObject(new Estado { Nome = obj.Nome, Uf = obj.Uf }).Id_Estado !=0 )
                {
                    estadoDao.Update(obj);
                }
                else
                {
                    estadoDao.Insert(obj);
                }

                if (!IsOpen)
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
                EstadoDao estadoDao = new EstadoDao(conn, trans);
                Estado estado = estadoDao.GetAsObject(new Estado { Id_Estado = id });
                if (estado.Id_Estado > 0)
                {
                    if (!new CidadeDao(conn, trans).GetExistDependent(estado.Id_Estado))
                    {
                        if (!new EnderecoDao(conn, trans).GetExistDependent(estado.Id_Estado))
                        {
                            estadoDao.Delete(id);
                            trans.Commit();
                            return "Sucesso";
                        }else
                            return "Erro de Exclusão Estado Vinculada ao Endereco";
                    }
                    else
                        return "Erro de Exclusão Cidade Vinculada ao Estado";
                }
                else
                    return "Erro Estado Não Encontrado";
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
        public Estado SelecionarEstado(Estado estado)
        {
            try
            {
                conn.Open();          
                    return new EstadoDao(conn, conn.BeginTransaction()).GetAsObject(estado);
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
