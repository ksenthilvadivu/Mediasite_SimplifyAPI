using System.Data.Entity.ModelConfiguration;
using DH.Curbside.Core.Domain;

namespace DH.Curbside.Infrastructure.Persistence.Mappings
{
    public class EmailValidationMap : EntityTypeConfiguration<EmailValidation>
    {
        public EmailValidationMap()
        {
            // Table
            ToTable("EmailValidation");

            // Key
            HasKey(x => x.EmailValidationId);

            // Fields
            Property(x => x.EmailValidationId);
            Property(x => x.FirstName).IsRequired();
            Property(x => x.LastName).IsRequired();
            Property(x => x.Email).IsRequired();
            Property(x => x.InvitedBy);
            Property(x => x.IssedDate).HasColumnType("datetime2");
            Property(x => x.ProviderId);
            Property(x => x.EmailValidationDate).HasColumnType("datetime2");
        }
    }
}
