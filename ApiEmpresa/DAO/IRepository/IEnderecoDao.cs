using ApiEmpresa.Models;
using ApiEmpresa.Models.ViewModels;
using System.Collections.Generic;

namespace ApiEmpresa.DAO.IRepository
{
    public interface IEnderecoDao
    {
        List<EnderecoView> EnderecosList();
        EnderecoView GetAsObject(Endereco obj);
        int Insert(Endereco obj);
        int Update(Endereco obj);
        int Delete(int id);
        bool GetAsExist(int id);
        bool GetExistDependent(int idEstado);
    }
}
