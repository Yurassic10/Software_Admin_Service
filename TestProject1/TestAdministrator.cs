using DataAccessLogic.ADO;
using DataAccessLogic.Interfaces;
using DTO;
using System.Data.SqlClient;
using NUnit.Framework;
using DTO.Model;
using System.Diagnostics;

namespace UnitTestProject
{
    public class TestAdministrator
    {
        private IAdministratorDAL admin;
        //private IAdminUserActivityDAL adminUserActivity;
       
        
        [SetUp]
        public void Setup()
        {
            admin = new AdministratorDAL("test");
            //adminUserActivity = new AdminUserActivityDAL("test");
            
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
            List<Administrator> administrators = new List<Administrator>();

            Administrator ad = new Administrator
            {
                AdminID = 1,
                Username = "Yura",
                Password = "1234",
                IsActive = true,
                CreatedAt = DateTime.Now
            };
            Administrator ad1 = new Administrator
            {
                AdminID = 1,
                Username = "Yura",
                Password = "1234",
                IsActive = true,
                CreatedAt = DateTime.Now
            };


            administrators.Add(ad);
            administrators.Add(ad1);


            List<Administrator> result = admin.GetAllAdministrators();
            int expectedCountOfAdmins = 2;

            Assert.AreEqual(expectedCountOfAdmins, result.Count, "Expected exactly 2 administrators in the database.");

        }

        [Test]
        public void GetAllAdmins()
        {
            var ad = admin.GetAllAdministrators();

            Assert.IsNotNull(ad);
        }

        [Test]
        public void GetUserStatusBlocked()
        {
            //expected
            int userId = 1;
            bool newStatus = false;

            admin.UpdateUserStatus(userId, newStatus);

            //actual
            var user = admin.GetAllAdministrators().FirstOrDefault(u => u.AdminID == userId);

            //test
            Assert.AreEqual(newStatus, user.IsActive, "User status was not updated correctly.");
        }

        [Test]
        public void AddNewAdmin()
        {


            bool res = admin.AddAdministrator("Maria", "1234");
            Assert.IsTrue(res);

        }

    }
}