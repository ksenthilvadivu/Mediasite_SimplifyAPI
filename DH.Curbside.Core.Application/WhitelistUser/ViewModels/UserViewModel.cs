using System.Collections.Generic;

namespace DH.Curbside.Core.Application.WhitelistUser.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            Users = new List<UserInfo>();
        }

        public IList<UserInfo> Users { get; set; }
    }
}
