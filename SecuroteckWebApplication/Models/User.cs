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
        public enum Role
        {
            Admin,
            User
        }

        public User() { }

        [Key]
        public string ApiKey { get; set; }

        public string UserName { get; set; }

        public Role UserRole { get; set; }
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

        /// <summary>
        /// Check if a user with a given ApiKey string exists in the database, returning true or false.
        /// </summary>
        /// <param name="userContext"></param>
        /// <returns></returns>
        public bool CheckIfApiKeyExists(UserContext userContext)
        {
            User user = new User();
            var id = new Guid();

            if (userContext.Users.Any(o => o.ApiKey == id.ToString()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckIfApiKeyAndUserNameExist(UserContext userContext)
        {
            User user = new User();
            var id = new Guid();

            if (userContext.Users.Any(o => o.ApiKey == id.ToString() + u => u.user == ())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Create a new user, using a username given as a parameter and creating a new GUID which is saved as a
        /// string to the database as the ApiKey.This must return the ApiKey or the User object so that the server can
        /// pass the Key back to the client.
        /// </summary>
        /// <param name="userContext"></param>
        public void CreateNewUserIfNotExist(UserContext userContext)
        {

            User user = new User();
            var id = new Guid();

            if (userContext.Users.Any(o => o.ApiKey == id.ToString()))
            {
                return;
            }
            else
            {
                using (var uc = new UserContext())
                {
                    user = new User { ApiKey = id.ToString(), UserName = "", UserRole = User.Role.User };
                }

            }
        }

        public void ReturnUserIfExist(User user)
        {
            using (UserContext ctx = new UserContext())
            {
                var query = from b in ctx.Users orderby b.UserName select b;
                foreach (var in query)
                {
                    return user;
                }
            }
        }

    }
    // TODO: Make methods which allow us to read from/write to the database 
    #endregion
}