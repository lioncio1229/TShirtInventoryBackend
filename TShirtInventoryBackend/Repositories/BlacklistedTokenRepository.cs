using Microsoft.EntityFrameworkCore;
using TshirtInventoryBackend.Models;
using TshirtInventoryBackend.Repositories.Interface;

namespace TshirtInventoryBackend.Repositories
{
    public class BlacklistedTokenRepository : Repository<BlacklistedToken>, IBlacklistedTokenRepository
    {
        public BlacklistedTokenRepository(DbContext context) : base(context)
        {
        }
    }
}
