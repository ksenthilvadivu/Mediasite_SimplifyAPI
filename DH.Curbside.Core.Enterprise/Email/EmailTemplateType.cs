using System.ComponentModel;

namespace DH.Curbside.Core.Enterprise.Email
{
    /// <summary>
    /// Email template type enum
    /// </summary>
    public enum EmailTemplateType
    {
        /// <summary>
        /// Patient Registration
        /// </summary>
        [Description("Submitter Registration")]
        SubmitterRegistration = 1,

        /// <summary>
        /// CarePartner Registration
        /// </summary>
        [Description("Reviewer Registration")]
        ReviewerRegistration = 2,

        /// <summary>
        /// Forgot password
        /// </summary>
        [Description("Forgot password Validation Code")]
        ForgotPassWord = 3
    }
}
