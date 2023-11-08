using Microsoft.EntityFrameworkCore;
using TshirtInventoryBackend.Models;

namespace TshirtInventoryBackend.Repositories
{
    public class TshirtRepository : Repository<Tshirt>, ITshirtRepository
    {
        public TshirtRepository(DbContext context) : base(context)
        {
        }
    }
}
