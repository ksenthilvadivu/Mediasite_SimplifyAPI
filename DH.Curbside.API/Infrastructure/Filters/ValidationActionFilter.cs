using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using DH.Curbside.Core.Enterprise.Common;

namespace DH.Curbside.API.Infrastructure.Filters
{
    /// <summary>
    /// Handles all request validation
    /// </summary>
    public class ValidationActionFilter : ActionFilterAttribute
    {
        /// <summary>
        /// Process all request validation in current context
        /// </summary>
        /// <param name="actionContext">HttpActionContext object</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var modelState = actionContext.ModelState;
            if (!modelState.IsValid)
            {
                var errorParameters = new StringBuilder();
                foreach (var item in modelState.Keys)
                {                  
                    var itemVal = modelState[item];
                    if (itemVal.Errors.Count > 0)
                    {
                        var itemIndex = item.IndexOf('.');
                        if (itemIndex > -1 || !string.IsNullOrEmpty(itemVal.Errors[0].ErrorMessage))
                        {
                            errorParameters.Append(item.Substring(item.IndexOf('.') + 1));
                            errorParameters.Append(',');
                        }
                    }
                }

                string errors = errorParameters.ToString().Substring(0, errorParameters.Length - 1);

                var serviceResponse = ServiceResponse.Instance.BuildResponse(ResponseCodes.INVALID_MISSING_INPUTS);
                serviceResponse.Message = serviceResponse.Message + " " + errors;

                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, serviceResponse);
            }
        }
    } 
}