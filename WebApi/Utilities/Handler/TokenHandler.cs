using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Contracts;
using WebApi.DTOs.Account;

namespace WebApi.Utilities.Handler
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTService:SecretKey"]));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOption = new JwtSecurityToken(issuer: _configuration["JWTService:Issuer"],
                audience: _configuration["JWTService:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(5),
                signingCredentials: signingCredentials);
            var encodedToken = new JwtSecurityTokenHandler().WriteToken(tokenOption);

            return encodedToken;
        }

        public ClaimsDTO ExtractClaimsFromJwt(string token)
        {
            if (token == "") return new ClaimsDTO(); // If the JWT token is empty, return an empty dictionary

            try
            {
                // Configure the token validation parameters
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidAudience = _configuration["JWT:Audience"],
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["JWT:Issuer"],
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]))
                };

                // Parse and validate the JWT token
                var tokenHandler = new JwtSecurityTokenHandler();
                var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

                // Extract the claims from the JWT token
                if (securityToken != null && claimsPrincipal.Identity is ClaimsIdentity identity)
                {
                    var claims = new ClaimsDTO
                    {
                        NameIdentifier = identity.FindFirst(ClaimTypes.NameIdentifier)!.Value,
                        Name = identity.FindFirst(ClaimTypes.Name)!.Value,
                        Email = identity.FindFirst(ClaimTypes.Email)!.Value
                    };

                    var roles = identity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(claim => claim.Value).ToList();
                    claims.Role = roles;

                    return claims;
                }
            }
            catch
            {
                // If an error occurs while parsing the JWT token, return an empty dictionary
                return new ClaimsDTO();
            }

            return new ClaimsDTO();
        }
    }
}
