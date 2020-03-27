using ApiEmpresa.Models;
using ApiEmpresa.Models.ViewModels;
using System.Collections.Generic;

namespace ApiEmpresa.DAO.IRepository
{
    public interface IEmpresaDao
    {
        List<Empresa> EmpresaList();
        int Insert(Empresa obj);
        int Update(Empresa obj);
        int Delete(int id);
        bool GetASExist(int id);
        Empresa GetAsObject(int id);       
    }
}
