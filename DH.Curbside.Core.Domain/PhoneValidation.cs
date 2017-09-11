using System;

namespace DH.Curbside.Core.Domain
{
    public class PhoneValidation
    {
        public int PhoneValidationId { get; set; }

        public string Phone { get; set; }

        public string SMSCode { get; set; }

        public DateTime? SMSValidationDate { get; set; }

        public DateTime IssueDate { get; set; }

        public Guid ProviderId { get; set; }

        public int EmailValidationId { get; set; }

        public virtual Provider Provider { get; set; }
    }
}
