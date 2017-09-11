using System.Data.Entity.ModelConfiguration;
using DH.Curbside.Core.Domain;

namespace DH.Curbside.Infrastructure.Persistence.Mappings
{
    public class PatientCaseHistoryMap : EntityTypeConfiguration<PatientCaseHistory>
    {
        public PatientCaseHistoryMap()
        {
            // Table
            ToTable("PatientCaseHistory");

            // Key
            HasKey(x => x.PatientCaseHistoryId);

            // Fields
            Property(x => x.PatientCaseHistoryId);
            Property(x => x.PatientCaseId);
            Property(x => x.CaseStatusId);
            Property(x => x.CreatedOn).HasColumnType("datetime2");
            Property(x => x.CreatedBy);
        }
    }
}
