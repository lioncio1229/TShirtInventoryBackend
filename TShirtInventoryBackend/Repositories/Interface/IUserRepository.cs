﻿using TshirtInventoryBackend.Models;

namespace TshirtInventoryBackend.Repositories.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserWithEmail(string email);
        Task<User?> RemoveWithEmail(string email);
        Task<User> GetUserWithEmailAndPassword(string email, string password);
    }
}