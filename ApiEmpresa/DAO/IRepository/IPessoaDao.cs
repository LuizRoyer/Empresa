using ApiEmpresa.Models;
using System.Collections.Generic;

namespace ApiEmpresa.DAO.IRepository
{
    public  interface IPessoaDao
    {
        List<Pessoa> PessoasList(int id);       
        int Insert(Pessoa obj);
        int Update(Pessoa obj);
        int Delete(int id);
        bool GetAsExist(int id);
        Pessoa GetAsObject(Pessoa obj);
    }
}
