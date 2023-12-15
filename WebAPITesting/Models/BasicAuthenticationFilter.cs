using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebAPITesting.Models
{
    public class BasicAuthenticationFilter:AuthorizationFilterAttribute
    {
        readonly string realm = "BasicRealm";
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if(actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);

                if(actionContext.Response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    actionContext.Request.Headers.Add("WWW-Authenticate", string.Format("realm \"{0}\"", realm));
                }               
            }
            else
            {
                string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                string authDecodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
                string[] authCredentials = authDecodedCredentials.Split(':');
                var userName = authCredentials[0];
                var password = authCredentials[1];
                if(UserValidate.Login(userName,password))
                {
                    var identity = new GenericIdentity(userName);
                    var principle = new GenericPrincipal(identity, null);
                    Thread.CurrentPrincipal = principle;

                    if(HttpContext.Current != null)
                    {
                        HttpContext.Current.User = principle; 
                    }
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}