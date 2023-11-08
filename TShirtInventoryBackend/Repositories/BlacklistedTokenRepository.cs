using Microsoft.EntityFrameworkCore;
using TshirtInventoryBackend.Models;

namespace TshirtInventoryBackend.Repositories
{
    public class BlacklistedTokenRepository : Repository<BlacklistedToken>, IBlacklistedTokenRepository
    {
        public BlacklistedTokenRepository(DbContext context) : base(context)
        {
        }
    }
}
