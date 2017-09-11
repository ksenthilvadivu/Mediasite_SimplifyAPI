using System.ComponentModel.DataAnnotations;

namespace DH.Curbside.Core.Application.WhitelistUser.ViewModels
{
    public class RequestUserViewModel
    {
        [Required]
        public string Prefix { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public int AppId { get; set; }

        public string Invite { get; set; }
    }
}
