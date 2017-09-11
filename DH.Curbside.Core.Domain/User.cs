using System;
using System.Collections.Generic;

namespace DH.Curbside.Core.Domain
{
    public class User
    {
        public User()
        {
            UserClientApplication = new HashSet<UserClientApplication>();
        }

        public int UserId { get; set; }

        public string Prefix { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public Guid? TenantId { get; set; }

        public DateTime CreatedOn { get; set; }
       
        public string CreatedBy { get; set; }

        public virtual Tenant Tenant { get; set; }
      
        public virtual ICollection<UserClientApplication> UserClientApplication { get; set; }
    }
}
