using System;
using System.Collections.Generic;

namespace DH.Curbside.Core.Domain
{
    public class ClientApplication
    {
        public ClientApplication()
        {
            Tenants = new HashSet<Tenant>();
        }

        public int ClientApplicationId { get; set; }

        public Guid ClientApplicationToken { get; set; }

        public string ClientApplicationName { get; set; }

        public bool IsMobile { get; set; }

        public string MinimumVersion { get; set; }

        public string LatestSupportedVersion { get; set; }

        public string UpgradeUrl { get; set; }

        public virtual ICollection<Tenant> Tenants { get; set; }
    }
}
