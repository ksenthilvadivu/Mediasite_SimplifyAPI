using System;

namespace DH.Curbside.Infrastructure.MediaSite.Mappings
{
    class AuthorizationTicket
    {        
        public String Username { get; set; }

        public String ResourceId { get; set; }

        public int MinutesToLive { get; set; }
    }
}
