using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DH.Curbside.Core.Domain
{
    public class Provider
    {
        public Provider()
        {
            EmailValidations = new HashSet<EmailValidation>();
            PatientCases = new HashSet<PatientCase>();
            PhoneValidations = new HashSet<PhoneValidation>();
            TermsOfServices = new HashSet<TermsOfService>();
            Roles = new HashSet<Role>();
        }

        public Guid ProviderId { get; set; }

        public Guid TenantId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string BIO { get; set; }

        public string Speciality { get; set; }

        public byte[] Picture { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string LoginUserName { get; set; }

        public string LoginSourceId { get; set; }

        public string LoginSourceSystem { get; set; }

        public DateTime? LoginDeactivatedDate { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public virtual ICollection<EmailValidation> EmailValidations { get; set; }

        public virtual ICollection<PatientCase> PatientCases { get; set; }

        public virtual ICollection<PhoneValidation> PhoneValidations { get; set; }

        public virtual Tenant Tenant { get; set; }

        public virtual ICollection<TermsOfService> TermsOfServices { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
