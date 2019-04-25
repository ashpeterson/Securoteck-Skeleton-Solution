using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SecuroteckWebApplication.Models;
using System.Security.Cryptography;
using System.Text;
using SecuroteckWebApplication;
using static SecuroteckWebApplication.Models.Log;

namespace SecuroteckWebApplication.Controllers
{
    public class ProtectedController : ApiController
    {
        UserDatabaseAccess ud = new UserDatabaseAccess();
        [APIAuthorise]
        [ActionName("Hello")]
        public HttpResponseMessage Get()
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
                User user = ud.CheckApiForUser(v);
                return Request.CreateErrorResponse(HttpStatusCode.OK, "Hello " + user.UserName);
            }
            return Request.CreateErrorResponse(HttpStatusCode.OK, "Hello");
        }

        [APIAuthorise]
        [Route("api/protected/sha1")]
        [HttpGet]
        public HttpResponseMessage SHA1([FromUri]string message)
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


            if (message != null)
            {
                byte[] asciiByteMessage = System.Text.Encoding.ASCII.GetBytes(message);
                byte[] sha1ByteMessage;
                SHA1 sha1Provider = new SHA1CryptoServiceProvider();
                sha1ByteMessage = sha1Provider.ComputeHash(asciiByteMessage);

                string hexoutput = ByteArrayToString(sha1ByteMessage);

                return Request.CreateErrorResponse(HttpStatusCode.OK, hexoutput);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "bad request");
            }
        }

        [APIAuthorise]
        [Route("api/protected/sha256")]
        [HttpGet]
        public HttpResponseMessage SHA256([FromUri]string message)
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

            if (message != null)
            {
                byte[] asciiByteMessage = System.Text.Encoding.ASCII.GetBytes(message);
                byte[] sha256ByteMessage;
                SHA256 sha256Provider = new SHA256CryptoServiceProvider();
                sha256ByteMessage = sha256Provider.ComputeHash(asciiByteMessage);

                string hexoutput = ByteArrayToString(sha256ByteMessage);

                return Request.CreateErrorResponse(HttpStatusCode.OK, hexoutput);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "bad request");
            }
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        // POST: api/Protected
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Protected/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Protected/5
        public void Delete(int id)
        {
        }
    }
}
