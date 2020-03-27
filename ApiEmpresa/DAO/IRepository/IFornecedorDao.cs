using ApiEmpresa.Models;
using ApiEmpresa.Models.ViewModels;
using System.Collections.Generic;

namespace ApiEmpresa.DAO.IRepository
{
    public interface IFornecedorDao
    {
        List<FornecedorView> FornecedoresList();
        Fornecedor GetAsObject(int id);
        int Insert(Fornecedor obj);
        int Update(Fornecedor obj);
        int Delete(int id);
        bool GetAsExist(int id);       
        
    }
}
