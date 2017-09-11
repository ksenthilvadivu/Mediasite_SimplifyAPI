using System;
using RestSharp;
using DH.Curbside.Infrastructure.MediaSite.Mapping;

namespace DH.Curbside.Infrastructure.MediaSite.Core
{
    public class ClientManager : IClientManager
    {
        private static RestClient _client;
        public RestClient Client
        {
            get { return getClient(); }
        }

        private readonly ConnectMap _mediasiteConn;
        private readonly Uri _baseUri;

        public ClientManager(ConnectMap mediasiteConn)
        {
            _mediasiteConn = mediasiteConn;
            _baseUri = new Uri(mediasiteConn.EndpointAddress);
        }


        private RestClient getClient()
        {
            if (null == _client)
            {
                if (string.IsNullOrEmpty(_mediasiteConn.EndpointAddress))
                {
                    //throw new ApiConnectionException(ProvideConnectionInformation);
                    //Error Handling
                }

                try
                {
                    _client = new RestClient(new Uri(_baseUri, "api/v1").ToString());
                    _client.Authenticator = new Auth(_mediasiteConn.UserName, _mediasiteConn.Password, _mediasiteConn.ApiKey);
                }
                catch (Exception)
                {
                    _client = null;
                }
            }

            return _client;
        }
    }
}
