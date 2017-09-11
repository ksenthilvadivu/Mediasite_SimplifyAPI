using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DH.Curbside.Core.Domain;

namespace DH.Curbside.Infrastructure.Persistence.Mappings
{
    public class DomainClientApplicationMap : EntityTypeConfiguration<DomainClientApplication>
    {
        public DomainClientApplicationMap()
        {
            // Table
            ToTable("DomainClientApplication");

            // Key
            HasKey(x => x.DomainClientApplicationId);

            // Fields
            Property(x => x.DomainClientApplicationId).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.DomainId);
            Property(x => x.CreatedOn).HasColumnType("datetime2");
            Property(x => x.CreatedBy).IsRequired();
            Property(x => x.DeProvisionedDate).HasColumnType("datetime2");
            Property(x => x.ClientApplicationId);
        }
    }
}
