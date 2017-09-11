using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace CurbsideTestProject.API.WhitelistUser
{
    public class UserCaller
    {
        RestClient client;
        public UserCaller(string baseurl)
        {
            client = new RestClient(baseurl);
        }

        /// <summary>
        /// GetWhitelist User Call
        /// </summary>        
        public List<UserModel> GetWhiteListUsers()
        {
            var request = new RestRequest("whitelist", Method.GET);
            var response = client.Execute<List<UserModel>>(request);
            return response.Data;
         }

      

    }
}
