using System;

namespace DH.Curbside.Core.Application.WhitelistDomain.ViewModels
{
    public class DomainClientApplicationViewModel
    {
        public int DomainClientApplicationId { get; set; }

        public string DomainName { get; set; }

        public int TenantId { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? DeactivateDate { get; set; }
    }
}
