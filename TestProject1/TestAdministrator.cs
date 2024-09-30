using DataAccessLogic.ADO;
using DataAccessLogic.Interfaces;
using DTO;
using System.Data.SqlClient;
using NUnit.Framework;
using DTO.Model;

namespace UnitTestProject
{
    public class TestAdministrator
    {
        private IAdministratorDAL admin;
        private IAdminUserActivityDAL adminUserActivity;
        private IUserDAL users;
        
        [SetUp]
        public void Setup()
        {
            admin = new AdministratorDAL("test");
            adminUserActivity = new AdminUserActivityDAL("test");
            users = new UserDAL("test");
        }

        [Test]
        public void TestLogin_Success()
        {
            
            string username = "Yura"; 
            string password = "Yura123"; 

            bool loginSuccess = admin.Login(username, password);

            Assert.IsTrue(loginSuccess);
        }
        [Test]
        public void GetAllAdministrators_ReturnsCountOfAdmins()
        {
            List<Administrator> result = admin.GetAllAdministrators();
            int expectedCountOfAdmins = 2;

            Assert.AreEqual(expectedCountOfAdmins, result.Count, "Expected exactly 2 administrators in the database.");

        }
        [Test]
        public void UpdateUserStatus_ShouldUpdateUserStatus()
        {           
            int userId = 1; 
            bool newStatus = false;

            admin.UpdateUserStatus(userId, newStatus);

            var user = admin.GetAllAdministrators().FirstOrDefault(u => u.AdminID == userId);
            Assert.AreEqual(newStatus, user.IsActive, "User status was not updated correctly.");
        }
        [Test]
        
        public void SearchUserById_ShouldReturnUser_WithName_Yurassic()
        {
            int userId = 1;
            var expectedUsername = "Yurassic";

            User user = users.SearchUserById(userId);

            Assert.AreEqual(expectedUsername, user.Username, "Username does not match.");
        }
        [Test]
        public void GetAllUsers_ReturnsUserWithNameYurassic()
        {
            var user = users.GetAllUsers();

            Assert.IsNotNull(user);

            var userWithNameYurassic = user.FirstOrDefault(u => u.Username == "Yurassic");
            Assert.IsNotNull(userWithNameYurassic);
        }
    }
}