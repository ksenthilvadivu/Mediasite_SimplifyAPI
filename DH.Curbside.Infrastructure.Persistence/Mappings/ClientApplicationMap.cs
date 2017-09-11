using System.Data.Entity.ModelConfiguration;
using DH.Curbside.Core.Domain;

namespace DH.Curbside.Infrastructure.Persistence.Mappings
{
    public class ClientApplicationMap : EntityTypeConfiguration<ClientApplication>
    {
        public ClientApplicationMap()
        {
            // Table
            ToTable("ClientApplication");

            // Key
            HasKey(x => x.ClientApplicationId);

            // Fields
            Property(x => x.ClientApplicationId);
            Property(x => x.ClientApplicationToken);
            Property(x => x.ClientApplicationName).IsRequired();
            Property(x => x.IsMobile);
            Property(x => x.MinimumVersion);
            Property(x => x.LatestSupportedVersion);
            Property(x => x.UpgradeUrl);

            // Relationship
            HasMany(e => e.Tenants)
                .WithMany(e => e.ClientApplications)
                .Map(m => m.ToTable("ClientApplicationTenant").MapLeftKey("ClientApplicationId").MapRightKey("TenantId"));
        }
    }
}
