using DH.Curbside.Core.Domain;
using DH.Curbside.Core.Persistence.RepositoryInterfaces;

namespace DH.Curbside.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Provides all whitelist user related operations
    /// </summary>
    public class WhitelistUserRepository : CurbsideRepository<User>, IWhitelistUserRepository
    {
        /// <summary>
        /// Whitelist user repository constructor
        /// </summary>
        public WhitelistUserRepository()
        {

        }
    }
}
