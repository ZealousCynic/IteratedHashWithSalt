using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashWithSalt
{
    class SqlSalt
    {
        SqlCon con;

        public SqlSalt()
        {
            con = new SqlCon();
        }

        //This is wrong AF  --- Quick testing
        public void StoreSalt(string salt)
        {
            Console.WriteLine("Saving salt...");
            con.Cmd.CommandText = "InsertSalt";
            con.Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Cmd.Parameters.Add(new SqlParameter("SaltValue", salt));
            con.Connect();
            con.Cmd.ExecuteNonQuery();
            con.Disconnect();
        }
        public byte[] GetSalt()
        {
            byte[] salt = null;
            con.Cmd.CommandText = "SELECT * FROM Salt";
            con.Connect();
            SqlDataReader reader = con.Cmd.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                salt = Convert.FromBase64String(reader.GetString(1));
            }

            con.Disconnect();

            return salt;
        }
    }
}
