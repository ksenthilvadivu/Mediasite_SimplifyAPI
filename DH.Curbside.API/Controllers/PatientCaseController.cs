using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DH.Curbside.Core.Application.Exceptions;
using DH.Curbside.Core.Application.PatientCase;
using DH.Curbside.Core.Application.PatientCase.ViewModels;
using DH.Curbside.Core.Enterprise.Common;
using DH.Curbside.Core.Enterprise.Logging;
using ResponseCodes = DH.Curbside.Core.Enterprise.Common.ResponseCodes;
using ServiceResponse = DH.Curbside.Core.Enterprise.Common.ServiceResponse;

namespace DH.Curbside.API.Controllers
{
    /// <summary>
    /// Manage all patient case operations
    /// </summary>    [ApiVersion("1.0")]
    public class PatientCaseController : CurbsideBaseController
    {
        private readonly IPatientCaseApplication _patientCaseApplication;

        /// <summary>
        /// Patient case controller constructor
        /// </summary>
        /// <param name="patientCaseApplication"></param>
        public PatientCaseController(IPatientCaseApplication patientCaseApplication)
        {
            _patientCaseApplication = patientCaseApplication;
        }

        /// <summary>
        /// Add a new case for a patient
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="patientCaseViewModel">PatientCase View Model</param>
        /// <example url="http://localhost:50020/api/v1.0/d2e5a14f-1de2-4e29-891e-3d8252a13273/PatientCase"></example>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(CurbsideResponse))]
        [Route("api/v{version:apiVersion}/{tenantId}/case")]
        public async Task<HttpResponseMessage> CreatePatientCase(string tenantId, RequestPatientCaseViewModel patientCaseViewModel)
        {
            try
            {
                var response = await _patientCaseApplication.CreatePatientCase(tenantId, patientCaseViewModel);
                return Request.CreateResponse(HttpStatusCode.OK
                                        , ServiceResponse<CreatePatientCaseResponseViewModel>.Instance.BuildResponse(ResponseCodes.PATIENTCASES_ADDED, response));
            }
            catch (InvalidTenantIdException ex)
            {
                Logger.Instance.Log(ex.StackTrace, ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest
                                            , ServiceResponse.Instance.BuildResponse(ResponseCodes.INVALID_TENANT_ID));
            }
            catch (InvalidJsonException ex)
            {
                Logger.Instance.Log(ex.StackTrace, ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest
                                            , ServiceResponse.Instance.BuildResponse(ResponseCodes.INVALID_JSON_DATA));
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex.StackTrace, ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError
                                            , ServiceResponse.Instance.BuildResponse(ResponseCodes.INTERNAL_SEREVR_ERROR));
            }
        }

        /// <summary>
        /// Get list of cases, given providerId return only submitted or reviewed
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="providerId">Provider Id</param>
        /// <example url="http://localhost:50020/api/v1.0/dd1f863a-d7d9-4df6-b1f5-4f570185185d/PatientCase?providerId=0aa252c5-0545-4a32-978e-d815c9ea7bf5"></example>        
        /// <returns></returns>
        [HttpGet]
        [Route("api/v{version:apiVersion}/{tenantId}/case")]
        public HttpResponseMessage GetPatientCase(string tenantId, string providerId = null)
        {
            try
            {
                var patientCases = _patientCaseApplication.GetPatientCase(tenantId, providerId);
                return Request.CreateResponse(HttpStatusCode.OK
                                            , ServiceResponse<PatientCaseViewModel>.Instance.BuildResponse(ResponseCodes.OK, patientCases));
            }
            catch (InvalidTenantIdException ex)
            {
                Logger.Instance.Log(ex.StackTrace, ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest
                                            , ServiceResponse.Instance.BuildResponse(ResponseCodes.INVALID_TENANT_ID));
            }
            catch (InvalidProviderIdException ex)
            {
                Logger.Instance.Log(ex.StackTrace, ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest
                                            , ServiceResponse.Instance.BuildResponse(ResponseCodes.INVALID_PROVIDER_ID));
            }
            catch (PatientCaseNotAvailableException ex)
            {
                Logger.Instance.Log(ex.StackTrace, ex);
                return Request.CreateResponse(HttpStatusCode.NotFound
                                            , ServiceResponse.Instance.BuildResponse(ResponseCodes.PATIENTCASES_NOT_AVAILABLE));
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex.StackTrace, ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError
                                            , ServiceResponse.Instance.BuildResponse(ResponseCodes.INTERNAL_SEREVR_ERROR));
            }
        }

        /// <summary>
        /// Get individual case detail
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="caseId">Case Id</param>
        /// <example url="http://localhost:50020/api/v1.0/PatientCase/dd1f863a-d7d9-4df6-b1f5-4f570185185d/0aa252c5-0545-4a32-978e-d815c9ea7bf5"></example>        
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(CurbsideResponse))]
        [Route("api/v{version:apiVersion}/{tenantId}/case/{caseId}")]
        public HttpResponseMessage GetSinglePatientCase(string tenantId, string caseId)
        {
            try
            {
                var patientCase = _patientCaseApplication.GetSinglePatientCase(tenantId, caseId);
                return Request.CreateResponse(HttpStatusCode.OK
                                        , ServiceResponse<PatientCaseInfo>.Instance.BuildResponse(ResponseCodes.OK, patientCase));
            }
            catch (InvalidTenantIdException ex)
            {
                Logger.Instance.Log(ex.StackTrace, ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest
                                            , ServiceResponse.Instance.BuildResponse(ResponseCodes.INVALID_TENANT_ID));
            }
            catch (InvalidCaseIdException ex)
            {
                Logger.Instance.Log(ex.StackTrace, ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest
                                            , ServiceResponse.Instance.BuildResponse(ResponseCodes.INVALID_CASE_ID));
            }
            catch (PatientCaseNotAvailableException ex)
            {
                Logger.Instance.Log(ex.StackTrace, ex);
                return Request.CreateResponse(HttpStatusCode.NotFound
                                            , ServiceResponse.Instance.BuildResponse(ResponseCodes.PATIENTCASE_NOT_AVAILABLE));
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex.StackTrace, ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError
                                            , ServiceResponse.Instance.BuildResponse(ResponseCodes.INTERNAL_SEREVR_ERROR));
            }
        }

        /// <summary>
        /// Gets patient case status master data
        /// </summary>
        /// <param name="tenantId">tenantId</param>
        /// <example url="http://localhost:50020/api/v1/PatienceCaseMasterStatus"></example>        
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(CurbsideResponse))]
        [Route("api/v{version:apiVersion}/PatienceCaseMasterStatus")]
        public HttpResponseMessage GetPatientCaseMasterStatus()
        {
            try
            {
                var caseStatusDescriptions = _patientCaseApplication.GetPatientCaseMasterStatus();
                return Request.CreateResponse(HttpStatusCode.OK
                                       , ServiceResponse<PatientCaseStatusMasterViewModel>.Instance.BuildResponse(ResponseCodes.OK, caseStatusDescriptions));
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex.StackTrace, ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError
                                            , ServiceResponse.Instance.BuildResponse(ResponseCodes.INTERNAL_SEREVR_ERROR));
            }
        }
    }
}