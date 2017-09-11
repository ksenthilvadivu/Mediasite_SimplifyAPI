using System.Data.Entity.ModelConfiguration;
using DH.Curbside.Core.Domain;

namespace DH.Curbside.Infrastructure.Persistence.Mappings
{
    public class ProviderMap : EntityTypeConfiguration<Provider>
    {
        public ProviderMap()
        {
            // Table
            ToTable("Provider");

            // Key
            HasKey(x => x.ProviderId);

            // Fields
            Property(x => x.ProviderId);
            Property(x => x.TenantId);
            Property(x => x.FirstName).IsRequired();
            Property(x => x.LastName).IsRequired();
            Property(x => x.BIO).HasMaxLength(2000);
            Property(x => x.Speciality);
            Property(x => x.Picture);
            Property(x => x.Phone).IsRequired();
            Property(x => x.Email).IsRequired();
            Property(x => x.LoginUserName);
            Property(x => x.LoginSourceId);
            Property(x => x.LoginSourceSystem).IsRequired();
            Property(x => x.LoginDeactivatedDate).HasColumnType("datetime2");
            Property(x => x.CreatedOn).HasColumnType("datetime2");
            Property(x => x.UpdatedOn).HasColumnType("datetime2");

            // Relationship
            HasMany(e => e.EmailValidations)
                .WithRequired(e => e.Provider)
                .WillCascadeOnDelete(false);

            HasMany(e => e.PhoneValidations)
            .WithRequired(e => e.Provider)
            .WillCascadeOnDelete(false);

            HasMany(e => e.Roles)
            .WithMany(e => e.Providers)
            .Map(m => m.ToTable("ProviderRole").MapLeftKey("ProviderId").MapRightKey("RoleId"));
        }
    }
}




