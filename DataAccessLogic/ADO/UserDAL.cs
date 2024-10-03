using DataAccessLogic.Interfaces;
using DTO.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.ADO
{
    public class UserDAL : IUserDAL
    {
        private readonly SqlConnection connection;
        private readonly string connectionStr;
        public UserDAL(string test1 = "")
        {
            if (test1 == "test")
            {
                connectionStr = "Data Source=DESKTOP-NALH133;Initial Catalog=Administrator_Service_Test;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                //Data Source=DESKTOP-NALH133;Initial Catalog=Administrator_Service_Test;Integrated Security=True;Encrypt=True;Trust Server Certificate=True
            }
            else
            {
                connectionStr = "Data Source=DESKTOP-NALH133;Initial Catalog=Administrator_Service;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
            }
            connection = new SqlConnection(connectionStr);
        }

        public List<User> GetAllUsers()
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM tblUser";

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                List<User> users = new List<User>();
                while (reader.Read())
                {
                    users.Add(new User
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        Username = reader["Username"].ToString(),
                        Email = reader["Email"].ToString(),
                        IsBlocked = (bool)reader["IsBlocked"]
                    });
                }
                connection.Close();
                return users;
            }
        }

        public User SearchUserById(int userId)
        {
            User user = null; 
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT UserID, Username, Email, IsBlocked FROM tblUser WHERE UserID = @userId";
                command.Parameters.AddWithValue("@userId", userId);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read()) 
                    {
                        user = new User
                        {
                            UserID = Convert.ToInt32(reader["UserID"]),
                            Username = reader["Username"].ToString(),
                            Email = reader["Email"].ToString(),
                            IsBlocked = (bool)reader["IsBlocked"] 
                        };
                    }
                }
                connection.Close();
            }
            return user;
        }


        public List<User> SortUsers(string sortBy, string order)
        {
            //var users = new List<User>();
            //using (SqlCommand command = connection.CreateCommand())
            //{
            //    string query = $"SELECT UserID,Username,Email FROM tblUser ORDER BY {sortBy} {order}";
            //    command.CommandText = query;

            //    connection.Open();
            //    using (var reader = command.ExecuteReader())
            //    {
            //        while (reader.Read())
            //        {
            //            users.Add(new User
            //            {
            //                UserID = (int)reader["UserID"],
            //                Username = reader["Username"].ToString(),
            //                Email = reader["Email"].ToString(),
            //            });
            //        }
            //    }
            //}
            //return users;

            var users = GetAllUsers();
            var usersSorted = new List<User>();
            foreach (var user in users.OrderBy(u => u.Username))
            {
                usersSorted.Add(user);
            }
            return usersSorted;
        }
    }
}
