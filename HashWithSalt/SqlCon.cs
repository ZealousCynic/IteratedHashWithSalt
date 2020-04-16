using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashWithSalt
{
    class SqlCon : ISQLCon, IDisposable
    {
        /// <summary>
        /// -- Lazy connection string may fuck this up with other SQL-server instances.
        /// Server: Loopback
        /// Security: Trusted
        /// </summary>
        readonly string conString = "Server=127.0.0.1; Initial Catalog=UserDB; Trusted_Connection=True;";

        public SqlConnection con;

        SqlCommand cmd;

        public SqlCommand Cmd { get { return cmd; } set { cmd = value; } }

        public SqlCon()
        {
            con = new SqlConnection(conString);
            Cmd = new SqlCommand();

            Cmd.Connection = con;
        }

        public void TestCon()
        {
            SqlCommand testCmd = new SqlCommand();

            testCmd.CommandText = "INSERT INTO Users(username, password) VALUES ('TESTCON','TESTCON');";
            testCmd.CommandType = System.Data.CommandType.Text;

            testCmd.Connection = con;

            con.Open();
            testCmd.ExecuteNonQuery();
            con.Close();
        }
        public void Connect()
        {
            con.Open();
        }

        public void Disconnect()
        {
            con.Close();
        }

        /// <summary>
        /// Not a proper implementation of dispose, but eh.
        /// </summary>
        public void Dispose()
        {
            if (con.State != System.Data.ConnectionState.Closed)
                con.Close();
        }


    }
}
