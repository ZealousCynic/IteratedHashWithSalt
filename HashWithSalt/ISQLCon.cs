using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashWithSalt
{
    interface ISQLCon
    {
        SqlCommand Cmd { get;  }
        void Connect();
        void Disconnect();
    }
}
