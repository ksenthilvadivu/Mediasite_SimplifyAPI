using Microsoft.Web.Http;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using DH.Curbside.Core.Application.Mediasite;
using DH.Curbside.Core.Application.Mediasite.ViewModels;
using DH.Curbside.Core.Enterprise.Common;
using DH.Curbside.Core.Enterprise.Logging;
using ResponseCodes = DH.Curbside.Core.Enterprise.Common.ResponseCodes;
using ServiceResponse = DH.Curbside.Core.Enterprise.Common.ServiceResponse;

namespace DH.Curbside.API.Controllers
{
    /// <summary>
    /// Manage all media site operations
    /// </summary>
    [ApiVersion("1.0")]
    public class MediasiteController : CurbsideBaseController
    {
        private readonly IMediasiteApplication _mediasiteApplication;

        /// <summary>
        /// Constructor for MediasiteController
        /// </summary>
        /// <param name="mediasiteApplication">IWhitelistApplication object</param>
        public MediasiteController(IMediasiteApplication mediasiteApplication)
        {
            _mediasiteApplication = mediasiteApplication;
        }

        /// <summary>
        /// Get list of Mediasite Videos
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>        
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(CurbsideResponse))]
        [Route("api/v{version:apiVersion}/{tenantId}/Content")]
        public HttpResponseMessage GetMediasiteVideos(string tenantId) //http://localhost:50020/api/v1.0/80C641E8-E12F-406A-BB31-9557CE7D9F66/MediasiteVideos?Search=stroke&recordCount=10&pageNumber=1
        {
            NameValueCollection qsParam = HttpUtility.ParseQueryString(Request.RequestUri.Query);
            string searchCriteria = string.Empty;
            var top = 100;
            var skip = 0;
            if (qsParam.Count > 0)
            { 
                if (!string.IsNullOrEmpty(qsParam["Search"]))
                {
                    searchCriteria = qsParam["Search"].ToString();
                }
                if (!string.IsNullOrEmpty(qsParam["top"]))
                {
                    top = Convert.ToInt32(qsParam["top"].ToString());
                }
                if (!string.IsNullOrEmpty(qsParam["skip"]))
                {
                    skip = Convert.ToInt32(qsParam["skip"].ToString());
                }
            }
            try
            {
                var mediasiteVideos = _mediasiteApplication.GetMediasiteVideos(tenantId, searchCriteria,skip,top);
                
                return Request.CreateResponse(HttpStatusCode.OK
                                            , ServiceResponse<MediasiteModel>.Instance.BuildResponse(ResponseCodes.OK, mediasiteVideos));
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex.StackTrace, ex);
                return Request.CreateResponse(HttpStatusCode.NotFound
                                            , ServiceResponse.Instance.BuildResponse(ResponseCodes.RESOURCE_NOTFOUND));
            }
        }

        /// <summary>
        /// Get list of Mediasite Videos
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>        
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(CurbsideResponse))]
        [Route("api/v{version:apiVersion}/{tenantId}/AuthorizationTicket")]
        public HttpResponseMessage GetMediasiteAuthTicket(string tenantId) //http://localhost:50020/api/v1.0/80C641E8-E12F-406A-BB31-9557CE7D9F66/MediasiteVideos?Search=stroke&recordCount=10&pageNumber=1
        {
            NameValueCollection qsParam = HttpUtility.ParseQueryString(Request.RequestUri.Query);

            string presentationId = string.Empty;
            int minutesToLive = 0;


            if (qsParam.Count > 0)
            {
                if (!string.IsNullOrEmpty(qsParam["presentationId"]))
                {
                    presentationId = qsParam["presentationId"].ToString();
                }
                if (!string.IsNullOrEmpty(qsParam["minutesToLive"]))
                {
                    minutesToLive = Convert.ToInt32(qsParam["minutesToLive"].ToString());
                }
            }
            try
            {
                var msAuthorizationTicket = _mediasiteApplication.GetAuthorizationTicket(presentationId, minutesToLive);

                return Request.CreateResponse(HttpStatusCode.OK
                                            , ServiceResponse<string>.Instance.BuildResponse(ResponseCodes.OK, msAuthorizationTicket));
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex.StackTrace, ex);
                return Request.CreateResponse(HttpStatusCode.NotFound
                                            , ServiceResponse.Instance.BuildResponse(ResponseCodes.RESOURCE_NOTFOUND));
            }
        }

        /// <summary>
        /// Simplify API : Get MediaSite object for Presentation Id
        /// </summary>
        /// <param name="presentationId">Presentation Id</param>        
        /// <returns>PresentationModel Object</returns>
        [HttpGet]
        [ResponseType(typeof(CurbsideResponse))]
        [Route("api/v{version:apiVersion}/{presentationId}/Presentation")]
        public HttpResponseMessage GetMediasitePresentation(string presentationId)
        {
            try
            {
                var mediasiteVideos = _mediasiteApplication.GetPresentation(presentationId);

                return Request.CreateResponse(HttpStatusCode.OK
                                            , ServiceResponse<PresentationModel>.Instance.BuildResponse(ResponseCodes.OK, mediasiteVideos));
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex.StackTrace, ex);
                return Request.CreateResponse(HttpStatusCode.NotFound
                                            , ServiceResponse.Instance.BuildResponse(ResponseCodes.RESOURCE_NOTFOUND));
            }
        }
    }
}
