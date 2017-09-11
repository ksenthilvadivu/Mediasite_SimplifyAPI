using System;

namespace DH.Curbside.Core.Domain
{
    public class DomainClientApplication
    {
        public int DomainClientApplicationId { get; set; }

        public int DomainId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? DeProvisionedDate { get; set; }

        public int? ClientApplicationId { get; set; }

        public virtual Domain Domain { get; set; }
    }
}