using ApiEmpresa.Models;
using System.Collections.Generic;

namespace ApiEmpresa.DAO.IRepository
{
    public interface IPesFisica
    {
        List<PesFisica> PesFisicasList(int id);        
        int Insert(PesFisica obj);
        int Update(PesFisica obj);
        int Delete(int id);
        bool GetAsExist(int id);
        
    }
}
