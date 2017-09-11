using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DH.Curbside.Core.Application.Exceptions;
using DH.Curbside.Core.Application.PatientCase.ViewModels;
using DH.Curbside.Core.Domain;
using DH.Curbside.Core.Persistence.RepositoryInterfaces;

namespace DH.Curbside.Core.Application.PatientCase
{
    /// <summary>
    /// Patient case application class
    /// </summary>
    public class PatientCaseApplication : IPatientCaseApplication
    {
        private readonly IPatientCaseRepository _patientCaseRepository;
        private readonly IMapper _mapper;
        private readonly IProviderRepository _providerRepository;
        private readonly IPatientCaseStatusRepository _patientCaseStatusRepository;

        /// <summary>
        /// PatientCase application constructor
        /// </summary>
        /// <param name="patientCaseRepository">PatientCaseRepository Interface</param>
        /// <param name="mapper">Mapper Interface</param>
        /// <param name="providerRepository">ProviderRepository Interface</param>
        public PatientCaseApplication(IPatientCaseRepository patientCaseRepository
                                    , IMapper mapper
                                    , IProviderRepository providerRepository
                                    , IPatientCaseStatusRepository patientCaseStatusRepository)
        {
            _patientCaseRepository = patientCaseRepository;
            _mapper = mapper;
            _providerRepository = providerRepository;
            _patientCaseStatusRepository = patientCaseStatusRepository;
        }

        /// <summary>
        /// Get list of patient record by tenant id
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="providerId">Provider Id</param>
        /// <returns>List of Patient Cases</returns>
        public PatientCaseViewModel GetPatientCase(string tenantId, string providerId)
        {
            Guid tenantGuid, providerGuid;
            List<PatientCaseInfo> patientCaseInfo;
            tenantGuid = ParseTenantGuid(tenantId);

            if (!string.IsNullOrEmpty(providerId))
            {
                Guid.TryParse(providerId, out providerGuid);
                if (providerGuid == Guid.Empty)
                    throw new InvalidProviderIdException();

                var patientCases = _patientCaseRepository.All().Where(x => x.ProviderId == providerGuid).ToList();
                patientCaseInfo = _mapper.Map<List<Domain.PatientCase>, List<PatientCaseInfo>>(patientCases);
            }
            else
            {
                Guid.TryParse(tenantId, out tenantGuid);
                if (tenantGuid == Guid.Empty)
                    throw new InvalidTenantIdException();

                var providers = _providerRepository.All().Where(x => x.TenantId == tenantGuid).ToList();
                patientCaseInfo = _mapper.Map<List<Provider>, List<PatientCaseInfo>>(providers);
            }

            if (patientCaseInfo.Count == 0)
                throw new PatientCaseNotAvailableException();

            var patientCaseViewModel = new PatientCaseViewModel();
            foreach (var patientCase in patientCaseInfo)
            {
                patientCaseViewModel.PatientCases.Add(patientCase);
            }

            return patientCaseViewModel;
        }        

        /// <summary>
        /// Get single patient record by tenant id and case id
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="caseId">Case Id</param>
        /// <returns></returns>
        public PatientCaseInfo GetSinglePatientCase(string tenantId, string caseId)
        {
            Guid caseGuid, tenantGuid;
            PatientCaseInfo patientCaseInfo = null;

            Guid.TryParse(tenantId, out tenantGuid);
            if (tenantGuid == Guid.Empty)
                throw new InvalidTenantIdException();

            Guid.TryParse(caseId, out caseGuid);
            if (caseGuid == Guid.Empty)
                throw new InvalidCaseIdException();

            Domain.PatientCase patientCase;

            patientCase = _patientCaseRepository.All().FirstOrDefault(x => x.PatientCaseGuid == caseGuid);
            if (patientCase == null)
                throw new PatientCaseNotAvailableException();

            var providers = (from provider in _providerRepository.All().Where(p => p.TenantId == tenantGuid) select provider).ToList();

            foreach (var provider in providers)
            {
                if (patientCase.ProviderId == provider.ProviderId)
                {
                    patientCaseInfo = _mapper.Map<Domain.PatientCase, PatientCaseInfo>(patientCase);
                }
            }
            if(patientCaseInfo == null)
                throw new PatientCaseNotAvailableException();
            return patientCaseInfo;
        }

        /// <summary>
        /// create patient case implementation method
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="patientCaseViewModel"></param>
        /// <returns></returns>        
        public async Task<CreatePatientCaseResponseViewModel> CreatePatientCase(string tenantId, RequestPatientCaseViewModel patientCaseViewModel)
        {
            Guid tenantGuid;
            Guid.TryParse(tenantId, out tenantGuid);
            if (tenantGuid == Guid.Empty)
                throw new InvalidTenantIdException();

            var patientCaseModels = _mapper.Map<RequestPatientCaseViewModel, Domain.PatientCase>(patientCaseViewModel);

            if (patientCaseModels == null)
                throw new InvalidJsonException();

            patientCaseModels.CreatedOn = DateTime.Now;
            patientCaseModels.PatientCaseGuid = Guid.NewGuid();

            var providerInfo = _providerRepository.All().FirstOrDefault(data => data.TenantId.Equals(tenantGuid)
                                                                           && data.ProviderId.Equals(patientCaseModels.ProviderId));

            if (providerInfo != null)
            {
                await _patientCaseRepository.Add(patientCaseModels);
            }
            var responseBody = _mapper.Map<Domain.PatientCase, CreatePatientCaseResponseViewModel>(patientCaseModels);
            return responseBody;
        }

        /// <summary>
        /// Get case status master details 
        /// </summary>
        /// <returns></returns>
        public PatientCaseStatusMasterViewModel GetPatientCaseMasterStatus()
        {         
            var patientStatuses = _patientCaseStatusRepository.All().ToList();
            var patientCaseStatusVM = _mapper.Map<List<Status>, List<PatientCaseStatusDescription>>(patientStatuses);

            var caseStatusViewModel = new PatientCaseStatusMasterViewModel();
            foreach (var patientStatus in patientCaseStatusVM)
            {
                caseStatusViewModel.CaseStatuses.Add(patientStatus);
            }
            return caseStatusViewModel;
        }

        /// <summary>
        /// returns parsed GUID or error
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        private static Guid ParseTenantGuid(string tenantId)
        {
            Guid tenantGuid;
            Guid.TryParse(tenantId, out tenantGuid);

            if (tenantGuid == Guid.Empty)
                throw new InvalidTenantIdException();
            return tenantGuid;
        }
    }

}
