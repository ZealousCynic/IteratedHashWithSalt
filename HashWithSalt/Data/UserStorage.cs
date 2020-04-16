using System.Collections.Generic;
using System.Data.SqlClient;

namespace HashWithSalt
{
    class UserStorage : IDataStorage<User>
    {
        ISQLCon con;

        public UserStorage()
        {
            con = new SqlCon();
        }

        public void Create(User entity)
        {
            con.Cmd.CommandText = "InsertUser";
            con.Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            con.Cmd.Parameters.AddRange(
                new[]
                {
                    new SqlParameter("Username", entity.Username),
                    new SqlParameter("Password", entity.Password)
                });

            con.Connect();

            con.Cmd.ExecuteNonQuery();

            con.Disconnect();
        }



        public User[] GetAll()
        {
            con.Cmd.CommandText = "GetAll";
            con.Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            con.Connect();

            SqlDataReader reader = con.Cmd.ExecuteReader();

            List<User> toRet = new List<User>();

            if (reader.HasRows)
                while (reader.Read())
                    toRet.Add(
                        new User
                        {
                            ID = (int)reader.GetValue(0),
                            Username = (string)reader.GetValue(1),
                            Password = (string)reader.GetValue(2)
                        }
                        );

            return toRet.ToArray();
        }

        public User Read(int id)
        {
            con.Cmd.CommandText = "GetUserById";
            con.Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            con.Cmd.Parameters.Add(new SqlParameter("ID", id));

            con.Connect();

            SqlDataReader reader = con.Cmd.ExecuteReader();

            User toRet = null;

            if (reader.HasRows)
                reader.Read();
            toRet = new User
            {
                ID = (int)reader.GetValue(0),
                Username = (string)reader.GetValue(1),
                Password = (string)reader.GetValue(2)
            };

            return toRet;
        }

        public User GetByPassword(string password)
        {
            con.Cmd.CommandText = "GetByPassword";
            con.Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            con.Cmd.Parameters.Add(new SqlParameter("Password", password));

            con.Connect();

            SqlDataReader reader = con.Cmd.ExecuteReader();

            User toRet = null;

            if (reader.HasRows)
            {
                reader.Read();
                toRet = new User
                {
                    ID = (int)reader.GetValue(0),
                    Username = (string)reader.GetValue(1),
                    Password = (string)reader.GetValue(2)
                };
            }

            return toRet;
        }

        /// <summary>
        /// Abuse of IDisposable
        /// </summary>
        public void Dispose()
        {

        }
    }
}
