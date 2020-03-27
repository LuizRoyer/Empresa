using System.Data.SqlClient;

namespace ApiEmpresa.DAO.Connections
{
    public class Connection
    {
        const string connSQL = @"Data Source=DESKTOP-1V9L1UF\SQLEXPRESS;Initial Catalog=BluData;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static SqlConnection ConnectionSQL()
        {
            return new SqlConnection(connSQL);
        }
    }
}
