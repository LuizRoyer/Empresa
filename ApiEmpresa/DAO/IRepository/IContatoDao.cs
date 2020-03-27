using ApiEmpresa.Models;
using System.Collections.Generic;

namespace ApiEmpresa.DAO.IRepository
{
    public interface IContatoDao
    {
        List<Contato> ContatosList(int id);      
        int Insert(Contato obj);
        int Update(Contato obj);
        int Delete(int id , int idPessoa);
        bool GetASExist(int id);
        Contato GetAsObject(Contato obj);
    }
}
