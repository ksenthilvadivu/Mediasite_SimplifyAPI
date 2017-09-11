using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DH.Curbside.Core.Domain;

namespace DH.Curbside.Infrastructure.Persistence.Mappings
{
    public class DomainMap : EntityTypeConfiguration<Domain>
    {
        public DomainMap()
        {
            // Table
            ToTable("Domain");

            // Key
            HasKey(x => x.DomainId);

            // Fields
            Property(x => x.DomainId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.DomainName).IsRequired();
            Property(x => x.TenantId);
            Property(x => x.CreatedOn).HasColumnType("datetime2");
            Property(x => x.CreatedBy);


            // Relationship

            HasMany(e => e.DomainClientApplication)
                .WithRequired(e => e.Domain)
                .HasForeignKey(f => f.DomainId)
                .WillCascadeOnDelete(false);
        }
    }
}
