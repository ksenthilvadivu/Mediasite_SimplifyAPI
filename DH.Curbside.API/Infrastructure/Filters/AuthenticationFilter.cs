using System;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using Enterprise;
using Enterprise.Security;
using DentalWarranty.Infrastructure.Utilities;
using DentalWarranty.WebApi.Infrastructure.Utilities;

namespace DentalWarranty.WebApi.Infrastructure.Filters
{
    /// <summary>
    /// Handles token validation.
    /// </summary>
    public class AuthenticationFilter : Attribute, IAuthenticationFilter
    {
        private const string PatientId = "patientId";
        private const string CarePartnerId = "carePartnerId";

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            if (!(context.ActionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
               || context.ActionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
                && ConfigurationHelper.EnableTokenValidation)
            {
                string accessToken;

                if (!TryRetrieveAccessToken(context.Request, out accessToken))
                    await BuildErrorResponse(context);

                try
                {
                    SecurityToken authToken;
                    JwtTokenManager.ValidateJwtToken(accessToken, out authToken);

                    if (authToken.ValidTo < DateTime.Now)
                    {
                        await BuildErrorResponse(context);
                    }

                    if (authToken.Claims.FirstOrDefault(c => c.Type == JwtTokenManager.IsAccessToken && c.Value == "True") == null)
                        await BuildErrorResponse(context);

                    if (ConfigurationHelper.EnableExtraSecureTokenValidation)
                    {
                        await EnsureRequestFromAuthorisedUser(context, authToken);
                    }

                    await Task.FromResult(0);
                }
                catch (SecurityTokenValidationException)
                {
                    BuildErrorResponse(context);
                }
                catch (Exception)
                {
                    context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
                }
            }
            await Task.FromResult(0);
        }

        /// <summary>
        /// Ensures the request is from authorized user who has logged in.
        /// </summary>
        /// <param name="context">HttpAuthenticationContext object</param>
        /// <param name="authToken">JwtSecurityToken object</param>
        /// <returns></returns>
        private async Task EnsureRequestFromAuthorisedUser(HttpAuthenticationContext context, JwtSecurityToken authToken)
        {
            var providerId = authToken.Claims.Where(c => c.Type == JwtTokenManager.ProviderId).Select(c => c.Value).FirstOrDefault();

            if (providerId == null)  //Nurse not logged in,Either Patient or CarePartner logged in.
            {
                

                var routeData = context.ActionContext.ControllerContext.RouteData.Values;

                var patientId = authToken.Claims.Where(c => c.Type == JwtTokenManager.PatientId).Select(c => c.Value).FirstOrDefault();

                if (routeData.ContainsKey(PatientId))
                {
                    var queryPatientId = routeData[PatientId];
                    if (patientId != (string)queryPatientId)
                        await BuildErrorResponse(context);
                }

                var carePartnerId = authToken.Claims.Where(c => c.Type == JwtTokenManager.CarePartnerId).Select(c => c.Value).FirstOrDefault();
                if (carePartnerId != null)
                {
                    if (routeData.ContainsKey(CarePartnerId))
                    {
                        var queryCarePartnerId = routeData[CarePartnerId];
                        if (carePartnerId != (string)queryCarePartnerId)
                            await BuildErrorResponse(context);
                    }
                }
            }
            else //Nurse logged in
            {
                var deviceId = authToken.Claims.Where(c => c.Type == JwtTokenManager.DeviceId).Select(c => c.Value).FirstOrDefault();
                //If the request is not made from the same IP address,from where the token is generated,then return 401.
                if (deviceId != HttpContextFactory.Current.Request.UserHostAddress)
                    await BuildErrorResponse(context);
            }
        }

        /// <summary>
        /// Builds error response.
        /// </summary>
        /// <param name="context">HttpAuthenticationContext context</param>
        /// <returns></returns>
        private async Task BuildErrorResponse(HttpAuthenticationContext context)
        {
            var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
            await Task.FromResult(response);
        }

        /// <summary>
        /// Retrieves the token from header
        /// </summary>
        /// <param name="request">HttpRequestMessage object</param>
        /// <param name="token">Token string</param>
        /// <returns></returns>
        private static bool TryRetrieveAccessToken(HttpRequestMessage request, out string token)
        {
            token = null;

            if (!request.Headers.Contains("AccessToken"))
                return false;

            var authHeader = request.Headers.GetValues("AccessToken").First();
            token = authHeader;
            return token != null;
        }



        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            context.Result = new ResultWithChallenge(context.Result);
            await Task.FromResult(0);
        }

        /// <summary>
        /// 
        /// </summary>
        public class ResultWithChallenge : IHttpActionResult
        {
            private readonly IHttpActionResult _next;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="next"></param>
            public ResultWithChallenge(IHttpActionResult next)
            {
                _next = next;
            }

            public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var response = await _next.ExecuteAsync(cancellationToken);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue(GlobalConstants.AuthSchema, GlobalConstants.AuthSchemaParam));
                }
                return response;
            }

        }

        public bool AllowMultiple
        {
            get { return false; }
        }
    }
}