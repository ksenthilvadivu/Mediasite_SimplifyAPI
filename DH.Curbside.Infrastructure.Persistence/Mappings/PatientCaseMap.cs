using System.Data.Entity.ModelConfiguration;
using DH.Curbside.Core.Domain;

namespace DH.Curbside.Infrastructure.Persistence.Mappings
{
    public class PatientCaseMap : EntityTypeConfiguration<PatientCase>
    {
        public PatientCaseMap()
        {
            // Table
            ToTable("PatientCase");

            // Key
            HasKey(x => x.PatientCaseId);

            // Fields
            Property(x => x.PatientCaseId);
            Property(x => x.PatientCaseGuid);
            Property(x => x.CaseTitle).IsRequired();
            Property(x => x.CaseDescription).IsUnicode(false);
            Property(x => x.ProviderId);
            Property(x => x.CaseStatus).IsRequired();
            Property(x => x.CreatedOn).HasColumnType("datetime2"); ;

            // Relationship
            HasMany(e => e.PatientCaseAttachments)
                .WithRequired(e => e.PatientCase)
                .HasForeignKey(e=>e.PatientCaseId)
                .WillCascadeOnDelete(false);

            HasMany(e => e.PatientCaseHistories)
                .WithRequired(e => e.PatientCase)
                .WillCascadeOnDelete(false);

            HasMany(e => e.PatientCaseReviews)
                .WithRequired(e => e.PatientCase)
                .WillCascadeOnDelete(false);
        }
    }
}
