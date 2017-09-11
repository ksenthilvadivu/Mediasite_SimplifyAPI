using System;
using System.Net;

namespace DH.Curbside.Core.Enterprise.Enums
{
    /// <summary>
    /// Custom attribute to declare HttpStatus.
    /// </summary>
    public sealed class HttpStatusAttribute : Attribute, IEnumAttribute<HttpStatusCode>
    {
        private readonly HttpStatusCode _value;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="value">HttpStatusCode value</param>
        public HttpStatusAttribute(HttpStatusCode value)
        {
            _value = value;
        }

        /// <summary>
        /// Gets HttpStatusCode value.
        /// </summary>
        public HttpStatusCode Value
        {
            get { return _value; }
        }
    }
}
