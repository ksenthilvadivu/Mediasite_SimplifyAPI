using DH.Curbside.Core.Domain;
using DH.Curbside.Core.Persistence.RepositoryInterfaces;

namespace DH.Curbside.Infrastructure.Persistence.Repositories
{
    public class DomainRepository : CurbsideRepository<Domain>, IDomainRepository
    {

        public DomainRepository()
        {

        }

    }
}
