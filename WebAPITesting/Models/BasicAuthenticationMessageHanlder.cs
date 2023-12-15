using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebAPITesting.Models
{
    public class BasicAuthenticationMessageHanlder:DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authToken = request.Headers.GetValues("Authorization").FirstOrDefault();
            if(authToken != null)
            {
                string[] decodeAuth = Encoding.UTF8.GetString(Convert.FromBase64String(authToken)).Split(':');
                var userName = decodeAuth[0];
                var password = decodeAuth[1];
                var Obj = UserValidate.ValidateUserDb(userName, password);
                if (Obj != null)
                {
                    var identity = new GenericIdentity(userName);
                    identity.AddClaim(new System.Security.Claims.Claim("Email", Obj.UserEmailID));

                    var principle = new GenericPrincipal(identity, Obj.UserRoles.Split(':'));
                    Thread.CurrentPrincipal = principle;

                    if (HttpContext.Current != null)
                    {
                        HttpContext.Current.User = principle;
                    }
                    return base.SendAsync(request, cancellationToken);
                }
                else
                {
                    var responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                    var tsk = new TaskCompletionSource<HttpResponseMessage>();
                    tsk.SetResult(responseMessage);
                    return tsk.Task;

                }
            }
            else
            {
                var responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
                var tsk = new TaskCompletionSource<HttpResponseMessage>();
                tsk.SetResult(responseMessage);
                return tsk.Task;
            }

            
        }
    }
}