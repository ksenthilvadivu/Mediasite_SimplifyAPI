using DH.Curbside.Core.Enterprise.Enums;

namespace DH.Curbside.Core.Enterprise.Common
{
    /// <summary>
    /// Manages service response
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceResponse<T> : SingletonBase<ServiceResponse<T>>
    {
        private ServiceResponse()
        {
        }

        /// <summary>
        /// Builds the service response
        /// </summary>
        /// <param name="code">ResponseCodes</param>
        /// <param name="data">Object of type T</param>
        /// <returns>CurbsideResponse object</returns>
        public CurbsideResponse<T> BuildResponse(ResponseCodes code, T data)
        {
            var peResonse = new CurbsideResponse<T>
            {
                Code = (int)code,
                Message = EnumManager.Instance.GetDescription<ResponseCodes>(code),
                Data = data
            };

            return peResonse;
        }
    }

    /// <summary>
    /// Manages service response
    /// </summary>
    public class ServiceResponse : SingletonBase<ServiceResponse>
    {
        private ServiceResponse()
        { }

        /// <summary>
        /// Builds the service response
        /// </summary>
        /// <param name="code">ResponseCodes</param>
        /// <returns>CurbsideResponse object</returns>
        public CurbsideResponse BuildResponse(ResponseCodes code)
        {
            var peResponse = new CurbsideResponse
            {
                Code = (int)code,
                Message = EnumManager.Instance.GetDescription<ResponseCodes>(code)
            };
            return peResponse;
        }

        /// <summary>
        /// Builds the service response with custom message
        /// </summary>
        /// <param name="code">ResponseCodes</param>
        /// <param name="customMessage">CustomMessage</param>
        /// <returns>CurbsideResponse object</returns>
        public CurbsideResponse BuildResponse(ResponseCodes code, string customMessage)
        {
            var peResponse = new CurbsideResponse
            {
                Code = (int)code,
                Message = EnumManager.Instance.GetDescription<ResponseCodes>(code) + customMessage
            };
            return peResponse;
        }
    }
}