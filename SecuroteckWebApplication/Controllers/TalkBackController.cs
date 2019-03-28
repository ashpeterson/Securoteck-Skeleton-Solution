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
            return "Hello World";          
        }

        [ActionName("Sort")]
        public int[] Get([FromUri]int[] integers)
        {           
            try
            {
                var ints = integers.OrderBy(i => i).ToArray();
                return ints;
            }
            catch (Exception e)
            {
                throw  e;
               
            }       
           
        }

    }
}

