using RestSharp;

namespace DH.Curbside.Infrastructure.MediaSite.Core
{
    public interface IClientManager
    {
        RestClient Client { get; }       
    }
}
