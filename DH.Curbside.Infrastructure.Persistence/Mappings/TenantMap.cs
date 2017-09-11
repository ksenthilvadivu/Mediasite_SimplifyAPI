using System.Data.Entity.ModelConfiguration;
using DH.Curbside.Core.Domain;

namespace DH.Curbside.Infrastructure.Persistence.Mappings
{
    public class TenantMap : EntityTypeConfiguration<Tenant>
    {
        public TenantMap()
        {
            // Table
            ToTable("Tenant");

            // Key
            HasKey(x =>  x.TenantId);

            // Fields
            Property(x => x.TenantId).IsRequired();
            Property(x => x.TenantName);

            // Relationship
            HasMany(e => e.Providers)
                .WithRequired(e => e.Tenant)
                .WillCascadeOnDelete(false);
        }
    }
}
