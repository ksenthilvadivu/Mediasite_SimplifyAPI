using System.Net;
using System.ComponentModel;
using DH.Curbside.Core.Enterprise.Enums;

namespace DH.Curbside.Core.Enterprise.Common
{
    /// <summary>
    /// Services response codes, messages for below supported HTTPStatusCodes 
    /// 200 - OK            (Success)
    /// 201 - Created       (Success)
    /// 400 - Bad Request   (Client Error)
    /// 401 - Unauthorized  (Client Error)
    /// 403 - Forbidden     (Client Error)
    /// 404 - NotFound      (Client Error)
    /// 500 - Internal Server Error (Server Error)
    /// </summary>
    public enum ResponseCodes
    {
        /// <summary>
        /// Success
        /// </summary>
        [HttpStatus(HttpStatusCode.OK)]
        [Description("Success")]
        OK = 2000,

        /// <summary>
        /// The request was processed successfully
        /// </summary>
        [HttpStatus(HttpStatusCode.Created)]
        [Description("The request was processed successfully")]
        CREATED = 2001,

        /// <summary>
        /// Invalid Or missing parameters
        /// </summary>
        [HttpStatus(HttpStatusCode.BadRequest)]
        [Description("Invalid Or missing parameters :")]
        INVALID_MISSING_INPUTS = 4000,

        /// <summary>
        /// Unauthorized access
        /// </summary>
        [HttpStatus(HttpStatusCode.Unauthorized)]
        [Description("Unauthorized access")]
        UNAUTHORIZED = 4001,

        /// <summary>
        /// Invalid json data
        /// </summary>
        [HttpStatus(HttpStatusCode.BadRequest)]
        [Description("Invalid json data")]
        INVALID_JSON_DATA = 4002,

        /// <summary>
        /// Invalid tenant id
        /// </summary>
        [HttpStatus(HttpStatusCode.BadRequest)]
        [Description("Invalid tenant id")]
        INVALID_TENANT_ID = 4003,

        /// <summary>
        /// Invalid provider id
        /// </summary>
        [HttpStatus(HttpStatusCode.BadRequest)]
        [Description("Invalid provider id")]
        INVALID_PROVIDER_ID = 4004,

        /// <summary>
        /// Invalid case id
        /// </summary>
        [HttpStatus(HttpStatusCode.BadRequest)]
        [Description("Invalid case id")]
        INVALID_CASE_ID = 4005,

        /// <summary>
        /// Invalid email id
        /// </summary>
        [HttpStatus(HttpStatusCode.BadRequest)]
        [Description("Invalid email id")]
        INVALID_EMAIL_ID = 4006,

        /// <summary>
        /// Invalid application id for user
        /// </summary>
        [HttpStatus(HttpStatusCode.BadRequest)]
        [Description("Invalid application id for user")]
        INVALID_APPLICATION_ID = 4007,

        /// <summary>
        /// Resource not found with the given details
        /// </summary>
        [HttpStatus(HttpStatusCode.NotFound)]
        [Description("Resource not found with the given details")]
        RESOURCE_NOTFOUND = 4101,

        /// <summary>
        /// WhitelistUser not found with the given details
        /// </summary>
        [HttpStatus(HttpStatusCode.NotFound)]
        [Description("WhitelistUser not found with the given details")]
        WHITELISTUSER_NOT_AVAILABLE = 4102,

        /// <summary>
        /// PatientCase not found with the given details
        /// </summary>
        [HttpStatus(HttpStatusCode.NotFound)]
        [Description("PatientCase not found with the given details")]
        PATIENTCASE_NOT_AVAILABLE = 4103,

        /// <summary>
        /// Unable to update the whitelist
        /// </summary>
        [HttpStatus(HttpStatusCode.InternalServerError)]
        [Description("Unable to update whitelist")]
        WHITELIST_UPDATION_FAILED = 4104,

        /// <summary>
        /// Patient Cases failed to add
        /// </summary>
        [HttpStatus(HttpStatusCode.OK)]
        [Description("Patient Cases failed to add")]
        PATIENTCASES_FAILED = 4105,

        /// <summary>
        /// WhitelistUser not found with the given details
        /// </summary>
        [HttpStatus(HttpStatusCode.NotFound)]
        [Description("Patient cases not found with the given details")]
        PATIENTCASES_NOT_AVAILABLE = 4106,

        /// <summary>
        /// PatientCase not found with the given details
        /// </summary>
        [HttpStatus(HttpStatusCode.NotFound)]
        [Description("Invalid Application Id or User Id or Tenant Id for User:")]
        USER_NOT_AVAILABLE = 4107,

        /// <summary>
        /// Whitelist Users successfully added
        /// </summary>
        [HttpStatus(HttpStatusCode.OK)]
        [Description("Whitelist Users successfully added")]
        WHITELISTEUSER_ADDED = 2002,

        /// <summary>
        /// Whitelist Domain successfully added
        /// </summary>
        [HttpStatus(HttpStatusCode.InternalServerError)]
        [Description("Whitelist Domain successfully added")]
        WHITELISTDOMAIN_ADDED = 2003,

        /// <summary>
        /// Patient Cases successfully added
        /// </summary>
        [HttpStatus(HttpStatusCode.OK)]
        [Description("Patient Cases successfully added")]
        PATIENTCASES_ADDED = 2004,

        /// <summary>
        /// Whitelist Users successfully updated
        /// </summary>
        [HttpStatus(HttpStatusCode.Created)]
        [Description("Whitelist Users successfully updated")]
        WHITELISTUSERS_UPDATED = 2101,

        /// <summary>
        /// Whitelist Domains successfully updated
        /// </summary>
        [HttpStatus(HttpStatusCode.Created)]
        [Description("Whitelist Domains successfully updated")]
        WHITELISTDOMAINS_UPDATED = 2102,

        /// <summary>
        /// Whitelist details successfully updated
        /// </summary>
        [HttpStatus(HttpStatusCode.Created)]
        [Description("Whitelist details successfully updated")]
        WHITELIST_UPDATED = 2103,

        /// <summary>
        /// Whitelist user successfully updated
        /// </summary>
        [HttpStatus(HttpStatusCode.Created)]
        [Description("Whitelist user successfully updated")]
        WHITELISTEUSER_DEPROVISIONED = 2104,

        /// <summary>
        /// Invitation sent successfully
        /// </summary>
        [HttpStatus(HttpStatusCode.OK)]
        [Description("Invitation sent successfully")]
        INVITATION_SENT = 2105,

        /// <summary>
        /// An unexpected error occurred. Please contact administrator
        /// </summary>
        [HttpStatus(HttpStatusCode.InternalServerError)]
        [Description("An unexpected error occurred. Please contact administrator")]
        INTERNAL_SEREVR_ERROR = 5000
    }
}
