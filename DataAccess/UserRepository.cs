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
        public List<User> GetAllUsers()
        {
            var list = new List<User>();

            string select = "Select * from USERS";
            SqlCommand selectUsers = new SqlCommand(select, sqlConnection);
            sqlConnection.Open();
            var reader = selectUsers.ExecuteReader();
            while (reader.Read())
            {
                var useriId = (int)reader["id"];
                var username = reader["username"] as string;
                var email = reader["email"] as string;
                var description = reader["description"] as string;
                var city = reader["city"] as string;
                var street = reader["street"] as string;

                list.Add(new User
                {
                    Id = useriId,
                    UserName = username,
                    Email = email,
                    Description = description,
                    City = city,
                    Street = street,
                });
            }
            sqlConnection.Close();
            return list;
        }


        public void AddUser(string username, string email, string city, string description, string street)
        {
            string add = "INSERT INTO USERS VALUES(@username, @email, @description, @city, @street)";
            SqlCommand sqlCommand = new SqlCommand(add, sqlConnection);
            sqlConnection.Open();
            sqlCommand.Parameters.AddRange(
                new SqlParameter[] {
                    new SqlParameter { ParameterName = "username", Value = username },
                    new SqlParameter { ParameterName = "email", Value = email },
                    new SqlParameter { ParameterName = "city", Value = city },
                    new SqlParameter { ParameterName = "description", Value = description },
                    new SqlParameter { ParameterName = "street", Value = street },
            });
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public void EditUser(int id, string userName, string email, string description, string city, string street)
        {
            sqlConnection.Open();
            string sqlString = "UPDATE USERS SET USERNAME = @userName, EMAIL = @email, DESCRIPTION = @description, CITY = @city, STREET = @street WHERE ID = @id";
            SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection);
            sqlCommand.Parameters.AddRange(
                new SqlParameter[]
                {
                    new SqlParameter { ParameterName = "id", Value = id},
                    new SqlParameter { ParameterName = "userName", Value = userName },
                    new SqlParameter { ParameterName = "email", Value = email },
                    new SqlParameter { ParameterName = "description", Value = description },
                    new SqlParameter { ParameterName = "city", Value = city },
                    new SqlParameter { ParameterName = "street", Value = street },
                });

            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public int GetLastUserId()
        {
            string sqlString = "SELECT MAX(ID) FROM USERS";
            SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection);
            sqlConnection.Open();
            int id = (int)sqlCommand.ExecuteScalar();
            sqlConnection.Close();
            return id;
        }



        public User GetUserById(int id)
        {
            User user = new User();
            string sqlString = "SELECT * FROM USERS WHERE ID = @id";
            SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection);
            sqlCommand.Parameters.Add(new SqlParameter { ParameterName = "id", Value = id });
            sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                user.Id = (int)reader["ID"];
                user.UserName = reader["USERNAME"] as string;
                user.Email = reader["EMAIL"] as string;
                user.Description = reader["DESCRIPTION"] as string;
                user.City = reader["CITY"] as string;
                user.Street = reader["STREET"] as string;
            }

            sqlConnection.Close();
            return user;
        }

        public void DeleteUser(int id)
        {
            string sqlString = @"DELETE FROM USERS WHERE ID = @id";
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection);
            sqlCommand.Parameters.Add(new SqlParameter { ParameterName = "id", Value = id });
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
        public IList<User> GetAllUsers(int id)
        {
            string sqlString = "SELECT * FROM USERS WHERE ID BETWEEN @id*10-10+1 AND @id*10";
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sqlString, sqlConnection);
            sqlCommand.Parameters.AddRange(new SqlParameter[]
            {
                new SqlParameter {ParameterName = "id", Value = id},
            });

            SqlDataReader reader = sqlCommand.ExecuteReader();
            IList<User> users = new List<User>();

            while (reader.Read())
            {
                users.Add(new User
                {
                    Id = (int)reader["ID"],
                    UserName = reader["USERNAME"] as string,
                    Email = reader["EMAIL"] as string,
                    Description = reader["DESCRIPTION"] as string,
                    City = reader["CITY"] as string,
                    Street = reader["STREET"] as string
                });
            }

            sqlConnection.Close();
            return users;
        }
    }
}




