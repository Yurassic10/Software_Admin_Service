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
    public class AdministratorDAL : IAdministratorDAL
    {
        private readonly SqlConnection connection;
        private readonly string connectionStr;
        public AdministratorDAL(string test1="")
        {
            if (test1 == "test")
            {
                connectionStr = "Data Source=DESKTOP-NALH133;Initial Catalog=Administrator_Service_Test;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                // Data Source=DESKTOP-NALH133;Initial Catalog=Administrator_Service_Test;Integrated Security=True;Encrypt=True;Trust Server Certificate=True
            }
            else
            {
                connectionStr = "Data Source=DESKTOP-NALH133;Initial Catalog=Administrator_Service;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
            }
            connection = new SqlConnection(connectionStr);
        }

        public void ActivateUser(int userId)
        {
            UpdateUserStatus(userId, false);
        }

        public void BlockUser(int userId)
        {
            UpdateUserStatus(userId, true);
        }

        public bool Login(string username, string password)
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT AdminID, Username, IsActive FROM tblAdministrator WHERE Username = @username AND Password = @password AND IsActive = 1"; // Поміняно passwordHash на password


                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    int adminId = Convert.ToInt32(reader["AdminID"]);
                    string user = (string)reader["Username"];
                    bool isActive = (bool)reader["IsActive"];
                    Console.WriteLine($"Login successful for AdminId: {adminId}\n Username: {user}\n IsActive: {isActive}");

                    reader.Close();

                    connection.Close();
                    return true; 
                }
                else
                {
                    Console.WriteLine("Login failed: invalid Username or Password.");
                    connection.Close();
                    return false;
                }
            }
        }

        public void UpdateUserStatus(int userId, bool isBlocked)
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText= "UPDATE tblUser SET IsBlocked = @isBlocked WHERE UserID = @userId";
                command.Parameters.AddWithValue("@isBlocked", isBlocked);
                command.Parameters.AddWithValue("@userId", userId);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<Administrator> GetAllAdministrators()
        {
            using(SqlCommand command = connection.CreateCommand()) 
            {
                command.CommandText = "Select AdminID,Username,CreatedAt FROM tblAdministrator";
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Administrator> administrators = new List<Administrator>();
                while (reader.Read()) 
                {                    
                    administrators.Add(new Administrator
                    {
                        AdminID = Convert.ToInt32(reader["AdminID"]),
                        Username = reader["Username"].ToString(),
                        CreatedAt = (DateTime)reader["CreatedAt"]    

                    });
                    
                }
                connection.Close();
                return administrators;
            }
        }

        public bool DeleteUserById(int id)
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM tblAdminUserActivity WHERE UserID=@id";
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                command.CommandText = "DELETE FROM tblUser WHERE UserID=@id";
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected > 0;
            }
        }
      
        public int GetAdminId(string username)
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT AdminID FROM tblAdministrator WHERE Username = @username";
                command.Parameters.AddWithValue("@username", username);

                connection.Open();
                var adminId = (int)command.ExecuteScalar();
                connection.Close();

                return adminId;
            }
        }
        public bool AddAdministrator(string username, string password)
        {
            bool isAdd = true;
            try
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    // SQL-запит для вставки нового адміністратора
                    command.CommandText = "INSERT INTO tblAdministrator (Username, Password, CreatedAt, IsActive) VALUES (@username, @password, @createdAt, @isActive)";

                    // Додаємо параметри
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@createdAt", DateTime.Now);  // Дата створення
                    command.Parameters.AddWithValue("@isActive", true);

                    // Відкриваємо з'єднання, виконуємо команду і закриваємо з'єднання
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                    // Console.WriteLine("Administrator registrated successfully.");
                }
            }catch(Exception ex)
            {
                isAdd = false;
            }
            return isAdd;
        }

    }
}
