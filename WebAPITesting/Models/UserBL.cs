using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPITesting.Models
{
    public class UserBL
    {        
        public List<User> GetUsersList()
        {
            List<User> users = new List<User> {
            new User {ID = 101,UserName="MaleUsers",Password = "admin@123",Email="admin@users.com",Roles="Admin"},
            new User{ID=102,UserName="FemaleUsers",Password="superAdmin@123",Email="super.admin@users.com",Roles="Superadmin"},
            new User{ID=103,UserName="AllUsers",Password="all@123",Email="both@users.com",Roles="Superadmin,Admin"}

            };
            return users;
        }
    }
}