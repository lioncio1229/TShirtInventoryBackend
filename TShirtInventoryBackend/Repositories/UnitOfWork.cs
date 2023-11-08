using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TshirtInventoryBackend.Data;
using TshirtInventoryBackend.DTOs;
using TshirtInventoryBackend.Models;
using TshirtInventoryBackend.Models.Request;

namespace TshirtInventoryBackend.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly JWTSettings _jwtSettings;
        private readonly IMapper _mapper;

        public UnitOfWork(DataContext context, IOptions<JWTSettings> options, IMapper mapper)
        {
            _context = context;
            _jwtSettings = options.Value;
            _mapper = mapper;

            UserRepository = new UserRepository(_context);
            RoleRepository = new RoleRepository(_context);
            BlacklistedTokenRepository = new BlacklistedTokenRepository(_context);
            TshirtRepository = new TshirtRepository(_context);
            CategoryRepository = new CategoryRepository(_context);
        }

        public IUserRepository UserRepository { get; private set; }
        public IRoleRepository RoleRepository { get; private set; }
        public IBlacklistedTokenRepository BlacklistedTokenRepository { get; private set; }
        public ITshirtRepository TshirtRepository { get; private set; }
        public ICategoryRepository CategoryRepository { get; private set; }

        public async Task<User> AddNewUser(UserAddInputs userInput)
        {
            var role = await RoleRepository.Get(userInput.RoleId);
            var newUser = new User
            {
                Email = userInput.Email,
                Password = userInput.Password,
                FullName = userInput.FullName,
                Role = role,
                IsActived = true,
            };
            UserRepository.Add(newUser);
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
            var role = await RoleRepository.Get(userInput.RoleId);
            var user = await UserRepository.GetUserWithEmail(userEmail);

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

        public string GenerateToken(string email, string role)
        {
            string jti = Guid.NewGuid().ToString();
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, email),
                        new Claim(ClaimTypes.Role, role),
                        new Claim(JwtRegisteredClaimNames.Jti, jti)
                    }
                ),
                Expires = DateTime.Now.AddMinutes(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string finalToken = tokenHandler.WriteToken(token);

            return finalToken;
        }

        public void BlacklistToken(string jti)
        {
            BlacklistedTokenRepository.Add(new BlacklistedToken
            {
                Jti = jti,
                DateBlacklisted = DateTime.Now.Date,
            });
            Complete();
        }

        public bool IsTokenValid(string jti)
        {
            var token = BlacklistedTokenRepository.Find(o => o.Jti == jti).FirstOrDefault();
            if(token == null)
            {
                return true;
            }
            return false;
        }

        public async Task<Tshirt> AddTshirt(TshirtRequest tshirt)
        {
            var category = await CategoryRepository.Get(tshirt.CategoryId);
            var tshirtRequest = new Tshirt
            {
                Name = tshirt.Name,
                Color = tshirt.Color,
                Category = category,
                Design = tshirt.Design,
                QuantityInStock = tshirt.QuantityInStock,
                Size = tshirt.Size,
                UnitPrice = tshirt.UnitPrice
            };

            var newTshirt = TshirtRepository.Add(tshirtRequest);
            Complete();

            return newTshirt;
        }

        public async Task UpdateTshirt(int id, TshirtRequest tshirtRequest)
        {
            var category = await CategoryRepository.Get(tshirtRequest.CategoryId);

            var tshirt = await TshirtRepository.Get(id);
            tshirt.Name = tshirtRequest.Name;
            tshirt.Color = tshirtRequest.Color;
            tshirt.Category = category;
            tshirt.Design = tshirtRequest.Design;
            tshirt.QuantityInStock = tshirtRequest.QuantityInStock;
            tshirt.UnitPrice = tshirtRequest.UnitPrice;
            tshirt.Size = tshirtRequest.Size;

            _context.Entry<Tshirt>(tshirt).State = EntityState.Modified;
            Complete();
        }

        public async Task<Tshirt?> RemoveTshirt(int id)
        {
            var tshirtToRemove = await TshirtRepository.Get(id);

            if (tshirtToRemove == null)
            {
                return null;
            }

            TshirtRepository.Remove(tshirtToRemove);
            Complete();

            return tshirtToRemove;
        }
    }
}
