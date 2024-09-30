using DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.Interfaces
{
    public interface IAdministratorDAL
    {
        bool Login(string username, string password);
        List<Administrator> GetAllAdministrators();
        //List<User> GetAllUsers();
        bool DeleteUserById(int id); // Поміняв на bool з void
        void ActivateUser(int userId);
        void BlockUser(int userId);

        void UpdateUserStatus(int userId, bool isBlocked);
        int GetAdminId(string username);
        //List<User> SearchUserById(int userId);
        //List<User> SortUsers(string sortBy, string order);

    }
}
