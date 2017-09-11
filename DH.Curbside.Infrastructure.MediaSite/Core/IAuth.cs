using RestSharp;

namespace DH.Curbside.Infrastructure.MediaSite.Core
{
    interface IAuth
    {
        void Authenticate(IRestClient client, IRestRequest request);
    }
}
