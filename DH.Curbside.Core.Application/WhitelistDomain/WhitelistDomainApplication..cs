using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DH.Curbside.Core.Application.WhitelistDomain.ViewModels;
using DH.Curbside.Core.Persistence.RepositoryInterfaces;
using DH.Curbside.Core.Application.Exceptions;

namespace DH.Curbside.Core.Application.WhitelistDomain
{
    public class WhitelistDomainApplication : IWhitelistDomainApplication
    {
        private readonly IDomainRepository _domainRepository;
        private readonly IMapper _mapper;

        public WhitelistDomainApplication(IDomainRepository domainRepository, IMapper mapper)
        {
            _mapper = mapper;
            _domainRepository = domainRepository;
        }

        /// <summary>
        /// Adds domain
        /// </summary>
        /// <param name="requestDomainViewModel">Request Domain View Model</param>
        /// <param name="tenantId">Tenant Id</param>
        /// <returns>True or False</returns>
        public async Task<bool> AddDomain(List<RequestDomainViewModel> requestDomainViewModel, string tenantId)
        {
            Guid tenantGuid;
            Guid.TryParse(tenantId, out tenantGuid);

            if (tenantGuid == Guid.Empty)
                throw new InvalidTenantIdException();

            var whitelistDomainModels = _mapper.Map<List<RequestDomainViewModel>, List<Domain.Domain>>(requestDomainViewModel);

            if (whitelistDomainModels==null|| !whitelistDomainModels.Any())
                throw new InvalidJsonException();

            var validWhitelistDomain = new List<Domain.Domain>();

            foreach (var domain in whitelistDomainModels)
            {
                domain.TenantId = tenantGuid;
                //TODO::Need to replace with AD User
                domain.CreatedBy = "System User";
                domain.CreatedOn = DateTime.Now;

                validWhitelistDomain.Add(domain);
            }

            return await _domainRepository.Add(whitelistDomainModels);

        }
    }
}
