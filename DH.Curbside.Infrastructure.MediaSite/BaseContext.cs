using DH.Curbside.Infrastructure.MediaSite.Core;
using DH.Curbside.Infrastructure.MediaSite.Mapping;
using System.Collections.Generic;
using System.Configuration;

namespace DH.Curbside.Infrastructure.MediaSite
{
    public class BaseContext
    {
        protected ConnectMap _mediasiteConn = new ConnectMap();

        private IClientManager _clientManagerField;

        public IClientManager ClientManager
        {
            get { return _clientManagerField ?? (_clientManagerField = new ClientManager(_mediasiteConn)); }
        }

        /// <summary>
        /// BaseContext Constructor
        /// </summary>
        public BaseContext()
        {            
            _mediasiteConn.UserName = ConfigurationManager.AppSettings["MS_UserName"];
            _mediasiteConn.Password = ConfigurationManager.AppSettings["MS_Password"];
            _mediasiteConn.ApiKey = ConfigurationManager.AppSettings["MS_APIKey"];
            _mediasiteConn.EndpointAddress = ConfigurationManager.AppSettings["MS_EndPointURI"];
        }
    }
}
