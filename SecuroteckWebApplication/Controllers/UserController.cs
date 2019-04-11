using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SecuroteckWebApplication.Models;

namespace SecuroteckWebApplication.Controllers
{
    public class UserController : ApiController
    {
        // GET: api/User
        [ActionName("New")]
        public IHttpActionResult Get([FromUri]string username)
        {        
            UserContext uc = new UserContext();

            var user = uc.Users.Any(o => o.UserName == username);

            if (user == true)
            {
                return Ok("True - User Does Exist! Did you mean to do a POST to create a new user?");
            }
            else if (string.IsNullOrEmpty(user.ToString()))
            {
                return Ok("False - User Does Not Exist! Did you mean to do a POST to create a new user? ");
            }
            return Ok("False - User Does Not Exist! Did you mean to do a POST to create a new user?");
        }


        // POST: api/User
        [ActionName("New")]
        public IHttpActionResult Post([FromBody]string username)
        {
            UserContext uc = new UserContext();
            string apikey = Guid.NewGuid().ToString();
            var user = uc.Users.Any(o => o.UserName == username);
            var firstUser = uc.Users.Count(o => o.UserName == username);

            if (string.IsNullOrEmpty(user.ToString()))
            {
                return BadRequest("Oops.Make sure your body contains a string with your username and your Content - Type is Content - Type:application / json");
            }
            else if (user == true)
            {
                return Content(HttpStatusCode.Forbidden, "Oops. This username is already in use. Please try again with a new username");
            }
            else if (firstUser == 0)
            {
                var usr = new User()
                {
                    ApiKey = apikey,
                    UserName = username,
                    UserRole = Models.User.Role.Admin
                };
                uc.Users.Add(usr);
                uc.SaveChanges();             
            }
            else
            {
                var usr = new User()
                {
                    ApiKey = apikey,
                    UserName = username,
                    UserRole = Models.User.Role.User
                };
                uc.Users.Add(usr);
                uc.SaveChanges();
            }
            return Ok(apikey.ToString());
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
