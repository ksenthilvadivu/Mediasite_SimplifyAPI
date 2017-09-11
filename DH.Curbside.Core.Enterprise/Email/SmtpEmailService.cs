using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Threading.Tasks;
using DH.Curbside.Core.Enterprise.Enums;

namespace DH.Curbside.Core.Enterprise.Email
{
    public class SmtpEmailService : IEmailService
    {
        #region constants
        /// <summary>
        /// jpg extension
        /// </summary>
        private const string PngExtension = ".jpg";

        /// <summary>
        /// Village health logo image file name
        /// </summary>
        private const string DignityHealthLogo = "DG";


        #endregion

        /// <summary>
        /// Sends EmailTemplate
        /// </summary>
        /// <param name="emailTemplateType">EmailTemplateType</param>
        /// <param name="toEmail">To Email</param>
        public async Task SendEmailTemplate(EmailTemplateType emailTemplateType, string toEmail)
        {
            string emailLogoUrl = string.Empty;

            string subject = EnumManager.Instance.GetDescription(emailTemplateType);

            var imageUrl = Path.Combine(DignityHealthLogo + PngExtension);
            if (!string.IsNullOrEmpty(imageUrl))
            {
                emailLogoUrl = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"Email\Images", imageUrl);

            }
            await SendEmail(subject, toEmail, emailLogoUrl);
        }


        /// <summary>
        /// Sends mail
        /// </summary>
        /// <param name="subject">Mail subject</param>
        /// <param name="mailTo">To address</param>
        /// <param name="imageUrl">Image url</param>
        /// <param name="isBodyHtml">Boolean</param>
        public async Task SendEmail(string subject, string mailTo, string imageUrl = "", bool isBodyHtml = true)
        {
            MailMessage mailMessage = new MailMessage { Subject = subject };

            if (!string.IsNullOrEmpty(imageUrl) && File.Exists(imageUrl))
            {
                string fileExtension = Path.GetExtension(imageUrl);

                Attachment inline = new Attachment(imageUrl);
                inline.ContentDisposition.Inline = true;
                inline.ContentDisposition.DispositionType = DispositionTypeNames.Inline;
                inline.ContentType = new ContentType($"image/{fileExtension}") { Name = Path.GetFileName(imageUrl) };
                mailMessage.Attachments.Add(inline);
                // mailMessage.Body = body;
            }
            else
            {
                mailMessage.Body = "Dignity Heath HTML body";
            }

            mailMessage.IsBodyHtml = isBodyHtml;
            foreach (var item in mailTo.Split(',').Where(item => item != null))
            {
                mailMessage.To.Add(item);
            }
            SmtpClient smtpClient = new SmtpClient();
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
