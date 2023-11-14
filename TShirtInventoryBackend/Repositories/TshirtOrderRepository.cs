﻿using Microsoft.EntityFrameworkCore;
using TshirtInventoryBackend.Models;
using TshirtInventoryBackend.Repositories.Interface;

namespace TshirtInventoryBackend.Repositories
{
    public class TshirtOrderRepository : Repository<TshirtOrder>, ITshirtOrderRepository
    {
        public TshirtOrderRepository(DbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<TshirtOrder>> GetAllAsync()
        {
            return await context.Set<TshirtOrder>()
                .Include(to => to.Order)
                .Include(to => to.Tshirt)
                .ToListAsync();
        }
    }
}
