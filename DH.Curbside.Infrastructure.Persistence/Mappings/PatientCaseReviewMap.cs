using System.Data.Entity.ModelConfiguration;
using DH.Curbside.Core.Domain;

namespace DH.Curbside.Infrastructure.Persistence.Mappings
{
    public class PatientCaseReviewMap : EntityTypeConfiguration<PatientCaseReview>
    {
        public PatientCaseReviewMap()
        {
            // Table
            ToTable("PatientCaseReview");

            // Key
            HasKey(x => x.PatientCaseReviewId);

            // Fields
            Property(x => x.PatientCaseReviewId);
            Property(x => x.PatientCaseId);
            Property(x => x.ReviewerProviderId);
            Property(x => x.ReviewNotes).IsRequired().HasColumnType("text").IsUnicode(false);
            Property(x => x.DiagnosisCategory).IsRequired();
            Property(x => x.ReviewStatus);
            Property(x => x.CreatedOn).HasColumnType("datetime2");
            Property(x => x.UpdatedOn).HasColumnType("datetime2");
        }
    }
}
