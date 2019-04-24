using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using SecuroteckWebApplication.Models;
using static SecuroteckWebApplication.Models.Log;

namespace SecuroteckWebApplication.Controllers
{
    public class APIAuthorisationHandler : DelegatingHandler
    {
     
      
        protected override Task<HttpResponseMessage> SendAsync (HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Task5
            // TODO:  Find if a header ‘ApiKey’ exists, and if it does, check the database to determine if the given API Key is valid
            // Then authorise the principle on the current thread using a claim, claimidentity and claimsprinciple

            UserDatabaseAccess userDatabaseAccess = new UserDatabaseAccess();

            IEnumerable<string> values;
            request.Headers.TryGetValues("ApiKey", out values);
            if (values != null)
            {
                foreach (string v in values)
                {
                    if (userDatabaseAccess.CheckApi(v))
                    {
                        User user = userDatabaseAccess.CheckApiForUser(v);
                        var claims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Name, user.UserName, ClaimTypes.Role, user.UserRole.ToString())
                    };
                        var id = new ClaimsIdentity(claims, authenticationType: "ApiKey");
                        var principle = new ClaimsPrincipal(id);
                        Thread.CurrentPrincipal = principle;
                    }
                }
            }
            return base.SendAsync(request, cancellationToken);
        }

    }
}