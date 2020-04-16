using System.Data.SqlClient;

namespace HashWithSalt
{
    interface ISQLCon
    {
        SqlCommand Cmd { get;  }
        void Connect();
        void Disconnect();
    }
}
