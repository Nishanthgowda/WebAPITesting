using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPITesting.Models
{
    public class UserValidate
    {
        public static bool Login(string UserName,string Password)
        {
            UserBL userBL = new UserBL();
            var usersList = userBL.GetUsersList();
            return usersList.Any(u => u.UserName.ToLower() == UserName.ToLower() && u.Password == Password);
        }
        public static User GetUserDetails(string UserName,string Password)
        {
            UserBL userBL = new UserBL();
            var usersList = userBL.GetUsersList();
            return usersList.FirstOrDefault(u => u.UserName.ToLower() == UserName.ToLower() && u.Password == Password);
        }
    }
}