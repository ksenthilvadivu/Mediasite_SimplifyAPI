using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DH.Curbside.Core.Domain;

namespace DH.Curbside.Infrastructure.Persistence.Mappings
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Table
            ToTable("User");

            // Key
            HasKey(x => x.UserId);

            // Fields
            Property(x => x.UserId).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Prefix).IsRequired();
            Property(x => x.FirstName).IsRequired();
            Property(x => x.LastName).IsRequired();
            Property(x => x.EmailAddress).IsRequired();
            Property(x => x.TenantId);
            Property(x => x.CreatedOn).HasColumnType("datetime2");
            Property(x => x.CreatedBy).IsRequired();

            // Relationship

            HasMany(e => e.UserClientApplication)
                .WithRequired(e => e.User)
                .HasForeignKey(f=>f.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}
