using System;
using System.Collections.Generic;

namespace DH.Curbside.Core.Domain
{
    public class Tenant
    {
        public Tenant()
        {
            Domains = new HashSet<Domain>();
            Providers = new HashSet<Provider>();
            Users = new HashSet<User>();
            ClientApplications = new HashSet<ClientApplication>();
        }

        public Guid TenantId { get; set; }

        public string TenantName { get; set; }

        public virtual ICollection<Domain> Domains { get; set; }

        public virtual ICollection<Provider> Providers { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<ClientApplication> ClientApplications { get; set; }
    }
}
