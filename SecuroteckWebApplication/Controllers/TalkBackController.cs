using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SecuroteckWebApplication.Controllers
{
    public class TalkBackController : ApiController
    {
        [ActionName("Hello")]
        public string Get()
        {
            #region TASK1
            return "Hello World 123";
            // TODO: add api/talkback/hello response
            #endregion
        }

        [ActionName("Sort")]
        public int[] Get([FromUri]int[] integers)
        {
            #region TASK1
            try
            {
                var ints = integers.OrderBy(i => i).ToArray();
                return ints;
            }
            catch (Exception)
            {
                throw;
               // return new HttpResponseMessage(HttpStatusCode.NotModified);
            }
           //return new HttpResponseMessage(HttpStatusCode.NotModified);
            // TODO: 
            // sort the integers into ascending order
            // send the integers back as the api/talkback/sort response
            #endregion
        }

    }
}
