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
        // User Controller api/User/New 


        //TODO create GET request for 
        //// GET: api/Todo
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<User>>> GetUser()
        //{
        //    return await UserContext();
        //}



        //public HttpResponseMessage PostUser(User user)
        //{
            

        //    bool usernameAlreadyExists = (x => x.Username == user.Username);

        //    if (ModelState.IsValid && usernameAlreadyExists != true)
        //    {
        //        db.Users.Add(user);
        //        db.SaveChanges();

        //        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, user);
        //        response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = user.ApiKey }));
        //        return response;
        //    }
        //    else
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState,"The user does exist! Did you mean to do a post reequest?");
        //    }
        //}

        //    CspParameters cspParams = new CspParameters();
        //    cspParams.Flags = CspProviderFlags.UseMachineKeyStore;
        //    RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider(cspParams);
        //    string Message = "Hello!";
        //    byte[] asciiByteMessage = System.Text.Encoding.ASCII.GetBytes(Message);
        //    byte[] encryptedBytes = rsaProvider.Encrypt(asciiByteMessage, true);
    }
}
