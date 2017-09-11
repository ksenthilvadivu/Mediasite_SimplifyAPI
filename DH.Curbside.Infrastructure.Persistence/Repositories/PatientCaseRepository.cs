using DH.Curbside.Core.Domain;
using DH.Curbside.Core.Persistence.RepositoryInterfaces;

namespace DH.Curbside.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Provides all patient case related operations 
    /// </summary>
    public class PatientCaseRepository : CurbsideRepository<PatientCase>, IPatientCaseRepository
    {
        /// <summary>
        /// Patient case repository constructor
        /// </summary>
        public PatientCaseRepository()
        {

        }
    }
}
