using ApiEmpresa.Models;
using System.Collections.Generic;

namespace ApiEmpresa.DAO.IRepository
{
    public  interface IPesJuridica
    {
        List<PesJuridica> PesJuridicasList(int id);       
        int Insert(PesJuridica obj);
        int Update(PesJuridica obj);
        int Delete(int id);
        bool GetAsExist(int id);       
    }
}
