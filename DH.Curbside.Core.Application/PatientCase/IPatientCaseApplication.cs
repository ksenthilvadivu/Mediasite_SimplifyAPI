using System.Threading.Tasks;
using DH.Curbside.Core.Application.PatientCase.ViewModels;

namespace DH.Curbside.Core.Application.PatientCase
{
    /// <summary>
    /// Patient case application interface
    /// </summary>
    public interface IPatientCaseApplication
    {
        /// <summary>
        /// Add a new case for a patient
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="patientCaseViewModel">PatientCase View Model</param>
        /// <returns>True or False</returns>
        Task<CreatePatientCaseResponseViewModel> CreatePatientCase(string tenantId,RequestPatientCaseViewModel patientCaseViewModel);

        /// <summary>
        /// Get list of cases, given providerId return only submitted or reviewed
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="providerId">Provider Id</param>
        /// <returns>List of Patient Cases</returns>
        PatientCaseViewModel GetPatientCase(string tenantId, string providerId);

        /// <summary>
        /// Get individual case detail
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="caseId">Case Id</param>
        /// <returns>Patient Case</returns>
        PatientCaseInfo GetSinglePatientCase(string tenantId, string caseId);

        /// <summary>
        /// Get all statuses descriptions related to case
        /// </summary>
        /// <returns>Patient case master status</returns>
        PatientCaseStatusMasterViewModel GetPatientCaseMasterStatus();
    }
}
