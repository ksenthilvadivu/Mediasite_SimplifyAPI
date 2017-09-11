using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DH.Curbside.Core.Application.Exceptions;
using DH.Curbside.Core.Application.WhitelistDomain;
using DH.Curbside.Core.Application.WhitelistDomain.ViewModels;
using DH.Curbside.Core.Enterprise.Common;
using DH.Curbside.Core.Enterprise.Logging;

namespace DH.Curbside.API.Controllers
{
    /// <summary>
    /// Manages all whitelist domain related operations
    /// </summary>
    public class WhitelistDomainController : CurbsideBaseController
    {
        private readonly IWhitelistDomainApplication _whitelistDomainApplication;

        /// <summary>
        /// Constructor for DomainController
        /// </summary>
        /// <param name="whitelistDomainApplication">IWhitelistApplication object</param>
        public WhitelistDomainController(IWhitelistDomainApplication whitelistDomainApplication)
        {
            _whitelistDomainApplication = whitelistDomainApplication;
        }

        /// <summary>
        /// Adds a domain 
        /// </summary>
        /// <param name="domainViewModels">Request Domain View Model</param>
        /// <param name="tenantId">Tenant Id</param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(CurbsideResponse))]
        [Route("api/v{version:apiVersion}/{tenantId}/whitelistDomain")]
        public async Task<HttpResponseMessage> CreateDomains(List<RequestDomainViewModel> domainViewModels, string tenantId)//http://localhost:50020/api/v1.0/DD1F863A-D7D9-4DF6-B1F5-4F570185185D/WhiteList
        {
            try
            {
                await _whitelistDomainApplication.AddDomain(domainViewModels, tenantId);

                return Request.CreateResponse(HttpStatusCode.OK
                                        , ServiceResponse.Instance.BuildResponse(ResponseCodes.WHITELISTDOMAIN_ADDED));                                
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
                return Request.CreateResponse(HttpStatusCode.NotFound
                                            , ServiceResponse.Instance.BuildResponse(ResponseCodes.INTERNAL_SEREVR_ERROR));
            }
        }
    }
}