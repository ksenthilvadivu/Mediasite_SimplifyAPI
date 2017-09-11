using System.ComponentModel.DataAnnotations;

namespace DH.Curbside.Core.Application.WhitelistDomain.ViewModels
{
    public class RequestDomainViewModel
    {
        [Required]
        public string DomainName { get; set; }

        public int? AppId { get; set; }
    }
}
