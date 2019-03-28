using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Xml;
using SecuroteckWebApplication.Models;

namespace SecuroteckWebApplication.Models
{
    public class User
    {
        #region Task2

        public User() { }

        [Key]
        public string ApiKey { get; set; }

        public string UserName { get; set; }

        public Role UserRole { get; set; }
    }

    public enum Role
    {
        Admin,
        User
    }

        // TODO: Create a User Class for use with Entity Framework
        // Note that you can use the [key] attribute to set your ApiKey Guid as the primary key  
        #endregion
    

    #region Task13?
    // TODO: You may find it useful to add code here for Logging
    #endregion

    public class UserDatabaseAccess
    {
        #region Task3 

        
        public void Create (User user)
        {
            UserContext ctx = new UserContext();                

            if (ctx.Users.Add(o => o.ctx.Users == user))
            {
                var id = new Guid();
                using(var uc = new UserContext())
                {
                    user = new User { ApiKey = id.ToString(), UserName = "New User", UserRole = Role.User };              
                }         
                // Match!
            }

        } 

        public void Read(User user)  {
            using (UserContext ctx = new UserContext())
            {
                var query = from b in ctx.Users orderby b.UserName select b;
                foreach (var  in query)
                {
                    return user;
                }
            }
        }            
        
    }
     // TODO: Make methods which allow us to read from/write to the database 
     #endregion
} 