using System.Data.Entity.ModelConfiguration;
using DH.Curbside.Core.Domain;

namespace DH.Curbside.Infrastructure.Persistence.Mappings
{
    public class MediaAttachmentMap : EntityTypeConfiguration<MediaAttachment>
    {
        public MediaAttachmentMap()
        {
            // Table
            ToTable("MediaAttachment");

            // Key
            HasKey(x => x.MediaAttachmentId);

            // Fields
            Property(x => x.MediaAttachmentId);
            Property(x => x.AttachmentThumbnail).HasColumnType("image");
            Property(x => x.AttachmentSourceId);
            Property(x => x.AttachmentSourceSystem);
            Property(x => x.AttachmentURL);

            // Relationship
            HasMany(e => e.PatientCaseAttachments)
                .WithRequired(e => e.MediaAttachment)
                .WillCascadeOnDelete(false);
        }
    }
}
