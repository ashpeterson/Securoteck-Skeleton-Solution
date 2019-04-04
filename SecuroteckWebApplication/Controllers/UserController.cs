using SecuroteckWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Security.Cryptography;

namespace SecuroteckWebApplication.Controllers
{
    public class UserController : ApiController
    {
        //localhost:<portnumber>/api/user/new?username=UserOne
        //If a user with the username ‘UserOne’ exists in the database, the server should return "True - User Does Exist!
        //Did you mean to do a POST to create a new user?" in the body of the result with a status code of OK (200)
        //If a user with the username ‘UserOne’ does not exist in the database, the server should return "False - User Does
        //Not Exist! Did you mean to do a POST to create a new user?" in the body of the result with a status code of OK(200).
        //If there is no string submitted, the server should return "False - User Does Not Exist! Did you mean to do
        //a POST to create a new user?" in the body of the result with a status code of OK (200).

        /// <summary>
        /// 4.1 
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public User Get (string UserName)  
        {
            if(UserName == UserName)
            {
                return "True - User Does Exist! Did you mean to do a POST to create a new user? ";
            }
            else
            {
                return "False - User Does Not Exist!Did you mean to do a POST to create a new user? ";
            }

            if(UserName == string.Empty)
            {
                return "False - User Does Not Exist! Did you mean to do a POST to create a new user? ";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public User Post (string UserName)
        {

        }


    }
}
