using ApiEmpresa.Models;
using System.Collections.Generic;

namespace ApiEmpresa.DAO.IRepository
{
    public interface IBairroDao
    {
        List<Bairro> BairrosList();
        Bairro GetAsObject(Bairro obj);
        int Insert(Bairro obj);
        int Update(Bairro obj);        
        int Delete(int id);
        bool GetAsExist(int id);
        bool GetExistDependent(int idCidade);
       
    }
}
