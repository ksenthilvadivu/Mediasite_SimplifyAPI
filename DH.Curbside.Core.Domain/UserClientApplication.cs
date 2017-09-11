using System;

namespace DH.Curbside.Core.Domain
{
    public class UserClientApplication
    {
        public int UserClientApplicationId { get; set; }

        public DateTime? DeProvisionedDate { get; set; }

        public DateTime? WhitelistedDate { get; set; }

        public int UserId { get; set; }

        public int? ClientApplicationId { get; set; }

        public DateTime? InvitationDate { get; set; }

        public DateTime? ReinviteDate { get; set; }

        public string UpdatedBy { get; set; }

        public virtual User User { get; set; }
    }
}