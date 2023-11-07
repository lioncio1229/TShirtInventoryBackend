using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TshirtInventoryBackend.Data;
using TshirtInventoryBackend.Models;

namespace TshirtInventoryBackend.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly JWTSettings _jwtSettings;

        public UnitOfWork(DataContext context, IOptions<JWTSettings> options)
        {
            _context = context;
            UserRepositories = new UserRepository(_context);
            RoleRepositories = new RoleRepository(_context);
            _jwtSettings = options.Value;
        }

        public IUserRepository UserRepositories { get; private set; }
        public IRoleRepository RoleRepositories { get; private set; }

        public async Task<User> AddNewUser(UserRegistrationInputs userInput)
        {
            var role = await RoleRepositories.Get(userInput.RoleId);
            var newUser = new User
            {
                Email = userInput.Email,
                Password = userInput.Password,
                FullName = userInput.FullName,
                Role = role,
                IsActived = true,
            };
            UserRepositories.Add(newUser);
            Complete();

            return newUser;
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<User?> UpdateUser(string userEmail, UserUpdateInputs userInput)
        {
            var role = await RoleRepositories.Get(userInput.RoleId);
            var user = await UserRepositories.GetUserWithEmail(userEmail);

            if(user == null)
            {
                return null;
            }

            user.FullName = userInput.FullName;
            user.Role = role;
            user.IsActived = userInput.IsActive;
            Complete();

            return user;
        }

        public string? Authenticate(string email, string password)
        {
            var user = _context.Users
                .Include(o => o.Role)
                .FirstOrDefault(o => o.Email == email && o.Password == password);

            if (user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim(ClaimTypes.Role, user.Role.Name),
                    }
                ),
                Expires = DateTime.Now.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string finalToken = tokenHandler.WriteToken(token);

            return finalToken;
        }
    }
}
