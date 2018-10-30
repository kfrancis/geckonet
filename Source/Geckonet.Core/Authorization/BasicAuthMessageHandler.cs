using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;

namespace Geckonet.Core.Authorization
{
    public class BasicAuthMessageHandler : DelegatingHandler
    {
        private const string BasicAuthResponseHeader = "WWW-Authenticate";
        private const string BasicAuthResponseHeaderValue = "Basic";
        public IProvidePrincipal PrincipalProvider { get; set; }
        protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            AuthenticationHeaderValue authValue = request.Headers.Authorization;

            // Fix auth by query string apiKey value, which was the old method. Converts the query string to an AuthHeader value, then continues as normal.
            if (authValue == null)
            {
                var requestQuery = request.RequestUri.Query.Split('&').Select(p => p.Split('=')).ToDictionary(p => p[0], p => p[1]);
                if (requestQuery.ContainsKey("apiKey"))
                {
                    var apiKeyValue = requestQuery.FirstOrDefault(k => k.Key == "apiKey").Value;
                    authValue = AuthenticationHeaderValue.Parse(string.Format("Basic {0}", Convert.ToBase64String(Encoding.Default.GetBytes(string.Format("{0}:X", apiKeyValue)))));
                }
            }

            if (authValue != null && !String.IsNullOrWhiteSpace(authValue.Parameter))
            {
                Credentials parsedCredentials = ParseAuthorizationHeader(authValue.Parameter);
                if (parsedCredentials != null)
                {
                    Thread.CurrentPrincipal = PrincipalProvider
                        .CreatePrincipal(parsedCredentials.ApiKey, parsedCredentials.ApiPassword);
                }
            }
            return base.SendAsync(request, cancellationToken)
               .ContinueWith(task =>
               {
                   var response = task.Result;
                   if (response.StatusCode == HttpStatusCode.Unauthorized
                       && !response.Headers.Contains(BasicAuthResponseHeader))
                   {
                       response.Headers.Add(BasicAuthResponseHeader
                           , BasicAuthResponseHeaderValue);
                   }
                   return response;
               });
        }
        private Credentials ParseAuthorizationHeader(string authHeader)
        {
            string[] credentials = Encoding.ASCII.GetString(Convert
                                                            .FromBase64String(authHeader))
                                                            .Split(
                                                            new[] { ':' });
            if (credentials.Length != 2 || string.IsNullOrEmpty(credentials[0])
                || string.IsNullOrEmpty(credentials[1])) return null;
            return new Credentials()
            {
                ApiKey = credentials[0],
                ApiPassword = credentials[1],
            };
        }
    }

    public class Credentials
    {
        public string ApiKey { get; set; }
        public string ApiPassword { get; set; }
    }

    public interface IProvidePrincipal
    {
        IPrincipal CreatePrincipal(string apiKey, string apiPassword);
    }

    /// <summary>
    /// This provider makes sure that the api key is a guid, and not empty
    /// </summary>
    public class GuidAPIKeyPrincipalProvider : IProvidePrincipal
    {
        public IPrincipal CreatePrincipal(string apiKey, string apiPassword)
        {
            Guid apiKeyGuid = Guid.Empty;
            bool result = Guid.TryParse(apiKey, out apiKeyGuid);
            if (result == false || string.IsNullOrWhiteSpace(apiPassword))
            {
                return null;
            }
            var identity = new GenericIdentity(apiKey);
            IPrincipal principal = new GenericPrincipal(identity, new[] { "User" });
            return principal;
        }
    }
}
