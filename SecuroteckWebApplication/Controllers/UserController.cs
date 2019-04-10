using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SecuroteckWebApplication.Models;
using SecuroteckWebApplication.Enums;

namespace SecuroteckWebApplication.Controllers
{
    public class UserController : ApiController
    {
        // GET: api/User
        [ActionName("new")]
        public string Get([FromUri]string username)
        {
            HttpResponseMessage responseTrue = Request.CreateResponse(HttpStatusCode.OK, "True - User Does Exist! Did you mean to do a POST to create a new user?");
            HttpResponseMessage responseFalse = Request.CreateResponse(HttpStatusCode.OK, "False - User Does Not Exist! Did you mean to do a POST to create a new user?");
            HttpResponseMessage responseNull = Request.CreateResponse(HttpStatusCode.OK, "False - User Does Not Exist! Did you mean to do a POST to create a new user?");
            UserContext uc = new UserContext();

            if (uc.Users.Any(o => o.UserName == username))
            {
                return responseTrue.ToString();
            }
            else if (uc.Users.Any(o => o.UserName != username))
            {
                return responseFalse.ToString();
            }
            else if (string.IsNullOrEmpty(username))
            {
                return responseNull.ToString();
            }
            return username;
        }

        // GET: api/User/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        public string Post([FromBody]string username)
        {
            UserContext uc = new UserContext();
            string apikey = Guid.NewGuid().ToString();
            HttpResponseMessage responseCreated = Request.CreateResponse(HttpStatusCode.OK, apikey.ToString());
            HttpResponseMessage responseStringEmpty = Request.CreateResponse(HttpStatusCode.BadRequest, "Oops. Make sure your body contains a string with your username and your Content - Type is Content - Type:application / json");
            HttpResponseMessage responseUserNameExists = Request.CreateResponse(HttpStatusCode.Forbidden, "Oops. Make sure your body contains a string with your username and your Content - Type is Content - Type:application / json");

            if (string.IsNullOrEmpty(username))
            {
                return responseStringEmpty.ToString();
            }

            else if (uc.Users.Any(o => o.UserName == username))
            {
                return responseUserNameExists.ToString();
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
                return responseCreated.ToString();

            }
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
