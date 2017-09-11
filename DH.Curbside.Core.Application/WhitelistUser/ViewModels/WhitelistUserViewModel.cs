using System;

namespace DH.Curbside.Core.Application.WhitelistUser.ViewModels
{
    public class WhitelistUserViewModel
    {
        public int WhitelistUserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int TenantId { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? DeactivatedDate { get; set; }
    }
}
