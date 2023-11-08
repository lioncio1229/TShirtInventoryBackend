using Microsoft.EntityFrameworkCore;
using TshirtInventoryBackend.Models;

namespace TshirtInventoryBackend.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DbContext context) : base(context)
        {
        }
    }
}
