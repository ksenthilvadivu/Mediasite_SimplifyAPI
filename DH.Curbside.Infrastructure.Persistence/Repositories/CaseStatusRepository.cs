using DH.Curbside.Core.Domain;
using DH.Curbside.Core.Persistence.RepositoryInterfaces;

namespace DH.Curbside.Infrastructure.Persistence.Repositories
{
    public class PatientCaseStatusRepository : CurbsideRepository<Status>, IPatientCaseStatusRepository
    {
        public PatientCaseStatusRepository()
        {

        }
    }
}
