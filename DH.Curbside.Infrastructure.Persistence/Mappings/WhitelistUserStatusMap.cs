using System.Data.Entity.ModelConfiguration;
using DH.Curbside.Core.Domain;

namespace DH.Curbside.Infrastructure.Persistence.Mappings
{
    public class WhitelistUserStatusMap : EntityTypeConfiguration<WhitelistUserStatus>
    {
        public WhitelistUserStatusMap()
        {
            // Table
            ToTable("WhitelistUserStatus");

            // Key
            HasKey(x => x.WhiteListUserStatusId);

            // Fields
            Property(x => x.WhiteListUserStatusId).IsRequired();
            Property(x => x.StatusName);
            Property(x => x.StatusDescription);
        }
    }
}
