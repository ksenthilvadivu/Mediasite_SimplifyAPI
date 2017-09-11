using System.Threading.Tasks;

namespace DH.Curbside.Core.Enterprise.Email
{
    public interface IEmailService
    {
        Task SendEmailTemplate(EmailTemplateType emailTemplateType, string toEmail);
    }
}
