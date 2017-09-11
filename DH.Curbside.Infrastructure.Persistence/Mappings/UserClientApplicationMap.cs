using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DH.Curbside.Core.Domain;

namespace DH.Curbside.Infrastructure.Persistence.Mappings
{
    public class UserClientApplicationMap : EntityTypeConfiguration<UserClientApplication>
    {
        public UserClientApplicationMap()
        {
            // Table
            ToTable("UserClientApplication");

            // Key
            HasKey(x => x.UserClientApplicationId);

            // Fields
            Property(x => x.UserClientApplicationId).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.DeProvisionedDate).HasColumnType("datetime2");
            Property(x => x.WhitelistedDate).HasColumnType("datetime2");            
            Property(x => x.UserId);
            Property(x => x.ClientApplicationId);
            Property(x => x.InvitationDate);
            Property(x => x.ReinviteDate);
        }
    }
}
