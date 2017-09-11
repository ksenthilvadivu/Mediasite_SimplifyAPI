using System;
using System.Collections.Generic;

namespace DH.Curbside.Core.Domain
{
    public class Domain
    {
        public Domain()
        {
            DomainClientApplication = new HashSet<DomainClientApplication>();
        }
        public int DomainId { get; set; }

        public string DomainName { get; set; }

        public Guid? TenantId { get; set; }

        public DateTime CreatedOn { get; set; }
      
        public string CreatedBy { get; set; }

        public virtual Tenant Tenant { get; set; }
   
        public virtual ICollection<DomainClientApplication> DomainClientApplication { get; set; }
    }
}
