using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Model
{
    public class AdminUserActivity
    {
        public int ActivityID { get; set; }
        public int AdminID { get; set; }
        public int UserID { get; set; }
        public string Action {  get; set; } //Опис дії (Активував,Заблокував)
        public DateTime Timestamp { get; set; }
    }
}
