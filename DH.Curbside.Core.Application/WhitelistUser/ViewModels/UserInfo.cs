namespace DH.Curbside.Core.Application.WhitelistUser.ViewModels
{
    public class UserInfo
    {
        public int UserId { get; set; }

        public string Prefix { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public int ClientApplicationId { get; set; }
    }
}
