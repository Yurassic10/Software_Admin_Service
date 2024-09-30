// See https://aka.ms/new-console-template for more information
using ConsoleApp.EditorHelper;
using DataAccessLogic.ADO;
using System.Data.SqlClient;
using DTO.Model;


public class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        string connStr = "Data Source=DESKTOP-NALH133;Initial Catalog=Administrator_Service;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        //string connStr = "Data Source=DESKTOP-NALH133;Initial Catalog=Administrator_Service;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        MenuConsole.Introdusing();

        Console.WriteLine("\nInput Admin name to enter:");
        string temp_adminName = Console.ReadLine();
        Console.WriteLine("\nInput Password to enter:");
        string temp_password = Console.ReadLine();
        var administrator = new AdministratorDAL(connStr);
        var activityDal = new AdminUserActivityDAL(connStr);
        var User = new UserDAL(connStr);

        if (true)
        {
            while (administrator.Login(temp_adminName, temp_password))
            {
                Console.WriteLine("Login successful!");
                MenuConsole.ShowMenu();
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        {
                            bool operationShowAdmins = true;
                            do
                            {
                                MenuConsole.ShowAdminMenu();
                                int ans = Convert.ToInt32(Console.ReadLine());
                                switch (ans)
                                {
                                    case 1:
                                        {
                                            GetAllAdmins(); 
                                        }
                                        break;
                                    case 2:
                                        {
                                            AdminDeleteUserById(); // 
                                        }
                                        break;
                                    case 3:
                                        {
                                            AdminActivateUserById(); 
                                        }
                                        break;
                                    case 4:
                                        {
                                            AdminBlockUserById(); 
                                        }
                                        break;
                                    case 5:
                                        {
                                            operationShowAdmins = false;
                                        }
                                        break;
                                }
                            } while (operationShowAdmins);

                        }
                        break;
                    case 2:
                        {
                            bool operationShowUser = true;
                            do
                            {
                                MenuConsole.ShowUserMenu();
                                int ans = Convert.ToInt32(Console.ReadLine());
                                switch (ans)
                                {
                                    case 1:
                                        {
                                            SeeAllUsers();
                                        }
                                        break;
                                    case 2:
                                        {
                                            SearchUserById(); 
                                        }
                                        break;
                                    case 3:
                                        {

                                            SortAllUsers(); 
                                        }
                                        break;
                                    case 4:
                                        {
                                            operationShowUser = false;
                                        }
                                        break;
                                }
                            } while (operationShowUser);
                        }
                        break;
                    case 3:
                        {
                            bool operationShowAction = true;
                            do
                            {
                                MenuConsole.ShowAdminUserActivity();
                                int ans = Convert.ToInt32(Console.ReadLine());
                                switch (ans)
                                {
                                    case 1:
                                        {
                                            SeeAllActivityLogged(); 
                                        }
                                        break;
                                    case 2:
                                        {
                                            operationShowAction = false;
                                        }
                                        break;
                                }
                            } while (operationShowAction);
                        }
                        break;
                }
            }
        }
        else
        {
            Console.WriteLine("Login failed. Invalid Name or Password!");
        }

        void LogActivityToTable() //////////////////////////
        {
            Console.WriteLine("Enter User ID:");
            int userId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter action:");
            string action = Console.ReadLine();

            activityDal.LogActivity(administrator.GetAdminId(temp_adminName), userId, action);
            Console.WriteLine("Activity logged successfully.");
            Console.ReadKey();
        }

        void SeeAllActivityLogged()
        {
            var logs = activityDal.GetActivityLog();
            foreach (var log in logs)
            {
                Console.WriteLine($"Admin ID: {log.AdminID}, User ID: {log.UserID}, Action: {log.Action}, Date: {log.Timestamp}");
            }
            Console.ReadKey();
        }

        void SortAllUsers()
        {
            Console.WriteLine("Users sorted by name:");
            var users = User.GetAllUsers(); 
            foreach (var user in users.OrderBy(u => u.Username))
            {
                Console.WriteLine($"User ID: {user.UserID}, Name: {user.Username}");
            }
            Console.ReadKey();
        }

        void SearchUserById()
        {
            Console.WriteLine("Enter UserId to search:");
            int userId = Convert.ToInt32(Console.ReadLine());

            var user = User.SearchUserById(userId);
            if (user != null)
            {
                Console.WriteLine($"UserID: {user.UserID}, Name: {user.Username}, Statys: {(user.IsBlocked ? "Active" : "Blocked")}");
            }
            else
            {
                Console.WriteLine("Users not found.");
            }
            Console.ReadKey();
        }

        void SeeAllUsers()
        {
            var users = User.GetAllUsers();
            foreach (var user in users)
            {
                Console.WriteLine($"User ID: {user.UserID}, Name: {user.Username}, Status: {(user.IsBlocked ? "Active" : "Blocked")}");
            }
            Console.ReadKey();
        }

        void AdminBlockUserById() 
        {
            Console.WriteLine("Enter User ID to block:");
            int userId = Convert.ToInt32(Console.ReadLine());

            administrator.BlockUser(userId);
            Console.WriteLine("User blocked successfully.");
            activityDal.LogActivity(administrator.GetAdminId(temp_adminName), userId, "Blocked user");
            Console.ReadKey();
        }

        void AdminActivateUserById() 
        {
            Console.WriteLine("Enter User ID to activate:");
            int userId = Convert.ToInt32(Console.ReadLine());

            administrator.ActivateUser(userId);
            Console.WriteLine("User activated successfully.");
            activityDal.LogActivity(administrator.GetAdminId(temp_adminName), userId, "Activated user");
            Console.ReadKey();
        }

        void AdminDeleteUserById() ////////////////
        {
            Console.WriteLine("Enter User ID to delete:");
            int userId = Convert.ToInt32(Console.ReadLine());

            bool success = administrator.DeleteUserById(userId);

            if (success)
            {
                Console.WriteLine("User deleted successfully.");             
            }
            else
            {
                Console.WriteLine("Error deleting user.");
            }
            Console.ReadKey();
        }


        void GetAllAdmins()
        {
            var adminDal = new AdministratorDAL(connStr);
            var admins = adminDal.GetAllAdministrators();

            foreach (var admin in admins)
            {
                Console.WriteLine($"Admin ID: {admin.AdminID}, Name: {admin.Username}, Logged at: {admin.CreatedAt}");
            }
            
            Console.ReadKey();
        }
    }
}


