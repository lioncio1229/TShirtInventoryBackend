﻿using TshirtInventoryBackend.Models;

namespace TshirtInventoryBackend.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepositories { get; }
        IRoleRepository RoleRepositories { get; }
        ITokenRepository TokenRepositories { get; }

        Task<User> AddNewUser(UserAddInputs userInput);

        Task<User?> UpdateUser(string userEmail, UserUpdateInputs userInput);

        string GenerateToken(string email, string password);

        void InvalidateToken(string token);
        public bool IsTokenValid(string jti);

        int Complete();
    }
}
