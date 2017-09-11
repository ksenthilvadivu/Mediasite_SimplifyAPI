using System.Data.Entity.ModelConfiguration;
using DH.Curbside.Core.Domain;

namespace DH.Curbside.Infrastructure.Persistence.Mappings
{
    public class PatientCaseAttachmentMap : EntityTypeConfiguration<PatientCaseAttachment>
    {
        public PatientCaseAttachmentMap()
        {
            // Table
            ToTable("PatientCaseAttachment");

            // Key
            HasKey(x => x.PatientCaseAttachmentId);

            // Fields
            Property(x => x.PatientCaseId);
            Property(x => x.MediaAttachmentId);
        }
    }
}
