using System;

namespace DH.Curbside.Infrastructure.MediaSite.Model
{
    public class AuthResponse
    {
        public String TicketId { get; set; }

        public String Username { get; set; }

        public String ClientIpAddress { get; set; }

        public String Owner { get; set; }

        public String ResourceId { get; set; }

        public int MinutesToLive { get; set; }

        public DateTime? CreationTime { get; set; }

        public DateTime? ExpirationTime { get; set; }
    }
}
