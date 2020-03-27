using ApiEmpresa.DAO.Connections;
using ApiEmpresa.DAO.Repository;
using ApiEmpresa.Models;
using ApiEmpresa.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ApiEmpresa.Services
{
    public class EnderecoService
    {
        SqlConnection conn = Connection.ConnectionSQL();

        public List<EnderecoView> SelecionarEnderecos()
        {
            try
            {
                conn.Open();
                return new EnderecoDao(conn, conn.BeginTransaction()).EnderecosList();
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
        public string InsertOrUpdate(EnderecoView obj)
        {
            conn.Open();
            var trans = conn.BeginTransaction();
            try
            {
                EnderecoDao EnderecoDao = new EnderecoDao(conn, trans);
                obj = ValidarEstruturaEndereco(obj, trans);
                if (EnderecoDao.GetAsExist(obj.IdEndereco))
                {
                    EnderecoDao.Update(PopularParametrosEndereco(obj));
                }
                else
                {
                    EnderecoDao.Insert(PopularParametrosEndereco(obj));
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
        #region Metodos Auxliares 
        /// <summary>
        /// Metodo para validar se Possui Estado , Cidade  e Bairro
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public EnderecoView ValidarEstruturaEndereco(EnderecoView obj, SqlTransaction trans)
        {
            new EstadoService().InsertOrUpdate(PopularParametrosEstado(obj), trans, conn);
            obj.IdEstado = new EstadoDao(conn, trans).GetAsObject(new Estado { Nome = obj.NomeEstado, Uf = obj.Uf }).Id_Estado;

            new CidadeService().InsertOrUpdate(PopularParametrosCidade(obj), trans, conn);
            obj.IdCidade = new CidadeDao(conn, trans).GetAsObject(new Cidade { Nome = obj.Localidade, Estado = obj.IdEstado }).Id_Cidade;

            new BairroService().InsertOrUpdate(PopularParametrosBairro(obj), trans, conn);
            obj.IdBairro = new BairroDao(conn, trans).GetAsObject(new Bairro { Nome = obj.Bairro, Cidade = obj.IdCidade }).Id_Bairro;

            return obj;
        }

        private Bairro PopularParametrosBairro(EnderecoView obj)
        {
            Bairro bairro = new Bairro();
            bairro.Id_Bairro = obj.IdBairro;
            bairro.Cidade = obj.IdCidade;
            bairro.Nome = obj.Bairro;

            return bairro;
        }

        private Cidade PopularParametrosCidade(EnderecoView obj)
        {
            Cidade cidade = new Cidade();
            cidade.Id_Cidade = obj.IdCidade;
            cidade.Estado = obj.IdEstado;
            cidade.Nome = obj.Localidade;

            return cidade;
        }

        private Estado PopularParametrosEstado(EnderecoView obj)
        {
            Estado estado = new Estado();
            estado.Id_Estado = obj.IdEstado;
            estado.Uf = obj.Uf;
            estado.Nome = obj.NomeEstado;

            return estado;
        }

        private Endereco PopularParametrosEndereco(EnderecoView obj)
        {
            Endereco endereco = new Endereco();
            endereco.Id_Endereco = obj.IdEndereco;
            endereco.Id_Estado = obj.IdEstado;
            endereco.Id_Cidade = obj.IdCidade;
            endereco.Id_Bairro = obj.IdBairro;
            endereco.Logradouro = obj.Logradouro;
            endereco.Complemento = obj.Complemento;
            endereco.Cep = obj.Cep;
            return endereco;
        }
        #endregion

        public string Delete(int id)
        {
            conn.Open();
            var trans = conn.BeginTransaction();
            try
            {
                new EnderecoDao(conn, trans).Delete(id);

                trans.Commit();
                return "Excluido com Sucesso";
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
        public EnderecoView SelecionarEndereco(int id)
        {
            try
            {
                conn.Open();
                return new EnderecoDao(conn, conn.BeginTransaction()).GetAsObject(new Endereco { Id_Endereco = id });
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
