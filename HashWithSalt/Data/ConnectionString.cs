using System.Configuration;

namespace HashWithSalt.Data
{
    class ConnectionString
    {
        public static string GetConnection()
        {
            return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
    }
}
