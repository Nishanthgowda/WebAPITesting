using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPITesting.Data;

namespace WebAPITesting.Models
{
    public class UserValidate
    {
        static UserBL userBL = new UserBL();
        public static bool Login(string UserName,string Password)
        {
            var usersList = userBL.GetUsersList();
            return usersList.Any(u => u.UserName.ToLower() == UserName.ToLower() && u.Password == Password);
        }
        public static User GetUserDetails(string UserName,string Password)
        {
            
            var usersList = userBL.GetUsersList();
            return usersList.FirstOrDefault(u => u.UserName.ToLower() == UserName.ToLower() && u.Password == Password);
        }
        public static UserMaster ValidateUserDb(string UserName, string Password)
        {
            using (WebApi_DbContext dbContext = new WebApi_DbContext())
            {
                return dbContext.UserMasters.FirstOrDefault(u => u.UserName.ToLower() == UserName.ToLower() && u.UserPassword == Password);
            }                
        }
    }
}