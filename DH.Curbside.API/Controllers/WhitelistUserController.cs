using Microsoft.Web.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DH.Curbside.Core.Application.Exceptions;
using DH.Curbside.Core.Application.WhitelistUser;
using DH.Curbside.Core.Application.WhitelistUser.ViewModels;
using DH.Curbside.Core.Enterprise.Common;
using DH.Curbside.Core.Enterprise.Logging;
using ResponseCodes = DH.Curbside.Core.Enterprise.Common.ResponseCodes;
using ServiceResponse = DH.Curbside.Core.Enterprise.Common.ServiceResponse;

namespace DH.Curbside.API.Controllers
{
    /// <summary>
    /// Manages all whitelist user related operations
    /// </summary>
    [ApiVersion("1.0")]
    public class WhitelistUserController : CurbsideBaseController
    {
        private readonly IWhitelistUserApplication _whitelistApplication;

        /// <summary>
        /// Constructor for WhitelistController
        /// </summary>
        /// <param name="whitelistApplication">IWhitelistApplication object</param>
        public WhitelistUserController(IWhitelistUserApplication whitelistApplication)
        {
            _whitelistApplication = whitelistApplication;
        }

        /// <summary>
        /// Get list of whitelisted users
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <example url="http://localhost:50020/api/v1.0/DD1F863A-D7D9-4DF6-B1F5-4F570185185D/whitelist"></example>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(CurbsideResponse))]
        [Route("api/v{version:apiVersion}/{tenantId}/whitelist")]
        public HttpResponseMessage GetWhitelistUsers(string tenantId)
        {
            try
            {
                var whiteListUsers = _whitelistApplication.GetWhitelistUsers(tenantId);
                return Request.CreateResponse(HttpStatusCode.OK
                                            , ServiceResponse<UserViewModel>.Instance.BuildResponse(ResponseCodes.OK, whiteListUsers));
            }
            catch (InvalidTenantIdException ex)
            {
                Logger.Instance.Log(ex.StackTrace, ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest
                                            , ServiceResponse.Instance.BuildResponse(ResponseCodes.INVALID_TENANT_ID));
            }
            catch (WhitelistUserNotAvaliableException ex)
            {
                Logger.Instance.Log(ex.StackTrace, ex);
                return Request.CreateResponse(HttpStatusCode.NotFound
                                            , ServiceResponse.Instance.BuildResponse(ResponseCodes.WHITELISTUSER_NOT_AVAILABLE));
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex.StackTrace, ex);
                return Request.CreateResponse(HttpStatusCode.NotFound
                                            , ServiceResponse.Instance.BuildResponse(ResponseCodes.RESOURCE_NOTFOUND));
            }
        }

        /// <summary>
        /// Get user with whitelistinginfo
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="email">email</param>
        /// <example url="http://localhost:50020/api/v1.0/DD1F863A-D7D9-4DF6-B1F5-4F570185185D/whitelist/k@s.com"></example>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(CurbsideResponse))]
        [Route("api/v{version:apiVersion}/{tenantId}/whitelist/{email}")]
        public HttpResponseMessage GetWhitelistUser(string tenantId, string email)
        {
            try
            {
                var whiteListUsers = _whitelistApplication.GetWhitelistUser(tenantId, email);

                return Request.CreateResponse(HttpStatusCode.NotFound
                                            , ServiceResponse<UserInfo>.Instance.BuildResponse(ResponseCodes.OK, whiteListUsers));
            }
            catch (InvalidTenantIdException ex)
            {
                Logger.Instance.Log(ex.StackTrace, ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest
                                            , ServiceResponse.Instance.BuildResponse(ResponseCodes.INVALID_TENANT_ID));
            }
            catch (InvalidEmailException ex)
            {
                Logger.Instance.Log(ex.StackTrace, ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest
                                            , ServiceResponse.Instance.BuildResponse(ResponseCodes.INVALID_EMAIL_ID));
            }
            catch (WhitelistUserNotAvaliableException ex)
            {
                Logger.Instance.Log(ex.StackTrace, ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest
                                            , ServiceResponse.Instance.BuildResponse(ResponseCodes.WHITELISTUSER_NOT_AVAILABLE));
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex.StackTrace, ex);
                return Request.CreateResponse(HttpStatusCode.NotFound
                                        , ServiceResponse.Instance.BuildResponse(ResponseCodes.RESOURCE_NOTFOUND));
            }
        }

        /// <summary>
        /// Deprovision an user from whitelist
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="email">Email ID</param>
        /// <param name="appId">Application Id</param>
        /// <returns></returns>
        [HttpDelete]
        [ResponseType(typeof(CurbsideResponse))]
        [Route("api/v{version:apiVersion}/{tenantId}/whitelist/{email}")]
        public async Task<HttpResponseMessage> DeprovisionUser(string tenantId, string email, int appId) //http://localhost:50020/api/v1.0/DD1F863A-D7D9-4DF6-B1F5-4F570185185D/user/2?appId=2
        {//todo : validate with sai
            try
            {
                await _whitelistApplication.DeprovisionUser(tenantId, email, appId);
                return Request.CreateResponse(HttpStatusCode.OK
                                            , ServiceResponse.Instance.BuildResponse(ResponseCodes.WHITELISTEUSER_DEPROVISIONED));
            }
            catch (InvalidTenantIdException ex)
            {
                Logger.Instance.Log(ex.StackTrace, ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest
                                            , ServiceResponse.Instance.BuildResponse(ResponseCodes.INVALID_TENANT_ID));
            }
            catch (InvalidEmailException ex)
            {
                Logger.Instance.Log(ex.StackTrace, ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest
                                            , ServiceResponse.Instance.BuildResponse(ResponseCodes.INVALID_EMAIL_ID));
            }
            catch (WhitelistUserNotAvaliableException ex)
            {
                Logger.Instance.Log(ex.StackTrace, ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest
                                            , ServiceResponse.Instance.BuildResponse(ResponseCodes.WHITELISTUSER_NOT_AVAILABLE));
            }
            catch(InvalidApplicationIdException ex)
            {
                Logger.Instance.Log(ex.StackTrace, ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest
                                            , ServiceResponse.Instance.BuildResponse(ResponseCodes.INVALID_APPLICATION_ID));
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex.StackTrace, ex);
                return Request.CreateResponse(HttpStatusCode.NotFound
                                        , ServiceResponse.Instance.BuildResponse(ResponseCodes.INTERNAL_SEREVR_ERROR));
            }
        }

        /// <summary>
        /// Add new users to the whitelist
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(CurbsideResponse))]
        [Route("api/v{version:apiVersion}/{tenantId}/whitelistUser")]
        public async Task<HttpResponseMessage> CreateWhitelistUser(List<RequestUserViewModel> whitelistUserViewModels, string tenantId)//http://localhost:50020/api/v1.0/DD1F863A-D7D9-4DF6-B1F5-4F570185185D/whitelistUser
        {
            try
            {
                await _whitelistApplication.CreateWhitelistUser(whitelistUserViewModels, tenantId);
                return Request.CreateResponse(HttpStatusCode.OK
                                            , ServiceResponse.Instance.BuildResponse(ResponseCodes.WHITELISTEUSER_ADDED));
            }
            catch(InvalidTenantIdException ex)
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


        /// <summary>
        /// InviteUsers 
        /// </summary>
        /// <param name="inviteViewModel">InviteViewModel object</param>
        /// <param name="tenantId">Tenant Id</param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(CurbsideResponse))]
        [Route("api/v{version:apiVersion}/{tenantId}/Invite")]
        public HttpResponseMessage InviteUsers(List<RequestInviteViewModel> inviteViewModel,
            string tenantId) //http://localhost:50020/api/v1.0/DD1F863A-D7D9-4DF6-B1F5-4F570185185D/Invite
        {
            try
            {
                _whitelistApplication.InviteWhitelistUser(inviteViewModel, tenantId);
                return Request.CreateResponse(HttpStatusCode.OK
                                            , ServiceResponse.Instance.BuildResponse(ResponseCodes.INVITATION_SENT));
            }
            catch (InvalidJsonException ex)
            {
                Logger.Instance.Log(ex.StackTrace, ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest
                                            , ServiceResponse.Instance.BuildResponse(ResponseCodes.INVALID_JSON_DATA));
            }
            catch (InvalidTenantIdException ex)
            {
                Logger.Instance.Log(ex.StackTrace, ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest
                                            , ServiceResponse.Instance.BuildResponse(ResponseCodes.INVALID_TENANT_ID));
            }
            catch (InValidData ex)
            {
                Logger.Instance.Log(ex.StackTrace, ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest
                                               , ServiceResponse.Instance.BuildResponse(ResponseCodes.USER_NOT_AVAILABLE,ex.Message));
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
