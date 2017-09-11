using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;
using DH.Curbside.Core.Enterprise.Common;
using DH.Curbside.Core.Enterprise.Logging;

namespace DH.Curbside.API.Infrastructure.Filters
{
    /// <summary>
    ///  Handles all exceptions
    /// </summary>
    public class HandleApiExceptionFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// Process all Exceptions in current context
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(HttpActionExecutedContext context)
        {
            Logger.Instance.Log(string.Format("Error occurred while processing Request: {0} || Method : {1}", context.Request.RequestUri, context.Request.Method)
                                ,context.Exception);

            context.Response = context.ActionContext.Request.CreateResponse(HttpStatusCode.InternalServerError
                                                                           , ServiceResponse.Instance.BuildResponse(ResponseCodes.INTERNAL_SEREVR_ERROR)
                                                                           );
        }
    }
}