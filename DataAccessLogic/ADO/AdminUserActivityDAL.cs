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
    public class AdminUserActivityDAL : IAdminUserActivityDAL
    {
        private readonly SqlConnection connection;
        private readonly string connectionStr;
        public AdminUserActivityDAL(string test1 = "")
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
        public List<AdminUserActivity> GetActivityLog()
        {
            var activities = new List<AdminUserActivity>();
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM tblAdminUserActivity";

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    activities.Add(new AdminUserActivity
                    {
                        ActivityID = (int)reader["ActivityID"],
                        AdminID = (int)reader["AdminID"],
                        UserID = (int)reader["UserID"],
                        Action = reader["Action"].ToString(),
                        Timestamp = (DateTime)reader["Timestamp"]
                    });
                }
            }
            return activities;
        }

        public void LogActivity(int adminId, int userId, string action)
        {
            using(SqlCommand command=connection.CreateCommand())
            {
                command.CommandText= "INSERT INTO tblAdminUserActivity (AdminID, UserID, Action, Timestamp) VALUES (@adminId, @userId, @action, @timestamp)";
                command.Parameters.AddWithValue("@adminId", adminId);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@action", action);
                command.Parameters.AddWithValue("@timestamp", DateTime.Now);
                connection.Open(); 
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }     
}

