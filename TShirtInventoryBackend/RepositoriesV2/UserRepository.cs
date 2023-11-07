﻿using Microsoft.EntityFrameworkCore;
using TshirtInventoryBackend.Data;
using TshirtInventoryBackend.Models;

namespace TshirtInventoryBackend.RepositoriesV2
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context) { }

        public DataContext DataContext {
            get
            {
                return context as DataContext;
            }
        }

        public override async Task<IEnumerable<User>> GetAll()
        {
            return await DataContext.Users
                .Include(o => o.Role)
                .ToListAsync();
        }

        public async Task<User> GetUserWithEmail(string email)
        {
            return await DataContext.Users
                .Include(o => o.Role)
                .FirstOrDefaultAsync(o => o.Email == email);
        }

        public async Task<User?> RemoveWithEmail(string email)
        {
            var user = await GetUserWithEmail(email);
            if (user == null)
            {
                return null;
            }
            Remove(user);
            context.SaveChanges();

            return user;
        }
    }
}