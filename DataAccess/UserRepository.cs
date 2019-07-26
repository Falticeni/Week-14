using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using System.Data.SqlClient;

namespace DataAccess
{

    public class Connection
    {
        public static SqlConnection sqlConnection { get; } = new SqlConnection("Data Source =.; Initial Catalog = Week14; Integrated Security = True");
    }

    public class UserRepository : Connection, IUserRepository
    {
        public List<User> GettAllUsers()
        {
            var list = new List<User>();

            string select = "Select * from USERS";
            SqlCommand selectUser = new SqlCommand(select, sqlConnection);
            sqlConnection.Open(); // open connection
            var reader = selectUser.ExecuteReader();
            while (reader.Read())
            {
                
                var useriId = (int)reader["id"];
                var username = reader["username"] as string;
                var email = reader["email"] as string;
                var city = reader["city"] as string;
                var description = reader["description"] as string;
                var street = reader["street"] as string;

                list.Add(new User
                {
                    Id = useriId,
                    UserName = username,
                    Email = email,
                    City = city,
                    Description = description,
                    Street = street,
                }
                    );
            }
            sqlConnection.Close();
            return list;
        }
    }

}




