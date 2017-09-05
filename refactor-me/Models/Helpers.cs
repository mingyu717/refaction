using System.Data.SqlClient;
using System.Web;

namespace refactor_me.Models
{
    public class Helpers
    {
        private const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={DataDirectory}\Database.mdf;Integrated Security=True";

        public static SqlConnection NewConnection(string dataDirectory)
        {
            var connstr = ConnectionString.Replace("{DataDirectory}", dataDirectory);
            return new SqlConnection(connstr);
        }
    }
}