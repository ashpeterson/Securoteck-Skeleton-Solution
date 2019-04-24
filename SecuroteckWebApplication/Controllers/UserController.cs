using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SecuroteckWebApplication.Models;
using static SecuroteckWebApplication.Models.Log;

namespace SecuroteckWebApplication.Controllers
{
    public class UserController : ApiController
    {
        // GET: localhost:<portnumber>/api/user/new?username=UserOne
        UserDatabaseAccess ud = new UserDatabaseAccess();

        [ActionName("New")]
        public HttpResponseMessage Get([FromUri]string Username)
        {
            if (ud.CheckUserName(Username) == true)
            {
                return Request.CreateErrorResponse(HttpStatusCode.OK, "True - User Does Exist! Did you mean to do a POST to create a new user?");
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.OK, "False - User Does Not Exist! Did you mean to do a POST to create a new user?");
            }
        }

        // POST: localhost:<portnumber>/api/user/new with “UserOne” in the body of the request
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
        [APIAuthorise]
        [ActionName("RemoveUser")]
        public HttpResponseMessage Delete([FromUri]string Username)
        {

            IEnumerable<string> values;
            this.Request.Headers.TryGetValues("ApiKey", out values);

            foreach (string v in values)
            {
                if (ud.CheckApi(v))
                {
                    ud.AddUserLogs("User Requested /user/removeuser", v);
                }
            }

            foreach (string v in values)
            {
                if (ud.CheckApiandUserName(v, Username))
                {
                    ud.DeleteUserApi(v);
                    return Request.CreateErrorResponse(HttpStatusCode.OK, "true");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.OK, "false");
                }

            }
            return Request.CreateErrorResponse(HttpStatusCode.OK, "false");
        }
    }
}
