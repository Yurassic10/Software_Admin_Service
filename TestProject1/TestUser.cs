using DataAccessLogic.ADO;
using DataAccessLogic.Interfaces;
using DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
    public class TestUser
    {
       
        
        private IUserDAL users;

        [SetUp]
        public void Setup()
        {
            
            users = new UserDAL("test");
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
        public void GetAllUsers()
        {
            var user = users.GetAllUsers();

            Assert.IsNotNull(user);
        }

        [Test]
        public void SortUserArrByName()
        {
            /*
             * List<User> usersEx = new List<User>();

            User ad = new User
            {
                UserID = 1,
                Username = "Yurassic",
                Password = "1234",  
                Email = "y@y.com",
                IsBlocked = false,
                CreatedAt = DateTime.Now
            };
            User ad1 = new User
            {
                UserID = 2,
                Username = "Mari",
                Password = "123",
                Email = "m@m.com",
                IsBlocked = true,
                CreatedAt = DateTime.Now
            };


            usersEx.Add(ad1);
            usersEx.Add(ad);
            usersEx.OrderBy(u => u.Username);
            */
           

            var userEx = users.SortUsers("", "ASC"); // users.GetAllUsers().Username

            Assert.IsNotEmpty(userEx);
        }
    }
}
