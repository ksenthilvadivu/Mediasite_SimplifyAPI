using System.Threading.Tasks;
using System.Collections.Generic;
using DH.Curbside.Core.Application.WhitelistUser.ViewModels;

namespace DH.Curbside.Core.Application.WhitelistUser
{
    public interface IWhitelistUserApplication
    {
        /// <summary>
        /// Get list of whitelisted users
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <returns>List of Whitelist Users</returns>
        UserViewModel GetWhitelistUsers(string tenantId);

        /// <summary>
        /// Get user with whitelistinginfo
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="email">Email</param>
        /// <returns>Whitelist User</returns>
        UserInfo GetWhitelistUser(string tenantId, string email);

        /// <summary>
        /// Deprovision an user from whitelist
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="email">Email</param>
        /// <param name="appId">Application Id</param>
        /// <returns>True or False</returns>
        Task<bool> DeprovisionUser(string tenantId, string email, int appId);

        /// <summary>
        /// Add new users to the whitelist
        /// </summary>
        /// <param name="whitelistUserViewModel">WhitelistUser View Model</param>
        /// <param name="tenantId">Tenant Id</param>
        /// <returns>True or False</returns>
        Task<bool> CreateWhitelistUser(List<RequestUserViewModel> whitelistUserViewModel, string tenantId);


        /// <summary>
        /// InviteWhitelistUser
        /// </summary>
        /// <param name="requestInviteViewModel">WhitelistUser View Model</param>
        /// <param name="tenantId">Tenant Id</param>
        /// <returns>True or False</returns>
        void InviteWhitelistUser(List<RequestInviteViewModel> requestInviteViewModel, string tenantId);
    }
}
