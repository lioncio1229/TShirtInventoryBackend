using Microsoft.EntityFrameworkCore;
using TshirtInventoryBackend.Models;

namespace TshirtInventoryBackend.Repositories
{
    public class TokenRepository : Repository<BlacklistedToken>, ITokenRepository
    {
        public TokenRepository(DbContext context) : base(context)
        {
        }
    }
}
