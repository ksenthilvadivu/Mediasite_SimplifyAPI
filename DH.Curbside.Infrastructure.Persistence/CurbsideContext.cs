using System.Data.Entity;
using DH.Curbside.Core.Domain;
using DH.Curbside.Infrastructure.Persistence.Mappings;

namespace DH.Curbside.Infrastructure.Persistence
{
    public class CurbsideContext : DbContext
    {
        public CurbsideContext()
            : base("CurbsideConnection")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<CurbsideContext>());
        }

        public virtual DbSet<ClientApplication> ClientApplications { get; set; }

        public virtual DbSet<Domain> Domains { get; set; }

        public virtual DbSet<DomainClientApplication> DomainClientApplications { get; set; }

        public virtual DbSet<EmailValidation> EmailValidations { get; set; }

        public virtual DbSet<MediaAttachment> MediaAttachments { get; set; }

        public virtual DbSet<PatientCase> PatientCases { get; set; }

        public virtual DbSet<PatientCaseAttachment> PatientCaseAttachments { get; set; }

        public virtual DbSet<PatientCaseHistory> PatientCaseHistories { get; set; }

        public virtual DbSet<PatientCaseReview> PatientCaseReviews { get; set; }

        public virtual DbSet<PhoneValidation> PhoneValidations { get; set; }

        public virtual DbSet<Provider> Providers { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<Status> Status { get; set; }

        public virtual DbSet<Tenant> Tenants { get; set; }

        public virtual DbSet<TermsOfService> TermsOfServices { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<UserClientApplication> UserClientApplications { get; set; }

        public virtual DbSet<WhitelistUserStatus> WhitelistUserStatus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<CurbsideContext>(null);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new PatientCaseHistoryMap());
            modelBuilder.Configurations.Add(new PatientCaseReviewMap());
            modelBuilder.Configurations.Add(new ClientApplicationMap());
            modelBuilder.Configurations.Add(new DomainMap());
            modelBuilder.Configurations.Add(new EmailValidationMap());
            modelBuilder.Configurations.Add(new MediaAttachmentMap());
            modelBuilder.Configurations.Add(new PatientCaseAttachmentMap());
            modelBuilder.Configurations.Add(new PatientCaseMap());
            modelBuilder.Configurations.Add(new ProviderMap());
            modelBuilder.Configurations.Add(new TenantMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserClientApplicationMap());
            modelBuilder.Configurations.Add(new DomainClientApplicationMap());
            modelBuilder.Configurations.Add(new WhitelistUserStatusMap());
        }
    }
}
