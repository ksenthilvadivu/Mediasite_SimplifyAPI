using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Text;

namespace DH.Curbside.Infrastructure.MediaSite.Core
{
    public class Auth : IAuthenticator, IAuth
    {
        private string userName, password, apiKey;

        public Auth(string userName, string password, string apiKey)
        {
            this.userName = userName;
            this.password = password;
            this.apiKey = apiKey;
        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            var basicAuthHeaderValue = string.Format("Basic {0}", Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", userName, password))));
            request.AddHeader("Authorization", basicAuthHeaderValue);
            request.AddHeader("sfapikey", apiKey);
        }
    }
}
