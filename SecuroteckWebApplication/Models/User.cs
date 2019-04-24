using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Http;

namespace SecuroteckWebApplication.Models
{
    public class User
    {
        #region Task2
        public enum Role
        {
            Admin =  0,
            User =  1
        }
        public User() { Logs = new List<Log>(); }

        [Key]
        public string ApiKey { get; set; }


        public string UserName { get; set; }

        public Role UserRole { get; set; }

        public virtual ICollection<Log> Logs { get; set; }
    }

    // TODO: Create a User Class for use with Entity Framework
    // Note that you can use the [key] attribute to set your ApiKey Guid as the primary key  
    #endregion


    #region Task13?
    // TODO: You may find it useful to add code here for Logging

    public class Log
    {
        public Log() { }
        [Key]
        public string LogID { get; set; }
        public string LogString { get; set; }
        public DateTime LogDateTime { get; set; }

        #endregion

        public class UserDatabaseAccess : IDisposable
        {
            #region Task3 
            //TODO - add multiple access handling
            /// <summary>
            /// 3.1
            /// Create a new user, using a username given as a parameter and creating a new GUID which is saved as a
            /// string to the database as the ApiKey.This must return the ApiKey or the User object so that the server can
            /// pass the Key back to the client.
            /// </summary>
            /// <param name="userContext"></param>
            public void CreateNewUserIfNotExist(UserContext userContext)
            {

                User user = new User();
                Guid id = new Guid();

                if (userContext.Users.Any(o => o.ApiKey == id.ToString()))
                {
                    return;
                }
                else
                {
                    using (UserContext uc = new UserContext())
                    {
                        user = new User { ApiKey = id.ToString(), UserName = "User 1", UserRole = User.Role.User };
                    }

                }
            }

            public bool CheckUserName([FromUri]string Name)
            {
                using (UserContext ctx = new UserContext())
                {
                    IQueryable<User> users = from b in ctx.Users
                                             where b.UserName.Contains(Name)
                                             select b;

                    foreach (User result in users)
                    {
                        if (result.UserName == Name)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }

            public bool CheckApi([FromUri]string Key)
            {
                using (UserContext ctx = new UserContext())
                {
                    if (Key != null)
                    {
                        User user = ctx.Users.Find(Key);
                        if (user.ApiKey == Key)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            public User CheckApiForUser([FromUri]string Key)
            {
                using (UserContext ctx = new UserContext())
                {
                    User user = ctx.Users.Find(Key);
                    if (user.ApiKey == Key)
                    {
                        return user;
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            private string _apiKey;

            /// <summary>
            /// 3.2
            /// Check if a user with a given ApiKey string exists in the database, returning true or false.
            /// </summary>
            /// <param name="userContext"></param>
            /// <returns></returns>
            public bool CheckIfApiKeyExists(string apiKey)
            {
                _apiKey = apiKey;

                UserContext uc = new UserContext();

                if (uc.Users.Any(o => o.ApiKey == apiKey.ToString()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            /// <summary>
            /// 3.3
            /// Check if a user with a given ApiKey and UserName exists in the database, returning true or false.
            /// </summary>
            /// <param name="Key"></param>
            /// <param name="Name"></param>
            /// <returns></returns>
            public bool CheckApiandUserName([FromUri]string Key, string Name)
            {
                using (var ctx = new UserContext())
                {
                    User user = ctx.Users.Find(Key);
                    if (user.ApiKey == Key && user.UserName == Name)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            /// <summary>
            /// 3.4
            /// Check if a user with a given ApiKey string exists in the database, returning the User object.
            /// </summary>
            /// <param name="user"></param>
            public string ReturnUserIfExist(string apikey)
            {
                //https://stackoverflow.com/questions/1802286/best-way-to-check-if-object-exists-in-entity-framework

                UserContext context = new UserContext();

                if (context.Users.Any(o => o.ApiKey == apikey))
                {
                    return apikey.ToString();
                }
                return apikey;
            }

            /// <summary>        
            /// 3.5
            /// Delete a user with a given ApiKey from the database.
            /// </summary>
            public void DeleteUserApi([FromUri]string Key)
            {
                using (var ctx = new UserContext())
                {
                    User user = ctx.Users.Find(Key);
                    if (user.ApiKey == Key)
                    {
                        ctx.Users.Remove(user);
                        ctx.SaveChanges();
                    }
                }
            }

            public void ChangeRole ([FromUri]string Key, string Role)
            {
                var user = new User() {ApiKey = Key,  UserRole =Role.ToString() };
                using (var ctx = new UserContext())
                {
                    ctx.Users.Attach(user);
                    ctx.Entry(user).Property(x => x.UserRole).IsModified = true;
                    ctx.SaveChanges();
                }

            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public void AddUserLogs(string item, string Key)
            {

                using (UserContext ctx = new UserContext())
                {
                    User user = ctx.Users.Find(Key);
                    if (user.ApiKey == Key)
                    {
                        Log log = new Log()
                        {
                            LogDateTime = DateTime.Now,
                            LogString = item,
                            LogID = Guid.NewGuid().ToString()
                        };
                        User usertoupdate = new User()
                        {
                            UserName = user.UserName,
                            ApiKey = user.ApiKey
                        };
                        usertoupdate.Logs.Add(log);
                        try
                        {
                            ctx.Logs.Add(log);
                            ctx.Entry(user).CurrentValues.SetValues(usertoupdate);
                            ctx.SaveChanges();
                        }
                        catch (DbEntityValidationException e)
                        {
                            foreach (var eve in e.EntityValidationErrors)
                            {
                                Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                                foreach (var ve in eve.ValidationErrors)
                                {
                                    Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                        ve.PropertyName, ve.ErrorMessage);
                                }
                            }
                            throw;
                        }
                    }
                    else
                    {

                    }
                }
            }
        }



        // TODO: Make methods which allow us to read from/write to the database 
        #endregion
    }
}