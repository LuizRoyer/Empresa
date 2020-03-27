using ApiEmpresa.Models;
using System.Collections.Generic;

namespace ApiEmpresa.DAO.IRepository
{
    public interface IEstadoDao
    {
        List<Estado> EstadosList();
        int Insert(Estado estado);
        int Update(Estado estado);        
        int Delete(int id);
        bool GetAsExist(int id);
        Estado GetAsObject(Estado obj);       
    }
}
