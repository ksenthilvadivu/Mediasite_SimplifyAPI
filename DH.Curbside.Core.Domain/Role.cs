using System.Collections.Generic;

namespace DH.Curbside.Core.Domain
{
    public class Role
    {
        public Role()
        {
            Providers = new HashSet<Provider>();
        }

        public int RoleId { get; set; }
       
        public string Name { get; set; }

        public virtual ICollection<Provider> Providers { get; set; }
    }
}
