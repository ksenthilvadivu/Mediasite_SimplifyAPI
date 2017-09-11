using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DH.Curbside.Core.Enterprise.Logging;

namespace DH.Curbside.API.Infrastructure.Handlers
{
    /// <summary>
    /// Handles  all exceptions and trace logging.
    /// </summary>
    public class LoggingHandler : DelegatingHandler
    {
        /// <summary>
        /// Logs the request persist information
        /// </summary>
        /// <param name="request">request object</param>
        /// <param name="cancellationToken">cancel token object</param>
        /// <returns>HttpResponseMessag object</returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Log the request information
            LogRequestLoggingInfo(request);

            // Execute the request
            return base.SendAsync(request, cancellationToken).ContinueWith(task =>
            {
                var response = task.Result;
                // Extract the response logging info then persist the information
                LogResponseLoggingInfo(response);
                return response;
            });
        }

        /// <summary>
        /// Logs the request  information.
        /// </summary>
        /// <param name="request">HttpRequestMessage object</param>
        private void LogRequestLoggingInfo(HttpRequestMessage request)
        {
            if (Logger.Instance.IsDebugEnabled)
            {
                if (request.Content != null)
                {
                    request.Content.ReadAsByteArrayAsync()
                        .ContinueWith(task =>
                        {
                            var token = string.Empty; 
                            var result = Encoding.UTF8.GetString(task.Result);
                            Logger.Instance.Log(
                                $"Message :Entered with Request: {request.RequestUri.AbsolutePath} || Method : {request.Method} ||  Request Body: {result} || Token : {token}"
                                , LogLevel.Debug);
                        }).Wait();
                }
            }
        }

        /// <summary>
        /// Logs the response  information
        /// </summary>
        /// <param name="response">HttpRequestMessage object</param>
        private void LogResponseLoggingInfo(HttpResponseMessage response)
        {
            //if (Logger.Instance.IsDebugEnabled)
            //{
            //    var token = string.Empty;
            //    if (response.Content != null)
            //    {
            //        response.Content.ReadAsByteArrayAsync()
            //            .ContinueWith(task =>
            //            {
            //                var responseMsg = Encoding.UTF8.GetString(task.Result);
            //                Logger.Instance.Log(
            //                    $"Message :Exited from Request: {response.RequestMessage.RequestUri.AbsolutePath} || Method : {response.RequestMessage.Method} ||  Response Body: {responseMsg} || Token : {token}"
            //                    , LogLevel.Debug);
            //            });
            //    }
            //    else
            //    {
            //        Logger.Instance.Log(
            //            $"Message :Exited from Request: {response.RequestMessage.RequestUri.AbsolutePath} || Method : {response.RequestMessage.Method} ||  Response Body: {response.StatusCode} || Token : {token}"
            //            , LogLevel.Debug);
            //    }
            //}
        }
    }
}