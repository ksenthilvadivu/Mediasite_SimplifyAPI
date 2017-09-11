using System;

namespace DH.Curbside.Core.Domain
{
    public class EmailValidation
    {
        public int EmailValidationId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

       public string Email { get; set; }

        public string InvitedBy { get; set; }

        public DateTime IssedDate { get; set; }

        public Guid ProviderId { get; set; }

        public DateTime? EmailValidationDate { get; set; }

        public virtual Provider Provider { get; set; }
    }
}
