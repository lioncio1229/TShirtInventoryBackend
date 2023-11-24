using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TshirtInventoryBackend.DTOs;
using TshirtInventoryBackend.Models;
using TshirtInventoryBackend.Models.Reponse;
using TshirtInventoryBackend.Repositories.Interface;

namespace TshirtInventoryBackend.Repositories
{
    public class TshirtOrderRepository : Repository<TshirtOrder>, ITshirtOrderRepository
    {
        private readonly IMapper _mapper;
        public TshirtOrderRepository(DbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public override async Task<IEnumerable<TshirtOrder>> GetAllAsync()
        {
            return await context.Set<TshirtOrder>()
                .Include(to => to.Status)
                .Include(to => to.Tshirt)
                    .ThenInclude(tshirt => tshirt.Category)
                .Include(to => to.Order)
                    .ThenInclude(order => order.Customer)
                .ToListAsync();
        }

        public async Task<IEnumerable<TshirtOrder>> GetAllWithQuery(string query)
        {
            return await context.Set<TshirtOrder>()
                .Where(to => to.ProductId.Contains(query))
                .Include(to => to.Status)
                .Include(to => to.Tshirt)
                    .ThenInclude(tshirt => tshirt.Category)
                .Include(to => to.Order)
                    .ThenInclude(order => order.Customer)
                .ToListAsync();
        }

        public async Task<IEnumerable<TshirtOrder>> GetAllWithQuery(string query, int statusId)
        {
            return await context.Set<TshirtOrder>()
                .Where(to => to.Status.Id == statusId)
                .Where(to => to.ProductId.Contains(query))
                .Include(to => to.Status)
                .Include(to => to.Tshirt)
                    .ThenInclude(tshirt => tshirt.Category)
                .Include(to => to.Order)
                    .ThenInclude(order => order.Customer)
                .ToListAsync();
        }
    }
}
