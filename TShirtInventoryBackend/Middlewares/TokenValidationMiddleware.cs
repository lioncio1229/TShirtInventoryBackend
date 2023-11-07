using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using TshirtInventoryBackend.Repositories;

namespace TshirtInventoryBackend.Middlewares
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUnitOfWork unitOfWork)
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var jti = GetJtiFromToken(token);

            if (!unitOfWork.IsTokenValid(jti))
            {
                var response = new
                {
                    message = "Token is invalid or has expired.",
                };

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                return;
            }

            await _next(context);
        }

        private string GetJtiFromToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (securityToken != null)
                {
                    var jtiClaim = securityToken.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Jti);
                    if (jtiClaim != null)
                    {
                        return jtiClaim.Value;
                    }
                }
            }
            catch (Exception)
            {
                // Handle any exceptions related to token decoding or invalid tokens here
                // For example, log the error or return null for an invalid token
            }

            return null;
        }
    }
}
