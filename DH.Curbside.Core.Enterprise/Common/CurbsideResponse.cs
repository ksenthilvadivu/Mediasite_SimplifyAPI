using System.Runtime.Serialization;

namespace DH.Curbside.Core.Enterprise.Common
{
    /// <summary>
    /// CurbsideResponse object
    /// </summary>
    /// <typeparam name="T">Type T</typeparam>
    [DataContract(Name = "CurbsideResponse{0}")]
    public class CurbsideResponse<T>
    {
        /// <summary>
        /// service custom code
        /// </summary>
        [DataMember]
        public int Code { get; set; }

        /// <summary>
        /// service custom message
        /// </summary>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// response object
        /// </summary>
        [DataMember]
        public T Data { get; set; }
    }

    /// <summary>
    /// CurbsideResponse object
    /// </summary>
    public class CurbsideResponse
    {
        /// <summary>
        /// service custom code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// service custom message
        /// </summary>
        public string Message { get; set; }
    }
}