using DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.Interfaces
{
    public interface IUserDAL
    {
        List<User> GetAllUsers();
        User SearchUserById(int userId);
        List<User> SortUsers(string sortBy, string order);
       // bool AddUser(string username, string password);
    }
}
