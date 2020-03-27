using ApiEmpresa.Models;
using System.Collections.Generic;

namespace ApiEmpresa.DAO.IRepository
{
    public interface ICidadeDao
    {
        List<Cidade> CidadeList();
        Cidade GetAsObject(Cidade cidade);
        int Insert(Cidade obj);
        int Update(Cidade obj);
        int Delete(int id);
        bool GetAsExist(int id);
        bool GetExistDependent(int idEstado);   
    }
}
