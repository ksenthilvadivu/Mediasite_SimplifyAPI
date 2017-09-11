using System.Collections.Generic;
using System.Threading.Tasks;
using DH.Curbside.Core.Application.WhitelistDomain.ViewModels;

namespace DH.Curbside.Core.Application.WhitelistDomain
{
    public interface IWhitelistDomainApplication
    {
        /// <summary>
        /// Adds domain
        /// </summary>
        /// <param name="requestDomainViewModel">Request Domain View Model</param>
        /// <param name="tenantId">Tenant Id</param>
        /// <returns>True or False</returns>
        Task<bool> AddDomain(List<RequestDomainViewModel> requestDomainViewModel, string tenantId);
    }
}
