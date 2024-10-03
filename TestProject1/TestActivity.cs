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
    public class TestActivity
    {
        
        private IAdminUserActivityDAL activity;


        [SetUp]
        public void Setup()
        {

            activity = new AdminUserActivityDAL("test");

        }

        [Test]
        public void GetAllActivities()
        {
            var ad = activity.GetActivityLog();

            Assert.IsNotNull(ad);
        }

        [Test]
        public void AddNewActivity()
        {
          

            bool res = activity.LogActivity(1, 2, "test");
            Assert.IsTrue(res);

        }

    }
}
