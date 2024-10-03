using DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.Interfaces
{
    public interface IAdminUserActivityDAL
    {
        bool LogActivity(int adminId, int userId, string action); // Метод для логування дій адміністратора
        List<AdminUserActivity> GetActivityLog();


    }
}
